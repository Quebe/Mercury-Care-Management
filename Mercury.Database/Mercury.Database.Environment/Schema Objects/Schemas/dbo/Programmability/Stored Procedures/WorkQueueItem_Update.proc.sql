/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_Update' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItem_Update
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_Update
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemId         BIGINT,
      @workQueueId             BIGINT,
       
      @itemObjectType            VARCHAR (060),
      @itemObjectId               BIGINT,
      @workQueueItemName         VARCHAR (060),
      @workQueueItemDescription  VARCHAR (099),
      @itemGroupKey              VARCHAR (060),
      
      @workflowInstanceId     VARCHAR (040),
      @workflowStatus         VARCHAR (020),
      @workflowLastStep       VARCHAR (060),
      @workflowNextStep       VARCHAR (060),

      @addedDate              DATETIME,
      @lastWorkedDate         DATETIME,
      @constraintDate         DATETIME,
      @milestoneDate          DATETIME,
      @thresholdDate          DATETIME,
      @dueDate                DATETIME,
      @completionDate         DATETIME,
      @workOutcomeId            BIGINT,
      
      @workTimeRestrictions        XML,

      @assignedToSecurityAuthorityId         BIGINT,
      @assignedToUserAccountId      VARCHAR (060),
      @assignedToUserAccountName    VARCHAR (060),
      @assignedToUserDisplayName    VARCHAR (060),
      @assignedToDate                DATETIME,
      
      @extendedProperties XML,

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
        
        IF EXISTS (SELECT * FROM WorkQueueItem WHERE WorkQueueItemId = @workQueueItemId) 
        
          BEGIN 
        
            UPDATE WorkQueueItem
            
              SET 
              
                WorkQueueId = @workQueueId, 
                
                ItemObjectType = @itemObjectType,
                
                ItemObjectId = @itemObjectId,
                
				WorkQueueItemName = @workQueueItemName,
				
				WorkQueueItemDescription = @workQueueItemDescription,
                
                ItemGroupKey = @itemGroupKey,
                
                
                WorkflowInstanceId = @workflowInstanceId,
                
                WorkflowStatus = @workflowStatus,
                
                WorkflowLastStep = @workflowLastStep,
                
                WorkflowNextStep = @workflowNextStep,
                                
                
                AddedDate = @addedDate, 
                
                LastWorkedDate = @lastWorkedDate,
                
                ConstraintDate = @constraintDate,
                
                MilestoneDate = CASE WHEN (@milestoneDate < @constraintDate) THEN @constraintDate ELSE @milestoneDate END,
                
                ThresholdDate = @thresholdDate,
                
                DueDate = @dueDate,
                
                CompletionDate = @completionDate,
                
                WorkOutcomeId = @workOutcomeId,
                
                WorkTimeRestrictions = @workTimeRestrictions,
                
                
                AssignedToSecurityAuthorityId = @assignedToSecurityAuthorityId,
                
                AssignedToUserAccountId = @assignedToUserAccountId,
                
                AssignedToUserAccountName = @assignedToUserAccountName,
                
                AssignedToUserDisplayName = @assignedToUserDisplayName,
                
                AssignedToDate = @assignedToDate,
                
                
                ExtendedProperties = @extendedProperties,
                
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                
                ModifiedAccountId     = @modifiedAccountId,
                
                ModifiedAccountName   = @modifiedAccountName,
                
                ModifiedDate          = GETDATE ()
                
              WHERE WorkQueueItemId = @workQueueItemId
              
                  
            DECLARE @itemPriority AS INT

            SELECT @itemPriority = CASE WHEN (SUM (WorkQueueItemSender.Priority) > 100) THEN 100 ELSE SUM (WorkQueueItemSender.Priority) END
                        
              FROM WorkQueueItem JOIN WorkQueueItemSender ON WorkQueueItem.WorkQueueItemId = WorkQueueItemSender.WorkQueueItemId
              
              WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId

                              
              UPDATE WorkQueueItem
              
                SET WorkQueueItem.Priority = ISNULL (@itemPriority, 0)
                
                WHERE WorkQueueItem.WorkQueueItemId = @workQueueItemId
                          
          END           
   
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_Update TO PUBLIC
GO          
*/