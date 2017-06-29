<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EntityAddress.aspx.cs" Inherits="Mercury.Web.Application.Actions.EntityAddress" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberProfileDemographics" Src="~/Application/Controls/MemberDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderProfileDemographics" Src="~/Application/Controls/ProviderDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityAddress" Src="~/Application/Controls/EntityAddress.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title></title>
    
</head>

<body>

<form id="form1" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="TelerikAjaxManager" runat="server">
    
        <AjaxSettings>
        
        </AjaxSettings>
            
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="100" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
</div>


<div id="AddressMemberContainer" style="overflow: visible; padding: 10px; font-family: segoe ui, arial; font-size: 12px; background-color: white ">

    <div style="font-family: segoe ui, arial; font-size: 12px; padding: 10px; font-weight: bold; overflow: auto;">
    
        Address <asp:Literal ID="EntityTypeDescription" runat="server"></asp:Literal>: <asp:Literal ID="EntityName" runat="server"></asp:Literal>
        
    </div>
                                               
    <Telerik:RadSplitter ID="EntityInfoAddressSplitter" Orientation="Horizontal" BackColor="White" Width="99%" runat="server">

        <Telerik:RadPane ID="EntityInfoPane" Height="22" Scrolling="None" runat="server">
        
            <Telerik:RadSlidingZone ID="EntityInfoSlidingZone" ClickToOpen="true" Height="22" SlideDirection="Bottom" runat="server">
            
                <Telerik:RadSlidingPane ID="EntityInfoDemographics" Title="Demographics" Height="400" runat="server">
                                        
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">

                        <MercuryUserControl:MemberProfileDemographics ID="MemberProfileDemographicsControl" Visible="false" runat="server" />

                        <MercuryUserControl:ProviderProfileDemographics ID="ProviderProfileDemographicsControl" Visible="false" runat="server" />
                        
                    </div>
               
                </Telerik:RadSlidingPane>
            
            </Telerik:RadSlidingZone>
        
        </Telerik:RadPane>    
        
        <Telerik:RadPane ID="EntityAddressPane" Height="600" runat="server">
        
                <MercuryUserControl:EntityAddress ID="EntityAddressControl" OnEntityAddressCompleted="EntityAddressControl_OnEntityAddressCompeleted"  runat="server" />
    
        </Telerik:RadPane>
    
    </Telerik:RadSplitter>
                               
</div>                               

</form>

</body>

</html>