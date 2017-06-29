/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Role_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Role_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Role_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @roleId            BIGINT,
      @roleName          VARCHAR (060),
      @roleDescription   VARCHAR (999),
    
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

        IF EXISTS (SELECT * FROM Role WHERE RoleId = @roleId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE Role
              SET
                RoleName = @roleName,
                RoleDescription = @roleDescription,

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                RoleId = @roleId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO Role (
                                        RoleName,    RoleDescription ,

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                                        @roleName, @roleDescription,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Role_InsertUpdate TO PUBLIC
GO          
*/