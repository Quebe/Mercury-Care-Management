-- DBO.[CARELEVELACTIVITYTHRESHOLD] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'CareLevelActivityThreshold')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[CareLevelActivityThreshold]
GO 
*/ 


CREATE TABLE dbo.[CareLevelActivityThreshold] (
    CareLevelActivityThresholdId                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    CareLevelActivityThresholdName                                           VARCHAR (0060) NOT NULL,
    CareLevelActivityThresholdDescription                                    VARCHAR (0999) NOT NULL,

    CareLevelActivityId                                                       BIGINT  NOT NULL,

    RelativeDateValue                                                            INT  NOT NULL,
    RelativeDateQualifier                                                        INT  NOT NULL,

    Status                                                                       INT  NOT NULL,

    ActionId                                                                  BIGINT      NULL,
    ActionParameters                                                             XML      NULL,
    ActionDescription                                                        VARCHAR (0099)     NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [CareLevelActivityThreshold_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [CareLevelActivityThreshold_Pk] PRIMARY KEY (CareLevelActivityThresholdId)

  , CONSTRAINT [CareLevelActivityThreshold_Fk_CareLevelActivityId] FOREIGN KEY (CareLevelActivityId) REFERENCES dbo.[CareLevelActivity] (CareLevelActivityId)

  ) -- dbo.CareLevelActivityThreshold

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CareLevelActivityThresholdId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CareLevelActivityThresholdName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CareLevelActivityThresholdDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CareLevelActivityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Integer Date Value (used w/ Date Qualifier)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'RelativeDateValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'RelativeDateQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'Status'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Action Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'CareLevelActivityThreshold', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.CARELEVELACTIVITYTHRESHOLD ( END ) 


