using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.WebService {

    [DataContract (Name="WebServiceHostConfiguration")]
    public class WebServiceHostConfiguration {

        #region Private Properties

        [DataMember (Name = "Server")]
        private String server;

        [DataMember (Name = "Port")]
        private Int32 port = 80;

        [DataMember (Name = "ServicePath")]
        private String servicePath;

        [DataMember (Name = "ServiceName")]
        private String serviceName;

        [DataMember (Name = "BindingConfiguration")]
        private BindingConfiguration bindingConfiguration = new BindingConfiguration ();

        [DataMember (Name = "ClientCredentials")]
        private ClientCredentials clientCredentials = new ClientCredentials ();

        #endregion


        #region Public Properties

        public String Server { get { return server; } set { server = value; } }

        public Int32 Port { get { return port; } set { port = value; } }

        public String ServicePath { get { return servicePath; } set { servicePath = value; } }

        public String ServiceName { get { return serviceName; } set { serviceName = value; } }


        public BindingConfiguration BindingConfiguration { get { return bindingConfiguration; } set { bindingConfiguration = value; } }

        public System.ServiceModel.EndpointAddress EndpointAddress {

            get {

                System.ServiceModel.EndpointAddress endpointAddress = null;


                endpointAddress = new System.ServiceModel.EndpointAddress (

                    bindingConfiguration.Protocol + "://" +

                    server + ":" +

                    port +

                    ((!String.IsNullOrEmpty (servicePath)) ? "/" + servicePath + "/" : String.Empty) +

                    serviceName);


                return endpointAddress;

            }

        }

        public ClientCredentials ClientCredentials { get { return clientCredentials; } set { clientCredentials = value; } }

        #endregion


        #region Constructors

        public WebServiceHostConfiguration () { /* DO NOTHING */ }

        public WebServiceHostConfiguration (String forServer, Int32 forPort, String forServicePath, String forServiceName) {

            server = forServer;

            port = forPort;

            servicePath = forServicePath;

            serviceName = forServiceName;

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

            System.Xml.XmlElement coreObjectNode = coreObjectDocument.CreateElement ("WebServiceHostConfiguration");

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

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Server", server);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Port", port.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ServicePath", servicePath);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "ServiceName", serviceName);

            #endregion



            coreObjectDocument.ChildNodes[1].AppendChild (coreObjectDocument.ImportNode (BindingConfiguration.XmlSerialize ().ChildNodes[1], true));

            coreObjectDocument.ChildNodes[1].AppendChild (coreObjectDocument.ImportNode (ClientCredentials.XmlSerialize ().ChildNodes[1], true));



            return coreObjectDocument;

        }

        public void XmlImport (System.Xml.XmlNode objectNode) {

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if ("WebServiceHostConfiguration" != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected 'WebServiceHostConfiguration', but found '" + objectNode.Name + "'.";

                throw new ApplicationException (exceptionMessage);

            }


            propertiesNode = objectNode.SelectSingleNode ("Properties");

            foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "Server": Server = currentPropertyNode.InnerText; break;

                    case "Port": Port = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                    case "ServicePath": ServicePath = currentPropertyNode.InnerText; break;

                    case "ServiceName": ServiceName = currentPropertyNode.InnerText; break;

                }

            }


            bindingConfiguration = new BindingConfiguration ();

            bindingConfiguration.XmlImport (objectNode.SelectSingleNode ("BindingConfiguration"));


            clientCredentials = new ClientCredentials ();

            clientCredentials.XmlImport (objectNode.SelectSingleNode ("ClientCredentials"));

            return;

        }

        #endregion


    }

}
