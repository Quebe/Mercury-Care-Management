/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareLevelActivityThreshold_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareLevelActivityThreshold_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareLevelActivityThreshold_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careLevelActivityThresholdId               BIGINT,
      @careLevelActivityThresholdName            VARCHAR (060),
      @careLevelActivityThresholdDescription     VARCHAR (999),
      
      @careLevelActivityId BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CareLevelActivityThreshold WHERE ((CareLevelActivityThresholdId = @careLevelActivityThresholdId) AND (@careLevelActivityThresholdId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareLevelActivityThreshold
              SET
                CareLevelActivityThresholdName = @careLevelActivityThresholdName,
                CareLevelActivityThresholdDescription = @careLevelActivityThresholdDescription,

								CareLevelActivityId = @careLevelActivityId,
								
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
                CareLevelActivityThresholdId = @careLevelActivityThresholdId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareLevelActivityThreshold (CareLevelActivityThresholdName, CareLevelActivityThresholdDescription, 
            
							  CareLevelActivityId, 
							  
							  RelativeDateValue, RelativeDateQualifier, 
							  
							  Status, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careLevelActivityThresholdName, @careLevelActivityThresholdDescription,

							  @careLevelActivityId, 
							  
							  @relativeDateValue, @relativeDateQualifier, 
							  
							  @status, @actionId, @actionParameters, @actionDescription,
							  
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareLevelActivityThreshold_InsertUpdate TO PUBLIC
GO          
*/