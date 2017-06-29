using System;
using System.Collections;
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

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class NoteType : System.Web.UI.Page {

        #region Private Properties

        private Client.Core.Reference.NoteType noteType;

        #endregion


        #region Public Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forNoteTypeId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["NoteTypeId"] != null) {

                    forNoteTypeId = Int64.Parse (Request.QueryString["NoteTypeId"]);

                }

                if (forNoteTypeId != 0) {

                    noteType = MercuryApplication.NoteTypeGet (forNoteTypeId, false);

                    if (noteType == null) {

                        noteType = new Mercury.Client.Core.Reference.NoteType (MercuryApplication);

                    }

                }

                else {

                    noteType = new Mercury.Client.Core.Reference.NoteType (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "NoteType"] = noteType;

                Session[SessionCachePrefix + "NoteTypeUnmodified"] = noteType.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                noteType = (Mercury.Client.Core.Reference.NoteType) Session[SessionCachePrefix + "NoteType"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (noteType.Name)) { Page.Title = "Note Type - " + noteType.Name; } else { Page.Title = "Note Type"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();


            return;

        }


        protected void InitializeGeneralPage () {

            NoteTypeName.Text = noteType.Name;

            NoteTypeDescription.Text = noteType.Description;


            NoteTypeEnabled.Checked = noteType.Enabled;

            NoteTypeVisible.Checked = noteType.Visible;


            NoteTypeCreateAuthorityName.Text = noteType.CreateAccountInfo.SecurityAuthorityName;

            NoteTypeCreateAccountId.Text = noteType.CreateAccountInfo.UserAccountId;

            NoteTypeCreateAccountName.Text = noteType.CreateAccountInfo.UserAccountName;

            NoteTypeCreateDate.MinDate = DateTime.MinValue;

            NoteTypeCreateDate.SelectedDate = noteType.CreateAccountInfo.ActionDate;


            NoteTypeModifiedAuthorityName.Text = noteType.ModifiedAccountInfo.SecurityAuthorityName;

            NoteTypeModifiedAccountId.Text = noteType.ModifiedAccountInfo.UserAccountId;

            NoteTypeModifiedAccountName.Text = noteType.ModifiedAccountInfo.UserAccountName;

            NoteTypeModifiedDate.MinDate = DateTime.MinValue;

            NoteTypeModifiedDate.SelectedDate = noteType.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage);

            NoteTypeName.ReadOnly = !hasManagePermission;

            NoteTypeDescription.ReadOnly = !hasManagePermission;


            NoteTypeEnabled.Enabled = hasManagePermission;

            NoteTypeVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;

            Boolean isValid = false;

            System.Collections.Generic.Dictionary<String, String> validationResponse;



            Mercury.Client.Core.Reference.NoteType noteTypeUnmodified = (Mercury.Client.Core.Reference.NoteType) Session[SessionCachePrefix + "NoteTypeUnmodified"];

            if (noteTypeUnmodified.Id == 0) { isModified = true; }


            noteType.Name = NoteTypeName.Text.Trim ();

            noteType.Description = NoteTypeDescription.Text.Trim ();

            noteType.Enabled = NoteTypeEnabled.Checked;

            noteType.Visible = NoteTypeVisible.Checked;

            if (!isModified) { isModified = !noteType.IsEqual (noteTypeUnmodified); }


            validationResponse = noteType.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                success = MercuryApplication.NoteTypeSave (noteType);

                if (success) {

                    noteType = MercuryApplication.NoteTypeGet (noteType.Id, false);

                    Session[SessionCachePrefix + "NoteType"] = noteType;

                    Session[SessionCachePrefix + "NoteTypeUnmodified"] = noteType.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }


            else if (!isModified) { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.NoteTypeManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

                Server.Transfer ("/WindowClose.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}
