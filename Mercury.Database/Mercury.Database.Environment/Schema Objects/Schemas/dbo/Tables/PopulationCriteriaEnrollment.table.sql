-- DBO.[POPULATIONCRITERIAENROLLMENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationCriteriaEnrollment')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationCriteriaEnrollment]
GO 
*/ 


CREATE TABLE dbo.[PopulationCriteriaEnrollment] (
    PopulationCriteriaEnrollmentId                                            BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationId                                                              BIGINT  NOT NULL,

    InsurerId                                                                 BIGINT  NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_InsurerId] DEFAULT (0),
    ProgramId                                                                 BIGINT  NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_ProgramId] DEFAULT (0),
    BenefitPlanId                                                             BIGINT  NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_BenefitPlanId] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationCriteriaEnrollment_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationCriteriaEnrollment_Pk] PRIMARY KEY (PopulationCriteriaEnrollmentId)

  , CONSTRAINT [PopulationCriteriaEnrollment_Fk_PopulationId] FOREIGN KEY (PopulationId) REFERENCES dbo.[Population] (PopulationId)

  ) -- dbo.PopulationCriteriaEnrollment

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'PopulationCriteriaEnrollmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'InsurerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'ProgramId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'BenefitPlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaEnrollment', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONCRITERIAENROLLMENT ( END ) 


