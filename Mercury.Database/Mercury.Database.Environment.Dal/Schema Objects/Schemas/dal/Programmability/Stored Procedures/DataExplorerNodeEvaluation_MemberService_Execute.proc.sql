/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeEvaluation_MemberService_Execute' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeEvaluation_MemberService_Execute
GO      
*/

CREATE PROCEDURE [dal].[DataExplorerNodeEvaluation_MemberService_Execute] 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			
			@nodeInstanceId UNIQUEIDENTIFIER,

			@serviceId            BIGINT,

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
				
							SELECT DISTINCT @nodeInstanceId, MemberService.MemberId, MemberService.MemberServiceId 

								FROM MemberService
	
								WHERE 
	
									(MemberService.ServiceId = @serviceId)
		
									AND (MemberService.EventDate BETWEEN @startDate AND @endDate)
		
									AND (MemberService.MemberId IN (

										SELECT MemberService.MemberId

											FROM MemberService
				
											WHERE 
				
												(MemberService.ServiceId = @serviceId)
					
												AND (MemberService.EventDate BETWEEN @startDate AND @endDate)
					
											GROUP BY MemberId
				
											HAVING COUNT (1) >= @countOf
				
										)) -- COUNT OF MATCH CRITERIA

							END

				ELSE

					BEGIN

						INSERT INTO DataExplorerNodeResultDetail (DataExplorerNodeInstanceId, Id, DetailId) 
				
							SELECT DISTINCT @nodeInstanceId, MemberService.MemberId, MemberService.MemberServiceId 

								FROM MemberService
	
								WHERE 
	
									(MemberService.ServiceId = @serviceId)
		
									AND (MemberService.EventDate BETWEEN @startDate AND @endDate)
		
									AND (MemberService.MemberId IN (

										SELECT MemberService.MemberId

											FROM MemberService

												JOIN Member ON MemberService.MemberId = Member.MemberId
				
											WHERE 
				
												(MemberService.ServiceId = @serviceId)
					
												AND (MemberService.EventDate BETWEEN @startDate AND @endDate)

												AND (-- AGE CRITERIA

														 ((@ageQualifier = 0) AND (DATEDIFF (DAY, Member.BirthDate, MemberService.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													OR ((@ageQualifier = 1) AND (dbo.AgeInMonthsOnDate (Member.BirthDate, MemberService.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													OR ((@ageQualifier = 2) AND (dbo.AgeInYearsOnDate (Member.BirthDate, MemberService.EventDate) BETWEEN @ageMinimum AND @ageMaximum))

													)

					
											GROUP BY MemberService.MemberId
				
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
              

