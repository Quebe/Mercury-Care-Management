using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class EntityNote : System.Web.UI.UserControl {


        #region Private Properties

        private Client.Core.Entity.EntityNote entityNote = null;

        #endregion


        #region State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + "_";

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

                    entity = value;

                }

            }

        }

        public Int64 EntityId {

            get {

                Int64 entityId = 0;

                if (Session[SessionCachePrefix + "EntityId"] != null) {

                    entityId = (Int64) Session[SessionCachePrefix + "EntityId"];

                }

                else {

                    Int64.TryParse (Request.QueryString["EntityId"], out entityId);

                    Session[SessionCachePrefix + "EntityId"] = entityId;

                }

                return entityId;

            }

        }

        public Int64 EntityNoteId {

            get {

                Int64 entityNoteId = 0;

                if (Session[SessionCachePrefix + "EntityNoteId"] != null) {

                    entityNoteId = (Int64) Session[SessionCachePrefix + "EntityNoteId"];

                }

                else {

                    Int64.TryParse (Request.QueryString["EntityNoteId"], out entityNoteId);

                    Session[SessionCachePrefix + "EntityNoteId"] = entityNoteId;

                }

                return entityNoteId;

            }

        }

        public String Action {

            get {

                String action = "";

                if (Session[SessionCachePrefix + "Action"] != null) {

                    action = (String) Session[SessionCachePrefix + "Action"];

                }

                else {

                    if (Request.QueryString["Action"] != null) {

                        action = (Request.QueryString["Action"]);

                    }

                    Session[SessionCachePrefix + "Action"] = action;

                }

                return action;

            }

        }

        public String SubjectMessage { get { return NoteSubject.Text; } set { NoteSubject.Text = value; } }

        public Boolean AllowEditSubject { get { return NoteSubject.Enabled; } set { NoteSubject.Enabled = value; } }

        public Boolean AllowCancel { get { return ButtonCancel.Enabled; } set { ButtonCancel.Enabled = value; } }

        public Boolean AllowTerminate { get { return NoteTerminationDatePicker.Enabled; } set { NoteTerminationDatePicker.Enabled = value; } }

        public Boolean AllowCustomSubject { get { return NoteSubject.AllowCustomText; } set { NoteSubject.AllowCustomText = value; } }

        public Boolean AllowEditEffectiveDateTime { get { return NoteEffectiveDatePicker.Enabled; } set { NoteEffectiveDatePicker.Enabled = value; } }

        public Boolean AllowEditTerminationDate { get { return NoteTerminationDatePicker.Enabled; } set { NoteTerminationDatePicker.Enabled = value; } }

        public Boolean AllowEditImportance { get { return NoteImportanceSelection.Enabled; } set { NoteImportanceSelection.Enabled = value; } }

        public Boolean AllowAppendContent { get { return ButtonAppend.Visible; } set { ButtonAppend.Visible = value; } }

        //public Boolean AllowAppendContent { get { return AllowOkButton; } set { AllowOkButton = value; } }


        //private Boolean AllowOkButton {

        //    get { return ButtonOk.Enabled; }

        //    set {

        //        if (entityNote == null) {

        //            ButtonOk.Enabled = true;

        //        }

        //        else {

        //            ButtonOk.Enabled = AllowAppendContent;

        //        }

        //    }

        //}


        public Client.Core.Entity.EntityNote EntityNoteInstance { get { return entityNote; } }

        public event EventHandler<EntityNoteCompletedEventArgs> NoteCompleted;

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            ActionResponseLabel.Text = String.Empty;

            NoteSubject.AllowCustomText = true;

            InitializeSubject ();

            InitializeContentGrid ();

            InitializeSecurity ();

            if (NoteImportanceSelection.SelectedValue == "0") {

                NoteImportanceSelection.SelectedValue = "1";

            }

            InitializeAction ();

            if (NoteImportanceSelection.SelectedValue == "1") {

                NoteEffectiveDatePicker.Enabled = false;

                NoteTerminationDatePicker.Enabled = false;

            }

            return;

        }

        #endregion


        #region Initialization

        private Telerik.Web.UI.RadComboBoxItem CreateRadComboBoxItem (String text, String value, Boolean isSelected) {

            Telerik.Web.UI.RadComboBoxItem item = new Telerik.Web.UI.RadComboBoxItem ();

            item = new Telerik.Web.UI.RadComboBoxItem ();

            item.Text = text;

            item.Value = value;

            item.Selected = isSelected;

            return item;

        }

        private void InitializeSubject () {

            foreach (Client.Core.Reference.NoteType currentNoteType in MercuryApplication.NoteTypesAvailable (true)) {

                if (currentNoteType.Enabled && currentNoteType.Visible) {

                    NoteSubject.Items.Add (CreateRadComboBoxItem (currentNoteType.Name, currentNoteType.Id.ToString (), false));

                }

            }

            return;

        }

        private void InitializeContentGrid () {

            System.Data.DataTable dataTable;

            dataTable = new System.Data.DataTable ();

            dataTable.Columns.Add ("EntityNoteContentId");

            dataTable.Columns.Add ("EntityNoteId");

            dataTable.Columns.Add ("Content");

            dataTable.Columns.Add ("CreateDate");

            dataTable.Columns.Add ("CreateAccountName");

            dataTable.Columns.Add ("ModifiedDate");

            dataTable.Columns.Add ("ModifiedAccountName");


            List<Mercury.Server.Application.EntityNoteContent> contents = new List<Server.Application.EntityNoteContent> ();

            if (EntityNoteId != 0) {

                entityNote = new Mercury.Client.Core.Entity.EntityNote (MercuryApplication);

                entityNote = MercuryApplication.EntityNoteGet (EntityNoteId, false);

                if (NoteImportanceSelection.SelectedValue != "0") {

                    NoteImportanceSelection.SelectedValue = Convert.ToString (NoteImportanceSelection.SelectedValue);

                }

                else {

                    NoteImportanceSelection.SelectedValue = Convert.ToString ((Int32) (Mercury.Server.Application.NoteImportance) entityNote.Importance);
                    
                }

                NoteEffectiveDatePicker.SelectedDate = (NoteEffectiveDatePicker.SelectedDate.HasValue) ? NoteEffectiveDatePicker.SelectedDate : entityNote.EffectiveDate;

                if (NoteTerminationDatePicker.SelectedDate.HasValue) {

                    NoteTerminationDatePicker.SelectedDate = NoteTerminationDatePicker.SelectedDate;

                }

                else {

                    if (entityNote.TerminationDate.ToString ("MM/dd/yyyy") != "12/31/9999") {

                        NoteTerminationDatePicker.SelectedDate = entityNote.TerminationDate;

                    }

                }

                //String entityNoteTerminationDate = (entityNote.TerminationDate == Convert.ToDateTime ("12/31/9999")) ? null : Convert.ToString(entityNote.TerminationDate);

                //NoteTerminationDatePicker.SelectedDate = (NoteTerminationDatePicker.SelectedDate.HasValue) ? NoteTerminationDatePicker.SelectedDate : Convert.ToDateTime(entityNoteTerminationDate);

                NoteSubject.Text = (NoteSubject.Text != "") ? NoteSubject.Text : entityNote.Subject;

                NoteImportanceSelection.Enabled = false;

                NoteEffectiveDatePicker.Enabled = false;

                NoteTerminationDatePicker.Enabled = false;

                NoteSubject.Enabled = false;

                contents = MercuryApplication.EntityNoteContentsGet (EntityNoteId, false);

                foreach (Mercury.Server.Application.EntityNoteContent currentContent in contents) {

                    dataTable.Rows.Add (

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

            EntityNoteContentGrid.MasterTableView.DataSource = dataTable;

            EntityNoteContentGrid.MasterTableView.DataBind ();

            return;

        }

        private void InitializeSecurity () {

            if (EntityNoteId == 0) {

                if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberNoteAdd)) {

                    ButtonOk.Enabled = true;

                    ButtonAppend.Enabled = false;

                    ButtonAppend.Visible = false;

                }

                else {

                    ButtonOk.Enabled = false;

                    ButtonAppend.Enabled = false;

                    ButtonAppend.Visible = false;

                }

            }

            else {

                if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberNoteAppend)) {

                    ButtonOk.Enabled = false;

                    ButtonAppend.Enabled = true;

                    ButtonAppend.Visible = true;

                }

                else {

                    ButtonOk.Enabled = false;

                    ButtonAppend.Enabled = false;

                }

                if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberNoteModify)) {

                    ButtonAppend.Enabled = false;

                    ButtonAppend.Visible = false;

                    ButtonOk.Enabled = true;

                    NoteImportanceSelection.Enabled = true;

                    NoteSubject.Enabled = true;

                    if (NoteImportanceSelection.SelectedValue != "1") {

                        NoteEffectiveDatePicker.Enabled = true;

                        NoteTerminationDatePicker.Enabled = true;

                    }

                }

            }

            // IF USER HAS MEMBER NOTE TERMINATE PERMISSION, NOTE TERMINATION DATE PICKER IS ENABLED, ELSE IT IS DISABLED

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberNoteTerminate)) {

                NoteTerminationDatePicker.Enabled = true;

            }

            else {

                NoteTerminationDatePicker.Enabled = false;

                NoteTerminationDatePicker.Visible = false;

            }

            return;

        }


        private void InitializeAction () {

            if (Action == "Append") {

                ButtonAppend.Visible = true;

                ButtonAppend.Enabled = true;

                ButtonOk.Visible = false;

                ButtonOk.Enabled = false;

                NoteImportanceSelection.Enabled = false;

                NoteSubject.Enabled = false;

                NoteTerminationDatePicker.Enabled = false;

                NoteEffectiveDatePicker.Enabled = false;


            }

            if (Action == "Modify") {

                NoteContent.Enabled = false;

                NoteContent.Visible = false;

                ButtonAppend.Enabled = false;

                ButtonAppend.Visible = false;

                ButtonOk.Enabled = true;

                ButtonOk.Visible = true;

            }
        
        }

        #endregion


        #region Control Events

        public void ButtonOk_OnClick (Object sender, EventArgs e) {

            // NOTE SUBJECT IS REQUIRED

            if (String.IsNullOrEmpty (NoteSubject.Text.Trim())) {

                ActionResponseLabel.Text = "** Note Subject is required.";

                ActionResponseLabel.Visible = true;

                return;

            }

            // EFFECTIVE DATE IS REQUIRED WHEN IMPORATANCE IS NOT INFORMATIONAL

            if ((NoteImportanceSelection.SelectedValue != "1") && (!NoteEffectiveDatePicker.SelectedDate.HasValue)) {

                ActionResponseLabel.Text = "** Effective Date is required when importance is '" + NoteImportanceSelection.SelectedItem.Text.ToString () + "'.";

                ActionResponseLabel.Visible = true;

                return;

            }

            if (NoteTerminationDatePicker.SelectedDate.HasValue && NoteEffectiveDatePicker.SelectedDate.HasValue) {

                if (NoteTerminationDatePicker.SelectedDate < NoteEffectiveDatePicker.SelectedDate) {

                    ActionResponseLabel.Text = "** Termination Date cannot be prior to the Effective Date.";

                    ActionResponseLabel.Visible = true;

                    return;

                }

            }

            if (EntityNoteId == 0) {

                #region If New Note

                if (String.IsNullOrEmpty (NoteContent.Text.Trim ())) {

                    ActionResponseLabel.Text = "** Note must contain Content (blank notes cannot be saved).";

                    ActionResponseLabel.Visible = true;

                    return;

                }

                ActionResponseLabel.Text = String.Empty;

                ActionResponseLabel.Visible = false;

                // IF NEW ENTITY NOTE

                entityNote = new Mercury.Client.Core.Entity.EntityNote (MercuryApplication);

                entityNote.EntityId = Entity.Id;

                entityNote.Importance = (Mercury.Server.Application.NoteImportance) Convert.ToInt32 (NoteImportanceSelection.SelectedValue);

                entityNote.Subject = NoteSubject.Text.Trim();

                entityNote.NoteTypeId = (NoteSubject.SelectedValue == "") ? 0 : Convert.ToInt64 (NoteSubject.SelectedValue);


                // IF NOTE IMPORTANCE IS INFORMATIONAL, THEN EFFECTIVE AND TERMINATION IS THE CURRENT DATE

                if (NoteImportanceSelection.SelectedValue == "1") {

                    entityNote.EffectiveDate = DateTime.Now;

                    entityNote.TerminationDate = DateTime.Now;

                }

                else {

                    // IF NOTE IMPORTANCE IS OTHER THAN INFORMATIONAL, SET EFFECTIVE DATE AS INDICATED (IF NOT INDICATED SET TO CURRENT DATE)

                    entityNote.EffectiveDate = (NoteEffectiveDatePicker.SelectedDate.HasValue) ? NoteEffectiveDatePicker.SelectedDate.Value : DateTime.Now;

                    // IF NOTE IMPORTANCE IS OTHER THAN INFORMATIONAL, SET TERMINATION DATE AS INDICATED, IF NOT INDICATED SET TO 12/31/2099

                    entityNote.TerminationDate = (NoteTerminationDatePicker.SelectedDate.HasValue) ? NoteTerminationDatePicker.SelectedDate.Value : Convert.ToDateTime ("12/31/9999");

                }

                Mercury.Server.Application.EntityNoteContent entityNoteContent = new Mercury.Server.Application.EntityNoteContent ();

                entityNoteContent.Content = NoteContent.Text;

                entityNoteContent.EntityNoteId = entityNote.Id;

                entityNote.Contents.Add (entityNoteContent);

                if (NoteCompleted != null) {

                    NoteCompleted (this, new EntityNoteCompletedEventArgs (entityNote));

                }

            }

                #endregion

            // IF EXISTING ENTITY NOTE

            else {

                #region If Existing Note

                entityNote = MercuryApplication.EntityNoteGet (EntityNoteId, false);

                entityNote.Subject = NoteSubject.Text.Trim();

                entityNote.Importance = (Mercury.Server.Application.NoteImportance) Convert.ToInt32(NoteImportanceSelection.SelectedValue);

                if (NoteImportanceSelection.SelectedValue == "1") {

                    entityNote.EffectiveDate = DateTime.Now;

                    entityNote.TerminationDate = DateTime.Now;

                }

                else {

                    // IF NOTE IMPORTANCE IS OTHER THAN INFORMATIONAL, SET EFFECTIVE DATE AS INDICATED (IF NOT INDICATED SET TO CURRENT DATE)

                    entityNote.EffectiveDate = (NoteEffectiveDatePicker.SelectedDate.HasValue) ? NoteEffectiveDatePicker.SelectedDate.Value : DateTime.Now;

                    // IF NOTE IMPORTANCE IS OTHER THAN INFORMATIONAL, SET TERMINATION DATE AS INDICATED, IF NOT INDICATED SET TO 12/31/9999

                    entityNote.TerminationDate = (NoteTerminationDatePicker.SelectedDate.HasValue) ? NoteTerminationDatePicker.SelectedDate.Value : Convert.ToDateTime ("12/31/9999");

                }

                Mercury.Server.Application.EntityNoteContent entityNoteContent = new Mercury.Server.Application.EntityNoteContent ();

                entityNoteContent.Content = NoteContent.Text.Trim();

                entityNoteContent.EntityNoteId = entityNote.Id;

                entityNote.Contents.Add (entityNoteContent);

                if (entityNote != null) {

                    NoteCompleted (this, new EntityNoteCompletedEventArgs (entityNote));

                }

                #endregion

            }

            return;

        }

        public void ButtonCancel_OnClick (Object sender, EventArgs e) {

            if (NoteCompleted != null) {

                NoteCompleted (this, new EntityNoteCompletedEventArgs ());

            }

            return;

        }

        public void ButtonAppend_OnClick (Object sender, EventArgs e) {

            // VALIDATE CONTENT 

            if (String.IsNullOrEmpty (NoteContent.Text.Trim ())) {

                ActionResponseLabel.Text = "** Note must contain Content (blank notes cannot be saved).";

                ActionResponseLabel.Visible = true;

                return;

            }


            entityNote = MercuryApplication.EntityNoteGet (EntityNoteId, false);

            if (NoteCompleted != null) {

                NoteCompleted (this, new EntityNoteCompletedEventArgs (entityNote, NoteContent.Text.Trim ()));

            }

            return;

        }

        #endregion

    }


    public class EntityNoteCompletedEventArgs : EventArgs {

        #region Private Properties

        private Client.Core.Entity.EntityNote entityNote = null;

        private Boolean cancel = false;

        private Boolean appendOnly = false;

        private String appendContent = String.Empty;

        #endregion 


        #region Public Properties

        public Client.Core.Entity.EntityNote EntityNote { get { return entityNote; } }

        public Boolean Cancel { get { return cancel; } }

        public Boolean AppendOnly { get { return appendOnly; } }

        public String AppendContent { get { return appendContent; } }

        #endregion


        #region Constructors

        public EntityNoteCompletedEventArgs (Client.Core.Entity.EntityNote forEntityNote) { entityNote = forEntityNote; return; }

        public EntityNoteCompletedEventArgs () { cancel = true; return; }

        public EntityNoteCompletedEventArgs (Client.Core.Entity.EntityNote forEntityNote, String forAppendContent) {

            appendOnly = true;

            entityNote = forEntityNote;

            appendContent = forAppendContent;

            return;

        }

        #endregion

    }

}