/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ActivityEvents' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ActivityEvents
GO      
*/

CREATE PROCEDURE [dbo].PopulationProcess_ActivityEvents
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
        
        DECLARE @rowsAffected AS INT
        
        DECLARE @activityTable AS TABLE (
        
            PopulationMembershipId BIGINT,
            
            EffectiveDate DATETIME,
            
            AnchorDate DATETIME,
            
            ActivityEventId BIGINT,
            
            ActionDescription VARCHAR (099),
            
            ScheduleQualifier INT,
            
            ScheduleValue INT,
                        
            PopulationActivityEvent_AnchorDate INT,
            
            Reoccuring BIT,
            
            PreviousActivityDate DATETIME,
            
            NextActivityDate DATETIME
            
          )
        
        /* LOCAL VARIABLES ( END ) */
        
        INSERT INTO @activityTable SELECT * FROM PopulationProcess_ActivityEvents_Schedule0 (@populationId)
        
        INSERT INTO @activityTable SELECT * FROM PopulationProcess_ActivityEvents_Schedule1_3 (@populationId)
        
        INSERT INTO @activityTable SELECT * FROM PopulationProcess_ActivityEvents_Schedule4 (@populationId)
        
        INSERT INTO @activityTable SELECT * FROM PopulationProcess_ActivityEvents_Schedule5 (@populationId)
        
        
        BEGIN TRANSACTION

          INSERT INTO PopulationMembershipActivityEvent

            SELECT 

                PopulationMembershipId,

                ActivityEventId,

                CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS ActivityDate,
                
                ActionDescription,

                @modifiedAuthorityName,

                @modifiedAccountId,

                @modifiedAccountName,

                GETDATE (),

                @modifiedAuthorityName,

                @modifiedAccountId,

                @modifiedAccountName,

                GETDATE ()

              FROM @activityTable
              
            SET @rowsAffected = @@ROWCOUNT            
            
            
            
          INSERT INTO PopulationAction (PopulationId, PopulationMembershipId, 
          
							SenderObjectType, SenderId, EventInstanceId, ScheduleDate, 
				
							ActionDate, ActionId, ActionParameters, ActionDescription,
				
							Exception)
				
            SELECT
            
                PopulationMembership.PopulationId,
                
                ActivityTable.PopulationMembershipId,
                
                'Mercury.Server.Core.Population.PopulationEvents.ActivityEvent' AS SenderObjectType,
                
                ActivityTable.ActivityEventId AS SenderId,
                
                PopulationMembershipActivityEvent.PopulationMembershipActivityEventId AS EventInstanceId,
                
                CASE 
                
                    WHEN (PopulationActivityEvent.PerformActionDateType = 0) THEN CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) -- IMMEDIATELY 
                    
                    WHEN (PopulationActivityEvent.PerformActionDateType = 1) THEN -- FIRST DAY OF MONTH
                    
                      CASE 
                      
                          WHEN (DAY (GETDATE ()) = 1) THEN CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) -- TODAY
                          
                          ELSE CAST (LTRIM (MONTH (DATEADD (MONTH, 1, GETDATE () ))) + '/01/' + LTRIM (YEAR (DATEADD (MONTH, 1, GETDATE () ))) AS DATETIME) -- FIRST OF NEXT MONTH
                          
                        END
                        
                    WHEN (PopulationActivityEvent.PerformActionDateType = 2) THEN 
                    
                      CASE 
                      
                          WHEN (DAY (GETDATE ()) <= 15) THEN CAST (LTRIM (MONTH (GETDATE () )) + '/15/' + LTRIM (YEAR (GETDATE ())) AS DATETIME) -- MIDDLE OF CURRENT MONTH
                          
                          ELSE CAST (LTRIM (MONTH (DATEADD (MONTH, 1, GETDATE () ))) + '/15/' + LTRIM (YEAR (DATEADD (MONTH, 1, GETDATE () ))) AS DATETIME) -- MIDDLE OF NEXT MONTH
                          
                        END
                        
                    WHEN (PopulationActivityEvent.PerformActionDateType = 3) THEN 
                    
                      DATEADD (DD, -1, CAST (LTRIM (MONTH (DATEADD (MONTH, 1, GETDATE () ))) + '/01/' + LTRIM (YEAR (DATEADD (MONTH, 1, GETDATE () ))) AS DATETIME)) -- LAST DAY OF MONTH                          
                    
                    ELSE CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) -- IMMEDIATELY 
                                  
                  END AS ScheduleDate,
                
                CAST (NULL AS DATETIME) AS ActionDate,
                
                PopulationActivityEvent.ActionId,
                
                PopulationActivityEvent.ActionParameters,
                
                PopulationActivityEvent.ActionDescription,
                
                NULL
                
              FROM
              
                @activityTable AS ActivityTable 
                
				          JOIN PopulationMembershipActivityEvent 
				          
				            ON ActivityTable.PopulationMembershipId = PopulationMembershipActivityEvent.PopulationMembershipId
				            
				              AND ActivityTable.ActivityEventId = PopulationMembershipActivityEvent.PopulationActivityEventId
				              
				              AND PopulationMembershipActivityEvent.ActivityDate = CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME)
                
                  JOIN PopulationMembership 
                  
                    ON ActivityTable.PopulationMembershipId = PopulationMembership.PopulationMembershipId
                    
                  JOIN PopulationActivityEvent 
                  
                    ON ActivityTable.ActivityEventId = PopulationActivityEvent.PopulationActivityEventId
                    
              WHERE (PopulationActivityEvent.ActionId > 0)
                
                

        COMMIT TRANSACTION
        
        
      /* STORED PROCEDURE ( END ) */
      
    END