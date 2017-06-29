
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationTriggerEvent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationTriggerEvent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationTriggerEvent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @triggerEventId           BIGINT,
      @populationId             BIGINT,
      @eventType                   INT,
      
      @serviceId                BIGINT,

      @metricType                  INT,
      @metricId                 BIGINT,
      @metricMinimum           DECIMAL (20, 08),
      @metricMaximum           DECIMAL (20, 08),      
      
      @authorizedServiceId      BIGINT,
      
      @problemStatementId       BIGINT,

      @actionId                    INT,
      @actionParameters            XML,
      @actionDescription      VARCHAR (099),
      
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

        IF EXISTS (SELECT * FROM dbo.PopulationTriggerEvent WHERE PopulationTriggerEventId = @triggerEventId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationTriggerEvent
              SET
                TriggerEventType = @eventType,
                ServiceId = @serviceId,
                MetricType = @metricType,
                MetricId   = @metricId,
                MetricMinimum = @metricMinimum,                
                MetricMaximum = @metricMaximum,
                AuthorizedServiceId = @authorizedServiceId,
                
                ProblemStatementId = @problemStatementId,
                
                ActionId   = @actionId,
                ActionParameters = @actionParameters,
                ActionDescription = @actionDescription,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationTriggerEventId = @triggerEventId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.PopulationTriggerEvent (
                PopulationId, TriggerEventType, ServiceId, MetricType, MetricId, MetricMinimum, MetricMaximum, AuthorizedServiceId, ProblemStatementId, ActionId, ActionParameters, ActionDescription,
    
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationId, @eventType, @serviceId, @metricType, @metricId, @metricMinimum, @metricMaximum, @authorizedServiceId, @problemStatementId, @actionId, @actionParameters, @actionDescription,


                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationTriggerEvent_InsertUpdate TO PUBLIC
GO          
*/