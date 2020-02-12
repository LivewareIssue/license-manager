# license-manager
Sage 200 add-on for license retrieval and validation.

## Protecting a Form (or Control)

* Add a reference to the Eureka.Common.LicenseManager.dll assembly
    *	Ensure that the Copy Local property is set to False
*	Declare the license provider for the form or control that you wish to protect by adding the System.ComponentModel.LicenseProviderAttribute attribute its class
    *	The type Eureka.Common.LicenseManager.EurekaLicenseProvider must be given as the argument of the LicenseProvider-attribute’s constructor, i.e.

![1st Image](/images/1.png)

*	Associate the form with an add-on by adding the Eureka.Common.LicenseManager.LicensedAddonAttribute attribute to its class
    * The name argument given to the LicensedAddon’s constructor must be an exact match with the Name field of a record in the EurekaCommonAddon table, e.g.

![2nd Image](/images/2.png)

* Add a field of type System.ComponentModel.License to the form to store the result of the license check.
*	In the form’s Load event-handler, call the EurekaLicenseProvider.FetchLicense method, giving Me and a variable of type License as arguments.
    *	The result of this call is a Boolean that represents whether or not a valid license was fetched.
    *	If the call succeeded, the given license variable will have been set to fetched license.
    *	The integrity and authenticity of any license fetched is guaranteed.
    *	A fetched license may be expired, but it will always be within its grace-period (7 days after its expiry date)

![3rd Image](/images/3.png)

*	(**optional**) The license information may be displayed by creating an instance of the Eureka.Common.LicenseManager.LicenseInformation form, passing the fetched license as the argument to its constructor.
*	When deploying a protected add-on, in the add-on packager, add Eureka.Common.LicenseManager as a required application (guid: f723dbdc-de0f-4d75-969e-0aad874203d1, any version ≥ 1.3)

![4th Image](/images/4.png)
