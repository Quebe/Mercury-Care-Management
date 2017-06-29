-- DBO.[DATAEXPLORERNODERESULTDETAIL] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'DataExplorerNodeResultDetail')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[DataExplorerNodeResultDetail]
GO 
*/ 


CREATE TABLE dbo.[DataExplorerNodeResultDetail] (
    DataExplorerNodeInstanceId                                      UNIQUEIDENTIFIER  NOT NULL,
    Id                                                                        BIGINT  NOT NULL,
    DetailId                                                                  BIGINT  NOT NULL

  ) -- dbo.DataExplorerNodeResultDetail

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerNodeResultDetail', @level2type=N'COLUMN', @level2name=N'DataExplorerNodeInstanceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerNodeResultDetail', @level2type=N'COLUMN', @level2name=N'Id'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'DataExplorerNodeResultDetail', @level2type=N'COLUMN', @level2name=N'DetailId'

GO
*/ 

-- DBO.DATAEXPLORERNODERESULTDETAIL ( END ) 


