CREATE VIEW [dal].[MemberEntity]
	AS 
	
		SELECT 
		
				Member.*, 

				Entity.EntityType,

				Entity.EntityName,

				Entity.NameLast,

				Entity.NameFirst,

				Entity.NameMiddle,

				Entity.NamePrefix,

				Entity.NameSuffix,

				Entity.FederalTaxId,

				Entity.IdCodeQualifier,

				Entity.UniqueId
				
																	
			FROM 
			
				dbo.Member 
				
					JOIN dbo.Entity 
					
						ON Member.EntityId = Entity.EntityId

	
	
      