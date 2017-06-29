
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_AddAnchor2' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor2
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor2
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

              CASE 
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, ParentServiceEvent.EventDate)
                  WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, ParentServiceEvent.EventDate)
                  ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, ParentServiceEvent.EventDate)
                END AS ExpectedServiceDate,

                CAST (NULL AS   BIGINT) AS MemberServiceId,
                CAST (NULL AS DATETIME) AS EventDate,
                
                CAST (NULL AS   BIGINT) AS PreviousMemberServiceId,
                CAST (NULL AS DATETIME) AS PreviousEventDate,

                CAST (ParentServiceEvent.PopulationMembershipServiceEventId AS   BIGINT) AS ParentMembershipServiceEventId,
                CAST (ParentServiceEvent.EventDate AS DATETIME) AS ParentMembershipServiceEventDate,

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

                  -- COMPLIANT PARENT EVENT
                  JOIN PopulationMembershipServiceEvent AS ParentServiceEvent
                    ON PopulationMembership.PopulationMembershipId = ParentServiceEvent.PopulationMembershipId 
                    AND ParentServiceEvent.PopulationServiceEventId = PopulationServiceEvent.AnchorDateValue
                    AND ParentServiceEvent.EventDate IS NOT NULL

                  -- PARENT EVENT NOT ALREADY USED
                  LEFT JOIN PopulationMembershipServiceEvent AS UtilizedParentEvent
                    ON ParentServiceEvent.PopulationMembershipServiceEventId = UtilizedParentEvent.ParentPopulationMembershipServiceEventId
               

              WHERE 
                (PopulationServiceEvent.AnchorDate = 2)

                AND (UtilizedParentEvent.PopulationMembershipServiceEventId IS NULL)
                
                AND (PopulationServiceEvent.PopulationServiceEventId = @serviceEventId) -- ONLY EVALUATE SPECIFIC SERVICE EVENT
                
                AND (PopulationMembership.TerminationDate > GETDATE ()) -- ACTIVE POPULATION MEMBERSHIP SEGMENT


        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
      
    END
    
    