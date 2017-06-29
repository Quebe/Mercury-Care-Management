/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugAdministration' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugAdministration]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugAdministration]

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

				DELETE FROM edi.DrugAdministration

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'ROUTES.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)
				

				INSERT INTO edi.DrugAdministration (DrugListingId, DrugAdministrationRoute, DrugAdministrationRouteName)

					SELECT 

							SUBSTRING (RowContent, 1, 7) AS DrugListingId,
		
							SUBSTRING (RowContent, 9, 3) AS DrugAdministrationRoute,

							SUBSTRING (RowContent, 13, 60) AS DrugAdministrationRouteName

						FROM 
	
							edi.BulkInsertFlatText

						WHERE SUBSTRING (RowContent, 1, 7) NOT LIKE '%[A-Z]%'

	
				
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugAdministration] TO PUBLIC
GO          
*/