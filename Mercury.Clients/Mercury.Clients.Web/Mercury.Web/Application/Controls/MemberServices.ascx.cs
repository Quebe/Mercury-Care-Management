using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberServices : System.Web.UI.UserControl {

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

                    MemberServicesGrid.DataSource = null;

                    MemberServicesGrid.Rebind ();

                }

            }

        }

        public Unit HistoryGridHeight { get { return MemberServicesGrid.Height; } set { MemberServicesGrid.Height = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        private System.Data.DataTable MemberServicesGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberServicesGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberServiceId");

                    dataTable.Columns.Add ("ServiceName");

                    dataTable.Columns.Add ("ServiceType");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("AddedManually");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("CreateDate");

                    Session[SessionCachePrefix + "MemberServicesGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_DataTable"] = value; }

        }

        private Int32 MemberServicesGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberServicesGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberServicesGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_CurrentPage"] = value; }

        }

        private Int32 MemberServicesGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberServicesGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberServicesGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_PageSize"] = value; }

        }

        private Int32 MemberServicesGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberServicesGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberServicesGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_Count"] = value; }

        }


        private System.Data.DataTable MemberServicesGrid_DataTableSingletonTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberServicesGrid_DataTableSingletonTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberServiceId");

                    dataTable.Columns.Add ("DefinitionId");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("ClaimId");

                    dataTable.Columns.Add ("ClaimLine");

                    dataTable.Columns.Add ("ClaimType");

                    dataTable.Columns.Add ("BillType");

                    dataTable.Columns.Add ("PrincipalDiagnosisCode");

                    dataTable.Columns.Add ("DiagnosisCode");

                    dataTable.Columns.Add ("Icd9ProcedureCode");

                    dataTable.Columns.Add ("LocationCode");

                    dataTable.Columns.Add ("RevenueCode");

                    dataTable.Columns.Add ("ProcedureCode");

                    dataTable.Columns.Add ("ModifierCode");


                    dataTable.Columns.Add ("SpecialtyName");

                    dataTable.Columns.Add ("IsPcpClaim");

                    dataTable.Columns.Add ("NdcCode");

                    dataTable.Columns.Add ("Units");

                    dataTable.Columns.Add ("TherapeuticClassification");

                    dataTable.Columns.Add ("LabLoincCode");

                    dataTable.Columns.Add ("LabValue");

                    dataTable.Columns.Add ("Description");

                    Session[SessionCachePrefix + "MemberServicesGrid_DataTableSingletonTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_DataTableSingletonTable"] = value; }

        }

        private System.Data.DataTable MemberServicesGrid_DataTableSetTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberServicesGrid_DataTableSetTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberServiceId");

                    dataTable.Columns.Add ("DefinitionId");

                    dataTable.Columns.Add ("DetailMemberServiceId");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("ServiceName");

                    dataTable.Columns.Add ("ServiceType");

                    Session[SessionCachePrefix + "MemberServicesGrid_DataTableSetTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberServicesGrid_DataTableSetTable"] = value; }

        }


        
        private Telerik.Web.UI.RadToolBar MemberServiceToolbar {
            
            get {

                Telerik.Web.UI.RadToolBar toolbar = null;

                if (MemberServicesGrid.MasterTableView.Controls[0] != null) {

                    if (MemberServicesGrid.MasterTableView.Controls[0].Controls[0] != null) {

                        if (MemberServicesGrid.MasterTableView.Controls[0].Controls[0].Controls[0] != null) {

                            toolbar = (Telerik.Web.UI.RadToolBar) MemberServicesGrid.MasterTableView.Controls[0].Controls[0].Controls[0].FindControl ("MemberServiceToolbar");

                        }

                    }

                }

                return toolbar;

            }

        }

        private Boolean MemberServiceShowHidden {

            get {

                Boolean showHidden = false;

                if (Session[SessionCachePrefix + "MemberServiceShowHidden"] != null) {

                    showHidden = (Boolean) Session[SessionCachePrefix + "MemberServiceShowHidden"];

                }

                return showHidden;

            }

            set { Session[SessionCachePrefix + "MemberServiceShowHidden"] = value; }

        }

        private String MemberServiceSelection_SelectedValue {

            get {

                String selectedValue = (String) Session[SessionCachePrefix + "MemberServiceSelection_SelectedValue"];

                if (selectedValue == null) { selectedValue = String.Empty; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberServiceSelection_SelectedValue"] = value; }

        }

        private DateTime? MemberServiceEventDate_SelectedDate {

            get {

                DateTime? selectedValue = (DateTime?) Session[SessionCachePrefix + "MemberServiceEventDate_SelectedDate"];

                if (selectedValue == null) { selectedValue = null; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberServiceEventDate_SelectedDate"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        #endregion 


        #region Member Services Grid Events

        protected void MemberServicesGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar MemberServiceToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("MemberServiceToolbar");

                ((Telerik.Web.UI.RadToolBarButton) MemberServiceToolbar.Items[0]).Checked = MemberServiceShowHidden;

                Telerik.Web.UI.RadComboBox MemberServiceSelection = (Telerik.Web.UI.RadComboBox) MemberServiceToolbar.Items[2].FindControl ("MemberServiceSelection");

                Telerik.Web.UI.RadDateInput MemberServiceEventDate = (Telerik.Web.UI.RadDateInput) MemberServiceToolbar.Items[2].FindControl ("MemberServiceEventDate");

                if (MemberServiceSelection != null) {

                    if (MemberServiceSelection.Items.Count == 0) {

                        MemberServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Service Selected", String.Empty));

                        foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader currentService in MercuryApplication.MedicalServiceHeadersGet (true)) {

                            if (currentService.Enabled) {

                                MemberServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentService.Name, currentService.Id.ToString ()));

                            }

                        }

                    }

                    MemberServiceSelection.SelectedValue = MemberServiceSelection_SelectedValue;

                }

                if (MemberServiceEventDate != null) {

                    if (Member != null) { MemberServiceEventDate.MinDate = Member.BirthDate; }

                    MemberServiceEventDate.SelectedDate = MemberServiceEventDate_SelectedDate; 
                
                }

            }

            return;

        }

        protected void MemberServicesGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable memberServicesDataTable = MemberServicesGrid_DataTable;

            if (!eventArgs.IsFromDetailTable) {

                switch (eventArgs.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                        #region Initialize Grid

                        MemberServicesGrid_Count = 0;

                        MemberServicesGrid_CurrentPage = 0;

                        MemberServicesGrid_PageSize = 10;


                        MemberServicesGrid.CurrentPageIndex = MemberServicesGrid_CurrentPage;

                        MemberServicesGrid.PageSize = MemberServicesGrid_PageSize;

                        MemberServicesGrid.VirtualItemCount = MemberServicesGrid_Count;                        

                        #endregion 
                        
                        break;

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        #region Restore Grid State

                        MemberServicesGrid.CurrentPageIndex = MemberServicesGrid_CurrentPage;

                        MemberServicesGrid.PageSize = MemberServicesGrid_PageSize;

                        MemberServicesGrid.VirtualItemCount = MemberServicesGrid_Count;

                        
                        if (MemberServiceToolbar != null) {

                            Telerik.Web.UI.RadComboBox MemberServiceSelection = (Telerik.Web.UI.RadComboBox) MemberServiceToolbar.Items[2].FindControl ("MemberServiceSelection");

                            if (MemberServiceSelection != null) {

                                MemberServiceSelection_SelectedValue = MemberServiceSelection.SelectedValue;

                            }

                            Telerik.Web.UI.RadDateInput MemberServiceEventDate = (Telerik.Web.UI.RadDateInput) MemberServiceToolbar.Items[2].FindControl ("MemberServiceEventDate");

                            if (MemberServiceEventDate != null) {

                                MemberServiceEventDate_SelectedDate= MemberServiceEventDate.SelectedDate;

                            }

                        }

                        #endregion 

                        break;

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                        #region Initialize Toolbar and Security

                        if (MemberServiceToolbar != null) {
                            
                            MemberServiceToolbar.Items[1].Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberServiceManage);

                            MemberServiceToolbar.Items[2].Visible = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberServiceManage);

                        }

                        #endregion

                        #region Rebind Grid

                        if (Member == null) { memberServicesDataTable.Rows.Clear (); }

                        else {

                            if (MemberServicesGrid_Count == 0) {

                                MemberServicesGrid_Count = Convert.ToInt32 (MercuryApplication.MemberServicesGetCount (Member.Id, MemberServiceShowHidden));

                                MemberServicesGrid.VirtualItemCount = MemberServicesGrid_Count;

                            }

                            MemberServicesGrid_PageSize = MemberServicesGrid.PageSize;

                            MemberServicesGrid_CurrentPage = MemberServicesGrid.CurrentPageIndex;

                            memberServicesDataTable.Rows.Clear ();

                            List<Mercury.Server.Application.MemberService> memberServices;

                            memberServices = MercuryApplication.MemberServicesGetByPage (Member.Id, (MemberServicesGrid.CurrentPageIndex) * MemberServicesGrid.PageSize + 1, MemberServicesGrid.PageSize, MemberServiceShowHidden);

                            foreach (Mercury.Server.Application.MemberService currentService in memberServices) {

                                memberServicesDataTable.Rows.Add (

                                    currentService.Id.ToString (),

                                    currentService.Service.Name,

                                    currentService.Service.ServiceType,

                                    currentService.EventDate.ToString ("MM/dd/yyyy"),

                                    currentService.AddedManually.ToString (),

                                    currentService.CreateAccountInfo.UserAccountName,

                                    currentService.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                                );

                            }

                        }

                        #endregion 

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                        break;

                }

                MemberServicesGrid_DataTable = memberServicesDataTable;

                MemberServicesGrid.DataSource = MemberServicesGrid_DataTable;

                MemberServicesGrid.MasterTableView.DetailTables[0].DataSource = (MemberServicesGrid_DataTableSingletonTable.Rows.Count != 0) ? MemberServicesGrid_DataTableSingletonTable : null;

                MemberServicesGrid.MasterTableView.DetailTables[1].DataSource = (MemberServicesGrid_DataTableSetTable.Rows.Count != 0) ? MemberServicesGrid_DataTableSetTable : null;

            }   

            return;

        }

        protected void MemberServicesGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            String anchorText;

            String parameterString;

            if (eventArgs.Item is Telerik.Web.UI.GridDataItem) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                if (gridItem.OwnerTableView.Name != "MemberServicesView") { return; }


                Boolean addedManually = Convert.ToBoolean (gridItem["AddedManually"].Text);

                if (addedManually != false) {

                    Int64 memberServiceId = Convert.ToInt64 (gridItem["MemberServiceId"].Text);

                    String serviceName = Convert.ToString (gridItem["ServiceName"].Text);

                    String eventDate = Convert.ToString (gridItem["EventDate"].Text);


                    anchorText = gridItem["AddedManually"].Text;

                    if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberServiceManage)) {

                        parameterString = String.Empty;

                        parameterString = parameterString + memberServiceId.ToString () + ", "; // MEMBER SERVICE ID

                        parameterString = parameterString + "'" + serviceName.Replace ("'", "''") + "', "; // CURRENT SERVICE

                        parameterString = parameterString + "'" + eventDate.Replace ("'", "''") + "' "; // CURRENT SERVICE

                        anchorText = anchorText + " <a href=\"javascript:MemberServices_MemberService_OnRemoveManual_" + MemberServiceAction_MemberServiceId.ClientID.Replace ('.', '_') + " (" + parameterString + ")\">(delete)</a>";

                        gridItem["AddedManually"].Text = anchorText;

                    }

                }

            }

            return;

        }

        protected void MemberServicesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }
            

            System.Data.DataTable detailTable = null;

            Telerik.Web.UI.RadToolBar gridToolBar = null;

            Boolean success = false;
            
            String postScript = String.Empty;


            switch (eventArgs.CommandName) {

                case "ExpandCollapse":

                    #region Expand/Collapse

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                    Int64 memberServiceId;

                    if (Int64.TryParse (gridItem["MemberServiceId"].Text, out memberServiceId)) {

                        switch (gridItem["ServiceType"].Text) {

                            case "Singleton":

                                #region Singleton Detail Table

                                detailTable = MemberServicesGrid_DataTableSingletonTable;

                                detailTable.Rows.Clear ();

                                List<Mercury.Server.Application.MemberServiceDetailSingleton> detailSingletons;

                                detailSingletons = MercuryApplication.MemberServiceDetailSingletonGet (memberServiceId);

                                foreach (Mercury.Server.Application.MemberServiceDetailSingleton currentDetail in detailSingletons) {

                                    // String principalDiagnosisInformation = "<span title=\"" + MercuryApplication.DiagnosisDescription (currentDetail.PrincipalDiagnosisCode, currentDetail.PrincipalDiagnosisVersion) + "\">" + currentDetail.PrincipalDiagnosisCode + "</span>";

                                    // String diagnosisInformation = "<span title=\"" + MercuryApplication.DiagnosisDescription (currentDetail.DiagnosisCode, currentDetail.DiagnosisVersion) + "\">" + currentDetail.DiagnosisCode + "</span>";

                                    String principalDiagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentDetail.PrincipalDiagnosisCode, currentDetail.PrincipalDiagnosisVersion);

                                    String diagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentDetail.DiagnosisCode, currentDetail.DiagnosisVersion);


                                    String revenueCodeInformation = "<span title=\"" + MercuryApplication.RevenueCodeDescription (currentDetail.RevenueCode) + "\">" + currentDetail.RevenueCode + "</span>";

                                    String procedureCodeInformation = "<span title=\"" + MercuryApplication.ProcedureCodeDescription (currentDetail.ProcedureCode) + "\">" + currentDetail.ProcedureCode + "</span>";

                                    String billTypeInformation = "<span title=\"" + MercuryApplication.BillTypeDescription (currentDetail.BillType) + "\">" + currentDetail.BillType + "</span>";

                                    String icd9ProcedureCodeInformation = "<span title=\"" + MercuryApplication.Icd9ProcedureCodeDescription (currentDetail.Icd9ProcedureCode) + "\">" + currentDetail.Icd9ProcedureCode + "</span>";

                                    detailTable.Rows.Add (

                                        currentDetail.MemberServiceId.ToString (),

                                        currentDetail.SingletonDefinitionId.ToString (),

                                        currentDetail.EventDate.ToString ("MM/dd/yyyy"),

                                        currentDetail.ExternalClaimId,

                                        currentDetail.ClaimLine.ToString (),

                                        currentDetail.ClaimType,

                                        billTypeInformation,

                                        principalDiagnosisInformation,

                                        diagnosisInformation,

                                        icd9ProcedureCodeInformation,

                                        currentDetail.LocationCode,

                                        revenueCodeInformation,

                                        procedureCodeInformation,

                                        currentDetail.ModifierCode,

                                        currentDetail.SpecialtyName,

                                        currentDetail.IsPcpClaim.ToString (),

                                        currentDetail.NdcCode,

                                        currentDetail.Units.ToString (),

                                        currentDetail.TherapeuticClassification,

                                        currentDetail.LabLoincCode,

                                        currentDetail.LabValue.ToString (),

                                        currentDetail.Description

                                    );

                                }

                                MemberServicesGrid_DataTableSingletonTable = detailTable;

                                MemberServicesGrid.MasterTableView.DetailTables[0].DataSource = detailTable;


                                #endregion

                                break;

                            case "Set":

                                #region Set Detail Table

                                detailTable = MemberServicesGrid_DataTableSetTable;

                                detailTable.Rows.Clear ();

                                List<Mercury.Server.Application.MemberServiceDetailSet> detailSets;

                                detailSets = MercuryApplication.MemberServiceDetailSetGet (memberServiceId);

                                foreach (Mercury.Server.Application.MemberServiceDetailSet currentDetail in detailSets) {

                                    detailTable.Rows.Add (

                                        currentDetail.MemberServiceId.ToString (),

                                        currentDetail.SetDefinitionId.ToString (),

                                        currentDetail.DetailMemberServiceId.ToString (),

                                        currentDetail.EventDate.ToString ("MM/dd/yyyy"),

                                        currentDetail.ServiceName,

                                        currentDetail.ServiceType.ToString ()

                                    );

                                }

                                MemberServicesGrid_DataTableSetTable = detailTable;

                                MemberServicesGrid.MasterTableView.DetailTables[1].DataSource = detailTable;

                                #endregion

                                break;

                        }

                    }

                    #endregion

                    break;

                case "MemberServiceAdd":

                    #region Member Service Add

                    gridToolBar = (Telerik.Web.UI.RadToolBar) eventArgs.Item.FindControl ("MemberServiceToolbar");

                    if (gridToolBar != null) {

                        Telerik.Web.UI.RadComboBox MemberServiceSelection = (Telerik.Web.UI.RadComboBox) (gridToolBar.Items[2].FindControl ("MemberServiceSelection"));

                        Telerik.Web.UI.RadDateInput MemberServiceEventDate = (Telerik.Web.UI.RadDateInput) (gridToolBar.Items[2].FindControl ("MemberServiceEventDate"));

                        if (MemberServiceSelection != null) {

                            if (!String.IsNullOrEmpty (MemberServiceSelection.SelectedValue)) {

                                if (MemberServiceEventDate.SelectedDate.HasValue) {

                                    success = MercuryApplication.MemberServiceAddManual (Member.Id, Convert.ToInt64 (MemberServiceSelection.SelectedValue), MemberServiceEventDate.SelectedDate.Value);

                                    if (!success) { postScript = ("alert (\"" + MercuryApplication.LastException.Message.Replace ("\"", "\\") + "\");"); }

                                    else {

                                        MemberServicesGrid_Count = 0;

                                        MemberServicesGrid.DataSource = null;

                                        MemberServicesGrid.Rebind ();

                                    }

                                }

                                else { postScript = ("alert (\"Event Date of Service is Required.\");"); }

                            }

                            else { postScript = ("alert (\"No Service Selected for Manual Add.\");"); }
                           
                        }

                        else { postScript = ("alert (\"Internal Error: Unable to Find Control MemberServiceSelection.\");"); }
                           
                            
                        if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                    }

                    #endregion 

                    break;

                default: 

                    System.Diagnostics.Debug.WriteLine ("MemberServicesGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        protected void MemberServicesGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberServicesGrid_PageSize != eventArgs.NewPageSize) {

                MemberServicesGrid_PageSize = eventArgs.NewPageSize;

                MemberServicesGrid.PageSize = eventArgs.NewPageSize;

                MemberServicesGrid.DataSource = null;

                MemberServicesGrid.Rebind ();

            }

        }


        protected void MemberServiceToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            switch (eventArgs.Item.Text) {

                case "Show Hidden":

                    MemberServiceShowHidden = !MemberServiceShowHidden;

                    MemberServicesGrid_CurrentPage = 0;

                    MemberServicesGrid.CurrentPageIndex = MemberServicesGrid_CurrentPage;

                    MemberServicesGrid.DataSource = null;

                    MemberServicesGrid.Rebind ();

                    break;

                default: System.Diagnostics.Debug.WriteLine ("MemberServiceToolbar_OnButtonClick: " + eventArgs.Item.Text); break;

            }

            return;

        }

        #endregion


        #region Action Events

        protected void MemberServiceAction_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int64 memberServiceId;

            Boolean success = false;

            String postScript = String.Empty;

            String postScriptArguments = String.Empty;


            if (Int64.TryParse (MemberServiceAction_MemberServiceId.Text, out memberServiceId)) {

                switch (MemberServiceAction_CommandName.Text) {

                    case "RemoveManual":

                        success = MercuryApplication.MemberServiceRemoveManual (memberServiceId);

                        if (!success) { postScript = ("alert (\"" + MercuryApplication.LastException.Message.Replace ("\"", "\\") + "\");"); }

                        if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unknown Command: " + MemberServiceAction_CommandName);

                        break;

                }

            }


            MemberServicesGrid.DataSource = null;

            MemberServicesGrid.Rebind ();

            return;

        }

        #endregion 

    }

}
