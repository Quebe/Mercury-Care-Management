-- DBO.[POPULATIONMEMBERSHIPACTIVITYEVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationMembershipActivityEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationMembershipActivityEvent]
GO 
*/ 


CREATE TABLE dbo.[PopulationMembershipActivityEvent] (
    PopulationMembershipActivityEventId                                       BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationMembershipId                                                    BIGINT  NOT NULL,
    PopulationActivityEventId                                                 BIGINT  NOT NULL,

    ActivityDate                                                            DATETIME      NULL,
    ActionDescription                                                        VARCHAR (0099) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_ActionDescription] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipActivityEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationMembershipActivityEvent_Pk] PRIMARY KEY (PopulationMembershipActivityEventId)

  , CONSTRAINT [PopulationMembershipActivityEvent_Fk_PopulationMembershipId] FOREIGN KEY (PopulationMembershipId) REFERENCES dbo.[PopulationMembership] (PopulationMembershipId)

  , CONSTRAINT [PopulationMembershipActivityEvent_Fk_PopulationActivityEventId] FOREIGN KEY (PopulationActivityEventId) REFERENCES dbo.[PopulationActivityEvent] (PopulationActivityEventId)

  ) -- dbo.PopulationMembershipActivityEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipActivityEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'PopulationActivityEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ActivityDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipActivityEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONMEMBERSHIPACTIVITYEVENT ( END ) 


