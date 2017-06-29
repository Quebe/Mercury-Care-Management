/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollmentPcp_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollmentPcp_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollmentPcp_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberEnrollmentPcpId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

		SELECT * FROM dbo.MemberEnrollmentPcp WHERE MemberEnrollmentPcpId = @memberEnrollmentPcpId

    END