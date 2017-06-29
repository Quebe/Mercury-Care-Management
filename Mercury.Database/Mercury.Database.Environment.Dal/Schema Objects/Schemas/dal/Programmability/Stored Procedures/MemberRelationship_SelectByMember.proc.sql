/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberRelationship_SelectByMember' AND type = 'P'))
  DROP PROCEDURE dal.MemberRelationship_SelectByMember
GO      
*/

CREATE PROCEDURE dal.MemberRelationship_SelectByMember
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

		SELECT * FROM dbo.MemberRelationship WHERE MemberId = @memberId
         
    END
