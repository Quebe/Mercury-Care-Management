CREATE VIEW [dal].[Insurer] AS 

	SELECT 
	
			Insurer.*, 
			
			Entity.EntityName AS InsurerName -- OBJECT LOOKUP SUPPORT (AS REFERENCE TABLE)
			
		FROM 
		
			dbo.Insurer
			
				JOIN dbo.Entity ON Insurer.EntityId = Entity.EntityId