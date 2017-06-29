
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkTeam_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkTeam_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkTeam_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workTeamId                BIGINT,
      @workTeamName             VARCHAR (060),
      @workTeamDescription      VARCHAR (999),
      
      @workTeamType               INT,

      @enabled                    BIT,
      @visible                    BIT,

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

        IF EXISTS (SELECT * FROM dbo.WorkTeam WHERE ((WorkTeamId = @workTeamId) AND (@workTeamId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkTeam
              SET
                WorkTeamName = @workTeamName,
                WorkTeamDescription = @workTeamDescription,

				WorkTeamType = @workTeamType,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkTeamId = @workTeamId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.WorkTeam (
                WorkTeamName, WorkTeamDescription, WorkTeamType, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workTeamName, @workTeamDescription, @workTeamType, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/