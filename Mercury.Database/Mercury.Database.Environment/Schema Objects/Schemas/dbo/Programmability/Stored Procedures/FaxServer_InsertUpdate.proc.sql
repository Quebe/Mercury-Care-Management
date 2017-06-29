/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'FaxServer_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.FaxServer_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.FaxServer_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @faxServerId              BIGINT,
      @faxServerName           VARCHAR (060),
      @faxServerDescription    VARCHAR (999),
      
      @assemblyPath           VARCHAR (255),
      @assemblyName           VARCHAR (255),
      @assemblyClassName      VARCHAR (255),
      
      @faxServerConfiguration      XML,
      @webServiceHostConfiguration XML,
      @extendedProperties          XML,

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

        IF EXISTS (SELECT * FROM dbo.FaxServer WHERE ((FaxServerId = @faxServerId) AND (@faxServerId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.FaxServer
              SET
                FaxServerName = @faxServerName,
                FaxServerDescription = @faxServerDescription,
                                
                AssemblyPath = @assemblyPath,
                AssemblyName = @assemblyName,
                AssemblyClassName = @assemblyClassName,
                
                FaxServerConfiguration = @faxServerConfiguration,
                WebServiceHostConfiguration = @webServiceHostConfiguration,
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                FaxServerId = @faxServerId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.FaxServer (
                FaxServerName, FaxServerDescription, AssemblyPath, AssemblyName, AssemblyClassName, FaxServerConfiguration, WebServiceHostConfiguration, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @faxServerName, @faxServerDescription, @assemblyPath, @assemblyName, @assemblyClassName, @faxServerConfiguration, @webServiceHostConfiguration, @extendedProperties, @enabled, @visible, 

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