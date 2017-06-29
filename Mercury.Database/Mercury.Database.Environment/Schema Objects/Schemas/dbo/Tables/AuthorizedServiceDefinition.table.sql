-- DBO.[AUTHORIZEDSERVICEDEFINITION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'AuthorizedServiceDefinition')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[AuthorizedServiceDefinition]
GO 
*/ 


CREATE TABLE dbo.[AuthorizedServiceDefinition] (
    AuthorizedServiceDefinitionId                                             BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    AuthorizedServiceId                                                       BIGINT  NOT NULL,

    Category                                                                 VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_Category] DEFAULT (''),
    Subcategory                                                              VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_Subcategory] DEFAULT (''),
    ServiceType                                                              VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ServiceType] DEFAULT (''),

    PrincipalDiagnosisCriteria                                               VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_PrincipalDiagnosisCriteria] DEFAULT (''),
    PrincipalDiagnosisVersion                                                    INT  NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_PrincipalDiagnosisVersion] DEFAULT (9),
    DiagnosisCriteria                                                        VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_DiagnosisCriteria] DEFAULT (''),
    DiagnosisVersion                                                             INT  NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_DiagnosisVersion] DEFAULT (9),

    DrgCriteria                                                              VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_DrgCriteria] DEFAULT (''),
    Icd9ProcedureCodeCriteria                                                VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_Icd9ProcedureCodeCriteria] DEFAULT (''),

    BillTypeCriteria                                                         VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_BillTypeCriteria] DEFAULT (''),
    LocationCodeCriteria                                                     VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_LocationCodeCriteria] DEFAULT (''),
    RevenueCodeCriteria                                                      VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_RevenueCodeCriteria] DEFAULT (''),
    ProcedureCodeCriteria                                                    VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ProcedureCodeCriteria] DEFAULT (''),
    ModifierCodeCriteria                                                     VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ModifierCodeCriteria] DEFAULT (''),

    ProviderSpecialtyCriteria                                                VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ProviderSpecialtyCriteria] DEFAULT (''),

    NdcCodeCriteria                                                          VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_NdcCodeCriteria] DEFAULT (''),
    DrugNameCriteria                                                         VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_DrugNameCriteria] DEFAULT (''),
    DeaClassificationCriteria                                                VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_DeaClassificationCriteria] DEFAULT (''),
    TherapeuticClassificationCriteria                                        VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_TherapeuticClassificationCriteria] DEFAULT (''),

    LabLoincCodeCriteria                                                     VARCHAR (0999) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_LabLoincCodeCriteria] DEFAULT (''),

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_Enabled] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [AuthorizedServiceDefinition_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [AuthorizedServiceDefinition_Pk] PRIMARY KEY (AuthorizedServiceDefinitionId)

  , CONSTRAINT [AuthorizedServiceDefinition_Fk_AuthorizedServiceId] FOREIGN KEY (AuthorizedServiceId) REFERENCES dbo.[AuthorizedService] (AuthorizedServiceId)

  ) -- dbo.AuthorizedServiceDefinition

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'AuthorizedServiceDefinitionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'AuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'Category'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'Subcategory'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ServiceType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence [(header)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'DiagnosisCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence [(header)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'DiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header) Institutional]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'DrgCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header) Institutional]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'Icd9ProcedureCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header) Institutional]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'BillTypeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(line) Professional]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'LocationCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(line) Institutional]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'RevenueCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(line) both]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ProcedureCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(line) both]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ModifierCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria [(header)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ProviderSpecialtyCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'NdcCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'DrugNameCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'DeaClassificationCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'TherapeuticClassificationCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Criteria', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'LabLoincCodeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizedServiceDefinition', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.AUTHORIZEDSERVICEDEFINITION ( END ) 


