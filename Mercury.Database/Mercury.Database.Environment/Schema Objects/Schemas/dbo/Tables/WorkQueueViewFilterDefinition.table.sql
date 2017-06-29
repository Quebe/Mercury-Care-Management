-- DBO.[WORKQUEUEVIEWFILTERDEFINITION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueViewFilterDefinition')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueViewFilterDefinition]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueViewFilterDefinition] (
    WorkQueueViewId                                                           BIGINT  NOT NULL,
    Sequence                                                                     INT  NOT NULL,
    FieldName                                                                VARCHAR (0060) NOT NULL,
    FilterOperator                                                               INT  NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_FilterOperator] DEFAULT (0),
    FilterValue                                                              VARCHAR (0999) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_FilterValue] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueueViewFilterDefinition_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueViewFilterDefinition_Pk] PRIMARY KEY (WorkQueueViewId, Sequence)

  , CONSTRAINT [WorkQueueViewFilterDefinition_Fk_WorkQueueViewId] FOREIGN KEY (WorkQueueViewId) REFERENCES dbo.[WorkQueueView] (WorkQueueViewId)

  ) -- dbo.WorkQueueViewFilterDefinition

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'WorkQueueViewId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'Sequence'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'FieldName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration [Ascending or Descending Sort Order]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'FilterOperator'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'FilterValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFilterDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUEVIEWFILTERDEFINITION ( END ) 

