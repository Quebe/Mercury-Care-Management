/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'AgeInMonthsOnDate' AND type = 'FN'))
  DROP FUNCTION dbo.[AgeInMonthsOnDate]
GO          
*/

CREATE FUNCTION [dbo].[AgeInMonthsOnDate] (@birthDate DATETIME, @forDate DATETIME) RETURNS INT AS
  
  BEGIN
  
    DECLARE @currentAge AS INTEGER

    SET @currentAge = DATEDIFF (MONTH, @birthDate, @forDate)

    SET @currentAge = @currentAge - (CASE WHEN ((MONTH (@forDate) = MONTH (@birthDate)) AND (DAY (@forDate) < DAY (@birthDate))) THEN 1 ELSE 0 END)

    RETURN @currentAge

  END