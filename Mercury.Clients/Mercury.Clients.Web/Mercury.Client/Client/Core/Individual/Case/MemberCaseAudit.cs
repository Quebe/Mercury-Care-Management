using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseAudit : CoreObject {


        #region Private Properties

        private Int64 memberCaseAuditHistoryId = 0;

        private Int64 memberCaseId = 0;

        private String auditObjectType = String.Empty;

        private Int64 auditObjectId = 0;

        private String sourceObjectType = String.Empty;

        private Int64 sourceObjectId = 0;

        private String userDisplayName = String.Empty;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseAuditHistoryId { get { return memberCaseAuditHistoryId; } set { memberCaseAuditHistoryId = value; } }

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public String AuditObjectType { get { return auditObjectType; } set { auditObjectType = value; } }

        public Int64 AuditObjectId { get { return auditObjectId; } set { auditObjectId = value; } }

        public String SourceObjectType { get { return sourceObjectType; } set { sourceObjectType = value; } }

        public Int64 SourceObjectId { get { return sourceObjectId; } set { sourceObjectId = value; } }

        public String UserDisplayName { get { return userDisplayName; } set { userDisplayName = value; } }

        #endregion


        #region Public Properties

        #endregion


        #region Constructors

        public MemberCaseAudit (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseAudit (Application applicationReference, Mercury.Server.Application.MemberCaseAudit serverObject) {

            BaseConstructor (applicationReference, serverObject);

            memberCaseId = serverObject.MemberCaseId;

            auditObjectType = serverObject.AuditObjectType;

            auditObjectId = serverObject.AuditObjectId;

            sourceObjectType = serverObject.SourceObjectType;

            sourceObjectId = serverObject.SourceObjectId;

            userDisplayName = serverObject.UserDisplayName;

            description = serverObject.Description;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCaseAudit serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);

            serverObject.Id = memberCaseAuditHistoryId;

            serverObject.MemberCaseId = memberCaseId;

            serverObject.AuditObjectType = auditObjectType;

            serverObject.AuditObjectId = auditObjectId;

            serverObject.SourceObjectType = sourceObjectType;

            serverObject.SourceObjectId = sourceObjectId;

            serverObject.UserDisplayName = userDisplayName;

            serverObject.Description = description;

            return;

        }

        public override object ToServerObject () {

            Server.Application.MemberCaseAudit serverObject = new Server.Application.MemberCaseAudit ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        #endregion


    }

}