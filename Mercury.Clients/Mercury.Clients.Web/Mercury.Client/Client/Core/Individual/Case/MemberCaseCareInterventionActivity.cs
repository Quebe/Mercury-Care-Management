using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCareInterventionActivity : CareInterventionActivity {

        #region Private Properties

        private Int64 memberCaseCareInterventionId = 0;

        private Int64 careInterventionActivityId = 0;

        private MemberCaseCareIntervention memberCaseCareIntervention = null;

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCareInterventionId { get { return memberCaseCareInterventionId; } set { memberCaseCareInterventionId = value; } }

        public Int64 CareInterventionActivityId { get { return careInterventionActivityId; } set { careInterventionActivityId = value; } }

        public MemberCaseCareIntervention MemberCaseCareIntervention { get { return memberCaseCareIntervention; } set { memberCaseCareIntervention = value; } }

        #endregion


        #region Public Properties

        public String InterventionActivityDescriptionHtml {

            get {

                String description = "<span style=\"font-weight: bold;\">" + Name + "</span><br />";

                description += "<span style=\"font-style: italic;\">" + CareInterventionActivityTypeDescription + "</span><br />";

                description += ClinicalNarrative.Replace ("\r\n", " •");


                return description;

            }

        }

        #endregion


        #region Constructors

        public MemberCaseCareInterventionActivity (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCareInterventionActivity (Application applicationReference, Mercury.Server.Application.MemberCaseCareInterventionActivity serverObject) {

            BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion


        #region Public Methods

        public void MapFromServerObject (Mercury.Server.Application.MemberCaseCareInterventionActivity serverObject) {

            base.MapFromServerObject ((Mercury.Server.Application.CareInterventionActivity)serverObject);


            MemberCaseCareInterventionId = serverObject.MemberCaseCareInterventionId;

            CareInterventionActivityId = serverObject.CareInterventionActivityId;


            return;

        }

        public void MapToServerObject (Server.Application.MemberCaseCareInterventionActivity serverObject) {

            base.MapToServerObject ((Server.Application.CareInterventionActivity)serverObject);


            serverObject.MemberCaseCareInterventionId = MemberCaseCareInterventionId;

            serverObject.CareInterventionActivityId = CareInterventionActivityId;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCareInterventionActivity serverObject = new Server.Application.MemberCaseCareInterventionActivity ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public new MemberCaseCareInterventionActivity Copy () {

            Server.Application.MemberCaseCareInterventionActivity serverObject = (Server.Application.MemberCaseCareInterventionActivity)ToServerObject ();

            MemberCaseCareInterventionActivity copiedObject = new MemberCaseCareInterventionActivity (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCareInterventionActivity compareObject) {

            Boolean isEqual = base.IsEqual ((CareInterventionActivity)compareObject);



            return isEqual;

        }

        #endregion

    }

}

