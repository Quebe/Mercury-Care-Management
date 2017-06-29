/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ActivityEvents_Schedule1_3' AND type = 'IF'))
  DROP FUNCTION dbo.PopulationProcess_ActivityEvents_Schedule1_3
GO      
*/

CREATE FUNCTION [dbo].PopulationProcess_ActivityEvents_Schedule1_3 (@populationId BIGINT)
    
  RETURNS TABLE AS
   
    RETURN SELECT 

        PopulationMembership.PopulationMembershipId,
        
        PopulationMembership.EffectiveDate,
        
        PopulationMembership.AnchorDate,

        PopulationActivityEvent.PopulationActivityEventId,
        
        PopulationActivityEvent.ActionDescription,
        
        PopulationActivityEvent.ScheduleQualifier,
        
        PopulationActivityEvent.ScheduleValue,
        
        PopulationActivityEvent.AnchorDate AS PopulationActivityEvent_AnchorDate,

        PopulationActivityEvent.IsReoccurring,
        
        NULL AS PreviousActivityDate,
        
        NULL AS NextActivityDate
            
    FROM 

      PopulationMembership

        JOIN PopulationActivityEvent
          ON PopulationMembership.PopulationId = PopulationActivityEvent.PopulationId

        LEFT JOIN PopulationMembershipActivityEvent
          ON PopulationMembership.PopulationMembershipId = PopulationMembershipActivityEvent.PopulationMembershipId 
          AND (PopulationActivityEvent.PopulationActivityEventId = PopulationMembershipActivityEvent.PopulationActivityEventId)

          AND ActivityDate > 
            CAST (CONVERT (CHAR (10), 
              CASE 
                  /* MONTHLY   */ WHEN (PopulationActivityEvent.ScheduleType = 1) THEN DATEADD (MM, -1, CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME))
                  /* QUARTERLY */ WHEN (PopulationActivityEvent.ScheduleType = 2) THEN DATEADD (QQ, -1, CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME))
                  /* YEARLY    */ ELSE                                                 DATEADD (YY, -1, CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME))
                END
              , 121) AS DATETIME)


    WHERE 

      ((PopulationMembership.PopulationId = @populationId) AND (PopulationActivityEvent.ScheduleType IN (1, 2, 3)))

      AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)

      AND (PopulationMembershipActivityEvent.PopulationMembershipId IS NULL)


        
      