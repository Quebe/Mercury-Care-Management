-- DBO.[DATAEXPLORER] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DataExplorer')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[DataExplorer]
GO 
*/ 


CREATE TABLE dbo.[DataExplorer] (
    DataExplorerId                                                            BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    DataExplorerName                                                         VARCHAR (0060) NOT NULL,
    DataExplorerDescription                                                  VARCHAR (0999) NOT NULL,

    IsPublic                                                                     BIT  NOT NULL    CONSTRAINT [DataExplorer_Dft_IsPublic] DEFAULT (1),
    IsReadOnly                                                                   BIT  NOT NULL    CONSTRAINT [DataExplorer_Dft_IsReadOnly] DEFAULT (1),

    OwnerSecurityAuthorityId                                                  BIGINT      NULL,
    OwnerUserAccountId                                                       VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_OwnerUserAccountId] DEFAULT (''),
    OwnerUserAccountName                                                     VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_OwnerUserAccountName] DEFAULT (''),
    OwnerUserDisplayName                                                     VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_OwnerUserDisplayName] DEFAULT (''),

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [DataExplorer_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [DataExplorer_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [DataExplorer_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [DataExplorer_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [DataExplorer_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [DataExplorer_Pk] PRIMARY KEY (DataExplorerId)

  , CONSTRAINT [DataExplorer_Unq_DataExplorerName] UNIQUE (DataExplorerName)

  ) -- dbo.DataExplorer

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'DataExplorerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'DataExplorerName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'DataExplorerDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'IsPublic'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'IsReadOnly'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'OwnerSecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'OwnerUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'OwnerUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'OwnerUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorer', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.DATAEXPLORER ( END ) 


