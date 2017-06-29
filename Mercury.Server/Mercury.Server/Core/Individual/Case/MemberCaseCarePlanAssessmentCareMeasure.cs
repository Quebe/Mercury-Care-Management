using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual.Case {
    
    [DataContract (Name = "MemberCaseCarePlanAssessmentCareMeasure")]
    public class MemberCaseCarePlanAssessmentCareMeasure : CoreObject {

        #region Private Properties

        [DataMember (Name = "MemberCaseCarePlanAssessmentId")]
        private Int64 memberCaseCarePlanAssessmentId = 0;

        private MemberCaseCarePlanAssessment memberCaseCarePlanAssessment = null; // LOCAL PARENT REFEERENCE ONLY

        [DataMember (Name = "CareMeasureDomainId")]
        private Int64 careMeasureDomainId = 0;

        [DataMember (Name = "CareMeasureDomainName")]
        private String careMeasureDomainName = String.Empty;

        [DataMember (Name = "CareMeasureClassId")]
        private Int64 careMeasureClassId = 0;

        [DataMember (Name = "CareMeasureClassName")]
        private String careMeasureClassName = String.Empty;

        [DataMember (Name = "CareMeasureId")]
        private Int64 careMeasureId = 0;

        [DataMember (Name = "TargetValue")]
        private Decimal targetValue = 0;


        [DataMember (Name = "Goals")]
        private List<MemberCaseCarePlanAssessmentCareMeasureGoal> goals = new List<MemberCaseCarePlanAssessmentCareMeasureGoal> ();

        [DataMember (Name = "Components")]
        private List<MemberCaseCarePlanAssessmentCareMeasureComponent> components = new List<MemberCaseCarePlanAssessmentCareMeasureComponent> ();

        #endregion


        #region Public Properties - Encapsulated

        public Int64 MemberCaseCarePlanAssessmentId { get { return memberCaseCarePlanAssessmentId; } set { memberCaseCarePlanAssessmentId = value; } }

        public MemberCaseCarePlanAssessment MemberCaseCarePlanAssessment { get { return memberCaseCarePlanAssessment; } set { memberCaseCarePlanAssessment = value; } }


        public Int64 CareMeasureDomainId { get { return careMeasureDomainId; } set { careMeasureDomainId = value; } }

        public String CareMeasureDomainName { get { return careMeasureDomainName; } set { careMeasureDomainName = value; } }


        public Int64 CareMeasureClassId { get { return careMeasureClassId; } set { careMeasureClassId = value; } }

        public String CareMeasureClassName { get { return careMeasureClassName; } set { careMeasureClassName = value; } }


        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public Decimal TargetValue { get { return targetValue; } set { targetValue = ((value >= 0) && (value <= 5)) ? value : 0; } }


        public List<MemberCaseCarePlanAssessmentCareMeasureGoal> Goals { get { return goals; } set { goals = value; } }

        public List<MemberCaseCarePlanAssessmentCareMeasureComponent> Components { get { return components; } set { components = value; } }


        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;


                if (goals == null) { goals = new List<MemberCaseCarePlanAssessmentCareMeasureGoal> (); }

                foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentAssessmentCareMeasureGoal in goals) {

                    currentAssessmentCareMeasureGoal.Application = value;

                    currentAssessmentCareMeasureGoal.MemberCaseCarePlanAssessmentCareMeasure = this;

                }


                if (components == null) { components = new List<MemberCaseCarePlanAssessmentCareMeasureComponent> (); }

                foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentAssessmentCareMeasureComponent in components) {

                    currentAssessmentCareMeasureComponent.Application = value;

                    currentAssessmentCareMeasureComponent.MemberCaseCarePlanAssessmentCareMeasure = this;

                }

            }

        }

        #endregion


        #region Public Properties

        public List<CareMeasureScale> CareMeasureScales {

            get {

                List<CareMeasureScale> careMeasureScales =

                    (from currentComponent in components

                     orderby currentComponent.CareMeasureScaleName

                     select currentComponent.CareMeasureScale).Distinct ().ToList ();

                return careMeasureScales;

            }

        }

        public Decimal ComponentScore {

            get {

                Decimal componentScore = 0;

                Decimal componentTotal = 0;

                Int32 componentCount = 0;

                foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentComponent in components) {

                    if (currentComponent.ComponentValue > 0) {

                        componentCount = componentCount + 1;

                        componentTotal = componentTotal + currentComponent.ComponentValue;

                    }

                }

                if (componentCount > 0) { componentScore = componentTotal / componentCount; }

                return componentScore;

            }

        }

        #endregion


        #region Constructors

        public MemberCaseCarePlanAssessmentCareMeasure (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public MemberCaseCarePlanAssessmentCareMeasure (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region Data Functions

        public override Boolean LoadChildObjects () {

            Boolean success = true;

            String selectStatement;

            System.Data.DataTable dataTable;


            // LOAD CHILD OBJECTS 

            selectStatement = "SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasureGoal WHERE MemberCaseCarePlanAssessmentCareMeasureId = " + Id.ToString ();

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                MemberCaseCarePlanAssessmentCareMeasureGoal goal = new MemberCaseCarePlanAssessmentCareMeasureGoal (application);

                goal.MapDataFields (currentRow);

                goal.LoadChildObjects ();

                goal.MemberCaseCarePlanAssessmentCareMeasure = this;

                goals.Add (goal);

            }


            selectStatement = "SELECT * FROM dbo.MemberCaseCarePlanAssessmentCareMeasureComponent WHERE MemberCaseCarePlanAssessmentCareMeasureId = " + Id.ToString ();

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

            foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                MemberCaseCarePlanAssessmentCareMeasureComponent component = new MemberCaseCarePlanAssessmentCareMeasureComponent (application);

                component.MapDataFields (currentRow);

                component.LoadChildObjects ();

                component.MemberCaseCarePlanAssessmentCareMeasure = this;

                components.Add (component);

            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            MemberCaseCarePlanAssessmentId = base.IdFromSql (currentRow, "MemberCaseCarePlanAssessmentId");

            CareMeasureDomainId = base.IdFromSql (currentRow, "CareMeasureDomainId");

            CareMeasureClassId = base.IdFromSql (currentRow, "CareMeasureClassId");

            CareMeasureId = base.IdFromSql (currentRow, "CareMeasureId");

            TargetValue = Convert.ToDecimal (currentRow["MemberCaseCarePlanAssessmentId"]);


            return;

        }

        public override Dictionary<String, String> Validate () {

            return new Dictionary<String, String> ();

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String deleteStatement = String.Empty;

            String childIds = String.Empty;


            try {

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("MemberCaseCarePlanAssessmentCareMeasure_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentCareMeasureId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@memberCaseCarePlanAssessmentId", MemberCaseCarePlanAssessmentId);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureDomainId", CareMeasureDomainId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureClassId", CareMeasureClassId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careMeasureId", CareMeasureId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@targetValue", TargetValue);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@componentValue", ComponentScore);



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


                #region Save Care Measure Components

                // CREATE LIST OF CHILD IDS

                childIds = String.Empty;

                foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentAssessmentCareComponent in components) {

                    if (currentAssessmentCareComponent.Id != 0) {

                        // if (currentAssessmentCareComponent.ComponentValue != 0) { // ONLY KEEP THOSE ASSIGNED AN ACTUAL VALUE

                            childIds += currentAssessmentCareComponent.Id.ToString () + ", ";

                        // }
                    
                    }

                }

                childIds += "0";

                deleteStatement = "DELETE FROM MemberCaseCarePlanAssessmentCareMeasureComponent WHERE MemberCaseCarePlanAssessmentCareMeasureId = " + Id.ToString () + " AND MemberCaseCarePlanAssessmentCareMeasureComponentId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement); // REMOVE ORPHANED CARE MEASURES (THIS WILL NOT WORK UNLESS YOU REMOVE THE CHILD OBJECTS, TOO

                // TODO: FIX THE DELETE STATEMENT



                foreach (MemberCaseCarePlanAssessmentCareMeasureComponent currentAssessmentCareComponent in components) {

                    currentAssessmentCareComponent.MemberCaseCarePlanAssessmentCareMeasureId = Id;

                    currentAssessmentCareComponent.Application = application;

                    // if (currentAssessmentCareComponent.ComponentValue > 0) { // ONLY SAVE THOSE THAT HAVE AN ACTUAL VALUE ASSIGNED

                        success = currentAssessmentCareComponent.Save ();

                        if (!success) {

                            application.SetLastException (application.EnvironmentDatabase.LastException);

                            throw application.EnvironmentDatabase.LastException;

                        }

                    // }

                }

                #endregion

                #region Save Care Measure Goals

                // CREATE LIST OF CHILD IDS

                childIds = String.Empty;

                foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentAssessmentCareGoal in goals) {

                    if (currentAssessmentCareGoal.Id != 0) {

                        childIds += currentAssessmentCareGoal.Id.ToString () + ", ";

                    }

                }

                childIds += "0";

                deleteStatement = "DELETE FROM MemberCaseCarePlanAssessmentCareMeasureGoal WHERE MemberCaseCarePlanAssessmentCareMeasureId = " + Id.ToString () + " AND MemberCaseCarePlanAssessmentCareMeasureGoalId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement); // REMOVE ORPHANED CARE MEASURES (THIS WILL NOT WORK UNLESS YOU REMOVE THE CHILD OBJECTS, TOO

                // TODO: FIX THE DELETE STATEMENT



                foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentAssessmentCareGoal in goals) {

                    currentAssessmentCareGoal.MemberCaseCarePlanAssessmentCareMeasureId = Id;

                    currentAssessmentCareGoal.Application = application;

                    success = currentAssessmentCareGoal.Save ();

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;


                    }

                }

                #endregion


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

        public void SetCareMeasure (CareMeasure forCareMeasure) {

            careMeasureDomainId = forCareMeasure.CareMeasureDomainId;

            careMeasureDomainName = forCareMeasure.CareMeasureDomainName;

            careMeasureClassId = forCareMeasure.CareMeasureClassId;

            careMeasureClassName = forCareMeasure.CareMeasureClassName;

            careMeasureId = forCareMeasure.Id;

            Name = forCareMeasure.Name;

            Description = forCareMeasure.Description;


            // CREATE COMPONENTS HERE

            foreach (CareMeasureComponent currentComponent in forCareMeasure.Components) {

                MemberCaseCarePlanAssessmentCareMeasureComponent careMeasureComponent = new MemberCaseCarePlanAssessmentCareMeasureComponent (application);

                careMeasureComponent.MemberCaseCarePlanAssessmentCareMeasureId = Id;

                careMeasureComponent.MemberCaseCarePlanAssessmentCareMeasure = this;

                careMeasureComponent.SetCareMeasureComponent (currentComponent);

                components.Add (careMeasureComponent);

            }

            return;

        }

        public void AddMemberCaseCarePlanGoal (MemberCaseCarePlanGoal forMemberCaseCarePlanGoal) {

            foreach (MemberCaseCarePlanAssessmentCareMeasureGoal currentGoal in goals) {

                if (currentGoal.MemberCaseCarePlanGoalId == forMemberCaseCarePlanGoal.Id) { return; }

            }

            MemberCaseCarePlanAssessmentCareMeasureGoal goal = new MemberCaseCarePlanAssessmentCareMeasureGoal (application);

            goal.MemberCaseCarePlanAssessmentCareMeasureId = Id;

            goal.MemberCaseCarePlanAssessmentCareMeasure = this;

            goal.SetMemberCaseCarePlanGoal (forMemberCaseCarePlanGoal);

            goals.Add (goal);

            return;

        }

        public List<MemberCaseCarePlanAssessmentCareMeasureComponent> ComponentsByScale (Int64 forCareMeasureScaleId) {

            List<MemberCaseCarePlanAssessmentCareMeasureComponent> scaleComponents =

                (from currentComponent in components

                 where currentComponent.CareMeasureScaleId == forCareMeasureScaleId

                 select currentComponent).ToList ();

            return scaleComponents;

        }        

        #endregion 

    }

}
