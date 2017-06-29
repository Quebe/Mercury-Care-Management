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

    public class Member : CoreObject {

        #region Private Properties

        private Int64 entityId;

        private DateTime birthDate;

        private DateTime? deathDate;

        private String gender;

        private Int64 ethnicityId;

        private Int64 citizenshipId;

        private Int64 languageId;

        private Int64 maritalStatusId;


        private Entity.Entity entity = null;

        private ObservableCollection<MemberEnrollment> memberEnrollments = null;

        private MemberEnrollment currentEnrollment = null;

        private ObservableCollection<MemberRelationship> memberRelationships = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        public override string Description { get { return Name + "[" + Gender + " | " + CurrentAge.ToString ().PadLeft (2, '0') + "]"; } }


        public Int64 EntityId { get { return entityId; } }

        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }

        public DateTime? DeathDate { get { return deathDate; } set { deathDate = value; } }

        public String Gender { get { return gender; } set { gender = Server.CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 CitizenshipId { get { return citizenshipId; } set { citizenshipId = value; } }

        public Int64 LanguageId { get { return languageId; } set { languageId = value; } }

        public Int64 MaritalStatusId { get { return maritalStatusId; } set { maritalStatusId = value; } }

        #endregion 
        

        #region Public Object/Calculated Properties

        public String BirthDateDescription { get { return birthDate.ToString ("MM/dd/yyyy"); } }

        public String DeathDateDescription { get { return ((deathDate.HasValue) ? deathDate.Value.ToString ("MM/dd/yyyy") : "< alive >"); } }

        public String GenderDescription {

            get {

                switch (gender) {

                    case "M": return "Male";

                    case "F": return "Female";

                    default: return "Unknown";

                }

            }

        }

        //public String EthnicityDescription { get { return application.CoreObjectGetNameById ("Ethnicity", ethnicityId, true); } }

        //public String LanguageDescription { get { return application.CoreObjectGetNameById ("Language", languageId, true); } }

        //public String CitizenshipDescription { get { return application.CoreObjectGetNameById ("Citizenship", citizenshipId, true); } }

        //public String MaritalStatusDescription { get { return application.CoreObjectGetNameById ("MaritalStatus", maritalStatusId, true); } }


        public Int32 CurrentAge {

            get {

                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Year - birthDate.Year;

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public Int32 CurrentAgeInMonths {

            get {

                Int32 currentAge = 0;

                DateTime birthDay;


                currentAge = DateTime.Today.Month - birthDate.Month;

                currentAge = currentAge + (12 * (DateTime.Today.Year - birthDate.Year));

                birthDay = new DateTime (DateTime.Today.Year, birthDate.Month, (((birthDate.Month == 2) && (birthDate.Day == 29)) ? 28 : birthDate.Day));

                if (DateTime.Today.CompareTo (birthDay) < 1) { currentAge = currentAge - 1; }

                return currentAge;

            }

        }

        public String CurrentAgeDescription { get { return ((CurrentAge >= 2) ? CurrentAge.ToString () + "y" : CurrentAgeInMonths.ToString () + "m"); } }


        //public List<MemberRelationship> Relationships { get { return application.MemberRelationshipsGet (Id, true); } }

        //public List<MemberRelationship> RelationshipsActive {

        //    get {

        //        List<MemberRelationship> memberRelationships = new List<MemberRelationship> ();

        //        foreach (MemberRelationship currentRelationship in Relationships) {

        //            if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

        //                memberRelationships.Add (currentRelationship);

        //            }

        //        }

        //        return memberRelationships;

        //    }

        //}

        //public MemberRelationship RelationshipSelf {

        //    get {

        //        MemberRelationship selfRelationship = null;

        //        foreach (MemberRelationship currentRelationship in Relationships) {

        //            if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

        //                if (currentRelationship.RelationshipId == 18) {

        //                    selfRelationship = currentRelationship;

        //                    break;

        //                }

        //            }

        //        }

        //        return selfRelationship;

        //    }

        //}

        //public String FamilyId {

        //    get {

        //        String familyId = (RelationshipSelf != null) ? RelationshipSelf.FamilyId : String.Empty;

        //        return familyId;

        //    }

        //}


        public Entity.Entity Entity {

            get {

                if ((entity == null) && (!serverRequests.Contains ("Entity"))) {

                    serverRequests.Add ("Entity");

                    GlobalProgressBarShow ("Entity");

                    Application.EntityGet (entityId, true, EntityGetCompleted);

                }

                return entity;

            }

        }

        public ObservableCollection<MemberEnrollment> Enrollments {

            get {

                if ((memberEnrollments == null) && (!serverRequests.Contains ("MemberEnrollments"))) {

                    serverRequests.Add ("MemberEnrollments");

                    GlobalProgressBarShow ("MemberEnrollments");

                    Application.MemberEnrollmentsGet (id, true, MemberEnrollmentsGetCompleted);

                }

                return memberEnrollments;

            }

        }

        public MemberEnrollment CurrentEnrollment {

            get {

                if ((currentEnrollment == null) && (!serverRequests.Contains ("MemberEnrollments"))) {

                    serverRequests.Add ("MemberEnrollments");

                    GlobalProgressBarShow ("CurrentEnrollment");

                    Application.MemberEnrollmentsGet (id, true, MemberEnrollmentsGetCompleted);

                }

                return currentEnrollment;

            }

        }

        public ObservableCollection<MemberRelationship> Relationships {

            get {

                if ((memberRelationships == null) && (!serverRequests.Contains ("MemberRelationships"))) {

                    serverRequests.Add ("MemberRelationships");

                    GlobalProgressBarShow ("MemberRelationships");

                    Application.MemberRelationshipsGet (id, true, MemberRelationshipsGetCompleted);

                }

                return memberRelationships;

            }

        }

        #endregion


        #region Property Data Binding Callbacks

        private void EntityGetCompleted (Object sender, Server.Application.EntityGetCompletedEventArgs e) {

            serverRequests.Remove ("Entity");

            GlobalProgressBarHide ("Entity");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                entity = new Entity.Entity (Application, e.Result);

                NotifyPropertyChanged ("Name");

                NotifyPropertyChanged ("Entity");

            }

            return;

        }

        private void MemberEnrollmentsGetCompleted (Object sender, Server.Application.MemberEnrollmentsGetCompletedEventArgs e) {

            serverRequests.Remove ("MemberEnrollments");

            GlobalProgressBarHide ("MemberEnrollments");

            GlobalProgressBarHide ("CurrentEnrollment");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                memberEnrollments = Converters.ServerCollectionToClient.MemberEnrollmentCollection (Application, e.Result.Collection);

                foreach (MemberEnrollment currentMemberEnrollment in memberEnrollments) {

                    if ((DateTime.Today >= currentMemberEnrollment.EffectiveDate) && (DateTime.Today <= currentMemberEnrollment.TerminationDate)) {

                        currentEnrollment = currentMemberEnrollment;

                        break;

                    }

                }

                NotifyPropertyChanged ("Enrollments");

                NotifyPropertyChanged ("CurrentEnrollment");

            }

            return;

        }

        private void MemberRelationshipsGetCompleted (Object sender, Server.Application.MemberRelationshipsGetCompletedEventArgs e) {

            serverRequests.Remove ("MemberRelationships");

            GlobalProgressBarHide ("MemberRelationships");

            if ((!e.Cancelled) && (e.Error == null) && (e.Result != null)) {

                memberRelationships = Converters.ServerCollectionToClient.MemberRelationshipCollection (Application, e.Result.Collection);

                NotifyPropertyChanged ("Relationships");

            }

            return;

        }

        #endregion 


        #region Constructors

        public Member (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public Member (Application applicationReference, Server.Application.Member serverMember) {

            BaseConstructor (applicationReference, serverMember);

            return;

        }

        protected void BaseConstructor (Application applicationReference, Server.Application.Member serverMember) {

            base.BaseConstructor (applicationReference, serverMember);


            entityId = serverMember.EntityId;

            birthDate = serverMember.BirthDate;

            deathDate = serverMember.DeathDate;

            gender = serverMember.Gender;


            ethnicityId = serverMember.EthnicityId;

            citizenshipId = serverMember.CitizenshipId;

            languageId = serverMember.LanguageId;

            maritalStatusId = serverMember.MaritalStatusId;

            return;

        }

        #endregion


    }

}
