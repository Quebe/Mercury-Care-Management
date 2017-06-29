using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace Mercury.Web.Application.Enterprise {

    public partial class Management : System.Web.UI.Page {

        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }

        #endregion 

        
        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            Response.Cache.SetCacheability (HttpCacheability.NoCache);

            Response.Cache.SetExpires (DateTime.Now);


            if ((!MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterpriseManagement))
                && (!MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityManagement))) 
                { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterpriseManagement)) {

                Page.Title = "Mercury - Enterprise Management [" + MercuryApplication.Session.EnvironmentName + "]";

            }

            else if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityManagement)) {

                Page.Title = "Mercury - Security Management [" + MercuryApplication.Session.EnvironmentName + "]";

            }

            else { Response.Redirect ("/PermissionDenied.aspx", true); return; }



            if ((MercuryApplication != null) && (!Page.IsPostBack)) {

                InitializeToolbar ();

                Initialize_ManagementTree ();

                Session[SessionCachePrefix + "ManagementTreeState"] = ManagementTree.GetXml ();

            }

            else { // POSTBACK

                InitializeToolbar ();

                ManagementTree.LoadXmlString ((String) Session[SessionCachePrefix + "ManagementTreeState"]);

                if (Session[SessionCachePrefix + "ManagementGrid"] != null) {

                    ManagementGrid.DataSource = Session[SessionCachePrefix + "ManagementGrid"];

                }

            }

        }

        #endregion


        #region Control Initializations and Support Functions

        private void InitializeToolbar () {

            ToolbarGeneral.Items.FindItemByValue ("Separator1").Visible = false;

            ToolbarGeneral.Items.FindItemByValue ("EnvironmentPermissions").Visible = false;

            ToolbarGeneral.Items.FindItemByValue ("AddEnvironmentRole").Visible = false;

            return;

        }

        private void InitializeToolbarEnvironment (String forEnvironmentName) {

            ToolbarGeneral.Items.FindItemByValue ("Separator1").Visible = true;

            ToolbarGeneral.Items.FindItemByValue ("EnvironmentPermissions").Visible = false; // TODO: NOT IMPLEMENTED

            ToolbarGeneral.Items.FindItemByValue ("AddEnvironmentRole").Visible = true;

            // ToolbarGeneral.Items.FindItemByValue ("AddEnvironmentReportingServer").Visible = MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ReportingServerManage);

            return;

        }

        protected void Initialize_ManagementTree () {

            Telerik.Web.UI.RadTreeNode parentNode;

            ManagementTree.LoadingStatusPosition = Telerik.Web.UI.TreeViewLoadingStatusPosition.AfterNodeText;

            if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterpriseManagement)) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Enterprise Management";
                parentNode.Value = "EnterpriseRoot";
                parentNode.Category = "EnterpriseRoot";
                parentNode.ImageUrl = "/Images/Common16/Servers.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                ManagementTree.Nodes.Add (parentNode);

            }

            if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityManagement)) {

                parentNode = new Telerik.Web.UI.RadTreeNode ();

                parentNode.Text = "Security Management";
                parentNode.Value = "SecurityRoot";
                parentNode.Category = "SecurityRoot";
                parentNode.ImageUrl = "/Images/Common16/FolderClosedAccounts.png";

                parentNode.ExpandedImageUrl = "/Images/Common16/FolderOpenedAccounts.png";
                parentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                ManagementTree.Nodes.Add (parentNode);

            }

            return;

        }

        protected void Initialize_ManagementGrid () {

            ManagementGrid.MasterTableView.Columns.Clear ();

            ManagementGrid.MasterTableView.DataKeyNames = new String[0];

            ManagementGrid.MasterTableView.ClientDataKeyNames = new String[0];

            ManagementGridAddColumn ("ContextMenu", "ContextMenu", false, false);

            ManagementGrid.MasterTableView.SortExpressions.Clear ();

            ManagementGrid.DataSource = new System.Data.DataTable ();

            ManagementGrid.Rebind ();

            return;

        }

        protected void ManagementGridAddColumn (String columnName, String boundField, Boolean isVisible, Boolean hasImage) {

            Telerik.Web.UI.GridBoundColumn gridColumn;

            gridColumn = new Telerik.Web.UI.GridBoundColumn ();
            ManagementGrid.MasterTableView.Columns.Add (gridColumn);
            ManagementGrid.MasterTableView.Columns[ManagementGrid.MasterTableView.Columns.Count - 1].Visible = isVisible;

            gridColumn.UniqueName = columnName.Replace (" ", "");
            gridColumn.HeaderText = columnName;
            gridColumn.DataField = boundField;
            gridColumn.Visible = isVisible;

            if (!String.IsNullOrEmpty (boundField)) {

                String[] dataKeyNames = new String[(ManagementGrid.MasterTableView.DataKeyNames.Length + 1)];

                ManagementGrid.MasterTableView.DataKeyNames.CopyTo (dataKeyNames, 0);

                dataKeyNames[dataKeyNames.Length - 1] = boundField;

                ManagementGrid.MasterTableView.DataKeyNames = dataKeyNames;


                dataKeyNames = new String[(ManagementGrid.MasterTableView.ClientDataKeyNames.Length + 1)];

                ManagementGrid.MasterTableView.ClientDataKeyNames.CopyTo (dataKeyNames, 0);

                dataKeyNames[dataKeyNames.Length - 1] = boundField;

                ManagementGrid.MasterTableView.ClientDataKeyNames = dataKeyNames;

            }

            return;

        }

        protected System.Data.DataTable CreateDataTable (String columnList) {

            System.Data.DataTable newTable = new System.Data.DataTable ();

            foreach (String currentColumn in columnList.Split (';')) {

                newTable.Columns.Add (currentColumn.Trim ());

            }

            return newTable;

        }

        #endregion


        #region Node Support Methods

        protected String GetNodeType (String nodeValue) {

            String controlType = String.Empty;

            controlType = nodeValue.Split ('/')[nodeValue.Split ('/').Length - 1];

            controlType = controlType.Split ('|')[0];

            return controlType;

        }

        protected String GetNodeId (String nodeValue) {

            String controlId = String.Empty;

            controlId = nodeValue.Split ('/')[nodeValue.Split ('/').Length - 1];

            controlId = controlId.Split ('|')[1];

            return controlId;

        }

        #endregion


        #region Configuration Tree Node Events

        protected void ManagementTreeDisplay_ActiveSessions (Int64 forEnvironmentId) {

            System.Data.DataTable managementGridTable;

            List<Mercury.Server.Application.AuditAuthentication> activeSessions = MercuryApplication.ActiveSessionsAvailable ();

            managementGridTable = CreateDataTable ("ContextMenu;SessionToken;EnvironmentName;SecurityAuthorityName;UserAccountName;UserDisplayName;LogonDate;LastActivityTime;AuthenticationTime");

            foreach (Mercury.Server.Application.AuditAuthentication currentSession in activeSessions) {

                if ((forEnvironmentId == 0) || (currentSession.EnvironmentId == forEnvironmentId)) {

                    managementGridTable.Rows.Add (

                        "ActiveSession",

                        currentSession.SessionToken.ToString (),

                        MercuryApplication.EnvironmentGetNameById (currentSession.EnvironmentId),

                        MercuryApplication.SecurityAuthorityGetNameById (currentSession.SecurityAuthorityId, true),

                        currentSession.UserAccountName,

                        currentSession.UserDisplayName,

                        currentSession.LogonDate.ToString ("MM/dd/yyyy hh:mm:ss"),

                        currentSession.LastActivityTime.ToString ("MM/dd/yyyy hh:mm:ss"),

                        currentSession.AuthenticationTime.ToString ()

                        );

                }

            }

            ManagementGrid.Columns.Clear ();

            ManagementGridAddColumn ("Token", "SessionToken", true, true);

            ManagementGridAddColumn ("Environment", "EnvironmentName", true, true);

            ManagementGridAddColumn ("Security Authority", "SecurityAuthorityName", true, true);

            ManagementGridAddColumn ("User Account", "UserAccountName", true, true);

            ManagementGridAddColumn ("Display Name", "UserDisplayName", true, true);

            ManagementGridAddColumn ("Logon", "LogonDate", true, true);

            ManagementGridAddColumn ("Last Activity", "LastActivityTime", true, true);

            ManagementGridAddColumn ("Authentication Time", "AuthenticationTime", true, true);

            ManagementGrid.DataSource = managementGridTable;

            ManagementGrid.Rebind ();

            return;

        }

        protected void ManagementTree_OnNodeExpand (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            Telerik.Web.UI.RadTreeNode childNode;

            System.Diagnostics.Debug.WriteLine ("Node Clicked Path: " + eventArgs.Node.Value);

            System.Diagnostics.Debug.WriteLine ("Node Clicked Path Depth: " + eventArgs.Node.Value.Split ('/').Length.ToString ());


            if (MercuryApplication == null) { return; }

            ManagementTree.LoadXmlString ((String) Session[SessionCachePrefix + "ManagementTreeState"]);

            Telerik.Web.UI.RadTreeNode expandedNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

            expandedNode.Nodes.Clear ();


            #region Evaluate Expanded Node

            switch (eventArgs.Node.Value.Split ('/')[0]) {

                case "EnterpriseRoot":

                    #region Enterprise Root

                    if (eventArgs.Node.Value.Split ('/').Length == 1) {

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityReview)) {

                            childNode = new Telerik.Web.UI.RadTreeNode ();

                            childNode.Text = "Security Authorities";
                            childNode.Value = "EnterpriseRoot/SecurityAuthorities";
                            childNode.Category = "EnterpriseRoot.SecurityAuthority";
                            childNode.ImageUrl = "/Images/Common16/SecurityServer.png";
                            childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                            expandedNode.Nodes.Add (childNode);

                        }

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterprisePermissionReview)) {

                            childNode = new Telerik.Web.UI.RadTreeNode ();

                            childNode.Text = "Enterprise Permissions";
                            childNode.Value = "EnterpriseRoot/EnterprisePermissions";
                            childNode.Category = "EnterpriseRoot.EnterprisePermission";
                            childNode.ImageUrl = "/Images/Common16/FolderClosedSecurityShieldOk.png";
                            childNode.ExpandedImageUrl = "/Images/Common16/FolderOpenedSecurityShieldOk.png";
                            childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                            expandedNode.Nodes.Add (childNode);

                        }

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentReview)) {

                            childNode = new Telerik.Web.UI.RadTreeNode ();

                            childNode.Text = "Environments";
                            childNode.Value = "EnterpriseRoot/Environments";
                            childNode.Category = "EnterpriseRoot.Environment";
                            childNode.ImageUrl = "/Images/Common16/FolderClosed.png";
                            childNode.ExpandedImageUrl = "/Images/Common16/FolderOpen.png";
                            childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                            expandedNode.Nodes.Add (childNode);

                        }

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ActiveSessionsReview)) {

                            childNode = new Telerik.Web.UI.RadTreeNode ();

                            childNode.Text = "Active Sessions";
                            childNode.Value = "EnterpriseRoot/ActiveSessions";
                            childNode.Category = "EnterpriseRoot.ActiveSession";
                            childNode.ImageUrl = "/Images/Common16/UserAccounts.png";
                            childNode.ExpandedImageUrl = "/Images/Common16/UserAccounts.png";
                            childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                            expandedNode.Nodes.Add (childNode);

                        }

                    }

                    else {

                        switch (eventArgs.Node.Value.Split ('/')[1]) {

                            case "SecurityAuthorities":

                                TreeExpandSecurityAuthorityBranch (eventArgs);

                                break;

                            case "EnterprisePermissions":

                                TreeExpandEnterprisePermissionBranch (eventArgs);

                                break;

                            case "Environments":

                                TreeExpandEnterpriseEnvironmentBranch (eventArgs);

                                break;

                        }

                    }

                    #endregion

                    break; // case "EnterpriseRoot":

                case "SecurityRoot":

                    #region Security Root 

                    String directoryPath = eventArgs.Node.Value.Replace ("SecurityRoot", "{UserRoot}");

                    List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> directoryEntries = MercuryApplication.SecurityAuthorityProviderBrowseDirectory (MercuryApplication.Session.SecurityAuthorityName, directoryPath);

                    foreach (Mercury.Server.Application.SecurityAuthorityDirectoryEntry currentDirectoryEntry in directoryEntries) {

                        if (currentDirectoryEntry.ObjectType == "Organizational Unit") {

                            childNode = new Telerik.Web.UI.RadTreeNode ();

                            childNode.Text = currentDirectoryEntry.Name;
                            childNode.Value = eventArgs.Node.Value + "/" + currentDirectoryEntry.Name;
                            childNode.ImageUrl = "/Images/Common16/FolderClosed.png";
                            childNode.ExpandedImageUrl = "/Images/Common16/FolderOpen.png";
                            childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                            expandedNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                            expandedNode.Nodes.Add (childNode);

                        }

                    }

                    #endregion

                    break;

            }

            #endregion

            expandedNode.Expanded = true;

            while (expandedNode.ParentNode != null) {

                expandedNode = expandedNode.ParentNode;

                expandedNode.Expanded = true;

            }

            Session[SessionCachePrefix + "ManagementTreeState"] = ManagementTree.GetXml ();

            return;

        }

        protected void ManagementTree_OnNodeCollapse (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            ManagementTree.LoadXmlString ((String) Session[SessionCachePrefix + "ManagementTreeState"]);

            Telerik.Web.UI.RadTreeNode collapsedNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

            collapsedNode.Expanded = false;

            Session[SessionCachePrefix + "ManagementTreeState"] = ManagementTree.GetXml ();

            return;

        }

        protected void ManagementTree_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            System.Data.DataTable managementGridTable;

            System.Diagnostics.Debug.WriteLine ("OnNodeClick: " + eventArgs.Node.Value);

            if (MercuryApplication == null) { return; }

            ManagementTree.LoadXmlString ((String) Session[SessionCachePrefix + "ManagementTreeState"]);

            foreach (Telerik.Web.UI.RadTreeNode selectedNode in ManagementTree.SelectedNodes) {

                selectedNode.Selected = false;

            }

            Telerik.Web.UI.RadTreeNode clickedNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

            clickedNode.Selected = true;

            Session[SessionCachePrefix + "ManagementTreeState"] = ManagementTree.GetXml ();

            Initialize_ManagementGrid ();

            System.Data.DataTable gridDataTable = new DataTable ();

            switch (clickedNode.Value.Split ('/')[0].ToLower ()) {

                case "enterpriseroot":

                    #region Enterprise Root

                    if (eventArgs.Node.Value.Split ('/').Length != 1) {

                        switch (eventArgs.Node.Value.Split ('/')[1]) {

                            case "SecurityAuthorities":

                                TreeSelectNodeSecurityAuthorityBranch (eventArgs);

                                break;

                            case "EnterprisePermissions":

                                TreeSelectNodeEnterprisePermissionBranch (eventArgs);

                                break;

                            case "Environments":

                                TreeSelectNodeEnvironmentBranch (eventArgs);

                                break;

                            case "ActiveSessions": // EnterpriseRoot/ActiveSessions

                                #region EnterpriseRoot/ActiveSessions

                                ManagementTreeDisplay_ActiveSessions (0);

                                #endregion

                                break;

                        }

                    }

                    else {

                        // Enterprise Root Selection, Show available child nodes

                        managementGridTable = CreateDataTable ("ContextMenu;Name");

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.SecurityAuthorityReview)) {

                            managementGridTable.Rows.Add ("EnterpriseRootChildNode", "Security Authorities");

                        }

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnterprisePermissionReview)) {

                            managementGridTable.Rows.Add ("EnterpriseRootChildNode", "Enterprise Permissions");

                        }

                        if (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.EnvironmentReview)) {

                            managementGridTable.Rows.Add ("EnterpriseRootChildNode", "Environments");

                        }

                        ManagementGridAddColumn ("Name", "Name", true, true);

                        ManagementGrid.DataSource = managementGridTable;

                        ManagementGrid.Rebind ();

                    }

                    #endregion

                    break; // case "EnterpriseRoot":


                case "securityroot":

                    #region Security Root

                    String directoryPath = eventArgs.Node.Value.Replace ("SecurityRoot", "{UserRoot}");

                    List<Mercury.Server.Application.SecurityAuthorityDirectoryEntry> directoryEntries = MercuryApplication.SecurityAuthorityProviderBrowseDirectory (MercuryApplication.Session.SecurityAuthorityName, directoryPath);

                    ManagementGridAddColumn ("Object Type", "ObjectType", true, false);

                    ManagementGridAddColumn ("Id", "ObjectSid", false, false);

                    ManagementGridAddColumn ("Account Name", "Name", true, false);

                    ManagementGridAddColumn ("Display Name", "DisplayName", true, false);


                    managementGridTable = CreateDataTable ("ContextMenu;ObjectType;ObjectSid;Name;DisplayName");

                    foreach (Mercury.Server.Application.SecurityAuthorityDirectoryEntry currentDirectoryEntry in directoryEntries) {

                        managementGridTable.Rows.Add ("DirectoryEntryProperties",
                            currentDirectoryEntry.ObjectType,
                            currentDirectoryEntry.ObjectSid,
                            currentDirectoryEntry.Name,
                            currentDirectoryEntry.DisplayName
                        );

                        /*
                        if (currentDirectoryEntry.ObjectType == "User") {

                            managementGridTable.Rows.Add ("DirectoryEntryProperties",
                                currentDirectoryEntry.ObjectType,
                                currentDirectoryEntry.ObjectSid,
                                currentDirectoryEntry.Name,
                                currentDirectoryEntry.DisplayName
                            );

                        }
*/
                    }

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    #endregion

                    break;

            }

            

            return;

        }

        #endregion


        #region Tree View Node Branch Expand

        private void TreeExpandSecurityAuthorityBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            InitializeToolbar ();

            List<String> securityAuthorities = new List<string> ();

            Telerik.Web.UI.RadTreeNode parentNode;

            Telerik.Web.UI.RadTreeNode childNode;

            if (eventArgs.Node.Value.Split ('/').Length > 2) {

                // SPECIFIC SECURITY AUTHORITY FUNCTION

            }

            else {

                securityAuthorities.AddRange (MercuryApplication.SecurityAuthorityDictionary (false).Values);

                if (securityAuthorities != null) {

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    foreach (String currentSecurityAuthority in securityAuthorities) { 

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = currentSecurityAuthority;
                        childNode.Value = "EnterpriseRoot/SecurityAuthorities/" + currentSecurityAuthority;
                        childNode.ImageUrl = "/Images/Common16/DirectoryService.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        parentNode.Nodes.Add (childNode);

                    }

                }

            }

            return;

        }

        private void TreeExpandEnterprisePermissionBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {
          
            InitializeToolbar ();

            List<String> permissions;

            Telerik.Web.UI.RadTreeNode parentNode;
            Telerik.Web.UI.RadTreeNode childNode;

            if (eventArgs.Node.Value.Split ('/').Length > 2) {

                // SPECIFIC SUB NODE CLICKED

            }

            else {

                permissions = MercuryApplication.EnterprisePermissionList ();

                if (permissions != null) {

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    foreach (String currentPermission in permissions) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = currentPermission;
                        childNode.Value = "EnterpriseRoot/EnterprisePermissions|" + currentPermission;
                        childNode.ImageUrl = "/Images/Common16/SecurityShieldOk.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        parentNode.Nodes.Add (childNode);

                    }

                }

            }

            return;

        } // private void TreeExpandEnterprisePermissionBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

        private void TreeExpandEnterpriseEnvironmentBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            InitializeToolbar ();

            Telerik.Web.UI.RadTreeNode parentNode;

            Telerik.Web.UI.RadTreeNode childNode;

            String environmentName;

            String nodeType = GetNodeType (eventArgs.Node.Value);


            switch (nodeType) {

                case "Environments":    // EnterpriseRoot/Environments

                    #region EnterpriseRoot/Environments

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    System.Collections.Generic.List<String> environmentList = MercuryApplication.EnvironmentList ();

                    foreach (String currentEnvironment in environmentList) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = currentEnvironment;
                        childNode.Value = parentNode.Value + "/Environment|" + currentEnvironment;
                        childNode.ImageUrl = "/Images/Common16/DatabaseTable.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                        parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                        parentNode.Nodes.Add (childNode);

                    }

                    #endregion

                    break;

                case "Environment":     // EnterpriseRoot/Environments/Environment|[Name]

                    #region EnterpriseRoot/Environments/Environment|[Name]

                    environmentName = GetNodeId (eventArgs.Node.Value);

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    childNode = new Telerik.Web.UI.RadTreeNode ();
                    childNode.Text = "Environment Permissions";
                    childNode.Value = eventArgs.Node.Value + "/EnvironmentPermissions";
                    childNode.ImageUrl = "/Images/Common16/FolderClosedSecurityShieldOk.png";
                    childNode.ExpandedImageUrl = "/Images/Common16/FolderOpenedSecurityShieldOk.png";
                    childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                    parentNode.Nodes.Add (childNode);

                    childNode = new Telerik.Web.UI.RadTreeNode ();
                    childNode.Text = "Roles";
                    childNode.Value = eventArgs.Node.Value + "/EnvironmentRoles";
                    childNode.ImageUrl = "/Images/Common16/FolderClosedAccounts.png";
                    childNode.ExpandedImageUrl = "/Images/Common16/FolderOpenedAccounts.png";
                    childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                    parentNode.Nodes.Add (childNode);

                    //if ((MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ReportingServerReview)) 
                    
                    //    || (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ReportingServerManage))) {

                    //    childNode = new Telerik.Web.UI.RadTreeNode ();

                    //    childNode.Text = "Reporting Servers";
                    //    childNode.Value = eventArgs.Node.Value + "/ReportingServers";
                    //    childNode.ImageUrl = "/Images/Common16/ReportingServer.png";
                    //    childNode.ExpandedImageUrl = "/Images/Common16/ReportingServer.png";
                    //    childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                    //    parentNode.Nodes.Add (childNode);

                    //}

                    if ((MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ActiveSessionsReview)) 
                        
                        || (MercuryApplication.HasEnterprisePermission (Mercury.Server.EnterprisePermissions.ActiveSessionsManage))) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = "Active Sessions";
                        childNode.Value = eventArgs.Node.Value + "/ActiveSessions";
                        childNode.ImageUrl = "/Images/Common16/UserAccounts.png";
                        childNode.ExpandedImageUrl = "/Images/Common16/UserAccounts.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        parentNode.Nodes.Add (childNode);

                    }

                    #endregion

                    break;

                case "EnvironmentPermissions":      // EnterpriseRoot/Environments/Environment[Name]/EnvironmentPermissions

                    #region EnterpriseRoot/Environments/Environment|[Name]/EnvironmentPermissions

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    environmentName = GetNodeId (parentNode.ParentNode.Value);

                    System.Collections.Generic.List<String> environmentPermissionsList;

                    environmentPermissionsList = MercuryApplication.GetEnvironmentPermissionList (environmentName);

                    foreach (String currentPermission in environmentPermissionsList) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = currentPermission;
                        childNode.Value = parentNode.Value + "/EnvironmentPermission|" + currentPermission;
                        childNode.ImageUrl = "/Images/Common16/SecurityShieldOk.png";
                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                        parentNode.Nodes.Add (childNode);

                    }

                    #endregion

                    break;

                case "EnvironmentRoles":     // EnterpriseRoot/Environments/Environment[Name]/EnvironmentRoles

                    #region EnterpriseRoot/Environments/Environment|[Name]/EnvironmentRoles

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    environmentName = GetNodeId (parentNode.ParentNode.Value);

                    System.Collections.Generic.List<String> rolesList;

                    rolesList = MercuryApplication.EnvironmentRoleList  (environmentName);

                    foreach (String currentRole in rolesList) {

                        childNode = new Telerik.Web.UI.RadTreeNode ();

                        childNode.Text = currentRole;

                        childNode.Value = parentNode.Value + "/EnvironmentRole|" + currentRole;

                        childNode.ImageUrl = "/Images/Common16/SecurityGroup.png";

                        childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ServerSide;

                        parentNode.Nodes.Add (childNode);

                    }

                    #endregion

                    break;

                case "EnvironmentRole":

                    #region EnterpriseRoot/Environments/Environment|[Name]/EnvironmentRoles/EnvironmentRole [Name]

                    parentNode = ManagementTree.FindNodeByValue (eventArgs.Node.Value);

                    childNode = new Telerik.Web.UI.RadTreeNode ();

                    childNode.Text = "Role Permissions";

                    childNode.Value = parentNode.Value + "/EnvironmentRolePermissions";

                    childNode.ImageUrl = "/Images/Common16/FolderOpenedSecurityShieldOk.png";

                    childNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

                    parentNode.Nodes.Add (childNode);

                    #endregion

                    break;

            } // nodeType

        } // private void TreeExpandEnterpriseEnvironmentBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

        #endregion


        #region Tree View Node Branch Click

        protected void TreeSelectNodeSecurityAuthorityBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {


            System.Data.DataTable managementGridTable = new DataTable ();


            if (eventArgs.Node.Value.Split ('/').Length > 2) {

                // SPECIFIC SECURITY AUTHORITY FUNCTION

                if (eventArgs.Node.Value.Split ('/').Length == 3) {

                    // SPECIFIC SECURITY AUTHORITY NODE

                    String securityAuthorityName = eventArgs.Node.Value.Split ('/')[2];


                    System.Collections.Generic.Dictionary<String, String> groupDictionary = MercuryApplication.SecurityAuthoritySecurityGroupDictionary (securityAuthorityName);

                    managementGridTable = CreateDataTable ("ContextMenu;AuthorityName;Id;Name");

                    foreach (String currentGroupKey in groupDictionary.Keys) {

                        managementGridTable.Rows.Add ("GridSecurityGroup", securityAuthorityName, currentGroupKey, groupDictionary[currentGroupKey]);

                    }

                    ManagementGridAddColumn ("AuthorityName", "AuthorityName", false, false);

                    ManagementGridAddColumn ("Id", "Id", true, true);

                    ManagementGridAddColumn ("Group Name", "Name", true, false);

                }


            }

            else {

                List<Mercury.Server.Application.SecurityAuthority> authorityCollection = new List<Mercury.Server.Application.SecurityAuthority> ();

                authorityCollection = MercuryApplication.SecurityAuthoritiesAvailable (false);

                managementGridTable = CreateDataTable ("ContextMenu;Name;Type;Server;Domain");

                foreach (Mercury.Server.Application.SecurityAuthority currentAuthority in authorityCollection) {

                    managementGridTable.Rows.Add ("GridSecurityAuthority",
                           currentAuthority.Name,
                           currentAuthority.SecurityAuthorityType,
                           currentAuthority.ServerName,
                           currentAuthority.Domain
                       );

                }

                ManagementGridAddColumn ("Security Authority Name", "Name", true, true);
                ManagementGridAddColumn ("Type", "Type", true, false);
                ManagementGridAddColumn ("Server", "Server", true, false);
                ManagementGridAddColumn ("Domain", "Domain", true, false);

            }

            ManagementGrid.DataSource = managementGridTable;

            ManagementGrid.Rebind ();

        }
        
        protected void TreeSelectNodeEnterprisePermissionBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            List<Mercury.Server.Application.Permission> permissionCollection = new List<Mercury.Server.Application.Permission> ();

            if (eventArgs.Node.Value.Split ('/').Length > 2) {

                // SPECIFIC PERMISSION FUNCTION

            }

            else {

                permissionCollection = MercuryApplication.EnterprisePermissionsAvailable ();

                System.Data.DataTable managementGridTable = CreateDataTable ("ContextMenu;Name");

                foreach (Mercury.Server.Application.Permission currentPermission in permissionCollection) {

                    managementGridTable.Rows.Add ("EnterprisePermission", currentPermission.Name);

                }

                ManagementGridAddColumn ("Permission", "Name", true, true);

                ManagementGrid.DataSource = managementGridTable;

                ManagementGrid.Rebind ();

            }

        }

        protected void TreeSelectNodeEnvironmentBranch (Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            List<Mercury.Server.Application.Environment> environmentCollection;

            List<Mercury.Server.Application.EnvironmentAccess> enviornmentAccessCollection;

            Int64 environmentId;

            String environmentName;

            String roleName;

            System.Data.DataTable managementGridTable;

            System.Collections.Generic.Dictionary<String, String> securityGroupDictionary;

            String securityGroupName = String.Empty;


            switch (GetNodeType (eventArgs.Node.Value)) {

                case "Environments": // EnterpriseRoot/Environments

                    #region EnterpriseRoot/Environments

                    environmentCollection = MercuryApplication.EnvironmentsAvailable ();

                    managementGridTable = CreateDataTable ("ContextMenu;Name");

                    foreach (Mercury.Server.Application.Environment currentEnvironment in environmentCollection) {

                        managementGridTable.Rows.Add ("GridEnvironment", currentEnvironment.Name);

                    }

                    ManagementGridAddColumn ("Environment", "Name", true, true);

                    ManagementGrid.DataSource = managementGridTable;
                    ManagementGrid.Rebind ();

                    #endregion

                    break;

                case "Environment": // EnterpriseRoot/Environments/Environment[Name]

                    #region EnterpriseRoot/Environments/Environment[Name]

                    environmentName = GetNodeId (eventArgs.Node.Value);

                    enviornmentAccessCollection = MercuryApplication.EnvironmentAccessGetByEnvironmentName (environmentName, false);


                    managementGridTable = CreateDataTable ("ContextMenu; EnvironmentId; SecurityAuthorityId; SecurityGroupId; SecurityAuthorityName; SecurityGroupName; IsGranted; IsDenied");

                    foreach (Mercury.Server.Application.EnvironmentAccess currentEnvironmentAccess in enviornmentAccessCollection) {

                        if (Session["EnterpriseManagement.SecurityAuthority." +  MercuryApplication.SecurityAuthorityGetNameById (currentEnvironmentAccess.SecurityAuthorityId, true) + ".SecurityGroupDictionary"] == null) {

                            Session["EnterpriseManagement.SecurityAuthority." + MercuryApplication.SecurityAuthorityGetNameById (currentEnvironmentAccess.SecurityAuthorityId, true) + ".SecurityGroupDictionary"] = MercuryApplication.SecurityAuthoritySecurityGroupDictionary (currentEnvironmentAccess.SecurityAuthorityId);

                        }

                        securityGroupDictionary = (System.Collections.Generic.Dictionary<String, String>)Session["EnterpriseManagement.SecurityAuthority." +
                            
                            MercuryApplication.SecurityAuthorityGetNameById (currentEnvironmentAccess.SecurityAuthorityId, true) + ".SecurityGroupDictionary"];

                        if (securityGroupDictionary != null) {

                            if (securityGroupDictionary.ContainsKey (currentEnvironmentAccess.SecurityGroupId)) {

                                securityGroupName = securityGroupDictionary[currentEnvironmentAccess.SecurityGroupId];

                            }

                        }


                        managementGridTable.Rows.Add ("EnvironmentAccessEntry",
                            currentEnvironmentAccess.EnvironmentId,
                            currentEnvironmentAccess.SecurityAuthorityId,
                            currentEnvironmentAccess.SecurityGroupId,

                            MercuryApplication.SecurityAuthorityGetNameById (currentEnvironmentAccess.SecurityAuthorityId, true),
                            securityGroupName,
                            currentEnvironmentAccess.IsGranted,
                            currentEnvironmentAccess.IsDenied
                        );

                    }

                    ManagementGridAddColumn ("EnvironmentId", "EnvironmentId", false, false);
                    ManagementGridAddColumn ("SecurityAuthorityId", "SecurityAuthorityId", false, false);
                    ManagementGridAddColumn ("SecurityGroupId", "SecurityGroupId", false, false);

                    ManagementGridAddColumn ("Security Authority Name", "SecurityAuthorityName", true, false);
                    ManagementGridAddColumn ("Security Group Name", "SecurityGroupName", true, false);
                    ManagementGridAddColumn ("Is Granted", "IsGranted", true, false);
                    ManagementGridAddColumn ("Is Denied", "IsDenied", true, false);

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    // RibbonBand_AddGroupEnvironment (environmentName);

                    InitializeToolbarEnvironment (environmentName);

                    #endregion

                    break;

                case "EnvironmentPermissions": // EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentPermissions

                    #region EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentPermissions

                    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.Value);

                    List<Mercury.Server.Application.Permission> permissionCollection = new List<Mercury.Server.Application.Permission> ();

                    permissionCollection = MercuryApplication.EnvironmentPermissionsAvailable (environmentName);

                    managementGridTable = CreateDataTable ("ContextMenu;Name");

                    foreach (Mercury.Server.Application.Permission currentPermission in permissionCollection) {

                        managementGridTable.Rows.Add ("EnvironmentPermission", currentPermission.Name);

                    }

                    ManagementGridAddColumn ("Permission", "Name", true, true);

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    InitializeToolbarEnvironment (environmentName);

                    #endregion

                    break;

                case "EnvironmentRoles": // EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles

                    #region EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles

                    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.Value);

                    List<Mercury.Server.Application.EnvironmentRole> roles = MercuryApplication.EnvironmentRolesGet (environmentName);

                    managementGridTable = CreateDataTable ("ContextMenu;EnvironmentName;Name;Description");

                    foreach (Mercury.Server.Application.EnvironmentRole currentRole in roles) {

                        managementGridTable.Rows.Add ("GridEnvironmentRole", environmentName, currentRole.Name, currentRole.Description);

                    }

                    ManagementGridAddColumn ("EnvironmentName", "EnvironmentName", false, false);

                    ManagementGridAddColumn ("Role", "Name", true, true);

                    ManagementGridAddColumn ("Description", "Description", true, true);

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    InitializeToolbarEnvironment (environmentName);

                    #endregion

                    break;

                case "EnvironmentRole": // EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles/EnvironmentRole[Name]
                    
                    #region EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles/EnvironmentRole[Name]

                    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.ParentNode.Value);

                    roleName = GetNodeId (eventArgs.Node.Value);

                    List<Server.Application.EnvironmentRoleMembership> roleMembership = MercuryApplication.EnvironmentRoleGetMembership (environmentName, roleName);

                    managementGridTable = CreateDataTable ("ContextMenu;SecurityAuthorityName;SecurityGroupId;SecurityGroupName");

                    foreach (Server.Application.EnvironmentRoleMembership currentMembership in roleMembership) { 

                        managementGridTable.Rows.Add (String.Empty, currentMembership.SecurityAuthorityName, currentMembership.SecurityGroupId, currentMembership.SecurityGroupName);

                    }

                    ManagementGridAddColumn ("Security Authority", "SecurityAuthorityName", true, false);

                    ManagementGridAddColumn ("Security Group Id", "SecurityGroupId", true, true);

                    ManagementGridAddColumn ("Security Group", "SecurityGroupName", true, true);

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    InitializeToolbarEnvironment (environmentName);

                    #endregion

                    break;

                case "EnvironmentRolePermissions": // EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles/EnvironmentRole[Name]/EnvironmentRolePermissions

                    #region EnterpriseRoot/Environments/EnvironmentName[Name]/EnvironmentRoles/EnvironmentRole[Name]/EnvironmentRolePermissions

                    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.ParentNode.ParentNode.Value);

                    roleName = GetNodeId (eventArgs.Node.ParentNode.Value);

                    List<Server.Application.EnvironmentRolePermission> rolePermissions = MercuryApplication.EnvironmentRoleGetPermissions (environmentName, roleName);

                    managementGridTable = CreateDataTable ("ContextMenu;PermissionId;Permission;IsGranted;IsDenied");



                    List<Mercury.Server.Application.Permission> environmentPermissionCollection = MercuryApplication.EnvironmentPermissionsAvailable  (environmentName);

                    foreach (Server.Application.EnvironmentRolePermission currentRolePermission in rolePermissions) {

                        foreach (Mercury.Server.Application.Permission currentPermission in environmentPermissionCollection) {

                            if (currentRolePermission.PermissionId == currentPermission.Id) {

                                managementGridTable.Rows.Add (String.Empty, currentPermission.Name, currentRolePermission.IsGranted.ToString (), currentRolePermission.IsDenied.ToString ());

                                break;

                            }

                        }

                    }

                    ManagementGridAddColumn ("PermissionId", "PermissionId", true, false);

                    ManagementGridAddColumn ("Permission", "Permission", true, false);

                    ManagementGridAddColumn ("Granted", "IsGranted", true, true);

                    ManagementGridAddColumn ("Denied", "IsDenied", true, true);

                    ManagementGrid.DataSource = managementGridTable;

                    ManagementGrid.Rebind ();

                    InitializeToolbarEnvironment (environmentName);

                    #endregion 

                    break;

                    // TODO: UPDATE V2

                //case "ReportingServers":      // EnterpriseRoot/Environments/Environment[Name]/ReportingServers

                //    #region EnterpriseRoot/Environments/Environment|[Name]/ReportingServers

                //    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.Value);

                //    environmentId = MercuryApplication.EnvironmentGetIdByName (environmentName);


                //    List<Client.Reporting.ReportingServer> reportingServers = MercuryApplication.ReportingServersAvailable (environmentId, true);

                //    managementGridTable = CreateDataTable ("ContextMenu;ReportingServerId;ReportingServerName;ReportingServerType");

                //    foreach (Client.Reporting.ReportingServer currentReportingServer in reportingServers) {

                //        managementGridTable.Rows.Add (

                //            "ReportingServer",

                //            currentReportingServer.ReportingServerId.ToString (),

                //            currentReportingServer.ReportingServerName,

                //            MercuryApplication.EnumerationToString (currentReportingServer.ReportingServerType)

                //            );

                //    }

                //    ManagementGridAddColumn ("Id", "ReportingServerId", true, true);

                //    ManagementGridAddColumn ("Name", "ReportingServerName", true, true);

                //    ManagementGridAddColumn ("Type", "ReportingServerType", true, true);

                //    ManagementGrid.DataSource = managementGridTable;

                //    ManagementGrid.Rebind ();

                //    InitializeToolbarEnvironment (environmentName);

                //    #endregion

                //    break;

                case "ActiveSessions":      // EnterpriseRoot/Environments/Environment[Name]/ActiveSessions

                    #region EnterpriseRoot/Environments/Environment|[Name]/ActiveSessions

                    environmentName = GetNodeId (ManagementTree.FindNodeByValue (eventArgs.Node.Value).ParentNode.Value);

                    environmentId = MercuryApplication.EnvironmentGetIdByName (environmentName);

                    ManagementTreeDisplay_ActiveSessions (environmentId);

                    #endregion

                    break;

            } // switch (NodeType)

        }
        
        #endregion


        #region Context Menus - Environment Access
/*
        protected void ContextMenu_EnvironmentAccess_OnItemClick (Object sender, Telerik.Web.UI.RadMenuEventArgs eventArgs) {

            Telerik.Web.UI.RadMenuItem itemClicked = eventArgs.Item;

            System.Diagnostics.Debug.WriteLine (itemClicked.Text);

            Int32 selectedIndex = Int32.Parse (ManagementGrid.SelectedIndexes[0]);

            System.Data.DataTable accessTable = (System.Data.DataTable) ManagementGrid.MasterTableView.DataSource;


            Int64 environmentId = Int64.Parse ((String) accessTable.Rows[selectedIndex]["EnvironmentId"]);

            Int64 securityAuthorityId = Int64.Parse ((String) accessTable.Rows[selectedIndex]["SecurityAuthorityId"]);

            String securityGroupId = (String) accessTable.Rows[selectedIndex]["SecurityGroupId"];


            switch (itemClicked.Value) {

                case "GrantAccess": application.SetSecurityGroupEnvironmentAccess (securityAuthorityId, securityGroupId, environmentId, true, false); break;

                case "DenyAccess": application.SetSecurityGroupEnvironmentAccess (securityAuthorityId, securityGroupId, environmentId, false, true); break;

                case "RemoveAccess": application.SetSecurityGroupEnvironmentAccess (securityAuthorityId, securityGroupId, environmentId, false, false); break;

            }


            Telerik.Web.UI.RadTreeNodeEventArgs nodeClickEventArgs = new Telerik.Web.UI.RadTreeNodeEventArgs ();

            nodeClickEventArgs.Node = ManagementTree.SelectedNode;

            ManagementTree_NodeClick (sender, nodeClickEventArgs);

        }
*/
        #endregion


    }

}
