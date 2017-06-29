/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueue_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueue_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkQueue_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueId                BIGINT,
      @workQueueName             VARCHAR (060),
      @workQueueDescription      VARCHAR (999),
      @workflowId                BIGINT,

      @scheduleValue              INT,
      @scheduleQualifier          INT,
      @thresholdValue             INT,
      @thresholdQualifier         INT,
      
      @initialConstraintValue     INT,
      @initialConstraintQualifier INT,
      @initialMilestoneValue      INT,
      @initialMilestoneQualifier  INT,
      
      @getWorkViewId           BIGINT,
      @getWorkUseGrouping         BIT,
            
      @enabled                    BIT,
      @visible                    BIT,
      
      @extendedProperties         XML,

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
        
        SET @scheduleValue = CASE WHEN (@scheduleValue < 0) THEN 0 ELSE @scheduleValue END
        
        SET @thresholdValue = CASE WHEN (@thresholdValue < 0) THEN 0 ELSE @thresholdValue END
        
        SET @initialConstraintValue = CASE WHEN (@initialConstraintValue < 0) THEN 0 ELSE @initialConstraintValue END
        
        SET @initialMilestoneValue = CASE WHEN (@initialMilestoneValue < 0) THEN 0 ELSE @initialMilestoneValue END

        IF EXISTS (SELECT * FROM dbo.WorkQueue WHERE WorkQueueId = @workQueueId AND @workQueueId <> 0)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkQueue
              SET
                WorkQueueName = @workQueueName,
                WorkQueueDescription = @workQueueDescription,
                WorkflowId = @workflowId,

                ScheduleValue = @scheduleValue,
                ScheduleQualifier = @scheduleQualifier,
                ThresholdValue = @thresholdValue,
                ThresholdQualifier = @thresholdQualifier,
                
                InitialConstraintValue     = @initialConstraintValue,
                InitialConstraintQualifier = @initialConstraintQualifier,              
                InitialMilestoneValue      = @initialMilestoneValue,
                InitialMilestoneQualifier  = @initialMilestoneQualifier,
                
                GetWorkViewId = @getWorkViewId,
                GetWorkUseGrouping = @getWorkUseGrouping,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkQueueId = @workQueueId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.WorkQueue (
                WorkQueueName, WorkQueueDescription, WorkflowId, 
                
                ScheduleValue, ScheduleQualifier, ThresholdValue, ThresholdQualifier,
                
                InitialConstraintValue, InitialConstraintQualifier, InitialMilestoneValue, InitialMilestoneQualifier,
                
                GetWorkViewId, GetWorkUseGrouping,

                Enabled, Visible, ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workQueueName, @workQueueDescription, @workflowId, 
                
                @scheduleValue, @scheduleQualifier, @thresholdValue, @thresholdQualifier,
                
                @initialConstraintValue, @initialConstraintQualifier, @initialMilestoneValue, @initialMilestoneQualifier,
                
                @getWorkViewId, @getWorkUseGrouping,

                @enabled, @visible, @extendedProperties,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/