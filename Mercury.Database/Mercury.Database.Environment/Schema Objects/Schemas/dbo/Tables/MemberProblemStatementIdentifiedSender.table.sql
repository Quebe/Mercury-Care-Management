-- DBO.[MEMBERPROBLEMSTATEMENTIDENTIFIEDSENDER] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberProblemStatementIdentifiedSender')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberProblemStatementIdentifiedSender]
GO 
*/ 


CREATE TABLE dbo.[MemberProblemStatementIdentifiedSender] (
    MemberProblemStatementIdentifiedSenderId                                  BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    MemberProblemStatementIdentifiedId                                        BIGINT  NOT NULL,
    SenderObjectType                                                         VARCHAR (0120) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_SenderObjectType] DEFAULT (''),
    SenderObjectId                                                            BIGINT  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_SenderObjectId] DEFAULT (0),
    EventObjectType                                                          VARCHAR (0120) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_EventObjectType] DEFAULT (''),
    EventObjectId                                                             BIGINT  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_EventObjectId] DEFAULT (0),
    EventInstanceId                                                           BIGINT  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_EventInstanceId] DEFAULT (0),
    EventDescription                                                         VARCHAR (0999) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_EventDescription] DEFAULT (''),
    Priority                                                                     INT  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_Priority] DEFAULT (0),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberProblemStatementIdentifiedSender_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberProblemStatementIdentifiedSender_Pk] PRIMARY KEY (MemberProblemStatementIdentifiedSenderId)

  , CONSTRAINT [MemberProblemStatementIdentifiedSender_Fk_MemberProblemStatementIdentifiedId] FOREIGN KEY (MemberProblemStatementIdentifiedId) REFERENCES dbo.[MemberProblemStatementIdentified] (MemberProblemStatementIdentifiedId)

  ) -- dbo.MemberProblemStatementIdentifiedSender

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'MemberProblemStatementIdentifiedSenderId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'MemberProblemStatementIdentifiedId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'SenderObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'SenderObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Object Type Name String', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'EventObjectType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'EventObjectId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'EventInstanceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'EventDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Priority', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'Priority'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberProblemStatementIdentifiedSender', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERPROBLEMSTATEMENTIDENTIFIEDSENDER ( END ) 


