<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberProfile.aspx.cs" Inherits="Mercury.Web.Application.MemberProfile.MemberProfile" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<%@ Register TagPrefix="MercuryUserControl" TagName="MemberProfileDemographics" Src="~/Application/Controls/MemberDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityContactHistory" Src="~/Application/Controls/EntityContactHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityDocumentHistory" Src="~/Application/Controls/EntityDocumentHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityNoteHistory" Src="~/Application/Controls/EntityNoteHistory.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberWorkHistory" Src="~/Application/Controls/MemberWorkHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberServices" Src="~/Application/Controls/MemberServices.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberMetrics" Src="~/Application/Controls/MemberMetrics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberAuthorizedServices" Src="~/Application/Controls/MemberAuthorizedServices.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberAuthorizationHistory" Src="~/Application/Controls/MemberAuthorizationHistory.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberClaimHistory" Src="~/Application/Controls/MemberClaimHistory.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberCaseView" Src="~/Application/Controls/MemberCaseView.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Member Profile</title>
    
    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />

    <style type="text/css">

    .radReadOnlyCss_Office2007 {
    	border:1px solid #999999 !important;
        color:#000 !important;
        font:12px segoe ui, arial,tahoma,sans-serif !important;
	    background:#fff !important;
	    padding:1px 0 1px 1px !important;
    }
        
    .TabPageClass { width: 100%; height: 402px; font-family: segoe ui, arial; font-size: 12px; overflow: auto; }

    </style>
    
</head>

<body style="margin: 0px;" class="TextNormal BackgroundColorLight">

<form id="FormMemberProfile" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div style="display: none"><asp:TextBox ID="MemberId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none; background-color:#6699CC;">

    <asp:ScriptManager ID="AjaxScriptManager" AsyncPostBackTimeout="600" runat="Server" />
    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="MemberTabStrip">
              
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="MemberTabStrip" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="MemberMultipage" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="MemberEnrollmentGrid"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberEnrollmentGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="MemberServicesGrid"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="MemberMetricsGrid"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberMetricsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="MemberServiceToolbar"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberServicesGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
        
            <Telerik:AjaxSetting AjaxControlID="MemberMetricToolbar"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberMetricsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="PopulationCareManagementGrid"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="PopulationCareManagementGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
                        
        </AjaxSettings>
            
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="100" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
</div>


<div id="TitleBar" runat="server">

    <table style="width: 100%; table-layout: auto; background-color:#6699CC; border-bottom: solid 1px black" border="0"><tr>

        <td style="width: 32px"><img id="GlobalSearchTitleImage" src="/Images/Common24/Person.png" alt="Member Profile" /></td>    
       
        <td style="max-width: 32px;"><img id="EntityNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="padding: 4px" visible="false" runat="server" /></td>   
        
        <td style="max-width: 32px;"><img id="EntityNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="padding: 4px" visible="false" runat="server" /></td>   

        <td style="width: 100%; text-align: left; vertical-align: middle; font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold">
        
            <div style="font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold; text-decoration: none;"><asp:Label ID="MemberDemographicHeaderLabel" Style="overflow: hidden" Text="Member Name (Age | Gender)" runat="server" /></div>
    
        </td>
        
    </tr></table>

<div>
    
    <Telerik:RadToolBar ID="MemberProfileToolbar" Width="100%" OnClientButtonClicked="MemberProfileToolbar_OnClientButtonClicked" runat="server">
    
        <Items>
        
            <Telerik:RadToolBarButton Text="Search" Value="Search" ToolTip="Search for another Member" ImagePosition="AboveText" ImageUrl="/Images/Common16/SearchMember.png" Visible="false" />
            
            <Telerik:RadToolBarButton ID="MemberProfileToolbar_Action" Text="Action" Value="Action"  CommandName="Action" ImagePosition="AboveText" ImageUrl="/Images/Common16/Gear.png" Visible="true"  />
            
            <Telerik:RadToolBarButton IsSeparator="true" Visible="true" />
            
            <Telerik:RadToolBarButton Text="Contact" Value="Contact" ToolTip="Contact Member" ImagePosition="AboveText" ImageUrl="/Images/Common16/Phone.png" />
            
            <Telerik:RadToolBarButton IsSeparator="false" />
            
            <Telerik:RadToolBarButton Text="Send" Value="SendCorrespondence" ToolTip="Send Correspondence" ImagePosition="AboveText" ImageUrl="/Images/Common16/Address.png" />
        
            <Telerik:RadToolBarButton IsSeparator="false" />
            
            <Telerik:RadToolBarButton Text="Note" Value="CreateNote" ToolTip="Add a Note" ImagePosition="AboveText" ImageUrl="/Images/Common16/Note.png" />
            
            <Telerik:RadToolBarButton IsSeparator="false" />
            
            <Telerik:RadToolBarButton Text="Address" Value="CreateAddress" ToolTip="Add an Address" ImagePosition="AboveText" ImageUrl="/Images/Common16/Address.png" />

        </Items>
    
    </Telerik:RadToolBar>
    
    <Telerik:RadToolTip 
     ID="MemberActionToolTip" TargetControlID="MemberProfileToolbar_Action" IsClientID="true" RelativeTo="Element" Position="BottomRight" ShowEvent="FromCode" HideEvent="FromCode" Skin="Hay" Animation="Fade" runat="server">
    
        <div style="margin-top: .125in; margin-bottom: 0in; padding: .125in; width: 400px;">
        
        <!-- ToolTip="Perform an Action on the Member" -->
            <table>
                <tr>
                    <td style="width: 65px">
                        Action:
                    </td>
                    <td>
                        <Telerik:RadComboBox ID="MemberActionSelection" Width="100%" ZIndex="9999" runat="server">
                        </Telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                    <td>
                        <table width="100%" cellpadding="2" style="margin-top: 8px;">
                            <tr>
                                <td>
                                    &nbsp
                                </td>
                                <td align="right" style="width: 90px; max-width: 90px;">
                                    <asp:Button ID="MemberActionGo" runat="server" Text="Go" Width="73px" Font-Names="segoe ui, arial"
                                        Font-Size="11px" Height="24" OnClick="MemberActionGo_OnClick" />
                                </td>
                                <td align="right" style="width: 90px; max-width: 90px;">
                                    <input type="button" onclick="javascript:$find ('MemberActionToolTip').hide ();"
                                        value="Cancel" style="width: 73px; height: 24px; font-family: segoe ui, arial;
                                        font-size: 11px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            
        </div>
    
    </Telerik:RadToolTip>
    
    <script type="text/javascript">

        function MemberProfileToolbar_OnClientButtonClicked(sender, e) {

            var button = e.get_item();

            switch (button.get_commandName()) {

                case "Action":

                    var memberActionToolTip = $find("MemberActionToolTip");

                    if (memberActionToolTip != null) { memberActionToolTip.show(); }

                    e.set_cancel = true;

                    break;

            }

            return;
            
        }

    </script>

</div>

</div>

<div style="height: 100%; line-height: 150%; border: solid 1px black">

    <div style="width: 100%; height: 32px; background-color: #bfdbff; font-family: segoe ui, arial; font-size: 16px; line-height: 150%; vertical-align: middle; cursor: pointer; display: none" onclick="SectionCollapseExpand ('MemberDemographics');">
    
        <div style="float: left; height: 32px; padding: 0px;"><img id="MemberDemographicsExpandCollapse" src="/Images/Common16/TreeViewChildrenExpanded.png" alt="Expand or Collapse" style="padding: 6px;" /></div>
        
    </div>
    
    <div id="MemberDemographicsContent" style="display: block;">

        <Telerik:RadTabStrip ID="MemberTabStrip" MultiPageID="MemberMultipage" SelectedIndex="0" OnTabClick="MemberTabStrip_OnTabClick" ScrollChildren="true" ScrollButtonsPosition="Right" PerTabScrolling="true" EnableViewState="true" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="Demographic" PageViewID="PageMemberDemographic" Selected="True"></Telerik:RadTab>

                <Telerik:RadTab Text="Enrollment">
                
                    <Tabs>
                    
                        <Telerik:RadTab Text="Enrollment" Selected="true" PageViewID="PageMemberEnrollment" />
                        
                        <Telerik:RadTab Text="TPL/COB" PageViewID="PageMemberTplCob" />
                    
                    </Tabs>
                
                </Telerik:RadTab>

                <Telerik:RadTab Text="Services">
                
                    <Tabs>
                    
                        <Telerik:RadTab Text="Services" PageViewID="PageMemberServices" Selected="true" runat="server" />
                        
                        <Telerik:RadTab Text="Metrics" PageViewID="PageMemberMetrics" runat="server" />
                        
                        <Telerik:RadTab Text="Authorized Services" PageViewID="PageMemberAuthorizedServices" runat="server" />
                    
                    </Tabs>
                
                </Telerik:RadTab>

                <Telerik:RadTab Text="Contacts" PageViewID="PageMemberContactHistory"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Documents" PageViewID="PageMemberDocuments"></Telerik:RadTab>

                <Telerik:RadTab Text="Notes" PageViewID="PageMemberNotes"></Telerik:RadTab>

                <Telerik:RadTab Text="Work History" PageViewID="PageMemberWorkHistory"></Telerik:RadTab>
               
                <Telerik:RadTab Text="Claims" PageViewID="PageMemberClaims"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Authorizations" PageViewID="PageMemberAuthorizations"></Telerik:RadTab>
                
            </Tabs>
                  
        </Telerik:RadTabStrip>
        
        <Telerik:RadMultiPage ID="MemberMultipage" SelectedIndex="0" EnableViewState="true" BackColor="White" runat="server">
        
            <Telerik:RadPageView ID="PageMemberDemographic" runat="server">
             
                <div class="TabPageClass">
                
                    <MercuryUserControl:MemberProfileDemographics ID="MemberDemographicsControl" runat="server" />
                
                </div>
                
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberEnrollment" runat="server">
            
                <div class="TabPageClass" id="PageMemberEnrollmentPlugin" runat="server">

                    <Telerik:RadGrid ID="MemberEnrollmentGrid" Height="400" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="false" OnItemCommand="MemberEnrollmentGrid_OnItemCommand" OnNeedDataSource="MemberEnrollmentGrid_OnNeedDataSource" runat="server">
                    
                        <MasterTableView Name="MemberEnrollment" TableLayout="Auto" DataKeyNames="EnrollmentId">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="EnrollmentId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerId" UniqueName="InsurerId" HeaderText="Insurer" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerName" UniqueName="InsurerName" HeaderText="Insurer" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramId" UniqueName="ProgramId" HeaderText="Program" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="ProgramName" UniqueName="ProgramName" HeaderText="Program" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="SponsorId" UniqueName="SponsorId" HeaderText="Sponsor" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="SponsorName" UniqueName="SponsorName" HeaderText="Sponsor" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="SubscriberId" UniqueName="SubscriberId" HeaderText="Subscriber" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="SubscriberName" UniqueName="SubscriberName" HeaderText="Subscriber" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramMemberId" UniqueName="ProgramMemberId" HeaderText="Member Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="RateCode" UniqueName="RateCode" HeaderText="Rate Code" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="PcpProviderName" UniqueName="PcpProviderName" HeaderText="PCP Provider" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />
                    
                                <Telerik:GridBoundColumn DataField="SortDateField" UniqueName="SortDateField" Visible="false" />
                            
                            </Columns>
                            
                            <SortExpressions><Telerik:GridSortExpression FieldName="SortDateField" SortOrder="Descending" /></SortExpressions>

                            <DetailTables>
                            
                                <Telerik:GridTableView Name="MemberEnrollmentCoverage" DataKeyNames="MemberEnrollmentId" AllowPaging="false" Width="100%">
                                
                                    <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="EnrollmentId" DetailKeyField="MemberEnrollmentId" /></ParentTableRelation>
                                    
                                    <Columns>
                                                                       
                                        <Telerik:GridBoundColumn DataField="MemberEnrollmentId" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="Id" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="BenefitPlanName" HeaderText="Benefit Plan" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="CoverageTypeName" HeaderText="Coverage Type" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="CoverageLevelName"  HeaderText="Coverage Level" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="RateCode" HeaderText="Rate Code"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                                        <Telerik:GridBoundColumn DataField="EffectiveDate"  HeaderText="Effective" DataFormatString="{0:MM/dd/yyyy}"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />

                                        <Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination" DataFormatString="{0:MM/dd/yyyy}"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                    
                                    </Columns>
                                    
                                    <SortExpressions><Telerik:GridSortExpression FieldName="TerminationDate" SortOrder="Descending" /></SortExpressions>

                                </Telerik:GridTableView>
                                
                                <Telerik:GridTableView Name="MemberEnrollmentPcpAssignment" DataKeyNames="EnrollmentId" AllowPaging="false" Width="100%">
                                
                                    <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="EnrollmentId" DetailKeyField="EnrollmentId" /></ParentTableRelation>
                                    
                                    <Columns>
                                                                           
                                        <Telerik:GridBoundColumn DataField="EnrollmentId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="PcpAssignmentId" UniqueName="PcpAssignmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="PcpProviderId" UniqueName="PcpProviderId" HeaderText="PcpProviderId" ReadOnly="true" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="PcpProviderName" UniqueName="PcpProviderName" HeaderText="PCP Provider" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="PcpAffiliateId" UniqueName="PcpAffiliateId" HeaderText="PcpAffiliateId" ReadOnly="true" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="PcpAffiliateName" UniqueName="PcpAffiliateName" HeaderText="PCP Affiliate" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />
                                        
                                        <Telerik:GridBoundColumn DataField="SortDateField" UniqueName="SortDateField" Visible="false" />
                                    
                                    </Columns>
                                    
                                    <SortExpressions><Telerik:GridSortExpression FieldName="SortDateField" SortOrder="Descending" /></SortExpressions>

                                </Telerik:GridTableView>
                            
                            </DetailTables>
                        
                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                        
                        <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>
                    
                    </Telerik:RadGrid>
                   
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberTplCob" runat="server">
                            
                <div class="TabPageClass" id="Div6" runat="server">                                    
                    
                    <Telerik:RadGrid ID="MemberEnrollmentTplCobGrid" Height="400" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="false" 
                    
                        OnNeedDataSource="MemberEnrollmentTplCobGrid_OnNeedDataSource" runat="server">
                    
                        <MasterTableView Name="MemberTplCob" TableLayout="Auto" DataKeyNames="EnrollmentTplCobId">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="EnrollmentTplCobId" UniqueName="TplCobId" HeaderText="Id" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerId" UniqueName="InsurerId" HeaderText="Insurer" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerName" UniqueName="InsurerName" HeaderText="Insurer" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramId" UniqueName="ProgramId" HeaderText="Program" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="ProgramName" UniqueName="ProgramName" HeaderText="Program" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="SponsorId" UniqueName="SponsorId" HeaderText="Sponsor" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="SponsorName" UniqueName="SponsorName" HeaderText="Sponsor" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="SubscriberId" UniqueName="SubscriberId" HeaderText="Subscriber" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="SubscriberName" UniqueName="SubscriberName" HeaderText="Subscriber" ReadOnly="true" Visible="true" />
                                
                                <Telerik:GridBoundColumn DataField="BenefitPlanName" UniqueName="BenefitPlanName" HeaderText="Benefit Plan" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramMemberId" UniqueName="ProgramMemberId" HeaderText="Member Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />
                    
                                <Telerik:GridBoundColumn DataField="SortDateField" UniqueName="SortDateField" Visible="false" />
                            
                            </Columns>
                            
                            <SortExpressions><Telerik:GridSortExpression FieldName="SortDateField" SortOrder="Descending" /></SortExpressions>
                        
                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                        
                        <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>
                    
                    </Telerik:RadGrid>
                   
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberMetrics" runat="server">
            
                <div class="TabPageClass" runat="server">
                
                    <MercuryUserControl:MemberMetrics ID="MemberMetricsControl" HistoryGridHeight="400" runat="server" />

                </div>
        
            </Telerik:RadPageView>
                                      
            <Telerik:RadPageView ID="PageMemberServices" runat="server">
            
                <div id="Div7" class="TabPageClass" runat="server">

                    <MercuryUserControl:MemberServices ID="MemberServicesControl" HistoryGridHeight="400" runat="server" />
                    
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberAuthorizedServices" runat="server">
                            
                <div id="Div2" class="TabPageClass" runat="server">
                                    
                    <MercuryUserControl:MemberAuthorizedServices ID="MemberAuthorizedServicesControl" HistoryGridHeight="400" runat="server" />
                    
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberContactHistory" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:EntityContactHistory ID="EntityContactHistoryControl" HistoryGridHeight="400" runat="server" />
                
                </div>
            
            </Telerik:RadPageView>
             
            <Telerik:RadPageView ID="PageMemberDocuments" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:EntityDocumentHistory ID="EntityDocumentHistoryControl" HistoryGridHeight="400" runat="server" />
                    
                </div>
        
            </Telerik:RadPageView>
           
            <Telerik:RadPageView ID="PageMemberNotes" runat="server">
            
                <div id="Div3" class="TabPageClass" runat="server">
                
                    <MercuryUserControl:EntityNoteHistory ID="EntityNoteHistoryControl" HistoryGridHeight="400" runat="server" />
                                    
                </div>
        
            </Telerik:RadPageView>
                  
            <Telerik:RadPageView ID="PageMemberWorkHistory" runat="server">
            
                <div id="Div1" class="TabPageClass" runat="server">

                    <MercuryUserControl:MemberWorkHistory ID="MemberWorkHistoryControl" HistoryGridHeight="400" runat="server" />

                </div>
        
            </Telerik:RadPageView>
              
            <Telerik:RadPageView ID="PageMemberClaims" runat="server">
            
                <div id="Div4" class="TabPageClass" runat="server">

                    <MercuryUserControl:MemberClaimHistory ID="MemberClaimHistoryControl" HistoryGridHeight="400" runat="server" />
                
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageMemberAuthorizations" runat="server">
                            
                <div id="Div5" class="TabPageClass" runat="server">
                                    
                    <MercuryUserControl:MemberAuthorizationHistory ID="MemberAuthorizationHistoryControl" HistoryGridHeight="400" runat="server" />

                </div>
        
            </Telerik:RadPageView>
            
        </Telerik:RadMultiPage>
        
    </div>
                        
</div>                        
                        
<div style="height: 100%; line-height: 150%; border: solid 1px black">

    <div style="width: 100%; height: 32px; background-color: #bfdbff; font-family: segoe ui, arial; font-size: 16px; line-height: 150%; vertical-align: middle; cursor: pointer" onclick="SectionCollapseExpand ('PopulationCareManagement');">
    
        <div style="float: left; height: 32px; padding: 0px;"><img id="PopulationCareManagementExpandCollapse" src="/Images/Common16/TreeViewChildrenExpanded.png" alt="Expand or Collapse" style="padding: 6px;" /></div>
        
        <div style="float: left; height: 32px; vertical-align: middle">Care Management</div>
    
    </div>
    
    <div id="PopulationCareManagementContent" style="display: block;">
    
        <Telerik:RadTabStrip ID="CareManagementTabStrip" MultiPageID="CareManagementMultiPage" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="Population"></Telerik:RadTab>

                <Telerik:RadTab Text="Individual" Visible="true"></Telerik:RadTab>

                <Telerik:RadTab Text="Episodic" Visible="false"></Telerik:RadTab>
                
            </Tabs>
                  
        </Telerik:RadTabStrip>
        
        <Telerik:RadMultiPage ID="CareManagementMultiPage" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="PagePopulationCareManagement" runat="server">

                <div id="Div8" style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;" runat="server">
                
                    <Telerik:RadGrid ID="PopulationCareManagementGrid" OnNeedDataSource="PopulationCareManagementGrid_OnNeedDataSource" OnItemCommand="PopulationCareManagementGrid_OnItemCommand" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="false" Skin="Sunset" runat="server">
                    
                        <MasterTableView DataKeyNames="PopulationMembershipId">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="PopulationMembershipId" UniqueName="PopulationMembershipId" HeaderText="PopulationMembershipId" ReadOnly="true" Visible="false" />
                                
                                <Telerik:GridBoundColumn DataField="PopulationId" UniqueName="PopulationId" HeaderText="PopulationId" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="PopulationName" UniqueName="PopulationName" HeaderText="Population" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />
                            
                                <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AnchorDate" UniqueName="AnchorDate" HeaderText="Anchor" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Next Service" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ExpectedEventDate" UniqueName="ExpectedEventDate" HeaderText="Expected Date" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="PreviousThresholdDate" UniqueName="PreviousThresholdDate" HeaderText="Previous Threshold" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="NextThresholdDate" UniqueName="NextThresholdDate" HeaderText="Next Threshold" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />

                            </Columns>
                            
                            <DetailTables>
                            
                                <Telerik:GridTableView Name="ServiceEventGrid" DataKeyNames="PopulationMembershipId" AllowPaging="false" BorderColor="Black" BorderStyle="Solid" Width="100%">
                                
                                    <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="PopulationMembershipId" DetailKeyField="PopulationMembershipId" /></ParentTableRelation>
                                    
                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="PopulationMembershipId" UniqueName="PopulationMembershipId" HeaderText="PopulationMembershipId" ReadOnly="true" Visible="false" />
                                        
                                        <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Service" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="ExpectedEventDate" UniqueName="ExpectedEventDate" HeaderText="Expected Date" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Actual Date" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="PreviousThresholdDate" UniqueName="PreviousThresholdDate" HeaderText="Previous Threshold" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="NextThresholdDate" UniqueName="NextThresholdDate" HeaderText="Next Threshold" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />
                                    
                                    </Columns>
                                                                
                                </Telerik:GridTableView>
                            
                                <Telerik:GridTableView Name="TriggerEventGrid" DataKeyNames="PopulationMembershipId" AllowPaging="false" BorderColor="Black" BorderStyle="Solid" Width="100%">
                                
                                    <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="PopulationMembershipId" DetailKeyField="PopulationMembershipId" /></ParentTableRelation>
                                    
                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="PopulationMembershipId" HeaderText="PopulationMembershipId" ReadOnly="true" Visible="false" />
                                        
                                        <Telerik:GridBoundColumn DataField="PopulationMembershipTriggerEventId" HeaderText="PopulationMembershipTriggerEventId" ReadOnly="true" Visible="false" />

                                        <Telerik:GridBoundColumn DataField="TriggerType" HeaderText="Trigger Type" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="TriggerName" HeaderText="Trigger Name" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="TriggerDate" HeaderText="Trigger Date" ReadOnly="true" Visible="true" />
                                        
                                        <Telerik:GridBoundColumn DataField="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="ProblemStatement" HeaderText="Problem Statement" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="ActionDescription" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />
                                    
                                    </Columns>
                                                                
                                </Telerik:GridTableView>
                            
                            </DetailTables>

                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                            
                        </ClientSettings>
                                           
                    </Telerik:RadGrid>
                   
                </div>
               
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="RadPageView2" runat="server">
            
                <div id="Div9" style="width: 100%; height: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;" runat="server">
                
                    <MercuryUserControl:MemberCaseView ID="MemberCaseViewControl" AllowUserInteraction="true" runat="server" />
                                                       
                </div>
        
            </Telerik:RadPageView>
                    
            <Telerik:RadPageView ID="RadPageView3" runat="server">
            
                <div id="Div10" style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;" runat="server">
                
                    <div style="text-align: center"><span style="text-align: center"><img src="/Images/Misc/Loading.gif" alt="Loading" /></span></div>
                    
                </div>
        
            </Telerik:RadPageView>
                    
        </Telerik:RadMultiPage>
        
    </div>
                        
</div>                              

</form>
    
<script type="text/javascript">

    // SET WINDOW NAME TO UNIQUE NAME

    var memberId = document.getElementById("<%= MemberId.ClientID %>").value;

    window.name = "MemberProfile_" + memberId;

    
    function SectionCollapseExpand (sectionPrefix) {
    
        var sectionContent = document.getElementById (sectionPrefix + "Content");
        
        var sectionImage = document.getElementById (sectionPrefix + "ExpandCollapse");
        
        if (sectionContent == null) { return; }
        
        if (sectionContent.style.display == "block") {
        
            sectionContent.style.display = "none";

            sectionImage.src = "/Images/Common16/TreeViewChildrenCollapsed.png";
            
        }
        
        else { 
        
            sectionContent.style.display = "block";

            sectionImage.src = "/Images/Common16/TreeViewChildrenExpanded.png";
            
        }
        
        return;
    
    }
    
        function ExpandSection (sectionName) {
        
            expandedSection = document.getElementById (sectionName + "_Expanded"); 
            
            expandedSection.style.display = "block";
            
            expandedImage = document.getElementById (sectionName + "_ExpandedImage");
            
            expandedImage.style.display = "block";
            
            collapsedImage = document.getElementById (sectionName + "_CollapsedImage");
            
            collapsedImage.style.display = "none";
            
            return;
        
        }
    
        function CollapseSection (sectionName) {
        
            expandedSection = document.getElementById (sectionName + "_Expanded"); 
            
            expandedSection.style.display = "none";
            
            expandedImage = document.getElementById (sectionName + "_ExpandedImage");
            
            expandedImage.style.display = "none";
            
            collapsedImage = document.getElementById (sectionName + "_CollapsedImage");
            
            collapsedImage.style.display = "block";
            
            return;
        
        }
        
</script>    
    
</body>

</html>


