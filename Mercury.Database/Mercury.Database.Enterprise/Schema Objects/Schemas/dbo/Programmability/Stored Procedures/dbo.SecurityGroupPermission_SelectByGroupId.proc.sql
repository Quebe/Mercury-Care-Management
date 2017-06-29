/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SecurityGroupPermission_SelectByGroupId' AND type = 'P'))
  DROP PROCEDURE dbo.SecurityGroupPermission_SelectByGroupId
GO      
*/

CREATE PROCEDURE dbo.SecurityGroupPermission_SelectByGroupId
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
        
            SecurityGroupPermission.SecurityAuthorityId AS SecurityAuthorityId,
            SecurityAuthority.SecurityAuthorityName     AS SecurityAuthorityName,
            SecurityGroupPermission.SecurityGroupId     AS SecurityGroupId,
            SecurityGroupPermission.PermissionId        AS PermissionId,
            Permission.PermissionName                   AS PermissionName,
            SecurityGroupPermission.IsGranted           AS IsGranted,
            SecurityGroupPermission.IsDenied            AS IsDenied,
            
            SecurityGroupPermission.CreateAuthorityName  AS CreateAuthorityName,
            SecurityGroupPermission.CreateAccountId      AS CreateAccountId,
            SecurityGroupPermission.CreateAccountName    AS CreateAccountName,
            SecurityGroupPermission.CreateDate           AS CreateDate,            
            
            SecurityGroupPermission.ModifiedAuthorityName  AS ModifiedAuthorityName,
            SecurityGroupPermission.ModifiedAccountId      AS ModifiedAccountId,
            SecurityGroupPermission.ModifiedAccountName    AS ModifiedAccountName,
            SecurityGroupPermission.ModifiedDate           AS ModifiedDate            
            
          FROM
            SecurityGroupPermission
              JOIN Permission ON SecurityGroupPermission.PermissionId = Permission.PermissionId
              JOIN SecurityAuthority ON SecurityGroupPermission.SecurityAuthorityId = SecurityAuthority.SecurityAuthorityId
              
          WHERE 
            SecurityAuthority.SecurityAuthorityName = @securityAuthorityName
              AND SecurityGroupPermission.SecurityGroupId = @securityGroupId
              
          ORDER BY SecurityGroupPermission.SecurityGroupId
                        

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.SecurityGroupPermission_SelectByGroupId TO PUBLIC
GO          
*/