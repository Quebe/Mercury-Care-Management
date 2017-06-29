-- EDI.[NATIONALDRUGCODE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'NationalDrugCode')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[NationalDrugCode]
GO 
*/ 


CREATE TABLE edi.[NationalDrugCode] (
    NationalDrugCode                                                            CHAR (0011) NOT NULL,

    DrugFirmId                                                                  CHAR (0006) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_DrugFirmId] DEFAULT (''),
    DrugProductCode                                                             CHAR (0004) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_DrugProductCode] DEFAULT (''),
    DrugPackageCode                                                             CHAR (0002) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_DrugPackageCode] DEFAULT (''),

    DrugName                                                                 VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_DrugName] DEFAULT (''),

    Strength                                                                 VARCHAR (0010) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_Strength] DEFAULT (''),
    DrugUnitType                                                             VARCHAR (0020) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_DrugUnitType] DEFAULT (''),

    PackageSize                                                              VARCHAR (0030) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_PackageSize] DEFAULT (''),
    PackageType                                                              VARCHAR (0030) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_PackageType] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [NationalDrugCode_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [NationalDrugCode_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [NationalDrugCode_Dft_ModifiedDate] DEFAULT (GETDATE ())

  ) -- edi.NationalDrugCode

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Code', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'NationalDrugCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Firm Id (in character form)', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'DrugFirmId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Product Code', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'DrugProductCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Package Code', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'DrugPackageCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'DrugName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Drug Strength', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'Strength'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'DrugUnitType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'PackageSize'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'PackageType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'NationalDrugCode', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- EDI.NATIONALDRUGCODE ( END ) 


