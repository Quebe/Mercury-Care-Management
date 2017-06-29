/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContact_SelectByEntityPage' AND type = 'P'))
  DROP PROCEDURE dal.EntityContact_SelectByEntityPage
GO      
*/

CREATE PROCEDURE dal.EntityContact_SelectByEntityPage
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

            SELECT ROW_NUMBER () OVER (ORDER BY ContactDate DESC) AS RowNumber, *

              FROM (SELECT * FROM dbo.EntityContact WHERE EntityId = @entityId) AS EntityContact

            ) AS EntityContactPage

        WHERE EntityContactPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)

	    END    
              
