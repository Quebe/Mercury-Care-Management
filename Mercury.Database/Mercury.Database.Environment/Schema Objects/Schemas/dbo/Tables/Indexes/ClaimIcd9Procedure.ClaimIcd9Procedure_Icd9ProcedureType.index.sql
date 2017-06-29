CREATE NONCLUSTERED INDEX ClaimIcd9Procedure_Icd9ProcedureCodeType 

	ON dbo.ClaimIcd9Procedure (Icd9ProcedureCode, Icd9ProcedureType) 
	
	INCLUDE (Icd9ProcedureDate, ClaimId)