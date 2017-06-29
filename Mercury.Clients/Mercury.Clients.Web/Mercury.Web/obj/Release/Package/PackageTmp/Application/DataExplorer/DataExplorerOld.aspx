<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="DataExplorerOld.aspx.cs" Inherits="Mercury.Web.Application.DataExplorer.DataExplorerOld" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="DataExplorerHeader" ContentPlaceHolderID="head" runat="server">

    <link rel="Stylesheet" href="/Styles/PropertyPage.css" type="text/css" />
    
    <link rel="Stylesheet" href="/Styles/RadTabStripBasic.css" type="text/css" />

</asp:Content>

<asp:Content ID="DataExplorerContentControl" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>
   
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="DataExplorerContent" style="padding: .125in;">

    <!-- DATA EXPLORER SELECTION/DEFINITION (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding-bottom: .125in">
    
        <!-- DATA EXPLORER HEADER (BEGIN) -->
        
        <div id="DataExplorerTitleBar">

            <table width="100%" cellpadding="0" cellspacing="0">
    
                <tr class="BackgroundColorDark" style="height: 36px;">

                    <td style="padding-left: .125in">
            
                        <img src="../../Images/Common16/Database.png" alt="Member Case" />

                    </td>

                    <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                        <a id="ApplicationTitle" class="NoDecoration HoverTextWhiteBold" href="/PermissionDenied.aspx" target="_blank" style="color: White; font-weight: bold; white-space: nowrap" runat="server">Data Explorer: New</a>
                
                    </td>

                    <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="javascript:Tour();" style="white-space: nowrap; font-weight: bold; text-align: center;">Open</a></td>

                </tr>
     
                <tr><td colspan="6" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
             </table>

        </div>

        <div id="NavigationBar" class="BackgroundColorComplementLight" style="display: block;">

            <Telerik:RadTabStrip ID="NavigationStrip" Skin="" EnableEmbeddedSkins="False" 

                MultiPageID="ContentMultiPage" SelectedIndex="0"  CssClass="RadTabStripBasic" 

                runat="server">
       
                <Tabs>
            
                    <Telerik:RadTab Text="General" Selected="True" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>
                
                    <Telerik:RadTab Text="Definition" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

                    <Telerik:RadTab Text="Results" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

                </Tabs>

            </Telerik:RadTabStrip>

            <div class="BackgroundColorComplementDark" style="height: 1px;"></div>

        </div>

        <!-- DATE EXPLORER HEADER ( END ) -->
        
    </div>
    
    <div style="height: .125in;">&nbsp</div>
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table width="100%" cellpadding="0" cellspacing="0">
        
            <tr>

                <td style="white-space: nowrap;">Data Explorer Selection</td>

                <td></td>

                <td colspan="2" style="text-align: right">
                
                        <asp:LinkButton ID="LinkButton6" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(new)</asp:LinkButton>
                
                        <asp:LinkButton ID="LinkButton3" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(open)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton5" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(save)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton4" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(save as)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton1" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(query)</asp:LinkButton>

                </td>

            </tr>

            <tr style="height: 10px;"><td>&nbsp</td></tr>
            
        </table>
        
        <!-- DATA EXPLORER PROPERTIES (BEGIN) -->
    
        <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
            <table width="100%" cellpadding="0" cellspacing="0">
        
                <tr>

                    <td style="white-space: nowrap;">Data Explorer Properties</td>

                    <td></td>

                    <td style="text-align: right">
                
                        <asp:LinkButton ID="DataExplorerPropertiesToggleVisibility" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(collapse)</asp:LinkButton>
                
                    </td>

                </tr>

                <tr><td colspan="3" >
                                
                    <div id="DataExplorerPropertiesContent" runat="server">
        
                        <div class="PropertyPageSectionTitle">Name and Description</div>

                        <table width="100%">

                            <tr>

                                <td style="width: 10%; padding: 4px;">Name:</td>
                    
                                <td style="width: 50%; padding: 4px;"><Telerik:RadTextBox ID="FaxServerName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>

                                <td style="width: 175px; white-space: nowrap;">

                                    <asp:RadioButtonList ID="DataExplorerIsPublic" RepeatDirection="Horizontal" runat="server">
                                
                                        <asp:ListItem Text="Public" Selected="False" Value="1"></asp:ListItem>

                                        <asp:ListItem Text="Private, Owner:" Selected="True" Value="0"></asp:ListItem>
                                                                    
                                    </asp:RadioButtonList>

                                </td>

                                <td style="text-align: left;">(owner)</td>

                            </tr>
                    
                            <tr>
                            
                                <td valign="top" style="padding: 4px;">Description:</td>

                                <td colspan="3" style="padding: 4px;"><Telerik:RadTextBox ID="FaxServerDescription" Width="100%" Rows="2" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></td>
                                
                            </tr>

                        </table>

                    </div>
                                    
                </td></tr>

            </table>
        
        </div>

        <!-- DATA EXPLORER PROPERTIES ( END ) -->
    
    
        <div style="height: .125in;">&nbsp</div>


        <!-- DATA EXPLORER DEFINITION (BEGIN) -->
    
        <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
            <table width="100%" cellpadding="0" cellspacing="0">
        
                <tr>

                    <td style="white-space: nowrap;">Data Explorer Definition</td>

                    <td></td>

                    <td style="text-align: right">
                
                        <asp:LinkButton ID="DataExplorerDefinitionToggleVisibility" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(collapse)</asp:LinkButton>
                
                    </td>

                </tr>
                
                <tr><td colspan="3" > 
                
                    <div id="DataExplorerDefinitionContent" runat="server">

                        <table width="100%" cellpadding="0" cellspacing="0"><tr>
                        
                            <td valign="top" style="width: 50%">
                            
                                <Telerik:RadTreeView ID="DataExplorerDefinitionTree" AllowNodeEditing="false" ShowLineImages="true" runat="server">

                                    <Nodes>
                                    
                                        <Telerik:RadTreeNode Text="Global Variables" ImageUrl="/Images/Common16/Variable.png" ContextMenuID="ContextMenuDataExplorerGlobalVariable">
                                        
                                            <Nodes>
                                            
                                                <Telerik:RadTreeNode Text="Anchor Date" ImageUrl="/Images/Common16/Calendar.png" runat="server"></Telerik:RadTreeNode>
                                            
                                                <Telerik:RadTreeNode Text="Anchor Date" ImageUrl="/Images/Common16/Calendar.png" runat="server"></Telerik:RadTreeNode>

                                                <Telerik:RadTreeNode Text="Anchor Date" ImageUrl="/Images/Common16/Calendar.png" runat="server"></Telerik:RadTreeNode>

                                                <Telerik:RadTreeNode Text="Anchor Date" ImageUrl="/Images/Common16/Calendar.png" runat="server"></Telerik:RadTreeNode>

                                            </Nodes>
                                        
                                        </Telerik:RadTreeNode>

                                        <Telerik:RadTreeNode Text="[AND] Definition Root" ImageUrl="/Images/Common16/Set2Intersection.png" ContextMenuID="ContextMenuDataExplorerSet" ></Telerik:RadTreeNode>
                                    
                                    </Nodes>

                                    <ContextMenus>

                                        <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerGlobalVariable" runat="server">
                                        
                                            <Items>
                                            
                                                <Telerik:RadMenuItem Text="Add Variable of Type: Date" ImageUrl="/Images/Common16/Calendar.png" runat="server"></Telerik:RadMenuItem>

                                                <Telerik:RadMenuItem Text="Delete Selected Variable" ImageUrl="/Images/Common16/Delete.png" runat="server"></Telerik:RadMenuItem>
                                            
                                            </Items>
                                        
                                        </Telerik:RadTreeViewContextMenu>
                                    
                                        <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerSet" runat="server">
                                        
                                            <Items>
                                            
                                                <Telerik:RadMenuItem Text="Add a Union Subset [OR]" ImageUrl="/Images/Common16/Set2Union.png" runat="server"></Telerik:RadMenuItem>

                                                <Telerik:RadMenuItem Text="Add a Intersection Subset [AND]" ImageUrl="/Images/Common16/Set2Intersection.png" runat="server"></Telerik:RadMenuItem>

                                                <Telerik:RadMenuItem Text="Add an Evaluation" ImageUrl="/Images/Common16/DataExplorerEvaluation.png" runat="server"></Telerik:RadMenuItem>

                                                <Telerik:RadMenuItem Text="Delete Item" ImageUrl="/Images/Common16/Delete.png" runat="server"></Telerik:RadMenuItem>
                                            
                                            </Items>
                                        
                                        </Telerik:RadTreeViewContextMenu>
                                    
                                    </ContextMenus>

                                
                                </Telerik:RadTreeView>

                            </td>
                            
                            <td valign="top">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Definition Node Properties</div>

                                <div class="BorderColorDark">

                                    Some Properties Here

                                </div>
                            
                            </td>
                        
                        </tr></table>

                    </div>

                </td></tr>

            </table>
        
        </div>

        <!-- DATA EXPLORER DEFINITION ( END ) -->
    
    </div>

    <!-- DATA EXPLORER SELECTION/DEFINITION ( END ) -->
    
    
    <div style="height: .125in;">&nbsp</div>


    <!-- DATA EXPLORER RESULTS (BEGIN) -->
    
    <div id="DataExplorerResultsSection" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in;" runat="server">
        
        <table id="Workspace_MyAssignedWorkContainer_TitleTable" width="100%" cellpadding="0" cellspacing="0">
        
            <tr>

                <td style="white-space: nowrap;">Results for:</td>

                <td></td>

                <td colspan="2" style="text-align: right">
                
                    <asp:LinkButton ID="MyAssignedWorkRefresh" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(query)</asp:LinkButton>

                    <asp:LinkButton ID="LinkButton2" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(export)</asp:LinkButton>
                
                </td>

            </tr>

            <tr style="height: 10px;"><td>&nbsp</td></tr>
            
        </table>
        
        <Telerik:RadSplitter ID="DataExplorerResultsGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

            <Telerik:RadPane ID="DataExplorerResultsGridSplitterPane" Scrolling="None" Width="100%" Height="450" runat="server">
            
                <Telerik:RadGrid ID="DataExplorerResultsGrid" Width="100%" Height="100%" 
                
                        AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
            
                        AllowFilteringByColumn="false" EnableHeaderContextFilterMenu="false" EnableHeaderContextMenu="false"

                        AllowPaging="true" AllowCustomPaging="true" 

                        runat="server">

                </Telerik:RadGrid>

            </Telerik:RadPane>

        </Telerik:RadSplitter>

    </div>

    <!-- DATA EXPLORER END ( END ) -->
    
</div>



<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', DataExplorer_Body_OnResize, false); } else { window.attachEvent('onresize', DataExplorer_Body_OnResize); }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isDataExplorerPainting = false;

    setTimeout('DataExplorer_OnPaint()', 500);


    function DataExplorer_OnPaint(forEvent) {

        if (isDataExplorerPainting) { return; }

        isDataExplorerPainting = true;


        var container = document.getElementById("<%= DataExplorerResultsSection.ClientID %>");

        var splitter = $find("<%= DataExplorerResultsGridSplitter.ClientID %>");

        if ((container == null) || (splitter == null)) {

            isDataExplorerPainting = false;

            setTimeout('DataExplorer_OnPaint ()', 100);

            return;

        }


        var availableHeight = GetWindowHeight() - container.offsetTop;


        //        availableHeight = availableHeight - document.getElementById("EntityContactStep1SectionTitle").offsetHeight;

        //        availableHeight = availableHeight - document.getElementById("EntityContactStep2SectionTitle").offsetHeight;

        //        availableHeight = availableHeight - document.getElementById("EntityContactStep2").offsetHeight;

        availableHeight = availableHeight - (13 * 3); // MARGIN * 2

        availableHeight = availableHeight - 10;

        if (availableHeight < 100) { availableHeight = 100; }

        container.style.height = availableHeight + "px";

        splitter.set_width("100%");

        splitter.set_height(availableHeight - 36);


        isDataExplorerPainting = false;

        return;

    }


    function DataExplorer_Body_OnResize(forEvent) {

        DataExplorer_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>

    
</asp:Content>
