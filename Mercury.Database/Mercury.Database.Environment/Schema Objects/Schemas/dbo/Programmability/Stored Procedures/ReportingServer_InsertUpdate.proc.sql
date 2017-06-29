/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ReportingServer_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ReportingServer_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ReportingServer_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @reportingServerId              BIGINT,
      @reportingServerName           VARCHAR (060),
      @reportingServerDescription    VARCHAR (999),
      
      @assemblyPath           VARCHAR (255),
      @assemblyName           VARCHAR (255),
      @assemblyClassName      VARCHAR (255),
      
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

        IF EXISTS (SELECT * FROM dbo.ReportingServer WHERE ((ReportingServerId = @reportingServerId) AND (@reportingServerId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ReportingServer
              SET
                ReportingServerName = @reportingServerName,
                ReportingServerDescription = @reportingServerDescription,
                                
                AssemblyPath = @assemblyPath,
                AssemblyName = @assemblyName,
                AssemblyClassName = @assemblyClassName,
                
                WebServiceHostConfiguration = @webServiceHostConfiguration,
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ReportingServerId = @reportingServerId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ReportingServer (
                ReportingServerName, ReportingServerDescription, AssemblyPath, AssemblyName, AssemblyClassName, WebServiceHostConfiguration, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @reportingServerName, @reportingServerDescription, @assemblyPath, @assemblyName, @assemblyClassName, @webServiceHostConfiguration, @extendedProperties, @enabled, @visible, 

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