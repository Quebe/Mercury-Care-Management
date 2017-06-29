<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportingServer.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.ReportingServer" %>

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

<form id="FormReportingServer" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
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

            <Telerik:RadTab Text="Configuration"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/ReportingServer.png" alt="Reporting Server Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Reporting Server</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ReportingServerEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ReportingServerVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ReportingServerCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ReportingServerModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
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
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/ReportingServer.png" alt="Assembly Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Assembly Definition for the Reporting Server</td>
                    
                    </tr></table>
                                                       
                    <div class="PropertyPageSectionTitle">Assembly and Class Name</div>
                                         
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Path:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerAssemblyPath" Width="100%" MaxLength="255" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">File Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerAssemblyName" Width="100%" MaxLength="99" EmptyMessage="(required)" runat="server" /></div>

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Class:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerAssemblyClassName" Width="100%" MaxLength="99" EmptyMessage="(required)" runat="server" /></div>

                    </div>

                    <div style="clear: both;"></div>
                    
                </td>
                                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>
                 
            <Telerik:RadPageView ID="ConfigurationPage" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/ReportingServer.png" alt="Reporting Server Configuration Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Reporting Server Configuration Properties</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Server Configuration</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Server:</div>
                    
                        <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationServerName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 10%; padding: 4px; text-align: right;">Protocol:</div>
                    
                        <div style="position: relative; float: left; width: 10%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationBindingProtocol" Width="60" MaxLength="10" EmptyMessage="(required)" runat="server" /></div>
                        
                        <div style="position: relative; float: left; width: 05%; padding: 4px; text-align: right;">Port:</div>
                    
                        <div style="position: relative; float: left; width: 10%; padding: 4px;">

                            <Telerik:RadNumericTextBox ID="ReportingServerConfigurationPort" EmptyMessage="(required)" Width="55" MinValue="0" MaxValue="32000" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server" />
                        
                        </div>


                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Service Path:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationServicePath" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></div>

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Service Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationServiceName" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></div>
                    
                    </div>
                    
                                    
                    <div class="PropertyPageSectionTitle">Binding Configuration</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Binding Type:</div>
                    
                        <div style="position: relative; float: left; width: 35%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ReportingServerConfigurationBindingTypeSelection" Width="100%" runat="server">
                            
                                <Items>

                                    <Telerik:RadComboBoxItem Text="Basic HTTP Binding" Value="0" Selected="true" />
                                
                                    <Telerik:RadComboBoxItem Text="Net TCP Binding" Value="2" />

                                    <Telerik:RadComboBoxItem Text="WS HTTP Binding" Value="1" />

                                    <Telerik:RadComboBoxItem Text="WS HTTP Anonymous Binding" Value="3" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                        
                        </div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px; text-align: right;">Timeout (minutes):</div>
                    
                        <div style="position: relative; float: left; width: 10%; padding: 4px;">
                        
                            <Telerik:RadNumericTextBox ID="ReportingServerConfigurationBindingTimeout" EmptyMessage="(required)" Width="55" MinValue="0" MaxValue="360" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server" />
                        
                        </div>

                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Security Mode:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ReportingServerConfigurationBindingSecurityMode" Width="100%" runat="server">
                            
                                <Items>

                                    <Telerik:RadComboBoxItem Text="None" Value="0" Selected="true" />
                                
                                    <Telerik:RadComboBoxItem Text="Transport" Value="1" />

                                    <Telerik:RadComboBoxItem Text="Message" Value="2" />

                                    <Telerik:RadComboBoxItem Text="Transport with Message Credential" Value="3" />

                                    <Telerik:RadComboBoxItem Text="Transport Credential Only" Value="4" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                        
                        </div>

                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Transport Credential:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ReportingServerConfigurationBindingTransportCredential" Width="100%" runat="server">
                            
                                <Items>

                                    <Telerik:RadComboBoxItem Text="None" Value="0" Selected="true" />
                                
                                    <Telerik:RadComboBoxItem Text="Basic" Value="1" />

                                    <Telerik:RadComboBoxItem Text="Digest" Value="2" />

                                    <Telerik:RadComboBoxItem Text="Ntlm" Value="3" />

                                    <Telerik:RadComboBoxItem Text="Windows" Value="4" />

                                    <Telerik:RadComboBoxItem Text="Certificate" Value="5" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>

                        </div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Message Credential:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ReportingServerConfigurationBindingMessageCredential" Width="100%" runat="server">
                            
                                <Items>

                                    <Telerik:RadComboBoxItem Text="None" Value="0" Selected="true" />
                                
                                    <Telerik:RadComboBoxItem Text="Windows" Value="1" />

                                    <Telerik:RadComboBoxItem Text="UserName" Value="2" />

                                    <Telerik:RadComboBoxItem Text="Certificate" Value="3" />

                                    <Telerik:RadComboBoxItem Text="IssuedToken" Value="4" />

                                </Items>
                            
                            </Telerik:RadComboBox>

                        </div>

                    </div>
                    
                
                    <div class="PropertyPageSectionTitle">Client Credentials</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Domain:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationClientCredentialsDomain" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></div>

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">User Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationClientCredentialsUserName" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Password:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="ReportingServerConfigurationClientCredentialsPassword" Width="100%" MaxLength="60" EmptyMessage="(optional)" TextMode="Password" runat="server" /></div>
                        

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Impersonation:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ReportingServerConfigurationClientCredentialsImpersonation" Width="100%" runat="server">
                            
                                <Items>

                                    <Telerik:RadComboBoxItem Text="None" Value="0" Selected="true" />
                                
                                    <Telerik:RadComboBoxItem Text="Anonymous" Value="1" />

                                    <Telerik:RadComboBoxItem Text="Identification" Value="2" />

                                    <Telerik:RadComboBoxItem Text="Impersonation" Value="3" />

                                    <Telerik:RadComboBoxItem Text="Delegation" Value="4" />

                                </Items>
                            
                            </Telerik:RadComboBox>

                        </div>

                    </div>
                    
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>  
            
            <Telerik:RadPageView ID="PageExtendedProperties" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CorrespondenceExtendedProperties.png" alt="Extended Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Extended Properties</td>
                    
                    </tr></table>
                                       
                                       
                    <div class="PropertyPageSectionTitle">Current Extended Properties</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ExtendedPropertiesGrid" Height="390" AutoGenerateColumns="false" OnDeleteCommand="ExtendedPropertiesGrid_OnDeleteCommand" runat="server">
                    
                            <MasterTableView Width="100%" DataKeyNames="ExtendedPropertyName,ExtendedPropertyValue" TableLayout="Auto">
                            
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
                            
                        <td style="width: 50%;"><Telerik:RadTextBox ID="CorrespondenceExtendedPropertyName" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                                       
                        <td style="width: 50px; padding-left: .125in; padding-right: .125in;">Value:</td>
                            
                        <td style="width: 50%;"><Telerik:RadTextBox ID="CorrespondenceExtendedPropertyValue" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                           
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

        <td style="width: 80px;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Height="24" OnClick="ButtonOk_OnClick" runat="Server" /></td>    
    
        <td style="width: .125in;">&nbsp;</td>

        <td style="width: 80px;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Height="24" OnClick="ButtonCancel_OnClick" runat="Server" /></td>
        
        <td style="width: .125in;">&nbsp;</td>

        <td style="width: 80px;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Height="24" OnClick="ButtonApply_OnClick" runat="Server" /></td>

    </tr></table>
            
            
</div>        
 
 
</form>
    
</body>

</html>
