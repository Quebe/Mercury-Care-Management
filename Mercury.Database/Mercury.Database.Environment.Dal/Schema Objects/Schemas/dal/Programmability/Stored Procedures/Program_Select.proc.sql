/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Program_Select' AND type = 'P'))
  DROP PROCEDURE dal.Program_Select
GO      
*/

CREATE PROCEDURE dal.Program_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @programId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.Program WHERE ProgramId = @programId
              
    END        