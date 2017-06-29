-- DBO.[CAREPLANGOAL] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'CarePlanGoal')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[CarePlanGoal]
GO 
*/ 


CREATE TABLE dbo.[CarePlanGoal] (
    CarePlanGoalId                                                            BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    CarePlanGoalName                                                         VARCHAR (0060) NOT NULL,
    CareplanGoalDescription                                                  VARCHAR (0999) NOT NULL,
    CarePlanId                                                                BIGINT  NOT NULL,

    Inclusion                                                                    INT  NOT NULL    CONSTRAINT [CarePlanGoal_Dft_Inclusion] DEFAULT (3),
    ClinicalNarrative                                                        VARCHAR (0999) NOT NULL,
    CommonNarrative                                                          VARCHAR (0999) NOT NULL,

    GoalTimeframe                                                                INT  NOT NULL,
    ScheduleValue                                                                INT  NOT NULL,
    ScheduleQualifier                                                            INT  NOT NULL,

    CareMeasureId                                                             BIGINT  NOT NULL,

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [CarePlanGoal_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [CarePlanGoal_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [CarePlanGoal_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CarePlanGoal_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [CarePlanGoal_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [CarePlanGoal_Pk] PRIMARY KEY (CarePlanGoalId)

  , CONSTRAINT [CarePlanGoal_Fk_CarePlanId] FOREIGN KEY (CarePlanId) REFERENCES dbo.[CarePlan] (CarePlanId)

  , CONSTRAINT [CarePlanGoal_Fk_CareMeasureId] FOREIGN KEY (CareMeasureId) REFERENCES dbo.[CareMeasure] (CareMeasureId)

  ) -- dbo.CarePlanGoal

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CarePlanGoalId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CarePlanGoalName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CareplanGoalDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CarePlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'Inclusion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ClinicalNarrative'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CommonNarrative'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'GoalTimeframe'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ScheduleValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ScheduleQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CareMeasureId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CarePlanGoal', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CAREPLANGOAL ( END ) 


