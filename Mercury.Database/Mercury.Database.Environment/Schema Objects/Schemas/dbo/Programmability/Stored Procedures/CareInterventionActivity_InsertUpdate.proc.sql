
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareInterventionActivity_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareInterventionActivity_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareInterventionActivity_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careInterventionActivityId               BIGINT,
      @careInterventionActivityName            VARCHAR (060),
      @careInterventionActivityDescription     VARCHAR (999),
      
      @careInterventionId BIGINT,
      @careInterventionActivityType INT,
      @clinicalNarrative           VARCHAR (8000),
      @commonNarrative             VARCHAR (8000),
      
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

        IF EXISTS (SELECT * FROM dbo.CareInterventionActivity WHERE ((CareInterventionActivityId = @careInterventionActivityId) AND (@careInterventionActivityId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareInterventionActivity
              SET
                CareInterventionActivityName = @careInterventionActivityName,
                CareInterventionActivityDescription = @careInterventionActivityDescription,

								CareInterventionId = @careInterventionId,
								CareInterventionActivityType = @careInterventionActivityType,
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
                CareInterventionActivityId = @careInterventionActivityId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareInterventionActivity (CareInterventionActivityName, CareInterventionActivityDescription, 
            
							  CareInterventionId, CareInterventionActivityType, ClinicalNarrative, CommonNarrative, 
							  
							  ActivityType, IsReoccurring, InitialAnchorDate, AnchorDate,
							  
							  ScheduleType, ScheduleValue, ScheduleQualifier, ConstraintValue, ConstraintQualifier,
							  
							  PerformActionDate, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careInterventionActivityName, @careInterventionActivityDescription,

							  @careInterventionId, @careInterventionActivityType, @clinicalNarrative, @commonNarrative,
							  
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
GRANT EXECUTE ON dbo.CareInterventionActivity_InsertUpdate TO PUBLIC
GO          
*/