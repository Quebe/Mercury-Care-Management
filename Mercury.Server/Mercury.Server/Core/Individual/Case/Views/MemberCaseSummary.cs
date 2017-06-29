using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case.Views {

    [Serializable]
    [DataContract (Name = "MemberCaseSummary")]
    public class MemberCaseSummary : CoreExtensibleObject {

        #region Private Properties

        [DataMember (Name = "MemberId")]
        private Int64 memberId;

        [DataMember (Name = "ReferenceNumber")]
        private String referenceNumber = String.Empty;

        [DataMember (Name = "Status")]
        private Enumerations.CaseItemStatus status = Enumerations.CaseItemStatus.NotSpecified;


        [DataMember (Name = "AssignedToWorkTeamId")]
        private Int64 assignedToWorkTeamId;

        [DataMember (Name = "AssignedToWorkTeamDate")]
        private DateTime? assignedToWorkTeamDate;

        [DataMember (Name = "AssignedToSecurityAuthorityId")]
        private Int64 assignedToSecurityAuthorityId;

        [DataMember (Name = "AssignedToUserAccountId")]
        private String assignedToUserAccountId;

        [DataMember (Name = "AssignedToUserAccountName")]
        private String assignedToUserAccountName;

        [DataMember (Name = "AssignedToUserDisplayName")]
        private String assignedToUserDisplayName;

        [DataMember (Name = "AssignedToDate")]
        private DateTime? assignedToDate;



        [DataMember (Name = "LockedBySecurityAuthorityId")]
        private Int64 lockedBySecurityAuthorityId;

        [DataMember (Name = "LockedByUserAccountId")]
        private String lockedByUserAccountId;

        [DataMember (Name = "LockedByUserAccountName")]
        private String lockedByUserAccountName;

        [DataMember (Name = "LockedByUserDisplayName")]
        private String lockedByUserDisplayName;

        [DataMember (Name = "LockedByDate")]
        private DateTime? lockedByDate;



        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String ReferenceNumber { get { return referenceNumber; } }

        public Enumerations.CaseItemStatus Status { get { return status; } }


        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public DateTime? AssignedToWorkTeamDate { get { return assignedToWorkTeamDate; } set { assignedToWorkTeamDate = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }


        public Int64 LockedBySecurityAuthorityId { get { return lockedBySecurityAuthorityId; } set { lockedBySecurityAuthorityId = value; } }

        public String LockedByUserAccountId { get { return lockedByUserAccountId; } set { lockedByUserAccountId = value; } }

        public String LockedByUserAccountName { get { return lockedByUserAccountName; } set { lockedByUserAccountName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public String LockedByUserDisplayName { get { return lockedByUserDisplayName; } set { lockedByUserDisplayName = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.Name); } }

        public DateTime? LockedByDate { get { return lockedByDate; } set { lockedByDate = value; } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        #endregion


        #region Constructors

        public MemberCaseSummary (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            id = base.IdFromSql (currentRow, "MemberCaseId");

            MemberId = base.IdFromSql (currentRow, "MemberId");

            referenceNumber = (String)currentRow["ReferenceNumber"];

            status = (Enumerations.CaseItemStatus)Convert.ToInt32 (currentRow["Status"]);


            assignedToWorkTeamId = base.IdFromSql (currentRow, "AssignedToWorkTeamId");

            assignedToWorkTeamDate = base.DateTimeFromSql (currentRow, "AssignedToWorkTeamDate");


            assignedToSecurityAuthorityId = base.IdFromSql (currentRow, "AssignedToSecurityAuthorityId");

            assignedToUserAccountId = (String)currentRow["AssignedToUserAccountId"];

            assignedToUserAccountName = (String)currentRow["AssignedToUserAccountName"];

            assignedToUserDisplayName = (String)currentRow["AssignedToUserDisplayName"];

            assignedToDate = base.DateTimeFromSql (currentRow, "AssignedToDate");


            lockedBySecurityAuthorityId = base.IdFromSql (currentRow, "LockedBySecurityAuthorityId");

            lockedByUserAccountId = (String)currentRow["LockedByUserAccountId"];

            lockedByUserAccountName = (String)currentRow["LockedByUserAccountName"];

            lockedByUserDisplayName = (String)currentRow["LockedByUserDisplayName"];

            lockedByDate = base.DateTimeFromSql (currentRow, "LockedByDate");


            effectiveDate = (DateTime)currentRow["EffectiveDate"];

            terminationDate = (DateTime)currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion

    }

}
