using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Condition {
    
    [DataContract (Name = "Condition")]
    public class Condition : CoreConfigurationObject {
        
        #region Private Properties

        [DataMember (Name = "ConditionClassId")]
        private Int64 conditionClassId;

        [DataMember (Name = "ConditionClassName")]
        private String conditionClassName = String.Empty;


        [DataMember (Name = "DemographicCriteria")]
        private List<ConditionCriteria.ConditionCriteriaDemographic> demographicCriteria = new List<Mercury.Server.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic> ();

        [DataMember (Name = "EventCriteria")]
        private List<ConditionCriteria.ConditionCriteriaEvent> eventCriteria = new List<Mercury.Server.Core.Condition.ConditionCriteria.ConditionCriteriaEvent> ();

        [DataMember (Name = "Events")]
        private Dictionary<String, Core.Action.Action> events = new Dictionary<string, Mercury.Server.Core.Action.Action> ();


        private Boolean processDebug = false;

        private Int64 processLogId = 0;

        private Int64 processStepId = 0;


        private ConditionClass conditionClass = null;

        #endregion 


        #region Public Properties

        public Int64 ConditionClassId { get { return conditionClassId; } set { conditionClassId = value; conditionClass = null; } }


        public List<ConditionCriteria.ConditionCriteriaDemographic> DemographicCriteria { get { return demographicCriteria; } set { demographicCriteria = value; } }

        public List<ConditionCriteria.ConditionCriteriaEvent> EventCriteria { get { return eventCriteria; } set { eventCriteria = value; } }

        public Dictionary<String, Core.Action.Action> Events { get { return events; } set { events = value; } }


        public override Application Application {
            
            set {
                
                base.Application = value;


                // PROPOGATE: SET ALL CHILD REFERENCES

                foreach (ConditionCriteria.ConditionCriteriaDemographic currentCriteria in DemographicCriteria) { currentCriteria.Application = value; }

                foreach (ConditionCriteria.ConditionCriteriaEvent currentCriteria in EventCriteria) { currentCriteria.Application = value; }

                foreach (String eventName in events.Keys) { events[eventName].Application = value; }

            }

        }

        public ConditionClass ConditionClass {

            get {

                if (conditionClass != null) { return conditionClass; }

                if (application == null) { return null; }

                conditionClass = application.ConditionClassGet (conditionClassId);

                return conditionClass;

            }

        }
        

        public Boolean HasEventCriteriaExcludedService {

            get {

                Boolean hasExcludedService = false;

                foreach (ConditionCriteria.ConditionCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Condition.Enumerations.ConditionCriteriaEventType.Exclusion) {

                        hasExcludedService = true;

                        break;

                    }

                }

                return hasExcludedService;

            }

        }

        public Boolean HasEventCriteriaIdentifyingService {

            get {

                Boolean hasIdentifyingService = false;

                foreach (ConditionCriteria.ConditionCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Condition.Enumerations.ConditionCriteriaEventType.Identifying) {

                        hasIdentifyingService = true;

                        break;

                    }

                }

                return hasIdentifyingService;

            }

        }

        public Boolean HasEventCriteriaTerminatingService {

            get {

                Boolean hasTerminatingService = false;

                foreach (ConditionCriteria.ConditionCriteriaEvent currentCriteria in eventCriteria) {

                    if (currentCriteria.EventType == Mercury.Server.Core.Condition.Enumerations.ConditionCriteriaEventType.Terminating) {

                        hasTerminatingService = true;

                        break;

                    }

                }

                return hasTerminatingService;

            }

        }

        #endregion


        #region Constructors

        private void ConditionBaseConstructor (Application applicationReference) {

            base.BaseConstructor (applicationReference);

            events.Clear ();


            return;

        }

        public Condition (Application applicationReference) { ConditionBaseConstructor (applicationReference); return; }

        public Condition (Application applicationReference, Int64 forConditionId) {

            ConditionBaseConstructor (applicationReference);

            BaseConstructor (applicationReference, forConditionId);

            return;

        }

        #endregion

        
        #region XML Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];


            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConditionClassId", conditionClassId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ConditionClassName", ((ConditionClass != null) ? ConditionClass.Name : String.Empty));

            #endregion

            
            #region Criteria

            System.Xml.XmlNode criteriaNode = document.CreateElement ("ConditionCriteria");

            document.LastChild.AppendChild (criteriaNode);

            foreach (ConditionCriteria.ConditionCriteriaDemographic currentDemographicCriteria in demographicCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentDemographicCriteria.XmlSerialize ().LastChild, true));

            }

            foreach (ConditionCriteria.ConditionCriteriaEvent currentEventCriteria in eventCriteria) {

                criteriaNode.AppendChild (document.ImportNode (currentEventCriteria.XmlSerialize ().LastChild, true));

            }

            #endregion 


            #region Condition Events

            System.Xml.XmlNode conditionEventNode = document.CreateElement ("ConditionEvents");

            document.LastChild.AppendChild (conditionEventNode);

            foreach (String currentConditionEventName in events.Keys) {

                if (events[currentConditionEventName] != null) {

                    System.Xml.XmlNode conditionEventActionNode = document.CreateElement (currentConditionEventName);

                    conditionEventActionNode.AppendChild (document.ImportNode (events[currentConditionEventName].XmlSerialize ().LastChild, true)); 

                    conditionEventNode.AppendChild (conditionEventActionNode);

                }

            }

            #endregion 

            
            #region Dependencies Nodes

            System.Xml.XmlNode dependenciesNode = document.CreateElement ("Dependencies");

            document.LastChild.InsertBefore (dependenciesNode, propertiesNode);

            if (ConditionClass != null) { dependenciesNode.AppendChild (document.ImportNode (ConditionClass.XmlSerialize ().LastChild, true)); }

            #endregion 


            return document;

        }

        
        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            try {

                foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                    switch (currentNode.Name) {

                        case "Dependencies":

                            #region Dependencies

                            foreach (System.Xml.XmlNode currentDependencyNode in currentNode.ChildNodes) {

                                switch (currentDependencyNode.Name) {

                                    case "ConditionClass":

                                        ConditionClass conditionClass = application.ConditionClassGet (currentDependencyNode.Attributes[Name].Value);

                                        if (conditionClass == null) {

                                            conditionClass = new Core.Condition.ConditionClass (application);

                                            response.AddRange (conditionClass.XmlImport (currentDependencyNode));

                                        }

                                        break;

                                }

                            }

                            #endregion 

                            break;

                        case "Properties":

                            #region Properties

                            foreach (System.Xml.XmlNode currentPropertyNode in currentNode.ChildNodes) {

                                switch (currentPropertyNode.Attributes["Name"].Value) {

                                    case "ConditionClassName":

                                        conditionClassId = application.ConditionClassGetIdByName (currentPropertyNode.InnerText);

                                        if (conditionClassId == 0) { throw new ApplicationException ("Unable to retreive Condition Type: " + currentPropertyNode.InnerText); }

                                        break;

                                }

                            }

                            #endregion 

                            break;

                        case "ConditionCriteria":

                            #region Condition Criteria

                            foreach (System.Xml.XmlNode currentCriteriaNode in currentNode.ChildNodes) {

                                switch (currentCriteriaNode.Name) {


                                    case "ConditionCriteriaDemographic":

                                        ConditionCriteria.ConditionCriteriaDemographic conditionCriteriaDemographic = new ConditionCriteria.ConditionCriteriaDemographic (application);

                                        response.AddRange (conditionCriteriaDemographic.XmlImport (currentCriteriaNode));

                                        demographicCriteria.Add (conditionCriteriaDemographic);

                                        break;

                                    case "ConditionCriteriaEvent":

                                        ConditionCriteria.ConditionCriteriaEvent conditionCriteriaEvent = new ConditionCriteria.ConditionCriteriaEvent (application);

                                        response.AddRange (conditionCriteriaEvent.XmlImport (currentCriteriaNode));

                                        eventCriteria.Add (conditionCriteriaEvent);

                                        break;

                                }

                            }

                            #endregion

                            break;

                        case "ConditionEvents":

                            #region Condition Events

                            foreach (System.Xml.XmlNode currentEventNode in currentNode.ChildNodes) {

                                String eventName = currentEventNode.Name;

                                Core.Action.Action eventAction = new Action.Action (application);

                                response.AddRange (eventAction.XmlImport (currentEventNode.FirstChild));

                                if (events.ContainsKey (eventName)) { events.Remove (eventName); }

                                events.Add (eventName, eventAction);

                            }

                            #endregion

                            break;

                    } // switch (currentNode.Name) {

                } // foreach (System.Xml.XmlNode currentNode in objectNode.ChildNodes) {

                // SAVE IMPORTED OBJECT 

                if (!Save ()) { throw new ApplicationException ("Unable to save " + ObjectType + ": " + Name + "."); }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion


        #region Data Functions

        public override Boolean Load (Int64 forId) {

            Boolean success = false;

            String selectStatement = String.Empty;

            System.Data.DataTable conditionTable;

            System.Data.DataTable criteriaTable;



            if (application.EnvironmentDatabase == null) { return false; }

            selectStatement += "SELECT Condition.*, ";

            selectStatement += "    ConditionClass.ConditionClassName";

            selectStatement += "  FROM Condition ";

            selectStatement += "    LEFT JOIN ConditionClass ON Condition.ConditionClassId = ConditionClass.ConditionClassId";

            selectStatement += "    WHERE ConditionId = " + forId.ToString ();

            conditionTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            if (conditionTable.Rows.Count == 1) {

                MapDataFields (conditionTable.Rows[0]);

                success = true;

            }

            else { success = false; }


            if (success) {


                selectStatement = "SELECT * FROM dbo.ConditionCriteriaDemographic WHERE ConditionId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    ConditionCriteria.ConditionCriteriaDemographic criteria = new Mercury.Server.Core.Condition.ConditionCriteria.ConditionCriteriaDemographic (application);

                    criteria.MapDataFields (currentRow);

                    demographicCriteria.Add (criteria);

                }
                
                selectStatement = "SELECT * FROM dbo.ConditionCriteriaEvent WHERE ConditionId = " + forId;

                criteriaTable = application.EnvironmentDatabase.SelectDataTable (selectStatement, 0);

                foreach (System.Data.DataRow currentRow in criteriaTable.Rows) {

                    ConditionCriteria.ConditionCriteriaEvent criteria = new Mercury.Server.Core.Condition.ConditionCriteria.ConditionCriteriaEvent (application);

                    criteria.MapDataFields (currentRow);

                    eventCriteria.Add (criteria);

                }
                
            }

            return success;

        }

        override public void MapDataFields (System.Data.DataRow currentRow) {

            base.MapDataFields (currentRow);


            conditionClassId = base.IdFromSql (currentRow, "ConditionClassId");

            conditionClassName = (String) currentRow["ConditionClassName"];

            
            //try {

            //    events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value = (IdFromSql (currentRow, "OnBeforeMembershipAddWorkflowId")).ToString ();

            //    events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].ValueDescription = application.CoreObjectGetNameById ("Workflow", Convert.ToInt64 (events["OnBeforeMembershipAdd"].ActionParameters["Workflow"].Value));

            //}

            //catch { /* DO NOTHING */ }



            //try { events["OnMembershipAdd"].MapDataFields ("OnMembershipAdd", currentRow); }

            //catch { /* DO NOTHING */ }


            //try {

            //    events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value = (IdFromSql (currentRow, "OnBeforeMembershipTerminateWorkflowId")).ToString ();

            //    events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].ValueDescription = application.CoreObjectGetNameById ("Workflow", Convert.ToInt64 (events["OnBeforeMembershipTerminate"].ActionParameters["Workflow"].Value));

            //}

            //catch { /* DO NOTHING */ }

            //try { events["OnMembershipTerminate"].MapDataFields ("OnMembershipTerminate", currentRow); }

            //catch { /* DO NOTHING */ }

            
            return;

        }

        public override Boolean Save () {

            Boolean success = false;

            StringBuilder sqlStatement;
            
            String criteriaIds;

            if (!application.HasEnvironmentPermission (Server.EnvironmentPermissions.ConditionManage)) { throw new ApplicationException ("Permission Denied"); }


            modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);
            

            try {

                application.EnvironmentDatabase.BeginTransaction ();



                conditionClassId = application.ConditionClassGetIdByName (conditionClassName);

                if (conditionClassId == 0) {

                    ConditionClass conditionClass = new ConditionClass (application);

                    conditionClass.Name = conditionClassName;

                    conditionClass.Description = conditionClassName;

                    conditionClass.Enabled = true;

                    conditionClass.Visible = true;

                    conditionClass.Save ();

                    conditionClassId = conditionClass.Id;

                }


                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.Condition_InsertUpdate ");

                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (conditionClassId.ToString () + ", ");
                
                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                //sqlStatement.Append (IdSqlAllowNull (OnBeforeMembershipAddWorkflowId) + ", ");

                //sqlStatement.Append (events["OnMembershipAdd"].Id.ToString () + ", ");

                //sqlStatement.Append ("'" + events["OnMembershipAdd"].ActionParametersXmlSqlParsedString + "', ");

                //sqlStatement.Append ("'" + events["OnMembershipAdd"].Description + "', ");


                //sqlStatement.Append (IdSqlAllowNull (OnBeforeMembershipTerminateWorkflowId) + ", ");

                //sqlStatement.Append (events["OnMembershipTerminate"].Id.ToString () + ", ");

                //sqlStatement.Append ("'" + events["OnMembershipTerminate"].ActionParametersXmlSqlParsedString + "', ");

                //sqlStatement.Append ("'" + events["OnMembershipTerminate"].Description + "', ");

                
                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");
                
                sqlStatement.Append ("'" + modifiedAccountInfo.SecurityAuthorityNameSql + "', '" + modifiedAccountInfo.UserAccountIdSql + "', '" + modifiedAccountInfo.UserAccountNameSql + "'");


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                SetIdentity ();
                

                #region Demographic Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (ConditionCriteria.ConditionCriteriaDemographic currentCriteria in demographicCriteria) {

                        currentCriteria.ConditionId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM ConditionCriteriaDemographic WHERE ConditionId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND ConditionCriteriaDemographicId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion


                #region Event Criteria

                if (success) {

                    criteriaIds = String.Empty;

                    foreach (ConditionCriteria.ConditionCriteriaEvent currentCriteria in eventCriteria) {

                        currentCriteria.ConditionId = id;

                        success = success && currentCriteria.Save (application);

                        criteriaIds = criteriaIds + currentCriteria.Id.ToString () + ",";

                        if (!success) { break; }

                    }

                    if (success) {

                        sqlStatement = new StringBuilder ("DELETE FROM ConditionCriteriaEvent WHERE ConditionId = " + id.ToString ());

                        if (criteriaIds.Length != 0) {

                            criteriaIds = criteriaIds.Substring (0, criteriaIds.Length - 1);

                            sqlStatement.Append (" AND ConditionCriteriaEventId NOT IN (" + criteriaIds + ")");

                        }

                        success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                    }

                }

                #endregion

                
                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }


                success = true;

                application.EnvironmentDatabase.CommitTransaction ();

            }

            catch (Exception applicationException) {

                success = false;

                application.EnvironmentDatabase.RollbackTransaction();

                application.SetLastException (applicationException);

            }

            return success;

        }

        virtual public Boolean Delete (Mercury.Server.Application application) {

            Boolean success = false;
            
            try {

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("EXEC Condition_Delete " + id.ToString (), 0);

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;
        }

        #endregion
        
    }

}
