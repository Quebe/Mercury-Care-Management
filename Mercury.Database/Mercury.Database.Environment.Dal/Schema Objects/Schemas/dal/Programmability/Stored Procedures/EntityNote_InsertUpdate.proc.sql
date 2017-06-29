
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_InsertUpdate
GO      
*/

CREATE PROCEDURE dal.EntityNote_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityNoteId             BIGINT,
      @entityId                 BIGINT,
      
      @relatedEntityId          BIGINT,
      
      @relatedEntityType           INT,           
      @relatedEntityObjectId    BIGINT,
      
      @relatedObjectType       VARCHAR (120),
      @relatedObjectId          BIGINT,      
      
      @importance                  INT,
      @noteTypeId               BIGINT,
      @subject                 VARCHAR (120),
      
      @effectiveDate          DATETIME,
      @terminationDate        DATETIME,
      
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
        
        IF EXISTS (SELECT * FROM dbo.EntityNote WHERE EntityNoteId = @entityNoteId AND @entityNoteId <>0) 
        
          BEGIN -- UPDATE EXISTING INTERNAL (MERCURY) NOTE 
          
            UPDATE dbo.EntityNote 
            
              SET 
                            
                EntityId = @entityId, 
                
                RelatedEntityId = @relatedEntityId,
                
                RelatedEntityType = @relatedEntityType, 
                
                RelatedEntityObjectId = @relatedEntityObjectId, 
                
                RelatedObjectType = @relatedObjectType, 
                
                RelatedObjectId = @relatedObjectId,
                
                Importance = @importance, 
                
                NoteTypeId = @noteTypeId, 
                
                Subject = @subject, 
                
                EffectiveDate = @effectiveDate, 
                
                TerminationDate = @terminationDate,
                               
                ModifiedAuthorityName = @modifiedAuthorityName, 
                
                ModifiedAccountId = @modifiedAccountId, 
                
                ModifiedAccountName = @modifiedAccountName, 
                
                ModifiedDate = GETDATE ()
                
              WHERE EntityNoteId = @entityNoteId
                
          
          END
          
        ELSE IF (@entityNoteId = 0) -- ONLY INSERT NEW NOTES, DON'T UPDATE EXTERNAL NOTES
        
          BEGIN 

            INSERT INTO dbo.EntityNote (
            
                EntityId, RelatedEntityId, RelatedEntityType, RelatedEntityObjectId, RelatedObjectType, RelatedObjectId,
                
                DataSource, Importance, NoteTypeId, Subject, EffectiveDate, TerminationDate,

                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
            
                @entityId, @relatedEntityId, @relatedEntityType, @relatedEntityObjectId, @relatedObjectType, @relatedObjectId,
                
                'MERCURY', @importance, @noteTypeId, @subject, @effectiveDate, @terminationDate,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
           END
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityNote_Insert TO PUBLIC
GO          
*/