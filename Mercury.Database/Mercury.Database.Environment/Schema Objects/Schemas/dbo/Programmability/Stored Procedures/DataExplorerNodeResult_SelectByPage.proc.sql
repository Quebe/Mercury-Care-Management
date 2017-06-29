/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeResult_SelectByPage' AND type = 'P'))
  DROP PROCEDURE DataExplorerNodeResult_SelectByPage
GO      
*/

CREATE PROCEDURE dbo.DataExplorerNodeResult_SelectByPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @nodeInstanceId            UNIQUEIDENTIFIER,
      @initialRow             INT,
      @count                  INT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT DataExplorerNodeResultPage.* 
          
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY Id) AS RowNumber, 
            
								DataExplorerNodeResult.*
           
              FROM DataExplorerNodeResult
              
              WHERE DataExplorerNodeInstanceId = @nodeInstanceId
              
            ) AS DataExplorerNodeResultPage

          WHERE DataExplorerNodeResultPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    