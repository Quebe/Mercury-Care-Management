-- DBO.[POPULATIONTRIGGEREVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationTriggerEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationTriggerEvent]
GO 
*/ 


CREATE TABLE dbo.[PopulationTriggerEvent] (
    PopulationTriggerEventId                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationId                                                              BIGINT  NOT NULL,
    TriggerEventType                                                             INT  NOT NULL,

    ServiceId                                                                 BIGINT      NULL,

    MetricType                                                                   INT  NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_MetricType] DEFAULT (0),
    MetricId                                                                  BIGINT      NULL,
    MetricMinimum                                                            DECIMAL (20, 8) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_MetricMinimum] DEFAULT (0),
    MetricMaximum                                                            DECIMAL (20, 8) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_MetricMaximum] DEFAULT (0),

    AuthorizedServiceId                                                       BIGINT      NULL,

    CompositeTriggerId                                                        BIGINT      NULL,

    ProblemStatementId                                                        BIGINT      NULL,

    ActionId                                                                  BIGINT  NOT NULL,
    ActionParameters                                                             XML  NOT NULL,
    ActionDescription                                                        VARCHAR (0099) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_ActionDescription] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationTriggerEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationTriggerEvent_Pk] PRIMARY KEY (PopulationTriggerEventId)

  , CONSTRAINT [PopulationTriggerEvent_Fk_PopulationId] FOREIGN KEY (PopulationId) REFERENCES dbo.[Population] (PopulationId)

  , CONSTRAINT [PopulationTriggerEvent_Fk_ServiceId] FOREIGN KEY (ServiceId) REFERENCES dbo.[Service] (ServiceId)

  , CONSTRAINT [PopulationTriggerEvent_Fk_MetricId] FOREIGN KEY (MetricId) REFERENCES dbo.[Metric] (MetricId)

  , CONSTRAINT [PopulationTriggerEvent_Fk_AuthorizedServiceId] FOREIGN KEY (AuthorizedServiceId) REFERENCES dbo.[AuthorizedService] (AuthorizedServiceId)

  , CONSTRAINT [PopulationTriggerEvent_Fk_ProblemStatementId] FOREIGN KEY (ProblemStatementId) REFERENCES dbo.[ProblemStatement] (ProblemStatementId)

  ) -- dbo.PopulationTriggerEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'PopulationTriggerEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'TriggerEventType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'MetricType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'MetricId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'MetricMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'MetricMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'AuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [(reserved)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'CompositeTriggerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ProblemStatementId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONTRIGGEREVENT ( END ) 


