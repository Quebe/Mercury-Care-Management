-- DROP VIEW [dbo].[PopulationProcess_ServiceEvents_AddAnchor4_SupportView] 

CREATE VIEW [dbo].[PopulationProcess_ServiceEvents_AddAnchor4_SupportView] AS 
	

          SELECT 
              PopulationMembership.PopulationMembershipId,
              PopulationServiceEvent.PopulationServiceEventId,


              PopulationMembership.EffectiveDate   AS PopulationEffectiveDate,
              PopulationMembership.TerminationDate AS PopulationTerminationDate,
              PopulationMembership.AnchorDate      AS PopulationAnchorDate,

              CASE 
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, DATEADD (MONTH, PopulationServiceEvent.AnchorDateValue, Member.BirthDate))
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, DATEADD (MONTH, PopulationServiceEvent.AnchorDateValue, Member.BirthDate) )
                  ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, DATEADD (MONTH, PopulationServiceEvent.AnchorDateValue, Member.BirthDate) )
                END AS ExpectedServiceDate,

                CAST (NULL AS   BIGINT) AS MemberServiceId,
                CAST (NULL AS DATETIME) AS EventDate,

                CAST (NULL AS   BIGINT) AS PreviousMemberServiceId,
                CAST (NULL AS DATETIME) AS PreviousEventDate,

                CAST (NULL AS   BIGINT) AS PreviousThresholdId,
                CAST (NULL AS DATETIME) AS PreviousThresholdDate,
                CAST (NULL AS DATETIME) AS NextThresholdDate,
                
                CAST ('Open' AS VARCHAR (040)) AS Status

            FROM 
              PopulationMembership

                JOIN dbo.Member AS Member 
                  ON PopulationMembership.MemberId = Member.MemberId

                JOIN PopulationServiceEvent
                  ON PopulationMembership.PopulationId = PopulationServiceEvent.PopulationId

                LEFT JOIN PopulationMembershipServiceEvent
                  ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId
                  AND PopulationServiceEvent.PopulationServiceEventId = PopulationMembershipServiceEvent.PopulationServiceEventId

            WHERE 
                (PopulationMembership.TerminationDate > GETDATE ()) -- OPEN POPULATION MEMBERSHIP SEGMENT

                AND (PopulationMembershipServiceEvent.PopulationMembershipId IS NULL) -- SERVICE EVENT DOES NOT EXIST FOR MEMBER/POPULATION
              