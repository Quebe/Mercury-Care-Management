-- DBO.[MEMBERENROLLMENTCOVERAGE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberEnrollmentCoverage')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberEnrollmentCoverage]
GO 
*/ 


CREATE TABLE dbo.[MemberEnrollmentCoverage] (
    MemberEnrollmentCoverageId                                                BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberEnrollmentId                                                        BIGINT  NOT NULL,

    BenefitPlanId                                                             BIGINT  NOT NULL,
    CoverageTypeId                                                            BIGINT  NOT NULL,
    CoverageLevelId                                                           BIGINT  NOT NULL,
    RateCode                                                                 VARCHAR (0020) NOT NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentCoverage_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberEnrollmentCoverage_Pk] PRIMARY KEY (MemberEnrollmentCoverageId)

  , CONSTRAINT [MemberEnrollmentCoverage_Fk_MemberEnrollmentId] FOREIGN KEY (MemberEnrollmentId) REFERENCES dbo.[MemberEnrollment] (MemberEnrollmentId)

  , CONSTRAINT [MemberEnrollmentCoverage_Fk_BenefitPlanId] FOREIGN KEY (BenefitPlanId) REFERENCES dbo.[BenefitPlan] (BenefitPlanId)

  , CONSTRAINT [MemberEnrollmentCoverage_Fk_CoverageTypeId] FOREIGN KEY (CoverageTypeId) REFERENCES dbo.[CoverageType] (CoverageTypeId)

  , CONSTRAINT [MemberEnrollmentCoverage_Fk_CoverageLevelId] FOREIGN KEY (CoverageLevelId) REFERENCES dbo.[CoverageLevel] (CoverageLevelId)

  ) -- dbo.MemberEnrollmentCoverage

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentCoverageId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Member Enrollment Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Benefit Plan Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'BenefitPlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CoverageTypeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CoverageLevelId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Rate Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'RateCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentCoverage', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERENROLLMENTCOVERAGE ( END ) 


