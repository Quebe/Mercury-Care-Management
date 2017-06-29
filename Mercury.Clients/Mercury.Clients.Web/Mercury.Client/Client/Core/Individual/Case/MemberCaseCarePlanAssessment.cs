using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanAssessment : CoreObject {

        #region Private Properties

        private Int64 memberCaseCarePlanId = 0;

        private DateTime assessmentDate = DateTime.Now;

        private List<MemberCaseCarePlanAssessmentCareMeasure> measures = new List<MemberCaseCarePlanAssessmentCareMeasure> ();

        private MemberCaseCarePlan memberCaseCarePlan = null; // LOCAL REFERENCE

        #endregion 


        #region Public Properties

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public DateTime AssessmentDate { get { return assessmentDate; } set { assessmentDate = value; } }

        public List<MemberCaseCarePlanAssessmentCareMeasure> Measures { get { return measures; } set { measures = value; } }

        public MemberCaseCarePlan MemberCaseCarePlan { get { return memberCaseCarePlan; } set { memberCaseCarePlan = value; } }

        #endregion 
        

        #region Constructors

        public MemberCaseCarePlanAssessment (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessment (Application applicationReference, Server.Application.MemberCaseCarePlanAssessment serverObject) {

            base.BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion 

        
        #region Public Methods

        public void MapFromServerObject (Server.Application.MemberCaseCarePlanAssessment serverObject) {

            base.MapFromServerObject ((Server.Application.CoreObject)serverObject);


            MemberCaseCarePlanId = serverObject.MemberCaseCarePlanId;

            AssessmentDate = serverObject.AssessmentDate;

            Measures = new List<MemberCaseCarePlanAssessmentCareMeasure> ();

            foreach (Server.Application.MemberCaseCarePlanAssessmentCareMeasure currentServerMeasure in serverObject.Measures) {

                MemberCaseCarePlanAssessmentCareMeasure assessmentCareMeasure = new MemberCaseCarePlanAssessmentCareMeasure (application, currentServerMeasure);

                assessmentCareMeasure.MemberCaseCarePlanAssessment = this;

                Measures.Add (assessmentCareMeasure);

            }

            return;

        }

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlanAssessment serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseCarePlanId = MemberCaseCarePlanId;

            serverObject.AssessmentDate = AssessmentDate;

            serverObject.Measures = new Server.Application.MemberCaseCarePlanAssessmentCareMeasure[Measures.Count];

            foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentMeasure in Measures) {

                Server.Application.MemberCaseCarePlanAssessmentCareMeasure serverAssessmentMeasure = (Server.Application.MemberCaseCarePlanAssessmentCareMeasure)currentAssessmentMeasure.ToServerObject ();

                serverObject.Measures[Measures.IndexOf (currentAssessmentMeasure)] = serverAssessmentMeasure;

            }

            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanAssessment serverObject = new Server.Application.MemberCaseCarePlanAssessment ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlanAssessment Copy () {

            Server.Application.MemberCaseCarePlanAssessment serverObject = (Server.Application.MemberCaseCarePlanAssessment)ToServerObject ();

            MemberCaseCarePlanAssessment copiedObject = new MemberCaseCarePlanAssessment (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanAssessment compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);
            
            // TODO:

            return isEqual;

        }

        #endregion


        #region Public Methods

        public Boolean ContainsCareMeasure (Int64 careMeasureId) { 

            foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentCareMeasure in measures) {

                if (currentAssessmentCareMeasure.CareMeasureId == careMeasureId) { return true; }

            }

            return false;

        }

        public MemberCaseCarePlanAssessmentCareMeasure CareMeasure (Int64 careMeasureId) {

            foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentCareMeasure in measures) {

                if (currentAssessmentCareMeasure.CareMeasureId == careMeasureId) { return currentAssessmentCareMeasure; }

            }

            return null;

        }

        #endregion 

    }

}
