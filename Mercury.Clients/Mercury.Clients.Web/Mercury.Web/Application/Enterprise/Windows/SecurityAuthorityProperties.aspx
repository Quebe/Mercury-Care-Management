<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityAuthorityProperties.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Windows.SecurityAuthorityProperties" %>

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
       
    </style>

</head>

<body>

<form id="FormSecurityAuthorityProperties" runat="server">

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

                <Telerik:RadTab Text="Provider Configuration"></Telerik:RadTab>

                <Telerik:RadTab Text="Create and Modified"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/SecurityServer.png" alt="Security Authority Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Security Authority</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Name and Description</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px; overflow: hidden;">Name:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px; overflow: hidden;"><Telerik:RadTextBox ID="SecurityAuthorityName" Width="300" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                            
                        </div>
                    
                        <div style="overflow: hidden;">
                    
                            <div style="position: relative; float: left; width: 25%; padding: 4px; overflow: hidden;">Type:</div>
                   
                            <div style="position: relative; float: left; width: 70%; padding: 4px; overflow: hidden;">
                        
                                <Telerik:RadComboBox ID="SecurityAuthorityType" runat="server">
                            
                                    <Items> 

                                        <Telerik:RadComboBoxItem Value="Windows Integrated" Text="Windows Integrated" runat="server" />
                                
                                        <Telerik:RadComboBoxItem Value="Active Directory" Text="Active Directory" Selected="true" runat="server" />
                                    
                                        <Telerik:RadComboBoxItem Value="Custom" Text="Custom" runat="server" />
                                    
                                    </Items>
                            
                                </Telerik:RadComboBox>
                                
                            </div>
                        
                        </div>
                    
                        <div style="overflow: hidden;">
                    
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Protocol:</div>
                   
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityProtocol" Width="300" MaxLength="30" EmptyMessage="" runat="server" /></div>

                        </div>

                        <div style="overflow: hidden;">

                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Server Name:</div>
                   
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityServerName" Width="300" MaxLength="60" EmptyMessage="" runat="server" /></div>

                        </div>

                        <div style="overflow: hidden;">

                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Domain:</div>
                       
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityDomain" Width="300" MaxLength="60" EmptyMessage="" runat="server" /></div>

                        </div>

                        <div style="overflow: hidden;">

                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Member Context:</div>
                       
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityMemberContext" Width="300" MaxLength="120" EmptyMessage="" runat="server" /></div>

                        </div>

                        <div style="overflow: hidden;">

                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Provider Context:</div>
                       
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityProviderContext" Width="300" MaxLength="120" EmptyMessage="" runat="server" /></div>

                        </div>

                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Associate Context:</div>
                       
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAssociateContext" Width="300" MaxLength="120" EmptyMessage="" runat="server" /></div>

                        </div>

                    </div>
                    

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Agent</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">                    

                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Agent Name:</div>
                   
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAgentName" Width="300" MaxLength="60" EmptyMessage="" runat="server" /></div>
                            
                        </div>

                        <div style="overflow: hidden;">
                                                
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Agent Password:</div>
                   
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAgentPassword" Width="300" MaxLength="40" TextMode="Password" runat="server" /></div>
                            
                        </div>

                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Agent Confirm:</div>
                       
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAgentConfirmPassword" Width="300" MaxLength="40" TextMode="Password" runat="server" /></div>
                            
                        </div>                           

                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>    

            <Telerik:RadPageView ID="ProviderConfiguration" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/SecurityServer.png" alt="Security Provider Configuration" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Security Provider Configuration</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Provider Configuration</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Assembly Path:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAssemblyPath" MaxLength="120" EmptyMessage="" Width="280" runat="server" /></div>
                    
                        </div>
                        
                        <div style="overflow: hidden;">
                    
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Assembly Name:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityAssemblyName" MaxLength="60" EmptyMessage="" Width="280" runat="server" /></div>
                            
                        </div>
                        
                        <div style="overflow: hidden;">
                        
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Provider Namespace:</div>
                        
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityProviderNamespace" MaxLength="120" EmptyMessage="" Width="280" runat="server" /></div>
                    
                        </div>
                        
                        <div style="overflow: hidden;">
                            
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Provider Class Name:</div>
                        
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityProviderClassName" MaxLength="60" EmptyMessage="" Width="280" runat="server" /></div>
                        
                        </div>
                        
                        <div style="overflow: hidden;">
                       
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Configuration Section:</div>
                    
                            <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityConfigurationSection" MaxLength="60" EmptyMessage="" Width="280" runat="server" /></div>
                            
                        </div>
                                       
                    </div>
                    
                </div>
                
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>
            

            <Telerik:RadPageView ID="CreateModified" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/SecurityServer.png" alt="Create and Modified By Information" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Create and Modified By Information</div>
                    
                    </div>
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created and Modified</b></div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityCreateAuthorityName" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityCreateAccountId" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityCreateAccountName" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="SecurityAuthorityCreateDate" DateFormat="MM/dd/yyyy" Width="100%" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityModifiedAuthorityName" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityModifiedAccountId" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SecurityAuthorityModifiedAccountName" Width="100%" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="SecurityAuthorityModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

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

