/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareInterventionActivityThreshold_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareInterventionActivityThreshold_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareInterventionActivityThreshold_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careInterventionActivityThresholdId               BIGINT,
      @careInterventionActivityThresholdName            VARCHAR (060),
      @careInterventionActivityThresholdDescription     VARCHAR (999),
      
      @careInterventionActivityId BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.CareInterventionActivityThreshold WHERE ((CareInterventionActivityThresholdId = @careInterventionActivityThresholdId) AND (@careInterventionActivityThresholdId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareInterventionActivityThreshold
              SET
                CareInterventionActivityThresholdName = @careInterventionActivityThresholdName,
                CareInterventionActivityThresholdDescription = @careInterventionActivityThresholdDescription,

								CareInterventionActivityId = @careInterventionActivityId,
								
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
                CareInterventionActivityThresholdId = @careInterventionActivityThresholdId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareInterventionActivityThreshold (CareInterventionActivityThresholdName, CareInterventionActivityThresholdDescription, 
            
							  CareInterventionActivityId, 
							  
							  RelativeDateValue, RelativeDateQualifier, 
							  
							  Status, ActionId, ActionParameters, ActionDescription,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careInterventionActivityThresholdName, @careInterventionActivityThresholdDescription,

							  @careInterventionActivityId, 
							  
							  @relativeDateValue, @relativeDateQualifier, 
							  
							  @status, @actionId, @actionParameters, @actionDescription,
							  
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareInterventionActivityThreshold_InsertUpdate TO PUBLIC
GO          
*/