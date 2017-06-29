
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_AddAnchor0' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor0
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor0
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceEventId                    BIGINT,
      
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        /* LOCAL VARIABLES ( END ) */

        BEGIN TRANSACTION

        INSERT INTO PopulationMembershipServiceEvent
        
          SELECT 
              PopulationMembership.PopulationMembershipId,
              PopulationServiceEvent.PopulationServiceEventId,

              --CASE 
                  --WHEN (PopulationServiceEvent.ScheduleQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleValue, PopulationMembership.EffectiveDate)
                  --WHEN (PopulationServiceEvent.ScheduleQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleValue, PopulationMembership.EffectiveDate)
                  --ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleValue, PopulationMembership.EffectiveDate)
                --END AS ExpectedServiceDate,

              CASE 
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                  ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                END AS ExpectedServiceDate,

                CAST (NULL AS   BIGINT) AS MemberServiceId,
                CAST (NULL AS DATETIME) AS EventDate,
                
                CAST (NULL AS   BIGINT) AS PreviousMemberServiceId,
                CAST (NULL AS DATETIME) AS PreviousEventDate,

                CAST (NULL AS   BIGINT) AS ParentMembershipServiceEventId,
                CAST (NULL AS DATETIME) AS ParentMembershipServiceEventDate,

                CAST (NULL AS   BIGINT) AS PreviousThresholdId,
                CAST (NULL AS DATETIME) AS PreviousThresholdDate,
                CAST (NULL AS DATETIME) AS NextThresholdDate,
                
                CAST (1 AS INT) AS Status,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()
              
            FROM 
              PopulationMembership

                JOIN PopulationServiceEvent
                  ON PopulationMembership.PopulationId = PopulationServiceEvent.PopulationId

                LEFT JOIN PopulationMembershipServiceEvent
                  ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId
                  AND PopulationServiceEvent.PopulationServiceEventId = PopulationMembershipServiceEvent.PopulationServiceEventId

            WHERE 
                    (PopulationMembershipServiceEvent.PopulationMembershipId IS NULL) -- SERVICE EVENT DOES NOT EXIST FOR MEMBER/POPULATION
              
                AND (PopulationServiceEvent.PopulationServiceEventId = @serviceEventId) -- ONLY EVALUATE SPECIFIC SERVICE EVENT
                
                AND (PopulationMembership.TerminationDate > GETDATE ()) -- ACTIVE POPULATION MEMBERSHIP SEGMENT


        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
      
    END
    
    