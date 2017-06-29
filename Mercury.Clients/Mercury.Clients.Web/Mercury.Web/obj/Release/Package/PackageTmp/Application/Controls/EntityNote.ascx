<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityNote.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityNote" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ButtonOk">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="ButtonOk" />
                
                <Telerik:AjaxUpdatedControl ControlID="ActionResponseLabel" />
           
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>


<table cellspacing="2" cellpadding="2" border="0" style="width: 100%; line-height: 150%; padding: 8px">

    <tr>
    
        <td style="width: 40px;">Importance: </td>
        
        <td style="width: 100px">
        
            <Telerik:RadComboBox ID="NoteImportanceSelection" OnClientSelectedIndexChanged="SelectionNoteImportanceSelection_OnClientSelectedIndexChanged" Width="100" runat="server">
            
                <Items>
                
                    <Telerik:RadComboBoxItem Text="Not Specified" Value="0" Selected="true" Visible="false" />
                
                    <Telerik:RadComboBoxItem Text="Informational" Value="1" Selected="false" />
                    
                    <Telerik:RadComboBoxItem Text="Warning"       Value="2" Selected="false" />
                    
                    <Telerik:RadComboBoxItem Text="Critical"      Value="3" Selected="false" />
                
                </Items>
                   
            </Telerik:RadComboBox>
            
        </td> 
        
        <td style="width: 50px; text-align: center;">Subject:</td>
        
        <td style="width: auto"><Telerik:RadComboBox ID="NoteSubject" MaxLength="120" Width="100%" runat="server">
        
        <Items></Items>
        </Telerik:RadComboBox></td>
        
        <td style="width: 70px; text-align: center;">Effective:</td>
        
        <td style="width: 100px"><Telerik:RadDatePicker ID="NoteEffectiveDatePicker" Width="100" runat="server"></Telerik:RadDatePicker></td>
        
        <td style="width: 70px; text-align: center;">Termination:</td>
    
        <td style="width: 100px"><Telerik:RadDatePicker ID="NoteTerminationDatePicker" Width="100" runat="server"></Telerik:RadDatePicker></td>
    
    </tr>
    
</table>        

<div style="margin: 10px;">

    <Telerik:RadGrid ID="EntityNoteContentGrid" Height="175" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
            
            AutoGenerateColumns="false" runat="server">

        <MasterTableView Name="EntityNoteContentMasterView" TableLayout="Auto" DataKeyNames="EntityNoteId">

            <Columns>
                                                   
                <Telerik:GridBoundColumn DataField="EntityNoteId" Visible="false" />

                <Telerik:GridBoundColumn DataField="Content" HeaderText="Content" Visible="true" />

                <Telerik:GridBoundColumn DataField="CreateAccountName" HeaderText="Created By" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="CreateDate" HeaderText="Create Date" Visible="true" />

                <Telerik:GridBoundColumn DataField="ModifiedAccountName" HeaderText="Modified By" Visible="true" />

                <Telerik:GridBoundColumn DataField="ModifiedDate" HeaderText="Modified Date" Visible="true" />
                
            </Columns>
        
        </MasterTableView>
        
        <ClientSettings>
        
            <Selecting AllowRowSelect="true" />
            
            <Scrolling AllowScroll="true" />
        
        </ClientSettings>
        
    </Telerik:RadGrid>

</div>

<div style="margin: 10px;">

    <table cellspacing="2" cellpadding="2" border="0" style="width: 100%; line-height: 150%; padding: 8px">

        <tr style="height: 24;">
        
            <td valign="top">Append Content:</td>
            
        </tr>
        
        <tr>
            
            <td><Telerik:RadTextBox ID="NoteContent" Width="100%" EmptyMessage="(required)" Rows="4" TextMode="MultiLine" runat="server"></Telerik:RadTextBox></td>        

        </tr>

    </table>

    <table cellspacing="2" cellpadding="2" border="0" style="width: 100%; line-height: 150%; padding: 8px">
      
        <tr>
        
            <td style="width: 100%">&nbsp</td>
            
            <td align="left"><asp:Button ID="ButtonAppend" OnClick="ButtonAppend_OnClick" Text="Append" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>

            <td align="right"><asp:Button ID="ButtonOk" OnClick="ButtonOk_OnClick" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                          
            <td align="center"><asp:Button ID="ButtonCancel" OnClick="ButtonCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>

        </tr>
      
    </table>
    
    <div style="padding: 4px; padding-top: 8px; line-height: 150%"><asp:Label ID="ActionResponseLabel" Text="" runat="server" /></div>
    
</div>


<Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

<script type="text/javascript">

    function SelectionNoteImportanceSelection_OnClientSelectedIndexChanged(sender, eventArgs) {

        var selectedItem = eventArgs.get_item();

        var effectiveDate = $find('<%=NoteEffectiveDatePicker.ClientID %>');

        var terminationDate = $find('<%=NoteTerminationDatePicker.ClientID %>');

        if (selectedItem.get_value() == "1") {

            if (effectiveDate != null) {

                effectiveDate.set_enabled(false);

                effectiveDate.hidePopup();

                effectiveDate.Display = "none";

                effectiveDate.clear();

                //effectiveDate.set_visible(false);

            }

            if (terminationDate != null) {

                terminationDate.set_enabled(false);

                terminationDate.hidePopup();

                terminationDate.Display = "none";

                terminationDate.clear();

                //terminationDate.set_visible(false);

            }

        }

        else {

            if (effectiveDate != null) {

                effectiveDate.set_enabled(true);

                effectiveDate.hidePopup();

                effectiveDate.Display = "block";

                //effectiveDate.set_visible(true);

            }

            if (terminationDate != null) {

                terminationDate.set_enabled(true);

                terminationDate.hidePopup();

                terminationDate.Display = "block";

                //terminationDate.set_visible(true);

            }

        }

        return;

    }

</script>

</Telerik:RadCodeBlock>
