<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProviderProfile.aspx.cs" Inherits="Mercury.Web.Application.Provider.ProviderProfile" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderProfileDemographics" Src="~/Application/Controls/ProviderDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderAffiliation" Src="~/Application/Controls/ProviderAffiliation.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderContract" Src="~/Application/Controls/ProviderContract.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderServiceLocation" Src="~/Application/Controls/ProviderServiceLocation.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderScheduler" Src="~/Application/Controls/ProviderScheduler.ascx" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityDocumentHistory" Src="~/Application/Controls/EntityDocumentHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityContactHistory" Src="~/Application/Controls/EntityContactHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityNoteHistory" Src="~/Application/Controls/EntityNoteHistory.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title>Provider Profile</title>
    
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

<form id="FormProviderProfile" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" AsyncPostBackTimeout="600" runat="Server" />
    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ProviderTabStrip">
              
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ProviderTabStrip" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ProviderMultipage" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
                
            </Telerik:AjaxSetting>

        </AjaxSettings>
            
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="100" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
</div>



<div id="TitleBar" runat="server">

    <table style="width: 100%; table-layout: auto; background-color:#6699CC; border-bottom: solid 1px black"><tr>

        <td style="width: 32px"><img id="GlobalSearchTitleImage" src="/Images/Common24/Doctor.png" alt="Provider Profile" /></td>    

        <td style="max-width: 32px;"><img id="EntityNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="padding: 4px" visible="false" runat="server" /></td>   
        
        <td style="max-width: 32px;"><img id="EntityNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="padding: 4px" visible="false" runat="server" /></td>   

        <td style="width: 100%; text-align: left; vertical-align: middle; font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold">
        
            <div style="font-family: Calibri, Arial; font-size: 10pt; color: White; font-weight: bold; text-decoration: none;"><asp:Label ID="ProviderDemographicHeaderLabel" Style="overflow: hidden" Text="Provider Name (Age | Gender)" runat="server" /></div>
    
        </td>
        
    </tr></table>

    <div>
        
        <Telerik:RadToolBar ID="ProviderProfileToolbar" Width="100%" OnClientButtonClicked="ProviderProfileToolbar_OnClientButtonClicked" runat="server">
        
            <Items>
            
                <Telerik:RadToolBarButton Text="Search" Value="Search" ToolTip="Search for another Provider" ImagePosition="AboveText" ImageUrl="/Images/Common16/SearchProvider.png" Visible="false" />
                
                <Telerik:RadToolBarButton ID="ProviderProfileToolbar_Action" Text="Action" Value="Action"  CommandName="Action" ImagePosition="AboveText" ImageUrl="/Images/Common16/Gear.png" Visible="true"  />
                
                <Telerik:RadToolBarButton IsSeparator="true" Visible="true" />
                
                <Telerik:RadToolBarButton Text="Contact" Value="Contact" ToolTip="Contact Provider" ImagePosition="AboveText" ImageUrl="/Images/Common16/Phone.png" />
                
                <Telerik:RadToolBarButton IsSeparator="false" />
                
                <Telerik:RadToolBarButton Text="Send" Value="SendCorrespondence" ToolTip="Send Correspondence" ImagePosition="AboveText" ImageUrl="/Images/Common16/Address.png" />
                
                <Telerik:RadToolBarButton IsSeparator="false" />
            
                <Telerik:RadToolBarButton Text="Note" Value="CreateNote" ToolTip="Add a Note" ImagePosition="AboveText" ImageUrl="/Images/Common16/Note.png" />
                
                <Telerik:RadToolBarButton IsSeparator="false" />
            
                <Telerik:RadToolBarButton Text="Address" Value="CreateAddress" ToolTip="Add an Address" ImagePosition="AboveText" ImageUrl="/Images/Common16/Address.png" />

            </Items>
        
        </Telerik:RadToolBar>
        
        <Telerik:RadToolTip 
         ID="ProviderActionToolTip" TargetControlID="ProviderProfileToolbar_Action" IsClientID="true" RelativeTo="Element" Position="BottomRight" ShowEvent="FromCode" HideEvent="FromCode" Skin="Hay" Animation="Fade" runat="server">
        
            <div style="margin-top: .125in; margin-bottom: 0in; padding: .125in; width: 400px;">
            
            <!-- ToolTip="Perform an Action on the Provider" -->
                <table>
                    <tr>
                        <td style="width: 65px">Action: </td>
                        <td><Telerik:RadComboBox ID="ProviderActionSelection" Width="100%" ZIndex="9999" runat="server" /></td>
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
                                        <asp:Button ID="ProviderActionGo" runat="server" Text="Go" Width="73px" Font-Names="segoe ui, arial"
                                            Font-Size="11px" Height="24" OnClick="ProviderActionGo_OnClick" />
                                    </td>
                                    <td align="right" style="width: 90px; max-width: 90px;">
                                        <input type="button" onclick="javascript:$find ('ProviderActionToolTip').hide ();"
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

            function ProviderProfileToolbar_OnClientButtonClicked(sender, e) {

                var button = e.get_item();

                switch (button.get_commandName()) {

                    case "Action":

                        var memberActionToolTip = $find("ProviderActionToolTip");

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

    <div id="ProviderDemographicsContent" style="display: block;">

        <Telerik:RadTabStrip ID="ProviderTabStrip" MultiPageID="ProviderMultipage" SelectedIndex="0" OnTabClick="ProviderTabStrip_OnTabClick" ScrollChildren="true" ScrollButtonsPosition="Right" PerTabScrolling="true" EnableViewState="true" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="Demographic" PageViewID="PageProviderDemographic" Selected="true"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Enrollments" PageViewID="PageProviderEnrollment"></Telerik:RadTab>
                                
                <Telerik:RadTab Text="Affiliations" PageViewID="PageProviderAffiliation"></Telerik:RadTab>

                <Telerik:RadTab Text="Contracts" PageViewID="PageProviderContract"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Service Locations" PageViewID="PageProviderServiceLocations"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Documents" PageViewID="PageProviderDocuments"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Contact History" PageViewID="PageContactHistory"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Notes" PageViewID="PageNotes"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Scheduler" PageViewID="PageScheduler" Visible="false"></Telerik:RadTab>
                
            </Tabs>
                  
        </Telerik:RadTabStrip>
        
        <Telerik:RadMultiPage ID="ProviderMultipage" SelectedIndex="0" BackColor="White" runat="server">
        
            <Telerik:RadPageView ID="PageProviderDemographic" runat="server">
             
                <div class="TabPageClass">
               
                    <MercuryUserControl:ProviderProfileDemographics ID="ProviderDemographicsControl" runat="server" />
                    
                </div>
                
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageProviderEnrollment" runat="server">
            
                <div class="TabPageClass" id="PageProviderEnrollmentPlugin" runat="server">

                    <Telerik:RadGrid ID="ProviderEnrollmentGrid" Height="400" AllowPaging="false" AllowCustomPaging="false" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView Name="ProviderEnrollment" Width="99%" TableLayout="Auto" DataKeyNames="ProviderEnrollmentId">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="ProviderEnrollmentId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerId" UniqueName="InsurerId" HeaderText="Insurer" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="InsurerName" UniqueName="InsurerName" HeaderText="Insurer" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramId" UniqueName="ProgramId" HeaderText="Program" ReadOnly="true" Visible="false" />

                                <Telerik:GridBoundColumn DataField="ProgramName" UniqueName="ProgramName" HeaderText="Program" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProgramProviderId" UniqueName="ProgramProviderId" HeaderText="Provider Id" ReadOnly="true" Visible="true" />

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
            
            <Telerik:RadPageView ID="PageProviderAffiliation" runat="server">
            
                <div class="TabPageClass">

                    <MercuryUserControl:ProviderAffiliation ID="ProviderAffiliationControl" GridHeight="400" runat="server" />
                  
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageProviderContract" runat="server">
            
                <div class="TabPageClass">

                    <MercuryUserControl:ProviderContract ID="ProviderContractControl" GridHeight="400" runat="server" />
                  
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageProviderServiceLocations" runat="server">
            
                <div class="TabPageClass">

                    <MercuryUserControl:ProviderServiceLocation ID="ProviderServiceLocationControl" GridHeight="400" runat="server" />
                  
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageProviderDocuments" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:EntityDocumentHistory ID="EntityDocumentHistoryControl" HistoryGridHeight="400" runat="server" />
                    
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageContactHistory" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:EntityContactHistory ID="EntityContactHistoryControl" HistoryGridHeight="400" runat="server" />
                
                </div>
            
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageNotes" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:EntityNoteHistory ID="EntityNoteHistoryControl" HistoryGridHeight="400" runat="server" />
                                    
                </div>
        
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageScheduler" runat="server">
            
                <div class="TabPageClass">
                
                    <MercuryUserControl:ProviderScheduler Id="ProviderSchedulerControl" runat="server" />
                
                </div>
            
            </Telerik:RadPageView>
            
        </Telerik:RadMultiPage>

    </div>
                        
</div>                        
                        
<div style="height: 10px"> </div>              

</form>

</body>

</html>  