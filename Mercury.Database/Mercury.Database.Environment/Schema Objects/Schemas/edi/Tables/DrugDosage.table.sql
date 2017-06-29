-- EDI.[DRUGDOSAGE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugDosage')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugDosage]
GO 
*/ 


CREATE TABLE edi.[DrugDosage] (
    DrugListingId                                                             BIGINT  NOT NULL,
    DrugDosageForm                                                            BIGINT  NOT NULL,
    DrugDosageFormName                                                       VARCHAR (0060) NOT NULL

  ) -- edi.DrugDosage

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugDosage', @level2type=N'COLUMN', @level2name=N'DrugListingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugDosage', @level2type=N'COLUMN', @level2name=N'DrugDosageForm'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugDosage', @level2type=N'COLUMN', @level2name=N'DrugDosageFormName'

GO
*/ 

-- EDI.DRUGDOSAGE ( END ) 


