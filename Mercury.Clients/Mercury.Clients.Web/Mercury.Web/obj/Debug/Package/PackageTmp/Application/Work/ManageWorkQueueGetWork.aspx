<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageWorkQueueGetWork.aspx.cs" Inherits="Mercury.Web.Application.Work.ManageWorkQueueGetWork" %>

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

<form id="FormWorkQueue" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
              
            <Telerik:AjaxSetting AjaxControlID="ButtonAddTeam">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddTeam" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueTeamSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="UserViewGrid" />

                    <Telerik:AjaxUpdatedControl ControlID="UserViewAccountSelection" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkTeamsGrid">
            
                <UpdatedControls>
                                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkTeamsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueTeamSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="UserViewGrid" />

                    <Telerik:AjaxUpdatedControl ControlID="UserViewAccountSelection" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddUserView">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddUserView" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="UserViewGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="UserViewWorkQueueViewSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="UserViewAccountSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkTeamsGrid">
            
                <UpdatedControls>
                                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddUserView" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="UserViewGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="UserViewWorkQueueViewSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="UserViewAccountSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddExtendedProperty">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddExtendedProperty" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ExtendedPropertiesGrid" LoadingPanelID="AjaxLoadingPanel" />
                                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ExtendedPropertiesGrid">
            
                <UpdatedControls>
                                    
                    <Telerik:AjaxUpdatedControl ControlID="ExtendedPropertiesGrid" LoadingPanelID="AjaxLoadingPanel" />
                                    
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
 
 
<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div style="min-width: 800px;">

    <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
        <Tabs>
            
            <Telerik:RadTab Text="General"></Telerik:RadTab>

            <Telerik:RadTab Text="Get Work"></Telerik:RadTab>
                
        </Tabs>
                  
    </Telerik:RadTabStrip>

    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueue.png" alt="Work Queue Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Work Queue</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueDescription" Width="98%" Rows="5" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>

                    </div>
                   
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkQueueEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkQueueVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                                      
                                      
                    <div class="PropertyPageSectionTitle">Work and Schedule</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                    <table style="width: 99%; table-layout: fixed"><tr>
                    
                        <td style="width: 20%;">Workflow:</td>
                    
                        <td style="width: 72%;"><Telerik:RadComboBox ID="WorkQueueWorkflow" Width="99%" runat="server" /></td>
                    
                    </tr></table>
                    
                    <table cellspacing="4" style="width: 99%; table-layout: fixed"><tr>

                        <td style="width: 20%; text-align: left">Initial Constraint:&nbsp</td>

                        <td style="width: 10%"><Telerik:RadNumericTextBox ID="WorkQueueInitialConstraintValue" Width="90%" NumberFormat-DecimalDigits="0" MinValue="0" runat="server" /></td>

                        <td style="width: 15%">
                        
                            <Telerik:RadComboBox ID="WorkQueueInitialConstraintQualifier" Width="99%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td style="width: 02%">&nbsp</td>
                        
                        <td style="width: 20%; text-align: left">Initial Milestone:&nbsp</td>

                        <td style="width: 10%"><Telerik:RadNumericTextBox ID="WorkQueueInitialMilestoneValue" Width="90%" NumberFormat-DecimalDigits="0" MinValue="0" runat="server" /></td>

                        <td style="width: 15%">
                        
                            <Telerik:RadComboBox ID="WorkQueueInitialMilestoneQualifier" Width="99%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months"/>
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        </tr><tr>
                        
                        <td style="width: 20%; text-align: left">Threshold:&nbsp</td>

                        <td style="width: 10%"><Telerik:RadNumericTextBox ID="WorkQueueThresholdValue" Width="90%" NumberFormat-DecimalDigits="0" MinValue="0" runat="server" /></td>

                        <td style="width: 15%">
                        
                            <Telerik:RadComboBox ID="WorkQueueThresholdQualifier" Width="99%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months"/>
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td style="width: 02%">&nbsp</td>
                        

                        <td style="width: 20%; text-align: left">Schedule:&nbsp</td>
                        
                        <td style="width: 10%"><Telerik:RadNumericTextBox ID="WorkQueueScheduleValue" Width="90%" NumberFormat-DecimalDigits="0" MinValue="0" runat="server" /></td>

                        <td style="width: 15%">
                        
                            <Telerik:RadComboBox ID="WorkQueueScheduleQualifier" Width="99%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                        </td>
                                                
                    </tr></table>
                            
                    </div>                
                                                         
                    
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="PageGetWork" runat="server">

            <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueueTeams.png" alt="Work Queue Get Work" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Work Queue Get Work Views</td>
                    
                    </tr></table>
                
                    
                    <div style="clear:both; margin: 2px 10px 10px 10px; line-height: 150%">
                    
                    This page allows you to associated Work Queue Views with the Work Queue and to individual users of the Work Queue and 
                    
                    the "Get Work" functionality. If a 

                    user is assigned a specific Work Queue View, this will override the default Work Queue View assigned to the Work Queue. Only

                    users with Work permission to the Work Queue are available for Work Queue View assignment.

                    </div>


                    <div class="PropertyPageSectionTitle">Default Get Work</div>
                      
                    <div style="margin: 2px 10px 0px 10px; padding: 4px 4px 0px 4px; line-height: 150%;">
                    
                    <table style="width: 99%; table-layout: fixed"><tr>
                    
                        <td style="width: 20%;">Work Queue View:</td>
                    
                        <td style="width: 55%;"><Telerik:RadComboBox ID="GetWorkViewSelection" Width="99%" runat="server" /></td>
                        
                        <td>

                            <div style="">
                            
                                <div style="position: relative; float: left; padding: 4px;"><asp:CheckBox ID="GetWorkUseGrouping" runat="server" /></div>

                                <div style="position: relative; float: left; padding: 4px;">Use Grouping</div>
                            
                            </div>
                        
                        </td>
                                   
                    </tr></table>
                    
                    </div>                
                                     
                    <div class="PropertyPageSectionTitle">User Specific Get Work Views</div>              

                    <div style="margin: 0px; padding: 0px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="UserViewGrid" Height="255" AutoGenerateColumns="false" 

                            OnNeedDataSource="UserViewGrid_OnNeedDataSource"

                            OnDeleteCommand="UserViewGrid_OnDeleteCommand"
                        
                            runat="server">
                    
                            <MasterTableView Width="100%" DataKeyNames="SecurityAuthorityId,SecurityAuthorityName,UserAccountId,UserAccountName" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="SecurityAuthorityId" UniqueName="SecurityAuthorityId" HeaderText="SecurityAuthorityId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="SecurityAuthorityName" UniqueName="SecurityAuthorityName" HeaderText="Authority Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserAccountId" UniqueName="UserAccountId" HeaderText="UserAccountId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserAccountName" UniqueName="UserAccountName" HeaderText="Account Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="UserDisplayName" UniqueName="UserDisplayName" HeaderText="User Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="WorkQueueViewName" UniqueName="WorkQueueViewName" HeaderText="Work Queue View" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn HeaderText="Action" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this User Account?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Assign Work Queue View to User</div>

                    <table cellpadding="0" cellspacing="0" style="margin: .125in;"><tr>                        
                                
                        <td style="white-space: nowrap; padding-right: .125in; width: 100px;">User Account:</td>
                        
                        <td style=""><Telerik:RadComboBox ID="UserViewAccountSelection" Width="100%" Sort="Ascending" runat="server"></Telerik:RadComboBox></td>
                                              
                        <td style="width: 100px; padding-left: .125in; padding-right: .125in;">Work Queue View:</td>
                            
                        <td style="">
                                
                            <Telerik:RadComboBox ID="UserViewWorkQueueViewSelection" Width="100%" Sort="Ascending" runat="server">                                    
                                
                            </Telerik:RadComboBox>
                                

                        </td>

                        <td style="width: 80px; padding-left: .25in;"><asp:Button ID="ButtonAddUserView" Text="Add User" OnClick="ButtonAddUserView_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>

                    </tr></table>
                                
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
