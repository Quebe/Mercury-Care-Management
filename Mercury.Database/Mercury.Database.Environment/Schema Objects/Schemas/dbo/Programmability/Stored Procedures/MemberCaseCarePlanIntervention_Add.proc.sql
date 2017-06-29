/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanIntervention_Add' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanIntervention_Add
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanIntervention_Add
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			@memberCaseCarePlanId BIGINT, -- MEMBER CASE CARE PLAN TO ADD NEW GOAL TO

      @copyCareInterventionId BIGINT,		-- CARE PLAN GOAL TO COPY, IF 0, ADD NEW

			@carePlanInterventionName VARCHAR (060),	-- IF ADDING NEW, NEW GOAL NAME
      
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

				DECLARE @memberCaseCarePlanInterventionId AS BIGINT
       
				DECLARE @validationError AS INT
				
				SET @validationError = 1 -- NOT FOUND 
				
        /* LOCAL VARIABLES ( END ) */
        
        
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION 
        
				SELECT 
				
						@memberCaseId = MemberCase.MemberCaseId, 
												
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
							
					WHERE MemberCaseCarePlan.MemberCaseCarePlanId = @memberCaseCarePlanId
						
												
					IF (@memberCaseId IS NOT NULL) 
					
						BEGIN

							SET @modifiedDate = GETDATE ()
	          
							IF (@copyCareInterventionId <> 0) 

								BEGIN

									INSERT INTO MemberCaseCarePlanIntervention (

											MemberCaseCarePlanInterventionName, MemberCaseCareplanInterventionDescription, MemberCaseCarePlanId, CareInterventionId, 

											Inclusion, Status, 

											ExtendedProperties, Enabled, Visible,

											CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,

											ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate

										)

										SELECT 
										
												CareIntervention.CareInterventionName,

												CareIntervention.CareInterventionDescription, 

												@memberCaseCarePlanId,

												CareIntervention.CareInterventionId,


												3, -- OPTIONAL

												1, -- UNDER DEVELOPMENT
												 

												CareIntervention.ExtendedProperties,

												CareIntervention.Enabled,

												CareIntervention.Visible,


												@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,

												@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

											FROM CareIntervention

											WHERE CareIntervention.CareInterventionId = @copyCareInterventionId

									SET @memberCaseCarePlanInterventionId = @@IDENTITY

									-- ADD RELATED ACTIVITIES FOR COMMON INTERVENTION

									INSERT INTO MemberCaseCarePlanInterventionActivity (

											MemberCaseCarePlanInterventionActivityName, MemberCaseCarePlanInterventionActivityDescription, MemberCaseCarePlanInterventionId,

											ClinicalNarrative, CommonNarrative, CareInterventionActivityType, ActivityType, IsReoccurring, 

											InitialAnchorDate, AnchorDate, ScheduleType, ScheduleValue, ScheduleQualifier, ConstraintValue, ConstraintQualifier, PerformActionDate, 

											ActionId, ActionParameters, ActionDescription, 

											CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,

											ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate

											)

										SELECT 

												CareInterventionActivity.CareInterventionActivityName, CareInterventionActivity.CareInterventionActivityDescription, @memberCaseCarePlanInterventionId,

												CareInterventionActivity.ClinicalNarrative, CareInterventionActivity.CommonNarrative, CareInterventionActivity.CareInterventionActivityType, CareInterventionActivity.ActivityType, CareInterventionActivity.IsReoccurring,

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
											
												CareIntervention

													JOIN CareInterventionActivity 

														ON CareIntervention.CareInterventionId = CareInterventionActivity.CareInterventionId

											WHERE 
											
												CareIntervention.CareInterventionId = @copyCareInterventionId

								END

							ELSE

								BEGIN
								
									INSERT INTO MemberCaseCarePlanIntervention (
									
											MemberCaseCarePlanInterventionName, MemberCaseCareplanInterventionDescription, MemberCaseCarePlanId, CareInterventionId, 

											Inclusion, Status, 

											ExtendedProperties, Enabled, Visible,

											CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,

											ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate

										)

										SELECT 

												@carePlanInterventionName, @carePlanInterventionName, @memberCaseCarePlanId, NULL, 
												
												3, -- OPTIONAL

												1, -- UNDER DEVELOPMENT
												 
												NULL, 1, 1,

												@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate,

												@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, @modifiedDate

									SET @memberCaseCarePlanInterventionId = @@IDENTITY

									-- NO ACTIVITIES ADDED BECAUSE IT IS A NEW INSERT

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
GRANT EXECUTE ON dbo.MemberCaseCarePlanIntervention_Add TO PUBLIC
GO          
*/