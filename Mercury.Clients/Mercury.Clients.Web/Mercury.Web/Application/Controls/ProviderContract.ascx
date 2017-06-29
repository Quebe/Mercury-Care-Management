<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderContract.ascx.cs" Inherits="Mercury.Web.Application.Controls.ProviderContract" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ProviderContractGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="ProviderContractGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="ProviderContractGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
    
    OnItemCreated="ProviderContractGrid_OnItemCreated" 
    
    OnNeedDataSource="ProviderContractGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="ProviderContractGrid_OnPageSizeChanged"
    
    OnItemCommand="ProviderContractGrid_OnItemCommand" 
     
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="ProviderContractGridMasterView" TableLayout="Auto" DataKeyNames="ProviderContractId">
            
        <Columns>

            <Telerik:GridBoundColumn DataField="ProviderContractId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />
            
            <Telerik:GridBoundColumn DataField="ProviderId" ReadOnly="true" Visible="false" />
            
            <Telerik:GridBoundColumn DataField="ProviderName" ReadOnly="true" Visible="false" />


            <Telerik:GridBoundColumn DataField="ProgramId" HeaderText="Program Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="ProgramName" HeaderText="Program" ReadOnly="true" Visible="true" />
            
            
            <Telerik:GridBoundColumn DataField="AffiliateProviderId" HeaderText="Affiliate Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="AffiliateProviderName" HeaderText="Affiliate Name" ReadOnly="true" Visible="true" />


            <Telerik:GridBoundColumn DataField="ContractId" HeaderText="Contract Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="ContractName" HeaderText="Contract" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="IsParticipating" HeaderText="Is Participating" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="IsCapitated" HeaderText="Is Capitated" ReadOnly="true" Visible="true" />


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


