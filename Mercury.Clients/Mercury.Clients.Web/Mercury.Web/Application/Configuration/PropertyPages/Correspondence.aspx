<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Correspondence.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Windows.Correspondence" %>

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

<form id="FormCorrespondence" runat="server">

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
            
            <Telerik:AjaxSetting AjaxControlID="CorrespondenceContentAddButtonNo">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentAddButton" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentAttachment" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentReportingServerSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentReportName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="CorrespondenceContentGrid">
            
                <UpdatedControls>
                    
                    <Telerik:AjaxUpdatedControl ControlID="CorrespondenceContentGrid" LoadingPanelID="AjaxLoadingPanel" />
                
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

            <Telerik:RadTab Text="Content"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>
                
        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Correspondence.png" alt="Correspondence Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Correspondence</td>
                    
                    </tr></table>

                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>                      
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceDescription" Width="98%" Rows="8" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                               
                        <div style="position: relative; float: left; width: 50%">
                            
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Version:</div>
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;"><Telerik:RadNumericTextBox ID="CorrespondenceVersion" Width="99%" MinValue="1" EmptyMessage="(required)" runat="server" /></div>
                                                 
                        </div>
                                         
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CorrespondenceEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CorrespondenceVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    
                    
                    <div class="PropertyPageSectionTitle">Associated Form</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 25%; padding: 4px;">Associated Form:</div>
                        
                        <div style="position: relative; float: left; width: 70%; padding: 4px;"><Telerik:RadComboBox ID="CorrespondenceFormSelection" Width="99%" runat="server" /></div>
                    
                    </div>
                    
                    
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CorrespondenceCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CorrespondenceModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CorrespondenceModifiedDate" DateFormat="MM/dd/yyyy" TabIndex="-1" Width="100%" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>               

            <Telerik:RadPageView ID="PageCorrespondenceContent" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; overflow: hidden;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Correspondence.png" alt="Correspondence Content" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Correspondence Content</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Content Options</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        
                        <div style="position: relative; float: left;  padding: 4px;"><asp:CheckBox ID="CorrespondenceStoreImage" runat="server" /></div>

                        <div style="position: relative; float: left; padding: 4px;">Store Generated Content as Images in the Database</div>

                        <div style="clear: both; margin-left: .25in">
                        
                            * Warning: This will have an impact on the data storage capacity of the database. Please consult with your Database Administrator

                            on the appropriate value.

                        </div>
                        
                    </div>

                    <div class="PropertyPageSectionTitle">Current Content</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="CorrespondenceContentGrid" Height="225" AutoGenerateColumns="false" OnDeleteCommand="CorrespondenceContentGrid_OnDeleteCommand" OnItemCommand="CorrespondenceContentGrid_OnItemCommand" runat="server">
                    
                            <MasterTableView Width="100%" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ContentSequence" HeaderText="Sequence" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ContentType" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ReportNameRaw" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridTemplateColumn DataField="AttachmentXps" HeaderText="XPS Available" Visible="true">
                            
                                        <ItemTemplate>
                                
                                            <%# 
                                                ((((Mercury.Server.Application.CorrespondenceContentType)Eval ("ContentType")) == Mercury.Server.Application.CorrespondenceContentType.Report) ? String.Empty :
                                                
                                                (String.IsNullOrWhiteSpace ((String) Eval ("AttachmentXpsBase64")) ? "False" : "True"))
                                                                                            
                                            %>
                                
                                        </ItemTemplate>
                            
                                    </Telerik:GridTemplateColumn>

                                    <Telerik:GridButtonColumn CommandName="MoveUp" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Up" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="MoveDown" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Down" />
                                                                                                                                                      
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Correspondence Content?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                    <div class="PropertyPageSectionTitle">Add New Content</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">Reporting Server:</div>
                        
                        <div style="position: relative; float: left; width: 30%"><Telerik:RadComboBox ID="CorrespondenceContentReportingServerSelection" AutoPostBack="true" Width="99%" runat="server"></Telerik:RadComboBox></div>
                   
                        <div style="position: relative; float: left; width: 05%">&nbsp</div>
                   
                        <div style="position: relative; float: left; width: 10%">Report Name:</div>
                        
                        <div style="position: relative; float: left; width: 40%"><Telerik:RadTextBox ID="CorrespondenceContentReportName" Width="99%" runat="server" MaxLength="310" /></div>
                                       
                    </div>

                    <div style="clear: both;height: 10px;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">(or) PDF Attachment:</div>
                        
                        <div style="position: relative; float: left; width: 70%"><Telerik:RadUpload ID="CorrespondenceContentAttachment" InputSize="80" InitialFileInputsCount="1" MaxFileInputsCount="1" ControlObjectsVisibility="None" runat="server" /></div>
                   
                    </div>

                    <div style="clear: both;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">with XPS Equivalent:</div>
                        
                        <div style="position: relative; float: left; width: 70%"><Telerik:RadUpload ID="CorrespondenceContentAttachmentXps" InputSize="80" InitialFileInputsCount="1" MaxFileInputsCount="1" ControlObjectsVisibility="None" runat="server" /></div>
                   
                        <div style="position: relative; float: left; width: 15%;"><asp:Button ID="CorrespondenceContentAddButton" Text="Add Content" OnClick="CorrespondenceContentAddButton_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                    </div>
                    
                    <div style="clear: both;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        * An XPS Document equivalent is only required when the Correspondence will be used with Automation. 

                    </div>

                </td></tr></table>

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
