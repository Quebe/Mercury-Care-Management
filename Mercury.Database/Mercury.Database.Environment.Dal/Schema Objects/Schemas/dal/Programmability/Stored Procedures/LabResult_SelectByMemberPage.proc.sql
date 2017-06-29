/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'LabResult_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE dal.LabResult_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dal.LabResult_SelectByMemberPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
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

        SELECT 

					LabResult.*

	      FROM (

          SELECT ROW_NUMBER () OVER (ORDER BY ServiceDate DESC, LabResultId DESC) AS RowNumber, 
          
              LabResultPage.*
                  
            FROM dbo.LabResult AS LabResultPage
        	  
            WHERE (LabResultPage.MemberId = @memberId)
            
          ) AS LabResult
            
        WHERE LabResult.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
        
	    END    
              
