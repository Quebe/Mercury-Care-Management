/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_MembershipTerminateByEvent' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_MembershipTerminateByEvent
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_MembershipTerminateByEvent
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @populationId                      BIGINT --,
      
      --@modifiedAuthorityName  VARCHAR (060),
      --@modifiedAccountId      VARCHAR (060),
      --@modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
          -- 2010-03-06: MODIFIED TO MOVE SERVER-SIDE PROCESSING (T-SQL) TO 
          -- BUSINESS LAYER TO SUPPORT THE OnBeforeMembershiptTerminate Event
        
        
          --DECLARE @populationMembershipId AS BIGINT

          --DECLARE @terminatingEventMemberServiceId AS BIGINT

          --DECLARE @terminatingEventServiceId AS BIGINT

          --DECLARE @terminatingEventDate AS DATETIME    

          --DECLARE PopulationTerminationCursor CURSOR FOR 

            SELECT 

                PopulationMembership.PopulationMembershipId AS PopulationMembershipId,
               
                MemberService.MemberServiceId AS TerminatingEventMemberServiceId,
                
                MemberService.ServiceId AS TerminatingEventServiceId,
                
                MemberService.EventDate AS TerminatingEventDate

              FROM 
              
                PopulationMembership
                
                  JOIN PopulationCriteriaEvent
                  
                    ON PopulationMembership.PopulationId = PopulationCriteriaEvent.PopulationId
                    
                    AND EventType IN (1, 2)
                
              
                  JOIN MemberService 
                  
                    ON PopulationMembership.MemberId = MemberService.MemberId
                    
                    AND PopulationCriteriaEvent.ServiceId = MemberService.ServiceId    
              
              
              WHERE 
                PopulationMembership.PopulationId = @populationId  -- SPECIFIC POPULATION
                
                  AND (GETDATE () BETWEEN PopulationMembership.EffectiveDate AND PopulationMembership.TerminationDate) -- OPEN POPULATION SEGMENT
                  
                  AND (MemberService.EventDate BETWEEN PopulationMembership.AnchorDate AND PopulationMembership.TerminationDate) -- BETWEEN ANCHOR DATE AND TERMINATION DATE
                  
                  -- THIS WAS CHANGED FROM EFFECTIVE DATE TO ANCHOR DATE 2009-10-16
                
              ORDER BY PopulationMembership.MemberId, TerminatingEventDate DESC
              

        /* LOCAL VARIABLES ( END ) */

          --OPEN PopulationTerminationCursor

          --FETCH NEXT FROM PopulationTerminationCursor INTO @populationMembershipId, @terminatingEventMemberServiceId, @terminatingEventServiceId, @terminatingEventDate

          --WHILE (@@FETCH_STATUS = 0) 

            --BEGIN 
            
              --EXEC PopulationProcess_MembershipTerminate @populationMembershipId, @terminatingEventMemberServiceId, @terminatingEventServiceId, @terminatingEventDate, 
                
                --@modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName    
              
              --FETCH NEXT FROM PopulationTerminationCursor INTO @populationMembershipId, @terminatingEventMemberServiceId, @terminatingEventServiceId, @terminatingEventDate

            --END 
            
          --CLOSE PopulationTerminationCursor

          --DEALLOCATE PopulationTerminationCursor
                  
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_MembershipTerminateByEvent TO PUBLIC
GO          
*/