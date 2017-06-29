<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RequireForm.ascx.cs" Inherits="Mercury.Web.Application.Workflow.UserInteractions.RequireForm" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="FormEditor" Src="~/Application/Forms/FormEditor/FormEditor.ascx"  %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
        
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="UserInteractionRequireFormContainer" style="background-color: White; width: 100%;"> 

    <MercuryUserControl:FormEditor ID="FormEditorControl" runat="server" />
     
<div id="FormSubmitBar" style="width: 99%; height: 32px; font-family: segoe ui, arial; font-size: 11px; line-height: 150%; background-color: #bbd7fa; border: solid 1px black; white-space: nowrap; overflow: hidden;" runat="server">

    <table cellpadding="0" cellspacing="0" border="0" style="height: 100%; ">

        <tr>

            <td style="padding-left: 4px;"><img alt="" src="/Images/Common16/Form.png" /></td>
            
            <td style="padding-left: 4px;"><img alt="" src="/Images/Common16/GrabBarBlueHorizontal.png" /></td>
            
            <td style="padding-left: 2px; padding-right: 2px; ">After completing all the required fields, click to submit document.</td>
            
            <td style="width: 80px;"><asp:Button ID="SubmitButton" OnClientClick="Button_OnSubmit (this, 'FormSubmitProgressImage');" OnClick="SubmitButton_OnClick" Text="Submit" TabIndex="32700" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
            
            <td style="padding: 0 8px 0 8px">
            
                <asp:LinkButton id="SaveAsDraftButton" OnClick="SaveAsDraftButton_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" TabIndex="32701" Visible="false" runat="server">Save As Draft</asp:LinkButton>
           
            </td>
                    
            <td style="padding: 0 8px 0 8px">
            
                <asp:LinkButton id="CancelButton" OnClick="CancelButton_OnClick" OnClientClick="return confirm('Are you sure that you want to cancel this form?');" Font-Names="segoe ui, arial" Font-Size="11px" TabIndex="32702" Visible="true" runat="server">Cancel</asp:LinkButton>
           
            </td>
            
            <td style="padding-left: 4px;"><img alt="" src="/Images/Common16/Print.png" /></td>

            <td style="padding: 0 8px 0 4px">

                <asp:LinkButton id="PrintButton" OnClick="PrintButton_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" TabIndex="32702" Visible="true" runat="server">Print</asp:LinkButton>
           
            </td>

            <td>&nbsp</td>
            
            <td><img id="FormSubmitProgressImage" src="/Images/Misc/LoadingProgressBar.gif" style="display: none;" /></td>
            
        </tr>

    </table>
        
    <script type="text/javascript">

        function SubmitButton_OnClick() {

            submitButton = document.getElementById("<%= SubmitButton.ClientID %>");

            if (submitButton) {

                submitButton.disabled = true;

            }

            return true;

        }

    </script>

</div>


<div id="FormSubmitMessageDiv" style="font-family: Arial; font-size: 10pt; line-height: 150%; padding: 10px" visible="false" runat="server">

    <div style="float: left;"><asp:Image ID="Image2" ImageUrl="/Images/Common32/SecurityShieldWarn.png" style="padding: 6px" align="left" runat="server" /></div>
 
    <div style="float: left; vertical-align: middle;"><asp:Literal ID="FormSubmitMessage" Text="" runat="server" /></div>

</div>
            

<div id="FormSubmitGridDiv" style="width: 100%;" visible="false" runat="server">

    <Telerik:RadGrid id="FormSubmitGrid" AutoGenerateColumns="false" Skin="Sunset" runat="server">

        <MasterTableView TableLayout="Fixed">
        
            <Columns>
            
                <Telerik:GridBoundColumn DataField="MessageType" ReadOnly="true" UniqueName="MessageType" HeaderText="Type" Visible="true">

                    <HeaderStyle Width="100" />

                    <ItemStyle Width="100" HorizontalAlign="Left" />
                   
                </Telerik:GridBoundColumn>
            
                <Telerik:GridBoundColumn DataField="Description" ReadOnly="true" UniqueName="Description" HeaderText="Description" Visible="true">
                    <ItemStyle HorizontalAlign="Left" />
                   
                </Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="ControlId" ReadOnly="true" UniqueName="ControlId" HeaderText="Control Id" Visible="false">
                    <ItemStyle HorizontalAlign="Left" />
                   
                </Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="ControlType" ReadOnly="true" UniqueName="ControlType" HeaderText="Control Type" Visible="false">
                    <ItemStyle HorizontalAlign="Left" />
                   
                </Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="ControlName" ReadOnly="true" UniqueName="ControlName" HeaderText="Control Name" Visible="true">
                    <ItemStyle HorizontalAlign="Left" />
                   
                </Telerik:GridBoundColumn>
                                    
            </Columns>
            
        </MasterTableView>         
        
        <ClientSettings>
        
            <Scrolling AllowScroll="false" UseStaticHeaders="true" SaveScrollPosition="false" />
            
            <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" />
        
        </ClientSettings>      
                                         
    </Telerik:RadGrid>
    
</div>
    
    
</div>


<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', UserInteractionRequireForm_Body_OnResize, false); } else { window.attachEvent('onresize', UserInteractionRequireForm_Body_OnResize); }

    var isUserInteractionRequireFormPainting = false;

    setTimeout('UserInteractionRequireForm_OnPaint()', 250);


    function UserInteractionRequireForm_OnPaint(forEvent) {

        if (isUserInteractionRequireFormPainting) { return; }

        isUserInteractionRequireFormPainting = true;


        var container = document.getElementById("UserInteractionRequireFormContainer");

        var panel = document.getElementById("WorkflowContentPanel");

        if ((container == null) || (panel == null)) {

            isUserInteractionRequireFormPainting = false;

            setTimeout('UserInteractionRequireForm_OnPaint ()', 100);

            return;

        }


        container.style.height = (container.parentNode.offsetHeight) + "px";

        
        isUserInteractionRequireFormPainting = false;

        return;

    }


    function UserInteractionRequireForm_Body_OnResize(forEvent) {

        UserInteractionRequireForm_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>