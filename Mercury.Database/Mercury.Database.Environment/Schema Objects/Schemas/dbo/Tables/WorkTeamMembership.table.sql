-- DBO.[WORKTEAMMEMBERSHIP] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'WorkTeamMembership')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[WorkTeamMembership]
GO 
*/ 


CREATE TABLE dbo.[WorkTeamMembership] (
    WorkTeamId                                                                BIGINT  NOT NULL,
    SecurityAuthorityId                                                       BIGINT  NOT NULL,
    UserAccountId                                                            VARCHAR (0060) NOT NULL,
    UserAccountName                                                          VARCHAR (0060) NOT NULL,
    UserDisplayName                                                          VARCHAR (0060) NOT NULL,
    WorkTeamRole                                                                 INT  NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_WorkTeamRole] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [WorkTeamMembership_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [WorkTeamMembership_Pk] PRIMARY KEY (WorkTeamId, SecurityAuthorityId, UserAccountId)

  , CONSTRAINT [WorkTeamMembership_Fk_WorkTeamId] FOREIGN KEY (WorkTeamId) REFERENCES dbo.[WorkTeam] (WorkTeamId)

  , CONSTRAINT [WorkTeamMembership_Fk_WorkTeamRole] FOREIGN KEY (WorkTeamRole) REFERENCES enum.[WorkTeamRole] (WorkTeamRole)

  ) -- dbo.WorkTeamMembership

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'WorkTeamId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier [From Enterprise Security Authority]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'SecurityAuthorityId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'UserAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'UserAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'UserDisplayName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'WorkTeamRole'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'WorkTeamMembership', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.WORKTEAMMEMBERSHIP ( END ) 


