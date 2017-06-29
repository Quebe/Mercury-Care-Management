
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberService_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberService_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberServiceId         BIGINT,
      @memberId                BIGINT,
      @serviceId               BIGINT,
      @eventDate             DATETIME,
      @addedManually              BIT,
      
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

        IF EXISTS (SELECT * FROM dbo.MemberService WHERE MemberServiceId = @memberServiceId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberService
              SET
                MemberId = @memberId,
                ServiceId = @serviceId,
                EventDate = @eventDate,
                AddedManually = @addedManually,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MemberServiceId = @memberServiceId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberService (MemberId, ServiceId, EventDate, AddedManually, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @memberId, @serviceId, @eventDate, @addedManually,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberService_InsertUpdate TO PUBLIC
GO          
*/