-- DBO.[SECURITYAUTHORITY] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'SecurityAuthority')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[SecurityAuthority]
GO 
*/ 


CREATE TABLE dbo.[SecurityAuthority] (
    SecurityAuthorityId                                   BIGINT                      NOT NULL    IDENTITY (1000000000, 1),
    SecurityAuthorityName                                VARCHAR (0060)               NOT NULL,
    SecurityAuthorityDescription                         VARCHAR (0999)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_SecurityAuthorityDescription] DEFAULT (''),
    SecurityAuthorityType                                    INT                      NOT NULL    CONSTRAINT [SecurityAuthority_Dft_SecurityAuthorityType] DEFAULT (1),

    Protocol                                             VARCHAR (0030)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_Protocol] DEFAULT (''),
    ServerName                                           VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ServerName] DEFAULT (''),
    Domain                                               VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_Domain] DEFAULT (''),

    MemberContext                                        VARCHAR (0120)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_MemberContext] DEFAULT (''),
    ProviderContext                                      VARCHAR (0120)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ProviderContext] DEFAULT (''),
    AssociateContext                                     VARCHAR (0120)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_AssociateContext] DEFAULT (''),

    AgentName                                            VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_AgentName] DEFAULT (''),
    AgentPassword                                        VARCHAR (0040)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_AgentPassword] DEFAULT (''),

    ProviderAssemblyPath                                 VARCHAR (0255)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ProviderAssemblyPath] DEFAULT (''),
    ProviderAssemblyName                                 VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ProviderAssemblyName] DEFAULT (''),
    ProviderNamespace                                    VARCHAR (0255)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ProviderNamespace] DEFAULT (''),
    ProviderClassName                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ProviderClassName] DEFAULT (''),

    ConfigurationSection                                 VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ConfigurationSection] DEFAULT (''),

    CreateAuthorityName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                      VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                          DATETIME                      NOT NULL    CONSTRAINT [SecurityAuthority_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                        DATETIME                      NOT NULL    CONSTRAINT [SecurityAuthority_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [SecurityAuthority_Pk] PRIMARY KEY (SecurityAuthorityId)

  , CONSTRAINT [SecurityAuthority_Fk_SecurityAuthorityType] FOREIGN KEY (SecurityAuthorityType) REFERENCES enum.[SecurityAuthorityType] (SecurityAuthorityType)

  , CONSTRAINT [SecurityAuthority_Unq_SecurityAuthorityName] UNIQUE (SecurityAuthorityName)

  ) -- dbo.SecurityAuthority

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration [(Windows Integrated, Active Directory, etc.)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Network Transport Protocol', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'Protocol'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ServerName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'Domain'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Context [Starting OU or other Context]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'MemberContext'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Context [Starting OU or other Context]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ProviderContext'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Context [Starting OU or other Context]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'AssociateContext'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [(not recommended) X-Domain Support]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'AgentName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unencrypted Password [(not recommended) X-Domain Support]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'AgentPassword'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Directory Path [Custom Security Interface Assembly]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ProviderAssemblyPath'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Custom Security Interface Assembly]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ProviderAssemblyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Namespace [Custom Security Interface Assembly]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ProviderNamespace'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Custom Security Interface Assembly]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ProviderClassName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Custom Security Interface Assembly]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ConfigurationSection'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SecurityAuthority', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.SECURITYAUTHORITY ( END ) 


