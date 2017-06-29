-- EDI.[DRUGFORMULATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugFormulation')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugFormulation]
GO 
*/ 


CREATE TABLE edi.[DrugFormulation] (
    DrugListingId                                                             BIGINT  NOT NULL,
    Strength                                                                 VARCHAR (0030) NOT NULL,

    DrugUnitType                                                             VARCHAR (0020) NOT NULL,
    IngredientName                                                           VARCHAR (0060) NOT NULL    CONSTRAINT [DrugFormulation_Dft_IngredientName] DEFAULT ('')

  ) -- edi.DrugFormulation

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFormulation', @level2type=N'COLUMN', @level2name=N'DrugListingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFormulation', @level2type=N'COLUMN', @level2name=N'Strength'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFormulation', @level2type=N'COLUMN', @level2name=N'DrugUnitType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugFormulation', @level2type=N'COLUMN', @level2name=N'IngredientName'

GO
*/ 

-- EDI.DRUGFORMULATION ( END ) 


