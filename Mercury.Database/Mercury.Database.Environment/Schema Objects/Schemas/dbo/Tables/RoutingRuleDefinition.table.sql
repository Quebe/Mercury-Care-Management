-- DBO.[ROUTINGRULEDEFINITION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'RoutingRuleDefinition')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[RoutingRuleDefinition]
GO 
*/ 


CREATE TABLE dbo.[RoutingRuleDefinition] (
    RoutingRuleId                                                             BIGINT  NOT NULL,
    Sequence                                                                     INT  NOT NULL,
    InsurerId                                                                 BIGINT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_InsurerId] DEFAULT (0),
    ProgramId                                                                 BIGINT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ProgramId] DEFAULT (0),
    BenefitPlanId                                                             BIGINT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_BenefitPlanId] DEFAULT (0),

    Gender                                                                      CHAR (0001) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_Gender] DEFAULT (0),
    UseAgeCriteria                                                               BIT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_UseAgeCriteria] DEFAULT (0),
    AgeMinimum                                                                   INT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_AgeMinimum] DEFAULT (0),
    AgeMaximum                                                                   INT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_AgeMaximum] DEFAULT (0),
    IsAgeInMonths                                                                BIT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_IsAgeInMonths] DEFAULT (0),
    EthnicityId                                                               BIGINT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_EthnicityId] DEFAULT (0),
    LanguageId                                                                BIGINT  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_LanguageId] DEFAULT (0),

    State                                                                       CHAR (0002) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_State] DEFAULT (''),
    City                                                                     VARCHAR (0030) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_City] DEFAULT (''),
    County                                                                   VARCHAR (0030) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_County] DEFAULT (''),
    ZipCode                                                                     CHAR (0005) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ZipCode] DEFAULT (''),

    WorkQueueId                                                               BIGINT      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [RoutingRuleDefinition_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [RoutingRuleDefinition_Pk] PRIMARY KEY (RoutingRuleId, Sequence)

  , CONSTRAINT [RoutingRuleDefinition_Fk_RoutingRuleId] FOREIGN KEY (RoutingRuleId) REFERENCES dbo.[RoutingRule] (RoutingRuleId)

  , CONSTRAINT [RoutingRuleDefinition_Fk_WorkQueueId] FOREIGN KEY (WorkQueueId) REFERENCES dbo.[WorkQueue] (WorkQueueId)

  ) -- dbo.RoutingRuleDefinition

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'RoutingRuleId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'Sequence'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'InsurerId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ProgramId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'BenefitPlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender (M, F, or U)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'Gender'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'UseAgeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'AgeMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'AgeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'IsAgeInMonths'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'EthnicityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'LanguageId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address State', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'State'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'City'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'County', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'County'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ZipCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Work Queue Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'WorkQueueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RoutingRuleDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ROUTINGRULEDEFINITION ( END ) 


