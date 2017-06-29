/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityDocument_CountByEntity' AND type = 'P'))
  DROP PROCEDURE dal.EntityDocument_CountByEntity
GO      
*/

CREATE PROCEDURE dal.EntityDocument_CountByEntity
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
        
        DECLARE @count AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @count = COUNT (1)
         
            FROM (

                SELECT 1 AS RowInstance

                  FROM dbo.EntityForm 
                     
                  WHERE EntityForm.EntityId = @entityId
                  
                UNION ALL SELECT 1 AS RowInstance
                  
                  FROM dbo.EntityCorrespondence

                  WHERE EntityCorrespondence.EntityId = @entityId
                  
              ) AS EntityDocument
                  
        RETURN @count
              
    END    
              
    