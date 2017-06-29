/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ConditionCriteriaEvent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ConditionCriteriaEvent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ConditionCriteriaEvent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @criteriaEventId          BIGINT,
      @conditionId             BIGINT,
      @eventType                   INT,
      @serviceId                BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.ConditionCriteriaEvent WHERE ConditionCriteriaEventId = @criteriaEventId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ConditionCriteriaEvent
              SET
                EventType = @eventType,
                ServiceId = @serviceId,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ConditionCriteriaEventId = @criteriaEventId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ConditionCriteriaEvent (ConditionId, EventType, ServiceId,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @conditionId, @eventType, @serviceId,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ConditionCriteriaEvent_InsertUpdate TO PUBLIC
GO          
*/