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


    var splitter = $find("ctl00_ApplicationContentControl_SplitterContainer");

    var container = document.getElementById("ApplicationContent");

    if ((splitter == null) || (container == null)) {

        isConfigurationPainting = false;

        setTimeout('Configuration_Paint()', 100);

        return;
    }


    // TOP FROM THE FULL WINDOW 
    var splitterTop = document.getElementById("ctl00_ApplicationContentControl_SplitterContainer").offsetTop;

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

    if (windowManager != null) {

        var radWindow = windowManager.getWindowByName("DialogWindow");

        radWindow.SetUrl("/WindowLoading.aspx");

    }


    var configurationTree = $find("ConfigurationTree");

    if (configurationTree != null) {

        var selectedNode = configurationTree.get_selectedNode();

        if (selectedNode != null) {

            selectedNode.select();

        }

    }

    return;

}


function TitleToolbar_OnClientButtonClicked(sender, eventArgs) {

    var buttonClicked = eventArgs.get_item().get_value();

    switch (buttonClicked) {

        case "FormDesigner": window.location = "/Application/Forms/FormDesigner/FormDesigner.aspx"; break;

        case "AddSingleton": OpenWindow("/Application/Configuration/PropertyPages/ServiceSingleton.aspx", 1000, 700); break;

        case "AddSet": OpenWindow("/Application/Configuration/PropertyPages/ServiceSet.aspx", 1000, 700); break;


        case "AddPopulation": OpenWindow("/Application/Configuration/PropertyPages/Population.aspx", 1000, 800); break;


        case "ConfigurationImportNcqaNdc": OpenWindow("/Application/Configuration/PropertyPages/ConfigurationImportNcqaNdc.aspx", 1000, 800); break;

        default:

            var objectType = buttonClicked.replace("Add", "");

            OpenWindow("/Application/Configuration/PropertyPages/" + objectType + ".aspx", 1000, 700);

            break;

    }

    return;

}


function ConfigurationGridContextMenu(sender, eventArgs) {

    var event = eventArgs.get_domEvent();

    var clickedRowIndex = eventArgs.get_itemIndexHierarchical();

    sender.get_masterTableView().selectItem(sender.get_masterTableView().get_dataItems()[clickedRowIndex].get_element(), true);


    var selectedRow = sender.get_masterTableView().get_dataItems()[clickedRowIndex];

    var contextMenuName = selectedRow.getDataKeyValue("ObjectType");


    var contextMenu = null;

    switch (contextMenuName) {

        default:

            contextMenu = $find("ctl00_ApplicationContentControl_ContextMenuConfigurationObject");
            
            break;

    }


    if (contextMenu != null) { contextMenu.show(event); }

    else { alert("Unknown Context Menu: " + contextMenuName); }


    event.cancelBubble = true;

    event.returnValue = false;

    if (event.stopPropagation) {

        event.stopPropagation();

        event.preventDefault();

    }

    return;

}

function ContextMenuConfigurationObject_OnClientItemClicked(sender, eventArgs) {

    var configurationGrid = $find ("ctl00_ApplicationContentControl_ConfigurationGrid").get_masterTableView();

    var selectedObject = configurationGrid.get_selectedItems()[0];

    var objectType = selectedObject.getDataKeyValue("ObjectType");

    var objectId = selectedObject.getDataKeyValue("Id");

    var contextMenuItemClicked = eventArgs._item.get_text();

    var propertiesUrl;

    var exportUrl;


    switch (contextMenuItemClicked) {

        case "Copy":

            propertiesUrl = "/Application/Configuration/PropertyPages/" + objectType + ".aspx?Copy" + objectType + "Id=" + objectId;

            OpenWindow(propertiesUrl, 1000, 700);

            break;

        case "Export":

            switch (objectType) {

                default:

                    exportUrl = "/Application/Configuration/PropertyPages/ConfigurationExport.aspx?ConfigurationType=" + objectType + "&ConfigurationId=" + objectId;

                    OpenWindow(exportUrl, 1000, 700);

                    break;

            }

            break;

        case "Properties":

            switch (objectType) {

                case "Form":

                    propertiesUrl = "/Application/Forms/FormDesigner/FormDesigner.aspx?FormId=" + objectId;

                    OpenWindow(propertiesUrl, 1000, 700);

                    break;

                case "Service":

                    var serviceType = selectedObject.getDataKeyValue("ServiceType");

                    propertiesUrl = "/Application/Configuration/PropertyPages/Service" + serviceType + ".aspx?" + objectType + "Id=" + objectId;

                    OpenWindow(propertiesUrl, 1000, 700);

                    break;

                case "Population":

                    propertiesUrl = "/Application/Configuration/PropertyPages/" + objectType + ".aspx?" + objectType + "Id=" + objectId;

                    OpenWindow(propertiesUrl, 1000, 800);

                    break;

                default:

                    propertiesUrl = "/Application/Configuration/PropertyPages/" + objectType + ".aspx?" + objectType + "Id=" + objectId;

                    OpenWindow(propertiesUrl, 1000, 700);

                    break;

            }

            break;

    }

    return;

}



function ContextMenuForm_OnClientItemClicked(sender, eventArgs) {

    var dialogUrl;

    var formId = $find("ConfigurationGrid").get_masterTableView().get_selectedItems()[0].getDataKeyValue("Id")

    contextMenuItemClicked = eventArgs._item.get_text();

    switch (contextMenuItemClicked) {

        case "Delete":

            dialogUrl = "/Application/Configuration/Windows/FormDelete.aspx?FormId=" + formId;

            OpenDialog(dialogUrl, 400, 325);

            break;

        case "Export":

            window.open("/Application/Configuration/Windows/ConfigurationExport.aspx?ConfigurationType=Form&ConfigurationId=" + formId);

            break;

        case "Design":

            window.location = "/Application/Forms/FormDesigner/FormDesigner.aspx?FormId=" + formId;

            break;

    }

    return;

}





