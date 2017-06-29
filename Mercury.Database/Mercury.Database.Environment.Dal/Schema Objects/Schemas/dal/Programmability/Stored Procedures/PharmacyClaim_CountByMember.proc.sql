/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PharmacyClaim_CountByMember' AND type = 'P'))
  DROP PROCEDURE dal.PharmacyClaim_CountByMember
GO      
*/

CREATE PROCEDURE dal.PharmacyClaim_CountByMember
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @claimCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @claimCount = COUNT (1)
         
            FROM dbo.PharmacyClaim
            
            WHERE (PharmacyClaim.MemberId = @memberId)
            
        RETURN @claimCount
              
    END    
              
    