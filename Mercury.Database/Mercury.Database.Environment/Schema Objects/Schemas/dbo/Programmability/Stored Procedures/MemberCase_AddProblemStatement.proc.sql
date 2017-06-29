/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_AddProblemStatement' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_AddProblemStatement
GO      
*/

CREATE PROCEDURE dbo.MemberCase_AddProblemStatement
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,
      @problemStatementId         BIGINT,
      @isSingleInstance              BIT,
      
      @ignoreAssignedTo       BIGINT,
      
      @modifiedAuthorityId     BIGINT,
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060), 
      
      @lastModifiedDate       DATETIME ,  -- LAST MODIFIED DATE OF THE ORIGINAL RECORD
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
    
			@modifiedDate DATETIME OUTPUT -- OUTPUT OF THE MODIFIED DATE ON SUCCESS 
    
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS
   
    BEGIN
    
    
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

				DECLARE @validationError AS INT
				
				SET @validationError = 1 -- NOT FOUND 
				
				
				DECLARE @memberCaseProblemClassId AS BIGINT
				
				DECLARE @memberCaseCarePlanId AS BIGINT
				
				DECLARE @defaultCarePlanId AS BIGINT
				
				DECLARE @memberCaseProblemCarePlanCount AS INT
				
				DECLARE @memberCaseProblemCarePlanProblemStatement AS BIGINT


				DECLARE @carePlanGoalId AS BIGINT

				DECLARE @carePlanInterventionId AS BIGINT

				DECLARE @careInterventionId AS BIGINT

				DECLARE @memberCaseCarePlanGoalId AS BIGINT

				DECLARE @memberCaseCareInterventionId AS BIGINT
				
        /* LOCAL VARIABLES ( END ) */
                
                
				-- TO ADD A PROBLEM: 
				
				-- 1. THE CASE MUST BE UNDER DEVELOPMENT OR ACTIVE
				
				-- 2. THE CASE MUST NOT BE LOCKED
				
				-- 3. THE CASE MUST BE UNASSIGNED OR THE USER A MEMBER OF THE CARE TEAM ASSIGNED TO THE CASE
				
				-- 4. THE PROBLEM STATEMENT MUST NOT ALREADY EXIST IN ACTIVE                 
                
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION
        
				SELECT @validationError = 
        
						CASE 
						
							  WHEN (MemberCase.Status NOT IN (1, 2)) THEN 7 -- INVALID STATUS 
						
								-- WHEN (MemberCase.ModifiedDate <> @lastModifiedDate) THEN 3		-- RECORD MODIFIED 
								
								WHEN ((MemberCase.LockedByDate IS NOT NULL) AND ((MemberCase.LockedBySecurityAuthorityId <> @modifiedAuthorityId) AND (MemberCase.LockedByUserAccountId <> @modifiedAccountId))) THEN 4 -- LOCKED
								
								WHEN ((MemberCase.AssignedToWorkTeamDate IS NOT NULL) AND (WorkTeamMembership.WorkTeamId IS NULL)
								
									AND (@ignoreAssignedTo = 0)) THEN 5 -- ASSIGNED TO
																		
								ELSE 0
									
							END 
							
					FROM 
					
						dbo.MemberCase
						
							LEFT JOIN dbo.WorkTeam 
							
								ON MemberCase.AssignedToWorkTeamId = WorkTeam.WorkTeamId

							LEFT JOIN dbo.WorkTeamMembership 
							
								ON WorkTeam.WorkTeamId = WorkTeamMembership.WorkTeamId
								
									AND ((WorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (WorkTeamMembership.UserAccountId = @modifiedAccountId))
									
					WHERE MemberCase.MemberCaseId = @memberCaseId
						
						
				-- 4. THE PROBLEM STATEMENT MUST NOT ALREADY EXIST IN ACTIVE
				
				--    EITHER THE NEW REQUEST IS FOR A SINGLE INSTANCE AND NO OTHER ACTIVE SINGLE INSTANCE EXISTS 
				
				--      OR THERE IS NO CURRENT PROBLEM STATEMENT WITH ACTIVE STATUS    
				
				--    SINGLE -> SHARED = ADD 
				
				--    SHARED -> SINGLE = ADD ONLY IF PROBLEM STATEMENT IS DIFFERENT THAN SINGLE, OTHERWISE EXISTS
				
				--    SINGLE -> SINGLE = EXISTS
				
				--    SHARED -> SHARED = EXISTS             
                
				SELECT @validationError = 8
						
					FROM 
					
						dbo.MemberCase
						
							JOIN dbo.MemberCaseProblemClass 
							
								ON MemberCase.MemberCaseId = MemberCaseProblemClass.MemberCaseId
								
							JOIN dbo.MemberCaseProblemCarePlan
							
								ON MemberCaseProblemClass.MemberCaseProblemClassId = MemberCaseProblemCarePlan.MemberCaseProblemClassId 
								
								AND (MemberCaseProblemCarePlan.ProblemStatementId = @problemStatementId)
						
							JOIN dbo.MemberCaseCarePlan
							
								ON MemberCaseProblemCarePlan.MemberCaseCarePlanId = MemberCaseCarePlan.MemberCaseCarePlanId
								
								AND (MemberCaseCarePlan.Status IN (1, 2)) -- UNDER DEVELOPMENT OR ACTIVE
								
					WHERE MemberCase.MemberCaseId = @memberCaseId
					
						AND ((MemberCaseProblemCarePlan.IsSingleInstance = @isSingleInstance) -- THE SHARED/SINGLE INSTANCE MATCHES THE REQUEST
						
							OR ( -- OR THE PROBLEM STATEMENT MATCHS AND THE REQUEST TO MOVE FROM SINGLE TO SHARED, WHICH IS NOT POSSIBLE
							
									  (MemberCaseProblemCarePlan.ProblemStatementId = @problemStatementId) 
							
								AND (MemberCaseProblemCarePlan.IsSingleInstance = 1) 
								
								AND (@isSingleInstance = 0)
								
								) 
								
							) 
						
								
				-- DETERMINE IF ERROR DETECTED AND ROLLBACK TRANSACTION 						
						
				IF (@validationError <> 0) 
				
					BEGIN 
					
						ROLLBACK TRANSACTION 
						
						RETURN @validationError
						
				 END 
    
    
        -- CHECK FOR EXISTING CLASS (SET) RECORD, APPEND TO EXISTING, OR CREATE NEW ONE
        
        SET @modifiedDate = GETDATE ()
        
        SELECT @memberCaseProblemClassId = MemberCaseProblemClass.MemberCaseProblemClassId
        
					FROM 
					
						dbo.MemberCase
						
							JOIN dbo.MemberCaseProblemClass 
							
								ON MemberCase.MemberCaseId = MemberCaseProblemClass.MemberCaseId

								AND MemberCaseProblemClass.ProblemClassId = (SELECT ProblemClassId FROM ProblemStatement WHERE ProblemStatementId = @problemStatementId)					
        
					WHERE MemberCase.MemberCaseId = @memberCaseId
					
					
				IF (@memberCaseProblemClassId IS NULL) 
				
				  BEGIN -- INSERT NEW MEMBER CASE PROBLEM CLASS (SET) 
				  
						INSERT INTO MemberCaseProblemClass 
						
								SELECT 
								
									-- IDENTITY PROPERTY
									
									@memberCaseId,
									
									ProblemStatement.ProblemClassId,
									
									NULL, '', '', '', NULL,  -- ASSIGNED TO USER
									
									NULL, NULL,							 -- ASSIGNED TO PROVIDER
					  
									NULL AS ExtendedProperties,
									
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

								FROM 
								
									dbo.ProblemStatement
									
								WHERE 
								
									ProblemStatement.ProblemStatementId = @problemStatementId
											
						SET @memberCaseProblemClassId = @@IDENTITY
				
				  END 
				

				-- MARK ANY OPEN IDENTIFIED MEMBER PROBLEM STATEMENTS AS ALLOCATED FOR MATCHING PROBLEM STATEMENT

				UPDATE MemberProblemStatementIdentified 

					SET 

						MemberCaseId = @memberCaseId,

						CompletionDate = @modifiedDate,

						ModifiedAuthorityName = @modifiedAuthorityName,

						ModifiedAccountId = @modifiedAccountId,

						ModifiedAccountName = @modifiedAccountName

					WHERE 

						(MemberProblemStatementIdentified.MemberId = (SELECT MemberId FROM MemberCase WHERE MemberCaseId = @memberCaseId))

							AND (ProblemStatementId = @problemStatementId)

							AND (MemberProblemStatementIdentified.CompletionDate IS NULL) 

							-- ADD ANY ADDITIONAL LOGIC HERE TO SINGLE OUT OPEN/UNASSIGNED IDENTIFIED PROBLEM STATEMENTS
				
				
				-- GET THE DEFAULT CARE PLAN FOR THE REQUESTED PROBLEM STATMENT
				
				SELECT @defaultCarePlanId = DefaultCarePlanId FROM ProblemStatement WHERE ProblemStatementId = @problemStatementId
				
				
				-- CHECK FOR EXISTING CARE PLAN RECORD, LINK TO EXISTING IF SHARED, OR CREATE NEW IF INSTANCE 
				
				SELECT @memberCaseCarePlanId = MemberCaseCarePlan.MemberCaseCarePlanId
				
					FROM 
					
						MemberCaseProblemClass
						
							JOIN MemberCaseProblemCarePlan 
							
								ON MemberCaseProblemClass.MemberCaseProblemClassId = MemberCaseProblemCarePlan.MemberCaseProblemClassId
								
									AND MemberCaseProblemCarePlan.IsSingleInstance = 0
							
							JOIN MemberCaseCarePlan 
							
								ON MemberCaseProblemCarePlan.MemberCaseCarePlanId = MemberCaseCarePlan.MemberCaseCarePlanId
							
					WHERE 
					
						MemberCaseProblemClass.MemberCaseId = @memberCaseId
						
--							AND MemberCaseProblemCarePlan.ProblemStatementId = @problemStatementId
							
							AND MemberCaseCarePlan.CarePlanId = @defaultCarePlanId
						
						
						
				-- DETERMINE IF A NEW PLAN SHOULD BE CREATED OR THE OLD ONE LINKED TO						
				
				IF (@memberCaseCarePlanId IS NOT NULL) -- THIS REPRESENTS AN EXISTING SHARED CARE PLAN
				
					BEGIN
					
						-- IDENTIFY HOW MANY PROBLEM STATEMENTS ARE LINKED TO THIS PLAN
						
						SELECT @memberCaseProblemCarePlanCount = COUNT (1), @memberCaseProblemCarePlanProblemStatement = MIN (ProblemStatementId)
						
							FROM MemberCaseProblemCarePlan
							
							WHERE MemberCaseCarePlanId = @memberCaseCarePlanId
							
							
						-- IF REQUEST IS TO CHANGE FROM LINKED TO SINGLE:
						
						--   1. IF ONLY PROBLEM STATEMENT ATTACHED TO CARE PLAN, FLIP SWITCH AND RETURN
						
						--   2. IF ONE OF MANY PROBLEM STATEMENTS ATTACHED TO CARE PLAN, DELETE LINK AND CLEAR CARE PLAN ID TO CREATE A NEW ONE
					
					
						IF ((@memberCaseProblemCarePlanCount = 1) AND (@memberCaseProblemCarePlanProblemStatement = @problemStatementId) AND (@isSingleInstance = 1)) 
						
							BEGIN 
							
								-- UPDATE EXISTING SHARED LINK WITH ONE PROBLEM STATEMENT TO EXISTING SINGLE INSTANCE FOR THAT CARE PLAN
								
								UPDATE MemberCaseProblemCarePlan
								
									SET IsSingleInstance = 1
									
									WHERE MemberCaseCarePlanId = @memberCaseCarePlanId
									
								COMMIT TRANSACTION 
								
								RETURN 0
								
							END 
							
						ELSE IF ((@memberCaseProblemCarePlanCount > 1) AND (@isSingleInstance = 1))
						
							BEGIN
							
								-- DELETE EXISTING LINK TO ALLOW CREATING A NEW LINK AS SINGLE INSTANCE
								
								DELETE FROM MemberCaseProblemCarePlan
								
									WHERE MemberCaseCarePlanId = @memberCaseCarePlanId
										
										AND ProblemStatementId = @problemStatementId
										
								SET @memberCaseCarePlanId = NULL
							
						  END 
						
					END
						
						
				IF (@memberCaseCarePlanId IS NULL) 
				
					BEGIN -- ACTIVE CARE PLAN DOES NOT EXIST, ADD NEW ONE 
							
						INSERT INTO MemberCaseCarePlan
		        
							SELECT 

									-- IDENTITY PROPERTY
									
									@memberCaseId,										
									
									CarePlan.CarePlanId,
								  
									CAST (1 AS INT) AS Status, -- UNDER DEVELOPMENT
									
									@modifiedDate AS AddedDate,
									
									-- NULL, '', '', '', NULL, -- ASSIGNED TO MOVED TO MEMBER CASE PROBLEM CLASS
									
									NULL, -- CareOutcomeId
									
									CAST ('01/01/1900' AS DATETIME) AS EffectiveDate,
									
									CAST ('12/31/9999' AS DATETIME) AS TerminationDate,
									
									CarePlan.ExtendedProperties AS ExtendedProperties,
									
									CarePlan.Enabled AS Enabled,
									
									CarePlan.Visible AS Visible,

									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

								FROM 
								
									dbo.ProblemStatement
									
										JOIN dbo.CarePlan 
										
											ON ProblemStatement.DefaultCarePlanId = CarePlan.CarePlanId
											
								WHERE 
								
									ProblemStatement.ProblemStatementId = @problemStatementId
						
						SET @memberCaseCarePlanId = @@IDENTITY
				
				
						-- INSERT DEFAULT GOALS AND INTERVENTIONS
				
						SELECT @defaultCarePlanId = DefaultCarePlanId FROM dbo.ProblemStatement WHERE ProblemStatement.ProblemStatementId = @problemStatementId

						DECLARE defaultCarePlanGoalCursor CURSOR FOR

							SELECT CarePlanGoalId FROM CarePlanGoal WHERE CarePlanId = @defaultCarePlanId AND Enabled = 1

						OPEN defaultCarePlanGoalCursor 

						FETCH NEXT FROM defaultCarePlanGoalCursor INTO @carePlanGoalId

						WHILE (@@FETCH_STATUS <> -1) 

							BEGIN 
							
								INSERT INTO MemberCaseCarePlanGoal (

											MemberCaseCarePlanGoalName, MemberCaseCareplanGoalDescription, MemberCaseCarePlanId, CarePlanGoalId,

											Status, ClinicalNarrative, CommonNarrative, GoalTimeframe, ScheduleValue, ScheduleQualifier, CareMeasureId, 

											ExtendedProperties, Enabled, Visible,

											CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, 
											
											ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)

							
									SELECT 
							
											-- IDENTITY PROPERTY
									
											CarePlanGoal.CarePlanGoalName,
									
											CarePlanGoal.CarePlanGoalDescription,
									
											@memberCaseCarePlanId,
									
											CarePlanGoal.CarePlanGoalId,

											1, -- STATUS: UNDER DEVELOPMENT
									
											CarePlanGoal.ClinicalNarrative,
									
											CarePlanGoal.CommonNarrative,
									
											CarePlanGoal.GoalTimeframe,
									
											CarePlanGoal.ScheduleValue,
									
											CarePlanGoal.ScheduleQualifier,
									
											CarePlanGoal.CareMeasureId,
									
											CarePlanGoal.ExtendedProperties,
									
											CarePlanGoal.Enabled, CarePlanGoal.Visible,
									
											@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
											@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate
									
										FROM 
								
											dbo.CarePlanGoal 
									
										WHERE 
								
											CarePlanGoal.CarePlanId = @defaultCarePlanId

												AND CarePlanGoal.CarePlanGoalId = @carePlanGoalId

												AND CarePlanGoal.Inclusion = 1 -- REQUIRED ONLY 

								SET @memberCaseCarePlanGoalId = @@IDENTITY
							
								-- FOR EACH GOAL INSERT INTERVENTIONS (IF NOT ALREADY EXISTING, OTHERWISE JUST CREATE LINK)

								DECLARE defaultCarePlanInterventionCursor CURSOR FOR

									SELECT CarePlanInterventionId, CareInterventionId FROM CarePlanIntervention WHERE CarePlanGoalId = @carePlanGoalId AND Enabled = 1 AND Inclusion = 1

								OPEN defaultCarePlanInterventionCursor 

								FETCH NEXT FROM defaultCarePlanInterventionCursor INTO @carePlanInterventionId, @careInterventionId

								WHILE (@@FETCH_STATUS <> -1) 

									BEGIN 
							
										-- DETERMINE IF THE CARE INTERVENTION IS ALREADY AVAILABLE UNDER THE MEMBER CASE

										SET @memberCaseCareInterventionId = NULL

										SELECT @memberCaseCareInterventionId = MemberCaseCareInterventionId 
								
											FROM MemberCaseCareIntervention 

											WHERE MemberCaseCareIntervention.MemberCaseId = @memberCaseId
									
												AND MemberCaseCareIntervention.CareInterventionId = @careInterventionId

												AND MemberCaseCareIntervention.Status IN (1, 2)

										IF (@memberCaseCareInterventionId IS NULL) 

											BEGIN
										
												INSERT INTO MemberCaseCareIntervention
						
													SELECT 
							
															-- IDENTITY PROPERTY
									
															CarePlanIntervention.CarePlanInterventionName,
									
															CarePlanIntervention.CarePlanInterventionDescription,
									
															@memberCaseId,
									
															CarePlanIntervention.CareInterventionId,
									
															1, -- STATUS
									
															CarePlanIntervention.ExtendedProperties,
									
															CarePlanIntervention.Enabled, CarePlanIntervention.Visible,
									
															@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
															@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate
									
														FROM 
								
															dbo.CarePlanIntervention
									
														WHERE 
								
															CarePlanGoalId = @carePlanGoalId

																AND CarePlanIntervention.CareInterventionId = @careInterventionId
									
																AND CarePlanIntervention.Inclusion IN (1) -- REQUIRED

												SET @memberCaseCareInterventionId = @@IDENTITY
												
												INSERT INTO MemberCaseCareInterventionActivity
							
													SELECT

															-- IDENTITY

															CareInterventionActivity.CareInterventionActivityName,

															CareInterventionActivity.CareInterventionActivityDescription,

															@memberCaseCareInterventionId,
									
															CareInterventionActivity.CareInterventionActivityId,

															CareInterventionActivity.ClinicalNarrative,

															CareInterventionActivity.CommonNarrative,

															CareInterventionActivity.CareInterventionActivityType,

															CareInterventionActivity.ActivityType,

															CareInterventionActivity.IsReoccurring,

															CareInterventionActivity.InitialAnchorDate,

															CareInterventionActivity.AnchorDate,

															CareInterventionActivity.ScheduleType,

															CareInterventionActivity.ScheduleValue,

															CareInterventionActivity.ScheduleQualifier,

															CareInterventionActivity.ConstraintValue, 

															CareInterventionActivity.ConstraintQualifier,

															CareInterventionActivity.PerformActionDate,

															CareInterventionActivity.ActionId,

															CareInterventionActivity.ActionParameters,

															CareInterventionActivity.ActionDescription,
									
															@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
															@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

														FROM 

																dbo.CareIntervention

																JOIN dbo.CareInterventionActivity 

																	ON CareIntervention.CareInterventionId = CareInterventionActivity.CareInterventionId

														WHERE 
									
															CareIntervention.CareInterventionId = @careInterventionId


										END 

										INSERT INTO MemberCaseCarePlanGoalIntervention 
								
											SELECT

													-- IDENTITY

													@memberCaseCarePlanGoalId,

													@memberCaseCareInterventionId,

													1, -- REQUIRED

													0, -- IS SINGLE INSTANCE
											
													@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
													@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

										FETCH NEXT FROM defaultCarePlanInterventionCursor INTO @carePlanInterventionId, @careInterventionId

									END

								CLOSE defaultCarePlanInterventionCursor

								DEALLOCATE defaultCarePlanInterventionCursor
						
						
								FETCH NEXT FROM defaultCarePlanGoalCursor INTO @carePlanGoalId

							END 

						CLOSE defaultCarePlanGoalCursor

						DEALLOCATE defaultCarePlanGoalCursor
												
				END	     
				
				
				IF (@memberCaseCarePlanId IS NULL) 
				
				  BEGIN
  
						ROLLBACK TRANSACTION 
						
						RETURN -1
						
					END 
  
  
				INSERT INTO MemberCaseProblemCarePlan
				
					SELECT
					
						-- IDENTITY
							
						@memberCaseProblemClassId,
						
						@problemStatementId,
						
						@memberCaseCarePlanId,
						
						@isSingleInstance,
				
						@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
				    
						@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate
        
  
  
			COMMIT TRANSACTION                             
			
			RETURN 0 
			
      /* STORED PROCEDURE ( END ) */
      
    END 
GO

/*
GRANT EXECUTE ON dbo.MemberCase_Update TO PUBLIC
GO          
*/