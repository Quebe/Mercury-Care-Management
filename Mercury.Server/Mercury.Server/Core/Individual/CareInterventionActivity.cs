using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {

    [DataContract (Name = "CareInterventionActivity")]
    public class CareInterventionActivity : Activity.Activity {
        
        #region Private Properties

        [DataMember (Name = "CareInterventionId")]
        private Int64 careInterventionId = 0;

        [DataMember (Name = "CareInterventionActivityType")]
        private Enumerations.CareInterventionActivityType careInterventionActivityType = Enumerations.CareInterventionActivityType.Intervention;

        [DataMember (Name = "ClinicalNarrative")]
        private String clinicalNarrative;

        [DataMember (Name = "CommonNarrative")]
        private String commonNarrative;

        #endregion


        #region Public Properties

        public Int64 CareInterventionId { get { return careInterventionId; } set { careInterventionId = value; } }

        public Enumerations.CareInterventionActivityType CareInterventionActivityType { get { return careInterventionActivityType; } set { careInterventionActivityType = value; } }

        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description8000); } }

        #endregion


        #region Constructors

        protected CareInterventionActivity () { /* DO NOTHING, FOR INHERITANCE */ }

        public CareInterventionActivity (Application applicationReference) {

            ObjectConstructor (applicationReference);
            
            return;  
        
        }

        public CareInterventionActivity (Application applicationReference, Int64 forId) {

            ObjectConstructor (applicationReference);

            base.BaseConstructor (applicationReference, forId);

            return;

        }

        #endregion


        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareInterventionActivityTypeInt32", ((Int32)CareInterventionActivityType).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CareInterventionActivityType", CareInterventionActivityType.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ClinicalNarrative", ClinicalNarrative);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CommonNarrative", CommonNarrative);

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

                                    case "CareInterventionActivityTypeInt32": CareInterventionActivityType = (Enumerations.CareInterventionActivityType)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ClinicalNarrative": ClinicalNarrative = currentPropertyNode.InnerText; break;

                                    case "CommonNarrative": CommonNarrative = currentPropertyNode.InnerText; break; 

                                    default: break;

                                }

                            }

                            break;
                            
                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                // DO NOT SAVE CHILD OBJECT OF CAREINTERVENTION (WHERE SAVE OCCURS)

                // if (!Save ()) { throw new ApplicationException ("Unable to save Care Intervention: " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation

        public override Dictionary<string, string> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> ();

            // OVERRIDE BASE AND DO NOT CALL, THIS IS TO SKIP THE NAME/DESCRIPTION CHECK


            if (String.IsNullOrWhiteSpace (clinicalNarrative)) { validationResponse.Add ("Clinical Narrative", "Empty or NULL."); }

            if (String.IsNullOrWhiteSpace (commonNarrative)) { validationResponse.Add ("Common Narrative", "Empty or NULL."); }


            return validationResponse;

        }

        #endregion 


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = base.Load (forId);


            if (success) {

                // LOAD CHILD OBJECTS 

                String selectActivities = "SELECT * FROM dbo.CareInterventionActivityThreshold WHERE CareInterventionActivityId = " + forId.ToString ();

                System.Data.DataTable dataTable = application.EnvironmentDatabase.SelectDataTable (selectActivities.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    Activity.ActivityThreshold threshold = new Activity.ActivityThreshold (application);

                    threshold.MapDataFields (currentRow);

                    Thresholds.Add (threshold);

                }


            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            careInterventionId = base.IdFromSql (currentRow, "CareInterventionId"); // THIS MAY NOT EXIST IF AN INHERIT OBJECT IS CALLING BASE

            careInterventionActivityType = (Enumerations.CareInterventionActivityType)Convert.ToInt32 (currentRow["CareInterventionActivityType"]);

            clinicalNarrative = (String)currentRow["ClinicalNarrative"];

            commonNarrative = (String)currentRow["CommonNarrative"];


            return;

        }

        public override Boolean Save () {

            Boolean useTransaction = (application.EnvironmentDatabase.OpenTransactions == 0); // NO NESTED TRANSACTIONS

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CareInterventionManage)) { throw new ApplicationException ("Permission Denied."); }

            Dictionary<String, String> validationResponse = Validate ();

            if (validationResponse.Count != 0) {

                foreach (String validationKey in validationResponse.Keys) {

                    throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                }

            }

            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);

            try {

                if (useTransaction) { application.EnvironmentDatabase.BeginTransaction (); }




                System.Data.IDbCommand sqlCommand = application.EnvironmentDatabase.CreateCommand ("CareInterventionActivity_InsertUpdate");

                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;




                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionActivityId", Id);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionActivityName", Name, Server.Data.DataTypeConstants.Name);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionActivityDescription", Description, Server.Data.DataTypeConstants.Description);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionId", CareInterventionId);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@careInterventionActivityType", ((Int32)CareInterventionActivityType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@clinicalNarrative", ClinicalNarrative, Server.Data.DataTypeConstants.Description8000);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@commonNarrative", CommonNarrative, Server.Data.DataTypeConstants.Description8000);


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@activityType", ((Int32)CareInterventionActivityType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@isReoccurring", Reoccurring);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@initialAnchorDate", ((Int32)InitialAnchorDate));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@anchorDate", ((Int32)AnchorDate));


                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scheduleType", ((Int32)ScheduleType));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scheduleValue", ScheduleValue);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@scheduleQualifier", ((Int32)ScheduleQualifier));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@constraintValue", ConstraintValue);

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@constraintQualifier", ((Int32)ConstraintQualifier));

                application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@performActionDate", ((Int32)PerformActionDate));


                if (Action == null) {

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionId", ((Int64?)null));

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionParameters", (String)null, 0);

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionDescription", (String)null, 0);

                }

                else {

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionId", Action.Id);

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionParameters", Action.ActionParametersXml);

                    application.EnvironmentDatabase.AppendCommandParameter (sqlCommand, "@actionDescription", Action.Description, Server.Data.DataTypeConstants.Description99);

                }


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


                String deleteStatement = "DELETE FROM CareInterventionActivityThreshold WHERE CareInterventionActivityId = " + Id.ToString ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);


                foreach (Activity.ActivityThreshold currentThreshold in Thresholds) {

                    currentThreshold.ActivityId = Id;

                    currentThreshold.Application = application;


                    sqlStatement = new StringBuilder ();

                    sqlStatement.Append ("EXEC dbo.CareInterventionActivityThreshold_InsertUpdate ");

                    sqlStatement.Append (currentThreshold.Id.ToString () + ", ");

                    sqlStatement.Append ("'" + currentThreshold.Name.Replace ("'", "''") + "', ");

                    sqlStatement.Append ("'" + currentThreshold.Description.Replace ("'", "''") + "', ");

                    sqlStatement.Append (Id.ToString () + ", ");


                    sqlStatement.Append (currentThreshold.RelativeDateValue.ToString () + ", ");

                    sqlStatement.Append (((Int32)currentThreshold.RelativeDateQualifier).ToString () + ", ");

                    sqlStatement.Append (((Int32)currentThreshold.Status).ToString () + ", ");


                    if (Action == null) { sqlStatement.Append ("NULL, NULL, NULL, "); }

                    else {

                        sqlStatement.Append (Action.Id.ToString () + ", ");

                        sqlStatement.Append ("'" + Action.ActionParametersXmlSqlParsedString + "', ");

                        sqlStatement.Append ("'" + Action.Description + "', ");

                    }


                    sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                    success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                    currentThreshold.SetIdentity ();

                }


                success = true;

                if (useTransaction) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (useTransaction) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }
   
        #endregion

    }

}
