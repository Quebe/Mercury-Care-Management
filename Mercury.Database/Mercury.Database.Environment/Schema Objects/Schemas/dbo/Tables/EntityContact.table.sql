-- DBO.[ENTITYCONTACT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityContact')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityContact]
GO 
*/ 


CREATE TABLE dbo.[EntityContact] (
    EntityContactId                                                           BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,
    EntityContactInformationId                                                BIGINT  NOT NULL    CONSTRAINT [EntityContact_Dft_EntityContactInformationId] DEFAULT (0),

    RelatedEntityId                                                           BIGINT      NULL,
    RelatedObjectType                                                        VARCHAR (0120) NOT NULL    CONSTRAINT [EntityContact_Dft_RelatedObjectType] DEFAULT (''),
    RelatedObjectId                                                           BIGINT      NULL    CONSTRAINT [EntityContact_Dft_RelatedObjectId] DEFAULT (0),
    ScriptEntityFormId                                                        BIGINT      NULL    CONSTRAINT [EntityContact_Dft_ScriptEntityFormId] DEFAULT (0),

    DataSource                                                               VARCHAR (0030) NOT NULL    CONSTRAINT [EntityContact_Dft_DataSource] DEFAULT ('MERCURY'),
    ReadyToContactDate                                                      DATETIME  NOT NULL    CONSTRAINT [EntityContact_Dft_ReadyToContactDate] DEFAULT (GETDATE ()),
    ContactDate                                                             DATETIME      NULL,
    ContactDirection                                                             INT  NOT NULL    CONSTRAINT [EntityContact_Dft_ContactDirection] DEFAULT (0),
    ContactType                                                                  INT  NOT NULL    CONSTRAINT [EntityContact_Dft_ContactType] DEFAULT (0),
    Successful                                                                   BIT  NOT NULL    CONSTRAINT [EntityContact_Dft_Successful] DEFAULT (0),
    ContactOutcome                                                               INT  NOT NULL    CONSTRAINT [EntityContact_Dft_ContactOutcome] DEFAULT (0),

    ContactRegardingId                                                        BIGINT      NULL,
    Regarding                                                                VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_Regarding] DEFAULT (''),
    Remarks                                                                  VARCHAR (0999) NOT NULL    CONSTRAINT [EntityContact_Dft_Remarks] DEFAULT (''),
    ContactedByName                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_ContactedByName] DEFAULT (''),

    AutomationId                                                    UNIQUEIDENTIFIER      NULL,
    AutomationStatus                                                             INT  NOT NULL    CONSTRAINT [EntityContact_Dft_AutomationStatus] DEFAULT (0),
    AutomationDate                                                          DATETIME      NULL,
    AutomationException                                                      VARCHAR (0099)     NULL,

    ExtendedProperties                                                           XML      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [EntityContact_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityContact_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [EntityContact_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EntityContact_Pk] PRIMARY KEY (EntityContactId)

  , CONSTRAINT [EntityContact_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityContact_Fk_EntityContactInformationId] FOREIGN KEY (EntityContactInformationId) REFERENCES dbo.[EntityContactInformation] (EntityContactInformationId)

  , CONSTRAINT [EntityContact_Fk_RelatedEntityId] FOREIGN KEY (RelatedEntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityContact_Fk_ContactOutcome] FOREIGN KEY (ContactOutcome) REFERENCES enum.[ContactOutcome] (ContactOutcome)

  , CONSTRAINT [EntityContact_Fk_ContactRegardingId] FOREIGN KEY (ContactRegardingId) REFERENCES dbo.[ContactRegarding] (ContactRegardingId)

  ) -- dbo.EntityContact

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'EntityContactId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Contact Information Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'EntityContactInformationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference (related)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'RelatedEntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String [Object Related to the Contact]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'RelatedObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Object Related to the Contact]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'RelatedObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Related Form (Script) Used during Contact]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ScriptEntityFormId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Data Source Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'DataSource'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Used for Automation]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ReadyToContactDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [NULL only valid for Automation]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [(Inbound/Outbound)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactDirection'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [(Enumeration)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'Successful'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactOutcome'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactRegardingId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Regarding Message]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'Regarding'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description [User Remarks about the Contact]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'Remarks'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Display Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ContactedByName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'AutomationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'AutomationStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'AutomationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Automation Exception Message (short)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'AutomationException'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityContact', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITYCONTACT ( END ) 


