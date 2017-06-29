-- DBO.[POPULATIONMEMBERSHIPTRIGGEREVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationMembershipTriggerEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationMembershipTriggerEvent]
GO 
*/ 


CREATE TABLE dbo.[PopulationMembershipTriggerEvent] (
    PopulationMembershipTriggerEventId                                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationMembershipId                                                    BIGINT  NOT NULL,
    PopulationTriggerEventId                                                  BIGINT  NOT NULL,

    TriggerDate                                                             DATETIME  NOT NULL,
    EventDate                                                               DATETIME  NOT NULL,

    MemberServiceId                                                           BIGINT      NULL,
    MemberMetricId                                                            BIGINT      NULL,
    MemberAuthorizedServiceId                                                 BIGINT      NULL,

    ProblemStatementId                                                        BIGINT  NOT NULL,
    ActionDescription                                                        VARCHAR (0099) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipTriggerEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationMembershipTriggerEvent_Pk] PRIMARY KEY (PopulationMembershipTriggerEventId)

  , CONSTRAINT [PopulationMembershipTriggerEvent_Fk_PopulationMembershipId] FOREIGN KEY (PopulationMembershipId) REFERENCES dbo.[PopulationMembership] (PopulationMembershipId)

  , CONSTRAINT [PopulationMembershipTriggerEvent_Fk_PopulationTriggerEventId] FOREIGN KEY (PopulationTriggerEventId) REFERENCES dbo.[PopulationTriggerEvent] (PopulationTriggerEventId)

  , CONSTRAINT [PopulationMembershipTriggerEvent_Fk_MemberServiceId] FOREIGN KEY (MemberServiceId) REFERENCES dbo.[MemberService] (MemberServiceId)

  , CONSTRAINT [PopulationMembershipTriggerEvent_Fk_MemberMetricId] FOREIGN KEY (MemberMetricId) REFERENCES dbo.[MemberMetric] (MemberMetricId)

  , CONSTRAINT [PopulationMembershipTriggerEvent_Fk_MemberAuthorizedServiceId] FOREIGN KEY (MemberAuthorizedServiceId) REFERENCES dbo.[MemberAuthorizedService] (MemberAuthorizedServiceId)

  ) -- dbo.PopulationMembershipTriggerEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipTriggerEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'PopulationTriggerEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'TriggerDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'MemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'MemberMetricId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'MemberAuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ProblemStatementId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipTriggerEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONMEMBERSHIPTRIGGEREVENT ( END ) 


