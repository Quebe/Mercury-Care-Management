/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'AuthorizationLine_SelectByAuthorizationId' AND type = 'P'))
  DROP PROCEDURE dal.AuthorizationLine_SelectByAuthorizationId
GO      
*/

CREATE PROCEDURE dal.AuthorizationLine_SelectByAuthorizationId
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @authorizationId             BIGINT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT *
           
	        FROM 
        
						dbo.AuthorizationLine 
          
					WHERE [AuthorizationLine].AuthorizationId = @authorizationId
        
					ORDER BY LineNumber
        
	    END    
              
