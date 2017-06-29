-- LOGS.[SERVICEPROCESS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ServiceProcess')  AND (TABLE_SCHEMA = 'logs'))
  DROP TABLE logs.[ServiceProcess]
GO 
*/ 


CREATE TABLE logs.[ServiceProcess] (
    ProcessLogId                                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ServiceId                                                                 BIGINT  NOT NULL    CONSTRAINT [ServiceProcess_Dft_ServiceId] DEFAULT (0),
    ServiceName                                                              VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_ServiceName] DEFAULT (''),

    StartDate                                                               DATETIME  NOT NULL,
    EndDate                                                                 DATETIME      NULL,

    Outcome                                                                  VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_Outcome] DEFAULT (''),
    Exception                                                                VARCHAR (0999)     NULL,
    Debug                                                                    VARCHAR (0999)     NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ServiceProcess_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ServiceProcess_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ServiceProcess_Dft_ModifiedDate] DEFAULT (GETDATE ())

  ) -- logs.ServiceProcess

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ProcessLogId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ServiceName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'StartDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'EndDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'Outcome'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'Exception'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'Debug'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'logs', @level1type=N'TABLE', @level1name=N'ServiceProcess', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- LOGS.SERVICEPROCESS ( END ) 


