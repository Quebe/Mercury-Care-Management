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

    public partial class CareOutcome : System.Web.UI.Page {


        #region Private Properties

        private Client.Core.Individual.CareOutcome careOutcome;

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

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forCareOutcomeId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["CareOutcomeId"] != null) {

                    forCareOutcomeId = Int64.Parse (Request.QueryString["CareOutcomeId"]);

                }

                if (forCareOutcomeId != 0) {

                    careOutcome = MercuryApplication.CareOutcomeGet (forCareOutcomeId, false);

                    if (careOutcome == null) {

                        careOutcome = new Mercury.Client.Core.Individual.CareOutcome (MercuryApplication);

                    }

                }

                else {

                    careOutcome = new Mercury.Client.Core.Individual.CareOutcome (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "CareOutcome"] = careOutcome;

                Session[SessionCachePrefix + "CareOutcomeUnmodified"] = careOutcome.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                careOutcome = (Mercury.Client.Core.Individual.CareOutcome)Session[SessionCachePrefix + "CareOutcome"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (careOutcome.Name)) { Page.Title = "Care Outcome - " + careOutcome.Name; } else { Page.Title = "Care Outcome"; }

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

            CareOutcomeName.Text = careOutcome.Name;

            CareOutcomeDescription.Text = careOutcome.Description;


            CareOutcomeEnabled.Checked = careOutcome.Enabled;

            CareOutcomeVisible.Checked = careOutcome.Visible;


            CareOutcomeCreateAuthorityName.Text = careOutcome.CreateAccountInfo.SecurityAuthorityName;

            CareOutcomeCreateAccountId.Text = careOutcome.CreateAccountInfo.UserAccountId;

            CareOutcomeCreateAccountName.Text = careOutcome.CreateAccountInfo.UserAccountName;

            CareOutcomeCreateDate.MinDate = DateTime.MinValue;

            CareOutcomeCreateDate.SelectedDate = careOutcome.CreateAccountInfo.ActionDate;


            CareOutcomeModifiedAuthorityName.Text = careOutcome.ModifiedAccountInfo.SecurityAuthorityName;

            CareOutcomeModifiedAccountId.Text = careOutcome.ModifiedAccountInfo.UserAccountId;

            CareOutcomeModifiedAccountName.Text = careOutcome.ModifiedAccountInfo.UserAccountName;

            CareOutcomeModifiedDate.MinDate = DateTime.MinValue;

            CareOutcomeModifiedDate.SelectedDate = careOutcome.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage);

            CareOutcomeName.ReadOnly = !hasManagePermission;

            CareOutcomeDescription.ReadOnly = !hasManagePermission;


            CareOutcomeEnabled.Enabled = hasManagePermission;

            CareOutcomeVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Core.Individual.CareOutcome careOutcomeUnmodified = (Mercury.Client.Core.Individual.CareOutcome)Session[SessionCachePrefix + "CareOutcomeUnmodified"];

            if (careOutcomeUnmodified.Id == 0) { isModified = true; }


            careOutcome.Name = CareOutcomeName.Text.Trim ();

            careOutcome.Description = CareOutcomeDescription.Text.Trim ();

            careOutcome.Enabled = CareOutcomeEnabled.Checked;

            careOutcome.Visible = CareOutcomeVisible.Checked;

            if (!isModified) { isModified = !careOutcome.IsEqual (careOutcomeUnmodified); }

            if (isModified) {

                success = MercuryApplication.CareOutcomeSave (careOutcome);

                if (success) {

                    careOutcome = MercuryApplication.CareOutcomeGet (careOutcome.Id, false);

                    Session[SessionCachePrefix + "CareOutcome"] = careOutcome;

                    Session[SessionCachePrefix + "CareOutcomeUnmodified"] = careOutcome.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareOutcomeManage)) {

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
