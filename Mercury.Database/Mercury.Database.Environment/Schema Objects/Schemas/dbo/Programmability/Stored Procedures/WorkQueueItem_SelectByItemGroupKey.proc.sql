/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueItem_SelectByItemGroupKey' AND type = 'P'))
  DROP PROCEDURE WorkQueueItem_SelectByItemGroupKey
GO      
*/

CREATE PROCEDURE dbo.WorkQueueItem_SelectByItemGroupKey
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueId                    BIGINT,
      @itemGroupKey                  VARCHAR (060),
    
      @assignedToSecurityAuthorityId  BIGINT,
      @assignedToUserAccountId       VARCHAR (060),
      
      @ignoreAssignment                  BIT
      
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        IF (LEN (RTRIM (LTRIM (@itemGroupKey))) > 0) 
        
          BEGIN

            IF (@ignoreAssignment = 1) -- PULL ITEMS WITH GROUP KEY INDEPENDENT OF WHO IS ASSIGNED

              BEGIN
              
                SELECT * FROM WorkQueueItem WHERE (WorkQueueId = @workQueueId) AND (ItemGroupKey = @itemGroupKey) AND (CompletionDate IS NULL)
                
              END
              
            ELSE -- UNASSIGNED AND ASSIGNED TO USER ONLY
            
              BEGIN 
              
                SELECT * FROM WorkQueueItem
                
                  WHERE (WorkQueueId = @workQueueId) AND (ItemGroupKey = @itemGroupKey) 
                  
                    AND (CompletionDate IS NULL)
                  
                    AND (((AssignedToSecurityAuthorityId = 0) AND (AssignedToUserAccountId = '')) 
                    
                      OR ((AssignedToSecurityAuthorityId = @assignedToSecurityAuthorityId) AND (AssignedToUserAccountId = @assignedToUserAccountId)))
                       
                       
               END
               
           END
           
         ELSE SELECT TOP 0 * FROM WorkQueueItem
         
    END    
              
    