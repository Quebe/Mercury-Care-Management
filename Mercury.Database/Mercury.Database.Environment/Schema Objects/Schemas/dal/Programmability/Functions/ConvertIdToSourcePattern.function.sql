-- DROP FUNCTION dal.ConvertIdToSourcePattern

CREATE FUNCTION [dal].[ConvertIdToSourcePattern] (@id BIGINT, @knownPatterns VARCHAR (8000)) RETURNS VARCHAR (020) AS 

  BEGIN

    DECLARE @alphaPattern AS VARCHAR (020)

    DECLARE @numericPattern AS VARCHAR (020)

    DECLARE @numericValue AS VARCHAR (020)

    DECLARE @prefixOffset AS INT

    DECLARE @numericLength AS INT

    DECLARE @convertedId AS VARCHAR (020)

    DECLARE @numberPadding AS BIGINT
    
    SET @numberPadding = 1000000000000    --  12 DIGITS FOR NUMBERS
  

    SET @prefixOffset = CAST ((@id / (@numberPadding * 100)) AS INT)

    SET @alphaPattern = CAST (SUBSTRING (@knownPatterns, @prefixOffset, LEN (@knownPatterns)) AS VARCHAR (020))                                     

    SET @alphaPattern = SUBSTRING (@alphaPattern, 1, PATINDEX ('%|%', @alphaPattern))

    SET @alphaPattern = REPLACE (@alphaPattern, '|', '')


    SET @numericLength = RIGHT (CAST ((@id / @numberPadding) AS INT), 2)

    SET @numericPattern = RIGHT ('##############', @numericLength)

    SET @numericValue = RIGHT (@id, @numericLength)


    SET @convertedId = REPLACE (@alphaPattern, @numericPattern, @numericValue)
  
    
    RETURN @convertedId
    
  END