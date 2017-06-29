/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'LabResult_Select' AND type = 'P'))
  DROP PROCEDURE dal.LabResult_Select
GO      
*/

CREATE PROCEDURE dal.LabResult_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @labResultId            BIGINT
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

        FROM LabResult
    	  
        WHERE (LabResult.LabResultId = @labResultId)
            
	    END    
              
