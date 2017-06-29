using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.Faxing {

    [Serializable]
    [DataContract (Name = "FaxServerConfiguration")]
    public class FaxServerConfiguration {

        #region Private Properties

        [DataMember (Name = "FaxUrl")]
        private String faxUrl = String.Empty;

        [DataMember (Name = "FaxQueueName")]
        private String faxQueueName = String.Empty;


        [DataMember (Name = "MonitorInterval")]
        private Int32 monitorInterval = 60;

        [DataMember (Name = "MonitorTimeout")]
        private Int32 monitorTimeout = 600;

        [DataMember (Name = "SenderEmailAddress")]
        private String senderEmailAddress = String.Empty;

        #endregion 


        #region Public Properties

        public String FaxUrl { get { return faxUrl; } set { faxUrl = value; } }

        public String FaxQueueName { get { return faxQueueName; } set { faxQueueName = value; } }


        public Int32 MonitorInterval { get { return monitorInterval; } set { monitorInterval = value; } }

        public Int32 MonitorTimeout { get { return monitorTimeout; } set { monitorTimeout = value; } }


        public String SenderEmailAddress { get { return senderEmailAddress; } set { senderEmailAddress = value; } }
        
        #endregion 


        #region Public Constructors

        public FaxServerConfiguration () { /* DO NOTHING */ }

        public FaxServerConfiguration (String forFaxUrl, String forFaxQueueName) {

            faxUrl = forFaxUrl;

            faxQueueName = forFaxQueueName;

            return;

        }

        #endregion 


        #region XML Serialization

        public System.Xml.XmlDocument XmlEmptyDocument () {

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            xmlDocument.InsertBefore (xmlDeclaration, xmlDocument.DocumentElement);


            return xmlDocument;

        }

        public System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument coreObjectDocument = new System.Xml.XmlDocument ();

            System.Xml.XmlDeclaration xmlDeclaration = coreObjectDocument.CreateXmlDeclaration ("1.0", "utf-8", String.Empty);

            System.Xml.XmlElement coreObjectNode = coreObjectDocument.CreateElement ("FaxServerConfiguration");

            System.Xml.XmlElement propertiesNode = coreObjectDocument.CreateElement ("Properties");



            #region Initialize Document Structure

            coreObjectDocument.InsertBefore (xmlDeclaration, coreObjectDocument.DocumentElement);

            coreObjectDocument.AppendChild (coreObjectNode);

            coreObjectNode.AppendChild (propertiesNode);


            // ROOT (OBJECT TYPE) [NAME/ID]

            // +-- PROPERTIES 

            // +-- +-- PROPERTY [NAME/VALUE]

            #endregion


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "FaxUrl", FaxUrl);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "FaxQueueName", FaxQueueName);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "MonitorInterval", monitorInterval.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "MonitorTimeout", monitorTimeout.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "SenderEmailAddress", senderEmailAddress);


            #endregion


            return coreObjectDocument;

        }

        public void XmlImport (System.Xml.XmlNode objectNode) {

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if ("FaxServerConfiguration" != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected 'FaxServerConfiguration', but found '" + objectNode.Name + "'.";

                throw new ApplicationException (exceptionMessage);

            }


            propertiesNode = objectNode.SelectSingleNode ("Properties");

            foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "FaxUrl": FaxUrl = currentPropertyNode.InnerText; break;

                    case "FaxQueueName": FaxQueueName = currentPropertyNode.InnerText; break;

                    case "MonitorInterval": MonitorInterval = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "MonitorTimeout": MonitorTimeout = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "SenderEmailAddress": SenderEmailAddress = currentPropertyNode.InnerText; break;

                }

            }


            return;

        }

        #endregion

    }

}
