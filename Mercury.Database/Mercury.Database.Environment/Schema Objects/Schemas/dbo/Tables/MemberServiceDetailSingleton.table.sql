-- DBO.[MEMBERSERVICEDETAILSINGLETON] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberServiceDetailSingleton')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberServiceDetailSingleton]
GO 
*/ 


CREATE TABLE dbo.[MemberServiceDetailSingleton] (
    MemberServiceId                                                           BIGINT  NOT NULL,
    ServiceSingletonDefinitionId                                              BIGINT  NOT NULL,

    EventDate                                                               DATETIME  NOT NULL,

    ClaimId                                                                   BIGINT  NOT NULL,
    ExternalClaimId                                                          VARCHAR (0020) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ExternalClaimId] DEFAULT (''),
    ClaimLine                                                                    INT  NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ClaimLine] DEFAULT (0),

    MemberId                                                                  BIGINT  NOT NULL,
    ProviderId                                                                BIGINT  NOT NULL,

    ClaimType                                                                VARCHAR (0030) NOT NULL,
    ClaimStatus                                                              VARCHAR (0030) NOT NULL,
    ClaimDateFrom                                                           DATETIME  NOT NULL,
    ClaimDateThru                                                           DATETIME  NOT NULL,

    ServiceDateFrom                                                         DATETIME      NULL,
    ServiceDateThru                                                         DATETIME      NULL,

    AdmissionDate                                                           DATETIME      NULL,
    DischargeDate                                                           DATETIME      NULL,
    ReceivedDate                                                            DATETIME  NOT NULL,
    PaidDate                                                                DATETIME  NOT NULL,

    BillType                                                                 VARCHAR (0003) NOT NULL,
    PrincipalDiagnosisCode                                                   VARCHAR (0006) NOT NULL,
    PrincipalDiagnosisVersion                                                    INT  NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_PrincipalDiagnosisVersion] DEFAULT (9),
    DiagnosisCode                                                            VARCHAR (0006) NOT NULL,
    DiagnosisVersion                                                             INT  NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_DiagnosisVersion] DEFAULT (9),

    Icd9ProcedureCode                                                        VARCHAR (0006) NOT NULL,
    LocationCode                                                             VARCHAR (0002) NOT NULL,
    RevenueCode                                                              VARCHAR (0004) NOT NULL,
    ProcedureCode                                                            VARCHAR (0005) NOT NULL,
    ModifierCode                                                             VARCHAR (0002) NOT NULL,
    Units                                                                    DECIMAL (20, 8) NOT NULL,
    SpecialtyName                                                            VARCHAR (0060) NOT NULL,
    IsPcpClaim                                                                   BIT  NOT NULL,
    NdcCode                                                                     CHAR (0011) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_NdcCode] DEFAULT (''),
    DeaClassification                                                           CHAR (0001) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_DeaClassification] DEFAULT (''),
    TherapeuticClassification                                                VARCHAR (0006) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_TherapeuticClassification] DEFAULT (''),

    LabLoincCode                                                             VARCHAR (0007) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_LabLoincCode] DEFAULT (''),
    LabName                                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_LabName] DEFAULT (''),
    LabValue                                                                 DECIMAL (20, 8) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_LabValue] DEFAULT (0),
    ServiceDescription                                                       VARCHAR (0099) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ServiceDescription] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberServiceDetailSingleton_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberServiceDetailSingleton_Fk_MemberServiceId] FOREIGN KEY (MemberServiceId) REFERENCES dbo.[MemberService] (MemberServiceId)

  , CONSTRAINT [MemberServiceDetailSingleton_Fk_ServiceSingletonDefinitionId] FOREIGN KEY (ServiceSingletonDefinitionId) REFERENCES dbo.[ServiceSingletonDefinition] (ServiceSingletonDefinitionId)

  , CONSTRAINT [MemberServiceDetailSingleton_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  ) -- dbo.MemberServiceDetailSingleton

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'MemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ServiceSingletonDefinitionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ExternalClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Line Number', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimLine'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimDateFrom'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ClaimDateThru'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ServiceDateFrom'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ServiceDateThru'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'AdmissionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'DischargeDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ReceivedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'PaidDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Institutional Bill Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'BillType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'DiagnosisCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'DiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'Icd9ProcedureCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Location Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'LocationCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Revenue Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'RevenueCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPT4/HCPCS Procedure Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ProcedureCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ModifierCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Units Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'Units'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'SpecialtyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'IsPcpClaim'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'NdcCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DEA Classification', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'DeaClassification'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Therapeutic Classification', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'TherapeuticClassification'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LAB LOINC Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'LabLoincCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'LabName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'LabValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ServiceDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSingleton', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERSERVICEDETAILSINGLETON ( END ) 


