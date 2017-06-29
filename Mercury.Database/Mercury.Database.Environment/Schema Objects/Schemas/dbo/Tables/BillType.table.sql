-- DBO.[BILLTYPE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'BillType')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[BillType]
GO 
*/ 


CREATE TABLE dbo.[BillType] (
    BillType                                                                 VARCHAR (0003) NOT NULL,
    BillTypeName                                                             VARCHAR (0060) NOT NULL,
    BillTypeDescription                                                      VARCHAR (0999) NOT NULL,

    FacilityTypeName                                                         VARCHAR (0060) NOT NULL,
    BillClassificationName                                                   VARCHAR (0060) NOT NULL,
    FrequencyName                                                            VARCHAR (0060) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [BillType_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [BillType_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [BillType_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [BillType_Pk] PRIMARY KEY (BillType)

  ) -- dbo.BillType

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Institutional Bill Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'BillType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'BillTypeName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'BillTypeDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'FacilityTypeName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'BillClassificationName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'FrequencyName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'BillType', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.BILLTYPE ( END ) 


