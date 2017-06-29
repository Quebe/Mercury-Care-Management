/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Entity_SelectMember' AND type = 'P'))
  DROP PROCEDURE dal.Entity_SelectMember
GO      
*/

CREATE PROCEDURE dal.Entity_SelectMember
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
        
        /* LOCAL VARIABLES ( END ) */
        
        SELECT * FROM dbo.Entity WHERE EntityId = @entityId

    END        