-- DBO.[MEMBERCASECAREPLANGOALINTERVENTION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseCarePlanGoalIntervention')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseCarePlanGoalIntervention]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseCarePlanGoalIntervention] (
    MemberCaseCarePlanGoalInterventionId                                      BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseCarePlanGoalId                                                  BIGINT  NOT NULL,
    MemberCaseCareInterventionId                                              BIGINT  NOT NULL,

    Inclusion                                                                    INT  NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_Inclusion] DEFAULT (0),
    IsSingleInstance                                                             BIT  NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_IsSingleInstance] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCaseCarePlanGoalIntervention_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseCarePlanGoalIntervention_Pk] PRIMARY KEY (MemberCaseCarePlanGoalInterventionId)

  , CONSTRAINT [MemberCaseCarePlanGoalIntervention_Fk_MemberCaseCarePlanGoalId] FOREIGN KEY (MemberCaseCarePlanGoalId) REFERENCES dbo.[MemberCaseCarePlanGoal] (MemberCaseCarePlanGoalId)

  ) -- dbo.MemberCaseCarePlanGoalIntervention

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanGoalInterventionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'MemberCaseCarePlanGoalId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'MemberCaseCareInterventionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'Inclusion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'IsSingleInstance'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCarePlanGoalIntervention', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASECAREPLANGOALINTERVENTION ( END ) 


