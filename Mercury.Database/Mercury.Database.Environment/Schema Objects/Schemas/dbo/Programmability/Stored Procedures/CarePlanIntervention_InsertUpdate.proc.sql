/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CarePlanIntervention_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CarePlanIntervention_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CarePlanIntervention_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @carePlanInterventionId               BIGINT,
      @carePlanInterventionName            VARCHAR (060),
      @carePlanInterventionDescription     VARCHAR (999),
      
      @carePlanGoalId                  BIGINT,
      @careInterventionId          BIGINT,
      
      @inclusion INT, 
      
      @extendedProperties          XML,
      
      @enabled                       BIT,
      @visible                       BIT,
            
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

        IF EXISTS (SELECT * FROM dbo.CarePlanIntervention WHERE ((CarePlanInterventionId = @carePlanInterventionId) AND (@carePlanInterventionId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CarePlanIntervention
              SET
                CarePlanInterventionName = @carePlanInterventionName,
                CarePlanInterventionDescription = @carePlanInterventionDescription,
                
                CarePlanGoalId = @carePlanGoalId,
                CareInterventionId = @careInterventionId,
                
                Inclusion = @inclusion,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CarePlanInterventionId = @carePlanInterventionId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CarePlanIntervention (CarePlanInterventionName, CarePlanInterventionDescription, 
            
								CarePlanGoalId, CareInterventionId,
								
								Inclusion,
            
								ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @carePlanInterventionName, @carePlanInterventionDescription, 
                
                @carePlanGoalId, @careInterventionId,
                
                @inclusion,
                
                @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CarePlanIntervention_InsertUpdate TO PUBLIC
GO          
*/