<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Printer.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.Printer" %>

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

<form id="FormPrinter" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
           <Telerik:AjaxSetting AjaxControlID="PrinterConfigurationServerQuery">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="PrinterConfigurationServerQuery" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationPrintQueuesAvailable" LoadingPanelID="AjaxLoadingPanel" />
                                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

           <Telerik:AjaxSetting AjaxControlID="ConfigurationPrintQueuesAvailable">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationPrintQueuesAvailable" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ConfigurationPrinterCapabilities" LoadingPanelID="AjaxLoadingPanel" />
                                    
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
            
            <Telerik:RadTab Text="Configuration"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Printer.png" alt="Printer Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Printer</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="PrinterName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="PrinterDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="PrinterEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="PrinterVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="PrinterCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PrinterModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="PrinterModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
                             
            <Telerik:RadPageView ID="ConfigurationPage" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Printer.png" alt="Printer Configuration Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Printer Configuration Properties</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Print Server Configuration</div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Print Server:</div>
                    
                        <div style="position: relative; float: left; width: 55%; padding: 4px;"><Telerik:RadTextBox ID="PrinterConfigurationServerName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;"><asp:Button ID="PrinterConfigurationServerQuery" Text="Query" OnClick="PrinterConfigurationServerQuery_OnClick" Width="73px" Height="24" runat="Server" /></div>
                    
                        
                        <div style="position: relative; float: left; width: 100%; padding: 4px;">Print Queues Available on the Print Server (select one):</div>
                                           
                    </div>
                                       
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;"> 

                        <Telerik:RadListBox ID="ConfigurationPrintQueuesAvailable" SelectionMode="Single" Sort="Ascending" AutoPostBack="true" Width="100%" Height="200" OnSelectedIndexChanged="ConfigurationPrintQueuesAvailable_OnSelectedIndexChanged" runat="server"></Telerik:RadListBox>
                                  
                    </div>
                            
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;">Printer Capabilities:</div>
                                           
                    </div>
                                       
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;"> 

                        <Telerik:RadListBox ID="ConfigurationPrinterCapabilities" Width="100%" Height="200" runat="server"></Telerik:RadListBox>
                                  
                    </div>

                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>  
            
            <Telerik:RadPageView ID="PageExtendedProperties" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Printer.png" alt="Extended Properties" /></td>
                        
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
