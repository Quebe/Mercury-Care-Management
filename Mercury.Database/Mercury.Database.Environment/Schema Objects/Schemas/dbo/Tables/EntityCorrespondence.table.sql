-- DBO.[ENTITYCORRESPONDENCE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityCorrespondence')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityCorrespondence]
GO 
*/ 


CREATE TABLE dbo.[EntityCorrespondence] (
    EntityCorrespondenceId                                                    BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    EntityId                                                                  BIGINT  NOT NULL,

    CorrespondenceId                                                          BIGINT  NOT NULL,
    CorrespondenceName                                                       VARCHAR (0060) NOT NULL,
    CorrespondenceVersion                                                    DECIMAL (20, 8) NOT NULL,

    EntityFormId                                                              BIGINT      NULL,
    RelatedEntityId                                                           BIGINT      NULL,
    RelatedObjectType                                                        VARCHAR (0120) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_RelatedObjectType] DEFAULT (''),
    RelatedObjectId                                                           BIGINT      NULL,

    ReadyToSendDate                                                         DATETIME  NOT NULL,
    SentDate                                                                DATETIME      NULL,
    ReceivedDate                                                            DATETIME      NULL,
    ReturnedDate                                                            DATETIME      NULL,

    ContactType                                                                  INT  NOT NULL,
    EntityAddressId                                                           BIGINT      NULL,
    EntityContactInformationId                                                BIGINT      NULL,
    Attention                                                                VARCHAR (0060) NOT NULL,
    AddressLine1                                                             VARCHAR (0055) NOT NULL,
    AddressLine2                                                             VARCHAR (0055) NOT NULL,
    AddressCity                                                              VARCHAR (0030) NOT NULL,
    AddressState                                                                CHAR (0002) NOT NULL,
    AddressZipCode                                                              CHAR (0005) NOT NULL,
    AddressZipPlus4                                                             CHAR (0004) NOT NULL,
    AddressPostalCode                                                           CHAR (0015) NOT NULL,

    ContactFaxNumber                                                         VARCHAR (0020) NOT NULL,
    ContactEmail                                                             VARCHAR (0060) NOT NULL,

    Remarks                                                                  VARCHAR (0999) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_Remarks] DEFAULT (''),

    AutomationId                                                    UNIQUEIDENTIFIER      NULL,
    AutomationStatus                                                             INT  NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_AutomationStatus] DEFAULT (0),
    AutomationDate                                                          DATETIME      NULL,
    AutomationException                                                      VARCHAR (0099)     NULL,

    ExtendedProperties                                                           XML      NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [EntityCorrespondence_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [EntityCorrespondence_Pk] PRIMARY KEY (EntityCorrespondenceId)

  , CONSTRAINT [EntityCorrespondence_Fk_EntityId] FOREIGN KEY (EntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityCorrespondence_Fk_CorrespondenceId] FOREIGN KEY (CorrespondenceId) REFERENCES dbo.[Correspondence] (CorrespondenceId)

  , CONSTRAINT [EntityCorrespondence_Fk_EntityFormId] FOREIGN KEY (EntityFormId) REFERENCES dbo.[EntityForm] (EntityFormId)

  , CONSTRAINT [EntityCorrespondence_Fk_RelatedEntityId] FOREIGN KEY (RelatedEntityId) REFERENCES dbo.[Entity] (EntityId)

  , CONSTRAINT [EntityCorrespondence_Fk_EntityAddressId] FOREIGN KEY (EntityAddressId) REFERENCES dbo.[EntityAddress] (EntityAddressId)

  , CONSTRAINT [EntityCorrespondence_Fk_EntityContactInformationId] FOREIGN KEY (EntityContactInformationId) REFERENCES dbo.[EntityContactInformation] (EntityContactInformationId)

  ) -- dbo.EntityCorrespondence

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'EntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CorrespondenceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CorrespondenceName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Version', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CorrespondenceVersion'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'EntityFormId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Reference (related)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'RelatedEntityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'RelatedObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'RelatedObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ReadyToSendDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'SentDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ReceivedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ReturnedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ContactType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'EntityAddressId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'EntityContactInformationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'Attention'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressLine1'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Line', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressLine2'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address City', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressCity'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address State', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressState'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressZipCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Address Zip Code Plus 4', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressZipPlus4'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'(reserved)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AddressPostalCode'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Number', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ContactFaxNumber'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Contact Email', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ContactEmail'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'Remarks'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AutomationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AutomationStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AutomationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Automation Exception Message (short)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'AutomationException'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondence', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ENTITYCORRESPONDENCE ( END ) 


