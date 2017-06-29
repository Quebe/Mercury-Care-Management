<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromptUser.ascx.cs" Inherits="Mercury.Web.Application.Workflow.UserInteractions.PromptUser" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityInfoContactSplitter" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityInfoContactSplitter" />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="UserInteractionContentPromptUser" class="BackgroundColorLight" style="width: 100%;"><div style="padding-top:.25in;">

    <div style="border: solid 1px #bbd7fa; min-width: 300px; max-width: 500px; margin-left: auto; margin-right: auto; background-color: White">

        <div class="BackgroundColorComplementLight" style="font-size: 10pt; line-height: 150%; height: 24px; padding: 4px; padding-left: 8px; vertical-align:middle">

            <asp:Label ID="PromptTitle" runat="server"></asp:Label>
      
        </div>               
    
        <table cellspacing="4" cellpadding="4" border="0" style="width: 100%; line-height: 150%;">
     
            <tr style="min-height: 32px;">
        
                <td colspan="3">
            
                    <img id="PromptImage" src="/Images/Common32/Question.png" style="float: left; padding: 4px 8px 4px 2px" alt="" runat="server" />
            
                    <asp:Label ID="PromptMessage" runat="server"></asp:Label>
                
                </td>        
            
            </tr>
        
            <tr id="PromptSelectionItemsRow" visible="false" runat="server">

                <td align="center" colspan="3">
            
                    <Telerik:RadComboBox ID="PromptSelectionItemsSelection" Width="80%" runat="server"></Telerik:RadComboBox>
            
                </td>
                         
            </tr>

            <tr style="height: 32px;">
        
                <td style="width: 100%">&nbsp</td>
        
                <td align="center" style="min-width: 110px;"><asp:Button ID="ButtonOk" OnClick="ButtonOkCancel_OnClick" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>

                <td align="center" style="min-width: 110px;"><asp:Button ID="ButtonCancel" OnClick="ButtonOkCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                          
            </tr>
      
        </table>
    
    </div>

    </div>

</div>


<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', UserInteractionPromptUser_Body_OnResize, false); } else { window.attachEvent('onresize', UserInteractionPromptUser_Body_OnResize); }

    if (window.addEventListener) { window.addEventListener('load', UserInteractionPromptUser_Page_Load, false); } else { window.attachEvent('onload', UserInteractionPromptUser_Page_Load); }


    function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isUserInteractionPromptUserPainting = false;

    setTimeout('UserInteractionPromptUser_OnPaint()', 250);


    function UserInteractionPromptUser_Page_Load() {

        setTimeout('UserInteractionPromptUser_OnPaint()', 250);

        return;

    }

    function UserInteractionPromptUser_OnPaint(forEvent) {

        if (isUserInteractionPromptUserPainting) { return; }

        isUserInteractionPromptUserPainting = true;


        var container = document.getElementById("UserInteractionContentPromptUser");

        var panel = document.getElementById("WorkflowContentPanel");

        if ((container == null) || (panel == null)) {

            isUserInteractionPromptUserPainting = false;

            setTimeout('UserInteractionPromptUser_OnPaint ()', 100);

            return;

        }


        container.style.height = (container.parentNode.offsetHeight) + "px";

        
        isUserInteractionPromptUserPainting = false;

        return;

    }


    function UserInteractionPromptUser_Body_OnResize(forEvent) {

        UserInteractionPromptUser_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>