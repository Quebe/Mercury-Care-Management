-- DBO.[MEMBERCASE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCase')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCase]
GO 
*/ 


CREATE TABLE dbo.[MemberCase] (
    MemberCaseId                                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberId                                                                  BIGINT  NOT NULL,
    ReferenceNumber                                                          VARCHAR (0020) NOT NULL    CONSTRAINT [MemberCase_Dft_ReferenceNumber] DEFAULT (''),
    Status                                                                       INT  NOT NULL    CONSTRAINT [MemberCase_Dft_Status] DEFAULT (0),

    MemberCaseDescription                                                    VARCHAR (8000) NOT NULL    CONSTRAINT [MemberCase_Dft_MemberCaseDescription] DEFAULT (''),

    AssignedToWorkTeamId                                                      BIGINT      NULL,
    AssignedToWorkTeamDate                                                  DATETIME      NULL,

    AssignedToSecurityAuthorityId                                             BIGINT      NULL,
    AssignedToUserAccountId                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_AssignedToUserAccountId] DEFAULT (''),
    AssignedToUserAccountName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_AssignedToUserAccountName] DEFAULT (''),
    AssignedToUserDisplayName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_AssignedToUserDisplayName] DEFAULT (''),
    AssignedToDate                                                          DATETIME      NULL,

    LockedBySecurityAuthorityId                                               BIGINT      NULL,
    LockedByUserAccountId                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_LockedByUserAccountId] DEFAULT (''),
    LockedByUserAccountName                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_LockedByUserAccountName] DEFAULT (''),
    LockedByUserDisplayName                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_LockedByUserDisplayName] DEFAULT (''),
    LockedByDate                                                            DATETIME      NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberCase_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberCase_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    ExtendedProperties                                                           XML      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCase_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCase_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCase_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCase_Pk] PRIMARY KEY (MemberCaseId)

  , CONSTRAINT [MemberCase_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberCase_Fk_AssignedToWorkTeamId] FOREIGN KEY (AssignedToWorkTeamId) REFERENCES dbo.[WorkTeam] (WorkTeamId)

  ) -- dbo.MemberCase

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'MemberCaseId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ReferenceNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description (8000 characters)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'MemberCaseDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToWorkTeamId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToWorkTeamDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToSecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'AssignedToDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'LockedBySecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'LockedByUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'LockedByUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'LockedByUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'LockedByDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties [Used to Save State and Reporting]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCase', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASE ( END ) 


