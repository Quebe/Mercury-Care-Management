-- DBO.[MEMBERCASECAREPLANASSESSMENTCAREMEASURECOMPONENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseCarePlanAssessmentCareMeasureComponent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasureComponent]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasureComponent] (
    MemberCaseCarePlanAssessmentCareMeasureComponentId                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseCarePlanAssessmentCareMeasureId                                 BIGINT  NOT NULL,
    CareMeasureComponentId                                                    BIGINT  NOT NULL,

    CareMeasureComponentName                                                 VARCHAR (0099) NOT NULL,
    CareMeasureScaleId                                                        BIGINT  NOT NULL,
    Tag                                                                      VARCHAR (0020) NOT NULL,
    ComponentValue                                                           DECIMAL (20, 8) NOT NULL

  , CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasureComponent_Pk] PRIMARY KEY (MemberCaseCarePlanAssessmentCareMeasureComponentId)

  ) -- dbo.MemberCaseCarePlanAssessmentCareMeasureComponent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentCareMeasureComponentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentCareMeasureId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'CareMeasureComponentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'CareMeasureComponentName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'CareMeasureScaleId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'Tag'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureComponent', @level2type=N'COLUMN', @level2name=N'ComponentValue'

GO
*/ 

-- DBO.MEMBERCASECAREPLANASSESSMENTCAREMEASURECOMPONENT ( END ) 


