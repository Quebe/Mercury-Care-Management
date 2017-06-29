/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'ProviderEnrollments_Select' AND type = 'P'))
  DROP PROCEDURE dal.ProviderEnrollments_Select
GO      
*/

CREATE PROCEDURE dal.ProviderEnrollments_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @providerId BIGINT
    
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

        WHERE (ProviderEnrollment.ProviderId = @providerId)

        ORDER BY TerminationDate DESC, EffectiveDate DESC, ProgramId

    END