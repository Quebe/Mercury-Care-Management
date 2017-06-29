/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_SelectByMemberEvent' AND type = 'P'))
  DROP PROCEDURE MemberMetric_SelectByMemberEvent
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_SelectByMemberEvent
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

        SELECT MemberMetricId 
        
          FROM MemberMetric 
          
          WHERE (MemberId = @memberId) AND (MetricId = @serviceId) AND (EventDate = @eventDate)
          
    END    
              
    