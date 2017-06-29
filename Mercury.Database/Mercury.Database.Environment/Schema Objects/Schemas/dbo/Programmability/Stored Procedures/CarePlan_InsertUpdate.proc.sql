
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CarePlan_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CarePlan_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CarePlan_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @carePlanId               BIGINT,
      @carePlanName            VARCHAR (060),
      @carePlanDescription     VARCHAR (999),
      
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

        IF EXISTS (SELECT * FROM dbo.CarePlan WHERE ((CarePlanId = @carePlanId) AND (@carePlanId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CarePlan
              SET
                CarePlanName = @carePlanName,
                CarePlanDescription = @carePlanDescription,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CarePlanId = @carePlanId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CarePlan (CarePlanName, CarePlanDescription, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @carePlanName, @carePlanDescription, @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CarePlan_InsertUpdate TO PUBLIC
GO          
*/