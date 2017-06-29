using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class ProviderContract : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean pageSizeChanged = false;

        #endregion


        #region Session Properties

        public String SessionCachePrefix {

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


        public Client.Core.Provider.Provider Provider {

            get { return (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"]; }

            set {

                Client.Core.Provider.Provider provider = (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"];

                if (provider != value) {

                    Session[SessionCachePrefix + "Provider"] = value;

                    InitializeProviderContractGrid ();

                    ProviderContractGrid_ManualDataRebind ();

                }

            }

        }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean) Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                // OVERRIDE TO DISABLE TOOLBAR

                Session[SessionCachePrefix + "AllowUserInteraction"] = false;

                // Session[SessionCachePrefix + "AllowUserInteraction"] = value; 

            }

        }


        private System.Data.DataTable ProviderContractGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "ProviderContractGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ProviderContractId");

                    dataTable.Columns.Add ("ProviderId");

                    dataTable.Columns.Add ("ProviderName");

                    dataTable.Columns.Add ("AffiliateProviderId");

                    dataTable.Columns.Add ("AffiliateProviderName");

                    dataTable.Columns.Add ("ProgramId");

                    dataTable.Columns.Add ("ProgramName");

                    dataTable.Columns.Add ("ContractId");

                    dataTable.Columns.Add ("ContractName");

                    dataTable.Columns.Add ("IsParticipating");

                    dataTable.Columns.Add ("IsCapitated");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("SortDateField");

                    Session[SessionCachePrefix + "ProviderContractGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "ProviderContractGrid_DataTable"] = value; }

        }

        private Int32 ProviderContractGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "ProviderContractGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "ProviderContractGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "ProviderContractGrid_CurrentPage"] = value; }

        }

        private Int32 ProviderContractGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "ProviderContractGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "ProviderContractGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "ProviderContractGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "ProviderContractGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "ProviderContractGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "ProviderContractGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 ProviderContractGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "ProviderContractGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "ProviderContractGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "ProviderContractGrid_Count"] = value; }

        }


        public Unit GridHeight { get { return ProviderContractGrid.Height; } set { ProviderContractGrid.Height = value; } }

        public Int32 GridPageSize { get { return ProviderContractGrid.PageSize; } set { ProviderContractGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeProviderContractGrid () {

            ProviderContractGrid.CurrentPageIndex = 0;

            ProviderContractGrid.PageSize = ProviderContractGrid_PageSize;

            ProviderContractGrid_Count = 0;

            ProviderContractGrid.DataSource = ProviderContractGrid_DataTable;

            ProviderContractGrid.DataBind ();

            return;

        }

        #endregion


        #region Contract Grid Events

        protected void ProviderContractGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar ContactToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("EntityContactToolbar");

                Telerik.Web.UI.RadToolBarItem toolbarItem;


                AllowUserInteraction = AllowUserInteraction && (

                    (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionContact))

                    );


                if (!AllowUserInteraction) {

                    ContactToolbar.Visible = false;

                }


                toolbarItem = ContactToolbar.Items.FindItemByValue ("Contact");

                toolbarItem.Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionContact));

            }

            return;

        }

        protected void ProviderContractGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = ProviderContractGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    ProviderContractGrid_Count = 0;

                    ProviderContractGrid_CurrentPage = 0;

                    ProviderContractGrid_PageSize = 10;


                    ProviderContractGrid.CurrentPageIndex = ProviderContractGrid_CurrentPage;

                    ProviderContractGrid.PageSize = ProviderContractGrid_PageSize;

                    ProviderContractGrid.VirtualItemCount = ProviderContractGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    ProviderContractGrid.CurrentPageIndex = ProviderContractGrid_CurrentPage;

                    ProviderContractGrid.PageSize = ProviderContractGrid_PageSize;

                    ProviderContractGrid.VirtualItemCount = ProviderContractGrid_Count;

                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Provider == null) { dataTable.Rows.Clear (); }

                    else {

                        if (ProviderContractGrid_Count == 0) {

                            ProviderContractGrid_Count = Convert.ToInt32 (MercuryApplication.ProviderContractsGet (Provider.Id, true).Count);

                            ProviderContractGrid.VirtualItemCount = Convert.ToInt32 (ProviderContractGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            ProviderContractGrid_PageSize = ProviderContractGrid.PageSize;

                        }

                        else {

                            ProviderContractGrid.PageSize = ProviderContractGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        ProviderContractGrid_CurrentPage = ProviderContractGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        Int32 initialRow = ProviderContractGrid.CurrentPageIndex * ProviderContractGrid.PageSize + 1;

                        List<Client.Core.Provider.ProviderContract> contracts;

                        contracts = MercuryApplication.ProviderContractsGet (Provider.Id, true);

                        foreach (Client.Core.Provider.ProviderContract currentContract in contracts) {

                            #region Create Data Row

                            //String relatedEntity = currentContact.RelatedEntityName;

                            //if (currentContact.RelatedEntity != null) {

                            //    switch (currentContact.RelatedEntity.EntityType) {

                            //        case Mercury.Server.Application.EntityType.Member: relatedEntity = CommonFunctions.MemberProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //        case Mercury.Server.Application.EntityType.Provider: relatedEntity = CommonFunctions.ProviderProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //    }

                            //}

                            dataTable.Rows.Add (

                                currentContract.Id,

                                currentContract.ProviderId,

                                currentContract.Provider.Name,

                                ((currentContract.ProviderAffiliation != null) ? currentContract.ProviderAffiliation.Id : 0),

                                currentContract.AffiliateProviderName,

                                currentContract.ProgramId,

                                currentContract.ProgramName,

                                currentContract.ContractId,

                                currentContract.ContractName,

                                currentContract.IsParticipating,

                                currentContract.IsCapitated,


                                currentContract.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentContract.TerminationDate.ToString ("MM/dd/yyyy"),

                                currentContract.TerminationDate.ToString ("yyyyMMdd") + currentContract.EffectiveDate.ToString ("yyyyMMdd")

                            );

                            #endregion

                        }

                    }

                    break;

                    #endregion

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                    break;

            }


            ProviderContractGrid_DataTable = dataTable;

            ProviderContractGrid.DataSource = ProviderContractGrid_DataTable;

            return;

        }

        protected void ProviderContractGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (ProviderContractGrid_PageSize != eventArgs.NewPageSize) {

                ProviderContractGrid_PageSize = eventArgs.NewPageSize;

                ProviderContractGrid_ManualDataRebind ();

            }

            return;

        }

        protected void ProviderContractGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            return;

        }

        public void ProviderContractGrid_ManualDataRebind () {

            ProviderContractGrid_Count = 0;

            ProviderContractGrid.DataSource = null;

            ProviderContractGrid.Rebind ();

            return;

        }

        #endregion

    }

}