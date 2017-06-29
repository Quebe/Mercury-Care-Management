/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'InsurerReference_Select' AND type = 'P'))
  DROP PROCEDURE dal.InsurerReference_Select
GO      
*/

CREATE PROCEDURE dal.InsurerReference_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
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
        
        FROM dbo.Insurer AS Insurer 
        
          JOIN dbo.Entity AS Entity ON Insurer.EntityId = Entity.EntityId
      
        ORDER BY EntityName
      
    END        