using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Controls {

    [Serializable]
    [DataContract (Name = "FormControlSectionColumn")]
    public class SectionColumn : Mercury.Server.Core.Forms.Control {


        #region Private Members

        #endregion


        #region Public Properties

        public override System.Xml.XmlDocument ExtendedPropertiesXml {

            get {

                System.Xml.XmlDocument extendedProperties = base.ExtendedPropertiesXml;

                /*
                System.Xml.XmlNode rootNode = extendedProperties.GetElementsByTagName ("ExtendedProperties")[0];

                System.Xml.XmlElement propertyNode;

                propertyNode = extendedProperties.CreateElement ("Property");

                propertyNode.SetAttribute ("Name", "Columns");

                propertyNode.InnerText = columns.ToString ();

                rootNode.AppendChild (propertyNode);
                */

                return extendedProperties;

            }

        }

        #endregion


        #region Constructors

        public SectionColumn (Application applicationReference) {

            BaseConstructor (applicationReference);

            ControlType = Mercury.Server.Core.Forms.Enumerations.FormControlType.SectionColumn;

            Capabilities.HasLabel = false;

            return;

        }

        #endregion


        #region Compile Methods

        public override List<CompileMessage> Compile () {

            List<CompileMessage> compileMessages = new List<CompileMessage> ();


            compileMessages.AddRange (base.Compile ());

            return compileMessages;

        }

        #endregion

    }

}
