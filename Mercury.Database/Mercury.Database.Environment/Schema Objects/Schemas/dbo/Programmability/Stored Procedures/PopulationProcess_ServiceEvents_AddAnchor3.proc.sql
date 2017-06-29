
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_AddAnchor3' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor3
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_AddAnchor3
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
              -- POPULATION MEMBERSHIP SERVICE EVENT ID
              PopulationMembershipId,
              PopulationServiceEventId,
              ExpectedServiceDate,
              MemberServiceId,
              EventDate,
              PreviousMemberServiceId,              
              PreviousEventDate,
              CAST (NULL AS   BIGINT) AS ParentMembershipServiceEventId,
              CAST (NULL AS DATETIME) AS ParentMembershipServiceEventDate,
              PreviousThresholdId,
              PreviousThresholdDate,
              NextThresholdDate,
              CAST (1 AS INT) Status,

              @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
              @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ()
            
            FROM 
              PopulationProcess_ServiceEvents_AddAnchor3_SupportView AS SupportView

            WHERE 
           
              (PopulationServiceEventId = @serviceEventId) -- ONLY EVALUATE SPECIFIC SERVICE EVENT
              
              -- AND (ExpectedServiceDate BETWEEN PopulationEffectiveDate AND PopulationTerminationDate)
              
              AND (ExpectedServiceDate BETWEEN PopulationAnchorDate AND PopulationTerminationDate)


        COMMIT TRANSACTION
    
      /* STORED PROCEDURE ( END ) */
      
    END
    
    