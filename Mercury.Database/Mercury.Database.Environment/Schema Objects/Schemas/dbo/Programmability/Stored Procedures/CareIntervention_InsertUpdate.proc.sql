
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareIntervention_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareIntervention_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareIntervention_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careInterventionId               BIGINT,
      @careInterventionName            VARCHAR (060),
      @careInterventionDescription     VARCHAR (999),
      
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

        IF EXISTS (SELECT * FROM dbo.CareIntervention WHERE ((CareInterventionId = @careInterventionId) AND (@careInterventionId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareIntervention
              SET
                CareInterventionName = @careInterventionName,
                CareInterventionDescription = @careInterventionDescription,
                
                ExtendedProperties = @extendedProperties,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareInterventionId = @careInterventionId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareIntervention (CareInterventionName, CareInterventionDescription, ExtendedProperties, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careInterventionName, @careInterventionDescription, @extendedProperties, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareIntervention_InsertUpdate TO PUBLIC
GO          
*/