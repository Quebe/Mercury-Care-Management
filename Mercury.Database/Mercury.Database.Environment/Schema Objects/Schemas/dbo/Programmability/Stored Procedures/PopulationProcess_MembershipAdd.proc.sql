/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_MembershipAdd' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_MembershipAdd
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_MembershipAdd
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId                      BIGINT,
      @memberId                          BIGINT,
      @anchorDate                      DATETIME,
      @identifyingEventMemberServiceId   BIGINT,
      @identifyingEventServiceId         BIGINT,
      @identifyingEventDate             DATETIME,
      
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
        
        DECLARE @populationMembershipId AS BIGINT
        
        /* LOCAL VARIABLES ( END ) */
        
        BEGIN TRANSACTION   
      
          INSERT INTO dbo.PopulationMembership (
          
              PopulationId, MemberId, EffectiveDate, TerminationDate, AnchorDate,
              
              IdentifyingEventMemberServiceId, IdentifyingEventServiceId, IdentifyingEventDate,
          
              CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
              
              ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
              
          VALUES (
          
              @populationId, @memberId, CONVERT (DATE, GETDATE ()), '12/31/9999', @anchorDate,
              
              @identifyingEventMemberServiceId, @identifyingEventServiceId, @identifyingEventDate,

              @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
              
              @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())        
              
          
          SET @populationMembershipId = @@IDENTITY
          
          
          INSERT INTO PopulationAction (PopulationId, PopulationMembershipId, 
          
				SenderObjectType, SenderId, EventInstanceId, ScheduleDate, 
				
				ActionDate, ActionId, ActionParameters, ActionDescription,
				
				Exception)
          
            SELECT 
                
                Population.PopulationId, 
                
                @populationMembershipId, 
                
                'PopulationEvent' AS SenderObjectType, 
                
                1 AS SenderId, 
                
                @populationMembershipId AS EventInstanceId,
                
                CAST (CONVERT (CHAR (10), GETDATE (), 101) AS DATETIME) AS ScheduleDate, 
                
                CAST (NULL AS DATETIME) AS ActionDate, 
                                
                Population.OnMembershipAddActionId   AS ActionId,
                
                Population.OnMembershipAddActionParameters AS ActionParameters,
                
                Population.OnMembershipAddActionDescription AS ActionDescription,
                
                NULL AS Exception
                
              FROM 
              
                Population 
                
              WHERE 
              
                (Population.PopulationId = @populationId) AND (Population.OnMembershipAddActionId > 0)
                
          
        COMMIT TRANSACTION 
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.PopulationProcess_MembershipAdd TO PUBLIC
GO          
*/