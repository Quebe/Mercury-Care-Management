using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityAddressHistory : System.Web.UI.UserControl {

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

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }


        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                    InitializeEntityAddressHistoryGrid ();

                    EntityAddressHistoryGrid_ManualDataRebind ();

                }

            }

        }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean)Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                // OVERRIDE TO DISABLE TOOLBAR

                Session[SessionCachePrefix + "AllowUserInteraction"] = value;

                InitializeEntityAddressHistoryGridAction ();

            }

        }


        private System.Data.DataTable EntityAddressHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable)Session[SessionCachePrefix + "EntityAddressHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("EntityAddressId");

                    dataTable.Columns.Add ("EntityId");

                    dataTable.Columns.Add ("AddressDate");

                    dataTable.Columns.Add ("AddressDirection");

                    dataTable.Columns.Add ("AddressType");

                    dataTable.Columns.Add ("Outcome");

                    dataTable.Columns.Add ("Regarding");

                    dataTable.Columns.Add ("Remarks");

                    dataTable.Columns.Add ("RelatedEntity");

                    dataTable.Columns.Add ("RelatedObject");

                    dataTable.Columns.Add ("AddressedByName");

                    Session[SessionCachePrefix + "EntityAddressHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityAddressHistoryGrid_DataTable"] = value; }

        }

        private Int32 EntityAddressHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "EntityAddressHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32)Session[SessionCachePrefix + "EntityAddressHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "EntityAddressHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 EntityAddressHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32)Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32)Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "EntityAddressHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 EntityAddressHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "EntityAddressHistoryGrid_Count"] != null) {

                    count = (Int32)Session[SessionCachePrefix + "EntityAddressHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "EntityAddressHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { get { return EntityAddressHistoryGrid.Height; } set { EntityAddressHistoryGrid.Height = value; } }

        public Int32 HistoryGridPageSize { get { return EntityAddressHistoryGrid.PageSize; } set { EntityAddressHistoryGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeEntityAddressHistoryGrid () {

            EntityAddressHistoryGrid.CurrentPageIndex = 0;

            EntityAddressHistoryGrid.PageSize = EntityAddressHistoryGrid_PageSize;

            EntityAddressHistoryGrid_Count = 0;

            EntityAddressHistoryGrid.DataSource = EntityAddressHistoryGrid_DataTable;

            EntityAddressHistoryGrid.DataBind ();

            InitializeEntityAddressHistoryGridAction ();

            return;

        }

        protected void InitializeEntityAddressHistoryGridAction () {

            if (Entity == null) { return; }

            
            String permissionRequired = Mercury.Server.EnvironmentPermissions.MemberAddressManage;

            permissionRequired = permissionRequired.Replace ("Member", Entity.EntityType.ToString ());


            EntityAddressHistoryGrid.Columns[EntityAddressHistoryGrid.Columns.Count - 1].Visible = ((AllowUserInteraction) && (MercuryApplication.HasEnvironmentPermission (permissionRequired)));

            return;

        }

        #endregion


        #region Entity Address History Grid Events

        protected void EntityAddressHistoryGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                //Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem)eventArgs.Item;

                //Telerik.Web.UI.RadToolBar AddressToolbar = (Telerik.Web.UI.RadToolBar)commandItem.FindControl ("EntityAddressToolbar");

                //Telerik.Web.UI.RadToolBarItem toolbarItem;


                AllowUserInteraction = AllowUserInteraction && (

                    (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage))

                    );


                //if (!AllowUserInteraction) {

                //    AddressToolbar.Visible = false;

                //}


                //toolbarItem = AddressToolbar.Items.FindItemByValue ("Address");

                //toolbarItem.Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberAddressManage));

            }

            return;

        }

        protected void EntityAddressHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = EntityAddressHistoryGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:


                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Entity == null) { dataTable.Rows.Clear (); }

                    else {
                        
                        EntityAddressHistoryGrid.DataSource = Entity.Addresses;

                    }


                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE


                    //else {

                    //    if (EntityAddressHistoryGrid_Count == 0) {

                    //        EntityAddressHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.EntityAddressesGetCount (Entity.Id));

                    //        EntityAddressHistoryGrid.VirtualItemCount = Convert.ToInt32 (EntityAddressHistoryGrid_Count);

                    //    }

                    //    if (!pageSizeChanged) {

                    //        EntityAddressHistoryGrid_PageSize = EntityAddressHistoryGrid.PageSize;

                    //    }

                    //    else {

                    //        EntityAddressHistoryGrid.PageSize = EntityAddressHistoryGrid_PageSize;

                    //        pageSizeChanged = false;

                    //    }


                    //    EntityAddressHistoryGrid_CurrentPage = EntityAddressHistoryGrid.CurrentPageIndex;

                    //    dataTable.Rows.Clear ();

                    //    List<Client.Core.Entity.EntityAddress> entityAddresss;

                    //    Int32 initialRow = EntityAddressHistoryGrid.CurrentPageIndex * EntityAddressHistoryGrid.PageSize + 1;

                    //    entityAddresss = MercuryApplication.EntityAddresssGetByPage (Entity.Id, initialRow, EntityAddressHistoryGrid.PageSize);

                    //    foreach (Client.Core.Entity.EntityAddress currentAddress in entityAddresss) {

                    //        #region Create Data Row

                    //        String relatedEntity = String.Empty;

                    //        if (currentAddress.RelatedEntity != null) {

                    //            relatedEntity = currentAddress.RelatedEntity.Name;

                    //            switch (currentAddress.RelatedEntity.EntityType) {

                    //                case Mercury.Server.Application.EntityType.Member:

                    //                    relatedEntity = CommonFunctions.MemberProfileAnchor (currentAddress.RelatedEntityId, relatedEntity).Replace ("MemberId=", "EntityId=");

                    //                    break;

                    //                case Mercury.Server.Application.EntityType.Provider:

                    //                    relatedEntity = CommonFunctions.ProviderProfileAnchor (currentAddress.RelatedEntityId, relatedEntity).Replace ("ProviderId=", "EntityId=");

                    //                    break;

                    //            }

                    //        }

                    //        dataTable.Rows.Add (

                    //            currentAddress.Id.ToString (),

                    //            currentAddress.EntityId.ToString (),

                    //            currentAddress.AddressDate.ToString (),

                    //            currentAddress.Direction.ToString (),

                    //            currentAddress.AddressType.ToString (),

                    //            currentAddress.AddressOutcome.ToString (),

                    //            currentAddress.Regarding,

                    //            currentAddress.Remarks,

                    //            relatedEntity,

                    //            currentAddress.RelatedObjectType,

                    //            currentAddress.AddressedByName

                    //        );

                    //        #endregion

                    //    }

                    //}

                    break;

                    #endregion

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                    break;

            }

            return;

        }

        protected void EntityAddressHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (EntityAddressHistoryGrid_PageSize != eventArgs.NewPageSize) {

                EntityAddressHistoryGrid_PageSize = eventArgs.NewPageSize;

                EntityAddressHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        protected void EntityAddressHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            String hrefLocation = String.Empty;

            switch (eventArgs.CommandName) {

                case "Address":

                    hrefLocation = "/Application/Actions/Address.aspx?EntityId=" + Entity.Id.ToString ();

                    Response.Redirect (hrefLocation, true);

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("EntityAddressHistoryGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        public void EntityAddressHistoryGrid_ManualDataRebind () {

            EntityAddressHistoryGrid_Count = 0;

            EntityAddressHistoryGrid.DataSource = null;

            EntityAddressHistoryGrid.Rebind ();

            return;

        }

        #endregion


        #region Dialog Windows Events

        protected void EntityAddressTerminateWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            Boolean success = true;


            if (!EntityAddressTerminateTerminationDate.SelectedDate.HasValue) {

                EntityAddressTerminateTerminationDate.SelectedDate = null;

                EntityAddressTerminateResponse.Text = "Unable to determine the requested Termination Date.";

                return;

            }


            Int64 terminatedEntityAddressId = Convert.ToInt64 (EntityAddressTerminateId.Text);

            Client.Core.Entity.EntityAddress currentEntityAddress = MercuryApplication.EntityAddressGet (terminatedEntityAddressId, false);

            if (Convert.ToDateTime (EntityAddressTerminateTerminationDate.SelectedDate) < currentEntityAddress.EffectiveDate) {

                EntityAddressTerminateTerminationDate.SelectedDate = null;

                EntityAddressTerminateResponse.Text = "The requested Termination Date is not valid.";

                return;

            }


            success = MercuryApplication.EntityAddressTerminate (currentEntityAddress, EntityAddressTerminateTerminationDate.SelectedDate.Value);


            if (success) {

                EntityAddressHistoryGrid_ManualDataRebind ();

                EntityAddressTerminateTerminationDate.SelectedDate = null;


                Telerik.Web.UI.RadAjaxManager telerikAjaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                if (telerikAjaxManager != null) {

                    telerikAjaxManager.ResponseScripts.Add ("EntityAddressTerminateWindow_Close ();");

                }

            }

            else {

                if (MercuryApplication.LastException != null) {

                    EntityAddressTerminateResponse.Text = MercuryApplication.LastException.Message;

                }

                else { EntityAddressTerminateResponse.Text = "Unknown exception occurreed. Unable to Terminate."; }

            }

            return;

        }

        #endregion 

    }

}