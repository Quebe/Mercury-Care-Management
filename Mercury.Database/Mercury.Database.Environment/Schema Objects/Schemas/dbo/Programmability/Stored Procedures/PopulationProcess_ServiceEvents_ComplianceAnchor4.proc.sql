﻿
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_ComplianceAnchor4' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_ComplianceAnchor4
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_ComplianceAnchor4
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

        DECLARE @CompliantServiceEventTable AS TABLE (PopulationMembershipServiceEventId BIGINT, MemberServiceId BIGINT, EventDate DATETIME)

        /* LOCAL VARIABLES ( END ) */

        BEGIN TRANSACTION

          -- IDENTIFY ALL SERVICE EVENTS THAT ARE NOW COMPLIANT, INSERT INTO WORKING TABLE

          INSERT INTO @CompliantServiceEventTable

          SELECT DISTINCT 

              PopulationMembershipServiceEvent.PopulationMembershipServiceEventId,

              MemberService.MemberServiceId,

              MemberService.EventDate

            FROM 

              PopulationMembership
              
                JOIN dbo.Member AS Member ON PopulationMembership.MemberId = Member.MemberId

                JOIN PopulationMembershipServiceEvent
                  ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId 
            
                JOIN PopulationServiceEvent 
                  ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEvent.PopulationServiceEventId

                -- MATCHING MEMBER SERVICE EVENT FOUND FOR COMPLIANCE
                JOIN MemberService
                  ON  PopulationMembership.MemberId = MemberService.MemberId            -- SAME MEMBER AS ON SERVICE FOUND
                  AND PopulationServiceEvent.ServiceId = MemberService.ServiceId        -- EXPECTED SERVICE FOR THE MEMBER SERVICE
                  AND MemberService.EventDate >= DATEADD (MONTH, AnchorDateValue, Member.BirthDate)
                  
                -- NOT PREVIOUSLY USED TO MAKE SAME TYPE OF SERVICE EVENT (BY ID) COMPLIANT, WITHIN THE SAME POPULATION SEGMENT 
                LEFT JOIN PopulationMembershipServiceEvent AS UtilizedMemberService
                  ON  PopulationMembership.PopulationMembershipId = UtilizedMemberService.PopulationMembershipId  -- WITHIN THE SAME POPULATION SEGMENT
                  AND PopulationMembershipServiceEvent.PopulationServiceEventId = UtilizedMemberService.PopulationServiceEventId      -- SAME SERVICE EVENT
                  AND MemberService.MemberServiceId = UtilizedMemberService.MemberServiceId                       -- SAME MEMBER SERVICE (LEFT JOIN FOR NULL ONLY FILTERING)

            WHERE 

              (PopulationMembershipServiceEvent.EventDate IS NULL) -- OPEN SERVICE EVENT 

              AND (PopulationMembershipServiceEvent.PopulationServiceEventId = @serviceEventId) -- PROCESS FOR SPECIFIC SERVICE EVENT ID
              
              AND (UtilizedMemberService.PopulationServiceEventId IS NULL)


          -- USE THE LAST (MAX) MATCHED SERVICE IF MULTIPLES ARE FOUND
          -- IF USING MIN, THEN IT WOULD JUST RECREATE REOCCURING FOR THE NEXT

          DELETE FROM @CompliantServiceEventTable

            FROM 

              @CompliantServiceEventTable AS CompliantServiceEventTable

              LEFT JOIN 

                (SELECT PopulationMembershipServiceEventId, MAX (EventDate) AS EventDate FROM @CompliantServiceEventTable GROUP BY PopulationMembershipServiceEventId

                ) AS MaxMemberService

                ON CompliantServiceEventTable.PopulationMembershipServiceEventId = MaxMemberService.PopulationMembershipServiceEventId 

                  AND CompliantServiceEventTable.EventDate = MaxMemberService.EventDate

            WHERE MaxMemberService.PopulationMembershipServiceEventId IS NULL

          
          -- MARK SERVICE EVENTS COMPLIANT

          UPDATE PopulationMembershipServiceEvent

            SET 
              
              MemberServiceId = UpdateSourceTable.MemberServiceId,
              
              EventDate = UpdateSourceTable.EventDate,

              Status = 0,
              
              ModifiedAuthorityName = @modifiedAuthorityName,
              ModifiedAccountId     = @modifiedAccountId,
              ModifiedAccountName   = @modifiedAccountName,
              ModifiedDate          = GETDATE ()
    


            FROM 

              PopulationMembershipServiceEvent

                JOIN @CompliantServiceEventTable AS UpdateSourceTable 
                
                  ON PopulationMembershipServiceEvent.PopulationMembershipServiceEventId = UpdateSourceTable.PopulationMembershipServiceEventId
                  

                    
          -- INSERT REOCCURRING SERVICE EVENTS 
          
                    
          INSERT INTO PopulationMembershipServiceEvent

            SELECT
                
                -- PopulationMembershipServiceEventId (IDENTITY),

                PopulationMembershipServiceEvent.PopulationMembershipId,

                PopulationMembershipServiceEvent.PopulationServiceEventId,

                CAST (
                  CASE 
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 0) THEN DATEADD (DAY,   PopulationServiceEvent.ScheduleDateValue, CompliantService.EventDate)
                      WHEN (PopulationServiceEvent.ScheduleDateQualifier = 2) THEN DATEADD (YEAR,  PopulationServiceEvent.ScheduleDateValue, CompliantService.EventDate)
                      ELSE DATEADD (MONTH, PopulationServiceEvent.ScheduleDateValue, CompliantService.EventDate)
                    END AS DATETIME) AS ExpectedEventDate,

                CAST (NULL AS BIGINT) AS MemberServiceId, 

                CAST (NULL AS DATETIME) AS EventDate,

                CompliantService.MemberServiceId AS PreviousMemberServiceId,
                
                CompliantService.EventDate AS PreviousEventDate,

                CAST (NULL  AS   BIGINT) AS ParentMembershipServiceEventId,
                
                CAST (NULL AS DATETIME) AS ParentMembershipServiceEventDate,

                CAST (NULL AS BIGINT) AS PreviousThresholdId,

                CAST (NULL AS DATETIME) AS PreviousThresholdDate,

                CAST (NULL AS DATETIME) AS NextThresholdDate,

                CAST (1 AS INT) AS Status,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()

              FROM 

                PopulationMembershipServiceEvent
              
                  JOIN PopulationServiceEvent 
                    ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEvent.PopulationServiceEventId

                  JOIN @CompliantServiceEventTable AS CompliantService
                    ON PopulationMembershipServiceEvent.PopulationMembershipServiceEventId = CompliantService.PopulationMembershipServiceEventId 

              WHERE (PopulationServiceEvent.IsReoccurring = 1) -- REOCCURRING SERVICE EVENT
                                    
                    
        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_ServiceEvents_ComplianceAnchor4 TO PUBLIC
GO          
*/