/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_RemovedFromPopulation' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_RemovedFromPopulation
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_RemovedFromPopulation 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId                    BIGINT
      
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
                  
          DELETE FROM PopulationMembershipServiceEvent 

            FROM 
              PopulationMembershipServiceEvent  

                JOIN PopulationMembership ON PopulationMembershipServiceEvent.PopulationMembershipId = PopulationMembership.PopulationMembershipId
                
                  AND PopulationMembership.PopulationId = @populationId

                LEFT JOIN PopulationServiceEvent
                
                  ON PopulationMembershipServiceEvent.PopulationServiceEventId = PopulationServiceEvent.PopulationServiceEventId
              
            WHERE PopulationServiceEvent.PopulationServiceEventId IS NULL
        
        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_ServiceEvents_RemovedFromPopulation TO PUBLIC
GO          
*/