--DROP FUNCTION dal.ConvertIdFromSourcePattern

/*

SELECT DISTINCT REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (provid, 
    '9', '#'), '8', '#'), '7', '#'), '6', '#'), '5', '#'), '4', '#'), '3', '#'), '2', '#'), '1', '#'), '0', '#')
  FROM provider

*/

CREATE FUNCTION [dal].[ConvertIdFromSourcePattern] (@externalId VARCHAR (020), @knownPatterns VARCHAR (8000)) RETURNS BIGINT AS

  BEGIN
    DECLARE @alphaPattern AS VARCHAR (020)

    DECLARE @numericPattern AS VARCHAR (020)

    DECLARE @patternIndex AS INT

    DECLARE @alphaChar AS VARCHAR (01)

    SET @alphaPattern = 
      REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (REPLACE (RTRIM (@externalId), 
      '9', '#'), '8', '#'), '7', '#'), '6', '#'), '5', '#'), '4', '#'), '3', '#'), '2', '#'), '1', '#'), '0', '#')

    SET @numericPattern = RTRIM (@externalId)

    WHILE (PATINDEX ('%[A-Z]%', @numericPattern) > 0)
  
      BEGIN

        SET @patternIndex = PATINDEX ('%[A-Z]%', @numericPattern)

        SET @alphaChar = SUBSTRING (@numericPattern, @patternIndex, 1)

        SET @numericPattern = REPLACE (@numericPattern, @alphaChar, '')

      END

    DECLARE @offsetId AS BIGINT

    DECLARE @convertedId AS BIGINT
    
    DECLARE @numberPadding AS BIGINT
    
    SET @numberPadding = 1000000000000    --  12 DIGITS FOR NUMBERS
    
    SET @offsetId = (PATINDEX ('%|' + @alphaPattern + '|%', @knownPatterns) + 1) * @numberPadding * 100 -- 100 FOR LENGTH ENCODING
  
    SET @offsetId = @offsetId + (LEN (@numericPattern) * @numberPadding)

    SET @convertedId = @offsetId + (CAST (@numericPattern AS BIGINT))

    RETURN @convertedId
  
  END
  
 