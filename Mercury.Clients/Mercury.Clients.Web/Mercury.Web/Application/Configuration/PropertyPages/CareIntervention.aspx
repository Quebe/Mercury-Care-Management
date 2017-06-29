<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CareIntervention.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.CareIntervention" %>
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

<form id="FormCareIntervention" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="InterventionActivityType">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="InterventionActivityType" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeManualNameDescription" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityParameters" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ActivitiesGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddActivity" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateActivity" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityGoalSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityClinicalNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityCommonNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeManualNameDescription" LoadingPanelID="AjaxLoadingPanelWhiteout" />


                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityReoccurring" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityConstraintValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityConstraintQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />


                    <Telerik:AjaxUpdatedControl ControlID="ActivityParameters" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" />


                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            

            <Telerik:AjaxSetting AjaxControlID="ActivityTypeSelection">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityTypeManualNameDescription" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityParameters" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="ActivityActionSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ActivityParametersGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ActivityThresholdsGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ActivityThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddActivity">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddActivity" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>            
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateActivity">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateActivity" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
                
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
            
            <Telerik:RadTab Text="General" Selected="True"></Telerik:RadTab>    
            
            <Telerik:RadTab Text="Activities"></Telerik:RadTab>          

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CareIntervention.png" alt="Care Intervention Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Care Intervention</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CareInterventionEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CareInterventionVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CareInterventionCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CareInterventionModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CareInterventionModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="PageActivities" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; min-height: 600px; overflow: hidden"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CareIntervention.png" alt="Care Intervention Goal Activities" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Care Intervention Goal Activities</td>
                    
                    </tr></table>
                                               
                    <div class="PropertyPageSectionTitle">Activities</div>
                                        
                    <Telerik:RadSplitter ID="ActivitiesGridSplitter" Orientation="Horizontal" Width="100%" Height="210"  runat="server">

                        <Telerik:RadPane ID="ActivitiesGridSplitterPane" Scrolling="None" Width="100%" runat="server">

                        <Telerik:RadGrid ID="ActivitiesGrid" Width="100%" Height="100%"
                        
                            AutoGenerateColumns="false" AllowSorting="false" AllowPaging="false" AllowMultiRowSelection="false" 
                        
                            ShowGroupPanel="False"

                            OnNeedDataSource="ActivitiesGrid_OnNeedDataSource" 

                            OnDeleteCommand="ActivitiesGrid_OnDeleteCommand" OnItemCommand="ActivitiesGrid_OnItemCommand" runat="server">
                    
                            <MasterTableView TableLayout="Fixed">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ActivityIndex" HeaderText="GoalIndex" ReadOnly="true" Visible="false" />

                                    <Telerik:GridBoundColumn HeaderStyle-Width="85" ItemStyle-Width="85" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="CareInterventionActivityType" HeaderText="Type" ReadOnly="true" Visible="true" />
                                   
                                    <Telerik:GridBoundColumn DataField="Name" HeaderText="Name" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ClinicalNarrative" HeaderText="Clinical" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="CommonNarrative" HeaderText="Common" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="AnchorDescription" HeaderText="Anchor" ReadOnly="true" Visible="false" />

                                    <Telerik:GridBoundColumn DataField="ScheduleDescription" HeaderText="Schedule" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn HeaderStyle-Width="50" ItemStyle-Width="50" DataField="Reoccurring" HeaderText="Reoccurring" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ThresholdsDescription" HeaderText="Thresholds" ReadOnly="true" Visible="true" />

                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this activity?" UniqueName="DeleteActivity" Text="Delete" />
                                                                           
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this activity?" UniqueName="EditActivity" Text="Edit" />

                                </Columns>

                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                            
                            </ClientSettings>

                            <GroupingSettings ShowUnGroupButton="false" />
                        
                        </Telerik:RadGrid>
            
                        </Telerik:RadPane>

                    </Telerik:RadSplitter>
                             
                
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px; display: block;">Add New or Modify Existing Activity
                    
                        <a id="ActivityPropertiesTitle" href="javascript:ToggleActivityProperties();" style="color:#FFE0B3; text-decoration: underline;">(collapse)</a>
                    
                    </div>

                    <div id="ActivityPropertiesContent" style="margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                    
                        <div style=" border: solid 0px black">
                        
                            <Telerik:RadTabStrip ID="ActivitiesTabStrip" MultiPageID="ActivitiesMultiPage" SelectedIndex="0" runat="server">
                            
                                <Tabs>
                                
                                    <Telerik:RadTab Text="Activity"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Narrative"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Thresholds"></Telerik:RadTab>
                                    
                                </Tabs>
                                      
                            </Telerik:RadTabStrip>
                            
                            <div style="margin: 0px 0px 0px 0px; height: 210px; line-height: 150%; overflow: hidden;">
                         
                                <Telerik:RadMultiPage ID="ActivitiesMultiPage" SelectedIndex="0" runat="server">
                                
                                    <Telerik:RadPageView ID="PageActivityDefinition" runat="server">

                                        <div style="margin: 2px 10px 2px 10px; padding: 4px;">
                                        
                                            <table width="100%" cellpadding="4" cellspacing="0" border="0"><tr>
                                        
                                                <td style="width: 08%;">Type:</td>

                                                <td style="width: 12%;">

                                                    <Telerik:RadComboBox ID="CareInterventionActivityType" OnSelectedIndexChanged="CareInterventionActivityType_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">

                                                        <Items>
                                                        
                                                            <Telerik:RadComboBoxItem Text="Intervention" Value="0" Selected="true" />

                                                            <Telerik:RadComboBoxItem Text="Member Task" Value="1" />
                                                        
                                                        </Items>
                                                    
                                                    </Telerik:RadComboBox>
                                                
                                                </td>

                                                <td style="width: 08%;">Action:</td>

                                                <td style="width: 12%;">
                                            
                                                    <Telerik:RadComboBox ID="ActivityTypeSelection" OnSelectedIndexChanged="ActivityTypeSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" runat="server">

                                                        <Items>
                                                    
                                                            <Telerik:RadComboBoxItem Text="Manual" Value="0" />

                                                            <Telerik:RadComboBoxItem Text="Automation" Value="1" Enabled="false" />

                                                            <Telerik:RadComboBoxItem Text="Workflow" Value="2" />

                                                            <Telerik:RadComboBoxItem Text="Monitor" Value="3" Enabled="false" Visible="false" />
                                                    
                                                        </Items>
                                            
                                                    </Telerik:RadComboBox>
                                                
                                                </td>

                                                <td style="width: 08%; text-align: center;">Activity:</td>
                                            
                                                <td>
                                            
                                                    <Telerik:RadComboBox ID="ActivityActionSelection" OnSelectedIndexChanged="ActivityActionSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="100%" Enabled="false" runat="server"></Telerik:RadComboBox>
                                                
                                                </td>

                                            </tr></table>

                                            <div id="ActivityTypeManualNameDescription" style="" runat="server">

                                                <table width="100%" cellpadding="4" cellspacing="0" border="0">

                                                    <tr>
                                                
                                                        <td style="width: 08%;">Name:</td>
                    
                                                        <td style="width: 92%;"><Telerik:RadTextBox ID="ActivityName" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server" /></td>
                    
                                                    </tr>

                                                    <tr style="display: none;">
                                                
                                                        <td valign="top" style="width: 08%;">Description:</td>
                    
                                                        <td style="width: 92%;"><Telerik:RadTextBox ID="ActivityDescription" Width="100%" Rows="2" TextMode="MultiLine" EmptyMessage="(optional)" runat="server" /></td>

                                                    </tr>
                                            
                                                </table>
                    
                                            </div>                                                               
                                            
                                            <table width="100%" cellpadding="4" cellspacing="0" border="0"><tr>
                                        
                                                <td style="width: 08%">Schedule:</td>

                                                <td style="width: 15%">

                                                    <Telerik:RadComboBox ID="ActivityScheduleTypeSelection" 
                                                    
                                                        Width="99%" runat="server">
                                    
                                                        <Items>

                                                            <Telerik:RadComboBoxItem Value="0" Text="By Frequency" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Monthly"  />
                                        
                                                            <Telerik:RadComboBoxItem Value="2" Text="Quarterly"  />

                                                            <Telerik:RadComboBoxItem Value="3" Text="Yearly"  />

                                                            <Telerik:RadComboBoxItem Value="4" Text="Birth Month"  />

                                                            <Telerik:RadComboBoxItem Value="5" Text="By Calendar Month"  />

                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>
                                    
                                                </td>

                                                <td style="width: 05%">Value:</td>

                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityScheduleValue" Width="98%" NumberFormat-DecimalDigits="0" MinValue="0" Value="0" runat="server" /></td>

                                                <td style="width: 08%">
                                                
                                                    <Telerik:RadComboBox ID="ActivityScheduleQualifierSelection" Width="99%" runat="server">
                                    
                                                        <Items>
                                        
                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>

                                                </td>

                                                <td style="width: 15%"><asp:CheckBox ID="ActivityReoccurring" Text="Reoccurring" Checked="false" runat="server" /></td>

                                                <td style="width: 20%">Relative Constraint Date Value:</td>
                                                
                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityConstraintValue" Width="98%" NumberFormat-DecimalDigits="0" MaxValue="0" Value="0" runat="server" /></td>

                                                <td style="width: 08%">
                                                
                                                    <Telerik:RadComboBox ID="ActivityConstraintQualifierSelection" Width="99%" runat="server">
                                    
                                                        <Items>
                                        
                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                                        </Items>
                                    
                                                    </Telerik:RadComboBox>

                                                </td>

                                                <td>&nbsp</td>

                                            </tr></table>
                                   
                                            <div id="ActivityParameters" style="display: none;" runat="server">
                            
                                                <Telerik:RadGrid ID="ActivityParametersGrid" Height="110" 
                                            
                                                    OnNeedDataSource="ActivityParametersGrid_OnNeedDataSource" 
                                                
                                                    OnItemCommand="ActivityParametersGrid_OnItemCommand" 
                                                
                                                    OnItemDataBound="ActivityParametersGrid_OnItemDataBound" 
                                                
                                                    AutoGenerateColumns="false" runat="server">
                            
                                                    <MasterTableView TableLayout="Auto" EditMode="EditForms" DataKeyNames="ParameterName">
                                    
                                                        <Columns>
                                    
                                                            <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                            
                                                            <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />

                                                            <Telerik:GridEditCommandColumn HeaderText="Action"></Telerik:GridEditCommandColumn>                                                       

                                                        </Columns>
                                
                                                        <EditFormSettings EditFormType="Template">
                                        
                                                            <FormTemplate>
                                                
                                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                    
                                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                            
                                                                        <td style="width: 08%;">Value:&nbsp</td>
                                                        
                                                                        <td style="width: 25%"><Telerik:RadComboBox ID="ActivityParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                    
                                                                        <td style="width: 02%">&nbsp</td>

                                                                        <td style="width: 08%;">Fixed:&nbsp</td>
                                                       
                                                                        <td style="width: 15%"><Telerik:RadTextBox ID="ActivityParameterFixedValue" Width="99%" runat="server" /></td>

                                                                        <td style="width: 03%">&nbsp</td>
                                                        
                                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityParameterUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                        <td style="width: 02%">&nbsp</td>

                                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityParameterCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                    
                                                                        <td style="width: 01%">&nbsp</td>
                                                        
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

                                        </div>

                                    </Telerik:RadPageView>

                                    <Telerik:RadPageView ID="PageActivityNarrative" runat="server">
                                    
                                        <div style="width: 50%; padding: 4px;">Clinical Narrative:</div>
                                   
                                        <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ActivityClinicalNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="8000" EmptyMessage="(required)" runat="server" /></div>

                                        <div style="width: 50%; padding: 4px;">Common Narrative:</div>
                                    
                                        <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ActivityCommonNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="8000" EmptyMessage="(required)" runat="server" /></div>

                                    </Telerik:RadPageView>
                                    
                                    <Telerik:RadPageView ID="PageActivityThresholds" runat="server">
                                    
                                        <div style="margin: 0px 0px 10px 0px; padding: 0px; line-height: 150%;">
                                        
                                            <Telerik:RadGrid ID="ActivityThresholdsGrid" Height="205" 
                                            
                                                    OnItemCommand="ActivityThresholdsGrid_OnItemCommand" 
                                                
                                                    OnItemDataBound="ActivityThresholdsGrid_OnItemDataBound" 
                                                    
                                                    OnNeedDataSource="ActivityThresholdsGrid_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        
                                                <MasterTableView Name="Thresholds" TableLayout="Auto" DataKeyNames="ThresholdId" CommandItemDisplay="Bottom">
                                                
                                                    <Columns>
                                                    
                                                        <Telerik:GridBoundColumn DataField="ThresholdId" UniqueName="ThresholdId" HeaderText="Threshold" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeValue" UniqueName="RelativeValue" HeaderText="Relative" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeQualifier" UniqueName="RelativeQualifier" HeaderText="Qualifier" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />
                                                       
                                                        <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="false" />

                                                        <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                                        
                                                        <Telerik:GridButtonColumn ConfirmText="Are you sure you want to Delete this Threshold?" ButtonType="LinkButton" CommandName="Delete" Text="Delete" />
              
                                                    </Columns>
                                                    
                                                    <DetailTables>
                                                    
                                                        <Telerik:GridTableView  Name="ThresholdParameters" Width="99%" TableLayout="Auto" DataKeyNames="ThresholdId,ThresholdKey,ParameterName" EditMode="EditForms">
                                                        
                                                            <ParentTableRelation><Telerik:GridRelationFields MasterKeyField="ThresholdId" DetailKeyField="ThresholdId" /></ParentTableRelation>
                                                                 
                                                            <Columns>
                                                        
                                                                <Telerik:GridBoundColumn DataField="ThresholdKey" UniqueName="ThresholdKey" HeaderText="ThresholdKey" ReadOnly="true" Visible="false" />

                                                                <Telerik:GridBoundColumn DataField="ThresholdId" UniqueName="ThresholdId" HeaderText="Threshold" ReadOnly="true" Visible="false" />
                                                                
                                                                <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                                                
                                                                <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />
                                                               
                                                                <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                                                
                                                            </Columns>      
                                                            
                                                            <EditFormSettings EditFormType="Template">
                                                            
                                                                <FormTemplate>
                                                                    
                                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                                        
                                                                        <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                                                
                                                                            <td style="width: 08%;">Value:&nbsp</td>
                                                                            
                                                                            <td style="width: 25%"><Telerik:RadComboBox ID="ActivityThresholdParameterValue" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                                        
                                                                            <td style="width: 02%">&nbsp</td>
                                                                                                        
                                                                            <td style="width: 08%;">Fixed:&nbsp</td>
                                                                           
                                                                            <td style="width: 15%"><Telerik:RadTextBox ID="ActivityThresholdParameterFixedValue" Width="99%" runat="server" /></td>

                                                                            <td style="width: 03%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ActivityThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                            <td style="width: 02%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ActivityThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                        
                                                                            <td style="width: 01%">&nbsp</td>
                                                                            
                                                                        </tr></table>
                                                                        
                                                                    </td></tr></table>     
                                                                    
                                                                </FormTemplate>
                                                            
                                                            </EditFormSettings>
                                                                                                                
                                                        </Telerik:GridTableView>                                                    
                                                    
                                                    </DetailTables>
                                                
                                                    <CommandItemTemplate>
                                                    
                                                        <div style="padding: 4px;">
                                                        
                                                            <asp:LinkButton ID="ActivityThresholdsGridCommandAddThreshold" CommandName="InitInsert" Visible='<%# !ActivityThresholdsGrid.MasterTableView.IsItemInserted %>' runat="server">Add new Threshold</asp:LinkButton>
                                                        
                                                        </div>
                                                    
                                                    </CommandItemTemplate>
                                                    
                                                    <EditFormSettings EditFormType="Template">
                                                    
                                                        <FormTemplate>
                                                        
                                                            <table width="100%" style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; table-layout: fixed;"><tr>
                                                                                                                        
                                                                <td style="width: 06%;">Relative:&nbsp</td>

                                                                <td style="width: 05%"><Telerik:RadNumericTextBox ID="ActivityThresholdRelativeDateValue" Width="50" NumberFormat-DecimalDigits="0" runat="server" /></td>
                                                               
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 08%">
                                                                    
                                                                    <Telerik:RadComboBox ID="ActivityThresholdRelativeDateQualifier" Width="99%" runat="server">
                                                                        
                                                                        <Items>
                                                                            
                                                                            <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                                                            
                                                                        </Items>
                                                                        
                                                                    </Telerik:RadComboBox>
                                                                        
                                                                </td>
                                                                      
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 05%;">Status:&nbsp</td>
                                                                    
                                                                <td style="width: 15%">
                                                                    
                                                                    <Telerik:RadComboBox ID="ActivityThresholdStatusSelection" Width="99%" runat="server">
                                                                        
                                                                        <Items>
                                                                            
                                                                            <Telerik:RadComboBoxItem Value="0" Text="No Change" Selected="true" />
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="1" Text="Open"/>
                                                                                
                                                                            <Telerik:RadComboBoxItem Value="2" Text="Warning" />

                                                                            <Telerik:RadComboBoxItem Value="3" Text="Critical" />
                                                                                
                                                                        </Items>
                                                                        
                                                                    </Telerik:RadComboBox>
                                                                        
                                                                </td>
                                                                    
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="width: 08%"><asp:Button ID="ButtonActivityThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                <td style="width: 01%">&nbsp</td>

                                                                <td style="width: 08%"><asp:Button ID="ButtonActivityThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                
                                                                <td style="width: 02%">&nbsp</td>
                                                                    
                                                                <td style="">&nbsp</td>

                                                            </tr></table>
                                                                
                                                        </FormTemplate>
                                                                                                        
                                                    </EditFormSettings>
                                                                                            
                                                </MasterTableView>
                                                
                                                <ClientSettings>
                                                
                                                    <Selecting AllowRowSelect="true" />
                                                    
                                                    <Scrolling AllowScroll="true" />
                                                
                                                </ClientSettings>
                                            
                                            </Telerik:RadGrid>
                                        
                                        </div>            
                                        
                                    </Telerik:RadPageView>
                                    
                                </Telerik:RadMultiPage>
                                
                            </div>
                            
                        </div>
                                     
                        <div style="margin: 2px 10px 0px 10px; padding: 4px; line-height: 150%;">
                           
                            <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateActivity" Text="Update Selected" OnClick="ButtonAddUpdateActivity_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddActivity" Text="Add Activity" OnClick="ButtonAddUpdateActivity_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
                        </div>    
                    
                    </div>                        
                           
                </td></tr></table>
                    
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
 

 <Telerik:RadCodeBlock ID="ClientScriptBlock" runat="server">

<script type="text/javascript">

    function ToggleActivityProperties() {

        var link = document.getElementById("ActivityPropertiesTitle");

        var properties = document.getElementById("ActivityPropertiesContent");

        var splitter = $find("<%= ActivitiesGridSplitter.ClientID %>");


        if (properties.style.display == "") {

            properties.style.display = "none";

            link.innerText = "(expand)";

            splitter.set_height(486);

        }

        else {

            properties.style.display = "";

            link.innerText = "(collapsed)";

            splitter.set_height(210);

        }

    }
    
</script>
     
 </Telerik:RadCodeBlock>

 
</form>
    
</body>

</html>