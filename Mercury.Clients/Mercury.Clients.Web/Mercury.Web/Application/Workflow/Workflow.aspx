<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflow.aspx.cs" Inherits="Mercury.Web.Application.Workflow.Workflow" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">

    <title>Mercury Care Management - Workflow</title>
    
    <meta http-equiv="expires" content="0" />
    
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />

    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />

    <link rel="Stylesheet" href="/Styles/PagerOffice2007.css" type="text/css" />

    <style type="text/css">
    
        html { overflow: hidden; }
    
    </style>
    
    <style type="text/css">

    .radReadOnlyCss_Office2007 {
        
        color:#000 !important;
	    
	    background:#fff !important;
	    
    }

    </style>

</head>

<body style="margin: 0px;" class="TextNormal BackgroundColorLight">

<form id="WorkflowForm" runat="server">


<!-- AJAX MANAGER CONTENT (BEGIN) -->

<div>

    <div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>
    
    <div style="display: none"><asp:TextBox ID="WorkflowReferrerUrl" Text="" runat="server" /></div>

    <div style="display: none">
    
        <asp:ScriptManager ID="MicrosoftScriptManager" AsyncPostBackTimeout="600" runat="server">
        
            <Scripts>
            
                <asp:ScriptReference Path="Workflow.js" />
        
            </Scripts>

        </asp:ScriptManager>
        
        <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
        
            <AjaxSettings>
                
                <Telerik:AjaxSetting AjaxControlID="WorkflowStart">
                
                    <UpdatedControls>
                    
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowTitleBar" LoadingPanelID="AjaxLoadingPanel"  />

                        <Telerik:AjaxUpdatedControl ControlID="WorkflowHeaderSection" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowFlowControl" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowContentPanel" LoadingPanelID="AjaxLoadingPanel" />

                    </UpdatedControls>
                
                </Telerik:AjaxSetting>
                    
                <Telerik:AjaxSetting AjaxControlID="WorkflowContinue">
                
                    <UpdatedControls>
                    
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowTitleBar" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowHeaderSection" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowFlowControl" />                      

                        <Telerik:AjaxUpdatedControl ControlID="WorkflowContentPanel" LoadingPanelID="AjaxLoadingPanel" />

                    </UpdatedControls>
                
                </Telerik:AjaxSetting>      
                                   
                <Telerik:AjaxSetting AjaxControlID="WorkflowResume">
                
                    <UpdatedControls>
                    
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowTitleBar" LoadingPanelID="AjaxLoadingPanel"  />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowHeaderSection" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowFlowControl" />                      

                        <Telerik:AjaxUpdatedControl ControlID="WorkflowContentPanel" LoadingPanelID="AjaxLoadingPanel" />

                    </UpdatedControls>
                
                </Telerik:AjaxSetting>      
                                   
                <Telerik:AjaxSetting AjaxControlID="WorkflowNextItemLink">
                
                    <UpdatedControls>
                    
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowNextItemContainer" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                        
                    </UpdatedControls>
                
                </Telerik:AjaxSetting>      
                
                <Telerik:AjaxSetting AjaxControlID="WorkflowNextItemButton">
                
                    <UpdatedControls>
                    
                        <Telerik:AjaxUpdatedControl ControlID="WorkflowNextItemContainer" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                        
                    </UpdatedControls>
                
                </Telerik:AjaxSetting>      

            </AjaxSettings>
            
            <ClientEvents OnRequestStart="TelerikAjaxManager_OnRequestStart" OnResponseEnd="TelerikAjaxManager_OnResponseEnd" />
            
        </Telerik:RadAjaxManager>
        
        <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" InitialDelayTime="250" runat="server"></Telerik:RadAjaxLoadingPanel>
    
        <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75"  MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
            <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
            </div>
            
        </Telerik:RadAjaxLoadingPanel>
        
        <asp:TextBox ID="LastBlurControl" Text="" runat="server" />
        
        <asp:TextBox ID="LastFocusControl" Text="" runat="server" />
        
    </div>

</div>

<!-- AJAX MANAGER CONTENT ( END ) -->


<!-- TITLE BAR (BEGIN) -->

<div id="WorkflowTitleBar" runat="server">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                <asp:Label ID="WorkflowTitleLabel" Text="Workflow: Not Available" runat="server"></asp:Label>
                
            </td>
            
            <td style="padding-left: .125in; padding-right: .25in">
            
                <div id="WorkflowCancelContainer" style="" runat="server">
                
                    <a class="NoDecoration ColorLight HoverTextWhiteBold" href="javascript:Workflow_Close ();" style="white-space: nowrap; font-weight: bold; text-align: center;">Cancel</a>

                </div>
                
            </td>

            <td style="padding-left: .125in; padding-right: .25in">
            
                <div id="WorkflowExitContainer" style="display: none;" runat="server">

                    <asp:LinkButton ID="WorkflowCloseLink" CssClass="NoDecoration ColorLight HoverTextWhiteBold" style="white-space: nowrap; font-weight: bold; text-align: center;" runat="server">Close</asp:LinkButton>                                   

                </div>
                
            </td>

            <td style="padding-right: .125in;">

                <div id="WorkflowNextItemContainer" style="display: none;" runat="server">
            
                    <table cellpadding="0" cellspacing="0"><tr>

                        <td style="padding-left: .125in; padding-right: .0625in"><asp:LinkButton ID="WorkflowNextItemLink" class="NoDecoration ColorLight HoverTextWhiteBold" style="white-space: nowrap; font-weight: bold; text-align: center;" OnClick="WorkflowNextItemButton_OnClick"  Text="Next Item" runat="server" /></td>
                
                        <td style="height: 20px; padding-left: 4px; padding-right: 4px;"><asp:ImageButton ID="WorkflowNextItemButton" ImageUrl="~/Images/Common16/ArrowGreenRight.png" AlternateText="Next Work Queue Item" OnClick="WorkflowNextItemButton_OnClick" runat="server" /></td>  

                    </tr></table>

                </div>

            </td>

        </tr>
     
        <tr><td colspan="5" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<!-- TITLE BAR ( END ) -->



<!-- WORKFLOW HEADER (BEGIN) -->

<div id="WorkflowHeaderSection" style="padding: .125in; padding-top: .0625in; padding-bottom: .0625in;" runat="server">

    <!-- WORKFLOW INFORMATION (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">

        <!-- WORK QUEUE ITEM INFORMATION (BEGIN) -->

        <div id="WorkQueueItemInformationMember" style="display: none;" runat="server">

            <table width="100%" cellpadding="0" cellspacing="0">
    
                <tr class="" style="height: 36px;">

                    <td style="max-width: 24px; padding-right: 4px;"><img id="UserInteractionEntityInformationMemberNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="display: none;" runat="server" /></td>   
        
                    <td style="max-width: 24px; padding-right: 4px;"><img id="UserInteractionEntityInformationMemberNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="display: none;" runat="server" /></td>   

        
                    <td style="text-align: left"><b>Member Name:</b> <asp:Label id="UserInteractionEntityInformationMemberName" Text="** No Member Selected" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Birth Date:</b> <asp:Label id="UserInteractionEntityInformationMemberBirthDate" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Age:</b> <asp:Label id="UserInteractionEntityInformationMemberAge" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Gender:</b> <asp:Label id="UserInteractionEntityInformationMemberGender" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Program:</b> <asp:Label id="UserInteractionEntityInformationMemberProgram" Text="" runat="server" /></td>
                                                
                    <td style="text-align: left"><b>Id:</b> <asp:Label id="UserInteractionEntityInformationMemberProgramMemberId" Text="" runat="server" /></td>

                    <td style="width: 50px; text-align: center;"><a id="MemberInformationCoverageToggle" href="#" onclick="javascript:MemberInformationCoverage_Toggle()" title="Toggle Coverage Information">(more)</a></td>

                </tr>
        
            </table>

            <div id="WorkQueueItemInformationMemberCoverage" style="display: none;" runat="server">
            
                <table width="100%" cellpadding="0" cellspacing="0">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left"><b>Benefit Plan:</b> <asp:Label id="UserInteractionEntityInformationMemberCoverageBenefitPlan" Text="** Not Enrolled" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Coverage Type:</b> <asp:Label id="UserInteractionEntityInformationMemberCoverageType" Text="" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Coverage Level:</b> <asp:Label id="UserInteractionEntityInformationMemberCoverageLevel" Text="" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Rate Code:</b> <asp:Label id="UserInteractionEntityInformationMemberCoverageRateCode" Text="" runat="server" /></td>

                    </tr>
        
                </table>
            
                <table width="100%" cellpadding="0" cellspacing="0">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left"><b>PCP Name:</b> <asp:Label id="UserInteractionEntityInformationMemberPcpName" Text="** No PCP" runat="server" /></td>
                        
                        <td style="text-align: left"><b>PCP Affiliate Name:</b> <asp:Label id="UserInteractionEntityInformationMemberPcpAffiliateName" Text="" runat="server" /></td>
                        
                    </tr>
        
                </table>

            </div>

        </div>
                
        <div id="WorkQueueItemInformationProvider" style="display: none;" runat="server">

            <table width="100%" cellpadding="0" cellspacing="0">
    
                <tr class="" style="height: 36px;">

                    <td style="max-width: 24px; padding-right: 4px;"><img id="UserInteractionEntityInformationProviderNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="display: none;" runat="server" /></td>   
        
                    <td style="max-width: 24px; padding-right: 4px;"><img id="UserInteractionEntityInformationProviderNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="display: none;" runat="server" /></td>   

        
                    <td style="text-align: left"><b>Provider Name:</b> <asp:Label id="UserInteractionEntityInformationProviderName" Text="** No Provider Selected" runat="server" /></td>
                        
                    <td style="text-align: left"><b>NPI:</b> <asp:Label id="UserInteractionEntityInformationProviderNpi" Text="" runat="server" /></td>

                    <td style="text-align: left"><b>Program:</b> <asp:Label id="UserInteractionEntityInformationProviderProgram" Text="" runat="server" /></td>
                                                
                    <td style="text-align: left"><b>Id:</b> <asp:Label id="UserInteractionEntityInformationProviderProgramProviderId" Text="" runat="server" /></td>

                </tr>
        
            </table>

        </div>
        
        <!-- WORK QUEUE ITEM INFORMATION ( END ) -->


        <!-- WORKFLOW ACTION (BEGIN) -->

        <div style="display: block;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 36px;"><tr>
                        
                <td style="width: 20px;"><img id="WorkflowIcon" src="/Images/Common16/Gear.png" style="padding-right: 8px;" alt="Workflow Action" runat="server" /></td>
                
                <td style="width: 100px; font-weight: bold;">Workflow Action:</td>
                
                <td style="text-align: left;"><asp:Label ID="WorkflowActionMessage" Text="Workflow Starting" runat="server" /></td>

            </tr></table>

        </div>

        <!-- WORKFLOW ACTION ( END ) -->
        
        <!-- WORKFLOW LAST MESSAGE (BEGIN) -->

        <div id="WorkflowLastMessageContainer" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 36px;"><tr>
                        
                <td style="width: 20px;"><img id="WorkflowLastMessageIcon" src="/Images/Common16/Gear.png" style="padding-right: 8px;" alt="Last Message" runat="server" /></td>
                
                <td style="width: 100px; font-weight: bold;">Last Message:</td>
                
                <td style="text-align: left;"><asp:Label ID="WorkflowLastMessage" Text="Last Message" runat="server" /></td>

            </tr></table>

        </div>

        <!-- WORKFLOW LAST MESSAGE ( END ) -->
            
        <div id="WorkflowExceptionMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
                <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
                <td style="text-align: left;"><asp:Label ID="WorkflowExceptionMessage" runat="server" /></td>

                <td><a class="NoDecoration HoverTextBlack" href="#" ID="WorkflowExceptionExit" onclick="javascript:window.location='/Application/Workspace/Workspace.aspx';" runat="server">(close)</a></td>

            </tr></table>
            
        </div>
           
        <div id="WorkflowInformationalMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Informational.png" style="padding-right: 8px;" alt="Informational Indicator" /></td>
                               
                <td class="ColorDark" style="text-align: left;"><asp:Label ID="WorkflowInformationalMessage" runat="server" /></td>

            </tr></table>
            
        </div>
            
    </div>

    <!-- WORKFLOW INFORMATION ( END ) -->
    
    <!-- WORKFLOW FLOW CONTROL (BEGIN) -->

        <div id="WorkflowFlowControl" style="border-bottom: solid 1px black; display: block; height: 0px; width: 0px; overflow: hidden;" runat="server">
    
            <asp:Button ID="WorkflowStart" Text="Start" OnClick="WorkflowStart_OnClick" runat="server" />
        
            <asp:Button ID="WorkflowContinue" Text="Continue" OnClick="WorkflowContinue_OnClick" runat="server" />

            <asp:Button ID="WorkflowResume" Text="Resume" OnClick="WorkflowResume_OnClick" runat="server" />
        
            <asp:Button ID="WorkflowCancel" Text="Cancel" runat="server" />

        </div>               

    <!-- WORKFLOW FLOW CONTROL ( END ) -->

</div>

<!-- WORKFLOW HEADER ( END ) -->

<div id="WorkflowContentSection" style="padding-left: .125in; padding-bottom: .125in; padding-right: .125in; padding-bottom: .0625in;">

    <div id="WorkflowContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White;">
    
        <div ID="WorkflowContentPanel" style="overflow: auto;" runat="server" />

    </div>

</div>


<div id="FocusScript" style="display: none;" runat="server">

<Telerik:RadCodeBlock  ID="WorkflowCodeBlock" runat="server">

<script type="text/javascript" >

    if (document.addEventListener) { document.addEventListener('focus', Document_OnControlFocus, true); }

    if (document.addEventListener) { document.addEventListener('click', Document_OnClick, true); }

    function Document_OnControlFocus(forEvent) {

        lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

        lastBlurControl = document.getElementById("<%= LastBlurControl.ClientID %>");

        lastBlurControl.value = lastFocusControl.value;

        if (forEvent.target != null) {

            lastFocusControl.value = forEvent.target.id;

        }

        else if (forEvent.srcElement != null) {

            lastFocusControl.value = forEvent.srcElement.id;

        }

        return;

    }

    function Document_OnClick(event) {

        if (event.target != null) {

            if (event.target) {

                event.target.focus();

            }

        }

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

    function MemberInformationCoverage_Toggle() {

        var coverageDiv = document.getElementById("WorkQueueItemInformationMemberCoverage");

        var coverageAnchor = document.getElementById("MemberInformationCoverageToggle");

        if (coverageDiv != null) {

            if (coverageDiv.style.display == "none") {

                coverageDiv.style.display = "block";

                coverageAnchor.innerText = "(less)";

            }

            else {

                coverageDiv.style.display = "none";

                coverageAnchor.innerText = "(more)";

            }

        }

        return;

    }


</script>

<script type="text/javascript">

    function ButtonClicked_Disable(forButton) {

        if (forButton) {

            forButton.disabled = true;

        }

        return true;

    }

    function Control_SetVisible(forControl) {

        if (forControl) {

            forControl.style.display = 'block';

        }

        return true;

    }

    function Button_OnSubmit(forButton, forProgressControlName) {

        if (forButton) {

            forButton.disabled = true;

        }

        forProgressControl = document.getElementById(forProgressControlName);

        if (forProgressControl) {

            forProgressControl.style.display = 'block';

        }

        return true;

    }
                
    
</script>

</Telerik:RadCodeBlock>

</div>

<!-- DIALOG WINDOWS (BEGIN) -->

<Telerik:RadWindowManager ID="WorkflowWindowManager" runat="server">

    <Windows>
                        
        <Telerik:RadWindow ID="WorkflowCloseWindow" Behaviors="Close" Modal="true" Width="500" Height="280" VisibleStatusbar="false"  Title="Close the currently running Workflow?" runat="server">
                
            <ContentTemplate>
                
                <div id="DialogCloseContent">

                    <div style="margin: .125in" >

                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Close the currently running Workflow?</p>
                            
                        <p>This will close the actively running Workflow and return you to your Workspace. 
                                
                        </p>
                                
                        <p>If the Workflow is processing a Work Queue Item, 
                                
                        this action will not Reset the Work Queue Item and it will resume from where you left off, but none of the data (e.g. form data) or your 
                                
                        selections will be saved. </p>

                        <br />
                                

                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkflowCloseWindow_ButtonOk" Text="OK" OnClientClick="return WorkflowCloseWindow_Ok ();"  Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="Server" /></td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="WorkflowCloseWindow_ButtonCancel" Text="Cancel" OnClientClick="return WorkflowCloseWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

                            </tr></table>

                        </div>
            
                    </div>

                </div>

            </ContentTemplate>
                
        </Telerik:RadWindow>

    </Windows>

</Telerik:RadWindowManager>

<Telerik:RadCodeBlock ID="WindowFunctions" runat="server" >

    <script language="javascript" type="text/javascript">

        // SET WINDOW NAME TO UNIQUE NAME

        pageInstanceId = document.getElementById("<%= PageInstanceId.ClientID %>").value;

        window.name = "workflow_" + pageInstanceId;


        function Workflow_Close() {

            // SHOW DIALOG

            var dialogWindow = $find("<%= WorkflowCloseWindow.ClientID %>");

            dialogWindow.show();

            return;

        }

        function WorkflowCloseWindow_Ok() {

            var dialogWindow = $find("<%= WorkflowCloseWindow.ClientID %>");

            dialogWindow.close();


            var referralUrl = document.getElementById("<%= WorkflowReferrerUrl.ClientID %>");

            window.location.href = referralUrl.value;

            return false;

        }

        function WorkflowCloseWindow_Close() {

            var dialogWindow = $find("<%= WorkflowCloseWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }

    </script>
        
</Telerik:RadCodeBlock>

<!-- DIALOG WINDOWS ( END ) -->

</form>

</body>

</html>
