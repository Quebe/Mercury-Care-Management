
/*
IF EXISTS (SELECT name FROM sysobjects WHERE (name = 'Metric_InsertUpdate' AND type = 'P'))
  DROP PROCEDURE dbo.Metric_InsertUpdate
GO      
*/

CREATE PROCEDURE dbo.Metric_InsertUpdate
    /* STORED PROCEDURE - INPUTS (BEGIN) */
      @metricId                BIGINT,
      @metricName             VARCHAR (060),
      @metricDescription      VARCHAR (999),
      @metricType                 INT,
      @dataType                   INT,
      @minimumValue         DECIMAL (20, 08),
      @maximumValue         DECIMAL (20, 08),
            
      @costDataSource INT,
      @costClaimDateType INT,
      
      @costReportingPeriod INT,
      @costReportingPeriodValue INT,
      @costReportingPeriodQualifier INT,
      
      @costWatermarkPeriod INT,
      @costWatermarkPeriodValue INT,
      @costWatermarkPeriodQualifier INT,
      
      @enabled                    BIT,
      @visible                    BIT,
      @extendedProperties         XML,
      
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

        IF EXISTS (SELECT * FROM dbo.Metric WHERE MetricId = @metricId)
          BEGIN
            -- EXISTING RECORD, UPDATE
            
            UPDATE dbo.Metric
              SET
                MetricName = @metricName,
                MetricDescription = @metricDescription,
                MetricType = @metricType,
                DataType   = @dataType,
                MinimumValue = @minimumValue,
                MaximumValue = @maximumValue,
					                
								CostDataSource = @costDataSource,
								CostClaimDateType = @costClaimDateType,
					      
								CostReportingPeriod = @costReportingPeriod,
								CostReportingPeriodValue = @costReportingPeriodValue,
								CostReportingPeriodQualifier = @costReportingPeriodQualifier,
					      
								CostWatermarkPeriod = @costWatermarkPeriod,
								CostWatermarkPeriodValue = @costWatermarkPeriodValue,
								CostWatermarkPeriodQualifier = @costWatermarkPeriodQualifier,
                
                Enabled = @enabled,
                Visible = @visible,
                ExtendedProperties = @extendedProperties,
                
                ModifiedAuthorityName = @modifiedAuthorityName,
                ModifiedAccountId     = @modifiedAccountId,
                ModifiedAccountName   = @modifiedAccountName,
                ModifiedDate          = GETDATE ()
                
              WHERE 
                MetricId = @metricId

          END
          
        ELSE -- NO EXISTING RECORD, INSERT NEW
          BEGIN
          
            INSERT INTO dbo.Metric (MetricName, MetricDescription, MetricType, DataType, MinimumValue, MaximumValue, 
            
								CostDataSource,
								CostClaimDateType,
					      
								CostReportingPeriod,
								CostReportingPeriodValue,
								CostReportingPeriodQualifier,
					      
								CostWatermarkPeriod,
								CostWatermarkPeriodValue,
								CostWatermarkPeriodQualifier,
            
								Enabled, Visible, ExtendedProperties,
            
                CreateAuthorityName,   CreateAccountId,   CreateAccountName,   CreateDate, 
                ModifiedAuthorityName, ModifiedAccountId, ModifiedAccountName, ModifiedDate)
                
            VALUES (
                
                @metricName, @metricDescription, @metricType, @dataType, @minimumValue, @maximumValue, 
                
                
								@costDataSource,
								@costClaimDateType,
					      
								@costReportingPeriod,
								@costReportingPeriodValue,
								@costReportingPeriodQualifier,
					      
								@costWatermarkPeriod,
								@costWatermarkPeriodValue,
								@costWatermarkPeriodQualifier,
                
                @enabled, @visible, @extendedProperties,

                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE (),
                @modifiedAuthorityName, @modifiedAccountId, @modifiedAccountName, GETDATE ())
           
          END                       
    
      /* STORED PROCEDURE ( END ) */
    END 
GO

/*
GRANT EXECUTE ON dbo.Document_InsertUpdate TO PUBLIC
GO          
*/