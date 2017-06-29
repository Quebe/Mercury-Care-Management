-- DBO.[WORKQUEUEITEMASSIGNMENTHISTORY] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueItemAssignmentHistory')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueItemAssignmentHistory]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueItemAssignmentHistory] (
    WorkQueueItemAssignmentHistoryId                                          BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    WorkQueueItemId                                                           BIGINT  NOT NULL,
    AssignedFromWorkQueueId                                                   BIGINT  NOT NULL,
    AssignedToWorkQueueId                                                     BIGINT  NOT NULL,

    AssignedToSecurityAuthorityId                                             BIGINT  NOT NULL,
    AssignedToUserAccountId                                                  VARCHAR (0060) NOT NULL,
    AssignedToUserAccountName                                                VARCHAR (0060) NOT NULL,
    AssignedToUserDisplayName                                                VARCHAR (0060) NOT NULL,
    AssignedToDate                                                          DATETIME  NOT NULL,

    AssignmentSource                                                         VARCHAR (0060) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueueItemAssignmentHistory_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueItemAssignmentHistory_Pk] PRIMARY KEY (WorkQueueItemAssignmentHistoryId)

  , CONSTRAINT [WorkQueueItemAssignmentHistory_Fk_WorkQueueItemId] FOREIGN KEY (WorkQueueItemId) REFERENCES dbo.[WorkQueueItem] (WorkQueueItemId)

  ) -- dbo.WorkQueueItemAssignmentHistory

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'WorkQueueItemAssignmentHistoryId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'WorkQueueItemId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Item moved from Queue.]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedFromWorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Item moved to Queue.]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToWorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToSecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToUserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToUserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignedToDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [How/Where the assignment was made.]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'AssignmentSource'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueItemAssignmentHistory', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUEITEMASSIGNMENTHISTORY ( END ) 


