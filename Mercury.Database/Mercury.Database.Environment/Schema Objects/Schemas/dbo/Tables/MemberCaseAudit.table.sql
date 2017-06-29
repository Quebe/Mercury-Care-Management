-- DBO.[MEMBERCASEAUDIT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseAudit')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseAudit]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseAudit] (
    MemberCaseAuditId                                                         BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseId                                                              BIGINT  NOT NULL,
    MemberCaseAuditDescription                                               VARCHAR (0999) NOT NULL,
    AuditObjectType                                                          VARCHAR (0120) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_AuditObjectType] DEFAULT (''),
    AuditObjectId                                                             BIGINT  NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_AuditObjectId] DEFAULT (0),
    UserDisplayName                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_UserDisplayName] DEFAULT (''),
    SourceObjectType                                                         VARCHAR (0120) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_SourceObjectType] DEFAULT (''),
    SourceObjectId                                                            BIGINT  NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_SourceObjectId] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseAudit_Dft_CreateDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseAudit_Pk] PRIMARY KEY (MemberCaseAuditId)

  , CONSTRAINT [MemberCaseAudit_Fk_MemberCaseId] FOREIGN KEY (MemberCaseId) REFERENCES dbo.[MemberCase] (MemberCaseId)

  ) -- dbo.MemberCaseAudit

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'MemberCaseAuditId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'MemberCaseId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'MemberCaseAuditDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'AuditObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'AuditObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'UserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'SourceObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'SourceObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseAudit', @level2type=N'COLUMN', @level2name=N'CreateDate'

GO
*/ 

-- DBO.MEMBERCASEAUDIT ( END ) 


