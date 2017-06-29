-- DBO.[PROBLEMSTATEMENT] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'ProblemStatement')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[ProblemStatement]
GO 
*/ 


CREATE TABLE dbo.[ProblemStatement] (
    ProblemStatementId                                                        BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    ProblemStatementName                                                     VARCHAR (0060) NOT NULL,
    ProblemStatementDescription                                              VARCHAR (0999) NOT NULL,

    ProblemDomainId                                                           BIGINT  NOT NULL,
    ProblemClassId                                                            BIGINT      NULL,

    DefiningCharacteristics                                                  VARCHAR (0999) NOT NULL    CONSTRAINT [ProblemStatement_Dft_DefiningCharacteristics] DEFAULT (''),
    RelatedFactors                                                           VARCHAR (0999) NOT NULL    CONSTRAINT [ProblemStatement_Dft_RelatedFactors] DEFAULT (''),

    DefaultCarePlanId                                                         BIGINT      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [ProblemStatement_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [ProblemStatement_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [ProblemStatement_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [ProblemStatement_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [ProblemStatement_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [ProblemStatement_Pk] PRIMARY KEY (ProblemStatementId)

  , CONSTRAINT [ProblemStatement_Fk_ProblemDomainId] FOREIGN KEY (ProblemDomainId) REFERENCES dbo.[ProblemDomain] (ProblemDomainId)

  , CONSTRAINT [ProblemStatement_Fk_ProblemClassId] FOREIGN KEY (ProblemClassId) REFERENCES dbo.[ProblemClass] (ProblemClassId)

  , CONSTRAINT [ProblemStatement_Fk_DefaultCarePlanId] FOREIGN KEY (DefaultCarePlanId) REFERENCES dbo.[CarePlan] (CarePlanId)

  ) -- dbo.ProblemStatement

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ProblemStatementId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ProblemStatementName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ProblemStatementDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ProblemDomainId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ProblemClassId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'DefiningCharacteristics'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'RelatedFactors'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'DefaultCarePlanId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'ProblemStatement', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.PROBLEMSTATEMENT ( END ) 


