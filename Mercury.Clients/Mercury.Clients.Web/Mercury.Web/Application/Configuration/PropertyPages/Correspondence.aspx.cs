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

    public partial class Correspondence : System.Web.UI.Page {

        #region Private Propreties

        Mercury.Client.Core.Reference.Correspondence correspondence;

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

            if (MercuryApplication == null) { return; }


            Int64 forCorrespondenceId = 0;

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage)) 

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceReview)))

                { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["CorrespondenceId"] != null) {

                    forCorrespondenceId = Int64.Parse (Request.QueryString["CorrespondenceId"]);

                }

                if (forCorrespondenceId != 0) {

                    correspondence = MercuryApplication.CorrespondenceGet (forCorrespondenceId, false);

                    if (correspondence == null) {

                        correspondence = new Mercury.Client.Core.Reference.Correspondence (MercuryApplication);

                    }

                    else { correspondence.LoadContentAttachments (); }

                }

                else {

                    correspondence = new Mercury.Client.Core.Reference.Correspondence (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "Correspondence"] = correspondence;

                Session[SessionCachePrefix + "CorrespondenceUnmodified"] = correspondence.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                correspondence = (Mercury.Client.Core.Reference.Correspondence)Session[SessionCachePrefix + "Correspondence"];

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

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeCorrespondenceContentPage ();

            InitializeCorrespondenceContentGrid ();

            InitializeExtendedPropertiesGrid ();

            ApplySecurity ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (correspondence.Name)) { Page.Title = "Correspondence - " + correspondence.Name; } else { Page.Title = "New Correspondence"; }

            CorrespondenceName.Text = correspondence.Name;

            CorrespondenceDescription.Text = correspondence.Description;


            CorrespondenceFormSelection.Items.Clear ();

            CorrespondenceFormSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No Associated Form", "0"));


            foreach (Mercury.Server.Application.SearchResultFormHeader currentForm in MercuryApplication.FormsAvailable (false)) {

                if (currentForm.Enabled) {

                    CorrespondenceFormSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentForm.Name, currentForm.Id.ToString ()));

                }

            }

            CorrespondenceFormSelection.SelectedValue = correspondence.FormId.ToString ();


            CorrespondenceVersion.Value = correspondence.Version;

            CorrespondenceVersion.MinValue = correspondence.Version;
            
            CorrespondenceEnabled.Checked = correspondence.Enabled;

            CorrespondenceVisible.Checked = correspondence.Visible;


            CorrespondenceCreateAuthorityName.Text = correspondence.CreateAccountInfo.SecurityAuthorityName;

            CorrespondenceCreateAccountId.Text = correspondence.CreateAccountInfo.UserAccountId;

            CorrespondenceCreateAccountName.Text = correspondence.CreateAccountInfo.UserAccountName;

            CorrespondenceCreateDate.MinDate = DateTime.MinValue;

            CorrespondenceCreateDate.SelectedDate = correspondence.CreateAccountInfo.ActionDate;


            CorrespondenceModifiedAuthorityName.Text = correspondence.ModifiedAccountInfo.SecurityAuthorityName;

            CorrespondenceModifiedAccountId.Text = correspondence.ModifiedAccountInfo.UserAccountId;

            CorrespondenceModifiedAccountName.Text = correspondence.ModifiedAccountInfo.UserAccountName;

            CorrespondenceModifiedDate.MinDate = DateTime.MinValue;

            CorrespondenceModifiedDate.SelectedDate = correspondence.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeCorrespondenceContentPage () {

            CorrespondenceStoreImage.Checked = correspondence.StoreImage;


            CorrespondenceContentReportingServerSelection.Items.Clear ();


            foreach (Client.Reporting.ReportingServer currentReportingServer in MercuryApplication.ReportingServersAvailable (false)) {

                CorrespondenceContentReportingServerSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentReportingServer.Name, currentReportingServer.Id.ToString ()));

            }

            // TODO: IF NO REPORTING SERVERS AVAILABLE, UNABLE TO ADD REPORTS

            if (CorrespondenceContentReportingServerSelection.Items.Count == 0) {

                CorrespondenceContentReportingServerSelection.Enabled = false;

                CorrespondenceContentReportName.Enabled = false;

            }


            return;

        }

        protected void InitializeCorrespondenceContentGrid () {

            CorrespondenceContentGrid.DataSource = correspondence.Content.Values;

            CorrespondenceContentGrid.Rebind ();

            return;

        }

        protected void InitializeExtendedPropertiesGrid () {

            System.Data.DataTable propertiesTable = new DataTable ();

            propertiesTable.Columns.Add ("ExtendedPropertyName");

            propertiesTable.Columns.Add ("ExtendedPropertyValue");

            foreach (String currentPropertyName in correspondence.ExtendedProperties.Keys) {

                propertiesTable.Rows.Add (

                    currentPropertyName,

                    correspondence.ExtendedProperties[currentPropertyName]

                );

            }

            ExtendedPropertiesGrid.DataSource = propertiesTable;

            ExtendedPropertiesGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage);


            CorrespondenceName.ReadOnly = !hasManagePermission;

            CorrespondenceDescription.ReadOnly = !hasManagePermission;

            CorrespondenceEnabled.Enabled = hasManagePermission;

            CorrespondenceVisible.Enabled = hasManagePermission;
            

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Correspondence Content Grid Event Handlers

        protected void CorrespondenceContentGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            correspondence.Content.RemoveAt (deleteIndex);


            SortedList<Int32, Client.Core.Reference.CorrespondenceContent> newContent = new SortedList<Int32, Mercury.Client.Core.Reference.CorrespondenceContent> ();

            Int32 newSequence = 0;

            foreach (Int32 currentSequence in correspondence.Content.Keys) {

                newSequence = newSequence + 1;

                newContent.Add (newSequence, correspondence.Content[currentSequence].Copy ());

                newContent[newSequence].ContentSequence = newSequence;

            }

            correspondence.Content = newContent;


            InitializeCorrespondenceContentGrid ();

            return;

        }

        protected void CorrespondenceContentGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.ItemIndex;

            Int32 sequence;

            String direction = eventArgs.CommandName;

            SortedList<Int32, Client.Core.Reference.CorrespondenceContent> newContent = new SortedList<int, Mercury.Client.Core.Reference.CorrespondenceContent> ();


            switch (direction.ToLowerInvariant ()) {

                case "moveup":

                    if (itemIndex != 0) {

                        for (Int32 currentIndex = 0; currentIndex < (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newContent.Add (sequence, correspondence.Content[sequence].Copy ());

                        }

                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newContent.Add (itemIndex, correspondence.Content[sequence].Copy ());

                        newContent[itemIndex].ContentSequence = itemIndex;

                        sequence = itemIndex;

                        newContent.Add (itemIndex + 1, correspondence.Content[sequence].Copy ());

                        newContent[itemIndex + 1].ContentSequence = itemIndex + 1;


                        for (Int32 currentIndex = itemIndex + 1; currentIndex < correspondence.Content.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newContent.Add (sequence, correspondence.Content[sequence].Copy ());

                        }

                        correspondence.Content = newContent;

                    }

                    break;

                case "movedown":

                    if (itemIndex != (correspondence.Content.Count - 1)) {

                        for (Int32 currentIndex = 0; currentIndex <= (itemIndex - 1); currentIndex++) {

                            sequence = currentIndex + 1;

                            newContent.Add (sequence, correspondence.Content[sequence].Copy ());

                        }


                        // I = 1 / S = 2
                        // I = 2 / S = 3

                        // I = 1 / S = 3
                        //       / S = 2


                        // SWITCH THE TWO SPOTS

                        sequence = itemIndex + 1;

                        newContent.Add (sequence + 1, correspondence.Content[sequence].Copy ());

                        newContent[sequence + 1].ContentSequence = itemIndex + 2;


                        sequence = itemIndex + 2;

                        newContent.Add (sequence - 1, correspondence.Content[sequence].Copy ());

                        newContent[sequence - 1].ContentSequence = sequence - 1;


                        for (Int32 currentIndex = itemIndex + 2; currentIndex < correspondence.Content.Count; currentIndex++) {

                            sequence = currentIndex + 1;

                            newContent.Add (sequence, correspondence.Content[sequence].Copy ());

                        }

                        correspondence.Content = newContent;

                    }

                    break;

            }


            InitializeCorrespondenceContentGrid ();

            return;

        }

        protected void CorrespondenceContentAddButton_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Client.Core.Reference.CorrespondenceContent newContent = new Client.Core.Reference.CorrespondenceContent (MercuryApplication);

            
            SaveResponseLabel.Text = String.Empty;


            // ATTACHMENTS OVERRIDE REPORT ENTRY

            if (CorrespondenceContentAttachment.UploadedFiles.Count == 1) {

                newContent.Name = CorrespondenceContentAttachment.UploadedFiles[0].GetName ();

                System.IO.MemoryStream memoryStream = new System.IO.MemoryStream ();
                
                CorrespondenceContentAttachment.UploadedFiles[0].InputStream.CopyTo (memoryStream);


                newContent.ReportingServerId = 0;

                newContent.Name = CorrespondenceContentAttachment.UploadedFiles[0].GetName ();

                newContent.CorrespondenceContentPath = String.Empty;

                newContent.Attachment = memoryStream;

                newContent.ContentType = Mercury.Server.Application.CorrespondenceContentType.Attachment;


                if (CorrespondenceContentAttachmentXps.UploadedFiles.Count == 1) {

                    memoryStream = new System.IO.MemoryStream ();

                    CorrespondenceContentAttachmentXps.UploadedFiles[0].InputStream.CopyTo (memoryStream);

                    newContent.AttachmentXps = memoryStream;

                }

            }

            else {

                if (CorrespondenceContentReportingServerSelection.SelectedItem != null) { newContent.ReportingServerId = Convert.ToInt64 (CorrespondenceContentReportingServerSelection.SelectedValue); }

                newContent.ContentType = Mercury.Server.Application.CorrespondenceContentType.Report;

                newContent.ReportName = CorrespondenceContentReportName.Text;

            }


            Dictionary<String, String> validationResponse;

            validationResponse = newContent.Validate ();

            if (validationResponse.Count == 0) {

                if (correspondence.ContentExists (newContent)) {

                    SaveResponseLabel.Text = "Invalid Content: Duplicate Found";

                }

                else {

                    correspondence.AppendContent (newContent);

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }

            InitializeCorrespondenceContentGrid ();               

            return;

        }

        #endregion 


        #region Extended Properties Event Handlers

        protected void ButtonAddExtendedProperty_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            if (!String.IsNullOrEmpty (CorrespondenceExtendedPropertyName.Text)) {

                if (!correspondence.ExtendedProperties.ContainsKey (CorrespondenceExtendedPropertyName.Text)) {

                    correspondence.ExtendedProperties.Add (CorrespondenceExtendedPropertyName.Text, CorrespondenceExtendedPropertyValue.Text);

                }

                else { SaveResponseLabel.Text = "Cannot add duplicate Extended Property to Work Queue."; }

            }

            InitializeExtendedPropertiesGrid ();

            return;

        }

        protected void ExtendedPropertiesGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            String extendedPropertyName = (String) eventArgs.Item.OwnerTableView.DataKeyValues[deleteIndex]["ExtendedPropertyName"];

            correspondence.ExtendedProperties.Remove (extendedPropertyName);


            InitializeExtendedPropertiesGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }


            if (!CorrespondenceVersion.Value.HasValue) {

                SaveResponseLabel.Text = "A valid Version must be specified.";

                return false;

            }



            Mercury.Client.Core.Reference.Correspondence correspondenceUnmodified = (Mercury.Client.Core.Reference.Correspondence) Session[SessionCachePrefix + "CorrespondenceUnmodified"];


            correspondence.Name = CorrespondenceName.Text;

            correspondence.Description = CorrespondenceDescription.Text;

            correspondence.Version = CorrespondenceVersion.Value.Value;

            correspondence.FormId = Convert.ToInt64 (CorrespondenceFormSelection.SelectedValue);

            correspondence.StoreImage = CorrespondenceStoreImage.Checked;

            correspondence.Enabled = CorrespondenceEnabled.Checked;

            correspondence.Visible = CorrespondenceVisible.Checked;


            if (correspondenceUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !correspondence.IsEqual (correspondenceUnmodified); }


            validationResponse = correspondence.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }


                success = MercuryApplication.CorrespondenceSave (correspondence);

                if (success) {

                    correspondence = MercuryApplication.CorrespondenceGet (correspondence.Id, false);

                    correspondence.LoadContentAttachments ();

                    Session[SessionCachePrefix + "Correspondence"] = correspondence;

                    Session[SessionCachePrefix + "CorrespondenceUnmodified"] = correspondence.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CorrespondenceManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

                Server.Transfer ("/WindowClose.aspx");

            }

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

        }

        #endregion

    }

}
