/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EnvironmentAccess_SelectByGroupId' AND type = 'P'))
  DROP PROCEDURE dbo.EnvironmentAccess_SelectByGroupId
GO      
*/

CREATE PROCEDURE dbo.EnvironmentAccess_SelectByGroupId
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityName VARCHAR (060),
      @securityGroupId       VARCHAR (060)
    
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
        
      		EnvironmentAccess.EnvironmentId         AS EnvironmentId,        
            EnvironmentAccess.SecurityAuthorityId   AS SecurityAuthorityId,
            SecurityAuthority.SecurityAuthorityName AS SecurityAuthorityName,
            EnvironmentAccess.SecurityGroupId       AS SecurityGroupId,
            EnvironmentAccess.SecurityGroupName     AS SecurityGroupName,
            
            EnvironmentAccess.IsGranted             AS IsGranted,
            EnvironmentAccess.IsDenied              AS IsDenied,
            
            EnvironmentAccess.CreateAuthorityName  AS CreateAuthorityName,
            EnvironmentAccess.CreateAccountId      AS CreateAccountId,
            EnvironmentAccess.CreateAccountName    AS CreateAccountName,
            EnvironmentAccess.CreateDate           AS CreateDate,            
            
            EnvironmentAccess.ModifiedAuthorityName  AS ModifiedAuthorityName,
            EnvironmentAccess.ModifiedAccountId      AS ModifiedAccountId,
            EnvironmentAccess.ModifiedAccountName    AS ModifiedAccountName,
            EnvironmentAccess.ModifiedDate           AS ModifiedDate            

          FROM
            EnvironmentAccess
              JOIN SecurityAuthority ON EnvironmentAccess.SecurityAuthorityId = SecurityAuthority.SecurityAuthorityId
              
          WHERE 
            SecurityAuthority.SecurityAuthorityName = @securityAuthorityName
              AND EnvironmentAccess.SecurityGroupId = @securityGroupId
              
          ORDER BY EnvironmentAccess.SecurityGroupId
                        

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.EnvironmentAccess_SelectByGroupId TO PUBLIC
GO          
*/