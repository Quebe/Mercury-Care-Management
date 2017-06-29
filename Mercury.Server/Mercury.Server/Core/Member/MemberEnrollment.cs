using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [XmlRoot ("MemberEnrollment")]
    [DataContract (Name = "MemberEnrollment")]
    public class MemberEnrollment : CoreObject {
            
        #region Private Properties

        [XmlAttribute]
        [DataMember (Name = "MemberId")]
        private Int64 memberId = 0;

        [XmlAttribute]
        [DataMember (Name = "SponsorId")]
        private Int64 sponsorId = 0;

        [XmlAttribute]
        [DataMember (Name = "SubscriberId")]
        private Int64 subscriberId = 0;
        
        [XmlAttribute]
        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [XmlAttribute]
        [DataMember (Name = "ProgramMemberId")]
        private String programMemberId = String.Empty;

        [XmlAttribute]
        [DataMember (Name = "EffectiveDate")]
        private DateTime effectiveDate = new DateTime (1900, 1, 1);

        [XmlAttribute]
        [DataMember (Name = "TerminationDate")]
        private DateTime terminationDate = new DateTime (9999, 12, 31);


        private Member member = null;

        private Sponsor.Sponsor sponsor = null;

        private Member subscriber = null;
        
        private Insurer.Program program = null;


        private List<MemberEnrollmentCoverage> enrollmentCoverages = null;

        private List<MemberEnrollmentPcp> pcpAssignments = null;

        private MemberEnrollment previousEnrollment = null;

        #endregion


        #region Public Properties

        public Int64 MemberId { get { return memberId; } }

        public Int64 SponsorId { get { return sponsorId; } }

        public Int64 SubscriberId { get { return subscriberId; } }

        public Int64 ProgramId { get { return programId; } }

        public String ProgramMemberId { get { return programMemberId; } set { programMemberId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public Member Member {

            get {

                if (member != null) { return member; }

                if (application == null) { return null; }

                member = Application.MemberGet (memberId);

                return member;

            }

        }

        public Sponsor.Sponsor Sponsor {

            get {

                if (sponsor != null) { return sponsor; }

                if (application == null) { return null; }

                sponsor = Application.SponsorGet (sponsorId);

                return sponsor;

            }

        }

        public Member Subscriber {

            get {

                if (subscriber != null) { return subscriber; }

                if (application == null) { return null; }

                subscriber = Application.MemberGet (subscriberId);

                return subscriber;

            }

        }

        public Insurer.Insurer Insurer { get { return ((Program == null) ? null : Program.Insurer); } }

        public Insurer.Program Program {

            get {

                if (program != null) { return program; }

                if (application == null) { return null; }

                program = application.ProgramGet (programId);

                return program;

            }

        }


        public List<MemberEnrollmentCoverage> Coverages {

            get {

                if (enrollmentCoverages != null) { return enrollmentCoverages; }

                if (base.application == null) { return null; }

                if (Id == 0) { enrollmentCoverages = new List<MemberEnrollmentCoverage> (); return enrollmentCoverages; }

                enrollmentCoverages = base.application.MemberEnrollmentCoveragesGet (Id);

                return enrollmentCoverages;

            }

        }

        public List<MemberEnrollmentPcp> Pcps {

            get {

                if (pcpAssignments != null) { return pcpAssignments; }

                if (base.application == null) { return null; }

                if (Id == 0) { pcpAssignments = new List<MemberEnrollmentPcp> (); return pcpAssignments; }

                pcpAssignments = base.application.MemberEnrollmentPcpsGet(Id);

                return pcpAssignments;

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


        public Boolean HasPreviousEnrollment { get { return (PreviousEnrollment != null); } }

        public MemberEnrollment PreviousEnrollment {

            get {

                if (previousEnrollment != null) { return previousEnrollment; }

                List<MemberEnrollment> memberEnrollments = base.application.MemberEnrollmentsGet (memberId);

                foreach (MemberEnrollment currentEnrollment in memberEnrollments) {

                    if (currentEnrollment.Id != Id) {

                        if (currentEnrollment.TerminationDate < effectiveDate) {

                            if (previousEnrollment == null) { previousEnrollment = currentEnrollment; }

                            else {

                                if (currentEnrollment.TerminationDate > previousEnrollment.TerminationDate) {

                                    previousEnrollment = currentEnrollment;

                                }

                            }

                        }

                    }

                }

                return previousEnrollment;

            }

        }

        #endregion


        #region Constructors 

        public MemberEnrollment (Application applicationReference) {

            BaseConstructor (applicationReference);
        
            return; 
        
        }

        public MemberEnrollment (Application applicationReference, Int64 forMemberEnrollmentId) {

            BaseConstructor (applicationReference, forMemberEnrollmentId);

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {
            
            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnrollment;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.MemberEnrollment_Select " + forId.ToString ());

            tableEnrollment = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnrollment.Rows.Count == 1) {

                MapDataFields (tableEnrollment.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberId     = (Int64) currentRow["MemberId"];

            sponsorId    = (Int64) currentRow["SponsorId"];

            subscriberId = (Int64) currentRow["SubscriberId"];
                        
            programId = (Int64) currentRow["ProgramId"];

            programMemberId = (String) currentRow["ProgramMemberId"];

            
            effectiveDate = (DateTime) currentRow ["EffectiveDate"];

            terminationDate = (DateTime) currentRow ["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|MemberEnrollment");

                bindingContexts.Add ("MemberId", "Id|Member");

                bindingContexts.Add ("SponsorId", "Id|Sponsor");

                bindingContexts.Add ("SubscriberId", "Id|Member");
                
                bindingContexts.Add ("ProgramId", "Id|Program");

                bindingContexts.Add ("ProgramMemberId", "String");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");

                bindingContexts.Add ("EnrollmentCoverages", "Collection|MemberEnrollmentCoverage");

                bindingContexts.Add ("EnrollmentPcps", "Collection|MemberEnrollmentPcp");



                Dictionary<String, String> memberBindingContexts = new Member (base.application).DataBindingContexts;

                foreach (String currentKey in memberBindingContexts.Keys) {

                    bindingContexts.Add ("Member." + currentKey, memberBindingContexts[currentKey]);

                }

                foreach (String currentKey in memberBindingContexts.Keys) {

                    bindingContexts.Add ("Subscriber." + currentKey, memberBindingContexts[currentKey]);

                }
                
                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "Id": dataValue = Id.ToString (); break;

                case "MemberId": dataValue = memberId.ToString (); break;

                case "SponsorId": dataValue = sponsorId.ToString (); break;

                case "SubscriberId": dataValue = subscriberId.ToString (); break;


                // BACKWARDS COMPATIBILITY ONLY
                case "InsurerId": dataValue = ((Insurer != null) ? Insurer.Id.ToString () : "0"); break;


                case "ProgramId": dataValue = programId.ToString (); break;

                case "ProgramMemberId": dataValue = programMemberId; break;                       
            
                case "EffectiveDate": dataValue = effectiveDate.ToString ("MM/dd/yyyy"); break;

                case "TerminationDate": dataValue = terminationDate.ToString ("MM/dd/yyyy"); break;

                case "Member":

                    #region Member 

                    if (Member == null) { return "!Error"; }

                    dataValue = Member.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                case "Subscriber":

                    #region Subscriber

                    if (Subscriber == null) { return "!Error"; }

                    dataValue = Subscriber.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", ""));

                    #endregion

                    break;

                case "EnrollmentCoverages": 

                    dataValue = "MemberEnrollmentCoverage";

                    foreach (MemberEnrollmentCoverage currentEnrollmentCoverage in base.application.MemberEnrollmentCoveragesGet (Id)) {

                        dataValue = dataValue + "|" + currentEnrollmentCoverage.Id;

                    }

                    break;


                case "PcpAssignments": // BACKWARDS COMPATIBILITY

                case "EnrollmentPcps":

                    dataValue = "MemberEnrollmentPcp";

                    foreach (MemberEnrollmentPcp currentPcpAssignment in base.application.MemberEnrollmentPcpsGet (Id)) {

                        dataValue = dataValue + "|" + currentPcpAssignment.Id;

                    }

                    break;

                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion

    }

}
