/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberService_RemoveManual' AND type = 'P'))
  DROP PROCEDURE dbo.MemberService_RemoveManual
GO      
*/

CREATE PROCEDURE dbo.MemberService_RemoveManual
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberServiceId         BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.MemberService WHERE (MemberServiceId = @memberServiceId) AND AddedManually = 1)
        
          BEGIN
          
            DELETE FROM MemberService WHERE MemberServiceId = @memberServiceId AND AddedManually = 1
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberService_RemoveManual TO PUBLIC
GO          
*/