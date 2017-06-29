/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugUnitType' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugUnitType]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugUnitType]

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

				DELETE FROM edi.DrugUnitType

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'TBLUNIT.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugUnitType (DrugUnitType, DrugUnitTypeName)

					SELECT 

							SUBSTRING (RowContent, 1, 15) AS DrugUnitType,
		
							SUBSTRING (RowContent, 16, 60) AS DrugUnitTypeName

						FROM 
	
							edi.BulkInsertFlatText
		
		
				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
	
	
				-- UPDATE EXISTING ITEMS	
		
				UPDATE dbo.DrugUnitType

					SET 
	
						DrugUnitTypeName = DataSource.DrugUnitTypeName,
		
						ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
						ModifiedAccountId = DataSource.ModifiedAccountId,
		
						ModifiedAccountName = DataSource.ModifiedAccountName,
		
						ModifiedDate = DataSource.ModifiedDate
	
					FROM 
	
						dbo.DrugUnitType AS DataDestination
		
							JOIN edi.DrugUnitType AS DataSource
			
								ON DataDestination.DrugUnitType = DataSource.DrugUnitType
				
					WHERE 
	
						(DataDestination.DrugUnitTypeName <> DataSource.DrugUnitTypeName)
		

				-- INSERT NEW ITEMS
	
				INSERT INTO dbo.DrugUnitType (DrugUnitType, DrugUnitTypeName) 

					SELECT DISTINCT DrugUnitType, DrugUnitTypeName
		
						FROM edi.DrugUnitType
		
						WHERE DrugUnitType NOT IN (SELECT DrugUnitType FROM dbo.DrugUnitType)

						
      /* STORED PROCEDURE ( END ) */

    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugUnitType] TO PUBLIC
GO          
*/