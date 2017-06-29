<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Workflow.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.Workflow" %>

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

<form id="FormWorkflow" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server">

        <Scripts>
    
        </Scripts>

    </asp:ScriptManager>
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="WorkflowParametersGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddWorkflowParameter">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddWorkflowParameter" LoadingPanelID="" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>        
        
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddWorkflowPermission">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddWorkflowPermission" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowPermissionTeamSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowPermissionsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>        
        
        
        
            <Telerik:AjaxSetting AjaxControlID="WorkflowPermissionsGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddWorkflowPermission" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowPermissionTeamSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkflowPermissionsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
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
                
            <Telerik:RadTab Text="Assembly"></Telerik:RadTab>

            <Telerik:RadTab Text="Parameters"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Permissions"></Telerik:RadTab>
            
            <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>

    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Workflow.png" alt="Workflow Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Workflow</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowDescription" Width="98%" Rows="7" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>

                    </div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkflowEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 20%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkflowVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                    
                    <div class="PropertyPageSectionTitle">Entity Type and Action</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Entity Type:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadComboBox ID="WorkflowEntityType" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Action Verb:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowActionVerb" Width="100%" MaxLength="60" EmptyMessage="(required if Entity Type specified)" runat="server" /></div>
                    
                    </div>
                    
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkflowCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkflowModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="PageAssembly" runat="server">

                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkflowAssembly.png" alt="Assembly Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Assembly Definition for the Workflow</td>
                    
                    </tr></table>
                                                       
                    <div class="PropertyPageSectionTitle">Assembly and Class Name</div>
                                         
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 25%; padding: 4px;">.NET Framework Version:</div>
                    
                        <div style="position: relative; float: left; width: 25%; padding: 4px; padding-right: .125in">
                            
                            <Telerik:RadComboBox ID="WorkflowFramework" Width="100" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Text=".NET 3.5 WF" Value="0" />

                                    <Telerik:RadComboBoxItem Text=".NET 4.0 WF" Value="1" Selected="true" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                         </div>

                    </div>
    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Path:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowAssemblyPath" Width="100%" MaxLength="255" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">File Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowAssemblyName" Width="100%" MaxLength="99" EmptyMessage="(required)" runat="server" /></div>

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Class:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkflowAssemblyClassName" Width="100%" MaxLength="99" EmptyMessage="(required)" runat="server" /></div>

                    </div>

                    <div style="clear: both;"></div>
                    
                </td>
                                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>
                 
            <Telerik:RadPageView ID="PageParameters" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkflowParameters.png" alt="Workflow Parameters" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Parameters for the Workflow</td>
                    
                    </tr></table>
                    
                    <div class="PropertyPageSectionTitle">Workflow Parameters</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkflowParametersGrid" Height="390" OnItemCommand="WorkflowParametersGrid_OnItemCommand" OnDeleteCommand="WorkflowParametersGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView DataKeyNames="ParameterName" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ParameterDataType" UniqueName="ParameterDataType" HeaderText="Data Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="AllowFixedValue" UniqueName="AllowFixedValue" HeaderText="Allow Fixed Value" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Required" UniqueName="Required" HeaderText="Required" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this parameter?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            

                    <div class="PropertyPageSectionTitle">Add Workflow Parameter</div>
                    
                    <table cellpadding="0" cellspacing="0" style="margin: .25in;" border="0"><tr>
                        
                        <td style="padding-right: .125in; width: 50px;">Name:</td>
                    
                        <td style=""><Telerik:RadTextBox ID="WorkflowParameterName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>
                    
                        <td style="padding-left: .125in; padding-right: .125in; width: 50px; text-align: center;">Type:</td>
                        
                        <td style="width: 12%">
                        
                            <Telerik:RadComboBox ID="WorkflowParameterDataType" Width="100%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="String" />
                                
                                    <Telerik:RadComboBoxItem Value="1" Text="Workflow" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Work Queue" />
                                
                                    <Telerik:RadComboBoxItem Value="3" Text="Correspondence" />
                                    
                                    <Telerik:RadComboBoxItem Value="4" Text="Routing Rule" />
                                
                                    <Telerik:RadComboBoxItem Value="5" Text="Id" />
                                    
                                    <Telerik:RadComboBoxItem Value="6" Text="Date Time" />
                                
                                    <Telerik:RadComboBoxItem Value="7" Text="Integer" />
                                    
                                    <Telerik:RadComboBoxItem Value="8" Text="Decimal" />
                                
                                    <Telerik:RadComboBoxItem Value="9" Text="Entity Id" />
                                    
                                    <Telerik:RadComboBoxItem Value="10" Text="Member Id" />

                                </Items>
                            
                            </Telerik:RadComboBox>
                    
                        </td>
                                                
                        <td style="white-space: nowrap; padding-left: .125in;">
                        
                            <asp:CheckBox ID="WorkflowParameterAllowFixedValue" Text="Allow Fixed Value" runat="server" />
                        
                        </td>
                       
                        <td style="white-space: nowrap; padding-left: .125in;">
                        
                            <asp:CheckBox ID="WorkflowParameterRequired" Text="Required" Checked="true" runat="server" />
                        
                        </td>
                                    
                        <td style="padding-left: .25in; padding-right: .25in; text-align: right; width: 100px;"><asp:Button ID="ButtonAddWorkflowParameter" Text="Add Parameter" OnClick="ButtonAddUpdateWorkflowParameter_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
                              
                    </tr></table> 
                    
                </td>
                
                </tr></table>

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PagePermissions" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; min-height: 600px; overflow: hidden"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkflowPermissions.png" alt="Workflow Permissions" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Permissions for the Workflow</td>
                    
                    </tr></table>
                    
                    <div class="PropertyPageSectionTitle">Workflow Permissions</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkflowPermissionsGrid" Height="390" OnDeleteCommand="WorkflowPermissionsGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="WorkTeamId" HeaderText="WorkTeamId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="WorkTeamName" HeaderText="Work Team Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsGranted" HeaderText="Is Granted" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="80" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center" />
                                    
                                    </Telerik:GridCheckBoxColumn>
                                    
                                    <Telerik:GridCheckBoxColumn DataField="IsDenied" HeaderText="Is Denied" ReadOnly="false" Visible="true">
                                    
                                        <HeaderStyle Width="80" HorizontalAlign="Center" />
                                        
                                        <ItemStyle HorizontalAlign="Center"  />
                                    
                                    </Telerik:GridCheckBoxColumn>
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this permission?" UniqueName="DeletePermission" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            

                    <div class="PropertyPageSectionTitle">Add Workflow Permission</div>

                    <table cellpadding="0" cellspacing="0" style="margin: .25in;"><tr>
                        
                        <td style="white-space: nowrap; padding-right: .25in;">Work Team:</td>
                        
                        <td style=""><Telerik:RadComboBox ID="WorkflowPermissionTeamSelection" Width="100%" Sort="Ascending" runat="server"></Telerik:RadComboBox></td>
                        
                        <td style="width: .125in;">&nbsp</td>
                        
                        <td style="white-space: nowrap; padding-right: .25in;">Permission:</td>
                        
                        <td style="width: 100px;">
                        
                            <Telerik:RadComboBox ID="WorkflowPermissionTeamPermissionSelection" Width="100%" Sort="Ascending" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Text="Denied" Value="0" />
                                    
                                    <Telerik:RadComboBoxItem Text="Granted" Value="1" Selected="true" />
                                    
                                </Items>
                            
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td style="padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonAddWorkflowPermission" Text="Add Permission" OnClick="ButtonAddWorkflowPermission_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
           
                    </tr></table>
                    
                </td>
                
                </tr></table>        
            
            </Telerik:RadPageView>
              
            <Telerik:RadPageView ID="PageExtendedProperties" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkflowExtendedProperties.png" alt="Extended Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Extended Properties</td>
                    
                    </tr></table>
                                       
                                       
                    <div class="PropertyPageSectionTitle">Current Extended Properties</div>
                                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ExtendedPropertiesGrid" Height="390" AutoGenerateColumns="false" OnDeleteCommand="ExtendedPropertiesGrid_OnDeleteCommand" runat="server">
                    
                            <MasterTableView Width="99%" DataKeyNames="ExtendedPropertyName,ExtendedPropertyValue" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ExtendedPropertyName" UniqueName="ExtendedPropertyName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ExtendedPropertyValue" UniqueName="ExtendedPropertyValue" HeaderText="Value" ReadOnly="true" Visible="true" />
                                                                        
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Property?" UniqueName="DeleteProperty" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>     
                    
                    
                    <div class="PropertyPageSectionTitle">Add a Extended Properties</div>
                    
                    <table cellpadding="0" cellspacing="0" style="margin: .25in;" border="0"><tr>
                                                   
                        <td style="width: 50px; padding-right: .125in;">Name:</td>
                            
                        <td style="width: 50%;"><Telerik:RadTextBox ID="WorkQueueExtendedPropertyName" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                                       
                        <td style="width: 50px; padding-left: .125in; padding-right: .125in;">Value:</td>
                            
                        <td style="width: 50%;"><Telerik:RadTextBox ID="WorkQueueExtendedPropertyValue" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                           
                        <td style="padding-left: .25in; padding-right: .25in; text-align: right; width: 100px;"><asp:Button ID="ButtonAddExtendedProperty" Text="Add Property" OnClick="ButtonAddExtendedProperty_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></td>
                 
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
