/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_SelectByObjectMostRecent' AND type = 'P'))
  DROP PROCEDURE WorkQueueItem_SelectByObjectMostRecent
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_SelectByObjectMostRecent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueId                BIGINT,
      @itemObjectType            VARCHAR (60),
      @itemObjectId			      BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT WorkQueueItemMostRecent.*
                  
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY ISNULL (CompletionDate, '12/31/9999') DESC, AddedDate, WorkQueueItemId) AS RowNumber, 
            
                WorkQueueItem.*
           
              FROM WorkQueueItem
              
              WHERE ((WorkQueueId = @workQueueId) AND (ItemObjectType = @itemObjectType) AND (ItemObjectId = @itemObjectId))
              
            ) AS WorkQueueItemMostRecent
            
          WHERE WorkQueueItemMostRecent.RowNumber = 1
                     
    END    
              
    