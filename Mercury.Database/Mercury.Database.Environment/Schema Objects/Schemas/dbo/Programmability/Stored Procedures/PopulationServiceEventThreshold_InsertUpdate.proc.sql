
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationServiceEventThreshold_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationServiceEventThreshold_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationServiceEventThreshold_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceEventThresholdId           BIGINT,
      @serviceEventId                    BIGINT,
      @populationId                      BIGINT,
      @relativeDateValue                    INT,
      @relativeDateQualifier                INT,
      @status                               INT,
      @actionId                             INT,
      @actionParameters                     XML,
      @actionDescription                VARCHAR (099)
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.PopulationServiceEventThreshold WHERE PopulationServiceEventThresholdId = @serviceEventThresholdId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationServiceEventThreshold
              SET
                PopulationServiceEventId = @serviceEventId,
                PopulationId = @populationId,
                RelativeDateValue = @relativeDateValue,
                RelativeDateQualifier = @relativeDateQualifier,
                Status = @status,
                ActionId = @actionId,
                ActionParameters = @actionParameters,
                ActionDescription = @actionDescription
                
             WHERE PopulationServiceEventThresholdId = @serviceEventThresholdId
              
              
          END

        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
              
            INSERT INTO dbo.PopulationServiceEventThreshold (
                PopulationServiceEventId, PopulationId, RelativeDateValue, RelativeDateQualifier, Status, ActionId, ActionParameters, ActionDescription
                
              )
            
            VALUES (
                @serviceEventId, @populationId, @relativeDateValue, @relativeDateQualifier, @status, @actionId, @actionParameters, @actionDescription
            
              )
              
          END
               
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationServiceEventThreshold_InsertUpdate TO PUBLIC
GO          
*/