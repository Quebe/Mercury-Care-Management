using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual {

    [Serializable]
    public class CareInterventionActivity : Core.Activity.Activity {
        
        #region Private Properties

        private Int64 careInterventionId;

        private Server.Application.CareInterventionActivityType careInterventionActivityType = Server.Application.CareInterventionActivityType.Intervention;

        private String clinicalNarrative;

        private String commonNarrative;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Server.Application.CareInterventionActivityType CareInterventionActivityType { get { return careInterventionActivityType; } set { careInterventionActivityType = value; } }

        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        #endregion


        #region Public Properties

        public String CareInterventionActivityTypeDescription {

            get {

                String description = Server.CommonFunctions.EnumerationToString (careInterventionActivityType);

                description += " (" + Server.CommonFunctions.EnumerationToString (base.ActivityType) + ")";

                return description;

            }

        }

        #endregion 


        #region Constructors

        protected CareInterventionActivity () { /* DO NOTHING, FOR INHERITANCE */ }

        public CareInterventionActivity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public CareInterventionActivity (Application applicationReference, Mercury.Server.Application.CareInterventionActivity serverObject) {

            BaseConstructor (applicationReference, (Mercury.Server.Application.Activity) serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public virtual void MapFromServerObject (Mercury.Server.Application.CareInterventionActivity serverObject) {


            careInterventionId = serverObject.CareInterventionId;

            careInterventionActivityType = serverObject.CareInterventionActivityType;

            clinicalNarrative = serverObject.ClinicalNarrative;

            commonNarrative = serverObject.CommonNarrative;


            return; 

        }

        public virtual void MapToServerObject (Server.Application.CareInterventionActivity serverObject) {

            base.MapToServerObject ((Server.Application.Activity)serverObject);


            serverObject.CareInterventionId = careInterventionId;

            serverObject.CareInterventionActivityType = careInterventionActivityType;

            serverObject.ClinicalNarrative = clinicalNarrative;

            serverObject.CommonNarrative = commonNarrative;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.CareInterventionActivity serverObject = new Server.Application.CareInterventionActivity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new CareInterventionActivity Copy () {

            Server.Application.CareInterventionActivity serverObject = (Server.Application.CareInterventionActivity)ToServerObject ();

            CareInterventionActivity copiedObject = new CareInterventionActivity (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (CareInterventionActivity compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);


            isEqual &= (careInterventionId == compareObject.careInterventionId);

            isEqual &= (careInterventionActivityType == compareObject.careInterventionActivityType);

            isEqual &= (clinicalNarrative == compareObject.ClinicalNarrative);

            isEqual &= (commonNarrative == compareObject.CommonNarrative);


            return isEqual;

        }

        #endregion 

    }

}
