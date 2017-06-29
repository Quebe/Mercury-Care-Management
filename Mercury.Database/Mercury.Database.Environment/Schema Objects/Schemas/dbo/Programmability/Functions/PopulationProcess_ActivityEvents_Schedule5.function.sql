/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ActivityEvents_Schedule5' AND type = 'IF'))
  DROP FUNCTION dbo.PopulationProcess_ActivityEvents_Schedule5
GO      
*/

CREATE FUNCTION [dbo].PopulationProcess_ActivityEvents_Schedule5 (@populationId BIGINT)
    
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
            AND (MONTH (ActivityDate) = PopulationActivityEvent.ScheduleValue)
            AND (YEAR  (ActivityDate) = YEAR  (GETDATE ()))

      WHERE

        (PopulationMembership.PopulationId = 1000000000) AND (PopulationActivityEvent.ScheduleType = 5)

        AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate) -- ACTIVE POPULATION SEGMENT

        AND (MONTH (GETDATE ()) = PopulationActivityEvent.ScheduleValue)

        AND (PopulationMembershipActivityEvent.PopulationMembershipId IS NULL)

        