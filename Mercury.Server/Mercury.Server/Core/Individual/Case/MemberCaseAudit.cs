using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [Serializable]
    [DataContract (Name = "MemberCaseAudit")]
    public class MemberCaseAudit : Core.CoreObject {


        #region Private Properties

        [DataMember (Name = "MemberCaseAuditHistoryId")]
        private Int64 memberCaseAuditHistoryId;

        [DataMember (Name = "MemberCaseId")]
        private Int64 memberCaseId;

        [DataMember (Name = "AuditObjectType")]
        private String auditObjectType;

        [DataMember (Name = "AuditObjectId")]
        private Int64 auditObjectId;

        [DataMember (Name = "SourceObjectType")]
        private String sourceObjectType;

        [DataMember (Name = "SourceObjectId")]
        private Int64 sourceObjectId;

        [DataMember (Name = "UserDisplayName")]
        private String userDisplayName;

        // DESCRIPTION, DATE, AND CREATE INFORMATION WITH CORE OBJECT

        #endregion


        #region Public Properties

        public Int64 MemberCaseAuditHistoryId { get { return memberCaseAuditHistoryId; } set { memberCaseAuditHistoryId = value; } }

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public String AuditObjectType { get { return auditObjectType; } set { auditObjectType = value; } }

        public Int64 AuditObjectId { get { return auditObjectId; } set { auditObjectId = value; } }

        public String SourceObjectType { get { return sourceObjectType; } set { sourceObjectType = value; } }

        public Int64 SourceObjectId { get { return sourceObjectId; } set { sourceObjectId = value; } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } }

        #endregion


        #region Constructors

        public MemberCaseAudit (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseAudit (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Validation Functions

        public override Dictionary<String, String> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> (); // DO NOT USE BASE VALIDATIO FOR ID/NAME


            return validationResponse;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseAuditHistoryId = base.IdFromSql (currentRow, "MemberCaseAuditHistoryId");

            MemberCaseId = base.IdFromSql (currentRow, "MemberCaseId");

            description = (String)currentRow["MemberCaseAuditDescription"];

            AuditObjectType = base.StringFromSql (currentRow, "AuditObjectType");

            AuditObjectId = base.IdFromSql (currentRow, "AuditObjectId");

            SourceObjectType = base.StringFromSql (currentRow, "SourceObjectType");

            SourceObjectId = base.IdFromSql (currentRow, "SourceObjectId");

            UserDisplayName = base.StringFromSql (currentRow, "UserDisplayName");

        }

        #endregion


    }

}

