using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityDocumentHistory : System.Web.UI.UserControl {

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

                    InitializeEntityDocumentHistoryGrid ();

                    EntityDocumentHistoryGrid_ManualDataRebind ();

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
                
                Session[SessionCachePrefix + "AllowUserInteraction"] = value; 
            
            }

        }


        private System.Data.DataTable EntityDocumentHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "EntityDocumentHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("Detail");

                    dataTable.Columns.Add ("DocumentType");

                    dataTable.Columns.Add ("DocumentInstanceId");

                    dataTable.Columns.Add ("DocumentId");

                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("DocumentTitle");

                    dataTable.Columns.Add ("Version");

                    dataTable.Columns.Add ("ContactType");

                    dataTable.Columns.Add ("ReadyToSendDate");

                    dataTable.Columns.Add ("SentDate");

                    dataTable.Columns.Add ("ReceivedDate");

                    dataTable.Columns.Add ("ReturnedDate");

                    dataTable.Columns.Add ("RelatedEntity");

                    dataTable.Columns.Add ("Automation");

                    dataTable.Columns.Add ("CreateDate");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("ModifiedDate");

                    dataTable.Columns.Add ("ModifiedAccountName");

                    Session[SessionCachePrefix + "EntityDocumentHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityDocumentHistoryGrid_DataTable"] = value; }

        }

        private Int32 EntityDocumentHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "EntityDocumentHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "EntityDocumentHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "EntityDocumentHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 EntityDocumentHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"] = value;

                }
                
                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session [SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "EntityDocumentHistoryGrid_PageSize"] = value; 

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 EntityDocumentHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "EntityDocumentHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "EntityDocumentHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "EntityDocumentHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { get { return EntityDocumentHistoryGrid.Height; } set { EntityDocumentHistoryGrid.Height = value; } }

        public Int32 HistoryGridPageSize { get { return EntityDocumentHistoryGrid.PageSize; } set { EntityDocumentHistoryGrid.PageSize = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }



        private Telerik.Web.UI.RadToolBar EntityDocumentToolbar {

            get {

                Telerik.Web.UI.RadToolBar toolbar = null;

                if (EntityDocumentHistoryGrid.MasterTableView.Controls[0] != null) {

                    if (EntityDocumentHistoryGrid.MasterTableView.Controls[0].Controls[0] != null) {

                        if (EntityDocumentHistoryGrid.MasterTableView.Controls[0].Controls[0].Controls[0] != null) {

                            toolbar = (Telerik.Web.UI.RadToolBar) EntityDocumentHistoryGrid.MasterTableView.Controls[0].Controls[0].Controls[0].FindControl ("EntityDocumentToolbar");

                        }

                    }

                }

                return toolbar;

            }

        }

        private String FormSelection_SelectedValue {

            get {

                String selectedValue = (String) Session[SessionCachePrefix + "FormSelection_SelectedValue"];

                if (selectedValue == null) { selectedValue = String.Empty; }

                return selectedValue;

            }

            set { Session[SessionCachePrefix + "FormSelection_SelectedValue"] = value; }

        }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (!AllowUserInteraction) {

                CorrespondenceMarkSentDate.ClientEvents.OnError = String.Empty;

                CorrespondenceMarkReturnedDate.ClientEvents.OnError = String.Empty;
               
            }

            return;

        }

        protected void InitializeEntityDocumentHistoryGrid () {

            EntityDocumentHistoryGrid.CurrentPageIndex = 0;

            EntityDocumentHistoryGrid.PageSize = EntityDocumentHistoryGrid_PageSize;

            EntityDocumentHistoryGrid_Count = 0;

            EntityDocumentHistoryGrid.DataSource = EntityDocumentHistoryGrid_DataTable;

            EntityDocumentHistoryGrid.DataBind ();

            return;

        }

        #endregion


        #region Entity Document History Grid Events

        protected void EntityDocumentHistoryGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem) eventArgs.Item;

                Telerik.Web.UI.RadToolBar EnterFormToolbar = (Telerik.Web.UI.RadToolBar) commandItem.FindControl ("EntityDocumentToolbar");

                Telerik.Web.UI.RadToolBarItem toolbarItem;


                AllowUserInteraction = AllowUserInteraction && (

                    (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionSendCorrespondence))

                    || (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionDataEnterForm))

                    );


                if (!AllowUserInteraction) {

                    EnterFormToolbar.Visible = false;

                }


                // OVERRIDE TOOLBAR VISIBILTIY 

                EnterFormToolbar.Visible = false;


                toolbarItem = EnterFormToolbar.Items.FindItemByValue ("SendCorrespondence");

                toolbarItem.Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionSendCorrespondence));

                #region Enter Form

                toolbarItem = EnterFormToolbar.Items.FindItemByValue ("EnterReceivedFormToolbarButton");

                toolbarItem.Visible = (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberActionDataEnterForm));

                Telerik.Web.UI.RadComboBox FormSelection = (Telerik.Web.UI.RadComboBox) toolbarItem.FindControl ("FormSelection");

                if (FormSelection != null) {

                    if (FormSelection.Items.Count == 0) {

                        FormSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Form Selected", String.Empty));

                        foreach (Mercury.Server.Application.SearchResultFormHeader currentForm in MercuryApplication.FormsAvailable (true)) {

                            if (currentForm.Enabled) {

                                FormSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentForm.Name, currentForm.Id.ToString ()));

                            }

                        }

                    }

                    FormSelection.SelectedValue = FormSelection_SelectedValue;

                }

                #endregion 

            }

            return;

        }

        protected void EntityDocumentHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = EntityDocumentHistoryGrid_DataTable;


            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    EntityDocumentHistoryGrid_Count = 0;

                    EntityDocumentHistoryGrid_CurrentPage = 0;

                    EntityDocumentHistoryGrid_PageSize = 10;


                    EntityDocumentHistoryGrid.CurrentPageIndex = EntityDocumentHistoryGrid_CurrentPage;

                    EntityDocumentHistoryGrid.PageSize = EntityDocumentHistoryGrid_PageSize;

                    EntityDocumentHistoryGrid.VirtualItemCount = EntityDocumentHistoryGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    if (EntityDocumentHistoryGrid_CurrentPage >= 0) {

                        EntityDocumentHistoryGrid.CurrentPageIndex = EntityDocumentHistoryGrid_CurrentPage;

                    }

                    EntityDocumentHistoryGrid.PageSize = EntityDocumentHistoryGrid_PageSize;

                    EntityDocumentHistoryGrid.VirtualItemCount = EntityDocumentHistoryGrid_Count;


                    if (EntityDocumentToolbar != null) {

                        Telerik.Web.UI.RadToolBarItem toolbarItem = EntityDocumentToolbar.Items.FindItemByValue ("EnterReceivedFormToolbarButton");

                        Telerik.Web.UI.RadComboBox FormSelection = (Telerik.Web.UI.RadComboBox) toolbarItem.FindControl ("FormSelection");

                        if (FormSelection != null) {

                            FormSelection_SelectedValue = FormSelection.SelectedValue;

                        }

                    }

                    #endregion 

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid
                    
                    if (Entity == null) { dataTable.Rows.Clear (); }

                    else {

                        if (EntityDocumentHistoryGrid_Count == 0) {

                            EntityDocumentHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.EntityDocumentsGetCount (Entity.Id));

                            EntityDocumentHistoryGrid.VirtualItemCount = Convert.ToInt32 (EntityDocumentHistoryGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            EntityDocumentHistoryGrid_PageSize = EntityDocumentHistoryGrid.PageSize;

                        }

                        else {

                            EntityDocumentHistoryGrid.PageSize = EntityDocumentHistoryGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        EntityDocumentHistoryGrid_CurrentPage = EntityDocumentHistoryGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        List<Mercury.Server.Application.EntityDocumentDataView> entityDocuments;

                        Int32 initialRow = EntityDocumentHistoryGrid.CurrentPageIndex * EntityDocumentHistoryGrid.PageSize + 1;

                        entityDocuments = MercuryApplication.EntityDocumentsGetByPage (Entity.Id, initialRow, EntityDocumentHistoryGrid.PageSize);

                        foreach (Mercury.Server.Application.EntityDocumentDataView currentDocument in entityDocuments) {

                            #region Create Data Row

                            String detail = String.Empty;

                            String markSent = "&nbsp";

                            String markReceived = "&nbsp";

                            String markReturned = "&nbsp";

                            String automation = "&nbsp";

                            String relatedEntity = String.Empty;

                            
                            if (currentDocument.DocumentType == "Correspondence") {

                                Client.Core.Reference.Correspondence correspondence = MercuryApplication.CorrespondenceGet (currentDocument.DocumentId, true);


                                if ((currentDocument.HasImage) || (correspondence.Content.Count > 0)) {

                                    detail = "<a href=\"javascript:void(0);\" onclick=\"javascript:window.open ('/Application/Common/Image.aspx?ObjectType=EntityCorrespondence&ObjectId=" + currentDocument.EntityDocumentId.ToString () + "&Render=true', '_blank', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1');\"><img src=\"/Images/Common16/Document.png\" border=\"0\" /></a>";

                                }


                                if (currentDocument.SentDate.HasValue) {

                                    markSent = currentDocument.SentDate.Value.ToString ("MM/dd/yyyy");

                                    if (currentDocument.ReturnedDate.HasValue) {

                                        markReturned = currentDocument.ReturnedDate.Value.ToString ("MM/dd/yyyy");

                                    }

                                    else if ((!currentDocument.ReceivedDate.HasValue) && (correspondence != null)) {

                                        markReturned = " <a href=\"javascript:Correspondence_MarkReturned (" + currentDocument.EntityDocumentId.ToString () + ", '" + correspondence.Name + "', '" + currentDocument.SentDate.Value.ToString ("MM/dd/yyyy") + "')\" title=\"Mark the correspondence as returned as undeliverable.\">(mark returned)</a>";

                                    }

                                }

                                else {

                                    markSent = " <a href=\"javascript:Correspondence_MarkSent (" + currentDocument.EntityDocumentId.ToString () + ", '" + correspondence.Name + "', '" + currentDocument.ReadyToSendDate.Value.ToString ("MM/dd/yyyy") + "')\">(mark sent)</a>";

                                }


                                // MCM-1175: Received Date was not visible unless the sent date had a value. This was not true for

                                // inbound correspondence only. Moved the received date setter outside. 

                                if (currentDocument.ReceivedDate.HasValue) {

                                    markReceived = currentDocument.ReceivedDate.Value.ToString ("MM/dd/yyyy");

                                }

                                // ALLOW DATA ENTRY ON THOSE OUTBOUND CORRESPONDENCE THAT HAVE BEEN SENT

                                else if ((currentDocument.SentDate.HasValue) && (!currentDocument.ReturnedDate.HasValue) && (correspondence.FormId != 0) && (currentDocument.EntityFormId == 0)) {

                                    markReceived = " <a href=\"/Application/Forms/FormDataEntry/FormDataEntry.aspx?EntityCorrespondenceId=" + currentDocument.EntityDocumentId.ToString () + "&formid=" + correspondence.FormId.ToString () + "\">(received - data enter)</a>";

                                }


                                // TODO: MOVE AUTOMATION INTO THE DATA VIEW 

                                Client.Core.Entity.EntityCorrespondence entityCorrespondence = MercuryApplication.EntityCorrespondenceGet (currentDocument.EntityDocumentId, true);

                                if (entityCorrespondence != null) {

                                    String automationTitle = ((entityCorrespondence.AutomationDate.HasValue) ? "[" + entityCorrespondence.AutomationDate.Value.ToString ("MM/dd/yyyy") + "] " : String.Empty);

                                    automationTitle += entityCorrespondence.AutomationStatus.ToString ();

                                    automationTitle += (!String.IsNullOrWhiteSpace (entityCorrespondence.AutomationException)) ? ": " + entityCorrespondence.AutomationException : String.Empty;

                                    automation = "<span title=\"" + automationTitle + "\"><img src=\"/Images/Common16/Automation" + entityCorrespondence.AutomationStatus.ToString () + ".png\" /></span>";

                                }


                                if (entityCorrespondence.RelatedEntity != null) {

                                    relatedEntity = entityCorrespondence.RelatedEntity.Name;

                                    switch (entityCorrespondence.RelatedEntity.EntityType) {

                                        case Mercury.Server.Application.EntityType.Member:

                                            relatedEntity = CommonFunctions.MemberProfileAnchor (entityCorrespondence.RelatedEntityId, relatedEntity).Replace ("MemberId=", "EntityId=");

                                            break;

                                        case Mercury.Server.Application.EntityType.Provider:

                                            relatedEntity = CommonFunctions.ProviderProfileAnchor (entityCorrespondence.RelatedEntityId, relatedEntity).Replace ("ProviderId=", "EntityId=");

                                            break;

                                    }

                                }

                            }                            

                            dataTable.Rows.Add (

                                detail,

                                currentDocument.DocumentType,

                                currentDocument.EntityDocumentId.ToString (),

                                currentDocument.DocumentId.ToString (),

                                currentDocument.EntityId.ToString (),

                                (currentDocument.DocumentType == "Form") ? CommonFunctions.FormAnchor (currentDocument.EntityDocumentId, currentDocument.Name) : currentDocument.Name,

                                String.Format ("{0:0.00######}", currentDocument.Version),

                                ((currentDocument.ContactType  != Mercury.Server.Application.EntityContactType.NotSpecified) ? Mercury.Server.CommonFunctions.EnumerationToString  (currentDocument.ContactType) : String.Empty),

                                (currentDocument.ReadyToSendDate.HasValue) ? currentDocument.ReadyToSendDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                                markSent,

                                markReceived,

                                markReturned,

                                relatedEntity,

                                automation,

                                currentDocument.CreateAccountInfo.ActionDate.ToString (),

                                currentDocument.CreateAccountInfo.UserAccountName,

                                currentDocument.ModifiedAccountInfo.ActionDate.ToString (),

                                currentDocument.ModifiedAccountInfo.UserAccountName

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


            EntityDocumentHistoryGrid_DataTable = dataTable;

            EntityDocumentHistoryGrid.DataSource = EntityDocumentHistoryGrid_DataTable;

            return;

        }

        protected void EntityDocumentHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (EntityDocumentHistoryGrid_PageSize != eventArgs.NewPageSize) {

                EntityDocumentHistoryGrid_PageSize = eventArgs.NewPageSize;

                EntityDocumentHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        protected void EntityDocumentHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Telerik.Web.UI.RadToolBar EnterFormToolbar;

            String hrefLocation = String.Empty;

            switch (eventArgs.CommandName) {

                case "SendCorrespondence":

                    hrefLocation = "/Application/Actions/SendCorrespondence.aspx?EntityId=" + Entity.Id.ToString ();

                    Response.Redirect (hrefLocation, true);

                    break;

                case "EnterReceivedForm":

                    EnterFormToolbar = (Telerik.Web.UI.RadToolBar) eventArgs.Item.FindControl ("EntityDocumentToolbar");

                    if (EnterFormToolbar != null) {

                        Telerik.Web.UI.RadToolBarItem toolbarItem = EnterFormToolbar.Items.FindItemByValue ("EnterReceivedFormToolbarButton");

                        Telerik.Web.UI.RadComboBox FormSelection = (Telerik.Web.UI.RadComboBox) toolbarItem.FindControl ("FormSelection");

                        if (!String.IsNullOrEmpty (FormSelection.SelectedValue)) {

                            hrefLocation = "/Application/Forms/FormDataEntry/FormDataEntry.aspx?EntityCorrespondenceId=0&FormId=" + FormSelection.SelectedValue + "&EntityId=" + Entity.Id.ToString ();

                            Response.Redirect (hrefLocation, true);

                        }

                    }

                    break;

                default:

                    System.Diagnostics.Debug.WriteLine ("MemberMetricsGrid_OnItemCommand: " + eventArgs.CommandSource + " " + eventArgs.CommandName + " (" + eventArgs.CommandArgument + ")");

                    break;


            }

            return;

        }

        public void EntityDocumentHistoryGrid_ManualDataRebind () {

            EntityDocumentHistoryGrid_Count = 0;

            EntityDocumentHistoryGrid.DataSource = null;

            EntityDocumentHistoryGrid.Rebind ();

            return;

        }

        #endregion


        #region 

        public void EntityDocumentHistoryAction_OnClick (Object sender, EventArgs eventArgs) {

            DateTime parsedDate;

            Int64 entityCorrespondenceId;

            Client.Core.Entity.EntityCorrespondence entityCorrespondence;

            switch (EntityDocumentHistory_CommandName.Text) {

                case "MarkSent":

                    if (Int64.TryParse (EntityDocumentHistory_Arguments.Text.Split ('|')[0], out entityCorrespondenceId)) {

                        if (DateTime.TryParse (EntityDocumentHistory_Arguments.Text.Split ('|')[1], out parsedDate)) {

                            entityCorrespondence = MercuryApplication.EntityCorrespondenceGet (entityCorrespondenceId, false);

                            if (entityCorrespondence != null) {

                                entityCorrespondence.SentDate = parsedDate;

                                MercuryApplication.EntityCorrespondenceSave (entityCorrespondence);

                                EntityDocumentHistoryGrid_ManualDataRebind ();

                            }

                        }

                    }

                    break;


                case "MarkReturned":

                    if (Int64.TryParse (EntityDocumentHistory_Arguments.Text.Split ('|')[0], out entityCorrespondenceId)) {

                        if (DateTime.TryParse (EntityDocumentHistory_Arguments.Text.Split ('|')[1], out parsedDate)) {

                            entityCorrespondence = MercuryApplication.EntityCorrespondenceGet (entityCorrespondenceId, false);

                            if (entityCorrespondence != null) {

                                entityCorrespondence.ReturnedDate = parsedDate;

                                MercuryApplication.EntityCorrespondenceSave (entityCorrespondence);

                                EntityDocumentHistoryGrid_ManualDataRebind ();

                            }

                        }

                    }

                    break;

            }

            return;

        }

        #endregion 

    }

}