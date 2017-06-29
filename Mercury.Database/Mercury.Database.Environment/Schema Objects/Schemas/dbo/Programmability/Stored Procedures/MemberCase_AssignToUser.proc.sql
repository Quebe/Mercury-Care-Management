/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_AssignToUser' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_AssignToUser
GO      
*/

CREATE PROCEDURE dbo.MemberCase_AssignToUser
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,

      @assignedToSecurityAuthorityId         BIGINT,
      @assignedToUserAccountId      VARCHAR (060),
      @assignedToUserAccountName    VARCHAR (060),
      @assignedToUserDisplayName			 VARCHAR (060),

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
				
				SET @validationError = 0 -- SUCCESS
				
        /* LOCAL VARIABLES ( END ) */
                
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION
        
        
        SET @validationError = 1 -- NOT FOUND
        
        SELECT @validationError = 
        
						CASE 
						
								WHEN (MemberCase.Status NOT IN (1, 2)) THEN 7 -- NOT A VALID CASE STATUS
						
								WHEN (MemberCase.ModifiedDate <> @lastModifiedDate) THEN 3 -- MODIFIED RECORD
								
								WHEN ((LockedByDate IS NOT NULL) AND ((LockedBySecurityAuthorityId <> @modifiedAuthorityId) OR (LockedByUserAccountId <> @modifiedAccountId))) THEN 4 -- LOCKED
								
								WHEN ((AssignedToSecurityAuthorityId = @assignedToSecurityAuthorityId) AND (AssignedToUserAccountId = @assignedToUserAccountId)) THEN 6 -- NO CHANGE DETECTED
								
						
								ELSE 0 -- VALID RECORD FOUND
								
							END
        
					FROM 
					
						dbo.MemberCase
						
					WHERE (MemberCase.MemberCaseId = @memberCaseId)
					
																	
				-- DETERMINE IF ERROR DETECTED AND ROLLBACK TRANSACTION 						
						
				IF (@validationError <> 0) 
				
					BEGIN 
					
						ROLLBACK TRANSACTION 
						
						RETURN @validationError
						
				 END 
				 
				 
        -- IF WE REACH THIS POINT, THE RECORD IS UNLOCKED OR LOCKED BY REQUESTING USER AND IN AN APPROPRIATE STATUS
        
        -- TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE
        
        -- 1. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS THE ASSIGN TO USER (SELF-ASSIGN)
        
        -- 2. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
        
        -- 3. CASE IS ASSIGNED TO A USER, REQUESTING USER IS THE CURRENT ASSIGNED TO USER AND THE NEW ASSIGNED TO IS UNASSIGN
        
        -- 4. CASE IS ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
        
        -- TODO: CHECK FOR CARE PLAN ASSIGNMENTS, AS THIS WILL REPLACE THEM
                        
        IF NOT EXISTS (SELECT MemberCase.MemberCaseId
        
						FROM 
						
							dbo.MemberCase
								
								LEFT JOIN dbo.WorkTeamMembership AS CurrentWorkTeamMembership
								
									ON MemberCase.AssignedToWorkTeamId = CurrentWorkTeamMembership.WorkTeamId
									
										AND ((CurrentWorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (CurrentWorkTeamMembership.UserAccountId = @modifiedAccountId))
									
										AND (CurrentWorkTeamMembership.WorkTeamRole = 1) -- MANAGER
										
								LEFT JOIN dbo.WorkTeamMembership AS AssignedToWorkTeamMembership
								
										ON MemberCase.AssignedToWorkTeamId = AssignedToWorkTeamMembership.WorkTeamId
										
										AND ((AssignedToWorkTeamMembership.SecurityAuthorityId = @assignedToSecurityAuthorityId) AND (AssignedToWorkTeamMembership.UserAccountId = @assignedToUserAccountId))
						
						WHERE (MemberCaseId = @memberCaseId)
						
							AND ((AssignedToWorkTeamMembership.WorkTeamId IS NOT NULL) OR (@assignedToUserAccountId = '')) -- ASSIGN TO USER IS PART OF WORK TEAM (OR UNASSIGN)
						
							AND (
							
								-- 1. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS THE ASSIGN TO USER (SELF-ASSIGN)
								
								((MemberCase.AssignedToUserAccountId = '') AND ((@modifiedAuthorityId = @assignedToSecurityAuthorityId) AND (@modifiedAccountId = @assignedToUserAccountId)))
								
								-- 2. CASE IS NOT CURRENTLY ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
								
								OR ((MemberCase.AssignedToUserAccountId = '') AND (CurrentWorkTeamMembership.WorkTeamId IS NOT NULL))
								
								-- 3. CASE IS ASSIGNED TO A USER, REQUESTING USER IS THE CURRENT ASSIGNED TO USER AND THE NEW ASSIGNED TO IS UNASSIGN
								
								OR ((MemberCase.AssignedToUserAccountId <> '') AND ((@modifiedAuthorityId = MemberCase.AssignedToSecurityAuthorityId) AND (@modifiedAccountId = MemberCase.AssignedToUserAccountId))
								
									AND (@assignedToUserAccountId = ''))
								
								-- 4. CASE IS ASSIGNED TO A USER, REQUESTING USER IS A MANAGER OF THE ASSIGNED TO TEAM
								
								OR (CurrentWorkTeamMembership.WorkTeamId IS NOT NULL)
							
							) -- AND CRITERIA
							
						) -- IF NOT EXISTS 

          BEGIN  
          
            ROLLBACK TRANSACTION 
            
            RETURN 2 -- PERMISSION DENIED
            
          END 
         
        -- NO VALIDATION ERROR
         
        -- EXISTING RECORD, UPDATE
        
        SET @modifiedDate = GETDATE ()
        
        UPDATE dbo.MemberCase
        
          SET
						
            AssignedToSecurityAuthorityId = @assignedToSecurityAuthorityId,
            
            AssignedToUserAccountId = @assignedToUserAccountId,
            
            AssignedToUserAccountName = @assignedToUserAccountName,
            
            AssignedToUserDisplayName = @assignedToUserDisplayName,
            
						AssignedToDate = CASE WHEN (@assignedToUserAccountId = '') THEN NULL ELSE @modifiedDate END,
            
            
            ModifiedAuthorityName = @modifiedAuthorityName,
            ModifiedAccountId     = @modifiedAccountId,
            ModifiedAccountName   = @modifiedAccountName,
            ModifiedDate          = @modifiedDate
            
          WHERE 
          
            MemberCaseId = @memberCaseId
  
			COMMIT TRANSACTION                             
			
			RETURN 0
			
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.MemberCase_AssignToUser TO PUBLIC
GO          
*/ 