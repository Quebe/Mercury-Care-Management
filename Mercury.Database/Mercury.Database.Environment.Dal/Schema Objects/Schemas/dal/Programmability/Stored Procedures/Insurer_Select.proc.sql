/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Insurer_Select' AND type = 'P'))
  DROP PROCEDURE dal.Insurer_Select
GO      
*/

CREATE PROCEDURE dal.Insurer_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @insurerId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.Insurer WHERE InsurerId = @insurerId
        
    END        