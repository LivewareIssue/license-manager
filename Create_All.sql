IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=N'EurekaLicense')
	BEGIN
		CREATE TABLE dbo.EurekaLicense (
			EurekaLicenseID BIGINT,
			LicensedAddonName VARCHAR(60) NOT NULL,
			ExpiryDate Date NOT NULL,
			LicenseKey VARCHAR(MAX) NOT NULL

			CONSTRAINT PK_EurekaLicense PRIMARY KEY (EurekaLicenseID)
		)
	END
GO