/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_SelectByMemberMostRecent' AND type = 'P'))
  DROP PROCEDURE MemberMetric_SelectByMemberMostRecent
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_SelectByMemberMostRecent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @metricId           BIGINT,
      @metricName        VARCHAR (060)
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

        IF (@metricId = 0) 
        
          BEGIN
        
            SELECT @metricId = MetricId FROM Metric WHERE MetricName = @metricName
          
          END 
        

        SELECT @eventDate = MAX (EventDate)
        
          FROM MemberMetric 
          
          WHERE (MemberId = @memberId) AND (MetricId = @metricId) 

              
        SELECT * 
        
          FROM MemberMetric 
          
          WHERE (MemberId = @memberId) AND (MetricId = @metricId) AND (EventDate = @eventDate)
              
    END    
              
    