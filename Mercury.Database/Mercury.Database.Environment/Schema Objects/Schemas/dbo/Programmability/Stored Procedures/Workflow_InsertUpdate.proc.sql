/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Workflow_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Workflow_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Workflow_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workflowId              BIGINT,
      @workflowName           VARCHAR (060),
      @workflowDescription    VARCHAR (999),
      
      @framework                  INT,
      
      @entityType                 INT,
      @actionVerb             VARCHAR (060),
      
      @assemblyPath           VARCHAR (255),
      @assemblyName           VARCHAR (255),
      @assemblyClassName      VARCHAR (255),
      
      @workflowParameters         XML,
      @extendedProperties         XML,

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

        IF EXISTS (SELECT * FROM dbo.Workflow WHERE ((WorkflowId = @workflowId) AND (@workflowId != 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Workflow
              SET
                WorkflowName = @workflowName,
                WorkflowDescription = @workflowDescription,
                
                Framework = @framework,
                
                EntityType = @entityType,
                ActionVerb = @actionVerb,
                
                AssemblyPath = @assemblyPath,
                AssemblyName = @assemblyName,
                AssemblyClassName = @assemblyClassName,
                
                WorkflowParameters = @workflowParameters,
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkflowId = @workflowId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Workflow (
                WorkflowName, WorkflowDescription, Framework, EntityType, ActionVerb, AssemblyPath, AssemblyName, AssemblyClassName, WorkflowParameters, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workflowName, @workflowDescription, @framework, @entityType, @actionVerb, @assemblyPath, @assemblyName, @assemblyClassName, @workflowParameters, @extendedProperties, @enabled, @visible, 

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