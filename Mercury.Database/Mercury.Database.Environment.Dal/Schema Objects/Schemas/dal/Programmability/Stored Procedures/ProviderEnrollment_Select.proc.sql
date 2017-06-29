/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProviderEnrollment_Select' AND type = 'P'))
  DROP PROCEDURE dal.ProviderEnrollment_Select
GO      
*/

CREATE PROCEDURE dal.ProviderEnrollment_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @providerEnrollmentId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */


        SELECT * FROM dbo.ProviderEnrollment

        WHERE (ProviderEnrollment.ProviderEnrollmentId = @providerEnrollmentId)
        
        ORDER BY TerminationDate DESC, EffectiveDate DESC, ProgramId

    END