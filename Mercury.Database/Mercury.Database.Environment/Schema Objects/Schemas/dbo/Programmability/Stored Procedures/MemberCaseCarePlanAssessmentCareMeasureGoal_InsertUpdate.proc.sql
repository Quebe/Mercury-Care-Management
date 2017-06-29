/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanAssessmentCareMeasureGoal_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasureGoal_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasureGoal_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanAssessmentCareMeasureGoalId    BIGINT,
			
			@memberCaseCarePlanAssessmentCareMeasureId BIGINT,
			@memberCaseCarePlanGoalId BIGINT,
      
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

        IF EXISTS (SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasureGoal WHERE MemberCaseCarePlanAssessmentCareMeasureGoalId = @memberCaseCarePlanAssessmentCareMeasureGoalId)

          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberCaseCarePlanAssessmentCareMeasureGoal

              SET

                MemberCaseCarePlanAssessmentCareMeasureId = @memberCaseCarePlanAssessmentCareMeasureId,

								MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId --,

                -- ModifiedAuthorityName = @modifiedAuthorityName,
                -- ModifiedAccountId     = @modifiedAccountId,
                -- ModifiedAccountName   = @modifiedAccountName,
                -- ModifiedDate          = GETDATE ()
                
              WHERE 

                MemberCaseCarePlanAssessmentCareMeasureGoalId = @memberCaseCarePlanAssessmentCareMeasureGoalId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberCaseCarePlanAssessmentCareMeasureGoal (

                MemberCaseCarePlanAssessmentCareMeasureId, MemberCaseCarePlanGoalId) --,
								
                -- CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 

                -- ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (

                @memberCaseCarePlanAssessmentCareMeasureId, @memberCaseCarePlanGoalId) --,
								
                -- @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),

                -- @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
         
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/