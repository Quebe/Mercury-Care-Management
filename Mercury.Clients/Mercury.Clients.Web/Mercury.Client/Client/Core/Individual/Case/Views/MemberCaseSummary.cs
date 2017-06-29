using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case.Views {
    
    [Serializable]
    public class MemberCaseSummary : CoreObject {

        #region Private Properties

        private Int64 memberId;

        private String referenceNumber = String.Empty;

        private Server.Application.CaseItemStatus status = Server.Application.CaseItemStatus.UnderDevelopment;


        private Int64 assignedToWorkTeamId;

        private DateTime? assignedToWorkTeamDate;


        private Int64 assignedToSecurityAuthorityId;

        private String assignedToUserAccountId;

        private String assignedToUserAccountName;

        private String assignedToUserDisplayName;

        private DateTime? assignedToDate;


        private Int64 lockedBySecurityAuthorityId;

        private String lockedByUserAccountId;

        private String lockedByUserAccountName;

        private String lockedByUserDisplayName;

        private DateTime? lockedByDate;


        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberId { get { return memberId; } set { memberId = value; } }

        public String ReferenceNumber { get { return referenceNumber; } }

        public Server.Application.CaseItemStatus Status { get { return status; } set { status = value; } }


        public Int64 AssignedToWorkTeamId { get { return assignedToWorkTeamId; } set { assignedToWorkTeamId = value; } }

        public DateTime? AssignedToWorkTeamDate { get { return assignedToWorkTeamDate; } set { assignedToWorkTeamDate = value; } }


        public Int64 AssignedToSecurityAuthorityId { get { return assignedToSecurityAuthorityId; } set { assignedToSecurityAuthorityId = value; } }

        public String AssignedToUserAccountId { get { return assignedToUserAccountId; } set { assignedToUserAccountId = value; } }

        public String AssignedToUserAccountName { get { return assignedToUserAccountName; } set { assignedToUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String AssignedToUserDisplayName { get { return assignedToUserDisplayName; } set { assignedToUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? AssignedToDate { get { return assignedToDate; } set { assignedToDate = value; } }

        public Boolean HasWorkTeamAssignment { get { return (assignedToWorkTeamId != 0); } }

        public Boolean HasAssignment { get { return (assignedToSecurityAuthorityId != 0); } }

        public Boolean AssignedToThisSession {

            get {

                if (assignedToSecurityAuthorityId == 0) { return false; }

                if ((assignedToSecurityAuthorityId == application.Session.SecurityAuthorityId)

                    && (assignedToUserAccountId == application.Session.UserAccountId)) {

                    return true;

                }

                return false;

            }

        }

        public Boolean AssignedToThisSessionTeam {

            get {

                if (assignedToWorkTeamId == 0) { return false; }

                foreach (Client.Core.Work.WorkTeam currentTeam in application.WorkTeamsForSession (true)) {

                    if (currentTeam.Id == assignedToWorkTeamId) { return true; }

                }

                return false;

            }

        }

        public Boolean AssignedToThisSessionTeamManager {

            get {

                if (assignedToWorkTeamId == 0) { return false; }

                foreach (Client.Core.Work.WorkTeam currentTeam in application.WorkTeamsForSession (true)) {

                    if (currentTeam.Id == assignedToWorkTeamId) {

                        return (currentTeam.MembershipGet (application.Session.SecurityAuthorityId, application.Session.UserAccountId).WorkTeamRole == Server.Application.WorkTeamRole.Manager);

                    }

                }

                return false;

            }

        }

        public Int64 LockedBySecurityAuthorityId { get { return lockedBySecurityAuthorityId; } set { lockedBySecurityAuthorityId = value; } }

        public String LockedByUserAccountId { get { return lockedByUserAccountId; } set { lockedByUserAccountId = value; } }

        public String LockedByUserAccountName { get { return lockedByUserAccountName; } set { lockedByUserAccountName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public String LockedByUserDisplayName { get { return lockedByUserDisplayName; } set { lockedByUserDisplayName = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Name); } }

        public DateTime? LockedByDate { get { return lockedByDate; } set { lockedByDate = value; } }

        public Boolean LockedByThisSession {

            get {

                if (lockedBySecurityAuthorityId == 0) { return false; }

                if ((lockedBySecurityAuthorityId == application.Session.SecurityAuthorityId)

                    && (lockedByUserAccountId == application.Session.UserAccountId)) {

                    return true;

                }

                return false;

            }

        }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public String EffectiveDateDescription { get { return ((status != Server.Application.CaseItemStatus.UnderDevelopment) ? effectiveDate.ToString ("MM/dd/yyyy") : String.Empty); } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String TerminationDateDescription { get { return ((status != Server.Application.CaseItemStatus.UnderDevelopment) ? ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")) : String.Empty); } }


        public String StatusDescription { get { return Server.CommonFunctions.EnumerationToString (status); } }

        public Client.Core.Member.Member Member { get { return application.MemberGet (memberId, true); } }

        public Client.Core.Work.WorkTeam AssignedToWorkTeam { get { return application.WorkTeamGet (assignedToWorkTeamId, true); } }

        #endregion


        #region Constructors

        public MemberCaseSummary (Application applicationReference, Server.Application.MemberCaseSummary serverObject) {

            BaseConstructor (applicationReference, serverObject);


            AssignedToDate = serverObject.AssignedToDate;

            AssignedToSecurityAuthorityId = serverObject.AssignedToSecurityAuthorityId;

            AssignedToUserAccountId = serverObject.AssignedToUserAccountId;

            AssignedToUserAccountName = serverObject.AssignedToUserAccountName;
            
            AssignedToUserDisplayName = serverObject.AssignedToUserDisplayName;

            AssignedToWorkTeamDate = serverObject.AssignedToWorkTeamDate;

            AssignedToWorkTeamId = serverObject.AssignedToWorkTeamId;

            CreateAccountInfo = serverObject.CreateAccountInfo;

            Description = serverObject.Description;

            EffectiveDate = serverObject.EffectiveDate;

            Id = serverObject.Id;

            LockedByDate = serverObject.LockedByDate;

            LockedBySecurityAuthorityId = serverObject.LockedBySecurityAuthorityId;

            LockedByUserAccountId = serverObject.LockedByUserAccountId;

            LockedByUserAccountName = serverObject.LockedByUserAccountName;

            LockedByUserDisplayName = serverObject.LockedByUserDisplayName;

            MemberId = serverObject.MemberId;

            ModifiedAccountInfo = serverObject.ModifiedAccountInfo;

            Name = serverObject.Name;

            referenceNumber = serverObject.ReferenceNumber;

            Status = serverObject.Status;

            TerminationDate = serverObject.TerminationDate;

            return;

        }

        #endregion 

    }

}
