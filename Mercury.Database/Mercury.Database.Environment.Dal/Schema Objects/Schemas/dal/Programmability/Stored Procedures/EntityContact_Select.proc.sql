/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContact_Select' AND type = 'P'))
  DROP PROCEDURE dal.EntityContact_Select
GO      
*/

CREATE PROCEDURE dal.EntityContact_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityContactId      BIGINT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  
  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
              
        SELECT * FROM dbo.EntityContact WHERE EntityContactId = @entityContactId

	END    
              
