/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_SelectByMemberMostRecent' AND type = 'P'))
  DROP PROCEDURE MemberService_SelectByMemberMostRecent
GO      
*/

CREATE PROCEDURE dbo.MemberService_SelectByMemberMostRecent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @serviceId           BIGINT,
      @serviceName        VARCHAR (060)
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @eventDate AS DATETIME

        /* LOCAL VARIABLES ( END ) */

        IF (@serviceId = 0) 
        
          BEGIN
        
            SELECT @serviceId = ServiceId FROM Service WHERE ServiceName = @serviceName
          
          END 
        

        SELECT @eventDate = MAX (EventDate)
        
          FROM MemberService 
          
          WHERE (MemberId = @memberId) AND (ServiceId = @serviceId) 

              
        SELECT * 
        
          FROM MemberService 
          
          WHERE (MemberId = @memberId) AND (ServiceId = @serviceId) AND (EventDate = @eventDate)
              
    END    
              
    