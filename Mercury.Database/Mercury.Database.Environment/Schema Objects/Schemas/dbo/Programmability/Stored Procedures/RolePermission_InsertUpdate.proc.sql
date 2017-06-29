/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'RolePermission_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.RolePermission_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.RolePermission_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @roleId                  BIGINT,
      @permissionId            BIGINT,
      @isGranted                  BIT,
      @isDenied                   BIT,
    
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.RolePermission WHERE RoleId = @roleId AND PermissionId = @permissionId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.RolePermission
              SET
                IsGranted = CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END,
                IsDenied  = @isDenied,                

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                RoleId = @roleId AND PermissionId = @permissionId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO RolePermission (
                RoleId,    PermissionId, IsGranted, IsDenied,

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @roleId, @permissionId, CASE WHEN (@isDenied = 1) THEN 0 ELSE @isGranted END, @isDenied,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.RolePermission_InsertUpdate TO PUBLIC
GO          
*/