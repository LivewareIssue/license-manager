Imports Eureka.Common.LicenseManager.EurekaLicenseProvider

Public Class ExpiredLicense
    Public Sub New(license As RuntimeLicense)
        InitializeComponent()

        PromptLabel.Text = $"Your license expired on {license.ExpiryDate.ToShortDateString}.
        
        This add-on will continue working for {license.RemainingGracePeriod.Days} day(s), after which it will be deactivated.

        Please renew you subscription, or contact Eureka for support."
    End Sub
End Class