/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MetricProcess_CostMetric' AND type = 'P'))
  DROP PROCEDURE dal.MetricProcess_CostMetric
GO      
*/

CREATE PROCEDURE dal.MetricProcess_CostMetric
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @metricId            BIGINT,
      
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

					DECLARE @metricType AS INT

					DECLARE @costDataSource AS INT

					DECLARE @costClaimDateType AS INT

					DECLARE @costReportingPeriod AS INT

					DECLARE @costReportingPeriodValue AS INT

					DECLARE @costReportingPeriodQualifier AS INT

					DECLARE @costWatermarkPeriod AS INT

					DECLARE @costWatermarkPeriodValue AS INT

					DECLARE @costWatermarkPeriodQualifier AS INT



					DECLARE @detailTable AS TABLE (MemberId BIGINT, MetricValue DECIMAL (20, 08))

					DECLARE @metricValueTable AS TABLE (MemberId BIGINT, MetricValue DECIMAL (20, 08), MemberMetricId BIGINT)

					DECLARE @anchorDateStart AS DATETIME

					DECLARE @anchorDateEnd   AS DATETIME

					DECLARE @eventDate AS DATETIME

        /* LOCAL VARIABLES ( END ) */

			
				-- SELECT METRIC CONFIGURATION INFORMATION

				SELECT 

						@metricType = Metric.MetricType, 
					
						@costDataSource = Metric.CostDataSource,
						
						@costClaimDateType = Metric.CostClaimDateType,
						
						@costReportingPeriod = Metric.CostReportingPeriod,
								
						@costReportingPeriodValue = Metric.CostReportingPeriodValue,
						
						@costReportingPeriodQualifier = Metric.CostReportingPeriodQualifier,

						@costWatermarkPeriod = Metric.CostWatermarkPeriod,
						 
						@costWatermarkPeriodValue = Metric.CostWatermarkPeriodValue,
						
						@costWatermarkPeriodQualifier = Metric.CostWatermarkPeriodQualifier		
					
					FROM Metric WHERE MetricId = @metricId
					

				IF (@metricType = 1) -- VALIDATE METRIC TYPE IS COST UTILIZATION FOR PROCESSING

					BEGIN
					
						-- CALCULATE ANCHOR DATES, SET REPORTING PERIOD
						
						IF (@costReportingPeriod = 0) -- CALENDAR YEAR
						
							BEGIN 
						
								SET @anchorDateStart = CAST ('01/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
						
								SET @anchorDateEnd   = CAST ('12/31/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
					
							END
							
						ELSE IF (@costReportingPeriod = 1) -- YEAR FROM START MONTH
						
							BEGIN
						  
								IF (MONTH (GETDATE ()) > @costReportingPeriodValue) -- PREVIOUS YEAR
								
									BEGIN
								  
										SET @anchorDateStart = CAST (LTRIM (@costReportingPeriodValue) + '/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
										
										SET @anchorDateEnd   = @anchorDateStart
										
										SET @anchorDateEnd   = DATEADD (DAY, -1, @anchorDateEnd)
										
										SET @anchorDateEnd   = DATEADD (YEAR, 1, @anchorDateEnd)
										
									END
								  
								ELSE -- NEXT YEAR
								
									BEGIN
								  
										SET @anchorDateStart = CAST (LTRIM (@costReportingPeriodValue) + '/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
										
										SET @anchorDateEnd   = @anchorDateStart
										
										SET @anchorDateStart = DATEADD (YEAR, -1, @anchorDateStart)
								
										SET @anchorDateEnd   = DATEADD (DAY, -1, @anchorDateEnd)
										
									END 
						  
							END
						  
						ELSE IF (@costReportingPeriod = 2) -- ROLLING PERIOD
						
							BEGIN
						  
								SET @anchorDateEnd = CONVERT (CHAR (010), GETDATE (), 101)
						    
								SET @anchorDateStart = 
						    
									CASE	
									
											WHEN (@costReportingPeriodQualifier = 0) THEN DATEADD (DAY, @costReportingPeriodValue, @anchorDateEnd) -- DAYS
										
											WHEN (@costReportingPeriodQualifier = 1) THEN -- MONTHS
											
												DATEADD (DD, 1, DATEADD (MONTH, @costReportingPeriodValue, @anchorDateEnd)) -- ADD AN ADDITIONAL DAY 
											
											WHEN (@costReportingPeriodQualifier = 2) THEN -- YEARS
											
												DATEADD (DD, 1, DATEADD (YEAR, @costReportingPeriodValue, @anchorDateEnd)) -- ADD AN ADDITIONAL DAY 
																								
										END 
						  
							END 
						  
						  
						-- CALCULATE EVENT DATE (THE WATERMARK DATE) 
						
						IF (@costWatermarkPeriod = 0) -- CALENDAR YEAR
						
							BEGIN
						  
								SET @eventDate = CAST ('01/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
						  
							END 
							
						ELSE IF (@costWatermarkPeriod = 1) -- YEAR FROM START MONTH
						
							BEGIN
						  
								IF (MONTH (GETDATE ()) > @costWatermarkPeriodValue) -- PREVIOUS YEAR
								
									BEGIN
								  
										SET @eventDate = CAST (LTRIM (@costWatermarkPeriodValue) + '/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
										
									END
								  
								ELSE -- NEXT YEAR
								
									BEGIN
								  
										SET @eventDate = CAST (LTRIM (@costWatermarkPeriodValue) + '/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)
										
										SET @eventDate = DATEADD (YEAR, -1, @eventDate)
								
									END 
						  
							END
						  
						ELSE IF (@costWatermarkPeriod = 2) -- MONTH
						
							BEGIN
						  
								SET @eventDate = CAST (LTRIM (MONTH (GETDATE ())) + '/01/' + LTRIM (YEAR (GETDATE ())) AS DATETIME)						
						  
							END 
						
						
						-- INSERT DETAIL BY EITHER PAID DATE OR SERVICE DATE
						
						IF (@costClaimDateType = 0) -- PAID DATE
						
							BEGIN
						  
								-- INSERT MEDICAL CLAIMS BY PAID DATE
						  
								IF (@costDataSource IN (0, 1)) 
								
									BEGIN

										INSERT INTO @detailTable
														  
											SELECT 
											
													Claim.MemberId, 
											
													SUM (Claim.PaidAmount) AS PaidAmount

												FROM dal.Claim AS Claim 
														
												WHERE (Claim.PaidDate BETWEEN @anchorDateStart AND @anchorDateEnd)
												
												GROUP BY Claim.MemberId
								  
									END				  
								  
								-- INSERT PHARMACY CLAIMS BY PAID DATE
												  				
								IF (@costDataSource IN (0, 2)) 
								
									BEGIN

										INSERT INTO @detailTable
														  
											SELECT 
											
													Claim.MemberId, 
											
													SUM (Claim.PaidAmount) AS PaidAmount

												FROM dal.ServiceAnalysisPharmacyClaim AS Claim
														
												WHERE (Claim.PaidDate BETWEEN @anchorDateStart AND @anchorDateEnd)
												
												GROUP BY Claim.MemberId
								  
									END				  
						  
							END
						  
						ELSE IF (@costClaimDateType = 1) -- SERVICE DATE
						
							BEGIN
						  
								-- INSERT MEDICAL CLAIMS BY SERVICE DATE
						  
								IF (@costDataSource IN (0, 1)) 
								
									BEGIN

										INSERT INTO @detailTable
														  
											SELECT 
											
													Claim.MemberId, 
											
													SUM (Claim.PaidAmount) AS PaidAmount

												FROM dal.Claim AS Claim 
														
												WHERE (Claim.ClaimDateFrom BETWEEN @anchorDateStart AND @anchorDateEnd)
												
												GROUP BY Claim.MemberId
								  
									END				  
								  
								-- INSERT PHARMACY CLAIMS BY SERVICE DATE
												  				
								IF (@costDataSource IN (0, 2)) 
								
									BEGIN

										INSERT INTO @detailTable
														  
											SELECT 
											
													Claim.MemberId, 
											
													SUM (Claim.PaidAmount) AS PaidAmount

												FROM dal.ServiceAnalysisPharmacyClaim AS Claim
														
												WHERE (Claim.ServiceDateFrom BETWEEN @anchorDateStart AND @anchorDateEnd)
												
												GROUP BY Claim.MemberId
								  
									END				
								    
							END


						-- AGGREGATE DETAIL TABLE

						INSERT INTO @metricValueTable
						
								SELECT MemberId, SUM (MetricValue) AS MetricValue, NULL FROM @detailTable GROUP BY MemberId HAVING (SUM (MetricValue) > 0)
								
					
						-- MARK EXISTING WATERMARKS
						
						UPDATE @metricValueTable 
						
							SET MemberMetricId = MemberMetric.MemberMetricId
						
							FROM 
								
								@metricValueTable AS MetricValueTable
								
									JOIN MemberMetric 
									
										ON MetricValueTable.MemberId = MemberMetric.MemberId
										
							WHERE					
							
								(MemberMetric.MetricId = @metricId) 
								
								AND (MemberMetric.EventDate = @eventDate)
												
												
						-- INSERT INTO MEMBER METRIC FOR NON-EXISTING WATERMARKS				

						INSERT INTO MemberMetric
								
							SELECT 				
							
									MetricValueTable.MemberId AS MemberId,
									
									@metricId AS MetricId,
									
									MetricValueTable.MetricValue AS MetricValue,
									
									@eventDate AS EventDate,
									
									0 AS AddedManually,
									
									@modifiedAuthorityName AS CreateAuthorityName,
									
									@modifiedAccountId AS CreateAccountId,
								
									@modifiedAccountName AS CreateAccountName,
									
									GETDATE () AS CreateDate,
									
									@modifiedAuthorityName AS ModifiedAuthorityName,
									
									@modifiedAccountId AS ModifiedAccountId,
									
									@modifiedAccountName AS ModifiedAccountName,
									
									GETDATE () AS ModifiedDate
							
								FROM 
								
									@metricValueTable AS MetricValueTable
									
								WHERE MemberMetricId IS NULL
								
											
								
						-- UPDATE MEMBER METRIC FOR EXISTING WATERMARKS WHERE WATERMARK HAS INCREASED

						UPDATE MemberMetric
								
							SET 
							
									MetricValue = MetricValueTable.MetricValue,
									
									ModifiedAuthorityName = @modifiedAuthorityName,
									
									ModifiedAccountId = @modifiedAccountId,
									
									ModifiedAccountName = @modifiedAccountName,
									
									ModifiedDate = GETDATE () 
							
								FROM 
								
									@metricValueTable AS MetricValueTable
									
										JOIN MemberMetric 
										
											ON MetricValueTable.MemberMetricId = MemberMetric.MemberMetricId
											
								WHERE							
								
									(MetricValueTable.MetricValue > MemberMetric.MetricValue)
							  
					END

    END    
              
    