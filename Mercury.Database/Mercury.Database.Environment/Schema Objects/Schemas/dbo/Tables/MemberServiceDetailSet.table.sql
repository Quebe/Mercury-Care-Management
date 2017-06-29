-- DBO.[MEMBERSERVICEDETAILSET] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'MemberServiceDetailSet')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[MemberServiceDetailSet]
GO 
*/ 


CREATE TABLE dbo.[MemberServiceDetailSet] (
    MemberServiceId                                                           BIGINT  NOT NULL,
    ServiceSetDefinitionId                                                    BIGINT  NOT NULL,
    DetailMemberServiceId                                                     BIGINT  NOT NULL,
    MemberId                                                                  BIGINT  NOT NULL,
    ParentServiceId                                                           BIGINT  NOT NULL,

    ServiceId                                                                 BIGINT  NOT NULL,
    ServiceName                                                              VARCHAR (0060) NOT NULL,
    ServiceType                                                                  INT  NOT NULL,

    EventDate                                                               DATETIME  NOT NULL,

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [MemberServiceDetailSet_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [MemberServiceDetailSet_Fk_MemberServiceId] FOREIGN KEY (MemberServiceId) REFERENCES dbo.[MemberService] (MemberServiceId)

  , CONSTRAINT [MemberServiceDetailSet_Fk_ServiceSetDefinitionId] FOREIGN KEY (ServiceSetDefinitionId) REFERENCES dbo.[ServiceSetDefinition] (ServiceSetDefinitionId)

  , CONSTRAINT [MemberServiceDetailSet_Fk_DetailMemberServiceId] FOREIGN KEY (DetailMemberServiceId) REFERENCES dbo.[MemberService] (MemberServiceId)

  , CONSTRAINT [MemberServiceDetailSet_Fk_MemberId] FOREIGN KEY (MemberId) REFERENCES dbo.[Member] (MemberId)

  , CONSTRAINT [MemberServiceDetailSet_Fk_ParentServiceId] FOREIGN KEY (ParentServiceId) REFERENCES dbo.[Service] (ServiceId)

  , CONSTRAINT [MemberServiceDetailSet_Fk_ServiceId] FOREIGN KEY (ServiceId) REFERENCES dbo.[Service] (ServiceId)

  ) -- dbo.MemberServiceDetailSet

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'MemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ServiceSetDefinitionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'DetailMemberServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'MemberId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ParentServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ServiceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ServiceName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enumeration', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ServiceType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'EventDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'MemberServiceDetailSet', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.MEMBERSERVICEDETAILSET ( END ) 


