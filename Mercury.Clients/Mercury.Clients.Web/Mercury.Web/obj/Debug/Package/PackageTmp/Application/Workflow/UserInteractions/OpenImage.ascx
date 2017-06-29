<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OpenImage.ascx.cs" Inherits="Mercury.Web.Application.Workflow.UserInteractions.OpenImage" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>





<div id="UserInteractionOpenImageContainer" style="overflow: hidden;">

    <div id="OpenImageWorkflowControl">
    
        <table border="0" style="margin: .125in;">
        
            <tr>

                <td style="padding-right: .125in;"><a href="javascript:document.getElementById ('WorkflowContinue').click ();">Continue Workflow</a></td>

                <td><a id="OpenImageWindow" href="#" target="_blank" runat="server">Open Image in New Window</a></td>
            
            </tr>
        
        </table>
    
    </div>

    <div id="ImageFrameContainer" style="width: 100%; border: 1px solid black;">

        <iframe id="ImageFrame" runat="server"></iframe>

    </div>

</div>

    
<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', UserInteractionOpenImage_OnPaint, false); } else { window.attachEvent('onresize', UserInteractionOpenImage_OnPaint); }

    
    var isUserInteractionOpenImagePainting = false;

    setTimeout('UserInteractionOpenImage_OnPaint()', 250);


    function UserInteractionOpenImage_OnPaint(forEvent) {

        if (isUserInteractionOpenImagePainting) { return; }

        isUserInteractionOpenImagePainting = true;


        var container = document.getElementById("UserInteractionOpenImageContainer");

        var workflowControl = document.getElementById("OpenImageWorkflowControl");

        var frameObject = document.getElementById("<%= ImageFrame.ClientID %>");

        if ((container == null) || (frameObject == null)) { 

            isUserInteractionOpenImagePainting = false;

            setTimeout('UserInteractionOpenImage_OnPaint ()', 100);

            return;

        }


        container.style.height = (container.parentNode.offsetHeight) + "px";

        frameObject.style.width = (frameObject.parentNode.offsetWidth - 4) + "px";

        frameObject.style.height = (container.offsetHeight - 51 - 4) + "px";

        isUserInteractionOpenImagePainting = false;

        return;

    }


    function UserInteractionOpenImage_Body_OnResize(forEvent) {

        UserInteractionOpenImage_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>