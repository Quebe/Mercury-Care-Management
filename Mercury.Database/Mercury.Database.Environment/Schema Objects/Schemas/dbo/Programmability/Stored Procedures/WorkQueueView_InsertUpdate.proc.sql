/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueView_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkQueueView_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkQueueView_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workQueueViewId                BIGINT,
      @workQueueViewName             VARCHAR (060),
      @workQueueViewDescription      VARCHAR (999),
     
      @enabled                    BIT,
      @visible                    BIT,
      
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
        
        IF EXISTS (SELECT * FROM dbo.WorkQueueView WHERE ((WorkQueueViewId = @workQueueViewId) AND (@workQueueViewId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkQueueView
              SET
                WorkQueueViewName = @workQueueViewName,
                WorkQueueViewDescription = @workQueueViewDescription,               

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkQueueViewId = @workQueueViewId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.WorkQueueView (
                WorkQueueViewName, WorkQueueViewDescription,
                
                Enabled, Visible,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @workQueueViewName, @workQueueViewDescription,
                
                @enabled, @visible,

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