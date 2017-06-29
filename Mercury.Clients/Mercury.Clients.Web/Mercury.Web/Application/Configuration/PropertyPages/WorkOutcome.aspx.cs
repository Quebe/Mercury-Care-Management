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

    public partial class WorkOutcome : System.Web.UI.Page {


        #region Private Properties

        private Client.Core.Work.WorkOutcome workOutcome;

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

            Int64 forWorkOutcomeId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeReview)) 

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage)))
            
                { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkOutcomeId"] != null) {

                    forWorkOutcomeId = Int64.Parse (Request.QueryString["WorkOutcomeId"]);

                }

                if (forWorkOutcomeId != 0) {

                    workOutcome = MercuryApplication.WorkOutcomeGet (forWorkOutcomeId, false);

                    if (workOutcome == null) {

                        workOutcome = new Mercury.Client.Core.Work.WorkOutcome (MercuryApplication);

                    }

                }

                else {

                    workOutcome = new Mercury.Client.Core.Work.WorkOutcome (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "WorkOutcome"] = workOutcome;

                Session[SessionCachePrefix + "WorkOutcomeUnmodified"] = workOutcome.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                workOutcome = (Mercury.Client.Core.Work.WorkOutcome) Session[SessionCachePrefix + "WorkOutcome"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (workOutcome.Name)) { Page.Title = "Work Outcome - " + workOutcome.Name; } else { Page.Title = "Work Outcome"; }

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

            WorkOutcomeName.Text = workOutcome.Name;

            WorkOutcomeDescription.Text = workOutcome.Description;


            WorkOutcomeEnabled.Checked = workOutcome.Enabled;

            WorkOutcomeVisible.Checked = workOutcome.Visible;


            WorkOutcomeCreateAuthorityName.Text = workOutcome.CreateAccountInfo.SecurityAuthorityName;

            WorkOutcomeCreateAccountId.Text = workOutcome.CreateAccountInfo.UserAccountId;

            WorkOutcomeCreateAccountName.Text = workOutcome.CreateAccountInfo.UserAccountName;

            WorkOutcomeCreateDate.MinDate = DateTime.MinValue;

            WorkOutcomeCreateDate.SelectedDate = workOutcome.CreateAccountInfo.ActionDate;


            WorkOutcomeModifiedAuthorityName.Text = workOutcome.ModifiedAccountInfo.SecurityAuthorityName;

            WorkOutcomeModifiedAccountId.Text = workOutcome.ModifiedAccountInfo.UserAccountId;

            WorkOutcomeModifiedAccountName.Text = workOutcome.ModifiedAccountInfo.UserAccountName;

            WorkOutcomeModifiedDate.MinDate = DateTime.MinValue;

            WorkOutcomeModifiedDate.SelectedDate = workOutcome.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage);

            WorkOutcomeName.ReadOnly = !hasManagePermission;

            WorkOutcomeDescription.ReadOnly = !hasManagePermission;


            WorkOutcomeEnabled.Enabled = hasManagePermission;

            WorkOutcomeVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Core.Work.WorkOutcome workOutcomeUnmodified = (Mercury.Client.Core.Work.WorkOutcome) Session[SessionCachePrefix + "WorkOutcomeUnmodified"];

            if (workOutcomeUnmodified.Id == 0) { isModified = true; }


            workOutcome.Name = WorkOutcomeName.Text.Trim ();

            workOutcome.Description = WorkOutcomeDescription.Text.Trim ();

            workOutcome.Enabled = WorkOutcomeEnabled.Checked;

            workOutcome.Visible = WorkOutcomeVisible.Checked;

            if (!isModified) { isModified = !workOutcome.IsEqual (workOutcomeUnmodified); }

            if (isModified) {

                success = MercuryApplication.WorkOutcomeSave (workOutcome);

                if (success) {

                    workOutcome = MercuryApplication.WorkOutcomeGet (workOutcome.Id, false);

                    Session[SessionCachePrefix + "WorkOutcome"] = workOutcome;

                    Session[SessionCachePrefix + "WorkOutcomeUnmodified"] = workOutcome.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkOutcomeManage)) {

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
