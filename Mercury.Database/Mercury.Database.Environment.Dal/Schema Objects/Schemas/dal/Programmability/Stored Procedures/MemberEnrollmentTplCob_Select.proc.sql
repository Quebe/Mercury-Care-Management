/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollmentTplCob_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollmentTplCob_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollmentTplCob_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberEnrollmentTplCobId BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT * FROM dbo.MemberEnrollmentTplCob WHERE MemberEnrollmentTplCobId = @memberEnrollmentTplCobId

    END        