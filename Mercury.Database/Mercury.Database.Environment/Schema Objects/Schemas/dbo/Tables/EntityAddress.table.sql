-- DBO.[ENTITYADDRESS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityAddress')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityAddress]
GO 
*/ 


CREATE TABLE dbo.[EntityAddress] (
    EntityAddressId                                                           BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,
    AddressType                                                                  INT  NOT NULL,
    Line1                                                                    VARCHAR (0055) NOT NULL,
    Line2                                                                    VARCHAR (0055) NOT NULL,
    City                                                                     VARCHAR (0030) NOT NULL,
    State                                                                       CHAR (0002) NOT NULL,
    ZipCode                                                                     CHAR (0005) NOT NULL,
    ZipPlus4                                                                    CHAR (0004) NOT NULL,
    PostalCode                                                                  CHAR (0015) NOT NULL,
    County                                                                   VARCHAR (0030) NOT NULL,
    Longitude                                                                DECIMAL (9, 6) NOT NULL,
    Latitude                                                                 DECIMAL (9, 6) NOT NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [EntityAddress_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [EntityAddress_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [EntityAddress_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityAddress_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [EntityAddress_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EntityAddress_Pk] PRIMARY KEY (EntityAddressId)

  , CONSTRAINT [EntityAddress_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityAddress_Fk_AddressType] FOREIGN KEY (AddressType) REFERENCES enum.[AddressType] (AddressType)

  ) -- dbo.EntityAddress

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Unique Entity Address Id]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'EntityAddressId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Id]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Address Type (Physical, Mailing, etc.)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'AddressType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line [Line 1]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'Line1'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line [Line 2]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'Line2'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'City'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address State', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'State'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ZipCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code Plus 4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ZipPlus4'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(reserved)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'PostalCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'County', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'County'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Longitude', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'Longitude'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Latitude', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'Latitude'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityAddress', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITYADDRESS ( END ) 


