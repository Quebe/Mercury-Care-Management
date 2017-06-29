CREATE VIEW [dal].[AuthorizedServiceAnalysisAuthorization] AS 

	SELECT 
	
			[Authorization].AuthorizationId,
			
			[Authorization].AuthorizationNumber,
			
			
			[Authorization].AuthorizationTypeId,
			
			AuthorizationType.AuthorizationCategoryId,
			
			AuthorizationType.AuthorizationSubcategoryId,
			
			AuthorizationType.AuthorizationTypeName,
			
			
			[Authorization].MemberId,
			
			[Authorization].ReferringProviderId,
			
			[Authorization].ServiceProviderId,
			
			
			[Authorization].ReceivedDate,
			
			[Authorization].ReferralDate,
			
			[Authorization].EffectiveDate,
			
			[Authorization].TerminationDate,
			
			[Authorization].AuthorizationStatus,
			
			
			PrincipalDiagnosis.DiagnosisCode AS PrincipalDiagnosisCode,
			
			PrincipalDiagnosis.DiagnosisVersion AS PrincipalDiagnosisVersion,
			
			AdmittingDiagnosis.DiagnosisCode AS AdmittingDiagnosisCode,
			
			AdmittingDiagnosis.DiagnosisVersion AS AdmittingDiagnosisVersion
		


	  FROM 
	  
			dbo.[Authorization] AS [Authorization]
			
				JOIN dbo.AuthorizationType AS AuthorizationType ON [Authorization].AuthorizationTypeId = AuthorizationType.AuthorizationTypeId			
				
				LEFT JOIN dbo.AuthorizationDiagnosis AS PrincipalDiagnosis 
				
					ON [Authorization].AuthorizationId = PrincipalDiagnosis.AuthorizationId
					
					AND PrincipalDiagnosis.DiagnosisType = 1 -- PRINCIPAL
					
				LEFT JOIN dbo.AuthorizationDiagnosis AS AdmittingDiagnosis 
				
					ON [Authorization].AuthorizationId = AdmittingDiagnosis.AuthorizationId
					
					AND AdmittingDiagnosis.DiagnosisType = 2 -- ADMITTING
					