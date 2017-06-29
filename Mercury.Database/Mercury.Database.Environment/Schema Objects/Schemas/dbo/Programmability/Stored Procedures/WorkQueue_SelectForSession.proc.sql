/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'WorkQueue_SelectForSession' AND type = 'P'))
  DROP PROCEDURE WorkQueue_SelectForSession
GO      
*/

CREATE PROCEDURE dbo.WorkQueue_SelectForSession
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId    BIGINT,
      @userAccountId          VARCHAR (060)
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

          DECLARE @SessionWorkQueues AS TABLE (WorkQueueId BIGINT, WorkQueuePermission INT)

        /* LOCAL VARIABLES ( END ) */


          INSERT INTO @SessionWorkQueues

            SELECT 

                WorkQueue.WorkQueueId,

                MAX (WorkQueueTeam.WorkQueuePermission) AS WorkQueuePermission

              FROM WorkQueue 

                JOIN WorkQueueTeam ON WorkQueue.WorkQueueId = WorkQueueTeam.WorkQueueId

                JOIN WorkTeamMembership ON WorkQueueTeam.WorkTeamId = WorkTeamMembership.WorkTeamId

              WHERE ((WorkQueue.Visible = 1) AND (WorkQueue.Enabled = 1))

                AND WorkQueue.WorkQueueId NOT IN (

                  SELECT WorkQueueTeam.WorkQueueId 

                    FROM 

                      WorkQueueTeam 

                        JOIN WorkTeamMembership ON WorkQueueTeam.WorkTeamId = WorkTeamMembership.WorkTeamId

                    WHERE (WorkQueueTeam.WorkQueuePermission = 0)

                    AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId

                  )

                AND SecurityAuthorityId = @securityAuthorityId AND UserAccountId = @userAccountId

              GROUP BY WorkQueue.WorkQueueId


          SELECT WorkQueue.*, SessionWorkQueues.WorkQueuePermission 

            FROM WorkQueue 

              JOIN @SessionWorkQueues AS SessionWorkQueues 
                
                ON WorkQueue.WorkQueueId = SessionWorkQueues.WorkQueueId

            ORDER BY WorkQueueName
                    
    END    
              
    