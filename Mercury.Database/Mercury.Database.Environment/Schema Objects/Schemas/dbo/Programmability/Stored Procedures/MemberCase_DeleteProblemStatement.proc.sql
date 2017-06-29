/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_DeleteProblemStatement' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_DeleteProblemStatement
GO      
*/

CREATE PROCEDURE dbo.MemberCase_DeleteProblemStatement
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId                BIGINT,
      @memberCaseProblemCarePlanId BIGINT,
      
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
								
								
				DECLARE @memberCaseCarePlanId AS BIGINT

				DECLARE @memberCaseProblemCarePlanCount AS INT

				DECLARE @memberCaseCarePlanStatus AS INT

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
						
						

				-- A PROBLEM CARE PLAN CAN BE REMOVED IF

				--   1. THE MEMBER CASE CARE PLAN STATUS IS NOT ACTIVE AND NOT CLOSED (NOT SPECIFIED, UNDER DEVELOPMENT, VOID)

				--   2. OR THERE IS ANOTHER MEMBER CASE PROBLEM CARE PLAN USING THE SAME CARE PLAN


				-- GET COUNT OF PROBLEMS USING THE CARE PLAN ASSOCIATED WITH THE PROBLEM CARE PLAN TABLE

				SELECT @memberCaseProblemCarePlanCount = CountOf 

					FROM (

						SELECT 

								MemberCaseProblemClass.MemberCaseProblemClassId, 
				
								MemberCaseProblemCarePlan.MemberCaseProblemCarePlanId, 
				
								COUNT (OtherMemberCaseProblemCarePlan.MemberCaseProblemCarePlanId) AS CountOf

							FROM 
			
								MemberCaseProblemClass -- START FROM PROBLEM CLASS TO ENSURE THAT ALL CARE PLANS FROM THE CASE ARE LOOKED AT
			
									JOIN MemberCaseProblemCarePlan 
					
										ON MemberCaseProblemClass.MemberCaseProblemClassId = MemberCaseProblemCarePlan.MemberCaseProblemClassId

									LEFT JOIN MemberCaseProblemClass AS OtherMemberCaseProblemClass 
					
										ON MemberCaseProblemClass.MemberCaseId = OtherMemberCaseProblemClass.MemberCaseId
						
										AND MemberCaseProblemClass.MemberCaseProblemClassId <> OtherMemberCaseProblemClass.MemberCaseProblemClassId
				
									LEFT JOIN MemberCaseProblemCarePlan AS OtherMemberCaseProblemCarePlan
					
										ON OtherMemberCaseProblemClass.MemberCaseProblemClassId = OtherMemberCaseProblemCarePlan.MemberCaseProblemClassId
						
										AND MemberCaseProblemCarePlan.MemberCaseCarePlanId = OtherMemberCaseProblemCarePlan.MemberCaseCarePlanId -- SAME CARE PLAN REFERENCE
					
										AND MemberCaseProblemCarePlan.MemberCaseProblemCarePlanId <> OtherMemberCaseProblemCarePlan.MemberCaseProblemCarePlanId
										
				
							WHERE 
			
								MemberCaseProblemCarePlan.MemberCaseProblemCarePlanId = @memberCaseProblemCarePlanId
				
							GROUP BY 
			
								MemberCaseProblemClass.MemberCaseProblemClassId, 
				
								MemberCaseProblemCarePlan.MemberCaseProblemCarePlanId
				
						) AS DetailTable
		
				SELECT @memberCaseCarePlanId = MemberCaseCarePlanId FROM MemberCaseProblemCarePlan WHERE MemberCaseProblemCarePlanId = @memberCaseProblemCarePlanId
		
				SELECT @memberCaseCarePlanStatus = Status FROM MemberCaseCarePlan WHERE MemberCaseCarePlanId = @memberCaseCarePlanId		
		

				-- VALIDATE THAT THIS IS NOT THE LAST PROBLEM STATEMENT ASSIGNED TO AN OPEN/CLOSED CARE PLAN
		
				IF ((@memberCaseProblemCarePlanCount = 0) AND ((@memberCaseCarePlanStatus NOT IN (-1, 0, 1))))

					BEGIN 
	
						SET @validationError = 9
		
					END
		

				-- DETERMINE IF ERROR DETECTED AND ROLLBACK TRANSACTION 						
						
				IF (@validationError <> 0) 
				
					BEGIN 
					
						ROLLBACK TRANSACTION 
						
						RETURN @validationError
						
				 END 
    
				
				-- REMOVE PROBLEM CLASS <--> CARE PLAN LINK

				DELETE FROM MemberCaseProblemCarePlan WHERE MemberCaseProblemCarePlan.MemberCaseProblemCarePlanId = @memberCaseProblemCarePlanId
		
				-- REMOVE ORPHANED PROBLEM CLASSES 

				DELETE FROM MemberCaseProblemClass

					FROM MemberCaseProblemClass 
	
						LEFT JOIN MemberCaseProblemCarePlan 
	
						ON MemberCaseProblemClass.MemberCaseProblemClassId = MemberCaseProblemCarePlan.MemberCaseProblemClassId
		
					WHERE 
	
						MemberCaseProblemClass.MemberCaseId = @memberCaseId
	
						AND MemberCaseProblemCarePlan.MemberCaseProblemClassId IS NULL
		

				-- REMOVE ORPHANED MEMBER CARE PLANS, START FROM GOAL INTERVENTION -> GOAL -> CARE PLAN

				-- THIS ALLOWS FOR CARE PLAN ASSESSMENTS TO BE ORPHANED (INSTEAD OF LOSING THAT DATA)

				-- OR SHOULD THE CARE PLAN AND ALL ITEMS BE 'VOIDED' IF THERE IS AN ASSESSMENT?

				DELETE FROM MemberCaseCarePlanGoalIntervention 

					WHERE MemberCaseCarePlanGoalIntervention.MemberCaseCarePlanGoalId IN (
	
						SELECT MemberCaseCarePlanGoal.MemberCaseCarePlanGoalId
		
							FROM 
			
								MemberCaseCarePlanGoal
				
									JOIN MemberCaseCarePlan 
					
										ON MemberCaseCarePlanGoal.MemberCaseCarePlanId = MemberCaseCarePlan.MemberCaseCarePlanId
						
									LEFT JOIN MemberCaseProblemCarePlan 
					
										ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseProblemCarePlan.MemberCaseCarePlanId
						
							WHERE 
			
								MemberCaseProblemCarePlan.MemberCaseCarePlanId IS NULL
						
					)					
						
				DELETE FROM MemberCaseCarePlanGoal 

					WHERE MemberCaseCarePlanGoal.MemberCaseCarePlanId IN (
	
						SELECT MemberCaseCarePlan.MemberCaseCarePlanId
		
							FROM 
			
								MemberCaseCarePlan 
					
									LEFT JOIN MemberCaseProblemCarePlan 
					
										ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseProblemCarePlan.MemberCaseCarePlanId
						
							WHERE 
			
								MemberCaseProblemCarePlan.MemberCaseCarePlanId IS NULL
						
					)					
	
				DELETE FROM MemberCaseCarePlan 
			
					FROM 
	
						MemberCaseCarePlan 
			
							LEFT JOIN MemberCaseProblemCarePlan 
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseProblemCarePlan.MemberCaseCarePlanId
				
							LEFT JOIN MemberCaseCarePlanAssessment
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseCarePlanAssessment.MemberCaseCarePlanId 
				
					WHERE 
	
						MemberCaseProblemCarePlan.MemberCaseCarePlanId IS NULL
		
							AND MemberCaseCarePlanAssessment.MemberCaseCarePlanId IS NULL
				
				UPDATE MemberCaseCarePlan 

					SET Status = -1
			
					FROM 
	
						MemberCaseCarePlan 
			
							LEFT JOIN MemberCaseProblemCarePlan 
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseProblemCarePlan.MemberCaseCarePlanId
				
							LEFT JOIN MemberCaseCarePlanAssessment
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseCarePlanAssessment.MemberCaseCarePlanId 
				
					WHERE 
	
						MemberCaseProblemCarePlan.MemberCaseCarePlanId IS NULL
		
							AND MemberCaseCarePlanAssessment.MemberCaseCarePlanId IS NOT NULL
				


				-- REMOVE ORPHANED CARE INTERVENTIONS

				DELETE FROM MemberCaseCareInterventionActivityThreshold

					WHERE MemberCaseCareInterventionActivityId IN (

						SELECT MemberCaseCareInterventionActivity.MemberCaseCareInterventionActivityId

							FROM 
			
								MemberCaseCareInterventionActivity
			
									JOIN MemberCaseCareIntervention 
					
										ON MemberCaseCareInterventionActivity.MemberCaseCareInterventionId = MemberCaseCareIntervention.MemberCaseCareInterventionId
				
									LEFT JOIN MemberCaseCarePlanGoalIntervention
					
										ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId
						
							WHERE 
			
								MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL
				
						)
				
				DELETE FROM MemberCaseCareInterventionActivity

					WHERE MemberCaseCareInterventionId IN (

						SELECT MemberCaseCareIntervention.MemberCaseCareInterventionId

							FROM 
			
								MemberCaseCareIntervention 
				
									LEFT JOIN MemberCaseCarePlanGoalIntervention
					
										ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId
						
							WHERE 
			
								MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL
				
					)

				DELETE FROM MemberCaseCareIntervention

					FROM 
	
						MemberCaseCareIntervention 
		
							LEFT JOIN MemberCaseCarePlanGoalIntervention
			
								ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId
				
					WHERE 
	
						MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL
		

    
  
				COMMIT TRANSACTION                             
			
				RETURN 0 
			
      /* STORED PROCEDURE ( END ) */
					      
    END 
GO

/*
GRANT EXECUTE ON dbo.MemberCase_Update TO PUBLIC
GO          
*/