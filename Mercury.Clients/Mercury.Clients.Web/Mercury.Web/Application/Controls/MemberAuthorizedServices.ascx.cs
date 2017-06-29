using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberAuthorizedServices : System.Web.UI.UserControl {

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

                    MemberAuthorizedServicesGrid.DataSource = null;

                    MemberAuthorizedServicesGrid.Rebind ();

                }

            }

        }

        public Unit HistoryGridHeight { get { return MemberAuthorizedServicesGrid.Height; } set { MemberAuthorizedServicesGrid.Height = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        private System.Data.DataTable MemberAuthorizedServicesGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberAuthorizedServiceId");

                    dataTable.Columns.Add ("AuthorizedServiceName");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("InitialIdentifiedDate");

                    dataTable.Columns.Add ("AddedManually");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("CreateDate");

                    Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTable"] = value; }

        }

        private Int32 MemberAuthorizedServicesGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_CurrentPage"] = value; }

        }

        private Int32 MemberAuthorizedServicesGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_PageSize"] = value; }

        }

        private Int32 MemberAuthorizedServicesGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_Count"] = value; }

        }


        private System.Data.DataTable MemberAuthorizedServicesGrid_DataTableDetailTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTableSingletonTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("MemberAuthorizedServiceId");

                    dataTable.Columns.Add ("AuthorizedServiceDefinitionId");

                    dataTable.Columns.Add ("EventDate");

                    dataTable.Columns.Add ("AuthorizationId");

                    dataTable.Columns.Add ("AuthorizationNumber");

                    dataTable.Columns.Add ("ExternalAuthorizationId");

                    dataTable.Columns.Add ("AuthorizationLine");

                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("ReferringProviderId");

                    dataTable.Columns.Add ("ServiceProviderId");

                    dataTable.Columns.Add ("Category");

                    dataTable.Columns.Add ("Subcategory");

                    dataTable.Columns.Add ("ServiceType");

                    dataTable.Columns.Add ("Status");


                    dataTable.Columns.Add ("ReceivedDate");

                    dataTable.Columns.Add ("ReferralDate");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("ServiceDate");

                    dataTable.Columns.Add ("DiagnosisCode");

                    dataTable.Columns.Add ("RevenueCode");

                    dataTable.Columns.Add ("ProcedureCode");

                    dataTable.Columns.Add ("ModifierCode");

                    dataTable.Columns.Add ("SpecialtyName");

                    dataTable.Columns.Add ("NdcCode");

                    dataTable.Columns.Add ("Description");

                    Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTableSingletonTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServicesGrid_DataTableSingletonTable"] = value; }

        }


        private Telerik.Web.UI.RadToolBar MemberAuthorizedServiceToolbar {

            get {

                Telerik.Web.UI.RadToolBar toolbar = null;

                if (MemberAuthorizedServicesGrid.MasterTableView.Controls[0] != null) {

                    if (MemberAuthorizedServicesGrid.MasterTableView.Controls[0].Controls[0] != null) {

                        if (MemberAuthorizedServicesGrid.MasterTableView.Controls[0].Controls[0].Controls[0] != null) {

                            toolbar = (Telerik.Web.UI.RadToolBar) MemberAuthorizedServicesGrid.MasterTableView.Controls[0].Controls[0].Controls[0].FindControl ("MemberAuthorizedServiceToolbar");

                        }

                    }

                }

                return toolbar;

            }

        }

        private Boolean MemberAuthorizedServiceShowHidden {

            get {

                Boolean showHidden = false;

                if (Session[SessionCachePrefix + "MemberAuthorizedServiceShowHidden"] != null) {

                    showHidden = (Boolean) Session[SessionCachePrefix + "MemberAuthorizedServiceShowHidden"];

                }

                return showHidden;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServiceShowHidden"] = value; }

        }

        private String MemberAuthorizedServiceSelection_SelectedValue {

            get {

                String selectedValue = (String) Session[SessionCachePrefix + "MemberAuthorizedServiceSelection_SelectedValue"];

                if (selectedValue == null) { selectedValue = String.Empty; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServiceSelection_SelectedValue"] = value; }

        }

        private DateTime? MemberAuthorizedServiceEventDate_SelectedDate {

            get {

                DateTime? selectedValue = (DateTime?) Session[SessionCachePrefix + "MemberAuthorizedServiceEventDate_SelectedDate"];

                if (selectedValue == null) { selectedValue = null; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizedServiceEventDate_SelectedDate"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        #endregion


        #region Member AuthorizedServices Grid Events

        protected void MemberAuthorizedServicesGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar MemberAuthorizedServiceToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("MemberAuthorizedServiceToolbar");

                ((Telerik.Web.UI.RadToolBarButton) MemberAuthorizedServiceToolbar.Items[0]).Checked = MemberAuthorizedServiceShowHidden;

            }

            return;

        }

        protected void MemberAuthorizedServicesGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable memberAuthorizedServicesDataTable = MemberAuthorizedServicesGrid_DataTable;

            if (!eventArgs.IsFromDetailTable) {

                switch (eventArgs.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:

                        #region Initialize Grid

                        MemberAuthorizedServicesGrid_Count = 0;

                        MemberAuthorizedServicesGrid_CurrentPage = 0;

                        MemberAuthorizedServicesGrid_PageSize = 10;


                        MemberAuthorizedServicesGrid.CurrentPageIndex = MemberAuthorizedServicesGrid_CurrentPage;

                        MemberAuthorizedServicesGrid.PageSize = MemberAuthorizedServicesGrid_PageSize;

                        MemberAuthorizedServicesGrid.VirtualItemCount = MemberAuthorizedServicesGrid_Count;

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                        #region Restore Grid State

                        MemberAuthorizedServicesGrid.CurrentPageIndex = MemberAuthorizedServicesGrid_CurrentPage;

                        MemberAuthorizedServicesGrid.PageSize = MemberAuthorizedServicesGrid_PageSize;

                        MemberAuthorizedServicesGrid.VirtualItemCount = MemberAuthorizedServicesGrid_Count;


                        //if (MemberAuthorizedServiceToolbar != null) {

                        //    Telerik.Web.UI.RadComboBox MemberAuthorizedServiceSelection = (Telerik.Web.UI.RadComboBox) MemberAuthorizedServiceToolbar.Items[2].FindControl ("MemberAuthorizedServiceSelection");

                        //    if (MemberAuthorizedServiceSelection != null) {

                        //        MemberAuthorizedServiceSelection_SelectedValue = MemberAuthorizedServiceSelection.SelectedValue;

                        //    }

                        //    Telerik.Web.UI.RadDateInput MemberAuthorizedServiceEventDate = (Telerik.Web.UI.RadDateInput) MemberAuthorizedServiceToolbar.Items[2].FindControl ("MemberAuthorizedServiceEventDate");

                        //    if (MemberAuthorizedServiceEventDate != null) {

                        //        MemberAuthorizedServiceEventDate_SelectedDate = MemberAuthorizedServiceEventDate.SelectedDate;

                        //    }

                        //}

                        #endregion

                        break;

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                        #region Initialize Toolbar and Security

                        if (MemberAuthorizedServiceToolbar != null) {


                        }

                        #endregion

                        #region Rebind Grid

                        if (Member == null) { memberAuthorizedServicesDataTable.Rows.Clear (); }

                        else {

                            if (MemberAuthorizedServicesGrid_Count == 0) {

                                MemberAuthorizedServicesGrid_Count = Convert.ToInt32 (MercuryApplication.MemberAuthorizedServicesGetCount (Member.Id, MemberAuthorizedServiceShowHidden));

                                MemberAuthorizedServicesGrid.VirtualItemCount = MemberAuthorizedServicesGrid_Count;

                            }

                            MemberAuthorizedServicesGrid_PageSize = MemberAuthorizedServicesGrid.PageSize;

                            MemberAuthorizedServicesGrid_CurrentPage = MemberAuthorizedServicesGrid.CurrentPageIndex;

                            memberAuthorizedServicesDataTable.Rows.Clear ();

                            List<Mercury.Server.Application.MemberAuthorizedService> memberAuthorizedServices;

                            memberAuthorizedServices = MercuryApplication.MemberAuthorizedServicesGetByPage (Member.Id, (MemberAuthorizedServicesGrid.CurrentPageIndex) * MemberAuthorizedServicesGrid.PageSize + 1, MemberAuthorizedServicesGrid.PageSize, MemberAuthorizedServiceShowHidden);

                            foreach (Mercury.Server.Application.MemberAuthorizedService currentAuthorizedService in memberAuthorizedServices) {

                                memberAuthorizedServicesDataTable.Rows.Add (

                                    currentAuthorizedService.Id.ToString (),

                                    currentAuthorizedService.AuthorizedService.Name,

                                    currentAuthorizedService.EventDate.ToString ("MM/dd/yyyy"),

                                    currentAuthorizedService.InitialIdentifiedDate.ToString ("MM/dd/yyyy"),

                                    currentAuthorizedService.AddedManually.ToString (),

                                    currentAuthorizedService.CreateAccountInfo.UserAccountName,

                                    currentAuthorizedService.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                                );

                            }

                        }

                        #endregion

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                        break;

                }

                MemberAuthorizedServicesGrid_DataTable = memberAuthorizedServicesDataTable;

                MemberAuthorizedServicesGrid.DataSource = MemberAuthorizedServicesGrid_DataTable;

                MemberAuthorizedServicesGrid.MasterTableView.DetailTables[0].DataSource = (MemberAuthorizedServicesGrid_DataTableDetailTable.Rows.Count != 0) ? MemberAuthorizedServicesGrid_DataTableDetailTable : null;

            }

            return;

        }

        protected void MemberAuthorizedServicesGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            String anchorText;

            //String parameterString;

            if (eventArgs.Item is Telerik.Web.UI.GridDataItem) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                if (gridItem.OwnerTableView.Name != "MemberAuthorizedServicesView") { return; }


                Boolean addedManually = Convert.ToBoolean (gridItem["AddedManually"].Text);

                if (addedManually != false) {

                    Int64 memberAuthorizedServiceId = Convert.ToInt64 (gridItem["MemberAuthorizedServiceId"].Text);

                    String authorizedServiceName = Convert.ToString (gridItem["AuthorizedServiceName"].Text);

                    String eventDate = Convert.ToString (gridItem["EventDate"].Text);


                    anchorText = gridItem["AddedManually"].Text;

                    //if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAuthorizedServiceManage)) {

                    //    parameterString = String.Empty;

                    //    parameterString = parameterString + memberAuthorizedServiceId.ToString () + ", "; // MEMBER SERVICE ID

                    //    parameterString = parameterString + "'" + authorizedServiceName.Replace ("'", "''") + "', "; // CURRENT SERVICE

                    //    parameterString = parameterString + "'" + eventDate.Replace ("'", "''") + "' "; // CURRENT SERVICE

                    //    anchorText = anchorText + " <a href=\"javascript:MemberAuthorizedServices_MemberAuthorizedService_OnRemoveManual_" + MemberAuthorizedServiceAction_MemberAuthorizedServiceId.ClientID.Replace ('.', '_') + " (" + parameterString + ")\">(delete)</a>";

                    //    gridItem["AddedManually"].Text = anchorText;

                    //}

                }

            }

            return;

        }

        protected void MemberAuthorizedServicesGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            System.Data.DataTable detailTable = null;

            //Telerik.Web.UI.RadToolBar gridToolBar = null;

            //Boolean success = false;

            String postScript = String.Empty;


            switch (eventArgs.CommandName) {

                case "ExpandCollapse":

                    #region Expand/Collapse

                    Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                    Int64 memberAuthorizedServiceId;

                    if (Int64.TryParse (gridItem["MemberAuthorizedServiceId"].Text, out memberAuthorizedServiceId)) {

                        detailTable = MemberAuthorizedServicesGrid_DataTableDetailTable;

                        detailTable.Rows.Clear ();

                        List<Mercury.Server.Application.MemberAuthorizedServiceDetail> details;

                        details = MercuryApplication.MemberAuthorizedServiceDetailsGet (memberAuthorizedServiceId);


                        foreach (Mercury.Server.Application.MemberAuthorizedServiceDetail currentDetail in details) {

                            // String diagnosisInformation = "<span title=\"" + MercuryApplication.DiagnosisDescription (currentDetail.DiagnosisCode, currentDetail.DiagnosisVersion) + "\">" + currentDetail.DiagnosisCode + "</span>";

                            String diagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentDetail.DiagnosisCode, currentDetail.DiagnosisVersion);

                            String revenueCodeInformation = "<span title=\"" + MercuryApplication.RevenueCodeDescription (currentDetail.RevenueCode) + "\">" + currentDetail.RevenueCode + "</span>";

                            String procedureCodeInformation = "<span title=\"" + MercuryApplication.ProcedureCodeDescription (currentDetail.ProcedureCode) + "\">" + currentDetail.ProcedureCode + "</span>";


                            detailTable.Rows.Add (

                                currentDetail.MemberAuthorizedServiceId.ToString (),

                                currentDetail.AuthorizedServiceDefinitionId.ToString (),

                                currentDetail.EventDate.ToString ("MM/dd/yyyy"),

                                currentDetail.AuthorizationId,

                                currentDetail.AuthorizationNumber,

                                currentDetail.ExternalAuthorizationId,

                                currentDetail.AuthorizationLine.ToString (),

                                currentDetail.MemberId,

                                CommonFunctions.ProviderProfileAnchor (MercuryApplication, currentDetail.ReferringProviderId),

                                CommonFunctions.ProviderProfileAnchor (MercuryApplication, currentDetail.ServiceProviderId),

                                currentDetail.AuthorizationCategory,

                                currentDetail.AuthorizationSubcategory,

                                currentDetail.AuthorizationServiceType,

                                currentDetail.AuthorizationStatus,

                                (currentDetail.ReceivedDate.HasValue) ? currentDetail.ReceivedDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                                (currentDetail.ReferralDate.HasValue) ? currentDetail.ReferralDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                                currentDetail.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentDetail.TerminationDate.ToString ("MM/dd/yyyy"),

                                (currentDetail.ServiceDate.HasValue) ? currentDetail.ServiceDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",


                                diagnosisInformation,

                                revenueCodeInformation,

                                procedureCodeInformation,

                                currentDetail.ModifierCode,

                                currentDetail.SpecialtyName,

                                currentDetail.NdcCode,

                                currentDetail.Description

                            );

                        }

                        MemberAuthorizedServicesGrid_DataTableDetailTable = detailTable;

                        MemberAuthorizedServicesGrid.MasterTableView.DetailTables[0].DataSource = detailTable;

                    }

                    #endregion

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("MemberAuthorizedServicesGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        protected void MemberAuthorizedServicesGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberAuthorizedServicesGrid_PageSize != eventArgs.NewPageSize) {

                MemberAuthorizedServicesGrid_PageSize = eventArgs.NewPageSize;

                MemberAuthorizedServicesGrid.PageSize = eventArgs.NewPageSize;

                MemberAuthorizedServicesGrid.DataSource = null;

                MemberAuthorizedServicesGrid.Rebind ();

            }

        }


        protected void MemberAuthorizedServiceToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            switch (eventArgs.Item.Text) {

                case "Show Hidden":

                    MemberAuthorizedServiceShowHidden = !MemberAuthorizedServiceShowHidden;

                    MemberAuthorizedServicesGrid_CurrentPage = 0;

                    MemberAuthorizedServicesGrid.CurrentPageIndex = MemberAuthorizedServicesGrid_CurrentPage;

                    MemberAuthorizedServicesGrid.DataSource = null;

                    MemberAuthorizedServicesGrid.Rebind ();

                    break;

                default: System.Diagnostics.Debug.WriteLine ("MemberAuthorizedServiceToolbar_OnButtonClick: " + eventArgs.Item.Text); break;

            }

            return;

        }

        #endregion



    }

}
