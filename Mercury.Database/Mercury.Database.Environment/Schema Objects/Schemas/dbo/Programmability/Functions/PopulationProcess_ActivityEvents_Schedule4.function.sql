/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ActivityEvents_Schedule4' AND type = 'IF'))
  DROP FUNCTION dbo.PopulationProcess_ActivityEvents_Schedule4
GO      
*/

CREATE FUNCTION [dbo].PopulationProcess_ActivityEvents_Schedule4 (@populationId BIGINT)
    
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

          JOIN dbo.Member AS Member
            ON PopulationMembership.MemberId = Member.MemberId

          LEFT JOIN PopulationMembershipActivityEvent
            ON PopulationMembership.PopulationMembershipId = PopulationMembershipActivityEvent.PopulationMembershipId 
            AND (PopulationActivityEvent.PopulationActivityEventId = PopulationMembershipActivityEvent.PopulationActivityEventId)
            AND (MONTH (PopulationMembershipActivityEvent.ActivityDate) = MONTH (GETDATE ()))
            AND (YEAR  (PopulationMembershipActivityEvent.ActivityDate) = YEAR  (GETDATE ()))

      WHERE

        (PopulationMembership.PopulationId = @populationId) AND (PopulationActivityEvent.ScheduleType = 4)

        AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate) -- ACTIVE POPULATION SEGMENT

        AND (MONTH (Member.BirthDate) = MONTH (GETDATE ()))

        AND (PopulationMembershipActivityEvent.PopulationMembershipId IS NULL)

        