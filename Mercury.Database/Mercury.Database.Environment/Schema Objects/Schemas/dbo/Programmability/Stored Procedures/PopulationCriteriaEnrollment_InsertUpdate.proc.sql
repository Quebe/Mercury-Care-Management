
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationCriteriaEnrollment_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationCriteriaEnrollment_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.PopulationCriteriaEnrollment_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @criteriaEnrollmentId     BIGINT,
      @populationId             BIGINT,
      @insurerId                BIGINT,
      @programId                BIGINT,
      @benefitPlanId            BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.PopulationCriteriaEnrollment WHERE PopulationCriteriaEnrollmentId = @criteriaEnrollmentId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.PopulationCriteriaEnrollment
              SET
                InsurerId = @insurerId,
                ProgramId = @programId,
                BenefitPlanId = @benefitPlanId,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                PopulationCriteriaEnrollmentId  = @criteriaEnrollmentId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.PopulationCriteriaEnrollment (PopulationId, InsurerId, ProgramId, BenefitPlanId,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @populationId, @insurerId, @programId, @benefitPlanId,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationCriteriaEnrollment_InsertUpdate TO PUBLIC
GO          
*/