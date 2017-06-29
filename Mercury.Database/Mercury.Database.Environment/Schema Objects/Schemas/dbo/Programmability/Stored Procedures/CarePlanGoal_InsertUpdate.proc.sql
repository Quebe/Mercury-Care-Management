/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CarePlanGoal_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CarePlanGoal_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CarePlanGoal_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @carePlanGoalId               BIGINT,
      @carePlanGoalName            VARCHAR (060),
      @carePlanGoalDescription     VARCHAR (999),
      
      @carePlanId                  BIGINT,
			@inclusion                      INT,
      @clinicalNarrative           VARCHAR (999),
      @commonNarrative             VARCHAR (999),
      
      @goalTimeframe               INT,
      @scheduleValue               INT,
      @scheduleQualifier           INT,
      @careMeasureId           BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CarePlanGoal WHERE ((CarePlanGoalId = @carePlanGoalId) AND (@carePlanGoalId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CarePlanGoal
              SET
                CarePlanGoalName = @carePlanGoalName,
                CarePlanGoalDescription = @carePlanGoalDescription,
                
                CarePlanId = @carePlanId,
								Inclusion = @inclusion,
                
                ClinicalNarrative = @clinicalNarrative,
                CommonNarrative = @commonNarrative,
                
                GoalTimeframe = @goalTimeframe,
                ScheduleValue = @scheduleValue,
                ScheduleQualifier = @scheduleQualifier,
                CareMeasureId = @careMeasureId,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CarePlanGoalId = @carePlanGoalId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CarePlanGoal (CarePlanGoalName, CarePlanGoalDescription, 
            
								CarePlanId, Inclusion, ClinicalNarrative, CommonNarrative, GoalTimeframe, ScheduleValue, ScheduleQualifier, CareMeasureId,
            
								ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @carePlanGoalName, @carePlanGoalDescription, 
                
                @carePlanId, @inclusion, @clinicalNarrative, @commonNarrative, @goalTimeframe, @scheduleValue, @scheduleQualifier, @careMeasureId,
                
                @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CarePlanGoal_InsertUpdate TO PUBLIC
GO          
*/