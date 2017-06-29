using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using Mercury.Server.Data;

namespace Mercury.Server.Core.Member {

    [Serializable]
    [DataContract (Name="Member")]
    public class Member : CoreObject {

        #region Private Properties

        [DataMember (Name = "EntityId")]
        private Int64 entityId;

        [DataMember (Name = "BirthDate")]
        private DateTime birthDate;

        [DataMember (Name = "DeathDate")]
        private DateTime? deathDate;

        [DataMember (Name = "Gender")]
        private String gender;

        [DataMember (Name = "EthnicityId")]
        private Int64 ethnicityId;

        [DataMember (Name = "CitizenshipId")]
        private Int64 citizenshipId;

        [DataMember (Name = "LanguageId")]
        private Int64 languageId;

        [DataMember (Name = "MaritalStatusId")]
        private Int64 maritalStatusId;

        [DataMember (Name = "FamilyId")]
        private String familyId = String.Empty;

        [DataMember (Name = "Entity")]
        private Entity.Entity entity = null;

        private List<MemberEnrollment> enrollments = null;

        private List<MemberEnrollmentTplCob> enrollmentTplCobs = null;

        #endregion


        #region Public Properties

        public override String Name { get { return (Entity != null) ? Entity.Name : String.Empty; } set { if (Entity != null) { Entity.Name = value; } } }

        public override string Description { get { return Name + "[" + Gender + " | " + CurrentAge.ToString ().PadLeft (2, '0') + "]"; } }

        
        public Int64 EntityId { get { return entityId; } }

        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }

        public DateTime? DeathDate { get { return deathDate; } set { deathDate = value; } }

        public String Gender { get { return gender; } set { gender = CommonFunctions.SetValueInRange (value.ToUpper (), "M;F;U", "U"); } }

        public Int64 EthnicityId { get { return ethnicityId; } set { ethnicityId = value; } }

        public Int64 CitizenshipId { get { return citizenshipId; } set { citizenshipId = value; } }

        public Int64 LanguageId { get { return languageId; } set { languageId = value; } }

        public Int64 MaritalStatusId { get { return maritalStatusId; } set { maritalStatusId = value; } }

        public String FamilyId { get { return familyId; } set { familyId = value; } }

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

        //public String EthnicityDescription { get { return application.CoreObjectGetName ("Ethnicity", ethnicityId, true); } }

        //public String LanguageDescription { get { return application.CoreObjectGetName ("Language", languageId, true); } }

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
            
        public Entity.Entity Entity {
            
            get {

                if (entity != null) { return entity; }

                if (application == null) { return null; }

                entity = base.application.EntityGetMember (entityId, true);

                return entity;

            }

        }


        public List<MemberRelationship> Relationships {

            get {

                List<MemberRelationship> memberRelationships = new List<MemberRelationship> ();

                if (application != null) { memberRelationships = application.MemberRelationshipsGet (Id); }

                return memberRelationships;

            }

        }

        public List<MemberRelationship> RelationshipsActive {

            get {

                List<MemberRelationship> memberRelationships = new List<MemberRelationship> ();

                foreach (MemberRelationship currentRelationship in Relationships) {

                    if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

                        memberRelationships.Add (currentRelationship);

                    }

                }

                return memberRelationships;

            }

        }

        public MemberRelationship RelationshipSelf {

            get {

                MemberRelationship selfRelationship = null;

                foreach (MemberRelationship currentRelationship in Relationships) {

                    if ((DateTime.Today >= currentRelationship.EffectiveDate) && (DateTime.Today <= currentRelationship.TerminationDate)) {

                        if (currentRelationship.RelationshipId == 18) {

                            selfRelationship = currentRelationship;

                            break;

                        }

                    }

                }

                return selfRelationship;

            }

        }

        public String FamilyIdFromRelationship {

            get {

                String familyId = (RelationshipSelf != null) ? RelationshipSelf.FamilyId : String.Empty;

                return familyId;

            }

        }


        public List <MemberEnrollment> Enrollments { 
                
            get {

                if (enrollments != null) { return enrollments; }

                if (base.application == null) { return new List<MemberEnrollment> (); }

                enrollments = base.application.MemberEnrollmentsGet (Id);

                return enrollments;

            }

        }

        public Boolean HasCurrentEnrollment { get { return (CurrentEnrollment != null); } }

        public MemberEnrollment CurrentEnrollment {

            get {

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if ((DateTime.Today >= enrollment.EffectiveDate) && (DateTime.Today <= enrollment.TerminationDate)) {

                        return enrollment;

                    }

                }

                return null;

            }

        }

        public MemberEnrollment MostRecentEnrollment {

            get {

                MemberEnrollment mostRecent = null;

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if (mostRecent == null) { mostRecent = enrollment; }

                    if (enrollment.TerminationDate > mostRecent.TerminationDate) { mostRecent = enrollment; }

                    else if (enrollment.TerminationDate == mostRecent.TerminationDate) {

                        if (enrollment.EffectiveDate > mostRecent.EffectiveDate) { mostRecent = enrollment; }

                    }

                }

                return mostRecent;

            }

        }


        public Boolean HasCurrentEnrollmentCoverage { get { return (CurrentEnrollmentCoverage != null); } }

        public MemberEnrollmentCoverage CurrentEnrollmentCoverage {

            get {

                if (CurrentEnrollment != null) {

                    if (CurrentEnrollment.Coverages != null) {

                        foreach (MemberEnrollmentCoverage currentCoverage in CurrentEnrollment.Coverages) {

                            if ((DateTime.Today >= currentCoverage.EffectiveDate) && (DateTime.Today <= currentCoverage.TerminationDate)) {

                                return currentCoverage;

                            }

                        }

                    }

                }

                return null;

            }

        }


        public Boolean HasCurrentEnrollmentPcp { get { return (CurrentEnrollmentPcp != null); } }

        public MemberEnrollmentPcp CurrentEnrollmentPcp {

            get {

                if (CurrentEnrollment != null) {

                    if (CurrentEnrollment.Pcps != null) {

                        foreach (MemberEnrollmentPcp currentMemberEnrollmentPcp in CurrentEnrollment.Pcps) {

                            if ((DateTime.Today >= currentMemberEnrollmentPcp.EffectiveDate) && (DateTime.Today <= currentMemberEnrollmentPcp.TerminationDate)) {

                                return currentMemberEnrollmentPcp;

                            }

                        }

                    }

                }

                return null;

            }

        }


        public Boolean HasProspectiveEnrollment { get { return (ProspectiveEnrollment != null); }}

        public MemberEnrollment ProspectiveEnrollment {

            get {

                foreach (MemberEnrollment enrollment in Enrollments) {

                    if (DateTime.Today <= enrollment.EffectiveDate) {

                        return enrollment;

                    }

                }

                return null;

            }

        }


        public List<MemberEnrollmentTplCob> EnrollmentTplCobs {

            get {

                if (enrollmentTplCobs != null) { return enrollmentTplCobs; }

                if (application == null) { return new List<MemberEnrollmentTplCob> (); }

                enrollmentTplCobs = application.MemberEnrollmentTplCobsGet (Id);

                return enrollmentTplCobs;

            }

        }

        public Boolean HasCurrentEnrollmentTplCob { get { return (CurrentEnrollmentTplCob != null); } }

        public MemberEnrollmentTplCob CurrentEnrollmentTplCob {

            get {

                foreach (MemberEnrollmentTplCob enrollmentTplCob in EnrollmentTplCobs) {

                    if ((DateTime.Today >= enrollmentTplCob.EffectiveDate) && (DateTime.Today <= enrollmentTplCob.TerminationDate)) {

                        return enrollmentTplCob;

                    }

                }

                return null;

            }

        }

        public MemberEnrollmentTplCob MostRecentEnrollmentTplCob {

            get {

                MemberEnrollmentTplCob mostRecent = null;

                foreach (MemberEnrollmentTplCob enrollmentTplCob in EnrollmentTplCobs) {

                    if (mostRecent == null) { mostRecent = enrollmentTplCob; }

                    if (enrollmentTplCob.TerminationDate > mostRecent.TerminationDate) { mostRecent = enrollmentTplCob; }

                    else if (enrollmentTplCob.TerminationDate == mostRecent.TerminationDate) {

                        if (enrollmentTplCob.EffectiveDate > mostRecent.EffectiveDate) { mostRecent = enrollmentTplCob; }

                    }

                }

                return mostRecent;

            }

        }

        #endregion


        #region Base Properties Override

        public override Application Application {

            set {

                base.Application = value;

                if (entity != null) { entity.Application = value; }

                if (enrollments != null) {

                    foreach (MemberEnrollment currentEnrollment in enrollments) {

                        currentEnrollment.Application = value;

                    }

                }

                return;

            }

        }

        #endregion


        #region Constructors 

        public Member (Application applicationReference) {

            BaseConstructor (applicationReference);

            entity = null;
            
            return; 
        
        }

        public Member (Application applicationReference, Int64 memberId) {

            BaseConstructor (applicationReference);

            if (!Load (memberId)) {

                throw new ApplicationException ("Unable to load Member from the database for " + memberId.ToString () + ".");

            }

            return;

        }

        #endregion


        #region Database Functions

        override public Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable tableMember;

            if (base.application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("EXEC dal.Member_Select " + forId.ToString ());


            tableMember = base.application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString ());

            if (tableMember.Rows.Count == 1) {

                MapDataFields (tableMember.Rows[0]);

                success = true;

            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            entityId = base.IdFromSql (currentRow, "EntityId");
            
            birthDate = (DateTime) currentRow["BirthDate"];
            
            if (!(currentRow ["DeathDate"] is System.DBNull)) { deathDate = (DateTime) currentRow ["DeathDate"]; } else { deathDate = null; } 

            gender = (String) currentRow["Gender"];


            ethnicityId = base.IdFromSql (currentRow, "EthnicityId");

            citizenshipId = base.IdFromSql (currentRow, "CitizenshipId");

            languageId = base.IdFromSql (currentRow, "LanguageId");

            maritalStatusId = base.IdFromSql (currentRow, "MaritalStatusId");

            FamilyId = base.StringFromSql (currentRow, "FamilyId");

            
            if (currentRow.Table.Columns.Contains ("EntityName")) {

                try {

                    entity = new Entity.Entity (base.application);

                    entity.MapDataFields (currentRow);

                }

                catch { entity = null; }

            }
            
            return;

        } // MapDataFields (System.Data.DataRow currentRow) 

        #endregion


        #region Data Binding

        public override Dictionary<String, String>  DataBindingContexts {

            get {

                Dictionary<String, String> bindingContexts = base.DataBindingContexts;


                bindingContexts.Add ("BirthDate", "DateTime");

                bindingContexts.Add ("DeathDate", "DateTime");

                bindingContexts.Add ("CurrentAge", "Int16");

                bindingContexts.Add ("CurrentAgeInMonths", "Int16");

                bindingContexts.Add ("Gender", "String");

                bindingContexts.Add ("GenderDescription", "String");

                bindingContexts.Add ("EthnicityId", "Id|Ethnicity");

                bindingContexts.Add ("Ethnicity", "String");

                bindingContexts.Add ("CitizenshipId", "String");

                bindingContexts.Add ("Citizenship", "String");
                
                bindingContexts.Add ("LanguageId", "Id|Language");

                bindingContexts.Add ("Language", "String");

                bindingContexts.Add ("MaritalStatusId", "Id|MaritalStatus");

                bindingContexts.Add ("MaritalStatus", "String");

                bindingContexts.Add ("FamilyId", "String");


                Dictionary<String, String> entityBindings = (new Entity.Entity (base.application)).DataBindingContexts;

                foreach (String currentContext in entityBindings.Keys) {

                    bindingContexts.Add ("Entity." + currentContext, entityBindings[currentContext]);

                }



                bindingContexts.Add ("Relationships", "Collection|MemberRelationship");

                bindingContexts.Add ("Enrollments", "Collection|MemberEnrollment");

                bindingContexts.Add ("EnrollmentCoverages", "Collection|MemberEnrollmentCoverage");

                bindingContexts.Add ("EnrollmentPcps", "Collection|MemberEnrollmentPcp");




                bindingContexts.Add ("PopulationMembership", "Collection|PopulationMembership");

                bindingContexts.Add ("MemberServices", "Collection|MemberService");

                return bindingContexts;

            }

        }

        override public String EvaluateDataBinding (String bindingContext) {

            String dataValue = String.Empty;

            String bindingContextPart = bindingContext.Split ('.')[0];

            switch (bindingContextPart) {

                case "ExternalMemberId": dataValue = (Entity != null) ? Entity.UniqueId : String.Empty; break;

                case "Entity":

                    if (bindingContext == "Entity.Id") { dataValue = entityId.ToString (); }

                    else { dataValue = Entity.EvaluateDataBinding (bindingContext.Replace (bindingContextPart + ".", "")); }
                    
                    break;

                case "BirthDate": dataValue = birthDate.ToString ("MM/dd/yyyy"); break;

                case "DeathDate": if (!deathDate.HasValue) { dataValue = String.Empty; } else { dataValue = deathDate.Value.ToString ("MM/dd/yyyy"); } break;

                case "CurrentAge": dataValue = CurrentAge.ToString (); break;

                case "Gender": dataValue = gender; break;

                case "GenderDescription": dataValue = GenderDescription; break;

                case "EthnicityId": dataValue = ethnicityId.ToString (); break;

                case "Ethnicity": dataValue = base.application.CoreObjectGetNameById ("Ethnicity", ethnicityId); break;

                case "CitizenshipId": dataValue = citizenshipId.ToString (); break;

                case "Citizenship": dataValue = application.CoreObjectGetNameById ("Citizenship", citizenshipId); break;

                case "LanguageId": dataValue = languageId.ToString (); break;

                case "Language": dataValue = application.CoreObjectGetNameById ("Language", languageId); break;

                case "MaritalStatusId": dataValue = maritalStatusId.ToString (); break;

                case "MaritalStatus": dataValue = application.CoreObjectGetNameById ("MaritalStatus", maritalStatusId); break;

                case "FamilyId": dataValue = FamilyId; break;


                case "Relationships":

                    dataValue = "Relationships";

                    foreach (MemberRelationship currentRelationship in base.application.MemberRelationshipsGet (Id)) {

                        dataValue = dataValue + "|" + currentRelationship.Id;

                    }

                    break;


                case "Enrollments":

                    dataValue = "Enrollment";

                    foreach (MemberEnrollment currentEnrollment in base.application.MemberEnrollmentsGet (Id)) {

                        dataValue = dataValue + "|" + currentEnrollment.Id;

                    }

                    break;


                case "CurrentEnrollment":

                    if (HasCurrentEnrollment) {

                        bindingContextPart = bindingContext.Replace ("CurrentEnrollment.", "");

                        dataValue = CurrentEnrollment.EvaluateDataBinding (bindingContextPart);

                    }

                    else { dataValue = String.Empty; }

                    break;

                case "CurrentEnrollmentCoverage":

                    if (HasCurrentEnrollmentCoverage) {

                        bindingContextPart = bindingContextPart.Replace ("CurrentEnrollmentCoverage.", "");

                        dataValue = CurrentEnrollmentCoverage.EvaluateDataBinding (bindingContextPart);

                    }

                    break;

                case "CurrentPcpAssignment": // BACKWARDS COMPATIBILITY

                case "CurrentEnrollmentPcp":

                    if (HasCurrentEnrollmentPcp) {

                        bindingContextPart = bindingContext.Replace ("CurrentPcpAssignment.", ""); // BACKWARDS COMPATIBILITY

                        bindingContextPart = bindingContext.Replace ("CurrentEnrollmentPcp.", "");

                        dataValue = CurrentEnrollmentPcp.EvaluateDataBinding (bindingContextPart);

                    }

                    else { dataValue = String.Empty; }

                    break;

                //case "PopulationMembership":

                //    dataValue = "PopulationMembership";

                //    List<Core.Population.PopulationMembership> populationMembership = application.PopulationMembershipGetByMember (memberId);

                //    foreach (Core.Population.PopulationMembership currentMembership in populationMembership) {

                //        dataValue = dataValue + "|" + currentMembership.Id.ToString ();

                //    }

                //    break;

                //case "MemberServices":

                //    dataValue = "MemberService";

                //    List<Int64> memberServices = application.MemberServiceGetIdListByMember (memberId);

                //    foreach (Int64 currentMemberServiceId in memberServices) {

                //        dataValue = dataValue + "|" + currentMemberServiceId.ToString ();

                //    }

                //    break;

                default: dataValue = base.EvaluateDataBinding (bindingContext); break;

            }

            return dataValue;

        }

        #endregion 


        #region Public Methods

        public MedicalServices.MemberService MostRecentMemberService (String serviceName) {

            if (application == null) { return null; }

            return application.MemberServiceGetByMemberMostRecent (Id, serviceName);

        }

        public Metrics.MemberMetric MostRecentMemberMetric (String metricName) {

            if (application == null) { return null; }

            return application.MemberMetricGetByMemberMostRecent (Id, metricName);

        }

        public Core.Forms.Form MostRecentForm (String formName) {

            Core.Forms.Form mostRecentForm = null;

            List<Entity.Views.EntityDocument> documents = application.EntityDocumentsGetByPage (entityId, 0, 1000);

            foreach (Entity.Views.EntityDocument currentDocument in documents) {

                if ((currentDocument.DocumentType == "Form") && (currentDocument.Name == formName)) {

                    mostRecentForm = new Mercury.Server.Core.Forms.Form (application, currentDocument.EntityFormId, true);

                    break;

                }

            }

            return mostRecentForm;

        }

        #endregion


        #region Public Methods - Population Management

        public List<Population.PopulationMembership> PopulationMembership { get { return application.PopulationMembershipGetByMember (id); } }


        public Population.PopulationMembership CurrentPopulationMembership (String populationName) {

            Population.PopulationMembership populationMembership = null;


            foreach (Population.PopulationMembership currentMembership in PopulationMembership) {

                if (currentMembership.PopulationName == populationName) {

                    if ((DateTime.Today >= currentMembership.EffectiveDate) && (DateTime.Today <= currentMembership.TerminationDate)) {

                        populationMembership = currentMembership;

                        break;

                    }

                }

            }

            return populationMembership;

        }

        public Int64 CurrentPopulationMembershipId (String forPopulationName) { return ((CurrentPopulationMembership (forPopulationName) != null) ? CurrentPopulationMembership (forPopulationName).Id : 0); }

        public Boolean InPopulation (String forPopulationName) { return (CurrentPopulationMembership (forPopulationName) != null); }


        public Population.PopulationMembership MostRecentPopulationMembership (String populationName) {

            Population.PopulationMembership populationMembership = null;

            foreach (Population.PopulationMembership currentMembership in PopulationMembership) {

                if (currentMembership.PopulationName == populationName) {

                    if (populationMembership == null) { populationMembership = currentMembership; }

                    else {

                        if (currentMembership.TerminationDate > populationMembership.TerminationDate) {

                            populationMembership = currentMembership;

                        }

                    }

                }

            }

            return populationMembership;

        }


        //public List<PopulationManagement.DataViews.PopulationMembershipSummary> PopulationMembershipSummary () {

        //    return application.PopulationMembershipSummaryByMember (memberId);

        //}

        //public List<PopulationManagement.DataViews.PopulationMembershipServiceEvent> PopulationMembershipServiceEventsDataView (Int64 membershipId) {
           
        //    return application.PopulationMembershipServiceEventsByMembershipDataView (membershipId);

        //}

        ///// <summary>
        ///// Get the Trigger Events that have fired against a specific Population Membership segment.
        ///// </summary>
        ///// <param name="membershipId">Population Membership Id</param>
        ///// <returns>List of Trigger Events (Data View)</returns>
        //public List<PopulationManagement.DataViews.PopulationMembershipTriggerEvent> PopulationMembershipTriggerEventsDataView (Int64 membershipId) {

        //    return application.PopulationMembershipTriggerEventsByMembershipDataView (membershipId);

        //}

        #endregion
        
        
        #region Public Methods - Work 

        /// <summary>
        /// Returns the most recent instance of the member in a given Work Queue, or null if member was never in the Work Queue.
        /// </summary>
        /// <param name="workQueueName">Name of the Work Queue.</param>
        /// <returns>Work Queue Item</returns>
        public Work.WorkQueueItem MostRecentWorkQueueItem (String workQueueName) {

            return application.WorkQueueItemGetByObjectIdMostRecent (workQueueName, ObjectType, Id);

        }

        #endregion 

    }

}
