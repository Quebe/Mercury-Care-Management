-- DBO.[LABRESULT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'LabResult')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[LabResult]
GO 
*/ 


CREATE TABLE dbo.[LabResult] (
    LabResultId                                                               BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    LabReferenceNumber                                                       VARCHAR (0020) NOT NULL    CONSTRAINT [LabResult_Dft_LabReferenceNumber] DEFAULT (''),
    MemberId                                                                  BIGINT  NOT NULL,
    ProviderId                                                                BIGINT      NULL,
    ClaimId                                                                   BIGINT      NULL,

    ServiceDate                                                             DATETIME  NOT NULL,
    ReportedDate                                                            DATETIME  NOT NULL    CONSTRAINT [LabResult_Dft_ReportedDate] DEFAULT (GETDATE ()),
    Loinc                                                                    VARCHAR (0007) NOT NULL    CONSTRAINT [LabResult_Dft_Loinc] DEFAULT (''),
    LabTestName                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_LabTestName] DEFAULT (''),

    LabValue                                                                 DECIMAL (20, 8)     NULL,
    LabUnitType                                                              VARCHAR (0020) NOT NULL    CONSTRAINT [LabResult_Dft_LabUnitType] DEFAULT (''),
    LabResultText                                                            VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_LabResultText] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [LabResult_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [LabResult_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [LabResult_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [LabResult_Pk] PRIMARY KEY (LabResultId)

  , CONSTRAINT [LabResult_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [LabResult_Fk_ProviderId] FOREIGN KEY (ProviderId) REFERENCES dbo.[Provider] (ProviderId)

  ) -- dbo.LabResult

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabResultId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabReferenceNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ServiceDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ReportedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOINC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'Loinc'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabTestName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabUnitType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'LabResultText'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'LabResult', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.LABRESULT ( END ) 


