<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityDocumentHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityDocumentHistory" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityDocumentHistoryGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="EntityDocumentHistoryGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityDocumentHistoryAction">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityDocumentHistoryAction" LoadingPanelID="AjaxLoadingPanel" />
            
                <Telerik:AjaxUpdatedControl ControlID="EntityDocumentHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<Telerik:RadGrid ID="EntityDocumentHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 

    OnItemCreated="EntityDocumentHistoryGrid_OnItemCreated" 
    
    OnNeedDataSource="EntityDocumentHistoryGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="EntityDocumentHistoryGrid_OnPageSizeChanged"
    
    OnItemCommand="EntityDocumentHistoryGrid_OnItemCommand" 
    
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="EntityDocumentHistoryMasterView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="DocumentInstanceId">
    
        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="EntityDocumentToolbar" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton ImageUrl="/Images/Common16/CorrespondenceSend.png" Text="Send Correspondence" CommandName="SendCorrespondence" Value="SendCorrespondence" ImagePosition="Left"></Telerik:RadToolBarButton>
                   
                        <Telerik:RadToolBarButton IsSeparator="true"></Telerik:RadToolBarButton>
                   
                        <Telerik:RadToolBarButton BorderStyle="None" Value="EnterReceivedFormToolbarButton">
                        
                            <ItemTemplate>
                                                                    
                                <table cellpadding="0" cellspacing="0" border="0" style="border: none; padding: 0px"><tr>
                                
                                    <td style="width: 20px"><img src="/Images/Common16/CorrespondenceReceived.png" alt="Enter Received Form" /></td>
                                    
                                    <td style="width: 140px;">Enter Received Form:</td>
                                    
                                    <td style="width: 300px"><Telerik:RadComboBox ID="FormSelection" Width="300" runat="server" /></td>
                                    
                                    <td><asp:Button ID="EnterReceivedFormButton" CommandName="EnterReceivedForm" Text="Enter" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                
                                </tr></table>
                            
                            </ItemTemplate>
                        
                        </Telerik:RadToolBarButton>                                             
                        
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
            
        
        </CommandItemTemplate>
    
        <Columns>
        
            <Telerik:GridBoundColumn DataField="Detail"  Visible="true" />

            <Telerik:GridBoundColumn DataField="DocumentType" UniqueName="DocumentType" HeaderText="Type" Visible="true" />

            <Telerik:GridBoundColumn DataField="DocumentInstanceId" UniqueName="DocumentInstanceId" Visible="false" />

            <Telerik:GridBoundColumn DataField="DocumentId" UniqueName="DocumentId" HeaderText="Id" Visible="false" />

            <Telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="false" />

            <Telerik:GridBoundColumn DataField="DocumentTitle" UniqueName="DocumentTitle" HeaderText="Name" ItemStyle-Wrap="false" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="Version" HeaderText="Version" ItemStyle-Wrap="false" Visible="false" />
            
            <Telerik:GridBoundColumn DataField="ContactType" HeaderText="Method" ItemStyle-Wrap="false" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ReadyToSendDate" UniqueName="ReadyToSendDate" HeaderText="Ready to Send" Visible="true" />

            <Telerik:GridBoundColumn DataField="SentDate" UniqueName="SentDate" HeaderText="Sent Date" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="ReceivedDate" UniqueName="ReceivedDate" HeaderText="Received Date" Visible="true" />

            <Telerik:GridBoundColumn DataField="ReturnedDate" UniqueName="ReturnedDate" HeaderText="Returned Date" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="RelatedEntity" HeaderText="Related Entity" />
            
            <Telerik:GridBoundColumn DataField="Automation" HeaderText="Automation*" ItemStyle-HorizontalAlign="Center" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Create Date" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateAccountName" UniqueName="CreateAccountName" HeaderText="Create Account Name" Visible="false" />

            <Telerik:GridBoundColumn DataField="ModifiedDate" UniqueName="ModifiedDate" HeaderText="Modified Date" Visible="false" />

            <Telerik:GridBoundColumn DataField="ModifiedAccountName" UniqueName="ModifiedAccountName" HeaderText="Modified Account Name" Visible="false" />

        </Columns>
        
        <PagerStyle Mode="NextPrevAndNumeric" />
    
    </MasterTableView>
    
    <ClientSettings>          
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>


<Telerik:RadToolTip ID="CorrespondenceMarkSent" RelativeTo="Mouse" Position="Center" ManualClose="true" Title="Mark as Sent" Font-Size="15px" Font-Names="segoe ui, arial"  Animation="None" runat="server">

    <div style="margin-top: 0px; padding: 4px; background-color: White">

    <table cellpadding="6" cellspacing="2" style="padding: 8px;">
    
        <tr><td colspan="3">
            
            <asp:Label ID="CorrespondenceMarkedSendId" Text="" runat="server" style="display:none;" />
        
            <asp:Label ID="CorrespondenceMarkedSentName" Text="Unknown Correspondence" runat="server"></asp:Label></td></tr>
    
        <tr>
        
            <td>Sent Date:</td>
        
            <td colspan="2"><Telerik:RadDateInput ID="CorrespondenceMarkSentDate" ClientEvents-OnError="CorrespondenceMarkSent_SentDateOnError" Width="100" MinDate="01/01/1900" ZIndex="9999" runat="server" /></td>
            
        </tr>
    
        <tr>
        
            <td></td><td></td>
            
            <td align="right">
            
                <input type="button" name="ButtonOkCorrespondenceSentDate" value="OK" onclick="CorrespondenceMarkSent_OkOnClick ();" id="ButtonOkCorrespondenceSentDate"  style="font-family:segoe ui,arial;font-size:11px;height:24px;width:73px;" />
            
            </td>
                                       
            <td align="right">
            
                <input type="button" name="ButtonCancelCorrespondenceSentDate" value="Cancel" onclick="CorrespondenceMarkSent_CancelOnClick ();" id="ButtonCancelCorrespondenceSentDate" style="font-family:segoe ui,arial;font-size:11px;height:24px;width:73px;" />
                
            </td>
                                             
        </tr>
        
    </table>

    </div>
    
</Telerik:RadToolTip>

<Telerik:RadToolTip ID="CorrespondenceMarkReturned" RelativeTo="Mouse" Position="Center" ManualClose="true" Title="Mark as Returned" Font-Size="15px" Font-Names="segoe ui, arial"  Animation="None" runat="server">

    <div style="margin-top: 0px; padding: 4px; background-color: White">

    <table cellpadding="6" cellspacing="2" style="padding: 8px;">
    
        <tr><td colspan="3"><asp:Label ID="CorrespondenceMarkedReturnedId" Text="" runat="server" style="display:none;" /><asp:Label ID="CorrespondenceMarkedReturnedName" Text="Unknown Correspondence" runat="server"></asp:Label></td></tr>
    
        <tr>
        
            <td>Returned Date:</td>
        
            <td colspan="2"><Telerik:RadDateInput ID="CorrespondenceMarkReturnedDate" ClientEvents-OnError="CorrespondenceMarkReturned_ReturnedDateOnError" Width="100" MinDate="01/01/1900" ZIndex="9999" runat="server" /></td>
            
        </tr>
    
        <tr>
        
            <td></td><td></td>
            
            <td align="right">
            
                <input type="button" name="ButtonOkCorrespondenceReturnedDate" value="OK" onclick="CorrespondenceMarkReturned_OkOnClick ();" id="ButtonOkCorrespondenceReturnedDate"  style="font-family:segoe ui,arial;font-size:11px;height:24px;width:73px;" />
            
            </td>
                                       
            <td align="right">
            
                <input type="button" name="ButtonCancelCorrespondenceReturnedDate" value="Cancel" onclick="CorrespondenceMarkReturned_CancelOnClick ();" id="ButtonCancelCorrespondenceReturnedDate" style="font-family:segoe ui,arial;font-size:11px;height:24px;width:73px;" />
                
            </td>
                                             
        </tr>
        
    </table>

    </div>
    
</Telerik:RadToolTip>
        

<div style="display: none">

    <asp:TextBox ID="EntityDocumentHistory_CommandName" Text="No Command" runat="server" />
    
    <asp:TextBox ID="EntityDocumentHistory_Arguments" runat="server" />
    
    <asp:Button  ID="EntityDocumentHistoryAction" OnClick="EntityDocumentHistoryAction_OnClick" runat="server" />
    
</div>
                         
                         
<div id="Div1" style="display: none;" runat="server">

<script type="text/javascript">

    function Correspondence_MarkSent(entityCorrespondenceId, correspondenceName, readyToSendDate) {

        var changeToolTip = $find("<%= CorrespondenceMarkSent.ClientID %>");

        if (changeToolTip) {

            var itemIdLabel = document.getElementById("<%= CorrespondenceMarkedSendId.ClientID %>");

            if (itemIdLabel) { itemIdLabel.innerText = entityCorrespondenceId; }
            

            var itemDescriptionLabel = document.getElementById("<%= CorrespondenceMarkedSentName.ClientID %>");

            if (itemDescriptionLabel) { itemDescriptionLabel.innerText = correspondenceName + " (" + readyToSendDate + ")"; }

            

            var returnedDateInput = $find("<%= CorrespondenceMarkSentDate.ClientID %>");

            if (returnedDateInput) {

                var minimumDate = new Date();

                minimumDate.setTime(Date.parse(readyToSendDate));

                returnedDateInput.set_minDate(minimumDate);

                returnedDateInput.set_value(Date());

                changeToolTip.show();

            }

            else { alert("Unable to open Dialog for Marking Sent."); }

        }

        else { alert("Unable to open Dialog for Marking Sent."); }

        return;

    }

    function CorrespondenceMarkSent_SentDateOnError(sender, eventArgs) {

        if (sender.get_id() != "<%= CorrespondenceMarkSentDate.ClientID %>") { return; }

        var sentDateInput = $find("<%= CorrespondenceMarkSentDate.ClientID %>");

        if (sentDateInput) {

            switch (eventArgs.get_reason()) {

                case 1: // PARSING ERROR

                    sentDateInput.set_value("");

                    break;

                case 2:

                    sentDateInput.set_value(sender.get_minDate().toDateString());

                    break;

            }

        }

        // sender.set_selectedDate(sender.get_minDate());

        sentDateInput.set_value(sender.get_minDate().toDateString());

        eventArgs.set_cancel(true);

        sentDateInput.set_value(sender.get_minDate().toDateString());

        return;

    }

    function CorrespondenceMarkSent_OkOnClick() {

        var sentDateInput = $find("<%= CorrespondenceMarkSentDate.ClientID %>");

        if (sentDateInput) {

            var changeToolTip = $find("<%= CorrespondenceMarkSent.ClientID %>");

            if (changeToolTip) { changeToolTip.hide(); }


            var commandName = document.getElementById("<%= EntityDocumentHistory_CommandName.ClientID %>");

            var arguments = document.getElementById("<%= EntityDocumentHistory_Arguments.ClientID %>");

            var action = document.getElementById("<%= EntityDocumentHistoryAction.ClientID %>");

            var itemIdLabel = document.getElementById("<%= CorrespondenceMarkedSendId.ClientID %>");


            commandName.value = "MarkSent";

            arguments.value = itemIdLabel.innerText + "|" + sentDateInput.get_value();

            action.click();


            
        }
        
        return;

    }

    function CorrespondenceMarkSent_CancelOnClick() {

        var changeToolTip = $find("<%= CorrespondenceMarkSent.ClientID %>");

        if (changeToolTip) {

            changeToolTip.hide();

        }

        return;

    }
    
</script>

<script type="text/javascript">

    function Correspondence_MarkReturned (entityCorrespondenceId, correspondenceName, sentDate) {

        var changeToolTip = $find("<%= CorrespondenceMarkReturned.ClientID %>");

        if (changeToolTip) {

            var itemIdLabel = document.getElementById("<%= CorrespondenceMarkedReturnedId.ClientID %>");

            if (itemIdLabel) { itemIdLabel.innerText = entityCorrespondenceId; }
            
            var itemDescriptionLabel = document.getElementById("<%= CorrespondenceMarkedReturnedName.ClientID %>");

            if (itemDescriptionLabel) { itemDescriptionLabel.innerText = correspondenceName + " (" + sentDate + ")"; }


            var returnedDateInput = $find("<%= CorrespondenceMarkReturnedDate.ClientID %>");

            if (returnedDateInput) {

                var minimumDate = new Date();

                minimumDate.setTime(Date.parse(sentDate));

                returnedDateInput.set_minDate(minimumDate);

                returnedDateInput.set_value(Date());

                changeToolTip.show();

            }

            else { alert("Unable to open Dialog for Marking Returned."); }
            
        }

        else { alert("Unable to open Dialog for Marking Returned."); }
        
        return;

    }

    function CorrespondenceMarkReturned_ReturnedDateOnError(sender, eventArgs) {

        if (sender.get_id() != "<%= CorrespondenceMarkReturnedDate.ClientID %>") { return; }

        var returnedDateInput = $find("<%= CorrespondenceMarkReturnedDate.ClientID %>");

        if (returnedDateInput) {

            switch (eventArgs.get_reason()) {

                case 1: // PARSING ERROR

                    returnedDateInput.set_value("");

                    break;

                case 2:

                    returnedDateInput.set_value(sender.get_minDate().toDateString());

                    break;

            }

        }

        // sender.set_selectedDate(sender.get_minDate());

        returnedDateInput.set_value(sender.get_minDate().toDateString());
        
        eventArgs.set_cancel(true);

        returnedDateInput.set_value(sender.get_minDate().toDateString());

        return;

    }

    function CorrespondenceMarkReturned_OkOnClick() {

        var returnedDateInput = $find("<%= CorrespondenceMarkReturnedDate.ClientID %>");

        if (returnedDateInput) {

            var changeToolTip = $find("<%= CorrespondenceMarkReturned.ClientID %>");

            if (changeToolTip) { changeToolTip.hide(); }


            var commandName = document.getElementById("<%= EntityDocumentHistory_CommandName.ClientID %>");

            var arguments = document.getElementById("<%= EntityDocumentHistory_Arguments.ClientID %>");

            var action = document.getElementById("<%= EntityDocumentHistoryAction.ClientID %>");

            var itemIdLabel = document.getElementById("<%= CorrespondenceMarkedReturnedId.ClientID %>");


            commandName.value = "MarkReturned";

            arguments.value = itemIdLabel.innerText + "|" + returnedDateInput.get_value();

            action.click();

        }
        
        return;

    }

    function CorrespondenceMarkReturned_CancelOnClick() {

        var changeToolTip = $find("<%= CorrespondenceMarkReturned.ClientID %>");

        if (changeToolTip) {
        
            changeToolTip.hide();

        }
        
        return;

    }
    
</script>

</div>

