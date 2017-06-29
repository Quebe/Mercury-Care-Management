-- DBO.[MEMBERAUTHORIZEDSERVICE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberAuthorizedService')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberAuthorizedService]
GO 
*/ 


CREATE TABLE dbo.[MemberAuthorizedService] (
    MemberAuthorizedServiceId                                                 BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberId                                                                  BIGINT  NOT NULL,
    AuthorizedServiceId                                                       BIGINT  NOT NULL,

    EventDate                                                               DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_EventDate] DEFAULT ('01/01/1900'),
    InitialIdentifiedDate                                                   DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_InitialIdentifiedDate] DEFAULT ('01/01/1900'),
    AddedManually                                                                BIT  NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_AddedManually] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberAuthorizedService_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberAuthorizedService_Pk] PRIMARY KEY (MemberAuthorizedServiceId)

  , CONSTRAINT [MemberAuthorizedService_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberAuthorizedService_Fk_AuthorizedServiceId] FOREIGN KEY (AuthorizedServiceId) REFERENCES dbo.[AuthorizedService] (AuthorizedServiceId)

  ) -- dbo.MemberAuthorizedService

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'MemberAuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'AuthorizedServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'InitialIdentifiedDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'AddedManually'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberAuthorizedService', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERAUTHORIZEDSERVICE ( END ) 


