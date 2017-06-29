/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Member_SelectByEntityId' AND type = 'P'))
  DROP PROCEDURE dal.Member_SelectByEntityId
GO      
*/

CREATE PROCEDURE dal.Member_SelectByEntityId
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
        
        SELECT * FROM dbo.Member WHERE EntityId = @entityId 

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.Member_SelectByEntityId TO PUBLIC
GO          
*/