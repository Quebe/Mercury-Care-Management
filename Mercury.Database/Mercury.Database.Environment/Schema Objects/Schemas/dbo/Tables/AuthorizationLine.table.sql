-- DBO.[AUTHORIZATIONLINE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'AuthorizationLine')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[AuthorizationLine]
GO 
*/ 


CREATE TABLE dbo.[AuthorizationLine] (
    AuthorizationId                                                           BIGINT  NOT NULL,
    LineNumber                                                                   INT  NOT NULL,

    AuthorizationLineStatus                                                      INT  NOT NULL,
    ServiceDate                                                             DATETIME  NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ServiceDate] DEFAULT (GETDATE ()),

    RevenueCode                                                              VARCHAR (0004) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_RevenueCode] DEFAULT (''),
    ProcedureCode                                                            VARCHAR (0005) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ProcedureCode] DEFAULT (''),
    ModifierCode                                                             VARCHAR (0002) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ModifierCode] DEFAULT (''),
    NdcCode                                                                     CHAR (0011) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_NdcCode] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [AuthorizationLine_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [AuthorizationLine_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [AuthorizationLine_Pk] PRIMARY KEY (AuthorizationId, LineNumber)

  , CONSTRAINT [AuthorizationLine_Fk_AuthorizationId] FOREIGN KEY (AuthorizationId) REFERENCES dbo.[Authorization] (AuthorizationId)

  , CONSTRAINT [AuthorizationLine_Fk_AuthorizationLineStatus] FOREIGN KEY (AuthorizationLineStatus) REFERENCES enum.[AuthorizationStatus] (AuthorizationStatus)

  ) -- dbo.AuthorizationLine

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'AuthorizationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Line Number', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'LineNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'AuthorizationLineStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ServiceDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Revenue Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'RevenueCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPT4/HCPCS Procedure Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ProcedureCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ModifierCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'NDC Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'NdcCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'AuthorizationLine', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.AUTHORIZATIONLINE ( END ) 


