using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class Metric : System.Web.UI.Page {


        #region Private Propreties

        private Boolean isPageUnloading = false;

        private Mercury.Client.Core.Metrics.Metric metric;

        #endregion


        #region Private Session Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 forMetricId = 0;


            if (MercuryApplication == null) { return; }


            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage))) { Response.Redirect ("/SessionExpired.aspx", true); }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["MetricId"] != null) {

                    forMetricId = Int64.Parse (Request.QueryString["MetricId"]);

                }

                if (forMetricId != 0) {

                    metric = MercuryApplication.MetricGet (forMetricId, false);

                    if (metric == null) {

                        metric = new Mercury.Client.Core.Metrics.Metric (MercuryApplication);

                    }

                    Page.Title = "Metric - " + metric.Name;

                }

                else {

                    metric = new Mercury.Client.Core.Metrics.Metric (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Metric"] = metric;

                Session[SessionCachePrefix + "MetricUnmodified"] = metric.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                metric = (Mercury.Client.Core.Metrics.Metric)Session[SessionCachePrefix + "Metric"];

            }

            SaveResponseLabel.Text = String.Empty;

            ApplySecurity ();

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeCostDefinitionPage ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (metric.Name)) { Page.Title = "Metric - " + metric.Name; } else { Page.Title = "New Metric"; }

            MetricName.Text = metric.Name;

            MetricDescription.Text = metric.Description;

            MetricType.SelectedValue = ((Int32)metric.MetricType).ToString ();

            MetricDataType.SelectedValue = ((Int32)metric.DataType).ToString ();

            MetricMinimumValue.Value = Convert.ToDouble (metric.MinimumValue);

            MetricMaximumValue.Value = Convert.ToDouble (metric.MaximumValue);

            MetricEnabled.Checked = metric.Enabled;

            MetricVisible.Checked = metric.Visible;


            MetricCreateAuthorityName.Text = metric.CreateAccountInfo.SecurityAuthorityName;

            MetricCreateAccountId.Text = metric.CreateAccountInfo.UserAccountId;

            MetricCreateAccountName.Text = metric.CreateAccountInfo.UserAccountName;

            MetricCreateDate.MinDate = DateTime.MinValue;

            MetricCreateDate.SelectedDate = metric.CreateAccountInfo.ActionDate;


            MetricModifiedAuthorityName.Text = metric.ModifiedAccountInfo.SecurityAuthorityName;

            MetricModifiedAccountId.Text = metric.ModifiedAccountInfo.UserAccountId;

            MetricModifiedAccountName.Text = metric.ModifiedAccountInfo.UserAccountName;

            MetricModifiedDate.MinDate = DateTime.MinValue;

            MetricModifiedDate.SelectedDate = metric.ModifiedAccountInfo.ActionDate;


            PropertiesTab.Tabs[1].Enabled = (metric.MetricType == Mercury.Server.Application.MetricType.Cost);

            MetricDataType.Enabled = (metric.MetricType == Mercury.Server.Application.MetricType.Health);

            MetricMinimumValue.Enabled = (metric.MetricType == Mercury.Server.Application.MetricType.Health);

            MetricMaximumValue.Enabled = (metric.MetricType == Mercury.Server.Application.MetricType.Health);

            return;

        }

        protected void InitializeCostDefinitionPage () {

            MetricCostDataSource.SelectedValue = ((Int32)metric.CostDataSource).ToString ();

            MetricCostClaimDateType.SelectedValue = ((Int32)metric.CostClaimDateType).ToString ();


            MetricCostReportingPeriod.SelectedValue = ((Int32)metric.CostReportingPeriod).ToString ();

            MetricCostReportingPeriodValue.Text = metric.CostReportingPeriodValue.ToString ();

            MetricCostReportingPeriodQualifier.SelectedValue = ((Int32)metric.CostReportingPeriodQualifier).ToString ();


            switch (metric.CostReportingPeriod) {

                case Mercury.Server.Application.MetricCostReportingPeriod.CalenderYear:

                    MetricCostReportingPeriodValue.Enabled = false;

                    MetricCostReportingPeriodQualifier.Enabled = false;

                    break;

                case Mercury.Server.Application.MetricCostReportingPeriod.YearByMonth:

                    MetricCostReportingPeriodValue.Enabled = true;

                    MetricCostReportingPeriodValue.MinValue = 1;

                    MetricCostReportingPeriodValue.MaxValue = 12;

                    MetricCostReportingPeriodQualifier.Enabled = false;

                    break;

                case Mercury.Server.Application.MetricCostReportingPeriod.RollingPeriod:

                    MetricCostReportingPeriodValue.Enabled = true;

                    MetricCostReportingPeriodValue.MinValue = -9999;

                    MetricCostReportingPeriodValue.MaxValue = -1;

                    MetricCostReportingPeriodQualifier.Enabled = true;

                    break;

            }


            MetricCostWatermarkPeriod.SelectedValue = ((Int32)metric.CostWatermarkPeriod).ToString ();

            MetricCostWatermarkPeriodValue.Text = metric.CostWatermarkPeriodValue.ToString ();


            switch (metric.CostWatermarkPeriod) {

                case Mercury.Server.Application.MetricCostWatermarkPeriod.CalenderYear:

                case Mercury.Server.Application.MetricCostWatermarkPeriod.Month:

                    MetricCostWatermarkPeriodValue.Enabled = false;

                    break;

                case Mercury.Server.Application.MetricCostWatermarkPeriod.YearByMonth:

                    MetricCostWatermarkPeriodValue.Enabled = true;

                    MetricCostWatermarkPeriodValue.MinValue = 1;

                    MetricCostWatermarkPeriodValue.MaxValue = 12;

                    break;

            }


            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage);

            MetricName.ReadOnly = !hasManagePermission;

            MetricDescription.ReadOnly = !hasManagePermission;

            MetricEnabled.Enabled = hasManagePermission;

            MetricVisible.Enabled = hasManagePermission;

            MetricType.Enabled = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        private void ValidateSession () {

            if (Session["Mercury.Application"] == null) { Server.Transfer ("/SessionExpired.aspx"); }

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            ValidateSession ();

            Mercury.Client.Core.Metrics.Metric metricUnmodified = (Mercury.Client.Core.Metrics.Metric)Session[SessionCachePrefix + "MetricUnmodified"];


            metric.Name = MetricName.Text;

            metric.Description = MetricDescription.Text;

            metric.MetricType = (Mercury.Server.Application.MetricType)Convert.ToInt32 (MetricType.SelectedValue);

            metric.DataType = (Mercury.Server.Application.MetricDataType)Convert.ToInt32 (MetricDataType.SelectedValue);

            metric.MinimumValue = (MetricMinimumValue.Value.HasValue) ? Convert.ToDecimal (MetricMinimumValue.Value) : 0;

            metric.MaximumValue = (MetricMaximumValue.Value.HasValue) ? Convert.ToDecimal (MetricMaximumValue.Value) : 0;


            metric.CostDataSource = (Mercury.Server.Application.MetricCostDataSource)Convert.ToInt32 (MetricCostDataSource.SelectedValue);

            metric.CostClaimDateType = (Mercury.Server.Application.MetricCostClaimDateType)Convert.ToInt32 (MetricCostClaimDateType.SelectedValue);


            metric.CostReportingPeriod = (Mercury.Server.Application.MetricCostReportingPeriod)Convert.ToInt32 (MetricCostReportingPeriod.SelectedValue);

            metric.CostReportingPeriodValue = Convert.ToInt32 (MetricCostReportingPeriodValue.Value);

            metric.CostReportingPeriodQualifier = (Mercury.Server.Application.DateQualifier)Convert.ToInt32 (MetricCostReportingPeriodQualifier.SelectedValue);

            metric.CostWatermarkPeriod = (Mercury.Server.Application.MetricCostWatermarkPeriod)Convert.ToInt32 (MetricCostWatermarkPeriod.SelectedValue);

            metric.CostWatermarkPeriodValue = Convert.ToInt32 (MetricCostWatermarkPeriodValue.Value);
            

            metric.Enabled = MetricEnabled.Checked;

            metric.Visible = MetricVisible.Checked;


            if (metricUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !metric.IsEqual (metricUnmodified); }


            validationResponse = metric.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MetricManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.MetricSave (metric);

                if (success) {

                    metric = MercuryApplication.MetricGet (metric.Id, false);

                    Session[SessionCachePrefix + "Metric"] = metric;

                    Session[SessionCachePrefix + "MetricUnmodified"] = metric.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

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

            InitializeAll ();

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            if (ApplyChanges ()) {

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