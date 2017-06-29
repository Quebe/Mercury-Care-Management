using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlText")]
    public class Text : Mercury.Server.Core.Forms.Control {

        #region Private Properties

        [DataMember (Name = "Text")]
        String text = String.Empty;

        #endregion


        #region Public Properties

        public String TextContent { 
            
            get { return text; } 
            
            set { 
                
                text = value;

                text = text.Replace ((char) 0x2019, (char) 0x0027);

            }
        
        }


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

            // foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.SelectNodes ("./Property")) {


            foreach (System.Xml.XmlNode currentPropertyNode in extendedPropertiesXml.ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "Text": text = currentPropertyNode.InnerText; break;

                }

            }

            return;

        }


        public override Boolean HasValue { get { return (!String.IsNullOrEmpty (text)); } }

        public override String Value { get { return text; } }

        #endregion


        #region Constructors

        public Text (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Text;

            return;

        }

        public Text (Application applicationReference, String forText) {
            
            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.Text;

            text = forText;

            return;

        }

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();

            if (String.IsNullOrEmpty (text)) {

                compileMessages.Add (new CompileMessage (Mercury.Server.Core.Forms.Enumerations.FormCompileMessageType.Warning, "Text Control has no text assigned to it.", this));

            }

            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion


    }

}
