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

namespace Mercury.Web.Application.Enterprise.Windows {

    public partial class EnvironmentRoleProperties : System.Web.UI.Page {

        String SessionCachePrefix = String.Empty;

        String SessionCacheSuffix = String.Empty;

        protected Mercury.Client.Application application;


        Mercury.Server.Application.Environment environment;

        Client.Environment.Role environmentRole;


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            String environmentName;

            String roleName;


            if (Session["Mercury.Application"] == null) { Response.RedirectLocation = "/SessionExpired.aspx"; return; }

            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentReview)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }

            ButtonApply.Click += new EventHandler (this.ButtonApply_OnClick);

            ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);

            if ((application != null) && (!Page.IsPostBack)) {

                #region Initial Page Load

                SessionCachePrefix = Form.Name;

                PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", "");

                SessionCacheSuffix = PageInstanceId.Text;


                environmentName = Request.QueryString["EnvironmentName"];

                roleName = Request.QueryString["RoleName"];


                environment = application.EnvironmentGet (environmentName);

                environmentRole = application.EnvironmentRoleGet (environmentName, roleName);

                if ((environmentRole == null) && !(roleName == String.Empty)) { Server.Transfer ("/PermissionDenied.aspx"); }

                else if (environmentRole == null) {

                    environmentRole = new Mercury.Client.Environment.Role (application);

                    environmentRole.Name = String.Empty;

                    environmentRole.Description = String.Empty;

                    environmentRole.CreateAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

                    environmentRole.CreateAccountInfo.ActionDate = EnvironmentRoleCreateDate.MinDate;

                    environmentRole.ModifiedAccountInfo = new Mercury.Server.Application.AuthorityAccountStamp ();

                    environmentRole.ModifiedAccountInfo.ActionDate = EnvironmentRoleModifiedDate.MinDate;
                
                }


                Session.Add (SessionCachePrefix + "Environment" + SessionCacheSuffix, environment);

                Session.Add (SessionCachePrefix + "EnvironmentRole" + SessionCacheSuffix, environmentRole);

                Session.Add (SessionCachePrefix + "EnvironmentRoleUnmodified" + SessionCacheSuffix, environmentRole.Copy ());


                Initialize_GeneralPage ();

                if (application.HasEnvironmentPermission (environment.Name, Mercury.Server.EnvironmentPermissions.RoleReview)) {

                    // Initialize Enterprise Permission and Environment Access 

                    Initialize_RolePermissions ();

                    Initialize_RoleMembership ();

                    Initialize_SecurityAuthoritySelection ();

                    Initialize_SecurityGroupSelection ();

                }

                else {

                    PropertiesTab.Tabs[1].Visible = false;

                    PropertiesContent.PageViews[1].Visible = false;

                    PropertiesTab.Tabs[2].Visible = false;

                    PropertiesContent.PageViews[2].Visible = false;

                }

                #endregion

            } // Initial Page Load

            else { // Postback

                SessionCachePrefix = Form.Name;

                SessionCacheSuffix = PageInstanceId.Text;

                environment = (Mercury.Server.Application.Environment) Session[SessionCachePrefix + "Environment" + SessionCacheSuffix];

                environmentRole = (Client.Environment.Role) Session[SessionCachePrefix + "EnvironmentRole" + SessionCacheSuffix];

            }

            return;

        }

        #endregion


        #region Control Initializations

        protected void Initialize_All () {

            Initialize_GeneralPage ();

            Initialize_RolePermissions ();

            Initialize_RoleMembership ();

            Initialize_SecurityAuthoritySelection ();

            Initialize_SecurityGroupSelection ();

            return;

        }

        protected void Initialize_GeneralPage () {

            if (!String.IsNullOrEmpty (environmentRole.Name)) { Page.Title = "Environment Role - " + environmentRole.Name; }

            else { Page.Title = "New Environment Role"; }


            EnvironmentRoleName.Text = environmentRole.Name;

            EnvironmentRoleDescription.Text = environmentRole.Description;


            EnvironmentRoleCreateAuthorityName.Text = environmentRole.CreateAccountInfo.SecurityAuthorityName;

            EnvironmentRoleCreateAccountId.Text = environmentRole.CreateAccountInfo.UserAccountId;

            EnvironmentRoleCreateAccountName.Text = environmentRole.CreateAccountInfo.UserAccountName;

            EnvironmentRoleCreateDate.SelectedDate = environmentRole.CreateAccountInfo.ActionDate;


            EnvironmentRoleModifiedAuthorityName.Text = environmentRole.ModifiedAccountInfo.SecurityAuthorityName;

            EnvironmentRoleModifiedAccountId.Text = environmentRole.ModifiedAccountInfo.UserAccountId;

            EnvironmentRoleModifiedAccountName.Text = environmentRole.ModifiedAccountInfo.UserAccountName;

            EnvironmentRoleModifiedDate.SelectedDate = environmentRole.ModifiedAccountInfo.ActionDate;


            if (application.HasEnvironmentPermission (environment.Name, Mercury.Server.EnvironmentPermissions.RoleManage)) {

                EnvironmentRoleName.ReadOnly = false;

                EnvironmentRoleDescription.ReadOnly = false;

            }

            return;

        }

        protected void Initialize_RolePermissions () {

            List<Mercury.Server.Application.EnvironmentRolePermission> rolePermissions = application.EnvironmentRoleGetPermissions (environment.Name, environmentRole.Name);
            
            Dictionary<Int64, String> environmentPermissions = application.EnvironmentPermissionDictionary (environment.Name);


            System.Data.DataTable permissionTable = new DataTable ();

            Boolean permissionExists = false;


            permissionTable.Columns.Add ("PermissionId");

            permissionTable.Columns.Add ("Permission");

            permissionTable.Columns.Add ("IsGranted");

            permissionTable.Columns.Add ("IsDenied");

            foreach (Int64 currentPermissionId in environmentPermissions.Keys) {

                permissionExists = false;

                foreach (Client.Environment.RolePermission currentPermission in environmentRole.Permissions) { 

                    if (currentPermissionId == currentPermission.PermissionId) { 

                        permissionExists = true;

                        permissionTable.Rows.Add (

                                currentPermission.PermissionId,

                                environmentPermissions[currentPermissionId],

                                currentPermission.IsGranted,

                                currentPermission.IsDenied

                            );

                        break;

                    }

                } // foreach

                if (!permissionExists) {

                    permissionTable.Rows.Add (

                            currentPermissionId,

                            environmentPermissions[currentPermissionId],

                            false,

                            false

                        );

                }

            } // foreach 

            EnvironmentPermissionsGrid.DataSource = permissionTable;

            EnvironmentPermissionsGrid.Rebind ();

            if (application.HasEnvironmentPermission (environment.Name, Mercury.Server.EnvironmentPermissions.RoleManage)) {

                foreach (Telerik.Web.UI.GridItem currentItem in EnvironmentPermissionsGrid.Items) {

                    currentItem.Edit = true;

                }

            }


            EnvironmentPermissionsGrid.MasterTableView.DataKeyNames = new String[] { "PermissionId", "Permission", "IsGranted", "IsDenied" };

            EnvironmentPermissionsGrid.Rebind ();

            Session[SessionCachePrefix + "EnvironmentPermissionsGrid.DataSource" + SessionCacheSuffix] = EnvironmentPermissionsGrid.DataSource;


            return;

        }

        protected void Initialize_RoleMembership () {

            System.Data.DataTable membershipTable = new DataTable ();


            membershipTable.Columns.Add ("SecurityAuthorityId");

            membershipTable.Columns.Add ("SecurityAuthorityName");

            membershipTable.Columns.Add ("SecurityGroupId");

            membershipTable.Columns.Add ("SecurityGroupName");


            foreach (Client.Environment.RoleMembership currentMembership in environmentRole.Membership) {

                membershipTable.Rows.Add (currentMembership.SecurityAuthorityId, currentMembership.SecurityAuthorityName, currentMembership.SecurityGroupId, currentMembership.SecurityGroupName);

            }

            RoleMembershipGrid.DataSource = membershipTable;

            RoleMembershipGrid.MasterTableView.DataKeyNames = new String[] { "SecurityAuthorityId", "SecurityAuthorityName", "SecurityGroupId", "SecurityGroupName" };

            RoleMembershipGrid.Rebind ();


            Session[SessionCachePrefix + "RoleMembershipGrid.DataSource" + SessionCacheSuffix] = RoleMembershipGrid.DataSource;

            return;

        }

        protected void Initialize_SecurityAuthoritySelection () {

            Dictionary<Int64, String> securityAuthorities = application.SecurityAuthorityDictionary (false);

            SecurityAuthoritySelection.Items.Clear ();

            foreach (Int64 currentAuthorityId in securityAuthorities.Keys) {

                SecurityAuthoritySelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (securityAuthorities[currentAuthorityId], currentAuthorityId.ToString ()));

            }

            return;

        }

        protected void Initialize_SecurityGroupSelection () {

            Dictionary<String, String> securityGroups = application.SecurityAuthoritySecurityGroupDictionary(SecurityAuthoritySelection.SelectedItem.Text);

            SecurityGroupSelection.Items.Clear ();

            foreach (String currentGroupId in securityGroups.Keys) {

                SecurityGroupSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (securityGroups[currentGroupId], currentGroupId));

            }

            return;

        }

        #endregion


        #region Control Event Handlers

        protected void SecurityAuthoritySelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            Initialize_SecurityGroupSelection ();

            return;

        }

        protected void ButtonAddMembership_OnClick (Object sender, EventArgs eventArgs) {

            if ((SecurityAuthoritySelection.SelectedItem == null) || (SecurityGroupSelection.SelectedItem == null)) { return; }


            Int64 securityAuthorityId = Convert.ToInt64 (SecurityAuthoritySelection.SelectedItem.Value);

            String securityAuthorityName = SecurityAuthoritySelection.SelectedItem.Text;

            String securityGroupId = SecurityGroupSelection.SelectedItem.Value;

            String securityGroupName = SecurityGroupSelection.SelectedItem.Text;


            environmentRole.AddMembership (securityAuthorityId, securityAuthorityName, securityGroupId, securityGroupName);

            Session[SessionCachePrefix + "EnvironmentRole" + SessionCacheSuffix] = environmentRole;


            Initialize_RoleMembership ();

            return;

        }

        protected void RoleMembershipGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;


            environmentRole.Membership.RemoveAt (deleteIndex);

            Session[SessionCachePrefix + "EnvironmentRole" + SessionCacheSuffix] = environmentRole;


            Initialize_RoleMembership ();

            return;

        }

        #endregion
        

        #region Validation Methods

        protected Boolean ValidatedValues () {

            Boolean isValid = true;

            if (EnvironmentRoleName.Text.Length == 0) { SaveResponseLabel.Text = "Name is Invalid."; return false; }

            return isValid;

        }

        protected Boolean CompareStringValues (String originalValue, String newValue) {

            Boolean isModified = false;

            if (originalValue != newValue) {

                isModified = true;

            }

            return isModified;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = true;


            Client.Environment.Role unmodifiedEnvironmentRole = (Client.Environment.Role) Session[SessionCachePrefix + "EnvironmentRoleUnmodified" + SessionCacheSuffix];
            

            if (!ValidatedValues ()) { return false; }


            #region General Properties

            if (environmentRole.RoleId == 0) { isModified = true; }


            environmentRole.Name = EnvironmentRoleName.Text;

            environmentRole.Description = EnvironmentRoleDescription.Text;

            
            #region Role Environment Permission Changes

            environmentRole.Permissions.Clear ();

            foreach (Telerik.Web.UI.GridEditableItem currentItem in EnvironmentPermissionsGrid.EditItems) {

                System.Collections.Hashtable newValues = new Hashtable ();

                currentItem.ExtractValues (newValues);

                if (((Boolean) newValues ["IsGranted"]) || ((Boolean) newValues ["IsDenied"])) { 

                    String permissionIdText = currentItem.KeyValues.Split (',')[0].Split ('\"')[1];

                    Int64 permissionId = Int64.Parse (permissionIdText);

                    environmentRole.AddPermission (permissionId, (Boolean) newValues["IsGranted"], (Boolean) newValues["IsDenied"]);

                }

            }

            #endregion


            isModified = !environmentRole.IsEqual (unmodifiedEnvironmentRole);


            if (isModified) {

                success = application.EnvironmentRoleSave (environment.Name, environmentRole.ToServerObject ());

                if (success) {

                    environmentRole = application.EnvironmentRoleGet (environment.Name, environmentRole.Name);

                    Session[SessionCachePrefix + "EnvironmentRole" + SessionCacheSuffix] = environmentRole;

                    SaveResponseLabel.Text = "Save Successful.";

                    Initialize_All ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save Role.";

                    if (application.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + application.LastException.Message + "]"; }

                    success = false;

                }

            }

            else { success = true; }

            #endregion


            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            success = ApplyChanges ();

            if (success) {

                Server.Transfer ("/WindowCloseDialog.aspx");

            }

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowCloseDialog.aspx");

        }

        #endregion


    }

}
