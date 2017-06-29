/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ServiceSetDefinition_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ServiceSetDefinition_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ServiceSetDefinition_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceSetDefinitionId  BIGINT,
      @serviceId               BIGINT,
      @definitionServiceId     BIGINT,
      @enabled                    BIT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        IF EXISTS (SELECT ServiceSetDefinitionId FROM dbo.ServiceSetDefinition WHERE ServiceSetDefinitionId = @serviceSetDefinitionId)
        
          BEGIN
            
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ServiceSetDefinition 
              SET 
                ServiceId = @serviceId,  
                DefinitionServiceId = @definitionServiceId,
                Enabled = @enabled
                
              WHERE ServiceSetDefinitionId = @serviceSetDefinitionId;
              
          END 

          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN

            INSERT INTO dbo.ServiceSetDefinition (
                ServiceId, DefinitionServiceId, Enabled
              )
                
            VALUES (
                @serviceId, @definitionServiceId, @enabled
              )
              
          END
               
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ServiceSetDefinition_InsertUpdate TO PUBLIC
GO          
*/