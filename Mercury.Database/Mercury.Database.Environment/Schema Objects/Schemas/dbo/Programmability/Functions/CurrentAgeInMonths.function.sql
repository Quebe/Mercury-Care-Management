/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CurrentAgeInMonths' AND type = 'FN'))
  DROP FUNCTION dbo.[CurrentAgeInMonths]
GO          
*/

CREATE FUNCTION [dbo].[CurrentAgeInMonths] (@birthDate DATETIME) RETURNS INT AS
  
  BEGIN
  
    DECLARE @currentAge AS INTEGER

    SET @currentAge = DATEDIFF (MONTH, @birthDate, GETDATE ())

    SET @currentAge = @currentAge - (CASE WHEN ((MONTH (GETDATE ()) = MONTH (@birthDate)) AND (DAY (GETDATE ()) < DAY (@birthDate))) THEN 1 ELSE 0 END)

    RETURN @currentAge

  END