-- AUDIT.[AUTHENTICATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Authentication')  AND (TABLE_SCHEMA = 'audit'))
  DROP TABLE audit.[Authentication]
GO 
*/ 


CREATE TABLE audit.[Authentication] (
    SessionToken                                UNIQUEIDENTIFIER                      NOT NULL,
    LogonDateTime                                       DATETIME                      NOT NULL    CONSTRAINT [Authentication_Dft_LogonDateTime] DEFAULT (GETDATE ()),
    LogoffDateTime                                      DATETIME                          NULL,
    AuthenticationTime                                       INT                      NOT NULL,
    LastActivityDateTime                                DATETIME                      NOT NULL    CONSTRAINT [Authentication_Dft_LastActivityDateTime] DEFAULT (GETDATE ()),

    EnvironmentId                                         BIGINT                      NOT NULL,
    SecurityAuthorityId                                   BIGINT                      NOT NULL,
    UserAccountId                                        VARCHAR (0060)               NOT NULL,
    UserAccountName                                      VARCHAR (0060)               NOT NULL,
    UserDisplayName                                      VARCHAR (0060)               NOT NULL,

    CreateAuthorityName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                      VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                          DATETIME                      NOT NULL    CONSTRAINT [Authentication_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                    VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                  VARCHAR (0060)               NOT NULL    CONSTRAINT [Authentication_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                        DATETIME                      NOT NULL    CONSTRAINT [Authentication_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Authentication_Pk] PRIMARY KEY (SessionToken)

  ) -- audit.Authentication

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID) [Unique Session Identifier]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'SessionToken'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'LogonDateTime'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [NULL is an Active Session]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'LogoffDateTime'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of [Milliseconds to Authenticate w/ Security Authority]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'AuthenticationTime'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Last Date/Time of User Activity with Server]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'LastActivityDateTime'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'EnvironmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'UserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'UserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'UserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'audit', @level1type=N'TABLE', @level1name=N'Authentication', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- AUDIT.AUTHENTICATION ( END ) 


