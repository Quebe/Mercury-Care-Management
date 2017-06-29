/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SearchProviderByName' AND type = 'P'))
  DROP PROCEDURE dal.SearchProviderByName
GO      
*/


CREATE PROCEDURE [dal].SearchProviderByName

    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @providerName VARCHAR (060),
      @returnResults BIT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        BEGIN TRY 
        
            DROP TABLE #ProviderSearch
            
        END TRY
        
        BEGIN CATCH
        
            -- DO NOTHING
        
        END CATCH
        
        SET @providerName = REPLACE (@providerName, ',', ', ')
        
        SET @providerName = REPLACE (@providerName, '  ', ' ')
        
        SET @providerName = @providerName + '%'  
        
        SELECT ProviderId
        
          INTO #ProviderSearch
      
          FROM  dbo.Provider JOIN dbo.Entity ON Provider.EntityId = Entity.EntityId
            
          WHERE Entity.EntityName LIKE @providerName

      
        IF (@returnResults != 0) 
        
          BEGIN
          
            SELECT 
            
                Provider.ProviderId,
                
                Provider.EntityId, 
                
                Entity.EntityName, 
                
                Entity.FederalTaxId,
                
                Provider.NationalProviderId,
                
                CAST (ISNULL (
                  
                  (SELECT TOP 1 Specialty.SpecialtyName 
                  
                      FROM dbo.ProviderSpecialty AS ProviderSpecialty 
                      
												JOIN dbo.Specialty AS Specialty ON ProviderSpecialty.SpecialtyId = Specialty.SpecialtyId
                      
                      WHERE ProviderSpecialty.ProviderId = ProviderSearch.ProviderId
                      
                        AND GETDATE () BETWEEN ProviderSpecialty.EffectiveDate AND ProviderSpecialty.TerminationDate
                      
                        AND SpecialtyType = 1) 
                        
                  , '') AS VARCHAR (060)) AS PrimarySpecialtyName
                
              FROM 
              
                #ProviderSearch AS ProviderSearch
                
                  JOIN dbo.Provider AS Provider ON ProviderSearch.ProviderId = Provider.ProviderId
                    
                  JOIN dbo.Entity AS Entity  ON Provider.EntityId = Entity.EntityId
                    
              ORDER BY Entity.EntityName
           
          END
          
        ELSE
        
          BEGIN
          
            SELECT COUNT (1) AS CountOf FROM #ProviderSearch
  
	        END
        
              
    END     