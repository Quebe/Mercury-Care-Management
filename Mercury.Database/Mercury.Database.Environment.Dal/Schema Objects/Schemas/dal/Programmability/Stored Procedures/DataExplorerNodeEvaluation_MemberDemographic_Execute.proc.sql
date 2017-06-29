/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeEvaluation_MemberDemographic_Execute' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeEvaluation_MemberDemographic_Execute
GO      
*/

CREATE PROCEDURE [dal].[DataExplorerNodeEvaluation_MemberDemographic_Execute] 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			
			@nodeInstanceId UNIQUEIDENTIFIER,

			@gender                       INT,

			@useAgeCriteria               BIT,

			@ageMinimum                   INT,

			@ageMaximum                   INT,

			@ageQualifier                 INT,

			@ethnicityId               BIGINT,

      @startDate          DATETIME,

			@endDate            DATETIME,

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */

			@rowCount INT OUTPUT
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 

    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

				DECLARE @ageBoundMinimum AS DATETIME

				DECLARE @ageBoundMaximum AS DATETIME 

        /* LOCAL VARIABLES ( END ) */

				
				SET @ageBoundMinimum = 

									CASE 
						
											WHEN (@ageQualifier = 0) THEN DATEADD (DAY, 1, DATEADD (DAY,   (@ageMaximum + 1) * -1, @startDate))
						
											WHEN (@ageQualifier = 1) THEN DATEADD (DAY, 1, DATEADD (MONTH, (@ageMaximum + 1) * -1, @startDate))
		
											WHEN (@ageQualifier = 2) THEN DATEADD (DAY, 1, DATEADD (YEAR,  (@ageMaximum + 1) * -1, @startDate))
						
										END 
			
				SET @ageBoundMaximum = 

									CASE 
						
											WHEN (@ageQualifier = 0) THEN DATEADD (DAY,   @ageMinimum * -1, @endDate)
						
											WHEN (@ageQualifier = 1) THEN DATEADD (MONTH, @ageMinimum * -1, @endDate)
		
											WHEN (@ageQualifier = 2) THEN DATEADD (YEAR,  @ageMinimum * -1, @endDate)
						
										END 
			
			
				DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = @nodeInstanceId

				INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id) 

					SELECT DISTINCT

							@nodeInstanceId, 

							MemberId

						FROM 
	
							Member
		
						WHERE 
	
							-- GENDER CRITERIA
		
							((@gender = 0)
		
								OR ((Member.Gender = 'F') AND (@gender = 1))
			
								OR ((Member.Gender = 'M') AND (@gender = 2))
			
							)
		
							-- AGE CRITERIA
		
							AND ((@useAgeCriteria = 0)
		
								OR ((@useAgeCriteria = 1) AND (Member.BirthDate BETWEEN @ageBoundMinimum AND @ageBoundMaximum))
			
							)
		
							-- ETHNICITY
		
							AND ((@ethnicityId = 0) 
		
								OR (Member.EthnicityId = @ethnicityId))
			
				
				SET @rowCount = @@ROWCOUNT

	    END    
              

