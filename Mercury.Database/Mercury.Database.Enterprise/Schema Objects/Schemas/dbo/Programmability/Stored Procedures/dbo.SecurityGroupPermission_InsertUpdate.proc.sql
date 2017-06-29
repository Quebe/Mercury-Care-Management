/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SecurityGroupPermission_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.SecurityGroupPermission_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.SecurityGroupPermission_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId   BIGINT,
      @securityGroupId       VARCHAR (060),
      @permissionId          BIGINT, 
      @isGranted             BIT,
      @isDenied              BIT,
      
      @modifiedAuthorityName VARCHAR (060),
      @modifiedAccountId     VARCHAR (060),
      @modifiedAccountName   VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
	
        IF EXISTS (SELECT * FROM SecurityGroupPermission WHERE SecurityAuthorityId = @securityAuthorityId AND SecurityGroupId = @securityGroupId AND PermissionId = @permissionId) 
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE SecurityGroupPermission
              SET
                IsGranted = CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END,
                IsDenied  = @isDenied,
      
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                    SecurityAuthorityId = @securityAuthorityId
                AND SecurityGroupId     = @securityGroupId
                AND PermissionId        = @permissionId
          
          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO SecurityGroupPermission (SecurityAuthorityId, SecurityGroupId, PermissionId, IsGranted, IsDenied,
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (@securityAuthorityId, @securityGroupId, @permissionId,
                CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END, @isDenied, 
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.SecurityGroupPermission_InsertUpdate TO PUBLIC
GO          
*/