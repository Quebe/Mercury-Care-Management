-- DBO.[MEMBERENROLLMENTPCP] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberEnrollmentPcp')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberEnrollmentPcp]
GO 
*/ 


CREATE TABLE dbo.[MemberEnrollmentPcp] (
    MemberEnrollmentPcpId                                                     BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberEnrollmentId                                                        BIGINT  NOT NULL,
    PcpProviderId                                                             BIGINT  NOT NULL,
    ProviderAffiliationId                                                     BIGINT  NOT NULL,
    PcpServiceLocationId                                                      BIGINT      NULL,

    EffectiveDate                                                           DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_EffectiveDate] DEFAULT (GETDATE ()),
    TerminationDate                                                         DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_TerminationDate] DEFAULT (CAST ('12/31/9999' AS DATETIME)),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberEnrollmentPcp_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberEnrollmentPcp_Pk] PRIMARY KEY (MemberEnrollmentPcpId)

  , CONSTRAINT [MemberEnrollmentPcp_Fk_MemberEnrollmentId] FOREIGN KEY (MemberEnrollmentId) REFERENCES dbo.[MemberEnrollment] (MemberEnrollmentId)

  ) -- dbo.MemberEnrollmentPcp

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentPcpId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Reference Enrollment]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'MemberEnrollmentId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Reference Provider]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'PcpProviderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Reference Provider Affiliation]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'ProviderAffiliationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [Reference Service Location]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'PcpServiceLocationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Effective Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'EffectiveDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Termination Date', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'TerminationDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberEnrollmentPcp', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERENROLLMENTPCP ( END ) 


