/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_SelectByEntity' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_SelectByEntity
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_SelectByEntity
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.EntityContactInformation WHERE EntityId = @entityId
      
		ORDER BY TerminationDate DESC, ContactType, ContactSequence

    END        