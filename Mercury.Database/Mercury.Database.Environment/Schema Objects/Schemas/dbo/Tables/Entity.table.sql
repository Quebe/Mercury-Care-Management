-- DBO.[ENTITY] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Entity')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Entity]
GO 
*/ 


CREATE TABLE dbo.[Entity] (
    EntityId                                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityType                                                                   INT  NOT NULL,
    EntityName                                                               VARCHAR (0060) NOT NULL,
    NameLast                                                                 VARCHAR (0035) NOT NULL    CONSTRAINT [Entity_Dft_NameLast] DEFAULT (''),
    NameFirst                                                                VARCHAR (0025) NOT NULL    CONSTRAINT [Entity_Dft_NameFirst] DEFAULT (''),
    NameMiddle                                                               VARCHAR (0025) NOT NULL    CONSTRAINT [Entity_Dft_NameMiddle] DEFAULT (''),
    NamePrefix                                                               VARCHAR (0010) NOT NULL    CONSTRAINT [Entity_Dft_NamePrefix] DEFAULT (''),
    NameSuffix                                                               VARCHAR (0010) NOT NULL    CONSTRAINT [Entity_Dft_NameSuffix] DEFAULT (''),
    FederalTaxId                                                                CHAR (0009) NOT NULL    CONSTRAINT [Entity_Dft_FederalTaxId] DEFAULT (''),
    IdCodeQualifier                                                          VARCHAR (0002) NOT NULL    CONSTRAINT [Entity_Dft_IdCodeQualifier] DEFAULT ('ZZ'),
    UniqueId                                                                 VARCHAR (0020) NOT NULL    CONSTRAINT [Entity_Dft_UniqueId] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Entity_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Entity_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Entity_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Entity_Pk] PRIMARY KEY (EntityId)

  , CONSTRAINT [Entity_Fk_EntityType] FOREIGN KEY (EntityType) REFERENCES enum.[EntityType] (EntityType)

  ) -- dbo.Entity

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Unique Entity Id]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'EntityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'EntityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Last Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'NameLast'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'First Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'NameFirst'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Middle Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'NameMiddle'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name Prefix', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'NamePrefix'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name Suffix', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'NameSuffix'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Federal Tax ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'FederalTaxId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID Code Qualifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'IdCodeQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'UniqueId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Entity', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITY ( END ) 


