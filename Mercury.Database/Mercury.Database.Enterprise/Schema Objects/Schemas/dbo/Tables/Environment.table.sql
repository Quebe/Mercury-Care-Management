-- DBO.[ENVIRONMENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Environment')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Environment]
GO 
*/ 


CREATE TABLE dbo.[Environment] (
    EnvironmentId                                         BIGINT                      NOT NULL    IDENTITY (1000000000, 1),
    EnvironmentName                                      VARCHAR (0060)               NOT NULL,
    EnvironmentDescription                               VARCHAR (0999)               NOT NULL    CONSTRAINT [Environment_Dft_EnvironmentDescription] DEFAULT (''),

    EnvironmentTypeId                                     BIGINT                      NOT NULL    CONSTRAINT [Environment_Dft_EnvironmentTypeId] DEFAULT (0),
    EnvironmentTag                                       VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_EnvironmentTag] DEFAULT (''),

    ConfidentialityStatement                             VARCHAR (0999)               NOT NULL    CONSTRAINT [Environment_Dft_ConfidentialityStatement] DEFAULT (''),

    SqlServerName                                        VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_SqlServerName] DEFAULT ('(local)'),
    SqlDatabaseName                                      VARCHAR (0060)               NOT NULL,

    UseTrustedConnection                                     BIT                      NOT NULL    CONSTRAINT [Environment_Dft_UseTrustedConnection] DEFAULT (1),
    SqlUserName                                          VARCHAR (0060)               NOT NULL,
    SqlPassword                                          VARCHAR (0060)               NOT NULL,

    UseConnectionPooling                                     BIT                      NOT NULL    CONSTRAINT [Environment_Dft_UseConnectionPooling] DEFAULT (1),
    PoolSizeMinimum                                          INT                      NOT NULL    CONSTRAINT [Environment_Dft_PoolSizeMinimum] DEFAULT (1),
    PoolSizeMaximum                                          INT                      NOT NULL    CONSTRAINT [Environment_Dft_PoolSizeMaximum] DEFAULT (100),

    CustomAttributes                                     VARCHAR (0255)               NOT NULL,

    Enabled                                                  BIT                      NOT NULL    CONSTRAINT [Environment_Dft_Enabled] DEFAULT (1),
    Visible                                                  BIT                      NOT NULL    CONSTRAINT [Environment_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                      VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                          DATETIME                      NOT NULL    CONSTRAINT [Environment_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [Environment_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                        DATETIME                      NOT NULL    CONSTRAINT [Environment_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Environment_Pk] PRIMARY KEY (EnvironmentId)

  , CONSTRAINT [Environment_Fk_EnvironmentTypeId] FOREIGN KEY (EnvironmentTypeId) REFERENCES dbo.[EnvironmentType] (EnvironmentTypeId)

  , CONSTRAINT [Environment_Unq_EnvironmentName] UNIQUE (EnvironmentName)

  ) -- dbo.Environment

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'EnvironmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'EnvironmentName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'EnvironmentDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'EnvironmentTypeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'EnvironmentTag'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description [Confidentiality Statement]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'ConfidentialityStatement'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'SqlServerName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'SqlDatabaseName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'UseTrustedConnection'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [(not recommended)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'SqlUserName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [(not recommended)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'SqlPassword'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'UseConnectionPooling'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of [Connection Pool Size]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'PoolSizeMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of [Connection Pool Size]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'PoolSizeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Directory Path [Custom Connection Attributes]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'CustomAttributes'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Environment', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENVIRONMENT ( END ) 


