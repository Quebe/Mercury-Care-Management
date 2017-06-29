/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'CurrentAgeInYears' AND type = 'FN'))
  DROP FUNCTION dbo.[CurrentAgeInYears]
GO          
*/

CREATE FUNCTION [dbo].[CurrentAgeInYears] (@birthDate DATETIME) RETURNS INT AS
  
  BEGIN
  

    DECLARE @currentAge AS INTEGER

    DECLARE @birthDay AS DATETIME


    SET @currentAge = (YEAR (GETDATE ()) - YEAR (@birthDate))

    SET @birthDay = CAST (

      LTRIM (MONTH (@birthDate)) + '/' + 
      
      LTRIM (CASE WHEN ((MONTH (@birthDate) = 2) AND (DAY (@birthDate) = 29)) THEN 28 ELSE DAY (@birthDate) END) + '/' +
      
      LTRIM (YEAR (GETDATE ()))
      
      AS DATETIME)
     
    SET @currentAge = @currentAge - (CASE WHEN (GETDATE () < @birthDay) THEN 1 ELSE 0 END)

    RETURN @currentAge

  END