/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_SelectByTypeAndDate' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_SelectByTypeAndDate
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_SelectByTypeAndDate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId      BIGINT,
      @contactType      INT,
      @contactDate DATETIME
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT TOP 1 * FROM dbo.EntityContactInformation 
      
        WHERE 
        
          EntityId = @entityId AND ContactType = @contactType
          
            AND @contactDate BETWEEN EffectiveDate AND TerminationDate
            
        ORDER BY ContactSequence

    END        