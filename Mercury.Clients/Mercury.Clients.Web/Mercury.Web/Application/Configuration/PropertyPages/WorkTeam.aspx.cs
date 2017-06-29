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

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class WorkTeam : System.Web.UI.Page {

        #region Private Properties

        private Mercury.Client.Core.Work.WorkTeam workTeam;

        private List<Mercury.Server.Application.EnvironmentAccess> environmentAccessReference;

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

            Int64 forWorkTeamId = 0;

            if (MercuryApplication == null) { return; }


            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage))) 
            
                { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkTeamId"] != null) {

                    forWorkTeamId = Int64.Parse (Request.QueryString["WorkTeamId"]);

                }

                if (forWorkTeamId != 0) {

                    workTeam = MercuryApplication.WorkTeamGet (forWorkTeamId, false);

                    if (workTeam == null) {

                        workTeam = new Mercury.Client.Core.Work.WorkTeam (MercuryApplication);

                    }

                    Page.Title = "WorkTeam - " + workTeam.Name;

                }

                else {

                    workTeam = new Mercury.Client.Core.Work.WorkTeam (MercuryApplication);

                }

                environmentAccessReference = MercuryApplication.EnvironmentAccessGetByEnvironmentName (MercuryApplication.Session.EnvironmentName, false);

                InitializeAll ();

                Session[SessionCachePrefix + "WorkTeam"] = workTeam;

                Session[SessionCachePrefix + "WorkTeamUnmodified"] = workTeam.Copy ();

                Session[SessionCachePrefix + "EnvironmentAccessReference"] = environmentAccessReference;

                #endregion

            } // Initial Page Load

            else { // Postback

                workTeam = (Mercury.Client.Core.Work.WorkTeam) Session[SessionCachePrefix + "WorkTeam"];

                environmentAccessReference = (List<Mercury.Server.Application.EnvironmentAccess>) Session[SessionCachePrefix + "EnvironmentAccessReference"];

            }

            SaveResponseLabel.Text = String.Empty;

            ApplySecurity ();


            if (!String.IsNullOrEmpty (workTeam.Name)) { Page.Title = "Work Team - " + workTeam.Name; } else { Page.Title = "New Work Team"; }

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

            InitializeMembershipPage ();

            InitializeMembershipGrid ();

            return;

        }

        protected void InitializeGeneralPage () {

            WorkTeamName.Text = workTeam.Name;

            WorkTeamDescription.Text = workTeam.Description;

            WorkTeamType.SelectedValue = ((Int32)workTeam.WorkTeamType).ToString ();


            WorkTeamEnabled.Checked = workTeam.Enabled;

            WorkTeamVisible.Checked = workTeam.Visible;
            

            WorkTeamCreateAuthorityName.Text = workTeam.CreateAccountInfo.SecurityAuthorityName;

            WorkTeamCreateAccountId.Text = workTeam.CreateAccountInfo.UserAccountId;

            WorkTeamCreateAccountName.Text = workTeam.CreateAccountInfo.UserAccountName;

            WorkTeamCreateDate.MinDate = DateTime.MinValue;

            WorkTeamCreateDate.SelectedDate = workTeam.CreateAccountInfo.ActionDate;


            WorkTeamModifiedAuthorityName.Text = workTeam.ModifiedAccountInfo.SecurityAuthorityName;

            WorkTeamModifiedAccountId.Text = workTeam.ModifiedAccountInfo.UserAccountId;

            WorkTeamModifiedAccountName.Text = workTeam.ModifiedAccountInfo.UserAccountName;

            WorkTeamModifiedDate.MinDate = DateTime.MinValue;

            WorkTeamModifiedDate.SelectedDate = workTeam.ModifiedAccountInfo.ActionDate;

            return;

        }


        /// <summary>
        /// To Initialize the Membership Page, specifically the Membership Account Selection ComboBox.
        /// </summary>
        protected void InitializeMembershipPage () {

            Dictionary<Int64, String> securityAuthorities = MercuryApplication.SecurityAuthorityDictionary (false);

            Dictionary<String, String> membership = new Dictionary<String, String> ();

            WorkTeamMembershipAccountSelection.Items.Clear (); // CLEAR ANY EXISTING VALUES TO START OVER


            // FOR EACH SECURITY GROUP THAT HAS ENVIRONMENT ACCESS TO THE ENVIRONMENT

            foreach (Server.Application.EnvironmentAccess currentAccess in environmentAccessReference) {

                List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> groupMembership;

                groupMembership = MercuryApplication.SecurityAuthoritySecurityGroupGetMembership (currentAccess.SecurityAuthorityId, currentAccess.SecurityGroupId, false);


                // FOR EACH USER ACCOUNT IN EACH SECURITY GROUP THAT HAS ACCESS TO THE ENVIRONMENT

                foreach (Mercury.Server.Application.SecurityAuthorityDirectoryEntry currentEntry in groupMembership) {

                    if (!String.IsNullOrEmpty (currentEntry.Name)) {


                        // ENSURE THAT THE USER ACCOUNT HAS NOT ALREADY BEEN ADDED TO THE SELECTION 

                        if (!membership.ContainsKey (currentAccess.SecurityAuthorityId + "|" + currentEntry.ObjectSid)) {


                            // ENSURE THAT THE WORK TEAM DOES NOT ALREADY HAVE THE USER ACCOUNT AS A MEMBER

                            if (!workTeam.ContainsMembership (currentAccess.SecurityAuthorityId, currentEntry.ObjectSid)) {


                                // ADD USER ACCOUNT TO THE SELECTION 

                                membership.Add (currentAccess.SecurityAuthorityId + "|" + currentEntry.ObjectSid, currentEntry.DisplayName);

                                String securityAuthorityName = (securityAuthorities.ContainsKey (currentAccess.SecurityAuthorityId)) ? securityAuthorities[currentAccess.SecurityAuthorityId] : String.Empty;

                                WorkTeamMembershipAccountSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (

                                    securityAuthorityName + ": " + currentEntry.DisplayName,

                                    currentAccess.SecurityAuthorityId + "|" + currentEntry.ObjectSid + "|" + currentEntry.Name

                                ));

                            }

                        }

                    }

                }

            }

            WorkTeamMembershipAccountSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

            if (WorkTeamMembershipAccountSelection.Items.Count == 0) {

                WorkTeamMembershipAccountSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** No User Accounts Available", String.Empty));

            }

            return;

        }

        protected void InitializeMembershipGrid () {

            System.Data.DataTable membershipTable = new DataTable ();

            membershipTable.Columns.Add ("SecurityAuthorityId");

            membershipTable.Columns.Add ("SecurityAuthorityName");

            membershipTable.Columns.Add ("UserAccountId");

            membershipTable.Columns.Add ("UserAccountName");

            membershipTable.Columns.Add ("UserDisplayName");

            membershipTable.Columns.Add ("TeamRole");


            foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in workTeam.Membership) {

                membershipTable.Rows.Add (

                    currentMembership.SecurityAuthorityId,

                    currentMembership.SecurityAuthorityName,

                    currentMembership.UserAccountId,

                    currentMembership.UserAccountName,

                    currentMembership.UserDisplayName,

                    currentMembership.WorkTeamRole.ToString ()

                    );

            }

            WorkTeamMembershipGrid.DataSource = membershipTable;

            WorkTeamMembershipGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage);


            WorkTeamName.ReadOnly = !hasManagePermission;

            WorkTeamDescription.ReadOnly = !hasManagePermission;

            WorkTeamEnabled.Enabled = hasManagePermission;

            WorkTeamVisible.Enabled = hasManagePermission;

            ButtonAddMembership.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Event Handlers

        protected void ButtonAddMembership_OnClick (Object Sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            SaveResponseLabel.Text = String.Empty;

            if (WorkTeamMembershipAccountSelection.SelectedItem != null) {

                if ((!String.IsNullOrEmpty (WorkTeamMembershipAccountSelection.SelectedValue)) && (WorkTeamMembershipAccountSelection.SelectedItem != null)) {

                    Int64 securityAuthorityId = Convert.ToInt64 (WorkTeamMembershipAccountSelection.SelectedValue.Split ('|')[0]);

                    String userAccountId = WorkTeamMembershipAccountSelection.SelectedValue.Split ('|')[1];

                    String userAccountName = WorkTeamMembershipAccountSelection.SelectedValue.Split ('|')[2];

                    String userDisplayName = WorkTeamMembershipAccountSelection.SelectedItem.Text.Split (':')[1].Trim ();
                    

                    Server.Application.WorkTeamRole teamRole = (Mercury.Server.Application.WorkTeamRole) Convert.ToInt32 (WorkTeamMembershipTeamRole.SelectedValue);


                    if (!workTeam.ContainsMembership (securityAuthorityId, userAccountId)) {

                        workTeam.AddMembership (securityAuthorityId, WorkTeamMembershipAccountSelection.SelectedItem.Text.Split (':')[0], userAccountId, userAccountName, userDisplayName, teamRole);

                    }

                    else { SaveResponseLabel.Text = "Cannot add duplicate member to team."; }

                }

                else { SaveResponseLabel.Text = "No Member Selected to Add."; }

            }

            else { SaveResponseLabel.Text = "No Member Selected to Add."; }

            InitializeMembershipGrid ();

            InitializeMembershipPage ();

            return;

        }

        protected void WorkTeamMembershipGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            workTeam.Membership.RemoveAt (deleteIndex);

            InitializeMembershipGrid ();

            InitializeMembershipPage ();

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

            Mercury.Client.Core.Work.WorkTeam workTeamUnmodified = (Mercury.Client.Core.Work.WorkTeam) Session[SessionCachePrefix + "WorkTeamUnmodified"];


            workTeam.Name = WorkTeamName.Text;

            workTeam.Description = WorkTeamDescription.Text;

            workTeam.WorkTeamType = (Mercury.Server.Application.WorkTeamType)Convert.ToInt32 (WorkTeamType.SelectedValue);

            workTeam.Enabled = WorkTeamEnabled.Checked;

            workTeam.Visible = WorkTeamVisible.Checked;


            if (workTeamUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !workTeam.IsEqual (workTeamUnmodified); }

            validationResponse = workTeam.Validate ();

            isValid = (validationResponse.Count == 0);



            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.WorkTeamManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.WorkTeamSave (workTeam);

                if (success) {

                    workTeam = MercuryApplication.WorkTeamGet (workTeam.Id, false);

                    Session[SessionCachePrefix + "WorkTeam"] = workTeam;

                    Session[SessionCachePrefix + "WorkTeamUnmodified"] = workTeam.Copy ();

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

            if (ApplyChanges ()) { Server.Transfer ("/WindowClose.aspx"); }

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
