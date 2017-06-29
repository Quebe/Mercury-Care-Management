-- DBO.[MEMBERENROLLMENTTPLCOB] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberEnrollmentTplCob')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberEnrollmentTplCob]
GO 
*/ 


CREATE TABLE dbo.[MemberEnrollmentTplCob] (
    MemberEnrollmentTplCobId                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberId                                                                  BIGINT  NOT NULL,
    SponsorId                                                                 BIGINT  NOT NULL,
    SubscriberId                                                              BIGINT  NOT NULL,
    InsurerId                                                                 BIGINT  NOT NULL,
    ProgramId                                                                 BIGINT  NOT NULL,
    ProgramMemberId                                                          VARCHAR (0020) NOT NULL,

    PayerResponsibilityType                                                      INT  NOT NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentTplCob_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberEnrollmentTplCob_Pk] PRIMARY KEY (MemberEnrollmentTplCobId)

  , CONSTRAINT [MemberEnrollmentTplCob_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberEnrollmentTplCob_Fk_SponsorId] FOREIGN KEY (SponsorId) REFERENCES dbo.[Sponsor] (SponsorId)

  , CONSTRAINT [MemberEnrollmentTplCob_Fk_SubscriberId] FOREIGN KEY (SubscriberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberEnrollmentTplCob_Fk_InsurerId] FOREIGN KEY (InsurerId) REFERENCES dbo.[Insurer] (InsurerId)

  , CONSTRAINT [MemberEnrollmentTplCob_Fk_ProgramId] FOREIGN KEY (ProgramId) REFERENCES dbo.[Program] (ProgramId)

  ) -- dbo.MemberEnrollmentTplCob

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentTplCobId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Member Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Sponsor Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'SponsorId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Subscriber Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'SubscriberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Insurer Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'InsurerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Program Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ProgramId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ProgramMemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Payer Responsibility Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'PayerResponsibilityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentTplCob', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERENROLLMENTTPLCOB ( END ) 


