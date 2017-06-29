/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'LoincImport' AND type = 'P'))
  DROP PROCEDURE edi.[LoincImport]
GO      
*/

CREATE PROCEDURE edi.[LoincImport]

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

				DELETE FROM edi.Loinc

				DELETE FROM edi.BulkInsertFlatText
				

				-- LOAD FLAT FILE INTO SOURCE TABLE
								
				SET @bulkInsertStatement = 'BULK INSERT edi.BulkInsertFlatText FROM ''' + @filePath + 'LOINCDB.TXT'''

						+ ' WITH (FIELDTERMINATOR = ''\r'', ROWTERMINATOR = ''\n'')'

				EXEC (@bulkInsertStatement)
				

				-- DELETE NON-DATA ROWS

				DELETE FROM edi.BulkInsertFlatText WHERE RowContent NOT LIKE '"[0-9]%'


				-- INSERT INTO EDI.LOINC FOR PROCESSING 

				DECLARE @loinc AS VARCHAR (07)

				DECLARE @loincName AS VARCHAR (060)

				DECLARE @loincDescription AS VARCHAR (999)

				DECLARE @component AS VARCHAR (060)

				DECLARE @property AS VARCHAR (060)

				DECLARE @timeAspect AS VARCHAR (060)

				DECLARE @system AS VARCHAR (060)

				DECLARE @scale AS VARCHAR (060)

				DECLARE @method AS VARCHAR (060)

				DECLARE @classification AS VARCHAR (060)

				DECLARE @mapToLoinc AS VARCHAR (007)

				DECLARE @areUnitsRequired AS BIT

				DECLARE @lastChangedDate AS DATETIME

				DECLARE @changeType AS VARCHAR (030)

				DECLARE @status AS VARCHAR (030)

				DECLARE @statusReason AS VARCHAR (030)

				DECLARE @statusDescription AS VARCHAR (8000)



				DECLARE @rowContent AS VARCHAR (8000)

				DECLARE @delimiter AS CHAR (1)

				DECLARE @textField AS VARCHAR (8000)

				DECLARE @endPosition AS INT


				SET @delimiter = CHAR (9)


				DECLARE processCursor CURSOR FOR SELECT RowContent FROM edi.BulkInsertFlatText

				OPEN processCursor

				FETCH NEXT FROM processCursor INTO @rowContent

				WHILE (@@FETCH_STATUS = 0) 

					BEGIN
  
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @loinc = CAST (@textField AS VARCHAR (007))
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @component = CAST (@textField AS VARCHAR (060))
		  
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @property = CAST (@textField AS VARCHAR (060))
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @timeAspect = CAST (@textField AS VARCHAR (060))
  
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @system = CAST (@textField AS VARCHAR (060))
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @scale = CAST (@textField AS VARCHAR (007))
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @method = CAST (@textField AS VARCHAR (060))
  
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP RELAT_NMS
		
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @classification = CAST (@textField AS VARCHAR (060))
  
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP SOURCE
				
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @lastChangedDate = CASE WHEN (ISDATE (@textField) = 1) THEN CAST (@textField AS DATETIME) ELSE NULL END
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @changeType = CAST (@textField AS VARCHAR (003))
		

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP COMMENTS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT --SKIP ANSWERLIST


						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @status = CAST (@textField AS VARCHAR (011))
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET @mapToLoinc = CAST (@textField AS VARCHAR (007))

		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP SCOPE

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP CONSUMER_NAME

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP IPCC_UNITS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP REFERENCE

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP EXACT_CMP_SY

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP MOLAR_MASS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP CLASSTYPE
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP SPECIES

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
  
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP ACSSYM

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP FINAL

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP CODE_TABLE

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP PANELELEMENTS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP SURVEY_QUEST_SRC


						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET  @areUnitsRequired = CASE WHEN (@textField = 'Y') THEN 1 ELSE 0 END
		

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP SUBMITTED_UNITS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP RELATEDNAMES2


						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT
		
						SET  @loincName = CAST (@textField AS VARCHAR (060))


						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP ORDER_OBS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP CDISC_COMMON_TESTS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP HL7_FIELD_SUBFIELD_ID

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP EXTERNAL_COPYRIGHT_NOTICE

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP EXAMPLE_UNITS

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP INPC_PERCENTAGE

		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT 
		
						SET @loincDescription = CAST (@textField AS VARCHAR (999))
		

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP HL7_V2_DATATYPE

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP 

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP 

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP 

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP 
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP 

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT -- SKIP EXAMPLE_SI_UCUM_UNITS
		
		
						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT 
		
						SET @statusReason = CAST (@textField AS VARCHAR (009))

						EXEC dbo.ParseDelimitedQuotedTextField @rowContent, @delimiter, @textField OUT, @endPosition OUT, @rowContent OUT 
		
						SET @statusDescription = CAST (@textField AS VARCHAR (8000))


						INSERT edi.Loinc (Loinc, LoincName, LoincDescription, Component, Property, TimeAspect, [System], Scale, Method, Classification,
		
									AreUnitsRequired, LastChangedDate, ChangeType, MapToLoinc, Status, StatusReason, StatusDescription)

							SELECT 
		
									@loinc, @loincName, @loincDescription,
					
									@component, @property, @timeAspect, @system, @scale, @method, @classification,
					
									@areUnitsRequired, @lastChangedDate, @changeType, @mapToLoinc, @status, @statusReason, @statusDescription
				
				
  
						FETCH NEXT FROM processCursor INTO @rowContent
	
					END
  
				CLOSE processCursor

				DEALLOCATE processCursor  



					INSERT dbo.Loinc (Loinc, LoincName, LoincDescription, Component, Property, TimeAspect, [System], Scale, Method, Classification,
		
								AreUnitsRequired, MapToLoinc, Status)

						SELECT 

							LoincEdi.Loinc,

							LoincEdi.LoincName,

							LoincEdi.LoincDescription,

							LoincEdi.Component,

							LoincEdi.Property,

							LoincEdi.TimeAspect,

							LoincEdi.System,

							LoincEdi.Scale,

							LoincEdi.Method,

							LoincEdi.Classification,

							LoincEdi.AreUnitsRequired,

							LoincEdi.MapToLoinc,

							LoincEdi.Status
		

						FROM 
						
							edi.Loinc AS LoincEdi

								LEFT JOIN dbo.Loinc AS LoincDestination

									ON LoincEdi.Loinc = LoincDestination.Loinc

						WHERE 

							LoincDestination.Loinc IS NULL


				UPDATE dbo.Loinc

					SET 

						Loinc.LoincName = SourceLoinc.LoincName,

						Loinc.LoincDescription = SourceLoinc.LoincDescription,

						Loinc.MapToLoinc = SourceLoinc.MapToLoinc,

						Loinc.AreUnitsRequired = SourceLoinc.AreUnitsRequired,

						Loinc.Classification = SourceLoinc.Classification,

						Loinc.Component	 = SourceLoinc.Component,

						Loinc.Method = SourceLoinc.Method,

						Loinc.Property = SourceLoinc.Property,

						Loinc.Scale	 = SourceLoinc.Scale,

						Loinc.System = SourceLoinc.System,

						Loinc.TimeAspect = SourceLoinc.TimeAspect,

						Loinc.Status = SourceLoinc.Status
			

					FROM

						dbo.Loinc AS DestinationLoinc

							JOIN edi.Loinc AS SourceLoinc

								ON DestinationLoinc.Loinc= SourceLoinc.Loinc
				
				
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON edi.[LoincImport] TO PUBLIC
GO          
*/