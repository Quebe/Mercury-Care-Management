-- DBO.[ENVIRONMENTTYPE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EnvironmentType')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EnvironmentType]
GO 
*/ 


CREATE TABLE dbo.[EnvironmentType] (
    EnvironmentTypeId                                     BIGINT                      NOT NULL    IDENTITY (1000000000, 1),
    EnvironmentTypeName                                  VARCHAR (0060)               NOT NULL,
    EnvironmentTypeDescription                           VARCHAR (0999)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_EnvironmentTypeDescription] DEFAULT (''),

    Enabled                                                  BIT                      NOT NULL    CONSTRAINT [EnvironmentType_Dft_Enabled] DEFAULT (1),
    Visible                                                  BIT                      NOT NULL    CONSTRAINT [EnvironmentType_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                      VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                          DATETIME                      NOT NULL    CONSTRAINT [EnvironmentType_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [EnvironmentType_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                        DATETIME                      NOT NULL    CONSTRAINT [EnvironmentType_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EnvironmentType_Pk] PRIMARY KEY (EnvironmentTypeId)

  , CONSTRAINT [EnvironmentType_Unq_EnvironmentTypeName] UNIQUE (EnvironmentTypeName)

  ) -- dbo.EnvironmentType

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'EnvironmentTypeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'EnvironmentTypeName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'EnvironmentTypeDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EnvironmentType', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENVIRONMENTTYPE ( END ) 


