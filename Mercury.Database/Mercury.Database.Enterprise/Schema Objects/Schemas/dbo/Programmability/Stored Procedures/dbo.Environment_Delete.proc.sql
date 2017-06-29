/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Environment_Delete' AND type = 'P'))
  DROP PROCEDURE dbo.Environment_Delete
GO      
*/

CREATE PROCEDURE dbo.Environment_Delete
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @environmentId            BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM Environment WHERE EnvironmentId = @environmentId)
          BEGIN
            -- EXISTING RECORD, DELETE

            DELETE FROM EnvironmentAccess WHERE EnvironmentId = @environmentId     
            
            DELETE FROM Environment WHERE EnvironmentId = @environmentId     
          
          END
          
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Environment_Delete TO PUBLIC
GO          
*/