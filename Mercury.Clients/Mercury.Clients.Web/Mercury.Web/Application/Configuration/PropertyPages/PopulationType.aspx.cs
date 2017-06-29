using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class PopulationType : System.Web.UI.Page {

        #region Private Properties

        private Client.Core.Population.PopulationType populationType;

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

            Int64 forPopulationTypeId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["PopulationTypeId"] != null) {

                    forPopulationTypeId = Int64.Parse (Request.QueryString["PopulationTypeId"]);

                }

                if (forPopulationTypeId != 0) {

                    populationType = MercuryApplication.PopulationTypeGet (forPopulationTypeId, false);

                    if (populationType == null) {

                        populationType = new Mercury.Client.Core.Population.PopulationType (MercuryApplication);

                    }

                }

                else {

                    populationType = new Mercury.Client.Core.Population.PopulationType (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "PopulationType"] = populationType;

                Session[SessionCachePrefix + "PopulationTypeUnmodified"] = populationType.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                populationType = (Mercury.Client.Core.Population.PopulationType)Session[SessionCachePrefix + "PopulationType"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (populationType.Name)) { Page.Title = "Population Type - " + populationType.Name; } else { Page.Title = "Population Type"; }

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

            PopulationTypeName.Text = populationType.Name;

            PopulationTypeDescription.Text = populationType.Description;


            PopulationTypeEnabled.Checked = populationType.Enabled;

            PopulationTypeVisible.Checked = populationType.Visible;


            PopulationTypeCreateAuthorityName.Text = populationType.CreateAccountInfo.SecurityAuthorityName;

            PopulationTypeCreateAccountId.Text = populationType.CreateAccountInfo.UserAccountId;

            PopulationTypeCreateAccountName.Text = populationType.CreateAccountInfo.UserAccountName;

            PopulationTypeCreateDate.MinDate = DateTime.MinValue;

            PopulationTypeCreateDate.SelectedDate = populationType.CreateAccountInfo.ActionDate;


            PopulationTypeModifiedAuthorityName.Text = populationType.ModifiedAccountInfo.SecurityAuthorityName;

            PopulationTypeModifiedAccountId.Text = populationType.ModifiedAccountInfo.UserAccountId;

            PopulationTypeModifiedAccountName.Text = populationType.ModifiedAccountInfo.UserAccountName;

            PopulationTypeModifiedDate.MinDate = DateTime.MinValue;

            PopulationTypeModifiedDate.SelectedDate = populationType.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage);

            PopulationTypeName.ReadOnly = !hasManagePermission;

            PopulationTypeDescription.ReadOnly = !hasManagePermission;


            PopulationTypeEnabled.Enabled = hasManagePermission;

            PopulationTypeVisible.Enabled = hasManagePermission;


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



            Mercury.Client.Core.Population.PopulationType populationTypeUnmodified = (Mercury.Client.Core.Population.PopulationType)Session[SessionCachePrefix + "PopulationTypeUnmodified"];

            if (populationTypeUnmodified.Id == 0) { isModified = true; }


            populationType.Name = PopulationTypeName.Text.Trim ();

            populationType.Description = PopulationTypeDescription.Text.Trim ();

            populationType.Enabled = PopulationTypeEnabled.Checked;

            populationType.Visible = PopulationTypeVisible.Checked;

            if (!isModified) { isModified = !populationType.IsEqual (populationTypeUnmodified); }


            validationResponse = populationType.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                success = MercuryApplication.PopulationTypeSave (populationType);

                if (success) {

                    populationType = MercuryApplication.PopulationTypeGet (populationType.Id, false);

                    Session[SessionCachePrefix + "PopulationType"] = populationType;

                    Session[SessionCachePrefix + "PopulationTypeUnmodified"] = populationType.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PopulationTypeManage)) {

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