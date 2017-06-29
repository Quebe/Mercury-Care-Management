/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberCaseSummary_SelectByUserWorkTeamsPage' AND type = 'P'))
  DROP PROCEDURE MemberCaseSummary_SelectByUserWorkTeamsPage
GO      
*/

CREATE PROCEDURE dbo.MemberCaseSummary_SelectByUserWorkTeamsPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId   BIGINT,
			@userAccountId        VARCHAR (060),
      @initialRow             INT,
      @count                  INT,
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

        SELECT MemberCasePage.* 
          
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY MemberCase.MemberId, MemberCase.TerminationDate DESC, MemberCase.EffectiveDate DESC, MemberCase.MemberCaseId DESC) AS RowNumber, 
            
								MemberCase.MemberCaseId,
		
								MemberId,
		
								ReferenceNumber,
		
								MemberCase.Status,
		
								AssignedToWorkTeamId, 
		
								AssignedToWorkTeamDate,
		
								AssignedToSecurityAuthorityId,
		
								AssignedToUserAccountId,
		
								AssignedToUserAccountName,
		
								AssignedToUserDisplayName,
		
								AssignedToDate,
		
								LockedBySecurityAuthorityId, 
		
								LockedByUserAccountId,
		
								LockedByUserAccountName,
		
								LockedByUserDisplayName,
		
								LockedByDate,
		
								MemberCase.EffectiveDate,
		
								MemberCase.TerminationDate

							FROM MemberCase
							
								LEFT JOIN WorkTeamMembership ON MemberCase.AssignedToWorkTeamId = WorkTeamMembership.WorkTeamId
		
		          WHERE 
              
                ((MemberCase.Status IN (1, 2)) OR (@showClosed = 1)) -- ACTIVE/UNDERDEVELOPMENT, OR SHOW ALL
    
								AND (
								
									 ((WorkTeamMembership.SecurityAuthorityId = @securityAuthorityId)
		
										AND (WorkTeamMembership.UserAccountId = @userAccountId))

									OR (AssignedToWorkTeamId IS NULL)

									)



            ) AS MemberCasePage

          WHERE MemberCasePage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    