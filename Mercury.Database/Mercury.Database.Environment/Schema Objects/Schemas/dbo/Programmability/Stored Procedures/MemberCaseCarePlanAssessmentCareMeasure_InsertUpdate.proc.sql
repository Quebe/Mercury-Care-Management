/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanAssessmentCareMeasure_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasure_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasure_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanAssessmentCareMeasureId    BIGINT,
			@memberCaseCarePlanAssessmentId BIGINT,
			
			@careMeasureDomainId BIGINT,
			@careMeasureClassId  BIGINT,
			@careMeasureId       BIGINT,

			@targetValue    DECIMAL (20, 08),
			@componentValue DECIMAL (20, 08),

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

				-- TODO: VALIDATE IF USER HAS RIGHTS TO ADD THE ASSESSMENT

        IF EXISTS (SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasure WHERE MemberCaseCarePlanAssessmentCareMeasureId = @memberCaseCarePlanAssessmentCareMeasureId)

          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberCaseCarePlanAssessmentCareMeasure
              SET
                MemberCaseCarePlanAssessmentId = @memberCaseCarePlanAssessmentId,

                CareMeasureDomainId = @careMeasureDomainId,

								CareMeasureClassId = @careMeasureClassId,

								CareMeasureId = @careMeasureId,

								TargetValue = @targetValue, 

								ComponentValue = @componentValue,
								                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MemberCaseCarePlanAssessmentCareMeasureId = @memberCaseCarePlanAssessmentCareMeasureId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberCaseCarePlanAssessmentCareMeasure (

                MemberCaseCarePlanAssessmentId, CareMeasureDomainId, CareMeasureClassId, CareMeasureId, TargetValue, ComponentValue,
								
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 

                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (

                @memberCaseCarePlanAssessmentId, @careMeasureDomainId, @careMeasureClassId, @careMeasureId, @targetValue, @componentValue,
								
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
         
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/