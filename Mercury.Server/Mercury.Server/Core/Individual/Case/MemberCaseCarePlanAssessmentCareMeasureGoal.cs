using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {

    [DataContract (Name = "MemberCaseCarePlanAssessmentCareMeasureGoal")]
    public class MemberCaseCarePlanAssessmentCareMeasureGoal : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanAssessmentCareMeasureId")]
        private Int64 memberCaseCarePlanAssessmentCareMeasureId = 0;

        [DataMember (Name = "MemberCaseCarePlanGoalId")]
        private Int64 memberCaseCarePlanGoalId = 0;

        private MemberCaseCarePlanAssessmentCareMeasure memberCaseCarePlanAssessmentCareMeasure = null; // LOCAL REFERENCE ONLY

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

        public MemberCaseCarePlanAssessmentCareMeasureGoal (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseCarePlanAssessmentCareMeasureId = base.IdFromSql (currentRow, "MemberCaseCarePlanAssessmentCareMeasureId");

            MemberCaseCarePlanGoalId = base.IdFromSql (currentRow, "MemberCaseCarePlanGoalId");


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


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberCaseCarePlanAssessmentCareMeasureGoal_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentCareMeasureGoalId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentCareMeasureId", MemberCaseCarePlanAssessmentCareMeasureId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanGoalId", MemberCaseCarePlanGoalId);

                
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

        public void SetMemberCaseCarePlanGoal (MemberCaseCarePlanGoal forMemberCaseCarePlanGoal) {

            MemberCaseCarePlanGoalId = forMemberCaseCarePlanGoal.Id;

            return;

        }

        #endregion

    }

}
