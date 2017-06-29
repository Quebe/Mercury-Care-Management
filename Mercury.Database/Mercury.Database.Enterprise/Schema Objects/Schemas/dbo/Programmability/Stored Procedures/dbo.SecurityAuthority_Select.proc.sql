/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SecurityAuthority_Select' AND type = 'P'))
  DROP PROCEDURE dbo.SecurityAuthority_Select
GO      
*/

CREATE PROCEDURE dbo.SecurityAuthority_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @securityAuthorityId            BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT * FROM SecurityAuthority WHERE SecurityAuthorityId = @securityAuthorityId

      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.SecurityAuthority_Select TO PUBLIC
GO          
*/