/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollmentCoverage_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollmentCoverage_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollmentCoverage_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberEnrollmentCoverageId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

		SELECT * FROM dbo.MemberEnrollmentCoverage WHERE MemberEnrollmentCoverageId = @memberEnrollmentCoverageId

    END