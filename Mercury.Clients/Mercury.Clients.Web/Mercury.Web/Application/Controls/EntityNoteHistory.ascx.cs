using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityNoteHistory : System.Web.UI.UserControl {

        #region Private Properties

        private Boolean pageSizeChanged = false;

        #endregion


        #region Public Properties

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

        public Client.Core.Entity.Entity Entity { 
            
            get { return (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"]; } 
            
            set {

                if (Session[SessionCachePrefix + "Entity"] != value) {

                    Session[SessionCachePrefix + "Entity"] = value;

                    InitializeEntityNoteHistoryGrid ();

                    EntityNoteHistoryGrid_ManualDataRebind ();

                }
            
            }

        }

        private System.Data.DataTable EntityNoteHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "EntityNoteHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("NoteTypeImage");

                    dataTable.Columns.Add ("EntityNoteId");

                    dataTable.Columns.Add ("EntityId");

                    dataTable.Columns.Add ("RelatedEntityType");

                    dataTable.Columns.Add ("RelatedEntityObjectId");

                    dataTable.Columns.Add ("RelatedObjectType");

                    dataTable.Columns.Add ("RelatedObjectId");

                    dataTable.Columns.Add ("DataSource");

                    dataTable.Columns.Add ("Importance");

                    dataTable.Columns.Add ("NoteType");

                    dataTable.Columns.Add ("Subject");

                    dataTable.Columns.Add ("Content");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("CreateDate");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("ModifiedDate");

                    dataTable.Columns.Add ("ModifiedAccountName");

                    Session[SessionCachePrefix + "EntityNoteHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityNoteHistoryGrid_DataTable"] = value; }

        }

        private System.Data.DataTable EntityNoteHistoryGrid_Content_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "EntityNoteHistoryGrid_Content_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("EntityNoteContentId");

                    dataTable.Columns.Add ("EntityNoteId");

                    dataTable.Columns.Add ("Content");

                    dataTable.Columns.Add ("CreateDate");

                    dataTable.Columns.Add ("CreateAccountName");

                    dataTable.Columns.Add ("ModifiedDate");

                    dataTable.Columns.Add ("ModifiedAccountName");

                    Session[SessionCachePrefix + "EntityNoteHistoryGrid_Content_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "EntityNoteHistoryGrid_Content_DataTable"] = value; }

        }

        private Int32 EntityNoteHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "EntityNoteHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "EntityNoteHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "EntityNoteHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 EntityNoteHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32) Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "EntityNoteHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }
        }

        private Int32 EntityNoteHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "EntityNoteHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "EntityNoteHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "EntityNoteHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { get { return EntityNoteHistoryGrid.Height; } set { EntityNoteHistoryGrid.Height = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        public Boolean AllowUserInteraction { 
            
            get { 
                
                return ((Telerik.Web.UI.GridBoundColumn) EntityNoteHistoryGrid.Columns.FindByUniqueName ("Action")).Visible; 
            
            } 
            
            set { 
                
                ((Telerik.Web.UI.GridBoundColumn) EntityNoteHistoryGrid.Columns.FindByUniqueName ("Action")).Visible = value; 
            
            } 
        
        }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }

        protected void InitializeEntityNoteHistoryGrid () {

            EntityNoteHistoryGrid.CurrentPageIndex = 0;

            EntityNoteHistoryGrid.PageSize = EntityNoteHistoryGrid_PageSize;

            EntityNoteHistoryGrid_Count = 0;

            EntityNoteHistoryGrid.DataSource = EntityNoteHistoryGrid_DataTable;

            EntityNoteHistoryGrid.DataBind ();

            return;

        }

        #endregion


        #region Entity Note History Grid Events

        protected void EntityNoteHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = EntityNoteHistoryGrid_DataTable;

            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.InitialLoad:

                    #region Initialize Grid

                    EntityNoteHistoryGrid_Count = 0;

                    EntityNoteHistoryGrid_CurrentPage = 0;

                    EntityNoteHistoryGrid_PageSize = 10;


                    EntityNoteHistoryGrid.CurrentPageIndex = EntityNoteHistoryGrid_CurrentPage;

                    EntityNoteHistoryGrid.PageSize = EntityNoteHistoryGrid_PageSize;

                    EntityNoteHistoryGrid.VirtualItemCount = EntityNoteHistoryGrid_Count;

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:

                    #region Restore Grid State

                    if (EntityNoteHistoryGrid_CurrentPage >= 0) {

                        EntityNoteHistoryGrid.CurrentPageIndex = EntityNoteHistoryGrid_CurrentPage;

                    }

                    EntityNoteHistoryGrid.PageSize = EntityNoteHistoryGrid_PageSize;

                    EntityNoteHistoryGrid.VirtualItemCount = EntityNoteHistoryGrid_Count;

                    // RESTORE TOOLBAR STATE HERE

                    #endregion

                    break;

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:

                    #region Rebind Grid

                    if (Entity == null) { dataTable.Rows.Clear (); }

                    else {

                        if (EntityNoteHistoryGrid_Count == 0) {

                            EntityNoteHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.EntityNotesGetCount (Entity.Id));

                            EntityNoteHistoryGrid.VirtualItemCount = Convert.ToInt32 (EntityNoteHistoryGrid_Count);

                        }

                        if (!pageSizeChanged) {

                            EntityNoteHistoryGrid_PageSize = EntityNoteHistoryGrid.PageSize;

                        }

                        else {

                            EntityNoteHistoryGrid.PageSize = EntityNoteHistoryGrid_PageSize;

                            pageSizeChanged = false;

                        }


                        EntityNoteHistoryGrid_CurrentPage = EntityNoteHistoryGrid.CurrentPageIndex;

                        dataTable.Rows.Clear ();

                        Int32 initialRow = EntityNoteHistoryGrid.CurrentPageIndex * EntityNoteHistoryGrid.PageSize + 1;

                        List<Client.Core.Entity.EntityNote> entityNotes = MercuryApplication.EntityNotesGetByPage (Entity.Id, initialRow, EntityNoteHistoryGrid.PageSize);

                        //entityNoteList = new Dictionary<long, Mercury.Client.Core.EntityNote> ();

                        foreach (Client.Core.Entity.EntityNote currentNote in entityNotes) {

                            //entityNoteList.Add (currentNote.EntityNoteId, currentNote);

                            String noteTypeImage = "<img src=\"/Images/Common16/" + currentNote.Importance.ToString () + ".png\" />";

                            String terminationDateString = (currentNote.TerminationDate == new DateTime (9999, 12, 31)) ? "< active >" : currentNote.TerminationDate.ToString ("MM/dd/yyyy");


                            dataTable.Rows.Add (

                                noteTypeImage,

                                currentNote.Id.ToString (),

                                currentNote.EntityId.ToString (),

                                currentNote.RelatedEntityType.ToString (),

                                currentNote.RelatedEntityObjectId.ToString (),

                                currentNote.RelatedObjectType,

                                currentNote.RelatedObjectId.ToString (),

                                currentNote.DataSource,

                                currentNote.Importance.ToString (),

                                currentNote.NoteTypeName,

                                currentNote.Subject,

                                String.Empty,

                                currentNote.EffectiveDate.ToString ("MM/dd/yyyy"),

                                terminationDateString,


                                currentNote.CreateAccountInfo.ActionDate.ToString (),

                                currentNote.CreateAccountInfo.UserAccountName,

                                currentNote.ModifiedAccountInfo.ActionDate.ToString (),

                                currentNote.ModifiedAccountInfo.UserAccountName

                            );

                        }

                    }

                    break;

                    #endregion

                default:

                    System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                    break;

            }


            EntityNoteHistoryGrid_DataTable = dataTable;

            EntityNoteHistoryGrid.DataSource = EntityNoteHistoryGrid_DataTable;

            EntityNoteHistoryGrid.MasterTableView.DetailTables[0].DataSource = EntityNoteHistoryGrid_Content_DataTable;

            return;

        }

        protected void EntityNoteHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if ((eventArgs.CommandName == "ExpandCollapse")) {

                EntityNoteHistoryGrid_Content_DataTable.Rows.Clear ();

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;
                
                Int64 entityNoteId;

                List<Mercury.Server.Application.EntityNoteContent> contents = new List<Mercury.Server.Application.EntityNoteContent> ();

                if (Int64.TryParse (gridItem["EntityNoteId"].Text, out entityNoteId)) {

                    contents = MercuryApplication.EntityNoteContentsGet (entityNoteId, false);

                    foreach (Server.Application.EntityNoteContent currentContent in contents) {

                        EntityNoteHistoryGrid_Content_DataTable.Rows.Add (

                            currentContent.Id,

                            currentContent.EntityNoteId,

                            currentContent.Content,

                            currentContent.CreateAccountInfo.ActionDate.ToString ("MM/dd/yyyy"),

                            currentContent.CreateAccountInfo.UserAccountName,

                            currentContent.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy"),

                            currentContent.ModifiedAccountInfo.UserAccountName

                            );

                    }
                    
                }

                EntityNoteHistoryGrid.MasterTableView.DetailTables[0].DataSource = EntityNoteHistoryGrid_Content_DataTable;

                EntityNoteHistoryGrid.MasterTableView.DetailTables[0].DataBind ();

            }

            return;

        }

        protected void EntityNoteHistoryGrid_OnItemDataBound (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            #region Bind Action Column

            String anchorText = String.Empty;

            String parameterString = String.Empty;

            if (eventArgs.Item is Telerik.Web.UI.GridDataItem) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                if (gridItem.OwnerTableView.Name != "EntityNoteHistoryMasterView") { return; }

                if (gridItem["DataSource"].Text != "MERCURY") { return; }

                // ADD APPEND BUTTON

                if (MercuryApplication.HasEnvironmentPermission ("Environment." + Entity.EntityType.ToString() + ".NoteAppend")) {

                    parameterString = String.Empty;

                    parameterString = parameterString + "/Application/Actions/EntityNote.aspx?";

                    parameterString = parameterString + "EntityId=" + gridItem["EntityId"].Text;

                    parameterString = parameterString + "&EntityNoteId=" + gridItem["EntityNoteId"].Text;

                    parameterString = parameterString + "&Action=" + "Append";

                    anchorText = anchorText + " <a href=\"" + parameterString + "\">(append)</a>";

                }

                // ADD MODIFY BUTTON

                if (MercuryApplication.HasEnvironmentPermission ("Environment." + Entity.EntityType.ToString() + ".NoteModify")) {

                    parameterString = String.Empty;

                    parameterString = parameterString + "/Application/Actions/EntityNote.aspx?";

                    parameterString = parameterString + "EntityId=" + gridItem["EntityId"].Text;

                    parameterString = parameterString + "&EntityNoteId=" + gridItem["EntityNoteId"].Text;

                    parameterString = parameterString + "&Action=" + "Modify";

                    anchorText = anchorText + " <a href=\"" + parameterString + "\">(modify)</a>";

                }

                //// ADD TERMINATE BUTTON
                
                //if (((entityNote.EntityNoteId != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Terminate"))) 

                //    && ((entityNote.EntityNoteId != 0) && (!application.HasEnvironmentPermission ("Environment." + entityNote.Entity.EntityType.ToString () + ".Note.Modify")))) {

                if ((MercuryApplication.HasEnvironmentPermission ("Environment." + Entity.EntityType.ToString () + ".NoteTerminate")) && (gridItem["TerminationDate"].Text == "< active >")) {

                    parameterString = String.Empty;

                    parameterString = parameterString + "'" + gridItem["EntityNoteId"].Text.ToString() + "'" + ", ";

                    parameterString = parameterString + "'" + gridItem["Subject"].Text.Replace("'", @"'\") + "'" + ", ";

                    parameterString = parameterString + "'" + gridItem["EffectiveDate"].Text + "'" + ",";

                    parameterString = parameterString + "'0||'";

                    anchorText = anchorText + " <a href=\"javascript:EntityNoteTerminate (" + parameterString + ");" + "\">(terminate)</a>";

                }

                gridItem["Action"].Text = anchorText;

            }

            #endregion

        }

        protected void EntityNoteHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (EntityNoteHistoryGrid_PageSize != eventArgs.NewPageSize) {

                EntityNoteHistoryGrid_PageSize = eventArgs.NewPageSize;

                EntityNoteHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        protected void TerminationDialogueButtonSet_OnClick (Object sender, EventArgs eventArgs) {

            if (!TerminationDialogueSetNoteTerminationDate.SelectedDate.HasValue) { 
                
                TerminationDialogueSetNoteTerminationDate.SelectedDate = null; 
                
                return; 
            
            }

            long terminatedEntityNoteId = Convert.ToInt64 (TerminationDialogueTerminatedEntityNoteId.Text);

            Client.Core.Entity.EntityNote currentEntityNote = MercuryApplication.EntityNoteGet (terminatedEntityNoteId, false);

            if (Convert.ToDateTime (TerminationDialogueSetNoteTerminationDate.SelectedDate) < currentEntityNote.EffectiveDate) { 
                
                TerminationDialogueSetNoteTerminationDate.SelectedDate = null; 
                
                return; 
            
            }

            DateTime newTerminationDate = Convert.ToDateTime (TerminationDialogueSetNoteTerminationDate.SelectedDate);

            MercuryApplication.EntityNoteTerminate (currentEntityNote, newTerminationDate);

            EntityNoteHistoryGrid_ManualDataRebind ();

            return;

        }

        protected void TerminationDialogueButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            TerminationDialogueSetNoteTerminationDate.SelectedDate = null;

            return;

        }

        public void EntityNoteHistoryGrid_ManualDataRebind () {

            EntityNoteHistoryGrid_Count = 0;

            EntityNoteHistoryGrid.DataSource = null;

            EntityNoteHistoryGrid.Rebind ();

            return;

        }


        #endregion

    }

}