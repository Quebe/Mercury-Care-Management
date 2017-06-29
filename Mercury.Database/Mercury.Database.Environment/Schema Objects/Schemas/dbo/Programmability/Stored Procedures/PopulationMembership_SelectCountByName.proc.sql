/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationMembership_SelectCountByName' AND type = 'P'))
  DROP PROCEDURE PopulationMembership_SelectCountByName
GO      
*/

CREATE PROCEDURE dbo.PopulationMembership_SelectCountByName
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId        BIGINT,
      @name               VARCHAR (060)
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        SELECT COUNT (1)

            FROM 

              PopulationMembership
              
								JOIN dbo.Member AS Member ON PopulationMembership.MemberId = Member.MemberId
								
								JOIN dbo.Entity AS Entity ON Member.EntityId = Entity.EntityId

            WHERE PopulationId = @populationId

              AND GETDATE () BETWEEN EffectiveDate AND TerminationDate
              
              AND Entity.EntityName LIKE (@name + '%')
            
          
    END    
              
    
