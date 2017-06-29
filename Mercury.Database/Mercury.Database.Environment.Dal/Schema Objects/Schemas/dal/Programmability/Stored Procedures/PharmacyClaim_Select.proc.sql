/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PharmacyClaim_Select' AND type = 'P'))
  DROP PROCEDURE dal.PharmacyClaim_Select
GO      
*/

CREATE PROCEDURE dal.PharmacyClaim_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @claimId            BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT *

        FROM PharmacyClaim
    	  
        WHERE (PharmacyClaim.PharmacyClaimId = @claimId)
            
	    END    
              
