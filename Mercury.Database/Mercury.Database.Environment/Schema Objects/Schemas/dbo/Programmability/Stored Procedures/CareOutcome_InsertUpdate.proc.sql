
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CareOutcome_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.CareOutcome_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.CareOutcome_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @careOutcomeId               BIGINT,
      @careOutcomeName            VARCHAR (060),
      @careOutcomeDescription     VARCHAR (999),

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

        IF EXISTS (SELECT * FROM dbo.CareOutcome WHERE ((CareOutcomeId = @careOutcomeId) AND (@careOutcomeId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.CareOutcome
              SET
                CareOutcomeName = @careOutcomeName,
                CareOutcomeDescription = @careOutcomeDescription,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                CareOutcomeId = @careOutcomeId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.CareOutcome (CareOutcomeName, CareOutcomeDescription, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @careOutcomeName, @careOutcomeDescription, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.CareOutcome_InsertUpdate TO PUBLIC
GO          
*/