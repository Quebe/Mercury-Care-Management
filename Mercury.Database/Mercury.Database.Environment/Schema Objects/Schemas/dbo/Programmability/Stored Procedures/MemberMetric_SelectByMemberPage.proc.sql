/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'MemberMetric_SelectByMemberPage' AND type = 'P'))
  DROP PROCEDURE MemberMetric_SelectByMemberPage
GO      
*/

CREATE PROCEDURE dbo.MemberMetric_SelectByMemberPage
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @memberId            BIGINT,
      @initialRow             INT,
      @count                  INT,
      @showHidden             BIT
    /* STORED PROCEDURE - INPUTS ( END ) */

    /* STORED PROCEDURE - OUTPUTS (BEGIN) */
      -- EXAMPLE: @MYOUTPUT   VARCHAR (20)    OUTPUT    -- A VARIABLE DECLARATION FOR RETURN VALUE
    /* STORED PROCEDURE - OUTPUTS ( END ) */

  AS 
    BEGIN
      /* STORED PROCEDURE (BEGIN) */

        /* LOCAL VARIABLES (BEGIN) */

        /* LOCAL VARIABLES ( END ) */

        SELECT MemberMetricPage.* 
          
          FROM (

            SELECT ROW_NUMBER () OVER (ORDER BY MemberId, EventDate DESC, MemberMetricId DESC) AS RowNumber, 
            
                MemberMetric.*,

                Metric.MetricId AS MetricId1, Metric.MetricName AS MetricName1, Metric.MetricDescription AS MetricDescription1,
    
                Metric.MetricType AS MetricType1, Metric.DataType AS DataType1, Metric.MinimumValue AS MinimumValue1, Metric.MaximumValue AS MaximumValue1,
                
                Metric.CostDataSource AS CostDataSource1, Metric.CostClaimDateType AS CostClaimDateType1,
                
                Metric.CostReportingPeriod AS CostReportingPeriod1, Metric.CostReportingPeriodValue AS CostReportingPeriodValue1, Metric.CostReportingPeriodQualifier AS CostReportingPeriodQualifier1,
                
                Metric.CostWatermarkPeriod AS CostWatermarkPeriod1, Metric.CostWatermarkPeriodValue AS CostWatermarkPeriodValue1, Metric.CostWatermarkPeriodQualifier AS CostWatermarkPeriodQualifier1,
                
                Metric.Enabled AS Enabled1, Metric.Visible AS Visible1, 

                Metric.ExtendedProperties AS ExtendedProperties1, 

                Metric.CreateAuthorityName AS CreateAuthorityName1, Metric.CreateAccountId AS CreateAccountId1, Metric.CreateAccountName AS CreateAccountName1, Metric.CreateDate AS CreateDate1, 

                Metric.ModifiedAuthorityName AS ModifiedAuthorityName1, Metric.ModifiedAccountId AS ModifiedAccountId1, Metric.ModifiedAccountName AS ModifiedAccountName1, Metric.ModifiedDate AS ModifiedDate1
           
              FROM MemberMetric JOIN Metric ON MemberMetric.MetricId = Metric.MetricId
              
              WHERE MemberId = @memberId
              
                AND ((Metric.Visible = 1) OR (@showHidden = 1))
              
            ) AS MemberMetricPage

          WHERE MemberMetricPage.RowNumber BETWEEN @initialRow AND (@initialRow + @count - 1)
          
    END    
              
    