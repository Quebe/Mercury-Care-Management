<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkQueueView.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.WorkQueueView" %>

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

<form id="FormWorkQueueView" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
                      
            <Telerik:AjaxSetting AjaxControlID="ButtonAddCustomField">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCustomField" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFieldsGrid" LoadingPanelID="AjaxLoadingPanel" />
                                    
                    <Telerik:AjaxUpdatedControl ControlID="CustomFieldErrorLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    
                    <Telerik:AjaxUpdatedControl ControlID="FilteringFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFilterGrid" LoadingPanelID="AjaxLoadingPanel" />


                    <Telerik:AjaxUpdatedControl ControlID="SortingFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSortGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkQueueViewFieldsGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFieldsGrid" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCustomField" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    
                    <Telerik:AjaxUpdatedControl ControlID="FilteringFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFilterGrid" LoadingPanelID="AjaxLoadingPanel" />


                    <Telerik:AjaxUpdatedControl ControlID="SortingFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSortGrid" LoadingPanelID="AjaxLoadingPanel" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddFilter">
            
                <UpdatedControls>                
                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddFilter" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="FilteringFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFilterGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkQueueViewFilterGrid">
            
                <UpdatedControls>                
                    
                    <Telerik:AjaxUpdatedControl ControlID="FilteringFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddFilter" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewFilterGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>


            <Telerik:AjaxSetting AjaxControlID="ButtonAddSort">
            
                <UpdatedControls>                
                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddSort" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SortingFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSortGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="WorkQueueViewSortGrid">
            
                <UpdatedControls>                
                    
                    <Telerik:AjaxUpdatedControl ControlID="SortingFieldSelection" LoadingPanelID="AjaxLoadingPanel" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddSort" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="WorkQueueViewSortGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonPreview">
            
                <UpdatedControls>                
                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonPreview" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                                                        
                    <Telerik:AjaxUpdatedControl ControlID="PreviewGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
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
                
            <Telerik:RadTab Text="Custom Fields"></Telerik:RadTab>

            <Telerik:RadTab Text="Filtering"></Telerik:RadTab>

            <Telerik:RadTab Text="Sorting"></Telerik:RadTab>
                
            <Telerik:RadTab Text="Preview"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">

                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueueView.png" alt="Work Queue View Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Work Queue View</td>
                    
                    </tr></table>
                                       

                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewDescription" Width="98%" Rows="10" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>

                    </div>
                   
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkQueueViewEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="WorkQueueViewVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>                                           
                    
                                            
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueViewCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="WorkQueueViewModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="WorkQueueViewModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="PageCustomFields" runat="server">
                      
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueueView.png" alt="Work Queue View Custom Fields" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Custom Field Definitions</td>
                    
                    </tr></table>
                                                                   
                    
                    <div style="clear:both; margin: 2px 10px 10px 10px; line-height: 150%">
                    
                    This page allows you declare Extended Properties that are assigned to the Work Queue Item through a Workflow as Custom Fields to be used 
                    
                    in the view and available for filtering and sorting. You must add a Custom Field before it is available under the Filtering and Sorting Pages. Removing an existing
                    
                    Custom Field will remove it from the Filter or Sort.
                    
                    </div>
                    


                    <div class="PropertyPageSectionTitle">Currently Defined Custom Fields</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkQueueViewFieldsGrid" Height="300" AutoGenerateColumns="false" runat="server"
                        
                            OnDeleteCommand="WorkQueueViewFieldsGrid_OnDeleteCommand"
                        
                        >
                    
                            <MasterTableView Width="99%" DataKeyNames="DisplayName,PropertyName" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="DisplayName" HeaderText="Display Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="PropertyName" HeaderText="Property Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="DataType" HeaderText="Data Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="DefaultValue" HeaderText="Default Value" ReadOnly="true" Visible="true" />
                                    
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Custom Field?" UniqueName="DeleteField" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                      
                    <div class="PropertyPageSectionTitle">Define a New Custom Field</div>
                      
                    <table style="margin: 4px 14px 2px 14px; padding: 4px; line-height: 150%; overflow: hidden;">
                    
                        <tr>
                                                                    
                            <td style="width: 15%"></td>
                                               
                            <td style="width: 25%"></td>
                                                                    
                            <td style="width: 02%"></td>
                                                                    
                            <td style="width: 10%"></td>
                                                                    
                            <td style="width: 15%"></td>
                                                                    
                            <td style="width: 03%"></td>
                                                                    
                            <td style="width: 10%"></td>
                                                                    
                            <td style="width: 20%"></td>
                                                                                                                   
                        </tr>
                        
                        <tr>
                                                                                              
                            <td style="">Display Name:</td>
                            
                            <td style=""><Telerik:RadTextBox ID="CustomFieldDisplayName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>
                                                       
                            <td style="" colspan="3">&nbsp** used as the column header</td>
                                                     
                        </tr>
                        
                        <tr>
                                
                            <td style="">Property Name:</td>
                            
                            <td style=""><Telerik:RadTextBox ID="CustomFieldPropertyName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>
                            
                            <td style="">&nbsp</td>
                            
                            <td style="">Data Type:</td>
                            
                            <td style="">
                            
                                <Telerik:RadComboBox ID="CustomFieldDataTypeSelection" Width="100%" Sort="Ascending" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="String (VarChar)" Value="22" Selected="true" />
                                        
                                        <Telerik:RadComboBoxItem Text="Integer" Value="8" />
                                        
                                        <Telerik:RadComboBoxItem Text="Decimal" Value="5" />
                                        
                                        <Telerik:RadComboBoxItem Text="Date/Time" Value="4" />
                                        
                                        <Telerik:RadComboBoxItem Text="Boolean (Bit)" Value = "2" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                
                            </td>
                            
                            <td style="">&nbsp</td>
                            
                            <td style="">Default Value:</td>
                            
                            <td style=""><Telerik:RadTextBox ID="CustomFieldDefaultValue" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></td>
                            
                       </tr>
                       
                       <tr>
                       
                            <td style="" colspan="6"><asp:Label ID="CustomFieldErrorLabel" Text="" ForeColor="Red" runat="server"></asp:Label></td>
                           
                            <td style="">&nbsp</td>
                                                            
                            <td style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                     
                                <div style="position: relative; float: right"><asp:Button ID="ButtonAddCustomField" Text="Add Custom Field" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddCustomField_OnClick" runat="server" /></div>
               
                            </td>      
                            
                        </tr>                            
                                       
                    </table>
                    
                </td>
                
                </tr></table>
            
            </Telerik:RadPageView>

            <Telerik:RadPageView ID="PageFiltering" runat="server">
                      
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueueView.png" alt="Work Queue View Filter" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Filters</td>
                    
                    </tr></table>
                                                
                                                             
                    <div class="PropertyPageSectionTitle">Current Filters</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkQueueViewFilterGrid" Height="390" AutoGenerateColumns="false" runat="server"
                        
                            OnDeleteCommand="WorkQueueViewFilterGrid_OnDeleteCommand"
                            
                            OnItemCommand="WorkQueueViewFilterGrid_OnItemCommand"
                        
                        >
                    
                            <MasterTableView Width="100%" DataKeyNames="Sequence,FieldName,FilterOperator,FilterValue" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="Sequence" HeaderText="Sequence" ReadOnly="true" Visible="true" />
                            
                                    <Telerik:GridBoundColumn DataField="FieldName" HeaderText="Field Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="FilterOperator" HeaderText="Operator" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="FilterValue" HeaderText="Value" ReadOnly="true" Visible="true" />
                                                                       
                                    <Telerik:GridButtonColumn CommandName="MoveUp" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Up" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="MoveDown" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Down" />
                                                                                                                                                  
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Filter?" UniqueName="DeleteFilter" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                                            
                    <div class="PropertyPageSectionTitle">Add New Filtering Field</div>
                    
                    <table cellpadding="0" cellspacing="0" style="margin: .25in;"><tr>
                                
                        <td style="width: 80px;">Filter Field:</td>
                            
                        <td style=""><Telerik:RadComboBox ID="FilteringFieldSelection" Width="100%" Sort="Ascending" runat="server"></Telerik:RadComboBox></td>
                            
                        <td style="width: 60px; padding-left: .125in;">Operator:</td>
                            
                        <td style="width: 200px;">
                            
                            <Telerik:RadComboBox ID="FilteringOperatorSelection" Width="100%" Sort="Ascending" runat="server">
                                
                                <Items>
                                    
                                    <Telerik:RadComboBoxItem Text="Is Less Than (<)" Value="0" />
                                        
                                    <Telerik:RadComboBoxItem Text="Is Less Than or Equal To (<=)" Value="1" />

                                    <Telerik:RadComboBoxItem Text="Is Equal To (=)" Value="2" Selected="true" />

                                    <Telerik:RadComboBoxItem Text="Is Not Equal To (!=)" Value="3" />

                                    <Telerik:RadComboBoxItem Text="Is Greater Than or Equal To (>=)" Value="4" />

                                    <Telerik:RadComboBoxItem Text="Is Greater Than (>)" Value="5" />

                                    <Telerik:RadComboBoxItem Text="Starts With" Value="6" />

                                    <Telerik:RadComboBoxItem Text="Ends With" Value="7" />

                                    <Telerik:RadComboBoxItem Text="Contains" Value="8" />

                                    <Telerik:RadComboBoxItem Text="Is Contained In" Value="9" />
                                                                            
                                </Items>
                                
                            </Telerik:RadComboBox>
                                
                        </td>
                        
                        <td style="width: 60px; padding-left: .125in;">Value:</td>

                        <td style="width: 200px;"><Telerik:RadTextBox ID="FilteringValue" Width="100%" EmptyMessage="(required)" runat="server"></Telerik:RadTextBox></td>
                            
                        <td style="padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonAddFilter" Text="Add Filter" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" Width="73" OnClick="ButtonAddFilter_OnClick" runat="server" /></td>
               
                    </tr></table>
                    
                </td>
                
                </tr></table>

            </Telerik:RadPageView>
                        
            <Telerik:RadPageView ID="PageSortOrder" runat="server">
                      
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px; min-height: 600px; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/WorkQueueView.png" alt="Work Queue View Sort" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Sort Order Preference</td>
                    
                    </tr></table>
                                                
                                                             
                    <div class="PropertyPageSectionTitle">Current Sort Order Preference</div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="WorkQueueViewSortGrid" Height="390" AutoGenerateColumns="false" runat="server"
                        
                            OnDeleteCommand="WorkQueueViewSortGrid_OnDeleteCommand"
                            
                            OnItemCommand="WorkQueueViewSortGrid_OnItemCommand"
                        
                        >
                    
                            <MasterTableView Width="100%" DataKeyNames="Sequence,FieldName,SortDirection" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="Sequence" HeaderText="Sequence" ReadOnly="true" Visible="true" />
                            
                                    <Telerik:GridBoundColumn DataField="FieldName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="SortDirection" HeaderText="Sort Direction" ReadOnly="true" Visible="true" />
                                                                       
                                    <Telerik:GridButtonColumn CommandName="MoveUp" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Up" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="MoveDown" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Down" />
                                                                                                                                                  
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Sort?" UniqueName="DeleteSort" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                                            
                    <div class="PropertyPageSectionTitle">Add New Sorting Field</div>
                    
                    <table cellpadding="0" cellspacing="0" style="margin: .25in;"><tr>
                                
                        <td style="width: 100px; padding-right: .125in;">Sort Field:</td>
                            
                        <td style=""><Telerik:RadComboBox ID="SortingFieldSelection" Width="100%" Sort="Ascending" runat="server"></Telerik:RadComboBox></td>
                            
                        <td style="width: 100px; padding-right: .125in; padding-left: .125in;">Sort Direction:</td>
                            
                        <td style="width: 100px;">
                            
                            <Telerik:RadComboBox ID="SortingDirectionSelection" Width="100%" Sort="Ascending" runat="server">
                                
                                <Items>
                                    
                                    <Telerik:RadComboBoxItem Text="Ascending" Value="0" Selected="true" />
                                        
                                    <Telerik:RadComboBoxItem Text="Descending" Value="1" />
                                                                            
                                </Items>
                                
                            </Telerik:RadComboBox>
                                
                        </td>
                            
                        <td style="padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonAddSort" Text="Add Sort" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" Width="73" OnClick="ButtonAddSort_OnClick" runat="server" /></td>
               
                    </tr></table>
                    
                </td>
                
                </tr></table>

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PagePreview" runat="server">
                      
                <table cellpadding="0" cellspacing="0" style="width: 100%; overflow: hidden;"><tr><td valign="top">
                                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Document2Preview.png" alt="Work Queue View Preview" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Work Queue View Preview</td>
                    
                    </tr></table>
                    
                </td>
                
                </tr></table>
                
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="PreviewGrid" Height="480" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView Width="100%" TableLayout="Auto" AutoGenerateColumns="true">
                        
                            <Columns>
                            
                            </Columns>
                                
                        </MasterTableView>
                    
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                        
                    </Telerik:RadGrid>                        
                    
                </div>
            
                <table width="100%" style="height: 20px; padding: 0px 10px 0px 10px;"><tr>
                
                    <td style="width: 300px;">Warning: Retreiving Items may take time.</td>                   
                    
                    <td style="margin-left: 10px;">Work Queue:&nbsp</td>
                    
                    <td style=""><Telerik:RadComboBox ID="PreviewWorkQueueSelection" Width="400" runat="server"></Telerik:RadComboBox></td>

                    <td align="right" style="margin-left: 10px;"><asp:Button ID="ButtonPreview" Text="Preview" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonPreview_OnClick" runat="Server" /></td>

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
