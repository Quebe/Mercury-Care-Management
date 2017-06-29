<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvironmentProperties.aspx.cs" Inherits="Mercury.Web.Application.Enterprise.Windows.EnvironmentProperties" %>

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

<form id="FormEnvironmentProperties" runat="server">


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

                <Telerik:RadTab Text="Database Connection"></Telerik:RadTab>

                <Telerik:RadTab Text="Create and Modified"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="PageGeneral" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/ServerEnvironment.png" alt="Environment Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Environment</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Name and Confidentiality Statement</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 25%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentName" Width="98%" MaxLength="60" EmptyMessage="(required)" ReadOnly="true" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;">Confidentiality Statement:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentConfidentialityStatement" Width="98%" Rows="19" TextMode="MultiLine" ReadOnly="true" runat="server" /></div>

                    </div>
                    
                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>

            </Telerik:RadPageView>    

            <Telerik:RadPageView ID="PageDatabaseConnection" runat="server">
                                            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/ConfigurationServer.png" alt="Database Connection" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Database Connection Properties of the Environment</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>SQL Server and Database</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">SQL Server Name:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlServerName" Width="98%" MaxLength="60" EmptyMessage="(required)" ReadOnly="true" runat="server" /></div>
                            
                        </div>
                        
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Database Name:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlDatabaseName" Width="98%" MaxLength="60" EmptyMessage="(required)" ReadOnly="true" runat="server" /></div>
                            
                        </div>

                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Custom Attributes:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlCustomAttributes" Width="98%" MaxLength="60" ReadOnly="true" runat="server" /></div>
                            
                        </div>
                        
                    </div>


                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Log In</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 5%; padding: 4px;"><asp:CheckBox ID="EnvironmentSqlUseTrustedConnection" runat="server" /></div>
                        
                            <div style="position: relative; float: left; width: 75%; padding: 4px;">Use Trusted Connection (recommended)</div>
                                                   
                        </div>
                        
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">SQL User Name:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlUserName" Width="98%" MaxLength="60" ReadOnly="true" runat="server" /></div>
                            
                        </div>

                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">SQL Password:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlUserPassword" Width="98%" MaxLength="40" TextMode="Password" ReadOnly="true" runat="server" /></div>
                            
                        </div>

                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Confirm Password:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentSqlUserConfirmPassword" Width="98%" MaxLength="40" TextMode="Password" ReadOnly="true" runat="server" /></div>
                            
                        </div>

                    </div>


                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Connection Pooling</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 5%; padding: 4px;"><asp:CheckBox ID="EnvironmentSqlUseConnectionPooling" Enabled="false" runat="server" /></div>
                        
                            <div style="position: relative; float: left; width: 75%; padding: 4px;">Use Connection Pooling</div>
                            
                        </div>
                    
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Minimum Pool Size:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="EnvironmentSqlPoolSizeMinimum" Width="98%" Type="Number" MinValue="0" MaxLength="999" ReadOnly="true" NumberFormat-DecimalDigits="0" runat="server" /></div>
                            
                        </div>
                        
                        <div style="overflow: hidden;">                
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Maximum Pool Size:</div>
                        
                            <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadNumericTextBox ID="EnvironmentSqlPoolSizeMaximum" Width="98%" Type="Number" MinValue="0" MaxLength="999" ReadOnly="true" NumberFormat-DecimalDigits="0" runat="server" /></div>
                            
                        </div>

                    </div>


                </div>
                    
                <div style="position: relative; float: left; width:33%; height: 461px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
            
                </div>


            </Telerik:RadPageView>    


            <Telerik:RadPageView ID="CreateModified" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/ServerEnvironment.png" alt="Create and Modified By Information" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Create and Modified By Information</div>
                    
                    </div>
                    
                    

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created and Modified</b></div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="EnvironmentCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="EnvironmentModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="EnvironmentModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
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

        <span style="float: right;"><asp:Button ID="ButtonApply" Text="Apply" OnClick="ButtonApply_OnClick" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" OnClick="ButtonCancel_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonOk" Text="OK" OnClick="ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></span>

    </div>
                
</div>

</form>
    
</body>

</html>
