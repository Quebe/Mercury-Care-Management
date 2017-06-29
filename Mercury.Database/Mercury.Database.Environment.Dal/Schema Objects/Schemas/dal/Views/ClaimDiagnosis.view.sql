CREATE VIEW [dal].[ClaimDiagnosis]
	AS SELECT *, ClaimId AS ExternalClaimId FROM dbo.ClaimDiagnosis