/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkTeamMembership_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkTeamMembership_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkTeamMembership_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workTeamId              BIGINT,
      @securityAuthorityId     BIGINT,
      @userAccountId          VARCHAR (060),
      
      @userAccountName        VARCHAR (060),
      @userDisplayName        VARCHAR (060),
      @workTeamRole               INT,
    
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

        IF EXISTS (SELECT * FROM dbo.WorkTeamMembership WHERE WorkTeamId = @workTeamId AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkTeamMembership
              SET
                UserAccountName = @userAccountName,
                UserDisplayName = @userDisplayName,
                
                WorkTeamRole = @workTeamRole,
              
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkTeamId = @workTeamId AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO WorkTeamMembership (
                WorkTeamId, SecurityAuthorityId, UserAccountId, UserAccountName, UserDisplayName, WorkTeamRole,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workTeamId, @securityAuthorityId, @userAccountId, @userAccountName, @userDisplayName, @workTeamRole,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.WorkTeamMembership_InsertUpdate TO PUBLIC
GO          
*/