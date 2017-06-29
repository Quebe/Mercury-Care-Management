
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_MembershipTerminate' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_MembershipTerminate
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_MembershipTerminate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationMembershipId            BIGINT,
      @terminatingEventMemberServiceId   BIGINT,
      @terminatingEventServiceId         BIGINT,
      @terminatingEventDate             DATETIME,
      
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

      
        BEGIN TRANSACTION 
    
          -- CLOSE OUT THE MEMBERSHIP SEGMENT
          
          UPDATE PopulationMembership
            
            SET 
              
              TerminationDate = CAST (CONVERT (CHAR (010), GETDATE (), 101) AS DATETIME),
              
              TerminatingEventMemberServiceId = @terminatingEventMemberServiceId,
              
              TerminatingEventServiceId = @terminatingEventServiceId,
              
              TerminatingEventDate = @terminatingEventDate,
              
              ModifiedAuthorityName = @modifiedAuthorityName,
              
              ModifiedAccountId = @modifiedAccountId,
              
              ModifiedAccountName = @modifiedAccountName,
              
              ModifiedDate = GETDATE ()
              
            WHERE 
            
              PopulationMembershipId = @populationMembershipId
              
              AND (TerminationDate > GETDATE ())
              
              
          IF (@@ROWCOUNT > 0) 
          
            BEGIN
              
              -- REMOVE ANY OPEN EXPECTED EVENTS THAT HAVE NOT OCCURRED            
                  
              DELETE FROM PopulationMembershipServiceEvent            
              
                WHERE PopulationMembershipId = @populationMembershipId
                
                  AND EventDate IS NULL
                  
                  
              -- INSERT ACTION 
              
              INSERT INTO PopulationAction (PopulationId, PopulationMembershipId, 
          
				SenderObjectType, SenderId, EventInstanceId, ScheduleDate, 
				
				ActionDate, ActionId, ActionParameters, ActionDescription,
				
				Exception)
              
                SELECT 
                    
                    Population.PopulationId, 
                    
                    @populationMembershipId, 
                    
                    'PopulationEvent' AS SenderObjectType, 
                    
                    2 AS SenderId, 
                    
                    @populationMembershipId AS EventInstanceId,
                    
                    CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS ScheduleDate, 
                    
                    CAST (NULL AS DATETIME) AS ActionDate, 
                                       
                    Population.OnMembershipTerminateActionId   AS ActionId,
                    
                    Population.OnMembershipTerminateActionParameters AS ActionParameters,
                    
                    Population.OnMembershipTerminateActionDescription AS ActionDescription,
                    
                    NULL AS Exception
                    
                  FROM 
                  
                    PopulationMembership JOIN Population ON PopulationMembership.PopulationId = Population.PopulationId
                    
                  WHERE 
                  
                    (PopulationMembership.PopulationMembershipId = @populationMembershipId)
                    
                    AND (Population.OnMembershipTerminateActionId > 0)
                
            END                
            
        COMMIT TRANSACTION             
            
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_MembershipTerminate TO PUBLIC
GO          
*/