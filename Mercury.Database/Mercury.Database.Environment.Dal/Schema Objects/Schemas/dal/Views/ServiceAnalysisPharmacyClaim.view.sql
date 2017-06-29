CREATE VIEW [dal].[ServiceAnalysisPharmacyClaim] AS 

	SELECT *, 

			PharmacyClaim.PharmacyClaimId AS ClaimId,

			PharmacyClaim.ServiceDate AS ClaimDateFrom,
			
			PharmacyClaim.ServiceDate AS ClaimDateThru,

			PharmacyClaim.ServiceDate AS ServiceDateFrom,
	
			CAST (4 AS INT) AS ClaimType,

			PharmacyClaim.[Status] AS ClaimStatus,

			PharmacyClaim.PrescriberProviderId AS ProviderId,

			PharmacyClaim.NationalDrugCode AS NdcCode,
			
			'' AS DeaClassification,
			
			'' AS TherapeuticClassification,

			PharmacyClaim.PharmacyClaimId AS ExternalClaimId, 
			
			PharmacyClaim.MemberId AS ExternalMemberId,
			
			PharmacyClaim.PrescriberProviderId AS ExternalProviderId
		
		FROM dbo.PharmacyClaim