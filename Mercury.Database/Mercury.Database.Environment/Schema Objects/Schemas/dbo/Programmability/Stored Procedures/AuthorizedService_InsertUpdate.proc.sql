/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Service_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Service_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.AuthorizedService_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @authorizedServiceId               BIGINT,
      @authorizedServiceName            VARCHAR (060),
      @authorizedServiceDescription     VARCHAR (999),

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

        IF EXISTS (SELECT * FROM dbo.AuthorizedService WHERE AuthorizedServiceId = @authorizedServiceId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.AuthorizedService
              SET
                AuthorizedServiceName = @authorizedServiceName,
                AuthorizedServiceDescription = @authorizedServiceDescription,

                Enabled = @enabled,
                Visible = @visible,

                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                AuthorizedServiceId = @authorizedServiceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.AuthorizedService (AuthorizedServiceName, AuthorizedServiceDescription, Enabled, Visible, ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @authorizedServiceName, @authorizedServiceDescription, @enabled, @visible, @extendedProperties,

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