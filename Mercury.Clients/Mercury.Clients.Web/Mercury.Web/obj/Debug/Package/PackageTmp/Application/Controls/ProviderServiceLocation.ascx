<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderServiceLocation.ascx.cs" Inherits="Mercury.Web.Application.Controls.ProviderServiceLocation" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ProviderServiceLocationGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="ProviderServiceLocationGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="ProviderServiceLocationGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
    
    OnItemCreated="ProviderServiceLocationGrid_OnItemCreated" 
    
    OnNeedDataSource="ProviderServiceLocationGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="ProviderServiceLocationGrid_OnPageSizeChanged"
    
    OnItemCommand="ProviderServiceLocationGrid_OnItemCommand" 
     
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="ProviderServiceLocationGridMasterView" TableLayout="Auto" DataKeyNames="ProviderServiceLocationId">
            
        <Columns>

            <Telerik:GridBoundColumn DataField="ProviderServiceLocationId" UniqueName="EnrollmentId" HeaderText="Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="AffiliateProviderId" HeaderText="Affiliate Id" ReadOnly="true" Visible="false" />

            <Telerik:GridBoundColumn DataField="AffiliateProviderName" HeaderText="Affiliate Name" ReadOnly="true" Visible="true" />


            <Telerik:GridBoundColumn DataField="Program" HeaderText="Program" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="Address" HeaderText="Address" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="ServiceLocationNumber" HeaderText="Location Id" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="IsPcp" HeaderText="PCP" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="IsAcceptingNewPatients" HeaderText="New Patients" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="PanelSizeMaximum" HeaderText="Panel" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="AgeMinimum" HeaderText="Age Min" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="AgeMaximum" HeaderText="Age Max" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="HasHandicapAccess" HeaderText="Handicap" ReadOnly="true" Visible="true" ItemStyle-HorizontalAlign="Center" />

            <Telerik:GridBoundColumn DataField="OfficeHours" HeaderText="Office Hours" ReadOnly="true" Visible="true" />


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


