-- DROP VIEW [dbo].[PopulationProcess_ServiceEvents_AddAnchor1_SupportView] 

CREATE VIEW [dbo].[PopulationProcess_ServiceEvents_AddAnchor1_SupportView] AS 

  SELECT 
      -- PopulationMembershipServiceEvent

      PopulationMembership.PopulationMembershipId,
      PopulationServiceEvent.PopulationServiceEventId,
          
      --CASE 
          --WHEN (MemberService.EventDate IS NOT NULL) THEN 
        
            --CASE 
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 0) AND (DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN DATEADD (DAY,    PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 1) AND (DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN DATEADD (MONTH,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 2) AND (DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN DATEADD (YEAR,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                --ELSE
                  --CASE 
                      --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
                      --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
                      --ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
                    --END

              --END

          --ELSE

            --CASE 
                --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
                --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
                --ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, PopulationMembership.EffectiveDate)
              --END

        --END AS ExpectedServiceDate,

      CASE 
          WHEN (MemberService.EventDate IS NOT NULL) THEN 
        
            CASE 
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 0) AND (DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN DATEADD (DAY,    PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 1) AND (DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN DATEADD (MONTH,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 2) AND (DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN DATEADD (YEAR,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate)
                ELSE
                  CASE 
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                      ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                    END

              END

          ELSE

            CASE 
                WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
                ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, PopulationMembership.AnchorDate)
              END

        END AS ExpectedServiceDate,

      CAST (NULL AS   BIGINT) AS MemberServiceId,
      CAST (NULL AS DATETIME) AS EventDate,

      MemberService.MemberServiceId AS PreviousMemberServiceId,
      MemberService.EventDate       AS PreviousEventDate,

      CAST (NULL AS   BIGINT) AS PreviousThresholdId,
      CAST (NULL AS DATETIME) AS PreviousThresholdDate,
      CAST (NULL AS DATETIME) AS NextThresholdDate,
      
      CAST ('Open - Tracking' AS VARCHAR (030)) AS Status,

      --CASE 
          --WHEN (MemberService.EventDate IS NOT NULL) THEN 
        
            --CASE 
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 0) AND (DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN '1-0'
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 1) AND (DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN '1-2'
                --WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 2) AND (DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.EffectiveDate)) THEN '1-3'
                --ELSE
                  --CASE 
                      --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN '1-4-0'
                      --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN '1-4-2'
                      --ELSE '1-4-1'
                    --END

              --END

          --ELSE

            --CASE 
                --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN '2-0'
                --WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN '2-2'
                --ELSE '2-1'
              --END

        --END AS ExpectedServiceDateMethod
      
      CASE 
          WHEN (MemberService.EventDate IS NOT NULL) THEN 
        
            CASE 
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 0) AND (DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN '1-0'
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 1) AND (DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN '1-2'
                WHEN ((PopulationServiceEvent.ScheduleDateQualifier = 2) AND (DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, MemberService.EventDate) > PopulationMembership.AnchorDate)) THEN '1-3'
                ELSE
                  CASE 
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN '1-4-0'
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN '1-4-2'
                      ELSE '1-4-1'
                    END

              END

          ELSE

            CASE 
                WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN '2-0'
                WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN '2-2'
                ELSE '2-1'
              END

        END AS ExpectedServiceDateMethod
      

    FROM 

      PopulationMembership

        JOIN 
          PopulationServiceEvent ON PopulationMembership.PopulationId = PopulationServiceEvent.PopulationId

        LEFT JOIN PopulationMembershipServiceEvent
          ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId
          AND PopulationServiceEvent.PopulationServiceEventId = PopulationMembershipServiceEvent.PopulationServiceEventId

        LEFT JOIN MemberService 
          ON PopulationMembership.MemberId = MemberService.MemberId
          AND PopulationServiceEvent.ServiceId = MemberService.ServiceId

        LEFT JOIN MemberService AS ExclusionMemberService
          ON PopulationMembership.MemberId = ExclusionMemberService.MemberId
          AND PopulationServiceEvent.ExclusionServiceId = ExclusionMemberService.ServiceId
        

    WHERE (PopulationServiceEvent.AnchorDate = 1) -- PreviousServiceDate
      AND (PopulationMembershipServiceEvent.PopulationMembershipServiceEventId IS NULL)
      AND (ExclusionMemberService.MemberServiceId IS NULL)
      AND (PopulationMembership.TerminationDate > GETDATE ())

