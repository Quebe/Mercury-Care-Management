-- DBO.[ENTITYCONTACTINFORMATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityContactInformation')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityContactInformation]
GO 
*/ 


CREATE TABLE dbo.[EntityContactInformation] (
    EntityContactInformationId                                                BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,
    ContactType                                                                  INT  NOT NULL,
    ContactSequence                                                              INT  NOT NULL,
    ContactNumber                                                            VARCHAR (0020) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ContactNumber] DEFAULT (''),
    ContactExtension                                                         VARCHAR (0020) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ContactExtension] DEFAULT (''),
    ContactEmail                                                             VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ContactEmail] DEFAULT (''),

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [EntityContactInformation_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [EntityContactInformation_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [EntityContactInformation_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [EntityContactInformation_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EntityContactInformation_Pk] PRIMARY KEY (EntityContactInformationId)

  , CONSTRAINT [EntityContactInformation_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  ) -- dbo.EntityContactInformation

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'EntityContactInformationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Id]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Contact Type (Phone, Fax, etc.)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ContactType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Sequence', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ContactSequence'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Number', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ContactNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Extension', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ContactExtension'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Email', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ContactEmail'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContactInformation', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITYCONTACTINFORMATION ( END ) 


