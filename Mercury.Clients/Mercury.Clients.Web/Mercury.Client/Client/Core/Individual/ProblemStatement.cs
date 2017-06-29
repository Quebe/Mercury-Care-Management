using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class ProblemStatement : CoreConfigurationObject {

        #region Private Properties

        private Int64 problemDomainId = 0;

        private String problemDomainName = String.Empty;

        private Int64 problemClassId = 0;

        private String problemClassName = String.Empty;


        private String definingCharacteristrics = String.Empty;

        private String relatedFactors = String.Empty;


        private Int64 defaultCarePlanId = 0;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 ProblemDomainId { get { return problemDomainId; } set { problemDomainId = value; } }

        public String ProblemDomainName { get { return problemDomainName; } set { problemDomainName = value; } }

        
        public Int64 ProblemClassId { get { return problemClassId; } set { problemClassId = value; } }

        public String ProblemClassName { get { return problemClassName; } set { problemClassName = value; } }


        public String DefiningCharacteristics { get { return definingCharacteristrics; } set { definingCharacteristrics = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String RelatedFactors { get { return relatedFactors; } set { relatedFactors = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public Int64 DefaultCarePlanId { get { return defaultCarePlanId; } set { defaultCarePlanId = value; } }

        #endregion 
        

        #region Public Properties

        public String Classification {

            get {

                String classification = problemDomainName;

                if (!String.IsNullOrWhiteSpace (problemClassName)) { classification += " - " + problemClassName; }

                return classification;

            }

        }

        public String ClassificationWithName { get { return Classification + " - " + Name; } }

        public CarePlan DefaultCarePlan { get { return application.CarePlanGet (defaultCarePlanId, true); } }

        public String DefaultCarePlanName { get { return ((DefaultCarePlan != null) ? DefaultCarePlan.Name : String.Empty); } }

        #endregion 


        #region Constructors

        public ProblemStatement (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public ProblemStatement (Application applicationReference, Mercury.Server.Application.ProblemStatement serverObject) {

            BaseConstructor (applicationReference, serverObject);


            problemDomainId = serverObject.ProblemDomainId;

            problemDomainName = serverObject.ProblemDomainName;

            problemClassId = serverObject.ProblemClassId;

            problemClassName = serverObject.ProblemClassName;


            definingCharacteristrics = serverObject.DefiningCharacteristics;

            relatedFactors = serverObject.RelatedFactors;


            defaultCarePlanId = serverObject.DefaultCarePlanId;

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapToServerObject (Server.Application.ProblemStatement serverObject) {

            base.MapToServerObject ((Server.Application.CoreConfigurationObject)serverObject);

            serverObject.ProblemDomainId = problemDomainId;

            serverObject.ProblemDomainName = problemDomainName;

            serverObject.ProblemClassId = problemClassId;

            serverObject.ProblemClassName = problemClassName;

            
            serverObject.DefiningCharacteristics = definingCharacteristrics;

            serverObject.RelatedFactors = relatedFactors;


            serverObject.DefaultCarePlanId = defaultCarePlanId;

            return;

        }

        public override Object ToServerObject () {

            Server.Application.ProblemStatement serverObject = new Server.Application.ProblemStatement ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public ProblemStatement Copy () {

            Server.Application.ProblemStatement serverObject = (Server.Application.ProblemStatement)ToServerObject ();

            ProblemStatement copiedProblemStatement = new ProblemStatement (application, serverObject);

            return copiedProblemStatement;

        }

        public Boolean IsEqual (ProblemStatement compareObject) {

            Boolean isEqual = base.IsEqual ((CoreConfigurationObject)compareObject);


            isEqual &= (problemDomainId == compareObject.ProblemDomainId);

            isEqual &= (problemDomainName == compareObject.ProblemDomainName);

            isEqual &= (problemClassId == compareObject.ProblemClassId);

            isEqual &= (problemClassName == compareObject.ProblemClassName);
            

            isEqual &= (definingCharacteristrics == compareObject.DefiningCharacteristics);

            isEqual &= (relatedFactors == compareObject.relatedFactors);


            isEqual &= (defaultCarePlanId == compareObject.DefaultCarePlanId);

            return isEqual;

        }

        #endregion 
        
    }

}
