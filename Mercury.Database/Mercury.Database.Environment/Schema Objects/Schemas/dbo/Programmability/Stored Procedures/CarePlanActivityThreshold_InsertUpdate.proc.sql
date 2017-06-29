/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CarePlanActivityThreshold_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CarePlanActivityThreshold_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CarePlanActivityThreshold_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @carePlanActivityThresholdId               BIGINT,
      @carePlanActivityThresholdName            VARCHAR (060),
      @carePlanActivityThresholdDescription     VARCHAR (999),
      
      @carePlanActivityId BIGINT,
      
      @relativeDateValue INT,
      @relativeDateQualifier INT,
      
      @status INT,
      
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

        IF EXISTS (SELECT * FROM dbo.CarePlanActivityThreshold WHERE ((CarePlanActivityThresholdId = @carePlanActivityThresholdId) AND (@carePlanActivityThresholdId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CarePlanActivityThreshold
              SET
                CarePlanActivityThresholdName = @carePlanActivityThresholdName,
                CarePlanActivityThresholdDescription = @carePlanActivityThresholdDescription,

								CarePlanActivityId = @carePlanActivityId,
								
								RelativeDateValue = @relativeDateValue,
								RelativeDateQualifier = @relativeDateQualifier,
								
								Status = @status,
								ActionId = @actionId,
								ActionParameters = @actionParameters,
								ActionDescription = @actionDescription,
								
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CarePlanActivityThresholdId = @carePlanActivityThresholdId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CarePlanActivityThreshold (CarePlanActivityThresholdName, CarePlanActivityThresholdDescription, 
            
							  CarePlanActivityId, 
							  
							  RelativeDateValue, RelativeDateQualifier, 
							  
							  Status, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @carePlanActivityThresholdName, @carePlanActivityThresholdDescription,

							  @carePlanActivityId, 
							  
							  @relativeDateValue, @relativeDateQualifier, 
							  
							  @status, @actionId, @actionParameters, @actionDescription,
							  
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CarePlanActivityThreshold_InsertUpdate TO PUBLIC
GO          
*/