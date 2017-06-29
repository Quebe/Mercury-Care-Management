-- DBO.[MEMBERCASEPROBLEMCAREPLAN] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseProblemCarePlan')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseProblemCarePlan]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseProblemCarePlan] (
    MemberCaseProblemCarePlanId                                               BIGINT  NOT NULL    IDENTITY (1000000000, 1),

    MemberCaseProblemClassId                                                  BIGINT  NOT NULL,
    ProblemStatementId                                                        BIGINT  NOT NULL,
    MemberCaseCarePlanId                                                      BIGINT  NOT NULL,
    IsSingleInstance                                                             BIT  NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_IsSingleInstance] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCaseProblemCarePlan_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseProblemCarePlan_Pk] PRIMARY KEY (MemberCaseProblemCarePlanId)

  , CONSTRAINT [MemberCaseProblemCarePlan_Fk_MemberCaseProblemClassId] FOREIGN KEY (MemberCaseProblemClassId) REFERENCES dbo.[MemberCaseProblemClass] (MemberCaseProblemClassId)

  , CONSTRAINT [MemberCaseProblemCarePlan_Fk_ProblemStatementId] FOREIGN KEY (ProblemStatementId) REFERENCES dbo.[ProblemStatement] (ProblemStatementId)

  , CONSTRAINT [MemberCaseProblemCarePlan_Fk_MemberCaseCarePlanId] FOREIGN KEY (MemberCaseCarePlanId) REFERENCES dbo.[MemberCaseCarePlan] (MemberCaseCarePlanId)

  ) -- dbo.MemberCaseProblemCarePlan

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'MemberCaseProblemCarePlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'MemberCaseProblemClassId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'ProblemStatementId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'IsSingleInstance'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseProblemCarePlan', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASEPROBLEMCAREPLAN ( END ) 


