-- ENUM.[AUTHORIZATIONSTATUS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'AuthorizationStatus')  AND (TABLE_SCHEMA = 'enum'))
  DROP TABLE enum.[AuthorizationStatus]
GO 
*/ 


CREATE TABLE enum.[AuthorizationStatus] (
    AuthorizationStatus                                                          INT  NOT NULL,
    AuthorizationStatusName                                                  VARCHAR (0060) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [AuthorizationStatus_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [AuthorizationStatus_Pk] PRIMARY KEY (AuthorizationStatus)

  , CONSTRAINT [AuthorizationStatus_Unq_AuthorizationStatusName] UNIQUE (AuthorizationStatusName)

  ) -- enum.AuthorizationStatus

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'AuthorizationStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'AuthorizationStatusName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'AuthorizationStatus', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- ENUM.AUTHORIZATIONSTATUS ( END ) 


