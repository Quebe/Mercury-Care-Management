/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Correspondence_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Correspondence_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Correspondence_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @correspondenceId                BIGINT,
      @correspondenceName             VARCHAR (060),
      @correspondenceDescription      VARCHAR (999),
      
      @version                 DECIMAL (20, 08),
      @formId                  BIGINT,
      
      @storeImage                 BIT,

      @enabled                    BIT,
      @visible                    BIT,

      @extendedProperties         XML,
      
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

        IF EXISTS (SELECT * FROM dbo.Correspondence WHERE CorrespondenceId = @correspondenceId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Correspondence
              SET
                CorrespondenceName = @correspondenceName,
                CorrespondenceDescription = @correspondenceDescription,

                Version = @version,
                FormId = @formId,
                
                StoreImage = @storeImage,

                Enabled = @enabled,
                Visible = @visible,

                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CorrespondenceId = @correspondenceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Correspondence (
                CorrespondenceName, CorrespondenceDescription, Version, FormId, StoreImage, Enabled, Visible, 
                
                ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @correspondenceName, @correspondenceDescription, @version, @formId, @storeImage, @enabled, @visible, 
                
                @extendedProperties,

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