-- DBO.[MEMBERCASECAREPLANASSESSMENTCAREMEASURE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseCarePlanAssessmentCareMeasure')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasure]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasure] (
    MemberCaseCarePlanAssessmentCareMeasureId                                 BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseCarePlanAssessmentId                                            BIGINT  NOT NULL,
    CareMeasureDomainId                                                       BIGINT  NOT NULL,
    CareMeasureClassId                                                        BIGINT  NOT NULL,
    CareMeasureId                                                             BIGINT  NOT NULL,
    TargetValue                                                              DECIMAL (20, 8) NOT NULL,
    ComponentValue                                                           DECIMAL (20, 8) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasure_Pk] PRIMARY KEY (MemberCaseCarePlanAssessmentCareMeasureId)

  ) -- dbo.MemberCaseCarePlanAssessmentCareMeasure

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentCareMeasureId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CareMeasureDomainId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CareMeasureClassId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CareMeasureId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'TargetValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'ComponentValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasure', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASECAREPLANASSESSMENTCAREMEASURE ( END ) 


