/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'SecurityAuthority_Delete' AND type = 'P'))
  DROP PROCEDURE dbo.SecurityAuthority_Delete
GO      
*/

CREATE PROCEDURE dbo.SecurityAuthority_Delete
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

        IF EXISTS (SELECT * FROM SecurityAuthority WHERE SecurityAuthorityId = @securityAuthorityId)
          BEGIN
            -- EXISTING RECORD, DELETE

            DELETE FROM SecurityGroupPermission WHERE SecurityAuthorityId = @securityAuthorityId

            DELETE FROM EnvironmentAccess WHERE SecurityAuthorityId = @securityAuthorityId     
            
            DELETE FROM SecurityAuthority WHERE SecurityAuthorityId = @securityAuthorityId     
          
          END
          
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.SecurityAuthority_Delete TO PUBLIC
GO          
*/