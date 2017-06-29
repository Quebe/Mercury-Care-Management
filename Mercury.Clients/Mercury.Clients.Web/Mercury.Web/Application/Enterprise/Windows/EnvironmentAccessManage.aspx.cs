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

    public partial class EnvironmentAccessManage : System.Web.UI.Page {

        const String SessionCachePrefix = "EnterpriseManagement.EnvironmentAccessManage";

        const String ManagePermission = "Enterprise.EnterpriseManagement.Environment.Access.Manage";

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

            Page.Title = "Manage Environment Access";

            ButtonApply.Click += new EventHandler (this.ButtonApply_OnClick);

            ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);

            if ((application != null) && (!Page.IsPostBack)) {

                String environmentName = Request.QueryString["EnvironmentName"];


                Initialize_EnvironmentDropdown ();

                if (!String.IsNullOrEmpty (environmentName)) {

                    foreach (Telerik.Web.UI.RadComboBoxItem currentItem in EnvironmentSelection.Items) {

                        if (currentItem.Text == environmentName) {

                            currentItem.Selected = true;

                            break;

                        }

                    }

                }

                Initialize_SecurityAuthorityDropdown ();

                PopulateEnvironmentAccessGrid (EnvironmentSelection.SelectedItem.Text, SecurityAuthoritySelection.SelectedItem.Text);

            }

            else { // POSTBACK

                System.Diagnostics.Debug.WriteLine ("Is Postback");


            }

        } // Page_Load


        #region Initialization

        protected void Initialize_EnvironmentDropdown () {

            System.Collections.Generic.Dictionary<Int64, String> environmentDictionary = application.EnvironmentDictionary (false);

            System.Data.DataTable environmentTable = new DataTable ();

            environmentTable.Columns.Add ("EnvironmentId");
            environmentTable.Columns.Add ("EnvironmentName");

            foreach (Int64 currentEnvironmentId in environmentDictionary.Keys) {

                environmentTable.Rows.Add (currentEnvironmentId, environmentDictionary[currentEnvironmentId]);

            } // foreach 

            EnvironmentSelection.DataSource = environmentTable;

            EnvironmentSelection.DataTextField = "EnvironmentName";

            EnvironmentSelection.DataValueField = "EnvironmentId";

            EnvironmentSelection.DataBind ();

            EnvironmentSelection.SelectedIndex = 0;


            Session[SessionCachePrefix + "EnvironmentSelection.DataSource"] = EnvironmentSelection.DataSource;

            Session[SessionCachePrefix + "EnvironmentSelection.Value"] = EnvironmentSelection.SelectedValue;

            Session[SessionCachePrefix + "EnvironmentSelection.Text"] = EnvironmentSelection.SelectedItem.Text;


        }

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

        protected void PopulateEnvironmentAccessGrid (String environmentName, String securityAuthorityName) {

            System.Collections.Generic.Dictionary<String, String> groupDictionary = application.SecurityAuthoritySecurityGroupDictionary(securityAuthorityName);

            List<Mercury.Server.Application.EnvironmentAccess> environmentAccessCollection = application.EnvironmentAccessGetByEnvironmentName (environmentName, false);

            System.Data.DataTable accessTable = new DataTable ();

            Boolean accessExists = false;


            accessTable.Columns.Add ("EnvironmentId");
            accessTable.Columns.Add ("SecurityAuthorityId");
            accessTable.Columns.Add ("SecurityGroupId");

            accessTable.Columns.Add ("GroupName");
            accessTable.Columns.Add ("Is Granted");
            accessTable.Columns.Add ("Is Denied");

            EnvironmentAccessGrid.DataSource = accessTable;

            EnvironmentAccessGrid.Rebind ();


            foreach (String currentGroupKey in groupDictionary.Keys) {

                // DETERMINE IF ALREADY DEFINED 

                accessExists = false;

                foreach (Mercury.Server.Application.EnvironmentAccess currentAccess in environmentAccessCollection) {

                    if (currentAccess.SecurityGroupId == currentGroupKey) {

                        accessExists = true;

                        accessTable.Rows.Add (currentAccess.EnvironmentId, currentAccess.SecurityAuthorityId, currentGroupKey, groupDictionary[currentGroupKey], currentAccess.IsGranted, currentAccess.IsDenied);

                        break;

                    }

                } // foreach currentAccess in environmentAccessCollection


                if (!accessExists) {

                    accessTable.Rows.Add (EnvironmentSelection.SelectedValue, SecurityAuthoritySelection.SelectedValue, currentGroupKey, groupDictionary[currentGroupKey], false, false);

                }

            }

            EnvironmentAccessGrid.DataSource = accessTable;

            EnvironmentAccessGrid.Rebind ();

            foreach (Telerik.Web.UI.GridDataItem currentItem in EnvironmentAccessGrid.Items) {

                currentItem.Edit = true;

            }

            EnvironmentAccessGrid.Rebind ();

            Session["EnterpriseManagement.EnvironmentAccessManage.EnvironmentAccessGrid"] = EnvironmentAccessGrid.DataSource;

        }

        #endregion


        #region Control Event Handlers

        protected void EnvironmentSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            String environmentName = eventArgs.Text;

            Int64 securityAuthorityId = Int64.Parse ((String) Session[SessionCachePrefix + "SecurityAuthoritySelection.Value"]);

            String securityAuthorityName = (String) Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"];

            PopulateEnvironmentAccessGrid (environmentName, securityAuthorityName);

            Session[SessionCachePrefix + "EnvironmentSelection.Value"] = EnvironmentSelection.SelectedValue;

            Session[SessionCachePrefix + "EnvironmentSelection.Text"] = EnvironmentSelection.SelectedItem.Text;

        }

        protected void SecurityAuthoritySelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            String securityAuthorityName = eventArgs.Text;

            Int64 environmentId = Int64.Parse ((String) Session[SessionCachePrefix + "EnvironmentSelection.Value"]);

            String environmentName = (String) Session[SessionCachePrefix + "EnvironmentSelection.Text"];

            PopulateEnvironmentAccessGrid (environmentName, securityAuthorityName);

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Value"] = SecurityAuthoritySelection.SelectedValue;

            Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"] = SecurityAuthoritySelection.SelectedItem.Text;

        }


        protected Boolean ApplyChanges () {

            String securityGroupId;

            String groupName;

            Boolean success = true;

            try {

                foreach (Telerik.Web.UI.GridEditableItem currentItem in EnvironmentAccessGrid.EditItems) {

                    System.Collections.Hashtable newValues = new Hashtable ();

                    currentItem.ExtractValues (newValues);

                    if (((Boolean) currentItem.SavedOldValues["Is Granted"] != (Boolean) newValues["Is Granted"])
                        || ((Boolean) currentItem.SavedOldValues["Is Denied"] != (Boolean) newValues["Is Denied"])) {


                        securityGroupId = currentItem.KeyValues.Split (',')[0].Split ('\"')[1];

                        groupName = currentItem.KeyValues.Split (',')[1].Split ('\"')[1];


                        System.Diagnostics.Debug.Write (groupName + ": ");

                        System.Diagnostics.Debug.Write ("[" + newValues["Is Granted"] + "] ");

                        System.Diagnostics.Debug.Write ("[" + newValues["Is Denied"] + "] ");

                        System.Diagnostics.Debug.Write (" Old: ");

                        System.Diagnostics.Debug.Write ("[" + currentItem.SavedOldValues["Is Granted"] + "] ");

                        System.Diagnostics.Debug.WriteLine ("[" + currentItem.SavedOldValues["Is Denied"] + "] ");


                        Int64 environmentId = Int64.Parse ((String) Session[SessionCachePrefix + "EnvironmentSelection.Value"]);

                        Int64 securityAuthorityId = Int64.Parse ((String) Session[SessionCachePrefix + "SecurityAuthoritySelection.Value"]);

                        success = success && application.SecurityGroupEnvironmentAccessSave (securityAuthorityId, securityGroupId, environmentId, (Boolean)newValues["Is Granted"], (Boolean)newValues["Is Denied"]);

                    }

                    if (!success) { break; }

                }

                PopulateEnvironmentAccessGrid ((String) Session[SessionCachePrefix + "EnvironmentSelection.Text"], (String) Session[SessionCachePrefix + "SecurityAuthoritySelection.Text"]);


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
