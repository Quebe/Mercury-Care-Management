/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugListing' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugListing]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugListing]

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


				DELETE FROM edi.DrugListing

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'LISTINGS.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugListing (DrugListingId, DrugFirmId, 
				
						ProductCode, Strength, DrugUnitType, ProductName)
						
					SELECT 

							SUBSTRING (RowContent, 001, 07) AS DrugListingId,
		
							SUBSTRING (RowContent, 009, 06) AS DrugFirmId,

							SUBSTRING (RowContent, 016, 04) AS ProductCode,

							SUBSTRING (RowContent, 021, 10) AS Strength,

							SUBSTRING (RowContent, 032, 10) AS DrugUnitType,

							SUBSTRING (RowContent, 045, 60) AS ProductName

						FROM 
	
							edi.BulkInsertFlatText		
		

				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				DELETE FROM edi.DrugListing WHERE DrugListingId LIKE '%[A-Z]%'
	
	
				---- UPDATE EXISTING ITEMS	
		
				--UPDATE dbo.DrugListing

				--	SET 
	
				--		DrugListingName = DataSource.DrugListingName,
		
				--		ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
				--		ModifiedAccountId = DataSource.ModifiedAccountId,
		
				--		ModifiedAccountName = DataSource.ModifiedAccountName,
		
				--		ModifiedDate = DataSource.ModifiedDate
	
				--	FROM 
	
				--		dbo.DrugListing AS DataDestination
		
				--			JOIN edi.DrugListing AS DataSource
			
				--				ON DataDestination.DrugListing = DataSource.DrugListing
				
				--	WHERE 
	
				--		(DataDestination.DrugListingName <> DataSource.DrugListingName)
		

				---- INSERT NEW ITEMS
	
				--INSERT INTO dbo.DrugListing (DrugListing, DrugListingName) 

				--	SELECT DISTINCT DrugListing, DrugListingName
		
				--		FROM edi.DrugListing
		
				--		WHERE DrugListing NOT IN (SELECT DrugListing FROM dbo.DrugListing)


      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugListing] TO PUBLIC
GO          
*/