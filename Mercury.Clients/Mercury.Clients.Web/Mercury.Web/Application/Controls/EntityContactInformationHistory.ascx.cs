using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityContactInformationHistory : System.Web.UI.UserControl {

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

                    InitializeEntityContactInformationHistoryGrid ();

                    EntityContactInformationHistoryGrid_ManualDataRebind ();

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

                InitializeEntityContactInformationHistoryGridAction ();

            }

        }


        private System.Data.DataTable EntityContactInformationHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable)Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("EntityContactInformationId");

                    dataTable.Columns.Add ("EntityId");

                    dataTable.Columns.Add ("ContactInformationDate");

                    dataTable.Columns.Add ("ContactInformationDirection");

                    dataTable.Columns.Add ("ContactInformationType");

                    dataTable.Columns.Add ("Outcome");

                    dataTable.Columns.Add ("Regarding");

                    dataTable.Columns.Add ("Remarks");

                    dataTable.Columns.Add ("RelatedEntity");

                    dataTable.Columns.Add ("RelatedObject");

                    dataTable.Columns.Add ("ContactInformationedByName");

                    Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_DataTable"] = value; }

        }

        private Int32 EntityContactInformationHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32)Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 EntityContactInformationHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32)Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32)Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 EntityContactInformationHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_Count"] != null) {

                    count = (Int32)Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "EntityContactInformationHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { get { return EntityContactInformationHistoryGrid.Height; } set { EntityContactInformationHistoryGrid.Height = value; } }

        public Int32 HistoryGridPageSize { get { return EntityContactInformationHistoryGrid.PageSize; } set { EntityContactInformationHistoryGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeEntityContactInformationHistoryGrid () {

            EntityContactInformationHistoryGrid.CurrentPageIndex = 0;

            EntityContactInformationHistoryGrid.PageSize = EntityContactInformationHistoryGrid_PageSize;

            EntityContactInformationHistoryGrid_Count = 0;

            EntityContactInformationHistoryGrid.DataSource = EntityContactInformationHistoryGrid_DataTable;

            EntityContactInformationHistoryGrid.DataBind ();

            InitializeEntityContactInformationHistoryGridAction ();

            return;

        }

        protected void InitializeEntityContactInformationHistoryGridAction () {

            if (Entity == null) { return; }


            String permissionRequired = Mercury.Server.EnvironmentPermissions.MemberContactInformationManage;

            permissionRequired = permissionRequired.Replace ("Member", Entity.EntityType.ToString ());


            // ENABLE THE ACTION COLUMN (AND TERMINATE LINK) IF USER HAS PERMISSIONS, EDIT AND TERMINATE ACTION COLUMNS

            EntityContactInformationHistoryGrid.Columns[EntityContactInformationHistoryGrid.Columns.Count - 2].Visible = ((AllowUserInteraction) && (MercuryApplication.HasEnvironmentPermission (permissionRequired)));
            
            EntityContactInformationHistoryGrid.Columns[EntityContactInformationHistoryGrid.Columns.Count - 1].Visible = ((AllowUserInteraction) && (MercuryApplication.HasEnvironmentPermission (permissionRequired)));

            if ((AllowUserInteraction) && (MercuryApplication.HasEnvironmentPermission (permissionRequired))) {

                EntityContactInformationHistoryGrid.MasterTableView.CommandItemDisplay = Telerik.Web.UI.GridCommandItemDisplay.Bottom;

            }

            return;

        }

        #endregion


        #region Entity ContactInformation History Grid Events

        protected void EntityContactInformationHistoryGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                AllowUserInteraction = AllowUserInteraction && (

                    (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberContactInformationManage))

                    );

            }

            if ((eventArgs.Item is Telerik.Web.UI.GridEditableItem) && (eventArgs.Item.IsInEditMode)) {

                Telerik.Web.UI.GridEditableItem item = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

                Telerik.Web.UI.RadComboBox EntityContactInformationEditContactType = (Telerik.Web.UI.RadComboBox)item.FindControl ("EntityContactInformationEditContactType");

                Telerik.Web.UI.RadDatePicker effectiveDatePicker = (Telerik.Web.UI.RadDatePicker)item.FindControl ("EntityContactInformationEditEffectiveDate");

                Telerik.Web.UI.RadDatePicker terminationDatePicker = (Telerik.Web.UI.RadDatePicker)item.FindControl ("EntityContactInformationEditTerminationDate");

                if (eventArgs.Item.OwnerTableView.IsItemInserted) {

                    // ITEM TO BE INSERTED, SET INITIAL SETTINGS FOR THE INSERT

                    effectiveDatePicker.SelectedDate = DateTime.Today;

                }

                else {

                    // ITEM TO BE EDITED, VALUES ARE DATA BOUND IN THE TEMPLATE DEFINITION UNDER ASP


                    EntityContactInformationEditContactType.SelectedValue = item.GetDataKeyValue ("ContactTypeInt32").ToString ();

                    effectiveDatePicker.SelectedDate = Convert.ToDateTime (item.GetDataKeyValue ("EffectiveDate"));

                    if (!Convert.ToDateTime (item.GetDataKeyValue ("TerminationDate")).Equals (new DateTime (9999, 12, 31))) {

                        terminationDatePicker.SelectedDate = Convert.ToDateTime (item.GetDataKeyValue ("TerminationDate"));

                    }

                }

            }

            return;

        }

        protected void EntityContactInformationHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = EntityContactInformationHistoryGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                
                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Entity == null) { dataTable.Rows.Clear (); }

                    else {

                        EntityContactInformationHistoryGrid.DataSource = Entity.ContactInformations;

                    }


                    // INSERT ANY TOOLBAR SELECTION RESTORES HERE

                    break;

                    #endregion

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                    break;

            }

            return;

        }

        protected void EntityContactInformationHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (EntityContactInformationHistoryGrid_PageSize != eventArgs.NewPageSize) {

                EntityContactInformationHistoryGrid_PageSize = eventArgs.NewPageSize;

                EntityContactInformationHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        protected void EntityContactInformationHistoryGrid_OnInsertUpdateCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            if (MercuryApplication == null) { return; }


            Telerik.Web.UI.GridEditFormItem item = (Telerik.Web.UI.GridEditFormItem)e.Item;

            Boolean isInsertMode = (e.Item is Telerik.Web.UI.GridEditFormInsertItem);

            Boolean hasValidationError = false;

            
            Telerik.Web.UI.RadComboBox EntityContactInformationEditContactType;

            Telerik.Web.UI.RadMaskedTextBox EntityContactInformationEditNumber;

            Telerik.Web.UI.RadTextBox EntityContactInformationEditExtension;

            Telerik.Web.UI.RadTextBox EntityContactInformationEditEmail;

            Telerik.Web.UI.RadDatePicker EntityContactInformationEditEffectiveDate;

            Telerik.Web.UI.RadDatePicker EntityContactInformationEditTerminationDate;

            Label EntityContactInformationSaveResponse;


            Client.Core.Entity.EntityContactInformation entityContactInformation;

            Client.Core.Entity.EntityContactInformation modifiedEntityContactInformation;


            EntityContactInformationEditContactType = (Telerik.Web.UI.RadComboBox)item.FindControl ("EntityContactInformationEditContactType");

            EntityContactInformationEditNumber = (Telerik.Web.UI.RadMaskedTextBox) item.FindControl ("EntityContactInformationEditNumber");

            EntityContactInformationEditExtension = (Telerik.Web.UI.RadTextBox) item.FindControl ("EntityContactInformationEditExtension");

            EntityContactInformationEditEmail = (Telerik.Web.UI.RadTextBox) item.FindControl ("EntityContactInformationEditEmail");

            EntityContactInformationEditEffectiveDate = (Telerik.Web.UI.RadDatePicker) item.FindControl ("EntityContactInformationEditEffectiveDate");
            
            EntityContactInformationEditTerminationDate = (Telerik.Web.UI.RadDatePicker) item.FindControl ("EntityContactInformationEditTerminationDate");

            EntityContactInformationSaveResponse = (Label) item.FindControl ("EntityContactInformationSaveResponse");



            if (isInsertMode) {

                entityContactInformation = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

                modifiedEntityContactInformation = new Client.Core.Entity.EntityContactInformation (MercuryApplication);

            }

            else {

                // RELOAD BASED ON ID

                entityContactInformation = MercuryApplication.EntityContactInformationGet (Convert.ToInt64 (item.GetDataKeyValue ("Id")), false);

                modifiedEntityContactInformation = entityContactInformation.Copy ();

            }

            modifiedEntityContactInformation.EntityId = Entity.Id;

            modifiedEntityContactInformation.ContactType = (Mercury.Server.Application.EntityContactType)Convert.ToInt32 (EntityContactInformationEditContactType.SelectedValue);

            modifiedEntityContactInformation.ContactSequence = 1;

            modifiedEntityContactInformation.Number = EntityContactInformationEditNumber.Text;

            modifiedEntityContactInformation.NumberExtension = EntityContactInformationEditExtension.Text;

            modifiedEntityContactInformation.Email = EntityContactInformationEditEmail.Text;

            if (EntityContactInformationEditEffectiveDate.SelectedDate.HasValue) {

                modifiedEntityContactInformation.EffectiveDate = EntityContactInformationEditEffectiveDate.SelectedDate.Value;

            }

            else {

                hasValidationError = true;

                EntityContactInformationSaveResponse.Text = "Invalid Effective Date selected.";

            }

            if (EntityContactInformationEditTerminationDate.SelectedDate.HasValue) {

                modifiedEntityContactInformation.TerminationDate = EntityContactInformationEditTerminationDate.SelectedDate.Value;

            }

            else {

                modifiedEntityContactInformation.TerminationDate = new DateTime (9999, 12, 31);

            }

            if ((!isInsertMode) && (entityContactInformation.IsEqual (modifiedEntityContactInformation))) {

                hasValidationError = true;

                EntityContactInformationSaveResponse.Text = "No modifications to contact information detected. Changes not saved.";

            }

            if (!hasValidationError) {

                // SAVE CHANGES

                hasValidationError = !MercuryApplication.EntityContactInformationSave (modifiedEntityContactInformation);

                if (hasValidationError) {

                    if (MercuryApplication.LastException != null) {

                        EntityContactInformationSaveResponse.Text = MercuryApplication.LastExceptionMessage;

                    }

                    else { EntityContactInformationSaveResponse.Text = "Unknown exception occurred. Unable to save changes."; }

                }

            }

            e.Canceled = hasValidationError;

            return;

        }

        protected void EntityContactInformationHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs e) {

            if (MercuryApplication == null) { return; }

            switch (e.CommandName) {

                case "RebindGrid": EntityContactInformationHistoryGrid_ManualDataRebind (); break;

            }

            return;

        }

        public void EntityContactInformationHistoryGrid_ManualDataRebind () {

            if (MercuryApplication == null) { return; }


            // FLUSH CONTACT CACHE FOR MANUAL REBIND

            MercuryApplication.EntityContactInformationsGet (Entity.Id, false);


            EntityContactInformationHistoryGrid_Count = 0;

            EntityContactInformationHistoryGrid.DataSource = null;

            EntityContactInformationHistoryGrid.Rebind ();

            return;

        }

        #endregion


        #region Dialog Windows Events

        protected void EntityContactInformationTerminateWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            Boolean success = true;


            if (!EntityContactInformationTerminateTerminationDate.SelectedDate.HasValue) {

                EntityContactInformationTerminateTerminationDate.SelectedDate = null;

                EntityContactInformationTerminateResponse.Text = "Unable to determine the requested Termination Date.";

                return;

            }


            Int64 terminatedEntityContactInformationId = Convert.ToInt64 (EntityContactInformationTerminateId.Text);

            Client.Core.Entity.EntityContactInformation currentEntityContactInformation = MercuryApplication.EntityContactInformationGet (terminatedEntityContactInformationId, false);

            if (Convert.ToDateTime (EntityContactInformationTerminateTerminationDate.SelectedDate) < currentEntityContactInformation.EffectiveDate) {

                EntityContactInformationTerminateTerminationDate.SelectedDate = null;

                EntityContactInformationTerminateResponse.Text = "The requested Termination Date is not valid.";

                return;

            }


            success = MercuryApplication.EntityContactInformationTerminate (currentEntityContactInformation, EntityContactInformationTerminateTerminationDate.SelectedDate.Value);


            if (success) {

                EntityContactInformationHistoryGrid_ManualDataRebind ();

                EntityContactInformationTerminateTerminationDate.SelectedDate = null;


                Telerik.Web.UI.RadAjaxManager telerikAjaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                if (telerikAjaxManager != null) {

                    telerikAjaxManager.ResponseScripts.Add ("EntityContactInformationTerminateWindow_Close ();");

                }

            }

            else {

                if (MercuryApplication.LastException != null) {

                    EntityContactInformationTerminateResponse.Text = MercuryApplication.LastException.Message;

                }

                else { EntityContactInformationTerminateResponse.Text = "Unknown exception occurreed. Unable to Terminate."; }

            }

            return;

        }

        #endregion

    }

}