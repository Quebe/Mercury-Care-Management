-- DBO.[MEMBERCASECAREPLANASSESSMENTCAREMEASUREGOAL] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseCarePlanAssessmentCareMeasureGoal')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasureGoal]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseCarePlanAssessmentCareMeasureGoal] (
    MemberCaseCarePlanAssessmentCareMeasureGoalId                             BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseCarePlanAssessmentCareMeasureId                                 BIGINT  NOT NULL,
    MemberCaseCarePlanGoalId                                                  BIGINT  NOT NULL

  , CONSTRAINT [MemberCaseCarePlanAssessmentCareMeasureGoal_Pk] PRIMARY KEY (MemberCaseCarePlanAssessmentCareMeasureGoalId)

  ) -- dbo.MemberCaseCarePlanAssessmentCareMeasureGoal

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureGoal', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentCareMeasureGoalId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureGoal', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanAssessmentCareMeasureId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanAssessmentCareMeasureGoal', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanGoalId'

GO
*/ 

-- DBO.MEMBERCASECAREPLANASSESSMENTCAREMEASUREGOAL ( END ) 


