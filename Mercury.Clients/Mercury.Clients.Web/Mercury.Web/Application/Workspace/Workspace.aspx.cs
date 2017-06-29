using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workspace {

    public partial class Workspace : System.Web.UI.Page {

        #region Private Properties

        private Boolean useCaching = true;

        #endregion 


        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }

        private Boolean UseCaching { get { return ((useCaching) && (IsPostBack)); } set { useCaching = value; } }


        private Int64 SelectedWorkQueueId {

            get {

                Int64 selectedWorkQueueId = Convert.ToInt64 (Session ["Workspace_SelectedWorkQueueId"]);

                if (selectedWorkQueueId == 0) {

                    Int64.TryParse (WorkQueueSelection.SelectedValue, out selectedWorkQueueId);

                    SelectedWorkQueueId = selectedWorkQueueId;

                }

                return selectedWorkQueueId;

            }

            set { Session["Workspace_SelectedWorkQueueId"] = value; }

        }

        private Client.Core.Work.WorkQueue SelectedWorkQueue { get { if (MercuryApplication != null) { return MercuryApplication.WorkQueueGet (SelectedWorkQueueId, true); } return null; } }


        private List<Client.Core.Work.WorkQueueItem> WorkQueueItems {

            get {

                List<Client.Core.Work.WorkQueueItem> workQueueItems = (List<Client.Core.Work.WorkQueueItem>)Session[SessionCachePrefix + "WorkQueueItems"];

                if (workQueueItems == null) {

                    workQueueItems = MercuryApplication.WorkQueueItemsGetByViewPage ((Server.Application.WorkQueueView) null, MyAssignedWork_ItemFilters (), null, 1, 999999, false);

                    Session[SessionCachePrefix + "WorkQueueItems"] = workQueueItems;

                }

                return workQueueItems;

            }

            set { Session[SessionCachePrefix + "WorkQueueItems"] = value; }

        }

        private List<Client.Core.Work.WorkQueueItemSender> WorkQueueItemSenders {

            get {

                List<Client.Core.Work.WorkQueueItemSender> workQueueItemSenders = (List<Client.Core.Work.WorkQueueItemSender>)Session[SessionCachePrefix + "WorkQueueItemSenders"];

                if (workQueueItemSenders == null) {

                    workQueueItemSenders = new List<Client.Core.Work.WorkQueueItemSender> ();

                    Session[SessionCachePrefix + "WorkQueueItemSenders"] = workQueueItemSenders;

                }

                return workQueueItemSenders;

            }

            set { Session[SessionCachePrefix + "WorkQueueItemSenders"] = value; }

        }


        private List<Client.Core.Individual.Case.MemberCaseProblemClass> MyAssignedCasesProblemClasses {

            get {

                List<Client.Core.Individual.Case.MemberCaseProblemClass> problemClasses = (List<Client.Core.Individual.Case.MemberCaseProblemClass>)Session[SessionCachePrefix + "MyAssignedCasesProblemClasses"];

                if (problemClasses == null) {

                    problemClasses = new List<Client.Core.Individual.Case.MemberCaseProblemClass> ();

                    Session[SessionCachePrefix + "MyAssignedCasesProblemClasses"] = problemClasses;

                }

                return problemClasses;

            }

            set { Session[SessionCachePrefix + "MyAssignedCasesProblemClasses"] = value; }

        }

        private List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> CaseLoadGridUsers {

            get {

                List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> users = (List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary>)Session[SessionCachePrefix + "CaseLoadGridUsers"];

                if (users == null) {

                    users = new List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> ();

                    Session[SessionCachePrefix + "CaseLoadGridUsers"] = users;

                }

                return users;

            }

            set { Session[SessionCachePrefix + "CaseLoadGridUsers"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (!IsPostBack) {

                InitializeWorkQueueSelection ();

                WorkQueueItemCloseOutcomeSelection.DataSource = MercuryApplication.WorkOutcomesAvailable (true, true);

                WorkQueueItemCloseOutcomeSelection.DataTextField = "Name";

                WorkQueueItemCloseOutcomeSelection.DataValueField = "Id";

                WorkQueueItemCloseOutcomeSelection.DataBind ();

                WorkQueueItemCloseOutcomeSelection.SelectedValue = "0";

                InitializeAll ();

            }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeAll () {

            MyAssignedCasesGrid.Rebind ();

            MyTeamCasesGrid.Rebind ();

            return;

        }

        private void InitializeWorkQueueSelection () {

            // PRE-CACHE WORK QUEUES THROUGH WORK QUEUES AVAILABLE

            // PRE-CACHE WORKFLOWS THROUGH WORKFLOWS AVAILABLE

            MercuryApplication.WorkQueuesAvailable (true);

            MercuryApplication.WorkflowsAvailable (true);


            WorkQueueSelection.Items.Clear ();


            foreach (Int64 currentWorkQueueId in MercuryApplication.Session.WorkQueuePermissions.Keys) {

                Client.Core.Work.WorkQueue currentWorkQueue = MercuryApplication.WorkQueueGet (currentWorkQueueId, true);

                if (currentWorkQueue != null) {

                    if ((currentWorkQueue.Enabled) && (currentWorkQueue.Visible)) {

                        WorkQueueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueue.Name, currentWorkQueue.Id.ToString ()));

                    }

                }

            }

            WorkQueueSelection.SelectedValue = SelectedWorkQueueId.ToString ();
        
            InitializeMyWorkQueuesForSelection (SelectedWorkQueueId);

            return;

        }

        private void InitializeMyWorkQueuesForSelection (Int64 selectedWorkQueueId) {

            if (selectedWorkQueueId == 0) { return; }


            // UPDATE COUNT ON WORK QUEUE SELECTION 

            InitializeAvailableCount ();


            // UPDATE GET WORKFLOW

            Client.Core.Work.Workflow getWorkWorkFlow = MercuryApplication.WorkflowGetByWorkQueueId (selectedWorkQueueId, true);

            WorkQueueGetWorkButton.Text = (getWorkWorkFlow != null) ? getWorkWorkFlow.Name : "(Manual)";


            // UPDATE MONITOR LINK 

            WorkQueueMonitorLink.Style.Clear ();

            WorkQueueMonitorLink.Style.Add ("white-space", "nowrap");

            WorkQueueMonitorLink.Style.Add ("display", "none");

            if (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (selectedWorkQueueId)) {

                //if ((MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Work)

                //    || (MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.SelfAssign)

                //    || (MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Monitor)) {

                if (MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Manage) {

                    WorkQueueMonitorLink.Style.Remove ("display");

                    WorkQueueMonitorLink.Style.Add ("display", "block");

                    WorkQueueMonitorLink.HRef = @"/Application/Work/WorkQueueMonitor.aspx?WorkQueueId=" + selectedWorkQueueId.ToString ();

                }

            }


            // UPDATE MANAGE LINK

            WorkQueueManageLink.Style.Clear ();

            WorkQueueManageLink.Style.Add ("white-space", "nowrap");

            WorkQueueManageLink.Style.Add ("display", "none");

            if (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (selectedWorkQueueId)) {

                //if ((MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Work)

                //    || (MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.SelfAssign)

                //    || (MercuryApplication.Session.WorkQueuePermissions[selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Manage)) {

                if (MercuryApplication.Session.WorkQueuePermissions [selectedWorkQueueId] == Mercury.Server.Application.WorkQueueTeamPermission.Manage) {

                    WorkQueueManageLink.Style.Remove ("display");

                    WorkQueueManageLink.Style.Add ("display", "block");

                    WorkQueueManageLink.HRef = @"/Application/Work/WorkQueueManagement.aspx?WorkQueueId=" + selectedWorkQueueId.ToString ();

                }

            }


            return;

        }

        private void InitializeAvailableCount () {

            // UPDATE COUNT

            Int64 queueTotalItems = 0;

            Int64 queueAvailableItems = 0;


            List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

            filters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, SelectedWorkQueueId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

            queueTotalItems = MercuryApplication.WorkQueueItemsGetCount (SelectedWorkQueue.GetWorkView, filters, UseCaching);


            filters.Add (MercuryApplication.CreateFilterDescriptor ("HasConstraintDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsAssigned", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("WithinWorkTimeRestrictions", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

            queueAvailableItems = MercuryApplication.WorkQueueItemsGetCount (SelectedWorkQueue.GetWorkView, filters, UseCaching);


            WorkQueueItemsAvailableCount.Text = queueAvailableItems.ToString () + " / " + queueTotalItems.ToString ();


            return;

        }

        #endregion


        #region My Assigned Work Grid Events

        private List<Mercury.Server.Application.DataFilterDescriptor> MyAssignedWork_ItemFilters () {

            // BUILD FILTERS COLLECTION 

            List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();


            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToSecurityAuthorityId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.SecurityAuthorityId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserAccountId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.UserAccountId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));


            return filters;

        }

        protected void MyAssignedWork_WorkQueueItemsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent: // ALL DATA IS PRE-LOADED

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted: // ALL DATA IS PRE-LOADED

                        UseCaching = false;

                        if (e.RebindReason != Telerik.Web.UI.GridRebindReason.InitialLoad) { 

                            // ITEM COUNT IS INITIALIZED DURING WORK QUEUE SELECTION (DO NOT DOUBLE READ COUNT)

                            InitializeAvailableCount ();

                        }


                        WorkQueueItems = null;

                        MyAssignedWork_WorkQueueItemsGrid.VirtualItemCount = WorkQueueItems.Count;

                        MyAssignedWork_WorkQueueItemsGrid.DataSource = WorkQueueItems;

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }
            
            }

            else { // DETAIL TABLE NEEDS DATA SOURCE 

                MyAssignedWork_WorkQueueItemsGrid.MasterTableView.DetailTables[0].DataSource = WorkQueueItemSenders;

                
            }            

            return;

        }

        protected void MyAssignedWork_WorkQueueItemsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    System.Diagnostics.Debug.WriteLine ("Item Command Argument: " + e.CommandArgument);

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) e.Item;

                    Int64 workQueueItemId = 0;

                    if (Int64.TryParse (gridItem["Id"].Text, out workQueueItemId)) {

                        if (gridItem.Expanded) { // COLLAPSE ITEM

                            List<Client.Core.Work.WorkQueueItemSender> sendersToRemove = new List<Client.Core.Work.WorkQueueItemSender> ();

                            foreach (Client.Core.Work.WorkQueueItemSender currentSender in WorkQueueItemSenders) {

                                if (currentSender.WorkQueueItemId == workQueueItemId) {

                                    sendersToRemove.Add (currentSender);

                                }

                            }

                            foreach (Client.Core.Work.WorkQueueItemSender currentSenderToRemove in sendersToRemove) {

                                WorkQueueItemSenders.Remove (currentSenderToRemove);

                            }

                        }

                        else { // EXPAND ITEM

                            List<Client.Core.Work.WorkQueueItemSender> workQueueItemSenders = MercuryApplication.WorkQueueItemSendersGet (workQueueItemId, true);

                            WorkQueueItemSenders.AddRange (workQueueItemSenders);

                        }

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("Unhandled Grid Item Command: " + e.CommandName);

                    break;

            }

            return;

        }

        #endregion


        #region Member Case Grids Events

        protected void MyAssignedCasesGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent: // ALL DATA IS PRE-LOADED

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted: // ALL DATA IS PRE-LOADED

                        UseCaching = false;

                        if (e.RebindReason != Telerik.Web.UI.GridRebindReason.InitialLoad) {

                            // ITEM COUNT IS INITIALIZED DURING WORK QUEUE SELECTION (DO NOT DOUBLE READ COUNT)

                            

                        }

                        MyAssignedCasesGrid.DataSource = MercuryApplication.MemberCaseSummaryGetByAssignedToUserPage (1, 999999, false);

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }

            }

            else { // DETAIL TABLE NEEDS DATA SOURCE 

                MyAssignedCasesGrid.MasterTableView.DetailTables[0].DataSource = MyAssignedCasesProblemClasses;
                
            }

            return;

        }

        protected void MyAssignedCasesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    System.Diagnostics.Debug.WriteLine ("Item Command Argument: " + e.CommandArgument);

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem)e.Item;

                    Int64 memberCaseId = 0;

                    if (Int64.TryParse (gridItem["Id"].Text, out memberCaseId)) {

                        if (gridItem.Expanded) { // COLLAPSE ITEM

                            List<Client.Core.Individual.Case.MemberCaseProblemClass> sendersToRemove = new List<Client.Core.Individual.Case.MemberCaseProblemClass> ();

                            foreach (Client.Core.Individual.Case.MemberCaseProblemClass currentSender in MyAssignedCasesProblemClasses) {

                                if (currentSender.MemberCaseId == memberCaseId) {

                                    sendersToRemove.Add (currentSender);

                                }

                            }

                            foreach (Client.Core.Individual.Case.MemberCaseProblemClass currentSenderToRemove in sendersToRemove) {

                                MyAssignedCasesProblemClasses.Remove (currentSenderToRemove);

                            }

                        }

                        else { // EXPAND ITEM

                            Client.Core.Individual.Case.MemberCase memberCase = MercuryApplication.MemberCaseGet (memberCaseId, true);

                            List<Client.Core.Individual.Case.MemberCaseProblemClass> problemClasses =

                                (from currentProblemClass in memberCase.ProblemClasses

                                 where currentProblemClass.HasActiveCarePlans

                                 orderby currentProblemClass.Classification

                                 select currentProblemClass).ToList ();


                            MyAssignedCasesProblemClasses.AddRange (problemClasses);

                        }

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("Unhandled Grid Item Command: " + e.CommandName);

                    break;

            }

            return;

        }

        protected void MyTeamCasesGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent: // ALL DATA IS PRE-LOADED

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted: // ALL DATA IS PRE-LOADED

                        UseCaching = false;

                        if (e.RebindReason != Telerik.Web.UI.GridRebindReason.InitialLoad) {

                            // ITEM COUNT IS INITIALIZED DURING WORK QUEUE SELECTION (DO NOT DOUBLE READ COUNT)


                        }

                        MyTeamCasesGrid.DataSource = MercuryApplication.MemberCaseSummaryGetByUserWorkTeamsPage (1, 999999, false);

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }

            }

            else { // DETAIL TABLE NEEDS DATA SOURCE 





            }

            return;

        }

        protected void CaseLoadGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent: // ALL DATA IS PRE-LOADED

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted: // ALL DATA IS PRE-LOADED

                        UseCaching = false;

                        if (e.RebindReason != Telerik.Web.UI.GridRebindReason.InitialLoad) {

                            // ITEM COUNT IS INITIALIZED DURING WORK QUEUE SELECTION (DO NOT DOUBLE READ COUNT)


                        }

                        CaseLoadGrid.DataSource = MercuryApplication.MemberCaseLoadSummaryGetByUser (MercuryApplication.Session.SecurityAuthorityId, MercuryApplication.Session.UserAccountId, false);

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }

            }

            else { // DETAIL TABLE NEEDS DATA SOURCE 

                // CaseLoadGrid.MasterTableView.DetailTables[0].DataSource = CaseLoadGridUsers;

                CaseLoadGrid.MasterTableView.DetailTables[0].DataSource = CaseLoadGridUsers;

            }

            return;

        }

        protected void CaseLoadGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    System.Diagnostics.Debug.WriteLine ("Item Command Argument: " + e.CommandArgument);

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem)e.Item;

                    Int64 workTeamId = 0;

                    if (Int64.TryParse (gridItem["AssignedToWorkTeamId"].Text, out workTeamId)) {

                        if (gridItem.Expanded) { // COLLAPSE ITEM

                            List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> sendersToRemove = new List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> ();

                            foreach (Client.Core.Individual.Case.Views.MemberCaseLoadSummary currentSender in CaseLoadGridUsers) {

                                if (currentSender.AssignedToWorkTeamId == workTeamId) {

                                    sendersToRemove.Add (currentSender);

                                }

                            }

                            foreach (Client.Core.Individual.Case.Views.MemberCaseLoadSummary currentSenderToRemove in sendersToRemove) {

                                CaseLoadGridUsers.Remove (currentSenderToRemove);

                            }

                        }

                        else { // EXPAND ITEM
                            
                            List <Client.Core.Individual.Case.Views.MemberCaseLoadSummary> userSummary = MercuryApplication.MemberCaseLoadSummaryGetByWorkTeam (workTeamId, false);

                            if (userSummary.Count != 0) { CaseLoadGridUsers.AddRange (userSummary); } 
                            
                        }

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("Unhandled Grid Item Command: " + e.CommandName);

                    break;

            }

            return;

        }

        #endregion


        #region Control Events

        protected void WorkQueueSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            SelectedWorkQueueId = Int64.Parse (e.Value);

            UseCaching = false;

            InitializeMyWorkQueuesForSelection (SelectedWorkQueueId);

            return;

        }

        protected void WorkQueueGetWorkButton_OnClick (Object sender, EventArgs e) {

            GetWorkExceptionMessageRow.Style.Clear ();

            GetWorkExceptionMessageRow.Style.Add ("display", "none");

            GetWorkInformationalMessageRow.Style.Clear ();

            GetWorkInformationalMessageRow.Style.Add ("display", "none");


            if (SelectedWorkQueueId == 0) {

                GetWorkExceptionMessageRow.Style.Clear ();

                GetWorkExceptionMessage.Text = "No Work Queue Selected for Get Work.";

                return;

            }


            Mercury.Server.Application.GetWorkResponse response;

            response = MercuryApplication.WorkQueueGetWork (SelectedWorkQueueId);


            if (response.HasException) {

                // EXCEPTION OCCURRED WHILE GETTING WORK QUEUE ITEM

                GetWorkExceptionMessageRow.Style.Clear ();

                GetWorkExceptionMessage.Text = "[" + response.Exception.Source + "] " + response.Exception.Message;

                MercuryApplication.SetLastException (response.Exception);

            }

            else if (response.WorkQueueItem == null) {

                // NO WORK QUEUE ITEM AVAILABLE

                GetWorkInformationalMessageRow.Style.Clear ();

                GetWorkInformationalMessage.Text = "No Work Queue Items Available in the selected Work Queue.";
                
            }

            else {

                // VALID WORK QUEUE ITEM FOUND AND RETURNED

                if (response.Workflow != null) {

                    // KICK-OFF WORKFLOW PROCESS

                    String parameterString = String.Empty;

                    parameterString = parameterString + "?WorkflowId=" + SelectedWorkQueue.WorkflowId.ToString ();

                    parameterString = parameterString + "&WorkQueueItemId=" + response.WorkQueueItem.Id.ToString ();

                    parameterString = parameterString + "&" + response.WorkQueueItem.ItemObjectType + "Id=" + response.WorkQueueItem.ItemObjectId.ToString ();


                    if (response.WorkQueueItem.WorkflowInstanceId != Guid.Empty) {

                        parameterString = parameterString + "&WorkflowInstanceId=" + response.WorkQueueItem.WorkflowInstanceId.ToString ();

                    }

                    Response.RedirectLocation = "/Application/Workflow/Workflow.aspx" + parameterString;

                }

                else { // MANUAL WORKFLOW, JUST HARD REFRESH

                    WorkQueueItems = null;

                    InitializeMyWorkQueuesForSelection (SelectedWorkQueueId);

                    UseCaching = false; // ONLY ALLOW REFRESH OF COUNTS ON DATA REBIND

                    MyAssignedWork_WorkQueueItemsGrid.Rebind ();

                }
                                    
            }

            return;

        }

        protected void MyAssignedWorkRefresh_OnClick (Object sender, EventArgs e) {

            // HARD REFRESH

            WorkQueueItems = null;

            WorkQueueItemSenders = null;

            InitializeMyWorkQueuesForSelection (SelectedWorkQueueId);

            UseCaching = false; // ONLY ALLOW REFRESH OF COUNTS ON DATA REBIND

            MyAssignedWork_WorkQueueItemsGrid.Rebind ();

            return;

        }

        #endregion 


        #region Dialog Window Events

        protected void WorkQueueItemSuspendWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            Boolean success = false;

            String responseScript = "$find(\"" + WorkQueueItemSuspendWindow.ClientID + "\").close ();";


            Int64 workQueueItemId = 0;

            Int32 suspendDays = 0;


            // SUSPEND WORK QUEUE ITEM

            if (Int64.TryParse (WorkQueueItemSuspendId.Text, out workQueueItemId)) {

                suspendDays = Convert.ToInt32 (WorkQueueItemSuspendDays.Value);

                success = MercuryApplication.WorkQueueItemSuspend (workQueueItemId, "Manual Suspend", String.Empty, suspendDays, 0, true);


                if (!success) { responseScript += "alert('" + MercuryApplication.LastException.Message.Replace ("'", "''") + "');"; }

            }

            else { responseScript += "alert('Unable to parse the Work Queue Item Id.');"; }


            // REBIND DATE GRID

            MyAssignedWork_WorkQueueItemsGrid.DataSource = null;

            MyAssignedWork_WorkQueueItemsGrid.Rebind ();

           
            // CLIENT-SIDE CLOSE DIALOG WINDOW

            Master.TelerikAjaxManagerControl.ResponseScripts.Add (responseScript);

            return;

        }

        protected void WorkQueueItemCloseWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            Boolean success = false;

            String responseScript = "$find(\"" + WorkQueueItemCloseWindow.ClientID + "\").close ();";


            Int64 workQueueItemId = 0;

            Int64 workOutcomeId = 0;


            // SUSPEND WORK QUEUE ITEM

            if (Int64.TryParse (WorkQueueItemCloseId.Text, out workQueueItemId)) {

                if (Int64.TryParse (WorkQueueItemCloseOutcomeSelection.SelectedValue, out workOutcomeId)) {

                    success = MercuryApplication.WorkQueueItemClose (workQueueItemId, workOutcomeId);

                    if (!success) { responseScript += "alert('" + MercuryApplication.LastException.Message.Replace ("'", "''") + "');"; }

                }

                else { responseScript += "alert('Unable to parse the Work Outcome Id.');"; }

            }

            else { responseScript += "alert('Unable to parse the Work Queue Item Id.');"; }


            // REBIND DATE GRID

            MyAssignedWork_WorkQueueItemsGrid.DataSource = null;

            MyAssignedWork_WorkQueueItemsGrid.Rebind ();


            // CLIENT-SIDE CLOSE DIALOG WINDOW

            Master.TelerikAjaxManagerControl.ResponseScripts.Add (responseScript);

            return;

        }

        #endregion 

    }


}