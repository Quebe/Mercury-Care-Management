
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNoteContent_Insert' AND type = 'P'))
  DROP PROCEDURE dal.EntityNoteContent_Insert
GO      
*/

CREATE PROCEDURE dal.EntityNoteContent_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityNoteId             BIGINT,
      @content                    TEXT,
      
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
        
        INSERT INTO dbo.EntityNoteContent (
        
            EntityNoteId, Content,
            
            CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
            
            ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
            
        VALUES (
        
            @entityNoteId, @content,
            
            @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
            
            @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityNoteContent_Insert TO PUBLIC
GO          
*/