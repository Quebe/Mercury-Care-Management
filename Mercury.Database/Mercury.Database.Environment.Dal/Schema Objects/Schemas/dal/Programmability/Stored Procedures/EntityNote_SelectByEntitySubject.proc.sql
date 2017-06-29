/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_SelectByEntitySubject' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_SelectByEntitySubject
GO      
*/

CREATE PROCEDURE dal.EntityNote_SelectByEntitySubject
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId             BIGINT,
      @subject             VARCHAR (120)
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
				SELECT * FROM EntityNote WHERE EntityId = @entityId AND (Subject = @subject)
          
	    END    
              
