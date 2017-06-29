﻿
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkOutcome_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.WorkOutcome_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.WorkOutcome_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @workOutcomeId               BIGINT,
      @workOutcomeName            VARCHAR (060),
      @workOutcomeDescription     VARCHAR (999),

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

        IF EXISTS (SELECT * FROM dbo.WorkOutcome WHERE ((WorkOutcomeId = @workOutcomeId) AND (@workOutcomeId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.WorkOutcome
              SET
                WorkOutcomeName = @workOutcomeName,
                WorkOutcomeDescription = @workOutcomeDescription,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                WorkOutcomeId = @workOutcomeId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.WorkOutcome (WorkOutcomeName, WorkOutcomeDescription, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @workOutcomeName, @workOutcomeDescription, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.WorkOutcome_InsertUpdate TO PUBLIC
GO          
*/