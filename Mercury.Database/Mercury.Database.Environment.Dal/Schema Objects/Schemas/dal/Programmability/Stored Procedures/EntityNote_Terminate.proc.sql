/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_Terminate' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_Terminate
GO      
*/

CREATE PROCEDURE dal.EntityNote_Terminate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityNoteId             BIGINT,
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
        
        IF EXISTS (SELECT * FROM dbo.EntityNote WHERE EntityNoteId = @entityNoteId AND @entityNoteId <> 0) 
        
          BEGIN -- UPDATE EXISTING INTERNAL (MERCURY) NOTE 
          
            UPDATE dbo.EntityNote 
            
              SET 
                                            
                TerminationDate = @terminationDate,
                               
                ModifiedAuthorityName = @modifiedAuthorityName, 
                
                ModifiedAccountId = @modifiedAccountId, 
                
                ModifiedAccountName = @modifiedAccountName, 
                
                ModifiedDate = GETDATE ()
                
              WHERE EntityNoteId = @entityNoteId AND (@terminationDate >= EffectiveDate)
                
          
          END
                     
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityNote_Insert TO PUBLIC
GO          
*/