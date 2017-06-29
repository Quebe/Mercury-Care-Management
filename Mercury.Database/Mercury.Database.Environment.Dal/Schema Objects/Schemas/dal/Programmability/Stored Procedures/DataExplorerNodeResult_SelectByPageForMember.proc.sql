/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeResult_SelectByPageForMember' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeResult_SelectByPageForMember
GO      
*/

CREATE PROCEDURE dal.DataExplorerNodeResult_SelectByPageForMember
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @nodeInstanceId            UNIQUEIDENTIFIER,
      @initialRow             INT,
      @count                  INT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
				
				SELECT Member.*,
		
						Entity.EntityType,
				
						Entity.EntityName,
				
						Entity.NameLast,
				
						Entity.NameFirst,
				
						Entity.NameMiddle,
				
						Entity.NamePrefix,
				
						Entity.NameSuffix,
				
						Entity.FederalTaxId,
				
						Entity.IdCodeQualifier,
				
						Entity.UniqueId,
				
						Ethnicity.EthnicityName,
				
						Citizenship.CitizenshipName,
				
						Language.LanguageName,
				
						MaritalStatus.MaritalStatusName
								
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY Id) AS RowNumber, 
            
								DataExplorerNodeResult.Id
           
              FROM DataExplorerNodeResult
              
              WHERE DataExplorerNodeInstanceId = @nodeInstanceId
              
            ) AS DataExplorerNodeResultPage

						JOIN Member 

							ON DataExplorerNodeResultPage.Id = Member.MemberId
							
						JOIN dbo.Entity ON Member.EntityId = Entity.EntityId
					
						JOIN dbo.Ethnicity ON Member.EthnicityId = Ethnicity.EthnicityId
					
						JOIN dbo.Citizenship ON Member.CitizenshipId = Citizenship.CitizenshipId
					
						JOIN dbo.Language ON Member.LanguageId = Language.LanguageId
					
						JOIN dbo.MaritalStatus ON Member.MaritalStatusId = MaritalStatus.MaritalStatusId
			
          WHERE DataExplorerNodeResultPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    