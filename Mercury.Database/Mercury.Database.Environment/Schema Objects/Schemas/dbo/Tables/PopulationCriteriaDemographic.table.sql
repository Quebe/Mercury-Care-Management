-- DBO.[POPULATIONCRITERIADEMOGRAPHIC] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'PopulationCriteriaDemographic')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[PopulationCriteriaDemographic]
GO 
*/ 


CREATE TABLE dbo.[PopulationCriteriaDemographic] (
    PopulationCriteriaDemographicId                                           BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationId                                                              BIGINT  NOT NULL,

    UseAgeCriteria                                                               BIT  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_UseAgeCriteria] DEFAULT (0),
    AgeMinimum                                                                   INT  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_AgeMinimum] DEFAULT (0),
    AgeMaximum                                                                   INT  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_AgeMaximum] DEFAULT (0),

    Gender                                                                       INT  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_Gender] DEFAULT (0),

    EthnicityId                                                               BIGINT  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_EthnicityId] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [PopulationCriteriaDemographic_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [PopulationCriteriaDemographic_Pk] PRIMARY KEY (PopulationCriteriaDemographicId)

  , CONSTRAINT [PopulationCriteriaDemographic_Fk_PopulationId] FOREIGN KEY (PopulationId) REFERENCES dbo.[Population] (PopulationId)

  , CONSTRAINT [PopulationCriteriaDemographic_Fk_EthnicityId] FOREIGN KEY (EthnicityId) REFERENCES dbo.[Ethnicity] (EthnicityId)

  ) -- dbo.PopulationCriteriaDemographic

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'PopulationCriteriaDemographicId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'UseAgeCriteria'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'AgeMinimum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Age', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'AgeMaximum'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'Gender'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'EthnicityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'PopulationCriteriaDemographic', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATIONCRITERIADEMOGRAPHIC ( END ) 


