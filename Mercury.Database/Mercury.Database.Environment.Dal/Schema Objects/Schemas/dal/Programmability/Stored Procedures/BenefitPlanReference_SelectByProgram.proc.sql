/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'BenefitPlanReference_SelectByProgram' AND type = 'P'))
  DROP PROCEDURE dal.BenefitPlanReference_SelectByProgram
GO      
*/

CREATE PROCEDURE dal.BenefitPlanReference_SelectByProgram
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @programId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        SELECT * FROM dbo.BenefitPlan WHERE ProgramId = @programId
       
    END        