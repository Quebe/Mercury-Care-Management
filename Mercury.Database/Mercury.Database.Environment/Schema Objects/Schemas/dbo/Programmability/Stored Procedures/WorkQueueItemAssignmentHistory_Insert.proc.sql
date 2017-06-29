/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItemAssignmentHistory_Insert' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueItemAssignmentHistory_Insert
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItemAssignmentHistory_Insert
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueItemId         BIGINT,
       
      
      @assignedFromWorkQueueId                    BIGINT,
      @assignedToWorkQueueId                      BIGINT, 
       
      @assignedToSecurityAuthorityId         BIGINT,
      @assignedToUserAccountId      VARCHAR (060),
      @assignedToUserAccountName    VARCHAR (060),
      @assignedToUserDisplayName    VARCHAR (060),     
      @assignedToDate                DATETIME,
      
      @assignmentSource                VARCHAR (060),

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
        
        INSERT INTO WorkQueueItemAssignmentHistory (
        
            WorkQueueItemId,
            
            AssignedFromWorkQueueId, AssignedToWorkQueueId,

            AssignedToSecurityAuthorityId, AssignedToUserAccountId, AssignedToUserAccountName, AssignedToUserDisplayName,  AssignedToDate, AssignmentSource,
                
            CreateAuthorityName, CreateAccountId, CreateAccountName, CreateDate,
                
            ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate
            
          ) VALUES (
          
            @workQueueItemId, 
            
            @assignedFromWorkQueueId, @assignedToWorkQueueId,
            
            @assignedToSecurityAuthorityId, @assignedToUserAccountId, @assignedToUserAccountName, @assignedToUserDisplayName, ISNULL (@assignedToDate, GETDATE ()), ISNULL (@assignmentSource, 'Not Specified'),
                
            @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),

            @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()
            
          );
        
    END        