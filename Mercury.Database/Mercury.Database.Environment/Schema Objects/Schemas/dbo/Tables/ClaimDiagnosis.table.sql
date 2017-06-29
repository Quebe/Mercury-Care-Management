-- DBO.[CLAIMDIAGNOSIS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ClaimDiagnosis')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ClaimDiagnosis]
GO 
*/ 


CREATE TABLE dbo.[ClaimDiagnosis] (
    ClaimId                                                                   BIGINT  NOT NULL,
    DiagnosisCode                                                            VARCHAR (0006) NOT NULL,
    DiagnosisVersion                                                             INT  NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_DiagnosisVersion] DEFAULT (9),
    DiagnosisType                                                                INT  NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_DiagnosisType] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ClaimDiagnosis_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ClaimDiagnosis_Pk] PRIMARY KEY (ClaimId, DiagnosisCode, DiagnosisVersion, DiagnosisType)

  , CONSTRAINT [ClaimDiagnosis_Fk_ClaimId] FOREIGN KEY (ClaimId) REFERENCES dbo.[Claim] (ClaimId)

  , CONSTRAINT [ClaimDiagnosis_Fk_DiagnosisType] FOREIGN KEY (DiagnosisType) REFERENCES enum.[DiagnosisType] (DiagnosisType)

  ) -- dbo.ClaimDiagnosis

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'ClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Diagnosis Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'DiagnosisCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'DiagnosisVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'DiagnosisType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimDiagnosis', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CLAIMDIAGNOSIS ( END ) 


