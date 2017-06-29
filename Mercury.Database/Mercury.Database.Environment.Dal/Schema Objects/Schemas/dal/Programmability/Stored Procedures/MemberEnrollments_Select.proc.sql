/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberEnrollments_Select' AND type = 'P'))
  DROP PROCEDURE dal.MemberEnrollments_Select
GO      
*/

CREATE PROCEDURE dal.MemberEnrollments_Select
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

        SELECT * FROM dbo.MemberEnrollment WHERE MemberId = @memberId

    END