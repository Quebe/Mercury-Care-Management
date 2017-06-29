/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_NationalDrugCode' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_NationalDrugCode]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_NationalDrugCode]

    /* STORED PROCEDURE - INPUTS (BEGIN) */
    
		/* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN

      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */


				DELETE FROM edi.NationalDrugCode
				
				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.NationalDrugCode (NationalDrugCode, DrugFirmId, DrugProductCode, DrugPackageCode, 
				
						 DrugName, Strength, DrugUnitType, PackageSize, PackageType)
						
					SELECT DISTINCT

							CAST (REPLACE (RIGHT (DrugListing.DrugFirmId, 5) + DrugListing.ProductCode + DrugPackage.PackageCode, '*', '0') AS CHAR (011)) AS NationalDrugCode,
	
							CAST (DrugListing.DrugFirmId AS CHAR (006)),

							CAST (DrugListing.ProductCode AS CHAR (004)),

							CAST (DrugPackage.PackageCode AS CHAR (002)),

							CAST (DrugListing.ProductName AS VARCHAR (060)),

							CAST (DrugListing.Strength AS VARCHAR (010)),

							CAST (DrugListing.DrugUnitType AS VARCHAR (020)),

							CAST (DrugPackage.PackageSize AS VARCHAR (030)),

							CAST (DrugPackage.PackageType AS VARCHAR (030))

						FROM 
	
							edi.DrugListing AS DrugListing
		
								JOIN edi.DrugPackage AS DrugPackage
			
									ON DrugListing.DrugListingId = DrugPackage.DrugListingId
									
						WHERE DrugPackage.PackageCode <> '**'
		

				-- DELETE DUPLICATIONS THAT DO NOT MATCH

				DELETE FROM edi.NationalDrugCode

					WHERE NationalDrugCode IN 

						(SELECT NationalDrugCode FROM edi.NationalDrugCode GROUP BY NationalDrugCode HAVING (COUNT (1) > 1))


				INSERT INTO dbo.NationalDrugCode (NationalDrugCode, DrugFirmId, DrugProductCode, DrugPackageCode, 

							DrugName, Strength, DrugUnitType, PackageSize, PackageType)

					SELECT
	
							NationalDrugCode.NationalDrugCode, NationalDrugCode.DrugFirmId, NationalDrugCode.DrugProductCode, NationalDrugCode.DrugPackageCode, 

							NationalDrugCode.DrugName, NationalDrugCode.Strength, NationalDrugCode.DrugUnitType, NationalDrugCode.PackageSize, NationalDrugCode.PackageType
	
	
						FROM 
		
							edi.NationalDrugCode AS NationalDrugCode
		
								LEFT JOIN dbo.NationalDrugCode AS Destination
				
									ON NationalDrugCode.NationalDrugCode = Destination.NationalDrugCode
					
						WHERE Destination.NationalDrugCode IS NULL
		

    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_NationalDrugCode] TO PUBLIC
GO          
*/