using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Faxing {

    [DataContract (Name = "FaxServer")]
    public class FaxServer : Core.CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "AssemblyPath")]
        private String assemblyPath;

        [DataMember (Name = "AssemblyName")]
        private String assemblyName;

        [DataMember (Name = "AssemblyClassName")]
        private String assemblyClassName;

        [DataMember (Name = "FaxServerConfiguration")]
        private Public.Faxing.FaxServerConfiguration faxServerConfiguration = new Public.Faxing.FaxServerConfiguration ();

        [DataMember (Name = "WebServiceHostConfiguration")]
        private Public.WebService.WebServiceHostConfiguration webServiceHostConfiguration = new Public.WebService.WebServiceHostConfiguration ();

        #endregion


        #region Public Properties

        public String AssemblyPath { get { return assemblyPath; } set { assemblyPath = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Path); } }

        public String AssemblyName { get { return assemblyName; } set { assemblyName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Namespace); } }

        public String AssemblyClassName { get { return assemblyClassName; } set { assemblyClassName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Namespace); } }

        public Public.Faxing.FaxServerConfiguration FaxServerConfiguration { get { return faxServerConfiguration; } set { faxServerConfiguration = value; } }

        public Public.WebService.WebServiceHostConfiguration WebServiceHostConfiguration { get { return webServiceHostConfiguration; } set { webServiceHostConfiguration = value; } }

        public String FaxServerConfigurationSql {

            get {

                if (faxServerConfiguration == null) { faxServerConfiguration = new Public.Faxing.FaxServerConfiguration (); }


                String configurationSql = faxServerConfiguration.XmlSerialize ().ChildNodes[1].OuterXml;

                configurationSql = configurationSql.Replace ("'", "''");

                configurationSql = configurationSql.Replace ((char)0xA0, (char)0x20);

                configurationSql = configurationSql.Replace ((char)0xB7, (char)0x20);

                return configurationSql;

            }

        }

        public String WebServiceHostConfigurationSql {

            get {

                if (webServiceHostConfiguration == null) { webServiceHostConfiguration = new Public.WebService.WebServiceHostConfiguration (); }


                String configurationSql = webServiceHostConfiguration.XmlSerialize ().ChildNodes[1].OuterXml;

                configurationSql = configurationSql.Replace ("'", "''");

                configurationSql = configurationSql.Replace ((char)0xA0, (char)0x20);

                configurationSql = configurationSql.Replace ((char)0xB7, (char)0x20);

                return configurationSql;

            }

        }

        public String AssemblyReference {

            get {

                String assemblyReference = assemblyPath.Replace ("%EnvironmentName%", "");

                assemblyReference = assemblyReference + "\\" + assemblyName;

                assemblyReference = assemblyReference.Replace ("\\\\", "\\");

                return assemblyReference;

            }

        }

        public String SenderEmailAddress { get { return faxServerConfiguration.SenderEmailAddress.Replace ("%useraccountname", application.Session.UserAccountName); } }

        #endregion


        #region Constructors

        public FaxServer (Application applicationReference) { BaseConstructor (applicationReference); return; }

        public FaxServer (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        public FaxServer (Application applicationReference, Int64 forId, Int64 forEnvironmentId) {

            BaseConstructor (applicationReference, forId, forEnvironmentId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyPath", AssemblyPath);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyName", AssemblyName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "AssemblyClassName", AssemblyClassName);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "FaxServerConfiguration", FaxServerConfigurationSql);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WebServiceHostConfiguration", WebServiceHostConfigurationSql);

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlDocument configurationXml = new System.Xml.XmlDocument ();


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                                    case "AssemblyPath": AssemblyPath = currentPropertyNode.InnerText; break;

                                    case "AssemblyName": AssemblyName = currentPropertyNode.InnerText; break;

                                    case "AssemblyClassName": AssemblyClassName = currentPropertyNode.InnerText; break;

                                    case "FaxServerConfiguration":

                                        configurationXml = new System.Xml.XmlDocument ();


                                        FaxServerConfiguration = new Public.Faxing.FaxServerConfiguration ();

                                        try {

                                            if (!String.IsNullOrEmpty (currentPropertyNode.InnerText)) {

                                                configurationXml.LoadXml (currentPropertyNode.InnerText);

                                                FaxServerConfiguration.XmlImport (configurationXml.ChildNodes[0]);

                                            }

                                        }

                                        catch { /* DO NOTHING */ }

                                        break;

                                    case "WebServiceHostConfiguration":

                                        configurationXml = new System.Xml.XmlDocument ();


                                        WebServiceHostConfiguration = new Public.WebService.WebServiceHostConfiguration ();

                                        try {

                                            if (!String.IsNullOrEmpty (currentPropertyNode.InnerText)) {

                                                configurationXml.LoadXml (currentPropertyNode.InnerText);

                                                WebServiceHostConfiguration.XmlImport (configurationXml.ChildNodes[0]);

                                            }

                                        }

                                        catch { /* DO NOTHING */ }

                                        break;

                                    default: break;

                                }

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save " + base.ObjectType + ": " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 



        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE ASSEMBLY PATH IS NOT EMPTY
            AssemblyPath = AssemblyPath;

            if (String.IsNullOrEmpty (AssemblyPath)) { validationResponse.Add ("Assembly Path", "Empty or Null."); }

            // VALIDATE ASSEMBLY NAME IS NOT EMPTY
            AssemblyName = AssemblyName;

            if (String.IsNullOrEmpty (AssemblyName)) { validationResponse.Add ("Assembly Name", "Empty or Null."); }

            // VALIDATE CLASS NAME IS NOT EMPTY
            AssemblyClassName = AssemblyClassName;

            if (String.IsNullOrEmpty (AssemblyClassName)) { validationResponse.Add ("Class Name", "Empty or Null."); }


            return validationResponse;

        }

        #endregion


        #region Database Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            System.Xml.XmlDocument configurationXml = new System.Xml.XmlDocument ();


            assemblyPath = (String)currentRow["AssemblyPath"];

            assemblyName = (String)currentRow["AssemblyName"];

            assemblyClassName = (String)currentRow["AssemblyClassName"];


            FaxServerConfiguration = new Public.Faxing.FaxServerConfiguration ();

            try {

                if (!(currentRow["FaxServerConfiguration"] is DBNull)) {

                    if (!String.IsNullOrEmpty ((String)currentRow["FaxServerConfiguration"])) {

                        configurationXml.LoadXml ((String)currentRow["FaxServerConfiguration"]);

                        FaxServerConfiguration.XmlImport (configurationXml.ChildNodes[0]);

                    }

                }

            }
                
            catch { /* DO NOTHING */ }


            WebServiceHostConfiguration = new Public.WebService.WebServiceHostConfiguration ();

            try {

                if (!(currentRow["WebServiceHostConfiguration"] is DBNull)) {

                    if (!String.IsNullOrEmpty ((String)currentRow["WebServiceHostConfiguration"])) {

                        configurationXml.LoadXml ((String)currentRow["WebServiceHostConfiguration"]);

                        WebServiceHostConfiguration.XmlImport (configurationXml.ChildNodes[0]);

                    }

                }

            }

            catch { /* DO NOTHING */ }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.FaxServerManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.FaxServer_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append ("'" + assemblyPath.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assemblyName.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + assemblyClassName.Replace ("'", "''") + "', ");



                sqlStatement.Append ("'" + FaxServerConfigurationSql + "', ");

                sqlStatement.Append ("'" + WebServiceHostConfigurationSql + "', ");

                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
