-- DROP FUNCTION dal.ConvertIdToSource

CREATE FUNCTION [dal].[ConvertIdToSource] (@id BIGINT, @knownPrefixes VARCHAR (8000)) RETURNS VARCHAR (020) AS 

  BEGIN
  
    DECLARE @alphaPrefix AS VARCHAR (020)

    DECLARE @prefixOffset AS INT

    DECLARE @numericLength AS INT

    DECLARE @convertedId AS VARCHAR (020)

    DECLARE @numberPadding AS BIGINT
    
    SET @numberPadding = 1000000000000    --  12 DIGITS FOR NUMBERS
    
    DECLARE @zeroPadding AS VARCHAR (020)
    
    DECLARE @numberPaddingLength AS INT
    
    SET @numberPaddingLength = (LEN (@numberPadding) - 1)
    
    SET @zeroPadding = RIGHT (@numberPadding, @numberPaddingLength)


    SET @prefixOffset = CAST ((@id / (@numberPadding * 100)) AS INT)
                                     
    SET @alphaPrefix = CAST (SUBSTRING (@knownPrefixes, @prefixOffset, LEN (@knownPrefixes)) AS VARCHAR (020))

    SET @alphaPrefix = SUBSTRING (@alphaPrefix, 1, PATINDEX ('%[A-Z]|%', @alphaPrefix))

    SET @alphaPrefix = REPLACE (@alphaPrefix, '|', '')


    SET @numericLength = RIGHT (CAST ((@id / @numberPadding) AS INT), 2)

    SET @numericLength = @numericLength - LEN (@alphaPrefix)


    SET @convertedId = @alphaPrefix + RIGHT (@zeroPadding + LTRIM (RIGHT (@id, @numberPaddingLength)), @numericLength)

    
    RETURN @convertedId
    
  END