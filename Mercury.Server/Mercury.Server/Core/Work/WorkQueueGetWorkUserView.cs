using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueueGetWorkUserView")]
    public class WorkQueueGetWorkUserView : CoreObject {

        #region Private Properties

        [DataMember (Name = "WorkQueueId")]
        private Int64 workQueueId;

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

        [DataMember (Name = "WorkQueueViewId")]
        private Int64 workQueueViewId;

        [NonSerialized]
        private WorkQueueView workQueueView = null;

        #endregion


        #region Public Properties

        public Int64 WorkQueueId { get { return workQueueId; } set { workQueueId = value; } }

        public Int64 SecurityAuthorityId { get { return securityAuthorityId; } set { securityAuthorityId = value; } }

        public String SecurityAuthorityName { get { return securityAuthorityName; } set { securityAuthorityName = value; } }

        public String UserAccountId { get { return userAccountId; } set { userAccountId = value; } }

        public String UserAccountName { get { return userAccountName; } set { userAccountName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = CommonFunctions.SetValueMaxLength (value.Trim (), Data.DataTypeConstants.Name); } }

        public Int64 WorkQueueViewId { get { return workQueueViewId; } set { workQueueViewId = value; workQueueView = null; } }

        #endregion


        #region Public Properties

        public WorkQueueView WorkQueueView {

            get {

                if (workQueueView == null) {

                    workQueueView = application.WorkQueueViewGet (workQueueViewId);

                }

                return workQueueView;

            }

        }

        #endregion 


        #region Constructors

        public WorkQueueGetWorkUserView (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument workQueueMembershipDocument = base.XmlSerialize ();

            System.Xml.XmlElement workQueueMembershipNode = workQueueMembershipDocument.CreateElement ("WorkQueueUserView");

            System.Xml.XmlElement propertiesNode;



            workQueueMembershipDocument.AppendChild (workQueueMembershipNode);

            propertiesNode = workQueueMembershipDocument.CreateElement ("Properties");

            workQueueMembershipNode.AppendChild (propertiesNode);


            #region Population Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "WorkQueueId", workQueueId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "SecurityAuthorityId", securityAuthorityId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "SecurityAuthorityName", securityAuthorityName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "UserAccountId", userAccountId);

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "UserAccountName", userAccountName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "UserDisplayName", userDisplayName);

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "WorkQueueViewId", workQueueViewId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (workQueueMembershipDocument, propertiesNode, "WorkQueueViewName", WorkQueueView.Name);

            #endregion


            return workQueueMembershipDocument;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            workQueueId = (Int64) currentRow["WorkQueueId"];

            SecurityAuthorityId = (Int64) currentRow["SecurityAuthorityId"];

            UserAccountId = (String) currentRow["UserAccountId"];

            UserAccountName = (String) currentRow["UserAccountName"];

            UserDisplayName = (String) currentRow["UserDisplayName"];

            workQueueViewId = (Int64)currentRow["WorkQueueViewId"];

            
            if (application != null) {

                securityAuthorityName = application.SecurityAuthorityGetNameById (securityAuthorityId);

                workQueueView = application.WorkQueueViewGet (workQueueViewId); 

            }

            return;

        }

        public override Boolean Save () {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();

            sqlStatement.Append ("EXEC dbo.WorkQueueGetWorkUserView_InsertUpdate ");

            sqlStatement.Append (workQueueId.ToString () + ", ");

            sqlStatement.Append (securityAuthorityId.ToString () + ", ");

            sqlStatement.Append ("'" + userAccountId.ToString () + "', ");

            sqlStatement.Append ("'" + userAccountName.Replace ("'", "''") + "', ");

            sqlStatement.Append ("'" + userDisplayName.Replace ("'", "''") + "', ");

            sqlStatement.Append (workQueueViewId.ToString () + ", ");

            sqlStatement.Append ("'" + ModifiedAccountInfo.SecurityAuthorityNameSql + "', '" + ModifiedAccountInfo.UserAccountIdSql + "', '" + ModifiedAccountInfo.UserAccountNameSql + "'");

            success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());

            return success;


        }

        public Boolean Delete () {

            Boolean success = false;		// initially set to false;

            StringBuilder sqlStatement = new StringBuilder ();


            sqlStatement.Append ("DELETE FROM dbo.WorkQueueGetWorkUserView WHERE WorkQueueId = " + workQueueId.ToString ());

            sqlStatement.Append ("  AND SecurityAuthorityId = " + securityAuthorityId.ToString ());

            sqlStatement.Append ("  AND UserAccountId = '" + userAccountId.ToString () + "'");

            success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString ());


            return success;

        }

        #endregion


        #region Public Methods

        public Boolean IsEqual (WorkQueueGetWorkUserView compareMembership) {

            Boolean isEqual = true;

            isEqual = isEqual && (this.workQueueId == compareMembership.WorkQueueId);

            isEqual = isEqual && (this.securityAuthorityId != compareMembership.SecurityAuthorityId);

            isEqual = isEqual && (this.userAccountId != compareMembership.UserAccountId);

            isEqual = isEqual && (this.userDisplayName != compareMembership.UserDisplayName);

            isEqual = isEqual && (this.workQueueViewId != compareMembership.workQueueViewId);

            return isEqual;

        }

        #endregion

    }

}
