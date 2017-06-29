<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentAccessManage.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Windows.EnvironmentAccessManage" %>

<%@ Register TagPrefix="TelerikWeb" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

    <title>Untitled Page</title>
    
</head>

<body style="margin: 2px 2px 2px 2px ">

<div>

<form id="EnvironmentAccessAdd" runat="server">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
        
    <TelerikWeb:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>

            <TelerikWeb:AjaxSetting AjaxControlID="EnvironmentSelection">
                
                <UpdatedControls>
                
                    <TelerikWeb:AjaxUpdatedControl ControlID="EnvironmentSelection" />
                
                    <TelerikWeb:AjaxUpdatedControl ControlID="EnvironmentAccessGrid" LoadingPanelID="AjaxLoadingPanelTree" />
                                
                </UpdatedControls>
            
            </TelerikWeb:AjaxSetting>

        
            <TelerikWeb:AjaxSetting AjaxControlID="SecurityAuthoritySelection">
                
                <UpdatedControls>
                
                    <TelerikWeb:AjaxUpdatedControl ControlID="SecurityAuthoritySelection" />

                    <TelerikWeb:AjaxUpdatedControl ControlID="EnvironmentAccessGrid" LoadingPanelID="AjaxLoadingPanelTree" />
                                
                </UpdatedControls>
            
            </TelerikWeb:AjaxSetting>
                    
                    
            <TelerikWeb:AjaxSetting AjaxControlID="ButtonApply">
                
                <UpdatedControls>
                
                    <TelerikWeb:AjaxUpdatedControl ControlID="SecurityAuthoritySelection" />

                    <TelerikWeb:AjaxUpdatedControl ControlID="EnvironmentAccessGrid" LoadingPanelID="AjaxLoadingPanelTree" />
                                
                </UpdatedControls>
            
            </TelerikWeb:AjaxSetting>
            
        </AjaxSettings>
    
    </TelerikWeb:RadAjaxManager>    
    
    
    <TelerikWeb:RadAjaxLoadingPanel ID="AjaxLoadingPanelTree" Transparency="1" InitialDelayTime="100" MinDisplayTime="100" runat="server">
    
        <asp:Image ID="AjaxLoadingImageTree" ImageUrl="~/Images/Loading.gif" runat="server" />
    
    </TelerikWeb:RadAjaxLoadingPanel> 
    
    <table cellpadding="0" cellspacing="0" style="width: 100%;"><tr><td align="center" valign="middle">

    <div style="width: 100%; height: 600px; vertical-align: middle; text-align: center; overflow: hidden;">
        
        <!-- Window [LogOn] (BEGIN) -->

        <table cellpadding="0" cellspacing="0" border="0" style="font-family: Arial; font-size:10pt;line-height:150%; background-color:Black">
        
            <tr style="height:1px"><td></td></tr>

            <tr>
                <td style="width:1px"></td>
                
                <td style="text-align: center; vertical-align: top;">

                    <!-- INNER PADDING (BEGIN) -->
                    <table cellpadding="0" cellspacing="0" border="0" style="width:798px; height: 595px; font-family:Arial;font-size:10pt;line-height:150%; background-color:White"><tr><td style=" vertical-align: top; background-color:Transparent">
                    
                    
                    <table cellpadding="0" cellspacing="0" border="0" style="width:798px; height: 595px; font-family:Arial; font-size: 10pt; line-height: 150%;">
                    
                        <tr>
                        
                            <td style="width: 35%; height: 100%; color: White; border-right: solid 1px black; vertical-align: top;  background-image: url(/Images/Backgrounds/WizardPanelBackground.Gif); background-repeat: repeat-x">
                            
                                <div style="padding: 10px 5px 10px 5px; background-color:Transparent; line-height: 150%; text-align: left">
                            

                                    <p style="text-align: center"><b>Manage Environment Access</b></p>

                                    <div style="height: 10px"><span></span></div>
                                    <div style="height: 1px; background-color: White"><span></span></div>
                                    <div style="height: 10px"><span></span></div>
                                    
                                    <p>This window allows you to specify Environment Access for 
                                    specific Security Groups. </p>

                                    <p>Selecting an Environment and Security Authority will present
                                    you with a list of Security Groups within that Authority to Grant or Deny access to the selected
                                    Environment.</p>
                                    
                                    <p>Users can belong to multiple Security Groups, and their access is an acculumation of all
                                    granted and denied permissions. </p>
                                    
                                    <p><b>** Denied access overrides any Grants.</b></p>
                                    
                                    <p><b>** Changing either of the selections for Environment or Security Authorities will change
                                    the available Security Groups and cancel any pending changes that have not been committed.</b></p>

                            
                                </div>
                            
                            </td>
                        
                            <td style="width: 65%; height: 595px; vertical-align: top; overflow: hidden">
    
                                <div style="height: 10px"><span></span></div>

                                <div style="height: 70px; padding: 0px 10px 0px 10px;">
                                
                                    <table width="100%" cellpadding="4px" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                    
                                        <tr>
                                        
                                            <td align="left" style="width:150px">Environment:</td>
                                            
                                            <td align="left" style=""><TelerikWeb:RadComboBox ID="EnvironmentSelection" Width="335px" ToolTip="Select Environment to Control Access" AllowCustomText="false" MarkFirstMatch = "True" OnSelectedIndexChanged="EnvironmentSelection_OnSelectedIndexChanged" AutoPostBack="true"  runat="server" /></td>
                                            
                                        </tr>
                                        
                                        <tr>
                                        
                                            <td align="left" style="width:150px">Security Authority:</td>
                                            
                                            <td align="left" style=""><TelerikWeb:RadComboBox ID="SecurityAuthoritySelection" Width="335px" ToolTip="Select Security Authority to Control Access" AllowCustomText="false" MarkFirstMatch="True" OnSelectedIndexChanged="SecurityAuthoritySelection_OnSelectedIndexChanged" runat="server" AutoPostBack="true" /></td>
                                            
                                        </tr>
                                    </table>

                                </div>

                                <div style="height: 10px"><span></span></div>

                                <div style="width:495px; height: 445px; padding: 0px 0px 0px 10px; overflow: hidden"> <!-- 495 -->
                                            
                                    <TelerikWeb:RadGrid ID="EnvironmentAccessGrid" Height="440px" AutoGenerateColumns="false" AllowAutomaticUpdates="true" AllowMultiRowEdit="true" AllowSorting="true" EnableAJAXLoadingTemplate="true" EnableAJAX="true" runat="server" >
                                    
                                        <MasterTableView TableLayout="Fixed" EditMode="Inplace" DataKeyNames="SecurityGroupId, GroupName">
                                        
                                            <Columns>
                                                                                            
                                                <TelerikWeb:GridBoundColumn DataField="SecurityGroupId" ReadOnly="true" UniqueName="SecurityGroupId" Visible="false">
                                                    <HeaderStyle />
                                                    
                                                    <ItemStyle HorizontalAlign="Left" />
                                                   
                                                </TelerikWeb:GridBoundColumn>

                                                <TelerikWeb:GridBoundColumn DataField="GroupName" HeaderText="Group Name" ReadOnly="true" UniqueName="GroupName" Visible="true">
                                                    <HeaderStyle />
                                                    
                                                    <ItemStyle HorizontalAlign="Left" />
                                                   
                                                </TelerikWeb:GridBoundColumn>
                                                
                                                <TelerikWeb:GridCheckBoxColumn DataField="Is Granted" HeaderText="Is Granted" ReadOnly="false" UniqueName="Is Granted" Visible="true" AllowFiltering="false" >
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    
                                                </TelerikWeb:GridCheckBoxColumn>

                                                <TelerikWeb:GridCheckBoxColumn DataField="Is Denied" HeaderText="Is Denied" ReadOnly="false" UniqueName="Is Denied" Visible="true" AllowFiltering="false">
                                                    <HeaderStyle Width="70px" HorizontalAlign="Center" />
                                                
                                                    <ItemStyle HorizontalAlign="Center" />

                                                </TelerikWeb:GridCheckBoxColumn>
                                            
                                            </Columns>
                                        
                                        </MasterTableView>
                                    
                                        <ClientSettings>
                                                
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />

                                            <Selecting AllowRowSelect="true" />
                                        
                                        </ClientSettings>
                                    
                                    </TelerikWeb:RadGrid>
    
                                </div>
                                
                                <div style="height: 15px"><span></span></div>

                                <div style="height: 30px; padding: 0px 10px 0px 10px;">
                                
                                    <span style="float: right;"></span>

                                    <span style="float: right;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Font-Names="Arial" Font-Size="10pt" runat="Server" /></span>

                                    <span style="float: right; width: 10px">&nbsp</span>

                                    <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="Arial" Font-Size="10pt" runat="Server" /></span>

                                    <span style="float: right; width: 10px">&nbsp</span>

                                    <span style="float: right;"><asp:Button ID="ButtonOk" Text="Ok" Width="73px" Font-Names="Arial" Font-Size="10pt" runat="Server" /></span>

                                </div>

                            </td>

                        </tr>

                    </table>
                    
                    
                    </td></tr></table> <!-- INNER PADDING ( END ) -->

                </td>
                
                <td style="width:1px"></td>
                
            </tr>

            <tr style="height:1px"><td></td></tr>

        </table>

        <!-- Window [LogOn] (End) -->

    </div>    

    </td></tr></table>
   

</form>
    
</div>

</body>   

</html>