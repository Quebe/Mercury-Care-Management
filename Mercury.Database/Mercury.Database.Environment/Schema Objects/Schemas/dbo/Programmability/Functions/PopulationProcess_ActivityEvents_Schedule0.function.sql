/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ActivityEvents_Schedule0' AND type = 'IF'))
  DROP FUNCTION dbo.PopulationProcess_ActivityEvents_Schedule0
GO      
*/

CREATE FUNCTION [dbo].PopulationProcess_ActivityEvents_Schedule0 (@populationId BIGINT)
    
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

            MAX (PreviousActivityEvent.ActivityDate) AS PreviousActivityDate,

            CASE 
                WHEN (ScheduleQualifier = 0) THEN DATEADD (DAY,   ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))                  
                WHEN (ScheduleQualifier = 1) THEN DATEADD (MONTH, ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))
                WHEN (ScheduleQualifier = 2) THEN DATEADD (YEAR,  ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))
              END AS NextActivityDate
          
          FROM 

            PopulationMembership

              JOIN PopulationActivityEvent
              
                ON PopulationMembership.PopulationId = PopulationActivityEvent.PopulationId
                
              -- PREVIOUS (NEED TO GET MAXIMUM)
              LEFT JOIN PopulationMembershipActivityEvent AS PreviousActivityEvent
              
                ON PopulationMembership.PopulationMembershipId = PreviousActivityEvent.PopulationMembershipId -- SAME MEMBER (AND MEMBERSHIP SEGMENT)
            
                  AND PopulationActivityEvent.PopulationActivityEventId = PreviousActivityEvent.PopulationActivityEventId         -- LOOK FOR PREVIOUS OF SAME ACTIVITY 
                  
                    
          WHERE 

            (PopulationMembership.PopulationId = @populationId) AND (PopulationActivityEvent.ScheduleType = 0)

            AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate) -- ACTIVE POPULATION SEGMENT

                
          GROUP BY 

            PopulationMembership.PopulationMembershipId,
            
            PopulationMembership.EffectiveDate,
            
            PopulationMembership.AnchorDate,

            PopulationActivityEvent.PopulationActivityEventId,
            
            PopulationActivityEvent.ActionDescription,
            
            PopulationActivityEvent.ScheduleQualifier,
            
            PopulationActivityEvent.ScheduleValue,
            
            PopulationActivityEvent.AnchorDate,
            
            PopulationActivityEvent.IsReoccurring
            
          HAVING
            
            (CASE 
                WHEN (ScheduleQualifier = 0) THEN DATEADD (DAY,   ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))                  
                WHEN (ScheduleQualifier = 1) THEN DATEADD (MONTH, ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))
                WHEN (ScheduleQualifier = 2) THEN DATEADD (YEAR,  ScheduleValue, (ISNULL (((MAX (PreviousActivityEvent.ActivityDate))), (CASE WHEN (PopulationActivityEvent.AnchorDate = 0) THEN PopulationMembership.EffectiveDate ELSE PopulationMembership.AnchorDate END))))
              END <= CONVERT (CHAR (10), GETDATE (), 101))

              AND ((PopulationActivityEvent.IsReoccurring = 1) OR (MAX (PreviousActivityEvent.ActivityDate) IS NULL))
