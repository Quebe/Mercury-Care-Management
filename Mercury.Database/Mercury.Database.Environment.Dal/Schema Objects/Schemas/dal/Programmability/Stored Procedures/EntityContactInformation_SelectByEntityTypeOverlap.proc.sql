/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_SelectByEntityTypeOverlap' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_SelectByEntityTypeOverlap
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_SelectByEntityTypeOverlap
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId                 BIGINT,
      @contactType                 INT,
      @contactSequence						 INT,

      @effectiveDate          DATETIME,
      @terminationDate        DATETIME,
      
      @excludeEntityContactInformationId   BIGINT

      
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
        
          FROM dbo.EntityContactInformation
          
          WHERE 
          
						EntityId = @entityId
          
            AND (ContactType = @contactType)
            
            AND (ContactSequence = @contactSequence)
            
            AND (EffectiveDate <= @terminationDate) AND (TerminationDate >= @effectiveDate)
            
            AND (EntityContactInformationId <> @excludeEntityContactInformationId)
        
           
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dal.EntityContactInformation_Insert TO PUBLIC
GO          
*/