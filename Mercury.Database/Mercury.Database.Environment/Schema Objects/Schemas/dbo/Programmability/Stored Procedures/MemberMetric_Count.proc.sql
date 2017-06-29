/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_Count' AND type = 'P'))
  DROP PROCEDURE MemberMetric_Count
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_Count
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId                BIGINT,
      @showHidden                 BIT

    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */
        
        DECLARE @serviceCount AS INT

        /* LOCAL VARIABLES ( END ) */

        SELECT @serviceCount = COUNT (1)
         
          FROM MemberMetric 
          
            JOIN Metric ON MemberMetric.MetricId = Metric.MetricId 
            
          WHERE MemberMetric.MemberId = @memberId
          
            AND ((Metric.Visible = 1) OR (@showHidden = 1))
  
   
        RETURN @serviceCount
              
    END    
              
    