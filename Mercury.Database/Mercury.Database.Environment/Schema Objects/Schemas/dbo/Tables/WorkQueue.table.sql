-- DBO.[WORKQUEUE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueue')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueue]
GO 
*/ 


CREATE TABLE dbo.[WorkQueue] (
    WorkQueueId                                                               BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    WorkQueueName                                                            VARCHAR (0060) NOT NULL,
    WorkQueueDescription                                                     VARCHAR (0999) NOT NULL,

    WorkflowId                                                                BIGINT      NULL,

    ScheduleValue                                                                INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_ScheduleValue] DEFAULT (0),
    ScheduleQualifier                                                            INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_ScheduleQualifier] DEFAULT (0),

    ThresholdValue                                                               INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_ThresholdValue] DEFAULT (0),
    ThresholdQualifier                                                           INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_ThresholdQualifier] DEFAULT (0),
    InitialConstraintValue                                                       INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_InitialConstraintValue] DEFAULT (0),

    InitialConstraintQualifier                                                   INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_InitialConstraintQualifier] DEFAULT (0),

    InitialMilestoneValue                                                        INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_InitialMilestoneValue] DEFAULT (0),
    InitialMilestoneQualifier                                                    INT  NOT NULL    CONSTRAINT [WorkQueue_Dft_InitialMilestoneQualifier] DEFAULT (0),

    GetWorkViewId                                                             BIGINT  NOT NULL    CONSTRAINT [WorkQueue_Dft_GetWorkViewId] DEFAULT (0),
    GetWorkUseGrouping                                                           BIT  NOT NULL    CONSTRAINT [WorkQueue_Dft_GetWorkUseGrouping] DEFAULT (0),

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [WorkQueue_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [WorkQueue_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueue_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueue_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueue_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueue_Pk] PRIMARY KEY (WorkQueueId)

  , CONSTRAINT [WorkQueue_Fk_WorkflowId] FOREIGN KEY (WorkflowId) REFERENCES dbo.[Workflow] (WorkflowId)

  , CONSTRAINT [WorkQueue_Fk_GetWorkViewId] FOREIGN KEY (GetWorkViewId) REFERENCES dbo.[WorkQueueView] (WorkQueueViewId)

  , CONSTRAINT [WorkQueue_Unq_WorkQueueName] UNIQUE (WorkQueueName)

  ) -- dbo.WorkQueue

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'WorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'WorkQueueName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'WorkQueueDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Unassigned is Manual Only Work Queue]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'WorkflowId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ScheduleValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ScheduleQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ThresholdValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ThresholdQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'InitialConstraintValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'InitialConstraintQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'InitialMilestoneValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Schedule Qualifier (Days, Months, Years)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'InitialMilestoneQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Work Queue View for Get Work Function]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'GetWorkViewId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [Allow Get Work to Group by Item Group Key]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'GetWorkUseGrouping'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueue', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUE ( END ) 


