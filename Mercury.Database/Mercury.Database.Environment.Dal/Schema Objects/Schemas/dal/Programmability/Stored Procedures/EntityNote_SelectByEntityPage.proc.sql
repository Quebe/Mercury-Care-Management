/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_SelectByEntityPage' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_SelectByEntityPage
GO      
*/

CREATE PROCEDURE dal.EntityNote_SelectByEntityPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId             BIGINT,
      @initialRow              INT,
      @count                   INT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
                
        SELECT *

          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY TerminationDate DESC, EffectiveDate DESC) AS RowNumber, *

              FROM (SELECT * FROM EntityNote WHERE EntityId = @entityId) AS EntityNote

            ) AS EntityNotePage

        WHERE EntityNotePage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
        
        ORDER BY RowNumber

	    END    
              
