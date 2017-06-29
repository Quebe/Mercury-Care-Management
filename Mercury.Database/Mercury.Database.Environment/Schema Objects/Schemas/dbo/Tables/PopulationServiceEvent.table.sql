-- DBO.[POPULATIONSERVICEEVENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationServiceEvent')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationServiceEvent]
GO 
*/ 


CREATE TABLE dbo.[PopulationServiceEvent] (
    PopulationServiceEventId                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationId                                                              BIGINT  NOT NULL,
    ServiceId                                                                 BIGINT  NOT NULL,
    ExclusionServiceId                                                        BIGINT      NULL,

    AnchorDate                                                                   INT  NOT NULL,
    AnchorDateValue                                                              INT  NOT NULL,

    ScheduleDateValue                                                            INT  NOT NULL,
    ScheduleDateQualifier                                                        INT  NOT NULL,

    IsReoccurring                                                                BIT  NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationServiceEvent_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationServiceEvent_Pk] PRIMARY KEY (PopulationServiceEventId)

  , CONSTRAINT [PopulationServiceEvent_Fk_PopulationId] FOREIGN KEY (PopulationId) REFERENCES dbo.[Population] (PopulationId)

  , CONSTRAINT [PopulationServiceEvent_Fk_ServiceId] FOREIGN KEY (ServiceId) REFERENCES dbo.[Service] (ServiceId)

  , CONSTRAINT [PopulationServiceEvent_Fk_ExclusionServiceId] FOREIGN KEY (ExclusionServiceId) REFERENCES dbo.[Service] (ServiceId)

  , CONSTRAINT [PopulationServiceEvent_Fk_AnchorDate] FOREIGN KEY (AnchorDate) REFERENCES enum.[PopulationServiceEventAnchorDate] (AnchorDate)

  , CONSTRAINT [PopulationServiceEvent_Fk_ScheduleDateQualifier] FOREIGN KEY (ScheduleDateQualifier) REFERENCES enum.[DateQualifier] (DateQualifier)

  ) -- dbo.PopulationServiceEvent

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'PopulationServiceEventId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ExclusionServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'AnchorDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Integer Date Value (used w/ Date Qualifier)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'AnchorDateValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Integer Date Value (used w/ Date Qualifier)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ScheduleDateValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ScheduleDateQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'IsReoccurring'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationServiceEvent', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONSERVICEEVENT ( END ) 


