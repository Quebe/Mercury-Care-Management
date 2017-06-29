/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugPackage' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugPackage]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugPackage]

    /* STORED PROCEDURE - INPUTS (BEGIN) */

		@filePath VARCHAR (255)
    
		/* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN

      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

				DECLARE @bulkInsertStatement AS VARCHAR (8000)
				
        /* LOCAL VARIABLES ( END ) */


				DELETE FROM edi.DrugPackage

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'PACKAGES.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugPackage (DrugListingId, PackageCode, PackageSize, PackageType)
						
					SELECT 

							SUBSTRING (RowContent, 001, 07) AS DrugListingId,
		
							SUBSTRING (RowContent, 009, 02) AS PackageCode,

							SUBSTRING (RowContent, 012, 25) AS PackageSize,

							SUBSTRING (RowContent, 038, 25) AS PackageType

						FROM 
	
							edi.BulkInsertFlatText		
		

				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				DELETE FROM edi.DrugPackage WHERE DrugListingId LIKE '%[A-Z]%'

				DELETE FROM edi.DrugPackage WHERE PackageCode LIKE '%[A-Z]%'
	
	
				---- UPDATE EXISTING ITEMS	
		
				--UPDATE dbo.DrugPackage

				--	SET 
	
				--		DrugPackageName = DataSource.DrugPackageName,
		
				--		ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
				--		ModifiedAccountId = DataSource.ModifiedAccountId,
		
				--		ModifiedAccountName = DataSource.ModifiedAccountName,
		
				--		ModifiedDate = DataSource.ModifiedDate
	
				--	FROM 
	
				--		dbo.DrugPackage AS DataDestination
		
				--			JOIN edi.DrugPackage AS DataSource
			
				--				ON DataDestination.DrugPackage = DataSource.DrugPackage
				
				--	WHERE 
	
				--		(DataDestination.DrugPackageName <> DataSource.DrugPackageName)
		

				---- INSERT NEW ITEMS
	
				--INSERT INTO dbo.DrugPackage (DrugPackage, DrugPackageName) 

				--	SELECT DISTINCT DrugPackage, DrugPackageName
		
				--		FROM edi.DrugPackage
		
				--		WHERE DrugPackage NOT IN (SELECT DrugPackage FROM dbo.DrugPackage)

      /* STORED PROCEDURE ( END ) */

    END 

GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugPackage] TO PUBLIC
GO          
*/