<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeveloperTools.aspx.cs" Inherits="Mercury.Web.Application.Forms.FormEditor.DeveloperTools" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title></title>
    
</head>

<body style="margin: 0px 0px 0px 0px;">

<form id="form1" runat="server">

<div>

    <div style="display: none;">
    
        <asp:ScriptManager ID="MicrosoftScriptManager" AsyncPostBackTimeout="600" runat="server"></asp:ScriptManager>

        <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
        
            <AjaxSettings>
            
                <Telerik:AjaxSetting AjaxControlID="DesignerToolbar" >

                    <UpdatedControls>

                        <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="FormControlPropertiesGrid" LoadingPanelID="AjaxLoadingPanel" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="ToolsOutput" LoadingPanelID="AjaxLoadingPanel" />

                    </UpdatedControls>

                </Telerik:AjaxSetting>

                <Telerik:AjaxSetting AjaxControlID="FormExplorerTree" >

                    <UpdatedControls>

                        <Telerik:AjaxUpdatedControl ControlID="FormExplorerTree" LoadingPanelID="AjaxLoadingPanel" />
                        
                        <Telerik:AjaxUpdatedControl ControlID="FormControlPropertiesGrid" LoadingPanelID="AjaxLoadingPanel" />
                        
                    </UpdatedControls>

                </Telerik:AjaxSetting>
                
            </AjaxSettings>
            
        </Telerik:RadAjaxManager>
            
        <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="100" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
        
        <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="0" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
        
            <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
        
            </div>
                
        </Telerik:RadAjaxLoadingPanel>        
       
    </div>
    
    
<div style="display: none"><asp:TextBox ID="FormInstanceId" Text="" runat="server" /></div>    
   

    <div id="TitleBar" style="background-color: #bbd7fa; border: solid 1px black" runat="server">
       
        <Telerik:RadToolBar ID="DesignerToolbar" OnButtonClick="DesignerToolbar_OnButtonClick" AutoPostBack="true" runat="server">

            <Items>
            
                <Telerik:RadToolBarButton Text="Refresh"  Value="Refresh" ImagePosition="AboveText" ImageUrl="/Images/Common32/Refresh.png" />
                
                <Telerik:RadToolBarButton Value="Separator1" IsSeparator="true" Visible="false" />
                
            </Items>
            
        </Telerik:RadToolBar>
        
    </div>    
    
            
    <Telerik:RadSplitter ID="SplitterContainer" Orientation="Vertical" Width="100%" BackColor="White" runat="server">
    
        <Telerik:RadPane ID="SplitterPaneTreeView" Width="50%" Scrolling="Both" BackColor="White" runat="server" >
        
            <Telerik:RadTreeView ID="FormExplorerTree" OnNodeClick="FormExplorerTree_OnNodeClick" BackColor="White" runat="server">
            
                <Nodes></Nodes>
           
            </Telerik:RadTreeView>
        
        </Telerik:RadPane>
        
        <Telerik:RadSplitBar ID="SplitterBar" runat="server" CollapseMode="Both" />
        
        <Telerik:RadPane ID="SplitterPaneGrid" runat="server" Scrolling="Both" BackColor="White">
    
            <Telerik:RadGrid ID="FormControlPropertiesGrid" AutoGenerateColumns="false" AllowSorting="true" runat="server">
            
                <MasterTableView  runat="server" />
            
                <ClientSettings>
                
                    <Selecting AllowRowSelect="true" />    
                    
                </ClientSettings>
            
            </Telerik:RadGrid>
        
        </Telerik:RadPane>
    
    </Telerik:RadSplitter>


<asp:TextBox ID="ToolsOutput" TextMode="MultiLine" ReadOnly="true" Width="99%" Height="300" runat="server"></asp:TextBox>

    
</div>

</form>

</body>

</html>

