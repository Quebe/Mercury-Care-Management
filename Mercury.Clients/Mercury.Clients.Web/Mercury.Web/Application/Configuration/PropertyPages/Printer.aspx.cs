using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class Printer : System.Web.UI.Page {


        #region Private Properties

        private Client.Printing.Printer printer;

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

            Int64 forPrinterId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["PrinterId"] != null) {

                    forPrinterId = Int64.Parse (Request.QueryString["PrinterId"]);

                }

                if (forPrinterId != 0) {

                    printer = MercuryApplication.PrinterGet (forPrinterId, false);

                    if (printer == null) {

                        printer = new Mercury.Client.Printing.Printer (MercuryApplication);

                    }

                }

                else {

                    printer = new Mercury.Client.Printing.Printer (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Printer"] = printer;

                Session[SessionCachePrefix + "PrinterUnmodified"] = printer.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                printer = (Mercury.Client.Printing.Printer)Session[SessionCachePrefix + "Printer"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (printer.Name)) { Page.Title = "Printer - " + printer.Name; } else { Page.Title = "Printer"; }

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

            InitializeConfigurationPage ();

            InitializeExtendedPropertiesGrid ();

            return;

        }


        protected void InitializeGeneralPage () {

            PrinterName.Text = printer.Name;

            PrinterDescription.Text = printer.Description;


            PrinterEnabled.Checked = printer.Enabled;

            PrinterVisible.Checked = printer.Visible;


            PrinterCreateAuthorityName.Text = printer.CreateAccountInfo.SecurityAuthorityName;

            PrinterCreateAccountId.Text = printer.CreateAccountInfo.UserAccountId;

            PrinterCreateAccountName.Text = printer.CreateAccountInfo.UserAccountName;

            PrinterCreateDate.MinDate = DateTime.MinValue;

            PrinterCreateDate.SelectedDate = printer.CreateAccountInfo.ActionDate;


            PrinterModifiedAuthorityName.Text = printer.ModifiedAccountInfo.SecurityAuthorityName;

            PrinterModifiedAccountId.Text = printer.ModifiedAccountInfo.UserAccountId;

            PrinterModifiedAccountName.Text = printer.ModifiedAccountInfo.UserAccountName;

            PrinterModifiedDate.MinDate = DateTime.MinValue;

            PrinterModifiedDate.SelectedDate = printer.ModifiedAccountInfo.ActionDate;

            return;

        }
        
        protected void InitializeConfigurationPage () {

            PrinterConfigurationServerName.Text = printer.PrintServerName;

            PrinterConfigurationServerQuery_OnClick (this, new EventArgs ());

            ConfigurationPrintQueuesAvailable.SelectedValue = printer.PrintQueueName;

            ConfigurationPrintQueuesAvailable_OnSelectedIndexChanged (null, null);

            return;

        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new System.Data.DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in printer.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    printer.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage);

            PrinterName.ReadOnly = !hasManagePermission;

            PrinterDescription.ReadOnly = !hasManagePermission;


            PrinterEnabled.Enabled = hasManagePermission;

            PrinterVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Printer Configuration Events

        protected void PrinterConfigurationServerQuery_OnClick (Object sender, EventArgs e) {

            ConfigurationPrintQueuesAvailable.DataSource = MercuryApplication.PrintQueuesAvailable (PrinterConfigurationServerName.Text, false);

            ConfigurationPrintQueuesAvailable.DataValueField = "Key";

            ConfigurationPrintQueuesAvailable.DataTextField = "Value";

            ConfigurationPrintQueuesAvailable.DataBind ();

            return;

        }

        protected void ConfigurationPrintQueuesAvailable_OnSelectedIndexChanged (Object sender, EventArgs e) {

            List<String> capabilitiesList = new List<string> ();

            if (sender is Telerik.Web.UI.RadListBox) {

                ConfigurationPrintQueuesAvailable.SelectedValue = ((Telerik.Web.UI.RadListBox)sender).SelectedValue;

            }


            Mercury.Server.Application.PrinterCapabilities printerCapabilities =

                MercuryApplication.PrinterCapabilitiesGet (PrinterConfigurationServerName.Text, ConfigurationPrintQueuesAvailable.SelectedValue, true);

            if (printerCapabilities != null) {

                foreach (String currentPageResolution in printerCapabilities.PageResolutions.Keys) {

                    capabilitiesList.Add ("Resolutions: " + printerCapabilities.PageResolutions[currentPageResolution] + " [" + currentPageResolution + "]");

                }

                foreach (String currentColorOption in printerCapabilities.ColorOptions.Keys) {

                    capabilitiesList.Add ("Color Options: " + printerCapabilities.ColorOptions[currentColorOption] + " [" + currentColorOption + "]");

                }

                foreach (Mercury.Server.Application.Duplexing currentDuplexing in printerCapabilities.Duplexing) {

                    capabilitiesList.Add ("Duplex Support: " + Mercury.Server.CommonFunctions.EnumerationToString (currentDuplexing));

                }

                foreach (String currentInputBinName in printerCapabilities.InputBins.Keys) {

                    capabilitiesList.Add ("Input Bin: " + printerCapabilities.InputBins[currentInputBinName] + " [" + currentInputBinName + "]");

                }

                foreach (String currentOutputBinName in printerCapabilities.OutputBins.Keys) {

                    capabilitiesList.Add ("Output Bin: " + printerCapabilities.OutputBins[currentOutputBinName] + " [" + currentOutputBinName + "]");

                }

            }


            ConfigurationPrinterCapabilities.DataSource = capabilitiesList;

            ConfigurationPrinterCapabilities.DataBind ();

            return;

        }

        #endregion 


        #region Extended Properties Event Handlers

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (CorrespondenceExtendedPropertyName.Text)) {

                if (!printer.ExtendedProperties.ContainsKey (CorrespondenceExtendedPropertyName.Text)) {

                    printer.ExtendedProperties.Add (CorrespondenceExtendedPropertyName.Text, CorrespondenceExtendedPropertyValue.Text);

                }

                else { SaveResponseLabel.Text = "Cannot add duplicate Extended Property to Work Queue."; }

            }

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void ExtendedPropertiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            String extendedPropertyName = (String)eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["ExtendedPropertyName"];

            printer.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Printing.Printer printerUnmodified = (Mercury.Client.Printing.Printer)Session[SessionCachePrefix + "PrinterUnmodified"];

            if (printerUnmodified.Id == 0) { isModified = true; }


            printer.Name = PrinterName.Text.Trim ();

            printer.Description = PrinterDescription.Text.Trim ();

            printer.Enabled = PrinterEnabled.Checked;

            printer.Visible = PrinterVisible.Checked;


            printer.PrintServerName = PrinterConfigurationServerName.Text;

            printer.PrintQueueName = ConfigurationPrintQueuesAvailable.SelectedValue;


            if (!isModified) { isModified = !printer.IsEqual (printerUnmodified); }

            if (isModified) {

                success = MercuryApplication.PrinterSave (printer);

                if (success) {

                    printer = MercuryApplication.PrinterGet (printer.Id, false);

                    Session[SessionCachePrefix + "Printer"] = printer;

                    Session[SessionCachePrefix + "PrinterUnmodified"] = printer.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.PrinterManage)) {

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
