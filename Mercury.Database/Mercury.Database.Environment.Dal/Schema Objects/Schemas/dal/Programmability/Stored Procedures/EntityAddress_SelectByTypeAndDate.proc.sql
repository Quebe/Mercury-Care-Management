/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityAddress_SelectByTypeAndDate' AND type = 'P'))
  DROP PROCEDURE dal.EntityAddress_SelectByTypeAndDate
GO      
*/

CREATE PROCEDURE dal.EntityAddress_SelectByTypeAndDate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId      BIGINT,
      @addressType      INT,
      @addressDate DATETIME
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT EntityAddress.* 
      
        FROM dbo.EntityAddress AS EntityAddress
       
        WHERE EntityId = @entityId AND AddressType = @addressType 
               
			AND @addressDate BETWEEN EffectiveDate AND TerminationDate
            
    END        