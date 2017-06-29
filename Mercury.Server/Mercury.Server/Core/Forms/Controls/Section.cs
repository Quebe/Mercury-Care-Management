using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {
    
    [Serializable]
    [DataContract (Name = "FormControlSection")]
    public class Section : Mercury.Server.Core.Forms.Control {

        #region Private Properties

        [DataMember (Name = "PageBreakAfterSection")]
        private Boolean pageBreakAfterSection = false;

        #endregion 


        #region Public Properties

        public Boolean PageBreakAfterSection { get { return pageBreakAfterSection; } set { pageBreakAfterSection = value; } }

        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "PageBreakAfterSection", pageBreakAfterSection.ToString ());

                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);


            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "PageBreakAfterSection": pageBreakAfterSection = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }

        #endregion 


        #region Constructors

        public Section (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Section;

            Capabilities.HasLabel = false;

            return;

        }

        #endregion


    }

}
