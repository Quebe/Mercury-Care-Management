/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Role_Delete' AND type = 'P'))
  DROP PROCEDURE dbo.Role_Delete
GO      
*/

CREATE PROCEDURE dbo.Role_Delete
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @roleId            BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM Role WHERE RoleId = @roleId)
          BEGIN
            -- EXISTING RECORD, DELETE

            DELETE FROM RoleMembership WHERE RoleId = @roleId

            DELETE FROM RolePermission WHERE RoleId = @roleId     
            
            DELETE FROM Role WHERE RoleId = @roleId     
          
          END
          
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Role_Delete TO PUBLIC
GO          
*/