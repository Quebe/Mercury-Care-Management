-- DBO.[SERVICESINGLETONDEFINITION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ServiceSingletonDefinition')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ServiceSingletonDefinition]
GO 
*/ 


CREATE TABLE dbo.[ServiceSingletonDefinition] (
    ServiceSingletonDefinitionId                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ServiceId                                                                 BIGINT  NOT NULL,

    DataSourceType                                                               INT  NOT NULL,
    EventDateOrder                                                               INT  NOT NULL,

    PrincipalDiagnosisCriteria                                               VARCHAR (0999) NOT NULL,
    PrincipalDiagnosisVersion                                                    INT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_PrincipalDiagnosisVersion] DEFAULT (9),
    DiagnosisCriteria                                                        VARCHAR (0999) NOT NULL,
    DiagnosisVersion                                                             INT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_DiagnosisVersion] DEFAULT (9),

    DrgCriteria                                                              VARCHAR (0999) NOT NULL,
    Icd9ProcedureCodeCriteria                                                VARCHAR (0999) NOT NULL,
    BillTypeCriteria                                                         VARCHAR (0999) NOT NULL,
    LocationCodeCriteria                                                     VARCHAR (0999) NOT NULL,
    RevenueCodeCriteria                                                      VARCHAR (0999) NOT NULL,
    ProcedureCodeCriteria                                                    VARCHAR (0999) NOT NULL,
    ModifierCodeCriteria                                                     VARCHAR (0999) NOT NULL,

    ProviderSpecialtyCriteria                                                VARCHAR (0999) NOT NULL,
    IsPcpRequiredCriteria                                                        BIT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_IsPcpRequiredCriteria] DEFAULT (0),

    UseMemberAgeCriteria                                                         BIT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_UseMemberAgeCriteria] DEFAULT (0),
    MemberAgeDateQualifier                                                       INT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_MemberAgeDateQualifier] DEFAULT (0),
    MemberAgeMinimum                                                             INT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_MemberAgeMinimum] DEFAULT (0),
    MemberAgeMaximum                                                             INT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_MemberAgeMaximum] DEFAULT (0),

    NdcCodeCriteria                                                          VARCHAR (0999) NOT NULL,
    DrugNameCriteria                                                         VARCHAR (0999) NOT NULL,
    DeaClassificationCriteria                                                VARCHAR (0999) NOT NULL,
    TherapeuticClassificationCriteria                                        VARCHAR (0999) NOT NULL,

    LabLoincCodeCriteria                                                     VARCHAR (0999) NOT NULL,
    LabNameCriteria                                                          VARCHAR (0999) NOT NULL,
    LabValueExpressionCriteria                                               VARCHAR (0999) NOT NULL,
    LabMetricId                                                               BIGINT      NULL,

    CustomCriteria                                                           VARCHAR (8000) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_CustomCriteria] DEFAULT (''),

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_Enabled] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ServiceSingletonDefinition_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ServiceSingletonDefinition_Pk] PRIMARY KEY (ServiceSingletonDefinitionId)

  , CONSTRAINT [ServiceSingletonDefinition_Fk_ServiceId] FOREIGN KEY (ServiceId) REFERENCES dbo.[Service] (ServiceId)

  , CONSTRAINT [ServiceSingletonDefinition_Fk_LabMetricId] FOREIGN KEY (LabMetricId) REFERENCES dbo.[Metric] (MetricId)

  ) -- dbo.ServiceSingletonDefinition

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ServiceSingletonDefinitionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Parent Service]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DataSourceType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'EventDateOrder'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DiagnosisCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DrgCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'Icd9ProcedureCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'BillTypeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'LocationCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'RevenueCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ProcedureCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ModifierCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ProviderSpecialtyCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [Is Claim Required to be from PCP]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'IsPcpRequiredCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [Enable Member Age Constraints]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'UseMemberAgeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration [Age Constraints in (Months, Years, etc.)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'MemberAgeDateQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'MemberAgeMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'MemberAgeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'NdcCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DrugNameCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'DeaClassificationCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'TherapeuticClassificationCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'LabLoincCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'LabNameCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'LabValueExpressionCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'LabMetricId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Custom Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'CustomCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ServiceSingletonDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.SERVICESINGLETONDEFINITION ( END ) 


