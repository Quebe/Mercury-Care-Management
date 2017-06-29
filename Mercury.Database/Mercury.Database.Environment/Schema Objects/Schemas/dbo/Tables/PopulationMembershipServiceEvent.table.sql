-- DBO.[POPULATIONMEMBERSHIPSERVICEEVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationMembershipServiceEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationMembershipServiceEvent]
GO 
*/ 


CREATE TABLE dbo.[PopulationMembershipServiceEvent] (
    PopulationMembershipServiceEventId                                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationMembershipId                                                    BIGINT  NOT NULL,
    PopulationServiceEventId                                                  BIGINT  NOT NULL,

    ExpectedEventDate                                                       DATETIME  NOT NULL,
    MemberServiceId                                                           BIGINT      NULL,
    EventDate                                                               DATETIME      NULL,

    PreviousMemberServiceId                                                   BIGINT      NULL,
    PreviousEventDate                                                       DATETIME      NULL,

    ParentPopulationMembershipServiceEventId                                  BIGINT      NULL,
    ParentPopulationMembershipServiceEventDate                              DATETIME      NULL,

    PreviousThresholdId                                                       BIGINT      NULL,
    PreviousThresholdDate                                                   DATETIME      NULL,
    NextThresholdDate                                                       DATETIME      NULL,

    Status                                                                       INT  NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationMembershipServiceEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationMembershipServiceEvent_Pk] PRIMARY KEY (PopulationMembershipServiceEventId)

  , CONSTRAINT [PopulationMembershipServiceEvent_Fk_PopulationMembershipId] FOREIGN KEY (PopulationMembershipId) REFERENCES dbo.[PopulationMembership] (PopulationMembershipId)

  , CONSTRAINT [PopulationMembershipServiceEvent_Fk_PreviousMemberServiceId] FOREIGN KEY (PreviousMemberServiceId) REFERENCES dbo.[MemberService] (MemberServiceId)

  , CONSTRAINT [PopulationMembershipServiceEvent_Fk_Status] FOREIGN KEY (Status) REFERENCES enum.[ServiceEventStatus] (Status)

  ) -- dbo.PopulationMembershipServiceEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipServiceEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PopulationMembershipId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PopulationServiceEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ExpectedEventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'MemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PreviousMemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PreviousEventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ParentPopulationMembershipServiceEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ParentPopulationMembershipServiceEventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PreviousThresholdId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'PreviousThresholdDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'NextThresholdDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationMembershipServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONMEMBERSHIPSERVICEEVENT ( END ) 


