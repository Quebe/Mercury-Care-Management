/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItemSender_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItemSender_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItemSender_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemSenderId     BIGINT,
      @workQueueItemId           BIGINT,
                        
      @senderObjectType       VARCHAR (120),
      @senderObjectId          BIGINT,
      @eventObjectType        VARCHAR (120),
      @eventObjectId           BIGINT,
      @eventInstanceId         BIGINT,
      @eventDescription       VARCHAR (999),  
      @priority                   INT,
     
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
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkQueueItemSender
              SET
              
                WorkQueueItemId = @workQueueItemId, 
                
                SenderObjectType = @senderObjectType,
                
                SenderObjectId = @senderObjectId, 
                
                EventObjectType = @eventObjectType, 
                
                EventObjectId = @eventObjectId, 
                
                EventInstanceId = @eventInstanceId, 
                
                EventDescription = @eventDescription, 

                Priority = CASE WHEN (@priority < 0) THEN 0 WHEN (@priority > 100) THEN 100 ELSE @priority END, 

                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkQueueItemSenderId = @workQueueItemSenderId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.WorkQueueItemSender (
                WorkQueueItemId, SenderObjectType, SenderObjectId, EventObjectType, EventObjectId, EventInstanceId, EventDescription, 
                
                Priority,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workQueueItemId, @senderObjectType, @senderObjectId , @eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, 
                
                CASE WHEN (@priority < 0) THEN 0 WHEN (@priority > 100) THEN 100 ELSE @priority END, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
  
          DECLARE @itemPriority AS INT

          SELECT @itemPriority = CASE WHEN (SUM (WorkQueueItemSender.Priority) > 100) THEN 100 ELSE SUM (WorkQueueItemSender.Priority) END
                      
            FROM WorkQueueItem JOIN WorkQueueItemSender ON WorkQueueItem.WorkQueueItemId = WorkQueueItemSender.WorkQueueItemId
            
            WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId

                            
            UPDATE WorkQueueItem
            
              SET WorkQueueItem.Priority = @itemPriority
              
              WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId
                          
                  
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/