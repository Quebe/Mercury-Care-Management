/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_AssignToWorkTeam' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_AssignToWorkTeam
GO      
*/

CREATE PROCEDURE dbo.MemberCase_AssignToWorkTeam
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,

			@assignedToWorkTeamId     BIGINT,

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
								
								WHEN ((AssignedToWorkTeamId = @assignedToWorkTeamId) OR ((AssignedToWorkTeamId IS NULL) AND (@assignedToWorkTeamId IS NULL))) THEN 6 -- NO CHANGE DETECTED
								
						
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
				 
				 
				-- BELOW ARE THE OLD BUSINESS RULES NOW REMOVED IN FAVOR OF THE ABOVE CASE STATEMENT
        
        --IF EXISTS (SELECT MemberCase.MemberCaseId FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) AND )
        
          --BEGIN 
          
						--ROLLBACK TRANSACTION 
						
						--RETURN 3 -- MODIFIED RECORD
						
					--END 
                  
				---- CHECK TO SEE IF THE RECORD IS LOCKED, CAN ONLY MODIFY IF OWNER OF LOCK OR RECORD IS NOT LOCKED                  
                  
        --IF EXISTS (SELECT MemberCase.MemberCaseId FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) 
        
						--AND ((LockedByDate IS NOT NULL) AND ((LockedBySecurityAuthorityId <> @modifiedAuthorityId) OR (LockedByUserAccountId <> @modifiedAccountId)))
						
						--) -- IF EXISTS 
        
          --BEGIN  -- LOCKED
          
            --ROLLBACK TRANSACTION 
            
            --RETURN 4 -- LOCKED RECORD
            
          --END 
          
        -- CHECK TO SEE IF THERE IS ACTUALLY A CHANGE REQUEST
        
        --IF EXISTS (SELECT MemberCase.MemberCaseId FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) 
        
						--AND ((AssignedToWorkTeamId = @assignedToWorkTeamId) OR ((AssignedToWorkTeamId IS NULL) AND (@assignedToWorkTeamId IS NULL)))
						
						--) -- EXISTS 
						
          --BEGIN  
          
            --ROLLBACK TRANSACTION 
            
            --RETURN 6 -- NO CHANGE REQUEST DETECTED
            
          --END 
						
          
        -- IF WE REACH THIS POINT, THE RECORD IS UNLOCKED OR LOCKED BY REQUESTING USER, 
        
        -- TO ASSIGN, ONE OF THE FOLLOWING MUST BE TRUE
        
        -- 1. CASE IS NOT CURRENTLY ASSIGNED TO CASE AND REQUESTING USER IS MEMBER OF DESTINATION TEAM (ANY ROLE)
        
        -- 2. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO REMOVE ASSIGNMENT FROM TEAM, USER IS MANAGER OF TEAM
        
        -- 3. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO MOVE TO ANOTHER TEAM, USER IS MANAGER OF BOTH TEAMS
        
        -- TODO: CHECK FOR CARE PLAN ASSIGNMENTS, AS THIS WILL REPLACE THEM
                        
        IF NOT EXISTS (SELECT MemberCase.MemberCaseId
        
						FROM 
						
							dbo.MemberCase
								
								LEFT JOIN dbo.WorkTeamMembership AS CurrentWorkTeamMembership
								
									ON MemberCase.AssignedToWorkTeamId = CurrentWorkTeamMembership.WorkTeamId
									
										AND ((CurrentWorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (CurrentWorkTeamMembership.UserAccountId = @modifiedAccountId))
									
										AND (CurrentWorkTeamMembership.WorkTeamRole = 1) -- MANAGER
										
								LEFT JOIN dbo.WorkTeamMembership AS DestinationWorkTeamMembership										
								
										ON DestinationWorkTeamMembership.WorkTeamId = @assignedToWorkTeamId -- DESTINATION WORK TEAM
								
										AND ((DestinationWorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (DestinationWorkTeamMembership.UserAccountId = @modifiedAccountId))
						
						WHERE (MemberCaseId = @memberCaseId)
						
							AND (
							
								-- WORK TEAM IS UNASSIGNED, AND REQUESTING USER IS MEMBER OF DESTINATION TEAM (ANY ROLE)
								
								((MemberCase.AssignedToWorkTeamDate IS NULL) AND (DestinationWorkTeamMembership.WorkTeamId IS NOT NULL))
								
								-- CASE IS CURRENTLY ASSIGNED, REQUEST IS TO REMOVE ASSIGNMENT FROM TEAM, USER IS MANAGER OF TEAM
								
								OR ((MemberCase.AssignedToWorkTeamDate IS NOT NULL) AND (@assignedToWorkTeamId IS NULL) AND (CurrentWorkTeamMembership.WorkTeamId IS NOT NULL))
								
				        -- 3. CASE IS CURRENTLY ASSIGNED, REQUEST IS TO MOVE TO ANOTHER TEAM, USER IS MANAGER OF BOTH TEAMS

								OR ((MemberCase.AssignedToWorkTeamDate IS NOT NULL) AND (CurrentWorkTeamMembership.WorkTeamId IS NOT NULL) AND (DestinationWorkTeamMembership.WorkTeamRole = 1))
								
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
            
						AssignedToWorkTeamId = @assignedToWorkTeamId,
						
						AssignedtoWorkTeamDate = CASE WHEN (@assignedToWorkTeamId IS NULL) THEN NULL ELSE @modifiedDate END,

						-- CLEAR USER ASSIGNMENT (USER MIGHT NOT BE IN NEW TEAM)
						
						AssignedToSecurityAuthorityId = 0,
						
						AssignedToUserAccountId = '',
						
						AssignedToUserAccountName = '',
						
						AssignedToUserDisplayName = '',
						
						AssignedToDate = NULL,
            
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
GRANT EXECUTE ON dbo.MemberCase_AssignToWorkTeam TO PUBLIC
GO          
*/