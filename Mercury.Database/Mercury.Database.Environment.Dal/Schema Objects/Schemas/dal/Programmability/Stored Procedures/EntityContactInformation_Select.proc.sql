/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'EntityContactInformation_Select' AND type = 'P'))
  DROP PROCEDURE dal.EntityContactInformation_Select
GO      
*/

CREATE PROCEDURE dal.EntityContactInformation_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @entityContactInformationId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.EntityContactInformation WHERE EntityContactInformationId = @entityContactInformationId

    END        