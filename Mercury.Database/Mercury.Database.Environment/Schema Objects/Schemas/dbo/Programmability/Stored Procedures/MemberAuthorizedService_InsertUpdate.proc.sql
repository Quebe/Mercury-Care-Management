
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberAuthorizedService_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberAuthorizedService_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberAuthorizedService_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberAuthorizedServiceId BIGINT,
      @memberId                  BIGINT,
      @authorizedServiceId       BIGINT,
      @eventDate               DATETIME,
      @initialIdentifiedDate   DATETIME,
      @addedManually                BIT,
      
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

        IF EXISTS (SELECT * FROM dbo.MemberAuthorizedService WHERE MemberAuthorizedServiceId = @memberAuthorizedServiceId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberAuthorizedService
              SET
                MemberId = @memberId,
                AuthorizedServiceId = @authorizedServiceId,
                EventDate = @eventDate,
                InitialIdentifiedDate = @initialIdentifiedDate,
                AddedManually = @addedManually,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MemberAuthorizedServiceId = @memberAuthorizedServiceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberAuthorizedService (MemberId, AuthorizedServiceId, EventDate, InitialIdentifiedDate, AddedManually, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @memberId, @authorizedServiceId, @eventDate, @initialIdentifiedDate, @addedManually,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberAuthorizedService_InsertUpdate TO PUBLIC
GO          
*/