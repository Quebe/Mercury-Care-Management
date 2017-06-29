using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Text;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [XmlRoot ("MemberEnrollmentTplCob")]
    [DataContract (Name = "MemberEnrollmentTplCob")]
    public class MemberEnrollmentTplCob : CoreObject {

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
        [DataMember (Name = "InsurerId")]
        private Int64 insurerId = 0;

        [XmlAttribute]
        [DataMember (Name = "ProgramId")]
        private Int64 programId = 0;

        [XmlAttribute]
        [DataMember (Name = "BenefitPlanId")]
        private Int64 benefitPlanId = 0;

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

        private Insurer.Insurer insurer = null;

        private Insurer.Program program = null;

        private Insurer.BenefitPlan benefitPlan = null;


        private MemberEnrollmentTplCob previousEnrollmentTplCob = null;

        #endregion


        #region Public Properties
        
        public Int64 MemberId { get { return memberId; } }

        public Int64 SponsorId { get { return sponsorId; } }

        public Int64 SubscriberId { get { return subscriberId; } }

        public Int64 InsurerId { get { return insurerId; } }

        public Int64 ProgramId { get { return programId; } }

        public Int64 BenefitPlanId { get { return benefitPlanId; } }

        public String ProgramMemberId { get { return programMemberId; } set { programMemberId = CommonFunctions.SetValueMaxLength (value, Data.DataTypeConstants.UniqueId); } }

        public DateTime EffectiveDate { get { return effectiveDate; } }

        public DateTime TerminationDate { get { return terminationDate; } }


        public Member Member {

            get {

                if (member != null) { return member; }

                if (base.application == null) { member = new Member (base.application); return member; }

                if (memberId == 0) { member = new Member (base.application); return member; }

                member = new Member (base.application, memberId);

                return member;

            }

        }

        public Sponsor.Sponsor Sponsor {

            get {

                if (sponsor != null) { return sponsor; }

                if (base.application == null) { sponsor = new Sponsor.Sponsor (base.application); return sponsor; }

                if (sponsorId == 0) { sponsor = new Sponsor.Sponsor (base.application); return sponsor; }

                sponsor = new Sponsor.Sponsor (base.application, sponsorId);

                return sponsor;

            }

        }

        public Member Subscriber {

            get {

                if (subscriber != null) { return subscriber; }

                if (base.application == null) { subscriber = new Member (base.application); return subscriber; }

                if (subscriberId == 0) { subscriber = new Member (base.application); return subscriber; }

                subscriber = new Member (base.application, subscriberId);

                return subscriber;

            }

        }

        public Insurer.Insurer Insurer {

            get {

                if (insurer != null) { return insurer; }

                if (base.application == null) { insurer = new Insurer.Insurer (base.application); return insurer; }

                if (insurerId == 0) { insurer = new Insurer.Insurer (base.application); return insurer; }

                insurer = new Insurer.Insurer (base.application, insurerId);

                return insurer;

            }

        }

        public Insurer.Program Program {

            get {

                if (program != null) { return program; }

                if (base.application == null) { program = new Insurer.Program (base.application); return program; }

                if (programId == 0) { program = new Insurer.Program (base.application); return program; }

                program = new Insurer.Program (base.application, programId);

                return program;

            }

        }

        public Insurer.BenefitPlan BenefitPlan {

            get {

                if (benefitPlan != null) { return benefitPlan; }

                if (base.application == null) { benefitPlan = new Insurer.BenefitPlan (base.application, benefitPlanId); }

                return benefitPlan;

            }

        }



        public Boolean HasPreviousEnrollmentTplCob { get { return (PreviousEnrollmentTplCob != null); } }

        public MemberEnrollmentTplCob PreviousEnrollmentTplCob {

            get {

                if (previousEnrollmentTplCob != null) { return previousEnrollmentTplCob; }

                List<MemberEnrollmentTplCob> memberEnrollmentTplCobs = base.application.MemberEnrollmentTplCobsGet (memberId);

                foreach (MemberEnrollmentTplCob currentEnrollmentTplCob in memberEnrollmentTplCobs) {

                    if (currentEnrollmentTplCob.Id != Id) {

                        if (currentEnrollmentTplCob.TerminationDate < effectiveDate) {

                            if (previousEnrollmentTplCob == null) { previousEnrollmentTplCob = currentEnrollmentTplCob; }

                            else {

                                if (currentEnrollmentTplCob.TerminationDate > previousEnrollmentTplCob.TerminationDate) {

                                    previousEnrollmentTplCob = currentEnrollmentTplCob;

                                }

                            }

                        }

                    }

                }

                return previousEnrollmentTplCob;

            }

        }

        #endregion


        #region Constructors

        public MemberEnrollmentTplCob (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberEnrollmentTplCob (Application applicationReference, Int64 forEnrollmentTplCobId) {

            BaseConstructor (applicationReference);

            if (!Load (forEnrollmentTplCobId)) {

                throw new ApplicationException ("Unable to load EnrollmentTplCob from the database for " + forEnrollmentTplCobId.ToString () + ".");

            }

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableEnrollmentTplCob;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.EnrollmentTplCob_Select " + forId.ToString ());

            tableEnrollmentTplCob = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableEnrollmentTplCob.Rows.Count == 1) {

                MapDataFields (tableEnrollmentTplCob.Rows[0]);

                return true;

            }

            else {

                return false;

            }

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            memberId = IdFromSql (currentRow, "MemberId");

            sponsorId = IdFromSql (currentRow, "SponsorId");

            subscriberId = IdFromSql (currentRow, "SubscriberId");

            insurerId = IdFromSql (currentRow, "InsurerId");

            programId = IdFromSql (currentRow, "ProgramId");

            benefitPlanId = IdFromSql (currentRow, "BenefitPlanId");

            programMemberId = (String) currentRow["ProgramMemberId"];


            effectiveDate = (DateTime) currentRow["EffectiveDate"];

            terminationDate = (DateTime) currentRow["TerminationDate"];

            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Public Methods - Data Bindings

        public override Dictionary<String, String> DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = new Dictionary<String, String> ();

                bindingContexts.Add ("Id", "Id|EnrollmentTplCob");

                bindingContexts.Add ("MemberId", "Id|Member");

                bindingContexts.Add ("SponsorId", "Id|Sponsor");

                bindingContexts.Add ("SubscriberId", "Id|Member");

                bindingContexts.Add ("InsurerId", "Id|Insurer");

                bindingContexts.Add ("ProgramId", "Id|Program");

                bindingContexts.Add ("ProgramMemberId", "String");

                bindingContexts.Add ("EffectiveDate", "DateTime");

                bindingContexts.Add ("TerminationDate", "DateTime");


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

                case "InsurerId": dataValue = insurerId.ToString (); break;

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


                default: dataValue = "!Error"; break;

            }

            return dataValue;

        }

        #endregion

    }

}
