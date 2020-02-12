Imports System.ComponentModel
Imports System.Data.SqlClient
Imports Eureka.Common.LicenseManager.Eureka.Common.LicenseManager
Imports Sage.Common.Data
Imports System.Security.Cryptography
Imports System.Linq
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO
Imports Sage.Accounting

Public Class EurekaLicenseProvider
    Inherits LicenseProvider

    'Database-Connection Settings
    Private Const Host As String = "remote.eureka-bsl.co.uk"
    Private Const Database As String = "Eureka"
    Private Const Table As String = "EurekaSageAccountSubscriptions"
    Private Const UserID As String = "EurekaSubscriber"

    ''' <summary>
    ''' The period of time within which an add-on is permitted to continue operating after its license has expired.
    ''' </summary>
    Public Shared ReadOnly GracePeriod As New TimeSpan(days:=7, hours:=0, minutes:=0, seconds:=0)

    ''' <summary>
    ''' Gets the Sage 200 account-number associated with the current user.
    ''' </summary>
    ''' <returns>A Sage 200 account-number represented as an 8-digit string.</returns>
    Private Shared ReadOnly Property AccountNumber As String
        Get
            Return Application.Licence.CustomerEntitlement.CustomerIdentifier
        End Get
    End Property

    ''' <summary>
    ''' Gets the name of any add-on associated with the given type.
    ''' </summary>
    ''' <param name="type">A type which may be associated with an add-on.</param>
    ''' <returns>If the given type is associated with an add-on, the add-on's name; otherwise, nothing.</returns>
    Private Shared Function GetLicensedAddonName(type As Type) As String
        Dim licensedAddonAttribute As Attribute = Attribute.GetCustomAttribute(type, GetType(LicensedAddonAttribute))
        If IsNothing(licensedAddonAttribute) Then
            Return Nothing
        Else
            Return CType(licensedAddonAttribute, LicensedAddonAttribute).Name
        End If
    End Function

    ''' <summary>
    ''' Attempts to fetch a license for the given add-on; succeedes if the license is valid and within its grace period.
    ''' </summary>
    ''' <param name="licensedAddonName">The name of the add-on for which a license is to be fetched.</param>
    ''' <param name="localLicense">Set to the locally-stored license if it exists and is valid; otherwise, remains unaltered.</param>
    ''' <returns>True if a license is fetched successfuly; otherwise, False.</returns>
    Private Shared Function FetchLocalLicense(licensedAddonName As String, ByRef localLicense As RuntimeLicense) As Boolean
        Dim localLicenseRecords As New EurekaLicenses
        localLicenseRecords.Query.Filters.Add(New Sage.ObjectStore.Filter(EurekaLicense.FIELD_LICENSEDADDONNAME, FilterOperator.Equal, licensedAddonName))

        If localLicenseRecords.Count = 1 Then
            Dim localLicenseRecord As EurekaLicense = localLicenseRecords.First
            Dim prospectiveLicense As New RuntimeLicense(AccountNumber, localLicenseRecord.LicensedAddonName, localLicenseRecord.ExpiryDate, localLicenseRecord.LicenseKey)

            If prospectiveLicense.IsValid And prospectiveLicense.IsWithinGracePeriod Then
                localLicense = prospectiveLicense
                Return True
            End If
        End If

        Return False
    End Function

    ''' <summary>
    ''' Attempts to fetch a license from the remote licensing server; succeedes if the license is valid and within its grace period.
    ''' </summary>
    ''' <param name="licensedAddonName">The name of the add-on for which a license is to be fetched.</param>
    ''' <param name="remoteLicense">Set to the license fetched from the remote server if it exists and is valid; otherwise, remains unaltered.</param>
    ''' <param name="innerException">Set to any exception which occurs when attempting to fetch the remote license.</param>
    ''' <returns>True if a license is fetched successfuly; otherwise, False.</returns>
    Private Shared Function FetchRemoteLicense(licensedAddonName As String, ByRef remoteLicense As RuntimeLicense, ByRef innerException As Exception) As Boolean
        Try
            Using connection As New SqlConnection($"Data Source={Host};Network Library=DBMSSOCN;Initial Catalog={Database};User ID={UserID};Password={Settings.Default.Password};")
                connection.Open()

                Dim query As New SqlCommand($"SELECT TOP(1) ExpiryDate, LicenseKey FROM dbo.{Table} WHERE LicensedAddonName = '{licensedAddonName}' AND SageAccountNumber = '{AccountNumber}'", connection)
                Dim queryResults As SqlDataReader = query.ExecuteReader()

                If queryResults.HasRows Then
                    queryResults.Read()

                    Dim prospectiveLicense As New RuntimeLicense(AccountNumber, licensedAddonName, queryResults("ExpiryDate"), queryResults("LicenseKey"))
                    If prospectiveLicense.IsValid And prospectiveLicense.IsWithinGracePeriod Then
                        remoteLicense = prospectiveLicense
                        Return True
                    Else
                        innerException = New Exception("Remote license is invalid or expired")
                    End If
                End If

                queryResults.Close()
                connection.Close()
            End Using
        Catch ex As SqlException
            innerException = New Exception("Unable to contact licensing server")
        End Try

        Return False
    End Function

    Private Shared Sub UpdateLocalLicense(license As RuntimeLicense)
        Dim localLicenseRecords As New EurekaLicenses
        localLicenseRecords.Query.Filters.Add(New Sage.ObjectStore.Filter(EurekaLicense.FIELD_LICENSEDADDONNAME, FilterOperator.Equal, license.LicensedAddonName))

        Dim localLicenseRecord As EurekaLicense

        If localLicenseRecords.Count = 1 Then
            localLicenseRecord = localLicenseRecords.First
        Else
            localLicenseRecord = New EurekaLicense With {
                .LicensedAddonName = license.LicensedAddonName
            }
        End If

        localLicenseRecord.ExpiryDate = license.ExpiryDate
        localLicenseRecord.LicenseKey = license.LicenseKey
        localLicenseRecord.Update()
    End Sub

    ''' <summary>
    ''' Requests that a license be issued for the given component; A license is issued if the current user is subscribed to the add-on associated with the given component.
    ''' </summary>
    ''' <typeparam name="T">The type of the component for which a license is being requested.</typeparam>
    ''' <param name="instance">The component for which a license is being requested.</param>
    ''' <param name="license">Set to any license that is issued; otherwise, remains unaltered.</param>
    ''' <returns>True if a license is issued; otherwise, False.</returns>
    Public Shared Function FetchLicense(Of T)(instance As T, ByRef license As License) As Boolean
        Try
            license = ComponentModel.LicenseManager.Validate(GetType(T), instance)
            Return True
        Catch ex As LicenseException
            If IsNothing(ex.InnerException) Then
                MsgBox($"{ex.Message}. Please contact Eureka for support.", MsgBoxStyle.OkOnly, "Licensing Error")
            Else
                MsgBox($"{ex.InnerException.Message}. Please contact Eureka for support.", MsgBoxStyle.OkOnly, "Licensing Error")
            End If

            Return False
        End Try
    End Function

    ''' <summary>
    ''' Requests that a license be issued for the given component; A license is issued if the current user is subscribed to the add-on associated with the given component.
    ''' </summary>
    ''' <param name="context">Indicates whether a license is being requested at either run-time or design-time.</param>
    ''' <param name="type">The type of the component for which a license is being requested.</param>
    ''' <param name="instance">The component for which a license is being requested.</param>
    ''' <param name="allowExceptions">Indicates whether or not exceptions should be supressed.</param>
    ''' <returns></returns>
    Public Overrides Function GetLicense(context As LicenseContext, type As Type, instance As Object, allowExceptions As Boolean) As License
        If context.UsageMode = LicenseUsageMode.Designtime Then
            Return New DesigntimeLicense
        End If

        Dim licensedAddonName As String = GetLicensedAddonName(type)
        If IsNothing(licensedAddonName) Then
            Call New LicenseException(type, instance, "Unidentified add-on.").ThrowWhen(allowExceptions)
            Return Nothing
        End If

        Dim localLicense As RuntimeLicense = Nothing
        Dim remoteLicense As RuntimeLicense = Nothing
        Dim innerException As Exception = Nothing

        If FetchLocalLicense(licensedAddonName, localLicense) Then
            If localLicense.IsExpired Then
                If FetchRemoteLicense(licensedAddonName, remoteLicense, innerException) Then
                    If remoteLicense.IsExpired Then
                        Call New ExpiredLicense(localLicense).ShowDialog()
                    End If

                    UpdateLocalLicense(remoteLicense)
                    Return remoteLicense
                Else
                    Call New ExpiredLicense(localLicense).ShowDialog()
                    Return localLicense
                End If
            Else
                Return localLicense
            End If
        Else
            If FetchRemoteLicense(licensedAddonName, remoteLicense, innerException) Then
                If remoteLicense.IsExpired Then
                    Call New ExpiredLicense(remoteLicense).ShowDialog()
                End If

                UpdateLocalLicense(remoteLicense)
                Return remoteLicense
            Else
                Call New LicenseException(type, instance, "Unable to issue license", innerException).ThrowWhen(allowExceptions)
                Return Nothing
            End If
        End If
    End Function

    Public Class RuntimeLicense
        Inherits License

        ''' <summary>
        ''' The licensing-authority's (Eureka Business Solutions Ltd.) public DSA key, used to authenticate the origin of the license and verify its integrity.
        ''' </summary>
        Private Shared ReadOnly publicKey As String = "<DSAKeyValue><P>pxtT1zUmluJB+CqmxiIfqiH3YvKsb7SLNL4CFaPPBgtwbqpOg912yJeSjwcIY3aqe36xhAHn51b7iO2aiMdUFM8RmU1nuOxjEDYYBVbleVH8FYe8BQ9f9ZAw3YKnl7LppOZddl4IsE50sp3znNC+f5MbP06qeFJpEAarqqiWJNc=</P><Q>63orRAJ1v8j3NULdjWXD4RTQwrk=</Q><G>YqZ/TFzJhsd0Huv8I3Fbo9a/lCTy75ScAkM16nnO1x44fZaN9Bb+NeFmaGd7PNCb0zbj3h/nrktGq5W0D7v4Yzx51dgoYGgYFK5FHNs3OhdSN/eMtGom9vjrwsHG0IM57L+X1/7WUjssznPc0CYb7M0ysgbHD5QDaBZyN4zf+Io=</G><Y>Um0QY5n5m4C+qNWvJSk3891aZdad465ootrzT2BtFTtIdN1h0XSUJOVcRYXcT40EzNe1nDTQJ0c2cqvhQzQPF9jIKzsA3fts7azmftDY1jKo2oIc//LtUCl76wiqokb0z6RBFtHkujSVOvUKknFW8hYhcYIn3qRqwY5eK8KmnBM=</Y><Seed>hK2Sc3WrBrdL1q/zr5TrhXH3yEI=</Seed><PgenCounter>Lg==</PgenCounter></DSAKeyValue>"

        Protected Friend ReadOnly Property AccountNumber As String
        Protected Friend ReadOnly Property LicensedAddonName As String
        Protected Friend ReadOnly Property ExpiryDate As Date
        Protected Friend ReadOnly Property Signature As String

        Protected Friend Sub New(accountNumber As String, licensedAddonName As String, expiryDate As Date, signature As String)
            Me.AccountNumber = accountNumber
            Me.LicensedAddonName = licensedAddonName
            Me.ExpiryDate = expiryDate
            Me.Signature = signature
        End Sub

        Public ReadOnly Property IsExpired As Boolean
            Get
                Return Date.Today > ExpiryDate
            End Get
        End Property

        Public ReadOnly Property IsWithinGracePeriod As Boolean
            Get
                Return Date.Today < ExpiryDate + GracePeriod
            End Get
        End Property

        Public ReadOnly Property RemainingGracePeriod As TimeSpan
            Get
                Return (ExpiryDate + GracePeriod) - Today
            End Get
        End Property

        ''' <summary>
        ''' The license's key, derived from its expiry date and the add-on and Sage 200 account with which it is associated.
        ''' </summary>
        ''' <returns>A hash of the license, signed by the licensing authority, represented as a base-64 encoded string.</returns>
        Public Overrides ReadOnly Property LicenseKey As String
            Get
                Return Signature
            End Get
        End Property

        ''' <summary>
        ''' Verifies the license's integrity and authenticity.
        ''' </summary>
        ''' <returns>True if the license is valid; otherwise, False/</returns>
        Public Overloads ReadOnly Property IsValid() As Boolean
            Get
                Dim dsa As DSA = DSA.Create
                dsa.FromXmlString(publicKey)

                Return Enumerable.Zip(ComputeLicenseHash().ChunksOf(20), Convert.FromBase64String(LicenseKey).ChunksOf(40), AddressOf dsa.VerifySignature).All(Function(isVerified) isVerified)
            End Get
        End Property

        ''' <summary>
        ''' Computes a hash of the license's expiry date, the name of the associated add-on, and the assocated Sage 200 account-number.
        ''' </summary>
        ''' <returns>The computed hash as a byte-array.</returns>
        Private Function ComputeLicenseHash() As Byte()
            Dim binaryFormatter As New BinaryFormatter

            Using stream As New MemoryStream
                binaryFormatter.Serialize(stream, AccountNumber)
                binaryFormatter.Serialize(stream, LicensedAddonName)
                binaryFormatter.Serialize(stream, ExpiryDate)

                Return stream.ToArray
            End Using
        End Function

        Public Overrides Sub Dispose()

        End Sub
    End Class

    Protected Friend Class DesigntimeLicense
        Inherits License

        Public Overrides ReadOnly Property LicenseKey As String
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides Sub Dispose()

        End Sub
    End Class
End Class
