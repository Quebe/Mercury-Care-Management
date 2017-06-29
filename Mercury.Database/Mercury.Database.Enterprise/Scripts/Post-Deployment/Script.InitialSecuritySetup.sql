-- =============================================
-- Script Template
-- =============================================


DECLARE @securityAuthorityId AS BIGINT

INSERT INTO SecurityAuthority (

		SecurityAuthorityName, SecurityAuthorityDescription, SecurityAuthorityType, 
		
		Protocol, ServerName, Domain)
		
  VALUES (
  
				SUBSTRING (SUSER_SNAME (), 0, CHARINDEX ('\', SUSER_SNAME (), 0)), 
				
				SUBSTRING (SUSER_SNAME (), 0, CHARINDEX ('\', SUSER_SNAME (), 0)), 
				
				1, 
				
				'',
				
				SUBSTRING (SUSER_SNAME (), 0, CHARINDEX ('\', SUSER_SNAME (), 0)), 
				
				SUBSTRING (SUSER_SNAME (), 0, CHARINDEX ('\', SUSER_SNAME (), 0))
				
				)

SET @securityAuthorityId = @@IDENTITY

INSERT INTO SecurityGroupPermission (SecurityAuthorityId, SecurityGroupId, PermissionId, 

		IsGranted, IsDenied) 
		
	VALUES (
	
			@securityAuthorityId, 
			
			'S-1-1-0', -- EVERYONE
			
			(SELECT MIN (PermissionId) FROM Permission), -- ENTERPRISE ADMINISTRATOR
			
			1, 0 -- GRANTED
			
			)
	