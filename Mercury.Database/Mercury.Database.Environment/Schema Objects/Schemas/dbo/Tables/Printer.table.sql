-- DBO.[PRINTER] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Printer')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Printer]
GO 
*/ 


CREATE TABLE dbo.[Printer] (
    PrinterId                                                                 BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PrinterName                                                              VARCHAR (0060) NOT NULL,
    PrinterDescription                                                       VARCHAR (0999) NOT NULL,

    PrintServerName                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_PrintServerName] DEFAULT (''),
    PrintQueueName                                                           VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_PrintQueueName] DEFAULT (''),

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [Printer_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [Printer_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Printer_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Printer_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Printer_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Printer_Pk] PRIMARY KEY (PrinterId)

  , CONSTRAINT [Printer_Unq_PrinterName] UNIQUE (PrinterName)

  ) -- dbo.Printer

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'PrinterId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'PrinterName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'PrinterDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'PrintServerName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'PrintQueueName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Printer', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.PRINTER ( END ) 


