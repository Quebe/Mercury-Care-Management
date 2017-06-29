-- DBO.[ROLEPERMISSION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'RolePermission')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[RolePermission]
GO 
*/ 


CREATE TABLE dbo.[RolePermission] (
    RoleId                                                                    BIGINT  NOT NULL,
    PermissionId                                                              BIGINT  NOT NULL,

    IsGranted                                                                    BIT  NOT NULL    CONSTRAINT [RolePermission_Dft_IsGranted] DEFAULT (0),
    IsDenied                                                                     BIT  NOT NULL    CONSTRAINT [RolePermission_Dft_IsDenied] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [RolePermission_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [RolePermission_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [RolePermission_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [RolePermission_Pk] PRIMARY KEY (RoleId, PermissionId)

  , CONSTRAINT [RolePermission_Fk_RoleId] FOREIGN KEY (RoleId) REFERENCES dbo.[Role] (RoleId)

  , CONSTRAINT [RolePermission_Fk_PermissionId] FOREIGN KEY (PermissionId) REFERENCES dbo.[Permission] (PermissionId)

  ) -- dbo.RolePermission

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'RoleId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'PermissionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'IsGranted'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'IsDenied'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'RolePermission', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.ROLEPERMISSION ( END ) 


