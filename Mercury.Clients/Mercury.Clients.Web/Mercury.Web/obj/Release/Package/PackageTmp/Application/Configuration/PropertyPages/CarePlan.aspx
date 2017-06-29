<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CarePlan.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.CarePlan" %>

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

<form id="FormCarePlan" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>

            <Telerik:AjaxSetting AjaxControlID="CarePlanGoalsGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalUpdate" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalAdd" LoadingPanelID="AjaxLoadingPanelWhiteout" />


                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalEnabled" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalTimeframeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCareMeasurementSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalClinicalNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCommonNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />                
                
                
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    

                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityGoalSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="CarePlanGoalUpdate">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalUpdate" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalAdd" LoadingPanelID="AjaxLoadingPanelWhiteout" />


                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalEnabled" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalTimeframeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCareMeasurementSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalClinicalNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCommonNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />                
                
                
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityGoalSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                
                </UpdatedControls>

            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="CarePlanGoalAdd">
            
                <UpdatedControls>

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalUpdate" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalAdd" LoadingPanelID="AjaxLoadingPanelWhiteout" />


                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalName" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalEnabled" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalTimeframeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalScheduleQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCareMeasurementSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalClinicalNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalCommonNarrative" LoadingPanelID="AjaxLoadingPanelWhiteout" />                
                
                
                    <Telerik:AjaxUpdatedControl ControlID="CarePlanGoalsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivitiesGridSplitter" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="ActivityGoalSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

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
            
            <Telerik:RadTab Text="Goals"></Telerik:RadTab>          

            <Telerik:RadTab Text="Interventions"></Telerik:RadTab>          

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CarePlan.png" alt="Care Plan Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Care Plan</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CarePlanEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="CarePlanVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CarePlanCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="CarePlanModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    

            <Telerik:RadPageView ID="PageGoals" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px; overflow: hidden"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CarePlan.png" alt="Care Plan Goals" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Goals of the Care Plan</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Current Goals</div>
                       
                    <div style="width: 100%; margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="CarePlanGoalsGrid" Height="200" AutoGenerateColumns="false" 
                        
                            OnDeleteCommand="CarePlanGoalsGrid_OnDeleteCommand"

                            OnItemCommand="CarePlanGoalsGrid_OnItemCommand"
                        
                            runat="server">
                        
                            <MasterTableView Width="99%" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn HeaderStyle-Width="85" ItemStyle-Width="85" DataField="Id" UniqueName="GoalId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Name" UniqueName="GoalName" HeaderText="Goal" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ClinicalNarrative" UniqueName="ClinicalNarrative" HeaderText="Clinical" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="CommonNarrative" UniqueName="CommonNarrative" HeaderText="Common" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="GoalTimeframeDescription" UniqueName="Timeframe" HeaderText="Timeframe" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="CareMeasurementName" HeaderText="Measurement" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn HeaderStyle-Width="60" ItemStyle-Width="60" DataField="Enabled" UniqueName="Enabled" HeaderText="Enabled" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this goal?" UniqueName="EditGoal" Text="Edit" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this goal?" UniqueName="DeleteGoal" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this goal?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
                                </Columns>
                                
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                            
                            </ClientSettings>
                       
                        </Telerik:RadGrid>
                        
                    </div>
                     
                                 
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px;">Add New or Modify Existing Goal</div>

                    
                    <div style="margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                                            
                    <table width="100%" style="padding: 4px; table-layout: auto;"><tr>
                                        
                        <td style="width: 75px;">Name: </td>
                                            
                        <td style="padding-right: 4px;"><Telerik:RadTextBox ID="CarePlanGoalName" Width="98%" MaxLength="60" EmptyMessage="(required)" runat="server" /></td>
                                                                   
                        <td style="width: 09%"><asp:CheckBox ID="CarePlanGoalEnabled" Text="Enabled" Checked="true" runat="server" /></td>

                    </tr></table>
                    
                    <table width="100%" style="padding: 4px;"><tr>
                                        
                        <td style="width: 75px;">Timeframe:</td>

                        <td style="width: 10%;">
                                                
                            <Telerik:RadComboBox ID="CarePlanGoalTimeframeSelection" Width="99%" runat="server">
                                    
                                <Items>
                                        
                                    <Telerik:RadComboBoxItem Value="0" Text="Short-term" Selected="true" />
                                            
                                    <Telerik:RadComboBoxItem Value="1" Text="Long-term" />
                                            
                                </Items>
                                    
                            </Telerik:RadComboBox>

                        </td>
                                  
                        <td style="width: 10%; text-align: right">Schedule Value:</td>

                        <td style="width: 05%"><Telerik:RadNumericTextBox ID="CarePlanGoalScheduleValue" Width="98%" NumberFormat-DecimalDigits="0" MinValue="0" Value="0" runat="server" /></td>

                        <td style="width: 08%;">
                                                
                            <Telerik:RadComboBox ID="CarePlanGoalScheduleQualifierSelection" Width="99%" runat="server">
                                    
                                <Items>
                                        
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                    <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                </Items>
                                    
                            </Telerik:RadComboBox>

                        </td>

                        
                        <td style="width: 14%; text-align: right">Measurement:</td>
                        
                        <td style="width: 45%;">
                                                
                            <Telerik:RadComboBox ID="CarePlanGoalCareMeasurementSelection" Width="99%" runat="server"></Telerik:RadComboBox>

                        </td>

                    </tr></table>
 
                    <div style="width: 50%; padding: 4px;">Clinical Narrative:</div>
                                   
                    <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanGoalClinicalNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="999" EmptyMessage="(required)" runat="server" /></div>

                    <div style="width: 50%; padding: 4px;">Common Narrative:</div>
                                    
                    <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="CarePlanGoalCommonNarrative" Width="99%" Rows="3" TextMode="MultiLine" MaxLength="999" EmptyMessage="(required)" runat="server" /></div>
        
                    <div style="margin: 2px 10px 0px 10px; padding: 4px; line-height: 150%;">
                           
                        <div style="position: relative; float: right"><asp:Button ID="CarePlanGoalUpdate" Text="Update Selected" OnClick="ButtonAddUpdateCarePlanGoal_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                        <div style="position: relative; float: right"><asp:Button ID="CarePlanGoalAdd" Text="Add Goal" OnClick="ButtonAddUpdateCarePlanGoal_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
                    </div>    

                    </div>

                </td>
                
                </tr></table>
            
            </Telerik:RadPageView>

            <Telerik:RadPageView ID="PageInterventions" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; min-height: 600px; overflow: hidden"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/CarePlan.png" alt="Care Plan Interventions" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Care Plan Interventions</td>
                    
                    </tr></table>
                                               
                    <div class="PropertyPageSectionTitle">Interventions</div>
                                        
                    <Telerik:RadSplitter ID="InterventionsGridSplitter" Orientation="Horizontal" Width="100%" Height="410"  runat="server">

                        <Telerik:RadPane ID="InterventionsGridSplitterPane" Scrolling="None" Width="100%" runat="server">

                        <Telerik:RadGrid ID="InterventionsGrid" Width="100%" Height="100%"
                        
                            AutoGenerateColumns="false" AllowSorting="false" AllowPaging="false" AllowMultiRowSelection="false" 
                        
                            ShowGroupPanel="False"

                            OnNeedDataSource="InterventionsGrid_OnNeedDataSource" 

                            OnDeleteCommand="InterventionsGrid_OnDeleteCommand" OnItemCommand="InterventionsGrid_OnItemCommand" runat="server">
                    
                            <MasterTableView TableLayout="Fixed">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="InterventionIndex" ReadOnly="true" Visible="false" />

                                    <Telerik:GridBoundColumn HeaderStyle-Width="85" ItemStyle-Width="85" DataField="Id" HeaderText="Id" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="Name" HeaderText="Name" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn HeaderStyle-Width="85" ItemStyle-Width="85" DataField="Inclusion" HeaderText="Inclusion" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="CareInterventionDescription" HeaderText="Description" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn HeaderStyle-Width="60" ItemStyle-Width="60" DataField="Enabled" UniqueName="Enabled" HeaderText="Enabled" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this intervention?" UniqueName="EditIntervention" Text="Edit" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this intervention?" UniqueName="DeleteIntervention" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn HeaderStyle-Width="50" ItemStyle-Width="50" CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this intervention?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
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
                             
                
                    <div class="PropertyPageSectionTitle" style="margin-top: 0px; display: block;">Add New or Modify Existing Intervention</div>

                    <div id="InterventionPropertiesContent" style="margin: 0px 0px 0px 0px; padding: 0px; line-height: 150%;">
                    
                        <div style=" border: solid 0px black">
                        
                            <table width="100%" style="padding: 4px;"><tr>
                                        
                                <td style="width: 85px;">Intervention:</td>
                        
                                <td style="">
                                                
                                    <Telerik:RadComboBox ID="CarePlanCareInterventionSelection" Width="99%" runat="server"></Telerik:RadComboBox>

                                </td>

                                <td style="width: 100px; white-space: nowrap">Plan Inclusion:</td>

                                <td style="width: 110px; text-align: right;">
                                                
                                    <Telerik:RadComboBox ID="CarePlanInteventionInclusionSelection" Width="99%" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadComboBoxItem Text="Required" Value="1" />

                                            <Telerik:RadComboBoxItem Text="Suggested" Value="2" />

                                            <Telerik:RadComboBoxItem Text="Optional" Value="3" />
                                        
                                        </Items>
                                    
                                    </Telerik:RadComboBox>

                                </td>
                                
                                <td align="center" style="width: 100px;"><asp:CheckBox ID="CarePlanInterventionEnabled" Text="Enabled" Checked="true" runat="server" /></td>

                            </tr></table>
                            
                        </div>
                                     
                        <div style="margin: 2px 10px 0px 10px; padding: 4px; line-height: 150%;">
                           
                            <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateIntervention" Text="Update Selected" OnClick="ButtonAddUpdateIntervention_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddIntervention" Text="Add Intervention" OnClick="ButtonAddUpdateIntervention_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
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

    function ToggleInterventionProperties() {

        var link = document.getElementById("InterventionPropertiesTitle");
        
        var properties = document.getElementById("InterventionPropertiesContent");

        var splitter = $find("<%= InterventionsGridSplitter.ClientID %>");


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