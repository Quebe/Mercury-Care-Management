/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'BenefitPlan_Select' AND type = 'P'))
  DROP PROCEDURE dal.BenefitPlan_Select
GO      
*/

CREATE PROCEDURE dal.BenefitPlan_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @benefitPlanId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.BenefitPlan WHERE BenefitPlanId = @benefitPlanId
      
    END        