using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Work {

    [Serializable]
    [DataContract (Name = "WorkQueue")]
    public class WorkQueue : CoreConfigurationObject {

        #region Private Properties

        [DataMember (Name = "WorkflowId")]
        private Int64 workflowId;
        

        [DataMember (Name = "ScheduleValue")]
        private Int32 scheduleValue;

        [DataMember (Name = "ScheduleQualifier")]
        private Core.Enumerations.DateQualifier scheduleQualifier = Core.Enumerations.DateQualifier.Days;

        [DataMember (Name = "ThresholdValue")]
        private Int32 thresholdValue;

        [DataMember (Name = "ThresholdQualifier")]
        private Core.Enumerations.DateQualifier thresholdQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Days;

        [DataMember (Name = "InitialConstraintValue")]
        private Int32 initialConstraintValue;

        [DataMember (Name = "InitialConstraintQualifier")]
        private Core.Enumerations.DateQualifier initialConstraintQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Days;

        [DataMember (Name = "InitialMilestoneValue")]
        private Int32 initialMilestoneValue;

        [DataMember (Name = "InitialMilestoneQualifier")]
        private Core.Enumerations.DateQualifier initialMilestoneQualifier = Mercury.Server.Core.Enumerations.DateQualifier.Days;


        [DataMember (Name = "GetWorkViewId")]
        private Int64 getWorkViewId = 0;

        [NonSerialized]
        private WorkQueueView getWorkView = null;

        [DataMember (Name = "GetWorkUseGrouping")]
        private Boolean getWorkUseGrouping = false;

        [DataMember (Name = "GetWorkUserViews")]
        private List<WorkQueueGetWorkUserView> getWorkUserViews = new List<WorkQueueGetWorkUserView> ();


        [NonSerialized]
        [DataMember (Name = "WorkTeams")]
        private List<WorkQueueTeam> workTeams = new List<WorkQueueTeam> ();


        #endregion


        #region Public Properties - Encapsulated

        public Int64 WorkflowId { get { return workflowId; } set { workflowId = value; } }

        public Workflow Workflow { get { return application.WorkflowGet (workflowId, true); } }

        public String WorkflowName {

            get {

                String workflowName = String.Empty;

                Workflow workflow = Workflow;
                
                if (Workflow != null) { workflowName = Workflow.Name; }

                return workflowName;

            }

        }


        public Int32 ScheduleValue { get { return scheduleValue; } set { scheduleValue = value; } }

        public Core.Enumerations.DateQualifier ScheduleQualifier { get { return scheduleQualifier; } set { scheduleQualifier = value; } }

        public Int32 ThresholdValue { get { return thresholdValue; } set { thresholdValue = value; } }

        public Core.Enumerations.DateQualifier ThresholdQualifier { get { return thresholdQualifier; } set { thresholdQualifier = value; } }

        public Int32 InitialConstraintValue { get { return initialConstraintValue; } set { initialConstraintValue = value; } }

        public Core.Enumerations.DateQualifier InitialConstraintQualifier { get { return initialConstraintQualifier; } set { initialConstraintQualifier = value; } }

        public Int32 InitialMilestoneValue { get { return initialMilestoneValue; } set { initialMilestoneValue = value; } }

        public Core.Enumerations.DateQualifier InitialMilestoneQualifier { get { return initialMilestoneQualifier; } set { initialMilestoneQualifier = value; } }


        public Int64 GetWorkViewId { get { return getWorkViewId; } set { getWorkViewId = value; } }

        public WorkQueueView GetWorkView {

            get {

                if (getWorkView != null) { return getWorkView; }

                if (application == null) { return null; }

                getWorkView = application.WorkQueueViewGet (getWorkViewId);

                return getWorkView;

            }

        }

        public String GetWorkViewName { get { return (GetWorkView != null) ? GetWorkView.Name : String.Empty; } }

        public Boolean GetWorkUseGrouping { get { return getWorkUseGrouping; } set { getWorkUseGrouping = value; } }

        public List<WorkQueueGetWorkUserView> GetWorkUserViews { 
            
            get { return getWorkUserViews; } 
            
            set { 
                
                getWorkUserViews = value;

                // RESET ALL CHILD RELATIONSHIP PROPERTIES

                foreach (WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                    currentUserView.Application = application;

                    currentUserView.WorkQueueId = id;

                }
            
            }
        
        }


        public List<WorkQueueTeam> WorkTeams { get { return workTeams; } set { workTeams = value; } }


        public override Application Application {

            set {

                base.Application = value;

                // PROPOGATE: SET ALL CHILD REFERENCES

                foreach (WorkQueueTeam currentWorkTeam in workTeams) { currentWorkTeam.Application = value; }

            }

        }

        #endregion


        #region Constructors

        public WorkQueue (Application applicationReference) {

            BaseConstructor (applicationReference);

            return;

        }

        public WorkQueue (Application applicationReference, Int64 forWorkQueueId) {

            BaseConstructor (applicationReference, forWorkQueueId);

            return;

        }

        #endregion


        #region Xml Serialization

        public override System.Xml.XmlDocument XmlSerialize () {

            System.Xml.XmlDocument document = base.XmlSerialize ();

            System.Xml.XmlNode propertiesNode = document.ChildNodes[1].ChildNodes[0];



            #region Properties

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkflowId", workflowId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkflowName", application.CoreObjectGetNameById ("Workflow", workflowId));


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleValue", scheduleValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifier", ((Int32)scheduleQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ScheduleQualifierName", scheduleQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ThresholdValue", thresholdValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ThresholdQualifier", ((Int32)thresholdQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "ThresholdQualifierName", thresholdQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialConstraintValue", initialConstraintValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialConstraintQualifier", ((Int32)initialConstraintQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialConstraintQualifierName", initialConstraintQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialMilestoneValue", initialMilestoneValue.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialMilestoneQualifier", ((Int32)initialMilestoneQualifier).ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "InitialMilestoneQualifierName", initialMilestoneQualifier.ToString ());


            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GetWorkViewId", getWorkViewId.ToString ());

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GetWorkViewName", application.CoreObjectGetNameById ("WorkQueueView", getWorkViewId));

            CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "GetWorkUseGrouping", getWorkUseGrouping.ToString ());


            #endregion


            #region Work Queue Teams

            System.Xml.XmlElement workQueueTeamsNode = CommonFunctions.XmlDocumentAppendPropertyNode (document, propertiesNode, "WorkQueueTeams", String.Empty);

            foreach (WorkQueueTeam currentWorkQueueTeam in workTeams) {

                System.Xml.XmlElement workQueueTeamNode;


                workQueueTeamNode = document.CreateElement ("WorkQueueTeam");

                workQueueTeamNode.SetAttribute ("WorkTeamId", currentWorkQueueTeam.WorkTeamId.ToString ());

                workQueueTeamNode.SetAttribute ("WorkTeamName", application.CoreObjectGetNameById ("WorkTeam", currentWorkQueueTeam.WorkTeamId));

                workQueueTeamNode.SetAttribute ("Permission", ((Int32)currentWorkQueueTeam.Permission).ToString ());

                workQueueTeamNode.SetAttribute ("PermissionName", currentWorkQueueTeam.Permission.ToString ());


                workQueueTeamsNode.AppendChild (workQueueTeamNode);

            }

            #endregion


            return document;

        }

        public override List<ImportExport.Result> XmlImport (System.Xml.XmlNode objectNode) {

            List<ImportExport.Result> response = base.XmlImport (objectNode);

            System.Xml.XmlNode propertiesNode;

            String exceptionMessage = String.Empty;


            try {

                propertiesNode = objectNode.SelectSingleNode ("Properties");

                foreach (System.Xml.XmlNode currentPropertyNode in propertiesNode) {

                    switch (currentPropertyNode.Attributes["Name"].InnerText) {

                        case "WorkflowName": WorkflowId = application.CoreObjectGetIdByName ("Workflow", currentPropertyNode.InnerText); break;

                        case "ScheduleValue": ScheduleValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "ScheduleQualifier": ScheduleQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "ThresholdValue": ThresholdValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "ThresholdQualifier": ThresholdQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "InitialConstraintValue": InitialConstraintValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "InitialConstraintQualifier": InitialConstraintQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "InitialMilestoneValue": InitialMilestoneValue = Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "InitialMilestoneQualifier": InitialMilestoneQualifier = (Core.Enumerations.DateQualifier)Convert.ToInt32 (currentPropertyNode.InnerText); break;

                        case "GetWorkViewName": GetWorkViewId = application.CoreObjectGetIdByName ("WorkQueueView", currentPropertyNode.InnerText); break;

                        case "GetWorkUseGrouping": GetWorkUseGrouping = Convert.ToBoolean (currentPropertyNode.InnerText); break;

                        case "WorkQueueTeams":

                            foreach (System.Xml.XmlNode currentWorkQueueTeamNode in currentPropertyNode.ChildNodes) {

                                String workTeamName = currentWorkQueueTeamNode.Attributes["WorkTeamName"].InnerText;

                                Int64 workTeamId = application.WorkTeamGetIdByName (workTeamName);

                                if (workTeamId != 0) {

                                    Boolean workTeamExists = false;

                                    foreach (WorkQueueTeam currentWorkTeam in workTeams) {

                                        if (currentWorkTeam.Id == workTeamId) { workTeamExists = true; break; }

                                    }

                                    if (!workTeamExists) {

                                        WorkQueueTeam workQueueTeam = new WorkQueueTeam (application);

                                        workQueueTeam.WorkTeamId = workTeamId;

                                        workQueueTeam.WorkTeamName = workTeamName;

                                        workQueueTeam.Permission = (Enumerations.WorkQueueTeamPermission)Convert.ToInt32 (currentWorkQueueTeamNode.Attributes["Permission"].InnerText);

                                        workTeams.Add (workQueueTeam);

                                    }

                                }

                            }

                            break;

                    }

                }

            }

            catch (Exception importException) {

                response.Add (new ImportExport.Result (ObjectType, Name, importException));

            }

            return response;

        }

        #endregion 


        #region Data Functions

        public override void MapDataFields (System.Data.DataRow currentRow) {

            String selectStatement = String.Empty;


            base.MapDataFields (currentRow);


            workflowId = (currentRow["WorkflowId"] is DBNull) ? 0 : (Int64) currentRow["WorkflowId"];


            scheduleValue = (Int32) currentRow["ScheduleValue"];

            scheduleQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["ScheduleQualifier"];

            thresholdValue = (Int32) currentRow["ThresholdValue"];

            thresholdQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["ThresholdQualifier"];

            initialConstraintValue = (Int32) currentRow["InitialConstraintValue"];

            initialConstraintQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["InitialConstraintQualifier"];

            initialMilestoneValue = (Int32) currentRow["InitialMilestoneValue"];

            initialMilestoneQualifier = (Mercury.Server.Core.Enumerations.DateQualifier) (Int32) currentRow["InitialMilestoneQualifier"];


            GetWorkViewId = (Int64) currentRow["GetWorkViewId"];

            GetWorkUseGrouping = (Boolean) currentRow["GetWorkUseGrouping"];


            // LOAD WORK QUEUE USER VIEWS

            selectStatement = "SELECT * FROM dbo.WorkQueueGetWorkUserView WHERE WorkQueueId = " + Id.ToString () + " ORDER BY SecurityAuthorityId, UserAccountName";

            System.Data.DataTable userViewsTable = application.EnvironmentDatabase.SelectDataTable (selectStatement);

            getWorkUserViews = new List<WorkQueueGetWorkUserView> ();


            foreach (System.Data.DataRow currentUserViewRow in userViewsTable.Rows) {

                WorkQueueGetWorkUserView userView = new WorkQueueGetWorkUserView (application);

                userView.MapDataFields (currentUserViewRow);

                getWorkUserViews.Add (userView);

            }



            // ALWAYS LOAD WORK TEAMS 

            selectStatement = "SELECT WorkQueueTeam.*, WorkTeam.WorkTeamName FROM WorkQueueTeam JOIN WorkTeam ON WorkQueueTeam.WorkTeamId = WorkTeam.WorkTeamId WHERE WorkQueueId = " + id.ToString () + " ORDER BY WorkTeamName";

            System.Data.DataTable workTeamsTable = application.EnvironmentDatabase.SelectDataTable (selectStatement.ToString (), 0);

            foreach (System.Data.DataRow currentWorkTeamRow in workTeamsTable.Rows) {

                WorkQueueTeam team = new WorkQueueTeam (application);

                team.MapDataFields (currentWorkTeamRow);

                workTeams.Add (team);

            }

            return;

        }

        public override Boolean Save () { return Save (true); }

        public Boolean Save (Boolean validatePermissions) {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();


            try {

                if ((!application.HasEnvironmentPermission (Server.EnvironmentPermissions.WorkQueueManage)) && (validatePermissions)) { throw new ApplicationException ("Permission Denied"); }

                Dictionary<String, String> validationResponse = Validate ();

                if (validationResponse.Count != 0) {

                    foreach (String validationKey in validationResponse.Keys) {

                        throw new ApplicationException ("Invalid [" + validationKey + "]: " + validationResponse[validationKey]);

                    }

                }


                modifiedAccountInfo = new Mercury.Server.Data.AuthorityAccountStamp (application.Session);


                application.EnvironmentDatabase.BeginTransaction ();

                sqlStatement = new StringBuilder ();

                sqlStatement.Append ("EXEC dbo.WorkQueue_InsertUpdate ");


                sqlStatement.Append (Id.ToString () + ", ");

                sqlStatement.Append ("'" + NameSql + "', ");

                sqlStatement.Append ("'" + DescriptionSql + "', ");

                sqlStatement.Append (workflowId.ToString () + ", ");


                sqlStatement.Append (scheduleValue.ToString () + ", ");

                sqlStatement.Append (((Int32) scheduleQualifier).ToString () + ", ");

                sqlStatement.Append (thresholdValue.ToString () + ", ");

                sqlStatement.Append (((Int32) thresholdQualifier).ToString () + ", ");


                sqlStatement.Append (initialConstraintValue.ToString () + ", ");

                sqlStatement.Append (((Int32) initialConstraintQualifier).ToString () + ", ");

                sqlStatement.Append (initialMilestoneValue.ToString () + ", ");

                sqlStatement.Append (((Int32) initialMilestoneQualifier).ToString () + ", ");


                sqlStatement.Append (getWorkViewId.ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (getWorkUseGrouping).ToString () + ", ");


                sqlStatement.Append (Convert.ToInt32 (Enabled).ToString () + ", ");

                sqlStatement.Append (Convert.ToInt32 (Visible).ToString () + ", ");


                sqlStatement.Append ("'" + ExtendedPropertiesSql + "', ");

                sqlStatement.Append (modifiedAccountInfo.AccountInfoSql);


                success = application.EnvironmentDatabase.ExecuteSqlStatement (sqlStatement.ToString (), 0);

                if (!success) {

                    application.SetLastException (application.EnvironmentDatabase.LastException);

                    throw application.EnvironmentDatabase.LastException;

                }

                SetIdentity ();

                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM dbo.WorkQueueGetWorkUserView WHERE WorkQueueId = " + Id.ToString ());

                if (!success) { throw new ApplicationException (application.EnvironmentDatabase.LastException.Message); }


                foreach (WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                    currentUserView.Application = application;

                    currentUserView.WorkQueueId = Id;

                    currentUserView.Save ();

                }


                success = application.EnvironmentDatabase.ExecuteSqlStatement ("DELETE FROM WorkQueueTeam WHERE WorkQueueId = " + Id.ToString ());

                if (success) {

                    foreach (WorkQueueTeam currentTeam in workTeams) {

                        currentTeam.WorkQueueId = Id;

                        success = currentTeam.Save (application);

                        if (!success) { throw application.EnvironmentDatabase.LastException; }

                    }

                }

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

        public Boolean UpdateGetWork () {

            Boolean success = false;

            StringBuilder sqlStatement = new StringBuilder ();

            WorkQueue updatedWorkQueue = null;


            try {

                if (!application.SessionWorkQueueHasManagePermission (id)) { throw new ApplicationException ("Permission Denied. Not a manager of the Work Queue."); }

                updatedWorkQueue = new WorkQueue (application, id);

                if (modifiedAccountInfo.ActionDate != updatedWorkQueue.ModifiedAccountInfo.ActionDate) { throw new ApplicationException ("Permission Denied. Work Queue has been modified since last retreived from the database."); }


                updatedWorkQueue.GetWorkViewId = getWorkViewId;

                updatedWorkQueue.GetWorkUseGrouping = getWorkUseGrouping;

                updatedWorkQueue.GetWorkUserViews = getWorkUserViews;


                success = updatedWorkQueue.Save (false);

            }

            catch (Exception applicationException) {

                success = false;

                application.SetLastException (applicationException);

            }

            return success;

        }

        #endregion


        #region Public Functions

        public Boolean ContainsUserView (Int64 securityAuthorityId, String userAccountId) {

            Boolean containsUserView = false;

            foreach (WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                if ((currentUserView.SecurityAuthorityId == securityAuthorityId)

                    && (currentUserView.UserAccountId == userAccountId)) {

                    containsUserView = true;

                    break;

                }

            }

            return containsUserView;

        }

        public WorkQueueGetWorkUserView GetWorkUserView (Int64 securityAuthorityId, String userAccountId) {

            WorkQueueGetWorkUserView userView = null;

            foreach (WorkQueueGetWorkUserView currentUserView in getWorkUserViews) {

                if ((currentUserView.SecurityAuthorityId == securityAuthorityId)

                    && (currentUserView.UserAccountId == userAccountId)) {

                    userView = currentUserView;

                    break;

                }

            }

            return userView;

        }

        
        public Boolean HasWorkPermission {

            get {

                Boolean hasPermission = false;

                foreach (DataViews.WorkQueuePermission currentPermission in application.WorkQueuePermissionsForSession ()) {

                    if (currentPermission.WorkQueueId == Id) {

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Work) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.SelfAssign) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Denied) { hasPermission = false; break; }

                    }

                }

                return hasPermission;

            }

        }

        public Boolean HasSelfAssignPermission {

            get {

                Boolean hasPermission = false;

                foreach (DataViews.WorkQueuePermission currentPermission in application.WorkQueuePermissionsForSession ()) {

                    if (currentPermission.WorkQueueId == Id) {

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.SelfAssign) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Denied) { hasPermission = false; break; }

                    }

                }

                return hasPermission;

            }

        }

        public Boolean HasManagePermission {

            get {

                Boolean hasPermission = false;

                foreach (DataViews.WorkQueuePermission currentPermission in application.WorkQueuePermissionsForSession ()) {

                    if (currentPermission.WorkQueueId == Id) {

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Manage) { hasPermission = true; }

                        if (currentPermission.Permission == Mercury.Server.Core.Work.Enumerations.WorkQueueTeamPermission.Denied) { hasPermission = false; break; }

                    }

                }

                return hasPermission;

            }

        }

        public Boolean InsertEntity (Int64 entityId, String itemGroupKey, Server.Core.CoreObject sender, Server.Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority) {

            if (!HasManagePermission) {

                application.SetLastException (new ApplicationException ("Permission Denied trying to insert into Work Queue (\"" + Name + "\")."));

                return false;

            }

            Boolean success = false;

            success = application.WorkQueueInsertEntity (Id, entityId, itemGroupKey, sender, eventObject, eventInstanceId, eventDescription, priority);

            return success;

        }

        public WorkQueueItem GetWorkOld () {

            Int64 workQueueItemId = 0;

            WorkQueueItem workQueueItem = null;

            System.Data.SqlClient.SqlCommand getWorkCommand = null;

            if (!HasWorkPermission) { application.SetLastException (new ApplicationException ("Permission Denied. No Work Permissions to Work Queue (\"" + Name + "\").")); return null; }


            try {

                getWorkCommand = application.EnvironmentDatabase.CreateSqlCommand ("WorkQueue_GetWork");

                getWorkCommand.CommandType = System.Data.CommandType.StoredProcedure;

                getWorkCommand.Parameters.Add ("@workQueueId", System.Data.SqlDbType.BigInt);

                getWorkCommand.Parameters["@workQueueId"].Value = Id;

                getWorkCommand.Parameters.Add ("@securityAuthorityId", System.Data.SqlDbType.BigInt);

                getWorkCommand.Parameters["@securityAuthorityId"].Value = application.Session.SecurityAuthorityId;

                getWorkCommand.Parameters.Add ("@securityAuthorityName", System.Data.SqlDbType.VarChar);

                getWorkCommand.Parameters["@securityAuthorityName"].Value = application.Session.SecurityAuthorityName;

                getWorkCommand.Parameters.Add ("@securityAccountId", System.Data.SqlDbType.VarChar);

                getWorkCommand.Parameters["@securityAccountId"].Value = application.Session.UserAccountId;

                getWorkCommand.Parameters.Add ("@securityAccountName", System.Data.SqlDbType.VarChar);

                getWorkCommand.Parameters["@securityAccountName"].Value = application.Session.UserAccountName;

                getWorkCommand.Parameters.Add ("@securityUserDisplayName", System.Data.SqlDbType.VarChar);

                getWorkCommand.Parameters["@securityUserDisplayName"].Value = application.Session.UserDisplayName;

                getWorkCommand.Parameters.Add ("@workQueueItemId", System.Data.SqlDbType.BigInt);

                getWorkCommand.Parameters["@workQueueItemId"].Direction = System.Data.ParameterDirection.Output;

                application.EnvironmentDatabase.OnDemandOpen ();

                if (getWorkCommand.ExecuteNonQuery () != 0) {

                    workQueueItemId = (Int64) getWorkCommand.Parameters["@workQueueItemId"].Value;

                    if (workQueueItemId != 0) {

                        workQueueItem = new WorkQueueItem (application, workQueueItemId);

                    }

                }

            }

            catch (Exception applicationException) {

                throw applicationException;

            }

            finally {

                if (getWorkCommand != null) {

                    getWorkCommand.Dispose ();

                    getWorkCommand = null;

                }

                application.EnvironmentDatabase.OnDemandClose ();

            }

            return workQueueItem;

        }

        public WorkQueueItem GetWork () {

            Int64 workQueueItemId = 0;

            WorkQueueItem workQueueItem = null;

            WorkQueueGetWorkUserView getWorkUserView = null;

            WorkQueueView workQueueView = null;

            List<Data.FilterDescriptor> filters = new List<Data.FilterDescriptor> ();

            String customFilters = String.Empty;


            // GET WORK IS DYNAMICALLY CREATED BY WORK QUEUE VIEWS, THIS CANNOT BE DONE IN A STORED PROCEDURE


            if (!HasWorkPermission) { application.SetLastException (new ApplicationException ("Permission Denied. No Work Permissions to Work Queue (\"" + Name + "\").")); return null; }

            try {

                // ATTEMPT TO GET USER SPECIFIC VIEW FIRST

                getWorkUserView = GetWorkUserView (application.Session.SecurityAuthorityId, application.Session.UserAccountId);

                if (getWorkUserView != null) { workQueueView = getWorkUserView.WorkQueueView; }

                // ELSE FALLBACK TO DEFAULT VIEW FOR THE OVERALL WORK QUEUE

                else { workQueueView = application.WorkQueueViewGet (getWorkViewId); } // 0 VALUE IS HANDLED IN THE GET FUNCTION

                // FINALLY FALLBACK TO SYSTEM DEFAULT VIEW IF NO VIEW ASSIGNED TO WORK QUEUE

                if (workQueueView != null) {

                    // APPEND FILTERS THAT ARE FOR STANDARD FIELDS AND NOT CUSTOM FIELDS

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            filters.Add ((Data.FilterDescriptor)workQueueView.FilterDefinitions[currentFilterKey]);

                        }

                    }

                }


                StringBuilder selectStatement = new StringBuilder ();

                selectStatement.Append ("SET TRANSACTION ISOLATION LEVEL SERIALIZABLE \r\n\r\n");

                selectStatement.Append ("BEGIN TRANSACTION \r\n\r\n");

                selectStatement.Append ("  DECLARE @workQueueItemId AS BIGINT \r\n\r\n");

                selectStatement.Append ("  DECLARE @itemGroupKey AS VARCHAR (060) \r\n\r\n");


                selectStatement.Append ("  SELECT @workQueueItemId = WorkQueueItemId, @itemGroupKey = ItemGroupKey \r\n\r\n FROM (\r\n\r\n");


                selectStatement.Append ("  SELECT \r\n\r\n");

                selectStatement.Append ("      " + application.WorkQueueItemsGetRowNumberSql (null, workQueueView) + " WorkQueueItemId, ItemGroupKey \r\n\r\n");

                selectStatement.Append ("    FROM (" + application.WorkQueueItemsGetSqlStatement (filters) + ") AS WorkQueueItem ) AS WorkQueueItemTop1 WHERE RowNumber = 1 \r\n");


                if (workQueueView != null) {

                    String customFields = String.Empty;

                    foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                        String extendedPropertyField = currentFieldDefinition.SqlSelectList;

                        if (!String.IsNullOrEmpty (extendedPropertyField)) { customFields = customFields + ", " + extendedPropertyField; }

                    }

                    selectStatement.Replace ("/*_CUSTOM_FIELD_INSERT_*/", customFields);

                }

                String itemFilter = "      AND (WorkQueueItem.AssignedToSecurityAuthorityId = 0) AND (CompletionDate IS NULL) \r\n\r\n";

                itemFilter = itemFilter + "      AND (GETDATE () >= WorkQueueItem.ConstraintDate)\r\n\r\n";

                itemFilter = itemFilter + "      AND (WorkQueueId = " + Id.ToString () + ")\r\n\r\n";

                itemFilter = itemFilter + "      AND ((WorkTimeRestrictions IS NULL) \r\n\r\n";

                itemFilter = itemFilter + "        OR (CAST ((ISNULL (WorkTimeRestrictions.value ('(DayOfWeekTimes/";

                itemFilter = itemFilter + "Day[@DayOfWeek=\"" + ((Int32) DateTime.Today.DayOfWeek).ToString () + "\"]/";

                itemFilter = itemFilter + "Time[@StartTime <= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"";

                itemFilter = itemFilter + "and @EndTime >= \"" + DateTime.Now.ToString ("hh:mm:ss") + "\"])[1]', 'BIT'), 1) - 1) AS BIT) = 1)) \r\n\r\n";


                if (workQueueView != null) {

                    // APPEND FILTERS THAT ARE NOT STANDARD FIELDS 

                    foreach (Int32 currentFilterKey in workQueueView.FilterDefinitions.Keys) {

                        if (!workQueueView.WellKnownFields.ContainsKey (workQueueView.FilterDefinitions[currentFilterKey].PropertyPath)) {

                            foreach (Core.Work.WorkQueueViewFieldDefinition currentFieldDefinition in workQueueView.FieldDefinitions) {

                                if (currentFieldDefinition.DisplayName == workQueueView.FilterDefinitions[currentFilterKey].PropertyPath) {

                                    Data.FilterDescriptor filterDescriptor = new Data.FilterDescriptor (

                                        currentFieldDefinition.SqlDeclaration,

                                        workQueueView.FilterDefinitions[currentFilterKey].Operator,

                                        workQueueView.FilterDefinitions[currentFilterKey].Parameter.Value);

                                    itemFilter = itemFilter + " AND (" + filterDescriptor.SqlCriteriaString (String.Empty) + ")";

                                }

                            }

                        }

                    }

                }


                selectStatement.Replace ("/*_CUSTOM_FILTER_INSERT_*/", itemFilter);



                selectStatement.Append ("  IF (@workQueueItemId IS NOT NULL) \r\n\r\n");

                selectStatement.Append ("      BEGIN \r\n\r\n");

                selectStatement.Append ("        EXEC WorkQueueItem_AssignTo @workQueueItemId, ");

                selectStatement.Append (application.Session.SecurityAuthorityId.ToString () + ", ");

                selectStatement.Append ("'" + application.Session.UserAccountId.Replace ("'", "''") + "', ");

                selectStatement.Append ("'" + application.Session.UserAccountName.Replace ("'", "''") + "', ");

                selectStatement.Append ("'" + application.Session.UserDisplayName.Replace ("'", "''") + "', ");

                selectStatement.Append ("'Get Work', \r\n\r\n");

                selectStatement.Append ("'" + application.Session.SecurityAuthorityName.Replace ("'", "''") + "', ");

                selectStatement.Append ("'" + application.Session.UserAccountId.Replace ("'", "''") + "', ");

                selectStatement.Append ("'" + application.Session.UserAccountName.Replace ("'", "''") + "' \r\n\r\n");

                if (getWorkUseGrouping) {


                    selectStatement.Append ("        IF (LEN (RTRIM (@itemGroupKey)) > 0) \r\n\r\n");

                    selectStatement.Append ("          BEGIN \r\n\r\n");

                    selectStatement.Append ("            DECLARE @currentWorkQueueItemId AS BIGINT \r\n\r\n");

                    selectStatement.Append ("            DECLARE GroupItemsCursor CURSOR FOR  \r\n\r\n");

                    selectStatement.Append ("              SELECT WorkQueueItemId FROM WorkQueueItem WHERE (WorkQueueId = " + Id.ToString () + ") AND (ItemGroupKey = @itemGroupKey) AND (WorkQueueItemId <> @workQueueItemId) AND CompletionDate IS NULL AND AssignedToSecurityAuthorityId = 0 \r\n\r\n");

                    selectStatement.Append ("            OPEN GroupItemsCursor \r\n\r\n");

                    selectStatement.Append ("            FETCH NEXT FROM GroupItemsCursor INTO @currentWorkQueueItemId \r\n\r\n");

                    selectStatement.Append ("            WHILE (@@FETCH_STATUS = 0)    \r\n\r\n");

                    selectStatement.Append ("              BEGIN \r\n\r\n");

                    selectStatement.Append ("                EXEC WorkQueueItem_AssignTo @currentWorkQueueItemId, ");

                    selectStatement.Append (application.Session.SecurityAuthorityId.ToString () + ", ");

                    selectStatement.Append ("'" + application.Session.UserAccountId.Replace ("'", "''") + "', ");

                    selectStatement.Append ("'" + application.Session.UserAccountName.Replace ("'", "''") + "', ");

                    selectStatement.Append ("'" + application.Session.UserDisplayName.Replace ("'", "''") + "', ");

                    selectStatement.Append ("'Get Work - By Grouping', \r\n\r\n");

                    selectStatement.Append ("'" + application.Session.SecurityAuthorityName.Replace ("'", "''") + "', ");

                    selectStatement.Append ("'" + application.Session.UserAccountId.Replace ("'", "''") + "', ");

                    selectStatement.Append ("'" + application.Session.UserAccountName.Replace ("'", "''") + "' \r\n\r\n");


                    selectStatement.Append ("                FETCH NEXT FROM GroupItemsCursor INTO @currentWorkQueueItemId \r\n\r\n");

                    selectStatement.Append ("              END \r\n\r\n");

                    selectStatement.Append ("            CLOSE GroupItemsCursor   \r\n\r\n");

                    selectStatement.Append ("            DEALLOCATE GroupItemsCursor                      \r\n\r\n");

                    selectStatement.Append ("          END \r\n\r\n");

                }

                selectStatement.Append ("      END \r\n\r\n");

                selectStatement.Append ("    ELSE  \r\n\r\n");

                selectStatement.Append ("      BEGIN \r\n\r\n");

                selectStatement.Append ("        SET @workQueueItemId = 0 \r\n\r\n");

                selectStatement.Append ("      END \r\n\r\n");

                selectStatement.Append ("      SELECT @workQueueItemId \r\n\r\n");

                selectStatement.Append ("COMMIT TRANSACTION \r\n\r\n");


                workQueueItemId = (Int64) application.EnvironmentDatabase.ExecuteScalar (selectStatement.ToString ());

                workQueueItem = application.WorkQueueItemGet (workQueueItemId);

            }

            catch (Exception applicationException) {

                application.EnvironmentDatabase.ExecuteSqlStatement ("ROLLBACK TRANSACTION");

                throw applicationException;

            }

            finally {


            }

            return workQueueItem;

        }

        #endregion 

    }

}
