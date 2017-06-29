<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseAuditHistory.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseAuditHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div style="display: none"><asp:TextBox ID="MemberCaseId" Text="" runat="server"></asp:TextBox></div>

<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div id="MemberCaseAuditHistorySection" visible="true" runat="server">

    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Member Case Audit History</div>

    <Telerik:RadGrid ID="MemberCaseAuditHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false"

    OnPageSizeChanged="MemberCaseAuditHistoryGrid_OnPageSizeChanged"
    
    AutoGenerateColumns="false" runat="server"
    
    OnNeedDataSource="MemberCaseAuditHistoryGrid_OnNeedDataSource" Skin="Office2007">

        <MasterTableView Name="MemberCaseAuditHistoryMasterView" TableLayout="Auto" CommandItemDisplay="None" DataKeyNames="">

            <Columns>

                <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="AuditObjectType" HeaderText="Object Type"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="AuditObjectId" HeaderText="Object Id"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="Description" HeaderText="Description"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="SourceObjectType" HeaderText="Source Type"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="SourceObjectId" HeaderText="Source Id"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="UserDisplayName" HeaderText="User Name"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="CreateAccountInfo.ActionDate" HeaderText="Date of Change"></Telerik:GridBoundColumn>

            </Columns>

        </MasterTableView>

        <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

    </Telerik:RadGrid>

</div>