<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataExplorer.aspx.cs" Inherits="Mercury.Web.Application.DataExplorer.DataExplorer" %>

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

<form id="form1" runat="server">

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
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="NodePropertiesSetType">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesSetType" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            

        </AjaxSettings>
    
    </Telerik:RadAjaxManager>

    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>

</div>


<div id="ApplicationTitleBar">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="padding-left: .125in">
            
                <img src="../../Images/Common16/Database.png" alt="Data Explorer" />

            </td>

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                Data Explorer: 

                <a id="ApplicationTitle2" class="NoDecoration HoverTextWhiteBold" href="/PermissionDenied.aspx" target="_blank" style="color: White; font-weight: bold; white-space: nowrap" runat="server">** New Data Explorer</a>
                
            </td>
            
            <td id="ApplicationTitleBarFileMenuLink" style="padding-left: .125in; padding-right: .25in" runat="server">
            
                <a id="FileMenuLink" class="NoDecoration ColorLight HoverTextWhiteBold" href="javascript:FileMenuToggle ();" style="white-space: nowrap; font-weight: bold; text-align: center;">Menu</a>

            </td>

            <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="/Application/Workspace/Workspace.aspx" style="white-space: nowrap; font-weight: bold; text-align: center;">Home</a></td>
            
            <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="javascript:Tour();" style="white-space: nowrap; font-weight: bold; text-align: center;">Tour</a></td>

        </tr>
     
        <tr><td colspan="6" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<div id="FileMenuBar" class="BackgroundColorComplementLight" style="display: none;">

    <table width="100%" cellpadding="0" cellspacing="0" style=""><tr style="height: 24px;">

        <td style="width: .0in">&nbsp</td>

        <td id="FileMenuLinkNew"    style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/Enterprise/Management.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">New</a> </td>

        <td id="NavigationLinkConfiguraiton" style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/Configuration/Management.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">Open</a></td>

        <td id="NavigationLinkFormDesigner"  style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/Forms/FormDesigner/FormDesigner.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">Save</a></td>
        
        <td id="NavigationLinkAutomation"    style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/Automation/Automation.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">Save As</a></td>

        <td id="NavigationLinkDataExplorer"  style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/DataExplorer/DataExplorer.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">Clone</a></td>

        <td style="">&nbsp</td>

    </tr>
                
    <tr class="BackgroundColorComplementDark" style="height: 1px;"><td colspan="7"></td></tr>

    </table>
        
    <div id="ExceptionMessageRow" style="display: none;" runat="server">
        
        <table cellpadding="0" cellspacing="0" width="100%" style="background-color: White;"><tr style="height: 36px;">
                        
            <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
            <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
            <td style="text-align: left;"><asp:Label ID="ExceptionMessage" runat="server" /></td>

        </tr></table>
            
        <div class="BackgroundColorComplementDark" style="height: 1px;"></div>

    </div>

</div>



<div id="ContentContainer" style="padding: .125in">

    <!-- DATA EXPLORER PROPERTIES (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table width="100%" cellpadding="0" cellspacing="0">
        
            <tr>

                <td style="white-space: nowrap;">Data Explorer Properties</td>

                <td></td>

                <td style="text-align: right">
                
                    <a id="A1" class="NoDecoration ColorComplementDarker HoverTextBlack"" href="javascript:DataExplorerPropertiesToggle ();" style="white-space: nowrap; text-align: center;">(collapse)</a>

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
        
    <div id="DefinitionResultsContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table id="DefinitionResultsContainer_TitleTable" width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 10px;"><tr>
            
            <td>
            
                <Telerik:RadTabStrip ID="DefinitionResultsStrip" Skin="" EnableEmbeddedSkins="False" 

                    MultiPageID="ContentMultiPage" SelectedIndex="0"  CssClass="RadTabStripBasicWhite" BackColor="White"

                    runat="server">
       
                    <Tabs>
            
                        <Telerik:RadTab Text="Definition" Selected="True" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>
                
                        <Telerik:RadTab Text="Results" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

                    </Tabs>

                </Telerik:RadTabStrip>
            
            </td>

            <td style="">&nbsp</td>

            <td style="text-align: right">
                
                <asp:LinkButton ID="MyAssignedWorkRefresh" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(refresh)</asp:LinkButton>
                
            </td>

        </tr></table>
           
        <div id="Div1" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: 0in; overflow: auto">  

            <Telerik:RadMultiPage ID="ContentMultiPage" SelectedIndex="0" runat="server">

                <Telerik:RadPageView ID="PageDefinition" runat="server">
                
                <Telerik:RadSplitter ID="OuterSplitter" Width="100%" Height="100%" BackColor="White" runat="server">

                    <Telerik:RadPane ID="DataExplorerTreeViewPane" Width="60%" runat="server">

                        <div style="width: 100%; height: 100%; overflow: hidden;">

                            <Telerik:RadTreeView ID="DataExplorerTreeView" Width="100%" Height="100%" CheckBoxes="true" BorderColor="Black" BorderWidth="1" EnableDragAndDrop="true" MultipleSelect="false"

                                OnNodeClick="DataExplorerTreeView_OnNodeClick"

                                OnContextMenuItemClick="DataExplorerTreeView_OnContextMenuItemClick"
                        
                                runat="server">

                                <ContextMenus>
                                
                                    <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerNodeSet" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadMenuItem Value="AddSet" Text="Add Child Set" ImageUrl="/Images/Common16/Set2.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvalationMemberDemographic" Text="Add Evaluation: Member Demographic" ImageUrl="/Images/Common16/Gear.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvalationMemberEnrollment" Text="Add Evaluation: Member Enrollment" ImageUrl="/Images/Common16/Gear.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvalationPopulation" Text="Add Evaluation: Population" ImageUrl="/Images/Common16/Gear.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem IsSeparator="true"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="Delete" Text="Delete Set and Children" ImageUrl="/Images/Common16/Delete.png"></Telerik:RadMenuItem>

                                        
                                        </Items>
                                    
                                    </Telerik:RadTreeViewContextMenu>
                                
                                </ContextMenus>
                        
                            </Telerik:RadTreeView>
            
                        </div>

                    </Telerik:RadPane>
        
                    <Telerik:RadPane ID="DataExplorerNodePane" Width="40%" runat="server">
                  
                        <div id="NodePropertiesDiv" style="width: 100%; height: 100%; overflow: auto;" runat="server">

                            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Node Properties</div>
                                       
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                <table width="100%" cellpadding="4"><tr>
                    
                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Node Type:</td>
                    
                                    <td style="width: 70%; padding: 4px;"><asp:Label ID="NodePropertiesNodeType" runat="server" /></td>

                                    </tr><tr>
                    
                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Name:</td>
                    
                                    <td style="width: 70%; padding: 4px;"><Telerik:RadTextBox ID="NodePropertiesName" Width="100%" runat="server" /></td>
                            
                                    </tr>
                                    
                                    <tr id="NodePropertiesRow_SetType" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Set Type:</td>
                    
                                        <td style="width: 70%; padding: 4px;">
                                        
                                            <Telerik:RadComboBox ID="NodePropertiesSetType" OnSelectedIndexChanged="NodePropertiesSetType_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                <Items>
                                                
                                                    <Telerik:RadComboBoxItem Text="Union (OR)" Value="1" />

                                                    <Telerik:RadComboBoxItem Text="Intersection (AND)" Value="2" />

                                                    <Telerik:RadComboBoxItem Text="Complement" Value="3" />

                                                    <Telerik:RadComboBoxItem Text="Symmetric Difference" Value="4" />
                                                
                                                </Items>
                                            
                                            </Telerik:RadComboBox>
                                        
                                        </td>
                            
                                </tr></table>

                            </div>

                            <div id="PopulationEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Population</div>
                                       
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4">
                                                                        
                                        <tr runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member is:</td>

                                        </tr>

                                        <tr runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Function:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="PopulationEvaluationFunction" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="In" Value="1" />

                                                        <Telerik:RadComboBoxItem Text="Not In" Value="2" />

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                    
                                        <tr runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Evaluation Type:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="PopulationEvaluationType" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="Population" Value="1" />

                                                        <Telerik:RadComboBoxItem Text="Population Type" Value="2" />

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                      
                                        <tr id="PopulationEvalationPopulationSelectionRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Population:</td>
                                            
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="PopulationEvaluationPopulationSelection" AutoPostBack="true" Width="100%" runat="server" /></td>

                                        </tr>
                                        
                                        <tr id="PopulationEvalationPopulationTypeSelectionRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Population Type:</td>
                                            
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="PopulationEvaluationPopulationTypeSelection" AutoPostBack="true" Width="100%" runat="server" /></td>

                                        </tr>

                                    </table>

                                </div>

                            </div>

                        </div>
                          
                    </Telerik:RadPane>
        
                </Telerik:RadSplitter>

                </Telerik:RadPageView>

                <Telerik:RadPageView ID="PageResults" runat="server">

                    <Telerik:RadSplitter ID="MyAssignedWorkGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

                        <Telerik:RadPane ID="MyAssignedworkGridSplitterPane" Scrolling="None" Width="100%" Height="100%" runat="server">
                
                            <Telerik:RadGrid ID="MyAssignedWork_WorkQueueItemsGrid" Width="100%" Height="100%"
        
                                    AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
            
                                    AllowFilteringByColumn="true" EnableHeaderContextFilterMenu="true" EnableHeaderContextMenu="true"

                                    AllowPaging="true" AllowCustomPaging="false" 
            
                                    runat="server">

                                <MasterTableView IsFilterItemExpanded="false" TableLayout="Fixed" PageSize="10">


                                </MasterTableView>
            
                                <ClientSettings EnableRowHoverStyle="true">          

                                    <Selecting AllowRowSelect="true" />

                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                
                                </ClientSettings>

                            </Telerik:RadGrid>

                        </Telerik:RadPane>

                    </Telerik:RadSplitter>
                
                </Telerik:RadPageView>
        
            </Telerik:RadMultiPage>

        </div>

    </div>

</div>

<Telerik:RadCodeBlock ID="TourCodeBlock" runat="server">

<script type="text/javascript">

function FileMenuToggle() {

    var menuBar = document.getElementById("FileMenuBar");


    if (menuBar.style.display == "none") {

        // EXPAND NAVIGATION 

        menuBar.style.display = "";

    }

    else { // COLLAPSE NAVIGATION 

        menuBar.style.display = "none";

    }

    return;

}

function DataExplorerPropertiesToggle() {

    var propertiesContent = document.getElementById("DataExplorerPropertiesContent");


    if (propertiesContent.style.display == "none") {

        // EXPAND NAVIGATION 

        propertiesContent.style.display = "";

    }

    else { // COLLAPSE NAVIGATION 

        propertiesContent.style.display = "none";

    }

    return;

}

</script>

</Telerik:RadCodeBlock>

</form>

</body>

</html>