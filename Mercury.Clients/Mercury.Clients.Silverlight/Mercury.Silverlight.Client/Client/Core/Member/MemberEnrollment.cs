using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Mercury.Client.Core.Member {

    public class MemberEnrollment : CoreObject {

        #region Private Properties

        private Int64 memberId = 0;

        private Int64 sponsorId = 0;

        private Int64 subscriberId = 0;

        private Int64 programId = 0;

        private String programMemberId = String.Empty;

        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private Member member = null;

        private Sponsor.Sponsor sponsor = null;

        private Member subscriber = null;

        private Insurer.Program program = null;


        private ObservableCollection<MemberEnrollmentCoverage> memberEnrollmentCoverages = null;

        private MemberEnrollmentCoverage currentEnrollmentCoverage = null;

        private MemberEnrollmentCoverage mostRecentEnrollmentCoverage = null;


        private ObservableCollection<MemberEnrollmentPcp> memberEnrollmentPcps = null;

        private MemberEnrollmentPcp currentEnrollmentPcp = null;

        private MemberEnrollmentPcp mostRecentEnrollmentPcp = null;

        #endregion 
        

        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public Int64 SponsorId { get { return sponsorId; } }

        public Int64 SubscriberId { get { return subscriberId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramMemberId { get { return programMemberId; } set { programMemberId = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }


        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }

        public String EffectiveDateDescription { get { return EffectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : TerminationDate.ToString ("MM/dd/yyyy")); } }

        #endregion 

        
        #region Public Object/Calculated Properties

        public Member Member {

            get {

                if ((member == null) && (!serverRequests.Contains ("Member"))) {

                    serverRequests.Add ("Member");

                    GlobalProgressBarShow ("Member");

                    Application.MemberGet (memberId, true, MemberGetCompleted);

                }

                return member;

            }

        }

        public Sponsor.Sponsor Sponsor {

            get {

                if ((sponsor == null) && (!serverRequests.Contains ("Sponsor"))) {

                    serverRequests.Add ("Sponsor");

                    GlobalProgressBarShow ("Sponsor");

                    Application.SponsorGet (sponsorId, true, SponsorGetCompleted);

                }

                return sponsor;

            }

        }

        public Member Subscriber {

            get {

                if ((subscriber == null) && (!serverRequests.Contains ("Subscriber"))) {

                    serverRequests.Add ("Subscriber");

                    GlobalProgressBarShow ("Subscriber");

                    Application.MemberGet (subscriberId, true, SubscriberGetCompleted);

                }

                return subscriber;

            }

        }

        public Insurer.Program Program {

            get {

                if ((program == null) && (!serverRequests.Contains ("Program"))) {

                    serverRequests.Add ("Program");

                    GlobalProgressBarShow ("Program");

                    Application.ProgramGet (programId, true, ProgramGetCompleted);

                }

                return program;

            }

        }


        public ObservableCollection<MemberEnrollmentCoverage> EnrollmentCoverages {

            get {

                if ((!serverRequests.Contains ("EnrollmentCoverages")) && (!loadedData.Contains ("EnrollmentCoverages"))) {

                    serverRequests.Add ("EnrollmentCoverages");

                    GlobalProgressBarShow ("EnrollmentCoverages");

                    Application.MemberEnrollmentCoveragesGet (id, true, MemberEnrollmentCoveragesGetCompleted);

                }

                return memberEnrollmentCoverages;

            }

        }

        public MemberEnrollmentCoverage CurrentEnrollmentCoverage {

            get {

                if ((!loadedData.Contains ("CurrentEnrollmentCoverage")) && (!serverRequests.Contains ("EnrollmentCoverages"))) {

                    serverRequests.Add ("EnrollmentCoverages");

                    GlobalProgressBarShow ("CurrentEnrollmentCoverage");

                    Application.MemberEnrollmentCoveragesGet (id, true, MemberEnrollmentCoveragesGetCompleted);

                }

                return currentEnrollmentCoverage;

            }

        }

        public MemberEnrollmentCoverage MostRecentEnrollmentCoverage {

            get {

                if ((!loadedData.Contains ("MostRecentEnrollmentCoverage")) && (!serverRequests.Contains ("EnrollmentCoverages"))) {

                    serverRequests.Add ("EnrollmentCoverages");

                    GlobalProgressBarShow ("MostRecentEnrollmentCoverage");

                    Application.MemberEnrollmentCoveragesGet (id, true, MemberEnrollmentCoveragesGetCompleted);

                }

                return currentEnrollmentCoverage;

            }

        }


        public ObservableCollection<MemberEnrollmentPcp> EnrollmentPcps {

            get {

                if ((!loadedData.Contains ("EnrollmentPcps")) && (!serverRequests.Contains ("EnrollmentPcps"))) {

                    serverRequests.Add ("EnrollmentPcps");

                    GlobalProgressBarShow ("EnrollmentPcps");

                    Application.MemberEnrollmentPcpsGet (id, true, MemberEnrollmentPcpsGetCompleted);

                }

                return memberEnrollmentPcps;

            }

        }

        public MemberEnrollmentPcp CurrentEnrollmentPcp {

            get {

                if ((!loadedData.Contains ("CurrentEnrollmentPcp")) && (!serverRequests.Contains ("EnrollmentPcps"))) {

                    serverRequests.Add ("EnrollmentPcps");

                    GlobalProgressBarShow ("CurrentEnrollmentPcp");

                    Application.MemberEnrollmentPcpsGet (id, true, MemberEnrollmentPcpsGetCompleted);

                }

                return currentEnrollmentPcp;

            }

        }

        public MemberEnrollmentPcp MostRecentEnrollmentPcp {

            get {

                if ((!loadedData.Contains ("MostRecentEnrollmentPcp")) && (!serverRequests.Contains ("EnrollmentPcps"))) {

                    serverRequests.Add ("EnrollmentPcps");

                    GlobalProgressBarShow ("MostRecentEnrollmentPcp");

                    Application.MemberEnrollmentPcpsGet (id, true, MemberEnrollmentPcpsGetCompleted);

                }

                return currentEnrollmentPcp;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void MemberGetCompleted (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            serverRequests.Remove ("Member");

            GlobalProgressBarHide ("Member");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                member = new Member (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Member");

            }

            return;

        }

        private void SponsorGetCompleted (Object sender, Server.Application.SponsorGetCompletedEventArgs e) {

            serverRequests.Remove ("Sponsor");

            GlobalProgressBarHide ("Sponsor");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                sponsor = new Sponsor.Sponsor (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Sponsor");

            }

            return;

        }

        private void SubscriberGetCompleted (Object sender, Server.Application.MemberGetCompletedEventArgs e) {

            serverRequests.Remove ("Subscriber");

            GlobalProgressBarHide ("Subscriber");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                subscriber = new Member (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Subscriber");

            }

            return;

        }

        private void ProgramGetCompleted (Object sender, Server.Application.ProgramGetCompletedEventArgs e) {

            serverRequests.Remove ("Program");

            GlobalProgressBarHide ("Program");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                program = new Insurer.Program (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Program");

            }

            return;

        }

        private void MemberEnrollmentCoveragesGetCompleted (Object sender, Server.Application.MemberEnrollmentCoveragesGetCompletedEventArgs e) {

            serverRequests.Remove ("EnrollmentCoverages");

            GlobalProgressBarHide ("MemberEnrollmentCoverages");

            GlobalProgressBarHide ("CurrentEnrollmentCoverage");

            GlobalProgressBarHide ("MostRecentEnrollmentCoverage");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                loadedData.Add ("EnrollmentCoverages");

                loadedData.Add ("MemberEnrollmentCoverages");

                loadedData.Add ("CurrentEnrollmentCoverage");

                loadedData.Add ("MostRecentEnrollmentCoverage");

                memberEnrollmentCoverages = Converters.ServerCollectionToClient.MemberEnrollmentCoverageCollection (Application, e.Result.Collection);

                foreach (MemberEnrollmentCoverage currentMemberEnrollmentCoverage in memberEnrollmentCoverages) {

                    // GET CURRENT ENROLLMENT COVERAGE 

                    if ((DateTime.Today >= currentMemberEnrollmentCoverage.EffectiveDate) && (DateTime.Today <= currentMemberEnrollmentCoverage.TerminationDate)) {

                        if (currentEnrollmentCoverage == null) {

                            currentEnrollmentCoverage = currentMemberEnrollmentCoverage;

                        }
                        
                    }

                    // GET MOST RECENT

                    if (mostRecentEnrollmentCoverage == null) { mostRecentEnrollmentCoverage = currentMemberEnrollmentCoverage; }

                    else if (mostRecentEnrollmentCoverage.TerminationDate < currentMemberEnrollmentCoverage.TerminationDate) {

                        mostRecentEnrollmentCoverage = currentMemberEnrollmentCoverage;

                    }

                }

                NotifyPropertyChanged ("EnrollmentCoverages");

                NotifyPropertyChanged ("CurrentEnrollmentCoverage");

                NotifyPropertyChanged ("MostRecentEnrollmentCoverage");

            }

            return;

        }

        private void MemberEnrollmentPcpsGetCompleted (Object sender, Server.Application.MemberEnrollmentPcpsGetCompletedEventArgs e) {

            serverRequests.Remove ("EnrollmentPcps");

            GlobalProgressBarHide ("MemberEnrollmentPcps");

            GlobalProgressBarHide ("CurrentEnrollmentPcp");

            GlobalProgressBarHide ("MostRecentEnrollmentPcp");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                loadedData.Add ("EnrollmentPcps");

                loadedData.Add ("MemberEnrollmentPcps");

                loadedData.Add ("CurrentEnrollmentPcp");

                loadedData.Add ("MostRecentEnrollmentPcp");

                memberEnrollmentPcps = Converters.ServerCollectionToClient.MemberEnrollmentPcpCollection (Application, e.Result.Collection);

                foreach (MemberEnrollmentPcp currentMemberEnrollmentPcp in memberEnrollmentPcps) {

                    // GET CURRENT ENROLLMENT COVERAGE 

                    if ((DateTime.Today >= currentMemberEnrollmentPcp.EffectiveDate) && (DateTime.Today <= currentMemberEnrollmentPcp.TerminationDate)) {

                        if (currentEnrollmentPcp == null) {

                            currentEnrollmentPcp = currentMemberEnrollmentPcp;

                        }

                    }

                    // GET MOST RECENT

                    if (mostRecentEnrollmentPcp == null) { mostRecentEnrollmentPcp = currentMemberEnrollmentPcp; }

                    else if (mostRecentEnrollmentPcp.TerminationDate < currentMemberEnrollmentPcp.TerminationDate) {

                        mostRecentEnrollmentPcp = currentMemberEnrollmentPcp;

                    }

                }

                NotifyPropertyChanged ("EnrollmentPcps");

                NotifyPropertyChanged ("CurrentEnrollmentPcp");

                NotifyPropertyChanged ("MostRecentEnrollmentPcp");

            }

            return;

        }

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


    }

}
