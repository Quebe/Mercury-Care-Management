<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlanAssessment.aspx.cs" Inherits="Mercury.Web.Application.MemberCase.Actions.MemberCaseCarePlanAssessment" %>

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

<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="None" runat="server" />

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" ScriptMode="Release" runat="Server">

        <Scripts>
        
        </Scripts>
    
    </asp:ScriptManager>
    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>

        </AjaxSettings>
    
        <ClientEvents OnRequestStart="TelerikAjaxManager_OnRequestStart" OnResponseEnd="TelerikAjaxManager_OnResponseEnd" />

    </Telerik:RadAjaxManager>

    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
    <asp:TextBox ID="LastBlurControl" Text="" runat="server" />

    <asp:TextBox ID="LastScrollToDivId" Text="" runat="server" />

    <asp:TextBox ID="LastScrollToPositionY" Text="0" runat="server" />
        
    <asp:TextBox ID="LastFocusControl" Text="" runat="server" />

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

    <div id="ContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="height: 500px; background-color: White; padding: 0in; overflow: auto">  
    
        <div style="padding: .125in; padding-bottom: 0px; background-color: White">

            <Telerik:RadGrid ID="MemberCaseCarePlanListViewProblemsGrid" 
                
                OnNeedDataSource="MemberCaseCarePlanListViewProblemsGrid_OnNeedDataSource"
                
                AutoGenerateColumns="false" Skin="Office2007" runat="server">
                    
                <MasterTableView CommandItemDisplay="None" DataKeyNames="">

                    <Columns>
        
                        <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                        <Telerik:GridBoundColumn DataField="ProblemStatementClassificationWithName" HeaderText="Associated Problem Statements"></Telerik:GridBoundColumn>

                        <Telerik:GridBoundColumn DataField="ProblemStatement.Description" HeaderText="Description"></Telerik:GridBoundColumn>

                        <Telerik:GridBoundColumn DataField="MemberCaseProblemClass.AssignedToUserDisplayName" HeaderText="Assigned To"></Telerik:GridBoundColumn>

                        <Telerik:GridBoundColumn DataField="MemberCaseProblemClass.AssignedToProvider.Name" HeaderText="Provider"></Telerik:GridBoundColumn>

                        <Telerik:GridBoundColumn DataField="IsSingleInstance" HeaderText="Is Single Instance"></Telerik:GridBoundColumn>

                    </Columns>                                                                  
                                         
                </MasterTableView>

                <ClientSettings>
                                
                    <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                </ClientSettings>
                
            </Telerik:RadGrid>

        </div>

        <Telerik:RadListView ID="AssessmentCareMeasuresListView" 

            OnItemCreated="AssessmentCareMeasuresListView_OnItemCreated"
            
            OnNeedDataSource="AssessmentCareMeasuresListView_OnNeedDataSource"

            runat="server">
        
            <ItemTemplate>
                        
                <div style="margin: .125in; border: 1px solid #215485; background: #A6C6E6;">    
              
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">
        
                        Care Measure: <%# Eval ("Name") %>
            
                    </div>
                    
                    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; margin: .125in; padding: .125in">

                    <div style="font-weight: bold; line-height: 200%">Used by Goals:</div>

                        <Telerik:RadListView ID="AssessmentCareMeasureGoalsListView" DataSource="<%# ((Mercury.Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasure) Container.DataItem).Goals %>" runat="server">

                            <ItemTemplate>

                                <div style="margin-left: 8px;">
                
                                    <span style="font-style: italic;">
                                <%# 
                            
                                    ((((Mercury.Client.Core.Individual.Case.MemberCaseCarePlan) Eval ("MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessment.MemberCaseCarePlan")).Goal ((Int64) Eval ("MemberCaseCarePlanGoalId")) != null) ?
                            
                                     (((Mercury.Client.Core.Individual.Case.MemberCaseCarePlan) Eval ("MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessment.MemberCaseCarePlan")).Goal ((Int64) Eval ("MemberCaseCarePlanGoalId")).Name) : String.Empty)
                               
                                %>: &nbsp;

                                </span>
                
                                <%# 
                            
                                    ((((Mercury.Client.Core.Individual.Case.MemberCaseCarePlan) Eval ("MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessment.MemberCaseCarePlan")).Goal ((Int64) Eval ("MemberCaseCarePlanGoalId")) != null) ?
                            
                                     (((Mercury.Client.Core.Individual.Case.MemberCaseCarePlan) Eval ("MemberCaseCarePlanAssessmentCareMeasure.MemberCaseCarePlanAssessment.MemberCaseCarePlan")).Goal ((Int64) Eval ("MemberCaseCarePlanGoalId")).ClinicalNarrative) : String.Empty)
                               
                                %>

                                </div>
                
                            </ItemTemplate>

                        </Telerik:RadListView>

                    </div>
                    
                    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; margin: .125in; padding: .125in">

                        <div><span style="font-weight: bold;line-height: 200%">Description: </span> <%# Eval ("Description") %></div>

                        <Telerik:RadListView ID="AssessmentCareMeasureScalesListView" DataSource="<%# ((Mercury.Client.Core.Individual.Case.MemberCaseCarePlanAssessmentCareMeasure) Container.DataItem).CareMeasureScales %>" runat="server">

                            <ItemTemplate>

                                <table width="100%" cellpadding="0" cellspacing="0" style="border: 1px solid black">

                                    <tr style="line-height: 200%">
                                        <td></td>

                                        <td colspan="6" style="text-align: center"><%# Eval ("Description") %></td>
                                    
                                    </tr>
                                
                                    <tr>

                                        <td style="width: 35%;font-weight: bold;" valign="bottom">Component</td>
                                    
                                        <td valign="bottom" style="width: 80px; text-align: center">Not Applicable</td>
                                        
                                        <td valign="bottom" style="width: 80px; text-align: center"><%# Eval ("ScaleLabel1")%></td>
                                        
                                        <td valign="bottom" style="width: 80px; text-align: center"><%# Eval ("ScaleLabel2")%></td>
                                        
                                        <td valign="bottom" style="width: 80px; text-align: center"><%# Eval ("ScaleLabel3")%></td>
                                        
                                        <td valign="bottom" style="width: 80px; text-align: center"><%# Eval ("ScaleLabel4")%></td>

                                        <td valign="bottom" style="width: 80px; text-align: center"><%# Eval ("ScaleLabel5")%></td>
                                    
                                    </tr>

                                    <Telerik:RadListView ID="AssessmentCareMeasureComponentsListView" 
                                    
                                            OnNeedDataSource="AssessmentCareMeasureComponentsListView_OnNeedDataSource"

                                            OnItemDataBound="AssessmentCareMeasureComponentsListView_OnItemDataBound"

                                            OnItemCreated="AssessmentCareMeasureComponentsListView_OnItemCreated"

                                            runat="server">
                                    
                                        <ItemTemplate>

                                            <tr style="line-height: 200%">
                                            
                                                <td style="" valign="bottom"><%# Eval ("Name") %></td>

                                                <td colspan="6" align="center" style="text-align: center;">
                                            
                                                    <asp:RadioButtonList ID="ComponentValueSelection" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table" 
                                                    
                                                        OnSelectedIndexChanged="ComponentValueSelection_OnSelectedIndexChanged" AutoPostBack="true" 

                                                        runat="server">

                                                        <asp:ListItem Value="0" Text="" />
                                        
                                                        <asp:ListItem Value="1" Text="" />

                                                        <asp:ListItem Value="2" Text="" />

                                                        <asp:ListItem Value="3" Text="" />

                                                        <asp:ListItem Value="4" Text="" />

                                                        <asp:ListItem Value="5" Text="" />

                                                    </asp:RadioButtonList>

                                                </td>
                                    
                                            </tr>
                                        
                                        </ItemTemplate>

                                        <AlternatingItemTemplate>
                                        
                                            <tr style="line-height: 200%; background-color: #EEEEEE">
                                            
                                                <td style="" valign="bottom"><%# Eval ("Name") %></td>

                                                <td colspan="6" align="center" style="text-align: center;">
                                            
                                                    <asp:RadioButtonList ID="ComponentValueSelection" Width="100%" RepeatDirection="Horizontal" RepeatLayout="Table"
                                                    
                                                        OnSelectedIndexChanged="ComponentValueSelection_OnSelectedIndexChanged" AutoPostBack="true"

                                                        runat="server">
                                                    
                                                        <asp:ListItem Value="0" Text="" />
                                        
                                                        <asp:ListItem Value="1" Text="" />

                                                        <asp:ListItem Value="2" Text="" />

                                                        <asp:ListItem Value="3" Text="" />

                                                        <asp:ListItem Value="4" Text="" />

                                                        <asp:ListItem Value="5" Text="" />
                                                        
                                                    </asp:RadioButtonList>

                                                </td>
                                    
                                            </tr>                                        
                                        
                                        </AlternatingItemTemplate>

                                    </Telerik:RadListView>
                                                                
                                </table>
                                                        
                            </ItemTemplate>
                        
                        </Telerik:RadListView>                                                

                        <table width="100%" cellpadding="0" cellspacing="0" style="margin-top: .06125in;"><tr>
                        
                            <td>&nbsp;</td>

                            <td style="width: 50px; font-weight: bold;">Score:</td>
                        
                            <td style="width: 60px;"><asp:Label ID="AssessmentMeasureScore" runat="server"></asp:Label></td>

                            <td style="width: 50px; font-weight: bold;">Target:</td>

                            <td style="width: 50px;"><Telerik:RadNumericTextBox ID="AssessmentMeasureTarget" Width="40" MinValue="0" MaxValue="5" NumberFormat-DecimalDigits="2" OnTextChanged="AssessmentMeasureTarget_OnTextChanged" AutoPostBack="true" runat="server"></Telerik:RadNumericTextBox></td>
                        
                        </tr></table>
                        
                    </div>

                </div>

            </ItemTemplate>
            
        </Telerik:RadListView>
        

    </div>

    <div id="SubmissionContainer">
    
        <Telerik:RadToolBar ID="AssessmentToolbar" Width="100%" Skin="Sunset" 
        
            OnItemCreated="AssessmentToolbar_OnItemCreated"

            OnButtonClick="AssessmentToolbar_OnButtonClick"

            runat="server">

            <Items>
            
                <Telerik:RadToolBarButton Text="Save Assessment" Enabled="false" runat="server"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton IsSeparator="true" runat="server"></Telerik:RadToolBarButton>

                <Telerik:RadToolBarButton Text="x Goals Left" Enabled="false" runat="server"></Telerik:RadToolBarButton>
            
            </Items>
        
        </Telerik:RadToolBar>

    </div>

</div>

<Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

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

        var submissionContainer = document.getElementById("SubmissionContainer");



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

        availableHeight = availableHeight - submissionContainer.offsetHeight;

        if (availableHeight < 0) { availableHeight = 0; }

        contentContainer.style.height = availableHeight + "px";


        isActionPainting = false;

        return;

    }


    function Action_Body_OnResize(forEvent) {

        Action_OnPaint(forEvent);

        setTimeout('Action_OnPaint()', 250);

        return;

    }

    function TelerikAjaxManager_OnRequestStart(sender, e) {

        lastScrollToPositionY = document.getElementById("<%= LastScrollToPositionY.ClientID %>");


        contentContainer = document.getElementById("ContentContainer");

        lastScrollToPositionY.value = 0;

        if (contentContainer != null) { lastScrollToPositionY.value = contentContainer.scrollTop; }


        if (document.activeElement) {

            lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

            lastFocusControl.value = document.activeElement.id;

        }

        return;

    }

    function TelerikAjaxManager_OnResponseEnd(sender, eventArgs) {

        Action_OnPaint();
        

        lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

        if (lastFocusControl) {

            if (lastFocusControl.value) {

                setTimeout(AjaxResponse_SetFocus, 1);

            }

        }

        return;

    }

    function AjaxResponse_SetFocus() {

        lastScrollToPositionY = document.getElementById("<%= LastScrollToPositionY.ClientID %>");

        contentContainer = document.getElementById("ContentContainer");

        contentContainer.scrollTop = lastScrollToPositionY.value;


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

</script>

</Telerik:RadCodeBlock>

</form>

</body>

</html>
