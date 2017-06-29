-- DBO.[CLAIMLINE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ClaimLine')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ClaimLine]
GO 
*/ 


CREATE TABLE dbo.[ClaimLine] (
    ClaimId                                                                   BIGINT  NOT NULL,
    Line                                                                         INT  NOT NULL,
    LineStatus                                                                   INT  NOT NULL    CONSTRAINT [ClaimLine_Dft_LineStatus] DEFAULT (0),

    ServiceDateFrom                                                         DATETIME  NOT NULL,
    ServiceDateThru                                                         DATETIME  NOT NULL,

    ServicePlace                                                             VARCHAR (0002) NOT NULL    CONSTRAINT [ClaimLine_Dft_ServicePlace] DEFAULT (''),
    RevenueCode                                                              VARCHAR (0004) NOT NULL    CONSTRAINT [ClaimLine_Dft_RevenueCode] DEFAULT (''),
    ProcedureCode                                                            VARCHAR (0005) NOT NULL    CONSTRAINT [ClaimLine_Dft_ProcedureCode] DEFAULT (''),
    ModifierCode1                                                            VARCHAR (0002) NOT NULL    CONSTRAINT [ClaimLine_Dft_ModifierCode1] DEFAULT (''),
    ModifierCode2                                                            VARCHAR (0002) NOT NULL    CONSTRAINT [ClaimLine_Dft_ModifierCode2] DEFAULT (''),
    ModifierCode3                                                            VARCHAR (0002) NOT NULL    CONSTRAINT [ClaimLine_Dft_ModifierCode3] DEFAULT (''),
    ModifierCode4                                                            VARCHAR (0002) NOT NULL    CONSTRAINT [ClaimLine_Dft_ModifierCode4] DEFAULT (''),
    Units                                                                    DECIMAL (20, 8) NOT NULL,

    IsEmergency                                                                  BIT  NOT NULL    CONSTRAINT [ClaimLine_Dft_IsEmergency] DEFAULT (0),
    IsEpsdt                                                                      BIT  NOT NULL    CONSTRAINT [ClaimLine_Dft_IsEpsdt] DEFAULT (0),
    IsFamilyPlanning                                                             BIT  NOT NULL    CONSTRAINT [ClaimLine_Dft_IsFamilyPlanning] DEFAULT (0),

    AuthorizationNumber                                                      VARCHAR (0020) NOT NULL    CONSTRAINT [ClaimLine_Dft_AuthorizationNumber] DEFAULT (''),
    AuthorizationId                                                           BIGINT      NULL,

    BilledAmount                                                               MONEY  NOT NULL    CONSTRAINT [ClaimLine_Dft_BilledAmount] DEFAULT (0),
    PaidAmount                                                                 MONEY  NOT NULL    CONSTRAINT [ClaimLine_Dft_PaidAmount] DEFAULT (0)

  , CONSTRAINT [ClaimLine_Pk] PRIMARY KEY (ClaimId, Line)

  , CONSTRAINT [ClaimLine_Fk_ClaimId] FOREIGN KEY (ClaimId) REFERENCES dbo.[Claim] (ClaimId)

  , CONSTRAINT [ClaimLine_Fk_LineStatus] FOREIGN KEY (LineStatus) REFERENCES enum.[ClaimStatus] (ClaimStatus)

  , CONSTRAINT [ClaimLine_Fk_AuthorizationId] FOREIGN KEY (AuthorizationId) REFERENCES dbo.[Authorization] (AuthorizationId)

  ) -- dbo.ClaimLine

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'Line'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'LineStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ServiceDateFrom'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ServiceDateThru'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Location Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ServicePlace'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Revenue Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'RevenueCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CPT4/HCPCS Procedure Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ProcedureCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ModifierCode1'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ModifierCode2'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ModifierCode3'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Modifier Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'ModifierCode4'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Units Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'Units'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'IsEmergency'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'IsEpsdt'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'IsFamilyPlanning'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'AuthorizationNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'AuthorizationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'BilledAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ClaimLine', @level2type=N'COLUMN', @level2name=N'PaidAmount'

GO
*/ 

-- DBO.CLAIMLINE ( END ) 


