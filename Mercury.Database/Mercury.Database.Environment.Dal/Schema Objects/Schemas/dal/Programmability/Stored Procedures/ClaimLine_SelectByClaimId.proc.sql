/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ClaimLine_SelectByClaimId' AND type = 'P'))
  DROP PROCEDURE dal.ClaimLine_SelectByClaimId
GO      
*/

CREATE PROCEDURE [dal].[ClaimLine_SelectByClaimId]
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @claimId             BIGINT
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

					ClaimLine.*,

					ClaimLine.Line AS LineNumber,

					ISNULL (RevenueCode.RevenueCodeName, '') AS RevenueCodeName,

					ISNULL (ProcedureCode.ProcedureCodeName, '') AS ProcedureCodeName,

          CASE WHEN (ClaimLine.LineStatus = 5) THEN 'TODO' ELSE '' END AS DenialReason
                    
          
        FROM dbo.ClaimLine
        	  
          LEFT JOIN dbo.RevenueCode ON ClaimLine.RevenueCode = RevenueCode.RevenueCode
          
          LEFT JOIN dbo.ProcedureCode ON ClaimLine.ProcedureCode = ProcedureCode.ProcedureCode
              
    
        WHERE (ClaimLine.ClaimId = @claimId)
        
        ORDER BY ClaimLine.Line
        
	    END    
              

GO


