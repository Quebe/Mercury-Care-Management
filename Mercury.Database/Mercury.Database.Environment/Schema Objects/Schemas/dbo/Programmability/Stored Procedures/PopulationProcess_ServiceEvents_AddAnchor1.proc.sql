
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_AddAnchor1' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor1
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor1
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceEventId                    BIGINT,
      
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
        
          INSERT INTO PopulationMembershipServiceEvent
                      
            SELECT  

                PopulationMembershipServiceEvent.PopulationMembershipId, 
                PopulationMembershipServiceEvent.PopulationServiceEventId, 
                PopulationMembershipServiceEvent.ExpectedServiceDate,
                PopulationMembershipServiceEvent.MemberServiceId,
                PopulationMembershipServiceEvent.EventDate,
                PopulationMembershipServiceEvent.PreviousMemberServiceId,
                PopulationMembershipServiceEvent.PreviousEventDate,
                CAST (NULL AS   BIGINT) AS ParentMembershipServiceEventId,
                CAST (NULL AS DATETIME) AS ParentMembershipServiceEventDate,
                PopulationMembershipServiceEvent.PreviousThresholdId,
                PopulationMembershipServiceEvent.PreviousThresholdDate,
                PopulationMembershipServiceEvent.NextThresholdDate,
                CAST (1 AS INT) AS Status,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()
              
              FROM 
                [dbo].[PopulationProcess_ServiceEvents_AddAnchor1_SupportView] AS PopulationMembershipServiceEvent

                  LEFT JOIN 
                  
                    (SELECT PopulationMembershipId, PopulationServiceEventId, MAX (PreviousEventDate) AS PreviousEventDate 
                    
                      FROM [dbo].[PopulationProcess_ServiceEvents_AddAnchor1_SupportView] 
                      
                      GROUP BY PopulationMembershipId, PopulationServiceEventId
                      
                    ) AS MaxPreviousEvent
                    
                    ON PopulationMembershipServiceEvent.PopulationMembershipId = MaxPreviousEvent.PopulationMembershipId 
                    
                    AND PopulationMembershipServiceEvent.PopulationServiceEventId = MaxPreviousEvent.PopulationServiceEventId
                   
              WHERE
                ((PopulationMembershipServiceEvent.PreviousEventDate = MaxPreviousEvent.PreviousEventDate) OR (PopulationMembershipServiceEvent.PreviousEventDate IS NULL))

                AND (PopulationMembershipServiceEvent.PopulationServiceEventId = @serviceEventId)
              

        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_ServiceEvents_AddAnchor1 TO PUBLIC
GO          
*/