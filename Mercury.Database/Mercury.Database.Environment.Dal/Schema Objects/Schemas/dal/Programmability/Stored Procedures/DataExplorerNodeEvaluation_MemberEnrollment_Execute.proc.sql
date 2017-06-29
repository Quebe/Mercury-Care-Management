/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeEvaluation_MemberEnrollment_Execute' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeEvaluation_MemberEnrollment_Execute
GO      
*/

CREATE PROCEDURE [dal].[DataExplorerNodeEvaluation_MemberEnrollment_Execute] 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			
			@nodeInstanceId UNIQUEIDENTIFIER,

			@insurerId            BIGINT,

			@programId            BIGINT,

			@benefitPlanId				BIGINT,

			@continuousEnrollment    BIT,

			@continuousAllowedGaps INT,

			@continuousAllowedGapDays INT,

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

			
				IF (@continuousEnrollment = 0) 

					BEGIN 

						-- NON-CONTINUOUS ENROLLMENT CRITERIA 

						INSERT INTO DataExplorerNodeResultDetail (DataExplorerNodeInstanceId, Id, DetailId)

							SELECT DISTINCT

									@nodeInstanceId, 

									MemberEnrollment.MemberId, 
							
									MemberEnrollment.MemberEnrollmentId

								FROM 
	
									MemberEnrollment
		
										JOIN Program ON MemberEnrollment.ProgramId = Program.ProgramId
			
										LEFT JOIN MemberEnrollmentCoverage 
			
											ON MemberEnrollment.MemberEnrollmentId = MemberEnrollmentCoverage.MemberEnrollmentId
		
		
								WHERE 
	
									((Program.InsurerId = @insurerId) OR (@insurerId = 0))
		
									AND ((MemberEnrollment.ProgramId = @programId) OR (@programId = 0))
		
									AND ((MemberEnrollmentCoverage.BenefitPlanId = @benefitPlanId) OR (@benefitPlanId = 0))
		
									AND (
							
										(((@startDate <= MemberEnrollment.TerminationDate) AND (@endDate >= MemberEnrollment.EffectiveDate)) AND (@benefitPlanId = 0))

										OR (((@startDate <= MemberEnrollmentCoverage.TerminationDate) AND (@endDate >= MemberEnrollmentCoverage.EffectiveDate)) AND (MemberEnrollmentCoverage.BenefitPlanId = @benefitPlanId))

									)

						INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id) 

							SELECT DISTINCT 

									@nodeInstanceId, 

									Id

								FROM DataExplorerNodeResultDetail

								WHERE DataExplorerNodeInstanceId = @nodeInstanceId
				
					END 

				ELSE 

					BEGIN 

						-- CONTINUOUS ENROLLMENT (REQUIRES PROGRAM OR BENEFIT PLAN)

						IF (@benefitPlanId = 0) 

							BEGIN -- PROGRAM

								INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id)
								
									SELECT @nodeInstanceId, MemberId

										FROM 

											(SELECT 

													MemberEnrollment.MemberId,
				
													MemberEnrollment.MemberEnrollmentId,
				
													MemberEnrollment.EffectiveDate,
				
													MemberEnrollment.TerminationDate,
				
													CASE WHEN (MemberEnrollment.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollment.EffectiveDate END AS BoundedEffectiveDate,
				
													CASE WHEN (MemberEnrollment.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollment.TerminationDate END AS BoundedTerminationDate,
				
													ISNULL (MAX (Previous_MemberEnrollment.TerminationDate), DATEADD (DD, -1, @startDate)) AS PreviousTerminationDate,
				
													ISNULL (MIN (Next_MemberEnrollment.EffectiveDate), DATEADD (DD, +1, @endDate)) AS NextEffectiveDate,
				
													DATEDIFF (DD, 
				
															ISNULL (MAX (Previous_MemberEnrollment.TerminationDate), DATEADD (DD, -1, @startDate)),  -- PREVIOUS TERMINATION DATE
					
															CASE WHEN (MemberEnrollment.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollment.EffectiveDate END -- BOUNDED EFFECTIVE DATE
					
														) - 1 
					
														+ CASE 
					
																	WHEN (ISNULL (MIN (Next_MemberEnrollment.EffectiveDate), DATEADD (DD, +1, @endDate)) = DATEADD (DD, +1, @endDate)) THEN  -- WHEN LAST SEGMENT, ADD NEXT GAP DAYS
								
																		DATEDIFF (DD, 
									
																				CASE WHEN (MemberEnrollment.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollment.TerminationDate END, -- BOUNDED TERMINATION DATE 
											
																				ISNULL (MIN (Next_MemberEnrollment.EffectiveDate), DATEADD (DD, +1, @endDate)) -- NEXT EFFECTIVE DATE 
											
																			) - 1
				
																	ELSE 0 
								
																END 	
									
														AS GapDays,
					
					
													CASE 
				
															WHEN ((DATEDIFF (DD, 
				
																	ISNULL (MAX (Previous_MemberEnrollment.TerminationDate), DATEADD (DD, -1, @startDate)),  -- PREVIOUS TERMINATION DATE
					
																	CASE WHEN (MemberEnrollment.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollment.EffectiveDate END -- BOUNDED EFFECTIVE DATE
					
																) - 1) > 0) THEN 1 ELSE 0 END
					
														+ CASE 
					
																	WHEN (CASE 
					
																			WHEN (ISNULL (MIN (Next_MemberEnrollment.EffectiveDate), DATEADD (DD, +1, @endDate)) = DATEADD (DD, +1, @endDate)) THEN  -- WHEN LAST SEGMENT, ADD NEXT GAP DAYS
										
																				DATEDIFF (DD, 
											
																						CASE WHEN (MemberEnrollment.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollment.TerminationDate END, -- BOUNDED TERMINATION DATE 
													
																						ISNULL (MIN (Next_MemberEnrollment.EffectiveDate), DATEADD (DD, +1, @endDate)) -- NEXT EFFECTIVE DATE 
													
																					) - 1
					
																			ELSE 0 END > 0) THEN 1 ELSE 0 END
									
														AS Gaps
				
													-- GAPS = IF GAP DAYS BETWEEN PREVIOUS AND CURRENT, THEN 1 ELSE 0 
				
													--   + IF BoundedEndDate = DATEADD (DD, +1, @endDate) [I.E. LAST SEGMENT IN TIME PERIOD]
				
													--     THEN IF GAP DAYS BETWEEN CURRENT AND NEXT, THEN 1 ELSE 0 

												FROM 
			
													MemberEnrollment
				
														LEFT JOIN MemberEnrollment AS Previous_MemberEnrollment
					
															ON MemberEnrollment.MemberId = Previous_MemberEnrollment.MemberId
						
																AND MemberEnrollment.MemberEnrollmentId <> Previous_MemberEnrollment.MemberEnrollmentId
							
																AND MemberEnrollment.ProgramId = Previous_MemberEnrollment.ProgramId
							
																AND MemberEnrollment.EffectiveDate > Previous_MemberEnrollment.TerminationDate
							
																AND Previous_MemberEnrollment.TerminationDate >= @startDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
							
														LEFT JOIN MemberEnrollment AS Next_MemberEnrollment
								
															ON MemberEnrollment.MemberId = Next_MemberEnrollment.MemberId
						
																AND MemberEnrollment.MemberEnrollmentId <> Next_MemberEnrollment.MemberEnrollmentId
							
																AND MemberEnrollment.ProgramId = Next_MemberEnrollment.ProgramId
						
																AND MemberEnrollment.TerminationDate < Next_MemberEnrollment.EffectiveDate
							
																AND Next_MemberEnrollment.EffectiveDate <= @endDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
							
												WHERE 
			
													MemberEnrollment.ProgramId = @programId					
				
														AND ((MemberEnrollment.EffectiveDate <= @endDate) AND (MemberEnrollment.TerminationDate >= @startDate))
					
												GROUP BY 
			
													MemberEnrollment.MemberId,
				
													MemberEnrollment.MemberEnrollmentId,
				
													MemberEnrollment.EffectiveDate,
				
													MemberEnrollment.TerminationDate
				
											) AS MemberEnrollmentDetail

									GROUP BY MemberId

									HAVING (SUM (Gaps) <= @continuousAllowedGaps) AND (SUM (GapDays) <= @continuousAllowedGapDays)

							END

						ELSE

							BEGIN -- BENEFIT PLAN

								-- BENEFIT PLAN IS SPECIFIC TO A INSURER->PROGRAM RELATIONSHIP
								
								SELECT MemberId, SUM (Gaps) AS Gaps, SUM (GapDays) AS GapDays

									FROM 

										(SELECT 

												MemberEnrollment.MemberId,
				
												MemberEnrollmentCoverage.MemberEnrollmentCoverageId,
				
												MemberEnrollmentCoverage.EffectiveDate,
				
												MemberEnrollmentCoverage.TerminationDate,
				
												CASE WHEN (MemberEnrollmentCoverage.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollmentCoverage.EffectiveDate END AS BoundedEffectiveDate,
				
												CASE WHEN (MemberEnrollmentCoverage.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollmentCoverage.TerminationDate END AS BoundedTerminationDate,
				
												ISNULL (MAX (Previous_MemberEnrollmentCoverage.TerminationDate), DATEADD (DD, -1, @startDate)) AS PreviousTerminationDate,
				
												ISNULL (MIN (Next_MemberEnrollmentCoverage.EffectiveDate), DATEADD (DD, +1, @endDate)) AS NextEffectiveDate,
				
												DATEDIFF (DD, 
				
														ISNULL (MAX (Previous_MemberEnrollmentCoverage.TerminationDate), DATEADD (DD, -1, @startDate)),  -- PREVIOUS TERMINATION DATE
					
														CASE WHEN (MemberEnrollmentCoverage.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollmentCoverage.EffectiveDate END -- BOUNDED EFFECTIVE DATE
					
													) - 1 
					
													+ CASE 
					
																WHEN (ISNULL (MIN (Next_MemberEnrollmentCoverage.EffectiveDate), DATEADD (DD, +1, @endDate)) = DATEADD (DD, +1, @endDate)) THEN  -- WHEN LAST SEGMENT, ADD NEXT GAP DAYS
								
																	DATEDIFF (DD, 
									
																			CASE WHEN (MemberEnrollmentCoverage.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollmentCoverage.TerminationDate END, -- BOUNDED TERMINATION DATE 
											
																			ISNULL (MIN (Next_MemberEnrollmentCoverage.EffectiveDate), DATEADD (DD, +1, @endDate)) -- NEXT EFFECTIVE DATE 
											
																		) - 1
				
																ELSE 0 
								
															END 	
									
													AS GapDays,
					
					
												CASE 
				
														WHEN ((DATEDIFF (DD, 
				
																ISNULL (MAX (Previous_MemberEnrollmentCoverage.TerminationDate), DATEADD (DD, -1, @startDate)),  -- PREVIOUS TERMINATION DATE
					
																CASE WHEN (MemberEnrollmentCoverage.EffectiveDate < @startDate) THEN @startDate ELSE MemberEnrollmentCoverage.EffectiveDate END -- BOUNDED EFFECTIVE DATE
					
															) - 1) > 0) THEN 1 ELSE 0 END
					
													+ CASE 
					
																WHEN (CASE 
					
																		WHEN (ISNULL (MIN (Next_MemberEnrollmentCoverage.EffectiveDate), DATEADD (DD, +1, @endDate)) = DATEADD (DD, +1, @endDate)) THEN  -- WHEN LAST SEGMENT, ADD NEXT GAP DAYS
										
																			DATEDIFF (DD, 
											
																					CASE WHEN (MemberEnrollmentCoverage.TerminationDate > @endDate) THEN @endDate ELSE MemberEnrollmentCoverage.TerminationDate END, -- BOUNDED TERMINATION DATE 
													
																					ISNULL (MIN (Next_MemberEnrollmentCoverage.EffectiveDate), DATEADD (DD, +1, @endDate)) -- NEXT EFFECTIVE DATE 
													
																				) - 1
					
																		ELSE 0 END > 0) THEN 1 ELSE 0 END
									
													AS Gaps
				
												-- GAPS = IF GAP DAYS BETWEEN PREVIOUS AND CURRENT, THEN 1 ELSE 0 
				
												--   + IF BoundedEndDate = DATEADD (DD, +1, @endDate) [I.E. LAST SEGMENT IN TIME PERIOD]
				
												--     THEN IF GAP DAYS BETWEEN CURRENT AND NEXT, THEN 1 ELSE 0 

											FROM 
			
												MemberEnrollment 
			
													JOIN MemberEnrollmentCoverage ON MemberEnrollment.MemberEnrollmentId = MemberEnrollmentCoverage.MemberEnrollmentId
				
													LEFT JOIN MemberEnrollment AS Previous_MemberEnrollment
					
														ON MemberEnrollment.MemberId = Previous_MemberEnrollment.MemberId
						
															AND MemberEnrollment.MemberEnrollmentId <> Previous_MemberEnrollment.MemberEnrollmentId
							
															AND MemberEnrollment.EffectiveDate > Previous_MemberEnrollment.TerminationDate
							
															AND Previous_MemberEnrollment.TerminationDate >= @startDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
				
													LEFT JOIN MemberEnrollmentCoverage AS Previous_MemberEnrollmentCoverage
							
															ON Previous_MemberEnrollment.MemberEnrollmentId = Previous_MemberEnrollmentCoverage.MemberEnrollmentId
						
															AND MemberEnrollmentCoverage.MemberEnrollmentCoverageId <> Previous_MemberEnrollmentCoverage.MemberEnrollmentCoverageId
							
															AND MemberEnrollmentCoverage.BenefitPlanId = Previous_MemberEnrollmentCoverage.BenefitPlanId
							
															AND MemberEnrollmentCoverage.EffectiveDate > Previous_MemberEnrollmentCoverage.TerminationDate
							
															AND Previous_MemberEnrollmentCoverage.TerminationDate >= @startDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
							
													LEFT JOIN MemberEnrollment AS Next_MemberEnrollment
								
														ON MemberEnrollment.MemberId = Next_MemberEnrollment.MemberId
						
															AND MemberEnrollment.MemberEnrollmentId <> Next_MemberEnrollment.MemberEnrollmentId
							
															AND MemberEnrollment.TerminationDate < Next_MemberEnrollment.EffectiveDate
							
															AND Next_MemberEnrollment.EffectiveDate <= @endDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
							
													LEFT JOIN MemberEnrollmentCoverage AS Next_MemberEnrollmentCoverage
								
															ON Previous_MemberEnrollment.MemberEnrollmentId = Previous_MemberEnrollmentCoverage.MemberEnrollmentId
						
															AND MemberEnrollmentCoverage.MemberEnrollmentCoverageId <> Next_MemberEnrollmentCoverage.MemberEnrollmentCoverageId
							
															AND MemberEnrollmentCoverage.BenefitPlanId = Next_MemberEnrollmentCoverage.BenefitPlanId
						
															AND MemberEnrollmentCoverage.TerminationDate < Next_MemberEnrollmentCoverage.EffectiveDate
							
															AND Next_MemberEnrollmentCoverage.EffectiveDate <= @endDate -- ENROLLMENT SEGMENT MUST OVERLAP INTO REQUIRED DATES
							
											WHERE 
			
												MemberEnrollmentCoverage.BenefitPlanId = @benefitPlanId					
				
													AND ((MemberEnrollmentCoverage.EffectiveDate <= @endDate) AND (MemberEnrollmentCoverage.TerminationDate >= @startDate))
					
											GROUP BY 
			
												MemberEnrollment.MemberId,
				
												MemberEnrollmentCoverage.MemberEnrollmentCoverageId,
				
												MemberEnrollmentCoverage.EffectiveDate,
				
												MemberEnrollmentCoverage.TerminationDate
				
										) AS MemberEnrollmentCoverageDetail

								GROUP BY MemberId

								HAVING (SUM (Gaps) <= @continuousAllowedGaps) AND (SUM (GapDays) <= @continuousAllowedGapDays)

							END

					END 
				
				SET @rowCount = @@ROWCOUNT

	    END    
              

