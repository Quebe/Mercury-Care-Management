/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ServiceSingletonDefinition_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ServiceSingletonDefinition_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ServiceSingletonDefinition_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceSingletonDefinitionId BIGINT,
      @serviceId               BIGINT,
      @dataSourceType             INT,
      @eventDateOrder             INT,

      @principalDiagnosisCriteria VARCHAR (999),
      @principalDiagnosisVersion      INT,
      @diagnosisCriteria          VARCHAR (999),
      @diagnosisVersion               INT,
      @drgCriteria                VARCHAR (999),
      @icd9ProcedureCodeCriteria  VARCHAR (999),
      @billTypeCriteria           VARCHAR (999),
      @locationCodeCriteria       VARCHAR (999),
      @revenueCodeCriteria        VARCHAR (999),
      @procedureCodeCriteria      VARCHAR (999),
      @modifierCodeCriteria       VARCHAR (999),
      
      @providerSpecialtyCriteria  VARCHAR (999),
      @isPcpRequiredCriteria            BIT,
      
      @useMemberAgeCriteria            BIT,
      @memberAgeDateQualifier          INT,
      @memberAgeMinimum                INT,
      @memberAgeMaximum                INT,
      
      @ndcCodeCriteria                            VARCHAR (999),
      @drugNameCriteria                           VARCHAR (999),
      @deaClassificationCriteria                  VARCHAR (999),
      @therapeuticClassificationCriteria          VARCHAR (999),
      
      @labLoincCodeCriteria                       VARCHAR (999),
      @labNameCriteria                            VARCHAR (999),
      @labValueExpressionCriteria                 VARCHAR (999),
      @labMetricId                                 BIGINT,
    
      @customCriteria             VARCHAR (8000), 
      
      @enabled                    BIT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        BEGIN TRANSACTION 
        
        IF EXISTS (SELECT ServiceSingletonDefinitionId FROM dbo.ServiceSingletonDefinition WHERE ServiceSingletonDefinitionId = @serviceSingletonDefinitionId)
        
          BEGIN
            
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ServiceSingletonDefinition 
              SET 
                ServiceId = @serviceId,  
                DataSourceType = @dataSourceType,  
                EventDateOrder = @eventDateOrder, 
                PrincipalDiagnosisCriteria = @principalDiagnosisCriteria, 
                PrincipalDiagnosisVersion = @principalDiagnosisVersion,
                DiagnosisCriteria = @diagnosisCriteria, 
                DiagnosisVersion = @diagnosisVersion,
                DrgCriteria = @drgCriteria,
                Icd9ProcedureCodeCriteria = @icd9ProcedureCodeCriteria, 
                BillTypeCriteria = @billTypeCriteria,
                LocationCodeCriteria = @locationCodeCriteria, 
                RevenueCodeCriteria = @revenueCodeCriteria, 
                ProcedureCodeCriteria = @procedureCodeCriteria, 
                ModifierCodeCriteria = @modifierCodeCriteria,
                
                ProviderSpecialtyCriteria = @providerSpecialtyCriteria,
                IsPcpRequiredCriteria = @isPcpRequiredCriteria,
                
                UseMemberAgeCriteria = @useMemberAgeCriteria,
                MemberAgeDateQualifier = @memberAgeDateQualifier,
                MemberAgeMinimum = @memberAgeMinimum,
                MemberAgeMaximum = @memberAgeMaximum,
                                
                NdcCodeCriteria = @ndcCodeCriteria,
                DrugNameCriteria = @drugNameCriteria,
                DeaClassificationCriteria = @deaClassificationCriteria,
                TherapeuticClassificationCriteria = @therapeuticClassificationCriteria,
                LabLoincCodeCriteria = @labLoincCodeCriteria,
                LabNameCriteria = @labNameCriteria,
                LabValueExpressionCriteria = @labValueExpressionCriteria,
                LabMetricId = @labMetricId,
                CustomCriteria = @customCriteria,
                Enabled = @enabled
                
              WHERE ServiceSingletonDefinitionId = @serviceSingletonDefinitionId 
              
          END 

          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN

            INSERT INTO dbo.ServiceSingletonDefinition (
                ServiceId, DataSourceType, EventDateOrder,
                PrincipalDiagnosisCriteria, PrincipalDiagnosisVersion, DiagnosisCriteria, DiagnosisVersion, 
                DrgCriteria, Icd9ProcedureCodeCriteria, BillTypeCriteria,
                LocationCodeCriteria, RevenueCodeCriteria, ProcedureCodeCriteria, ModifierCodeCriteria, ProviderSpecialtyCriteria, IsPcpRequiredCriteria,
                
                UseMemberAgeCriteria, MemberAgeDateQualifier, MemberAgeMinimum, MemberAgeMaximum,
                
                NdcCodeCriteria, DrugNameCriteria, DeaClassificationCriteria, TherapeuticClassificationCriteria,
                LabLoincCodeCriteria, LabNameCriteria, LabValueExpressionCriteria, LabMetricId,
                CustomCriteria, Enabled
              )
                
            VALUES (
                @serviceId, @dataSourceType, @eventDateOrder,
                @principalDiagnosisCriteria, @principalDiagnosisVersion, @diagnosisCriteria, @diagnosisVersion,   
                
                @drgCriteria, @Icd9ProcedureCodeCriteria, @BillTypeCriteria,
                @LocationCodeCriteria,       @RevenueCodeCriteria, @ProcedureCodeCriteria, @ModifierCodeCriteria, @providerSpecialtyCriteria, @isPcpRequiredCriteria,
                
                @useMemberAgeCriteria, @memberAgeDateQualifier, @memberAgeMinimum, @memberAgeMaximum,
                
                @ndcCodeCriteria, @drugNameCriteria, @deaClassificationCriteria, @therapeuticClassificationCriteria,
                @labLoincCodeCriteria, @labNameCriteria, @labValueExpressionCriteria, @labMetricId,
                @CustomCriteria, @enabled
              )
              
          END
          
        COMMIT TRANSACTION 
               
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ServiceSingletonDefinition_InsertUpdate TO PUBLIC
GO          
*/