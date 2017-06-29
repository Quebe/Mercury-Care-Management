<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberCase.aspx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCase" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<%@ Register TagPrefix="MercuryUserControl" TagName="EntityInformationMember" Src="~/Application/Controls/EntityInformationMember.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseCarePlan" Src="~/Application/MemberCase/MemberCaseCarePlan.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseView" Src="~/Application/Controls/MemberCaseView.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberProfileDemographics" Src="~/Application/Controls/MemberDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseAuditHistory" Src="~/Application/MemberCase/MemberCaseAuditHistory.ascx" %>


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

    <asp:ScriptManager ID="AjaxScriptManager" AsyncPostBackTimeout="600" ScriptMode="Release" runat="Server">

        <Scripts>
        
            <asp:ScriptReference Path="~/Application/MemberCase/MemberCase.js" />

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


<Telerik:RadAjaxPanel ID="MemberCasePanel" ClientEvents-OnRequestStart="TelerikAjaxManager_OnRequestStart" ClientEvents-OnResponseEnd="TelerikAjaxManager_OnResponseEnd" LoadingPanelID="AjaxLoadingPanel" runat="server">

<div id="ApplicationTitleBar">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="padding-left: .125in">
            
                <img src="/Images/Common16/Case.png" alt="Member Case" />

            </td>

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">

                <a id="ApplicationTitle" class="NoDecoration HoverTextWhiteBold" href="/PermissionDenied.aspx" target="_blank" style="color: White; font-weight: bold; white-space: nowrap" runat="server">No Member Available</a>
                
            </td>

            <td style="padding-left: .125in; padding-right: .25in"><a class="NoDecoration ColorLight HoverTextWhiteBold" href="javascript:Tour();" style="white-space: nowrap; font-weight: bold; text-align: center;">Tour</a></td>

        </tr>
     
        <tr><td colspan="6" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<div id="NavigationBar" class="BackgroundColorComplementLight" style="display: block;">

    <Telerik:RadTabStrip ID="NavigationStrip" Skin="" EnableEmbeddedSkins="False" 

        MultiPageID="ContentMultiPage" SelectedIndex="0"  CssClass="RadTabStripBasic" 

        runat="server">
       
        <Tabs>
            
            <Telerik:RadTab Text="Overview" Selected="True" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Demographics" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

            <Telerik:RadTab Text="Care Plan" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

            <Telerik:RadTab Text="Journal" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>
            
            <Telerik:RadTab Text="Audit" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

            <Telerik:RadTab Text="Case History" SelectedCssClass="RadTabStripBasicSelected"></Telerik:RadTab>

        </Tabs>

    </Telerik:RadTabStrip>

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

    <!-- MEMBER CASE (BEGIN) -->
    
    <div id="ContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: 0in; overflow: auto">  

        <Telerik:RadMultiPage ID="ContentMultiPage" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="PageOverview" runat="server">

                <div style="padding-left: 8px; padding-right: 8px;">

                    <MercuryUserControl:EntityInformationMember ID="EntityInformationMemberControl" runat="server" />

                </div>

                <div class="BackgroundColorComplementDark" style="height: 1px; margin: 4px;"></div>
                
                <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left; width: 20%"><b>Case ID:</b> <asp:Label id="CaseId" Text="" runat="server" /></td>
                        
                        <td style="text-align: left; width: 40%"><b>Reference Number:</b>
                        
                            <asp:Label id="CaseReferenceNumberLabel" Text="" runat="server" />

                            <div id="CaseReferenceNumberEdit" style="display: none;">

                                <Telerik:RadTextBox ID="CaseReferenceNumber" Text="" runat="server"></Telerik:RadTextBox>
                            
                                <asp:LinkButton ID="CaseReferenceNumberSaveLink" OnClick="CaseReferenceNumberSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                            </div>

                            <div id="CaseReferenceNumberEditLink" runat="server">

                                <a id="CaseReferenceNumberEditToggle" href="javascript:CaseReferenceNumberEditToggle_OnClick ();">(edit)</a>

                            </div>

                        </td>
                        
                        <td style="text-align: left; width: 40%">
                        
                            <b>Locked By:</b> 
                        
                            <asp:Label id="CaseLockedBy" Text="Not Locked" runat="server" />

                            <asp:LinkButton ID="CaseLockToggleLink" Text="(lock)" OnClick="CaseLockToggleLink_OnClick" runat="server"></asp:LinkButton>
                            
                        </td>
                        
                    </tr>
        
                </table>                
                
                <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left; width: 25%"><b>Case Status:</b> <asp:Label id="CaseStatus" Text="Not Specified" runat="server" /></td>
                        
                        <td style="text-align: left; width: 20%"><b>Effective Date:</b> <asp:Label id="CaseEffectiveDate" Text="" runat="server" /></td>
                        
                        <td style="text-align: left; width: 20%"><b>Termination Date:</b> <asp:Label id="CaseTerminationDate" Text="< active >" runat="server" /></td>
                        
                        <td style="text-align: left; width: 35%"><b>Outcome:</b> <asp:Label id="CaseOutcome" Text="Not Specified" runat="server" /></td>
                        
                    </tr>
        
                </table>                
                
                <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left; width: 34%"><b>Care Level:</b> 
                            
                            <asp:Label id="CaseCareLevelLabel" Text="Not Specified" runat="server" />

                            <div id="CaseCareLevelEdit" style="display: none;">

                                <Telerik:RadComboBox ID="CaseCareLevelSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                                <asp:LinkButton ID="CaseCareLevelSaveLink" OnClick="CaseCareLevelSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                            </div>

                            <div id="CaseLevelChangeLink" runat="server">
                            
                                <a id="CaseCareLevelChangeToggle" href="javascript:CaseLevelChangeLinkChangeToggle_OnClick ();">(change)</a>

                            </div>

                        </td>
                        
                        <td style="text-align: left; width: 33%"><b>Care Team:</b> 
                        
                            <asp:Label id="CaseAssignedToWorkTeamLabel" Text="** Not Assigned" runat="server" />

                            <div id="CaseAssignedToWorkTeamEdit" style="display: none;">
                            
                                <Telerik:RadComboBox ID="CaseAssignedToWorkTeamSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                                <asp:LinkButton ID="CaseAssignedToWorkTeamSaveLink" OnClick="CaseAssignedToWorkTeamSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                            </div>
                              
                            <div id="CaseAssignedToWorkTeamChangeLink" runat="server">

                                <a id="CaseAssignedToWorkTeamChangeToggle" href="javascript:CaseAssignedToWorkTeamChangeToggle_OnClick ();">(change)</a>

                            </div>
                                                      
                        </td>
                        
                        <td style="text-align: left; width: 33%"><b>Team Member:</b> 
                        
                            <asp:Label id="CaseAssignedToUserLabel" Text="** Not Assigned" runat="server" />
                        
                            <div id="CaseAssignedToUserEdit" style="display: none;">
                            
                                <Telerik:RadComboBox ID="CaseAssignedToUserSelection" Width="100%" runat="server"></Telerik:RadComboBox>

                                <asp:LinkButton ID="CaseAssignedToUserSaveLink" OnClick="CaseAssignedToUserSaveLink_OnClick" runat="server">(save)</asp:LinkButton>

                            </div>
                              
                            <div id="CaseAssignedToUserChangeLink" runat="server">

                                <a id="CaseAssignedToUserChangeToggle" href="javascript:CaseAssignedToUserChangeToggle_OnClick ();">(change)</a>

                            </div>

                        </td>
                        
                    </tr>
        
                </table>           
                
                <div style="padding-left: 8px; padding-right: 8px; margin-top: 8px; line-height: 150%;">
                
                    <span style="font-weight: bold;">Description: </span>

                    <div id="CaseDescriptionLessDiv" style="display: inline;">

                        <asp:Label ID="CaseDescriptionLess" Text="" runat="server"></asp:Label>

                        &nbsp;<a id="CaseDescriptionShowMoreLink" href="javascript:CaseDescriptionShowMoreLink_OnClick();">(more)</a>

                    </div>
                    
                    <div id="CaseDescriptionMoreDiv" style="display: none;">

                        <asp:Label ID="CaseDescriptionMore" Text="" runat="server"></asp:Label>

                        &nbsp;<a id="CaseDescriptionShowLessLink" href="javascript:CaseDescriptionShowLessLink_OnClick ();">(less)</a>

                        &nbsp;<a id="CaseDescriptionEditLink" href="javascript:CaseDescriptionEditLink_OnClick ();">(edit)</a>

                    </div>

                    <div id="CaseDescriptionEditDiv" style="display: none;">
                    
                        <Telerik:RadTextBox ID="CaseDescription" Width="100%" MaxLength="8000" TextMode="MultiLine" Rows="10" runat="server"></Telerik:RadTextBox>
                    
                        <asp:LinkButton ID="CaseDescriptionSaveLink" OnClick="CaseDescriptionSaveLink_OnClick" runat="server">(save)</asp:LinkButton>
                                
                        &nbsp;<a id="CaseDescriptionEditRestoreLink" href="javascript:CaseDescriptionEditRestoreLink_OnClick ();">(restore)</a>

                        &nbsp;<a id="CaseDescriptionEditCancelLink" href="javascript:CaseDescriptionEditCancelLink_OnClick ();">(cancel)</a>

                    </div>

                </div>     
                
                <div class="BackgroundColorComplementDark" style="height: 1px; margin: 4px;"></div>
                
                <div id="NotificationSection" visible="false" runat="server">
                
                    <table width="100%" cellpadding="0" cellspacing="0" style="padding-left: 8px; padding-right: 8px;">
    
                        <tr class="" style="height: 36px;"><td style="text-align: left;"><b>** Notifications</b></td></tr>

                        <tr><td style="padding-left: .125in; padding-right: .125in;">
                        
                            <Telerik:RadListBox ID="CaseNotifications" Width="100%" SelectionMode="Single" Enabled="true" runat="server">

                                <Items>
                                
                                </Items>
                        
                            </Telerik:RadListBox>
                        
                        </td></tr>
                    
                    </table>                                            
                
                </div>

                <div class="PropertyPageSectionTitle">Problem Statements</div>
                
                    <Telerik:RadGrid ID="ProblemStatementsGrid" AutoGenerateColumns="false"  Width="100%" 
                    
                        OnNeedDataSource="ProblemStatementsGrid_OnNeedDataSource"

                        OnItemCommand="ProblemStatementsGrid_OnItemCommand"
                    
                        runat="server">
                        
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="Id">

                            <Columns>
        
                                <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                                <Telerik:GridBoundColumn DataField="Classification" HeaderText="Problem Statement Domain/Class"></Telerik:GridBoundColumn>

                                <Telerik:GridBoundColumn DataField="AssignedToUserDisplayName" HeaderText="Assigned To"></Telerik:GridBoundColumn>

                                <Telerik:GridBoundColumn DataField="AssignedToProvider.Name" HeaderText="Provider"></Telerik:GridBoundColumn>

                            </Columns>         
                            
                            <DetailTables>

                                <Telerik:GridTableView DataKeyNames="Id,MemberCaseProblemClassId" runat="server">
                                
                                    <ParentTableRelation>
                                    
                                        <Telerik:GridRelationFields MasterKeyField="Id" DetailKeyField="MemberCaseProblemClassId" />

                                    </ParentTableRelation>

                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                                        <Telerik:GridBoundColumn DataField="MemberCaseProblemClassId" HeaderText="MemberCaseProblemClassId" Visible="false"></Telerik:GridBoundColumn>
                                        
                                        <Telerik:GridBoundColumn DataField="ProblemStatementName" HeaderText="Problem" ItemStyle-Wrap="false" Visible="true"></Telerik:GridBoundColumn>

                                        <Telerik:GridBoundColumn DataField="ProblemStatement.Description" HeaderText="Description" Visible="true"></Telerik:GridBoundColumn>

                                        <Telerik:GridBoundColumn DataField="MemberCaseCarePlan.StatusDescription" HeaderText="Status" Visible="true"></Telerik:GridBoundColumn>
                                    
                                        <Telerik:GridButtonColumn HeaderText="Action" CommandName="DeleteProblemStatement" Text="(delete)" ConfirmText="Are you sure you want to delete this Problem?"></Telerik:GridButtonColumn>
                                                                   
                                    </Columns>
                                
                                </Telerik:GridTableView>
                            
                            </DetailTables>                                                         
                                         
                        </MasterTableView>

                        <ClientSettings>
                                
                            <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                        </ClientSettings>

                    </Telerik:RadGrid>

                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Interventions and Activities</div>

                <Telerik:RadGrid ID="MemberCaseInterventionsGrid" AutoGenerateColumns="false"  Width="100%" 
                    
                    OnNeedDataSource="MemberCaseInterventionsGrid_OnNeedDataSource"
                    
                    runat="server">
                        
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="Id">
                                   
                        <Columns>
        
                            <Telerik:GridBoundColumn DataField="Name" HeaderText="Intervention" />

                            <Telerik:GridBoundColumn DataField="StatusDescription" HeaderText="Status" />

                            <Telerik:GridBoundColumn DataField="Name" HeaderText="Next Activity" />

                            <Telerik:GridBoundColumn DataField="Name" HeaderText="Activity Date" />

                        </Columns>
                                
                        <DetailTables>

                            <Telerik:GridTableView DataKeyNames="Id,MemberCaseCareInterventionId">
                                
                                <ParentTableRelation>
                                    
                                    <Telerik:GridRelationFields MasterKeyField="Id" DetailKeyField="MemberCaseCareInterventionId" />

                                </ParentTableRelation>

                                <Columns>
                                    
                                    <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="MemberCaseCareInterventionId" HeaderText="MemberCaseCareInterventionId" Visible="false"></Telerik:GridBoundColumn>
                                        
                                    <Telerik:GridBoundColumn DataField="MemberCaseCarePlanGoal.MemberCaseCarePlan.Name" HeaderText="Care Plan" ItemStyle-Wrap="false" Visible="true"></Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="MemberCaseCarePlanGoal.Name" HeaderText="Goal" ItemStyle-Wrap="false" Visible="true"></Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="MemberCaseCarePlanGoal.ClinicalNarrative" HeaderText="Clinical Narrative" Visible="true"></Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="Inclusion" HeaderText="Inclusion" Visible="true"></Telerik:GridBoundColumn>

                                    <Telerik:GridBoundColumn DataField="IsSingleInstance" HeaderText="Single Instance" Visible="true"></Telerik:GridBoundColumn>
                                    
                                </Columns>
                                
                            </Telerik:GridTableView>
                            
                        </DetailTables>       

                    </MasterTableView>

                    <ClientSettings>
                                
                        <Scrolling AllowScroll="false" UseStaticHeaders="true" />
                                
                    </ClientSettings>
                
                </Telerik:RadGrid>

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageDemographics" runat="server">

                <MercuryUserControl:MemberProfileDemographics ID="MemberDemographicsControl" runat="server" />

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageCarePlan" BackColor="#B4B4B4" runat="server">

                <MercuryUserControl:MemberCaseCarePlan ID="MemberCaseCarePlanControl" runat="server"></MercuryUserControl:MemberCaseCarePlan>                            

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageJournal" runat="server">
                            

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageAudit" runat="server">
                            
                <MercuryUserControl:MemberCaseAuditHistory ID="MemberCaseAuditHistoryControl" runat="server"></MercuryUserControl:MemberCaseAuditHistory>

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageCaseHistory" runat="server">
                            
                <MercuryUserControl:MemberCaseView ID="MemberCaseViewControl" runat="server" />

            </Telerik:RadPageView>

        </Telerik:RadMultiPage>
    
    </div>

    <!-- MEMBER CASE ( END ) -->
    
</div>

</Telerik:RadAjaxPanel>


<Telerik:RadCodeBlock runat="server">

<script type="text/javascript">

    function TelerikAjaxManager_OnRequestStart(sender, e) {

        lastScrollToPositionY = document.getElementById("<%= LastScrollToPositionY.ClientID %>");


        contentContainer = document.getElementById("ContentContainer");

        lastScrollToPositionY.value = 0;

        if (contentContainer != null) { lastScrollToPositionY.value = contentContainer.scrollTop; }

        return;

    }

    function TelerikAjaxManager_OnResponseEnd(sender, eventArgs) {

        Page_Repaint();

        setTimeout(AjaxResponse_SetFocus, 1);

        return;

    }

    function AjaxResponse_SetFocus() {

        lastScrollToPositionY = document.getElementById("<%= LastScrollToPositionY.ClientID %>");

        contentContainer = document.getElementById("ContentContainer");

        contentContainer.scrollTop = lastScrollToPositionY.value;

        return;

    }

    function CaseReferenceNumberEditToggle_OnClick() {

        caseReferenceNumberLabel = document.getElementById("<%= CaseReferenceNumberLabel.ClientID %>");

        caseReferenceNumberEditToggle = document.getElementById("CaseReferenceNumberEditToggle");

        caseReferenceNumberEdit = document.getElementById("CaseReferenceNumberEdit");

        if (caseReferenceNumberEdit != null) {

            if (caseReferenceNumberEdit.style.display == "none") {

                caseReferenceNumberLabel.style.display = "none";

                caseReferenceNumberEdit.style.display = "inline";

                caseReferenceNumberEditToggle.innerHTML = "(cancel)";

            }

            else {

                caseReferenceNumberLabel.style.display = "inline";

                caseReferenceNumberEdit.style.display = "none";

                caseReferenceNumberEditToggle.innerHTML = "(edit)";
            
            }

        }

        Page_Repaint();

        return;

    }

    function CaseAssignedToWorkTeamChangeToggle_OnClick() {

        caseAssignedToWorkTeamLabel = document.getElementById("<%= CaseAssignedToWorkTeamLabel.ClientID %>");

        caseAssignedToWorkTeamChangeToggle = document.getElementById("CaseAssignedToWorkTeamChangeToggle");

        caseAssignedToWorkTeamEdit = document.getElementById("CaseAssignedToWorkTeamEdit");

        if (caseAssignedToWorkTeamEdit != null) {

            if (caseAssignedToWorkTeamEdit.style.display == "none") {

                caseAssignedToWorkTeamLabel.style.display = "none";

                caseAssignedToWorkTeamEdit.style.display = "inline";

                caseAssignedToWorkTeamChangeToggle.innerHTML = "(cancel)";

            }

            else {

                caseAssignedToWorkTeamLabel.style.display = "inline";

                caseAssignedToWorkTeamEdit.style.display = "none";

                caseAssignedToWorkTeamChangeToggle.innerHTML = "(change)";

            }

        } 
        
        Page_Repaint();

        return;

    }

    function CaseAssignedToUserChangeToggle_OnClick() {

        caseAssignedToUserLabel = document.getElementById("<%= CaseAssignedToUserLabel.ClientID %>");

        caseAssignedToUserChangeToggle = document.getElementById("CaseAssignedToUserChangeToggle");

        caseAssignedToUserEdit = document.getElementById("CaseAssignedToUserEdit");

        if (caseAssignedToUserEdit != null) {

            if (caseAssignedToUserEdit.style.display == "none") {

                caseAssignedToUserLabel.style.display = "none";

                caseAssignedToUserEdit.style.display = "inline";

                caseAssignedToUserChangeToggle.innerHTML = "(cancel)";

            }

            else {

                caseAssignedToUserLabel.style.display = "inline";

                caseAssignedToUserEdit.style.display = "none";

                caseAssignedToUserChangeToggle.innerHTML = "(change)";

            }

        }

        Page_Repaint();

        return;

    }

    function CaseDescriptionShowMoreLink_OnClick() {

        caseDescriptionLessDiv = document.getElementById("CaseDescriptionLessDiv");

        caseDescriptionMoreDiv = document.getElementById("CaseDescriptionMoreDiv");

        caseDescriptionEditDiv = document.getElementById("CaseDescriptionEditDiv");


        caseDescriptionLessDiv.style.display = "none";

        caseDescriptionMoreDiv.style.display = "inline";

        caseDescriptionEditDiv.style.display = "none";

        return;

    }

    function CaseDescriptionShowLessLink_OnClick() {

        caseDescriptionLessDiv = document.getElementById("CaseDescriptionLessDiv");

        caseDescriptionMoreDiv = document.getElementById("CaseDescriptionMoreDiv");

        caseDescriptionEditDiv = document.getElementById("CaseDescriptionEditDiv");


        caseDescriptionLessDiv.style.display = "inline";

        caseDescriptionMoreDiv.style.display = "none";

        caseDescriptionEditDiv.style.display = "none";

        return;

    }

    function CaseDescriptionEditLink_OnClick() {

        caseDescriptionLessDiv = document.getElementById("CaseDescriptionLessDiv");

        caseDescriptionMoreDiv = document.getElementById("CaseDescriptionMoreDiv");

        caseDescriptionEditDiv = document.getElementById("CaseDescriptionEditDiv");


        caseDescriptionLessDiv.style.display = "none";

        caseDescriptionMoreDiv.style.display = "none";

        caseDescriptionEditDiv.style.display = "block";

        return;

    }

    function CaseDescriptionEditRestoreLink_OnClick() {

        caseDescriptionMore = document.getElementById("CaseDescriptionMore"); // SPAN THAT CONTAINS ORIGINAL TEXT

        caseDescription = $find('<%= CaseDescription.ClientID %>');

        if (caseDescription == null) { return; }

        caseDescription.set_value(caseDescriptionMore.innerText);

        return;

    }

    function CaseDescriptionEditCancelLink_OnClick() {

        caseDescriptionLessDiv = document.getElementById("CaseDescriptionLessDiv");

        caseDescriptionMoreDiv = document.getElementById("CaseDescriptionMoreDiv");

        caseDescriptionEditDiv = document.getElementById("CaseDescriptionEditDiv");


        caseDescriptionLessDiv.style.display = "none";

        caseDescriptionMoreDiv.style.display = "inline";

        caseDescriptionEditDiv.style.display = "none";

        return;

    }

    function CaseLevelChangeLinkChangeToggle_OnClick() {

        caseCareLevelLabel = document.getElementById("<%= CaseCareLevelLabel.ClientID %>");

        caseCareLevelChangeToggle = document.getElementById("CaseCareLevelChangeToggle");

        caseCareLevelEdit = document.getElementById("CaseCareLevelEdit");

        if (caseCareLevelEdit != null) {

            if (caseCareLevelEdit.style.display == "none") {

                caseCareLevelLabel.style.display = "none";

                caseCareLevelEdit.style.display = "inline";

                caseCareLevelChangeToggle.innerHTML = "(cancel)";

            }

            else {

                caseCareLevelLabel.style.display = "inline";

                caseCareLevelEdit.style.display = "none";

                caseCareLevelChangeToggle.innerHTML = "(change)";

            }

        }

        Page_Repaint();

        return;

    }

</script>

</Telerik:RadCodeBlock>


</form>

</body>

</html>
