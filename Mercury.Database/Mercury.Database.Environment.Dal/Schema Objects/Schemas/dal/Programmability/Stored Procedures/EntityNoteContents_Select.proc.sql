/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_SelectByEntityPage' AND type = 'P'))
  DROP PROCEDURE dal.EntityNoteContents_Select
GO      
*/

CREATE PROCEDURE dal.EntityNoteContents_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityNoteId         BIGINT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        SELECT * FROM EntityNoteContent WHERE EntityNoteId = @entityNoteId ORDER BY CreateDate
                 
    END    
              
