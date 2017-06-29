/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanGoal_Delete' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanGoal_Delete
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanGoal_Delete
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanGoalId BIGINT,
      
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

				DECLARE @memberCaseCarePlanId AS BIGINT
        
				DECLARE @carePlanGoalCount AS INT

				DECLARE @validationError AS INT
				
				SET @validationError = 1 -- NOT FOUND 
				
        /* LOCAL VARIABLES ( END ) */
        
        
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION 
        
				SELECT 
				
						@memberCaseId = MemberCase.MemberCaseId, 

						@memberCaseCarePlanId = MemberCaseCarePlan.MemberCaseCarePlanId,
						
						@validationError = 
        
						CASE 
						
								WHEN (MemberCase.ModifiedDate <> @lastModifiedDate) THEN 3		-- RECORD MODIFIED 
								
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
				
							JOIN MemberCaseCarePlanGoal
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseCarePlanGoal.MemberCaseCarePlanId
				
					WHERE MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
						

					-- VALIDATE THAT THERE WILL BE AT LEAST ONE GOAL LEFT AFTER DELETING

					SELECT @carePlanGoalCount = COUNT (1) FROM MemberCaseCarePlanGoal WHERE MemberCaseCarePlanId = @memberCaseCarePlanId

					IF (@carePlanGoalCount <= 1) 

						BEGIN 

							ROLLBACK TRANSACTION 

							RETURN 9

						END

						
					IF (@memberCaseId IS NOT NULL) 
					
						BEGIN

							-- IDENTIFY GOAL <-> INTERVENTION LINKS AND DELETE THE LINK

							DELETE FROM MemberCaseCarePlanGoalIntervention WHERE MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
	          
							DELETE FROM MemberCaseCarePlanGoal WHERE MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId

							-- DELETE CARE INTERVENTION THREHOLDS NO LONGER IN USE
							
							DELETE FROM MemberCaseCareInterventionActivityThreshold 

								WHERE MemberCaseCareInterventionActivityThreshold.MemberCaseCareInterventionActivityId 
	
									IN (

										SELECT MemberCaseCareInterventionActivity.MemberCaseCareInterventionActivityId

											FROM MemberCaseCareInterventionActivity
				
											WHERE MemberCaseCareInterventionActivity.MemberCaseCareInterventionId
				
												IN (
					
													SELECT MemberCaseCareIntervention.MemberCaseCareInterventionId

														FROM 
							
															MemberCaseCareIntervention 
							
																LEFT JOIN MemberCaseCarePlanGoalIntervention
									
																	ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId 
										
														WHERE MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL
							
												)
					
									)

							-- DELETE CARE INTERVENTION ACTIVITIES NO LONGER IN USE

							DELETE FROM MemberCaseCareInterventionActivity 

								WHERE MemberCaseCareInterventionActivity.MemberCaseCareInterventionId 

									IN (
		
										SELECT MemberCaseCareIntervention.MemberCaseCareInterventionId

											FROM 
				
												MemberCaseCareIntervention 
				
													LEFT JOIN MemberCaseCarePlanGoalIntervention
						
														ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId 
							
											WHERE MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL
				
									)

							-- DELETE CARE INTERVENTIONS NO LONGER IN USE

							DELETE FROM MemberCaseCareIntervention 

								FROM 
				
									MemberCaseCareIntervention 
				
										LEFT JOIN MemberCaseCarePlanGoalIntervention
						
											ON MemberCaseCareIntervention.MemberCaseCareInterventionId = MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId 
							
								WHERE MemberCaseCarePlanGoalIntervention.MemberCaseCareInterventionId IS NULL

					
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
GRANT EXECUTE ON dbo.MemberCaseCarePlanGoal_Delete TO PUBLIC
GO          
*/