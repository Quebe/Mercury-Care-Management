using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Member {

    [Serializable]
    public class MemberEnrollment : CoreObject {

        #region Private Properties

        private Int64 memberId = 0;

        private Int64 sponsorId = 0;

        private Int64 subscriberId = 0;

        private Int64 programId = 0;

        private String programMemberId = String.Empty;

        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);

        #endregion 
        

        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public Int64 SponsorId { get { return sponsorId; } }

        public Int64 SubscriberId { get { return subscriberId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramMemberId { get { return programMemberId; } set { programMemberId = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }

        #endregion 


        #region Public Object Properties
        
        public Member Member { get { return application.MemberGet (memberId, true); } }

        public String MemberName { get { return ((Member != null) ? Member.Name : String.Empty); } }


        public Sponsor.Sponsor Sponsor { get { return application.SponsorGet (sponsorId, true); } }

        public String SponsorName { get { return ((Sponsor != null) ? Sponsor.Name : String.Empty); } }


        public Member Subscriber { get { return application.MemberGet (subscriberId, true); } }

        public String SubscriberName { get { return ((Subscriber != null) ? Subscriber.Name : String.Empty); } }


        public Insurer.Insurer Insurer { get { return ((Program == null) ? null : Program.Insurer); } }

        public String InsurerName { get { return ((Insurer != null) ? Insurer.Name : String.Empty); } }


        public Insurer.Program Program { get { return application.ProgramGet (programId, true); } }

        public String ProgramName { get { return ((Program != null) ? Program.Name : String.Empty); } }

        #endregion 
        

        #region Constructors

        public MemberEnrollment (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberEnrollment (Application applicationReference, Server.Application.MemberEnrollment serverMemberEnrollment) {

            BaseConstructor (applicationReference, serverMemberEnrollment);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.MemberEnrollment serverMemberEnrollment) {

            base.BaseConstructor (applicationReference, serverMemberEnrollment);


            memberId = serverMemberEnrollment.MemberId;

            sponsorId = serverMemberEnrollment.SponsorId;

            subscriberId = serverMemberEnrollment.SubscriberId;

            programId = serverMemberEnrollment.ProgramId;

            programMemberId = serverMemberEnrollment.ProgramMemberId;

            effectiveDate = serverMemberEnrollment.EffectiveDate;

            terminationDate = serverMemberEnrollment.TerminationDate;


            return;

        }

        #endregion


        #region Enrollment Objects

        public List<MemberEnrollmentCoverage> Coverages { get { return application.MemberEnrollmentCoveragesGet (id, true); } }

        public List<MemberEnrollmentPcp> Pcps { get { return application.MemberEnrollmentPcpsGet (id, true); } }


        public Boolean HasCurrentCoverage { get { return (CurrentCoverage != null); } }

        public MemberEnrollmentCoverage CurrentCoverage {

            get {

                foreach (MemberEnrollmentCoverage currentCoverage in Coverages) {

                    if ((DateTime.Today >= currentCoverage.EffectiveDate) && (DateTime.Today <= currentCoverage.TerminationDate)) {

                        return currentCoverage;

                    }

                }

                return null;

            }

        }

        public MemberEnrollmentCoverage MostRecentCoverage {

            get {

                MemberEnrollmentCoverage mostRecent = null;

                foreach (MemberEnrollmentCoverage currentEnrollmentCoverage in Coverages) {

                    if (mostRecent == null) { mostRecent = currentEnrollmentCoverage; }

                    else if (mostRecent.TerminationDate < currentEnrollmentCoverage.TerminationDate) {

                        mostRecent = currentEnrollmentCoverage;

                    }

                }

                return mostRecent;

            }

        }


        public Boolean HasCurrentPcp { get { return (CurrentPcp != null); } }

        public MemberEnrollmentPcp CurrentPcp {

            get {

                foreach (MemberEnrollmentPcp currentPcp in Pcps) {

                    if ((DateTime.Today >= currentPcp.EffectiveDate) && (DateTime.Today <= currentPcp.TerminationDate)) {

                        return currentPcp;

                    }

                }

                return null;

            }

        }

        public MemberEnrollmentPcp MostRecentPcp {

            get {

                MemberEnrollmentPcp mostRecent = null;

                foreach (MemberEnrollmentPcp currentMemberEnrollmentPcp in Pcps) {

                    if (mostRecent == null) { mostRecent = currentMemberEnrollmentPcp; }

                    else if (mostRecent.TerminationDate < currentMemberEnrollmentPcp.TerminationDate) {

                        mostRecent = currentMemberEnrollmentPcp;

                    }

                }

                return mostRecent;

            }

        }

        #endregion

    }

}
