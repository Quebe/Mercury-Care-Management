using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlButton")]
    public class Button : Mercury.Server.Core.Forms.Control {
        
        #region Private Members

        [DataMember (Name = "Text")]
        String text = String.Empty;

        #endregion


        #region Public Properties

        public String Text { get { return text; } set { text = value; } }

        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                text = text.Replace ((char) 0x2019, (char) 0x0027);

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                ExtendedProperties_AddProperty (extendedProperties, "Text", text);

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

        #endregion


        #region Constructors

        public Button (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Button;

            Capabilities.HasLabel = false;

            return;

        }

        #endregion


        #region Event Handlers

        public override List<String> Events {

            get {

                List<String> events = new List<String> ();

                events.Add ("Click");

                return events;

            }

        }

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();


            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion


        #region Public Methods

        public void Click () {

            RaiseEvent ("Click");

            return;

        }

        #endregion 

    }

}
