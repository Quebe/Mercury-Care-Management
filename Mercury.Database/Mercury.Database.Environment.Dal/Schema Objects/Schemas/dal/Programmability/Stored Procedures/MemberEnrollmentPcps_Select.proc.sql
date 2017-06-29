/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollmentPcps_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollmentPcps_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollmentPcps_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberEnrollmentId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

		SELECT * FROM dbo.MemberEnrollmentPcp WHERE MemberEnrollmentId = @memberEnrollmentId

    END