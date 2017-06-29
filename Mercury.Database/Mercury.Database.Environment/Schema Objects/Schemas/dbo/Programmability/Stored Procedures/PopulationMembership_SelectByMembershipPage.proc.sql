/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationMembership_SelectByMembershipPage' AND type = 'P'))
  DROP PROCEDURE PopulationMembership_SelectByMembershipPage
GO      
*/

CREATE PROCEDURE dbo.PopulationMembership_SelectByMembershipPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId        BIGINT,
      @name               VARCHAR (060),
      @initialRow          BIGINT,
      @count                  INT
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

            PopulationMembershipResult.PopulationMembershipId,

            PopulationMembershipResult.PopulationId, 

            PopulationMembershipResult.MemberId,

            PopulationMembershipResult.EffectiveDate,

            PopulationMembershipResult.MemberName,

            MAX (ISNULL (ServiceEvent.Status, 0)) AS Status

          FROM (

            SELECT 

                PopulationMembershipPage.*

              FROM (

                SELECT ROW_NUMBER () OVER (ORDER BY Entity.NameLast, Entity.NameFirst, Entity.NameMiddle, Member.MemberId) AS RowNumber,

                    PopulationMembership.PopulationMembershipId,

                    PopulationMembership.PopulationId, 

                    PopulationMembership.MemberId,

                    PopulationMembership.EffectiveDate,

                    Entity.EntityName AS MemberName

                  FROM 

                    PopulationMembership
                    
											JOIN dbo.Member AS Member ON PopulationMembership.MemberId = Member.MemberId

											JOIN dbo.Entity AS Entity ON Member.EntityId = Entity.EntityId
								
                  WHERE PopulationId = @populationId

                    AND GETDATE () BETWEEN EffectiveDate AND TerminationDate
                    
                    AND Entity.EntityName LIKE (@name + '%')
                  
                ) AS PopulationMembershipPage

              WHERE PopulationMembershipPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)

            ) AS PopulationMembershipResult

            LEFT JOIN PopulationMembershipServiceEvent AS ServiceEvent
  
              ON PopulationMembershipResult.PopulationMembershipId = ServiceEvent.PopulationMembershipId
  
              AND (ServiceEvent.EventDate IS NULL)

          GROUP BY 

            PopulationMembershipResult.PopulationMembershipId,

            PopulationMembershipResult.PopulationId, 

            PopulationMembershipResult.MemberId,

            PopulationMembershipResult.EffectiveDate,

            PopulationMembershipResult.MemberName

    END    
              
    
