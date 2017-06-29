/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'NdcDirectoryImport_DrugFormulation' AND type = 'P'))
  DROP PROCEDURE edi.[NdcDirectoryImport_DrugFormulation]
GO      
*/

CREATE PROCEDURE edi.[NdcDirectoryImport_DrugFormulation]

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

				DELETE FROM edi.DrugFormulation

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'FORMULAT.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)

				
				-- TRANSFORM BULK LOAD INTO EDI TABLE

				INSERT INTO edi.DrugFormulation (DrugListingId, Strength, DrugUnitType, IngredientName)
						
					SELECT 

							SUBSTRING (RowContent, 001, 07) AS DrugListingId,
		
							SUBSTRING (RowContent, 009, 10) AS Strength,

							SUBSTRING (RowContent, 020, 05) AS DrugUnitType,

							SUBSTRING (RowContent, 026, 60) AS IngredientName

						FROM 
	
							edi.BulkInsertFlatText		
		

				-- CLEAN UP ANY CODES THAT WOULD BREAK THE ID
		
				DELETE FROM edi.DrugFormulation WHERE DrugListingId LIKE '%[A-Z]%'

	
				---- UPDATE EXISTING ITEMS	
		
				--UPDATE dbo.DrugFormulation

				--	SET 
	
				--		DrugFormulationName = DataSource.DrugFormulationName,
		
				--		ModifiedAuthorityName = DataSource.ModifiedAuthorityName,
		
				--		ModifiedAccountId = DataSource.ModifiedAccountId,
		
				--		ModifiedAccountName = DataSource.ModifiedAccountName,
		
				--		ModifiedDate = DataSource.ModifiedDate
	
				--	FROM 
	
				--		dbo.DrugFormulation AS DataDestination
		
				--			JOIN edi.DrugFormulation AS DataSource
			
				--				ON DataDestination.DrugFormulation = DataSource.DrugFormulation
				
				--	WHERE 
	
				--		(DataDestination.DrugFormulationName <> DataSource.DrugFormulationName)
		

				---- INSERT NEW ITEMS
	
				--INSERT INTO dbo.DrugFormulation (DrugFormulation, DrugFormulationName) 

				--	SELECT DISTINCT DrugFormulation, DrugFormulationName
		
				--		FROM edi.DrugFormulation
		
				--		WHERE DrugFormulation NOT IN (SELECT DrugFormulation FROM dbo.DrugFormulation)

      /* STORED PROCEDURE ( END ) */

    END 
GO

/*
GRANT EXECUTE ON edi.[NdcDirectoryImport_DrugFormulation] TO PUBLIC
GO          
*/