<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityGroupProperties.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Windows.SecurityGroupProperties" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

    <title>Untitled Page</title>
            
    <style type="text/css">

        .radReadOnlyCss_Office2007 {
    	    border:1px solid #999999 !important;
            color:#000 !important;
            font:12px segoe ui, arial,tahoma,sans-serif !important;
	        background:#fff !important;
	        padding:1px 0 1px 1px !important;
        }
        

        .GridEditRow_Vista
        {
            background:#fcfcfc !important;
        }

        .GridEditRow_Vista td
        {
            		border-color:#fcfcfc #fff #fcfcfc #ededed !important;
        }

        
       
    </style>
    
</head>

<body>

<form id="FormSecurityGroupProperties" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />

    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
        </AjaxSettings>
        
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="1" InitialDelayTime="100" MinDisplayTime="0" runat="server">
   
        <div style="text-align: center"><span style="text-align: center"><img src="/Images/Loading.gif" alt="Loading" /></span></div>
    
    </Telerik:RadAjaxLoadingPanel>
    
</div>

<div style="font-family: segoe ui, Arial, Helvetica, Sans-Serif; font-size: 10pt; line-height: 150%">

    <div style="position: relative; float: left; width: 99%; height: 500px; margin: 1px; border: solid 1px black;">
    
        <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="General"></Telerik:RadTab>

                <Telerik:RadTab Text="Enterprise Permissions"></Telerik:RadTab>

                <Telerik:RadTab Text="Environment Access"></Telerik:RadTab>

                <Telerik:RadTab Text="Environment Roles"></Telerik:RadTab>
                
            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="PageGeneral" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Users.png" alt="Security Group Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Security Group</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Security Authority and Group</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 35%; padding: 4px;">Security Authority Name:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityName" Width="98%" ReadOnly="true" runat="server" /></div>
                    
                        </div>
                    
                        <div style="overflow: hidden;">
                                            
                            <div style="position: relative; float: left; width: 35%; padding: 4px;">Security Group Name:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityGroupName" Width="98%" ReadOnly="true" runat="server" /></div>
                        
                        </div>
                                    
                        <div style="overflow: hidden;">
                    
                            <div style="position: relative; float: left; width: 35%; padding: 4px;">Security Group Id:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityGroupId" Width="98%" ReadOnly="true" runat="server" /></div>
                    
                        </div>

                        <div style="overflow: hidden;">
                                            
                            <div style="position: relative; float: left; width: 35%; padding: 4px;">Group Description:</div>
                            
                         </div>
                    
                        <div style="overflow: hidden;">

                            <div style="position: relative; float: left; width: 99%; padding: 4px;"><Telerik:RadTextBox ID="SecurityGroupDescription" Width="98%" ReadOnly="true" TextMode="MultiLine" Rows="10" runat="server" /></div>
                    
                        </div>
                        
                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>    

            <Telerik:RadPageView ID="PageEnterprisePermissions" runat="server">
                                            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Users.png" alt="Enterprise Permissions" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Enterprise Permissions for Security Group</div>
                    
                    </div>
                  
                   
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Enterprise Permissions</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="EnterprisePermissionsGrid" Height="380" AutoGenerateColumns="false" AllowMultiRowEdit="true" runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Fixed" EditMode="InPlace">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="PermissionId" UniqueName="PermissionId" HeaderText="Id" ReadOnly="true" Visible="false">
                                    
                                        <HeaderStyle Width="60" HorizontalAlign="Left" />
                                        
                                        <ItemStyle HorizontalAlign="Left" />
                                    
                                    </Telerik:GridBoundColumn>
                                    
                                    <Telerik:GridBoundColumn DataField="Permission" UniqueName="Permission" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsGranted" UniqueName="IsGranted" HeaderText="Is Granted" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="60" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    
                                    </Telerik:GridCheckBoxColumn>
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsDenied" UniqueName="IsDenied" HeaderText="Is Denied" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="60" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center"  />
                                    
                                    </Telerik:GridCheckBoxColumn>
                                
                                </Columns>
                                                        
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Resizing EnableRealTimeResize="true" ResizeGridOnColumnResize="false" />
                            
                                <Scrolling AllowScroll="true" />

                                <Selecting AllowRowSelect="true" />
                                
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                        
                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>  
                                 
            <Telerik:RadPageView ID="PageEnvironmentAccess" runat="server">
                                            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Users.png" alt="Environment Access" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Environment Access for Security Group</div>
                    
                    </div>
                  
                   
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Environment Access</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="EnvironmentAccessGrid" Height="380" AutoGenerateColumns="false" AllowMultiRowEdit="true" runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Auto" EditMode="InPlace">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="EnvironmentId" UniqueName="EnvironmentId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="EnvironmentName" UniqueName="EnvironmentName" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsGranted" UniqueName="IsGranted" HeaderText="Is Granted" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="70" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center"  />
                                    
                                    </Telerik:GridCheckBoxColumn>                                    
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsDenied" UniqueName="IsDenied" HeaderText="Is Denied" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="70" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center"  />
                                    
                                    </Telerik:GridCheckBoxColumn>
                                    
                                </Columns>
                                                        
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />

                                <Selecting AllowRowSelect="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                        
                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>  
                             
            <Telerik:RadPageView ID="PageEnvironmentRoles" runat="server">
                                            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Users.png" alt="Environment Roles" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Environment Roles for Security Group</div>
                    
                    </div>
                  
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Environment Roles</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="RadGrid1" Height="380" AutoGenerateColumns="false" runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="EnvironmentId" UniqueName="EnvironmentId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="EnvironmentName" UniqueName="EnvironmentName" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="RoleId" UniqueName="RoleId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="RoleName" UniqueName="RoleName" HeaderText="Type" ReadOnly="true" Visible="true" />
                                
                                </Columns>
                                                        
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />

                                <Selecting AllowRowSelect="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                        
                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>  
                       
        </Telerik:RadMultiPage>
                
    </div>

    <div style="clear: both; height: 10px"><span></span></div>

    <div style="height: 20px; padding: 0px 10px 0px 10px;">
    
        <span style="float: left"><asp:Label ID="SaveResponseLabel" Text="" runat="server" /></span>
    
        <span style="float: right;"></span>

        <span style="float: right;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></span>

    </div>
                
</div>

</form>

</body>

</html>
