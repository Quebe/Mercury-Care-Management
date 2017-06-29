/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Environment_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Environment_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Environment_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @environmentId            BIGINT,
      @environmentName          VARCHAR (060),
      @confidentialityStatement VARCHAR (999),
    
      @sqlServerName            VARCHAR (060),
      @sqlDatabaseName          VARCHAR (060),

      @useTrustedConnection     BIT,
      @sqlUserName              VARCHAR (060),
      @sqlPassword              VARCHAR (040),

      @useConnectionPooling     BIT,
      @poolSizeMinimum          INT,
      @poolSizeMaximum          INT,

      @customAttributes         VARCHAR (120),
            
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

        IF EXISTS (SELECT * FROM Environment WHERE EnvironmentId = @EnvironmentId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE Environment
              SET
                EnvironmentName = @environmentName,
                ConfidentialityStatement = @confidentialityStatement,

                SqlServerName = @sqlServerName,               
                SqlDatabaseName = @sqlDatabaseName,             

                UseTrustedConnection = @useTrustedConnection,          
                SqlUserName = CASE WHEN (@useTrustedConnection = 0) THEN @sqlUserName ELSE '' END,
                SqlPassword = CASE WHEN (@useTrustedConnection = 0) AND (LEN (@sqlPassword) > 0) THEN @sqlPassword ELSE SqlPassword END,

                UseConnectionPooling = @useConnectionPooling,              
                PoolSizeMinimum = @poolSizeMinimum,          
                PoolSizeMaximum = @poolSizeMaximum,           

                CustomAttributes = @customAttributes,           

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                EnvironmentId = @EnvironmentId
                
          
          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO Environment (
                                        EnvironmentName,    ConfidentialityStatement,
                SqlServerName,          SqlDatabaseName,             
                UseTrustedConnection,   SqlUserName,        SqlPassword, 
                UseConnectionPooling,   PoolSizeMinimum,    PoolSizeMaximum,
                CustomAttributes, 

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                                        @environmentName, @confidentialityStatement,
                @sqlServerName,         @sqlDatabaseName,             

                @useTrustedConnection,       
                CASE WHEN (@useTrustedConnection = 0) THEN @sqlUserName ELSE '' END,
                CASE WHEN (@useTrustedConnection = 0) THEN @sqlPassword ELSE '' END,

                @useConnectionPooling,  @poolSizeMinimum, @poolSizeMaximum,

                @customAttributes, 
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Environment_InsertUpdate TO PUBLIC
GO          
*/