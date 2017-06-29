/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'AgeInYearsOnDate' AND type = 'FN'))
  DROP FUNCTION dbo.[AgeInYearsOnDate]
GO          
*/

CREATE FUNCTION [dbo].[AgeInYearsOnDate] (@birthDate DATETIME, @forDate DATETIME) RETURNS INT AS
  
  BEGIN
  

    DECLARE @currentAge AS INTEGER

    DECLARE @birthDay AS DATETIME


    SET @currentAge = (YEAR (@forDate) - YEAR (@birthDate))

    SET @birthDay = CAST (

      LTRIM (MONTH (@birthDate)) + '/' + 
      
      LTRIM (CASE WHEN ((MONTH (@birthDate) = 2) AND (DAY (@birthDate) = 29)) THEN 28 ELSE DAY (@birthDate) END) + '/' +
      
      LTRIM (YEAR (@forDate))
      
      AS DATETIME)
     
    SET @currentAge = @currentAge - (CASE WHEN (@forDate < @birthDay) THEN 1 ELSE 0 END)

    RETURN @currentAge

  END