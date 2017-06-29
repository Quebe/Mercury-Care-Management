CREATE VIEW [dal].[ServiceAnalysisMedicalClaim] AS

	SELECT 
	
			Claim.*,

			PrincipalDiagnosis.DiagnosisCode AS PrincipalDiagnosisCode,

			PrincipalDiagnosis.DiagnosisVersion AS PrincipalDiagnosisVersion,

			Claim.ClaimId AS ExternalClaimId,

			Claim.ServiceProviderId AS ProviderId,

			'' AS ExternalMemberId,

			'' AS ExternalProviderId

		FROM 

			dbo.Claim 

				LEFT JOIN dbo.ClaimDiagnosis AS PrincipalDiagnosis

					ON Claim.ClaimId = PrincipalDiagnosis.ClaimId

						AND (PrincipalDiagnosis.DiagnosisType = 1) -- PRIMARY DIAGNOSIS

