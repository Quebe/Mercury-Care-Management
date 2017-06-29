/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Provider_Select' AND type = 'P'))
  DROP PROCEDURE dal.Provider_Select
GO      
*/

CREATE PROCEDURE dal.Provider_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @providerId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */


		SELECT Provider.*,
		
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
				
				Ethnicity.EthnicityName
												
		
			FROM 
			
				dbo.Provider  
				
					JOIN dbo.Entity ON Provider.EntityId = Entity.EntityId
					
					JOIN dbo.Ethnicity ON Provider.EthnicityId = Ethnicity.EthnicityId
					
			WHERE ProviderId = @providerId;
			
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.Provider_Select TO PUBLIC
GO          
*/
