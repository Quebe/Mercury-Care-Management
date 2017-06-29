<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityContactHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityContactHistory" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityContactHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="EntityContactHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="EntityContactHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 

    OnItemCreated="EntityContactHistoryGrid_OnItemCreated" 
    
    OnNeedDataSource="EntityContactHistoryGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="EntityContactHistoryGrid_OnPageSizeChanged"
    
    OnItemCommand="EntityContactHistoryGrid_OnItemCommand" 
    
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="EntityContactHistoryMasterView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="EntityContactId">
    
        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="EntityContactToolbar" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton ImageUrl="/Images/Common16/Phone.png" Text="Contact" CommandName="Contact" Value="Contact" ImagePosition="Left"></Telerik:RadToolBarButton>
                   
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
        
        </CommandItemTemplate>
    
        <Columns>
    
            <Telerik:GridBoundColumn DataField="EntityContactId" UniqueName="EntityContactId" Visible="false" />

            <Telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="false" />

            <Telerik:GridBoundColumn DataField="EntityContactId" UniqueName="EntityContactId" Visible="false" />

            <Telerik:GridBoundColumn DataField="ContactDate" UniqueName="ContactDate" HeaderText="Date" ItemStyle-Wrap="false" Visible="true" />

            <Telerik:GridBoundColumn DataField="ContactDirection" UniqueName="ContactDirection" HeaderText="Direction" Visible="true" />

            <Telerik:GridBoundColumn DataField="ContactType" UniqueName="ContactType" HeaderText="Type" Visible="true" />

            <Telerik:GridBoundColumn DataField="Outcome" UniqueName="Outcome" HeaderText="Outcome" Visible="true" />

            <Telerik:GridBoundColumn DataField="Regarding" UniqueName="Regarding" HeaderText="Regarding" Visible="true" />

            <Telerik:GridBoundColumn DataField="Remarks" UniqueName="Remarks" HeaderText="Remarks" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="RelatedEntity" HeaderText="Related Entity" />
            
            <Telerik:GridBoundColumn DataField="RelatedObject" HeaderText="Related Object" />

            <Telerik:GridBoundColumn DataField="ContactedByName" UniqueName="ContactedByName" HeaderText="Contacted By" ItemStyle-Wrap="false" Visible="true" />

        </Columns>
        
        <PagerStyle Mode="NextPrevAndNumeric" />
    
    </MasterTableView>
    
    <ClientSettings>          
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>

