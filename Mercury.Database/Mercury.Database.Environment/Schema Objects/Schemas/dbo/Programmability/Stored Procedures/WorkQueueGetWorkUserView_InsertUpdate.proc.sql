/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueGetWorkUserView_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueGetWorkUserView_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkQueueGetWorkUserView_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueId              BIGINT,
      @securityAuthorityId     BIGINT,
      @userAccountId          VARCHAR (060),
      
      @userAccountName        VARCHAR (060),
      @userDisplayName        VARCHAR (060),
      @workQueueViewId            BIGINT,
    
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

        IF EXISTS (SELECT * FROM dbo.WorkQueueGetWorkUserView WHERE WorkQueueId = @workQueueId AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkQueueGetWorkUserView
              SET
                UserAccountName = @userAccountName,
                UserDisplayName = @userDisplayName,
                
                WorkQueueViewId = @workQueueViewId,
              
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkQueueId = @workQueueId AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO WorkQueueGetWorkUserView (
                WorkQueueId, SecurityAuthorityId, UserAccountId, UserAccountName, UserDisplayName, WorkQueueViewId,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workQueueId, @securityAuthorityId, @userAccountId, @userAccountName, @userDisplayName, @workQueueViewId,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.WorkQueueGetWorkUserView_InsertUpdate TO PUBLIC
GO          
*/