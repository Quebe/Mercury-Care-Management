/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'DataExplorerNodeEvaluation_PopulationMembership_Execute' AND type = 'P'))
  DROP PROCEDURE dal.DataExplorerNodeEvaluation_PopulationMembership_Execute
GO      
*/

CREATE PROCEDURE [dal].[DataExplorerNodeEvaluation_PopulationMembership_Execute] 
    /* STORED PROCEDURE - INPUTS (BEGIN) */
			
			@nodeInstanceId UNIQUEIDENTIFIER,

			@populationId        BIGINT,

			@populationTypeId    BIGINT,

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

				INSERT INTO DataExplorerNodeResultDetail (DataExplorerNodeInstanceId, Id, DetailId) 

					SELECT DISTINCT 

							@nodeInstanceId, 

							MemberId,

							PopulationMembership.PopulationMembershipId

						FROM 
	
							PopulationMembership
		
								JOIN Population	
			
									ON PopulationMembership.PopulationId = Population.PopulationId
		
						WHERE 
	
							-- POPULATION/POPULATION TYPE CRITERIA
		
							(((PopulationMembership.PopulationId = @populationId) OR (@populationId = 0))
		
								AND ((Population.PopulationTypeId = @populationTypeId) OR (@populationTypeId = 0)))

							AND ( -- DATE CRITERIA
		
								((@startDate BETWEEN PopulationMembership.EffectiveDate	AND PopulationMembership.TerminationDate))
			
								AND ((@endDate >= PopulationMembership.EffectiveDate) OR (@endDate IS NULL))
			
							) -- DATE CRITERIA
							
				INSERT INTO DataExplorerNodeResult (DataExplorerNodeInstanceId, Id) 

					SELECT DISTINCT 

							@nodeInstanceId, 

							Id

						FROM DataExplorerNodeResultDetail

						WHERE DataExplorerNodeInstanceId = @nodeInstanceId
				
				SET @rowCount = @@ROWCOUNT

	    END    
              

