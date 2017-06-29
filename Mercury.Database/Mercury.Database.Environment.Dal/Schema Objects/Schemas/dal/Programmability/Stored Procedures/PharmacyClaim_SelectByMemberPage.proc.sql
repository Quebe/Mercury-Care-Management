/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PharmacyClaim_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE dal.PharmacyClaim_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dal.PharmacyClaim_SelectByMemberPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @initialRow             INT,
      @count                  INT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT 

					PharmacyClaim.*

	      FROM (

          SELECT ROW_NUMBER () OVER (ORDER BY ServiceDate DESC, PharmacyClaimId DESC) AS RowNumber, 
          
              PharmacyClaimPage.*
                  
            FROM dbo.PharmacyClaim AS PharmacyClaimPage
        	  
            WHERE (PharmacyClaimPage.MemberId = @memberId)
            
          ) AS PharmacyClaim
            
        WHERE PharmacyClaim.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
        
	    END    
              
