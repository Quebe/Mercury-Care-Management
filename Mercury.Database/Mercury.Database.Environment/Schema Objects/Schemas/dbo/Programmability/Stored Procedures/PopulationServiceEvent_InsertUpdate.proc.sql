
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationServiceEvent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationServiceEvent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationServiceEvent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceEventId           BIGINT,
      @populationId             BIGINT,
      @serviceId                BIGINT,
      @exclusionServiceId       BIGINT,
      @anchorDate                  INT,
      @anchorDateValue             INT,
      @scheduleValue               INT,
      @scheduleQualifier           INT,
      @reoccurring                 BIT,
      
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

        IF EXISTS (SELECT * FROM dbo.PopulationServiceEvent WHERE PopulationServiceEventId = @serviceEventId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationServiceEvent
              SET
                ServiceId = @serviceId,
                ExclusionServiceId = @exclusionServiceId,
                AnchorDate = @anchorDate,
                AnchorDateValue = @anchorDateValue,
                ScheduleDateValue = @scheduleValue,
                ScheduleDateQualifier = @scheduleQualifier,
                IsReoccurring = @reoccurring,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationServiceEventId  = @serviceEventId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.PopulationServiceEvent (
                PopulationId, ServiceId, ExclusionServiceId, AnchorDate, AnchorDateValue, ScheduleDateValue, ScheduleDateQualifier, IsReoccurring,
    
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationId, @serviceId, @exclusionServiceId, @anchorDate, @anchorDateValue, @scheduleValue, @scheduleQualifier, @reoccurring, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationServiceEvent_InsertUpdate TO PUBLIC
GO          
*/