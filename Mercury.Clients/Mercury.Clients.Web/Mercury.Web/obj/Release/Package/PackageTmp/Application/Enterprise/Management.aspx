<%@ Page Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="Management.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Management" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="ConfigurationContentControlHeader" ContentPlaceHolderID="head" runat="server">

</asp:Content>


<asp:Content ID="ConfigurationContentControl" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<div style="display: none;">

    <asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

        <Scripts>

    
        </Scripts>

    </asp:ScriptManagerProxy>

    <Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ManagementTree" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ManagementTree" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ManagementGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ToolbarGeneral" LoadingPanelID="AjaxLoadingPanel" />               
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
        </AjaxSettings>
    
    </Telerik:RadAjaxManagerProxy>
    
</div>


<div id="ConfigurationToolbarStrip" runat="server">

    <Telerik:RadTabStrip ID="ToolbarTabStrip" MultiPageID="ToolbarMultiPage" SelectedIndex="0" Visible="true" runat="server">
    
        <Tabs>
        
            <Telerik:RadTab Text="General" Visible="true" />
            
        </Tabs>
    
    </Telerik:RadTabStrip>
    
    <Telerik:RadMultiPage ID="ToolbarMultiPage" SelectedIndex="0" runat="server">
    
        <Telerik:RadPageView ID="PageGeneral" runat="server">
            
            <Telerik:RadToolBar ID="ToolbarGeneral" OnClientButtonClicked="TitleToolbar_OnClientButtonClicked" runat="server">
    
                <Items>
                
                    <Telerik:RadToolBarButton Text="Add <br /> Authority" Value="AddAuthority" ImagePosition="AboveText" ImageUrl="/Images/Common32/SecurityServer.png" />
                    
                    <Telerik:RadToolBarButton Text="Enterprise <br /> Permissions" Value="ManageEnterprisePermissions" ImagePosition="AboveText" ImageUrl="/Images/Common32/SecurityServer.png" />
               
                    <Telerik:RadToolBarButton Text="Register <br /> Environment" Value="RegisterEnvironment" ImagePosition="AboveText" ImageUrl="/Images/Common32/ServerEnvironmentAdd.png" />

                    <Telerik:RadToolBarButton Text="Environment <br /> Access" Value="ManageEnvironmentAccess" ImagePosition="AboveText" ImageUrl="/Images/Common32/ServerEnvironment.png" />

                    <Telerik:RadToolBarButton Value="Separator1" IsSeparator="true" Visible="false" />
                    
                    <Telerik:RadToolBarButton Text="Environment <br /> Permissions" Value="EnvironmentPermissions" ImagePosition="AboveText" ImageUrl="/Images/Common32/SecurityShieldOk.png" Visible="false" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Role" Value="AddEnvironmentRole" ImagePosition="AboveText" ImageUrl="/Images/Common32/Users.png" Visible="false" />
                    
                    <Telerik:RadToolBarButton Text="Add <br /> Reporting Server" Value="AddEnvironmentReportingServer" ImagePosition="AboveText" ImageUrl="/Images/Common32/ReportingServer.png" Visible="false" />
                    
                </Items>
                
            </Telerik:RadToolBar>
        
        </Telerik:RadPageView>

    </Telerik:RadMultiPage>
    	
</div>

       
    <Telerik:RadSplitter ID="SplitterContainer" Orientation="Vertical" Width="100%" BackColor="White" runat="server">
    
        <Telerik:RadPane ID="SplitterPaneTreeView" Width="300px" Scrolling="Both" BackColor="White" runat="server" >
        
            <Telerik:RadTreeView ID="ManagementTree" OnNodeClick="ManagementTree_OnNodeClick" OnNodeExpand="ManagementTree_OnNodeExpand" OnNodeCollapse="ManagementTree_OnNodeCollapse" BackColor="White" runat="server">
            
                <Nodes></Nodes>
           
            </Telerik:RadTreeView>
        
        </Telerik:RadPane>
        
        <Telerik:RadSplitBar ID="SplitterBar" runat="server" CollapseMode="Both" />
        
        <Telerik:RadPane ID="SplitterPaneGrid" runat="server" Scrolling="Both" BackColor="White">
    
            <Telerik:RadGrid ID="ManagementGrid" AutoGenerateColumns="false" AllowSorting="true" runat="server">
            
                <MasterTableView ClientDataKeyNames="ContextMenu" runat="server" />
            
                <ClientSettings>
                
                    <Selecting AllowRowSelect="true" />    
                    
                    <ClientEvents OnRowContextMenu="ManagementGridContextMenu" />              
                
                </ClientSettings>
            
            </Telerik:RadGrid>
        
        </Telerik:RadPane>
    
    </Telerik:RadSplitter>

    <div id="TelerikWindows" style="display: none">
    
        <Telerik:RadWindowManager ID="TelerikWindowManager" OnClientClose="DialogWindow_OnClose" runat="server">
        
            <Windows>
            
                <Telerik:RadWindow ID="DialogWindow" VisibleOnPageLoad="false" VisibleStatusbar="false" NavigateUrl="~/WindowLoading.aspx" Modal="true" Behavior="Resize,Close" IconUrl="/Images/Common16/Properties.png" runat="server" />
                      
            </Windows>
        
        </Telerik:RadWindowManager>
        
    </div>

    <div id="GridContextMenus" style="display: none">
    
        <Telerik:RadContextMenu id="ContextMenuGridSecurityAuthority" OnClientItemClicked="ContextMenuGridSecurityAuthority_OnClientItemClicked" runat="server">
        
            <Items>
            
                <Telerik:RadMenuItem Text="Delete" ImageUrl="/Images/Common16/Delete.png"   />

                <Telerik:RadMenuItem Text="" Value="Separator1" IsSeparator="true"  />
            
                <Telerik:RadMenuItem Text="Properties" ImageUrl="/Images/Common16/Properties.png"  />
                        
            </Items>
        
        </Telerik:RadContextMenu>
        
        <Telerik:RadContextMenu id="ContextMenuGridSecurityGroup" OnClientItemClicked="ContextMenuGridSecurityGroup_OnClientItemClicked" runat="server">
        
            <Items>
            
                <Telerik:RadMenuItem Text="Properties" ImageUrl="/Images/Common16/Properties.png"  />
                        
            </Items>
        
        </Telerik:RadContextMenu>
        
        <Telerik:RadContextMenu id="ContextMenuGridEnvironment" OnClientItemClicked="ContextMenuGridEnvironment_OnClientItemClicked" runat="server">
    
            <Items>
            
                <Telerik:RadMenuItem Text="Delete" ImageUrl="/Images/Common16/Delete.png"   />

                <Telerik:RadMenuItem Text="" Value="Separator1" IsSeparator="true"  />
            
                <Telerik:RadMenuItem Text="Properties" ImageUrl="/Images/Common16/Properties.png"  />
                        
            </Items>
        
        </Telerik:RadContextMenu>
        
        <Telerik:RadContextMenu id="ContextMenuGridEnvironmentRole" OnClientItemClicked="ContextMenuGridEnvironmentRole_OnClientItemClicked" runat="server">
    
            <Items>

                <Telerik:RadMenuItem Text="Add" />
            
                <Telerik:RadMenuItem Text="Delete" ImageUrl="/Images/Common16/Delete.png"   />

                <Telerik:RadMenuItem Text="" Value="Separator1" IsSeparator="true"  />
            
                <Telerik:RadMenuItem Text="Properties" ImageUrl="/Images/Common16/Properties.png"  />
                        
            </Items>
        
        </Telerik:RadContextMenu>
        
    </div>


<Telerik:RadCodeBlock ID="ClientCode" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', Configuration_Body_OnResize, false); } else { window.attachEvent('onresize', Configuration_Body_OnResize); }

    if (window.addEventListener) { window.addEventListener('load', Configuration_Page_Load, false); } else { window.attachEvent('onload', Configuration_Page_Load); }


    var isConfigurationPainting = false;


    function Configuration_Page_Load() {

        setTimeout('Configuration_Paint()', 125);

        return;

    }


    function Configuration_Paint(forEvent) {

        if (isConfigurationPainting) { return; }

        isConfigurationPainting = true;

        var splitter = $find("<%= SplitterContainer.ClientID %>");

        var container = document.getElementById("ApplicationContent");

        if ((splitter == null) || (container == null)) {

            isConfigurationPainting = false;

            setTimeout('Configuration_Paint()', 100);

            return;
        }


        // TOP FROM THE FULL WINDOW 
        var splitterTop = document.getElementById("<%= SplitterContainer.ClientID %>").offsetTop;

        // MODIFY TOP TO BE RELATIVE TO THE APPLICATION CONTENT CONTROL
        splitterTop = splitterTop - container.offsetTop;


        var adjustedHeight = container.offsetHeight - splitterTop - 1;

        splitter.set_width("100%");

        splitter.set_height(adjustedHeight);

        isConfigurationPainting = false;

        return;

    }

    function Configuration_Body_OnResize(forEvent) {

        Configuration_Paint();

        return;

    }


    function TitleToolbar_OnClientButtonClicked(sender, eventArgs) {

        var buttonClicked = eventArgs.get_item().get_value();

        switch (buttonClicked) {

            case "AddAuthority": OpenDialog("/Application/Enterprise/Windows/SecurityAuthorityProperties.aspx", 850, 650); break;

            case "ManageEnterprisePermissions": OpenDialog("/Application/Enterprise/Windows/EnterprisePermissionManage.aspx", 850, 650); break;

            case "RegisterEnvironment": OpenDialog("/Application/Enterprise/Windows/EnvironmentProperties.aspx", 850, 650); break;

            case "ManageEnvironmentAccess": OpenDialog("/Application/Enterprise/Windows/EnvironmentAccessManage.aspx", 850, 650); break;

            case "EnvironmentPermissions": OpenDialog("", 1000, 800); break;

            case "AddEnvironmentRole":

                var treeView = $find("<%= ManagementTree.ClientID %>");

                if (treeView) {

                    var selectedNode = treeView.get_selectedNode();

                    var selectedNodeValue = selectedNode.get_value();

                    var environmentName = selectedNodeValue;

                    environmentName = environmentName.split('Environments/Environment|')[1];

                    environmentName = environmentName.split('/')[0];

                    OpenDialog("/Application/Enterprise/Windows/EnvironmentRoleProperties.aspx?EnvironmentName=" + environmentName + "&RoleName=", 850, 650);

                }

                break;

            case "AddEnvironmentReportingServer":

                OpenWindow("/Application/Enterprise/PropertyPages/ReportingServer.aspx", 1000, 700); break;

                break;

            default: alert("Unknown General Toolbar Button Clicked: " + buttonClicked);

        }

        return;

    }

    function OpenWindow(url, windowWidth, windowHeight) {

        window.open(url, "_blank", "width=" + windowWidth + ",height=" + windowHeight + ",resizable=1,scrollbars=1,status=1");

        return;

    }

    function OpenDialog(url, dialogWidth, dialogHeight) {

        var windowManager = GetRadWindowManager();

        var radWindow = windowManager.getWindowByName("DialogWindow");

        radWindow.SetWidth(dialogWidth);

        radWindow.SetHeight(dialogHeight);

        radWindow.SetUrl(url);

        radWindow.Show();

        radWindow.Center();

    }

    function DialogWindow_OnClose(forEvent) {

        var windowManager = GetRadWindowManager();

        var radWindow = windowManager.getWindowByName("DialogWindow");

        radWindow.SetUrl("/WindowLoading.aspx");


        var managementTree = $find("<%= ManagementTree.ClientID %>");

        if (managementTree != null) { // TODO: UPDATE V2, ADD SUPPORT FOR MASTER PAGE REFERENCE

            var selectedNode = managementTree.get_selectedNode();

            if (selectedNode != null) {

                selectedNode.select();

            }

        }

        return;

    }


    function ManagementGridContextMenu(sender, eventArgs) {

        var event = eventArgs.get_domEvent();

        var clickedRowIndex = eventArgs.get_itemIndexHierarchical();

        sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[clickedRowIndex].get_element(), true);


        var selectedRow = sender.get_masterTableView().get_dataItems()[clickedRowIndex];

        var contextMenuName = selectedRow.getDataKeyValue("ContextMenu");


        if (contextMenuName != "") {

            // contextMenuName = "ApplicationContentControl_" + "ContextMenu" + contextMenuName;

            contextMenuName = "<%= ContextMenuGridSecurityAuthority.ClientID %>".replace("SecurityAuthority", contextMenuName);

            contextMenuName = contextMenuName.replace("GridGrid", "Grid");

            var contextMenu = $find(contextMenuName);

            if (contextMenu != null) {

                contextMenu.show(event);

            }

            else { alert("Unknown Context Menu: " + contextMenuName); }

        }


        event.cancelBubble = true;

        event.returnValue = false;

        if (event.stopPropagation) {

            event.stopPropagation();

            event.preventDefault();

        }

        return;

    }


    function RibbonBar_SecurityAuthorities_AddAuthority_OnClick(forEvent) {

        OpenDialog("/Application/Enterprise/Windows/SecurityAuthorityProperties.aspx", 850, 650);

        return;

    }

    function OpenWindow_EnterprisePermissionManage(forEvent) {

        OpenDialog("/Application/Enterprise/Windows/EnterprisePermissionManage.aspx", 850, 650);

        return;

    }

    function RibbonBar_Environments_RegisterEnvironment_OnClick(forEvent) {

        OpenDialog("/Application/Enterprise/Windows/EnvironmentProperties.aspx", 850, 650);

        return;

    }

    function OpenWindow_EnvironmentAccessManage(forEvent) {

        OpenDialog("/Application/Enterprise/Windows/EnvironmentAccessManage.aspx", 850, 650);

        return;

    }


    function ContextMenuGridSecurityAuthority_OnClientItemClicked(sender, eventArgs) {

        var dialogUrl;

        var securityAuthorityName = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Name")

        contextMenuItemClicked = eventArgs._item.get_text();

        switch (contextMenuItemClicked) {

            case "Delete":

                dialogUrl = "/Application/Enterprise/Windows/SecurityAuthorityDelete.aspx?SecurityAuthorityName=" + securityAuthorityName;

                OpenDialog(dialogUrl, 400, 325);

                break;

            case "Properties":

                dialogUrl = "/Application/Enterprise/Windows/SecurityAuthorityProperties.aspx?SecurityAuthorityName=" + securityAuthorityName;

                OpenDialog(dialogUrl, 800, 600);

                break;

        }

        return;

    }

    function ContextMenuGridSecurityGroup_OnClientItemClicked(sender, eventArgs) {

        var dialogUrl;

        var securityAuthorityName = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("AuthorityName")

        var securityGroupId = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Id")

        contextMenuItemClicked = eventArgs._item.get_text();

        switch (contextMenuItemClicked) {

            case "Properties":

                dialogUrl = "/Application/Enterprise/Windows/SecurityGroupProperties.aspx?SecurityAuthorityName=" + securityAuthorityName + "&SecurityGroupId=" + securityGroupId;

                OpenDialog(dialogUrl, 800, 600);

                break;

        }

        return;

    }

    function ContextMenuGridEnvironment_OnClientItemClicked(sender, eventArgs) {

        var dialogUrl;

        var environmentName = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Name")

        contextMenuItemClicked = eventArgs._item.get_text();

        switch (contextMenuItemClicked) {

            case "Delete":

                dialogUrl = "/Application/Enterprise/Windows/EnvironmentDelete.aspx?EnvironmentName=" + environmentName;

                OpenDialog(dialogUrl, 400, 325);

                break;

            case "Properties":

                dialogUrl = "/Application/Enterprise/Windows/EnvironmentProperties.aspx?EnvironmentName=" + environmentName;

                OpenDialog(dialogUrl, 800, 600);

                break;

        }

        return;

    }

    function ContextMenuGridEnvironmentRole_OnClientItemClicked(sender, eventArgs) {

        var dialogUrl;

        var environmentName = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("EnvironmentName")

        var roleName = $find("<%= ManagementGrid.ClientID %>").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Name")

        contextMenuItemClicked = eventArgs._item.get_text();

        switch (contextMenuItemClicked) {

            case "Add":

                dialogUrl = "/Application/Enterprise/Windows/EnvironmentRoleProperties.aspx?EnvironmentName=" + environmentName + "&RoleName=";

                OpenDialog(dialogUrl, 800, 600);

                break;

            case "Delete":

                dialogUrl = "/Application/Enterprise/Windows/EnvironmentRoleDelete.aspx?EnvironmentName=" + environmentName + "&RoleName=" + roleName;

                OpenDialog(dialogUrl, 400, 325);

                break;

            case "Properties":

                dialogUrl = "/Application/Enterprise/Windows/EnvironmentRoleProperties.aspx?EnvironmentName=" + environmentName + "&RoleName=" + roleName;

                OpenDialog(dialogUrl, 800, 600);

                break;

        }

        return;

    }


</script>

</Telerik:RadCodeBlock>
        
        
</asp:Content>
