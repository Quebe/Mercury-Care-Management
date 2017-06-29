/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationMembershipSummaryByMember_Select' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationMembershipSummaryByMember_Select
GO      
*/

CREATE PROCEDURE dbo.PopulationMembershipSummaryByMember_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                          BIGINT

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        /* LOCAL VARIABLES ( END ) */
        
          SELECT 
              PopulationMembership.PopulationMembershipId,
              PopulationMembership.MemberId,
              Population.PopulationId,
              Population.PopulationName,
              PopulationMembership.EffectiveDate,
              PopulationMembership.TerminationDate,
              PopulationMembership.AnchorDate,
              
              PopulationMembership.IdentifyingEventMemberServiceId,
              PopulationMembership.IdentifyingEventServiceId,
              ISNULL (IdentifyingService.ServiceName, '') AS IdentifyingEventServiceName,
              PopulationMembership.IdentifyingEventDate,
              
              PopulationMembership.TerminatingEventMemberServiceId,
              PopulationMembership.TerminatingEventServiceId,
              ISNULL (TerminatingService.ServiceName, '') AS TerminatingEventServiceName,
              PopulationMembership.TerminatingEventDate,
              
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN 0    ELSE ISNULL (Service.ServiceId, 0) END AS BIGINT) AS ServiceId,
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN ''   ELSE ISNULL (Service.ServiceName, 'No Service') END AS VARCHAR (060)) AS ServiceName,
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN NULL ELSE PopulationMembershipServiceEvent.ExpectedEventDate END AS DATETIME) AS ExpectedEventDate,
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN NULL ELSE PopulationMembershipServiceEvent.PreviousThresholdDate END AS DATETIME) AS PreviousThresholdDate,
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN NULL ELSE PopulationMembershipServiceEvent.NextThresholdDate END AS DATETIME) AS NextThresholdDate,
              CAST (CASE WHEN (PopulationMembership.TerminationDate < GETDATE ()) THEN 0   ELSE ISNULL (PopulationMembershipServiceEvent.Status, 0) END AS INT) AS Status,
              
              Population.Enabled AS PopulationEnabled,
              Population.Visible AS PopulationVisible

            FROM 
              PopulationMembership

                JOIN Population 
                  ON PopulationMembership.PopulationId = Population.PopulationId

                LEFT JOIN PopulationMembershipServiceEvent 
                  ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId 
                  AND (PopulationMembershipServiceEvent.EventDate IS NULL)

                LEFT JOIN PopulationServiceEvent 
                  ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEvent.PopulationServiceEventId

                LEFT JOIN Service 
                  ON PopulationServiceEvent.ServiceId = Service.ServiceId
                  
                LEFT JOIN Service AS IdentifyingService
                  ON PopulationMembership.IdentifyingEventServiceId = IdentifyingService.ServiceId

                LEFT JOIN Service AS TerminatingService
                  ON PopulationMembership.TerminatingEventServiceId = TerminatingService.ServiceId
                  
            WHERE 

              (PopulationMembership.MemberId = @memberId)

              -- ONLY DISPLAY OPEN SERVICES 
              -- AND ((PopulationMembershipServiceEvent.PopulationMembershipId IS NULL) OR (PopulationMembershipServiceEvent.EventDate IS NULL))

            ORDER BY TerminationDate DESC, EffectiveDate DESC, PopulationName, ExpectedEventDate
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationMembershipSummaryByMember_Select TO PUBLIC
GO          
*/