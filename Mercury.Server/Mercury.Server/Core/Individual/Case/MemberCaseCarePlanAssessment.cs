using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {
 
    [DataContract (Name = "MemberCaseCarePlanAssessment")]
    public class MemberCaseCarePlanAssessment : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanId")]
        private Int64 memberCaseCarePlanId = 0;

        [DataMember (Name = "AssessmentDate")]
        private DateTime assessmentDate = DateTime.Now;

        [DataMember (Name = "Measures")]
        private List<MemberCaseCarePlanAssessmentCareMeasure> measures = new List<MemberCaseCarePlanAssessmentCareMeasure> ();

        private MemberCaseCarePlan memberCaseCarePlan = null;

        #endregion


        #region Public Properties

        public Int64 MemberCaseCarePlanId { get { return memberCaseCarePlanId; } set { memberCaseCarePlanId = value; } }

        public DateTime AssessmentDate { get { return assessmentDate; } set { assessmentDate = value; } }

        public List<MemberCaseCarePlanAssessmentCareMeasure> Measures { get { return measures; } set { measures = value; } }

        public MemberCaseCarePlan MemberCaseCarePlan { get { return memberCaseCarePlan; } set { memberCaseCarePlan = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                if (measures == null) { measures = new List<MemberCaseCarePlanAssessmentCareMeasure> (); }

                foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentCareMeasure in measures) {

                    currentAssessmentCareMeasure.Application = value;

                    currentAssessmentCareMeasure.MemberCaseCarePlanAssessment = this;

                }

            }

        }

        #endregion


        #region Constructors

        public MemberCaseCarePlanAssessment (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessment (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions
        
        public override Boolean LoadChildObjects () {

            Boolean success = true;


            // LOAD CHILD OBJECTS 

            String selectStatement = "SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasure WHERE MemberCaseCarePlanAssessmentId = " + Id.ToString ();

            System.Data.DataTable dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                MemberCaseCarePlanAssessmentCareMeasure measure = new MemberCaseCarePlanAssessmentCareMeasure (application);

                measure.MapDataFields (currentRow);

                measure.LoadChildObjects ();

                measure.MemberCaseCarePlanAssessment = this;

                measures.Add (measure);

            }


            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);

            MemberCaseCarePlanId = base.IdFromSql (currentRow, "MemberCaseCarePlanId");

            AssessmentDate = Convert.ToDateTime (currentRow["AssessmentDate"]);

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

                application.EnvironmentDatabase.BeginTransaction ();


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberCaseCarePlanAssessment_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanId", MemberCaseCarePlanId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@assessmentDate", AssessmentDate);

                
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


                #region Save Care Measures

                // CREATE LIST OF CHILD IDS

                foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentCareMeasure in measures) {

                    if (currentAssessmentCareMeasure.Id != 0) { childIds += currentAssessmentCareMeasure.Id.ToString () + ", "; }

                }

                childIds += "0";

                String deleteStatement = "DELETE FROM MemberCaseCarePlanAssessmentCareMeasure WHERE MemberCaseCarePlanAssessmentId = " + Id.ToString () + " AND MemberCaseCarePlanAssessmentCareMeasureId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement); // REMOVE ORPHANED CARE MEASURES (THIS WILL NOT WORK UNLESS YOU REMOVE THE CHILD OBJECTS, TOO

                // TODO: FIX THE DELETE STATEMENT



                foreach (MemberCaseCarePlanAssessmentCareMeasure currentAssessmentCareMeasure in measures) {

                    currentAssessmentCareMeasure.MemberCaseCarePlanAssessmentId = Id;

                    currentAssessmentCareMeasure.Application = application;

                    success = currentAssessmentCareMeasure.Save ();

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                }

                #endregion



                // UPDATE VALUES IN MEMBER CASE CARE PLAN GOALS (SUMMARY INFORMATION)

                sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberCaseCarePlanAssessment_UpdateValues");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanId", MemberCaseCarePlanId);

                success = (sqlCommand.ExecuteNonQuery () > 0);


                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction ();

                application.SetLastException (applicationException);

            }

            return success;

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
