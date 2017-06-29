/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_Lock' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_Lock
GO      
*/

CREATE PROCEDURE dbo.MemberCase_Lock
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,

      @lockedBySecurityAuthorityId         BIGINT,
      @lockedByUserAccountId      VARCHAR (060),
      @lockedByUserAccountName    VARCHAR (060),
      @lockedByUserDisplayName			 VARCHAR (060),

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
        
        IF EXISTS (SELECT * FROM dbo.MemberCase WHERE (MemberCaseId = @memberCaseId) AND (LockedByDate IS NOT NULL))
        
          BEGIN  -- ALREADY LOCKED 
          
            ROLLBACK TRANSACTION 
            
            RETURN 2 -- PERMISSION DENIED
            
          END 
          
        -- IF WE REACH THIS POINT, THE RECORD IS AVAILABLE FOR UPDATE AND UNLOCKED 
        
        -- PEOPLE THAT CAN LOCK IT ARE: (1) CASE IS NOT ASSIGNED TO TEAM/USER -> ANYONE; 
        
        -- (2) CASE ASSIGNED TO TEAM -> ANYONE ON TEAM;
        
        -- (3) CASE ASSIGNED TO USER -> USER ONLY
        
				SELECT @validationError = 
        
						CASE 
						
								-- CHECK FOR ASSIGNED TO USER LEVEL VALIDATION ERROR 
								
								WHEN (((MemberCase.AssignedToDate IS NOT NULL) AND (MemberCase.AssignedToSecurityAuthorityId <> 0))
								
									AND ((MemberCase.AssignedToSecurityAuthorityId <> @modifiedAuthorityId) AND (MemberCase.AssignedToUserAccountId <> @modifiedAccountId)) 
								
									AND (@ignoreAssignedTo = 0)) THEN 5 -- ASSIGNED TO
								
								-- CHECK FOR ASSIGNED TO WORK TEAM VALIDATION ERROR 
									
								WHEN ((MemberCase.AssignedToWorkTeamDate IS NOT NULL) AND (WorkTeamMembership.WorkTeamId IS NULL)
								
									AND (@ignoreAssignedTo = 0)) THEN 5 -- ASSIGNED TO
									
								ELSE 0 -- NO ASSIGNMENT, ANYONE CAN LOCK
									
							END 
							
					FROM 
					
						dbo.MemberCase
						
							LEFT JOIN dbo.WorkTeam 
							
								ON MemberCase.AssignedToWorkTeamId = WorkTeam.WorkTeamId

							LEFT JOIN dbo.WorkTeamMembership 
							
								ON WorkTeam.WorkTeamId = WorkTeamMembership.WorkTeamId
								
									AND ((WorkTeamMembership.SecurityAuthorityId = @modifiedAuthorityId) AND (WorkTeamMembership.UserAccountId = @modifiedAccountId))
								
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
            
            LockedBySecurityAuthorityId = @lockedBySecurityAuthorityId,
            LockedByUserAccountId = @lockedByUserAccountId,
            LockedByUserAccountName = @lockedByUserAccountName,
            LockedByUserDisplayName = @lockedByUserDisplayName,
            LockedByDate = @modifiedDate,
            
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
GRANT EXECUTE ON dbo.MemberCase_Lock TO PUBLIC
GO          
*/