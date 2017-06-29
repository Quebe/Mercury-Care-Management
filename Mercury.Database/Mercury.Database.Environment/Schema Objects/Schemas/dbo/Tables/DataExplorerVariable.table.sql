-- DBO.[DATAEXPLORERVARIABLE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DataExplorerVariable')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[DataExplorerVariable]
GO 
*/ 


CREATE TABLE dbo.[DataExplorerVariable] (
    DataExplorerVariableId                                                    BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    DataExplorerVariableName                                                 VARCHAR (0060) NOT NULL,
    DataExplorerVariableDescription                                          VARCHAR (0999) NOT NULL,
    DataExplorerId                                                            BIGINT  NOT NULL,

    VariableType                                                                 INT  NOT NULL,
    TextValue                                                                VARCHAR (0999) NOT NULL,
    NumericValue                                                             DECIMAL (20, 8) NOT NULL,
    DateValue                                                               DATETIME      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [DataExplorerVariable_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [DataExplorerVariable_Pk] PRIMARY KEY (DataExplorerVariableId)

  , CONSTRAINT [DataExplorerVariable_Fk_DataExplorerId] FOREIGN KEY (DataExplorerId) REFERENCES dbo.[DataExplorer] (DataExplorerId)

  ) -- dbo.DataExplorerVariable

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'DataExplorerVariableId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'DataExplorerVariableName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'DataExplorerVariableDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'DataExplorerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'VariableType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'TextValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'NumericValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'DateValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerVariable', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.DATAEXPLORERVARIABLE ( END ) 


