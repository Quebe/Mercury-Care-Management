using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class ProviderServiceLocation : System.Web.UI.UserControl {


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

                    InitializeProviderServiceLocationGrid ();

                    ProviderServiceLocationGrid_ManualDataRebind ();

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


        private System.Data.DataTable ProviderServiceLocationGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "ProviderServiceLocationGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ProviderServiceLocationId");

                    dataTable.Columns.Add ("AffiliateProviderName");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("Program");

                    dataTable.Columns.Add ("Address");

                    dataTable.Columns.Add ("ServiceLocationNumber");

                    dataTable.Columns.Add ("IsPcp");

                    dataTable.Columns.Add ("IsAcceptingNewPatients");

                    dataTable.Columns.Add ("PanelSizeMaximum");

                    dataTable.Columns.Add ("AgeMinimum");

                    dataTable.Columns.Add ("AgeMaximum");

                    dataTable.Columns.Add ("HasHandicapAccess");

                    dataTable.Columns.Add ("OfficeHours");

                    dataTable.Columns.Add ("SortDateField");

                    Session[SessionCachePrefix + "ProviderServiceLocationGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "ProviderServiceLocationGrid_DataTable"] = value; }

        }

        private Int32 ProviderServiceLocationGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "ProviderServiceLocationGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "ProviderServiceLocationGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "ProviderServiceLocationGrid_CurrentPage"] = value; }

        }

        private Int32 ProviderServiceLocationGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "ProviderServiceLocationGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 ProviderServiceLocationGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "ProviderServiceLocationGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "ProviderServiceLocationGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "ProviderServiceLocationGrid_Count"] = value; }

        }


        public Unit GridHeight { get { return ProviderServiceLocationGrid.Height; } set { ProviderServiceLocationGrid.Height = value; } }

        public Int32 GridPageSize { get { return ProviderServiceLocationGrid.PageSize; } set { ProviderServiceLocationGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeProviderServiceLocationGrid () {

            ProviderServiceLocationGrid.CurrentPageIndex = 0;

            ProviderServiceLocationGrid.PageSize = ProviderServiceLocationGrid_PageSize;

            ProviderServiceLocationGrid_Count = 0;

            ProviderServiceLocationGrid.DataSource = ProviderServiceLocationGrid_DataTable;

            ProviderServiceLocationGrid.DataBind ();

            return;

        }

        #endregion


        #region ServiceLocation Grid Events

        protected void ProviderServiceLocationGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

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

        protected void ProviderServiceLocationGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = ProviderServiceLocationGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    ProviderServiceLocationGrid_Count = 0;

                    ProviderServiceLocationGrid_CurrentPage = 0;

                    ProviderServiceLocationGrid_PageSize = 10;


                    ProviderServiceLocationGrid.CurrentPageIndex = ProviderServiceLocationGrid_CurrentPage;

                    ProviderServiceLocationGrid.PageSize = ProviderServiceLocationGrid_PageSize;

                    ProviderServiceLocationGrid.VirtualItemCount = ProviderServiceLocationGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    ProviderServiceLocationGrid.CurrentPageIndex = ProviderServiceLocationGrid_CurrentPage;

                    ProviderServiceLocationGrid.PageSize = ProviderServiceLocationGrid_PageSize;

                    ProviderServiceLocationGrid.VirtualItemCount = ProviderServiceLocationGrid_Count;

                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Provider == null) { dataTable.Rows.Clear (); }

                    else {

                        if (ProviderServiceLocationGrid_Count == 0) {

                            ProviderServiceLocationGrid_Count = Convert.ToInt32 (MercuryApplication.ProviderServiceLocationsGet (Provider.Id, true).Count);

                            ProviderServiceLocationGrid.VirtualItemCount = Convert.ToInt32 (ProviderServiceLocationGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            ProviderServiceLocationGrid_PageSize = ProviderServiceLocationGrid.PageSize;

                        }

                        else {

                            ProviderServiceLocationGrid.PageSize = ProviderServiceLocationGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        ProviderServiceLocationGrid_CurrentPage = ProviderServiceLocationGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        Int32 initialRow = ProviderServiceLocationGrid.CurrentPageIndex * ProviderServiceLocationGrid.PageSize + 1;

                        List<Client.Core.Provider.ProviderServiceLocation> locations;

                        locations = MercuryApplication.ProviderServiceLocationsGet (Provider.Id, true);

                        foreach (Client.Core.Provider.ProviderServiceLocation currentServiceLocation in locations) {

                            #region Create Data Row

                            //String relatedEntity = currentContact.RelatedEntityName;

                            //if (currentContact.RelatedEntity != null) {

                            //    switch (currentContact.RelatedEntity.EntityType) {

                            //        case Mercury.Server.Application.EntityType.Member: relatedEntity = CommonFunctions.MemberProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //        case Mercury.Server.Application.EntityType.Provider: relatedEntity = CommonFunctions.ProviderProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //    }

                            //}

                            dataTable.Rows.Add (

                                currentServiceLocation.ProviderServiceLocationId,

                                currentServiceLocation.AffiliateProviderName,

                                currentServiceLocation.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentServiceLocation.TerminationDate.ToString ("MM/dd/yyyy"),

                                currentServiceLocation.ProviderEnrollment.Program.Name,

                                currentServiceLocation.EntityAddress.Line1 + "<br />" +

                                currentServiceLocation.EntityAddress.Line2 + "<br />" +

                                currentServiceLocation.EntityAddress.CityStateZipCode,

                                currentServiceLocation.ServiceLocationNumber,

                                currentServiceLocation.IsPcp,

                                currentServiceLocation.IsAcceptingNewPatients,

                                currentServiceLocation.PanelSizeMaximum,

                                currentServiceLocation.AgeMinimum,

                                currentServiceLocation.AgeMaximum,

                                currentServiceLocation.HasHandicapAccess,

                                currentServiceLocation.OfficeHoursSunday,

                                currentServiceLocation.TerminationDate.ToString ("yyyyMMdd") + currentServiceLocation.EffectiveDate.ToString ("yyyyMMdd")

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


            ProviderServiceLocationGrid_DataTable = dataTable;

            ProviderServiceLocationGrid.DataSource = ProviderServiceLocationGrid_DataTable;

            return;

        }

        protected void ProviderServiceLocationGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (ProviderServiceLocationGrid_PageSize != eventArgs.NewPageSize) {

                ProviderServiceLocationGrid_PageSize = eventArgs.NewPageSize;

                ProviderServiceLocationGrid_ManualDataRebind ();

            }

            return;

        }

        protected void ProviderServiceLocationGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            return;

        }

        public void ProviderServiceLocationGrid_ManualDataRebind () {

            ProviderServiceLocationGrid_Count = 0;

            ProviderServiceLocationGrid.DataSource = null;

            ProviderServiceLocationGrid.Rebind ();

            return;

        }

        #endregion


    }
}