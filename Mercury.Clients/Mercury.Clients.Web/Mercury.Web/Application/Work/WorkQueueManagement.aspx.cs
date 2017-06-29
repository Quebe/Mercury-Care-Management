using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Work {

    public partial class WorkQueueManagement : System.Web.UI.Page {


        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }

        
        private Client.Core.Work.WorkQueue WorkQueueSelected {

            get {

                Client.Core.Work.WorkQueue workQueue = (Client.Core.Work.WorkQueue)Session[SessionCachePrefix + "WorkQueueSelected"];

                return workQueue;

            }

            set { Session[SessionCachePrefix + "WorkQueueSelected"] = value; }

        }

        private Client.Core.Work.WorkQueueView WorkQueueViewSelected {

            get {

                // return MercuryApplication.WorkQueueViewGet (Convert.ToInt64 (WorkQueueViewSelection.SelectedValue), true);

                Client.Core.Work.WorkQueueView workQueueView = (Client.Core.Work.WorkQueueView)Session[SessionCachePrefix + "WorkQueueViewSelected"];

                return workQueueView;

            }

            set { Session[SessionCachePrefix + "WorkQueueViewSelected"] = value; }

        }

        private Client.Core.Work.WorkQueue AssignWorkQueueSelected {

            get {

                Client.Core.Work.WorkQueue workQueue = (Client.Core.Work.WorkQueue)Session[SessionCachePrefix + "AssignWorkQueueSelected"];

                return workQueue;

            }

            set { Session[SessionCachePrefix + "AssignWorkQueueSelected"] = value; }

        }

        //private List<Client.Core.Work.WorkQueueItem> WorkQueueItems {

        //    get {

        //        List<Client.Core.Work.WorkQueueItem> workQueueItems = (List<Client.Core.Work.WorkQueueItem>)Session[SessionCachePrefix + "WorkQueueItems"];

        //        if (workQueueItems == null) {

        //            workQueueItems = MercuryApplication.WorkQueueItemsGetByViewPage ((Server.Application.WorkQueueView)null, MyAssignedWork_ItemFilters (), null, 1, 999999, false);

        //            Session[SessionCachePrefix + "WorkQueueItems"] = workQueueItems;

        //        }

        //        return workQueueItems;

        //    }

        //    set { Session[SessionCachePrefix + "WorkQueueItems"] = value; }

        //}

        //private List<Client.Core.Work.WorkQueueItemSender> WorkQueueItemSenders {

        //    get {

        //        List<Client.Core.Work.WorkQueueItemSender> workQueueItemSenders = (List<Client.Core.Work.WorkQueueItemSender>)Session[SessionCachePrefix + "WorkQueueItemSenders"];

        //        if (workQueueItemSenders == null) {

        //            workQueueItemSenders = new List<Client.Core.Work.WorkQueueItemSender> ();

        //            Session[SessionCachePrefix + "WorkQueueItemSenders"] = workQueueItemSenders;

        //        }

        //        return workQueueItemSenders;

        //    }

        //    set { Session[SessionCachePrefix + "WorkQueueItemSenders"] = value; }

        //}

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

        #endregion


        #region Page Events
        
        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            if (!IsPostBack) {

                // IF FIRST REQUEST, READ FROM QUERY STRING

                Int64 selectedWorkQueueId = 0;

                Int64.TryParse (Convert.ToString (Request.QueryString["WorkQueueId"]), out selectedWorkQueueId);

                WorkQueueSelected = MercuryApplication.WorkQueueGet (selectedWorkQueueId, true);

                if (WorkQueueSelected != null) {

                    WorkQueueViewSelected = MercuryApplication.WorkQueueViewGet (WorkQueueSelected.GetWorkViewId, true);

                }


                // ONLY LOAD ON FIRST LOAD, STATE IS SAVE IN VIEW STATE FOR POST BACK

                InitializeWorkQueueSelection ();
                
                InitializeAssignWorkQueueSelection ();
                
                InitializeWorkQueueViewSelection ();
                               

                WorkQueueItemsGrid.Rebind ();


                // INITIALIZE WORK OUTCOMES

                WorkQueueItemCloseOutcomeSelection.DataSource = MercuryApplication.WorkOutcomesAvailable (true, true);

                WorkQueueItemCloseOutcomeSelection.DataTextField = "Name";

                WorkQueueItemCloseOutcomeSelection.DataValueField = "Id";

                WorkQueueItemCloseOutcomeSelection.DataBind ();

                WorkQueueItemCloseOutcomeSelection.SelectedValue = "0";

            }

            else {


                // FORCE CLIENT-SIDE REPAINT AFTER AJAX CONTROL UPDATES

                Master.TelerikAjaxManagerControl.ResponseScripts.Add ("WorkQueueManagement_OnPaint ();");

            }

            return;

        }

        protected void Page_LoadComplete (Object sender, EventArgs e) {

            return;

        }

        protected void Page_Unload (Object sender, EventArgs e) {

            return;

        }

        #endregion 


        #region Initializations

        private void InitializeWorkQueueSelection () {

            List<Client.Core.Work.WorkQueue> availableWorkQueues = MercuryApplication.WorkQueuesAvailable (true, true);

            List<Client.Core.Work.WorkQueue> filteredWorkQueues = new List<Client.Core.Work.WorkQueue> ();


            // FILTER AVAILABLE WORK QUEUES TO JUST THOSE THE USER CAN MANAGE

            foreach (Client.Core.Work.WorkQueue currentWorkQueue in availableWorkQueues) {

                if (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (currentWorkQueue.Id)) {

                    if (MercuryApplication.Session.WorkQueuePermissions[currentWorkQueue.Id] == Mercury.Server.Application.WorkQueueTeamPermission.Manage) {

                        // HAS MANAGE RIGHT TO THE WORK QUEUE

                        filteredWorkQueues.Add (currentWorkQueue);

                    }

                }

            }


            WorkQueueSelection.DataSource = filteredWorkQueues;

            WorkQueueSelection.DataValueField = "Id";

            WorkQueueSelection.DataTextField = "Name";

            WorkQueueSelection.DataBind ();


            //WorkQueueItemAssignWorkQueueSelection.DataSource = filteredWorkQueues;

            //WorkQueueItemAssignWorkQueueSelection.DataValueField = "Id";

            //WorkQueueItemAssignWorkQueueSelection.DataTextField = "Name";

            //WorkQueueItemAssignWorkQueueSelection.DataBind ();


            //WorkQueueItemAssignWorkQueueSelection.SelectedValue = WorkQueueSelected.Id.ToString ();

            //InitializeAssignUserSelection ();


            if (WorkQueueSelected != null) { WorkQueueSelection.SelectedValue = WorkQueueSelected.Id.ToString (); }

            InitializeMonitorLink ();

            return;

        }

        private void InitializeMonitorLink () {

            if (WorkQueueSelected == null) { return; }


            // UPDATE SET GET WORK LINK

            WorkQueueSetGetWorkLink.HRef = @"#";

            WorkQueueSetGetWorkLink.Attributes.Remove ("onclick");

            WorkQueueSetGetWorkLink.Attributes.Add ("onclick",

                "javascript:window.open ('/Application/Work/ManageWorkQueueGetWork.aspx?WorkQueueId=" + WorkQueueSelection.SelectedValue + "', 'WorkQueueSetGetWork_" + WorkQueueSelection.SelectedValue +

                "', 'width=1000,height=700,location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')");



            // UPDATE MONITOR LINK 

            WorkQueueMonitorLink.HRef = @"/Application/Work/WorkQueueMonitor.aspx?WorkQueueId=" + WorkQueueSelection.SelectedValue;

            return;

        }

        private void InitializeWorkQueueViewSelection () {

            WorkQueueViewSelection.DataSource = MercuryApplication.WorkQueueViewsAvailable (true, true);

            WorkQueueViewSelection.DataValueField = "Id";

            WorkQueueViewSelection.DataTextField = "Name";

            WorkQueueViewSelection.DataBind ();

            WorkQueueViewSelection.SelectedValue = "0";


            if (WorkQueueViewSelected != null) { WorkQueueViewSelection.SelectedValue = WorkQueueViewSelected.Id.ToString (); } // ASSIGN TO PREVIOUSLY SELECT WORK QUEUE VIEW

            else if (WorkQueueSelected != null) { WorkQueueViewSelection.SelectedValue = WorkQueueSelected.GetWorkViewId.ToString (); } // ASSIGN TO VIEW FOR THE SELECTED WORK QUEUE

            return;

        }

        private void InitializeAssignWorkQueueSelection () {

            if (AssignWorkQueueSelected == null) { AssignWorkQueueSelected = WorkQueueSelected; }


            WorkQueueItemAssignWorkQueueSelection.DataSource = WorkQueueSelection.DataSource;

            WorkQueueItemAssignWorkQueueSelection.DataValueField = "Id";

            WorkQueueItemAssignWorkQueueSelection.DataTextField = "Name";

            WorkQueueItemAssignWorkQueueSelection.DataBind ();

            WorkQueueItemAssignWorkQueueSelection.SelectedValue = AssignWorkQueueSelected.Id.ToString ();

            
            InitializeAssignUserSelection ();

            return;

        }
                
        private void InitializeAssignUserSelection  () {
            
            Client.Core.Work.WorkQueue workQueue;

            Client.Core.Work.WorkTeam workTeam;


            // DO NOT CHANGE THE ASSIGNMENT WORK QUEUE SELECTION IN THIS FUNCTION, IT MUST BE SET BEFORE 

            // BECAUSE IT IS LOOSELY ASSOCIATED WITH THE WORK QUEUE SELECTION 

            WorkQueueItemAssignUserSelection.Items.Clear ();

            WorkQueueItemAssignUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned", "0||"));


            if (WorkQueueItemAssignWorkQueueSelection.SelectedItem != null) {

                workQueue = MercuryApplication.WorkQueueGet (Int64.Parse (WorkQueueItemAssignWorkQueueSelection.SelectedValue), true);

                if (workQueue != null) {

                    Dictionary<String, String> workPermissions = new Dictionary<String, String> ();

                    Dictionary<String, String> deniedPermissions = new Dictionary<String, String> ();

                    foreach (Mercury.Server.Application.WorkQueueTeam currentWorkQueueTeam in workQueue.WorkTeams) {

                        workTeam = MercuryApplication.WorkTeamGet (currentWorkQueueTeam.WorkTeamId, true);

                        if (workTeam == null) { workTeam = MercuryApplication.WorkTeamGet (currentWorkQueueTeam.WorkTeamId, false); }

                        String userAccountKey = String.Empty;

                        if ((currentWorkQueueTeam.Permission == Mercury.Server.Application.WorkQueueTeamPermission.Work)
                            || (currentWorkQueueTeam.Permission == Mercury.Server.Application.WorkQueueTeamPermission.SelfAssign)
                            || (currentWorkQueueTeam.Permission == Mercury.Server.Application.WorkQueueTeamPermission.Manage)) {

                            foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in workTeam.Membership) {

                                userAccountKey = currentMembership.SecurityAuthorityId.ToString () + "|" + currentMembership.UserAccountId + "|" + currentMembership.UserAccountName;

                                if (!workPermissions.ContainsKey (userAccountKey)) {

                                    workPermissions.Add (userAccountKey, currentMembership.SecurityAuthorityName + ": " + currentMembership.UserDisplayName);

                                }

                            }

                        }

                        else {

                            foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in workTeam.Membership) {

                                userAccountKey = currentMembership.SecurityAuthorityId.ToString () + "|" + currentMembership.UserAccountId + "|" + currentMembership.UserAccountName;

                                if (!deniedPermissions.ContainsKey (userAccountKey)) {

                                    deniedPermissions.Add (userAccountKey, currentMembership.SecurityAuthorityName + ": " + currentMembership.UserDisplayName);

                                }

                            }

                        }

                    }

                    foreach (String currentUserAccount in deniedPermissions.Keys) {

                        if (workPermissions.ContainsKey (currentUserAccount)) {

                            workPermissions.Remove (currentUserAccount);

                        }

                    }

                    foreach (String currentUserAccountKey in workPermissions.Keys) {

                        WorkQueueItemAssignUserSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (workPermissions[currentUserAccountKey], currentUserAccountKey));

                    }

                    WorkQueueItemAssignUserSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;


                }

            }

            return;

        }

        #endregion 


        #region Work Queue Items Grid Events

        private Int32 FilterNodeState (String forNodeValue) {

            Telerik.Web.UI.RadTreeView filterTreeView = (Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView");

            if (filterTreeView == null) { return 1; }
            

            Int32 filterState = 1;

            Telerik.Web.UI.RadTreeNode filterNode;

            filterNode = filterTreeView.FindNodeByValue (forNodeValue);


            if (filterNode.Nodes.Count == 2) {

                Telerik.Web.UI.RadTreeNode filterValue1 = filterNode.Nodes[0];

                Telerik.Web.UI.RadTreeNode filterValue2 = filterNode.Nodes[1];

                filterState = Convert.ToInt32 (filterValue1.Checked) + Convert.ToInt32 (filterValue2.Checked);

            }

            else { filterState = Convert.ToInt32 (filterNode.Checked); }


            return filterState;

        }

        protected void WorkQueueItemsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (!e.IsFromDetailTable) {

                // MASTER TABLE VIEW NEEDS DATA SOURCE

                switch (e.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent: // ALL DATA IS PRE-LOADED

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted: // ALL DATA IS PRE-LOADED


                        Int64 filteredItems = 0;

                        Int64 totalItems = 0;

                        Int32 filterNodeState;


                        List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

                        filters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, WorkQueueSelected.Id));


                        totalItems = MercuryApplication.WorkQueueItemsGetCount (filters, false);

                        #region Create Filters from Selections

                        filterNodeState = FilterNodeState ("FilterIsCompleted");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }

                        filterNodeState = FilterNodeState ("FilterIsAssigned");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("IsAssigned", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }

                        filterNodeState = FilterNodeState ("FilterHasConstraintDatePassed");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("HasConstraintDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }

                        filterNodeState = FilterNodeState ("FilterHasThresholdDatePassed");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("HasThresholdDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }

                        filterNodeState = FilterNodeState ("FilterHasDueDatePassed");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("HasDueDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }

                        filterNodeState = FilterNodeState ("FilterWithinWorkTimeRestrictions");

                        if (filterNodeState != 1) { filters.Add (MercuryApplication.CreateFilterDescriptor ("WithinWorkTimeRestrictions", Mercury.Server.Application.DataFilterOperator.IsEqualTo, (filterNodeState == 2))); }


                        if (FilterNodeState ("FilterWorkQueueItemName") == 1) {

                            Telerik.Web.UI.RadComboBox FilterWorkQueueItemNameOperatorSelection = (Telerik.Web.UI.RadComboBox)

                                ((Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView")).FindNodeByValue ("FilterWorkQueueItemName").FindControl ("FilterWorkQueueItemNameOperatorSelection");

                            Telerik.Web.UI.RadTextBox FilterWorkQueueItemNameValue = (Telerik.Web.UI.RadTextBox)

                                ((Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView")).FindNodeByValue ("FilterWorkQueueItemName").FindControl ("FilterWorkQueueItemNameValue");


                            switch (FilterWorkQueueItemNameOperatorSelection.SelectedValue) {

                                case "Contains": filters.Add (MercuryApplication.CreateFilterDescriptor ("Name", Mercury.Server.Application.DataFilterOperator.Contains, FilterWorkQueueItemNameValue.Text)); break;

                                case "StartsWith": filters.Add (MercuryApplication.CreateFilterDescriptor ("Name", Mercury.Server.Application.DataFilterOperator.StartsWith, FilterWorkQueueItemNameValue.Text)); break;

                                case "EndsWith": filters.Add (MercuryApplication.CreateFilterDescriptor ("Name", Mercury.Server.Application.DataFilterOperator.EndsWith, FilterWorkQueueItemNameValue.Text)); break;

                            }

                        }

                        if (FilterNodeState ("FilterAssignedToDisplayName") == 1) {

                            Telerik.Web.UI.RadComboBox FilterAssignedToDisplayNameOperatorSelection = (Telerik.Web.UI.RadComboBox)

                                ((Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView")).FindNodeByValue ("FilterAssignedToDisplayName").FindControl ("FilterAssignedToDisplayNameOperatorSelection");

                            Telerik.Web.UI.RadTextBox FilterAssignedToDisplayNameValue = (Telerik.Web.UI.RadTextBox)

                                ((Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView")).FindNodeByValue ("FilterAssignedToDisplayName").FindControl ("FilterAssignedToDisplayNameValue");


                            switch (FilterAssignedToDisplayNameOperatorSelection.SelectedValue) {

                                case "Contains": filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserDisplayName", Mercury.Server.Application.DataFilterOperator.Contains, FilterAssignedToDisplayNameValue.Text)); break;

                                case "StartsWith": filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserDisplayName", Mercury.Server.Application.DataFilterOperator.StartsWith, FilterAssignedToDisplayNameValue.Text)); break;

                                case "EndsWith": filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserDisplayName", Mercury.Server.Application.DataFilterOperator.EndsWith, FilterAssignedToDisplayNameValue.Text)); break;

                            }

                        }
                        #endregion


                        // UPDATE COUNT

                        filteredItems = MercuryApplication.WorkQueueItemsGetCount (WorkQueueViewSelected, filters, false);

                        WorkQueueItemsAvailableCount.Text = filteredItems.ToString () + " / " + totalItems.ToString ();

                        WorkQueueItemsGrid.VirtualItemCount = Convert.ToInt32 (filteredItems);


                        List<Mercury.Server.Application.DataSortDescriptor> sorts = new List<Server.Application.DataSortDescriptor> ();

                        foreach (Telerik.Web.UI.GridSortExpression currentSortExpression in WorkQueueItemsGrid.MasterTableView.SortExpressions) {

                            switch (currentSortExpression.SortOrder) {

                                case Telerik.Web.UI.GridSortOrder.Ascending:

                                case Telerik.Web.UI.GridSortOrder.Descending:

                                    sorts.Add (MercuryApplication.CreateSortDescription (currentSortExpression.FieldName, ((currentSortExpression.SortOrder == Telerik.Web.UI.GridSortOrder.Ascending) ? Mercury.Server.Application.DataSortDirection.Ascending : Mercury.Server.Application.DataSortDirection.Descending)));

                                    break;

                            }

                        }


                        Int32 initialRow = (WorkQueueItemsGrid.CurrentPageIndex * WorkQueueItemsGrid.PageSize) + 1;

                        List<Client.Core.Work.WorkQueueItem> workQueueItems = MercuryApplication.WorkQueueItemsGetByViewPage (WorkQueueViewSelected, filters, sorts, initialRow, WorkQueueItemsGrid.PageSize, false);

                        WorkQueueItemsGrid.DataSource = workQueueItems;

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unhandled Master Rebind Reason: " + e.RebindReason);

                        break;

                }

            }

            else { // DETAIL TABLE NEEDS DATA SOURCE 

                WorkQueueItemsGrid.MasterTableView.DetailTables[0].DataSource = WorkQueueItemSenders;

            }            

            return;

        }

        protected void WorkQueueItemsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            //System.Web.UI.Triplet filterTriplet;

            //System.Web.UI.Pair filterPair1;

            //System.Web.UI.Pair filterPair2;


            switch (e.CommandName) {

                case Telerik.Web.UI.RadGrid.HeaderContextMenuFilterCommandName:

                case Telerik.Web.UI.RadGrid.FilterCommandName:

                    #region Method to Filter from Grid Header - Disabled

                    //if (e.CommandArgument is System.Web.UI.Triplet) {

                    //    filterTriplet = (System.Web.UI.Triplet)e.CommandArgument;


                    //    // CREATE FILTER INFORMATION 

                    //    String fieldName = (String) filterTriplet.First;

                    //    filterPair1 = (System.Web.UI.Pair)filterTriplet.Second;

                    //    filterPair2 = (System.Web.UI.Pair)filterTriplet.Third;


                    //    if ((Convert.ToString (filterPair1.First) == "NoFilter") && (Convert.ToString (filterPair2.First) == "NoFilter")) {

                    //        // CLEAR FILTER FOR FIELD

                    //        List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

                    //        foreach (Mercury.Server.Application.DataFilterDescriptor currentFilter in WorkQueueItemFilters) {

                    //            if (currentFilter.PropertyPath != fieldName) { 

                    //                filters.Add (currentFilter);

                    //            }

                    //        }

                    //        WorkQueueItemFilters = filters;

                    //    }

                    //    else {

                    //        System.Web.UI.Pair[] filters = new Pair[2] { filterPair1, filterPair2 };

                    //        foreach (Pair currentFilter in filters) {

                    //            switch ((String)currentFilter.First) {

                    //                case "Contains": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.Contains, currentFilter.Second)); break;

                    //                case "StartsWith": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.StartsWith, currentFilter.Second)); break;

                    //                case "EndsWith": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.EndsWith, currentFilter.Second)); break;

                    //                case "EqualTo": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsEqualTo, currentFilter.Second)); break;

                    //                case "NotEqualTo": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsNotEqualTo, currentFilter.Second)); break;

                    //                case "GreaterThan": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsGreaterThan, currentFilter.Second)); break;

                    //                case "LessThan": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsLessThan, currentFilter.Second)); break;

                    //                case "GreaterThanOrEqualTo": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsGreaterThanOrEqualTo, currentFilter.Second)); break;

                    //                case "LessThanOrEqualTo": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsLessThanOrEqualTo, currentFilter.Second)); break;

                    //                case "IsEmpty": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsEqualTo, String.Empty)); break;

                    //                case "NotIsEmpty": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsNotEqualTo, String.Empty)); break;

                    //                case "IsNull": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsEqualTo, null)); break;

                    //                case "NotIsNull": WorkQueueItemFilters.Add (MercuryApplication.CreateFilterDescriptor (fieldName, Mercury.Server.Application.DataFilterOperator.IsNotEqualTo, null)); break;

                    //                default:

                    //                    System.Diagnostics.Debug.WriteLine ("Unhandled Filter Type: " + currentFilter.First);

                    //                    break;

                    //            }

                    //        } // FOREACH FILTER 

                    //    }

                    //}                    

                    #endregion 

                    break;

                case Telerik.Web.UI.RadGrid.ExpandCollapseCommandName:

                    System.Diagnostics.Debug.WriteLine ("Item Command Argument: " + e.CommandArgument);

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem)e.Item;

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

                    // REBIND AUTOMATICALLY OCCURS FOR DETAIL TABLE 

                    break;

                case "ChangePageSize":

                    Telerik.Web.UI.GridPageSizeChangedEventArgs pageSizeChanged = (Telerik.Web.UI.GridPageSizeChangedEventArgs)e;

                    WorkQueueItemsGrid.PageSize = pageSizeChanged.NewPageSize;

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("Unhandled Command: " + e.CommandName);

                    break;

            }

            return;

        }

        protected void WorkQueueItemGrid_OnSortCommand (Object sender, Telerik.Web.UI.GridSortCommandEventArgs e) {

            WorkQueueItemsGrid.DataSource = null; // FORCE HARD REBIND

            return;

        }

        #endregion


        #region Control Events

        protected void WorkQueueSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (WorkQueueSelection == null) { return; }

            if (WorkQueueSelection.SelectedItem == null) { return; }



            // CHANGE SELECTED WORK QUEUE 

            WorkQueueSelection.SelectedValue = eventArgs.Value;

            WorkQueueSelected = MercuryApplication.WorkQueueGet (Convert.ToInt64 (eventArgs.Value), true);

            WorkQueueViewSelected = MercuryApplication.WorkQueueViewGet (WorkQueueSelected.GetWorkViewId, true);

            WorkQueueViewSelection.SelectedValue = WorkQueueSelected.GetWorkViewId.ToString ();


            WorkQueueItemsGrid.Rebind ();


            // CHANGE SELECTED ASSIGN FOR WORK QUEUE

            AssignWorkQueueSelected = MercuryApplication.WorkQueueGet (eventArgs.Value, true);

            InitializeAssignWorkQueueSelection ();

            InitializeMonitorLink ();

            return;

        }

        protected void WorkQueueViewSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (WorkQueueViewSelection == null) { return; }

            if (WorkQueueViewSelection.SelectedItem == null) { return; }



            // CHANGE SELECTED WORK QUEUE 

            WorkQueueViewSelection.SelectedValue = eventArgs.Value;

            WorkQueueViewSelected = MercuryApplication.WorkQueueViewGet (Convert.ToInt64 (eventArgs.Value), true);


            WorkQueueItemsGrid.DataSource = null;

            WorkQueueItemsGrid.Rebind ();


            return;

        }

        protected void BasicFiltersTreeView_OnNodeCheck (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs e) {

            Telerik.Web.UI.RadTreeNode filterNode = e.Node;

            Telerik.Web.UI.RadTreeView filterTreeView = (Telerik.Web.UI.RadTreeView)BasicFiltersSelection.Items[0].FindControl ("BasicFiltersTreeView");

            if (filterTreeView == null) { return; }

            if (filterNode.Value.Contains ("Value")) { return; }


            if (filterNode.Nodes.Count == 2) {

                Telerik.Web.UI.RadTreeNode filterValue1 = filterNode.Nodes[0];

                Telerik.Web.UI.RadTreeNode filterValue2 = filterNode.Nodes[1];

                Int32 filterState = Convert.ToInt32 (filterValue1.Checked) + Convert.ToInt32 (filterValue2.Checked);


                switch ((filterState + 1)) {

                    case 1:  // MOVE FROM NO SELECTED TO INDETERMINATE

                        filterTreeView.FindNodeByValue (filterValue1.Value).Checked = true;

                        filterTreeView.FindNodeByValue (filterValue2.Value).Checked = false;

                        break;

                    case 2:  // MOVE FROM INDETERMINATE TO SELECTED

                        filterTreeView.FindNodeByValue (filterValue1.Value).Checked = true;

                        filterTreeView.FindNodeByValue (filterValue2.Value).Checked = true;

                        break;

                    case 3:  // MOVE FROM SELECTED TO UNSELECTED

                        filterTreeView.FindNodeByValue (filterValue1.Value).Checked = false;

                        filterTreeView.FindNodeByValue (filterValue2.Value).Checked = false;

                        break;

                }

            }

            WorkQueueItemsGrid.Rebind ();

            return;

        }

        protected void WorkQueueItemsGridRefresh_OnClick (Object sender, EventArgs e) {

            WorkQueueItemSenders = null;

            WorkQueueItemsGrid.DataSource = null;

            WorkQueueItemsGrid.Rebind ();

            return;

        }

        #endregion 

        
        #region Dialog Window Events
        
        protected void WorkQueueItemAssignWorkQueueSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (WorkQueueSelection == null) { return; }

            if (WorkQueueItemAssignWorkQueueSelection.SelectedItem == null) { return; }



            // CHANGE SELECTED WORK QUEUE 

            AssignWorkQueueSelected = MercuryApplication.WorkQueueGet (Convert.ToInt64 (eventArgs.Value), true);

            InitializeAssignWorkQueueSelection ();

            return;

        }

        protected void WorkQueueItemAssignWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            Boolean success = false;

            String responseScript = "$find(\"" + WorkQueueItemAssignWindow.ClientID + "\").close ();";

            String postScript = String.Empty;


            Int64 workQueueItemId = 0;


            // RE-ASSIGN WORK QUEUE ITEM

            if (Int64.TryParse (WorkQueueItemAssignId.Text, out workQueueItemId)) {

                Int64 assignedToWorkQueueId = Int64.Parse (WorkQueueItemAssignWorkQueueSelection.SelectedValue);

                Int64 assignedToSecurityAuthorityId = Int64.Parse (WorkQueueItemAssignUserSelection.SelectedValue.Split ('|')[0]);

                String assignedToUserAccountId = WorkQueueItemAssignUserSelection.SelectedValue.Split ('|')[1];

                String assignedToUserAccountName = WorkQueueItemAssignUserSelection.SelectedValue.Split ('|')[2];

                String assignedToUserDisplayName = (WorkQueueItemAssignUserSelection.Text.Split (':').Length >= 2) ? WorkQueueItemAssignUserSelection.Text.Split (':')[1] : String.Empty;


                Client.Core.Work.WorkQueueItem workQueueItem = MercuryApplication.WorkQueueItemGet (workQueueItemId);

                if (assignedToWorkQueueId != workQueueItem.WorkQueueId) {

                    success = workQueueItem.MoveToQueue (assignedToWorkQueueId);

                    if (!success) { postScript = ("alert (\"" + MercuryApplication.LastException.Message.Replace ("\"", "\\") + "\");"); }

                }

                else { success = true; }

                if (success) {

                    success = workQueueItem.AssignTo (assignedToSecurityAuthorityId, assignedToUserAccountId, assignedToUserAccountName, assignedToUserDisplayName, "Work Queue Management - Direct Assignment");

                }

            }

            else { responseScript += "alert('Unable to parse the Work Queue Item Id.');"; }


            // REBIND DATE GRID

            WorkQueueItemsGrid.DataSource = null;

            WorkQueueItemsGrid.Rebind ();


            // CLIENT-SIDE CLOSE DIALOG WINDOW

            Master.TelerikAjaxManagerControl.ResponseScripts.Add (responseScript + postScript);

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

            WorkQueueItemsGrid.DataSource = null;

            WorkQueueItemsGrid.Rebind ();


            // CLIENT-SIDE CLOSE DIALOG WINDOW

            Master.TelerikAjaxManagerControl.ResponseScripts.Add (responseScript);

            return;

        }

        #endregion 


    }

}
