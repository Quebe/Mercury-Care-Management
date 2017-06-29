/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollmentCobTpls_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollmentTplCobs_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollmentTplCobs_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT * FROM dbo.MemberEnrollmentTplCob WHERE MemberId = @memberId

    END