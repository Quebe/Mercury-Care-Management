<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Image.aspx.cs" Inherits="Mercury.Web.Application.Common.Image" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Mercury Care Management</title>
    
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />

    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />
    
    <style type="text/css">
    
        html { overflow: hidden; }
    
    </style>
    
</head>

<body style="margin: 0px;" class="TextNormal BackgroundColorLight">

<form id="ApplicationForm" runat="server">


<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div> 



<!-- AJAX MANAGER CONTENT (BEGIN) -->

<div>

    <div style="display: none"><asp:TextBox ID="TextBox1" Text="" runat="server" /></div>

    <div style="display: none">
    
    </div>

</div>

<!-- AJAX MANAGER CONTENT ( END ) -->


<!-- TITLE BAR (BEGIN) -->

<div id="TitleBar" runat="server">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="BackgroundColorDark" style="height: 36px;">

            <td style="width: 100%; color: White; font-weight: bold; padding-left: .125in; white-space: nowrap">Opening Image</td>
            
            <td style="padding-left: .125in; padding-right: .25in">
            
                <div id="CancelContainer" style="" runat="server">
                
                    <a class="NoDecoration ColorLight HoverTextWhiteBold" href="/WindowClose.aspx" style="white-space: nowrap; font-weight: bold; text-align: center;">Cancel</a>

                </div>
                
            </td>

        </tr>
     
        <tr><td colspan="5" style="width: 100%; height: 1px;" class="BackgroundColorComplementLight"></td></tr>   
        
     </table>

</div>

<!-- TITLE BAR ( END ) -->


<!-- HEADER (BEGIN) -->

<div id="HeaderSection" style="padding: .125in; padding-top: .0625in; padding-bottom: .0625in;" runat="server">

    <!-- WORKFLOW INFORMATION (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <!-- WORKFLOW ACTION (BEGIN) -->

        <div id="Div1" style="display: block;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 36px;"><tr>
                        
                <td valign="top" style="width: 20px;"><img id="WorkflowIcon" src="/Images/Common16/Image.png" style="padding-right: 8px;" alt="Action" runat="server" /></td>
                
                <td valign="top" style="width: 80px; font-weight: bold;">Action:</td>
                
                <td valign="top" style="text-align: left;"><asp:Label ID="ActionMessage" runat="server">
                
                    Retreiving Image. If the image needs to be generated from the database, this may take some time. When complete, 

                    the image will either display in the browser window or downloaded as a file at the bottom of the window. If, it is

                    at the bottom of the window, you will need to click to open - at which point you can close this window. <br /><br />

                    ** If the image is downloaded, click the dropdown arrow next to the download and select

                    "Always open files of this type" to have the file automatically openned in the future. Once the image is openned 
                    
                    in its own window, you can close this window.

                </asp:Label></td>

            </tr></table>

        </div>

        <!-- WORKFLOW ACTION ( END ) -->

        <!-- WORKFLOW LAST MESSAGE ( END ) -->
            
        <div id="ExceptionMessageRow" style="display: none;" runat="server">
        
            <table cellpadding="0" cellspacing="0" width="100%"><tr style="height: 36px;">
                        
                <td style="width: 20px;"><img src="/Images/Common16/Stop.png" style="padding-right: 8px;" alt="Exception Indicator" /></td>
                
                <td style="width: 125px; font-weight: bold; color: #A60000">Exception Occurred:</td>
                
                <td style="text-align: left;"><asp:Label ID="ExceptionMessage" runat="server" /></td>

                <td><a class="NoDecoration HoverTextBlack" href="/WindowClose.aspx" ID="ExceptionExit" runat="server">(close)</a></td>

            </tr></table>
            
            <asp:Label ID="InnerException" runat="server"></asp:Label>
        
        </div>
                       
    </div>

    <!-- WORKFLOW INFORMATION ( END ) -->
    
    <!-- WORKFLOW FLOW CONTROL (BEGIN) -->

        <div id="FlowControl" style="border-bottom: solid 1px black; display: block; height: 0px; width: 0px; overflow: hidden;" runat="server">
    
            <asp:Button ID="ImageStart" Text="Start" OnClick="ImageStart_OnClick" runat="server" />
        
        </div>               

    <!-- WORKFLOW FLOW CONTROL ( END ) -->

</div>

<!-- HEADER ( END ) -->


<center><img id="LoadingProgressImage" src="/Images/Misc/Loading.gif" alt="Loading" runat="server" /></center>

<script type="text/javascript">

    setTimeout("document.getElementById('<%= ImageStart.ClientID %>').click();", 250);

</script>


</form>

</body>

</html>
