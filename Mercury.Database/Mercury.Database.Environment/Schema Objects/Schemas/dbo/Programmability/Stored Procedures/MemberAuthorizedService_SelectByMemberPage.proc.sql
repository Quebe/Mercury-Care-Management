/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberAuthorizedService_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE MemberAuthorizedService_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dbo.MemberAuthorizedService_SelectByMemberPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @initialRow             INT,
      @count                  INT,
      @showHidden             BIT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT MemberAuthorizedServicePage.* 
          
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY MemberId, EventDate DESC, MemberAuthorizedServiceId DESC) AS RowNumber, 
            
                MemberAuthorizedService.*,

                AuthorizedService.AuthorizedServiceId AS AuthorizedServiceId1, AuthorizedService.AuthorizedServiceName AS AuthorizedServiceName1, AuthorizedService.AuthorizedServiceDescription AS AuthorizedServiceDescription1,
    
                AuthorizedService.Enabled AS Enabled1, AuthorizedService.Visible AS Visible1, 

                AuthorizedService.ExtendedProperties AS ExtendedProperties1, 

                AuthorizedService.CreateAuthorityName AS CreateAuthorityName1, AuthorizedService.CreateAccountId AS CreateAccountId1, AuthorizedService.CreateAccountName AS CreateAccountName1, AuthorizedService.CreateDate AS CreateDate1, 

                AuthorizedService.ModifiedAuthorityName AS ModifiedAuthorityName1, AuthorizedService.ModifiedAccountId AS ModifiedAccountId1, AuthorizedService.ModifiedAccountName AS ModifiedAccountName1, AuthorizedService.ModifiedDate AS ModifiedDate1
           
              FROM MemberAuthorizedService JOIN AuthorizedService ON MemberAuthorizedService.AuthorizedServiceId = AuthorizedService.AuthorizedServiceId
              
              WHERE MemberId = @memberId
              
                AND ((AuthorizedService.Visible = 1) OR (@showHidden = 1))
            
            ) AS MemberAuthorizedServicePage

          WHERE MemberAuthorizedServicePage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    