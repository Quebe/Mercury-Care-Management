/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanIntervention_Delete' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanIntervention_Delete
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanIntervention_Delete
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanInterventionId BIGINT,
      
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
        
				DECLARE @carePlanInterventionCount AS INT

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
				
							JOIN MemberCaseCarePlanIntervention
			
								ON MemberCaseCarePlan.MemberCaseCarePlanId = MemberCaseCarePlanIntervention.MemberCaseCarePlanId
				
					WHERE MemberCaseCarePlanInterventionId = @memberCaseCarePlanInterventionId
						

					-- VALIDATE THAT THERE WILL BE AT LEAST ONE GOAL LEFT AFTER DELETING

					SELECT @carePlanInterventionCount = COUNT (1) FROM MemberCaseCarePlanIntervention WHERE MemberCaseCarePlanId = @memberCaseCarePlanId

					IF (@carePlanInterventionCount <= 1) 

						BEGIN 

							ROLLBACK TRANSACTION 

							RETURN 9

						END

						
					IF (@memberCaseId IS NOT NULL) 
					
						BEGIN

							DELETE FROM MemberCaseCarePlanInterventionActivityThreshold

								FROM 

									MemberCaseCarePlanIntervention

										JOIN MemberCaseCarePlanInterventionActivity

											ON MemberCaseCarePlanIntervention.MemberCaseCarePlanInterventionId = MemberCaseCarePlanInterventionActivity.MemberCaseCarePlanInterventionId

										JOIN MemberCaseCarePlanInterventionActivityThreshold

											ON MemberCaseCarePlanInterventionActivity.MemberCaseCarePlanInterventionActivityId = MemberCaseCarePlanInterventionActivityThreshold.MemberCaseCarePlanInterventionActivityId

								WHERE MemberCaseCarePlanIntervention.MemberCaseCarePlanInterventionId = @memberCaseCarePlanInterventionId
									
	          
							DELETE FROM MemberCaseCarePlanInterventionActivity

								FROM 

									MemberCaseCarePlanIntervention

										JOIN MemberCaseCarePlanInterventionActivity

											ON MemberCaseCarePlanIntervention.MemberCaseCarePlanInterventionId = MemberCaseCarePlanInterventionActivity.MemberCaseCarePlanInterventionId

								WHERE MemberCaseCarePlanIntervention.MemberCaseCarePlanInterventionId = @memberCaseCarePlanInterventionId	


							DELETE FROM MemberCaseCarePlanIntervention 
							
								WHERE MemberCaseCarePlanInterventionId = @memberCaseCarePlanInterventionId
	           
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
GRANT EXECUTE ON dbo.MemberCaseCarePlanIntervention_Delete TO PUBLIC
GO          
*/