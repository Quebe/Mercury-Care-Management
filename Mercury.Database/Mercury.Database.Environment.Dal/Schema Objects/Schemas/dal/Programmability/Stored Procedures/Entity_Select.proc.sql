/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Entity_Select' AND type = 'P'))
  DROP PROCEDURE dal.Entity_Select
GO      
*/

CREATE PROCEDURE dal.Entity_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        SELECT * FROM dbo.Entity WHERE EntityId = @entityId

    END        