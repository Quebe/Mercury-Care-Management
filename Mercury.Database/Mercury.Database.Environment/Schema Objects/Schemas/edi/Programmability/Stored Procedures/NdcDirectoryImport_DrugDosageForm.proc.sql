/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugDosageForm' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugDosageForm]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugDosageForm]

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

				DELETE FROM edi.DrugDosageForm

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'TBLDOSAG.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugDosageForm (DrugDosageForm, DrugDosageFormName)

				SELECT 

						SUBSTRING (RowContent, 1, 3) AS DrugDosageForm,
		
						SUBSTRING (RowContent, 5, 60) AS DrugDosageFormName

					FROM 
	
						edi.BulkInsertFlatText
		
		
				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				DELETE FROM edi.DrugDosageForm WHERE DrugDosageForm LIKE '%[A-Z]%'
	
	
				-- UPDATE EXISTING ITEMS	
		
				UPDATE dbo.DrugDosageForm

					SET 
	
						DrugDosageFormName = DataSource.DrugDosageFormName,
		
						ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
						ModifiedAccountId = DataSource.ModifiedAccountId,
		
						ModifiedAccountName = DataSource.ModifiedAccountName,
		
						ModifiedDate = DataSource.ModifiedDate
	
					FROM 
	
						dbo.DrugDosageForm AS DataDestination
		
							JOIN edi.DrugDosageForm AS DataSource
			
								ON DataDestination.DrugDosageForm = DataSource.DrugDosageForm
				
					WHERE 
	
						(DataDestination.DrugDosageFormName <> DataSource.DrugDosageFormName)
		

				-- INSERT NEW ITEMS
	
				INSERT INTO dbo.DrugDosageForm (DrugDosageForm, DrugDosageFormName) 

					SELECT DISTINCT DrugDosageForm, DrugDosageFormName
		
						FROM edi.DrugDosageForm
		
						WHERE DrugDosageForm NOT IN (SELECT DrugDosageForm FROM dbo.DrugDosageForm)


      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugDosageForm] TO PUBLIC
GO          
*/