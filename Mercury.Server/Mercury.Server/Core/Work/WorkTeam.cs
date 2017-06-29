using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [DataContract (Name = "WorkTeam")]
    public class WorkTeam : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "WorkTeamType")]
        private Enumerations.WorkTeamType workTeamType = Enumerations.WorkTeamType.WorkTeam;

        [DataMember (Name = "Membership")]
        private List<WorkTeamMembership> membership = new List<WorkTeamMembership> ();

        #endregion


        #region Public Properties

        public Enumerations.WorkTeamType WorkTeamType { get { return workTeamType; } set { workTeamType = value; } }
    
        public List<WorkTeamMembership> Membership { get { return membership; } set { membership = value; } }

        #endregion


        #region Constructors

        public WorkTeam (Application applicationReference) { base.BaseConstructor (applicationReference); return; }

        public WorkTeam (Application applicationReference, Int64 forWorkTeamId) {

            base.BaseConstructor (applicationReference, forWorkTeamId);

            return;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = base.Validate ();


            // VALIDATE UNIQUE INSTANCE
            WorkTeam duplicateObject = application.WorkTeamGet (Name);

            if (duplicateObject != null) {

                if (Id != duplicateObject.Id) { validationResponse.Add ("Duplicate", "Duplicate Found."); }

            }

            return validationResponse;

        }

        #endregion


        #region XML Serialization

        //public override System.Xml.XmlDocument XmlSerialize () {

        //    System.Xml.XmlDocument workTeamDocument = base.XmlSerialize ();

        //    System.Xml.XmlElement workTeamNode = workTeamDocument.CreateElement ("WorkTeam");

        //    System.Xml.XmlElement propertiesNode;



        //    workTeamDocument.AppendChild (workTeamNode);

        //    workTeamNode.SetAttribute ("Id", Id.ToString ());

        //    workTeamNode.SetAttribute ("Name", Name);

        //    propertiesNode = workTeamDocument.CreateElement ("Properties");

        //    workTeamNode.AppendChild (propertiesNode);


        //    #region Population Properties

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workTeamDocument, propertiesNode, "Id", workTeamId.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workTeamDocument, propertiesNode, "Name", workTeamName);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workTeamDocument, propertiesNode, "Description", workTeamDescription);

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workTeamDocument, propertiesNode, "Enabled", enabled.ToString ());

        //    CommonFunctions.XmlDocumentAppendPropertyNode (workTeamDocument, propertiesNode, "Visible", visible.ToString ());

        //    #endregion

            
        //    #region Membership

        //    System.Xml.XmlDocument membershipDocument;

        //    System.Xml.XmlElement membershipsNode;


        //    membershipsNode = workTeamDocument.CreateElement ("WorkTeams");

        //    workTeamNode.AppendChild (membershipsNode);


        //    foreach (WorkTeamMembership currentMembership in membership) {

        //        membershipDocument = currentMembership.XmlSerialize ();

        //        if (membershipDocument.ChildNodes[1] != null) {

        //            membershipsNode.AppendChild (workTeamDocument.ImportNode (membershipDocument.ChildNodes[1], true));

        //        }

        //    }

        //    #endregion


        //    return workTeamDocument;

        //}

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = objectNode.Attributes["Name"].InnerText;

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "WorkTeam") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {

        //                            case "WorkTeamName": workTeamName = currentProperty.InnerText; break;

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

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as WorkTeam.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forWorkTeamId) {

            Boolean success = base.Load (forWorkTeamId);
            

            // BASE METHOD CALLS MAP, CHILD MEMBERSHIP TABLE LOADED THROUGH MAP DATA FIELDS FUNCTION


            return success;

        }

        public void LoadMembership () {

            String selectStatement = "SELECT * FROM dbo.WorkTeamMembership WHERE WorkTeamId = " + Id.ToString () + " ORDER BY SecurityAuthorityId, UserAccountName";

            System.Data.DataTable membershipTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);


            Dictionary<Int64, String> securityAuthorityDictionary = application.SecurityAuthorityDictionary ();

            foreach (System.Data.DataRow currentRow in membershipTable.Rows) {

                WorkTeamMembership newMembership = new WorkTeamMembership (null);

                newMembership.MapDataFields (currentRow);

                if (securityAuthorityDictionary.ContainsKey (newMembership.SecurityAuthorityId)) {

                    newMembership.SecurityAuthorityName = securityAuthorityDictionary[newMembership.SecurityAuthorityId];

                }

                newMembership.Application = application;

                membership.Add (newMembership);

            }

            return;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            workTeamType = (Enumerations.WorkTeamType)((Int32)currentRow["WorkTeamType"]);

            LoadMembership ();

            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkTeamManage)) { throw new ApplicationException ("PermissionDenied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session); 
                
                
                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkTeam_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append (((Int32)workTeamType).ToString () + ", ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM dbo.WorkTeamMembership WHERE WorkTeamId = " + Id.ToString ());

                if (!success) { throw new ApplicationException (application.EnvironmentDatabase.LastException.Message); }


                foreach (WorkTeamMembership currentMembership in membership) {

                    currentMembership.Application = application;

                    currentMembership.WorkTeamId = Id;

                    currentMembership.Save ();

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
