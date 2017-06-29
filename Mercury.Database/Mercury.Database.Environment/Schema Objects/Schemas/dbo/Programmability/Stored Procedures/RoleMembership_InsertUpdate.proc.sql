/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'RoleMembership_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.RoleMembership_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.RoleMembership_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @roleId                  BIGINT,
      @securityAuthorityId     BIGINT,
      @securityGroupId        VARCHAR (060),
    
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

        IF EXISTS (SELECT * FROM dbo.RoleMembership WHERE RoleId = @roleId AND SecurityAuthorityId = @securityAuthorityId AND SecurityGroupId = @securityGroupId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.RoleMembership
              SET
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                RoleId = @roleId AND SecurityAuthorityId = @securityAuthorityId AND SecurityGroupId = @securityGroupId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO RoleMembership (
                RoleId, SecurityAuthorityId, SecurityGroupId,

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @roleId, @securityAuthorityId, @securityGroupId,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.RoleMembership_InsertUpdate TO PUBLIC
GO          
*/