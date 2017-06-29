/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Member_Select' AND type = 'P'))
  DROP PROCEDURE dal.Member_Select
GO      
*/

CREATE PROCEDURE dal.Member_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId BIGINT
    
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
								
		
			FROM 
			
				dbo.Member  
				
					JOIN dbo.Entity ON Member.EntityId = Entity.EntityId
					
					JOIN dbo.Ethnicity ON Member.EthnicityId = Ethnicity.EthnicityId
					
					JOIN dbo.Citizenship ON Member.CitizenshipId = Citizenship.CitizenshipId
					
					JOIN dbo.Language ON Member.LanguageId = Language.LanguageId
					
					JOIN dbo.MaritalStatus ON Member.MaritalStatusId = MaritalStatus.MaritalStatusId
			
			WHERE MemberId = @memberId

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.Member_Select TO PUBLIC
GO          
*/