/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueueMonitorAgingByWorkQueue' AND type = 'P'))
  DROP PROCEDURE dbo.[WorkQueueMonitorAgingByWorkQueue]
GO      
*/

CREATE PROCEDURE dbo.[WorkQueueMonitorAgingByWorkQueue]
    /* STORED PROCEDURE - INPUTS (BEGIN) */

			@workQueueId AS BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        /* LOCAL VARIABLES ( END ) */
        
				        
				SELECT


-- 					ISNULL (SUM (CASE WHEN (WorkQueueItemAge <= -90) THEN 1 ELSE 0 END), 0) AS [Overdue 90+],
					
-- 					ISNULL (SUM (CASE WHEN (WorkQueueItemAge > -90) AND (WorkQueueItemAge <= -60) THEN 1 ELSE 0 END), 0) AS [Overdue 60-89],

					ISNULL (SUM (CASE WHEN (WorkQueueItemAge <= -60) THEN 1 ELSE 0 END), 0) AS [Overdue 60+],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge > -60) AND (WorkQueueItemAge <= -30) THEN 1 ELSE 0 END), 0) AS [Overdue 30-59],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge > -30) AND (WorkQueueItemAge <= -14) THEN 1 ELSE 0 END), 0) AS [Overdue 14-29],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge > -14) AND (WorkQueueItemAge <= -07) THEN 1 ELSE 0 END), 0) AS [Overdue  7-13],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge > -07) AND (WorkQueueItemAge <= -03) THEN 1 ELSE 0 END), 0) AS [Overdue  3-6],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge = -2) THEN 1 ELSE 0 END), 0) AS [Overdue 2],

					ISNULL (SUM (CASE WHEN (WorkQueueItemAge = -1) THEN 1 ELSE 0 END), 0) AS [Overdue 1],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge = 00) THEN 1 ELSE 0 END), 0) AS [Due Today],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge = 01) THEN 1 ELSE 0 END), 0) AS [Due 1],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge = 02) THEN 1 ELSE 0 END), 0) AS [Due 2],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge >= 03) AND (WorkQueueItemAge < 07) THEN 1 ELSE 0 END), 0) AS [Due  3-6],

					ISNULL (SUM (CASE WHEN (WorkQueueItemAge >= 07) AND (WorkQueueItemAge < 14) THEN 1 ELSE 0 END), 0) AS [Due  7-13],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge >= 14) AND (WorkQueueItemAge < 30) THEN 1 ELSE 0 END), 0) AS [Due 14-29],
					
					ISNULL (SUM (CASE WHEN (WorkQueueItemAge >= 30) THEN 1 ELSE 0 END), 0) AS [Due 30+]
					
					FROM 

						(SELECT 

								WorkQueueItemId,
								
								DueDate,
								
								DATEDIFF (DD, GETDATE (), DueDate) AS WorkQueueItemAge

							FROM 
							
								WorkQueueItem
								
							WHERE 
							
								(CompletionDate IS NULL) AND (WorkQueueId = @workQueueId)
							
						) AS Detail
								
			
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/