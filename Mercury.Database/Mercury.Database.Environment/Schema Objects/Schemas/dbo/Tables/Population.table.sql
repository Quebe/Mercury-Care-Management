-- DBO.[POPULATION] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'Population')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[Population]
GO 
*/ 


CREATE TABLE dbo.[Population] (
    PopulationId                                                              BIGINT  NOT NULL    IDENTITY (1000000000, 1),
    PopulationName                                                           VARCHAR (0060) NOT NULL,
    PopulationDescription                                                    VARCHAR (0999) NOT NULL    CONSTRAINT [Population_Dft_PopulationDescription] DEFAULT (''),
    PopulationTypeId                                                          BIGINT  NOT NULL    CONSTRAINT [Population_Dft_PopulationTypeId] DEFAULT (0),

    AllowProspective                                                             BIT  NOT NULL    CONSTRAINT [Population_Dft_AllowProspective] DEFAULT (0),
    InitialAnchorDate                                                            INT  NOT NULL    CONSTRAINT [Population_Dft_InitialAnchorDate] DEFAULT (0),

    OnBeforeMembershipAddWorkflowId                                           BIGINT      NULL,
    OnMembershipAddActionId                                                   BIGINT  NOT NULL    CONSTRAINT [Population_Dft_OnMembershipAddActionId] DEFAULT (0),
    OnMembershipAddActionParameters                                              XML  NOT NULL    CONSTRAINT [Population_Dft_OnMembershipAddActionParameters] DEFAULT (''),
    OnMembershipAddActionDescription                                         VARCHAR (0999) NOT NULL    CONSTRAINT [Population_Dft_OnMembershipAddActionDescription] DEFAULT (''),

    OnBeforeMembershipTerminateWorkflowId                                     BIGINT      NULL,
    OnMembershipTerminateActionId                                             BIGINT  NOT NULL    CONSTRAINT [Population_Dft_OnMembershipTerminateActionId] DEFAULT (0),
    OnMembershipTerminateActionParameters                                        XML  NOT NULL    CONSTRAINT [Population_Dft_OnMembershipTerminateActionParameters] DEFAULT (''),
    OnMembershipTerminateActionDescription                                   VARCHAR (0999) NOT NULL    CONSTRAINT [Population_Dft_OnMembershipTerminateActionDescription] DEFAULT (''),

    ExtendedProperties                                                           XML      NULL,

    Enabled                                                                      BIT  NOT NULL    CONSTRAINT [Population_Dft_Enabled] DEFAULT (1),
    Visible                                                                      BIT  NOT NULL    CONSTRAINT [Population_Dft_Visible] DEFAULT (1),

    CreateAuthorityName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_CreateAuthorityName] DEFAULT ('SQL Server'),
    CreateAccountId                                                          VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_CreateAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    CreateAccountName                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_CreateAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    CreateDate                                                              DATETIME  NOT NULL    CONSTRAINT [Population_Dft_CreateDate] DEFAULT (GETDATE ()),

    ModifiedAuthorityName                                                    VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_ModifiedAuthorityName] DEFAULT ('SQL Server'),
    ModifiedAccountId                                                        VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_ModifiedAccountId] DEFAULT (CAST (ISNULL (SUSER_ID (), SUSER_NAME ()) AS VARCHAR (060))),
    ModifiedAccountName                                                      VARCHAR (0060) NOT NULL    CONSTRAINT [Population_Dft_ModifiedAccountName] DEFAULT (CAST (SUSER_NAME () AS VARCHAR (060))),
    ModifiedDate                                                            DATETIME  NOT NULL    CONSTRAINT [Population_Dft_ModifiedDate] DEFAULT (GETDATE ())

  , CONSTRAINT [Population_Pk] PRIMARY KEY (PopulationId)

  , CONSTRAINT [Population_Fk_PopulationTypeId] FOREIGN KEY (PopulationTypeId) REFERENCES dbo.[PopulationType] (PopulationTypeId)

  , CONSTRAINT [Population_Fk_OnBeforeMembershipAddWorkflowId] FOREIGN KEY (OnBeforeMembershipAddWorkflowId) REFERENCES dbo.[Workflow] (WorkflowId)

  , CONSTRAINT [Population_Fk_OnBeforeMembershipTerminateWorkflowId] FOREIGN KEY (OnBeforeMembershipTerminateWorkflowId) REFERENCES dbo.[Workflow] (WorkflowId)

  , CONSTRAINT [Population_Unq_PopulationName] UNIQUE (PopulationName)

  ) -- dbo.Population

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'PopulationId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'PopulationName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'PopulationDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'PopulationTypeId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean [Allow Prospective Member Enrollments]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'AllowProspective'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Type [Enumeration]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'InitialAnchorDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnBeforeMembershipAddWorkflowId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipAddActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipAddActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipAddActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnBeforeMembershipTerminateWorkflowId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipTerminateActionId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'XML Stored Data', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipTerminateActionParameters'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Description', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'OnMembershipTerminateActionDescription'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ExtendedProperties', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'ExtendedProperties'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Enabled/Disabled Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'Enabled'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Visible/Not Visible Toggle', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'Visible'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'CreateAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'CreateAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'CreateAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Create Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'CreateDate'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [Security Authority Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'ModifiedAuthorityName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique User Account ID', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'ModifiedAccountId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name [User Account Name]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'ModifiedAccountName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Date and Time [Modified Date]', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Population', @level2type=N'COLUMN', @level2name=N'ModifiedDate'

GO
*/ 

-- DBO.POPULATION ( END ) 


