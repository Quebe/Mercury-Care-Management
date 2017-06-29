
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationMembershipTriggerEventsByMembership_Select' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationMembershipTriggerEventsByMembership_Select
GO      
*/

CREATE PROCEDURE dbo.PopulationMembershipTriggerEventsByMembership_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @membershipId                          BIGINT

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

              PopulationMembershipTriggerEvent.PopulationMembershipTriggerEventId,
              PopulationMembershipTriggerEvent.PopulationMembershipId,
              PopulationMembershipTriggerEvent.PopulationTriggerEventId,
              PopulationMembershipTriggerEvent.TriggerDate,
              PopulationMembershipTriggerEvent.EventDate,
              PopulationTriggerEvent.TriggerEventType AS EventType,
              
              CAST (CASE WHEN (PopulationTriggerEvent.PopulationTriggerEventId IS NULL) THEN 1 ELSE 0 END AS BIT) AS IsTriggerDeleted,

              ISNULL (ServiceTrigger.ServiceId,   0) AS ServiceId,              
              ISNULL (ServiceTrigger.ServiceName, 'Deleted Service') AS ServiceName,

              ISNULL (MetricTrigger.MetricId,   0) AS MetricId,              
              ISNULL (MetricTrigger.MetricName, 'Deleted Metric') AS MetricName,

              ISNULL (AuthorizedServiceTrigger.AuthorizedServiceId,   0) AS AuthorizedServiceId,              
              ISNULL (AuthorizedServiceTrigger.AuthorizedServiceName, 'Deleted Authorized Service') AS AuthorizedServiceName,

              -- ISNULL (ProblemStatement.ProblemStatementId, 0) AS ProblemStatementId,
              -- ISNULL (ProblemStatement.Problem, '') AS ProblemStatement,
              
              CAST (0 AS BIGINT) AS ProblemStatementId,
              
              CAST ('' AS VARCHAR (060)) AS ProblemStatement,
              
              
              PopulationMembershipTriggerEvent.ActionDescription AS ActionDescription,
              
              
              PopulationMembershipTriggerEvent.MemberServiceId,
              
              PopulationMembershipTriggerEvent.MemberMetricId,
              
              PopulationMembershipTriggerEvent.MemberAuthorizedServiceId,
              
              PopulationMembershipTriggerEvent.CreateAuthorityName,
              PopulationMembershipTriggerEvent.CreateAccountId,
              PopulationMembershipTriggerEvent.CreateAccountName,
              PopulationMembershipTriggerEvent.CreateDate,
              
              PopulationMembershipTriggerEvent.ModifiedAuthorityName,
              PopulationMembershipTriggerEvent.ModifiedAccountId,
              PopulationMembershipTriggerEvent.ModifiedAccountName,
              PopulationMembershipTriggerEvent.ModifiedDate  
                                        
            FROM 

              PopulationMembershipTriggerEvent

                LEFT JOIN PopulationTriggerEvent 
                  ON PopulationMembershipTriggerEvent.PopulationTriggerEventId = PopulationTriggerEvent.PopulationTriggerEventId

                  LEFT JOIN Service AS ServiceTrigger
                    ON PopulationTriggerEvent.ServiceId = ServiceTrigger.ServiceId
                    AND (PopulationTriggerEvent.TriggerEventType = 0)
                    
                  LEFT JOIN Metric AS MetricTrigger
                    ON PopulationTriggerEvent.MetricId = MetricTrigger.MetricId
                    AND (PopulationTriggerEvent.TriggerEventType = 1)

                  LEFT JOIN AuthorizedService AS AuthorizedServiceTrigger
                    ON PopulationTriggerEvent.AuthorizedServiceId = AuthorizedServiceTrigger.AuthorizedServiceId
                    AND (PopulationTriggerEvent.TriggerEventType = 2)
                    
                  --LEFT JOIN ProblemStatement                   
                    --ON PopulationMembershipTriggerEvent.ProblemStatementId = ProblemStatement.ProblemStatementId
                                                          
            WHERE PopulationMembershipId = @membershipId

            ORDER BY PopulationMembershipTriggerEvent.TriggerDate DESC
  
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationMembershipTriggerEventsByMembership_Select TO PUBLIC
GO          
*/