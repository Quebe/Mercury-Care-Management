using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization; 
using System.Text;

namespace Mercury.Server.Core.Individual.Case {
    
    [DataContract (Name = "MemberCaseCarePlanAssessmentCareMeasureComponent")]
    public class MemberCaseCarePlanAssessmentCareMeasureComponent : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanAssessmentCareMeasureId")]
        private Int64 memberCaseCarePlanAssessmentCareMeasureId = 0;

        private MemberCaseCarePlanAssessmentCareMeasure memberCaseCarePlanAssessmentCareMeasure = null; // LOCAL REFERENCE ONLY

        [DataMember (Name = "CareMeasureComponentId")]
        private Int64 careMeasureComponentId = 0;

        [DataMember (Name = "CareMeasureScaleId")]
        private Int64 careMeasureScaleId = 0;

        [DataMember (Name = "Tag")]
        private String tag = String.Empty;

        [DataMember (Name = "ComponentValue")]
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

        public CareMeasureScale CareMeasureScale { get { return application.CareMeasureScaleGet (careMeasureScaleId); } }

        public String CareMeasureScaleName { get { return ((CareMeasureScale != null) ? CareMeasureScale.Name : String.Empty); } }

        #endregion


        #region Constructors

        public MemberCaseCarePlanAssessmentCareMeasureComponent (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessmentCareMeasureComponent (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions
        
        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            Name = base.StringFromSql (currentRow, "CareMeasureComponentName");

            MemberCaseCarePlanAssessmentCareMeasureId = base.IdFromSql (currentRow, "MemberCaseCarePlanAssessmentCareMeasureId");

            CareMeasureComponentId = base.IdFromSql (currentRow, "CareMeasureComponentId");

            CareMeasureScaleId = base.IdFromSql (currentRow, "CareMeasureScaleId");

            Tag = base.StringFromSql (currentRow, "Tag");

            ComponentValue = Convert.ToInt32 (currentRow["ComponentValue"]);


            return;

        }

        public override Dictionary<String, String> Validate () {

            return new Dictionary<String, String> ();

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberCaseCarePlanAssessmentCareMeasureComponent_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentCareMeasureComponentId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentCareMeasureId", MemberCaseCarePlanAssessmentCareMeasureId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureComponentId", CareMeasureComponentId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureComponentName", Name, Server.Data.DataTypeConstants.Description99);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureScaleId", CareMeasureScaleId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@tag", Tag, Server.Data.DataTypeConstants.UniqueId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@componentValue", ComponentValue);



                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAuthorityName", ModifiedAccountInfo.SecurityAuthorityName, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountId", ModifiedAccountInfo.UserAccountId, Server.Data.DataTypeConstants.UserAccountId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@modifiedAccountName", ModifiedAccountInfo.UserAccountName, Server.Data.DataTypeConstants.Name);


                success = (sqlCommand.ExecuteNonQuery () > 0);


                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                success = true;

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public Methods

        public void SetCareMeasureComponent (CareMeasureComponent forCareMeasureComponent) {

            Name = forCareMeasureComponent.Name;

            Description = forCareMeasureComponent.Description;

            careMeasureScaleId = forCareMeasureComponent.CareMeasureScaleId;

            Tag = forCareMeasureComponent.Tag;

            return;

        }

        #endregion 

    }

}
