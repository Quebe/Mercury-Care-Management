/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Sponsor_Select' AND type = 'P'))
  DROP PROCEDURE dal.Sponsor_Select
GO      
*/

CREATE PROCEDURE dal.Sponsor_Select
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @sponsorId      BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

      SELECT * FROM dbo.Sponsor WHERE SponsorId = @sponsorId
              
    END        