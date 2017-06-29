/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE MemberService_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dbo.MemberService_SelectByMemberPage
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

        SELECT MemberServicePage.* 
          
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY MemberId, EventDate DESC, MemberServiceId DESC) AS RowNumber, 
            
                MemberService.*,

                Service.ServiceId AS ServiceId1, Service.ServiceName AS ServiceName1, Service.ServiceDescription AS ServiceDescription1,
    
                Service.ServiceType AS ServiceType1, ServiceClassification AS ServiceClassification1, Service.Enabled AS Enabled1, Service.Visible AS Visible1, 
                
                Service.SetType AS SetType1, Service.SetWithinDays AS SetWithinDays1,

                Service.LastPaidDate AS LastPaidDate1, Service.ExtendedProperties AS ExtendedProperties1, 

                Service.CreateAuthorityName AS CreateAuthorityName1, Service.CreateAccountId AS CreateAccountId1, Service.CreateAccountName AS CreateAccountName1, Service.CreateDate AS CreateDate1, 

                Service.ModifiedAuthorityName AS ModifiedAuthorityName1, Service.ModifiedAccountId AS ModifiedAccountId1, Service.ModifiedAccountName AS ModifiedAccountName1, Service.ModifiedDate AS ModifiedDate1
           
              FROM MemberService JOIN Service ON MemberService.ServiceId = Service.ServiceId
              
              WHERE MemberId = @memberId
              
                AND ((Service.Visible = 1) OR (@showHidden = 1))
            
            ) AS MemberServicePage

          WHERE MemberServicePage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    