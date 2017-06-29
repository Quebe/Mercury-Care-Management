using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberWorkHistory : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean pageSizeChanged = false;


        #endregion


        #region Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }


        public Client.Core.Member.Member Member {

            get { return (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"]; }

            set { 

                Client.Core.Member.Member member = (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"];

                if (member != value) {

                    Session[SessionCachePrefix + "Member"] = value;

                    MemberWorkHistoryGrid.DataSource = null;

                    MemberWorkHistoryGrid.Rebind ();

                }
            
            }

        }

        public Unit HistoryGridHeight { get { return MemberWorkHistoryGrid.Height; } set { MemberWorkHistoryGrid.Height = value; } }

        public Int32 HistoryGridPageSize { get { return MemberWorkHistoryGrid.PageSize; } set { MemberWorkHistoryGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean) Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                Session[SessionCachePrefix + "AllowUserInteraction"] = value;

            }

        }


        private List<Client.Core.Work.WorkQueueItem> WorkQueueItems {

            get {

                List<Client.Core.Work.WorkQueueItem> workQueueItems = (List<Client.Core.Work.WorkQueueItem>) Session[SessionCachePrefix + "WorkQueueItems"];

                if (workQueueItems == null) { workQueueItems = new List<Mercury.Client.Core.Work.WorkQueueItem> (); }

                return workQueueItems;

            }

            set { Session[SessionCachePrefix + "WorkQueueItems"] = value; }

        }

        private System.Data.DataTable MemberWorkHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberWorkHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("WorkQueueItemStatus");

                    dataTable.Columns.Add ("WorkQueueId");

                    dataTable.Columns.Add ("WorkQueueItemId");

                    dataTable.Columns.Add ("WorkQueueName");

                    dataTable.Columns.Add ("WorkflowName");

                    dataTable.Columns.Add ("WorkflowLastStep");

                    dataTable.Columns.Add ("WorkflowNextStep");

                    dataTable.Columns.Add ("AddedDate");

                    dataTable.Columns.Add ("LastWorkedDate");

                    dataTable.Columns.Add ("ConstraintDate");

                    dataTable.Columns.Add ("MilestoneDate");

                    dataTable.Columns.Add ("ThresholdDate");

                    dataTable.Columns.Add ("DueDate");

                    dataTable.Columns.Add ("CompletionDate");

                    dataTable.Columns.Add ("Outcome");

                    dataTable.Columns.Add ("Priority");

                    dataTable.Columns.Add ("AssignedTo");

                    dataTable.Columns.Add ("AssignedToDate");

                    Session[SessionCachePrefix + "MemberWorkHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberWorkHistoryGrid_DataTable"] = value; }

        }

        private Int32 MemberWorkHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberWorkHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberWorkHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberWorkHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberWorkHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "MemberWorkHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 MemberWorkHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberWorkHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberWorkHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberWorkHistoryGrid_Count"] = value; }

        }


        private System.Data.DataTable MemberWorkHistoryGrid_SenderTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberWorkHistoryGrid_SenderTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("WorkQueueItemId");

                    dataTable.Columns.Add ("WorkQueueItemSenderId");

                    dataTable.Columns.Add ("EventDescription");

                    dataTable.Columns.Add ("Priority");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("CreateDate");

                    Session[SessionCachePrefix + "MemberWorkHistoryGrid_SenderTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberWorkHistoryGrid_SenderTable"] = value; }

        }


        private Telerik.Web.UI.RadToolBar MemberWorkHistoryToolbar {

            get {

                Telerik.Web.UI.RadToolBar toolbar = null;

                if (MemberWorkHistoryGrid.MasterTableView.Controls[0] != null) {

                    if (MemberWorkHistoryGrid.MasterTableView.Controls[0].Controls[0] != null) {

                        if (MemberWorkHistoryGrid.MasterTableView.Controls[0].Controls[0].Controls[0] != null) {

                            toolbar = (Telerik.Web.UI.RadToolBar) MemberWorkHistoryGrid.MasterTableView.Controls[0].Controls[0].Controls[0].FindControl ("MemberWorkHistoryToolbar");

                        }

                    }

                }

                return toolbar;

            }

        }

        private String WorkQueueSelection_SelectedValue {

            get {

                String selectedValue = (String) Session[SessionCachePrefix + "WorkQueueSelection_SelectedValue"];

                if (selectedValue == null) { selectedValue = String.Empty; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "WorkQueueSelection_SelectedValue"] = value; }

        }

        private Double? WorkQueueItemPriority_Value {

            get {

                Double? selectedValue = (Double?) Session[SessionCachePrefix + "WorkQueueItemPriority_Value"];

                if (selectedValue == null) { selectedValue = 0; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "WorkQueueItemPriority_Value"] = value; }



        }

        #endregion


        #region Page Events

        protected void Page_Load (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        #endregion 


        #region Support Methods

        private List<Mercury.Server.Application.DataFilterDescriptor> MemberFilters () {

            List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

            filters.Add (MercuryApplication.CreateFilterDescriptor ("ItemObjectType", Mercury.Server.Application.DataFilterOperator.IsEqualTo, "Member"));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("ItemObjectId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, Member.Id));

            return filters;

        }

        #endregion 


        #region Control Events

        protected void MemberWorkHistoryGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar MemberWorkHistoryToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("MemberWorkHistoryToolbar");

                if (AllowUserInteraction) { MemberWorkHistoryToolbar.Enabled = true; }

                else { MemberWorkHistoryToolbar.Enabled = false; }


                if (AllowUserInteraction) {

                    Telerik.Web.UI.RadComboBox WorkQueueSelection = (Telerik.Web.UI.RadComboBox) MemberWorkHistoryToolbar.Items[0].FindControl ("WorkQueueSelection");

                    Telerik.Web.UI.RadNumericTextBox WorkQueueItemPriority = (Telerik.Web.UI.RadNumericTextBox) (MemberWorkHistoryToolbar.Items[0].FindControl ("WorkQueueItemPriority"));

                    if (WorkQueueSelection != null) {

                        if (WorkQueueSelection.Items.Count == 0) {

                            WorkQueueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Work Queue Selected", String.Empty));

                            foreach (Client.Core.Work.WorkQueue currentWorkQueue in MercuryApplication.WorkQueuesAvailable (true)) {

                                if (MercuryApplication.SessionWorkQueueHasManagePermission (currentWorkQueue.Id)) {

                                    WorkQueueSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueue.Name, currentWorkQueue.Id.ToString ()));

                                }

                            }

                        }

                        WorkQueueSelection.SelectedValue = WorkQueueSelection_SelectedValue;

                    }

                    if (WorkQueueItemPriority != null) {

                        WorkQueueItemPriority.Value = WorkQueueItemPriority_Value;

                    }

                }

            }

            return;

        }

        protected void MemberWorkHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = MemberWorkHistoryGrid_DataTable;


            if (!eventArgs.IsFromDetailTable) {

                switch (eventArgs.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                        #region Initialize Grid

                        MemberWorkHistoryGrid_Count = 0;

                        MemberWorkHistoryGrid_CurrentPage = 0;

                        MemberWorkHistoryGrid_PageSize = 10;


                        MemberWorkHistoryGrid.CurrentPageIndex = MemberWorkHistoryGrid_CurrentPage;

                        MemberWorkHistoryGrid.PageSize = MemberWorkHistoryGrid_PageSize;

                        MemberWorkHistoryGrid.VirtualItemCount = MemberWorkHistoryGrid_Count;

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        #region Restore Grid State

                        MemberWorkHistoryGrid.CurrentPageIndex = MemberWorkHistoryGrid_CurrentPage;

                        MemberWorkHistoryGrid.PageSize = MemberWorkHistoryGrid_PageSize;

                        MemberWorkHistoryGrid.VirtualItemCount = MemberWorkHistoryGrid_Count;


                        if (MemberWorkHistoryToolbar != null) {

                            Telerik.Web.UI.RadComboBox WorkQueueSelection = (Telerik.Web.UI.RadComboBox) MemberWorkHistoryToolbar.Items[0].FindControl ("WorkQueueSelection");

                            if (WorkQueueSelection != null) {

                                WorkQueueSelection_SelectedValue = WorkQueueSelection.SelectedValue;

                            }

                            Telerik.Web.UI.RadNumericTextBox WorkQueueItemPriority = (Telerik.Web.UI.RadNumericTextBox) (MemberWorkHistoryToolbar.Items[0].FindControl ("WorkQueueItemPriority"));

                            if (WorkQueueItemPriority != null) {

                                WorkQueueItemPriority_Value = WorkQueueItemPriority.Value;

                            }

                        }

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                        #region Rebind Grid

                        if (Member == null) { dataTable.Rows.Clear (); }

                        else {

                            if (MemberWorkHistoryGrid_Count == 0) {

                                MemberWorkHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.WorkQueueItemsGetCount (MemberFilters (), false));

                                // OLD METHOD BELOW

                                // MemberWorkHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.WorkQueueItemGetCountByObject ("Member", Member.Id));

                                MemberWorkHistoryGrid.VirtualItemCount = MemberWorkHistoryGrid_Count;

                            }

                            if (!pageSizeChanged) {

                                MemberWorkHistoryGrid_PageSize = MemberWorkHistoryGrid.PageSize;

                            }

                            else {

                                MemberWorkHistoryGrid.PageSize = MemberWorkHistoryGrid_PageSize;

                                pageSizeChanged = false;

                            }

                            MemberWorkHistoryGrid_CurrentPage = MemberWorkHistoryGrid.CurrentPageIndex;

                            dataTable.Rows.Clear ();

                            List<Client.Core.Work.WorkQueueItem> memberWorkHistory;

                            Int32 initialRow = MemberWorkHistoryGrid.CurrentPageIndex * MemberWorkHistoryGrid.PageSize + 1;

                            memberWorkHistory = MercuryApplication.WorkQueueItemsGetByViewPage ((Client.Core.Work.WorkQueueView) null, MemberFilters (), null, initialRow, MemberWorkHistoryGrid.PageSize, false);

                            // OLD METHOD BELOW

                            // memberWorkHistory = MercuryApplication.WorkQueueItemGetByObjectPage ("Member", Member.MemberId, initialRow, MemberWorkHistoryGrid.PageSize);

                            WorkQueueItems = memberWorkHistory;

                            foreach (Client.Core.Work.WorkQueueItem currentItem in memberWorkHistory) {

                                dataTable.Rows.Add (

                                    "&nbsp",

                                    currentItem.WorkQueueId.ToString (),

                                    currentItem.Id.ToString (),

                                    currentItem.WorkQueueName,

                                    currentItem.WorkflowName,

                                    currentItem.WorkflowLastStep,

                                    currentItem.WorkflowNextStep,

                                    currentItem.AddedDate.ToString ("MM/dd/yyyy"),

                                    (currentItem.LastWorkedDate.HasValue) ? currentItem.LastWorkedDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                                    currentItem.ConstraintDate.ToString ("MM/dd/yyyy"),

                                    currentItem.MilestoneDate.ToString ("MM/dd/yyyy"),

                                    currentItem.ThresholdDate.ToString ("MM/dd/yyyy"),

                                    currentItem.DueDate.ToString ("MM/dd/yyyy"),

                                    (currentItem.CompletionDate.HasValue) ? currentItem.CompletionDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                                    currentItem.WorkOutcomeName,

                                    currentItem.Priority,

                                    (!String.IsNullOrEmpty (currentItem.AssignedToUserAccountName)) ? currentItem.AssignedToUserAccountName : "&nbsp",

                                    (currentItem.AssignedToDate.HasValue) ? currentItem.AssignedToDate.Value.ToString ("MM/dd/yyyy") : "&nbsp"

                                );

                            }

                        }

                        #endregion

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                        break;

                }

            }

            MemberWorkHistoryGrid_DataTable = dataTable;

            MemberWorkHistoryGrid.DataSource = MemberWorkHistoryGrid_DataTable;

            MemberWorkHistoryGrid.MasterTableView.DetailTables[0].DataSource = MemberWorkHistoryGrid_SenderTable;

            return;

        }

        protected void MemberWorkHistoryGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            String anchorText;

            String parameterString = String.Empty;


            if (eventArgs.Item is Telerik.Web.UI.GridDataItem) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                if (gridItem.OwnerTableView.Name != "MemberWorkHistoryMasterView") { return; }


                Client.Core.Work.WorkQueueItem workQueueItem = null;

                Int64 workQueueItemId = Convert.ToInt64 (gridItem["WorkQueueItemId"].Text);

                foreach (Client.Core.Work.WorkQueueItem currentWorkQueueItem in WorkQueueItems) {

                    if (currentWorkQueueItem.Id == workQueueItemId) {

                        workQueueItem = currentWorkQueueItem;

                        break;

                    }

                }

                if (workQueueItem == null) { workQueueItem = MercuryApplication.WorkQueueItemGet (workQueueItemId); }


                String statusImage = String.Empty;


                if (!workQueueItem.CompletionDate.HasValue) {

                    if (workQueueItem.MilestoneDate <= DateTime.Today) { statusImage = "Warn.png"; }

                    if (workQueueItem.ThresholdDate <= DateTime.Today) { statusImage = "Warn.png"; }

                    if (workQueueItem.DueDate <= DateTime.Today) { statusImage = "Stop.png"; }

                }

                else { statusImage = "Ok.png"; }

                if (!String.IsNullOrEmpty (statusImage)) {

                    statusImage = "/Images/Common16/" + statusImage;

                    anchorText = "<img src=\"" + statusImage + "\" alt=\"Status\" style=\"padding: 2px;\" />";

                    gridItem["WorkQueueItemStatus"].Text = anchorText;

                }

                Client.Core.Work.WorkQueue workQueue = MercuryApplication.WorkQueueGet (Convert.ToInt64 (gridItem["WorkQueueId"].Text), true);

                if (workQueue == null) { workQueue = MercuryApplication.WorkQueueGet (Convert.ToInt64 (gridItem["WorkQueueId"].Text), false); }

                if (workQueue != null) {

                    // DETAILS COLUMN

                    if (AllowUserInteraction) {

                        parameterString = " <a href=\"/Application/Work/WorkQueueItemDetail.aspx?WorkQueueItemId=" + workQueueItem.Id.ToString () + "\" target=\"_blank\">(details)</a>";

                        gridItem["WorkQueueItemStatus"].Text = gridItem["WorkQueueItemStatus"].Text + parameterString;

                    }

                    // ACTION COLUMN

                    if ((workQueueItem != null) && (AllowUserInteraction)) {

                        #region Assign/Work To Column

                        anchorText = gridItem["AssignedTo"].Text;

                        if ((!workQueueItem.CompletionDate.HasValue) && (workQueueItem.WorkQueue.WorkflowId != 0)) {

                            // SELF-ASSIGN (IF NO MANAGE PERMISSION)

                            //if ((workQueue.HasSelfAssignPermission) && !((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.AuthorityId) && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId))) {

                            if ((workQueue.WorkflowId != 0) && ((MercuryApplication.SessionWorkQueueHasSelfAssignPermission (workQueue.Id)) || (MercuryApplication.SessionWorkQueueHasManagePermission (workQueue.Id))) 
                            
                                || ((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.SecurityAuthorityId) && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId))) {

                                parameterString = String.Empty;

                                parameterString = parameterString + "/Application/Workflow/Workflow.aspx";

                                if (workQueueItem.WorkflowInstanceId == Guid.Empty) {

                                    parameterString = parameterString + "?WorkflowId=" + workQueue.WorkflowId.ToString ();

                                    parameterString = parameterString + "&WorkQueueItemId=" + workQueueItem.Id.ToString ();

                                    parameterString = parameterString + "&" + workQueueItem.ItemObjectType + "Id=" + workQueueItem.ItemObjectId.ToString ();

                                    parameterString = parameterString + "&Source=MemberProfile";


                                    anchorText = anchorText + " <a href=\"" + parameterString + "\">(work)</a>";

                                }

                                else {

                                    parameterString = parameterString + "?WorkflowId=" + workQueue.WorkflowId.ToString ();

                                    parameterString = parameterString + "&WorkflowInstanceId=" + workQueueItem.WorkflowInstanceId.ToString ();

                                    parameterString = parameterString + "&Source=MemberProfile";

                                    anchorText = anchorText + " <a href=\"" + parameterString + "\">(work - resume)</a>";

                                }


                            }


                    //        // RELEASE ASSIGNMENT (EITHER SELF ASSIGNED OR MANAGE)

                    //        else if (!(workQueue.HasManagePermission) && ((workQueueItem.AssignedToSecurityAuthorityId == MercuryApplication.Session.AuthorityId) && (workQueueItem.AssignedToUserAccountId == MercuryApplication.Session.UserAccountId))) {

                    //            parameterString = String.Empty;

                    //            parameterString = parameterString + workQueueItem.WorkQueueItemId.ToString () + ", "; // WORK QUEUE ITEM ID

                    //            parameterString = parameterString + "'" + anchorText.Replace ("'", "''") + "', "; // CURRENT ASSIGNMENT

                    //            parameterString = parameterString + "'0', "; // SECURITY AUTHORITY ID

                    //            parameterString = parameterString + "'', "; // USER ACCOUNT ID

                    //            parameterString = parameterString + "'** Not Assigned'"; // USER ACCOUNT ID

                    //            anchorText = anchorText + " <a href=\"javascript:MyWorkQueues_WorkQueueItem_OnAssignItem_" + WorkQueueAction_WorkQueueItemId.ClientID.Replace ('.', '_') + " (" + parameterString + ")\">(release)</a>";

                    //        }

                    //        // CHANGE ASSIGNMENT (MANAGE)

                    //        else if (workQueue.HasManagePermission) {

                    //            parameterString = String.Empty;

                    //            parameterString = parameterString + workQueueItem.WorkQueueItemId.ToString () + ", "; // WORK QUEUE ITEM ID

                    //            parameterString = parameterString + "'" + workQueueItem.ObjectType + ": " + workQueueItem.ItemDescription.Replace ("'", "''") + "', ";

                    //            parameterString = parameterString + "'" + workQueueItem.AssignedToSecurityAuthorityId.ToString () + "|" + workQueueItem.AssignedToUserAccountId + "'";

                    //            anchorText = anchorText + " <a href=\"javascript:MyWorkQueues_WorkQueueItem_OnChangeAssignment_" + WorkQueueAction_WorkQueueItemId.ClientID.Replace ('.', '_') + " (" + parameterString + ")\">(change)</a>";

                    //        }

                            gridItem["AssignedTo"].Text = anchorText;

                        }

                        #endregion


                        #region Completed Workflow Steps Column

                        String completedAnchor = gridItem["CompletionDate"].Text;


                        if (workQueueItem.CompletionDate.HasValue) {

                        }


                        gridItem["CompletionDate"].Text = completedAnchor;

                        #endregion 

                    }

                }

            }

            return;

        }

        protected void MemberWorkHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            //System.Data.DataTable detailTable = null;

            Telerik.Web.UI.RadToolBar gridToolBar = null;

            switch (eventArgs.CommandName) {

                case "ExpandCollapse":

                    #region Expand/Collapse

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                    Int64 workQueueItemId;


                    if (Int64.TryParse (gridItem["WorkQueueItemId"].Text, out workQueueItemId)) {

                        System.Data.DataTable sendersTable = MemberWorkHistoryGrid_SenderTable;

                        sendersTable.Rows.Clear ();


                        List<Mercury.Client.Core.Work.WorkQueueItemSender> senders = MercuryApplication.WorkQueueItemSendersGet (workQueueItemId, false);

                        foreach (Mercury.Client.Core.Work.WorkQueueItemSender currentSender in senders) {

                            sendersTable.Rows.Add (

                                workQueueItemId.ToString (),

                                currentSender.Id.ToString (),

                                currentSender.EventDescription,

                                currentSender.Priority,

                                currentSender.CreateAccountInfo.UserAccountName,

                                currentSender.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                                );

                        }

                        MemberWorkHistoryGrid.MasterTableView.DetailTables[0].DataSource = sendersTable;

                        MemberWorkHistoryGrid.MasterTableView.DetailTables[0].DataBind ();

                    }

                    #endregion

                    break;

                case "WorkQueueItemAdd":

                    #region Add to Work Queue

                    gridToolBar = (Telerik.Web.UI.RadToolBar) eventArgs.Item.FindControl ("MemberWorkHistoryToolbar");

                    if (gridToolBar != null) {

                        Telerik.Web.UI.RadComboBox WorkQueueSelection = (Telerik.Web.UI.RadComboBox) (gridToolBar.Items[0].FindControl ("WorkQueueSelection"));

                        Telerik.Web.UI.RadNumericTextBox WorkQueueItemPriority = (Telerik.Web.UI.RadNumericTextBox) (gridToolBar.Items[0].FindControl ("WorkQueueItemPriority"));

                        if (WorkQueueSelection != null) {

                            if (!String.IsNullOrEmpty (WorkQueueSelection.SelectedValue)) {

                                Int64 workQueueId = Int64.Parse (WorkQueueSelection.SelectedValue);

                                Int32 priority = 0;


                                if (WorkQueueItemPriority.Value.HasValue) {

                                    Int32.TryParse (WorkQueueItemPriority.Value.Value.ToString (), out priority);

                                }
                                

                                Boolean insertSuccess = MercuryApplication.WorkQueueInsertEntity (workQueueId, Member.EntityId, null, null, 0, "Manual Addition", priority);

                                MemberWorkHistoryGrid_ManualDataRebind ();

                            }

                        }

                    }

                    #endregion 

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("MemberMetricsGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;

            }

            return;

        }

        protected void MemberWorkHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberWorkHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberWorkHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberWorkHistoryGrid_ManualDataRebind ();

            }

        }

        public void MemberWorkHistoryGrid_ManualDataRebind () {

            MemberWorkHistoryGrid_Count = 0;

            MemberWorkHistoryGrid.DataSource = null;

            MemberWorkHistoryGrid.Rebind ();

            return;

        }

        #endregion

    }

}