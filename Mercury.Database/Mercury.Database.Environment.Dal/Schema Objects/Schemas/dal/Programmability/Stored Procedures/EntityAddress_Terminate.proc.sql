
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityAddress_Terminate' AND type = 'P'))
  DROP PROCEDURE dal.EntityAddress_Terminate
GO      
*/

CREATE PROCEDURE dal.EntityAddress_Terminate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityAddressId          BIGINT,
      @terminationDate        DATETIME,

      @modifiedAuthorityName  VARCHAR (060),
      @modifiedAccountId      VARCHAR (060),
      @modifiedAccountName    VARCHAR (060)
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */
        
        IF EXISTS (SELECT * FROM dbo.EntityAddress WHERE EntityAddressId = @entityAddressId AND @entityAddressId <> 0) 
        
          BEGIN -- UPDATE EXISTING INTERNAL (MERCURY) ADDRESS 
          
            UPDATE dbo.EntityAddress 
            
              SET 
                                            
                TerminationDate = @terminationDate,
                               
                ModifiedAuthorityName = @modifiedAuthorityName, 
                
                ModifiedAccountId = @modifiedAccountId, 
                
                ModifiedAccountName = @modifiedAccountName, 
                
                ModifiedDate = GETDATE ()
                
              WHERE EntityAddressId = @entityAddressId AND (@terminationDate >= EffectiveDate)
                
          END
          
          -- ELSE UPDATE EXTERNAL ADDRESS 
                     
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityAddress_Insert TO PUBLIC
GO          
*/