/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_Terminate' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_Terminate
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_Terminate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityContactInformationId          BIGINT,
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
        
        IF EXISTS (SELECT * FROM dbo.EntityContactInformation WHERE EntityContactInformationId = @entityContactInformationId AND @entityContactInformationId <> 0) 
        
          BEGIN -- UPDATE EXISTING INTERNAL (MERCURY) ADDRESS 
          
            UPDATE dbo.EntityContactInformation 
            
              SET 
                                            
                TerminationDate = @terminationDate,
                               
                ModifiedAuthorityName = @modifiedAuthorityName, 
                
                ModifiedAccountId = @modifiedAccountId, 
                
                ModifiedAccountName = @modifiedAccountName, 
                
                ModifiedDate = GETDATE ()
                
              WHERE EntityContactInformationId = @entityContactInformationId AND (@terminationDate >= EffectiveDate)
                
          END
          
          -- ELSE UPDATE EXTERNAL ADDRESS 
                     
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityContactInformation_Insert TO PUBLIC
GO          
*/