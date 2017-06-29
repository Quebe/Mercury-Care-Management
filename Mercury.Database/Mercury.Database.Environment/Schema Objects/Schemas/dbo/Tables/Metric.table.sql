-- DBO.[METRIC] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Metric')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Metric]
GO 
*/ 


CREATE TABLE dbo.[Metric] (
    MetricId                                                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MetricName                                                               VARCHAR (0060) NOT NULL,
    MetricDescription                                                        VARCHAR (0999) NOT NULL    CONSTRAINT [Metric_Dft_MetricDescription] DEFAULT (''),
    MetricType                                                                   INT  NOT NULL    CONSTRAINT [Metric_Dft_MetricType] DEFAULT (0),

    DataType                                                                     INT  NOT NULL    CONSTRAINT [Metric_Dft_DataType] DEFAULT (0),
    MinimumValue                                                             DECIMAL (20, 8) NOT NULL    CONSTRAINT [Metric_Dft_MinimumValue] DEFAULT (0),
    MaximumValue                                                             DECIMAL (20, 8) NOT NULL    CONSTRAINT [Metric_Dft_MaximumValue] DEFAULT (0),

    CostDataSource                                                               INT  NOT NULL    CONSTRAINT [Metric_Dft_CostDataSource] DEFAULT (0),
    CostClaimDateType                                                            INT  NOT NULL    CONSTRAINT [Metric_Dft_CostClaimDateType] DEFAULT (0),
    CostReportingPeriod                                                          INT  NOT NULL    CONSTRAINT [Metric_Dft_CostReportingPeriod] DEFAULT (0),
    CostReportingPeriodValue                                                     INT  NOT NULL    CONSTRAINT [Metric_Dft_CostReportingPeriodValue] DEFAULT (0),
    CostReportingPeriodQualifier                                                 INT  NOT NULL    CONSTRAINT [Metric_Dft_CostReportingPeriodQualifier] DEFAULT (0),
    CostWatermarkPeriod                                                          INT  NOT NULL    CONSTRAINT [Metric_Dft_CostWatermarkPeriod] DEFAULT (0),
    CostWatermarkPeriodValue                                                     INT  NOT NULL    CONSTRAINT [Metric_Dft_CostWatermarkPeriodValue] DEFAULT (0),
    CostWatermarkPeriodQualifier                                                 INT  NOT NULL    CONSTRAINT [Metric_Dft_CostWatermarkPeriodQualifier] DEFAULT (0),

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [Metric_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [Metric_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Metric_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Metric_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Metric_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Metric_Pk] PRIMARY KEY (MetricId)

  , CONSTRAINT [Metric_Unq_MetricName] UNIQUE (MetricName)

  ) -- dbo.Metric

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MetricId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MetricName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MetricDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MetricType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'DataType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MinimumValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Metric Value', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'MaximumValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostDataSource'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration [Service/Paid Date Selection]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostClaimDateType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostReportingPeriod'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostReportingPeriodValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostReportingPeriodQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostWatermarkPeriod'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Count Of', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostWatermarkPeriodValue'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date Qualifier Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CostWatermarkPeriodQualifier'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Metric', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.METRIC ( END ) 


