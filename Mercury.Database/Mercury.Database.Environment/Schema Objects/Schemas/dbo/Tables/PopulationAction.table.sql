-- DBO.[POPULATIONACTION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationAction')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationAction]
GO 
*/ 


CREATE TABLE dbo.[PopulationAction] (
    PopulationActionId                                                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationId                                                              BIGINT  NOT NULL,
    PopulationMembershipId                                                    BIGINT  NOT NULL,

    SenderObjectType                                                         VARCHAR (0120) NOT NULL    CONSTRAINT [PopulationAction_Dft_SenderObjectType] DEFAULT (''),
    SenderId                                                                  BIGINT  NOT NULL    CONSTRAINT [PopulationAction_Dft_SenderId] DEFAULT (0),
    EventInstanceId                                                           BIGINT  NOT NULL    CONSTRAINT [PopulationAction_Dft_EventInstanceId] DEFAULT (0),

    ScheduleDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationAction_Dft_ScheduleDate] DEFAULT (GETDATE ()),
    ActionDate                                                              DATETIME      NULL,

    ActionId                                                                  BIGINT  NOT NULL    CONSTRAINT [PopulationAction_Dft_ActionId] DEFAULT (0),
    ActionParameters                                                             XML  NOT NULL    CONSTRAINT [PopulationAction_Dft_ActionParameters] DEFAULT (''),
    ActionDescription                                                        VARCHAR (0099) NOT NULL    CONSTRAINT [PopulationAction_Dft_ActionDescription] DEFAULT (''),

    Exception                                                                VARCHAR (8000)     NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationAction_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationAction_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationAction_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationAction_Pk] PRIMARY KEY (PopulationActionId)

  , CONSTRAINT [PopulationAction_Fk_PopulationId] FOREIGN KEY (PopulationId) REFERENCES dbo.[Population] (PopulationId)

  , CONSTRAINT [PopulationAction_Fk_PopulationMembershipId] FOREIGN KEY (PopulationMembershipId) REFERENCES dbo.[PopulationMembership] (PopulationMembershipId)

  ) -- dbo.PopulationAction

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'PopulationActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'PopulationMembershipId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'SenderObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'SenderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'EventInstanceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ScheduleDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ActionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Exception Content', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'Exception'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationAction', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONACTION ( END ) 


