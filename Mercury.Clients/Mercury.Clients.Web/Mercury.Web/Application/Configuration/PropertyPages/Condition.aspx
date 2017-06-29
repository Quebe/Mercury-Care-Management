<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Condition.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.Condition" %>

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

<form id="FormCondition" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="ButtonAddCriteriaDemographic" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCriteriaDemographic" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaDemographicGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
           
            <Telerik:AjaxSetting AjaxControlID="ButtonAddCriteriaEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCriteriaEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEventGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ConditionEventsGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="ConditionEventsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddServiceEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddServiceEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateServiceEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateServiceEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
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
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" Transparency="0" InitialDelayTime="100" MinDisplayTime="0" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
</div>


<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

<div style="min-width: 800px;">

        <Telerik:RadTabStrip ID="PropertiesTabStrip" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="General"></Telerik:RadTab>

                <Telerik:RadTab Text="Criteria"></Telerik:RadTab>

                <Telerik:RadTab Text="Events" Visible="false"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>

                <Telerik:RadTab Text="Preview" Visible="false"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>
        
    <div style="height: 600px; overflow: hidden; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Condition.png" alt="Condition Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Condition </td>
                    
                    </tr></table>
                    

                    <div class="PropertyPageSectionTitle">Name and Description</div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <table cellpadding="0" cellspacing="0" width="100%">
                    
                            <tr style="height: 32px;">
                        
                                <td style="width: 20%;">Condition Class:</td>

                                <td><Telerik:RadComboBox ID="ConditionClassSelection" Width="100%" MaxLength="60" EmptyMessage="(required)" AllowCustomText="true" MarkFirstMatch="true" AutoPostBack="true" runat="server"></Telerik:RadComboBox></td>

                            </tr>
                            
                            <tr style="height: 32px;">
                        
                                <td style="width: 20%;">Condition Name:</td>

                                <td><Telerik:RadTextBox ID="ConditionName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>
                        
                            </tr>

                        </table>
                    
                        <div style="height: 32px; margin-top: 4px;">Description:</div>
                    
                        <div style=""><Telerik:RadTextBox ID="ConditionDescription" Width="98%" Rows="12" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>

                    <div style="margin: 0px 10px 4px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ConditionEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 60%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ConditionVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 60%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ConditionCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ConditionModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ConditionModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
        
            <Telerik:RadPageView ID="PageCriteria" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top" style="">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Condition.png" alt="Condition Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Selection Criteria of the Condition </td>
                    
                    </tr></table>

                    
                    <Telerik:RadTabStrip ID="CriteriaTabStrip" MultiPageID="CriteriaContent" SelectedIndex="0" runat="server">
                        
                        <Tabs>
                            
                            <Telerik:RadTab Text="Demographic"></Telerik:RadTab>

                            <Telerik:RadTab Text="Event"></Telerik:RadTab>

                        </Tabs>
                                  
                    </Telerik:RadTabStrip>
                        
                    <Telerik:RadMultiPage ID="CriteriaContent" SelectedIndex="0" runat="server">
                                                    
                        <Telerik:RadPageView ID="PageCriteriaDemographic" runat="server">
                                                            
                            <Telerik:RadGrid ID="CriteriaDemographicGrid" Height="380" OnDeleteCommand="CriteriaDemographicGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                                
                                <MasterTableView TableLayout="Auto">
                                    
                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="CriteriaId" UniqueName="CriteriaId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="Gender" UniqueName="Gender" HeaderText="Gender" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="AgeMinimum" UniqueName="AgeMinimum" HeaderText="Min" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="AgeMaximum" UniqueName="AgeMaximum" HeaderText="Max" ReadOnly="true" Visible="true" />

                                        <Telerik:GridBoundColumn DataField="Ethnicity" UniqueName="Ethnicity" HeaderText="Ethnicity" ReadOnly="true" Visible="true" />

                                        <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this criteria?" UniqueName="DeleteCriteria" Text="Delete" />
                                            
                                    </Columns>
                                    
                                    
                                </MasterTableView>
                                    
                                <ClientSettings>
                                    
                                    <Selecting AllowRowSelect="true" />
                                        
                                    <Scrolling AllowScroll="true" />
                                    
                                </ClientSettings>
                                
                            </Telerik:RadGrid>
                                                                
                            <div id="AddCriteriaDemographicCriteria" style="margin-top: 0px;" runat="server">
                                  
                                <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Add Criteria</div>

                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                    <div style="position: relative; float: left; width: 20%">Gender:</div>
                                        
                                    <div style="position: relative; float: left; width: 15%">
                                        
                                        <Telerik:RadComboBox ID="CriteriaDemographicGender" Width="99%" runat="server">
                                            
                                            <Items>
                                                
                                                <Telerik:RadComboBoxItem Value="0" Text="Both" />
                                                    
                                                <Telerik:RadComboBoxItem Value="1" Text="Female" />
                                                    
                                                <Telerik:RadComboBoxItem Value="2" Text="Male" />
                                                
                                            </Items>
                                            
                                        </Telerik:RadComboBox>
                                    
                                    </div>
                                    
                                    <div style="position: relative; float: right; width: 60%">
                                        
                                        <div style="position: relative; float: left; width: 35%">Age Minimum:</div>
                                            
                                        <div style="position: relative; float: left; width: 15%">
                                            
                                            <Telerik:RadNumericTextBox ID="CriteriaDemographicAgeMinimum" Width="99%" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" runat="server" />

                                        </div>

                                        <div style="position: relative; float: left; padding-left: 8px; width: 25%">Maximum:</div>
                                            
                                        <div style="position: relative; float: left; width: 15%">
                                            
                                            <Telerik:RadNumericTextBox ID="CriteriaDemographicAgeMaximum" Width="99%" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" runat="server" />

                                        </div>
                                            
                                    </div>
                                   
                                </div>

                                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                    
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                    <div style="position: relative; float: left; width: 20%">Ethnicity:</div>
                                        
                                    <div style="position: relative; float: left; width: 55%"><Telerik:RadComboBox id="CriteriaDemographicEthnicitySelection" Width="98%" runat="server" /></div>
                                   
                                    <div style="position: relative; float: right"><asp:Button ID="ButtonAddCriteriaDemographic" Text="Add" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddCriteriaDemographic_OnClick" runat="server" /></div>
                                    
                                </div>

                            </div>                                
                                
                        </Telerik:RadPageView>
                            
                        <Telerik:RadPageView ID="PageCriteriaEvent" runat="server">
                            
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                <Telerik:RadGrid ID="CriteriaEventGrid" Height="380" OnDeleteCommand="CriteriaEventGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                                
                                <MasterTableView TableLayout="Auto">
                                    
                                    <Columns>
                                    
                                        <Telerik:GridBoundColumn DataField="CriteriaId" UniqueName="CriteriaId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="EventType" UniqueName="EventType" HeaderText="Type" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="ServiceId" UniqueName="ServiceId" HeaderText="Service Id" ReadOnly="true" Visible="true" />
                                            
                                        <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Service" ReadOnly="true" Visible="true" />

                                        <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this criteria?" UniqueName="DeleteCriteria" Text="Delete" />
                                                                                   
                                    </Columns>
                                    
                                    
                                </MasterTableView>
                                    
                                <ClientSettings>
                                    
                                    <Selecting AllowRowSelect="true" />
                                        
                                    <Scrolling AllowScroll="true" />
                                    
                                </ClientSettings>
                                
                            </Telerik:RadGrid>
                                
                            </div>
                                            
                            <div id="AddCriteriaEventDiv" style="clear: both" runat="server">
                                  
                                <div class="PropertyPageSectionTitle">Add Criteria</div>

                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                    <div style="position: relative; float: left; width: 25%">Event Type:</div>
                                        
                                    <div style="position: relative; float: left; width: 55%">
                                        
                                        <Telerik:RadComboBox ID="CriteriaEventType" Width="99%" runat="server">
                                            
                                            <Items>
                                                
                                                <Telerik:RadComboBoxItem Value="0" Text="Identifying Service" />
                                                    
                                                <Telerik:RadComboBoxItem Value="1" Text="Exclusion Service" />
                                                    
                                                <Telerik:RadComboBoxItem Value="2" Text="Terminating Service" />
                                                
                                            </Items>
                                            
                                        </Telerik:RadComboBox>
                                    
                                    </div>
                                   
                                </div>
                                    
                                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                    
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                    <div style="position: relative; float: left; width: 25%">Medical Service:</div>
                                        
                                    <div style="position: relative; float: left; width: 55%">
                                        
                                        <Telerik:RadComboBox ID="CriteriaEventMedicalServiceSelection" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                    </div>
                                   
                                    <div style="position: relative; float: right"><asp:Button ID="ButtonAddCriteriaEvent" Text="Add" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddCriteriaEvent_OnClick" runat="server" /></div>
                                        
                                </div>
                                
                            </div>                    
                                
                        </Telerik:RadPageView>
                                               
                    </Telerik:RadMultiPage>
                
                
            </td></tr></table>

            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageConditionEvents" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Condition.png" alt="Condition Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Selection Criteria of the Condition </td>
                    
                    </tr></table>
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Event Name and Action</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ConditionEventsGrid" Height="380" OnItemCommand="ConditionEventsGrid_OnItemCommand" OnNeedDataSource="ConditionEventsGrid_OnNeedDataSource" OnItemDataBound="ConditionEventsGrid_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView Name="ConditionEvent" TableLayout="Auto" DataKeyNames="EventName" CommandItemDisplay="None">
                            
                                <Columns>
                                
                                    <Telerik:GridBoundColumn DataField="EventName" UniqueName="EventName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                   
                                    <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />

                                    <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                    
                                </Columns>
                                
                                <DetailTables>
                                
                                    <Telerik:GridTableView  Name="ConditionEventParameters" Width="99%" TableLayout="Auto" DataKeyNames="EventName,ParameterName" EditMode="EditForms">
                                    
                                        <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="EventName" DetailKeyField="EventName" /></ParentTableRelation>
                                             
                                        <Columns>
                                    
                                            <Telerik:GridBoundColumn DataField="EventName" UniqueName="EventName" HeaderText="Name" ReadOnly="true" Visible="false" />
                                    
                                            <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />
                                           
                                            <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                            
                                        </Columns>      
                                        
                                        <EditFormSettings EditFormType="Template">
                                        
                                            <FormTemplate>
                                                
                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 95%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                    
                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                            
                                                        <td style="width: 10%;">Value:&nbsp</td>

                                                        <td style="width: 02%">&nbsp</td>
                                                        
                                                        <td style="width: 35%"><Telerik:RadComboBox ID="ConditionEventParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                    
                                                        <td style="width: 04%">&nbsp</td>
                                                                
                                                        <td style="width: 10%;">Fixed:&nbsp</td>

                                                        <td style="width: 02%">&nbsp</td>
                                                        
                                                        <td style="width: 35%"><Telerik:RadTextBox ID="ConditionEventParameterFixedValue" Width="99%" runat="server" /></td>

                                                    </tr></table>
                                                     
                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                    
                                                        <td style="width: 60%">&nbsp</td>

                                                        <td style="width: 15%"><asp:Button ID="ButtonConditionEventParameterUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                        <td style="width: 02%">&nbsp</td>

                                                        <td style="width: 15%"><asp:Button ID="ButtonConditionEventParameterCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                    
                                                        <td style="width: 04%">&nbsp</td>
                                                        
                                                    </tr></table>
                                                    
                                                </td></tr></table>     
                                                
                                            </FormTemplate>
                                        
                                        </EditFormSettings>
                                                                                            
                                    </Telerik:GridTableView>                                                    
                                
                                </DetailTables>
                                
                                <EditFormSettings EditFormType="Template">
                                
                                    <FormTemplate>
                                    
                                        <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 95%; table-layout: fixed; border: solid 1px black"><tr><td>
                                            
                                            <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                                          
                                                <td style="width: 10%">Action:</td>
                                                
                                                <td style="width: 55%"><Telerik:RadComboBox ID="ConditionEventActionSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                
                                                <td style="width: 02%">&nbsp</td>

                                                <td style="width: 14%"><asp:Button ID="ButtonConditionEventUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                <td style="width: 01%">&nbsp</td>

                                                <td style="width: 14%"><asp:Button ID="ButtonConditionEventCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                            
                                                <td style="width: 02%">&nbsp</td>
                                                
                                            </tr></table>
                                            
                                        </td></tr></table>                                                             
                                    
                                    </FormTemplate>
                                                                                    
                                </EditFormSettings>
                                                                        
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>

                    </td></tr></table>
                    
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageExtendedProperties" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Condition.png" alt="Condition Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Extended Properties</td>
                    
                    </tr></table>
                    
                    <div class="PropertyPageSectionTitle">Current Extended Properties</div>

                    <Telerik:RadGrid ID="ExtendedPropertiesGrid" Height="380" AutoGenerateColumns="false" OnDeleteCommand="ExtendedPropertiesGrid_OnDeleteCommand" runat="server">
                    
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
                    
                    
                    <div class="PropertyPageSectionTitle">Add Extended Property to Condition</div>

                    <table width="100%" cellpadding="0" cellspacing="0" style=""><tr>
                       
                        <td style="clear: both;">
                            
                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                            
                            <div style="position: relative; float: left; width: 10%">Name:</div>
                            
                            
                            <div style="position: relative; float: left; width: 35%"><Telerik:RadTextBox ID="ConditionExtendedPropertyName" Width="100%" runat="server"></Telerik:RadTextBox></div>
                            
                            <div style="position: relative; float: left; width: 05%">&nbsp</div>
                            
                            <div style="position: relative; float: left; width: 10%">Value:</div>
                            
                            <div style="position: relative; float: left; width: 35%"><Telerik:RadTextBox ID="ConditionExtendedPropertyValue" Width="100%" runat="server"></Telerik:RadTextBox></div>
                            
                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                            
                        </td>
                                                                          
                        <td style="clear: both; margin: 12px 10px 0px 10px; padding: 0px; line-height: 150%;">
                 
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddExtendedProperty" Text="Add Property" OnClick="ButtonAddExtendedProperty_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
           
                        </td>      
                   
                    </tr></table>    
                
                </td></tr></table>
                                    
            </Telerik:RadPageView>        

            <Telerik:RadPageView ID="PagePreview" runat="server">
            
                <div style="position: relative; float: left; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Condition.png" alt="Member Condition Preview" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Member Condition Preview</div>
                    
                    </div>

                </div>            
                        
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="ConditionPreviewGrid" Height="580" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView TableLayout="Auto">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="ServiceSetDefinitionId" UniqueName="ServiceSetDefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />
                            
                            </Columns>
                        
                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                    
                    </Telerik:RadGrid>
                    
                </div>
                
                <div style="height: 20px; padding: 0px 10px 0px 10px;">
                
                    <span style="float: left;">Warning: Retreiving membership may take time.</span>
                        
                    <span style="float: right;"><asp:Button ID="ButtonPreview" Text="Preview" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></span>

                </div>
        
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

<script type="text/javascript">

    function HelpPanel_OnHide(panelId) {

        helpPanel = document.getElementById(panelId + "Help");

        if (helpPanel != null) {

            helpPanel.style.display = "none";

            contentPanel = document.getElementById(panelId + "Content");

            if (contentPanel != null) {

                contentPanel.style.width = "99%";

            }

        }

        return;

    }

</script>
    
</body>

</html>

