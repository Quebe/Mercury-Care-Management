<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CareMeasure.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.CareMeasure" %>

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

<form id="FormCareMeasure" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="CareMeasureDomainSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CareMeasureDomainSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CareMeasureClassSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />               
                
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

            <Telerik:RadTab Text="Components"></Telerik:RadTab>
                
        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CareMeasure.png" alt="Care Measure Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Care Measure </td>
                    
                    </tr></table>
                    
                                       
                    <div class="PropertyPageSectionTitle">Classification</div>
                                       
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Domain:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadComboBox ID="CareMeasureDomainSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" 
                        
                            OnSelectedIndexChanged="CareMeasureDomainSelection_OnSelectedIndexChanged"
                                                      
                            runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Class:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadComboBox ID="CareMeasureClassSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" 
                        
                            runat="server" /></div>

                    </div>

                    
                    <div class="PropertyPageSectionTitle">Care Measure and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Measure (Name):</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                                           
                    </div>
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div>Description:</div>
                    
                        <div><Telerik:RadTextBox ID="CareMeasureDescription" Width="98%" Rows="3" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>
                        
                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CareMeasureEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CareMeasureVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>                                               

                    </div>
                
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CareMeasureCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareMeasureModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CareMeasureModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    

            <Telerik:RadPageView ID="PageComponents" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px; overflow: hidden"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CareMeasure.png" alt="Care Measure Components" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Components of the Care Measure</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Current Components</div>
                       
                    <div style="width: 100%; margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="CareMeasureComponentsGrid" Height="380" AutoGenerateColumns="false" 
                        
                            OnDeleteCommand="CareMeasureComponentsGrid_OnDeleteCommand"

                            OnItemCommand="CareMeasureComponentsGrid_OnItemCommand"
                        
                            runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn HeaderStyle-Width="85" ItemStyle-Width="85" DataField="Id" UniqueName="GoalId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Name" HeaderText="Component" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="CareMeasureScaleName" HeaderText="Scale" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Tag" HeaderText="Tag" ReadOnly="true" Visible="true" />

                                    
                                    <Telerik:GridBoundColumn HeaderStyle-Width="60" ItemStyle-Width="60" DataField="Enabled" UniqueName="Enabled" HeaderText="Enabled" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this Component?" UniqueName="EditGoal" Text="Edit" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this Component?" UniqueName="DeleteGoal" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this Component?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
                                </Columns>
                                
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                            
                            </ClientSettings>
                       
                        </Telerik:RadGrid>
                        
                    </div>
                                 
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Add New or Modify Existing Component</div>
                    
                    <div style="margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                                            
                    <table cellspacing="2" cellpadding="2" style="padding: 4px; table-layout: auto;"><tr>
                                        
                        <td style="width: 05%;">Name: </td>
                                            
                        <td style="width: 40%; padding-right: 4px;"><Telerik:RadTextBox ID="CareMeasureComponentName" Width="100%" MaxLength="99" EmptyMessage="(required)" runat="server" /></td>
                                                                   
                        <td style="width: 05%;">Scale: </td>
                                            
                        <td style="width: 40%; padding-right: 4px;"><Telerik:RadComboBox ID="CareMeasureComponentScaleSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>

                        <td style="width: 10%"><asp:CheckBox ID="CareMeasureComponentEnabled" Text="Enabled" Checked="true" runat="server" /></td>
                        
                        </tr><tr>

                        <td style="">Tag:</td>
                                   
                        <td style=""><Telerik:RadTextBox ID="CareMeasureComponentTag" Width="100%" MaxLength="20" EmptyMessage="(optional)" runat="server" /></td>

                    </tr></table>
                    
                    <div style="margin: 2px 10px 0px 10px; padding: 4px; line-height: 150%;">
                           
                        <div style="position: relative; float: right"><asp:Button ID="CareMeasureComponentUpdate" Text="Update Selected" OnClick="ButtonAddUpdateCareMeasureComponent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                        <div style="position: relative; float: right"><asp:Button ID="CareMeasureComponentAdd" Text="Add Component" OnClick="ButtonAddUpdateCareMeasureComponent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
                    </div>    

                    </div>

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
