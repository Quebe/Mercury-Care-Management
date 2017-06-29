using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Individual.Case {

    [Serializable]
    public class MemberCaseCarePlanAssessmentCareMeasureComponent : CoreObject {

        #region Private Properties

        private Int64 memberCaseCarePlanAssessmentCareMeasureId = 0;

        private MemberCaseCarePlanAssessmentCareMeasure memberCaseCarePlanAssessmentCareMeasure = null;

        private Int64 careMeasureComponentId = 0;

        private Int64 careMeasureScaleId = 0;

        private String tag = String.Empty;

        private Int32 componentValue = 0;

        #endregion


        #region Public Properties - Encapsulated

        public override String Name { get { return base.Name; } set { name = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description99); } }

        public Int64 MemberCaseCarePlanAssessmentCareMeasureId { get { return memberCaseCarePlanAssessmentCareMeasureId; } set { memberCaseCarePlanAssessmentCareMeasureId = value; } }

        public MemberCaseCarePlanAssessmentCareMeasure MemberCaseCarePlanAssessmentCareMeasure { get { return memberCaseCarePlanAssessmentCareMeasure; } set { memberCaseCarePlanAssessmentCareMeasure = value; } }

        public Int64 CareMeasureComponentId { get { return careMeasureComponentId; } set { careMeasureComponentId = value; } }

        public Int64 CareMeasureScaleId { get { return careMeasureScaleId; } set { careMeasureScaleId = value; } }

        public String Tag { get { return tag; } set { tag = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.UniqueId); } }

        public Int32 ComponentValue { get { return componentValue; } set { componentValue = ((value >= 0) && (value <= 5)) ? value : 0; } }

        #endregion


        #region Public Properties

        public CareMeasureScale CareMeasureScale { get { return application.CareMeasureScaleGet (careMeasureScaleId, true); } }

        public String CareMeasureScaleName { get { return ((CareMeasureScale != null) ? CareMeasureScale.Name : String.Empty); } }

        #endregion 
    

        #region Constructors

        public MemberCaseCarePlanAssessmentCareMeasureComponent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessmentCareMeasureComponent (Application applicationReference, Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverObject) {

            base.BaseConstructor (applicationReference, serverObject);

            MapFromServerObject (serverObject);

            return;

        }

        #endregion 

        
        #region Public Methods

        public void MapFromServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverObject) {

            base.MapFromServerObject ((Server.Application.CoreObject)serverObject);


            MemberCaseCarePlanAssessmentCareMeasureId = serverObject.MemberCaseCarePlanAssessmentCareMeasureId;

            CareMeasureComponentId = serverObject.CareMeasureComponentId;

            CareMeasureScaleId = serverObject.CareMeasureScaleId;

            Tag = serverObject.Tag;

            ComponentValue = serverObject.ComponentValue;


            return;

        }

        public virtual void MapToServerObject (Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverObject) {

            base.MapToServerObject ((Server.Application.CoreObject)serverObject);


            serverObject.MemberCaseCarePlanAssessmentCareMeasureId = MemberCaseCarePlanAssessmentCareMeasureId;

            serverObject.CareMeasureComponentId = CareMeasureComponentId;

            serverObject.CareMeasureScaleId = CareMeasureScaleId;

            serverObject.Tag = Tag;

            serverObject.ComponentValue = ComponentValue;


            return;

        }

        public override Object ToServerObject () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverObject = new Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent ();

            MapToServerObject (serverObject);

            return serverObject;

        }

        public MemberCaseCarePlanAssessmentCareMeasureComponent Copy () {

            Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent serverObject = (Server.Application.MemberCaseCarePlanAssessmentCareMeasureComponent)ToServerObject ();

            MemberCaseCarePlanAssessmentCareMeasureComponent copiedObject = new MemberCaseCarePlanAssessmentCareMeasureComponent (application, serverObject);

            return copiedObject;

        }

        public Boolean IsEqual (MemberCaseCarePlanAssessmentCareMeasureComponent compareObject) {

            Boolean isEqual = base.IsEqual ((CoreObject)compareObject);
            
            // TODO:

            return isEqual;

        }

        #endregion


        #region Public Methods

        public void SetCareMeasureComponent (CareMeasureComponent forCareMeasureComponent) {

            Name = forCareMeasureComponent.Name;

            Description = forCareMeasureComponent.Description;

            CareMeasureComponentId = forCareMeasureComponent.Id;

            CareMeasureScaleId = forCareMeasureComponent.CareMeasureScaleId;

            Tag = forCareMeasureComponent.Tag;

            return;

        }

        #endregion 

    }

}
