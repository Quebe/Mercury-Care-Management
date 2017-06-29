/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProviderAffiliations_Select' AND type = 'P'))
  DROP PROCEDURE dal.ProviderAffiliations_Select
GO      
*/

CREATE PROCEDURE dal.ProviderAffiliations_Select
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


		SELECT ProviderAffiliation.* 
						
			FROM 
			
				dbo.ProviderAffiliation  
				
			WHERE ProviderId = @providerId

			ORDER BY TerminationDate DESC, EffectiveDate DESC, AffiliateProviderId
			
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.ProviderAffiliations_Select TO PUBLIC
GO          
*/
