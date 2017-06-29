-- DBO.[WORKQUEUEGETWORKUSERVIEW] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkQueueGetWorkUserView')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkQueueGetWorkUserView]
GO 
*/ 


CREATE TABLE dbo.[WorkQueueGetWorkUserView] (
    WorkQueueId                                                               BIGINT  NOT NULL,
    SecurityAuthorityId                                                       BIGINT  NOT NULL,
    UserAccountId                                                            VARCHAR (0060) NOT NULL,
    UserAccountName                                                          VARCHAR (0060) NOT NULL,
    UserDisplayName                                                          VARCHAR (0060) NOT NULL,
    WorkQueueViewId                                                           BIGINT  NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkQueueGetWorkUserView_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkQueueGetWorkUserView_Pk] PRIMARY KEY (WorkQueueId, SecurityAuthorityId, UserAccountId)

  , CONSTRAINT [WorkQueueGetWorkUserView_Fk_WorkQueueId] FOREIGN KEY (WorkQueueId) REFERENCES dbo.[WorkQueue] (WorkQueueId)

  , CONSTRAINT [WorkQueueGetWorkUserView_Fk_WorkQueueViewId] FOREIGN KEY (WorkQueueViewId) REFERENCES dbo.[WorkQueueView] (WorkQueueViewId)

  ) -- dbo.WorkQueueGetWorkUserView

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'WorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [From Enterprise Security Authority]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'UserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'UserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'UserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'WorkQueueViewId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkQueueGetWorkUserView', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKQUEUEGETWORKUSERVIEW ( END ) 


