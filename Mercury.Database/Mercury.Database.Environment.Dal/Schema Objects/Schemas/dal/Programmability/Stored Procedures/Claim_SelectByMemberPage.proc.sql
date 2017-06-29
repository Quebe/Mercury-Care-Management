/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Claim_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE dal.Claim_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dal.Claim_SelectByMemberPage
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

						Claim.ClaimId,

						Claim.ClaimNumber,

						Claim.MemberId, 

						Claim.ServiceProviderId, 

						Claim.PaytoProviderId,

						Claim.PaytoProviderAffiliationId,

						Claim.ClaimType,

						Claim.ClaimDateFrom,

						Claim.ClaimDateThru,

						Claim.AdmissionDate,

						Claim.DischargeDate,

						Claim.ClaimStatus,

						Claim.BillType,


						ClaimDiagnosis.DiagnosisCode AS PrincipalDiagnosisCode,

						ClaimDiagnosis.DiagnosisVersion AS PrincipalDiagnosisVersion,

						DiagnosisCode.DiagnosisName AS PrincipalDiagnosisName,


						Claim.BilledAmount,

						Claim.PaidAmount,

						Claim.PaidDate,


						CAST ('TODO' AS VARCHAR (060)) AS DenialReason

					FROM (
					
						SELECT ROW_NUMBER () OVER (ORDER BY ClaimDateThru DESC, ClaimDateFrom DESC, PaidDate DESC, ClaimId DESC) AS RowNumber, 
          
								ClaimPage.*
                  
							FROM dbo.Claim AS ClaimPage
        	  
							WHERE (ClaimPage.MemberId = @memberId)
            
						) AS Claim
						
							LEFT JOIN dbo.ClaimDiagnosis

								ON Claim.ClaimId = ClaimDiagnosis.ClaimId

									AND ClaimDiagnosis.DiagnosisType = 1 -- PRINCIPAL

							LEFT JOIN dbo.DiagnosisCode

								ON ClaimDiagnosis.DiagnosisCode = DiagnosisCode.DiagnosisCode

								AND ClaimDiagnosis.DiagnosisVersion = DiagnosisCode.DiagnosisVersion
								
        WHERE Claim.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)

	    END    
              
