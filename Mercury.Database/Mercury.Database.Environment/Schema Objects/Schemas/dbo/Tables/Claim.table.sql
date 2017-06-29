-- DBO.[CLAIM] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Claim')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Claim]
GO 
*/ 


CREATE TABLE dbo.[Claim] (
    ClaimId                                                                   BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ClaimNumber                                                              VARCHAR (0020) NOT NULL,

    MemberId                                                                  BIGINT  NOT NULL,
    ServiceProviderId                                                         BIGINT  NOT NULL,
    PaytoProviderAffiliationId                                                BIGINT      NULL,
    PaytoProviderId                                                           BIGINT      NULL,

    ClaimType                                                                    INT  NOT NULL    CONSTRAINT [Claim_Dft_ClaimType] DEFAULT (0),
    ReceivedDate                                                            DATETIME  NOT NULL,
    ClaimDateFrom                                                           DATETIME  NOT NULL,
    ClaimDateThru                                                           DATETIME  NOT NULL,
    AdmissionDate                                                           DATETIME      NULL,
    DischargeDate                                                           DATETIME      NULL,

    IsPcpClaim                                                                   BIT  NOT NULL    CONSTRAINT [Claim_Dft_IsPcpClaim] DEFAULT (0),

    ClaimStatus                                                                  INT  NOT NULL,

    BillType                                                                 VARCHAR (0003) NOT NULL    CONSTRAINT [Claim_Dft_BillType] DEFAULT (''),

    BilledAmount                                                               MONEY  NOT NULL,
    PaidAmount                                                                 MONEY  NOT NULL,

    PaidDate                                                                DATETIME      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Claim_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Claim_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Claim_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Claim_Pk] PRIMARY KEY (ClaimId)

  , CONSTRAINT [Claim_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [Claim_Fk_ServiceProviderId] FOREIGN KEY (ServiceProviderId) REFERENCES dbo.[Provider] (ProviderId)

  , CONSTRAINT [Claim_Fk_PaytoProviderAffiliationId] FOREIGN KEY (PaytoProviderAffiliationId) REFERENCES dbo.[ProviderAffiliation] (ProviderAffiliationId)

  , CONSTRAINT [Claim_Fk_PaytoProviderId] FOREIGN KEY (PaytoProviderId) REFERENCES dbo.[Provider] (ProviderId)

  , CONSTRAINT [Claim_Fk_ClaimType] FOREIGN KEY (ClaimType) REFERENCES enum.[ClaimType] (ClaimType)

  , CONSTRAINT [Claim_Fk_ClaimStatus] FOREIGN KEY (ClaimStatus) REFERENCES enum.[ClaimStatus] (ClaimStatus)

  , CONSTRAINT [Claim_Unq_ClaimNumber] UNIQUE (ClaimNumber)

  ) -- dbo.Claim

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ServiceProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'PaytoProviderAffiliationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'PaytoProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ReceivedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimDateFrom'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimDateThru'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'AdmissionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'DischargeDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'IsPcpClaim'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ClaimStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Institutional Bill Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'BillType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'BilledAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'PaidAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'PaidDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Claim', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CLAIM ( END ) 


