using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Metrics {

    [Serializable]
    public class Metric : CoreConfigurationObject {
        
        #region Private Properties

        private Mercury.Server.Application.MetricType metricType = Mercury.Server.Application.MetricType.Health;

        private Mercury.Server.Application.MetricDataType dataType = Mercury.Server.Application.MetricDataType.Decimal;

        private Decimal minimumValue = 0;

        private Decimal maximumValue = 1000;


        private Mercury.Server.Application.MetricCostDataSource costDataSource = Mercury.Server.Application.MetricCostDataSource.AllClaims;

        private Mercury.Server.Application.MetricCostClaimDateType costClaimDateType = Server.Application.MetricCostClaimDateType.PaidDate;


        private Mercury.Server.Application.MetricCostReportingPeriod costReportingPeriod = Mercury.Server.Application.MetricCostReportingPeriod.CalenderYear;

        private Int32 costReportingPeriodValue = 0;

        private Mercury.Server.Application.DateQualifier costReportingPeriodQualifier = Mercury.Server.Application.DateQualifier.Months;

        private Mercury.Server.Application.MetricCostWatermarkPeriod costWatermarkPeriod = Mercury.Server.Application.MetricCostWatermarkPeriod.CalenderYear;

        private Int32 costWatermarkPeriodValue = 0;

        private Mercury.Server.Application.DateQualifier costWatermarkPeriodQualifier = Mercury.Server.Application.DateQualifier.Months;


        private Dictionary<Int64, String> costServices = new Dictionary<Int64, String> ();
        
        #endregion


        #region Public Properties

        public Mercury.Server.Application.MetricType MetricType { get { return metricType; } set { metricType = value; } }

        public Mercury.Server.Application.MetricDataType DataType { get { return dataType; } set { dataType = value; } }

        public Decimal MinimumValue { get { return minimumValue; } set { minimumValue = value; } }

        public Decimal MaximumValue { get { return maximumValue; } set { maximumValue = value; } }


        public Mercury.Server.Application.MetricCostDataSource CostDataSource { get { return costDataSource; } set { costDataSource = value; } }

        public Mercury.Server.Application.MetricCostClaimDateType CostClaimDateType { get { return costClaimDateType; } set { costClaimDateType = value; } }


        public Mercury.Server.Application.MetricCostReportingPeriod CostReportingPeriod { get { return costReportingPeriod; } set { costReportingPeriod = value; } }

        public Int32 CostReportingPeriodValue { get { return costReportingPeriodValue; } set { costReportingPeriodValue = value; } }

        public Mercury.Server.Application.DateQualifier CostReportingPeriodQualifier { get { return costReportingPeriodQualifier; } set { costReportingPeriodQualifier = value; } }

        public Mercury.Server.Application.MetricCostWatermarkPeriod CostWatermarkPeriod { get { return costWatermarkPeriod; } set { costWatermarkPeriod = value; } }

        public Int32 CostWatermarkPeriodValue { get { return costWatermarkPeriodValue; } set { costWatermarkPeriodValue = value; } }

        public Mercury.Server.Application.DateQualifier CostWatermarkPeriodQualifier { get { return costWatermarkPeriodQualifier; } set { costWatermarkPeriodQualifier = value; } }


        public Dictionary<Int64, String> CostServices { get { return costServices; } set { costServices = value; } }

        #endregion


        #region Public Properties

        public String ValueRangeDescription { get { return (MinimumValue.ToString () + " - " + MaximumValue.ToString ()); } }

        #endregion 


        #region Constructors

        public Metric (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public Metric (Application applicationReference, Mercury.Server.Application.Metric serverMetric) {

            base.BaseConstructor (applicationReference, serverMetric);


            metricType = serverMetric.MetricType;

            dataType = serverMetric.DataType;

            minimumValue = serverMetric.MinimumValue;

            maximumValue = serverMetric.MaximumValue;


            costDataSource = serverMetric.CostDataSource;

            costClaimDateType = serverMetric.CostClaimDateType;


            costReportingPeriod = serverMetric.CostReportingPeriod;

            costReportingPeriodValue = serverMetric.CostReportingPeriodValue;

            costReportingPeriodQualifier = serverMetric.CostReportingPeriodQualifier;

            costWatermarkPeriod = serverMetric.CostWatermarkPeriod;

            costWatermarkPeriodValue = serverMetric.CostWatermarkPeriodValue;

            costWatermarkPeriodQualifier = serverMetric.CostWatermarkPeriodQualifier; 
            
            return;

        }

        #endregion

    
        #region Public Methods

        public virtual void MapToServerObject (Server.Application.Metric serverMetric) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverMetric);


            serverMetric.Description = description;

            serverMetric.MetricType = metricType;

            serverMetric.DataType = dataType;

            serverMetric.MinimumValue = minimumValue;

            serverMetric.MaximumValue = maximumValue;


            serverMetric.CostDataSource = costDataSource;

            serverMetric.CostClaimDateType = costClaimDateType;


            serverMetric.CostReportingPeriod = costReportingPeriod;

            serverMetric.CostReportingPeriodValue = costReportingPeriodValue;

            serverMetric.CostReportingPeriodQualifier = costReportingPeriodQualifier;


            serverMetric.CostWatermarkPeriod = costWatermarkPeriod;

            serverMetric.CostWatermarkPeriodValue = costWatermarkPeriodValue;

            serverMetric.CostWatermarkPeriodQualifier = costWatermarkPeriodQualifier;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.Metric serverMetric = new Server.Application.Metric ();

            MapToServerObject (serverMetric);

            return serverMetric;

        }

        public Metric Copy () {

            Server.Application.Metric serverMetric = (Server.Application.Metric)ToServerObject ();

            Metric copiedMetric = new Metric (application, serverMetric);

            return copiedMetric;

        }

        public Boolean IsEqual (Metric compareMetric) {

            Boolean isEqual = base.IsEqual (compareMetric);


            if (description != compareMetric.Description) { isEqual = false; }

            if (metricType != compareMetric.MetricType) { isEqual = false; }

            if (dataType != compareMetric.DataType) { isEqual = false; }

            if (minimumValue != compareMetric.MinimumValue) { isEqual = false; }

            if (maximumValue != compareMetric.MaximumValue) { isEqual = false; }


            isEqual &= (costDataSource == compareMetric.CostDataSource);

            isEqual &= (costClaimDateType == compareMetric.CostClaimDateType);


            isEqual &= (costReportingPeriod == compareMetric.CostReportingPeriod);

            isEqual &= (costReportingPeriodValue == compareMetric.CostReportingPeriodValue);

            isEqual &= (costReportingPeriodQualifier == compareMetric.CostReportingPeriodQualifier);

            isEqual &= (costWatermarkPeriod == compareMetric.CostWatermarkPeriod);

            isEqual &= (costWatermarkPeriodValue == compareMetric.CostWatermarkPeriodValue);

            isEqual &= (costWatermarkPeriodQualifier == compareMetric.CostWatermarkPeriodQualifier);


            return isEqual;

        }

        #endregion 


    }

}
