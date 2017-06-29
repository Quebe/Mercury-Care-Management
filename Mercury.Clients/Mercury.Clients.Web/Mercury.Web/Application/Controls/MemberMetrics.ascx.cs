using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberMetrics : System.Web.UI.UserControl {


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

        private Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager) Page.FindControl ("TelerikAjaxManager"); } }


        public Client.Core.Member.Member Member {

            get { return (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"]; }

            set {

                Client.Core.Member.Member member = (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"];

                if (member != value) {

                    Session[SessionCachePrefix + "Member"] = value;

                    MemberMetricsGrid.DataSource = null;

                    MemberMetricsGrid.Rebind ();

                }

            }

        }

        public Unit HistoryGridHeight { get { return MemberMetricsGrid.Height; } set { MemberMetricsGrid.Height = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        private System.Data.DataTable MemberMetricsGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberMetricsGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberMetricId");

                    dataTable.Columns.Add ("MetricName");

                    dataTable.Columns.Add ("MetricType");

                    dataTable.Columns.Add ("MetricValue");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("AddedManually");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("CreateDate");

                    Session[SessionCachePrefix + "MemberMetricsGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberMetricsGrid_DataTable"] = value; }

        }

        private Int32 MemberMetricsGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberMetricsGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberMetricsGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberMetricsGrid_CurrentPage"] = value; }

        }

        private Int32 MemberMetricsGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberMetricsGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberMetricsGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberMetricsGrid_PageSize"] = value; }

        }

        private Int32 MemberMetricsGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberMetricsGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberMetricsGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberMetricsGrid_Count"] = value; }

        }


        private Telerik.Web.UI.RadToolBar MemberMetricToolbar {

            get {

                Telerik.Web.UI.RadToolBar toolbar = null;

                if (MemberMetricsGrid.MasterTableView.Controls[0] != null) {

                    if (MemberMetricsGrid.MasterTableView.Controls[0].Controls[0] != null) {

                        if (MemberMetricsGrid.MasterTableView.Controls[0].Controls[0].Controls[0] != null) {

                            toolbar = (Telerik.Web.UI.RadToolBar) MemberMetricsGrid.MasterTableView.Controls[0].Controls[0].Controls[0].FindControl ("MemberMetricToolbar");

                        }

                    }

                }

                return toolbar;

            }

        }

        private Boolean MemberMetricShowHidden {

            get {

                Boolean showHidden = false;

                if (Session[SessionCachePrefix + "MemberMetricShowHidden"] != null) {

                    showHidden = (Boolean) Session[SessionCachePrefix + "MemberMetricShowHidden"];

                }

                return showHidden;

            }

            set { Session[SessionCachePrefix + "MemberMetricShowHidden"] = value; }

        }

        private String MemberMetricSelection_SelectedValue {

            get {

                String selectedValue = (String) Session[SessionCachePrefix + "MemberMetricSelection_SelectedValue"];

                if (selectedValue == null) { selectedValue = String.Empty; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberMetricSelection_SelectedValue"] = value; }

        }

        private DateTime? MemberMetricEventDate_SelectedDate {

            get {

                DateTime? selectedValue = (DateTime?) Session[SessionCachePrefix + "MemberMetricEventDate_SelectedDate"];

                if (selectedValue == null) { selectedValue = null; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberMetricEventDate_SelectedDate"] = value; }

        }

        private Double? MemberMetricValue_SelectedValue {

            get {

                Double? selectedValue = (Double?) Session[SessionCachePrefix + "MemberMetricValue_SelectedValue"];

                if (selectedValue == null) { selectedValue = null; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberMetricValue_SelectedValue"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        #endregion


        #region Member Metrics Grid Events

        protected void MemberMetricsGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar MemberMetricToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("MemberMetricToolbar");

                ((Telerik.Web.UI.RadToolBarButton) MemberMetricToolbar.Items[0]).Checked = MemberMetricShowHidden;

                Telerik.Web.UI.RadComboBox MemberMetricSelection = (Telerik.Web.UI.RadComboBox) MemberMetricToolbar.Items[2].FindControl ("MemberMetricSelection");

                Telerik.Web.UI.RadDateInput MemberMetricEventDate = (Telerik.Web.UI.RadDateInput) MemberMetricToolbar.Items[2].FindControl ("MemberMetricEventDate");

                Telerik.Web.UI.RadNumericTextBox MemberMetricValue = (Telerik.Web.UI.RadNumericTextBox) MemberMetricToolbar.Items[2].FindControl ("MemberMetricValue");

                if (MemberMetricSelection != null) {

                    if (MemberMetricSelection.Items.Count == 0) {

                        MemberMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Metric Selected", String.Empty));

                        foreach (Client.Core.Metrics.Metric currentMetric in MercuryApplication.MetricsAvailable (true)) { 

                            if (currentMetric.Enabled) {

                                MemberMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMetric.Name, currentMetric.Id.ToString ()));

                            }

                        }

                    }

                    MemberMetricSelection.SelectedValue = MemberMetricSelection_SelectedValue;

                }

                if (MemberMetricEventDate != null) {

                    if (Member != null) { MemberMetricEventDate.MinDate = Member.BirthDate; }
    
                    MemberMetricEventDate.SelectedDate = MemberMetricEventDate_SelectedDate; 
                
                }

                if (MemberMetricValue != null) { MemberMetricValue.Value = MemberMetricValue_SelectedValue; }

            }

            return;

        }

        protected void MemberMetricsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable memberMetricsDataTable = MemberMetricsGrid_DataTable;

            if (!eventArgs.IsFromDetailTable) {

                switch (eventArgs.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                        #region Initialize Grid

                        MemberMetricsGrid_Count = 0;

                        MemberMetricsGrid_CurrentPage = 0;

                        MemberMetricsGrid_PageSize = 10;


                        MemberMetricsGrid.CurrentPageIndex = MemberMetricsGrid_CurrentPage;

                        MemberMetricsGrid.PageSize = MemberMetricsGrid_PageSize;

                        MemberMetricsGrid.VirtualItemCount = MemberMetricsGrid_Count;

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        #region Restore Grid State

                        MemberMetricsGrid.CurrentPageIndex = MemberMetricsGrid_CurrentPage;

                        MemberMetricsGrid.PageSize = MemberMetricsGrid_PageSize;

                        MemberMetricsGrid.VirtualItemCount = MemberMetricsGrid_Count;


                        if (MemberMetricToolbar != null) {

                            Telerik.Web.UI.RadComboBox MemberMetricSelection = (Telerik.Web.UI.RadComboBox) MemberMetricToolbar.Items[2].FindControl ("MemberMetricSelection");

                            if (MemberMetricSelection != null) {

                                MemberMetricSelection_SelectedValue = MemberMetricSelection.SelectedValue;

                            }

                            Telerik.Web.UI.RadDateInput MemberMetricEventDate = (Telerik.Web.UI.RadDateInput) MemberMetricToolbar.Items[2].FindControl ("MemberMetricEventDate");

                            if (MemberMetricEventDate != null) {

                                MemberMetricEventDate_SelectedDate = MemberMetricEventDate.SelectedDate;

                            }

                            Telerik.Web.UI.RadNumericTextBox MemberMetricValue = (Telerik.Web.UI.RadNumericTextBox) MemberMetricToolbar.Items[2].FindControl ("MemberMetricValue");

                            if (MemberMetricValue != null) {

                                MemberMetricValue_SelectedValue = MemberMetricValue.Value;

                            }

                        }

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                        #region Initialize Toolbar and Security

                        if (MemberMetricToolbar != null) {

                            MemberMetricToolbar.Items[1].Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberMetricManage);

                            MemberMetricToolbar.Items[2].Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberMetricManage);

                        }

                        #endregion

                        #region Rebind Grid

                        if (Member == null) { memberMetricsDataTable.Rows.Clear (); }

                        else {

                            if (MemberMetricsGrid_Count == 0) {

                                MemberMetricsGrid_Count = Convert.ToInt32 (MercuryApplication.MemberMetricsGetCount (Member.Id, MemberMetricShowHidden));

                                MemberMetricsGrid.VirtualItemCount = MemberMetricsGrid_Count;

                            }

                            MemberMetricsGrid_PageSize = MemberMetricsGrid.PageSize;

                            MemberMetricsGrid_CurrentPage = MemberMetricsGrid.CurrentPageIndex;

                            memberMetricsDataTable.Rows.Clear ();

                            List<Mercury.Server.Application.MemberMetric> memberMetrics;

                            memberMetrics = MercuryApplication.MemberMetricsGetByPage (Member.Id, (MemberMetricsGrid.CurrentPageIndex) * MemberMetricsGrid.PageSize + 1, MemberMetricsGrid.PageSize, MemberMetricShowHidden);

                            foreach (Mercury.Server.Application.MemberMetric currentMetric in memberMetrics) {

                                memberMetricsDataTable.Rows.Add (

                                    currentMetric.Id.ToString (),

                                    currentMetric.Metric.Name,

                                    currentMetric.Metric.MetricType,

                                    currentMetric.MetricValue.ToString (),

                                    currentMetric.EventDate.ToString ("MM/dd/yyyy"),

                                    currentMetric.AddedManually.ToString (),

                                    currentMetric.CreateAccountInfo.UserAccountName,

                                    currentMetric.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                                );

                            }

                        }

                        #endregion

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                        break;

                }

                MemberMetricsGrid_DataTable = memberMetricsDataTable;

                MemberMetricsGrid.DataSource = MemberMetricsGrid_DataTable;

            }

            return;

        }

        protected void MemberMetricsGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            String anchorText;

            String parameterString;

            if (eventArgs.Item is Telerik.Web.UI.GridDataItem) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                if (gridItem.OwnerTableView.Name != "MemberMetricsView") { return; }


                Boolean addedManually = Convert.ToBoolean (gridItem["AddedManually"].Text);

                if (addedManually != false) {

                    Int64 memberMetricId = Convert.ToInt64 (gridItem["MemberMetricId"].Text);

                    String metricName = Convert.ToString (gridItem["MetricName"].Text);

                    String eventDate = Convert.ToString (gridItem["EventDate"].Text);


                    anchorText = gridItem["AddedManually"].Text;

                    if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberMetricManage)) {

                        parameterString = String.Empty;

                        parameterString = parameterString + memberMetricId.ToString () + ", "; // MEMBER SERVICE ID

                        parameterString = parameterString + "'" + metricName.Replace ("'", "''") + "', "; // CURRENT SERVICE

                        parameterString = parameterString + "'" + eventDate.Replace ("'", "''") + "' "; // CURRENT SERVICE

                        anchorText = anchorText + " <a href=\"javascript:MemberMetrics_MemberMetric_OnRemoveManual_" + MemberMetricAction_MemberMetricId.ClientID.Replace ('.', '_') + " (" + parameterString + ")\">(delete)</a>";

                        gridItem["AddedManually"].Text = anchorText;

                    }

                }

            }

            return;

        }

        protected void MemberMetricsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Telerik.Web.UI.RadToolBar gridToolBar = null;

            Boolean success = false;

            String postScript = String.Empty;


            switch (eventArgs.CommandName) {

                case "ExpandCollapse":

                    #region Expand/Collapse

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                    Int64 memberServiceId;

                    if (Int64.TryParse (gridItem["MemberMetricId"].Text, out memberServiceId)) {

                        // TODO?

                    }

                    #endregion

                    break;

                case "MemberMetricAdd":

                    #region Add Member Metric

                    gridToolBar = (Telerik.Web.UI.RadToolBar) eventArgs.Item.FindControl ("MemberMetricToolbar");

                    if (gridToolBar != null) {

                        Telerik.Web.UI.RadComboBox MemberMetricSelection = (Telerik.Web.UI.RadComboBox) (gridToolBar.Items[2].FindControl ("MemberMetricSelection"));

                        Telerik.Web.UI.RadDateInput MemberMetricEventDate = (Telerik.Web.UI.RadDateInput) (gridToolBar.Items[2].FindControl ("MemberMetricEventDate"));

                        Telerik.Web.UI.RadNumericTextBox MemberMetricValue = (Telerik.Web.UI.RadNumericTextBox) (gridToolBar.Items[2].FindControl ("MemberMetricValue"));

                        if (MemberMetricSelection != null) {

                            if (!String.IsNullOrEmpty (MemberMetricSelection.SelectedValue)) {

                                if ((MemberMetricEventDate.SelectedDate.HasValue) && (MemberMetricValue.Value.HasValue)) {

                                    success = MercuryApplication.MemberMetricAddManual (Member.Id, Convert.ToInt64 (MemberMetricSelection.SelectedValue), MemberMetricEventDate.SelectedDate.Value, Convert.ToDecimal (MemberMetricValue.Value.Value));

                                    if (!success) { postScript = ("alert (\"" + MercuryApplication.LastException.Message.Replace ("\"", "\\") + "\");"); }

                                    else {

                                        MemberMetricsGrid_Count = 0;

                                        MemberMetricsGrid.DataSource = null;

                                        MemberMetricsGrid.Rebind ();

                                    }

                                }

                                else { postScript = ("alert (\"Event Date and Value of Metric is Required.\");"); }

                            }

                            else { postScript = ("alert (\"No Metric Selected for Manual Add.\");"); }

                        }

                        else { postScript = ("alert (\"Internal Error: Unable to Find Control MemberMetricSelection.\");"); }


                        if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                    }

                    #endregion

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("MemberMetricsGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        protected void MemberMetricsGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberMetricsGrid_PageSize != eventArgs.NewPageSize) {

                MemberMetricsGrid_PageSize = eventArgs.NewPageSize;

                MemberMetricsGrid.PageSize = eventArgs.NewPageSize;

                MemberMetricsGrid.DataSource = null;

                MemberMetricsGrid.Rebind ();

            }

        }


        protected void MemberMetricToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            switch (eventArgs.Item.Text) {

                case "Show Hidden":

                    MemberMetricShowHidden = !MemberMetricShowHidden;

                    MemberMetricsGrid_CurrentPage = 0;

                    MemberMetricsGrid.CurrentPageIndex = MemberMetricsGrid_CurrentPage;

                    MemberMetricsGrid.DataSource = null;

                    MemberMetricsGrid.Rebind ();

                    break;

            }

            return;

        }

        #endregion
        
        #region Action Events

        protected void MemberMetricAction_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int64 memberMetricId;

            Boolean success = false;

            String postScript = String.Empty;

            String postScriptArguments = String.Empty;


            if (Int64.TryParse (MemberMetricAction_MemberMetricId.Text, out memberMetricId)) {

                switch (MemberMetricAction_CommandName.Text) {

                    case "RemoveManual":

                        success = MercuryApplication.MemberMetricRemoveManual (memberMetricId);

                        if (!success) { postScript = ("alert (\"" + MercuryApplication.LastException.Message.Replace ("\"", "\\") + "\");"); }

                        if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unknown Command: " + MemberMetricAction_CommandName);

                        break;

                }

            }


            MemberMetricsGrid.DataSource = null;

            MemberMetricsGrid.Rebind ();

            return;

        }

        #endregion 

    }

}