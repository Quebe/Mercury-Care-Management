
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareLevelActivity_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareLevelActivity_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareLevelActivity_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careLevelActivityId               BIGINT,
      @careLevelActivityName            VARCHAR (060),
      @careLevelActivityDescription     VARCHAR (999),
      
      @careLevelId BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CareLevelActivity WHERE ((CareLevelActivityId = @careLevelActivityId) AND (@careLevelActivityId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareLevelActivity
              SET
                CareLevelActivityName = @careLevelActivityName,
                CareLevelActivityDescription = @careLevelActivityDescription,

								CareLevelId = @careLevelId,
								
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
                CareLevelActivityId = @careLevelActivityId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareLevelActivity (CareLevelActivityName, CareLevelActivityDescription, 
            
							  CareLevelId, ActivityType, IsReoccurring, InitialAnchorDate, AnchorDate,
							  
							  ScheduleType, ScheduleValue, ScheduleQualifier, ConstraintValue, ConstraintQualifier,
							  
							  PerformActionDate, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careLevelActivityName, @careLevelActivityDescription,

							  @careLevelId, @activityType, @isReoccurring, @initialAnchorDate, @anchorDate,
							  
							  @scheduleType, @scheduleValue, @scheduleQualifier, @constraintValue, @constraintQualifier,
							  
							  @performActionDate, @actionId, @actionParameters, @actionDescription,
							  
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareLevelActivity_InsertUpdate TO PUBLIC
GO          
*/