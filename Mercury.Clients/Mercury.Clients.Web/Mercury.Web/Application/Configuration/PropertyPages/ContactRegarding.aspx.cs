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

    public partial class ContactRegarding : System.Web.UI.Page {


        #region Private Properties

        private Client.Core.Reference.ContactRegarding contactRegarding;

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

            Int64 forContactRegardingId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["ContactRegardingId"] != null) {

                    forContactRegardingId = Int64.Parse (Request.QueryString["ContactRegardingId"]);

                }

                if (forContactRegardingId != 0) {

                    contactRegarding = MercuryApplication.ContactRegardingGet (forContactRegardingId, false);

                    if (contactRegarding == null) {

                        contactRegarding = new Mercury.Client.Core.Reference.ContactRegarding (MercuryApplication);

                    }

                }

                else {

                    contactRegarding = new Mercury.Client.Core.Reference.ContactRegarding (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "ContactRegarding"] = contactRegarding;

                Session[SessionCachePrefix + "ContactRegardingUnmodified"] = contactRegarding.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                contactRegarding = (Mercury.Client.Core.Reference.ContactRegarding)Session[SessionCachePrefix + "ContactRegarding"];

            }

            ApplySecurity ();
            
            if (!String.IsNullOrEmpty (contactRegarding.Name)) { Page.Title = "Contact Regarding - " + contactRegarding.Name; } else { Page.Title = "Contact Regarding"; }

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
            
            ContactRegardingName.Text = contactRegarding.Name;

            ContactRegardingDescription.Text = contactRegarding.Description;


            ContactRegardingEnabled.Checked = contactRegarding.Enabled;

            ContactRegardingVisible.Checked = contactRegarding.Visible;


            ContactRegardingCreateAuthorityName.Text = contactRegarding.CreateAccountInfo.SecurityAuthorityName;

            ContactRegardingCreateAccountId.Text = contactRegarding.CreateAccountInfo.UserAccountId;

            ContactRegardingCreateAccountName.Text = contactRegarding.CreateAccountInfo.UserAccountName;

            ContactRegardingCreateDate.MinDate = DateTime.MinValue;

            ContactRegardingCreateDate.SelectedDate = contactRegarding.CreateAccountInfo.ActionDate;


            ContactRegardingModifiedAuthorityName.Text = contactRegarding.ModifiedAccountInfo.SecurityAuthorityName;

            ContactRegardingModifiedAccountId.Text = contactRegarding.ModifiedAccountInfo.UserAccountId;

            ContactRegardingModifiedAccountName.Text = contactRegarding.ModifiedAccountInfo.UserAccountName;

            ContactRegardingModifiedDate.MinDate = DateTime.MinValue;

            ContactRegardingModifiedDate.SelectedDate = contactRegarding.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage);

            ContactRegardingName.ReadOnly = !hasManagePermission;

            ContactRegardingDescription.ReadOnly = !hasManagePermission;


            ContactRegardingEnabled.Enabled = hasManagePermission;

            ContactRegardingVisible.Enabled = hasManagePermission;


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


            Mercury.Client.Core.Reference.ContactRegarding contactRegardingUnmodified = (Mercury.Client.Core.Reference.ContactRegarding)Session[SessionCachePrefix + "ContactRegardingUnmodified"];

            if (contactRegardingUnmodified.Id == 0) { isModified = true; }


            contactRegarding.Name = ContactRegardingName.Text.Trim ();

            contactRegarding.Description = ContactRegardingDescription.Text.Trim ();

            contactRegarding.Enabled = ContactRegardingEnabled.Checked;

            contactRegarding.Visible = ContactRegardingVisible.Checked;

            if (!isModified) { isModified = !contactRegarding.IsEqual (contactRegardingUnmodified); }


            validationResponse = contactRegarding.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                success = MercuryApplication.ContactRegardingSave (contactRegarding);

                if (success) {

                    contactRegarding = MercuryApplication.ContactRegardingGet (contactRegarding.Id, false);

                    Session[SessionCachePrefix + "ContactRegarding"] = contactRegarding;

                    Session[SessionCachePrefix + "ContactRegardingUnmodified"] = contactRegarding.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ContactRegardingManage)) {

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
