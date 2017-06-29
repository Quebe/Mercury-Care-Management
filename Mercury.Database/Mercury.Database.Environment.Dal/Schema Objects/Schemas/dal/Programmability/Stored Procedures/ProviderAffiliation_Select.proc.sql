/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProviderAffiliation_Select' AND type = 'P'))
  DROP PROCEDURE dal.ProviderAffiliation_Select
GO      
*/

CREATE PROCEDURE dal.ProviderAffiliation_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @providerAffiliationId BIGINT
    
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
				
			WHERE ProviderAffiliationId = @providerAffiliationId;
			
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.ProviderAffiliation_Select TO PUBLIC
GO          
*/
