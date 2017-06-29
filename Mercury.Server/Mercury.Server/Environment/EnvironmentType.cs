using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Environment {

    [DataContract (Name = "EnvironmentType")]
    public class EnvironmentType : Core.CoreConfigurationObject {
        
        #region Constructors

        public EnvironmentType (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public EnvironmentType (Application applicationReference, Int64 forEnvironmentTypeId) {

            base.BaseConstructor (applicationReference, forEnvironmentTypeId);

            return;

        }

        #endregion 
        

        #region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument environmentTypeDocument = base.XmlSerialize ();

        //    System.Xml.XmlElement environmentTypeNode = environmentTypeDocument.CreateElement ("EnvironmentType");

        //    System.Xml.XmlElement propertiesNode;
            

        //    environmentTypeDocument.AppendChild (environmentTypeNode);

        //    environmentTypeNode.SetAttribute ("Id", Id.ToString ());

        //    environmentTypeNode.SetAttribute ("Name", Name);

        //    propertiesNode = environmentTypeDocument.CreateElement ("Properties");

        //    environmentTypeNode.AppendChild (propertiesNode);


        //    #region Environment Type Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (environmentTypeDocument, propertiesNode, "Id", Id.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (environmentTypeDocument, propertiesNode, "Name", Name);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (environmentTypeDocument, propertiesNode, "Description", Description);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (environmentTypeDocument, propertiesNode, "Enabled", Enabled.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (environmentTypeDocument, propertiesNode, "Visible", Visible.ToString ());

        //    #endregion


        //    return environmentTypeDocument;

        //}

        //public override List<Server.ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Server.ImportExport.Result> results = new List<ImportExport.Result> ();

        //    Server.ImportExport.Result importResult = new ImportExport.Result ();


        //    importResult.ObjectType = objectNode.Name;

        //    importResult.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResult.Success = true;


        //    if (importResult.ObjectType == "EnvironmentType") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "EnvironmentTypeName": environmentTypeName = currentProperty.InnerText; break;

        //                            case "EnvironmentTypeDescription": environmentTypeDescription = currentProperty.InnerText; break;

        //                            case "Enabled": enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "Visible": visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                        }

        //                    }

        //                    break;

        //            }

        //        }

        //        importResult.Success = Save ();

        //        importResult.Id = Id;

        //        if (!importResult.Success) { importResult.SetException (base.application.LastException); }

        //    }

        //    else { importResult.SetException (new ApplicationException ("Invalid Object Type Parsed as EnvironmentType.")); }


        //    results.Add (importResult);

        //    return results;

        //}

        #endregion

        
        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate (); // VALIDATE BASE OBJECT


            // VALIDATE UNIQUE INSTANCE
            if (application.EnvironmentTypeGetIdByName (Name) != Id) { validationResponse.Add ("Duplicate", "Duplicate Found."); }


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (long forId) {

            if (application.EnvironmentDatabase == null) { return false; }

            
            Boolean success = true;

            String selectStatement = "SELECT * FROM dbo.EnvironmentType WHERE EnvironmentTypeId = " + forId.ToString ();


            System.Data.DataTable table = application.EnterpriseDatabase.SelectDataTable (selectStatement);

            if (table.Rows.Count == 1) { 

                MapDataFields (table.Rows [0]);

            }

            else { success = false; }


            return success;
            
        }
        
        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            if (!application.HasEnvironmentPermission (Server.EnterprisePermissions.EnvironmentTypeManage)) {

                throw new ApplicationException ("Permission Denied.");

            }


            modifiedAccountInfo = new Data.AuthorityAccountStamp (application.Session);

            try {

                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.EnvironmentType_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                if (id == 0) { // RESET DOCUMENT ID CRITERIA

                    Object identity = application.EnvironmentDatabase.ExecuteScalar ("SELECT @@IDENTITY").ToString ();

                    if (!Int64.TryParse ((String) identity, out id)) { 

                        throw new ApplicationException ("Unable to retreive unique id.");

                    }

                }

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


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
