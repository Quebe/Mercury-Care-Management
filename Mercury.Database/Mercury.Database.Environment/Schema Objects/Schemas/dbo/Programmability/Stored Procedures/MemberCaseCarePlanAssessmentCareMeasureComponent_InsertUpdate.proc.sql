/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanAssessmentCareMeasureComponent_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasureComponent_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanAssessmentCareMeasureComponent_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberCaseCarePlanAssessmentCareMeasureComponentId    BIGINT,
			@memberCaseCarePlanAssessmentCareMeasureId BIGINT,
			
			@careMeasureComponentId BIGINT,
			@careMeasureComponentName VARCHAR (99),
			@careMeasureScaleId BIGINT,
			@tag AS VARCHAR (020),
			@componentValue INT,

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

        IF EXISTS (SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasureComponent WHERE MemberCaseCarePlanAssessmentCareMeasureComponentId = @memberCaseCarePlanAssessmentCareMeasureComponentId)

          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.MemberCaseCarePlanAssessmentCareMeasureComponent

              SET

                MemberCaseCarePlanAssessmentCareMeasureId = @memberCaseCarePlanAssessmentCareMeasureId,

								CareMeasureComponentId = @careMeasureComponentId,

								CareMeasureComponentName = @careMeasureComponentName,

                CareMeasureScaleId = @careMeasureScaleId,

								Tag = @tag,

								ComponentValue = @componentValue --,
								                
                -- ModifiedAuthorityName = @modifiedAuthorityName,
                -- ModifiedAccountId     = @modifiedAccountId,
                -- ModifiedAccountName   = @modifiedAccountName,
                -- ModifiedDate          = GETDATE ()
                
              WHERE 

                MemberCaseCarePlanAssessmentCareMeasureComponentId = @memberCaseCarePlanAssessmentCareMeasureComponentId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.MemberCaseCarePlanAssessmentCareMeasureComponent (

                MemberCaseCarePlanAssessmentCareMeasureId, CareMeasureComponentId, CareMeasureComponentName, CareMeasureScaleId, Tag, ComponentValue) --,
								
                -- CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 

                -- ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (

                @memberCaseCarePlanAssessmentCareMeasureId, @careMeasureComponentId, @careMeasureComponentName, @careMeasureScaleId, @tag, @componentValue) --,
								
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