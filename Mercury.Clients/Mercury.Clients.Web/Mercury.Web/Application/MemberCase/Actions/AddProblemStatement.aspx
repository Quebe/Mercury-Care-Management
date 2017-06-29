<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProblemStatement.aspx.cs" Inherits="Mercury.Web.Application.MemberCase.Actions.AddProblemStatement" %>

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

<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" ScriptMode="Release" runat="Server">

        <Scripts>
        
        </Scripts>
    
    </asp:ScriptManager>

    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="ProblemStatementTreeView">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementTreeView" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementPropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>

            </Telerik:AjaxSetting>

            
            <Telerik:AjaxSetting AjaxControlID="ProblemStatementFilter">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementTreeView" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementPropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>

            </Telerik:AjaxSetting>
            
            
            <Telerik:AjaxSetting AjaxControlID="ProblemStatementFilterClear">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementTreeView" LoadingPanelID="AjaxLoadingPanel" />

                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementPropertiesDiv" LoadingPanelID="AjaxLoadingPanel" />
                
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


<div id="ApplicationTitleBar">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="padding-left: .125in">
            
                <img src="/Images/Common16/Case.png" alt="Member Case" />

            </td>

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                <a id="ApplicationTitle" class="NoDecoration HoverTextWhiteBold" href="/PermissionDenied.aspx" target="_blank" style="color: White; font-weight: bold; white-space: nowrap" runat="server">No Member Available</a>
                
            </td>

            <td style="padding-left: .125in; padding-right: .25in">
                
                <asp:LinkButton ID="CloseLink" CssClass="NoDecoration ColorLight HoverTextWhiteBold" OnClick="ButtonCancel_OnClick" runat="server">
                
                    <span style="white-space: nowrap; font-weight: bold; text-align: center;">(close)</span>

                </asp:LinkButton>
            
            </td>

        </tr>
     
        <tr><td colspan="6" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<div id="NavigationBar" class="BackgroundColorComplementLight" style="display: block;">

    <div class="BackgroundColorComplementDark" style="height: 1px;"></div>
    
        <div id="ExceptionMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%" style="background-color: White;"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
                <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
                <td style="text-align: left;"><asp:Label ID="ExceptionMessageLabel" runat="server" /></td>

            </tr></table>
            
            <div class="BackgroundColorComplementDark" style="height: 1px;"></div>

        </div>

</div>

<div id="ApplicationContent" style="padding: .125in;">

    <div id="ContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="height: 500px; background-color: White; padding: 0in; overflow: hidden">  

        <Telerik:RadSplitter ID="OuterSplitter" Width="100%" Height="100%" BackColor="White" runat="server">

            <Telerik:RadPane ID="RadPane1" Width="50%" runat="server">

                <div style="width: 100%; height: 100%; overflow: hidden;">

                <table width="100%" style="height: 100%;">

                    <tr style=""><td valign="top" style="width: 100%;">
            
                        <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in; margin-top: 0px; margin-bottom: 0px;"><tr>
                    
                            <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Problem Statement Selection</td>
                    
                        </tr></table>

                    </td></tr>
                    
                    <tr style=""><td valign="top" style="padding-left: .125in; padding-right: .125in;">
            
                        <table cellpadding="0" cellspacing="0" style="width: 100%;" border="0"><tr>
                    
                            <td style="width: 60px;">Contains:</td>

                            <td style=""><Telerik:RadTextBox ID="ProblemStatementFilterText"  Width="90%" runat="server" /></td>

                            <td align="center" style="width: 70px;"><asp:Button ID="ProblemStatementFilter" Width="60" Text="Filter" OnClick="ProblemStatementFilter_OnClick" runat="server" /></td>

                            <td align="right" style="width: 70px;"><asp:Button ID="ProblemStatementFilterClear" Width="60" Text="Clear" OnClick="ProblemStatementFilterClear_OnClick" runat="server" /></td>
                    
                        </tr></table>

                    </td></tr>

                    <tr style=""><td valign="top" style="padding-left: 10px; padding-right: 10px;">
            
                        <Telerik:RadTreeView ID="ProblemStatementTreeView" Width="100%" Height="100" CheckBoxes="true" BorderColor="Black" BorderWidth="1" MultipleSelect="true"
                        
                            OnNodeClick="ProblemStatementTreeView_OnNodeClick"
                        
                            runat="server">

                        </Telerik:RadTreeView>
            
                    </td></tr>
                                
                    <tr style="height: 40px; background: white"><td>
                    
                        <div style="height: .125in;">&nbsp;</div>

                        <table cellpadding="0" cellspacing="0" style="width: 100%" border="0"><tr>
       
                            <td style="">&nbsp;</td>

                            <td style="width: 80px;"><asp:Button ID="ButtonOk" Text="OK" OnClick="ButtonOk_OnClick" Width="73px" Height="24" runat="Server" /></td>    
    
                            <td style="width: .125in;">&nbsp;</td>

                            <td style="width: 80px;"><asp:Button ID="ButtonCancel" Text="Cancel" OnClick="ButtonCancel_OnClick" Width="73px" Height="24" runat="Server" /></td>
        
                            <td style="width: .125in;">&nbsp;</td>

                            <td style="width: 80px;"><asp:Button ID="ButtonApply" Text="Apply" OnClick="ButtonApply_OnClick" Width="73px" Height="24" runat="Server" /></td>

                        </tr></table>
            
                    </td></tr>

                </table>

                </div>

            </Telerik:RadPane>
        
            <Telerik:RadPane ID="RadPane2" Width="50%" runat="server">

                <div id="ProblemStatementPropertiesDiv" style="width: 100%; height: 100%; overflow: auto;" runat="server">

                    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Selected Problem Statement</div>
                                       
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <table width="100%" cellpadding="4"><tr>
                    
                            <td style="width: 20%; padding: 4px; font-weight: bold;">Domain:</td>
                    
                            <td style="width: 80%; padding: 4px;"><asp:Label ID="ProblemStatementDomainName" runat="server" /></td>

                            </tr><tr>
                    
                            <td style="width: 20%; padding: 4px; font-weight: bold;">Class:</td>
                    
                            <td style="width: 80%; padding: 4px;"><asp:Label ID="ProblemStatementClassName" runat="server" /></td>
                            
                            </tr><tr>
                    
                            <td style="width: 20%; padding: 4px; font-weight: bold;">Problem:</td>
                    
                            <td style="width: 80%; padding: 4px;"><asp:Label ID="ProblemStatementName" runat="server" /></td>
                            
                        </tr></table>

                    </div>

                    
                    <div class="PropertyPageSectionTitle">Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                        <div style="margin-top: 8px;"><b>Description:</b></div>
                    
                        <div><asp:Label ID="ProblemStatementDescription" runat="server" /></div>
                        
                        <div style="margin-top: 8px;"><b>Defining Characteristics:</b></div>
                    
                        <div><asp:Label ID="ProblemStatementDefiningCharacteristics" runat="server" /></div>

                        <div style="margin-top: 8px;"><b>Related Factors:</b></div>
                    
                        <div><asp:Label ID="ProblemStatementRelatedFactors" runat="server" /></div>

                    </div>
                               
                               
                    <div class="PropertyPageSectionTitle">Default Care Plan</div>
                    
                    <div style="margin: 0px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <table width="100%" cellpadding="0"><tr>
                    
                            <td style="width: 20%; font-weight: bold;">Care Plan:</td>
                    
                            <td style="width: 80%;"><asp:Label ID="DefaultCarePlanName" runat="server" /></td>

                        </tr></table>
                                        
                        <div style="margin-top: 8px;"><b>Goals:</b></div>
                        
                        <div style="margin: 0px 10px 2px 10px; padding: 4px; line-height: 150%;">

                            <Telerik:RadListView ID="DefaultCarePlanGoals" runat="server">

                                <ItemTemplate>
                            
                                    <li><span style="font-style: italic;"><%# Eval ("Name") %></span>: <%# Eval ("ClinicalNarrative") %>
                                
                                    </li>
                            
                                </ItemTemplate>
                        
                            </Telerik:RadListView>

                        </div>

                        <div style="margin-top: 8px;"><b>Interventions:</b></div>
                        
                        <div style="margin: 0px 10px 2px 10px; padding: 4px; line-height: 150%;">

                            <Telerik:RadListView ID="DefaultCarePlanInterventions" runat="server">
                        
                                <ItemTemplate>
                            
                                    <li><span style="font-style: italic;"><%# Eval ("Name")%></span>: <%# Eval ("Description")%>
                                
                                    </li>
                            
                                </ItemTemplate>
                        
                            </Telerik:RadListView>

                        </div>

                    </div>

                </div>
            
            </Telerik:RadPane>
        
        </Telerik:RadSplitter>

    </div>

</div>

<Telerik:RadCodeBlock runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', Action_Body_OnResize, false); } else { window.attachEvent('onresize', Action_Body_OnResize); }

    if (window.addEventListener) { window.addEventListener('load', Action_Page_Load, false); } else { window.attachEvent('onload', Action_Page_Load); }


    var isActionPainting = false;


    function Action_Page_Load() {

        setTimeout('Action_OnPaint()', 250);

        return;

    }

    function Action_OnPaint(forEvent) {

        if (isActionPainting) { return; }

        isActionPainting = true;


        var applicationTitleBar = document.getElementById("ApplicationTitleBar");

        var applicationContent = document.getElementById("ApplicationContent");

        var contentContainer = document.getElementById("ContentContainer");


        var splitter = $find("<%= OuterSplitter.ClientID %>");

        var treeView = $find ("<%= ProblemStatementTreeView.ClientID %>");


        if ((splitter == null) || (applicationContent == null)) {

            isActionPainting = false;

            setTimeout('Action_OnPaint ()', 100);

            return;
        }

        // GET AVAILABLE WINDOW WIDTH
        if (window.innerWidth) { windowWidth = window.innerWidth; } else { windowWidth = document.documentElement.clientWidth; }

        // GET AVAILABLE WINDOW HEIGHT
        if (window.innerHeight) { windowHeight = window.innerHeight; } else { windowHeight = document.documentElement.clientHeight; }


        availableWidth = windowWidth - 0;

        availableHeight = windowHeight - applicationTitleBar.offsetHeight;
        
        if (availableHeight < 1) { availableHeight = 1; }

        applicationContent.style.height = availableHeight + "px";


        var marginHeight = contentContainer.offsetTop - applicationContent.offsetTop;

        availableHeight = applicationContent.offsetHeight - (marginHeight * 4);

        if (availableHeight < 0) { availableHeight = 0; }

        contentContainer.style.height = availableHeight + "px";

        document.getElementById("ProblemStatementTreeView").style.height = availableHeight - 120 + "px";


        splitter.set_width("100%");

        splitter.set_height("100%");


        isActionPainting = false;

        return;
        
    }


    function Action_Body_OnResize(forEvent) {

        Action_OnPaint(forEvent);

        setTimeout('Action_OnPaint()', 250);

        return;

    }


    function TelerikAjaxManager_OnRequestStart(sender, e) {

        
        return;

    }

    function TelerikAjaxManager_OnResponseEnd(sender, e) {

        Action_Body_OnResize();

        return;

    }

</script>

</Telerik:RadCodeBlock>

</form>

</body>

</html>
