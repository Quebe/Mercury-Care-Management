/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugAdministrationRoute' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugAdministrationRoute]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugAdministrationRoute]

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

				DELETE FROM edi.DrugAdministrationRoute

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'TBLROUTE.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				INSERT INTO edi.DrugAdministrationRoute (DrugAdministrationRoute, DrugAdministrationRouteName)

					SELECT 

							SUBSTRING (RowContent, 1, 3) AS DrugAdministrationRoute,
		
							SUBSTRING (RowContent, 5, 60) AS DrugAdministrationRouteName

						FROM 
	
							edi.BulkInsertFlatText

						WHERE SUBSTRING (RowContent, 1, 3) NOT LIKE '%[A-Z]%'

	
				-- UPDATE EXISTING ITEMS	
		
				UPDATE dbo.DrugAdministrationRoute

					SET 
	
						DrugAdministrationRouteName = DataSource.DrugAdministrationRouteName,
		
						ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
						ModifiedAccountId = DataSource.ModifiedAccountId,
		
						ModifiedAccountName = DataSource.ModifiedAccountName,
		
						ModifiedDate = DataSource.ModifiedDate
	
					FROM 
	
						dbo.DrugAdministrationRoute AS DataDestination
		
							JOIN edi.DrugAdministrationRoute AS DataSource
			
								ON DataDestination.DrugAdministrationRoute = DataSource.DrugAdministrationRoute
				
					WHERE 
	
						(DataDestination.DrugAdministrationRouteName <> DataSource.DrugAdministrationRouteName)
		

				-- INSERT NEW ITEMS
	
				INSERT INTO dbo.DrugAdministrationRoute (DrugAdministrationRoute, DrugAdministrationRouteName) 

					SELECT DISTINCT DrugAdministrationRoute, DrugAdministrationRouteName
		
						FROM edi.DrugAdministrationRoute
		
						WHERE DrugAdministrationRoute NOT IN (SELECT DrugAdministrationRoute FROM dbo.DrugAdministrationRoute)


      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugAdministrationRoute] TO PUBLIC
GO          
*/