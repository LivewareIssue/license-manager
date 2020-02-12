Imports Eureka.Common.LicenseManager.EurekaLicenseProvider

Public Class LicenseInformation
    Public Sub New(license As RuntimeLicense)
        InitializeComponent()

        AccountNumberTextBox.Text = license.AccountNumber
        LicensedAddonNameTextBox.Text = license.LicensedAddonName
        ExpiryDateTextBox.Text = license.ExpiryDate

        If license.IsExpired Then
            Text = "License (Expired)"
        End If
    End Sub
End Class