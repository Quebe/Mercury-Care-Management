/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItemSender_CopyToWorkQueueItem' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItemSender_CopyToWorkQueueItem
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItemSender_CopyToWorkQueueItem
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemSenderId     BIGINT,      -- SOURCE SENDER ID
      @workQueueItemId           BIGINT,      -- DESTINATION WORK QUEUE ITEM

      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.WorkQueueItemSender WHERE WorkQueueItemSenderId = @workQueueItemSenderId)
        
        IF EXISTS (SELECT * FROM dbo.WorkQueueItem WHERE WorkQueueItemId = @workQueueItemId) 
        
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            INSERT INTO WorkQueueItemSender (WorkQueueItemId,
            
                SenderObjectType, SenderObjectId, EventObjectType, EventObjectId, EventInstanceId, EventDescription, Priority, 
                
                CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, 
                
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
              
              SELECT @workQueueItemId, 
              
                  SenderObjectType, SenderObjectId, EventObjectType, EventObjectId, EventInstanceId, EventDescription, Priority, 
                  
                  CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate, 
                  
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()
                
                FROM WorkQueueItemSender WHERE WorkQueueItemSenderId = @workQueueItemSenderId
                
                

          DECLARE @itemPriority AS INT

          SELECT @itemPriority = CASE WHEN (SUM (WorkQueueItemSender.Priority) > 100) THEN 100 ELSE SUM (WorkQueueItemSender.Priority) END
                      
            FROM WorkQueueItem JOIN WorkQueueItemSender ON WorkQueueItem.WorkQueueItemId = WorkQueueItemSender.WorkQueueItemId
            
            WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId

                            
            UPDATE WorkQueueItem
            
              SET WorkQueueItem.Priority = @itemPriority
              
              WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId
                          
          END
                  
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_CopyToWorkQueueItem TO PUBLIC
GO          
*/