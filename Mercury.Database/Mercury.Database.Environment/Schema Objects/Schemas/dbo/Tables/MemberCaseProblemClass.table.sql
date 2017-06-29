-- DBO.[MEMBERCASEPROBLEMCLASS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseProblemClass')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseProblemClass]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseProblemClass] (
    MemberCaseProblemClassId                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),

    MemberCaseId                                                              BIGINT  NOT NULL,
    ProblemClassId                                                            BIGINT  NOT NULL,

    AssignedToSecurityAuthorityId                                             BIGINT      NULL,
    AssignedToUserAccountId                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_AssignedToUserAccountId] DEFAULT (''),
    AssignedToUserAccountName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_AssignedToUserAccountName] DEFAULT (''),
    AssignedToUserDisplayName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_AssignedToUserDisplayName] DEFAULT (''),
    AssignedToDate                                                          DATETIME      NULL,

    AssignedToProviderId                                                      BIGINT      NULL,
    AssignedToProviderDate                                                  DATETIME      NULL,

    ExtendedProperties                                                           XML      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCaseProblemClass_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseProblemClass_Pk] PRIMARY KEY (MemberCaseProblemClassId)

  , CONSTRAINT [MemberCaseProblemClass_Fk_MemberCaseId] FOREIGN KEY (MemberCaseId) REFERENCES dbo.[MemberCase] (MemberCaseId)

  , CONSTRAINT [MemberCaseProblemClass_Fk_ProblemClassId] FOREIGN KEY (ProblemClassId) REFERENCES dbo.[ProblemClass] (ProblemClassId)

  , CONSTRAINT [MemberCaseProblemClass_Fk_AssignedToProviderId] FOREIGN KEY (AssignedToProviderId) REFERENCES dbo.[Provider] (ProviderId)

  ) -- dbo.MemberCaseProblemClass

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'MemberCaseProblemClassId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'MemberCaseId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ProblemClassId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToSecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'AssignedToProviderDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemClass', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASEPROBLEMCLASS ( END ) 


