using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class CareMeasureScale : System.Web.UI.Page {

        #region Private Properties

        private Client.Core.Individual.CareMeasureScale careMeasureScale;

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

            Int64 forCareMeasureScaleId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["CareMeasureScaleId"] != null) {

                    forCareMeasureScaleId = Int64.Parse (Request.QueryString["CareMeasureScaleId"]);

                }

                if (forCareMeasureScaleId != 0) {

                    careMeasureScale = MercuryApplication.CareMeasureScaleGet (forCareMeasureScaleId, false);

                    if (careMeasureScale == null) {

                        careMeasureScale = new Mercury.Client.Core.Individual.CareMeasureScale (MercuryApplication);

                    }

                }

                else {

                    careMeasureScale = new Mercury.Client.Core.Individual.CareMeasureScale (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "CareMeasureScale"] = careMeasureScale;

                Session[SessionCachePrefix + "CareMeasureScaleUnmodified"] = careMeasureScale.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                careMeasureScale = (Mercury.Client.Core.Individual.CareMeasureScale)Session[SessionCachePrefix + "CareMeasureScale"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (careMeasureScale.Name)) { Page.Title = "Care Measure Scale - " + careMeasureScale.Name; } else { Page.Title = "Care Measure Scale"; }

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
            
            CareMeasureScaleName.Text = careMeasureScale.Name;

            CareMeasureScaleDescription.Text = careMeasureScale.Description;


            CareMeasureScaleEnabled.Checked = careMeasureScale.Enabled;

            CareMeasureScaleVisible.Checked = careMeasureScale.Visible;


            ScaleLabel1.Text = careMeasureScale.ScaleLabel1;

            ScaleLabel2.Text = careMeasureScale.ScaleLabel2;

            ScaleLabel3.Text = careMeasureScale.ScaleLabel3;

            ScaleLabel4.Text = careMeasureScale.ScaleLabel4;

            ScaleLabel5.Text = careMeasureScale.ScaleLabel5;


            CareMeasureScaleCreateAuthorityName.Text = careMeasureScale.CreateAccountInfo.SecurityAuthorityName;

            CareMeasureScaleCreateAccountId.Text = careMeasureScale.CreateAccountInfo.UserAccountId;

            CareMeasureScaleCreateAccountName.Text = careMeasureScale.CreateAccountInfo.UserAccountName;

            CareMeasureScaleCreateDate.MinDate = DateTime.MinValue;

            CareMeasureScaleCreateDate.SelectedDate = careMeasureScale.CreateAccountInfo.ActionDate;


            CareMeasureScaleModifiedAuthorityName.Text = careMeasureScale.ModifiedAccountInfo.SecurityAuthorityName;

            CareMeasureScaleModifiedAccountId.Text = careMeasureScale.ModifiedAccountInfo.UserAccountId;

            CareMeasureScaleModifiedAccountName.Text = careMeasureScale.ModifiedAccountInfo.UserAccountName;

            CareMeasureScaleModifiedDate.MinDate = DateTime.MinValue;

            CareMeasureScaleModifiedDate.SelectedDate = careMeasureScale.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage);

            CareMeasureScaleName.ReadOnly = !hasManagePermission;

            CareMeasureScaleDescription.ReadOnly = !hasManagePermission;


            CareMeasureScaleEnabled.Enabled = hasManagePermission;

            CareMeasureScaleVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Core.Individual.CareMeasureScale careMeasureScaleUnmodified = (Mercury.Client.Core.Individual.CareMeasureScale)Session[SessionCachePrefix + "CareMeasureScaleUnmodified"];

            if (careMeasureScaleUnmodified.Id == 0) { isModified = true; }


            careMeasureScale.Name = CareMeasureScaleName.Text.Trim ();

            careMeasureScale.Description = CareMeasureScaleDescription.Text.Trim ();

            careMeasureScale.Enabled = CareMeasureScaleEnabled.Checked;

            careMeasureScale.Visible = CareMeasureScaleVisible.Checked;


            careMeasureScale.ScaleLabel1 = ScaleLabel1.Text.Trim ();

            careMeasureScale.ScaleLabel2 = ScaleLabel2.Text.Trim ();

            careMeasureScale.ScaleLabel3 = ScaleLabel3.Text.Trim ();

            careMeasureScale.ScaleLabel4 = ScaleLabel4.Text.Trim ();

            careMeasureScale.ScaleLabel5 = ScaleLabel5.Text.Trim ();


            if (!isModified) { isModified = !careMeasureScale.IsEqual (careMeasureScaleUnmodified); }

            if (isModified) {

                success = MercuryApplication.CareMeasureScaleSave (careMeasureScale);

                if (success) {

                    careMeasureScale = MercuryApplication.CareMeasureScaleGet (careMeasureScale.Id, false);

                    Session[SessionCachePrefix + "CareMeasureScale"] = careMeasureScale;

                    Session[SessionCachePrefix + "CareMeasureScaleUnmodified"] = careMeasureScale.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureScaleManage)) {

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
