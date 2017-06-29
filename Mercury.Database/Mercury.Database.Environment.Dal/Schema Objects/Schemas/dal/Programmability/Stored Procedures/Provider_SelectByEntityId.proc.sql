/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Provider_SelectByEntityId' AND type = 'P'))
  DROP PROCEDURE dal.Provider_SelectByEntityId
GO      
*/

CREATE PROCEDURE dal.Provider_SelectByEntityId
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
        
        SELECT * FROM dbo.Provider WHERE Provider.EntityId = @entityId

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.Provider_SelectByEntityId TO PUBLIC
GO          
*/