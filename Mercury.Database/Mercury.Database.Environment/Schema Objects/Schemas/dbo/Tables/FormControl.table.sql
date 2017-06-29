-- DBO.[FORMCONTROL] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'FormControl')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[FormControl]
GO 
*/ 


CREATE TABLE dbo.[FormControl] (
    FormId                                                                    BIGINT  NOT NULL,
    ControlId                                                       UNIQUEIDENTIFIER  NOT NULL,
    ParentId                                                        UNIQUEIDENTIFIER  NOT NULL,
    ControlIndex                                                                 INT  NOT NULL,
    ControlName                                                              VARCHAR (0060) NOT NULL,
    ControlTitle                                                             VARCHAR (0060) NOT NULL,
    ControlType                                                                  INT  NOT NULL,
    TabIndex                                                                SMALLINT  NOT NULL    CONSTRAINT [FormControl_Dft_TabIndex] DEFAULT (-1),

    Style                                                                        XML      NULL,

    DataBindings                                                                 XML      NULL,
    EventHandlers                                                                XML      NULL,
    Value                                                                        XML      NULL,

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [FormControl_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [FormControl_Dft_Visible] DEFAULT (1)

  , CONSTRAINT [FormControl_Pk] PRIMARY KEY (FormId, ControlId)

  , CONSTRAINT [FormControl_Fk_FormId] FOREIGN KEY (FormId) REFERENCES dbo.[Form] (FormId)

  ) -- dbo.FormControl

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Entity Form Reference]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'FormId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ControlId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Globally Unique Identifier (GUID)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ParentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Control Index (position in branch)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ControlIndex'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ControlName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ControlTitle'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ControlType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Control TAB Index', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'TabIndex'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data [Control Style Properties]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'Style'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data [Control Data Bindings]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'DataBindings'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data [Control Event Handlers]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'EventHandlers'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data [Control Value(s)]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'Value'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'FormControl', @level2type=N'COLUMN', @level2name=N'Visible'

GO
*/ 

-- DBO.FORMCONTROL ( END ) 


