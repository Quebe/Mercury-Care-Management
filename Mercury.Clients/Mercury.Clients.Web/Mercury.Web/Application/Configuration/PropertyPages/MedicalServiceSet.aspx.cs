using System;
using System.Collections;
using System.Collections.Generic;
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

namespace Mercury.Web.Application.Configuration.Windows {
    
    public partial class MedicalServiceSet : System.Web.UI.Page {

        #region Private Propreties

        private const String ReviewPermission = Mercury.Server.EnvironmentPermissions.MedicalServiceReview;

        private const String ManagePermission = Mercury.Server.EnvironmentPermissions.MedicalServiceManage;


        private Mercury.Client.Core.MedicalServices.ServiceSet serviceSet;

        #endregion


        #region Private Session Properties

        private String SessionCachePrefix {

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

            Int64 forServiceId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (ReviewPermission))
                && (!MercuryApplication.HasEnvironmentPermission (ManagePermission)))
            
            { Response.Redirect ("/PermissionDenied.aspx", true); return; }

            if ((MercuryApplication != null) && (!Page.IsPostBack)) {

                #region Initial Page Load

                if (Request.QueryString["ServiceId"] != null) {

                    forServiceId = Int64.Parse (Request.QueryString["ServiceId"]);

                }

                if (forServiceId != 0) {

                    serviceSet = MercuryApplication.MedicalServiceSetGet(forServiceId);

                    if (serviceSet == null) {

                        serviceSet = new Mercury.Client.Core.MedicalServices.ServiceSet(MercuryApplication);

                    }

                    Page.Title = "Service Set - " + serviceSet.Name;

                }

                else {

                    serviceSet = new Mercury.Client.Core.MedicalServices.ServiceSet(MercuryApplication);

                }

                InitializeGeneralPage ();

                InitializeDefinitionGrid ();

                Session[SessionCachePrefix + "ServiceSet"] = serviceSet;

                Session[SessionCachePrefix + "ServiceSetUnmodified"] = serviceSet.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                serviceSet = (Mercury.Client.Core.MedicalServices.ServiceSet) Session[SessionCachePrefix + "ServiceSet"];

            }

            ApplySecurity ();

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion 
        
        
        #region Initialization

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (serviceSet.Name)) { Page.Title = "Service Set - " + serviceSet.Name; } else { Page.Title = "New Service Set"; }

            ServiceSetName.Text = serviceSet.Name;

            ServiceSetDescription.Text = serviceSet.Description;

            ServiceSetClassification.SelectedValue = ((Int32) serviceSet.ServiceClassification).ToString ();

            ServiceSetType.SelectedValue = ((Int32) serviceSet.SetType).ToString ();

            if (serviceSet.SetType == Mercury.Server.Application.ServiceSetType.Intersection) {

                ServiceSetWithinDays.Value = serviceSet.WithinDays;

                ServiceSetWithinDays.ReadOnly = false;

            }

            else { 
                
                ServiceSetWithinDays.Value = null; 
                
                ServiceSetWithinDays.ReadOnly = true; 
            
            }

            ServiceSetEnabled.Checked = serviceSet.Enabled;

            ServiceSetVisible.Checked = serviceSet.Visible;


            ServiceSetCreateAuthorityName.Text = serviceSet.CreateAccountInfo.SecurityAuthorityName;

            ServiceSetCreateAccountId.Text = serviceSet.CreateAccountInfo.UserAccountId;

            ServiceSetCreateAccountName.Text = serviceSet.CreateAccountInfo.UserAccountName;

            ServiceSetCreateDate.MinDate = DateTime.MinValue;

            ServiceSetCreateDate.SelectedDate = serviceSet.CreateAccountInfo.ActionDate;


            ServiceSetModifiedAuthorityName.Text = serviceSet.ModifiedAccountInfo.SecurityAuthorityName;

            ServiceSetModifiedAccountId.Text = serviceSet.ModifiedAccountInfo.UserAccountId;

            ServiceSetModifiedAccountName.Text = serviceSet.ModifiedAccountInfo.UserAccountName;

            ServiceSetModifiedDate.MinDate = DateTime.MinValue;
            
            ServiceSetModifiedDate.SelectedDate = serviceSet.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeDefinitionGrid () {

            System.Data.DataTable definitionTable = new DataTable ();

            definitionTable.Columns.Add ("DefinitionId");

            definitionTable.Columns.Add ("ServiceId");

            definitionTable.Columns.Add ("ServiceName");

            definitionTable.Columns.Add ("ServiceType");

            definitionTable.Columns.Add ("Enabled");

            foreach (Mercury.Client.Core.MedicalServices.Definitions.ServiceSetDefinition currentDefinition in serviceSet.Definitions) {

                Client.Core.MedicalServices.Service medicalService = MercuryApplication.MedicalServiceGet(currentDefinition.DefinitionServiceId, false);

                if (medicalService != null) {

                    definitionTable.Rows.Add (currentDefinition.Id, currentDefinition.DefinitionServiceId, medicalService.Name, medicalService.ServiceType.ToString (), currentDefinition.Enabled);

                }

            }

            ServiceDefinitionGrid.DataSource = definitionTable;

            ServiceDefinitionGrid.DataBind ();


            ServiceDefinitionSingletonSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in MercuryApplication.MedicalServiceHeadersGetByType(Mercury.Server.Application.MedicalServiceType.Singleton)) {

                ServiceDefinitionSingletonSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

            }


            ServiceDefinitionSetSelection.Items.Clear ();

            foreach (Mercury.Server.Application.SearchResultMedicalServiceHeader serviceHeader in MercuryApplication.MedicalServiceHeadersGetByType(Mercury.Server.Application.MedicalServiceType.Set)) {

                ServiceDefinitionSetSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (serviceHeader.Name, serviceHeader.Id.ToString ()));

            }



            return;

        }

        protected void ApplySecurity () {

            if (MercuryApplication.HasEnvironmentPermission(Mercury.Server.EnvironmentPermissions.MedicalServiceManage)) {

                ServiceSetName.ReadOnly = false;

                ServiceSetDescription.ReadOnly = false;

                ServiceSetEnabled.Enabled = true;

                ServiceSetVisible.Enabled = true;

                ServiceSetType.Enabled = true;

                ServiceSetWithinDays.Enabled = true;


                ButtonAddSingletonDefinition.Visible = true;

                ButtonAddSetDefinition.Visible = true;

                ButtonCancel.Visible = true;

                ButtonApply.Visible = true;

            }

            else {

                ServiceSetName.ReadOnly = true;

                ServiceSetDescription.ReadOnly = true;

                ServiceSetEnabled.Enabled = false;

                ServiceSetVisible.Enabled = false;

                ServiceSetType.Enabled = true;

                ServiceSetWithinDays.Enabled = false;


                AddDefinitionDiv.Style.Add ("display", "none");

                ButtonAddSingletonDefinition.Visible = false;

                ButtonAddSetDefinition.Visible = false;


                ButtonCancel.Visible = false;

                ButtonApply.Visible = false;

            }

            return;

        }

        private void ValidateSession () {

            if (Session["Mercury.Application"] == null) { Server.Transfer ("/SessionExpired.aspx"); }

            return;

        }

        #endregion


        #region Button Event Handlers

        protected void ButtonAddDefinition_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Boolean existingDefinitionFound = false;

            Client.Core.MedicalServices.Definitions.ServiceSetDefinition setDefinition = null;

            Dictionary<String, String> validationResponse;


            setDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSetDefinition ();

            setDefinition.ServiceId = serviceSet.Id;

            setDefinition.Enabled = true;


            switch (((System.Web.UI.WebControls.Button) sender).ID) {

                case "ButtonAddSingletonDefinition":

                    if (ServiceDefinitionSingletonSelection.SelectedItem == null) { return; }

                    setDefinition.DefinitionServiceId = Int64.Parse (ServiceDefinitionSingletonSelection.SelectedItem.Value);

                    break;

                case "ButtonAddSetDefinition":

                    if (ServiceDefinitionSetSelection.SelectedItem == null) { return; }

                    setDefinition.DefinitionServiceId = Int64.Parse (ServiceDefinitionSetSelection.SelectedItem.Value);

                    break;

            }

            validationResponse = MercuryApplication.MedicalServiceSetDefinitionValidate(setDefinition);

            if (validationResponse.Count == 0) {

                existingDefinitionFound = false;

                foreach (Client.Core.MedicalServices.Definitions.ServiceSetDefinition currentDefinition in serviceSet.Definitions) {

                    if (currentDefinition.IsEqual (setDefinition)) { existingDefinitionFound = true; break; }

                }

                if ((!existingDefinitionFound) || (serviceSet.SetType == Mercury.Server.Application.ServiceSetType.Intersection)) {

                    serviceSet.Definitions.Add (setDefinition);

                }

                SaveResponseLabel.Text = String.Empty;

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }


            InitializeDefinitionGrid ();

            return;

        }

        protected void ServiceDefinitionGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            serviceSet.Definitions.RemoveAt (deleteIndex);

            InitializeDefinitionGrid ();

            return;

        }

        protected void ServiceDefinitionGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    serviceSet.Definitions[itemIndex].Enabled = !serviceSet.Definitions[itemIndex].Enabled;

                    break;

            }

            InitializeDefinitionGrid ();

            return;

        }

        protected void ButtonPreview_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Collections.Generic.List<Mercury.Server.Application.MemberServiceDetailSet> previewDetailSet;

            previewDetailSet = serviceSet.Preview(MercuryApplication);


            System.Data.DataTable previewTable = new DataTable ();

            previewTable.Columns.Add ("SetDefinitionId");

            previewTable.Columns.Add ("MemberServiceId");

            previewTable.Columns.Add ("ServiceName");

            previewTable.Columns.Add ("ServiceType");

            previewTable.Columns.Add ("MemberId");

            previewTable.Columns.Add ("EventDate");


            
            foreach (Mercury.Server.Application.MemberServiceDetailSet detail in previewDetailSet) {

                previewTable.Rows.Add (

                    detail.SetDefinitionId.ToString (),

                    detail.DetailMemberServiceId.ToString (),

                    detail.ServiceName,

                    detail.ServiceType.ToString (),

                    detail.MemberId.ToString (),

                    detail.EventDate.ToString ("MM/dd/yyyy")

                    );

            }

            ServicePreviewGrid.DataSource = previewTable;

            ServicePreviewGrid.DataBind ();


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


            Mercury.Client.Core.MedicalServices.ServiceSet serviceSetUnmodified = (Mercury.Client.Core.MedicalServices.ServiceSet) Session[SessionCachePrefix + "ServiceSetUnmodified"];

            Mercury.Server.Application.ObjectSaveResponse saveResponse;


            serviceSet.Name = ServiceSetName.Text;

            serviceSet.Description = ServiceSetDescription.Text;

            serviceSet.ServiceClassification = (Mercury.Server.Application.ServiceClassification) Int32.Parse (ServiceSetClassification.SelectedValue);

            serviceSet.SetType = (Mercury.Server.Application.ServiceSetType) (Int32.Parse (ServiceSetType.SelectedItem.Value));

            if (serviceSet.SetType == Mercury.Server.Application.ServiceSetType.Intersection) {

                if (ServiceSetWithinDays.Value.HasValue) { serviceSet.WithinDays = (Int32) ServiceSetWithinDays.Value; }

                else { serviceSet.WithinDays = 0; }

            }

            serviceSet.Enabled = ServiceSetEnabled.Checked;

            serviceSet.Visible = ServiceSetVisible.Checked;


            if (serviceSetUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !serviceSet.IsEqual (serviceSetUnmodified); }


            validationResponse = serviceSet.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission(ManagePermission)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                saveResponse = MercuryApplication.MedicalServiceSave(serviceSet);

                success = saveResponse.Success;

                if (success) {

                    serviceSet = MercuryApplication.MedicalServiceSetGet(saveResponse.Id);

                    Session[SessionCachePrefix + "ServiceServiceSet"] = serviceSet;

                    Session[SessionCachePrefix + "ServiceServiceSetUnmodified"] = serviceSet.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeGeneralPage ();

                    InitializeDefinitionGrid ();

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

            if (MercuryApplication == null) { return; }

            if (ApplyChanges ()) {

                Server.Transfer ("/WindowClose.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }
            
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
