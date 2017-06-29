-- EDI.[LOINC] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Loinc')  AND (TABLE_SCHEMA = 'edi'))
  DROP TABLE edi.[Loinc]
GO 
*/ 


CREATE TABLE edi.[Loinc] (
    Loinc                                                                    VARCHAR (0007) NOT NULL,
    LoincName                                                                VARCHAR (0060) NOT NULL    CONSTRAINT [Loinc_Dft_LoincName] DEFAULT (''),
    LoincDescription                                                         VARCHAR (0999) NOT NULL    CONSTRAINT [Loinc_Dft_LoincDescription] DEFAULT (''),

    Component                                                                VARCHAR (0060) NOT NULL,
    Property                                                                 VARCHAR (0060) NOT NULL,
    TimeAspect                                                               VARCHAR (0060) NOT NULL,
    System                                                                   VARCHAR (0060) NOT NULL,
    Scale                                                                    VARCHAR (0060) NOT NULL,
    Method                                                                   VARCHAR (0060) NOT NULL,

    Classification                                                           VARCHAR (0060) NOT NULL,

    AreUnitsRequired                                                             BIT  NOT NULL    CONSTRAINT [Loinc_Dft_AreUnitsRequired] DEFAULT (0),

    LastChangedDate                                                         DATETIME      NULL,
    ChangeType                                                               VARCHAR (0030) NOT NULL,
    MapToLoinc                                                               VARCHAR (0007)     NULL,
    Status                                                                   VARCHAR (0030) NOT NULL,
    StatusReason                                                             VARCHAR (0030) NOT NULL    CONSTRAINT [Loinc_Dft_StatusReason] DEFAULT (''),
    StatusDescription                                                        VARCHAR (8000) NOT NULL    CONSTRAINT [Loinc_Dft_StatusDescription] DEFAULT ('')

  ) -- edi.Loinc

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOINC [LOINC_NUM]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Loinc'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SHORTNAME]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'LoincName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description [LONG_COMMON_NAME]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'LoincDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [COMPONENT]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Component'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [PROPERTY]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Property'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [TIME_ASPCT]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'TimeAspect'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SYSTEM]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'System'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [SCALE_TYP]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Scale'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [METHOD_TYP]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Method'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [CLASS]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Classification'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [UNITSREQUIRED]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'AreUnitsRequired'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [DT_LAST_CH]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'LastChangedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text [CHNG_TYPE]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'ChangeType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LOINC [MAP_TO]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'MapToLoinc'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text [STATUS]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type or Enumeration Name/Text [STATUS_REASON]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'StatusReason'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description (8000 characters) [STATUS_TEXT]', @level0type=N'SCHEMA', @level0name=N'edi', @level1type=N'TABLE', @level1name=N'Loinc', @level2type=N'COLUMN', @level2name=N'StatusDescription'

GO
*/ 

-- EDI.LOINC ( END ) 


