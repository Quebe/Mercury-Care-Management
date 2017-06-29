/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCase_Insert' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCase_Insert
GO      
*/

CREATE PROCEDURE dbo.MemberCase_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                   BIGINT,
      @ignoreExisting             BIT,
      
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060),
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */       
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
      
      @memberCaseId			BIGINT			OUTPUT
      
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        /* LOCAL VARIABLES ( END ) */
        
        
        SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
        
        BEGIN TRANSACTION 
        
					IF (@ignoreExisting = 0) 
					
					  BEGIN

							SELECT @memberCaseId = MemberCaseId FROM MemberCase WHERE MemberId = @memberId AND (Status NOT IN (3, 4)) -- NOT CLOSED OR VOID, ONLY ACTIVE AND DEVELOPMENT
							
						END 
					
				
					IF (@memberCaseId IS NULL) 
					
						BEGIN
	          
							INSERT INTO dbo.MemberCase (
	            
									MemberId, ReferenceNumber, Status,
	                
									AssignedToWorkTeamId, AssignedToWorkTeamDate, 
	                
									AssignedToSecurityAuthorityId, AssignedToUserAccountId, AssignedToUserAccountName, AssignedToUserDisplayName, AssignedToDate,
									
									LockedBySecurityAuthorityId, LockedByUserAccountId, LockedByUserAccountName, LockedByUserDisplayName, LockedByDate,
	                
									EffectiveDate, TerminationDate,
	                
									ExtendedProperties,
	            
									CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
	                
									ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
	                
							VALUES (
	            
									@memberId, '', 1,   -- UNDER DEVELOPMENT
	                
									NULL, NULL, -- ASSIGNED TO WORK TEAM ID/DATE
									
									0, '', '', '', NULL, -- ASSIGNED TO TEAM MEMBER INFORMATION 
									
									NULL, '', '', '', NULL, -- LOCKED BY TEAM MEMBER INFORMATION 
	                
									CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME), '12/31/9999', -- EFFECTIVE/TERMINATION DATE
	                
									NULL, -- XML EXTENDED PROPERTIES

									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
	                
									@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
									
									
							SET @memberCaseId = @@IDENTITY
	           
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
GRANT EXECUTE ON dbo.MemberCase_Insert TO PUBLIC
GO          
*/