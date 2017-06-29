using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [DataContract (Name = "WorkTeamMembership")]
    public class WorkTeamMembership : CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkTeamId")]
        private Int64 workTeamId;

        [DataMember (Name = "SecurityAuthorityId")]
        private Int64 securityAuthorityId;

        [DataMember (Name = "SecurityAuthorityName")]
        private String securityAuthorityName;

        [DataMember (Name = "UserAccountId")]
        private String userAccountId;

        [DataMember (Name = "UserAccountName")]
        private String userAccountName;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName;

        [DataMember (Name = "WorkTeamRole")]
        private Enumerations.WorkTeamRole workTeamRole = Mercury.Server.Core.Work.Enumerations.WorkTeamRole.Member;

        #endregion


        #region Public Properties

        public Int64 WorkTeamId { get { return workTeamId; } set { workTeamId = value; } }

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } }

        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } }

        public String UserAccountName { get { return userAccountName; } set { userAccountName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public Enumerations.WorkTeamRole TeamRole { get { return workTeamRole; } set { workTeamRole = value; } }

        #endregion


        #region Constructors

        public WorkTeamMembership (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument workTeamMembershipDocument = base.XmlSerialize ();

            System.Xml.XmlElement workTeamMembershipNode = workTeamMembershipDocument.CreateElement ("WorkTeamMembership");

            System.Xml.XmlElement propertiesNode;



            workTeamMembershipDocument.AppendChild (workTeamMembershipNode);

            propertiesNode = workTeamMembershipDocument.CreateElement ("Properties");

            workTeamMembershipNode.AppendChild (propertiesNode);


            #region Population Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "WorkTeamId", workTeamId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "SecurityAuthorityId", securityAuthorityId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "SecurityAuthorityName", securityAuthorityName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "UserAccountId", userAccountId);

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "UserAccountName", userAccountName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "UserDisplayName", userDisplayName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "WorkTeamRole", ((Int32)workTeamRole).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workTeamMembershipDocument, propertiesNode, "WorkTeamRoleName", workTeamRole.ToString ());

            #endregion


            return workTeamMembershipDocument;

        }

        //public override List<Mercury.Server.Services.Responses.ConfigurationImportResponse> XmlImport (System.Xml.XmlNode objectNode) {

        //    List<Mercury.Server.Services.Responses.ConfigurationImportResponse> response = new List<Mercury.Server.Services.Responses.ConfigurationImportResponse> ();

        //    Services.Responses.ConfigurationImportResponse importResponse = new Mercury.Server.Services.Responses.ConfigurationImportResponse ();


        //    importResponse.ObjectType = objectNode.Name;

        //    importResponse.ObjectName = "WorkTeamMembership";

        //    importResponse.Success = true;


        //    if (importResponse.ObjectType == "WorkTeamMembership") {

        //        foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

        //            switch (currentNode.Name) {

        //                case "Properties":

        //                    foreach (System.Xml.XmlNode currentProperty in currentNode.ChildNodes) {

        //                        switch (currentProperty.Attributes["Name"].InnerText) {


        //                            case "SecurityAuthorityId": SecurityAuthorityId = currentNode.InnerText; break;

        //                            case "WorkTeamMembershipName": workTeamMembershipName = currentProperty.InnerText; break;

        //                            case "Description": description = currentProperty.InnerText; break;

        //                            case "Enabled": enabled = Convert.ToBoolean (currentProperty.InnerText); break;

        //                            case "Visible": visible = Convert.ToBoolean (currentProperty.InnerText); break;

        //                        }

        //                    }

        //                    break;

        //            }

        //        }

        //        importResponse.Success = Save ();

        //        if (!importResponse.Success) { importResponse.SetException (base.application.LastException); }

        //    }

        //    else { importResponse.SetException (new ApplicationException ("Invalid Object Type Parsed as WorkTeamMembership.")); }

        //    response.Add (importResponse);

        //    return response;

        //}

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            workTeamId = (Int64) currentRow["WorkTeamId"];

            SecurityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];

            UserAccountId = (String) currentRow["UserAccountId"];

            UserAccountName = (String) currentRow["UserAccountName"];

            UserDisplayName = (String) currentRow["UserDisplayName"];

            workTeamRole = (Mercury.Server.Core.Work.Enumerations.WorkTeamRole) ((Int32) currentRow["WorkTeamRole"]);

            
            if (application != null) {

                securityAuthorityName = application.SecurityAuthorityGetNameById (securityAuthorityId);

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC dbo.WorkTeamMembership_InsertUpdate ");

            sqlStatement.Append (workTeamId.ToString () + ", ");

            sqlStatement.Append (securityAuthorityId.ToString () + ", ");

            sqlStatement.Append ("'" + userAccountId.ToString () + "', ");

            sqlStatement.Append ("'" + userAccountName.Replace ("'", "''") + "', ");

            sqlStatement.Append ("'" + userDisplayName.Replace ("'", "''") + "', ");

            sqlStatement.Append (((Int32)workTeamRole).ToString () + ", ");

            sqlStatement.Append ("'" + ModifiedAccountInfo.SecurityAuthorityNameSql + "', '" + ModifiedAccountInfo.UserAccountIdSql + "', '" + ModifiedAccountInfo.UserAccountNameSql + "'");

            success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;


        }

        public Boolean Delete () {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();


            sqlStatement.Append ("DELETE FROM dbo.WorkTeamMembership WHERE WorkTeamId = " + workTeamId.ToString ());

            sqlStatement.Append ("  AND SecurityAuthorityId = " + securityAuthorityId.ToString ());

            sqlStatement.Append ("  AND UserAccountId = '" + userAccountId.ToString () + "'");

            success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());


            return success;

        }

        #endregion


        #region Public Methods

        public Boolean IsEqual (WorkTeamMembership compareMembership) {

            Boolean isEqual = true;

            isEqual = isEqual && (this.workTeamId == compareMembership.WorkTeamId);

            isEqual = isEqual && (this.securityAuthorityId != compareMembership.SecurityAuthorityId);

            isEqual = isEqual && (this.userAccountId != compareMembership.UserAccountId);

            isEqual = isEqual && (this.userDisplayName != compareMembership.UserDisplayName);

            isEqual = isEqual && (this.workTeamRole != compareMembership.workTeamRole);

            return isEqual;

        }

        #endregion

    }

}
