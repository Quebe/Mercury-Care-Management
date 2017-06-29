/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanGoal_Add' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanGoal_Add
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanGoal_Add
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			@memberCaseCarePlanId BIGINT, -- MEMBER CASE CARE PLAN TO ADD NEW GOAL TO

      @copyCarePlanGoalId BIGINT,		-- CARE PLAN GOAL TO COPY, IF 0, ADD NEW

			@carePlanGoalName VARCHAR (060),	-- IF ADDING NEW, NEW GOAL NAME
			@careMeasureId BIGINT,				-- IF ADDING NEW, NEW CARE MEASUREMENT ID
      
      @ignoreAssignedTo       BIGINT,

      @modifiedAuthorityId     BIGINT,
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060),
    
      @lastModifiedDate       DATETIME ,  -- LAST MODIFIED DATE OF THE ORIGINAL RECORD

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */       
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
      
			@modifiedDate DATETIME OUTPUT -- OUTPUT OF THE MODIFIED DATE ON SUCCESS 
      
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

				DECLARE @memberCaseId AS BIGINT

				DECLARE @memberCaseCarePlanGoalId AS BIGINT

				DECLARE @carePlanInterventionId AS BIGINT

				DECLARE @careInterventionId AS BIGINT

				DECLARE @memberCaseCareInterventionId AS BIGINT
       
				DECLARE @validationError AS INT
				
				SET @validationError = 1 -- NOT FOUND 
				
        /* LOCAL VARIABLES ( END ) */
        
        
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION 

				SET @modifiedDate = GETDATE ()
        
				SELECT 
				
						@memberCaseId = MemberCase.MemberCaseId, 
												
						@validationError = 
        
						CASE 
						
								WHEN (MemberCaseCarePlan.ModifiedDate <> @lastModifiedDate) THEN 3		-- RECORD MODIFIED (AT CARE PLAN LEVEL SINCE ADDING NEW)
								
								WHEN ((MemberCase.LockedByDate IS NOT NULL) AND ((MemberCase.LockedBySecurityAuthorityId <> @modifiedAuthorityId) AND (MemberCase.LockedByUserAccountId <> @modifiedAccountId))) THEN 4 -- LOCKED
								
								WHEN ((MemberCase.AssignedToDate IS NOT NULL) AND ((MemberCase.AssignedToSecurityAuthorityId <> @modifiedAuthorityId) AND (MemberCase.AssignedToUserAccountId <> @modifiedAccountId)) 
								
									AND (@ignoreAssignedTo = 0)) THEN 5 -- ASSIGNED TO
									
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
								
							JOIN MemberCaseCarePlan 
			
								ON MemberCase.MemberCaseId = MemberCaseCarePlan.MemberCaseId
							
					WHERE MemberCaseCarePlan.MemberCaseCarePlanId = @memberCaseCarePlanId
						

				-- COPY THE NAME OVER FOR COMPARISONS IF USING COPY

				IF (@copyCarePlanGoalId <> 0) 

					BEGIN

						SELECT @carePlanGoalName = CarePlanGoalName FROM CarePlanGoal WHERE CarePlanGoalId = @copyCarePlanGoalId

					END
						
				-- LOOK FOR DUPLICATES

				IF (EXISTS (

						SELECT MemberCaseCarePlanGoalId
					
							FROM MemberCaseCarePlanGoal
	
							WHERE MemberCaseCarePlanGoal.MemberCaseCarePlanId = @memberCaseCarePlanId
	
								AND MemberCaseCarePlanGoal.MemberCaseCarePlanGoalName = @carePlanGoalName

								AND MemberCaseCarePlanGoal.Status IN (0, 1, 2)

						)) AND (@validationError = 0)

					BEGIN 

						SET @validationError = 11

					END


				IF (@validationError <> 0) 

					BEGIN 

						ROLLBACK TRANSACTION 

						RETURN @validationError

					END
												
					IF (@memberCaseId IS NOT NULL) 
					
						BEGIN
	          
							IF (@copyCarePlanGoalId <> 0) 

								BEGIN

									INSERT INTO MemberCaseCarePlanGoal (

											MemberCaseCarePlanGoalName, MemberCaseCareplanGoalDescription, MemberCaseCarePlanId,

											CarePlanGoalId, Status, ClinicalNarrative, CommonNarrative, 

											GoalTimeframe, ScheduleValue, ScheduleQualifier, CareMeasureId, 

											ExtendedProperties, Enabled, Visible

										)

										SELECT 

												CarePlanGoal.CarePlanGoalName,

												CarePlanGoal.CareplanGoalDescription, 

												@memberCaseCarePlanId,

												CarePlanGoal.CarePlanGoalId,

												1, -- STATUS (UNDER DEVELOPMENT)

												CarePlanGoal.ClinicalNarrative,

												CarePlanGoal.CommonNarrative, 

												CarePlanGoal.GoalTimeframe,

												CarePlanGoal.ScheduleValue,

												CarePlanGoal.ScheduleQualifier,

												CarePlanGoal.CareMeasureId,

												CarePlanGoal.ExtendedProperties,

												CarePlanGoal.Enabled,

												CarePlanGoal.Visible

											FROM CarePlanGoal

											WHERE CarePlanGoal.CarePlanGoalId = @copyCarePlanGoalId

									SET @memberCaseCarePlanGoalId = @@IDENTITY

											
									-- FOR EACH GOAL INSERT INTERVENTIONS (IF NOT ALREADY EXISTING, OTHERWISE JUST CREATE LINK)

									DECLARE defaultCarePlanInterventionCursor CURSOR FOR

										SELECT CarePlanInterventionId, CareInterventionId FROM CarePlanIntervention WHERE CarePlanGoalId = @copyCarePlanGoalId AND Enabled = 1 AND Inclusion = 1

									OPEN defaultCarePlanInterventionCursor 

									FETCH NEXT FROM defaultCarePlanInterventionCursor INTO @carePlanInterventionId, @careInterventionId

									WHILE (@@FETCH_STATUS <> -1) 

										BEGIN 
							
											-- DETERMINE IF THE CARE INTERVENTION IS ALREADY AVAILABLE UNDER THE MEMBER CASE

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
								
																CarePlanGoalId = @copyCarePlanGoalId

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

											INSERT INTO MemberCaseCarePlanGoalIntervention (MemberCaseCarePlanGoalId, MemberCaseCareInterventionId, Inclusion, IsSingleInstance,

													CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, 

													ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
								
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

								END

							ELSE

								BEGIN
								
									INSERT INTO MemberCaseCarePlanGoal (

											MemberCaseCarePlanGoalName, MemberCaseCareplanGoalDescription, MemberCaseCarePlanId,

											CarePlanGoalId, ClinicalNarrative, CommonNarrative, 

											GoalTimeframe, ScheduleValue, ScheduleQualifier, CareMeasureId, 

											ExtendedProperties, Enabled, Visible,
											
											CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, 

											ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)

										SELECT 

											@carePlanGoalName, @carePlanGoalName, @memberCaseCarePlanId, 

											NULL, @carePlanGoalName, @carePlanGoalName, 

											0, 0, 0, @careMeasureId,

											NULL, 1, 1,
											
											@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
											@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

								END
	           
						END    
						
					ELSE 
					
						BEGIN 
						
							ROLLBACK TRANSACTION 
							
							RETURN 1
							
					  END
	          
          
			COMMIT TRANSACTION                             
			
			RETURN 0
    
      /* STORED PROCEDURE ( END ) */
      
    END 
GO

/*
GRANT EXECUTE ON dbo.MemberCaseCarePlanGoal_Add TO PUBLIC
GO          
*/