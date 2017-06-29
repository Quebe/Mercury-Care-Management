/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseCarePlanAssessment_UpdateValues' AND type = 'P'))
  DROP PROCEDURE dbo.MemberCaseCarePlanAssessment_UpdateValues
GO      
*/

CREATE PROCEDURE dbo.MemberCaseCarePlanAssessment_UpdateValues
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			@memberCaseCarePlanId BIGINT
      
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

					DECLARE @memberCaseCarePlanAssessmentDateMinimum AS DATETIME

					DECLARE @memberCaseCarePlanAssessmentDateMaximum AS DATETIME


					DECLARE @memberCaseCarePlanAssessmentIdMinimum AS BIGINT

					DECLARE @memberCaseCarePlanAssessmentIdMaximum AS BIGINT


					DECLARE @initialValue AS DECIMAL (20, 08)

					DECLARE @latestValue AS DECIMAL (20, 08)

					DECLARE @targetValue AS DECIMAL (20, 08)

        /* LOCAL VARIABLES ( END ) */
				
				DECLARE @memberCaseCarePlanGoalId AS BIGINT

				DECLARE CursorMemberCaseCarePlanGoal CURSOR FOR SELECT MemberCaseCarePlanGoal.MemberCaseCarePlanGoalId FROM MemberCaseCarePlanGoal WHERE MemberCaseCarePlanId = @memberCaseCarePlanId

				OPEN CursorMemberCaseCarePlanGoal

				FETCH NEXT FROM CursorMemberCaseCarePlanGoal INTO @memberCaseCarePlanGoalId

				WHILE (@@FETCH_STATUS = 0) 

					BEGIN
	
						-- FOR EACH MEMBER CASE CARE PLAN GOAL, FIND INITIAL AND MOST RECENT ASSESSMENT (FOR THE GOAL) AND UPDATE
		
						SELECT 
		
								@memberCaseCarePlanAssessmentDateMinimum = MIN (MemberCaseCarePlanAssessment.AssessmentDate), 
				
								@memberCaseCarePlanAssessmentDateMaximum = MAX (MemberCaseCarePlanAssessment.AssessmentDate)
		
							FROM 
			
								MemberCaseCarePlanAssessmentCareMeasureGoal
				
									JOIN MemberCaseCarePlanAssessmentCareMeasure -- FIND RELATED MEASURE 
					
										ON MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanAssessmentCareMeasureId = MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentCareMeasureId
						
									JOIN MemberCaseCarePlanAssessment -- FIND RELATED ASSESSMENT
					
										ON MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentId = MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId
						
							WHERE 
				
								MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId -- FOR CURRENT GOAL
				
							GROUP BY 
			
								MemberCaseCarePlanAssessment.MemberCaseCarePlanId
				
						SELECT @memberCaseCarePlanAssessmentIdMinimum = MIN (MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId) 
		
							FROM MemberCaseCarePlanAssessment
			
							WHERE MemberCaseCarePlanAssessment.MemberCaseCarePlanId = @memberCaseCarePlanId
			
								AND MemberCaseCarePlanAssessment.AssessmentDate = @memberCaseCarePlanAssessmentDateMinimum
	
						SELECT @memberCaseCarePlanAssessmentIdMaximum = MAX (MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId) 
		
							FROM MemberCaseCarePlanAssessment
			
							WHERE MemberCaseCarePlanAssessment.MemberCaseCarePlanId = @memberCaseCarePlanId
			
								AND MemberCaseCarePlanAssessment.AssessmentDate = @memberCaseCarePlanAssessmentDateMaximum
				
	
						SELECT @initialValue = MAX (MemberCaseCarePlanAssessmentCareMeasure.ComponentValue)
		
							FROM 
			
								MemberCaseCarePlanAssessment 
				
									JOIN MemberCaseCarePlanAssessmentCareMeasure 
					
										ON MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId = MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentId
						
									JOIN MemberCaseCarePlanAssessmentCareMeasureGoal
					
										ON MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentCareMeasureId = MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanAssessmentCareMeasureId
				
							WHERE 
	
								MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId = @memberCaseCarePlanAssessmentIdMinimum
				
								AND MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
	
	
	
						SELECT 
		
								@latestValue = MAX (MemberCaseCarePlanAssessmentCareMeasure.ComponentValue),
				
								@targetValue = MAX (MemberCaseCarePlanAssessmentCareMeasure.TargetValue)
		
							FROM 
			
								MemberCaseCarePlanAssessment 
				
									JOIN MemberCaseCarePlanAssessmentCareMeasure 
					
										ON MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId = MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentId
						
									JOIN MemberCaseCarePlanAssessmentCareMeasureGoal
					
										ON MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessmentCareMeasureId = MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanAssessmentCareMeasureId
				
							WHERE 
	
								MemberCaseCarePlanAssessment.MemberCaseCarePlanAssessmentId = @memberCaseCarePlanAssessmentIdMaximum
				
								AND MemberCaseCarePlanAssessmentCareMeasureGoal.MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
	
	
						UPDATE MemberCaseCarePlanGoal
		
							SET 
			
								InitialValue = @initialValue,
				
								LastValue = @latestValue,
				
								TargetValue = @targetValue
				
							WHERE MemberCaseCarePlanGoalId = @memberCaseCarePlanGoalId
						
	
						FETCH NEXT FROM CursorMemberCaseCarePlanGoal INTO @memberCaseCarePlanGoalId

					END


				CLOSE CursorMemberCaseCarePlanGoal

				DEALLOCATE CursorMemberCaseCarePlanGoal

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_UpdateValues TO PUBLIC
GO          
*/