/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_SelectByMemberEvent' AND type = 'P'))
  DROP PROCEDURE MemberService_SelectByMemberEvent
GO      
*/

CREATE PROCEDURE dbo.MemberService_SelectByMemberEvent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @serviceId           BIGINT,
      @eventDate         DATETIME
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT MemberServiceId 
        
          FROM MemberService 
          
          WHERE (MemberId = @memberId) AND (ServiceId = @serviceId) AND (EventDate = @eventDate)
          
    END    
              
    