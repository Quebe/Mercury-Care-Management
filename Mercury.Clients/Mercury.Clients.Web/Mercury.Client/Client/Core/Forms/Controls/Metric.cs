using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms.Controls {

    [Serializable]
    public class Metric : Mercury.Client.Core.Forms.Control {

        #region Private Properties

        private Int64 memberId;

        private Int64 memberMetricId;

        protected Int64 metricId;

        private String metricName;

        protected DateTime? metricDate = null;

        protected Decimal metricValue = 0;

        #endregion


        #region Public Properties

        public Int64 MemberId {

            get { return memberId; }

            set {

                if (memberId != value) {

                    memberId = value;

                    DataSourceChanged ();

                }

            }

        }

        public Int64 MemberMetricId { get { return memberMetricId; } set { memberMetricId = value; } }

        public String MetricName { get { return metricName; } set { metricName = value; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public DateTime? MetricDate {

            get { return metricDate; }

            set {

                if ((!metricDate.HasValue) && (value == null)) { return; }

                if (metricDate != value) {

                    metricDate = value;

                    if (GetEventHandler ("MetricDateChanged") != null) { RaiseEvent ("MetricDateChanged"); }

                }

            }

        }

        public DateTime MetricDateValue { get { return (HasMetric) ? metricDate.Value : new DateTime (); } }

        public Decimal MetricValue { get { return metricValue; } set { metricValue = value; } }

        public Boolean HasMetric { get { return (metricDate.HasValue); } }


        public override Boolean HasValue { get { return HasMetric; } }

        public override String Value { get { return (HasValue) ? metricValue.ToString () : String.Empty; } }


        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Application.FormControlType.Metric;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = false;

            capabilities.IsDataSource = false;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, parentControl, serverControl);


            Mercury.Server.Application.FormControlMetric serverMetric = (Mercury.Server.Application.FormControlMetric) serverControl;


            memberId = serverMetric.MemberId;

            memberMetricId = serverMetric.MemberMetricId;

            metricId = serverMetric.MetricId;

            metricName = serverMetric.MetricName;

            metricDate = serverMetric.MetricDate;

            metricValue = serverMetric.MetricValue;


            label = new Label (Application, this, serverMetric.Label);

            return;

        }


        public Metric (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Metric (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label.Text = labelText;

            return;

        }

        public Metric (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Mercury.Server.Application.FormControlMetric serverMetric) {

            InitializeControl (applicationReference);

            BaseConstructor (applicationReference, parentControl, serverMetric);

            ChildServerControlsToLocal (this, serverMetric);

            return;

        }

        #endregion


        #region Virtual Overrides

        public override void LocalControlToServer (Mercury.Server.Application.FormControl parentControl, Mercury.Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Mercury.Server.Application.FormControlMetric) serverControl).MemberId = memberId;

            ((Mercury.Server.Application.FormControlMetric) serverControl).MemberMetricId = memberMetricId;

            ((Mercury.Server.Application.FormControlMetric) serverControl).MetricId = metricId;

            ((Mercury.Server.Application.FormControlMetric) serverControl).MetricName = metricName;

            ((Mercury.Server.Application.FormControlMetric) serverControl).MetricDate = metricDate;

            ((Mercury.Server.Application.FormControlMetric) serverControl).MetricValue = metricValue;


            ((Mercury.Server.Application.FormControlMetric) serverControl).Label = new Mercury.Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Mercury.Server.Application.FormControlMetric) serverControl).Label);

            return;

        }

        #endregion

    }

}
