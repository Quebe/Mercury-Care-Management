/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorer_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.DataExplorer_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.DataExplorer_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @dataExplorerId                BIGINT,
      @dataExplorerName             VARCHAR (060),
      @dataExplorerDescription      VARCHAR (999),
      
      @isPublic                 BIT,
      @isReadOnly                 BIT,


      @ownerSecurityAuthorityId         BIGINT,
      @ownerUserAccountId      VARCHAR (060),
      @ownerUserAccountName    VARCHAR (060),
      @ownerUserDisplayName    VARCHAR (060),
      
      @enabled                    BIT,
      @visible                    BIT,

      @extendedProperties         XML,
      
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

        IF EXISTS (SELECT * FROM dbo.DataExplorer WHERE DataExplorerId = @dataExplorerId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.DataExplorer
              SET
                DataExplorerName = @dataExplorerName,
                DataExplorerDescription = @dataExplorerDescription,
                
                IsPublic = @isPublic,
                IsReadOnly = @isReadOnly,
          
            OwnerSecurityAuthorityId = @ownerSecurityAuthorityId,
            OwnerUserAccountId       = @ownerUserAccountId,
            OwnerUserAccountName     = @ownerUserAccountName,
            OwnerUserDisplayName     = @ownerUserDisplayName,
            
								Enabled = @enabled,
                Visible = @visible,

                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                DataExplorerId = @dataExplorerId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.DataExplorer (
                DataExplorerName, DataExplorerDescription, IsPublic, IsReadOnly,
                
          
            OwnerSecurityAuthorityId,
            OwnerUserAccountId,
            OwnerUserAccountName,
            OwnerUserDisplayName,
            
                Enabled, Visible, 
                
                ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @dataExplorerName, @dataExplorerDescription, @isPublic, @isReadOnly,
                
          
            @ownerSecurityAuthorityId,
            @ownerUserAccountId,
            @ownerUserAccountName,
            @ownerUserDisplayName,
            
                @enabled, @visible, 
                
                @extendedProperties,

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