using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class ProviderAffiliation : System.Web.UI.UserControl {

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

            get { return (Client.Core.Provider.Provider) Session[SessionCachePrefix + "Provider"]; }

            set {

                Client.Core.Provider.Provider provider = (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"];

                if (provider != value) {

                    Session[SessionCachePrefix + "Provider"] = value;

                    InitializeProviderAffiliationGrid ();

                    ProviderAffiliationGrid_ManualDataRebind ();

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


        private System.Data.DataTable ProviderAffiliationGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "ProviderAffiliationGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ProviderAffiliationId");

                    dataTable.Columns.Add ("ProviderId");

                    dataTable.Columns.Add ("ProviderName");

                    dataTable.Columns.Add ("AffiliateProviderId");

                    dataTable.Columns.Add ("AffiliateProviderName");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("SortDateField");

                    Session[SessionCachePrefix + "ProviderAffiliationGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "ProviderAffiliationGrid_DataTable"] = value; }

        }

        private Int32 ProviderAffiliationGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "ProviderAffiliationGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "ProviderAffiliationGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "ProviderAffiliationGrid_CurrentPage"] = value; }

        }

        private Int32 ProviderAffiliationGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "ProviderAffiliationGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 ProviderAffiliationGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "ProviderAffiliationGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "ProviderAffiliationGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "ProviderAffiliationGrid_Count"] = value; }

        }


        public Unit GridHeight { get { return ProviderAffiliationGrid.Height; } set { ProviderAffiliationGrid.Height = value; } }

        public Int32 GridPageSize { get { return ProviderAffiliationGrid.PageSize; } set { ProviderAffiliationGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeProviderAffiliationGrid () {

            ProviderAffiliationGrid.CurrentPageIndex = 0;

            ProviderAffiliationGrid.PageSize = ProviderAffiliationGrid_PageSize;

            ProviderAffiliationGrid_Count = 0;

            ProviderAffiliationGrid.DataSource = ProviderAffiliationGrid_DataTable;

            ProviderAffiliationGrid.DataBind ();

            return;

        }

        #endregion


        #region Affiliation Grid Events

        protected void ProviderAffiliationGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

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

        protected void ProviderAffiliationGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = ProviderAffiliationGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    ProviderAffiliationGrid_Count = 0;

                    ProviderAffiliationGrid_CurrentPage = 0;

                    ProviderAffiliationGrid_PageSize = 10;


                    ProviderAffiliationGrid.CurrentPageIndex = ProviderAffiliationGrid_CurrentPage;

                    ProviderAffiliationGrid.PageSize = ProviderAffiliationGrid_PageSize;

                    ProviderAffiliationGrid.VirtualItemCount = ProviderAffiliationGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    ProviderAffiliationGrid.CurrentPageIndex = ProviderAffiliationGrid_CurrentPage;

                    ProviderAffiliationGrid.PageSize = ProviderAffiliationGrid_PageSize;

                    ProviderAffiliationGrid.VirtualItemCount = ProviderAffiliationGrid_Count;

                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Provider == null) { dataTable.Rows.Clear (); }

                    else {

                        if (ProviderAffiliationGrid_Count == 0) {

                            ProviderAffiliationGrid_Count = Convert.ToInt32 (MercuryApplication.ProviderAffiliationsGet (Provider.Id, true).Count);

                            ProviderAffiliationGrid.VirtualItemCount = Convert.ToInt32 (ProviderAffiliationGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            ProviderAffiliationGrid_PageSize = ProviderAffiliationGrid.PageSize;

                        }

                        else {

                            ProviderAffiliationGrid.PageSize = ProviderAffiliationGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        ProviderAffiliationGrid_CurrentPage = ProviderAffiliationGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        Int32 initialRow = ProviderAffiliationGrid.CurrentPageIndex * ProviderAffiliationGrid.PageSize + 1;

                        List<Client.Core.Provider.ProviderAffiliation> affiliations;

                        affiliations = MercuryApplication.ProviderAffiliationsGet (Provider.Id, true);

                        foreach (Client.Core.Provider.ProviderAffiliation currentAffiliation in affiliations) {

                            #region Create Data Row

                            //String relatedEntity = currentContact.RelatedEntityName;

                            //if (currentContact.RelatedEntity != null) {

                            //    switch (currentContact.RelatedEntity.EntityType) {

                            //        case Mercury.Server.Application.EntityType.Member: relatedEntity = CommonFunctions.MemberProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //        case Mercury.Server.Application.EntityType.Provider: relatedEntity = CommonFunctions.ProviderProfileAnchor (currentContact.RelatedEntityId, relatedEntity); break;

                            //    }

                            //}

                            dataTable.Rows.Add (

                                currentAffiliation.Id,

                                currentAffiliation.ProviderId,

                                currentAffiliation.Provider.Name,

                                currentAffiliation.AffiliateProviderId,

                                currentAffiliation.AffiliateProvider.Name,

                                currentAffiliation.EffectiveDate.ToString ("MM/dd/yyyy"),

                                currentAffiliation.TerminationDate.ToString ("MM/dd/yyyy"),

                                currentAffiliation.TerminationDate.ToString ("yyyyMMdd") + currentAffiliation.EffectiveDate.ToString ("yyyyMMdd")

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


            ProviderAffiliationGrid_DataTable = dataTable;

            ProviderAffiliationGrid.DataSource = ProviderAffiliationGrid_DataTable;

            return;

        }

        protected void ProviderAffiliationGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (ProviderAffiliationGrid_PageSize != eventArgs.NewPageSize) {

                ProviderAffiliationGrid_PageSize = eventArgs.NewPageSize;

                ProviderAffiliationGrid_ManualDataRebind ();

            }

            return;

        }

        protected void ProviderAffiliationGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            return;

        }

        public void ProviderAffiliationGrid_ManualDataRebind () {

            ProviderAffiliationGrid_Count = 0;

            ProviderAffiliationGrid.DataSource = null;

            ProviderAffiliationGrid.Rebind ();

            return;

        }

        #endregion

    }

}