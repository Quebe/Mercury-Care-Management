using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Controls {

    public partial class EntityContactHistory : System.Web.UI.UserControl {

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


        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                    InitializeEntityContactHistoryGrid ();

                    EntityContactHistoryGrid_ManualDataRebind ();

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


        private System.Data.DataTable EntityContactHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "EntityContactHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("EntityContactId");

                    dataTable.Columns.Add ("EntityId");

                    dataTable.Columns.Add ("ContactDate");

                    dataTable.Columns.Add ("ContactDirection");

                    dataTable.Columns.Add ("ContactType");

                    dataTable.Columns.Add ("Outcome");

                    dataTable.Columns.Add ("Regarding");

                    dataTable.Columns.Add ("Remarks");

                    dataTable.Columns.Add ("RelatedEntity");

                    dataTable.Columns.Add ("RelatedObject");

                    dataTable.Columns.Add ("ContactedByName");

                    Session[SessionCachePrefix + "EntityContactHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityContactHistoryGrid_DataTable"] = value; }

        }

        private Int32 EntityContactHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "EntityContactHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "EntityContactHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "EntityContactHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 EntityContactHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "EntityContactHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 EntityContactHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "EntityContactHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "EntityContactHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "EntityContactHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { get { return EntityContactHistoryGrid.Height; } set { EntityContactHistoryGrid.Height = value; } }

        public Int32 HistoryGridPageSize { get { return EntityContactHistoryGrid.PageSize; } set { EntityContactHistoryGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeEntityContactHistoryGrid () {

            EntityContactHistoryGrid.CurrentPageIndex = 0;

            EntityContactHistoryGrid.PageSize = EntityContactHistoryGrid_PageSize;

            EntityContactHistoryGrid_Count = 0;

            EntityContactHistoryGrid.DataSource = EntityContactHistoryGrid_DataTable;

            EntityContactHistoryGrid.DataBind ();

            return;

        }

        #endregion


        #region Entity Contact History Grid Events

        protected void EntityContactHistoryGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

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

        protected void EntityContactHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = EntityContactHistoryGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    EntityContactHistoryGrid_Count = 0;

                    EntityContactHistoryGrid_CurrentPage = 0;

                    EntityContactHistoryGrid_PageSize = 10;


                    EntityContactHistoryGrid.CurrentPageIndex = EntityContactHistoryGrid_CurrentPage;

                    EntityContactHistoryGrid.PageSize = EntityContactHistoryGrid_PageSize;

                    EntityContactHistoryGrid.VirtualItemCount = EntityContactHistoryGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    if (EntityContactHistoryGrid_CurrentPage >= 0) {

                        EntityContactHistoryGrid.CurrentPageIndex = EntityContactHistoryGrid_CurrentPage;

                    }

                    EntityContactHistoryGrid.PageSize = EntityContactHistoryGrid_PageSize;

                    EntityContactHistoryGrid.VirtualItemCount = EntityContactHistoryGrid_Count;

                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Entity == null) { dataTable.Rows.Clear (); }

                    else {

                        if (EntityContactHistoryGrid_Count == 0) {

                            EntityContactHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.EntityContactsGetCount (Entity.Id));

                            EntityContactHistoryGrid.VirtualItemCount = Convert.ToInt32 (EntityContactHistoryGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            EntityContactHistoryGrid_PageSize = EntityContactHistoryGrid.PageSize;

                        }

                        else {

                            EntityContactHistoryGrid.PageSize = EntityContactHistoryGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        EntityContactHistoryGrid_CurrentPage = EntityContactHistoryGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        List<Client.Core.Entity.EntityContact> entityContacts;

                        Int32 initialRow = EntityContactHistoryGrid.CurrentPageIndex * EntityContactHistoryGrid.PageSize + 1;

                        entityContacts = MercuryApplication.EntityContactsGetByPage (Entity.Id, initialRow, EntityContactHistoryGrid.PageSize);

                        foreach (Client.Core.Entity.EntityContact currentContact in entityContacts) {

                            #region Create Data Row

                            String relatedEntity = String.Empty;

                            if (currentContact.RelatedEntity != null) {

                                relatedEntity = currentContact.RelatedEntity.Name;

                                switch (currentContact.RelatedEntity.EntityType) {

                                    case Mercury.Server.Application.EntityType.Member: 
                                        
                                        relatedEntity = CommonFunctions.MemberProfileAnchor (currentContact.RelatedEntityId, relatedEntity).Replace ("MemberId=", "EntityId="); 
                                        
                                        break;

                                    case Mercury.Server.Application.EntityType.Provider:

                                        relatedEntity = CommonFunctions.ProviderProfileAnchor (currentContact.RelatedEntityId, relatedEntity).Replace ("ProviderId=", "EntityId=");
                                        
                                        break;

                                }

                            }

                            dataTable.Rows.Add (

                                currentContact.Id.ToString (),

                                currentContact.EntityId.ToString (),

                                currentContact.ContactDate.ToString (),

                                currentContact.Direction.ToString (),

                                currentContact.ContactType.ToString (),

                                currentContact.ContactOutcome.ToString (),

                                currentContact.Regarding,

                                currentContact.Remarks,

                                relatedEntity,
            
                                currentContact.RelatedObjectType,

                                currentContact.ContactedByName

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


            EntityContactHistoryGrid_DataTable = dataTable;

            EntityContactHistoryGrid.DataSource = EntityContactHistoryGrid_DataTable;

            return;

        }

        protected void EntityContactHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (EntityContactHistoryGrid_PageSize != eventArgs.NewPageSize) {

                EntityContactHistoryGrid_PageSize = eventArgs.NewPageSize;

                EntityContactHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        protected void EntityContactHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            String hrefLocation = String.Empty;

            switch (eventArgs.CommandName) {

                case "Contact":

                    hrefLocation = "/Application/Actions/Contact.aspx?EntityId=" + Entity.Id.ToString ();

                    Response.Redirect (hrefLocation, true);

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("EntityContactHistoryGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        public void EntityContactHistoryGrid_ManualDataRebind () {

            EntityContactHistoryGrid_Count = 0;

            EntityContactHistoryGrid.DataSource = null;

            EntityContactHistoryGrid.Rebind ();

            return;

        }

        #endregion

    }

}