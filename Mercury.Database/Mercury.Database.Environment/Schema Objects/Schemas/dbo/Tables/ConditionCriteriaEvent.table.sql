-- DBO.[CONDITIONCRITERIAEVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ConditionCriteriaEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ConditionCriteriaEvent]
GO 
*/ 


CREATE TABLE dbo.[ConditionCriteriaEvent] (
    ConditionCriteriaEventId                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ConditionId                                                               BIGINT  NOT NULL,

    EventType                                                                    INT  NOT NULL,
    ServiceId                                                                 BIGINT  NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ConditionCriteriaEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ConditionCriteriaEvent_Pk] PRIMARY KEY (ConditionCriteriaEventId)

  , CONSTRAINT [ConditionCriteriaEvent_Fk_ConditionId] FOREIGN KEY (ConditionId) REFERENCES dbo.[Condition] (ConditionId)

  , CONSTRAINT [ConditionCriteriaEvent_Fk_ServiceId] FOREIGN KEY (ServiceId) REFERENCES dbo.[Service] (ServiceId)

  ) -- dbo.ConditionCriteriaEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ConditionCriteriaEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ConditionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'EventType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ConditionCriteriaEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CONDITIONCRITERIAEVENT ( END ) 


