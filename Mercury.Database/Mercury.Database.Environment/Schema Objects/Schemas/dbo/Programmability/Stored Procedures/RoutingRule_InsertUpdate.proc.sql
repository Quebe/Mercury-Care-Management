
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'RoutingRule_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.RoutingRule_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.RoutingRule_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @routingRuleId              BIGINT,
      @routingRuleName           VARCHAR (060),
      @routingRuleDescription    VARCHAR (999),
      @defaultWorkQueueId         BIGINT,

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

        IF EXISTS (SELECT * FROM dbo.RoutingRule WHERE RoutingRuleId = @routingRuleId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.RoutingRule
              SET
                RoutingRuleName = @routingRuleName,
                RoutingRuleDescription = @routingRuleDescription,
                DefaultWorkQueueId = @defaultWorkQueueId,

                Enabled = @enabled,
                Visible = @visible,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                RoutingRuleId = @routingRuleId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.RoutingRule (
                RoutingRuleName, RoutingRuleDescription, DefaultWorkQueueId, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @routingRuleName, @routingRuleDescription, @defaultWorkQueueId, @enabled, @visible, 

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