-- DBO.[SESSIONCACHE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'SessionCache')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[SessionCache]
GO 
*/ 


CREATE TABLE dbo.[SessionCache] (
    SessionToken                                UNIQUEIDENTIFIER                      NOT NULL,
    ExpirationTime                                      DATETIME                      NOT NULL,
    CachedData                                           VARCHAR (4000)               NOT NULL    CONSTRAINT [SessionCache_Dft_CachedData] DEFAULT ('')

  , CONSTRAINT [SessionCache_Pk] PRIMARY KEY (SessionToken)

  ) -- dbo.SessionCache

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SessionCache', @level2type=N'COLUMN', @level2name=N'SessionToken'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SessionCache', @level2type=N'COLUMN', @level2name=N'ExpirationTime'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Cache (4000 characters) [Serialized Session Data]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'SessionCache', @level2type=N'COLUMN', @level2name=N'CachedData'

GO
*/ 

-- DBO.SESSIONCACHE ( END ) 


