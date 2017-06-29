/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberAuthorizedService_Count' AND type = 'P'))
  DROP PROCEDURE MemberAuthorizedService_Count
GO      
*/

CREATE PROCEDURE dbo.MemberAuthorizedService_Count
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                BIGINT,
      @showHidden                 BIT

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @authorizedServiceCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @authorizedServiceCount = COUNT (1)
         
          FROM MemberAuthorizedService 
          
            JOIN AuthorizedService ON MemberAuthorizedService.AuthorizedServiceId = AuthorizedService.AuthorizedServiceId
            
          WHERE MemberAuthorizedService.MemberId = @memberId
          
            AND ((AuthorizedService.Visible = 1) OR (@showHidden = 1))
  
   
        RETURN @authorizedServiceCount
              
    END    
              
    