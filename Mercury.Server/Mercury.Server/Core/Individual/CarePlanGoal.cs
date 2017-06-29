using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Individual {
    
    [DataContract (Name = "CarePlanGoal")]
    public class CarePlanGoal : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "CarePlanId")]
        private Int64 carePlanId = 0;

        [DataMember (Name = "Inclusion")]
        private Enumerations.CarePlanItemInclusion inclusion = Enumerations.CarePlanItemInclusion.Optional;


        [DataMember (Name = "ClinicalNarrative")]
        private String clinicalNarrative;

        [DataMember (Name = "CommonNarrative")]
        private String commonNarrative;


        [DataMember (Name = "GoalTimeframe")]
        private Enumerations.CarePlanGoalTimeframe goalTimeframe = Enumerations.CarePlanGoalTimeframe.ShortTerm;

        [DataMember (Name = "ScheduleValue")]
        private Int32 scheduleValue;

        [DataMember (Name = "ScheduleQualifier")]
        private Core.Enumerations.DateQualifier scheduleQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Months;

        [DataMember (Name = "CareMeasureId")]
        private Int64 careMeasureId = 0;

        [DataMember (Name = "Interventions")]
        private List<CarePlanIntervention> interventions = new List<CarePlanIntervention> ();
        
        #endregion 


        #region Public Properties - Encapsulated

        public Int64 CarePlanId { get { return carePlanId; } set { carePlanId = value; } }

        public Enumerations.CarePlanItemInclusion Inclusion { get { return inclusion; } set { inclusion = value; } }


        public String ClinicalNarrative { get { return clinicalNarrative; } set { clinicalNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }

        public String CommonNarrative { get { return commonNarrative; } set { commonNarrative = Server.CommonFunctions.SetValueMaxLength (value, Server.Data.DataTypeConstants.Description); } }


        public Enumerations.CarePlanGoalTimeframe GoalTimeframe { get { return goalTimeframe; } set { goalTimeframe = value; } }

        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Int64 CareMeasureId { get { return careMeasureId; } set { careMeasureId = value; } }

        public List<CarePlanIntervention> Interventions { get { return interventions; } set { interventions = value; } }

        public override Application Application {

            get { return base.Application; }

            set {

                base.Application = value;

                if (interventions == null) { interventions = new List<CarePlanIntervention> (); }

                foreach (CarePlanIntervention currentIntervention in interventions) {

                    currentIntervention.Application = value;

                    currentIntervention.CarePlanGoalId = Id;

                }

            }

        }

        #endregion 


        #region Public Properties
        
        public CareMeasure CareMeasure { get { return application.CareMeasureGet (careMeasureId, true); } }

        #endregion
        

        #region Constructors
        
        protected CarePlanGoal () { /* DO NOTHING, FOR INHERITANCE */ }

        public CarePlanGoal (Application applicationReference) {

            BaseConstructor (applicationReference);
            
            return;  
        
        }

        public CarePlanGoal (Application applicationReference, Int64 forId) {

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

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ClinicalNarrative", ClinicalNarrative);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "CommonNarrative", CommonNarrative);

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GoalTimeframeInt32", ((Int32)GoalTimeframe).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GoalTimeframe", GoalTimeframe.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleValue", ScheduleValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifierInt32", ((Int32)GoalTimeframe).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifier", GoalTimeframe.ToString ());

            #endregion
            

            #region Object Nodes

            document.LastChild.AppendChild (document.ImportNode (CareMeasure.XmlSerialize ().LastChild, true));

            #endregion


            #region Interventions

            System.Xml.XmlNode carePlanInterventionsNode = document.CreateElement ("CarePlanInterventions");

            document.LastChild.AppendChild (carePlanInterventionsNode);

            foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                carePlanInterventionsNode.AppendChild (document.ImportNode (currentCarePlanIntervention.XmlSerialize ().LastChild, true));

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

                                    case "ClinicalNarrative": ClinicalNarrative = currentPropertyNode.InnerText; break;

                                    case "CommonNarrative": CommonNarrative = currentPropertyNode.InnerText; break;

                                    case "GoalTimeframeInt32": GoalTimeframe = (Enumerations.CarePlanGoalTimeframe)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleValue": ScheduleValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    case "ScheduleQualifierInt32": ScheduleQualifier = (Core.Enumerations.DateQualifier) Convert.ToInt32 (currentPropertyNode.InnerText); break;

                                    default: break;

                                }

                            }

                            break;
                            
                        case "CareMeasure":

                            // USES THE DOMAIN ID CAPTURED UNDER PROPERTIES

                            String careMeasureName = currentNode.Attributes["Name"].InnerText;

                            CareMeasure careMeasure = application.CareMeasureGet (careMeasureName);

                            if (careMeasure == null) {

                                // DOES NOT EXIST, CREATE NEW FROM IMPORT

                                careMeasure = new CareMeasure (application);

                                response.AddRange (careMeasure.XmlImport (currentNode));

                                careMeasureId = careMeasure.Id;


                                if (careMeasureId == 0) {

                                    throw new ApplicationException ("Unable to import Care Measure : " + currentNode.Attributes["Name"].InnerText + ".");

                                }

                            }

                            else { careMeasureId = careMeasure.Id; }

                            break;

                        case "CarePlanInterventions":

                            foreach (System.Xml.XmlNode currentCarePlanInterventionNode in currentNode.ChildNodes) {

                                CarePlanIntervention carePlanIntervention = new CarePlanIntervention (application);

                                response.AddRange (carePlanIntervention.XmlImport (currentCarePlanInterventionNode));

                                interventions.Add (carePlanIntervention);

                            }

                            break;

                    } // switch (currentNode.Attributes["Name"].InnerText) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {


                
                // CHILD OBJECT THAT IS PART OF THE PARENT SAVE PROCESS

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Validation 

        public override Dictionary<string, string> Validate () {

            Dictionary<String, String> validationResponse = new Dictionary<string, string> (); // DO NOT USE BASE METHODS


            // VALIDATE NAME IS NOT NULL OR EMPTY

            if (String.IsNullOrWhiteSpace (Name)) { validationResponse.Add ("Name", "Empty or NULL."); }
                
            if (String.IsNullOrWhiteSpace (clinicalNarrative)) { validationResponse.Add ("Clinical Narrative", "Empty or NULL."); }

            if (String.IsNullOrWhiteSpace (commonNarrative)) { validationResponse.Add ("Common Narrative", "Empty or NULL."); }

            if (CareMeasure == null) { validationResponse.Add ("Care Measure", "Empty or NULL."); }


            return validationResponse;

        }

        #endregion 


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = base.Load (forId);


            if (success) {

                // LOAD CHILD OBJECTS 
                
                String selectInterventions = "SELECT * FROM dbo.CarePlanIntervention WHERE CarePlanGoalId = " + forId.ToString ();

                System.Data.DataTable dataTable = application.EnvironmentDatabase.SelectDataTable (selectInterventions.ToString (), 0);

                foreach (System.Data.DataRow currentRow in dataTable.Rows) {

                    CarePlanIntervention carePlanIntervention = new CarePlanIntervention (application, Convert.ToInt64 (currentRow["CarePlanInterventionId"]));

                    interventions.Add (carePlanIntervention);

                }


            }

            return success;

        }

        public override void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            carePlanId = base.IdFromSql (currentRow, "CarePlanId"); // THIS MAY NOT BE A VALID FIELD FOR OBJECTS DOWN THE INHERITANCE TREE, USE BASE 

            inclusion = (Enumerations.CarePlanItemInclusion)Convert.ToInt32 (currentRow["Inclusion"]);

            clinicalNarrative = (String)currentRow["ClinicalNarrative"];

            commonNarrative = (String)currentRow["CommonNarrative"];


            goalTimeframe = (Enumerations.CarePlanGoalTimeframe)Convert.ToInt32 (currentRow["GoalTimeframe"]);

            scheduleValue = Convert.ToInt32 (currentRow["ScheduleValue"]);

            scheduleQualifier = (Core.Enumerations.DateQualifier) Convert.ToInt32 (currentRow ["ScheduleQualifier"]);

            careMeasureId = base.IdFromSql (currentRow, "CareMeasureId");


            return;

        }

        public override Boolean Save () {

            Boolean usingTransaction = (application.EnvironmentDatabase.OpenTransactions == 0);

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            String childIds = String.Empty;


            try {

                if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.CarePlanManage)) { throw new ApplicationException ("Permission Denied."); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }

                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                if (usingTransaction) { application.EnvironmentDatabase.BeginTransaction (); }


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.CarePlanGoal_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");


                sqlStatement.Append (carePlanId.ToString () + ", ");

                sqlStatement.Append (((Int32)inclusion).ToString () + ", ");

                sqlStatement.Append ("'" + clinicalNarrative.Replace ("'", "''") + "', ");

                sqlStatement.Append ("'" + commonNarrative.Replace ("'", "''") + "', ");


                sqlStatement.Append (Convert.ToInt32 (goalTimeframe).ToString () + ", ");

                sqlStatement.Append (scheduleValue.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (scheduleQualifier).ToString () + ", ");

                sqlStatement.Append (careMeasureId.ToString () + ", ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                #region Save Interventions

                foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                    if (currentCarePlanIntervention.Id != 0) {

                        childIds += currentCarePlanIntervention.Id.ToString () + ", ";

                    }

                }


                childIds += "0";

                String deleteStatement = "DELETE FROM CarePlanIntervention WHERE CarePlanGoalId = " + Id.ToString () + " AND CarePlanInterventionId NOT IN (" + childIds + ")";

                success = application.EnvironmentDatabase.ExecuteSqlStatement (deleteStatement);


                foreach (CarePlanIntervention currentCarePlanIntervention in interventions) {

                    currentCarePlanIntervention.CarePlanGoalId = Id;

                    currentCarePlanIntervention.Application = application;

                    success = currentCarePlanIntervention.Save ();

                    if (!success) {

                        application.SetLastException (application.EnvironmentDatabase.LastException);

                        throw application.EnvironmentDatabase.LastException;

                    }

                }

                #endregion 

                

                success = true;

                if (usingTransaction) { application.EnvironmentDatabase.CommitTransaction (); }

            }

            catch (Exception applicationException) {

                success = false;

                if (usingTransaction) { application.EnvironmentDatabase.RollbackTransaction (); }

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion

    }

}
