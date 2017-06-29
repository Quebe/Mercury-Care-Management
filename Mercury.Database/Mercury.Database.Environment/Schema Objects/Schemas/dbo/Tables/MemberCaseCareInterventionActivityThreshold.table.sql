-- DBO.[MEMBERCASECAREINTERVENTIONACTIVITYTHRESHOLD] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberCaseCareInterventionActivityThreshold')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberCaseCareInterventionActivityThreshold]
GO 
*/ 


CREATE TABLE dbo.[MemberCaseCareInterventionActivityThreshold] (
    MemberCaseCareInterventionActivityThresholdId                             BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberCaseCareInterventionActivityThresholdName                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_MemberCaseCareInterventionActivityThresholdName] DEFAULT (''),
    MemberCaseCareInterventionActivityThresholdDescription                   VARCHAR (0999) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_MemberCaseCareInterventionActivityThresholdDescription] DEFAULT (''),

    MemberCaseCareInterventionActivityId                                      BIGINT  NOT NULL,
    CareInterventionActivityId                                                BIGINT      NULL,

    RelativeDateValue                                                            INT  NOT NULL,
    RelativeDateQualifier                                                        INT  NOT NULL,

    Status                                                                       INT  NOT NULL,

    ActionId                                                                  BIGINT      NULL,
    ActionParameters                                                             XML      NULL,
    ActionDescription                                                        VARCHAR (0099)     NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Pk] PRIMARY KEY (MemberCaseCareInterventionActivityThresholdId)

  , CONSTRAINT [MemberCaseCareInterventionActivityThreshold_Fk_MemberCaseCareInterventionActivityId] FOREIGN KEY (MemberCaseCareInterventionActivityId) REFERENCES dbo.[CareInterventionActivity] (CareInterventionActivityId)

  ) -- dbo.MemberCaseCareInterventionActivityThreshold

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'MemberCaseCareInterventionActivityThresholdId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'MemberCaseCareInterventionActivityThresholdName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'MemberCaseCareInterventionActivityThresholdDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'MemberCaseCareInterventionActivityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'CareInterventionActivityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Integer Date Value (used w/ Date Qualifier)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'RelativeDateValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'RelativeDateQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberCaseCareInterventionActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERCASECAREINTERVENTIONACTIVITYTHRESHOLD ( END ) 


