-- DBO.[WORKFLOW] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Workflow')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Workflow]
GO 
*/ 


CREATE TABLE dbo.[Workflow] (
    WorkflowId                                                                BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    WorkflowName                                                             VARCHAR (0060) NOT NULL,
    WorkflowDescription                                                      VARCHAR (0999) NOT NULL,

    Framework                                                                    INT  NOT NULL    CONSTRAINT [Workflow_Dft_Framework] DEFAULT (0),

    EntityType                                                                   INT  NOT NULL    CONSTRAINT [Workflow_Dft_EntityType] DEFAULT (0),
    ActionVerb                                                               VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_ActionVerb] DEFAULT (''),

    AssemblyPath                                                             VARCHAR (0255) NOT NULL    CONSTRAINT [Workflow_Dft_AssemblyPath] DEFAULT (''),
    AssemblyName                                                             VARCHAR (0255) NOT NULL    CONSTRAINT [Workflow_Dft_AssemblyName] DEFAULT (''),
    AssemblyClassName                                                        VARCHAR (0255) NOT NULL    CONSTRAINT [Workflow_Dft_AssemblyClassName] DEFAULT (''),

    WorkflowParameters                                                           XML      NULL,

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [Workflow_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [Workflow_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Workflow_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Workflow_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Workflow_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Workflow_Pk] PRIMARY KEY (WorkflowId)

  , CONSTRAINT [Workflow_Fk_EntityType] FOREIGN KEY (EntityType) REFERENCES enum.[EntityType] (EntityType)

  , CONSTRAINT [Workflow_Unq_WorkflowName] UNIQUE (WorkflowName)

  ) -- dbo.Workflow

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'WorkflowId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'WorkflowName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'WorkflowDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Windows Workflow Foundation Version]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'Framework'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'EntityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ActionVerb'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Directory Path', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'AssemblyPath'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Namespace', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'AssemblyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Namespace', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'AssemblyClassName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'WorkflowParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Workflow', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKFLOW ( END ) 


