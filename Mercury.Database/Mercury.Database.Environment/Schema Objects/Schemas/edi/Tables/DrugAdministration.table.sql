-- EDI.[DRUGADMINISTRATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DrugAdministration')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[DrugAdministration]
GO 
*/ 


CREATE TABLE edi.[DrugAdministration] (
    DrugListingId                                                             BIGINT  NOT NULL,
    DrugAdministrationRoute                                                   BIGINT  NOT NULL,

    DrugAdministrationRouteName                                              VARCHAR (0060) NOT NULL

  ) -- edi.DrugAdministration

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugAdministration', @level2type=N'COLUMN', @level2name=N'DrugListingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugAdministration', @level2type=N'COLUMN', @level2name=N'DrugAdministrationRoute'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'DrugAdministration', @level2type=N'COLUMN', @level2name=N'DrugAdministrationRouteName'

GO
*/ 

-- EDI.DRUGADMINISTRATION ( END ) 


