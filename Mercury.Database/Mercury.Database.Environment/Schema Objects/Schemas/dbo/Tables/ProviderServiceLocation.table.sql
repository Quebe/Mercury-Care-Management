-- DBO.[PROVIDERSERVICELOCATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ProviderServiceLocation')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ProviderServiceLocation]
GO 
*/ 


CREATE TABLE dbo.[ProviderServiceLocation] (
    ProviderServiceLocationId                                                 BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ProviderId                                                                BIGINT  NOT NULL,
    ProviderEnrollmentId                                                      BIGINT  NOT NULL,
    ProviderAffiliationId                                                     BIGINT  NOT NULL,
    EntityAddressId                                                           BIGINT  NOT NULL,
    EntityContactInformationId                                                BIGINT  NOT NULL,

    ServiceLocationNumber                                                    VARCHAR (0020) NOT NULL,

    IsPcp                                                                        BIT  NOT NULL,
    IsAcceptingNewPatients                                                       BIT  NOT NULL,

    PanelSizeMaximum                                                             INT  NOT NULL,
    PanelAgeMinimum                                                              INT  NOT NULL,
    PanelAgeMaximum                                                              INT  NOT NULL,

    IsHandicapAccessible                                                         BIT  NOT NULL,

    OfficeHours                                                                  XML  NOT NULL,
    OfficeSchedulingHours                                                        XML  NOT NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ProviderServiceLocation_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ProviderServiceLocation_Pk] PRIMARY KEY (ProviderServiceLocationId, ProviderId, ProviderAffiliationId, EntityAddressId)

  , CONSTRAINT [ProviderServiceLocation_Fk_ProviderId] FOREIGN KEY (ProviderId) REFERENCES dbo.[Provider] (ProviderId)

  ) -- dbo.ProviderServiceLocation

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ProviderServiceLocationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ProviderEnrollmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ProviderAffiliationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'EntityAddressId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'EntityContactInformationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ServiceLocationNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'IsPcp'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'IsAcceptingNewPatients'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age [Maximum Members allowed in Panel]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'PanelSizeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age [Minimum Age in Years]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'PanelAgeMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age [Maximum Age in Years (inclusive)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'PanelAgeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'IsHandicapAccessible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'OfficeHours'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'OfficeSchedulingHours'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProviderServiceLocation', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.PROVIDERSERVICELOCATION ( END ) 


