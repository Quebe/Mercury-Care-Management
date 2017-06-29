/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContact_CountByEntity' AND type = 'P'))
  DROP PROCEDURE dal.EntityContact_CountByEntity
GO      
*/

CREATE PROCEDURE dal.EntityContact_CountByEntity
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId            BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @contactCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @contactCount = COUNT (1)
         
            FROM EntityContact WHERE EntityId = @entityId
                
        RETURN @contactCount
              
    END    
              
    