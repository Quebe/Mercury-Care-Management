-- EDI.[DRUGPACKAGE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugPackage')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugPackage]
GO 
*/ 


CREATE TABLE edi.[DrugPackage] (
    DrugListingId                                                             BIGINT  NOT NULL,
    PackageCode                                                              VARCHAR (0020) NOT NULL,

    PackageSize                                                              VARCHAR (0060) NOT NULL,
    PackageType                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [DrugPackage_Dft_PackageType] DEFAULT ('')

  ) -- edi.DrugPackage

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugPackage', @level2type=N'COLUMN', @level2name=N'DrugListingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugPackage', @level2type=N'COLUMN', @level2name=N'PackageCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugPackage', @level2type=N'COLUMN', @level2name=N'PackageSize'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugPackage', @level2type=N'COLUMN', @level2name=N'PackageType'

GO
*/ 

-- EDI.DRUGPACKAGE ( END ) 


