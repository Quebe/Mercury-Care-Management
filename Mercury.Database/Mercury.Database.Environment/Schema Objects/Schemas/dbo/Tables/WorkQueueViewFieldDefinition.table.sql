-- DBO.[WORKQUEUEVIEWFIELDDEFINITION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueViewFieldDefinition')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueViewFieldDefinition]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueViewFieldDefinition] (
    WorkQueueViewId                                                           BIGINT  NOT NULL,
    PropertyName                                                             VARCHAR (0060) NOT NULL,
    DisplayName                                                              VARCHAR (0060) NOT NULL,
    DefaultValue                                                             VARCHAR (0099) NOT NULL,
    DataType                                                                     INT  NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_DataType] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueueViewFieldDefinition_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueViewFieldDefinition_Pk] PRIMARY KEY (WorkQueueViewId, PropertyName)

  , CONSTRAINT [WorkQueueViewFieldDefinition_Fk_WorkQueueViewId] FOREIGN KEY (WorkQueueViewId) REFERENCES dbo.[WorkQueueView] (WorkQueueViewId)

  ) -- dbo.WorkQueueViewFieldDefinition

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Work Queue View Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'WorkQueueViewId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Unique Property Name in Extended Properties]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'PropertyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Friendly Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'DisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Property Value [Used when Property not found.]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'DefaultValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Data Type (Integer, String, etc.)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'DataType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueViewFieldDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUEVIEWFIELDDEFINITION ( END ) 


