-- DBO.[ENTITYCORRESPONDENCEIMAGE] (BEGIN) 

/* 
IF EXISTS (SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE (TABLE_NAME = 'EntityCorrespondenceImage')  AND (TABLE_SCHEMA = 'dbo'))
  DROP TABLE dbo.[EntityCorrespondenceImage]
GO 
*/ 


CREATE TABLE dbo.[EntityCorrespondenceImage] (
    EntityCorrespondenceId                                                    BIGINT  NOT NULL,

    EntityCorrespondenceImageName                                            VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondenceImage_Dft_EntityCorrespondenceImageName] DEFAULT (''),
    EntityCorrespondenceImageExtension                                       VARCHAR (0006) NOT NULL    CONSTRAINT [EntityCorrespondenceImage_Dft_EntityCorrespondenceImageExtension] DEFAULT (''),
    EntityCorrespondenceImageMimeType                                        VARCHAR (0060) NOT NULL    CONSTRAINT [EntityCorrespondenceImage_Dft_EntityCorrespondenceImageMimeType] DEFAULT (''),

    EntityCorrespondenceImageIsCompressed                                        BIT  NOT NULL    CONSTRAINT [EntityCorrespondenceImage_Dft_EntityCorrespondenceImageIsCompressed] DEFAULT (0),

    EntityCorrespondenceImageData                                     VARBINARY(MAX)      NULL

  , CONSTRAINT [EntityCorrespondenceImage_Pk] PRIMARY KEY (EntityCorrespondenceId)

  , CONSTRAINT [EntityCorrespondenceImage_Fk_EntityCorrespondenceId] FOREIGN KEY (EntityCorrespondenceId) REFERENCES dbo.[EntityCorrespondence] (EntityCorrespondenceId)

  ) -- dbo.EntityCorrespondenceImage

GO

/* 
-- COLUMN DESCRIPTIONS 
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique Identifier', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceId'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceImageName'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'File Extension', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceImageExtension'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Name', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceImageMimeType'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'True/False Boolean', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceImageIsCompressed'
  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Binary Image', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'EntityCorrespondenceImage', @level2type=N'COLUMN', @level2name=N'EntityCorrespondenceImageData'

GO
*/ 

-- DBO.ENTITYCORRESPONDENCEIMAGE ( END ) 


