/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CorrespondenceContent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CorrespondenceContent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CorrespondenceContent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @correspondenceContentId                BIGINT,
      @correspondenceContentName             VARCHAR (060),
      @correspondenceContentPath             VARCHAR (255),

			@correspondenceId                BIGINT,
			@contentSequence                    INT,
			@contentType                        INT,

			@reportingServerId               BIGINT,
      @isAttachmentCompressed     BIT,
      
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.CorrespondenceContent WHERE CorrespondenceContentId = @correspondenceContentId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CorrespondenceContent
              SET
                CorrespondenceContentName = @correspondenceContentName,
                CorrespondenceContentPath = @correspondenceContentPath,
                
                CorrespondenceId = @correspondenceId,
                ContentSequence = @contentSequence,
                ContentType = @contentType,
                
                ReportingServerId = @reportingServerId,
                IsAttachmentCompressed = @isattachmentCompressed,

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CorrespondenceContentId = @correspondenceContentId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CorrespondenceContent (
                CorrespondenceContentName, CorrespondenceContentPath,
                
                CorrespondenceId, ContentSequence, ContentType, ReportingServerId, IsAttachmentCompressed,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @correspondenceContentName, @correspondenceContentPath,
                
                @correspondenceId, @contentSequence, @contentType, @reportingServerId, @isAttachmentCompressed,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/