CREATE VIEW [dal].[ServiceAnalysisMedicalClaimLine] AS 

	SELECT 
	
			ClaimLine.*,

			ClaimLine.ServicePlace AS ServiceLocation, -- BACKWARDS COMPATIBILITY ON NAME CHANGE

			ClaimLine.ModifierCode1 AS ModifierCode,   -- BACKWARDS COMPATIBILITY ON NAME CHANGE

			ClaimLine.ClaimId AS ExternalClaimId
	
		FROM dbo.ClaimLine

