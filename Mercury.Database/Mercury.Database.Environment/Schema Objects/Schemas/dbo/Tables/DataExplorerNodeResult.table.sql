-- DBO.[DATAEXPLORERNODERESULT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DataExplorerNodeResult')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[DataExplorerNodeResult]
GO 
*/ 


CREATE TABLE dbo.[DataExplorerNodeResult] (
    DataExplorerNodeInstanceId                                      UNIQUEIDENTIFIER  NOT NULL,
    Id                                                                        BIGINT  NOT NULL

  , CONSTRAINT [DataExplorerNodeResult_Pk] PRIMARY KEY (DataExplorerNodeInstanceId, Id)

  ) -- dbo.DataExplorerNodeResult

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerNodeResult', @level2type=N'COLUMN', @level2name=N'DataExplorerNodeInstanceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerNodeResult', @level2type=N'COLUMN', @level2name=N'Id'

GO
*/ 

-- DBO.DATAEXPLORERNODERESULT ( END ) 


