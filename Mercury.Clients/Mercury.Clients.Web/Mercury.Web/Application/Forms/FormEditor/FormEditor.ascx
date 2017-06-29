<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FormEditor.ascx.cs" EnableViewState="false" Inherits="Mercury.Web.Application.Forms.FormEditor.FormEditor" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div id="DocumentAjaxManagerProxy" style="display: none; overflow: hidden; height: 0px">

    <asp:ScriptManagerProxy runat="server">
    
        <Scripts>
                        
        </Scripts>
    
    </asp:ScriptManagerProxy>

    <Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="FormPagerFirst"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="FormPagerPrevious"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="FormPagerPage1"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerPage2"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="FormPagerPage3"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerPage4"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="FormPagerPage5"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerPage6"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
            <Telerik:AjaxSetting AjaxControlID="FormPagerPage7"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerPage8"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
           
            <Telerik:AjaxSetting AjaxControlID="FormPagerPage9"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerNext"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        

            <Telerik:AjaxSetting AjaxControlID="FormPagerLast"><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="FormPaging" LoadingPanelID="AjaxLoadingPanelWhiteout" /><Telerik:AjaxUpdatedControl ControlID="FormContent" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>        
            
        </AjaxSettings>
              
    </Telerik:RadAjaxManagerProxy>
    
</div>

<div style="display: none;"><asp:TextBox ID="FormInstanceId" Text="" TabIndex="-1" style="width: 0px; height: 0px;" runat="server" /></div>    

    
<table cellpadding="0" cellspacing="0" style="width: 100%; background-color: #ababab;" border="0"><tr><td align="center" valign="top">

<div id="FormContent" style="width: 8in; min-height: 1in; text-align: left; margin: 8px; padding: 4px; border: solid 1px black; background-color: White; overflow: auto;" runat="server">
                        

</div>
     
</td></tr></table>

<link rel="Stylesheet" href="/Styles/PagerOffice2007.css" type="text/css" runat="server"/>


<div id="FormPaging" style="font-family: segoe ui, arial; font-size: 11px; line-height: 150%; display: none;" runat="server">

    <table class="PagerOffice2007"><tr>

        <td><asp:LinkButton ID="FormPagerFirst" CssClass="PagerButton PageFirst" Text=" " OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPrevious" CssClass="PagerButton PagePrevious" Text=" " OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage1" CssClass="PagerButton" Text="1" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage2" CssClass="PagerButton" Text="2" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage3" CssClass="PagerButton" Text="3" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage4" CssClass="PagerButton" Text="4" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage5" CssClass="PagerButton" Text="5" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage6" CssClass="PagerButton" Text="6" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage7" CssClass="PagerButton" Text="7" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage8" CssClass="PagerButton" Text="8" OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerPage9" CssClass="PagerButton" Text="9" OnClick="FormPagerButton_OnClick" runat="server" /></td>
        
        <td><asp:LinkButton ID="FormPagerNext" CssClass="PagerButton PageNext" Text=" " OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td><asp:LinkButton ID="FormPagerLast" CssClass="PagerButton PageLast" Text=" " OnClick="FormPagerButton_OnClick" runat="server" /></td>

        <td style="width: 100%;">&nbsp;</td>

    </tr></table>


    

</div>


<div runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('keydown', FormEditor_OnKeyDown, false); } else { document.attachEvent('onkeydown', FormEditor_OnKeyDown); }

    function FormEditor_OnKeyDown(forEvent) {

        forEvent = (forEvent) ? forEvent : ((event) ? event : null);

        var cancelBubbleEvent = false;


        if ((forEvent.ctrlKey) && (forEvent.keyCode == 192)) {  // CTRL-`

            keyPressWindowSequence = true;

            cancelBubbleEvent = true;

            formInstanceIdControl = document.getElementById("<%= FormInstanceId.ClientID %>");

            if (formInstanceIdControl != null) {

                window.open("/Application/Forms/FormEditor/DeveloperTools.aspx?FormInstanceId=" + formInstanceIdControl.value, "FormDeveloperTools");

            }
            
        }

        if (cancelBubbleEvent) {

            forEvent.keyCode = 0;

            forEvent.cancelBubble = true;

            forEvent.returnValue = false;

            if (forEvent.preventDefault) { forEvent.preventDefault(); }

            if (forEvent.stopPropagation) { forEvent.stopPropagation(); }

            return false;

        }

        return true;

    }

</script>

</div>
