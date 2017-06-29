/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_RemoveManual' AND type = 'P'))
  DROP PROCEDURE dbo.MemberMetric_RemoveManual
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_RemoveManual
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberMetricId         BIGINT
    
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        IF EXISTS (SELECT * FROM dbo.MemberMetric WHERE (MemberMetricId = @memberMetricId) AND AddedManually = 1)
        
          BEGIN
          
            DELETE FROM MemberMetric WHERE MemberMetricId = @memberMetricId AND AddedManually = 1
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON MemberMetric_RemoveManual TO PUBLIC
GO          
*/