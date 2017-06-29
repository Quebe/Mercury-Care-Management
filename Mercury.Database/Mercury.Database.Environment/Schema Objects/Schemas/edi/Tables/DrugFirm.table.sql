-- EDI.[DRUGFIRM] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugFirm')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugFirm]
GO 
*/ 


CREATE TABLE edi.[DrugFirm] (
    DrugFirmId                                                               VARCHAR (0020) NOT NULL,
    DrugFirmName                                                             VARCHAR (0060) NOT NULL,

    AddressLine1                                                             VARCHAR (0055) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressLine1] DEFAULT (''),
    AddressLine2                                                             VARCHAR (0055) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressLine2] DEFAULT (''),
    AddressCity                                                              VARCHAR (0030) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressCity] DEFAULT (''),
    AddressState                                                                CHAR (0002) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressState] DEFAULT (''),
    AddressZipCode                                                              CHAR (0005) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressZipCode] DEFAULT (''),
    AddressZipPlus4                                                             CHAR (0004) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressZipPlus4] DEFAULT (''),
    AddressPostalCode                                                           CHAR (0015) NOT NULL    CONSTRAINT [DrugFirm_Dft_AddressPostalCode] DEFAULT (''),
    ProvinceName                                                             VARCHAR (0060) NOT NULL    CONSTRAINT [DrugFirm_Dft_ProvinceName] DEFAULT (''),
    CountryName                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [DrugFirm_Dft_CountryName] DEFAULT ('')

  ) -- edi.DrugFirm

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'DrugFirmId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'DrugFirmName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressLine1'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressLine2'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address City', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressCity'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address State', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressState'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressZipCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code Plus 4', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressZipPlus4'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(reserved)', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'AddressPostalCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'ProvinceName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFirm', @level2type=N'COLUMN', @level2name=N'CountryName'

GO
*/ 

-- EDI.DRUGFIRM ( END ) 


