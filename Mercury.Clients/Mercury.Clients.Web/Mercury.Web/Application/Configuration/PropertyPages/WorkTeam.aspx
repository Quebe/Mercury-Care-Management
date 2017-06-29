<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkTeam.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.WorkTeam" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

    <title>Untitled Page</title>
    
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />

    <link rel="Stylesheet" href="/Styles/Global.css" type="text/css" />

    <link rel="Stylesheet" href="/Styles/PropertyPage.css" type="text/css" />

    <style type="text/css">

    .radReadOnlyCss_Office2007 {
    	border:1px solid #999999 !important;
        color:#000 !important;
        font:12px segoe ui, arial,tahoma,sans-serif !important;
	    background:#fff !important;
	    padding:1px 0 1px 1px !important;
    }

    </style>

</head>

<body class="TextNormal" style="margin: 0px;">

<form id="FormWorkTeam" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
              
            <Telerik:AjaxSetting AjaxControlID="ButtonAddMembership">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddMembership" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamMembershipGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamMembershipAccountSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />   
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkTeamMembershipGrid">
            
                <UpdatedControls>
                                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamMembershipGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamMembershipAccountSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />                            
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
        </AjaxSettings>
        
    </Telerik:RadAjaxManager>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
 </div>
 
 <Telerik:RadFormDecorator Id="TelerikFormDecorator" DecoratedControls="All" runat="server" />
        
<div style="min-width: 800px;">

    <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
        <Tabs>
            
            <Telerik:RadTab Text="General"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Membership"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;"> 

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkTeam.png" alt="Work Team Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Work Team</td>
                    
                    </tr></table>
                                                           
                    <div class="PropertyPageSectionTitle">Name and Description</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamDescription" Width="98%" Rows="12" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>

                    </div>
                    
                    <div style="clear: both;"></div>
                    
                    <div style="margin: .125in;">
                    
                        <div style="float: left; padding-left: .125in; white-space: nowrap;">Work Team Type</div>
                    
                        <div style="position: relative; float: left; width: 25%;  padding-left: .125in">
                            
                            <Telerik:RadComboBox ID="WorkTeamType" Width="100" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Text="Work Team" Value="0" Selected="true" />

                                    <Telerik:RadComboBoxItem Text="Care Team" Value="1" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                         </div>

                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; "><asp:CheckBox ID="WorkTeamEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; ">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; "><asp:CheckBox ID="WorkTeamVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; ">Visible</div>
                        
                        </div>
                        
                    </div>                                      
                    
                    <div style="clear: both;"></div>

                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkTeamCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkTeamModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkTeamModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
           
            
            <Telerik:RadPageView ID="PageMembership" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkTeam.png" alt="Work Team Membership" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Work Team Membership</td>
                    
                    </tr></table>
                                                       
                    <div class="PropertyPageSectionTitle">Current Membership</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkTeamMembershipGrid" Height="390" AutoGenerateColumns="false" OnDeleteCommand="WorkTeamMembershipGrid_OnDeleteCommand" runat="server">
                    
                            <MasterTableView Width="99%" DataKeyNames="SecurityAuthorityId,SecurityAuthorityName,UserAccountId,UserAccountName" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="SecurityAuthorityId" UniqueName="SecurityAuthorityId" HeaderText="SecurityAuthorityId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="SecurityAuthorityName" UniqueName="SecurityAuthorityName" HeaderText="Authority Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserAccountId" UniqueName="UserAccountId" HeaderText="UserAccountId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserAccountName" UniqueName="UserAccountName" HeaderText="Account Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserDisplayName" UniqueName="UserDisplayName" HeaderText="User Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="TeamRole" UniqueName="TeamRole" HeaderText="Role" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this User Account?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                    <div class="PropertyPageSectionTitle">Add User to Work Team</div>

                    <table cellpadding="0" cellspacing="0" style="margin: .25in;"><tr>                        
                                
                        <td style="white-space: nowrap; padding-right: .125in; width: 100px;">User Account:</td>
                        
                        <td style=""><Telerik:RadComboBox ID="WorkTeamMembershipAccountSelection" Width="100%" Sort="Ascending" runat="server"></Telerik:RadComboBox></td>
                                              
                        <td style="width: 50px; padding-left: .125in; padding-right: .125in;">Role:</td>
                            
                        <td style="width: 100px;">
                                
                            <Telerik:RadComboBox ID="WorkTeamMembershipTeamRole" Width="100%" Sort="Ascending" runat="server">
                                    
                                <Items>
                                        
                                    <Telerik:RadComboBoxItem Text="Member" Value="0" />
                                            
                                    <Telerik:RadComboBoxItem Text="Manager" Value="1" />
                                        
                                </Items>
                                
                            </Telerik:RadComboBox>
                                

                        </td>

                        <td style="width: 100px; padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonAddMembership" Text="Add User" OnClick="ButtonAddMembership_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>

                    </tr></table>
                    
                </td>
                
                </tr></table>

            </Telerik:RadPageView>    
           
        </Telerik:RadMultiPage>
        
    </div>

    
    <div style="height: .125in;">&nbsp;</div>

    <table cellpadding="0" cellspacing="0" style="width: 100%" border="0"><tr>
    
        <td style="padding-left: .125in; white-space: nowrap; width: 85px;">Last Response:</td>

        <td style="padding-left: .125in;"><asp:Label ID="SaveResponseLabel" Text="N/A" runat="server" /></td>
    
        <td style="width: .125in;">&nbsp;</td>

        <td style="width: 80px;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonOk_OnClick" runat="Server" /></td>    
    
        <td style="width: .125in;">&nbsp;</td>

        <td style="width: 80px;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonCancel_OnClick" runat="Server" /></td>
        
        <td style="width: .125in;">&nbsp;</td>

        <td style="width: 80px;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonApply_OnClick" runat="Server" /></td>

    </tr></table>
            
</div>        

</form>

</body>

</html>
