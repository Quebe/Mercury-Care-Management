/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Authorization_CountByMember' AND type = 'P'))
  DROP PROCEDURE dal.Authorization_CountByMember
GO      
*/

CREATE PROCEDURE dal.Authorization_CountByMember
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
        
        DECLARE @authorizationCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @authorizationCount = COUNT (1)
         
            FROM dbo.[Authorization] AS [Authorization]
            
            WHERE ([Authorization].MemberId = @memberId)
            
        RETURN @authorizationCount
              
    END    
              
    