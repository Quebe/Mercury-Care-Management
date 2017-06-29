-- DBO.[WORKQUEUEITEM] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueItem')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueItem]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueItem] (
    WorkQueueItemId                                                           BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    WorkQueueId                                                               BIGINT  NOT NULL,

    ItemObjectType                                                           VARCHAR (0120) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ItemObjectType] DEFAULT (''),
    ItemObjectId                                                              BIGINT  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ItemObjectId] DEFAULT (0),
    WorkQueueItemName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkQueueItemName] DEFAULT (''),
    WorkQueueItemDescription                                                 VARCHAR (0099) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkQueueItemDescription] DEFAULT (''),
    ItemGroupKey                                                             VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ItemGroupKey] DEFAULT (''),
    WorkflowInstanceId                                              UNIQUEIDENTIFIER      NULL,
    WorkflowStatus                                                           VARCHAR (0020) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkflowStatus] DEFAULT (''),
    WorkflowLastStep                                                         VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkflowLastStep] DEFAULT (''),
    WorkflowNextStep                                                         VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkflowNextStep] DEFAULT (''),

    AddedDate                                                               DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_AddedDate] DEFAULT (GETDATE ()),
    LastWorkedDate                                                          DATETIME      NULL,
    ConstraintDate                                                          DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ConstraintDate] DEFAULT (GETDATE ()),
    MilestoneDate                                                           DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_MilestoneDate] DEFAULT (GETDATE ()),
    ThresholdDate                                                           DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ThresholdDate] DEFAULT (GETDATE ()),
    DueDate                                                                 DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_DueDate] DEFAULT (GETDATE ()),
    CompletionDate                                                          DATETIME      NULL,
    WorkOutcomeId                                                             BIGINT  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_WorkOutcomeId] DEFAULT (0),

    Priority                                                                     INT  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_Priority] DEFAULT (0),
    WorkTimeRestrictions                                                         XML      NULL,

    AssignedToSecurityAuthorityId                                             BIGINT  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_AssignedToSecurityAuthorityId] DEFAULT (0),
    AssignedToUserAccountId                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_AssignedToUserAccountId] DEFAULT (''),
    AssignedToUserAccountName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_AssignedToUserAccountName] DEFAULT (''),
    AssignedToUserDisplayName                                                VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_AssignedToUserDisplayName] DEFAULT (''),
    AssignedToDate                                                          DATETIME      NULL,

    ExtendedProperties                                                           XML      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueueItem_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueItem_Pk] PRIMARY KEY (WorkQueueItemId)

  , CONSTRAINT [WorkQueueItem_Fk_WorkQueueId] FOREIGN KEY (WorkQueueId) REFERENCES dbo.[WorkQueue] (WorkQueueId)

  , CONSTRAINT [WorkQueueItem_Fk_WorkOutcomeId] FOREIGN KEY (WorkOutcomeId) REFERENCES dbo.[WorkOutcome] (WorkOutcomeId)

  ) -- dbo.WorkQueueItem

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkQueueItemId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ItemObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ItemObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkQueueItemName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkQueueItemDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Associate Like Items by Same Key]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ItemGroupKey'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkflowInstanceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkflowStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkflowLastStep'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkflowNextStep'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Initial Date Item was Added]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AddedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Last Date/Time Item was Worked]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'LastWorkedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Date/Time that must Pass before Working.]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ConstraintDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'MilestoneDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Overall Warning Threshold]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ThresholdDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Overall Due Date of Item]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'DueDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Date/Time Completed]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'CompletionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Work Outcome Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkOutcomeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Sum of all Sender Priorities]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'Priority'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data [Limits when Item can be Worked]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'WorkTimeRestrictions'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AssignedToSecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AssignedToUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'AssignedToDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties [Used to Save State and Reporting]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItem', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUEITEM ( END ) 


