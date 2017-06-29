/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanAssessment_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanAssessment_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanAssessment_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanAssessmentId    BIGINT,
			@memberCaseCarePlanId BIGINT,
			@assessmentDate DATETIME,
      
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

        IF EXISTS (SELECT * FROM dbo.MemberCaseCarePlanAssessment WHERE MemberCaseCarePlanAssessmentId = @memberCaseCarePlanAssessmentId)

          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberCaseCarePlanAssessment
              SET
                MemberCaseCarePlanId = @memberCaseCarePlanId,
                AssessmentDate = @assessmentDate,
								                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MemberCaseCarePlanAssessmentId = @memberCaseCarePlanAssessmentId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberCaseCarePlanAssessment (
                MemberCaseCarePlanId, AssessmentDate, 
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                @memberCaseCarePlanId, @assessmentDate,
                
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