/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueMonitorSummary' AND type = 'P'))
  DROP PROCEDURE dbo.[WorkQueueMonitorSummary]
GO      
*/

CREATE PROCEDURE [dbo].[WorkQueueMonitorSummary] AS

	BEGIN 

		DECLARE @workQueueSummary AS TABLE (

				WorkQueueId					BIGINT,
				
				WorkQueueName				VARCHAR (060),
				
				FirstWorkedTime			DATETIME,
				
				LastWorkedTime			DATETIME,
				
				WorkedItemsCount		INT,
				
				CompletedItemsCount INT,
				
				AvailableItemsCount INT,
				
				TotalItemsCount			INT,
				
				WarningItemsCount   INT,
				
				OverdueItemsCount   INT,
				
				UsersInQueueCount   INT
				
			)
			

		DECLARE @workTimeRestrictions AS VARCHAR (8000)

		SET @workTimeRestrictions = '(DayOfWeekTimes/Day[@DayOfWeek="' + CAST ((DATEPART (WEEKDAY, GETDATE ()) - 1) AS CHAR (01)) + '"]/Time[@StartTime <= "' + CONVERT (CHAR (08), GETDATE (), 114) + '" and @EndTime >= "' + CONVERT (CHAR (08), GETDATE (), 114) + '"])[1]'


		INSERT INTO @workQueueSummary

			SELECT WorkQueueId, WorkQueueName, NULL, NULL, 0, 0, 0, 0, 0, 0, 0 FROM WorkQueue
				
				
		UPDATE @workQueueSummary

			SET 
			
				FirstWorkedTime = WorkQueueTime.FirstWorkedTime,
				
				LastWorkedTime = WorkQueueTime.LastWorkedTime,
				
				WorkedItemsCount = WorkQueueTime.WorkedItemsCount,
				
				CompletedItemsCount = WorkQueueTime.CompletedItemsCount

			FROM 
			
				@workQueueSummary AS SummaryTable
				
					JOIN 		
								
						(SELECT 

							WorkQueueId,

								MIN (FirstWorkedTime) AS FirstWorkedTime,

								MAX (LastWorkedDate) AS LastWorkedTime,
								
								COUNT (DISTINCT (WorkQueueItemId)) AS WorkedItemsCount,
								
								SUM (CASE WHEN (CompletionDate IS NOT NULL) THEN 1 ELSE 0 END) AS CompletedItemsCount
								

							FROM 
								
						(SELECT 

								WorkQueueItem.WorkQueueId,

								WorkQueueItem.WorkQueueItemId,
								
								WorkQueueItem.CompletionDate,

								MIN (WorkQueueItemWorkflowStep.CreateDate) AS FirstWorkedTime,
								
								MAX (WorkQueueItem.LastWorkedDate) AS LastWorkedDate
								
							FROM 
							
								WorkQueueItem
								
									JOIN WorkQueueItemWorkflowStep		
									
										ON WorkQueueItem.WorkQueueItemId = WorkQueueItemWorkflowStep.WorkQueueItemId
								
							WHERE 
								
									LastWorkedDate >= CONVERT (CHAR (010), GETDATE (), 101)
									
									AND WorkQueueItemWorkflowStep.CreateDate >= CONVERT (CHAR (010), GETDATE (), 101)
									
							
							GROUP BY 			

								WorkQueueItem.WorkQueueId,

								WorkQueueItem.WorkQueueItemId,
								
								WorkQueueITem.CompletionDate

							) AS FirstWorkDate
							
							
							GROUP BY WorkQueueId	
								
						) AS WorkQueueTime		
						
						ON SummaryTable.WorkQueueId = WorkQueueTime.WorkQueueId				


		UPDATE @workQueueSummary

			SET 
			
				AvailableItemsCount = WorkQueueCount.AvailableCount,
				
				TotalItemsCount = WorkQueueCount.TotalCount,
				
				WarningItemsCount = WorkQueueCount.WarningCount,
				
				OverdueItemsCount = WorkQueueCount.OverdueCount
				
			FROM 
			
				@workQueueSummary AS SummaryTable
				
					JOIN 		
															
						(SELECT 

								WorkQueueItem.WorkQueueId,
								
								SUM (
								
									CASE WHEN 
									
										(WorkQueueItem.ConstraintDate <= GETDATE ()) -- CONSTRAINT DATE HAS PASSED
										
										AND (WorkQueueItem.AssignedToSecurityAuthorityId = 0) -- NOT ASSIGNED (AVAILABLE THROUGH GET WORK)
											
										AND ( -- WITHIN WORK TIME RESTRICTIONS
										
												CASE WHEN (WorkTimeRestrictions IS NULL) THEN 1 
						            
												ELSE CAST ((ISNULL (WorkTimeRestrictions.value ('sql:variable("@workTimeRestrictions")', 'BIT'), 1) - 1) AS BIT) 
												
												END = 1

											)									
											
									THEN 1 ELSE 0 END
										
								) AS AvailableCount,
																								
								COUNT (1) AS TotalCount,
								
								SUM (CASE WHEN (GETDATE () >= WorkQueueItem.ThresholdDate) AND (GETDATE () < WorkQueueItem.DueDate) THEN 1 ELSE 0 END) AS WarningCount,
																
								SUM (CASE WHEN (CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) > WorkQueueItem.DueDate) THEN 1 ELSE 0 END) AS OverdueCount

							FROM 
						  
								WorkQueueItem   		
								
										
							WHERE 
							
								WorkQueueItem.CompletionDate IS NULL
							
							
							GROUP BY 
									
								WorkQueueItem.WorkQueueId			
								
						) AS WorkQueueCount			
						
						ON SummaryTable.WorkQueueId = WorkQueueCount.WorkQueueId				
								
								
		UPDATE @workQueueSummary

			SET 
			
				UsersInQueueCount = WorkQueueUserCount.UserCount
				
				
			FROM 
			
				@workQueueSummary AS SummaryTable
				
					JOIN 		
									
						(SELECT WorkQueueId, COUNT (DISTINCT (UserDisplayName)) AS UserCount 

							FROM 

								(SELECT 

										ROW_NUMBER () OVER (PARTITION BY WorkQueueItemWorkflowStep.UserDisplayName ORDER BY WorkQueueItemWorkflowStep.UserDisplayName, MAX (WorkQueueItemWorkflowStep.StepDate) DESC) AS RowNumber,

										WorkQueueItem.WorkQueueId, 
									  
										WorkQueue.WorkQueueName,	  
									  
										WorkQueueItemWorkflowStep.CreateAccountId, 
										
										WorkQueueItemWorkflowStep.UserDisplayName,
									  
										MAX (WorkQueueItemWorkflowStep.StepDate) AS MaxStepDate

									FROM 
									
										WorkQueueItem 
										
											JOIN WorkQueue
											
												ON WorkQueueItem.WorkQueueId = WorkQueue.WorkQueueId
										
											JOIN WorkQueueItemWorkflowStep
											
												ON WorkQueueItem.WorkQueueItemId = WorkQueueItemWorkflowStep.WorkQueueItemId
										
									WHERE StepDate >= DATEADD (MI, -30, GETDATE ())
																						
									GROUP BY 
									
										WorkQueueItem.WorkQueueId,
									  
										WorkQueue.WorkQueueName,
										
										WorkQueueItemWorkflowStep.CreateAccountId, 
										
										WorkQueueItemWorkflowStep.UserDisplayName
									  
								) AS DetailTable

							WHERE RowNumber = 1	 	  
							
							GROUP BY WorkQueueId) AS WorkQueueUserCount
							
						ON SummaryTable.WorkQueueId = WorkQueueUserCount.WorkQueueId					
			
															
		SELECT * FROM @workQueueSummary ORDER BY WorkQueueName						
	
	END 
	