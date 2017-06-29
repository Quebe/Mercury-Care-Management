-- DROP FUNCTION dal.ConvertIdToSourcePatternComplex

CREATE FUNCTION [dal].[ConvertIdToSourcePatternComplex] (@id BIGINT, @knownPatterns VARCHAR (8000)) RETURNS VARCHAR (020) AS 

  BEGIN

    DECLARE @alphaPattern AS VARCHAR (020)

    DECLARE @numericPattern AS VARCHAR (020)

    DECLARE @numericChar AS VARCHAR (020)

    DECLARE @currentIndex AS INT

    DECLARE @patternOffset AS INT

    DECLARE @numericOffset AS INT

    DECLARE @convertedId AS VARCHAR (020)

  
    DECLARE @numberPadding AS BIGINT
    
    SET @numberPadding = 1000000000000000    --  15 DIGITS FOR NUMBERS


    -- REPLACED CHAR INDEX VALUE WITH PATTERN MATCH COUNT

    --SET @patternOffset = CAST ((@id / (@numberPadding * 100)) AS INT)

    --SET @alphaPattern = CAST (SUBSTRING (@knownPatterns, @patternOffset, LEN (@knownPatterns)) AS VARCHAR (020))                                     

    --SET @alphaPattern = SUBSTRING (@alphaPattern, 1, PATINDEX ('%|%', @alphaPattern))

    --SET @alphaPattern = REPLACE (@alphaPattern, '|', '')


    SET @patternOffset = CAST ((@id / (@numberPadding * 100)) AS INT)
    
    SET @patternOffset = @patternOffset - 1 
    
    DECLARE @alphaPatternSearch AS VARCHAR (8000)
    
    SET @alphaPatternSearch = @knownPatterns
    
    WHILE (@patternOffset != 0) 
    
      BEGIN 
      
        SET @alphaPatternSearch = SUBSTRING (@alphaPatternSearch, PATINDEX ('%|%', @alphaPatternSearch) + 1, 8000)
        
        SET @patternOffset = @patternOffset - 1
      
      END
      
    SET @alphaPattern = SUBSTRING (@alphaPatternSearch, 1, PATINDEX ('%|%', @alphaPatternSearch) - 1)
      
  
    SET @convertedId = REVERSE (@alphaPattern)

    SET @numericPattern = REVERSE (CAST (@id AS VARCHAR (020)))


    SET @numericOffset = 1

    SET @currentIndex = 1

    
    WHILE (@currentIndex <= LEN (@convertedId)) 
 
      BEGIN

        IF (SUBSTRING (@convertedId, @currentIndex, 1) = '#')
  
          BEGIN

            SET @numericChar = SUBSTRING (@numericPattern, @numericOffset, 1)

            SET @numericOffset = @numericOffset + 1

            IF (@currentIndex > 1) 

              BEGIN

                SET @convertedId = SUBSTRING (@convertedId, 1, @currentIndex - 1) + @numericChar + SUBSTRING (@convertedId, @currentIndex + 1, LEN (@convertedId))

              END

            ELSE 

              BEGIN

                SET @convertedId = @numericChar + SUBSTRING (@convertedId, 2, LEN (@convertedId))

              END

          END

        SET @currentIndex = @currentIndex + 1

      END 

    SET @convertedId = REVERSE (@convertedId)

    RETURN @convertedId
    
  END