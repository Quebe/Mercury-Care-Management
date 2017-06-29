-- DBO.[MEMBER] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Member')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Member]
GO 
*/ 


CREATE TABLE dbo.[Member] (
    MemberId                                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,

    BirthDate                                                                   DATE  NOT NULL,
    DeathDate                                                                   DATE      NULL,
    Gender                                                                      CHAR (0001) NOT NULL,

    EthnicityId                                                               BIGINT  NOT NULL,
    CitizenshipId                                                             BIGINT  NOT NULL    CONSTRAINT [Member_Dft_CitizenshipId] DEFAULT (0),
    LanguageId                                                                BIGINT  NOT NULL,
    MaritalStatusId                                                           BIGINT  NOT NULL,

    FamilyId                                                                 VARCHAR (0020) NOT NULL    CONSTRAINT [Member_Dft_FamilyId] DEFAULT (''),

    ExternalMemberId                                                         VARCHAR (0020) NOT NULL    CONSTRAINT [Member_Dft_ExternalMemberId] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Member_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Member_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Member_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Member_Pk] PRIMARY KEY (MemberId)

  , CONSTRAINT [Member_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [Member_Fk_EthnicityId] FOREIGN KEY (EthnicityId) REFERENCES dbo.[Ethnicity] (EthnicityId)

  , CONSTRAINT [Member_Fk_CitizenshipId] FOREIGN KEY (CitizenshipId) REFERENCES dbo.[Citizenship] (CitizenshipId)

  , CONSTRAINT [Member_Fk_LanguageId] FOREIGN KEY (LanguageId) REFERENCES dbo.[Language] (LanguageId)

  , CONSTRAINT [Member_Fk_MaritalStatusId] FOREIGN KEY (MaritalStatusId) REFERENCES dbo.[MaritalStatus] (MaritalStatusId)

  ) -- dbo.Member

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Unique Member Id]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date [Birth Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'BirthDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date [Death Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'DeathDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Gender (M, F, or U)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'Gender'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'EthnicityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'CitizenshipId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'LanguageId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'MaritalStatusId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'FamilyId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Business Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'ExternalMemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Member', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBER ( END ) 


