using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Work {

    public partial class ManageWorkQueueGetWork : System.Web.UI.Page {

        #region Private Properties

        private Mercury.Client.Core.Work.WorkQueue workQueue;

        #endregion


        #region Private Session Cache

        private String SessionCachePrefix {

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

        private Dictionary<Int64, String> SecurityAuthorities {

            get {

                Dictionary<Int64, String> securityAuthorities = (Dictionary<Int64, String>)Session[SessionCachePrefix + "SecurityAuthorities"];

                if (securityAuthorities == null) {

                    securityAuthorities = MercuryApplication.SecurityAuthorityDictionary (true);

                }

                return securityAuthorities;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            Int64 forWorkQueueId = 0;


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkQueueId"] != null) {

                    forWorkQueueId = Int64.Parse (Request.QueryString["WorkQueueId"]);

                }

                if (forWorkQueueId != 0) {

                    if (!MercuryApplication.SessionWorkQueueHasManagePermission (forWorkQueueId)) { Response.Redirect ("/PermissionDenied.aspx", true); return; }

                    workQueue = MercuryApplication.WorkQueueGet (forWorkQueueId, false);
                    
                    if (workQueue == null) {

                        workQueue = new Mercury.Client.Core.Work.WorkQueue (MercuryApplication);

                    }

                    Page.Title = "Work Queue - " + workQueue.Name;

                }

                else {

                    Response.Redirect  ("/PermissionDenied.aspx", true);

                    return;

                }

                InitializeAll ();

                Session[SessionCachePrefix + "WorkQueue"] = workQueue;

                Session[SessionCachePrefix + "WorkQueueUnmodified"] = workQueue.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                workQueue = (Mercury.Client.Core.Work.WorkQueue)Session[SessionCachePrefix + "WorkQueue"];

                if (!MercuryApplication.SessionWorkQueueHasManagePermission (workQueue.Id)) { Response.Redirect ("/PermissionDenied.aspx", true); return; }

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (workQueue.Name)) { Page.Title = "Work Queue - " + workQueue.Name; } else { Page.Title = "New Work Queue"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            if (MercuryApplication != null) {

                MercuryApplication.ApplicationClientClose ();

            }

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeGetWorkPage ();

            return;

        }

        protected void InitializeGeneralPage () {

            WorkQueueName.Text = workQueue.Name;

            WorkQueueDescription.Text = workQueue.Description;


            WorkQueueScheduleValue.Text = workQueue.ScheduleValue.ToString ();

            WorkQueueScheduleQualifier.SelectedValue = ((Int32)workQueue.ScheduleQualifier).ToString ();

            WorkQueueThresholdValue.Text = workQueue.ThresholdValue.ToString ();

            WorkQueueThresholdQualifier.SelectedValue = ((Int32)workQueue.ThresholdQualifier).ToString ();

            WorkQueueInitialConstraintValue.Text = workQueue.InitialConstraintValue.ToString ();

            WorkQueueInitialConstraintQualifier.SelectedValue = ((Int32)workQueue.InitialConstraintQualifier).ToString ();

            WorkQueueInitialMilestoneValue.Text = workQueue.InitialMilestoneValue.ToString ();

            WorkQueueInitialMilestoneQualifier.SelectedValue = ((Int32)workQueue.InitialMilestoneQualifier).ToString ();


            WorkQueueEnabled.Checked = workQueue.Enabled;

            WorkQueueVisible.Checked = workQueue.Visible;


            WorkQueueWorkflow.Items.Clear ();

            WorkQueueWorkflow.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Not Assigned, Manual Process Only", "0"));

            foreach (Client.Core.Work.Workflow currentWorkflow in MercuryApplication.WorkflowsAvailable (false)) {

                if (currentWorkflow.Enabled) {

                    WorkQueueWorkflow.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkflow.Name, currentWorkflow.Id.ToString ()));

                }

            }

            WorkQueueWorkflow.SelectedValue = workQueue.WorkflowId.ToString ();


            WorkQueueCreateAuthorityName.Text = workQueue.CreateAccountInfo.SecurityAuthorityName;

            WorkQueueCreateAccountId.Text = workQueue.CreateAccountInfo.UserAccountId;

            WorkQueueCreateAccountName.Text = workQueue.CreateAccountInfo.UserAccountName;

            WorkQueueCreateDate.MinDate = DateTime.MinValue;

            WorkQueueCreateDate.SelectedDate = workQueue.CreateAccountInfo.ActionDate;


            WorkQueueModifiedAuthorityName.Text = workQueue.ModifiedAccountInfo.SecurityAuthorityName;

            WorkQueueModifiedAccountId.Text = workQueue.ModifiedAccountInfo.UserAccountId;

            WorkQueueModifiedAccountName.Text = workQueue.ModifiedAccountInfo.UserAccountName;

            WorkQueueModifiedDate.MinDate = DateTime.MinValue;

            WorkQueueModifiedDate.SelectedDate = workQueue.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeGetWorkPage () {

            GetWorkViewSelection.Items.Clear ();

            UserViewWorkQueueViewSelection.Items.Clear ();

            GetWorkViewSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** System Default", "0"));

            UserViewWorkQueueViewSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** System Default", "0"));

            foreach (Client.Core.Work.WorkQueueView currentWorkQueueView in MercuryApplication.WorkQueueViewsAvailable (false)) {

                if (currentWorkQueueView.Enabled) {

                    GetWorkViewSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueueView.Name, currentWorkQueueView.Id.ToString ()));

                    UserViewWorkQueueViewSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentWorkQueueView.Name, currentWorkQueueView.Id.ToString ()));

                }

            }

            GetWorkViewSelection.SelectedValue = workQueue.GetWorkViewId.ToString ();

            UserViewWorkQueueViewSelection.SelectedValue = "0";

            GetWorkUseGrouping.Checked = workQueue.GetWorkUseGrouping;



            UserViewGrid.DataSource = null;

            UserViewGrid.Rebind ();



            Dictionary<String, String> availableUsers = new Dictionary<String, String> ();

            Dictionary<String, String> deniedUsers = new Dictionary<String, String> ();


            // FOR EACH TEAM ASSOCIATED WITH THE WORK QUEUE

            foreach (Mercury.Server.Application.WorkQueueTeam currentWorkQueueTeam in workQueue.WorkTeams) {

                // GET ACTUAL TEAM OBJECT

                Client.Core.Work.WorkTeam workTeam = MercuryApplication.WorkTeamGet (currentWorkQueueTeam.WorkTeamId, true);

                // FOR EACH USER IN THE WORK TEAM

                foreach (Mercury.Server.Application.WorkTeamMembership currentMembership in workTeam.Membership) {

                    String membershipKey = currentMembership.SecurityAuthorityId + "|" + currentMembership.UserAccountId + "|" + currentMembership.UserAccountName;

                    String membershipName = currentMembership.SecurityAuthorityName + ": " + currentMembership.UserDisplayName;

                    // CHECK THE PERMISSION TO ADD TO THE AVAILABLE OR DENIED

                    switch (currentWorkQueueTeam.Permission) {

                        case Mercury.Server.Application.WorkQueueTeamPermission.Denied:

                            // DENIED USER CANNOT BE ADDED BY ANY TEAM

                            if (!deniedUsers.ContainsKey (membershipKey)) { deniedUsers.Add (membershipKey, membershipName); }

                            break;

                        case Mercury.Server.Application.WorkQueueTeamPermission.View:

                            // DO NOTHING, CANNOT ADD VIEW ONLY TO USER VIEWS (BUT THIS DOES NOT DENY THE USER FROM OTHER TEAMS)

                            break;

                        default:

                            if (!availableUsers.ContainsKey (membershipKey)) { availableUsers.Add (membershipKey, membershipName); }

                            break;

                    }

                }


            }

            // REMOVE ALL DENIED USERS FROM THE AVAILABLE USERS LIST

            foreach (String currentDeniedUserKey in deniedUsers.Keys) {

                if (availableUsers.ContainsKey (currentDeniedUserKey)) { availableUsers.Remove (currentDeniedUserKey); }

            }


            // REMOVE EXISTING USERS VIEWS FROM AVAILABLE

            foreach (Mercury.Server.Application.WorkQueueGetWorkUserView currentUserView in workQueue.GetWorkUserViews) {

                String currentUserViewKey = currentUserView.SecurityAuthorityId + "|" + currentUserView.UserAccountId + "|" + currentUserView.UserAccountName;

                if (availableUsers.ContainsKey (currentUserViewKey)) { availableUsers.Remove (currentUserViewKey); }

            }

            UserViewAccountSelection.DataSource = availableUsers;

            UserViewAccountSelection.DataTextField = "Value";

            UserViewAccountSelection.DataValueField = "Key";

            UserViewAccountSelection.Sort = Telerik.Web.UI.RadComboBoxSort.Ascending;

            UserViewAccountSelection.DataBind ();


            return;

        }

        protected void ApplySecurity () {


            WorkQueueName.ReadOnly = true;

            WorkQueueDescription.ReadOnly = true;

            WorkQueueEnabled.Enabled = false;

            WorkQueueVisible.Enabled = false;


            WorkQueueWorkflow.Enabled = false;

            WorkQueueInitialConstraintValue.ReadOnly = true;

            WorkQueueInitialConstraintQualifier.Enabled = false;

            WorkQueueInitialMilestoneValue.ReadOnly = true;

            WorkQueueInitialMilestoneQualifier.Enabled = false;

            WorkQueueScheduleValue.ReadOnly = true;

            WorkQueueScheduleQualifier.Enabled = false;

            WorkQueueThresholdValue.ReadOnly = true;

            WorkQueueThresholdQualifier.Enabled = false;


            ButtonCancel.Visible = true;

            ButtonApply.Visible = true;


            return;

        }

        #endregion


        #region UserViewGrid Events

        protected void UserViewGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e) {

            if (MercuryApplication == null) { return; }


            System.Data.DataTable viewTable = new System.Data.DataTable ();

            viewTable.Columns.Add ("SecurityAuthorityId");

            viewTable.Columns.Add ("SecurityAuthorityName");

            viewTable.Columns.Add ("UserAccountId");

            viewTable.Columns.Add ("UserAccountName");

            viewTable.Columns.Add ("UserDisplayName");

            viewTable.Columns.Add ("WorkQueueViewName");



            foreach (Mercury.Server.Application.WorkQueueGetWorkUserView currentUserView in workQueue.GetWorkUserViews) {

                Client.Core.Work.WorkQueueView workQueueView = MercuryApplication.WorkQueueViewGet (currentUserView.WorkQueueViewId, true);

                viewTable.Rows.Add (

                    currentUserView.SecurityAuthorityName,

                    currentUserView.SecurityAuthorityId,

                    currentUserView.UserAccountId,

                    currentUserView.UserAccountName,

                    currentUserView.UserDisplayName,

                    ((workQueueView == null) ? "** System Default" : workQueueView.Name)

                    );

            }


            UserViewGrid.DataSource = viewTable;


            return;

        }

        protected void UserViewGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            workQueue.GetWorkUserViews.RemoveAt (deleteIndex);



            UserViewGrid_ManualRebind ();

            InitializeGetWorkPage ();

            return;

        }

        protected void UserViewGrid_ManualRebind () {

            UserViewGrid.DataSource = null;

            UserViewGrid.Rebind ();

            return;

        }

        protected void ButtonAddUserView_OnClick (Object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (String.IsNullOrWhiteSpace (UserViewAccountSelection.SelectedValue)) { return; }

            if (String.IsNullOrWhiteSpace (UserViewWorkQueueViewSelection.SelectedValue)) { return; }


            Int64 securityAuthorityId = Convert.ToInt64 (UserViewAccountSelection.SelectedValue.Split ('|')[0]);

            String securityAuthorityName = UserViewAccountSelection.SelectedItem.Text.Split (':')[0];

            String userAccountId = UserViewAccountSelection.SelectedValue.Split ('|')[1];

            String userAccountName = UserViewAccountSelection.SelectedValue.Split ('|')[2];

            String userDisplayName = UserViewAccountSelection.SelectedItem.Text.Split (':')[1];

            Int64 workQueueViewId = Convert.ToInt64 (UserViewWorkQueueViewSelection.SelectedValue);


            workQueue.AddUserView (securityAuthorityId, securityAuthorityName, userAccountId, userAccountName, userDisplayName, workQueueViewId);

            InitializeGetWorkPage ();

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


            Mercury.Client.Core.Work.WorkQueue workQueueUnmodified = (Mercury.Client.Core.Work.WorkQueue)Session[SessionCachePrefix + "WorkQueueUnmodified"];


            workQueue.GetWorkViewId = (GetWorkViewSelection.SelectedItem != null) ? Convert.ToInt64 (GetWorkViewSelection.SelectedValue) : 0;

            workQueue.GetWorkUseGrouping = GetWorkUseGrouping.Checked;


            // USER VIEWS ARE UPDATED IN THEIR OWN EVENT CYCLE

            
            if (!isModified) { isModified = !workQueue.IsEqual (workQueueUnmodified); }

            validationResponse = workQueue.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {
                
                success = MercuryApplication.WorkQueueSaveGetWork (workQueue);

                if (success) {

                    workQueue = MercuryApplication.WorkQueueGet (workQueue.Id, false);

                    Session[SessionCachePrefix + "WorkQueue"] = workQueue;

                    Session[SessionCachePrefix + "WorkQueueUnmodified"] = workQueue.Copy ();

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
