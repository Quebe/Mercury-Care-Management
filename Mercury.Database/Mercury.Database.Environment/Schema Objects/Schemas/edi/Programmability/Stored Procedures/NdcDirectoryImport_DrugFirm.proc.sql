/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugFirm' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugFirm]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugFirm]

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

				DELETE FROM edi.DrugFirm

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'FIRMS.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugFirm (DrugFirmId, DrugFirmName, 
				
						AddressLine1, AddressLine2, AddressCity, AddressState, AddressZipCode, AddressZipPlus4,  
						
						ProvinceName, CountryName)

					SELECT 

							SUBSTRING (RowContent, 1, 6) AS DrugFirmId,
		
							SUBSTRING (RowContent, 8, 60) AS DrugFirmName,

							SUBSTRING (RowContent, 115, 40) AS AddressLine1,

							SUBSTRING (RowContent, 156, 9) AS AddressLine2,

							SUBSTRING (RowContent, 207, 25) AS AddressCity,

							SUBSTRING (RowContent, 238, 2) AS AddressState,

							SUBSTRING (RowContent, 241, 5) AS AddressZipCode,

							SUBSTRING (RowContent, 256, 4) AS AddressZipPlus4,

							SUBSTRING (RowContent, 251, 30) AS ProvinceName,

							SUBSTRING (RowContent, 282, 40) AS CountryName

						FROM 
	
							edi.BulkInsertFlatText		
		

				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				DELETE FROM edi.DrugFirm WHERE DrugFirmId LIKE '%[A-Z]%'
	
	
				---- UPDATE EXISTING ITEMS	
		
				--UPDATE dbo.DrugFirm

				--	SET 
	
				--		DrugFirmName = DataSource.DrugFirmName,
		
				--		ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
				--		ModifiedAccountId = DataSource.ModifiedAccountId,
		
				--		ModifiedAccountName = DataSource.ModifiedAccountName,
		
				--		ModifiedDate = DataSource.ModifiedDate
	
				--	FROM 
	
				--		dbo.DrugFirm AS DataDestination
		
				--			JOIN edi.DrugFirm AS DataSource
			
				--				ON DataDestination.DrugFirm = DataSource.DrugFirm
				
				--	WHERE 
	
				--		(DataDestination.DrugFirmName <> DataSource.DrugFirmName)
		

				---- INSERT NEW ITEMS
	
				--INSERT INTO dbo.DrugFirm (DrugFirm, DrugFirmName) 

				--	SELECT DISTINCT DrugFirm, DrugFirmName
		
				--		FROM edi.DrugFirm
		
				--		WHERE DrugFirm NOT IN (SELECT DrugFirm FROM dbo.DrugFirm)


      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugFirm] TO PUBLIC
GO          
*/