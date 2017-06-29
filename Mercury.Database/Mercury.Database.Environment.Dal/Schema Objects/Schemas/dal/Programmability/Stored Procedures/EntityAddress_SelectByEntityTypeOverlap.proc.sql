/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityAddress_SelectByEntityTypeOverlap' AND type = 'P'))
  DROP PROCEDURE dal.EntityAddress_SelectByEntityTypeOverlap
GO      
*/

CREATE PROCEDURE dal.EntityAddress_SelectByEntityTypeOverlap
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId                 BIGINT,
      @addressType                 INT,

      @effectiveDate          DATETIME,
      @terminationDate        DATETIME,
      
      @excludeEntityAddressId   BIGINT

      
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
        
          FROM dbo.EntityAddress
          
          WHERE 
          
						EntityId = @entityId
          
            AND (AddressType = @addressType)
            
            AND (EffectiveDate <= @terminationDate) AND (TerminationDate >= @effectiveDate)
            
            AND (EntityAddressId <> @excludeEntityAddressId)
        
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityAddress_Insert TO PUBLIC
GO          
*/