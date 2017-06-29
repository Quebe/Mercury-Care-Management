-- DBO.[LOINC] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Loinc')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Loinc]
GO 
*/ 


CREATE TABLE dbo.[Loinc] (
    Loinc                                                                    VARCHAR (0007) NOT NULL,
    LoincName                                                                VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_LoincName] DEFAULT (''),
    LoincDescription                                                         VARCHAR (0999) NOT NULL    CONSTRAINT [Loinc_Dft_LoincDescription] DEFAULT (''),

    Component                                                                VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_Component] DEFAULT (''),
    Property                                                                 VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_Property] DEFAULT (''),
    TimeAspect                                                               VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_TimeAspect] DEFAULT (''),
    System                                                                   VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_System] DEFAULT (''),
    Scale                                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_Scale] DEFAULT (''),
    Method                                                                   VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_Method] DEFAULT (''),

    Classification                                                           VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_Classification] DEFAULT (''),

    AreUnitsRequired                                                             BIT  NOT NULL    CONSTRAINT [Loinc_Dft_AreUnitsRequired] DEFAULT (0),

    MapToLoinc                                                               VARCHAR (0007)     NULL,

    Status                                                                   VARCHAR (0030) NOT NULL    CONSTRAINT [Loinc_Dft_Status] DEFAULT (''),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Loinc_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Loinc_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Loinc_Pk] PRIMARY KEY (Loinc)

  ) -- dbo.Loinc

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOINC [LOINC_NUM]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Loinc'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SHORTNAME]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'LoincName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description [LONG_COMMON_NAME]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'LoincDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [COMPONENT]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Component'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [PROPERTY]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Property'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [TIME_ASPCT]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'TimeAspect'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SYSTEM]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'System'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SCALE_TYP]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Scale'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [METHOD_TYP]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Method'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [CLASS]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Classification'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [UNITSREQUIRED]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'AreUnitsRequired'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOINC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'MapToLoinc'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.LOINC ( END ) 


