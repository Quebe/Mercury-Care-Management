using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Public.WebService {

    [DataContract (Name = "WebServiceHostClientCredentials")]
    public class ClientCredentials {

        #region Private Properties

        [DataMember (Name = "UserName")]
        private String userName = String.Empty;

        [DataMember (Name = "Password")]
        private String password = String.Empty;

        [DataMember (Name = "Domain")]
        private String domain = String.Empty;

        [DataMember (Name = "WindowsImpersonationLevel")]
        private System.Security.Principal.TokenImpersonationLevel windowsImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.None;

        #endregion


        #region Public Properties

        public String UserName { get { return userName; } set { userName = value; } }

        public String Password { get { return password; } set { password = value; } }

        public String Domain { get { return domain; } set { domain = value; } }

        public System.Security.Principal.TokenImpersonationLevel WindowsImpersonationLevel { get { return windowsImpersonationLevel; } set { windowsImpersonationLevel = value; } }


        public System.Net.NetworkCredential Credentials {

            get {

                System.Net.NetworkCredential credentials;

                if (String.IsNullOrEmpty (userName)) { credentials = System.Net.CredentialCache.DefaultNetworkCredentials; }

                else { credentials = new System.Net.NetworkCredential (userName, password, domain); }

                return credentials;

            }

        }

        #endregion


        #region Public Properties

        public ClientCredentials () { /* DO NOTHING */ }

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

            System.Xml.XmlElement coreObjectNode = coreObjectDocument.CreateElement ("ClientCredentials");

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

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "UserName", UserName);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Password", Password);

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "Domain", Domain);


            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "WindowsImpersonationLevel", ((Int32)WindowsImpersonationLevel).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (coreObjectDocument, propertiesNode, "WindowsImpersonationLevelName", WindowsImpersonationLevel.ToString ());

            #endregion


            return coreObjectDocument;

        }

        public void XmlImport (System.Xml.XmlNode objectNode) {

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            if ("ClientCredentials" != objectNode.Name) {

                exceptionMessage = "Mismatch Object Types during import. Expected 'ClientCredentials', but found '" + objectNode.Name + "'.";

                throw new ApplicationException (exceptionMessage);

            }


            propertiesNode = objectNode.SelectSingleNode ("Properties");

            foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    case "UserName": UserName= currentPropertyNode.InnerText; break;

                    case "Password": Password = currentPropertyNode.InnerText; break;

                    case "Domain": Domain = currentPropertyNode.InnerText; break;

                    case "WindowsImpersonationLevel": WindowsImpersonationLevel = (System.Security.Principal.TokenImpersonationLevel)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                }

            }

            return;

        }

        #endregion


    }

}