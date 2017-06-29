-- DBO.[CARELEVELACTIVITY] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'CareLevelActivity')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[CareLevelActivity]
GO 
*/ 


CREATE TABLE dbo.[CareLevelActivity] (
    CareLevelActivityId                                                       BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    CareLevelActivityName                                                    VARCHAR (0060) NOT NULL,
    CareLevelActivityDescription                                             VARCHAR (0999) NOT NULL,

    CareLevelId                                                               BIGINT  NOT NULL,

    ActivityType                                                                 INT  NOT NULL,
    IsReoccurring                                                                BIT  NOT NULL    CONSTRAINT [CareLevelActivity_Dft_IsReoccurring] DEFAULT (1),
    InitialAnchorDate                                                            INT  NOT NULL,
    AnchorDate                                                                   INT  NOT NULL,

    ScheduleType                                                                 INT  NOT NULL,
    ScheduleValue                                                                INT  NOT NULL,
    ScheduleQualifier                                                            INT  NOT NULL,
    ConstraintValue                                                              INT  NOT NULL,
    ConstraintQualifier                                                          INT  NOT NULL,

    PerformActionDate                                                            INT  NOT NULL,
    ActionId                                                                  BIGINT      NULL,
    ActionParameters                                                             XML      NULL,
    ActionDescription                                                        VARCHAR (0099)     NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [CareLevelActivity_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivity_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [CareLevelActivity_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [CareLevelActivity_Pk] PRIMARY KEY (CareLevelActivityId)

  , CONSTRAINT [CareLevelActivity_Fk_CareLevelId] FOREIGN KEY (CareLevelId) REFERENCES dbo.[CareLevel] (CareLevelId)

  ) -- dbo.CareLevelActivity

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CareLevelActivityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CareLevelActivityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CareLevelActivityDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CareLevelId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ActivityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'IsReoccurring'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'InitialAnchorDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'AnchorDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ScheduleType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ScheduleValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ScheduleQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ConstraintValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ConstraintQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'PerformActionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivity', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CARELEVELACTIVITY ( END ) 


