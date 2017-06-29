/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Authorization_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE dal.Authorization_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dal.Authorization_SelectByMemberPage
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

					[Authorization].*,


					ReferringProviderEntity.EntityName AS ReferringProviderName,

					ServiceProviderEntity.EntityName AS ServiceProviderName,


					CAST ('TODO' AS VARCHAR (060)) ServiceProviderSpecialtyName,

					CAST ('TODO' AS VARCHAR (060)) AS AuthorizationCategory,

					CAST ('TODO' AS VARCHAR (060)) AS AuthorizationSubcategory,

					AuthorizationType.AuthorizationTypeName AS AuthorizationServiceType,


					PrincipalDiagnosis.DiagnosisCode AS PrincipalDiagnosisCode,

					PrincipalDiagnosis.DiagnosisVersion AS PrincipalDiagnosisVersion,

					PrincipalDiagnosis.DiagnosisName AS PrincipalDiagnosisName,

					AdmittingDiagnosis.DiagnosisCode AS AdmittingDiagnosisCode,

					AdmittingDiagnosis.DiagnosisVersion AS AdmittingDiagnosisVersion,

					AdmittingDiagnosis.DiagnosisName AS AdmittingDiagnosisName,

					PrincipalDiagnosis.DiagnosisCode AS DischargeDiagnosisCode,

					DischargeDiagnosis.DiagnosisVersion AS DischargeDiagnosisVersion,

					DischargeDiagnosis.DiagnosisName AS DischargeDiagnosisName

	      FROM (

          SELECT ROW_NUMBER () OVER (ORDER BY TerminationDate DESC, EffectiveDate DESC, AuthorizationId DESC) AS RowNumber, 
          
              AuthorizationPage.*
                  
            FROM dbo.[Authorization] AS AuthorizationPage
        	  
            WHERE (AuthorizationPage.MemberId = @memberId)
            
          ) AS [Authorization]                          

						JOIN dbo.AuthorizationType 

							ON [Authorization].AuthorizationTypeId = AuthorizationType.AuthorizationTypeId


						-- PRINCIPAL DIAGNOSIS INFORMATION

						LEFT JOIN dbo.AuthorizationDiagnosis AS PrincipalAuthorizationDiagnosis

							ON [Authorization].AuthorizationId = AuthorizationDiagnosis.AuthorizationId

								AND AuthorizationDiagnosis.DiagnosisType = 1 -- PRINCIPAL

						LEFT JOIN dbo.DiagnosisCode AS PrincipalDiagnosis

							ON PrincipalAuthorizationDiagnosis.DiagnosisCode = PrincipalDiagnosis.DiagnosisCode

							AND PrincipalAuthorizationDiagnosis.DiagnosisVersion = PrincipalDiagnosis.DiagnosisVersion
            

						-- ADMITTING DIAGNOSIS INFORMATION

						LEFT JOIN dbo.AuthorizationDiagnosis AS AdmittingAuthorizationDiagnosis

							ON [Authorization].AuthorizationId = AuthorizationDiagnosis.AuthorizationId

								AND AuthorizationDiagnosis.DiagnosisType = 2 -- ADMITTING

						LEFT JOIN dbo.DiagnosisCode AS AdmittingDiagnosis

							ON AdmittingAuthorizationDiagnosis.DiagnosisCode = AdmittingDiagnosis.DiagnosisCode

							AND AdmittingAuthorizationDiagnosis.DiagnosisVersion = AdmittingDiagnosis.DiagnosisVersion
            
						
						-- DISCHARGE DIAGNOSIS INFORMATION

						LEFT JOIN dbo.AuthorizationDiagnosis AS DischargeAuthorizationDiagnosis

							ON [Authorization].AuthorizationId = AuthorizationDiagnosis.AuthorizationId

								AND AuthorizationDiagnosis.DiagnosisType = 4 -- DISCHARGE

						LEFT JOIN dbo.DiagnosisCode AS DischargeDiagnosis

							ON DischargeAuthorizationDiagnosis.DiagnosisCode = DischargeDiagnosis.DiagnosisCode

							AND DischargeAuthorizationDiagnosis.DiagnosisVersion = DischargeDiagnosis.DiagnosisVersion
            

						LEFT JOIN dbo.Provider AS ReferringProvider 

							ON [Authorization].ReferringProviderId = Provider.ProviderId

						LEFT JOIN dbo.Entity AS ReferringProviderEntity

							ON ReferringProvider.EntityId = ReferringProviderEntity.EntityId


						LEFT JOIN dbo.Provider AS ServiceProvider

							ON [Authorization].ServiceProviderId = Provider.ProviderId

						LEFT JOIN dbo.Entity AS ServiceProviderEntity

							ON ServiceProvider.EntityId = ServiceProviderEntity.EntityId


					

        WHERE [Authorization].RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
        
	    END    
              
