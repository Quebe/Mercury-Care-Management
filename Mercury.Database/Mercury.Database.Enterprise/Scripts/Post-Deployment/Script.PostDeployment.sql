/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- SEED ENVIRONMENT TYPE (BEGIN) 

PRINT 'SEED ENVIRONMENT TYPE (BEGIN)'

IF NOT EXISTS (SELECT * FROM EnvironmentType WHERE EnvironmentTypeId = 0) 
  
  BEGIN 
  
    SET IDENTITY_INSERT EnvironmentType ON
    
    INSERT INTO EnvironmentType (EnvironmentTypeId, EnvironmentTypeName, EnvironmentTypeDescription) VALUES (0, 'Not Specified', 'Not Specified')
    
    SET IDENTITY_INSERT EnvironmentType OFF
    
  END
  
PRINT 'SEED ENVIRONMENT TYPE ( END )'  
  
-- SEED ENVIRONMENT TYPE ( END ) 


-- SEED SECURITY AUTHORITY TYPE (BEGIN) 

PRINT '-- SEED SECURITY AUTHORITY TYPE (BEGIN) '

  IF NOT EXISTS (SELECT * FROM enum.SecurityAuthorityType WHERE SecurityAuthorityType = 0) INSERT INTO enum.SecurityAuthorityType (SecurityAuthorityType, SecurityAuthorityTypeName) VALUES (0, 'Custom (Token)')
    
	IF NOT EXISTS (SELECT * FROM enum.SecurityAuthorityType WHERE SecurityAuthorityType = 1) INSERT INTO enum.SecurityAuthorityType (SecurityAuthorityType, SecurityAuthorityTypeName) VALUES (1, 'Windows Integrated')
        
	IF NOT EXISTS (SELECT * FROM enum.SecurityAuthorityType WHERE SecurityAuthorityType = 2) INSERT INTO enum.SecurityAuthorityType (SecurityAuthorityType, SecurityAuthorityTypeName) VALUES (2, 'Active Directory')

PRINT '-- SEED SECURITY AUTHORITY TYPE ( END ) '

-- SEED SECURITY AUTHORITY TYPE ( END ) 


-- ENTERPRISE PERMISSIONS (BEGIN) 

PRINT 'ENTERPRISE PERMISSIONS (BEGIN)'

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = '.EnterpriseAdministrator')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('.EnterpriseAdministrator', 'Enterprise Administrator');

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.Review', 'Allows for access to the Enterprise Management portion of the Enterprise Management Console. This is for UI access only, does not control specific rights or permissions to actions.')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'SecurityManagement.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('SecurityManagement.Review', 'Allows for access to the directory service node that contains the user for management of accounts and that node. This is for UI access only, does not control specific rights or permissions to actions.')


IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.SecurityAuthority.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.SecurityAuthority.Review', '')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.SecurityAuthority.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.SecurityAuthority.Manage', '')



IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.EnterprisePermission.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.EnterprisePermission.Review', '')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.EnterprisePermission.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.EnterprisePermission.Manage', '')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.EnterprisePermission.Assignment.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.EnterprisePermission.Assignment.Manage', '')



IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.Environment.Review')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.Environment.Review', '')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.Environment.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.Environment.Manage', '')

IF NOT EXISTS (SELECT * FROM Permission WHERE PermissionName = 'EnterpriseManagement.Environment.Access.Manage')

  INSERT INTO Permission (PermissionName, PermissionDescription) VALUES ('EnterpriseManagement.Environment.Access.Manage', '')


PRINT 'ENTERPRISE PERMISSIONS ( END ) '

-- ENTERPRISE PERMISSIONS ( END ) 