using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms.Controls {

    public class Metric : Control {

        #region Private Properties

        private Int64 memberId;

        private Int64 memberMetricId;

        private Int64 metricId;

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

                    //DataSourceChanged ();

                }

            }

        }

        public Int64 MemberMetricId { get { return memberMetricId; } set { memberMetricId = value; } }

        public Int64 MetricId {

            get { return metricId; }

            set {

                if (metricId != value) {

                    metricId = value;

                    metricName = String.Empty;

                    NotifyPropertyChanged ("MetricId");

                    //if (Application != null) {

                    //    Core.MedicalMetrics.Metric metric = Application.MedicalMetricGet (metricId, true);

                    //    if (metric != null) { metricName = metric.Name; }

                    //}

                    //DataSourceChanged ();

                }

            }

        }

        public String MetricName { get { return metricName; } set { metricName = value; } }

        public DateTime? MetricDate {

            get { return metricDate; }

            set {

                if ((!metricDate.HasValue) && (value == null)) { return; }

                if (metricDate != value) {

                    metricDate = value;

                    // if (GetEventHandler ("MetricDateChanged") != null) { RaiseEvent ("MetricDateChanged"); }

                }

            }

        }

        public DateTime MetricDateValue { get { return (HasMetric) ? metricDate.Value : new DateTime (); } }

        public Decimal MetricValue { get { return metricValue; } set { metricValue = value; } }

        public Boolean HasMetric { get { return (metricDate.HasValue); } }


        public override Boolean HasValue { get { return HasMetric; } }

        public override String Value { get { return (HasValue) ? metricValue.ToString () : String.Empty; } }


        #endregion


        #region Silverlight Public Properties

        public String ServiceNameText {

            get {

                return string.Empty;

                //if (String.IsNullOrEmpty ()) { return "** No Metric Available"; }

                //return serviceName;

            }

        }

        #endregion


        #region Constructors

        virtual protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Server.Application.FormControlType.Metric;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.CanDataBind = false;

            capabilities.IsDataSource = false;

            label = new Label (Application, this);

            return;

        }

        override public void BaseConstructor (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (parentControl, serverControl);

            Server.Application.FormControlMetric serverMetric = (Server.Application.FormControlMetric) serverControl;


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

        public Metric (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControlMetric serverMetric) {

            InitializeControl (applicationReference);

            BaseConstructor (parentControl, serverMetric);

            ChildServerControlsToLocal (this, serverMetric);

            return;

        }

        #endregion


        #region Silverlight Data Bindings and Async Operations

        protected override void NotifyPropertyChanged (String propertyName) {

            if (String.IsNullOrEmpty (propertyName)) { return; }


            switch (propertyName) {

                case "MetricId":

                    base.NotifyPropertyChanged ("MetricId");

                    base.NotifyPropertyChanged ("MetricName");

                    base.NotifyPropertyChanged ("MetricNameText");

                    break;

                case "MostRecentMemberMetricDateVisible":

                    base.NotifyPropertyChanged ("MostRecentMemberMetricDateVisible");

                    base.NotifyPropertyChanged ("MetricLastDateVisibility");

                    break;

                case "MetricDateVisible":

                    base.NotifyPropertyChanged ("MetricDateVisible");

                    base.NotifyPropertyChanged ("MetricDateVisibility");

                    break;

                default:

                    base.NotifyPropertyChanged (propertyName);

                    break;

            }

            return;

        }

        #endregion 


        #region Virtual Overrides

        public override void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            base.LocalControlToServer (parentControl, serverControl);

            ((Server.Application.FormControlMetric) serverControl).MemberId = memberId;

            ((Server.Application.FormControlMetric) serverControl).MemberMetricId = memberMetricId;

            ((Server.Application.FormControlMetric) serverControl).MetricId = metricId;

            ((Server.Application.FormControlMetric) serverControl).MetricName = metricName;

            ((Server.Application.FormControlMetric) serverControl).MetricDate = metricDate;

            ((Server.Application.FormControlMetric) serverControl).MetricValue = metricValue;


            ((Server.Application.FormControlMetric) serverControl).Label = new Server.Application.FormControlLabel ();

            label.LocalControlToServer (serverControl, ((Server.Application.FormControlMetric) serverControl).Label);

            return;

        }

        #endregion

    }

}
