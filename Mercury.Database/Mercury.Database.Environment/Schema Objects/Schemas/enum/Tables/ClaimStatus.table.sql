-- ENUM.[CLAIMSTATUS] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ClaimStatus')  AND (TABLE_SCHEMA = 'enum'))
  DROP TABLE enum.[ClaimStatus]
GO 
*/ 


CREATE TABLE enum.[ClaimStatus] (
    ClaimStatus                                                                  INT  NOT NULL,
    ClaimStatusName                                                          VARCHAR (0060) NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ClaimStatus_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ClaimStatus_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ClaimStatus_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ClaimStatus_Pk] PRIMARY KEY (ClaimStatus)

  , CONSTRAINT [ClaimStatus_Unq_ClaimStatusName] UNIQUE (ClaimStatusName)

  ) -- enum.ClaimStatus

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ClaimStatus'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ClaimStatusName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'enum', @level1type=N'TABLE', @level1name=N'ClaimStatus', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- ENUM.CLAIMSTATUS ( END ) 


