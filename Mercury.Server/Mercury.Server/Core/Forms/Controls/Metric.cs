using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlMetric")]
    public class Metric : Mercury.Server.Core.Forms.Control {

        #region Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "MemberMetricId")]
        private Int64 memberMetricId;

        [DataMember (Name = "MetricId")]
        private Int64 metricId;

        [DataMember (Name = "MetricName")]
        private String metricName;

        [DataMember (Name = "MetricDate")]
        private DateTime? metricDate = null;

        [DataMember (Name = "MetricValue")]
        private Decimal metricValue = 0;


        [NonSerialized]
        private Member.Member member = null;

        [NonSerialized]
        private Core.Metrics.Metric metric = null;

        #endregion


        #region Public Properties

        public Int64 MemberId {

            get { return memberId; }

            set {

                memberId = value;

                member = null;
                
            }

        }

        public Int64 MemberMetricId { get { return memberMetricId; } set { memberMetricId = value; } }

        public Int64 MetricId { get { return metricId; } set { metricId = value; } }

        public String MetricName {

            get {

                if (String.IsNullOrEmpty (metricName)) {

                    if (MetricObject != null) { metricName = MetricObject.Name; }

                    else { metricName = String.Empty; }

                }

                return metricName;

            }


            set { metricName = value; }
        }

        public DateTime? MetricDate { get { return metricDate; } set { metricDate = value; } }

        public DateTime MetricDateValue { get { return (HasMetric) ? metricDate.Value : new DateTime (); } }

        public Decimal MetricValue { get { return metricValue; } set { metricValue = value; } }

        public Boolean HasMetric { get { return (metricDate.HasValue); } }


        public override Boolean HasValue { get { return HasMetric; } }

        public override String Value { get { return (HasValue) ? metricValue.ToString () : String.Empty; } }


        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "MemberId", memberId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MemberMetricId", memberMetricId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MetricId", metricId.ToString ());

                ExtendedProperties_AddProperty (extendedProperties, "MetricName", MetricName);

                ExtendedProperties_AddProperty (extendedProperties, "MetricDate", (HasMetric) ? MetricDateValue.ToString ("MM/dd/yyyy") : String.Empty);

                ExtendedProperties_AddProperty (extendedProperties, "MetricValue", metricValue.ToString ());


                System.Xml.XmlNode rootNode = extendedProperties.GetElementsByTagName ("ExtendedProperties")[0];


                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {


            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "MemberId": memberId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "MemberMetricId": memberMetricId = Convert.ToInt64 (currentPropertyNode.InnerText); break;

                    case "MetricId":

                        metricId = Convert.ToInt64 (currentPropertyNode.InnerText);

                        // metricName = application.MetricGetNameById (metricId);

                        break;

                    case "MetricName":

                        MetricId = application.MetricGetIdByName (currentPropertyNode.InnerText);

                        if (metricId != 0) { metricName = currentPropertyNode.InnerText; }

                        break;

                    case "MetricDate":

                        DateTime parsedDateTime;

                        if (DateTime.TryParse (currentPropertyNode.InnerText, out parsedDateTime)) {

                            metricDate = parsedDateTime;
                        }

                        else { metricDate = null; }

                        break;

                    case "MetricValue": Convert.ToDecimal (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }


        public override Application Application {

            set {

                base.Application = value;

                if (label != null) { label.Application = value; }

            }

        }

        public Member.Member Member {

            get {

                if (member != null) { return member; }

                if (application == null) { return null; }

                member = application.MemberGet (memberId);

                return member;

            }

        }

        public Core.Metrics.Metric MetricObject {

            get {

                if (metric != null) { return metric; }

                if (application == null) { return null; }

                metric = application.MetricGet (metricId);

                return metric;

            }

        }

        #endregion


        #region Constructors

        protected void InitializeControl (Application applicationReference) {

            BaseConstructor (applicationReference);

            controlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Metric;

            capabilities.HasValue = true;

            capabilities.HasLabel = true;

            capabilities.IsDataSource = false;

            capabilities.CanDataBind = false;

            return;

        }

        public Metric (Application applicationReference) {

            InitializeControl (applicationReference);

            return;

        }

        public Metric (Application applicationReference, String labelText) {

            InitializeControl (applicationReference);

            label = new Label (application, labelText);

            return;

        }

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();

            if ((label.Visible) && (String.IsNullOrEmpty (label.Text))) {

                compileMessages.Add (new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Warning, "Label is set to visible without label text specified.", this));

            }

            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion

    }

}
