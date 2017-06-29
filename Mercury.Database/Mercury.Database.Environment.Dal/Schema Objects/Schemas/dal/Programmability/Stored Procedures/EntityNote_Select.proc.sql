/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_Select' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_Select
GO      
*/

CREATE PROCEDURE dal.EntityNote_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityNoteId        BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
                
        SELECT * FROM EntityNote WHERE EntityNoteId = @entityNoteId
        
    END        