/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanGoal_AddCareIntervention' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanGoal_AddCareIntervention
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanGoal_AddCareIntervention
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,
			@memberCaseCarePlanGoalId   BIGINT,
      @careInterventionId         BIGINT,
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
                
				-- TODO: VALIDATE SINGLE VERSUS SHARED

				SELECT @validationError = 10

					FROM 
	
						MemberCaseCarePlanGoalIntervention
		
							JOIN MemberCaseCareIntervention
			
								ON MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId = MemberCaseCareIntervention.MemberCaseCareInterventionId
				
								AND MemberCaseCareIntervention.CareInterventionId = @careInterventionId
		
					WHERE 
	
						MemberCaseCarePlanGoalIntervention.MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId -- PARENT GOAL

						AND MemberCaseCareIntervention.Status IN (1, 2) -- UNDER DEVELOPMENT OR ACTIVE 
	
						AND (
		
							(MemberCaseCarePlanGoalIntervention.IsSingleInstance = @isSingleInstance) -- THE SHARED/SINGLE INSTANCE MATCHES THE REQUEST
			
							OR ( -- OR THE CARE INTERVENTION MATCHES AND THE REQUEST TO MOVE FROM SINGLE TO SHARED, WHICH IS NOT POSSIBLE
			
									(MemberCaseCarePlanGoalIntervention.IsSingleInstance = 1) 
					
									AND (@isSingleInstance = 0) -- @isSingleInstance = 0
					
							)
					
						)
		

				-- SET THE MODIFIED DATE FOR ALL ACTIONS AND RETURN VALUE

				SET @modifiedDate = GETDATE ();
								

				-- DETERMINE IF THE CARE INTERVENTION CAN BE LINKED OR A NEW ONE NEEDS TO BE INSERTED

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
									
									CareIntervention.CareInterventionName,
									
									CareIntervention.CareInterventionDescription,
									
									@memberCaseId,
									
									CareIntervention.CareInterventionId,
									
									1, -- STATUS
									
									CareIntervention.ExtendedProperties,
									
									CareIntervention.Enabled, CareIntervention.Visible,
									
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,
							    
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate
									
								FROM 
								
									dbo.CareIntervention
									
								WHERE 
								
										CareIntervention.CareInterventionId = @careInterventionId
									

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

						-- TODO: ACTIVITY THRESHOLDS

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

						
								
				-- DETERMINE IF ERROR DETECTED AND ROLLBACK TRANSACTION 						
						
				IF (@validationError <> 0) 
				
					BEGIN 
					
						ROLLBACK TRANSACTION 
						
						RETURN @validationError
						
				 END 
    
			
			
  
  
			COMMIT TRANSACTION                             
			
			RETURN 0 
			
      /* STORED PROCEDURE ( END ) */
      
    END 
GO

/*
GRANT EXECUTE ON dbo.MemberCase_Update TO PUBLIC
GO          
*/