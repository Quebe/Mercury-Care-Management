using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkOutcome")]
    public class WorkOutcome : CoreConfigurationObject {

        #region Constructors

        public WorkOutcome (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            return;

        }

        public WorkOutcome (Application applicationReference, Int64 forWorkOutcomeId) {

            base.BaseConstructor (applicationReference, forWorkOutcomeId);

            return;

        }

        #endregion


        #region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument workOutcomeDocument = base.XmlSerialize ();

        //    System.Xml.XmlElement workOutcomeNode = workOutcomeDocument.CreateElement ("WorkOutcome");

        //    System.Xml.XmlElement propertiesNode;

        //    //System.Xml.XmlNode importedNode;



        //    workOutcomeDocument.AppendChild (workOutcomeNode);

        //    workOutcomeNode.SetAttribute ("WorkOutcomeId", workOutcomeId.ToString ());

        //    workOutcomeNode.SetAttribute ("Name", workOutcomeName);

        //    propertiesNode = workOutcomeDocument.CreateElement ("Properties");

        //    workOutcomeNode.AppendChild (propertiesNode);


        //    #region Work Outcome Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workOutcomeDocument, propertiesNode, "WorkOutcomeId", workOutcomeId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workOutcomeDocument, propertiesNode, "WorkOutcomeName", workOutcomeName);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workOutcomeDocument, propertiesNode, "WorkOutcomeDescription", workOutcomeDescription);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workOutcomeDocument, propertiesNode, "Enabled", enabled.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workOutcomeDocument, propertiesNode, "Visible", visible.ToString ());

        //    #endregion

        //    return workOutcomeDocument;

        //}

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes ["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "WorkOutcome") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "WorkOutcomeName": workOutcomeName = currentProperty.InnerText; break;

        //                            case "Description": description = currentProperty.InnerText; break;

        //                            case "Enabled": enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "Visible": visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                        }

        //                    }

        //                    break;

        //            }

        //        }

        //        importResponse.Success = Save ();

        //        importResponse.Id = Id;

        //        if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as WorkOutcome.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE UNIQUE INSTANCE
            WorkOutcome duplicateObject = application.WorkOutcomeGet (Name);

            if (duplicateObject != null) {

                if (Id != duplicateObject.Id) { validationResponse.Add ("Duplicate", "Duplicate Found."); }

            }

            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.WorkOutcome WHERE WorkOutcomeId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

            }

            return success;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkOutcomeManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkOutcome_InsertUpdate ");

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

                SetIdentity ();

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
