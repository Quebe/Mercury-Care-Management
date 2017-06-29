<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventHandlerEditor.aspx.cs" Inherits="Mercury.Web.Application.Forms.FormDesigner.Windows.EventHandlerEditor" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title>Untitled Page</title>
    
    <style type="text/css">
        
        html { overflow: hidden; }
    
    </style>
    
</head>

<body style="padding: 0px; margin: 0px;">

<form id="form1" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>    

<div id="AjaxManagerDiv" style="display: none">
    
    <asp:ScriptManager ID="MicrosoftScriptManager" AsyncPostBackTimeout="300" runat="server"></asp:ScriptManager>

    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">

        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="EditorToolbar">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="EditorToolbar" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ScriptEditor" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ErrorListGrid" LoadingPanelID="AjaxLoadingPanel" />
                                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

        </AjaxSettings>
        
    </Telerik:RadAjaxManager>        
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="1" InitialDelayTime="100" MinDisplayTime="0" runat="server">
   
        <div style="text-align: center"><span style="text-align: center"><img src="/Images/Loading.gif" alt="Loading" /></span></div>
    
    </Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="1" InitialDelayTime="100" MinDisplayTime="0" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
        
    </Telerik:RadAjaxLoadingPanel>
    
</div>  
    
    <div style="display: none">
        
        <asp:TextBox ID="DocumentControlId" Visible="true" ReadOnly="true" runat="server" />       
        
        <asp:TextBox ID="EventName" Visible="true" ReadOnly="true" runat="server" />
        
    </div>
    
    <div style="width: 100%; background-color: #bbd7fa; padding-top: 4px; padding-bottom: 4px; height: 28px; border-bottom: solid 1px black">
    
    <Telerik:RadToolBar ID="EditorToolbar" Width="100%" AutoPostBack="true" OnClientButtonClicked="EditorToolbar_OnClientButtonClicked" OnButtonClick="EditorToolbar_OnButtonClick" runat="server">
        
        <Items>
        
            <Telerik:RadToolBarButton Text="Execute Code on Client" CheckOnClick="true" AllowSelfUnCheck="true" Group="ExecuteClient" Checked="true"  PostBack="false"  />
            
            <Telerik:RadToolBarButton IsSeparator="true" />
            
            <Telerik:RadToolBarButton Text="Smart Event" CheckOnClick="true" AllowSelfUnCheck="true" Group="SmartEvent" Checked="true"  PostBack="false"  />
            
            <Telerik:RadToolBarButton IsSeparator="true" />
            
            <Telerik:RadToolBarButton Text="Compile" ImageUrl="/Images/Common16/Compile.png" />
            
            <Telerik:RadToolBarButton Text="Errors (0)" ImageUrl="/Images/Common16/ErrorList.png" PostBack="false" />
            
            <Telerik:RadToolBarButton IsSeparator="true" />
            
            <Telerik:RadToolBarButton Text="Save" ImageUrl="/Images/Common16/Save.png" />
            
            <Telerik:RadToolBarButton Text="Save and Close" ImageUrl="/Images/Common16/Save.png" />
            
            <Telerik:RadToolBarButton Text="Cancel" ImageUrl="/Images/Common16/Stop.png"  />
        
        </Items>           
    
    </Telerik:RadToolBar>
    
    </div>
    
    <div style="clear: both;"></div>
    
    <br />
    
    <div id="ExecuteOnClient" style="font-family: Courier New; font-size: 8pt; line-height: 150%;"  runat="server">
    
        using Mercury.Client; <br /><br />

    </div>
    
    <div id="ExecuteOnServer" style="font-family: Courier New; font-size: 8pt; line-height: 150%" runat="server">
    
        using QuickSilver.Server; <br /><br />
    
    </div>
    
    <div style="font-family: Courier New; font-size: 8pt; line-height: 150%">

        void On<asp:Literal ID="EventNameLiteral" runat="server" /> (Core.Forms.Form form, Core.Forms.Control sender) { <br /><br />
        
    </div>
        
    <Telerik:RadEditor ID="ScriptEditor" EditModes="Design" Width="100%" Height="300" AutoResizeHeight="false" EnableResize="false" StripFormattingOnPaste="AllExceptNewLines" Font-Names="Courier New" Font-Size="8pt" runat="server">
    
        <Tools><Telerik:EditorToolGroup></Telerik:EditorToolGroup></Tools>
        
        <Content></Content>
    
    </Telerik:RadEditor>
    
    
    <div style="font-family: Courier New; font-size: 8pt; line-height: 150%">
    
        <br />            
        &nbsp&nbsp&nbsp&nbsp return; <br />
        <br />
        }
    
    </div>
    
    <br />
    
    <div style="border-top: solid 1px black"></div>
    
    <Telerik:RadTabStrip ID="ErrorListTabStrip" runat="server">
    
        <Tabs>
        
            <Telerik:RadTab Text="Error List" ImageUrl="/Images/Common16/ErrorList.png" Selected="true" />
        
        </Tabs>
    
    </Telerik:RadTabStrip>

    <Telerik:RadGrid ID="ErrorListGrid" Height="121" AutoGenerateColumns="false" runat="server">
    
        <MasterTableView>
        
            <Columns>
            
                <Telerik:GridBoundColumn DataField="ErrorLine" HeaderText="Line" />
                
                <Telerik:GridBoundColumn DataField="ErrorColumn" HeaderText="Column" />
                
                <Telerik:GridBoundColumn DataField="ErrorText" HeaderText="Description" />
            
            </Columns>
        
        </MasterTableView>
        
        <ClientSettings>
        
            <Scrolling AllowScroll="true" SaveScrollPosition="true" UseStaticHeaders="false" />
        
        </ClientSettings>
    
    </Telerik:RadGrid>

</form>
    

<script type="text/javascript">

    function CloseWindow() {

        var eventHandlerWindow = (window.radWindow) ? window.radWindow : ((window.frameElement.radWindow) ? window.frameElement.radWindow : null);

        if (eventHandlerWindow) { eventHandlerWindow.close(); }
        
        return;

    }

    function EditorToolbar_OnClientButtonClicked(sender, eventArgs) {

        var buttonText = eventArgs.get_item().get_text();

        switch (buttonText) {

            case "Execute Code on Client":

                document.getElementById("ExecuteOnClient").style.display = (eventArgs.get_item().get_checked()) ? "block" : "none";

                document.getElementById("ExecuteOnServer").style.display = (eventArgs.get_item().get_checked()) ? "none" : "block";

                break;

            case "Cancel":

                CloseWindow();

                break;

        }

        return;

    }

</script>

</body>

</html>
