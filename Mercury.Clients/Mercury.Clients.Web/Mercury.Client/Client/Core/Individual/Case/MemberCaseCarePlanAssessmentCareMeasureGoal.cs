using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanAssessmentCareMeasureGoal : CoreObject {

        #region Private Properties

        private Int64 memberCaseCarePlanAssessmentCareMeasureId = 0;

        private Int64 memberCaseCarePlanGoalId = 0;

        private MemberCaseCarePlanAssessmentCareMeasure memberCaseCarePlanAssessmentCareMeasure = null;

        #endregion


        #region Public Properties

        public Int64 MemberCaseCarePlanAssessmentCareMeasureId { get { return memberCaseCarePlanAssessmentCareMeasureId; } set { memberCaseCarePlanAssessmentCareMeasureId = value; } }

        public Int64 MemberCaseCarePlanGoalId { get { return memberCaseCarePlanGoalId; } set { memberCaseCarePlanGoalId = value; } }

        public MemberCaseCarePlanAssessmentCareMeasure MemberCaseCarePlanAssessmentCareMeasure { get { return memberCaseCarePlanAssessmentCareMeasure; } set { memberCaseCarePlanAssessmentCareMeasure = value; } }

        #endregion 
        

        #region Constructors

        public MemberCaseCarePlanAssessmentCareMeasureGoal (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }
        
        public MemberCaseCarePlanAssessmentCareMeasureGoal (Application applicationReference, Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverObject) {

            base.BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion 

        
        #region Public Methods

        public void MapFromServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverObject) {

            base.MapFromServerObject ((Server.Application.CoreObject) serverObject);

            MemberCaseCarePlanAssessmentCareMeasureId = serverObject.MemberCaseCarePlanAssessmentCareMeasureId;

            MemberCaseCarePlanGoalId = serverObject.MemberCaseCarePlanGoalId;

            return;

        }

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseCarePlanAssessmentCareMeasureId = MemberCaseCarePlanAssessmentCareMeasureId;

            serverObject.MemberCaseCarePlanGoalId = MemberCaseCarePlanGoalId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverObject = new Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlanAssessmentCareMeasureGoal Copy () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal serverObject = (Server.Application.MemberCaseCarePlanAssessmentCareMeasureGoal)ToServerObject ();

            MemberCaseCarePlanAssessmentCareMeasureGoal copiedObject = new MemberCaseCarePlanAssessmentCareMeasureGoal (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanAssessmentCareMeasureGoal compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);
            
            // TODO:

            return isEqual;

        }

        #endregion


        #region Public Methods

        public void SetMemberCaseCarePlanGoal (MemberCaseCarePlanGoal forMemberCaseCarePlanGoal) {

            MemberCaseCarePlanGoalId = forMemberCaseCarePlanGoal.Id;

            return;

        }

        #endregion

    }

}
