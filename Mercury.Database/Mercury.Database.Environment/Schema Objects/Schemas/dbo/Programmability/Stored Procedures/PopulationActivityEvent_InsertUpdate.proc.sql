
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationActivityEvent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationActivityEvent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationActivityEvent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @activityEventId          BIGINT,
      @populationId             BIGINT,

      @scheduleType          INT,
      @scheduleValue         INT,
      @scheduleQualifier     INT,
      
      @anchorDate            INT,
      @reoccurring           BIT,
      @performActionDateType INT,

      @actionId                   INT,
      @actionParameters           XML,
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

        IF EXISTS (SELECT * FROM dbo.PopulationActivityEvent WHERE PopulationActivityEventId = @activityEventId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationActivityEvent
              SET
                ScheduleType = @scheduleType,
                ScheduleValue = @scheduleValue,
                ScheduleQualifier = @scheduleQualifier,
                
                AnchorDate = @anchorDate,
                IsReoccurring = @reoccurring,
                PerformActionDateType = @performActionDateType,
      
                ActionId   = @actionId,
                ActionParameters = @actionParameters,
                ActionDescription = @actionDescription,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationActivityEventId = @activityEventId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.PopulationActivityEvent (
                
                PopulationId, ScheduleType, ScheduleValue, ScheduleQualifier, AnchorDate, IsReoccurring, PerformActionDateType,
                
                ActionId, ActionParameters, ActionDescription,
      
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationId, 
                
                @scheduleType,
                @scheduleValue,
                @scheduleQualifier,
                @anchorDate,
                @reoccurring,
                @performActionDateType,
                
                @actionId, @actionParameters, @actionDescription,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationActivityEvent_InsertUpdate TO PUBLIC
GO          
*/