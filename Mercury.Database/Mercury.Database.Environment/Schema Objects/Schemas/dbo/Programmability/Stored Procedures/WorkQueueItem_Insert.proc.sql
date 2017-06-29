/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_Insert' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItem_Insert
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueId             BIGINT,
       
      @itemObjectType            VARCHAR (060),
      @itemObjectId               BIGINT,
      @workQueueItemName         VARCHAR (060),
      @workQueueItemDescription  VARCHAR (099),
      @itemGroupKey              VARCHAR (060),
                         
      @senderObjectType       VARCHAR (120),
      @senderObjectId          BIGINT,
      @eventObjectType        VARCHAR (120),
      @eventObjectId           BIGINT,
      @eventInstanceId         BIGINT,
      @eventDescription       VARCHAR (999),      
      
      @priority                  INT,
      @workTimeRestrictions      XML,

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
        
        DECLARE @workQueueItemId AS BIGINT

        /* LOCAL VARIABLES ( END ) */
        
        SELECT @workQueueItemId = WorkQueueItemId 
        
           FROM WorkQueueItem 
           
           WHERE WorkQueueId = @workQueueId AND ItemObjectType = @itemObjectType AND ItemObjectId = @itemObjectId AND CompletionDate IS NULL
           
        
        IF (@workQueueItemId IS NULL) 
        
          BEGIN
          
			      DECLARE @currentDate AS DATETIME
          
            DECLARE @dueDate AS DATETIME
            
            DECLARE @constraintDate AS DATETIME
            
            DECLARE @milestoneDate AS DATETIME
            
            DECLARE @thresholdDate AS DATETIME
            
            SET @currentDate = CAST (CONVERT (CHAR (10), GETDATE(), 101) AS DATETIME)
            
            SELECT 
            
                @constraintDate =
               
                  CASE 
                      WHEN (InitialConstraintQualifier = 0) THEN DATEADD (DAY,   InitialConstraintValue, @currentDate)
                      WHEN (InitialConstraintQualifier = 2) THEN DATEADD (YEAR,  InitialConstraintValue, @currentDate)
                      ELSE DATEADD (MONTH, InitialConstraintValue, @currentDate)
                    END,
                
                @milestoneDate =
               
                  CASE 
                      WHEN (InitialMilestoneQualifier = 0) THEN DATEADD (DAY,   InitialMilestoneValue, @currentDate)
                      WHEN (InitialMilestoneQualifier = 2) THEN DATEADD (YEAR,  InitialMilestoneValue, @currentDate)
                      ELSE DATEADD (MONTH, InitialMilestoneValue, @currentDate)
                    END,

                @thresholdDate =
               
                  CASE 
                      WHEN (ThresholdQualifier = 0) THEN DATEADD (DAY,   ThresholdValue, @currentDate)
                      WHEN (ThresholdQualifier = 2) THEN DATEADD (YEAR,  ThresholdValue, @currentDate)
                      ELSE DATEADD (MONTH, ThresholdValue, @currentDate)
                    END,

                @dueDate =
               
                  CASE 
                      WHEN (ScheduleQualifier = 0) THEN DATEADD (DAY,   ScheduleValue, @currentDate)
                      WHEN (ScheduleQualifier = 2) THEN DATEADD (YEAR,  ScheduleValue, @currentDate)
                      ELSE DATEADD (MONTH, ScheduleValue, @currentDate)
                    END

              FROM WorkQueue WHERE WorkQueueId = @workQueueId
              
            SET @milestoneDate = CASE WHEN (@milestoneDate > @dueDate) THEN @dueDate ELSE @milestoneDate END
              
            SET @constraintDate = CASE WHEN (@constraintDate > @milestoneDate) THEN @milestoneDate ELSE @constraintDate END
            
            SET @thresholdDate = CASE WHEN (@thresholdDate > @dueDate) THEN @dueDate ELSE @thresholdDate END
            

            IF (@dueDate IS NOT NULL) 
              
              BEGIN

                INSERT INTO dbo.WorkQueueItem (
                    WorkQueueId, ItemObjectType, ItemObjectId, WorkQueueItemName, WorkQueueItemDescription, ItemGroupKey, 
                    
                    ConstraintDate, MilestoneDate, ThresholdDate, DueDate,
                    
                    WorkTimeRestrictions,
                    
                    ExtendedProperties,
                
                    CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                    ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                    
                VALUES (
                    @workQueueId, @itemObjectType, @itemObjectId, @workQueueItemName, @workQueueItemDescription, @itemGroupKey, 
                    
                    @constraintDate, @milestoneDate, @thresholdDate, @dueDate,
                    
                    @workTimeRestrictions,
                    
                    '<ExtendedProperties />',

                    @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                    @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
                    
                SET @workQueueItemId = @@IDENTITY
                
              END
            
          END
          
          
        IF (@workQueueItemId IS NOT NULL) 
        
          BEGIN
        
            INSERT INTO dbo.WorkQueueItemSender (
                  WorkQueueItemId, SenderObjectType, SenderObjectId, EventObjectType, EventObjectId, EventInstanceId, EventDescription, Priority,

                  CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                  ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                  
              VALUES (
                  @workQueueItemId, @senderObjectType, @senderObjectId , @eventObjectType, @eventObjectId, @eventInstanceId, @eventDescription, @priority,

                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                  @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
                  
                  
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
GRANT EXECUTE ON dbo.Document_Insert TO PUBLIC
GO          
*/