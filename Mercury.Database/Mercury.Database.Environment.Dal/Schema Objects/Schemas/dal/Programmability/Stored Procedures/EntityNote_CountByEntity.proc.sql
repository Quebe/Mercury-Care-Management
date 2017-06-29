/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityNote_CountByEntity' AND type = 'P'))
  DROP PROCEDURE dal.EntityNote_CountByEntity
GO      
*/

CREATE PROCEDURE dal.EntityNote_CountByEntity
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
        
        DECLARE @noteCount AS INT

        /* LOCAL VARIABLES ( END ) */

				SELECT @noteCount = COUNT (1) FROM EntityNote WHERE EntityId = @entityId
                
        RETURN @noteCount
              
    END    
              
    