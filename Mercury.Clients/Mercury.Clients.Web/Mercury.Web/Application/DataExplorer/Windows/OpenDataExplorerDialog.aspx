<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenDataExplorerDialog.aspx.cs" Inherits="Mercury.Web.Application.DataExplorer.Windows.OpenDataExplorerDialog" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title>Data Explorer - Open</title>
    
    <style type="text/css">
        
        html { overflow: hidden; }
    
    </style>
    
</head>

<body style="padding: 0px; margin: 0px;">

<form id="form1" runat="server"><div>

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
    
<Telerik:RadGrid ID="DataExplorersAvailableGrid" AutoGenerateColumns="false" OnNeedDataSource="DataExplorersAvailableGrid_OnNeedDataSource" EnableViewState="false" runat="server">

    <MasterTableView DataKeyNames="Id" ClientDataKeyNames="Id,Name">
    
        <Columns>
        
            <Telerik:GridBoundColumn DataField="Id" HeaderText="Id" />
            
            <Telerik:GridBoundColumn DataField="Name" HeaderText="Name" />
            
            <Telerik:GridBoundColumn DataField="ModifiedAccountInfo.UserAccountName" HeaderText="Modified By" />
            
            <Telerik:GridBoundColumn DataField="ModifiedAccountInfo.ActionDate" HeaderText="Modified Date" />
            
        </Columns>
    
    </MasterTableView>
    
    <ClientSettings>

        <ClientEvents OnRowDblClick="DataExplorersAvailableGrid_OnRowDblClick" />
    
        <Selecting AllowRowSelect="true" />

        <Scrolling AllowScroll="false" />
    
    </ClientSettings>

</Telerik:RadGrid>


<Telerik:RadCodeBlock ID="TourCodeBlock" runat="server">

<script type="text/javascript">

    function DataExplorersAvailableGrid_OnRowDblClick(sender, e) {

        var dataExplorerId = e.getDataKeyValue("Id");

        var dialogWindow = (window.radWindow) ? window.radWindow : window.frameElement.radWindow;

        dialogWindow.close(dataExplorerId);

        return;

    }

</script>

</Telerik:RadCodeBlock>


</div></form>

</body>

</html>

