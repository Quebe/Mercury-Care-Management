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

    public partial class SecurityGroupProperties : System.Web.UI.Page {

        String SessionCachePrefix = String.Empty;

        String SessionCacheSuffix = String.Empty;

        protected Mercury.Client.Application application;


        Mercury.Server.Application.SecurityGroup securityGroup;


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {
                
            String securityAuthorityName;

            String securityGroupId;


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

                
                securityAuthorityName = Request.QueryString["SecurityAuthorityName"];

                securityGroupId = Request.QueryString["SecurityGroupId"];


                securityGroup = application.SecurityAuthoritySecurityGroupGet (securityAuthorityName, securityGroupId);                

                Session.Add (SessionCachePrefix + "SecurityGroup" + SessionCacheSuffix, securityGroup);

                
                Initialize_GeneralPage ();

                if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentReview)) {

                    // Initialize Enterprise Permission and Environment Access 

                    Initialize_EnterprisePermissionsPage ();

                    Initialize_EnvironmentAccessPage ();

                }

                else { 

                    PropertiesTab.Tabs [1].Visible = false;

                    PropertiesContent.PageViews[1].Visible = false;

                    PropertiesTab.Tabs [2].Visible = false;

                    PropertiesContent.PageViews[2].Visible = false;

                }

                #endregion

            } // Initial Page Load

            else { // Postback

                SessionCachePrefix = Form.Name;

                SessionCacheSuffix = PageInstanceId.Text;

                securityGroup = (Mercury.Server.Application.SecurityGroup) Session[SessionCachePrefix + "SecurityGroup" + SessionCacheSuffix];

            }

            return;

        }

        #endregion


        #region Control Initializations

        protected void Initialize_GeneralPage () {

            SecurityAuthorityName.Text = securityGroup.SecurityAuthorityName;

            SecurityGroupName.Text = securityGroup.SecurityGroupName;

            SecurityGroupId.Text = securityGroup.SecurityGroupId;

            SecurityGroupDescription.Text = securityGroup.Description;

            return;

        }

        protected void Initialize_EnterprisePermissionsPage () {

            List<Mercury.Server.Application.SecurityGroupPermission> enterprisePermissions = application.SecurityGroupEnterprisePermissionsGet (securityGroup.SecurityAuthorityName, securityGroup.SecurityGroupId);

            System.Collections.Generic.Dictionary<Int64, String> permissionDictionary = application.EnterprisePermissionDictionary ();

            System.Data.DataTable permissionTable = new DataTable ();

            Boolean permissionExists = false;


            permissionTable.Columns.Add ("PermissionId");

            permissionTable.Columns.Add ("Permission");

            permissionTable.Columns.Add ("IsGranted");

            permissionTable.Columns.Add ("IsDenied");

            foreach (Int64 currentPermissionId in permissionDictionary.Keys) {

                permissionExists = false;

                foreach (Mercury.Server.Application.SecurityGroupPermission currentGroupPermission in enterprisePermissions) {

                    if (currentPermissionId == currentGroupPermission.PermissionId) {

                        permissionExists = true;

                        permissionTable.Rows.Add (
                                currentGroupPermission.PermissionId,
                                permissionDictionary[currentPermissionId],
                                currentGroupPermission.IsGranted,
                                currentGroupPermission.IsDenied
                            );

                        break;

                    }

                } // foreach

                if (!permissionExists) {

                    permissionTable.Rows.Add (
                            currentPermissionId,
                            permissionDictionary[currentPermissionId],
                            false,
                            false
                        );

                }

            } // foreach 

            EnterprisePermissionsGrid.DataSource = permissionTable;

            EnterprisePermissionsGrid.Rebind ();


            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterprisePermissionManage)) {

                foreach (Telerik.Web.UI.GridItem currentItem in EnterprisePermissionsGrid.Items) {

                    currentItem.Edit = true;

                }

            }

            EnterprisePermissionsGrid.MasterTableView.DataKeyNames = new String [] { "PermissionId", "Permission", "IsGranted", "IsDenied" };

            EnterprisePermissionsGrid.Rebind ();

            Session[SessionCachePrefix + "EnterprisePermissionGrid.DataSource" + SessionCacheSuffix] = EnterprisePermissionsGrid.DataSource;

        }

        protected void Initialize_EnvironmentAccessPage () {

            List<Mercury.Server.Application.EnvironmentAccess> environmentAccessCollection = application.SecurityGroupEnvironmentAccessGet (securityGroup.SecurityAuthorityName, securityGroup.SecurityGroupId);

            System.Collections.Generic.Dictionary<Int64, String> environmentDictionary = application.EnvironmentDictionary (false);

            System.Data.DataTable environmentTable = new DataTable ();

            Boolean environmentExists = false;


            environmentTable.Columns.Add ("EnvironmentId");

            environmentTable.Columns.Add ("EnvironmentName");

            environmentTable.Columns.Add ("IsGranted");

            environmentTable.Columns.Add ("IsDenied");

            foreach (Int64 currentEnvironmentId in environmentDictionary.Keys) {

                environmentExists = false;

                foreach (Mercury.Server.Application.EnvironmentAccess currentEnvironmentAccess in environmentAccessCollection) {

                    if (currentEnvironmentId == currentEnvironmentAccess.EnvironmentId) {

                        environmentExists = true;

                        environmentTable.Rows.Add (
                                currentEnvironmentAccess.EnvironmentId,
                                environmentDictionary[currentEnvironmentId],
                                currentEnvironmentAccess.IsGranted,
                                currentEnvironmentAccess.IsDenied
                            );

                        break;

                    }

                } // foreach

                if (!environmentExists) {

                    environmentTable.Rows.Add (
                            currentEnvironmentId,
                            environmentDictionary[currentEnvironmentId],
                            false,
                            false
                        );

                }

            } // foreach 

            EnvironmentAccessGrid.DataSource = environmentTable;

            EnvironmentAccessGrid.Rebind ();

            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentManage)) {

                foreach (Telerik.Web.UI.GridItem currentItem in EnvironmentAccessGrid.Items) {

                    currentItem.Edit = true;

                }

            }

            EnvironmentAccessGrid.MasterTableView.DataKeyNames = new String[] { "EnvironmentId", "EnvironmentName", "IsGranted", "IsDenied" };

            EnvironmentAccessGrid.Rebind ();

            Session[SessionCachePrefix + "EnvironmentAccessGrid.DataSource" + SessionCacheSuffix] = EnvironmentAccessGrid.DataSource;

        }

        #endregion


        #region Control Event Handlers

        protected Boolean ApplyChanges () {

            Boolean success = true;

            String permissionIdText;

            String environmentIdText;

            if (application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterprisePermissionAssignmentManage)) {

                // ENTERPRISE PERMISSIONS

                try {

                    foreach (Telerik.Web.UI.GridEditableItem currentItem in EnterprisePermissionsGrid.EditItems) {

                        System.Collections.Hashtable newValues = new Hashtable ();

                        currentItem.ExtractValues (newValues);

                        if (((Boolean) currentItem.SavedOldValues["IsGranted"] != (Boolean) newValues["IsGranted"])
                            || ((Boolean) currentItem.SavedOldValues["IsDenied"] != (Boolean) newValues["IsDenied"])) {

                            permissionIdText = currentItem.KeyValues.Split (',')[0].Split ('\"')[1];

                            Int64 permissionId = Int64.Parse (permissionIdText);

                            success = success && application.SecurityGroupEnterprisePermissionSave (securityGroup.SecurityAuthorityId, securityGroup.SecurityGroupId, permissionId, (Boolean)newValues["IsGranted"], (Boolean)newValues["IsDenied"]);

                            if (!success) { throw new ApplicationException (application.LastException.Message); }

                        }

                    }

                    Initialize_EnterprisePermissionsPage ();
                    
                    SaveResponseLabel.Text = "Save Successful.";
                    

                }

                catch (Exception applicationException) {

                    SaveResponseLabel.Text = "Unable to Save changes.";

                    SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + applicationException.Message + "]"; 

                    success = false;

                }

            }


            if ((application.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentAccessManage)) && (success)) {

                // ENVIRONMENT ACCESS
                try {

                    foreach (Telerik.Web.UI.GridEditableItem currentItem in EnvironmentAccessGrid.EditItems) {

                        System.Collections.Hashtable newValues = new Hashtable ();

                        currentItem.ExtractValues (newValues);

                        if (((Boolean) currentItem.SavedOldValues["IsGranted"] != (Boolean) newValues["IsGranted"])
                            || ((Boolean) currentItem.SavedOldValues["IsDenied"] != (Boolean) newValues["IsDenied"])) {

                            environmentIdText = currentItem.KeyValues.Split (',')[0].Split ('\"')[1];

                            Int64 environmentId = Int64.Parse (environmentIdText);

                            success = success && application.SecurityGroupEnvironmentAccessSave (securityGroup.SecurityAuthorityId, securityGroup.SecurityGroupId, environmentId, (Boolean)newValues["IsGranted"], (Boolean)newValues["IsDenied"]);

                            if (!success) { throw new ApplicationException (application.LastException.Message); }

                        }

                    }

                    Initialize_EnvironmentAccessPage ();

                    SaveResponseLabel.Text = "Save Successful.";

                }

                catch (Exception applicationException) {

                    SaveResponseLabel.Text = "Unable to Save changes.";

                    SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + applicationException.Message + "]";

                    success = false;

                }

            }

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
