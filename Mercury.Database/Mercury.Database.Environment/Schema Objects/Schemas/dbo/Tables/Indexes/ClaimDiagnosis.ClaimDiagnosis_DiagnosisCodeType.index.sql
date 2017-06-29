CREATE NONCLUSTERED INDEX ClaimDiagnosis_DiagnosisCodeType 

	ON dbo.ClaimDiagnosis (DiagnosisCode, DiagnosisType) 
	
	INCLUDE (DiagnosisVersion, ClaimId)