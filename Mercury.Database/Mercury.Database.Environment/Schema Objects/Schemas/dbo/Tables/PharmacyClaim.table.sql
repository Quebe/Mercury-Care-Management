-- DBO.[PHARMACYCLAIM] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PharmacyClaim')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PharmacyClaim]
GO 
*/ 


CREATE TABLE dbo.[PharmacyClaim] (
    PharmacyClaimId                                                           BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    AdjustedPharmacyClaimId                                                   BIGINT      NULL,
    ClaimNumber                                                              VARCHAR (0020) NOT NULL    CONSTRAINT [PharmacyClaim_Dft_ClaimNumber] DEFAULT (''),

    MemberId                                                                  BIGINT  NOT NULL,
    PharmacyId                                                                BIGINT      NULL,
    PrescriberProviderId                                                      BIGINT      NULL,

    PrescriptionDate                                                        DATETIME  NOT NULL,
    ServiceDate                                                             DATETIME  NOT NULL,
    ReceivedDate                                                            DATETIME  NOT NULL,
    PaidDate                                                                DATETIME  NOT NULL,
    Status                                                                       INT  NOT NULL,

    IsCompound                                                                   BIT  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_IsCompound] DEFAULT (0),
    DispenseAsWritten                                                            INT  NOT NULL,
    NationalDrugCode                                                            CHAR (0011) NOT NULL,
    Units                                                                    DECIMAL (20, 8) NOT NULL,
    UnitType                                                                 VARCHAR (0030) NOT NULL    CONSTRAINT [PharmacyClaim_Dft_UnitType] DEFAULT (''),
    DaysSupply                                                                   INT  NOT NULL,
    FillNumber                                                                   INT  NOT NULL,
    RefillCount                                                                  INT  NOT NULL,

    IngredientCostBasis                                                          INT  NOT NULL,
    IngredientCostAmount                                                       MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_IngredientCostAmount] DEFAULT (0),
    DispensingFeeAmount                                                        MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_DispensingFeeAmount] DEFAULT (0),
    UsualCustomaryChargeAmount                                                 MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_UsualCustomaryChargeAmount] DEFAULT (0),
    GrossDueAmount                                                             MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_GrossDueAmount] DEFAULT (0),
    PatientAmount                                                              MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_PatientAmount] DEFAULT (0),
    CobAmount                                                                  MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_CobAmount] DEFAULT (0),
    TaxAmount                                                                  MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_TaxAmount] DEFAULT (0),
    PaidAmount                                                                 MONEY  NOT NULL    CONSTRAINT [PharmacyClaim_Dft_PaidAmount] DEFAULT (0)

  , CONSTRAINT [PharmacyClaim_Pk] PRIMARY KEY (PharmacyClaimId)

  , CONSTRAINT [PharmacyClaim_Fk_AdjustedPharmacyClaimId] FOREIGN KEY (AdjustedPharmacyClaimId) REFERENCES dbo.[PharmacyClaim] (PharmacyClaimId)

  , CONSTRAINT [PharmacyClaim_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [PharmacyClaim_Fk_PrescriberProviderId] FOREIGN KEY (PrescriberProviderId) REFERENCES dbo.[Provider] (ProviderId)

  , CONSTRAINT [PharmacyClaim_Fk_DispenseAsWritten] FOREIGN KEY (DispenseAsWritten) REFERENCES enum.[DispenseAsWritten] (DispenseAsWritten)

  , CONSTRAINT [PharmacyClaim_Fk_IngredientCostBasis] FOREIGN KEY (IngredientCostBasis) REFERENCES enum.[IngredientCostBasis] (IngredientCostBasis)

  ) -- dbo.PharmacyClaim

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PharmacyClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'AdjustedPharmacyClaimId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'ClaimNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PharmacyId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PrescriberProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PrescriptionDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'ServiceDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'ReceivedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PaidDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'IsCompound'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'DispenseAsWritten'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'National Drug Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'NationalDrugCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Units Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'Units'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'UnitType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'DaysSupply'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'FillNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'RefillCount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'IngredientCostBasis'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'IngredientCostAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'DispensingFeeAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'UsualCustomaryChargeAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'GrossDueAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PatientAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'CobAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'TaxAmount'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Amount', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PharmacyClaim', @level2type=N'COLUMN', @level2name=N'PaidAmount'

GO
*/ 

-- DBO.PHARMACYCLAIM ( END ) 


