-- DBO.[WORKQUEUEITEMWORKFLOWSTEP] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueItemWorkflowStep')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueItemWorkflowStep]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueItemWorkflowStep] (
    WorkQueueItemId                                                           BIGINT  NOT NULL,
    StepSequence                                                                 INT  NOT NULL,
    StepDate                                                                DATETIME  NOT NULL,

    StepStatus                                                                   INT  NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_StepStatus] DEFAULT (0),
    StepName                                                                 VARCHAR (0060) NOT NULL,
    StepDescription                                                          VARCHAR (0999) NOT NULL,

    UserDisplayName                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_UserDisplayName] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueItemWorkflowStep_Dft_CreateDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueItemWorkflowStep_Pk] PRIMARY KEY (WorkQueueItemId, StepSequence)

  , CONSTRAINT [WorkQueueItemWorkflowStep_Fk_WorkQueueItemId] FOREIGN KEY (WorkQueueItemId) REFERENCES dbo.[WorkQueueItem] (WorkQueueItemId)

  ) -- dbo.WorkQueueItemWorkflowStep

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'WorkQueueItemId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'StepSequence'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'StepDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type ["Informational", "Warning", or "Critical"]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'StepStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'StepName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'StepDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'UserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemWorkflowStep', @level2type=N'COLUMN', @level2name=N'CreateDate'

GO
*/ 

-- DBO.WORKQUEUEITEMWORKFLOWSTEP ( END ) 


