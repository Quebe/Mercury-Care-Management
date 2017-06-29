﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProblemStatement.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.ProblemStatement" %>

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

<form id="FormProblemStatement" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="ProblemStatementDomainSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementDomainSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ProblemStatementClassSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />               
                
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
                
        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/ProblemStatement.png" alt="Problem Statement Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Problem Statement</td>
                    
                    </tr></table>
                    
                                       
                    <div class="PropertyPageSectionTitle">Classification</div>
                                       
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Domain:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadComboBox ID="ProblemStatementDomainSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" 
                        
                            OnSelectedIndexChanged="ProblemStatementDomainSelection_OnSelectedIndexChanged"
                                                      
                            runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Class:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadComboBox ID="ProblemStatementClassSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" 
                        
                            runat="server" /></div>

                    </div>

                    
                    <div class="PropertyPageSectionTitle">Problem and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Problem (Name):</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                                           
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementDescription" Width="98%" Rows="3" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>
                        
                        <div style="position: relative; float: left; width: 100%; padding: 4px;">Defining Characteristics:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementDefiningCharacteristics" Width="98%" Rows="3" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Related Factors:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementRelatedFactors" Width="98%" Rows="3" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ProblemStatementEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ProblemStatementVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                                       
                        <div style="clear: both"></div>
                                 
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Default Care Plan:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;">
                        
                            <Telerik:RadComboBox ID="ProblemStatementDefaultCarePlanSelection" Width="100%" MarkFirstMatch="true" runat="server" />
                        
                        </div>                                

                    </div>
                
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ProblemStatementCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ProblemStatementModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ProblemStatementModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

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
