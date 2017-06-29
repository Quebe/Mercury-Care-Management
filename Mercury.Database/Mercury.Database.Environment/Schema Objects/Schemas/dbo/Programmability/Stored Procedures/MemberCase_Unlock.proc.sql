/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_Unlock' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_Unlock
GO      
*/

CREATE PROCEDURE dbo.MemberCase_Unlock
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,

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
				
        /* LOCAL VARIABLES ( END ) */
                
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION
        
        
        IF EXISTS (SELECT * FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) AND (MemberCase.ModifiedDate <> @lastModifiedDate))
        
          BEGIN 
          
						ROLLBACK TRANSACTION 
						
						RETURN 3 -- MODIFIED RECORD
						
					END 
                  
        IF EXISTS (SELECT * FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) AND (LockedByDate IS NULL))
        
          BEGIN  -- ALREADY UNLOCKED 
          
            ROLLBACK TRANSACTION 
            
            RETURN 2 -- PERMISSION DENIED
            
          END 
          
        -- IF WE REACH THIS POINT, THE RECORD IS AVAILABLE FOR UPDATE AND LOCKED 
        
        -- PEOPLE THAT CAN LOCK IT ARE: (1) ORIGINAL PERSON THAT LOCKED IT
        
        -- (2) CASE ASSIGNED TO TEAM -> MANAGER OF THE TEAM;
        
				SELECT @validationError = 
        
						CASE 
						
							  WHEN ((MemberCase.LockedBySecurityAuthorityId = @modifiedAuthorityId) 
							  
										OR (MemberCase.LockedByUserAccountId = @modifiedAccountId)) THEN 0 -- LOCKED BY REQUESTING USER
									
								WHEN (WorkTeamMembership.WorkTeamId IS NULL) THEN 0 -- UNLOCK REQUEST BY TEAM MANAGER
								
								ELSE 2 -- PERMISSION DENIED
									
							END 
							
					FROM 
					
						dbo.MemberCase
						
							LEFT JOIN dbo.WorkTeam 
							
								ON MemberCase.AssignedToWorkTeamId = WorkTeam.WorkTeamId

							LEFT JOIN dbo.WorkTeamMembership 
							
								ON WorkTeam.WorkTeamId = WorkTeamMembership.WorkTeamId
								
									AND ((WorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (WorkTeamMembership.UserAccountId = @modifiedAccountId))
								
									AND (WorkTeamMembership.WorkTeamRole = 1) -- MANAGER
								
					WHERE MemberCaseId = @memberCaseId
						
						
				-- DETERMINE IF ERROR DETECTED AND ROLLBACK TRANSACTION 						
						
				IF (@validationError <> 0) 
				
					BEGIN 
					
						ROLLBACK TRANSACTION 
						
						RETURN @validationError
						
				 END 
    
        -- EXISTING RECORD, UPDATE
        
        SET @modifiedDate = GETDATE ()
        
        UPDATE dbo.MemberCase
          SET
            
            LockedBySecurityAuthorityId = NULL,
            LockedByUserAccountId = '', 
            LockedByUserAccountName = '', 
            LockedByUserDisplayName = '', 
            LockedByDate = NULL,
            
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
GRANT EXECUTE ON dbo.MemberCase_Unlock TO PUBLIC
GO          
*/