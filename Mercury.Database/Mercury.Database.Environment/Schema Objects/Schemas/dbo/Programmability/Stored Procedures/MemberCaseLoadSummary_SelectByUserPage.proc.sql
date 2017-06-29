/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseLoadSummary_SelectByUser' AND type = 'P'))
  DROP PROCEDURE MemberCaseLoadSummary_SelectByUser
GO      
*/

CREATE PROCEDURE dbo.MemberCaseLoadSummary_SelectByUser
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId   BIGINT,
			@userAccountId        VARCHAR (060),
      @showClosed             BIT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 

    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
				
			SELECT WorkTeam.WorkTeamId, WorkTeam.WorkTeamName, 
			
					-- WorkTeamMembership.UserDisplayName, 

					SUM (CASE WHEN (MemberCase.Status = 1) THEN 1 ELSE 0 END) AS StatusUnderDevelopmentCount,
		
					SUM (CASE WHEN (MemberCase.Status = 2) THEN 1 ELSE 0 END) AS StatusActiveCount,
		
					SUM (CASE WHEN (MemberCase.Status = 3) THEN 1 ELSE 0 END) AS StatusClosedCount

				FROM 
	
					MemberCase
		
						LEFT JOIN WorkTeam 
			
							ON MemberCase.AssignedToWorkTeamId = WorkTeamId
				
						LEFT JOIN WorkTeamMembership 
			
							ON WorkTeam.WorkTeamId = WorkTeamMembership.WorkTeamId
				
				WHERE 
    
					((MemberCase.Status IN (1, 2)) OR (@showClosed = 1)) -- ACTIVE/UNDERDEVELOPMENT, OR SHOW ALL
    
					AND (((WorkTeamMembership.SecurityAuthorityId = @securityAuthorityId) AND (WorkTeamMembership.UserAccountId = @userAccountId))
		
						OR (MemberCase.AssignedToWorkTeamId IS NULL))
				
				GROUP BY 
	
					WorkTeam.WorkTeamId, WorkTeam.WorkTeamName -- , WorkTeamMembership.UserDisplayName		
		
				ORDER BY WorkTeamName
	
          
    END    
              
    