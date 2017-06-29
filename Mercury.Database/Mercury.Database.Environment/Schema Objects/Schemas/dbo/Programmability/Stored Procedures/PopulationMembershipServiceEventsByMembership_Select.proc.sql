
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationMembershipServiceEventsByMembership_Select' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationMembershipServiceEventsByMembership_Select
GO      
*/

CREATE PROCEDURE dbo.PopulationMembershipServiceEventsByMembership_Select
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

              PopulationMembershipServiceEvent.PopulationMembershipServiceEventId,
              PopulationMembershipServiceEvent.PopulationMembershipId,
              PopulationMembershipServiceEvent.PopulationServiceEventId,

              ISNULL (Service.ServiceId,   ISNULL (ServiceReference.ServiceId,   0)) AS ServiceId,              
              ISNULL (Service.ServiceName, ISNULL (ServiceReference.ServiceName, 'Deleted Service')) AS ServiceName,

              PopulationMembershipServiceEvent.ExpectedEventDate,
              MemberService.EventDate,
              PopulationMembershipServiceEvent.PreviousThresholdDate,
              PopulationMembershipServiceEvent.NextThresholdDate,

              CAST (ISNULL (PopulationServiceEvent.ScheduleDateValue, 0) AS INTEGER) AS ScheduleValue,
              CAST (ISNULL (PopulationServiceEvent.ScheduleDateQualifier, 0) AS INTEGER) AS ScheduleQualifier,
              CAST (ISNULL (PopulationServiceEvent.IsReoccurring, 0) AS BIT) AS Reoccurring,
              
              PopulationMembershipServiceEvent.Status,

              CASE WHEN (MemberService.EventDate IS NULL) THEN 0 ELSE 1 END AS IsCompliant,
              
              
              PopulationMembershipServiceEvent.MemberServiceId,
              PopulationMembershipServiceEvent.PreviousMemberServiceId,
              PopulationMembershipServiceEvent.PreviousEventDate,
              PopulationMembershipServiceEvent.ParentPopulationMembershipServiceEventId,
              PopulationMembershipServiceEvent.ParentPopulationMembershipServiceEventDate,
              PopulationMembershipServiceEvent.PreviousThresholdId,
              
              PopulationMembershipServiceEvent.CreateAuthorityName,
              PopulationMembershipServiceEvent.CreateAccountId,
              PopulationMembershipServiceEvent.CreateAccountName,
              PopulationMembershipServiceEvent.CreateDate,
              
              PopulationMembershipServiceEvent.ModifiedAuthorityName,
              PopulationMembershipServiceEvent.ModifiedAccountId,
              PopulationMembershipServiceEvent.ModifiedAccountName,
              PopulationMembershipServiceEvent.ModifiedDate,              
              
              
              
              CASE WHEN (MemberService.EventDate IS NOT NULL) THEN PopulationMembershipServiceEvent.ExpectedEventDate ELSE NULL END AS SortDate

--              CASE WHEN (MemberService.EventDate IS NULL) THEN '12/31/9999' ELSE MemberService.EventDate END AS SortDate
              
            FROM 

              PopulationMembershipServiceEvent

                LEFT JOIN PopulationServiceEvent 
                  ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEvent.PopulationServiceEventId

                LEFT JOIN Service 
                  ON PopulationServiceEvent.ServiceId = Service.ServiceId

                LEFT JOIN MemberService 
                  ON PopulationMembershipServiceEvent.MemberServiceId = MemberService.MemberServiceId
                  
                LEFT JOIN Service AS ServiceReference
                  ON MemberService.ServiceId = ServiceReference.ServiceId

              
            WHERE PopulationMembershipId = @membershipId

            ORDER BY IsCompliant, MemberService.EventDate DESC, SortDate DESC, PopulationMembershipServiceEvent.ExpectedEventDate, NextThresholdDate, Status
  
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationMembershipServiceEventsByMembership_Select TO PUBLIC
GO          
*/