/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeEvaluation_MemberMetric_Execute' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeEvaluation_MemberMetric_Execute
GO      
*/

CREATE PROCEDURE [dal].[DataExplorerNodeEvaluation_MemberMetric_Execute] 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			
			@nodeInstanceId UNIQUEIDENTIFIER,

			@metricId            BIGINT,

			@valueMinimum				DECIMAL,

			@valueMaximum       DECIMAL,

			@countOf								 INT,
			
			@useAgeCriteria               BIT,

			@ageMinimum                   INT,

			@ageMaximum                   INT,

			@ageQualifier                 INT,

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

        /* LOCAL VARIABLES ( END ) */

				DELETE FROM DataExplorerNodeResultDetail WHERE DataExplorerNodeInstanceId = @nodeInstanceId

				DELETE FROM DataExplorerNodeResult WHERE DataExplorerNodeInstanceId = @nodeInstanceId


				if (@useAgeCriteria = 0) 

					BEGIN

						INSERT INTO DataExplorerNodeResultDetail (DataExplorerNodeInstanceId, Id, DetailId) 
				
							SELECT DISTINCT @nodeInstanceId, MemberMetric.MemberId, MemberMetric.MemberMetricId 

								FROM MemberMetric
	
								WHERE 
	
									(MemberMetric.MetricId = @metricId)
		
									AND (MemberMetric.EventDate BETWEEN @startDate AND @endDate)
		
									AND (MemberMetric.MemberId IN (

										SELECT MemberMetric.MemberId

											FROM MemberMetric
				
											WHERE 
				
												(MemberMetric.MetricId = @metricId)

												AND (MemberMetric.EventDate BETWEEN @startDate AND @endDate)

												AND (MemberMetric.MetricValue BETWEEN @valueMinimum AND @valueMaximum)
					
											GROUP BY MemberId
				
											HAVING COUNT (1) >= @countOf
				
										)) -- COUNT OF MATCH CRITERIA

							END

				ELSE

					BEGIN

						INSERT INTO DataExplorerNodeResultDetail (DataExplorerNodeInstanceId, Id, DetailId) 
				
							SELECT DISTINCT @nodeInstanceId, MemberMetric.MemberId, MemberMetric.MemberMetricId 

								FROM MemberMetric
	
								WHERE 
	
									(MemberMetric.MetricId = @metricId)
		
									AND (MemberMetric.EventDate BETWEEN @startDate AND @endDate)
		
									AND (MemberMetric.MemberId IN (

										SELECT MemberMetric.MemberId

											FROM MemberMetric

												JOIN Member ON MemberMetric.MemberId = Member.MemberId
				
											WHERE 
				
												(MemberMetric.MetricId = @metricId)
					
												AND (MemberMetric.EventDate BETWEEN @startDate AND @endDate)

												AND (MemberMetric.MetricValue BETWEEN @valueMinimum AND @valueMaximum)

												AND (-- AGE CRITERIA

														 ((@ageQualifier = 0) AND (DATEDIFF (DAY, Member.BirthDate, MemberMetric.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													OR ((@ageQualifier = 1) AND (dbo.AgeInMonthsOnDate (Member.BirthDate, MemberMetric.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													OR ((@ageQualifier = 2) AND (dbo.AgeInYearsOnDate (Member.BirthDate, MemberMetric.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													)

					
											GROUP BY MemberMetric.MemberId
				
											HAVING COUNT (1) >= @countOf
				
										)) -- COUNT OF MATCH CRITERIA

					END 
							
				INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id) 

					SELECT DISTINCT 

							@nodeInstanceId, 

							Id

						FROM DataExplorerNodeResultDetail

						WHERE DataExplorerNodeInstanceId = @nodeInstanceId
				
				SET @rowCount = @@ROWCOUNT

	    END    
              

