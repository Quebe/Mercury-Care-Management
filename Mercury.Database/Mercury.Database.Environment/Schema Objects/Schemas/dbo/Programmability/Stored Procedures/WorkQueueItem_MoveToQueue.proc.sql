/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_MoveToQueue' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItem_MoveToQueue
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_MoveToQueue
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemId         BIGINT,
      
      @destinationQueueId      BIGINT,
    
      @assignmentSource       VARCHAR (060),

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
        
            DECLARE @workQueueId AS BIGINT
            
            DECLARE @assignmentDate AS DATETIME
            
         
      			DECLARE @currentDate AS DATETIME
          
            DECLARE @dueDate AS DATETIME
            
            DECLARE @constraintDate AS DATETIME
            
            DECLARE @milestoneDate AS DATETIME
            
            DECLARE @thresholdDate AS DATETIME
            
            SELECT @currentDate = CONVERT (DATE, WorkQueueItem.AddedDate) FROM WorkQueueItem WHERE WorkQueueItemId = @workQueueItemId
            
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

              FROM WorkQueue WHERE WorkQueueId = @destinationQueueId
              
            -- SET @milestoneDate = CASE WHEN (@milestoneDate > @dueDate) THEN @dueDate ELSE @milestoneDate END
              
            SET @constraintDate = CASE WHEN (@constraintDate > @milestoneDate) THEN @milestoneDate ELSE @constraintDate END
            
            SET @thresholdDate = CASE WHEN (@thresholdDate > @dueDate) THEN @dueDate ELSE @thresholdDate END
            
            
            SELECT @workQueueId = WorkQueueId FROM WorkQueueItem WHERE WorkQueueItemId = @workQueueItemId 
            
            
            UPDATE WorkQueueItem 
            
              SET 
              
                WorkQueueId = @destinationQueueId,
                
                ConstraintDate = @constraintDate,
                
                MilestoneDate = @milestoneDate,
                
                ThresholdDate = @thresholdDate,
                
                DueDate = @dueDate,
                
                AssignedToSecurityAuthorityId = 0,
                
                AssignedToUserAccountId = '',
                
                AssignedToUserAccountName = '** Not Assigned',
                
                AssignedToUserDisplayName = '** Not Assigned',
                
                AssignedToDate = NULL
                
                
                
              WHERE WorkQueueItemId = @workQueueItemId
                    
      
            SET @assignmentDate = GETDATE ()                    
                    
            EXEC WorkQueueItemAssignmentHistory_Insert @workQueueItemId, @workQueueId, @destinationQueueId, 0, '', 
            
              '** Not Assigned', '** Not Assigned', @assignmentDate, @assignmentSource,
            
              @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName   
       
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_MoveToQueue TO PUBLIC
GO          
*/