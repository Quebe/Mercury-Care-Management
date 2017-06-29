-- DBO.[MEMBERENROLLMENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberEnrollment')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberEnrollment]
GO 
*/ 


CREATE TABLE dbo.[MemberEnrollment] (
    MemberEnrollmentId                                                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberId                                                                  BIGINT  NOT NULL,
    SponsorId                                                                 BIGINT  NOT NULL,
    SubscriberId                                                              BIGINT  NOT NULL,
    ProgramId                                                                 BIGINT  NOT NULL,
    ProgramMemberId                                                          VARCHAR (0020) NOT NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberEnrollment_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberEnrollment_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberEnrollment_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollment_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberEnrollment_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberEnrollment_Pk] PRIMARY KEY (MemberEnrollmentId)

  , CONSTRAINT [MemberEnrollment_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberEnrollment_Fk_SponsorId] FOREIGN KEY (SponsorId) REFERENCES dbo.[Sponsor] (SponsorId)

  , CONSTRAINT [MemberEnrollment_Fk_SubscriberId] FOREIGN KEY (SubscriberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberEnrollment_Fk_ProgramId] FOREIGN KEY (ProgramId) REFERENCES dbo.[Program] (ProgramId)

  ) -- dbo.MemberEnrollment

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Member Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Sponsor Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'SponsorId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Subscriber Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'SubscriberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Program Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ProgramId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ProgramMemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERENROLLMENT ( END ) 


