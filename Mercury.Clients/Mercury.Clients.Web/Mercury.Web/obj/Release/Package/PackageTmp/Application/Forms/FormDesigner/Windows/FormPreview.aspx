<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPreview.aspx.cs" Inherits="Mercury.Web.Application.Forms.FormDesigner.Windows.FormPreview" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register src="../../FormEditor/FormEditor.ascx" tagname="FormEditor" tagprefix="MercuryForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Form Preview</title>

</head>

<body style="background-color: #ababab">

    <form id="form1" runat="server">

    <div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>    

    <div style="display: none">
    
    <asp:ScriptManager ID="MicrosoftScriptManager" AsyncPostBackTimeout="300" runat="server"></asp:ScriptManager>

    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
    
        <ClientEvents OnRequestStart="TelerikAjaxManager_OnRequestStart" OnResponseEnd="TelerikAjaxManager_OnResponseEnd" />
            
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="0" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="0" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>

    <asp:TextBox ID="LastBlurControl" Text="" runat="server" />
    
    <asp:TextBox ID="LastFocusControl" Text="" runat="server" />
    
    </div>
        
        
    <MercuryForm:FormEditor ID="PreviewFormEditor" runat="server" />


    <div id="FocusScript" style="display: none;" runat="server">

    <script type="text/javascript" >

        if (document.addEventListener) { document.addEventListener('focus', Document_OnControlFocus, true); }

        if (document.addEventListener) { document.addEventListener('click', Document_OnClick, true); }
        
        function Document_OnControlFocus(forEvent) {

            lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

            lastBlurControl = document.getElementById("<%= LastBlurControl.ClientID %>");
            
            lastBlurControl.value = lastFocusControl.value;
            
            if (forEvent.target != null) {

                lastFocusControl.value = forEvent.target.id;

            }
            
            else if (forEvent.srcElement != null) {
            
                lastFocusControl.value = forEvent.srcElement.id;
                
            }

            return;
            
        }

        function Document_OnClick(event) {

            if (event.target != null) {

                if (event.target) {

                    event.target.focus();

                }

            }

            return;

        }

        function TelerikAjaxManager_OnRequestStart(sender, eventArgs) {
            
            if (document.activeElement) {

                lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

                lastFocusControl.value = document.activeElement.id;

            }

            return;

        }
    
        function TelerikAjaxManager_OnResponseEnd(sender, eventArgs) {

            lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");
            
            if (lastFocusControl) {

                if (lastFocusControl.value) {

                    setTimeout(AjaxResponse_SetFocus, 10);

                }

            }

            return;

        }

        function AjaxResponse_SetFocus() {

            lastFocusControl = document.getElementById("<%= LastFocusControl.ClientID %>");

            if (lastFocusControl) {

                if (lastFocusControl.value) {

                    control = document.getElementById(lastFocusControl.value);

                    if (control) { control.focus(); }
                       
                }

            }
            
            return;

        }

    </script>

    </div>
    
    </form>

</body>

</html>
