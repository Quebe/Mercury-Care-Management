-- DBO.[MEMBERAUTHORIZEDSERVICEDETAIL] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberAuthorizedServiceDetail')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberAuthorizedServiceDetail]
GO 
*/ 


CREATE TABLE dbo.[MemberAuthorizedServiceDetail] (
    MemberAuthorizedServiceId                                                 BIGINT  NOT NULL,
    AuthorizedServiceDefinitionId                                             BIGINT  NOT NULL,

    EventDate                                                               DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_EventDate] DEFAULT ('01/01/1900'),
    AuthorizationId                                                           BIGINT  NOT NULL,
    AuthorizationNumber                                                      VARCHAR (0020) NOT NULL,
    ExternalAuthorizationId                                                  VARCHAR (0020) NOT NULL,
    AuthorizationLine                                                            INT  NOT NULL,

    MemberId                                                                  BIGINT  NOT NULL,
    ReferringProviderId                                                       BIGINT  NOT NULL,
    ServiceProviderId                                                         BIGINT  NOT NULL,

    Category                                                                 VARCHAR (0060) NOT NULL,
    Subcategory                                                              VARCHAR (0060) NOT NULL,
    ServiceType                                                              VARCHAR (0060) NOT NULL,

    Status                                                                   VARCHAR (0020) NOT NULL,
    ReceivedDate                                                            DATETIME  NOT NULL,
    ReferralDate                                                            DATETIME  NOT NULL,
    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),
    ServiceDate                                                             DATETIME      NULL,

    PrincipalDiagnosisCode                                                   VARCHAR (0006) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_PrincipalDiagnosisCode] DEFAULT (''),
    PrincipalDiagnosisVersion                                                    INT  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_PrincipalDiagnosisVersion] DEFAULT (9),
    DiagnosisCode                                                            VARCHAR (0006) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_DiagnosisCode] DEFAULT (''),
    DiagnosisVersion                                                             INT  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_DiagnosisVersion] DEFAULT (9),

    RevenueCode                                                              VARCHAR (0004) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_RevenueCode] DEFAULT (''),
    ProcedureCode                                                            VARCHAR (0005) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ProcedureCode] DEFAULT (''),
    ModifierCode                                                             VARCHAR (0002) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ModifierCode] DEFAULT (''),
    SpecialtyName                                                            VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_SpecialtyName] DEFAULT (''),
    NdcCode                                                                     CHAR (0011) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_NdcCode] DEFAULT (''),
    Description                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_Description] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedServiceDetail_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberAuthorizedServiceDetail_Fk_MemberAuthorizedServiceId] FOREIGN KEY (MemberAuthorizedServiceId) REFERENCES dbo.[MemberAuthorizedService] (MemberAuthorizedServiceId)

  , CONSTRAINT [MemberAuthorizedServiceDetail_Fk_AuthorizedServiceDefinitionId] FOREIGN KEY (AuthorizedServiceDefinitionId) REFERENCES dbo.[AuthorizedServiceDefinition] (AuthorizedServiceDefinitionId)

  , CONSTRAINT [MemberAuthorizedServiceDetail_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  ) -- dbo.MemberAuthorizedServiceDetail

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'MemberAuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'AuthorizedServiceDefinitionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'AuthorizationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'AuthorizationNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ExternalAuthorizationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'AuthorizationLine'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ReferringProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ServiceProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'Category'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'Subcategory'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Authorization Type]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ServiceType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ReceivedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ReferralDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ServiceDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'PrincipalDiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'DiagnosisCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'DiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Revenue Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'RevenueCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPT4/HCPCS Procedure Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ProcedureCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ModifierCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'SpecialtyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'NdcCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [(reserved)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'Description'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedServiceDetail', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERAUTHORIZEDSERVICEDETAIL ( END ) 


