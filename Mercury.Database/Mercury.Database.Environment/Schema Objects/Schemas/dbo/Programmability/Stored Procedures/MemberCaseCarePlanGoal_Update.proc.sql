/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanGoal_Update' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanGoal_Update
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanGoal_Update
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			@memberCaseCarePlanGoalId BIGINT,
			@status AS INT,

			-- BASE OBJECT CARE PLAN GOAL PROPERTIES (BEGIN)

			@memberCaseCarePlanGoalName VARCHAR (060),

			@memberCaseCarePlanGoalDescription VARCHAR (999), 

			@clinicalNarrative VARCHAR (999),

			@commonNarrative VARCHAR (999),

			@goalTimeframe INT,

			@scheduleValue INT,

			@scheduleQualifier INT,

			@careMeasureId BIGINT,

			-- BASE OBJECT CARE PLAN GOAL PROPERTIES ( END )
		
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
       
				DECLARE @validationError AS INT
				
				SET @validationError = 1 -- NOT FOUND 
				
        /* LOCAL VARIABLES ( END ) */
        
        
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION 
        

				SELECT 
				
						@memberCaseId = MemberCase.MemberCaseId, 
												
						@validationError = 
        
						CASE 

								-- CHECK FOR MODIFICATION AT THE GOAL LEVEL ITSELF
						
								WHEN (MemberCaseCarePlanGoal.ModifiedDate <> @lastModifiedDate) THEN 3		-- RECORD MODIFIED 
								
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
							
					WHERE MemberCaseCarePlanGoal.MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
						

				IF (@validationError = 0) 

					BEGIN 

						SET @modifiedDate = GETDATE ()

						UPDATE MemberCaseCarePlanGoal

							SET 

								MemberCaseCarePlanGoalName = @memberCaseCarePlanGoalName,

								MemberCaseCareplanGoalDescription = @memberCaseCarePlanGoalDescription,

								[Status] = @status,

								ClinicalNarrative = @clinicalNarrative,

								CommonNarrative = @commonNarrative,

								GoalTimeframe = @goalTimeframe,

								ScheduleValue = @scheduleValue,

								ScheduleQualifier = @scheduleQualifier,

								CareMeasureId = @careMeasureId,

								ModifiedAuthorityName = @modifiedAccountName,

								ModifiedAccountName = @modifiedAccountName,

								ModifiedAccountId = @modifiedAccountId,

								ModifiedDate = @modifiedDate
							
							WHERE 
							
								MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId

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
GRANT EXECUTE ON dbo.MemberCaseCarePlanGoal_Update TO PUBLIC
GO          
*/