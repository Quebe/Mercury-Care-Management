using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CarePlan")]
    public class CarePlan : CoreConfigurationObject {
        
        #region Private Properties

        [DataMember (Name = "Goals")]
        private List<CarePlanGoal> goals = new List<CarePlanGoal> ();

        #endregion 


        #region Public Properties

        public List<CarePlanGoal> Goals { get { return goals; } set { goals = value; } }

        #endregion 


        #region Public Properties

        public override Application Application {

            set {

                base.Application = value;


                // PROPOGATE: SET ALL CHILD REFERENCES

                if (goals == null) { goals = new List<CarePlanGoal> (); }

                foreach (CarePlanGoal currentCarePlanGoal in goals) { currentCarePlanGoal.Application = value; }

            }

        }

        #endregion


        #region Constructors

        protected CarePlan () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlan (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public CarePlan (Application applicationReference, Int64 forId) {

            BaseConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion 
        
        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            // NO OBJECT SPECIFIC PROPERTIES, BELOW IS JUST AS AN EXAMPLE

            // CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareMeasureDomainId", CareMeasureDomainId.ToString ());

            #endregion


            #region Goals

            System.Xml.XmlNode carePlanGoalsNode = document.CreateElement ("CarePlanGoals");

            document.LastChild.AppendChild (carePlanGoalsNode);

            foreach (CarePlanGoal currentCarePlanGoal in goals) {

                carePlanGoalsNode.AppendChild (document.ImportNode (currentCarePlanGoal.XmlSerialize ().LastChild, true));

            }

            #endregion

            
            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);


            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Properties":

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].InnerText) {
               
                                    default: break;

                                }

                            }

                            break;

                        case "CarePlanGoals":

                            foreach (System.Xml.XmlNode currentCarePlanGoalNode in currentNode.ChildNodes) {

                                CarePlanGoal carePlanGoal = new CarePlanGoal (application);

                                response.AddRange (carePlanGoal.XmlImport (currentCarePlanGoalNode));

                                goals.Add (carePlanGoal);

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // SAVE IMPORTED CLASS

                if (!Save ()) { throw new ApplicationException ("Unable to save Care Plan: " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation

        public override Dictionary<string, string> Validate () {
            
            Dictionary <String, String> validationResponse = base.Validate();


            // FOR EXISTING, IF DISABLING THE CARE PLAN, ENSURE THAT THERE ARE NO DEPENDENCIES 

            if ((!Enabled) && (Id != 0)) {

                var problemStatements =

                    from problemStatement in application.ProblemStatementsAvailable ()

                    where (problemStatement.DefaultCarePlanId == Id)

                        && (problemStatement.Enabled)

                    select problemStatement;


                if (problemStatements.Distinct ().Count () > 0) { 

                    validationResponse.Add ("Enabled", "Cannot disable Care Plan; it has a dependent Problem Statement.");

                }

            }                               

            return validationResponse;
            
        }

        #endregion 


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            StringBuilder selectStatement = new StringBuilder ();

            System.Data.DataTable dataTable;


            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement.Append ("SELECT * FROM dbo.CarePlan WHERE CarePlanId = " + forId.ToString ());

            dataTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (dataTable.Rows.Count == 1) {

                MapDataFields (dataTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectGoals = "SELECT * FROM dbo.CarePlanGoal WHERE CarePlanId = " + forId.ToString ();

                dataTable = application.EnvironmentDatabase.SelectDataTable (selectGoals.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    CarePlanGoal carePlanGoal = new CarePlanGoal (application, Convert.ToInt64 (currentRow["CarePlanGoalId"]));

                    goals.Add (carePlanGoal);

                }


                //String selectInterventions = "SELECT * FROM dbo.CarePlanIntervention WHERE CarePlanId = " + forId.ToString ();

                //dataTable = application.EnvironmentDatabase.SelectDataTable (selectInterventions.ToString (), 0);

                //foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                //    CarePlanIntervention carePlanIntervention = new CarePlanIntervention (application, Convert.ToInt64 (currentRow["CarePlanInterventionId"]));

                //    interventions.Add (carePlanIntervention);

                //}

                
            }

            return success;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            String childIds = String.Empty;

            String deleteStatement = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CarePlanManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

                application.EnvironmentDatabase.BeginTransaction ();


                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CarePlan_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@carePlanDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@extendedProperties", ExtendedPropertiesXml);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@enabled", Enabled);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@visible", Visible);


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


                #region Save Goals

                foreach (CarePlanGoal currentCarePlanGoal in goals) {

                    if (currentCarePlanGoal.Id != 0) {

                        childIds += currentCarePlanGoal.Id.ToString () + ", ";

                    }

                }


                childIds += "0";

                deleteStatement = "DELETE FROM CarePlanGoal WHERE CarePlanId = " + Id.ToString () + " AND CarePlanGoalId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);


                foreach (CarePlanGoal currentCarePlanGoal in goals) {

                    currentCarePlanGoal.CarePlanId = Id;

                    currentCarePlanGoal.Application = application;

                    success = currentCarePlanGoal.Save ();

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                }

                #endregion 

                
                success = true;

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

    }

}
