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

            <Telerik:AjaxSetting AjaxControlID="DataExplorerSave">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerSave" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="NodeVariablesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerNodeResultsGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ExceptionMessageRow" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="DataExplorerClone">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerClone" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="NodeVariablesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerNodeResultsGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ExceptionMessageRow" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="DataExplorerTreeView">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="NodeVariablesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerNodeResultsGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="VariablePropertiesVariableName">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="VariablePropertiesVariableName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="VariablePropertiesVariableDataType">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="NodeVariablesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="NodePropertiesName">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="NodePropertiesSetType">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="NodePropertiesSetType" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="DataExplorerTreeView" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            
            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentEvaluationContinuousEnrollment"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentEvaluationContinuousAllowedGaps"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentEvaluationContinuousAllowedGapDays"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            
            

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousUntilAge"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGaps"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGapDays"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            



            <Telerik:AjaxSetting AjaxControlID="PopulationMembershipEvaluationTypeSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            
            
            
            <Telerik:AjaxSetting AjaxControlID="AgeCriteria1UseAgeCriteria"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="AgeCriteria1AgeMinimum"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="AgeCriteria1AgeMaximum"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="AgeCriteria1AgeQualifierSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            
            <Telerik:AjaxSetting AjaxControlID="DateCriteria1DateTypeSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1StartDatePicker"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1StartDateVariableSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1StartDateRelativeValue"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1StartDateRelativeQualifier"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="DateCriteria1EndDateVariableSelection"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1EndDateRelativeValue"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="DateCriteria1EndDateRelativeQualifier"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="NodePropertiesDiv" LoadingPanelID="AjaxLoadingPanelWhiteout" /></UpdatedControls></Telerik:AjaxSetting>            

        </AjaxSettings>

        <ClientEvents OnRequestStart="TelerikAjaxManager_OnRequestStart" OnResponseEnd="TelerikAjaxManager_OnResponseEnd" />

    </Telerik:RadAjaxManager>

    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
    <asp:TextBox ID="LastBlurControl" Text="" runat="server" />
        
    <asp:TextBox ID="LastFocusControl" Text="" runat="server" />
        
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
            
        </tr>
     
        <tr><td colspan="6" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<div id="FileMenuBar" class="BackgroundColorComplementLight" style="display: none;">

    <table width="100%" cellpadding="0" cellspacing="0" style=""><tr style="height: 24px;">

        <td style="width: .0in">&nbsp</td>

        <td id="FileMenuLinkNew"    style="width: 100px; text-align: center;" runat="server"><a class="NoDecoration ColorComplementDarker HoverTextBlack" href="/Application/DataExplorer/DataExplorer.aspx" style="white-space: nowrap; padding-right: .125in; font-weight: normal">New</a> </td>

        <td style="width: 100px; text-align: center;" runat="server">

            <a class="NoDecoration ColorComplementDarker HoverTextBlack" style="font-weight: normal; cursor: pointer" onclick="DataExplorerOpen_OnClick ()" >Open</a>

        </td>

        <td style="width: 100px; text-align: center;" runat="server">
        
            <asp:LinkButton ID="DataExplorerSave" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" Font-Bold="false" OnClick="DataExplorerSave_OnClick" runat="server">Save</asp:LinkButton>

        </td>
        
        <td id="Td1" style="width: 100px; text-align: center;" runat="server">
        
            <asp:LinkButton ID="DataExplorerClone" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" Font-Bold="false" OnClick="DataExplorerClone_OnClick" runat="server">Clone</asp:LinkButton>

        </td>
        
        <td style="">&nbsp</td>

    </tr>
                
    <tr class="BackgroundColorComplementDark" style="height: 1px;"><td colspan="7"></td></tr>

    </table>
        
</div>

<div id="ExceptionMessageRow" visible="false" runat="server">
        
    <table cellpadding="0" cellspacing="0" width="100%" style="background-color: White;"><tr style="height: 36px;">
                        
        <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
        <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
        <td style="text-align: left;"><asp:Label ID="ExceptionMessage" runat="server" /></td>

    </tr></table>
            
    <div class="BackgroundColorComplementDark" style="height: 1px;"></div>

</div>

<div id="ContentContainer" style="padding: .125in;" runat="server">

    <!-- DATA EXPLORER PROPERTIES (BEGIN) -->
    
    <div id="DataExplorerPropertiesContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
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
                    
                            <td style="width: 50%; padding: 4px;"><Telerik:RadTextBox ID="DataExplorerName" Width="98%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>

                            <td style="width: 175px; white-space: nowrap;">

                                <asp:RadioButtonList ID="DataExplorerIsPublic" RepeatDirection="Horizontal" runat="server">
                                
                                    <asp:ListItem Text="Public" Selected="False" Value="True"></asp:ListItem>

                                    <asp:ListItem Text="Private, Owner:" Selected="True" Value="False"></asp:ListItem>
                                                                    
                                </asp:RadioButtonList>

                            </td>

                            <td style="text-align: left;">(owner)</td>

                        </tr>
                    
                        <tr>
                            
                            <td valign="top" style="padding: 4px;">Description:</td>

                            <td colspan="3" style="padding: 4px;"><Telerik:RadTextBox ID="DataExplorerDescription" Width="100%" Rows="2" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></td>
                                
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

                    OnClientTabSelected="DataExplorer_OnPaint"

                    runat="server">
       
                    <Tabs>
            
                        <Telerik:RadTab Text="Definition" Selected="True" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>
                
                        <Telerik:RadTab Text="Results" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

                    </Tabs>

                </Telerik:RadTabStrip>
            
            </td>

            <td style="">&nbsp</td>

            <td style="text-align: right">
                
            </td>

        </tr></table>
           
        <div id="PageContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: 0in; overflow: auto">  

            <Telerik:RadMultiPage ID="ContentMultiPage" SelectedIndex="0" runat="server">

                <Telerik:RadPageView ID="PageDefinition" runat="server">
                
                <Telerik:RadSplitter ID="OuterSplitter" Width="100%" Height="100%" BackColor="White" runat="server">

                    <Telerik:RadPane ID="DataExplorerTreeViewPane" Width="60%" runat="server">

                        <div style="width: 100%; height: 100%; overflow: hidden;">

                            <Telerik:RadTreeView ID="DataExplorerTreeView" Width="100%" Height="100%" CheckBoxes="true" BorderColor="Black" BorderWidth="1" MultipleSelect="false" EnableDragAndDrop="true" EnableDragAndDropBetweenNodes="true"

                                OnNodeDrop="DataExplorerTreeView_OnNodeDrop"

                                OnNodeClick="DataExplorerTreeView_OnNodeClick"

                                OnContextMenuItemClick="DataExplorerTreeView_OnContextMenuItemClick"
                        
                                runat="server">

                                <ContextMenus>

                                    <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerVariable" runat="server">
                                    
                                        <Items>

                                            <Telerik:RadMenuItem Value="AddVariable" Text="Add Variable" ImageUrl="/Images/Common16/Variable.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem IsSeparator="true"></Telerik:RadMenuItem>
                                            
                                            <Telerik:RadMenuItem Value="Delete" Text="Delete this Variable" ImageUrl="/Images/Common16/Delete.png"></Telerik:RadMenuItem>
                                        
                                        </Items>

                                    </Telerik:RadTreeViewContextMenu>
                                
                                    <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerNodeSet" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadMenuItem Value="Execute" Text="Execute Evaluation" ImageUrl="/Images/Common16/Gear.png"></Telerik:RadMenuItem>
                                        
                                            <Telerik:RadMenuItem IsSeparator="true"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddSet" Text="Add Child Set" ImageUrl="/Images/Common16/Set2.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvaluationMemberDemographic" Text="Add Evaluation: Member Demographic" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvaluationMemberEnrollment" Text="Add Evaluation: Member Enrollment" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvaluationMemberEnrollmentContinuousFromBirthDate" Text="Add Evaluation: Member Enrollment (Continuous from Birth Date)" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>
                                            
                                            <Telerik:RadMenuItem Value="AddEvaluationMemberMetric" Text="Add Evaluation: Member Metric" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvaluationMemberService" Text="Add Evaluation: Member Service" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="AddEvaluationPopulationMembership" Text="Add Evaluation: Population Membership" ImageUrl="/Images/Common16/DataExplorerEvaluation.png"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem IsSeparator="true"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="Delete" Text="Delete Set and Children" ImageUrl="/Images/Common16/Delete.png"></Telerik:RadMenuItem>
                                        
                                        </Items>
                                    
                                    </Telerik:RadTreeViewContextMenu>

                                    <Telerik:RadTreeViewContextMenu ID="ContextMenuDataExplorerNodeEvaluation" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadMenuItem Value="Execute" Text="Execute Evaluation" ImageUrl="/Images/Common16/Gear.png"></Telerik:RadMenuItem>
                                        
                                            <Telerik:RadMenuItem IsSeparator="true"></Telerik:RadMenuItem>

                                            <Telerik:RadMenuItem Value="Delete" Text="Delete Evaluation" ImageUrl="/Images/Common16/Delete.png"></Telerik:RadMenuItem>

                                        </Items>
                                    
                                    </Telerik:RadTreeViewContextMenu>
                                
                                </ContextMenus>
                        
                            </Telerik:RadTreeView>
            
                        </div>

                    </Telerik:RadPane>
        
                    <Telerik:RadPane ID="DataExplorerNodePane" Width="40%" runat="server">
                  
                        <div id="NodeVariablesDiv" style="width: 100%;" visible="false" runat="server">
                        
                            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Variable Properties</div>
                            
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                <table width="100%" cellpadding="4" cellspacing="0"><tr>
                    
                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Name:</td>
                    
                                    <td style="width: 70%; padding: 4px;"><Telerik:RadTextBox ID="VariablePropertiesVariableName" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server" /></td>
                            
                                    </tr>
                                    
                                    <tr id="Tr4" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Data Type:</td>
                    
                                        <td style="width: 70%; padding: 4px;">
                                        
                                            <Telerik:RadComboBox ID="VariablePropertiesVariableDataType" OnSelectedIndexChanged="VariablePropertiesVariableDataType_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                <Items>
                                                
                                                    <Telerik:RadComboBoxItem Text="Text" Value="0" />

                                                    <Telerik:RadComboBoxItem Text="Number" Value="1" />

                                                    <Telerik:RadComboBoxItem Text="Date" Value="2" />

                                                </Items>
                                            
                                            </Telerik:RadComboBox>
                                        
                                        </td>

                                    </tr>
                            
                                    <tr id="PropertiesRow_VariablePropertiesVariableTextValue" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Value:</td>
                    
                                        <td style="width: 70%; padding: 4px;"><Telerik:RadTextBox ID="VariablePropertiesVariableTextValue" Width="100%" MaxLength="60" runat="server" /></td>
                                        
                                    </tr>

                                    <tr id="PropertiesRow_VariablePropertiesVariableNumericValue" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Value:</td>
                    
                                        <td style="width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="VariablePropertiesVariableNumericValue" NumberFormat-DecimalDigits="4" runat="server"></Telerik:RadNumericTextBox></td>
                                        
                                    </tr>

                                    <tr id="PropertiesRow_VariablePropertiesVariableDatePicker" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Date:</td>
                    
                                        <td style="width: 70%; padding: 4px;"><Telerik:RadDatePicker ID="VariablePropertiesVariableDatePicker" runat="server"></Telerik:RadDatePicker></td>
                                        
                                    </tr>

                                    <tr id="PropertiesRow_VariablePropertiesVariableDateFunctionSelection" runat="server">
                    
                                        <td style="width: 30%; padding: 4px; font-weight: bold;">Date Function:</td>
                    
                                        <td style="width: 70%; padding: 4px;">

                                            <Telerik:RadComboBox ID="VariablePropertiesVariableDateFunctionSelection" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server">

                                                <Items>
                                                
                                                    <Telerik:RadComboBoxItem Text="DateTime.Today" Value="DateTime.Today" Selected="true" />

                                                    <Telerik:RadComboBoxItem Text="DateTime.FirstDayOfYear" Value="DateTime.FirstDayOfYear" Selected="false" />

                                                    <Telerik:RadComboBoxItem Text="DateTime.LastDayOfYear" Value="DateTime.LastDayOfYear" Selected="false" />
                                                                
                                                </Items>

                                            </Telerik:RadComboBox>

                                        </td>
                                        
                                    </tr>
                                        
                                </table>

                            </div>

                        </div>

                        <div id="NodePropertiesDiv" style="width: 100%;" runat="server">

                            <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Node Properties</div>
                                       
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                <table width="100%" cellpadding="4" cellspacing="0"><tr>
                    
                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Node Type:</td>
                    
                                    <td style="width: 70%; padding: 4px;"><asp:Label ID="NodePropertiesNodeType" runat="server" /></td>

                                    </tr><tr>
                    
                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Name:</td>
                    
                                    <td style="width: 70%; padding: 4px;"><Telerik:RadTextBox ID="NodePropertiesName" AutoPostBack="true" Width="98%" OnTextChanged="Properties_OnTextChange" runat="server" /></td>
                            
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

                            <div id="MemberDemographicEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Member Demographic</div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                       
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member is:</td>

                                        </tr>
                                        
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Gender:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="MemberDemographicEvaluationGenderSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="Both" Value="0" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="Female" Value="1" /> 

                                                        <Telerik:RadComboBoxItem Text="Male" Value="2" /> 

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                                   
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Ethnicity:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberDemographicEthnicitySelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>

                                    </table>

                                </div>
                            
                            </div>
                            
                            <div id="MemberEnrollmentEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Member Enrollment</div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                       
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member is enrolled with:</td>

                                        </tr>
                                                                                           
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Insurer:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentEvaluationInsurerSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                                                                    
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Program:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentEvaluationProgramSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                            
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Benefit Plan:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentEvaluationBenefitPlanSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                        
                                        <tr id="MemberEnrollmentEvaluationContinuousEnrollmentRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Continuous:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><asp:CheckBox ID="MemberEnrollmentEvaluationContinuousEnrollment" OnCheckedChanged="Properties_OnTextChange" AutoPostBack="true" Checked="false" runat="server" /></td>

                                        </tr>
                                                               
                                        <tr id="MemberEnrollmentEvaluationContinuousAllowedGapsRow" runat="server">

                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Allowed Gaps:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="MemberEnrollmentEvaluationContinuousAllowedGaps" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        
                                        <tr id="MemberEnrollmentEvaluationContinuousAllowedGapDaysRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Allowed Gap Days:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="MemberEnrollmentEvaluationContinuousAllowedGapDays" MinValue="0" MaxValue="999" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        

                                    </table>

                                </div>
                            
                            </div>
                            
                            <div id="MemberEnrollmentContinuousFromBirthDateEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Member Enrollment Continuous From Birth Date</div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                       
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member is enrolled with:</td>

                                        </tr>
                                                                                           
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Insurer:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationInsurerSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                                                                    
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Program:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationProgramSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                            
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Benefit Plan:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationBenefitPlanSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                        
                                        <tr id="Tr10" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Until Age:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousUntilAge" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server" /></td>

                                        </tr>
                                                               
                                        <tr id="Tr11" runat="server">

                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Allowed Gaps:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGaps" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        
                                        <tr id="Tr12" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Allowed Gap Days:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="MemberEnrollmentContinuousFromBirthDateEvaluationContinuousAllowedGapDays" MinValue="0" MaxValue="999" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        

                                    </table>

                                </div>
                            
                            </div>
                            
                            <div id="MemberMetricEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Member Metric</div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                       
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member has Metric:</td>

                                        </tr>
                                                                                           
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Metric:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberMetricEvaluationMetricSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                                                                    
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Minimum Value:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="MemberMetricEvaluationValueMinimum" MinValue="-199" MaxValue="999" NumberFormat-DecimalDigits="2" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                     
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Maximum Value:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="MemberMetricEvaluationValueMaximum" MinValue="-199" MaxValue="999" NumberFormat-DecimalDigits="2" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                     
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Count Of:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="MemberMetricEvaluationCountOf" MinValue="1" MaxValue="999" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>

                                    </table>

                                </div>
                            
                            </div>

                            <div id="MemberServiceEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Member Service</div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                       
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member has Service:</td>

                                        </tr>
                                                                                           
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Service:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="MemberServiceEvaluationServiceSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server"></Telerik:RadComboBox></td>

                                        </tr>
                                                                                    
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Count Of:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="MemberServiceEvaluationCountOf" MinValue="1" MaxValue="999" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>

                                    </table>

                                </div>
                            
                            </div>
                            
                            <div id="PopulationMembershipEvaluationPanel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Evaluation: Population Membership</div>
                                       
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                                        
                                        <tr id="Tr5" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">&nbsp;</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;">Member is:</td>

                                        </tr>

                                        <tr id="Tr8" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Function:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="PopulationMembershipEvaluationFunction" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="In" Value="9" />

                                                        <Telerik:RadComboBoxItem Text="Not In" Value="10" Visible="false" /> 

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                    
                                        <tr id="Tr9" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Evaluation Type:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="PopulationMembershipEvaluationTypeSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="Population" Value="0" />

                                                        <Telerik:RadComboBoxItem Text="Population Type" Value="1" />

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                      
                                        <tr id="PopulationMembershipEvalationPopulationSelectionRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Population:</td>
                                            
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="PopulationMembershipEvaluationPopulationSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server" /></td>

                                        </tr>
                                        
                                        <tr id="PopulationMembershipEvalationPopulationTypeSelectionRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Population Type:</td>
                                            
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadComboBox ID="PopulationMembershipEvaluationPopulationTypeSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server" /></td>

                                        </tr>

                                    </table>

                                </div>

                            </div>

                            <div id="AgeCriteria1Panel" runat="server">
                            
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Age Criteria: <asp:Label ID="AgeCriteria1NameLabel" runat="server"></asp:Label></div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                    <table width="100%" cellpadding="4" cellspacing="0">
                                    
                                        <tr>
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Use Age Criteria:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><asp:CheckBox ID="AgeCriteria1UseAgeCriteria" OnCheckedChanged="Properties_OnTextChange" AutoPostBack="true" Checked="false" runat="server" /></td>

                                        </tr>
                                                               
                                        <tr id="AgeCriteria1AgeMinimumRow" runat="server">

                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Age Minimum:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="AgeCriteria1AgeMinimum" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        
                                        <tr id="AgeCriteria1AgeMaximumRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Age Maximum:</td>
                                            
                                            <td style="width: 70%; padding: 4px; font-weight: bold;"><Telerik:RadNumericTextBox ID="AgeCriteria1AgeMaximum" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>

                                        </tr>
                                        
                                        <tr id="AgeCriteria1AgeQualifierRow" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Age Qualifier:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="AgeCriteria1AgeQualifierSelection" OnTextChanged="Properties_OnTextChange" AutoPostBack="true" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="Days" Value="0" />

                                                        <Telerik:RadComboBoxItem Text="Months" Value="1" /> 

                                                        <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="true" /> 

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                               
                                    </table>

                                </div>
                            
                            </div>

                            <div id="DateCriteria1Panel" runat="server">
                                
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Date Criteria: <asp:Label ID="DateCriteria1NameLabel" runat="server"></asp:Label></div>
                                
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                    <table width="100%" cellpadding="4" cellspacing="0">
                                                 
                                        <tr id="Tr1" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Date Type:</td>
                    
                                            <td style="width: 70%; padding: 4px;">
                                        
                                                <Telerik:RadComboBox ID="DateCriteria1DateTypeSelection" AutoPostBack="true" OnTextChanged="Properties_OnTextChange" Width="100%" runat="server">
                                            
                                                    <Items>
                                                
                                                        <Telerik:RadComboBoxItem Text="As of Date Absolute" Value="0" Visible="false" />

                                                        <Telerik:RadComboBoxItem Text="As of Date" Value="1" /> 

                                                        <Telerik:RadComboBoxItem Text="Between Dates Absolute" Value="2" Visible="false" />

                                                        <Telerik:RadComboBoxItem Text="Between Dates" Value="3" /> 

                                                    </Items>
                                            
                                                </Telerik:RadComboBox>
                                        
                                            </td>

                                        </tr>
                                        
                                        <tr><td colspan="2"><hr /></td></tr>

                                        <tr id="Tr2" visible="false" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Date:</td>
                    
                                            <td style="width: 70%; padding: 4px;"><Telerik:RadDatePicker ID="DateCriteria1StartDatePicker" AutoPostBack="true" runat="server"></Telerik:RadDatePicker></td>
                                        
                                        </tr>

                                        <tr id="Tr3" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Date Variable:</td>
                    
                                            <td style="width: 70%; padding: 4px;">

                                                <Telerik:RadComboBox ID="DateCriteria1StartDateVariableSelection" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server">

                                                </Telerik:RadComboBox>

                                            </td>
                                        
                                        </tr>
                                        
                                        <tr id="NodePropertyRow_DateCriteria1StartDateRelativePanel" runat="server">
                    
                                            <td style="width: 30%; padding: 4px; font-weight: bold;">Relative Value:</td>
                    
                                            <td style="width: 70%; padding: 4px;">

                                                <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                            
                                                <td><Telerik:RadNumericTextBox ID="DateCriteria1StartDateRelativeValue" NumberFormat-DecimalDigits="0" AutoPostBack="true" OnTextChanged="Properties_OnTextChange" runat="server"></Telerik:RadNumericTextBox></td>
                                                
                                                <td><Telerik:RadComboBox ID="DateCriteria1StartDateRelativeQualifier" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="Days" Value="0" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="Months" Value="1" Selected="false" />

                                                        <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="false" />
                                                    
                                                    </Items>

                                                </Telerik:RadComboBox>

                                                </td>

                                                </tr></table>

                                            </td>
                                        
                                        </tr>                                        

                                        <tr><td colspan="2"><hr /></td></tr>

                                        <tr id="NodePropertiesRow_DateCriteria1EndDatePanel" runat="server"><td colspan="2">
                                        
                                            <table width="100%" cellpadding="4" cellspacing="0">
                                            
                                                <tr id="Tr6" visible="false" runat="server">
                    
                                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Date:</td>
                    
                                                    <td style="width: 70%; padding: 4px;"><Telerik:RadDatePicker ID="DateCriteria1EndDatePicker" AutoPostBack="true" runat="server"></Telerik:RadDatePicker></td>
                                        
                                                </tr>

                                                <tr id="Tr7" runat="server">
                    
                                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Date Variable:</td>
                    
                                                    <td style="width: 70%; padding: 4px;">

                                                        <Telerik:RadComboBox ID="DateCriteria1EndDateVariableSelection" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server">

                                                        </Telerik:RadComboBox>

                                                    </td>
                                        
                                                </tr>
                                        
                                                <tr id="NodePropertiesRow_DateCriteria1EndDateRelativePanel" runat="server">
                    
                                                    <td style="width: 30%; padding: 4px; font-weight: bold;">Relative Value:</td>
                    
                                                    <td style="width: 70%; padding: 4px;">

                                                        <table width="100%" cellpadding="0" cellspacing="0"><tr>
                                            
                                                        <td><Telerik:RadNumericTextBox ID="DateCriteria1EndDateRelativeValue" NumberFormat-DecimalDigits="0" AutoPostBack="true" OnTextChanged="Properties_OnTextChange" runat="server"></Telerik:RadNumericTextBox></td>
                                                
                                                        <td><Telerik:RadComboBox ID="DateCriteria1EndDateRelativeQualifier" AutoPostBack="true" Width="100%" OnTextChanged="Properties_OnTextChange" runat="server">

                                                            <Items>
                                                    
                                                                <Telerik:RadComboBoxItem Text="Days" Value="0" Selected="true" />

                                                                <Telerik:RadComboBoxItem Text="Months" Value="1" Selected="false" />

                                                                <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="false" />
                                                    
                                                            </Items>

                                                        </Telerik:RadComboBox>

                                                        </td>

                                                        </tr></table>

                                                    </td>
                                        
                                                </tr>                                     

                                            </table>

                                        </td></tr>
                                        
                                    </table>

                                </div>

                            </div>

                        </div>
                          
                    </Telerik:RadPane>
        
                </Telerik:RadSplitter>

                </Telerik:RadPageView>

                <Telerik:RadPageView ID="PageResults" BackColor="Azure" runat="server">

                    <Telerik:RadSplitter ID="DataExplorerNodeResultsGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

                        <Telerik:RadPane ID="DataExplorerNodeResultsGridSplitterPane" Scrolling="None" Width="100%" Height="100%" runat="server">
                
                            <Telerik:RadGrid ID="DataExplorerNodeResultsGrid" Width="100%" Height="100%"
        
                                    AutoGenerateColumns="false" AllowMultiRowSelection="false" AllowSorting="true" 
            
                                    AllowFilteringByColumn="true" EnableHeaderContextFilterMenu="true" EnableHeaderContextMenu="true"

                                    AllowPaging="true" AllowCustomPaging="true" 

                                    OnItemCreated="DataExplorerNodeResultsGrid_OnItemCreated"

                                    OnNeedDataSource="DataExplorerNodeResultsGrid_OnNeedDataSource"

                                    OnItemCommand="DataExplorerNodeResultsGrid_OnItemCommand"
            
                                    runat="server">

                                <MasterTableView IsFilterItemExpanded="false" TableLayout="Fixed" PageSize="10" CommandItemDisplay="Top">

                                    <CommandItemTemplate>

                                        <table cellpadding="0" cellspacing="0" style="margin: .125in;"><tr>
                                        
                                            <td>
                                            
                                                <asp:LinkButton ID="DataExplorerNodeResultsGrid_Export" runat="server">
                                                
                                                    <img style="border: 0px; vertical-align: middle;" alt="" src="../../Images/Common16/DatabaseTable.png" />
                                                    
                                                    <span style="padding-left: 8px; padding-right: 8px;">Export Data</span>
                                                
                                                </asp:LinkButton>
                                            
                                            </td>
                                            

                                        </tr></table>

                                    </CommandItemTemplate>

                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="true" />
                
                                        <Telerik:GridBoundColumn DataField="Name" HeaderText="Name" Visible="true" />

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
                
                </Telerik:RadPageView>
        
            </Telerik:RadMultiPage>

        </div>

    </div>

</div>

<div id="TelerikWindows" style="display: none;">

    <Telerik:RadWindowManager ID="TelerikWindowManager"  runat="server">
    
        <Windows>
        
            <Telerik:RadWindow ID="DataExplorerOpenDialog" Width="600" Height="100" Modal="true" OnClientClose="DataExplorerOpenDialog_OnClientClose"  ReloadOnShow="true" ShowContentDuringLoad="true" NavigateUrl="/WindowLoading.aspx"  VisibleOnPageLoad="false" VisibleStatusbar="false"  runat="server" />

        </Windows>
    
    </Telerik:RadWindowManager>
    
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

    DataExplorer_Body_OnResize();

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

    DataExplorer_Body_OnResize();

    return;

}

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


    var applicationTitleBar = document.getElementById("ApplicationTitleBar");

    var contentContainer = document.getElementById("<%= ContentContainer.ClientID %>");


    var splitter = $find("<%= OuterSplitter.ClientID %>");

    var treeView = $find("<%= DataExplorerTreeView.ClientID %>");


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

    availableHeight = windowHeight - applicationTitleBar.offsetHeight;

    if (availableHeight < 1) { availableHeight = 1; }

    contentContainer.style.height = availableHeight - marginHeight - (contentContainer.offsetTop - 37) + "px";
       


    availableHeight = contentContainer.offsetHeight - marginHeight;

    availableHeight = availableHeight - document.getElementById("DataExplorerPropertiesContainer").offsetHeight - marginHeight;

    if (availableHeight < 1) { availableHeight = 1; }

    document.getElementById("DefinitionResultsContainer").style.height = availableHeight + "px";


    availableHeight = availableHeight - 40;

    if (availableHeight < 1) { availableHeight = 1; }

    document.getElementById("PageContentContainer").style.height = availableHeight + "px";

    document.getElementById("PageDefinition").style.height = availableHeight + "px";

    document.getElementById("PageResults").style.height = availableHeight + "px";

    document.getElementById("DataExplorerTreeView").style.height = availableHeight + "px";


    splitter.set_width("100%");

    splitter.set_height("100%");


    if ($find("<%= DataExplorerNodeResultsGridSplitter.ClientID %>") != null) {

        $find("<%= DataExplorerNodeResultsGridSplitter.ClientID %>").set_width("100%");

        $find("<%= DataExplorerNodeResultsGridSplitter.ClientID %>").set_height(availableHeight);

    }


    isDataExplorerPainting = false;

    return;

}


function DataExplorer_Body_OnResize(forEvent) {

    DataExplorer_OnPaint(forEvent);

    setTimeout('DataExplorer_OnPaint()', 250);

    return;

}

function TelerikAjaxManager_OnRequestStart(sender, eventArgs) {

    if (document.activeElement) {

        lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

        lastFocusControl.value = document.activeElement.id;
        
    }

    return;

}

function TelerikAjaxManager_OnResponseEnd(sender, eventArgs) {

    DataExplorer_Body_OnResize();

    lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

    if (lastFocusControl) {

        if (lastFocusControl.value) {

            setTimeout(AjaxResponse_SetFocus, 10);

        }

    }

    return;

}

function AjaxResponse_SetFocus() {

    lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

    if (lastFocusControl) {

        if (lastFocusControl.value) {

            control = document.getElementById(lastFocusControl.value);

            if (control) {

                if ((!control.isDisabled) && (control.offsetHeight > 0)) {

                    control.focus();

                }

            }

        }

    }

    return;

}

function DataExplorerOpen_OnClick() {

    // GET AVAILABLE WINDOW WIDTH
    if (window.innerWidth) { windowWidth = window.innerWidth; } else { windowWidth = document.documentElement.clientWidth; }

    // GET AVAILABLE WINDOW HEIGHT
    if (window.innerHeight) { windowHeight = window.innerHeight; } else { windowHeight = document.documentElement.clientHeight; }


    var availableWidth = windowWidth;

    var availableHeight = windowHeight;


    var windowManager = GetRadWindowManager();

    var radWindow = windowManager.GetWindowByName("DataExplorerOpenDialog");

    radWindow.SetWidth(availableWidth);

    radWindow.SetHeight(availableHeight);

    radWindow.SetUrl("Windows/OpenDataExplorerDialog.aspx?PageInstanceId=" + document.getElementById("PageInstanceId").value);

    radWindow.Show();

    radWindow.SetWidth(availableWidth);

    radWindow.SetHeight(availableHeight);

    radWindow.Center();

    return;

}


function DataExplorerOpenDialog_OnClientClose(sender, e) {

    if (e.get_argument() != null) {

        window.location = "/Application/DataExplorer/DataExplorer.aspx?DataExplorerId=" + e.get_argument();

    }

    return;

}

</script>

</Telerik:RadCodeBlock>

</form>

</body>

</html>