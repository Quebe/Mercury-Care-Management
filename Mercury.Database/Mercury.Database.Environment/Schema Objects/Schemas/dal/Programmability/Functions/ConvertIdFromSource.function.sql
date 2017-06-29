--DROP FUNCTION dal.ConvertIdFromSource

/*

SELECT DISTINCT SUBSTRING (memid, 1, (PATINDEX ('%[0-9]%', memid) - 1)) + '|' FROM member WHERE PATINDEX ('%[0-9]%', memid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (provid, 1, (PATINDEX ('%[0-9]%', provid) - 1)) + '|' FROM provider WHERE PATINDEX ('%[0-9]%', provid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (eligibleorgid, 1, (PATINDEX ('%[0-9]%', eligibleorgid) - 1)) + '|' FROM eligibilityorg WHERE PATINDEX ('%[0-9]%', eligibleorgid ) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (carrierid, 1, (PATINDEX ('%[0-9]%', carrierid) - 1)) + '|' FROM carrier WHERE PATINDEX ('%[0-9]%', carrierid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (programid, 1, (PATINDEX ('%[0-9]%', programid) - 1)) + '|' FROM program WHERE PATINDEX ('%[0-9]%', programid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (enrollid, 1, (PATINDEX ('%[0-9]%', enrollid) - 1)) + '|' FROM enrollkeys WHERE PATINDEX ('%[0-9]%', enrollid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (ethnicid, 1, (PATINDEX ('%[0-9]%', ethnicid) - 1)) + '|' FROM ethnicity WHERE PATINDEX ('%[0-9]%', ethnicid) > 0 ORDER BY 1

SELECT DISTINCT SUBSTRING (languageid, 1, (PATINDEX ('%[0-9]%', languageid) - 1)) + '|' FROM language WHERE PATINDEX ('%[0-9]%', languageid) > 0 ORDER BY 1

*/

CREATE FUNCTION [dal].[ConvertIdFromSource] (@externalId VARCHAR (020), @knownPrefixes VARCHAR (8000)) RETURNS BIGINT AS

  BEGIN
  
    DECLARE @alphaPrefix   AS VARCHAR (020)

    DECLARE @numericSuffix AS VARCHAR (020)

    DECLARE @numericPosition AS INT

    DECLARE @offsetId AS BIGINT

    DECLARE @convertedId AS BIGINT
    
    DECLARE @numberPadding AS BIGINT
    
    SET @numberPadding = 1000000000000    --  12 DIGITS FOR NUMBERS


    SET @alphaPrefix = ''    

    SET @numericSuffix = REVERSE (RTRIM (@externalId))

    SET @numericPosition = 
      CASE 
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 20)) = 1 THEN 20
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 19)) = 1 THEN 19
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 18)) = 1 THEN 18
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 17)) = 1 THEN 17
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 16)) = 1 THEN 16
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 15)) = 1 THEN 15
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 14)) = 1 THEN 14
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 13)) = 1 THEN 13
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 12)) = 1 THEN 12
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 11)) = 1 THEN 11
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 10)) = 1 THEN 10
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 09)) = 1 THEN 9
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 08)) = 1 THEN 8
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 07)) = 1 THEN 7
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 06)) = 1 THEN 6
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 05)) = 1 THEN 5
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 04)) = 1 THEN 4
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 03)) = 1 THEN 3
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 02)) = 1 THEN 2
          WHEN ISNUMERIC (SUBSTRING (@numericSuffix, 01, 01)) = 1 THEN 1
          ELSE 0
        END

    IF (@numericPosition > 0)  

      BEGIN

        SET @alphaPrefix = SUBSTRING (@numericSuffix, @numericPosition + 1, LEN (@numericSuffix))

        SET @alphaPrefix = REVERSE (@alphaPrefix)

        SET @numericSuffix = SUBSTRING (@numericSuffix, 1, @numericPosition)

        SET @numericSuffix = REVERSE (@numericSuffix);

      END

    IF (ISNUMERIC (@numericSuffix) = 0) SET @numericSuffix = '0'
    
    SET @offsetId = (PATINDEX ('%|' + @alphaPrefix + '|%', @knownPrefixes) + 1) * @numberPadding * 100 -- 100 FOR LENGTH ENCODING

    SET @offsetId = @offsetId + (LEN (@externalId) * @numberPadding)

    SET @convertedId = @offsetId + (CAST (@numericSuffix AS BIGINT))

    RETURN @convertedId
  
  END
  
 