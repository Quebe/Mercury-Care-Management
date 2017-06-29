if (window.addEventListener) { window.addEventListener('resize', Configuration_Body_OnResize, false); } else { window.attachEvent('onresize', Configuration_Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', Configuration_Page_Load, false); } else { window.attachEvent('onload', Configuration_Page_Load); }


var isConfigurationPainting = false;


function Configuration_Page_Load() {

    setTimeout('Configuration_Paint()', 250);

    return;

}


function Configuration_Paint(forEvent) {

    if (isConfigurationPainting) { return; }

    isConfigurationPainting = true;


    var splitter = $find("ApplicationContentControl_SplitterContainer");

    var container = document.getElementById("ApplicationContent");

    if ((splitter == null) || (container == null)) {

        isConfigurationPainting = false;

        setTimeout('Configuration_Paint()', 100);

        return;
    }


    // TOP FROM THE FULL WINDOW 
    var splitterTop = document.getElementById("ApplicationContentControl_SplitterContainer").offsetTop;

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

    // setTimeout('Configuration_Paint()', 250);

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

            var treeView = $find("ApplicationContentControl_ManagementTree");

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

function OpenDialog (url, dialogWidth, dialogHeight) {

    var windowManager = GetRadWindowManager ();
    
    var radWindow = windowManager.getWindowByName ("DialogWindow");
    
    radWindow.SetWidth (dialogWidth);
    
    radWindow.SetHeight (dialogHeight);
    
    radWindow.SetUrl (url);
    
    radWindow.Show ();

    radWindow.Center ();
    
}

function DialogWindow_OnClose (forEvent) {

    var windowManager = GetRadWindowManager ();
    
    var radWindow = windowManager.getWindowByName ("DialogWindow");
    
    radWindow.SetUrl ("/WindowLoading.aspx");


    var managementTree = $find("ApplicationContentControl_ManagementTree");

    if (managementTree != null) { // TODO: UPDATE V2, ADD SUPPORT FOR MASTER PAGE REFERENCE

        var selectedNode = managementTree.get_selectedNode();

        if (selectedNode != null) {

            selectedNode.select();

        }

    }

    return;

}


function ManagementGridContextMenu (sender, eventArgs) {

    var event = eventArgs.get_domEvent ();

    var clickedRowIndex = eventArgs.get_itemIndexHierarchical ();
    
    sender.get_masterTableView().selectItem (sender.get_masterTableView().get_dataItems()[clickedRowIndex].get_element (), true);
    
    
    var selectedRow = sender.get_masterTableView().get_dataItems ()[clickedRowIndex];
    
    var contextMenuName = selectedRow.getDataKeyValue ("ContextMenu");
    
    
    if (contextMenuName != "") { 
    
        contextMenuName = "ApplicationContentControl_" + "ContextMenu" + contextMenuName;
    
        var contextMenu = $find (contextMenuName);
        
        if (contextMenu != null) { 
        
            contextMenu.show (event);
            
        }
        
        else { alert ("Unknown Context Menu: " + contextMenuName); }      
    
    }
    
    
    event.cancelBubble = true;
    
    event.returnValue = false;
    
    if (event.stopPropagation) { 
    
        event.stopPropagation ();
        
        event.preventDefault ();
        
   }
       
    return;     

}


function RibbonBar_SecurityAuthorities_AddAuthority_OnClick (forEvent) {

    OpenDialog ("/Application/Enterprise/Windows/SecurityAuthorityProperties.aspx", 850, 650);
    
    return;
    
}

function OpenWindow_EnterprisePermissionManage (forEvent) {

    OpenDialog ("/Application/Enterprise/Windows/EnterprisePermissionManage.aspx", 850, 650);
    
    return;

}

function RibbonBar_Environments_RegisterEnvironment_OnClick (forEvent) {

    OpenDialog ("/Application/Enterprise/Windows/EnvironmentProperties.aspx", 850, 650);
    
    return;

}

function OpenWindow_EnvironmentAccessManage (forEvent) {
    
    OpenDialog ("/Application/Enterprise/Windows/EnvironmentAccessManage.aspx", 850, 650);

    return;

}


function ContextMenuGridSecurityAuthority_OnClientItemClicked (sender, eventArgs) { 

    var dialogUrl;

    var securityAuthorityName = $find("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Name")

    contextMenuItemClicked = eventArgs._item.get_text ();

    switch (contextMenuItemClicked ) {
    
        case "Delete":
        
            dialogUrl ="/Application/Enterprise/Windows/SecurityAuthorityDelete.aspx?SecurityAuthorityName=" + securityAuthorityName;
        
            OpenDialog (dialogUrl, 400, 325);
          	 
            break;
    
        case "Properties":
        
            dialogUrl =  "/Application/Enterprise/Windows/SecurityAuthorityProperties.aspx?SecurityAuthorityName=" + securityAuthorityName;
            
            OpenDialog (dialogUrl, 800, 600);
            
            break;
            
    }
    
    return;

}

function ContextMenuGridSecurityGroup_OnClientItemClicked (sender, eventArgs) { 

    var dialogUrl;

    var securityAuthorityName = $find ("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue ("AuthorityName")

    var securityGroupId = $find ("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue ("Id")

    contextMenuItemClicked = eventArgs._item.get_text ();

    switch (contextMenuItemClicked ) {
    
        case "Properties":
        
            dialogUrl =  "/Application/Enterprise/Windows/SecurityGroupProperties.aspx?SecurityAuthorityName=" + securityAuthorityName + "&SecurityGroupId=" + securityGroupId;
            
            OpenDialog (dialogUrl, 800, 600);
            
            break;
            
    }
                        
    return;

}

function ContextMenuGridEnvironment_OnClientItemClicked (sender, eventArgs) { 

    var dialogUrl;

    var environmentName = $find ("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue ("Name")

    contextMenuItemClicked = eventArgs._item.get_text ();

    switch (contextMenuItemClicked ) {
    
        case "Delete":
        
            dialogUrl ="/Application/Enterprise/Windows/EnvironmentDelete.aspx?EnvironmentName=" + environmentName;
        
            OpenDialog (dialogUrl, 400, 325);
          	 
            break;
    
        case "Properties":
        
            dialogUrl =  "/Application/Enterprise/Windows/EnvironmentProperties.aspx?EnvironmentName=" + environmentName;
            
            OpenDialog (dialogUrl, 800, 600);
            
            break;
            
    }
    
    return;

}

function ContextMenuGridEnvironmentRole_OnClientItemClicked (sender, eventArgs) { 

    var dialogUrl;

    var environmentName = $find ("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue ("EnvironmentName")
    
    var roleName = $find ("ApplicationContentControl_ManagementGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue ("Name")

    contextMenuItemClicked = eventArgs._item.get_text ();

    switch (contextMenuItemClicked ) {
    
        case "Add": 

            dialogUrl ="/Application/Enterprise/Windows/EnvironmentRoleProperties.aspx?EnvironmentName=" + environmentName + "&RoleName=";
        
            OpenDialog (dialogUrl, 800, 600);
          	 
            break;
    
        case "Delete":
        
            dialogUrl ="/Application/Enterprise/Windows/EnvironmentRoleDelete.aspx?EnvironmentName=" + environmentName + "&RoleName=" + roleName;
        
            OpenDialog (dialogUrl, 400, 325);
          	 
            break;
    
        case "Properties":
        
            dialogUrl =  "/Application/Enterprise/Windows/EnvironmentRoleProperties.aspx?EnvironmentName=" + environmentName + "&RoleName=" + roleName;
            
            OpenDialog (dialogUrl, 800, 600);
            
            break;
            
    }
    
    return;

}
