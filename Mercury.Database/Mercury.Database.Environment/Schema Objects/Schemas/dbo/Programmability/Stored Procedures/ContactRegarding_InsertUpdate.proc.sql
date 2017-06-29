
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ContactRegarding_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ContactRegarding_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ContactRegarding_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @contactRegardingId               BIGINT,
      @contactRegardingName            VARCHAR (060),
      @contactRegardingDescription     VARCHAR (999),

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

        IF EXISTS (SELECT * FROM dbo.ContactRegarding WHERE ((ContactRegardingId = @contactRegardingId) AND (@contactRegardingId <> 0)))
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ContactRegarding
              SET
                ContactRegardingName = @contactRegardingName,
                ContactRegardingDescription = @contactRegardingDescription,

                Enabled = @enabled,
                Visible = @visible,
                                         
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ContactRegardingId = @contactRegardingId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ContactRegarding (ContactRegardingName, ContactRegardingDescription, Enabled, Visible, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @contactRegardingName, @contactRegardingDescription, @enabled, @visible, 

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ContactRegarding_InsertUpdate TO PUBLIC
GO          
*/