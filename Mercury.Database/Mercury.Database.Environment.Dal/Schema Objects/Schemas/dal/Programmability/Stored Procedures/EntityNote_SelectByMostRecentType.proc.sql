/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_SelectByMostRecentType' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_SelectByMostRecentType
GO      
*/

CREATE PROCEDURE dal.EntityNote_SelectByMostRecentType
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId             BIGINT,
      @noteTypeId           BIGINT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
                
        SELECT TOP 1 *
        
			FROM dbo.EntityNote
			
			WHERE EntityId = @entityId
			
				AND NoteTypeId = @noteTypeId
				
			ORDER BY TerminationDate DESC, EffectiveDate DESC, CreateDate DESC

	    END    
              
