using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Mercury.Web.Application.Enterprise.Windows {

    public partial class EnterprisePermissionManage : System.Web.UI.Page {

        const String SessionCachePrefix = "EnterpriseManagement.EnterprisePermissionManage.";

        const String ManagePermission = "Enterprise.EnterpriseManagement.EnterprisePermission.Assignment.Manage";


        protected Mercury.Client.Application application;

        protected void Page_Load (object sender, EventArgs e) {

            if (Session["Mercury.Application"] == null) {

                Response.RedirectLocation = "/SessionExpired.aspx";

                return;

            }

            application = (Mercury.Client.Application) Session["Mercury.Application"];

            if (!application.HasEnterprisePermission (ManagePermission)) {

                if (!IsPostBack) {

                    Server.Transfer ("/PermissionDenied.aspx");

                }

                else { 

                    Response.RedirectLocation = "/PermissionDenied.aspx";

                }

            }
            
            Page.Title = "Manage Enterprise Permissions";

            ButtonApply.Click += new EventHandler (this.ButtonApply_OnClick);

            ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);


            if ((application != null) && (!Page.IsPostBack)) {

                InitializePermissionGrid ();

            }

        } // Page_Load


        protected void Page_Unload (object sender, EventArgs e) {

            application.ApplicationClientClose ();

            return;

        }

        #region Initialization

        protected void Initialize_SecurityAuthorityDropdown () {

            System.Collections.Generic.Dictionary<Int64, String> securityAuthorityDictionary = application.SecurityAuthorityDictionary (false);

            System.Data.DataTable securityAuthorityTable = new DataTable ();

            securityAuthorityTable.Columns.Add ("SecurityAuthorityId");
            securityAuthorityTable.Columns.Add ("SecurityAuthorityName");

            foreach (Int64 currentAuthorityId in securityAuthorityDictionary.Keys) {

                securityAuthorityTable.Rows.Add (currentAuthorityId, securityAuthorityDictionary[currentAuthorityId]);

            } // foreach 

            SecurityAuthoritySelection.DataSource = securityAuthorityTable;

            SecurityAuthoritySelection.DataTextField = "SecurityAuthorityName";

            SecurityAuthoritySelection.DataValueField = "SecurityAuthorityId";

            SecurityAuthoritySelection.DataBind ();

            SecurityAuthoritySelection.SelectedIndex = 0;


            Session[SessionCachePrefix + "SecurityAuthoritySelection.DataSource"] = SecurityAuthoritySelection.DataSource;

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Value"] = SecurityAuthoritySelection.SelectedValue;

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"] = SecurityAuthoritySelection.SelectedItem.Text;

        }

        protected void InitializePermissionGrid () {

            Initialize_SecurityAuthorityDropdown ();

            RebindSecurityGroupSelection (SecurityAuthoritySelection.SelectedItem.Text);

            PopulateEnterprisePermissionGrid (SecurityAuthoritySelection.SelectedItem.Text, SecurityGroupSelection.SelectedValue);

        }

        protected void RebindSecurityGroupSelection (String securityAuthorityName) {


            System.Collections.Generic.Dictionary<String, String> securityGroupDictionary;

            securityGroupDictionary = application.SecurityAuthoritySecurityGroupDictionary (securityAuthorityName);

            System.Data.DataTable securityGroupTable = new DataTable ();

            securityGroupTable.Columns.Add ("SecurityGroupId");

            securityGroupTable.Columns.Add ("Name");

            String securityGroupId = String.Empty;

            foreach (String currentGroupKey in securityGroupDictionary.Keys) {

                if (securityGroupId == String.Empty) { securityGroupId = currentGroupKey; }

                securityGroupTable.Rows.Add (currentGroupKey, securityGroupDictionary[currentGroupKey]);

            }

            SecurityGroupSelection.DataTextField = "Name";

            SecurityGroupSelection.DataValueField = "SecurityGroupId";

            SecurityGroupSelection.DataSource = securityGroupTable;

            SecurityGroupSelection.DataBind ();

            if (securityGroupDictionary.Keys.Count > 0) {

                SecurityGroupSelection.SelectedIndex = 0;

            }

            if (SecurityGroupSelection.SelectedItem != null) {

                Session[SessionCachePrefix + "SecurityGroupSelection.Value"] = SecurityGroupSelection.SelectedValue;

                Session[SessionCachePrefix + "SecurityGroupSelection.Text"] = SecurityGroupSelection.SelectedItem.Text;

            }

            else {

                Session[SessionCachePrefix + "SecurityGroupSelection.Value"] = String.Empty;

                Session[SessionCachePrefix + "SecurityGroupSelection.Text"] = String.Empty;

            }

        }

        protected void PopulateEnterprisePermissionGrid (String securityAuthorityName, String securityGroupId) {

            List<Mercury.Server.Application.SecurityGroupPermission> enterprisePermissionCollection = application.SecurityGroupEnterprisePermissionsGet (securityAuthorityName, securityGroupId);

            System.Collections.Generic.Dictionary<Int64, String> permissionDictionary = application.EnterprisePermissionDictionary ();

            System.Collections.Generic.Dictionary<String, String> securityGroupDictionary;

            securityGroupDictionary = application.SecurityAuthoritySecurityGroupDictionary(securityAuthorityName);

            Session["EnterpriseManagement.EnterprisePermissionManage.SecurityAuthority.SecurityGroupDictionary." + securityAuthorityName] = securityGroupDictionary;


            System.Data.DataTable permissionTable = new DataTable ();

            Boolean permissionExists = false;

            permissionTable.Columns.Add ("SecurityAuthorityId");
            permissionTable.Columns.Add ("SecurityGroupId");
            permissionTable.Columns.Add ("PermissionId");
            permissionTable.Columns.Add ("Permission");
            permissionTable.Columns.Add ("IsGranted");
            permissionTable.Columns.Add ("IsDenied");

            foreach (Int64 currentPermissionKey in permissionDictionary.Keys) {

                permissionExists = false;

                foreach (Mercury.Server.Application.SecurityGroupPermission currentGroupPermission in enterprisePermissionCollection) {

                    if (currentPermissionKey == currentGroupPermission.PermissionId) {

                        permissionExists = true;

                        permissionTable.Rows.Add (
                                currentGroupPermission.SecurityAuthorityId,
                                currentGroupPermission.SecurityGroupId,
                                currentGroupPermission.PermissionId,
                                permissionDictionary[currentPermissionKey],
                                currentGroupPermission.IsGranted,
                                currentGroupPermission.IsDenied
                            );

                        break;

                    }

                } // foreach

                if (!permissionExists) {

                    permissionTable.Rows.Add (
                            SecurityAuthoritySelection.SelectedValue,
                            securityGroupId,
                            currentPermissionKey,
                            permissionDictionary[currentPermissionKey],
                            false,
                            false
                        );

                }

            } // foreach 


            EnterprisePermissionGrid.DataSource = permissionTable;

            EnterprisePermissionGrid.Rebind ();

            foreach (Telerik.Web.UI.GridDataItem currentItem in EnterprisePermissionGrid.Items) {

                currentItem.Edit = true;

            }

            EnterprisePermissionGrid.Rebind ();

            Session[SessionCachePrefix + "EnterprisePermissionGrid.DataSource"] = EnterprisePermissionGrid.DataSource;


        }

        #endregion


        #region Event Handlers

        protected void SecurityAuthoritySelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            String securityAuthorityName = eventArgs.Text;

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Value"] = SecurityAuthoritySelection.SelectedValue;

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"] = SecurityAuthoritySelection.SelectedItem.Text;

            RebindSecurityGroupSelection (securityAuthorityName);

            PopulateEnterprisePermissionGrid (securityAuthorityName, SecurityGroupSelection.SelectedValue);

        }

        protected void SecurityGroupSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            String securityGroupName = eventArgs.Text;

            String securityGroupId = SecurityGroupSelection.SelectedValue;

            String securityAuthorityName = (String) Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"];

            PopulateEnterprisePermissionGrid (securityAuthorityName, securityGroupId);

            Session[SessionCachePrefix + "SecurityGroupSelection.Id"] = securityGroupId;

        }


        protected Boolean ApplyChanges () {

            Boolean success = true;

            String securityAuthorityIdText;

            String permissionIdText;

            try {

                foreach (Telerik.Web.UI.GridEditableItem currentItem in EnterprisePermissionGrid.EditItems) {

                    System.Collections.Hashtable newValues = new Hashtable ();

                    currentItem.ExtractValues (newValues);

                    if (((Boolean) currentItem.SavedOldValues["IsGranted"] != (Boolean) newValues["IsGranted"])
                        || ((Boolean) currentItem.SavedOldValues["IsDenied"] != (Boolean) newValues["IsDenied"])) {

/*
                        System.Diagnostics.Debug.Write (currentItem["Permission"].Text + ": ");

                        System.Diagnostics.Debug.Write ("[" + newValues["IsGranted"] + "] ");

                        System.Diagnostics.Debug.Write ("[" + newValues["IsDenied"] + "] ");

                        System.Diagnostics.Debug.Write (" Old: ");

                        System.Diagnostics.Debug.Write ("[" + currentItem.SavedOldValues["IsGranted"] + "] ");

                        System.Diagnostics.Debug.WriteLine ("[" + currentItem.SavedOldValues["IsDenied"] + "] ");
*/

                        securityAuthorityIdText = currentItem.KeyValues.Split (',')[0].Split ('\"')[1];

                        permissionIdText = currentItem.KeyValues.Split (',')[2].Split ('\"')[1];


                        Int64 securityAuthorityId = Int64.Parse (securityAuthorityIdText);

                        String securityGroupId = SecurityGroupSelection.SelectedValue;

                        Int64 permissionId = Int64.Parse (permissionIdText);

                        success = success && application.SecurityGroupEnterprisePermissionSave (securityAuthorityId, securityGroupId, permissionId, (Boolean)newValues["IsGranted"], (Boolean)newValues["IsDenied"]);

                    }

                    if (!success) {

                        break; // STOP UPDATING ON ERROR

                    }

                }

                PopulateEnterprisePermissionGrid (SecurityAuthoritySelection.SelectedItem.Text, SecurityGroupSelection.SelectedValue);

            }

            catch (Exception accessViolation) {

                // CATCH ANY CASES WHERE THE GRID HAS BECOME CORRUPT BECAUSE OF POSTBACK
                //   AND NAVIGATION CANCEL AT THE SAME TIME

                System.Diagnostics.Debug.WriteLine (accessViolation);

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (application.HasEnterprisePermission (ManagePermission)) {

                success = ApplyChanges ();
            }

            else {

                success = true;

            }


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
