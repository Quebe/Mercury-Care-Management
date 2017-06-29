
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'PopulationProcess_ServiceEvents_ComplianceAnchor1' AND type = 'P'))
  DROP PROCEDURE dbo.PopulationProcess_ServiceEvents_ComplianceAnchor1
GO      
*/

CREATE PROCEDURE dbo.PopulationProcess_ServiceEvents_ComplianceAnchor1
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @serviceEventId                    BIGINT,
      
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

      EXEC PopulationProcess_ServiceEvents_ComplianceAnchor0 @serviceEventId, @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON PopulationProcess_ServiceEvents_ComplianceAnchor1 TO PUBLIC
GO          
*/