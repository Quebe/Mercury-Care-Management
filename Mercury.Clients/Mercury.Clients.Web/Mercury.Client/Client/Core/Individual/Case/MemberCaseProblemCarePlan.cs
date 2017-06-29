using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseProblemCarePlan : CoreObject {

        #region Private Properties

        private Int64 memberCaseProblemClassId;

        private MemberCaseProblemClass memberCaseProblemClass = null;

        private Int64 problemStatementId;

        private Int64 memberCaseCarePlanId; // MEMBER CASE CARE PLAN ID

        private Boolean isSingleInstance = false;

        #endregion 


        #region Public Properties - Encapsulated

        public Int64 MemberCaseProblemClassId { get { return memberCaseProblemClassId; } set { memberCaseProblemClassId = value; } }

        public MemberCaseProblemClass MemberCaseProblemClass { get { return memberCaseProblemClass; } set { memberCaseProblemClass = value; } }

        public Int64 ProblemStatementId { get { return problemStatementId; } set { problemStatementId = value; } }

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public Boolean IsSingleInstance { get { return isSingleInstance; } set { isSingleInstance = value; } }

        #endregion 


        #region Public Properties

        public ProblemStatement ProblemStatement { get { return application.ProblemStatementGet (problemStatementId, true); } }

        public String ProblemStatementName { get { return ((ProblemStatement != null) ? ProblemStatement.Name : String.Empty); } }

        public String ProblemStatementClassification { get { return ((ProblemStatement != null) ? ProblemStatement.Classification : String.Empty); } }

        public String ProblemStatementClassificationWithName { get { return ((ProblemStatement != null) ? ProblemStatement.ClassificationWithName : String.Empty); } }

        public MemberCaseCarePlan MemberCaseCarePlan { get { return MemberCaseProblemClass.MemberCase.CarePlan (memberCaseCarePlanId); } }

        #endregion 
        

        #region Constructors

        public MemberCaseProblemCarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseProblemCarePlan (Application applicationReference, Mercury.Server.Application.MemberCaseProblemCarePlan serverObject) {

            BaseConstructor (applicationReference, serverObject);

        
            MemberCaseProblemClassId = serverObject.MemberCaseProblemClassId;

            ProblemStatementId = serverObject.ProblemStatementId;

            MemberCaseCarePlanId = serverObject.MemberCaseCarePlanId;

            IsSingleInstance = serverObject.IsSingleInstance;
           
            return;

        }

        #endregion 
        
        #region Public Methods

        public virtual void MapToServerObject (Server.Application.MemberCaseProblemCarePlan serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseProblemClassId = MemberCaseProblemClassId;

            serverObject.ProblemStatementId = ProblemStatementId;

            serverObject.MemberCaseCarePlanId = MemberCaseCarePlanId;

            serverObject.IsSingleInstance = IsSingleInstance;
            

            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseProblemCarePlan serverObject = new Server.Application.MemberCaseProblemCarePlan ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseProblemCarePlan Copy () {

            Server.Application.MemberCaseProblemCarePlan serverObject = (Server.Application.MemberCaseProblemCarePlan)ToServerObject ();

            MemberCaseProblemCarePlan copiedObject = new MemberCaseProblemCarePlan (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseProblemCarePlan compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);



            return isEqual;

        }

        #endregion 

    }
}
