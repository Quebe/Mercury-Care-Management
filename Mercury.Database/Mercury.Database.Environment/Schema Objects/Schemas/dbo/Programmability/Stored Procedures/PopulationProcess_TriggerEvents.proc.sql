/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_TriggerEvents' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_TriggerEvents
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_TriggerEvents
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

          DECLARE @MemberTriggerEvent AS TABLE (
              PopulationId                       BIGINT,
              PopulationMembershipId             BIGINT,
							MemberId                           BIGINT, 
              TriggerEventId                     BIGINT, 
              EventType                             INT, 
              ServiceId                          BIGINT, 
              MetricType                            INT, 
              MetricId                           BIGINT,
              MetricValue                       DECIMAL (20, 08),
              AuthorizedServiceId                BIGINT,
              TriggerDate                      DATETIME,
              EventDate                        DATETIME,
              MemberServiceId                    BIGINT,
              MemberServiceEventDate           DATETIME,
              MemberMetricId                     BIGINT, 
              MemberMetricEventDate            DATETIME,
              MemberAuthorizedServiceId          BIGINT,
              MemberAuthorizedServiceEventDate DATETIME,
              ProblemStatementId                 BIGINT,
              ActionId                           BIGINT,
              ActionParameters                      XML,
              ActionDescription                 VARCHAR (099)
           )

        /* LOCAL VARIABLES ( END ) */

        BEGIN TRANSACTION

          INSERT INTO @MemberTriggerEvent

            SELECT 
                -- PopulationMembershipTriggerEventId (UNIQUE ID)
                PopulationMembership.PopulationId,
                PopulationMembership.PopulationMembershipId,
								PopulationMembership.MemberId,
                PopulationTriggerEvent.PopulationTriggerEventId,
                PopulationTriggerEvent.TriggerEventType,
                PopulationTriggerEvent.ServiceId,
                PopulationTriggerEvent.MetricType,
                PopulationTriggerEvent.MetricId,
                CAST (0 AS DECIMAL (20, 08)) AS MetricValue,
                PopulationTriggerEvent.AuthorizedServiceId,
                GETDATE () AS TriggerDate,
                MemberService.EventDate AS EventDate,
                MemberService.MemberServiceId AS MemberServiceId,
                MemberService.EventDate AS MemberServiceEventDate,
                CAST (NULL AS BIGINT) AS MemberMetricId,
                CAST (NULL AS DATETIME) AS MemberMetricEventDate,
                CAST (NULL AS BIGINT) AS MemberAuthorizedServiceId,
                CAST (NULL AS DATETIME) AS MemberAuthorizedServiceEventDate,                
                PopulationTriggerEvent.ProblemStatementId,
                PopulationTriggerEvent.ActionId,
                PopulationTriggerEvent.ActionParameters,
                PopulationTriggerEvent.ActionDescription

              FROM 
                PopulationMembership

                  JOIN PopulationTriggerEvent 
                    ON PopulationMembership.PopulationId = PopulationTriggerEvent.PopulationId

                  JOIN MemberService 
                    ON PopulationMembership.MemberId = MemberService.MemberId
                      AND PopulationTriggerEvent.ServiceId = MemberService.ServiceId

                  LEFT JOIN PopulationMembershipTriggerEvent AS UtilizedMemberService
                    ON PopulationMembership.PopulationMembershipId = UtilizedMemberService.PopulationMembershipId 
                    AND PopulationTriggerEvent.PopulationTriggerEventId = UtilizedMemberService.PopulationTriggerEventId 
                    AND MemberService.MemberServiceId = UtilizedMemberService.MemberServiceId
                    
                  LEFT JOIN PopulationMembershipTriggerEvent AS PreviouslyFiredTriggerEvent
                    ON PopulationMembership.PopulationMembershipId = PreviouslyFiredTriggerEvent.PopulationMembershipId
                    AND PopulationTriggerEvent.PopulationTriggerEventId = PreviouslyFiredTriggerEvent.PopulationTriggerEventId
                    AND MemberService.EventDate = PreviouslyFiredTriggerEvent.EventDate

              WHERE 

                -- SPECIFIC POPULATION AND SERVICE EVENTS 
                (PopulationMembership.PopulationId = @populationId) AND (PopulationTriggerEvent.TriggerEventType = 0)
                
                -- CURRENT SEGMENT
                AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)                

                -- SERVICE OCCURRED DURING POPULATION MEMBERSHIP
                -- AND (MemberService.EventDate BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)
                
                -- SERVICE OCCURRED DURING POPULATION MEMBERSHIP ANCHOR DATE AND TERMINATION DATE
                AND (MemberService.EventDate BETWEEN PopulationMembership.AnchorDate AND PopulationMembership.TerminationDate)

                -- SERVICE NOT PREVIOUSLY USED FOR SAME TRIGGER EVENT
                AND (UtilizedMemberService.MemberServiceId IS NULL)
                
                -- TRIGGER HAS NOT ALREADY FIRED ON THE SAME EVENT DATE
                AND (PreviouslyFiredTriggerEvent.EventDate IS NULL)



          INSERT INTO @MemberTriggerEvent
          
            SELECT 
                -- PopulationMembershipTriggerEventId (UNIQUE ID)
                PopulationMembership.PopulationId,
                PopulationMembership.PopulationMembershipId,
								PopulationMembership.MemberId,
                PopulationTriggerEvent.PopulationTriggerEventId,
                PopulationTriggerEvent.TriggerEventType,
                CAST (0 AS BIGINT) AS ServiceId, 
                PopulationTriggerEvent.MetricType,
                PopulationTriggerEvent.MetricId,
                MemberMetric.MetricValue AS MetricValue,
                PopulationTriggerEvent.AuthorizedServiceId,
                GETDATE () AS TriggerDate,
                MemberMetric.EventDate AS EventDate,
                CAST (NULL AS BIGINT) AS MemberServiceId,
                CAST (NULL AS DATETIME) AS MemberServiceEventDate,
                MemberMetric.MemberMetricId AS MemberMetricId,
                MemberMetric.EventDate AS MemberMetricEventDate,
                CAST (NULL AS BIGINT) AS MemberAuthorizedServiceId,
                CAST (NULL AS DATETIME) AS MemberAuthorizedServiceEventDate,
                PopulationTriggerEvent.ProblemStatementId,
                PopulationTriggerEvent.ActionId,
                PopulationTriggerEvent.ActionParameters,
                PopulationTriggerEvent.ActionDescription

              FROM 
                PopulationMembership

                  JOIN PopulationTriggerEvent 
                    ON PopulationMembership.PopulationId = PopulationTriggerEvent.PopulationId
                    
                  JOIN MemberMetric
                    ON PopulationMembership.MemberId = MemberMetric.MemberId
                      AND PopulationTriggerEvent.MetricId = MemberMetric.MetricId

                  LEFT JOIN PopulationMembershipTriggerEvent AS UtilizedMemberMetric
                    ON PopulationMembership.PopulationMembershipId = UtilizedMemberMetric.PopulationMembershipId 
                    AND PopulationTriggerEvent.PopulationTriggerEventId = UtilizedMemberMetric.PopulationTriggerEventId 
                    AND MemberMetric.MemberMetricId = UtilizedMemberMetric.MemberMetricId

                  LEFT JOIN PopulationMembershipTriggerEvent AS PreviouslyFiredTriggerEvent
                    ON PopulationMembership.PopulationMembershipId = PreviouslyFiredTriggerEvent.PopulationMembershipId
                    AND PopulationTriggerEvent.PopulationTriggerEventId = PreviouslyFiredTriggerEvent.PopulationTriggerEventId
                    AND MemberMetric.EventDate = PreviouslyFiredTriggerEvent.EventDate
                    
              WHERE 
              
                -- SPECIFIC POPULATION AND SERVICE EVENTS 
                (PopulationMembership.PopulationId = @populationId) AND (PopulationTriggerEvent.TriggerEventType = 1)

                -- CURRENT SEGMENT
                AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)                

                -- METRIC OCCURRED DURING POPULATION MEMBERSHIP
                -- AND (MemberMetric.EventDate BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)
                
                -- METRIC OCCURRED DURING POPULATION MEMBERSHIP ANCHOR DATE AND TERMINATION DATE
                AND (MemberMetric.EventDate BETWEEN PopulationMembership.AnchorDate AND PopulationMembership.TerminationDate)


                -- METRIC WITHING THRESHOLD VALUES
                AND ((MemberMetric.MetricValue BETWEEN PopulationTriggerEvent.MetricMinimum AND PopulationTriggerEvent.MetricMaximum))

                -- METRIC NOT PREVIOUSLY USED FOR SAME TRIGGER 
                AND (UtilizedMemberMetric.MemberMetricId IS NULL)

                -- TRIGGER HAS NOT ALREADY FIRED ON THE SAME EVENT DATE
                AND (PreviouslyFiredTriggerEvent.EventDate IS NULL)


          INSERT INTO @MemberTriggerEvent

            SELECT 
                -- PopulationMembershipTriggerEventId (UNIQUE ID)
                PopulationMembership.PopulationId,
                PopulationMembership.PopulationMembershipId,
								PopulationMembership.MemberId,
                PopulationTriggerEvent.PopulationTriggerEventId,
                PopulationTriggerEvent.TriggerEventType,
                PopulationTriggerEvent.ServiceId,
                PopulationTriggerEvent.MetricType,
                PopulationTriggerEvent.MetricId,
                CAST (0 AS DECIMAL (20, 08)) AS MetricValue,
                PopulationTriggerEvent.AuthorizedServiceId,
                GETDATE () AS TriggerDate,
                MemberAuthorizedService.EventDate AS EventDate,
                CAST (NULL AS BIGINT) AS MemberServiceId,
                CAST (NULL AS DATETIME) AS MemberServiceEventDate,
                CAST (NULL AS BIGINT) AS MemberMetricId,
                CAST (NULL AS DATETIME) AS MemberMetricEventDate,
                MemberAuthorizedService.MemberAuthorizedServiceId AS MemberAuthorizedServiceId,
                MemberAuthorizedService.EventDate AS MemberAuthorizedServiceEventDate,
                PopulationTriggerEvent.ProblemStatementId,
                PopulationTriggerEvent.ActionId,
                PopulationTriggerEvent.ActionParameters,
                PopulationTriggerEvent.ActionDescription

              FROM 
                PopulationMembership

                  JOIN PopulationTriggerEvent 
                    ON PopulationMembership.PopulationId = PopulationTriggerEvent.PopulationId

                  JOIN MemberAuthorizedService 
                    ON PopulationMembership.MemberId = MemberAuthorizedService.MemberId
                      AND PopulationTriggerEvent.AuthorizedServiceId = MemberAuthorizedService.AuthorizedServiceId

                  LEFT JOIN PopulationMembershipTriggerEvent AS UtilizedMemberAuthorizedService
                    ON PopulationMembership.PopulationMembershipId = UtilizedMemberAuthorizedService.PopulationMembershipId 
                    AND PopulationTriggerEvent.PopulationTriggerEventId = UtilizedMemberAuthorizedService.PopulationTriggerEventId 
                    AND MemberAuthorizedService.MemberAuthorizedServiceId = UtilizedMemberAuthorizedService.MemberAuthorizedServiceId

                  LEFT JOIN PopulationMembershipTriggerEvent AS PreviouslyFiredTriggerEvent
                    ON PopulationMembership.PopulationMembershipId = PreviouslyFiredTriggerEvent.PopulationMembershipId
                    AND PopulationTriggerEvent.PopulationTriggerEventId = PreviouslyFiredTriggerEvent.PopulationTriggerEventId
                    AND MemberAuthorizedService.EventDate = PreviouslyFiredTriggerEvent.EventDate
                    
              WHERE 

                -- SPECIFIC POPULATION AND SERVICE EVENTS 
                (PopulationMembership.PopulationId = @populationId) AND (PopulationTriggerEvent.TriggerEventType = 2)

                -- CURRENT SEGMENT
                AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)                

                -- SERVICE OCCURRED DURING POPULATION MEMBERSHIP
                -- AND (MemberAuthorizedService.EventDate BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate)                
                
                -- AUTHORIZED SERVICE OCCURRED DURING POPULATION MEMBERSHIP ANCHOR DATE AND TERMINATION DATE
                AND (MemberAuthorizedService.EventDate BETWEEN PopulationMembership.AnchorDate AND PopulationMembership.TerminationDate)


                -- SERVICE NOT PREVIOUSLY USED FOR SAME TRIGGER EVENT
                AND (UtilizedMemberAuthorizedService.MemberAuthorizedServiceId IS NULL)

                -- TRIGGER HAS NOT ALREADY FIRED ON THE SAME EVENT DATE
                AND (PreviouslyFiredTriggerEvent.EventDate IS NULL)


          INSERT INTO PopulationMembershipTriggerEvent
          
            SELECT 

                PopulationMembershipId, TriggerEventId, TriggerDate, EventDate, MemberServiceId, MemberMetricId, MemberAuthorizedServiceId, ProblemStatementId, ActionDescription,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()

              FROM @MemberTriggerEvent 
              


          INSERT INTO PopulationAction (PopulationId, PopulationMembershipId, 
          
				SenderObjectType, SenderId, EventInstanceId, ScheduleDate, 
				
				ActionDate, ActionId, ActionParameters, ActionDescription,
				
				Exception)
          
            SELECT 

                MemberTriggerEvent.PopulationId,

                MemberTriggerEvent.PopulationMembershipId,

                'Mercury.Server.Core.Population.PopulationEvents.TriggerEvent' AS SenderObjectType,

                MemberTriggerEvent.TriggerEventId AS SenderId,
                                
                PopulationMembershipTriggerEvent.PopulationMembershipTriggerEventId AS EventInstanceId,

                CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS ScheduleDate,

                CAST (NULL AS DATETIME) AS ActionDate,

                MemberTriggerEvent.ActionId, MemberTriggerEvent.ActionParameters, MemberTriggerEvent.ActionDescription,
                
                NULL AS Exception


              FROM 
              
                @MemberTriggerEvent AS MemberTriggerEvent
                
                  JOIN PopulationMembershipTriggerEvent 
                  
                    ON MemberTriggerEvent.PopulationMembershipId = PopulationMembershipTriggerEvent.PopulationMembershipId
                    
                    AND MemberTriggerEvent.TriggerEventId = PopulationMembershipTriggerEvent.PopulationTriggerEventId
                    
                    AND MemberTriggerEvent.TriggerDate = PopulationMembershipTriggerEvent.TriggerDate
                    
                    AND ((MemberTriggerEvent.MemberServiceId = PopulationMembershipTriggerEvent.MemberServiceId) OR (MemberTriggerEvent.MemberServiceId IS NULL))
                    
                    AND ((MemberTriggerEvent.MemberMetricId = PopulationMembershipTriggerEvent.MemberMetricId) OR (MemberTriggerEvent.MemberMetricId IS NULL))
                    
                    AND ((MemberTriggerEvent.MemberAuthorizedServiceId = PopulationMembershipTriggerEvent.MemberAuthorizedServiceId) OR (MemberTriggerEvent.MemberAuthorizedServiceId IS NULL))
                    
                    AND ((MemberTriggerEvent.ProblemStatementId = PopulationMembershipTriggerEvent.ProblemStatementId) OR (MemberTriggerEvent.ProblemStatementId IS NULL))

              WHERE (MemberTriggerEvent.ActionId > 0)



						-- MEMBER PROBLEM STATEMENT IDENTIFIED
						DECLARE @memberId                BIGINT
						DECLARE @problemStatementId      BIGINT
						DECLARE @isRequired                 BIT

						DECLARE @senderObjectType       VARCHAR (120)
						DECLARE @senderObjectId          BIGINT
						DECLARE @eventObjectType        VARCHAR (120)
						DECLARE @eventObjectId           BIGINT
						DECLARE @eventInstanceId         BIGINT
						DECLARE @eventDescription       VARCHAR (999)

						DECLARE @priority INT


						DECLARE TriggerProblemStatementCursor CURSOR FOR 
                        
							SELECT 

									MemberTriggerEvent.MemberId,

									MemberTriggerEvent.ProblemStatementId,

									0 AS IsRequired,

									'Mercury.Server.Core.Population.PopulationEvents.TriggerEvent' AS SenderObjectType,

									MemberTriggerEvent.TriggerEventId AS SenderObjectId,

									'Mercury.Server.Core.Population.PopulationEvents.PopulationMembershipTriggerEvent' AS EventObjectType,
								
									PopulationMembershipTriggerEvent.PopulationMembershipTriggerEventId AS EventObjectId,

									PopulationMembershipTriggerEvent.PopulationMembershipTriggerEventId AS EventInstanceId,

									'Population Membership Trigger Event' AS EventDescription,

									0 AS Priority
								
								FROM 
              
									@MemberTriggerEvent AS MemberTriggerEvent
                
										JOIN PopulationMembershipTriggerEvent 
                  
											ON MemberTriggerEvent.PopulationMembershipId = PopulationMembershipTriggerEvent.PopulationMembershipId
                    
											AND MemberTriggerEvent.TriggerEventId = PopulationMembershipTriggerEvent.PopulationTriggerEventId
                    
											AND MemberTriggerEvent.TriggerDate = PopulationMembershipTriggerEvent.TriggerDate
                    
											AND ((MemberTriggerEvent.MemberServiceId = PopulationMembershipTriggerEvent.MemberServiceId) OR (MemberTriggerEvent.MemberServiceId IS NULL))
                    
											AND ((MemberTriggerEvent.MemberMetricId = PopulationMembershipTriggerEvent.MemberMetricId) OR (MemberTriggerEvent.MemberMetricId IS NULL))
                    
											AND ((MemberTriggerEvent.MemberAuthorizedServiceId = PopulationMembershipTriggerEvent.MemberAuthorizedServiceId) OR (MemberTriggerEvent.MemberAuthorizedServiceId IS NULL))
                    
											AND ((MemberTriggerEvent.ProblemStatementId = PopulationMembershipTriggerEvent.ProblemStatementId) OR (MemberTriggerEvent.ProblemStatementId IS NULL))

								WHERE (MemberTriggerEvent.ProblemStatementId > 0)

						OPEN TriggerProblemStatementCursor
						
						FETCH NEXT FROM TriggerProblemStatementCursor

							INTO @memberId, @problemStatementId, @isRequired, @senderObjectType, @senderObjectId, @eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, @priority

						WHILE (@@FETCH_STATUS <> -1) 

							BEGIN

								EXEC MemberProblemStatementIdentified_Insert @memberId, @problemStatementId, @isRequired, @senderObjectType, @senderObjectId, 

										@eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, @priority, @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName
										
								FETCH NEXT FROM TriggerProblemStatementCursor

									INTO @memberId, @problemStatementId, @isRequired, @senderObjectType, @senderObjectId, @eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, @priority

							END 

						CLOSE TriggerProblemStatementCursor

						DEALLOCATE TriggerProblemStatementCursor

/*                        
                        
			-- RESERVED FOR FUTURE USE                        
                        
          INSERT INTO MemberProblemStatementIdentified 
          
            SELECT 
              
                ProblemStatement.ProblemStatementId AS ProblemStatementId,
                
                CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS IdentifiedDate,
                
                'Mercury.Server.Core.Population.PopulationEvents.TriggerEvent' AS SenderObjectType,

                PopulationTriggerEvent.TriggerEventId AS SenderId,
              
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()

            
              FROM 
              
                @MemberTriggerEvent AS MemberTriggerEvent
                
                  JOIN PopulationTriggerEvent ON MemberTriggerEvent.TriggerEventId = PopulationTriggerEvent.TriggerEventId
              
                  JOIN ProblemStatement ON PopulationTriggerEvent.ProblemStatementId = ProblemStatement.ProblemStatementId
             
*/                  
             
        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_TriggerEvents TO PUBLIC
GO          
*/