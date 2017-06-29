/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Claim_CountByMember' AND type = 'P'))
  DROP PROCEDURE dal.Claim_CountByMember
GO      
*/

CREATE PROCEDURE dal.Claim_CountByMember
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
         
            FROM dbo.Claim AS Claim
            
            WHERE Claim.MemberId = @memberId
            
        RETURN @claimCount
              
    END    
              
    