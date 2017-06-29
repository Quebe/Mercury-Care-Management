<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderAffiliation.ascx.cs" Inherits="Mercury.Web.Application.Controls.ProviderAffiliation" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ProviderAffiliationGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="ProviderAffiliationGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="ProviderAffiliationGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
    
    OnItemCreated="ProviderAffiliationGrid_OnItemCreated" 
    
    OnNeedDataSource="ProviderAffiliationGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="ProviderAffiliationGrid_OnPageSizeChanged"
    
    OnItemCommand="ProviderAffiliationGrid_OnItemCommand" 
     
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="ProviderAffiliationGridMasterView" TableLayout="Auto" DataKeyNames="ProviderAffiliationId">
            
        <Columns>

            <Telerik:GridBoundColumn DataField="ProviderAffiliationId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="AffiliateProviderId" HeaderText="Affiliate Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="AffiliateProviderName" HeaderText="Affiliate Name" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="Effective" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="Termination" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="SortDateField" UniqueName="SortDateField" Visible="false" />
        
        </Columns>
        
        <PagerStyle Mode="NextPrevAndNumeric" />
    
    </MasterTableView>
    
    <ClientSettings>          
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>


