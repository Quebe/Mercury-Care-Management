-- DBO.[ENTITYNOTE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityNote')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityNote]
GO 
*/ 


CREATE TABLE dbo.[EntityNote] (
    EntityNoteId                                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,

    RelatedEntityId                                                           BIGINT      NULL,

    RelatedEntityType                                                            INT  NOT NULL    CONSTRAINT [EntityNote_Dft_RelatedEntityType] DEFAULT (0),
    RelatedEntityObjectId                                                     BIGINT  NOT NULL    CONSTRAINT [EntityNote_Dft_RelatedEntityObjectId] DEFAULT (0),

    RelatedObjectType                                                        VARCHAR (0120) NOT NULL    CONSTRAINT [EntityNote_Dft_RelatedObjectType] DEFAULT (''),
    RelatedObjectId                                                           BIGINT  NOT NULL    CONSTRAINT [EntityNote_Dft_RelatedObjectId] DEFAULT (0),

    DataSource                                                               VARCHAR (0030) NOT NULL    CONSTRAINT [EntityNote_Dft_DataSource] DEFAULT ('MERCURY'),
    Importance                                                                   INT  NOT NULL    CONSTRAINT [EntityNote_Dft_Importance] DEFAULT (0),
    NoteTypeId                                                                BIGINT  NOT NULL    CONSTRAINT [EntityNote_Dft_NoteTypeId] DEFAULT (0),
    Subject                                                                  VARCHAR (0120) NOT NULL    CONSTRAINT [EntityNote_Dft_Subject] DEFAULT (''),

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [EntityNote_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [EntityNote_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [EntityNote_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityNote_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [EntityNote_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EntityNote_Pk] PRIMARY KEY (EntityNoteId)

  , CONSTRAINT [EntityNote_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityNote_Fk_RelatedEntityId] FOREIGN KEY (RelatedEntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityNote_Fk_NoteTypeId] FOREIGN KEY (NoteTypeId) REFERENCES dbo.[NoteType] (NoteTypeId)

  ) -- dbo.EntityNote

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'EntityNoteId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference (related)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'RelatedEntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Entity Type (Member, Provider, etc.)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'RelatedEntityType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'RelatedEntityObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'RelatedObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'RelatedObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Source Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'DataSource'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type ["Informational", "Warning", or "Critical"]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'Importance'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Note Type Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'NoteTypeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Subject', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'Subject'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityNote', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITYNOTE ( END ) 


