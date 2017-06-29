<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenDialog.ascx.cs" Inherits="Mercury.Web.Application.Forms.FormDesigner.Controls.OpenDialog" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadGrid ID="FormsGrid" Height="300" AutoGenerateColumns="false" OnNeedDataSource="FormsGrid_OnNeedDataSource" EnableViewState="false" runat="server">

    <MasterTableView>
    
        <Columns>
        
            <Telerik:GridBoundColumn DataField="FormId" HeaderText="Id" />
            
            <Telerik:GridBoundColumn DataField="FormName" HeaderText="Name" />
            
            <Telerik:GridBoundColumn DataField="ModifiedAccountName" HeaderText="Modified By" />
            
            <Telerik:GridBoundColumn DataField="ModifiedDate" HeaderText="Modified Date" />
            
        </Columns>
    
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />

        <Scrolling AllowScroll="true" />
    
    </ClientSettings>

</Telerik:RadGrid>