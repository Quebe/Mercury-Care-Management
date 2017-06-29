/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_AssignTo' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItem_AssignTo
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_AssignTo
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemId         BIGINT,

      @assignedToSecurityAuthorityId         BIGINT,
      @assignedToUserAccountId      VARCHAR (060),
      @assignedToUserAccountName    VARCHAR (060),
      @assignedToUserDisplayName    VARCHAR (060),
      
      @assignmentSource VARCHAR (060),
      
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
        
        DECLARE @assignmentDate AS DATETIME
        
        DECLARE @workQueueId AS BIGINT
        
        SET @assignmentDate = GETDATE ()
        
        /* LOCAL VARIABLES ( END ) */
        
        UPDATE WorkQueueItem 
        
          SET 
          
            AssignedToSecurityAuthorityId = @assignedToSecurityAuthorityId,
            
            AssignedToUserAccountId       = @assignedToUserAccountId,
            
            AssignedToUserAccountName     = @assignedToUserAccountName,
            
            AssignedToUserDisplayName     = @assignedToUserDisplayName,

            AssignedToDate                = @assignmentDate,


            ModifiedAuthorityName = @modifiedAuthorityName,
            
            ModifiedAccountId     = @modifiedAccountId,
            
            ModifiedAccountName   = @modifiedAccountName,
            
            ModifiedDate          = GETDATE ()
                
          WHERE WorkQueueItemId = @workQueueItemId
          
          
        SELECT @workQueueId = WorkQueueId FROM WorkQueueItem WHERE WorkQueueItemId = @workQueueItemId
          
   
        EXEC WorkQueueItemAssignmentHistory_Insert @workQueueItemId, @workQueueId, @workQueueId, @assignedToSecurityAuthorityId, @assignedToUserAccountId, 
        
          @assignedToUserAccountName, @assignedToUserDisplayName, @assignmentDate, @assignmentSource,
        
          @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName   
   
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_AssignTo TO PUBLIC
GO          
*/