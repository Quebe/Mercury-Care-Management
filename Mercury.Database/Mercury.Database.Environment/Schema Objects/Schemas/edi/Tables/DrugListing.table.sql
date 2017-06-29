-- EDI.[DRUGLISTING] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugListing')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugListing]
GO 
*/ 


CREATE TABLE edi.[DrugListing] (
    DrugListingId                                                             BIGINT  NOT NULL,
    DrugFirmId                                                               VARCHAR (0020) NOT NULL,
    ProductCode                                                              VARCHAR (0020) NOT NULL,

    Strength                                                                 VARCHAR (0030) NOT NULL    CONSTRAINT [DrugListing_Dft_Strength] DEFAULT (''),
    DrugUnitType                                                             VARCHAR (0020) NOT NULL    CONSTRAINT [DrugListing_Dft_DrugUnitType] DEFAULT (''),

    ProductName                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [DrugListing_Dft_ProductName] DEFAULT ('')

  ) -- edi.DrugListing

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'DrugListingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'DrugFirmId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'ProductCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'Strength'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'DrugUnitType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugListing', @level2type=N'COLUMN', @level2name=N'ProductName'

GO
*/ 

-- EDI.DRUGLISTING ( END ) 


