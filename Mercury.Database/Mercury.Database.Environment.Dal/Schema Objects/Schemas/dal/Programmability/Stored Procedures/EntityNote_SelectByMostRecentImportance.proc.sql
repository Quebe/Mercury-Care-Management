/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_SelectByMostRecentImportance' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_SelectByMostRecentImportance
GO      
*/

CREATE PROCEDURE dal.EntityNote_SelectByMostRecentImportance
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId             BIGINT,
      @importance              INT
      
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
			
				AND Importance = @importance
				
			ORDER BY TerminationDate DESC, EffectiveDate DESC, CreateDate DESC

	    END    
              
