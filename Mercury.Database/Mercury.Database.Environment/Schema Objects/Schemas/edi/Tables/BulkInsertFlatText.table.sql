-- EDI.[BULKINSERTFLATTEXT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'BulkInsertFlatText')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[BulkInsertFlatText]
GO 
*/ 


CREATE TABLE edi.[BulkInsertFlatText] (
    RowContent                                                               VARCHAR (8000) NOT NULL    CONSTRAINT [BulkInsertFlatText_Dft_RowContent] DEFAULT ('')

  ) -- edi.BulkInsertFlatText

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description (8000 characters)', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'BulkInsertFlatText', @level2type=N'COLUMN', @level2name=N'RowContent'

GO
*/ 

-- EDI.BULKINSERTFLATTEXT ( END ) 


