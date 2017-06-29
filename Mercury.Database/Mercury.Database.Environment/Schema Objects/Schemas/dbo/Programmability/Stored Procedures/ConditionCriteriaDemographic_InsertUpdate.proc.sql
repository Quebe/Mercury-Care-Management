
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ConditionCriteriaDemographic_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.ConditionCriteriaDemographic_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.ConditionCriteriaDemographic_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @criteriaDemographicId     BIGINT,
      @conditionId              BIGINT,

      @gender                       INT,
      @useAgeCriteria               BIT,
      @ageMinimum                   INT,
      @ageMaximum                   INT,
      @ethnicityId               BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.ConditionCriteriaDemographic WHERE ConditionCriteriaDemographicId = @criteriaDemographicId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.ConditionCriteriaDemographic
              SET
                Gender = @gender,
                UseAgeCriteria = @useAgeCriteria, 
                AgeMinimum = @ageMinimum, 
                AgeMaximum = @ageMaximum, 
                EthnicityId = @ethnicityId,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                ConditionCriteriaDemographicId = @criteriaDemographicId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.ConditionCriteriaDemographic (ConditionId, Gender, UseAgeCriteria, AgeMinimum, AgeMaximum, EthnicityId,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @conditionId, @gender, @useAgeCriteria, @ageMinimum, @ageMaximum, @ethnicityId,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.ConditionCriteriaDemographic_InsertUpdate TO PUBLIC
GO          
*/