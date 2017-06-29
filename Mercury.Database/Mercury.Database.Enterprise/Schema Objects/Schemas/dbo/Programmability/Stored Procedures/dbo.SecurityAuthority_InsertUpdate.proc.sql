/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SecurityAuthority_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.SecurityAuthority_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.SecurityAuthority_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId           BIGINT,
      @securityAuthorityName		VARCHAR (060),
      @securityAuthorityDescription VARCHAR (999),
      @securityAuthorityType		VARCHAR (060),
    
      @protocol               VARCHAR (030),
      @serverName             VARCHAR (060),
      @domain                 VARCHAR (060),

      @memberContext          VARCHAR (120),
      @providerContext        VARCHAR (120),
      @associateContext       VARCHAR (120),

      @agentName              VARCHAR (060),
      @agentPassword          VARCHAR (040),

      @providerAssemblyPath   VARCHAR (255),
      @providerAssemblyName   VARCHAR (060),

      @providerNamespace      VARCHAR (255),
      @providerClassName      VARCHAR (060),
      @configurationSection   VARCHAR (060),
            
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

        IF EXISTS (SELECT * FROM SecurityAuthority WHERE SecurityAuthorityId = @securityAuthorityId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE SecurityAuthority
              SET
                SecurityAuthorityName = @securityAuthorityName,
                SecurityAuthorityDescription = @securityAuthorityDescription,
                SecurityAuthorityType = @securityAuthorityType,

                Protocol = @protocol,               
                ServerName = @serverName,             
                Domain = @domain,                 

                MemberContext = @memberContext,          
                ProviderContext = @providerContext,        
                AssociateContext = @associateContext,       

                AgentName = @agentName,              
                AgentPassword = @agentPassword,          

                ProviderAssemblyPath = @providerAssemblyPath,           
                ProviderAssemblyName = @providerAssemblyName,           

                ProviderNamespace = @providerNamespace,      
                ProviderClassName = @providerClassName,      
                ConfigurationSection = @configurationSection,   
                      
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                SecurityAuthorityId = @securityAuthorityId
                
          
          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO SecurityAuthority (
				SecurityAuthorityName, SecurityAuthorityDescription, SecurityAuthorityType,
                Protocol,				ServerName,            Domain, 
                MemberContext,			ProviderContext,       AssociateContext, 
                AgentName,				AgentPassword,
                ProviderAssemblyPath,	ProviderAssemblyName,
                ProviderNamespace,		ProviderClassName,     ConfigurationSection,

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
				@securityAuthorityName, @securityAuthorityDescription, @securityAuthorityType,
                @protocol,            @serverName,            @domain, 
                @memberContext,       @providerContext,       @associateContext, 
                @agentName,           @agentPassword,
                @providerAssemblyPath,        @providerAssemblyName,
                @providerNamespace,   @providerClassName,     @configurationSection,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.SecurityAuthority_InsertUpdate TO PUBLIC
GO          
*/