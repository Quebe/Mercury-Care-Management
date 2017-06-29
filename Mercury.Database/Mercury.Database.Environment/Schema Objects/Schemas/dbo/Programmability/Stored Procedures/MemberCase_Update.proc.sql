/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_Update' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_Update
GO      
*/

CREATE PROCEDURE dbo.MemberCase_Update
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseId               BIGINT,
      @memberId                   BIGINT,
      @referenceNumber VARCHAR (020),
      @status INT,

			@memberCaseDescription VARCHAR (8000),

			@effectiveDate								DATETIME,
			@terminationDate							DATETIME,
      
      @extendedProperties          XML,
      
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
        
				SELECT @validationError = 
        
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
            
            MemberId = @memberId,
            ReferenceNumber = @referenceNumber,
            Status = @status,

						MemberCaseDescription = @memberCaseDescription,
            
            EffectiveDate = @effectiveDate,
            TerminationDate = @terminationDate,
            
            ExtendedProperties = @extendedProperties,

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
GRANT EXECUTE ON dbo.MemberCase_Update TO PUBLIC
GO          
*/