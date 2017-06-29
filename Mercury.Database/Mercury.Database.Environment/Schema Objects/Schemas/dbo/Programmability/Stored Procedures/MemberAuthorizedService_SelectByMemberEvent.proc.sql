/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberAuthorizedService_SelectByMemberEvent' AND type = 'P'))
  DROP PROCEDURE [MemberAuthorizedService_SelectByMemberEvent]
GO      
*/

CREATE PROCEDURE dbo.MemberAuthorizedService_SelectByMemberEvent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId               BIGINT,
      @authorizedServiceId    BIGINT,
      @eventDate            DATETIME
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT MemberAuthorizedServiceId 
        
          FROM MemberAuthorizedService 
          
          WHERE (MemberId = @memberId) AND (AuthorizedServiceId = @authorizedServiceId) AND (EventDate = @eventDate)
          
    END    
              
    