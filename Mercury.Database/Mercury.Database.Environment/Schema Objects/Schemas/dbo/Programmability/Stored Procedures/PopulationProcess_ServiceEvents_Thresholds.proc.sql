/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_Thresholds' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_Thresholds
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_Thresholds
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId            BIGINT,
      
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

        DECLARE @thresholdTable AS TABLE (PopulationId BIGINT, PopulationMembershipId BIGINT, PopulationMembershipServiceEventId BIGINT, ServiceEventThresholdId BIGINT, ThresholdDate DATETIME, Status INT, ActionId BIGINT, ActionParameters XML, ActionDescription VARCHAR (099)) 

        /* LOCAL VARIABLES ( END ) */

        BEGIN TRANSACTION

        -- SELECT ALL SERVICE EVENT THRESHOLDS THAT HAVE PASSED AND NEED ACTION

        INSERT INTO @thresholdTable

        SELECT 
        
            PopulationMembership.PopulationId,

            PopulationMembership.PopulationMembershipId,

            PopulationMembershipServiceEvent.PopulationMembershipServiceEventId,

            PopulationServiceEventThreshold.PopulationServiceEventThresholdId,

            CASE 
                WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
              END AS ThresholdDate, 
              
            PopulationServiceEventThreshold.Status,
            
            PopulationServiceEventThreshold.ActionId,
            
            PopulationServiceEventThreshold.ActionParameters,
            
            PopulationServiceEventThreshold.ActionDescription

          FROM 

            PopulationMembership 

            JOIN PopulationMembershipServiceEvent
              ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId 

              JOIN PopulationServiceEventThreshold       
                ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEventThreshold.PopulationServiceEventId


          WHERE 
            (PopulationMembership.PopulationId = @populationId) AND (PopulationMembershipServiceEvent.EventDate IS NULL)

            -- THERSHOLD MUST OCCUR AFTER THE PREVIOUS THRESHOLD FOR THE SERVICE EVENT
            AND (ISNULL (PopulationMembershipServiceEvent.PreviousThresholdDate, '01/01/1900') < 
              CASE 
                  WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                  WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                  WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
                END 
              )

            -- THRESHOLD MUST OCCUR BEFORE TODAY
            AND (GETDATE () > 
              CASE 
                  WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                  WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                  WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
                END 
              )
            
          ORDER BY ThresholdDate      
          
        -- PROCESS THRESHOLDS IN SEQUENCE OVER TIME
                  
        DELETE FROM @thresholdTable 
        
          FROM @thresholdTable AS ThresholdTable
        
          LEFT JOIN (SELECT PopulationMembershipId, PopulationMembershipServiceEventId, MIN (ThresholdDate) AS Min_ThresholdDate FROM @thresholdTable GROUP BY PopulationMembershipId, PopulationMembershipServiceEventId) AS MinimumThresholdDate
          
            ON ThresholdTable.PopulationMembershipId = MinimumThresholdDate.PopulationMembershipId
            
            AND ThresholdTable.PopulationMembershipServiceEventId = MinimumThresholdDate.PopulationMembershipServiceEventId
            
            AND ThresholdTable.ThresholdDate = MinimumThresholdDate.Min_ThresholdDate
            
          WHERE (MinimumThresholdDate.PopulationMembershipId IS NULL)
          
        
        -- UPDATE POPULATION MEMBERSHIP SERVICE EVENT PREVIOUS SERVICE EVENT
        
        UPDATE PopulationMembershipServiceEvent

          SET 

            PreviousThresholdId = ThresholdTable.ServiceEventThresholdId,

            PreviousThresholdDate = ThresholdTable.ThresholdDate,
            
            Status = CASE WHEN (ThresholdTable.Status = 0) THEN PopulationMembershipServiceEvent.Status ELSE ThresholdTable.Status END

          FROM 

            PopulationMembershipServiceEvent

              JOIN @thresholdTable AS ThresholdTable 
            
                ON PopulationMembershipServiceEvent.PopulationMembershipServiceEventId = ThresholdTable.PopulationMembershipServiceEventId 
            
                    
          -- UPDATE POPULATION MEMBERSHIP SERVICE EVENT FOR NEXT EXPECTED THRESHOLD DATE                    
          UPDATE PopulationMembershipServiceEvent

            SET 

              NextThresholdDate = NextThreshold.ThresholdDate

            FROM
            
              PopulationMembershipServiceEvent

                JOIN (

                  SELECT 
                      PopulationMembershipServiceEvent.PopulationMembershipServiceEventId,
                      PopulationServiceEventThreshold.PopulationServiceEventThresholdId,

                      MIN ( 
                      CASE 
                          WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                          WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                          WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
                        END ) AS ThresholdDate

                    FROM 

                      PopulationMembership 

                      JOIN PopulationMembershipServiceEvent
                        ON PopulationMembership.PopulationMembershipId = PopulationMembershipServiceEvent.PopulationMembershipId 

                        JOIN PopulationServiceEventThreshold       
                          ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEventThreshold.PopulationServiceEventId


                    WHERE 
                      (PopulationMembership.PopulationId = @populationId) AND (PopulationMembershipServiceEvent.EventDate IS NULL)

                      -- THERSHOLD MUST OCCUR AFTER THE PREVIOUS THRESHOLD FOR THE SERVICE EVENT
                      AND (ISNULL (PopulationMembershipServiceEvent.PreviousThresholdDate, '01/01/1900') < 
                        CASE 
                            WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                            WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                            WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
                          END 
                        )

                      -- THRESHOLD MUST OCCUR AFTER TODAY
                      AND (GETDATE () <
                        CASE 
                            WHEN (RelativeDateQualifier = 0) THEN DATEADD (DAY,   RelativeDateValue, ExpectedEventDate) 
                            WHEN (RelativeDateQualifier = 1) THEN DATEADD (MONTH, RelativeDateValue, ExpectedEventDate) 
                            WHEN (RelativeDateQualifier = 2) THEN DATEADD (YEAR,  RelativeDateValue, ExpectedEventDate) 
                          END 
                        )
                      

                    GROUP BY 
                      PopulationMembershipServiceEvent.PopulationMembershipServiceEventId,
                      PopulationServiceEventThreshold.PopulationServiceEventThresholdId

                  
                ) AS NextThreshold

                ON PopulationMembershipServiceEvent.PopulationMembershipServiceEventId = NextThreshold.PopulationMembershipServiceEventId
          


          -- QUEUE THRESHOLD ACTIONS                
          INSERT INTO PopulationAction (PopulationId, PopulationMembershipId, 
          
				SenderObjectType, SenderId, EventInstanceId, ScheduleDate, 
				
				ActionDate, ActionId, ActionParameters, ActionDescription,
				
				Exception)
                 
            SELECT 

                PopulationId,

                PopulationMembershipId,

                'Mercury.Server.Core.Population.PopulationEvents.ServiceEventThreshold' AS SenderObjectType,

                ServiceEventThresholdId AS SenderId,
                
                PopulationMembershipServiceEventId AS EventInstanceId,

                CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS ScheduleDate,

                CAST (NULL AS DATETIME) AS ActionDate,

                ActionId, ActionParameters, ActionDescription,
                
                NULL AS Exception


              FROM 
              
                @thresholdTable AS ThresholdTable

              WHERE (ThresholdTable.ActionId > 0)
                        
                    
        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_ServiceEvents_Thresholds TO PUBLIC
GO          
*/