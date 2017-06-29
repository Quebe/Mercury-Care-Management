using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlLabel")]
    public class Label : Mercury.Server.Core.Forms.Control {

        #region Private Properties

        [DataMember (Name = "Text")]
        private String text = String.Empty;

        #endregion


        #region Public Properties

        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                System.Xml.XmlNode rootNode = extendedProperties.GetElementsByTagName ("ExtendedProperties")[0];

                System.Xml.XmlElement propertyNode;


                propertyNode = extendedProperties.CreateElement ("Property");

                propertyNode.SetAttribute ("Name", "Text");

                propertyNode.InnerText = text;

                rootNode.AppendChild (propertyNode);


                return extendedProperties;

            }

        }

        public override void ExtendedPropertiesDeserialize (System.Xml.XmlNode extendedPropertiesXml) {

            base.ExtendedPropertiesDeserialize (extendedPropertiesXml);

            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "Text": text = currentPropertyNode.InnerText; break;

                }

            }

            return;

        }

        public String Text { get { return text; } set { text = value; } }

        #endregion


        #region Constructors

        public Label (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Label;

            Capabilities.HasLabel = false;

            Name = "(Label)";

            return;

        }

        public Label (Application applicationReference, String text) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Label;

            Capabilities.HasLabel = false;

            Name = "(Label)";

            this.text = text;

            return;

        }

        #endregion


    }

}
