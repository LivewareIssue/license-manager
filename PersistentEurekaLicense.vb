Imports System
Imports System.Collections
Imports System.ComponentModel
Imports Sage.Common.Data
Imports Sage.ObjectStore

Namespace Eureka.Common.LicenseManager
    
    <Sage.ObjectStore.Builder.PersistentObjectAttribute(TableName:="EurekaLicense", Name:="EurekaLicense"),  _
     System.Runtime.Serialization.DataContractAttribute(IsReference:=true, [Namespace]:="http://schemas.sage.com/sage200/2011/")>  _
    Public Class PersistentEurekaLicense
        Inherits Sage.ObjectStore.PersistentObject
        
        '''<summary>
        '''EurekaLicenseID Field Name
        '''</summary>
        Public Const FIELD_EUREKALICENSE As String = "EurekaLicense"
        
        Protected _EurekaLicense As Sage.ObjectStore.Field
        
        '''<summary>
        '''LicensedAddonName Field Name
        '''</summary>
        Public Const FIELD_LICENSEDADDONNAME As String = "LicensedAddonName"
        
        '''<summary>
        '''The name of the licensed add-on
        '''</summary>
        Protected _LicensedAddonName As Sage.ObjectStore.Field
        
        '''<summary>
        '''ExpiryDate Field Name
        '''</summary>
        Public Const FIELD_EXPIRYDATE As String = "ExpiryDate"
        
        '''<summary>
        '''The date on which the license expires
        '''</summary>
        Protected _ExpiryDate As Sage.ObjectStore.Field
        
        '''<summary>
        '''LicenseKey Field Name
        '''</summary>
        Public Const FIELD_LICENSEKEY As String = "LicenseKey"
        
        Protected _LicenseKey As Sage.ObjectStore.Field
        
        Public Sub New()
            MyBase.New
        End Sub
        
        Public Sub New(ByVal connectionData As Sage.ObjectStore.ConnectionData, ByVal metaDataType As System.Type)
            MyBase.New(connectionData, metaDataType)
        End Sub
        
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden),  _
         Sage.ObjectStore.Builder.MetaDataFieldAttribute(ColumnName:="EurekaLicenseID", IsReadOnly:=true, AllowOverwrite:=Sage.ObjectStore.Builder.AllowOverwriteType.Equal, IsPrimaryKey:=true, IsIndexed:=true, IsUnique:=true, DbType:=System.Data.DbType.Int64, Precision:=11)>  _
        Public Overridable ReadOnly Property EurekaLicense() As Sage.Common.Data.DbKey
            Get
                Return Me._EurekaLicense.GetDbKey
            End Get
        End Property
        
        '''<summary>
        '''The name of the licensed add-on
        '''</summary>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden),  _
         Sage.ObjectStore.Builder.MetaDataFieldAttribute(Description:="The name of the licensed add-on", DbType:=System.Data.DbType.[String], Precision:=60)>  _
        Public Overridable Property LicensedAddonName() As String
            Get
                Return Me._LicensedAddonName.GetString
            End Get
            Set
                Me._LicensedAddonName.Value = value
            End Set
        End Property
        
        '''<summary>
        '''The date on which the license expires
        '''</summary>
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden),  _
         Sage.ObjectStore.Builder.MetaDataFieldAttribute(Description:="The date on which the license expires", IsNullable:=true, DbType:=System.Data.DbType.[Date])>  _
        Public Overridable Property ExpiryDate() As Date
            Get
                Return Me._ExpiryDate.GetDateTime
            End Get
            Set
                Me._ExpiryDate.Value = value
            End Set
        End Property
        
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden),  _
         Sage.ObjectStore.Builder.MetaDataFieldAttribute(DbType:=System.Data.DbType.[String], Precision:=2147483647)>  _
        Public Overridable Property LicenseKey() As String
            Get
                Return Me._LicenseKey.GetString
            End Get
            Set
                Me._LicenseKey.Value = value
            End Set
        End Property
    End Class
    
    <System.Runtime.Serialization.DataContractAttribute(IsReference:=true, [Namespace]:="http://schemas.sage.com/sage200/2011/")>  _
    Public Class PersistentEurekaLicenses
        Inherits Sage.ObjectStore.PersistentObjectCollection
        
        Public Sub New()
            MyBase.New
        End Sub
        
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
        Public Shadows Overridable Default Property Item(ByVal index As Integer) As PersistentEurekaLicense
            Get
                Return CType(MyBase.Item(index),PersistentEurekaLicense)
            End Get
            Set
                MyBase.Item(index) = value
            End Set
        End Property
        
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
        Public Overrides Property Owner() As Sage.ObjectStore.PersistentObject
            Get
                If (Me.Query.Owner Is Nothing) Then
                    Me.Query.Owner = New PersistentEurekaLicense()
                End If
                Return Me.Query.Owner
            End Get
            Set
                Throw New System.ArgumentException("You are not allowed to change the Owner property of a collection")
            End Set
        End Property
        
        <System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)>  _
        Public Shadows Overridable ReadOnly Property First() As PersistentEurekaLicense
            Get
                Return CType(MyBase.First,PersistentEurekaLicense)
            End Get
        End Property
    End Class
End Namespace
