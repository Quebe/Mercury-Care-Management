/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_AddManual' AND type = 'P'))
  DROP PROCEDURE dbo.MemberService_AddManual
GO      
*/

CREATE PROCEDURE dbo.MemberService_AddManual
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                BIGINT,
      @serviceId               BIGINT,
      @eventDate             DATETIME,
      
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

        IF NOT EXISTS (SELECT * FROM dbo.MemberService WHERE (MemberId = @memberId) AND (ServiceId = @serviceId) AND (EventDate = @eventDate))
          BEGIN

            INSERT INTO dbo.MemberService (MemberId, ServiceId, EventDate, AddedManually, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @memberId, @serviceId, @eventDate, 1,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberService_AddManual TO PUBLIC
GO          
*/