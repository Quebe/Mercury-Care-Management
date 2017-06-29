<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityNoteHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityNoteHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityNoteHistoryGrid" >
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityNoteHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="EntityNoteHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" AutoGenerateColumns="false"

        OnNeedDataSource="EntityNoteHistoryGrid_OnNeedDataSource" 
        
        OnItemCommand="EntityNoteHistoryGrid_OnItemCommand"
        
        OnItemDataBound="EntityNoteHistoryGrid_OnItemDataBound"
        
        OnPageSizeChanged="EntityNoteHistoryGrid_OnPageSizeChanged"
        
        runat="server">

    <MasterTableView Name="EntityNoteHistoryMasterView" TableLayout="Auto" DataKeyNames="EntityNoteId">
    
        <Columns>
        
            <Telerik:GridBoundColumn DataField="NoteTypeImage" />
    
            <Telerik:GridBoundColumn DataField="EntityNoteId" HeaderText="Id" Visible="true" />

            <Telerik:GridBoundColumn DataField="EntityId" Visible="false" />

            <Telerik:GridBoundColumn DataField="RelatedEntityType" Visible="false" />

            <Telerik:GridBoundColumn DataField="RelatedEntityObjectIdId" Visible="false" />

            <Telerik:GridBoundColumn DataField="RelatedObjectType" Visible="false" />

            <Telerik:GridBoundColumn DataField="RelatedObjectId" Visible="false" />

            <Telerik:GridBoundColumn DataField="DataSource" HeaderText="Source" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="NoteType" HeaderText="Type" Visible="false" />
            
            <Telerik:GridBoundColumn DataField="Subject" HeaderText="Subject" Visible="true" />

            <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" Visible="true" />

            <Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="Action" HeaderText="Action"  Visible="true" />

        </Columns>
        
        <DetailTables>

            <Telerik:GridTableView DataKeyNames="EntityNoteId" AllowPaging="false" BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid" Width="100%">
            
                <ParentTableRelation>
                
                    <Telerik:GridRelationFields MasterKeyField="EntityNoteId" DetailKeyField="EntityNoteId" />
                    
                </ParentTableRelation>
                
                <Columns>
                                                       
                    <Telerik:GridBoundColumn DataField="EntityNoteId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="Content" HeaderText="Content" Visible="true" />

                    <Telerik:GridBoundColumn DataField="CreateAccountName" HeaderText="Created By" Visible="true" />
                    
                    <Telerik:GridBoundColumn DataField="CreateDate" HeaderText="Create Date" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ModifiedAccountName" HeaderText="Modified By" Visible="true" />

                    <Telerik:GridBoundColumn DataField="ModifiedDate" HeaderText="Modified Date" Visible="true" />
                    
                </Columns>
            
            </Telerik:GridTableView>
        
        </DetailTables>
    
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>

<Telerik:RadToolTip ID="TerminationDialogueEntityNoteTerminationDateEdit" RelativeTo="Mouse" Position="Center" BorderWidth="5px" ManualClose="true" Title="Set Note Termination Date" Animation="Fade" runat="server">

    <div style="margin-top: 0px; padding: 8px; background-color: White">

        <table cellpadding="4" cellspacing="2" border="0" style="padding: 8px;">
        
            <tr align="left"><td colspan="3"><b>ID: </b><asp:Label ID="TerminationDialogueEntityNoteId" runat="server"></asp:Label></td></tr>
        
            <tr><td colspan="3"><b>Subject:</b></td></tr>
        
            <tr><td colspan="3"><asp:Label ID="TerminationDialogueEntityNoteSubject" Width="350px" runat="server"></asp:Label></td></tr>
        
            <tr>
        
                <td><b>Effective Date:</b> <asp:Label ID="TerminationDialogueEntityNoteEffectiveDate" runat="server"></asp:Label></td>
        
                <td><b>Termination Date:</b></td>
        
                <td><Telerik:RadDatePicker ID="TerminationDialogueSetNoteTerminationDate" Width="100" runat="server"></Telerik:RadDatePicker></td>
        
            </tr>
            
            <tr><td colspan="3">** Termination Date cannot be prior to the Effective Date</td></tr>
        
            <tr align="right" style="padding-top: 8px;">
                
                <td>&nbsp</td>
        
                <td><asp:Button ID="ButtonSet" OnClick="TerminationDialogueButtonSet_OnClick" Text="Set" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
        
                <td><asp:Button ID="ButtonCancel" OnClick="TerminationDialogueButtonCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
        
            </tr>
            
        </table>
    
    </div>
    
</Telerik:RadToolTip>

<div style="display: none">

    <asp:TextBox ID="TerminationDialogueTerminatedEntityNoteId" Text="0" runat="server"></asp:TextBox>

</div>
    
    
    
<Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<script type="text/javascript">

    function EntityNoteTerminate(entityNoteId, subject, effectivedate) {

        var terminationDialogueEntityNoteId = document.getElementById("<%= TerminationDialogueEntityNoteId.ClientID %>");

        var terminationDialogueEntityNoteSubject = document.getElementById("<%= TerminationDialogueEntityNoteSubject.ClientID %>");

        var terminationDialogueEntityNoteEffectiveDate = document.getElementById("<%= TerminationDialogueEntityNoteEffectiveDate.ClientID %>");

        var terminationDialogueEntityNoteTerminationDateEdit = $find('<%=TerminationDialogueEntityNoteTerminationDateEdit.ClientID %>');

        var terminationDialogueTerminatedEntityNoteId = document.getElementById("<%= TerminationDialogueTerminatedEntityNoteId.ClientID %>");

        terminationDialogueTerminatedEntityNoteId.value = entityNoteId;

        terminationDialogueEntityNoteId.innerText = entityNoteId;

        terminationDialogueEntityNoteSubject.innerText = subject;

        terminationDialogueEntityNoteEffectiveDate.innerText = effectivedate;

        if (terminationDialogueEntityNoteTerminationDateEdit) {

            terminationDialogueEntityNoteTerminationDateEdit.show();

        }

        return;

    }


</script>

</Telerik:RadCodeBlock>