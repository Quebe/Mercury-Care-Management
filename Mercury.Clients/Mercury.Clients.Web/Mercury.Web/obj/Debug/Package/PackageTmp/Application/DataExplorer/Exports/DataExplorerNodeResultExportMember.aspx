<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataExplorerNodeResultExportMember.aspx.cs" Inherits="Mercury.Web.Application.DataExplorer.Exports.DataExplorerNodeResultExportMember" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Mercury Care Management</title>
    
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />

    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />
    
    <link rel="Stylesheet" href="/Styles/PropertyPage.css" type="text/css" />

    <link rel="Stylesheet" href="/Styles/RadTabStripBasic.css" type="text/css" />
    
    <style type="text/css">
    
        html { overflow: hidden; }
    
    </style>

</head>


<body style="margin: 0px;" class="TextNormal BackgroundColorLight">

<form id="form1" runat="server"><div>

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" ScriptMode="Release" runat="Server">

        <Scripts>
        
        </Scripts>
    
    </asp:ScriptManager>

    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="DataExplorerTreeView">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerNodeResultsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="DataExplorerNodeResultsGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerNodeResultsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

        </AjaxSettings>
        
        <ClientEvents OnRequestStart="TelerikAjaxManager_OnRequestStart" OnResponseEnd="TelerikAjaxManager_OnResponseEnd" />

    </Telerik:RadAjaxManager>

    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
    
</div>


<div id="ContentContainer" style="padding: .125in;" runat="server">

    <div id="DefinitionResultsContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White;">
                
        <Telerik:RadSplitter ID="OuterSplitter" Width="100%" Height="100%" BackColor="White" Orientation="Horizontal" runat="server">
                    
            <Telerik:RadPane ID="DataExplorerTreeViewPane" Height="50%" runat="server">
                    
                <div style="width: 100%; height: 100%; overflow: hidden;">
                
                    <Telerik:RadTreeView ID="DataExplorerTreeView" Width="100%" Height="100%" CheckBoxes="true" BorderColor="Black" BorderWidth="1" MultipleSelect="true" EnableDragAndDrop="false" EnableDragAndDropBetweenNodes="false"                        
                    
                        OnNodeCheck="DataExplorerTreeView_OnNodeCheck"

                        runat="server">

                    </Telerik:RadTreeView>
                            
                </div>

            </Telerik:RadPane>
            
            <Telerik:RadPane ID="DataResultsGridPane" Height="50%" Scrolling="None" Width="100%" BackColor="White" runat="server">
            
                <Telerik:RadGrid ID="DataExplorerNodeResultsGrid" Width="100%" Height="100%"
        
                        AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="false" 
            
                        AllowFilteringByColumn="false" EnableHeaderContextFilterMenu="false" EnableHeaderContextMenu="false"

                        AllowPaging="true" AllowCustomPaging="true" AllowAutomaticInserts="false"
            
                        OnNeedDataSource="DataExplorerNodeResultsGrid_OnNeedDataSource"

                        OnGridExporting="DataExplorerNodeResultsGrid_OnGridExporting"

                        OnItemCommand="DataExplorerNodeResultsGrid_OnItemCommand"

                        runat="server">

                    <MasterTableView IsFilterItemExpanded="false" TableLayout="Fixed" PageSize="10" CommandItemDisplay="Top" UseAllDataFields="false" Width="100%">

                        <CommandItemTemplate>
                        
                            <div style="padding: 8px;">

                                <asp:LinkButton ID="GridExportToExcel" CommandName="CustomExportToExcel" runat="server">
                                
                                    <img src="/Images/Common16/ExportToExcel.png" alt="Export to Excel" style="border: 0px;" />

                                    <span style="padding-left: 4px; padding-right: 4px;">Export to Excel</span>
                                
                                </asp:LinkButton>
                                
                                <asp:LinkButton ID="GridCreateMailings" CommandName="CreateMailings" runat="server">
                                
                                    <img src="/Images/Common16/Address.png" alt="Create Mailings" style="border: 0px;" />

                                    <span style="padding-left: 4px; padding-right: 4px;">Create Mailings</span>
                                
                                </asp:LinkButton>

                            </div>
                            
                        </CommandItemTemplate>

                        <Columns>
                                    
                        </Columns>

                    </MasterTableView>
            
                    <ClientSettings EnableRowHoverStyle="true">          

                        <Selecting AllowRowSelect="true" />

                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                
                    </ClientSettings>
                                
                    <PagerStyle AlwaysVisible="true" />

                </Telerik:RadGrid>

            </Telerik:RadPane>

        </Telerik:RadSplitter>

    </div>

</div>                

<Telerik:RadCodeBlock ID="TourCodeBlock" runat="server">

<script type="text/javascript">

if (window.addEventListener) { window.addEventListener('resize', DataExplorer_Body_OnResize, false); } else { window.attachEvent('onresize', DataExplorer_Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', DataExplorer_Page_Load, false); } else { window.attachEvent('onload', DataExplorer_Page_Load); }


var isDataExplorerPainting = false;


function DataExplorer_Page_Load() {

    setTimeout('DataExplorer_OnPaint()', 250);

    return;

}

function DataExplorer_OnPaint(forEvent) {

    if (isDataExplorerPainting) { return; }

    isDataExplorerPainting = true;

    

    var contentContainer = document.getElementById("<%= ContentContainer.ClientID %>");


    var splitter = $find("<%= OuterSplitter.ClientID %>");

    var treeView = $find("<%= DataExplorerTreeView.ClientID %>");

    var grid = $find("<%= DataExplorerNodeResultsGrid.ClientID %>");
    
    if ((splitter == null) || (contentContainer == null)) {

        isDataExplorerPainting = false;

        setTimeout('DataExplorer_OnPaint ()', 100);
        
        return;
    }

    
    // GET AVAILABLE WINDOW WIDTH
    if (window.innerWidth) { windowWidth = window.innerWidth; } else { windowWidth = document.documentElement.clientWidth; }

    // GET AVAILABLE WINDOW HEIGHT
    if (window.innerHeight) { windowHeight = window.innerHeight; } else { windowHeight = document.documentElement.clientHeight; }
    
    
    var marginHeight = 26;

    availableWidth = windowWidth - 0;

    availableHeight = windowHeight - 0;

    if (availableHeight < 1) { availableHeight = 1; }

    contentContainer.style.height = availableHeight - (marginHeight) + "px";


    availableHeight = contentContainer.offsetHeight - (marginHeight * 1);

    if (availableHeight < 1) { availableHeight = 1; }

    document.getElementById("DefinitionResultsContainer").style.height = availableHeight + "px";
    

    splitter.set_width("100%");

    splitter.set_height("100%");


    document.getElementById("DataExplorerTreeView").style.height = document.getElementById("RAD_SPLITTER_PANE_CONTENT_DataExplorerTreeViewPane").offsetHeight;

    availableHeight = document.getElementById("RAD_SPLITTER_PANE_CONTENT_DataResultsGridPane").offsetHeight;

    if (document.getElementById("DataExplorerNodeResultsGrid_ctl00_TopPager") != null) { availableHeight = availableHeight - document.getElementById("DataExplorerNodeResultsGrid_ctl00_TopPager").offsetHeight; }

    if (document.getElementById("DataExplorerNodeResultsGrid_GridHeader") != null) { availableHeight = availableHeight - document.getElementById("DataExplorerNodeResultsGrid_GridHeader").offsetHeight; }

    if (document.getElementById("DataExplorerNodeResultsGrid_ctl00_Pager") != null) { availableHeight = availableHeight - document.getElementById("DataExplorerNodeResultsGrid_ctl00_Pager").offsetHeight; }

    document.getElementById("DataExplorerNodeResultsGrid_GridData").style.height = availableHeight + "px";

    //DataExplorerNodeResultsGridPanel

    // DataExplorerNodeResultsGrid

    // DataExplorerNodeResultsGrid_ctl00_TopPager

    // DataExplorerNodeResultsGrid_GridHeader

    // DataExplorerNodeResultsGrid_GridData

    // DataExplorerNodeResultsGrid_ctl00_Pager 

    isDataExplorerPainting = false;

    return;

}


function DataExplorer_Body_OnResize(forEvent) {

    DataExplorer_OnPaint(forEvent);

    setTimeout('DataExplorer_OnPaint()', 250);

    return;

}

function TelerikAjaxManager_OnRequestStart(sender, e) {

    if (e.EventTarget.toString().indexOf("ExportToExcel") > -1) { e.EnableAjax = false; }

    return;

}

function TelerikAjaxManager_OnResponseEnd(sender, e) {

    DataExplorer_Body_OnResize();

    return;

}

</script>

</Telerik:RadCodeBlock>

</div></form>

</body>

</html>