
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CarePlanActivity_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CarePlanActivity_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CarePlanActivity_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @carePlanActivityId               BIGINT,
      @carePlanActivityName            VARCHAR (060),
      @carePlanActivityDescription     VARCHAR (999),
      
      @carePlanId BIGINT,
      @carePlanActivityType INT,
      @clinicalNarrative           VARCHAR (999),
      @commonNarrative             VARCHAR (999),
      
      @activityType INT,
      @isReoccurring BIT,
      @initialAnchorDate INT,
      @anchorDate INT,
      
      @scheduleType INT,
      @scheduleValue INT,
      @scheduleQualifier INT,
      @constraintValue INT,
      @constraintQualifier INT,
      
      @performActionDate INT,
      @actionId BIGINT,
      @actionParameters XML,
      @actionDescription VARCHAR (0099),
            
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

        IF EXISTS (SELECT * FROM dbo.CarePlanActivity WHERE ((CarePlanActivityId = @carePlanActivityId) AND (@carePlanActivityId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CarePlanActivity
              SET
                CarePlanActivityName = @carePlanActivityName,
                CarePlanActivityDescription = @carePlanActivityDescription,

								CarePlanId = @carePlanId,
								CarePlanActivityType = @carePlanActivityType,
                ClinicalNarrative = @clinicalNarrative,
                CommonNarrative = @commonNarrative,
                
								
								ActivityType = @activityType,
								IsReoccurring = @isReoccurring,
								InitialAnchorDate = @initialAnchorDate,
								AnchorDate = @anchorDate,
								
								ScheduleType = @scheduleType,
								ScheduleValue = @scheduleValue,
								ScheduleQualifier = @scheduleQualifier,
								ConstraintValue = @constraintValue,
								ConstraintQualifier = @constraintQualifier,
								
								PerformActionDate = @performActionDate,
								ActionId = @actionId,
								ActionParameters = @actionParameters,
								ActionDescription = @actionDescription,
								
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CarePlanActivityId = @carePlanActivityId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CarePlanActivity (CarePlanActivityName, CarePlanActivityDescription, 
            
							  CarePlanId, CarePlanActivityType, ClinicalNarrative, CommonNarrative, 
							  
							  ActivityType, IsReoccurring, InitialAnchorDate, AnchorDate,
							  
							  ScheduleType, ScheduleValue, ScheduleQualifier, ConstraintValue, ConstraintQualifier,
							  
							  PerformActionDate, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @carePlanActivityName, @carePlanActivityDescription,

							  @carePlanId, @carePlanActivityType, @clinicalNarrative, @commonNarrative,
							  
							  @activityType, @isReoccurring, @initialAnchorDate, @anchorDate,
							  
							  @scheduleType, @scheduleValue, @scheduleQualifier, @constraintValue, @constraintQualifier,
							  
							  @performActionDate, @actionId, @actionParameters, @actionDescription,
							  
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CarePlanActivity_InsertUpdate TO PUBLIC
GO          
*/