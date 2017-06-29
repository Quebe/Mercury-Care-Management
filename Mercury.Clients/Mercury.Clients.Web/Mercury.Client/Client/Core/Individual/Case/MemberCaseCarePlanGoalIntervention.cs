using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanGoalIntervention : CoreObject {

        #region Private Properties

        private Int64 memberCaseCarePlanGoalId;

        private Int64 memberCaseCareInterventionId; // MEMBER CASE CARE PLAN ID

        private Server.Application.CarePlanItemInclusion inclusion = Server.Application.CarePlanItemInclusion.Suggested;

        private Boolean isSingleInstance = false;

        private MemberCaseCarePlanGoal memberCaseCarePlanGoal = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCarePlanGoalId { get { return memberCaseCarePlanGoalId; } set { memberCaseCarePlanGoalId = value; } }

        public Int64 MemberCaseCareInterventionId { get { return memberCaseCareInterventionId; } set { memberCaseCareInterventionId = value; } }

        public Server.Application.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }

        public Boolean IsSingleInstance { get { return isSingleInstance; } set { isSingleInstance = value; } }

        public MemberCaseCarePlanGoal MemberCaseCarePlanGoal { get { return memberCaseCarePlanGoal; } set { memberCaseCarePlanGoal = value; } }

        #endregion


        #region Public Properties

        public ProblemStatement ProblemStatement { get { return application.ProblemStatementGet (memberCaseCarePlanGoalId, true); } }

        public String ProblemStatementName { get { return ((ProblemStatement != null) ? ProblemStatement.Name : String.Empty); } }

        public String ProblemStatementClassification { get { return ((ProblemStatement != null) ? ProblemStatement.Classification : String.Empty); } }

        public MemberCaseCareIntervention CareIntervention { get { return MemberCaseCarePlanGoal.MemberCaseCarePlan.MemberCase.CareIntervention (memberCaseCareInterventionId); } }

        #endregion


        #region Constructors

        public MemberCaseCarePlanGoalIntervention (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanGoalIntervention (Application applicationReference, Mercury.Server.Application.MemberCaseCarePlanGoalIntervention serverObject) {

            BaseConstructor (applicationReference, serverObject);


            MemberCaseCarePlanGoalId = serverObject.MemberCaseCarePlanGoalId;

            MemberCaseCareInterventionId = serverObject.MemberCaseCareInterventionId;

            Inclusion = serverObject.Inclusion;

            IsSingleInstance = serverObject.IsSingleInstance;

            return;

        }

        #endregion

        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlanGoalIntervention serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseCarePlanGoalId = MemberCaseCarePlanGoalId;

            serverObject.MemberCaseCareInterventionId = MemberCaseCareInterventionId;

            serverObject.Inclusion = Inclusion;

            serverObject.IsSingleInstance = IsSingleInstance;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanGoalIntervention serverObject = new Server.Application.MemberCaseCarePlanGoalIntervention ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlanGoalIntervention Copy () {

            Server.Application.MemberCaseCarePlanGoalIntervention serverObject = (Server.Application.MemberCaseCarePlanGoalIntervention)ToServerObject ();

            MemberCaseCarePlanGoalIntervention copiedObject = new MemberCaseCarePlanGoalIntervention (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanGoalIntervention compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);



            return isEqual;

        }

        #endregion

    }
}
