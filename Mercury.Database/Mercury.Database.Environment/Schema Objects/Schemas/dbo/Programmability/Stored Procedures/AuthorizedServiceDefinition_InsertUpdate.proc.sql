/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'AuthorizedServiceDefinition_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.AuthorizedServiceDefinition_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.AuthorizedServiceDefinition_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @authorizedServiceDefinitionId     BIGINT,
      @authorizedServiceId               BIGINT,

      @category                   VARCHAR (999),
      @subcategory                VARCHAR (999),
      @serviceType                VARCHAR (999),

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
      
      @ndcCodeCriteria                            VARCHAR (999),
      @drugNameCriteria                           VARCHAR (999),
      @deaClassificationCriteria                  VARCHAR (999),
      @therapeuticClassificationCriteria          VARCHAR (999),
      
      @labLoincCodeCriteria                       VARCHAR (999),
      
      @enabled                    BIT,
    
      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
      
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
        
        IF EXISTS (SELECT AuthorizedServiceDefinitionId FROM dbo.AuthorizedServiceDefinition WHERE AuthorizedServiceDefinitionId = @authorizedServiceDefinitionId)
        
          BEGIN
            
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.AuthorizedServiceDefinition 
              SET 
                AuthorizedServiceId = @authorizedServiceId,  

                Category = @category,
                Subcategory = @subcategory,
                ServiceType = @serviceType,                

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

                NdcCodeCriteria = @ndcCodeCriteria,
                DrugNameCriteria = @drugNameCriteria,
                DeaClassificationCriteria = @deaClassificationCriteria,
                TherapeuticClassificationCriteria = @therapeuticClassificationCriteria,
                LabLoincCodeCriteria = @labLoincCodeCriteria,

                Enabled = @enabled,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE AuthorizedServiceDefinitionId = @authorizedServiceDefinitionId;
              
          END 

          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN

            INSERT INTO dbo.AuthorizedServiceDefinition (
                AuthorizedServiceId,
                Category, Subcategory, ServiceType,
                PrincipalDiagnosisCriteria, PrincipalDiagnosisVersion, DiagnosisCriteria, DiagnosisVersion, DrgCriteria, 
                Icd9ProcedureCodeCriteria, BillTypeCriteria,
                LocationCodeCriteria, RevenueCodeCriteria, ProcedureCodeCriteria, ModifierCodeCriteria, ProviderSpecialtyCriteria, 
                NdcCodeCriteria, DrugNameCriteria, DeaClassificationCriteria, TherapeuticClassificationCriteria,
                LabLoincCodeCriteria, 
                Enabled,
                
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @authorizedServiceId, 
                @category, @subcategory, @serviceType,
                @principalDiagnosisCriteria, @principalDiagnosisVersion, @diagnosisCriteria, @diagnosisVersion, 
                @drgCriteria, @Icd9ProcedureCodeCriteria, @BillTypeCriteria,
                @LocationCodeCriteria,       @RevenueCodeCriteria, @ProcedureCodeCriteria, @ModifierCodeCriteria, @providerSpecialtyCriteria, 
                @ndcCodeCriteria, @drugNameCriteria, @deaClassificationCriteria, @therapeuticClassificationCriteria,
                @labLoincCodeCriteria,  
                @enabled,
                
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
              
          END
          
        COMMIT TRANSACTION 
               
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.AuthorizedServiceDefinition_InsertUpdate TO PUBLIC
GO          
*/