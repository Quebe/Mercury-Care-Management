using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlan : CoreConfigurationObject {

        #region Private Properties

        private Int64 memberCaseId = 0;

        private MemberCase memberCase = null;


        private Int64 carePlanId;

        private Server.Application.CaseItemStatus status = Server.Application.CaseItemStatus.NotSpecified;


        private DateTime addedDate;

        private DateTime effectiveDate;

        private DateTime terminationDate;

        private Int64 careOutcomeId = 0;


        private List<MemberCaseCarePlanGoal> goals = new List<MemberCaseCarePlanGoal> ();

        #endregion


        #region Public Properties - Encapsulated

        public override String Name { get { return (CarePlan != null) ? CarePlan.Name : String.Empty; } }

        public override String Description { get { return (CarePlan != null) ? CarePlan.Description : String.Empty; } }
            

        public Int64 MemberCaseId { get { return memberCaseId; } set { memberCaseId = value; } }

        public MemberCase MemberCase { get { return memberCase; } set { memberCase = value; } }


        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Server.Application.CaseItemStatus Status { get { return status; } set { status = value; } }


        public DateTime AddedDate { get { return addedDate; } set { addedDate = value; } }

        public DateTime EffectiveDate { get { return effectiveDate; } set { effectiveDate = value; } }

        public DateTime TerminationDate { get { return terminationDate; } set { terminationDate = value; } }
        
        public Int64 CareOutcomeId { get { return careOutcomeId; } set { careOutcomeId = value; } }


        public List<MemberCaseCarePlanGoal> Goals { get { return goals; } set { goals = value; } }

        #endregion 


        #region Public Properties

        public CarePlan CarePlan { get { return application.CarePlanGet (carePlanId, true); } }

        public CareOutcome CareOutcome { get { return application.CareOutcomeGet (careOutcomeId, true); } }


        public String AddedDateDescription { get { return addedDate.ToString ("MM/dd/yyyy"); } }

        public String EffectiveDateDescription { get { return effectiveDate.ToString ("MM/dd/yyyy"); } }

        public String TerminationDateDescription { get { return ((terminationDate != new DateTime (9999, 12, 31)) ? terminationDate.ToString ("MM/dd/yyyy") : "< active >"); } }


        public List<MemberCaseProblemCarePlan> Problems {

            get {

                List<MemberCaseProblemCarePlan> problems =

                    (from currentMemberCaseProblemClass in MemberCase.ProblemClasses

                     from currentMemberCaseProblemCarePlan in currentMemberCaseProblemClass.ProblemCarePlans

                     where currentMemberCaseProblemCarePlan.MemberCaseCarePlanId == Id

                     select currentMemberCaseProblemCarePlan).ToList ();

                return problems;

            }

        }

        public String ProblemsDescription {

            get {

                String problemDescription = String.Empty;

                List<MemberCaseProblemCarePlan> problems =

                    (from currentProblemCarePlan in Problems

                     orderby currentProblemCarePlan.ProblemStatementClassificationWithName

                     select currentProblemCarePlan).ToList ();

                foreach (MemberCaseProblemCarePlan currentProblemCarePlan in problems) {

                    problemDescription += currentProblemCarePlan.ProblemStatementClassificationWithName + "; ";

                }

                return problemDescription;

            }

        }

        public String StatusDescription { get { return Server.CommonFunctions.EnumerationToString (status); } }

        #endregion 


        #region Constructors
        
        public MemberCaseCarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlan (Application applicationReference, Mercury.Server.Application.MemberCaseCarePlan serverObject) {

            BaseConstructor (applicationReference, serverObject);


            MemberCaseId = serverObject.MemberCaseId;

            carePlanId = serverObject.CarePlanId;

            status = serverObject.Status;


            addedDate = serverObject.AddedDate;

            EffectiveDate = serverObject.EffectiveDate;

            TerminationDate = serverObject.TerminationDate;

            careOutcomeId = serverObject.CareOutcomeId;


            goals = new List<MemberCaseCarePlanGoal> ();

            foreach (Server.Application.MemberCaseCarePlanGoal currentServerGoal in serverObject.Goals) {

                MemberCaseCarePlanGoal memberCaseCarePlanGoal = new MemberCaseCarePlanGoal (application, currentServerGoal);

                memberCaseCarePlanGoal.MemberCaseCarePlan = this;

                goals.Add (memberCaseCarePlanGoal);

            }

           
            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlan serverObject) {

            base.MapToServerObject ((Server.Application.CoreExtensibleObject)serverObject);


            serverObject.MemberCaseId = MemberCaseId;

            serverObject.CarePlanId = CarePlanId;

            serverObject.Status = Status;


            serverObject.AddedDate = AddedDate;

            serverObject.EffectiveDate = EffectiveDate;

            serverObject.TerminationDate = TerminationDate;

            serverObject.CareOutcomeId = CareOutcomeId;


            serverObject.Goals = new Server.Application.MemberCaseCarePlanGoal[goals.Count];

            foreach (MemberCaseCarePlanGoal currentGoal in goals) {

                serverObject.Goals [goals.IndexOf (currentGoal)] = (Server.Application.MemberCaseCarePlanGoal) currentGoal.ToServerObject ();

            }
            
            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlan serverObject = new Server.Application.MemberCaseCarePlan ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlan Copy () {

            Server.Application.MemberCaseCarePlan serverObject = (Server.Application.MemberCaseCarePlan)ToServerObject ();

            MemberCaseCarePlan copiedObject = new MemberCaseCarePlan (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlan compareObject) {

            Boolean isEqual = base.IsEqual ((CoreExtensibleObject)compareObject);


            
            return isEqual;

        }

        #endregion 
        

        #region Public Methods

        public Boolean ContainsGoal (Int64 forCarePlanGoalId) {

            Boolean containsGoal = false;


            foreach (MemberCaseCarePlanGoal currentCarePlanGoal in Goals) {

                if (currentCarePlanGoal.Id == forCarePlanGoalId) {

                    containsGoal = true;

                    break;

                }

            }

            return containsGoal;

        }

        public MemberCaseCarePlanGoal Goal (Int64 forCarePlanGoalId) {

            MemberCaseCarePlanGoal goal = null;


            foreach (MemberCaseCarePlanGoal currentCarePlanGoal in Goals) {

                if (currentCarePlanGoal.Id == forCarePlanGoalId) {

                    goal = currentCarePlanGoal;

                    break;

                }

            }

            return goal;

        }

        public MemberCaseCarePlanAssessment CreateAssessment () {

            MemberCaseCarePlanAssessment assessment = new MemberCaseCarePlanAssessment (application);

            assessment.MemberCaseCarePlanId = this.Id;

            assessment.MemberCaseCarePlan = this;


            foreach (MemberCaseCarePlanGoal currentMemberCaseCarePlanGoal in goals) {

                MemberCaseCarePlanAssessmentCareMeasure assessmentCareMeasure = assessment.CareMeasure (currentMemberCaseCarePlanGoal.CareMeasureId);

                if (assessmentCareMeasure == null) {

                    // IF THE ASSESSMENT DOES NOT CONTAIN THE CARE MEASURE, ADD THE CARE MEASURE AND GET THE REFERENCE

                    assessmentCareMeasure = new MemberCaseCarePlanAssessmentCareMeasure (application);

                    assessmentCareMeasure.MemberCaseCarePlanAssessmentId = assessment.Id;

                    assessmentCareMeasure.MemberCaseCarePlanAssessment = assessment;

                    assessmentCareMeasure.SetCareMeasure (currentMemberCaseCarePlanGoal.CareMeasure);

                    assessment.Measures.Add (assessmentCareMeasure);

                }

                assessmentCareMeasure.AddMemberCaseCarePlanGoal (currentMemberCaseCarePlanGoal);

            }
            
            return assessment;

        }

        #endregion 

    }

}
