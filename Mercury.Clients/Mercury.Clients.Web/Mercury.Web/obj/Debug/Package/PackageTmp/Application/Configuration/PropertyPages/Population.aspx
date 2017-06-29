<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Population.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Windows.Population" %>

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

<form id="FormPopulation" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddCriteriaEnrollment" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCriteriaEnrollment" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddCriteriaDemographic" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCriteriaDemographic" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaDemographicGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="CriteriaGeographicGrid" >
            
                <UpdatedControls>
            
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaGeographicGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>   
            
            <Telerik:AjaxSetting AjaxControlID="CriteriaGeographicStateSelection" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaGeographicStateSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaGeographicCitySelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaGeographicCountySelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>                       

            <Telerik:AjaxSetting AjaxControlID="ButtonAddCriteriaGeographic" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddCriteriaGeographic" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaGeographicGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
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
                    
            <Telerik:AjaxSetting AjaxControlID="CriteriaEnrollmentInsurerSelection" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentInsurerSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentProgramSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentBenefitPlanSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="CriteriaEnrollmentProgramSelection" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentProgramSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="CriteriaEnrollmentBenefitPlanSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="PopulationEventsGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="PopulationEventsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>   
            
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
            
            <Telerik:AjaxSetting AjaxControlID="ServiceEventsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsServiceSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsExclusionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                 
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsAnchorSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsAnchorDateValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsScheduleQualifier" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsReoccurring" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" />                    
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ServiceEventsThresholdsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ServiceEventsThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" /> 
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddTriggerEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddTriggerEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateTriggerEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateTriggerEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="TriggerEventsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsServiceSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                 
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsMetricSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsMetricMinimum" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsMetricMaximum" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsAuthorizedServiceSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsProblemStatementSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="TriggerEventsThresholdsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" /> 
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="TriggerEventsActionSelection" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" /> 
                    
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" /> 

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
                    
            <Telerik:AjaxSetting AjaxControlID="TriggerEventsActionParametersGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="TriggerEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" /> 

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>                             
            
            <Telerik:AjaxSetting AjaxControlID="ActivityEventsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleValue" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                                 
                    <Telerik:AjaxUpdatedControl ControlID="ActivityScheduleQualifierSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityAnchorDate" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityReoccurring" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityActionDateTypeSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />

                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ActivityEventsThresholdsGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsThresholdsGrid" LoadingPanelID="AjaxLoadingPanel" /> 
                    
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ActivityEventsActionSelection" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsActionSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" /> 
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" /> 

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
                    
            <Telerik:AjaxSetting AjaxControlID="ActivityEventsActionParametersGrid" >
            
                <UpdatedControls>
                                   
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsActionParametersGrid" LoadingPanelID="AjaxLoadingPanel" /> 

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>                       
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddActivityEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddActivityEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateActivityEvent" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateActivityEvent" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ActivityEventsGrid" LoadingPanelID="AjaxLoadingPanel" />
                
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


<div style="font-family: segoe ui, Arial, Helvetica, Sans-Serif; font-size: 10pt; line-height: 150%; min-width: 740px">

    <div style="position: relative; float: left; width: 99%; height: 700px; margin: 1px; border: solid 1px black; overflow: hidden;">
    
        <Telerik:RadTabStrip ID="PropertiesTabStrip" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="General"></Telerik:RadTab>

                <Telerik:RadTab Text="Criteria"></Telerik:RadTab>

                <Telerik:RadTab Text="Events"></Telerik:RadTab>

                <Telerik:RadTab Text="Service Events"></Telerik:RadTab>

                <Telerik:RadTab Text="Trigger Events"></Telerik:RadTab>

                <Telerik:RadTab Text="Activity Events"></Telerik:RadTab>
                
                <Telerik:RadTab Text="Extended Properties"></Telerik:RadTab>

                <Telerik:RadTab Text="Preview" Visible="false"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagement.png" alt="Population Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Population</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Name and Description</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="PopulationName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="PopulationDescription" Width="98%" Rows="12" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                        <div style="clear: both;"></div>

                        <div style="position: relative; float: left; width: 99%">
                        
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Population Type:</div>

                            <div style="position: relative; float: left; width: 65%; padding: 4px;"><Telerik:RadComboBox ID="PopulationTypeSelection" Width="99%" runat="server"></Telerik:RadComboBox></div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 99%">
                        
                            <div style="position: relative; float: left; width: 30%; padding: 4px;">Initial Anchor Date:</div>

                            <div style="position: relative; float: left; width: 65%; padding: 4px;">
                            
                                <Telerik:RadComboBox ID="PopulationInitialAnchorDate" Width="99%" runat="server">
                                
                                    <Items>
                                    
                                        <Telerik:RadComboBoxItem Text="Process Date (default, recommended)" Value="0" Selected="true" />
                                        
                                        <Telerik:RadComboBoxItem Text="Member Birth Date (caution)" Value="1" />
                                        
                                        <Telerik:RadComboBoxItem Text="Member Enrollment (caution)" Value="2" />
                                        
                                        <Telerik:RadComboBoxItem Text="Identifying Service Date (caution)" Value="3" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                                                        
                            </div>
                        
                        </div>
                        
                        <div style="clear: both;"></div>
                        
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="position: relative; float: left; padding: 4px;"><asp:CheckBox ID="PopulationAllowProspective" Text="Allow Prospective Members" runat="server" /></div>
                       
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="PopulationEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 60%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="PopulationVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 60%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created and Modified</b></div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="PopulationCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="PopulationModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="PopulationModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </div>

                <div style="position: relative; float: left; width:33%; height: 661px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
                
                </div>

            </Telerik:RadPageView>    
        
            <Telerik:RadPageView ID="PageCriteria" runat="server">
            
                <div style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementCriteria.png" alt="Population Criteria" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Population Criteria</div>
                    
                    </div>
                                    
                    <div style="clear:both"></div>
                                       
                    <div style="height: 618px; line-height: 150%; border: solid 1px #bbd7fa">
                    
                        <Telerik:RadTabStrip ID="CriteriaTabStrip" MultiPageID="CriteriaContent" SelectedIndex="0" runat="server">
                        
                            <Tabs>
                            
                                <Telerik:RadTab Text="Enrollment"></Telerik:RadTab>

                                <Telerik:RadTab Text="Demographic"></Telerik:RadTab>

                                <Telerik:RadTab Text="Geographic"></Telerik:RadTab>
                                
                                <Telerik:RadTab Text="Event"></Telerik:RadTab>

                            </Tabs>
                                  
                        </Telerik:RadTabStrip>
                        
                        <Telerik:RadMultiPage ID="CriteriaContent" SelectedIndex="0" runat="server">
                        
                            <Telerik:RadPageView ID="PageCriteriaEnrollment" runat="server">
                                    
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                                <Telerik:RadGrid ID="CriteriaEnrollmentGrid" Height="443" OnDeleteCommand="CriteriaEnrollmentGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                                
                                    <MasterTableView TableLayout="Auto">
                                    
                                        <Columns>
                                    
                                            <Telerik:GridBoundColumn DataField="CriteriaId" UniqueName="CriteriaId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="InsurerName" UniqueName="InsurerName" HeaderText="Insurer" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="ProgramName" UniqueName="ProgramName" HeaderText="Program" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="BenefitPlanName" UniqueName="BenefitPlanName" HeaderText="Benefit Plan" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this criteria?" UniqueName="DeleteCriteria" Text="Delete" />                                            
                                        
                                        </Columns>
                                    
                                    </MasterTableView>
                                    
                                    <ClientSettings>
                                    
                                        <Selecting AllowRowSelect="true" />
                                        
                                        <Scrolling AllowScroll="true" />
                                    
                                    </ClientSettings>
                                
                                </Telerik:RadGrid>
                                
                                </div>
                                                  
                                <div id="AddCriteriaEnrollmentDiv" style="clear: both" runat="server">
                                  
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

                                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                        <div style="position: relative; float: left; width: 25%">Insurer:</div>
                                        
                                        <div style="position: relative; float: left; width: 55%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaEnrollmentInsurerSelection" AutoPostBack="true" OnSelectedIndexChanged="CriteriaEnrollmentInsurerSelection_OnSelectedIndexChanged" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                    </div>
                                    
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                    
                                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                        <div style="position: relative; float: left; width: 25%">Program:</div>
                                        
                                        <div style="position: relative; float: left; width: 55%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaEnrollmentProgramSelection" AutoPostBack="true" OnSelectedIndexChanged="CriteriaEnrollmentProgramSelection_OnSelectedIndexChanged" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                    </div>
                                                                    
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                    
                                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                        <div style="position: relative; float: left; width: 25%">Benefit Plan:</div>
                                        
                                        <div style="position: relative; float: left; width: 55%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaEnrollmentBenefitPlanSelection" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                        <div style="position: relative; float: right"><asp:Button ID="ButtonAddCriteriaEnrollment" Text="Add" OnClick="ButtonAddCriteriaEnrollment_OnClick" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                                                                        
                                    </div>

                                </div>                                
                                
                            </Telerik:RadPageView>
                            
                            <Telerik:RadPageView ID="PageCriteriaDemographic" runat="server">
                            
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                    <Telerik:RadGrid ID="CriteriaDemographicGrid" Height="475" OnDeleteCommand="CriteriaDemographicGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                                
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
                                
                                </div>
                                
                                <div id="AddCriteriaDemographicCriteria" style="clear: both" runat="server">
                                  
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

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
                            
                            <Telerik:RadPageView ID="PageCriteriaGeographic" runat="server">
                            
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                    <Telerik:RadGrid ID="CriteriaGeographicGrid" Height="475" AutoGenerateColumns="false" OnDeleteCommand="CriteriaGeographicGrid_OnDeleteCommand" runat="server">
                                
                                    <MasterTableView TableLayout="Auto">
                                    
                                        <Columns>
                                    
                                            <Telerik:GridBoundColumn DataField="CriteriaId" UniqueName="CriteriaId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="State" UniqueName="State" HeaderText="State" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="City" UniqueName="City" HeaderText="City" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="County" UniqueName="County" HeaderText="County" ReadOnly="true" Visible="true" />

                                            <Telerik:GridBoundColumn DataField="ZipCode" UniqueName="ZipCode" HeaderText="Zip" ReadOnly="true" Visible="true" />

                                            <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this criteria?" UniqueName="DeleteCriteria" Text="Delete" />
                                            
                                        </Columns>
                                    
                                    </MasterTableView>
                                    
                                    <ClientSettings>
                                    
                                        <Selecting AllowRowSelect="true" />
                                        
                                        <Scrolling AllowScroll="true" />
                                    
                                    </ClientSettings>
                                
                                </Telerik:RadGrid>
                                
                                </div>
                                
                                <div id="AddCriteriaGeographicDiv" style="clear: both" runat="server">
                                  
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

                                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                        <div style="position: relative; float: left; width: 15%">State:</div>
                                        
                                        <div style="position: relative; float: left; width: 15%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaGeographicStateSelection" MarkFirstMatch="true" AutoPostBack="true" OnSelectedIndexChanged="CriteriaGeographicStateSelection_OnSelectedIndexChanged" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                        <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                   
                                        <div style="position: relative; float: left; width: 10%">City:</div>
                                        
                                        <div style="position: relative; float: left; width: 40%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaGeographicCitySelection" MarkFirstMatch="true" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                    </div>
                                    
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                    
                                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                    
                                        <div style="position: relative; float: left; width: 15%">County:</div>
                                        
                                        <div style="position: relative; float: left; width: 40%">
                                        
                                            <Telerik:RadComboBox ID="CriteriaGeographicCountySelection" MarkFirstMatch="true" Width="99%" runat="server"></Telerik:RadComboBox>
                                    
                                        </div>
                                   
                                        <div style="position: relative; float: left; width: 04%">&nbsp</div>
                                   
                                        <div style="position: relative; float: left; width: 05%">Zip:</div>
                                        
                                        <div style="position: relative; float: left; width: 10%"><Telerik:RadTextBox ID="CriteriaGeographicZipCode" Width="99%" runat="server" MaxLength="5" /></div>
                                        
                                        <div style="position: relative; float: right"><asp:Button ID="ButtonAddCriteriaGeographic" Text="Add" OnClick="ButtonAddCriteriaGeographic_OnClick" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                                    
                                    </div>
                                    
                                </div>
                                
                            </Telerik:RadPageView>
                                
                            <Telerik:RadPageView ID="PageCriteriaEvent" runat="server">
                            
                                <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                
                                    <Telerik:RadGrid ID="CriteriaEventGrid" Height="475" OnDeleteCommand="CriteriaEventGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                                
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
                                  
                                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

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
                
                    </div>
                    
                </div>                
                
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PagePopulationEvents" runat="server">
            
                <div style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementEvent.png" alt="Population Events" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Events for the Population</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Event Name and Action</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="PopulationEventsGrid" Height="560" OnItemCommand="PopulationEventsGrid_OnItemCommand" OnNeedDataSource="PopulationEventsGrid_OnNeedDataSource" OnItemDataBound="PopulationEventsGrid_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView Name="PopulationEvent" TableLayout="Auto" DataKeyNames="EventName" CommandItemDisplay="None">
                            
                                <Columns>
                                
                                    <Telerik:GridBoundColumn DataField="EventName" UniqueName="EventName" HeaderText="Name" ReadOnly="true" Visible="true" />
                                   
                                    <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />

                                    <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>
                                    
                                </Columns>
                                
                                <DetailTables>
                                
                                    <Telerik:GridTableView  Name="PopulationEventParameters" Width="99%" TableLayout="Auto" DataKeyNames="EventName,ParameterName" EditMode="EditForms">
                                    
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
                                                        
                                                        <td style="width: 35%"><Telerik:RadComboBox ID="PopulationEventParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                    
                                                        <td style="width: 04%">&nbsp</td>
                                                                
                                                        <td style="width: 10%;">Fixed:&nbsp</td>

                                                        <td style="width: 02%">&nbsp</td>
                                                        
                                                        <td style="width: 35%"><Telerik:RadTextBox ID="PopulationEventParameterFixedValue" Width="99%" runat="server" /></td>

                                                    </tr></table>
                                                     
                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                    
                                                        <td style="width: 60%">&nbsp</td>

                                                        <td style="width: 15%"><asp:Button ID="ButtonPopulationEventParameterUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                        <td style="width: 02%">&nbsp</td>

                                                        <td style="width: 15%"><asp:Button ID="ButtonPopulationEventParameterCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                    
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
                                                
                                                <td style="width: 55%"><Telerik:RadComboBox ID="PopulationEventActionSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                
                                                <td style="width: 02%">&nbsp</td>

                                                <td style="width: 14%"><asp:Button ID="ButtonPopulationEventUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                <td style="width: 01%">&nbsp</td>

                                                <td style="width: 14%"><asp:Button ID="ButtonPopulationEventCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                            
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
                    
                </div>
                    
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageServiceEvents" runat="server">
            
                <div id="PageServiceEventsContent" style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementServiceEvent.png" alt="Population Service Events" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Expected Service Events for the Population</div>
                    
                    </div>
            
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ServiceEventsGrid" Height="300" OnDeleteCommand="ServiceEventsGrid_OnDeleteCommand" OnItemCommand="ServiceEventsGrid_OnItemCommand" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ServiceEventId" UniqueName="ServiceEventId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ServiceId" UniqueName="ServiceId" HeaderText="ServiceId" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="ServiceName" UniqueName="ServiceName" HeaderText="Service" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="ExclusionServiceName" UniqueName="ExclusionServiceName" HeaderText="Exclusion" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Anchor" UniqueName="Anchor" HeaderText="Anchor" ReadOnly="true" Visible="true" />

                                    <Telerik:GridBoundColumn DataField="Schedule" UniqueName="Schedule" HeaderText="Schedule" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Thresholds" UniqueName="Thresholds" HeaderText="Thresholds" ReadOnly="true" Visible="true" />

                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this event?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this event?" UniqueName="EditEvent" Text="Edit" />

                                </Columns>
                            
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style=" border: solid 1px black">
                        
                            <Telerik:RadTabStrip ID="ServiceEventTabStrip" MultiPageID="ServiceEventMultiPage" SelectedIndex="0" runat="server">
                            
                                <Tabs>
                                
                                    <Telerik:RadTab Text="Service"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Thresholds"></Telerik:RadTab>
                                    
                                </Tabs>
                                      
                            </Telerik:RadTabStrip>
                            
                            <div style="margin: 0px 0px 0px 0px; height: 230px; line-height: 150%; overflow: hidden;">
                         
                                <Telerik:RadMultiPage ID="ServiceEventMultiPage" SelectedIndex="0" runat="server">
                                
                                    <Telerik:RadPageView ID="PageServiceEventService" runat="server">
                                   
                                        <div style="clear: both; margin: 4px 10px 4px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 20%">Service:</div>
                                            
                                            <div style="position: relative; float: left; width: 75%">
                                            
                                                <Telerik:RadComboBox ID="ServiceEventsServiceSelection" Width="99%" runat="server"></Telerik:RadComboBox>
                                        
                                            </div>
                                            
                                        </div>
                                        
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 20%">Exclusion:</div>
                                            
                                            <div style="position: relative; float: left; width: 75%">
                                            
                                                <Telerik:RadComboBox ID="ServiceEventsExclusionSelection" Width="99%" runat="server"></Telerik:RadComboBox>
                                        
                                            </div>
                                       
                                        </div>
                                        
                                        <div style="clear: both; margin: 4px 10px 4px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 20%">Anchor Date:</div>
                                    
                                            <div style="position: relative; float: left; width: 45%">
                                            
                                                <Telerik:RadComboBox ID="ServiceEventsAnchorSelection" OnClientSelectedIndexChanged="ServiceEventsAnchorSelection_OnClientSelectedIndexChanged" Width="99%" runat="server">
                                                
                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Value="0" Text="Population Anchor Date" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="1" Text="Previous Service Date, Population Anchor Date" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Previous Service Event Date (Sequence)" />

                                                        <Telerik:RadComboBoxItem Value="3" Text="Age By Years" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="4" Text="Age By Months" />

                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </div>
                                            
                                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                            
                                            <div style="position: relative; float: left; width: 10%; text-align: left">Value:&nbsp</div>
                                    
                                            <div style="position: relative; float: left; width: 15%"><Telerik:RadNumericTextBox ID="ServiceEventsAnchorDateValue" Enabled="false" MinValue="0" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Width="98%" runat="server" /></div>                     

                                        </div>

                                        <div style="clear: both; margin: 4px 10px 4px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 20%; text-align: left">Schedule:&nbsp</div>

                                            <div style="position: relative; float: left; width: 15%"><Telerik:RadNumericTextBox ID="ServiceEventsScheduleValue" EmptyMessage="(required)" MinValue="0" Width="90%" NumberFormat-DecimalDigits="0" runat="server" /></div>
                                       
                                            <div style="position: relative; float: left; width: 20%">
                                            
                                                <Telerik:RadComboBox ID="ServiceEventsScheduleQualifier" Width="99%" runat="server">
                                                
                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Value="0" Text="Days" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="1" Text="Months" Selected="true" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </div>
                                            
                                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                            
                                            <div style="position: relative; float: left; width: 05%"><asp:CheckBox ID="ServiceEventsReoccurring" runat="server" /></div>

                                            <div style="position: relative; float: left; width: 25%">Reoccurring Event</div>

                                        </div>
                                        
                                    </Telerik:RadPageView>
                                    
                                    <Telerik:RadPageView ID="PageServiceEventThresholds" runat="server">
                                    
                                        <div style="margin: 0px 0px 10px 0px; padding: 0px; line-height: 150%;">
                                        
                                            <Telerik:RadGrid ID="ServiceEventsThresholdsGrid" Height="228" OnItemCommand="ServiceEventsThresholdsGrid_OnItemCommand" OnItemDataBound="ServiceEventsThresholdsGrid_OnItemDataBound" OnNeedDataSource="ServiceEventsThresholdsGrid_OnNeedDataSource" AutoGenerateColumns="false" runat="server">
                                        
                                                <MasterTableView Name="Thresholds" TableLayout="Auto" DataKeyNames="ThresholdId" CommandItemDisplay="Bottom">
                                                
                                                    <Columns>
                                                    
                                                        <Telerik:GridBoundColumn DataField="ThresholdId" UniqueName="ThresholdId" HeaderText="Threshold" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeValue" UniqueName="RelativeValue" HeaderText="Relative" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="RelativeQualifier" UniqueName="RelativeQualifier" HeaderText="Qualifier" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="Status" UniqueName="Status" HeaderText="Status" ReadOnly="true" Visible="true" />
                                                       
                                                        <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />

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
                                                                            
                                                                            <td style="width: 25%"><Telerik:RadComboBox ID="ServiceEventsThresholdParameterValue" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                                        
                                                                            <td style="width: 02%">&nbsp</td>
                                                                                                        
                                                                            <td style="width: 08%;">Fixed:&nbsp</td>
                                                                           
                                                                            <td style="width: 15%"><Telerik:RadTextBox ID="ServiceEventsThresholdParameterFixedValue" Width="99%" runat="server" /></td>

                                                                            <td style="width: 03%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ButtonServiceEventThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                            <td style="width: 02%">&nbsp</td>

                                                                            <td style="width: 10%"><asp:Button ID="ButtonServiceEventThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                        
                                                                            <td style="width: 01%">&nbsp</td>
                                                                            
                                                                        </tr></table>
                                                                        
                                                                    </td></tr></table>     
                                                                    
                                                                </FormTemplate>
                                                            
                                                            </EditFormSettings>
                                                                                                                
                                                        </Telerik:GridTableView>                                                    
                                                    
                                                    </DetailTables>
                                                
                                                    <CommandItemTemplate>
                                                    
                                                        <div style="padding: 4px;">
                                                        
                                                            <asp:LinkButton ID="ServiceEventsThresholdsGridCommandAddThreshold" CommandName="InitInsert" Visible='<%# !ServiceEventsThresholdsGrid.MasterTableView.IsItemInserted %>' runat="server">Add new Threshold</asp:LinkButton>
                                                        
                                                        </div>
                                                    
                                                    </CommandItemTemplate>
                                                    
                                                    <EditFormSettings EditFormType="Template">
                                                    
                                                        <FormTemplate>
                                                        
                                                            <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 95%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                                
                                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                                        
                                                                    <td style="width: 10%;">Relative:&nbsp</td>

                                                                    <td style="width: 12%"><Telerik:RadNumericTextBox ID="ServiceEventsThresholdRelativeDateValue" Width="50" NumberFormat-DecimalDigits="0" runat="server" /></td>
                                                               
                                                                    <td style="width: 02%">&nbsp</td>
                                                                    
                                                                    <td style="width: 20%">
                                                                    
                                                                        <Telerik:RadComboBox ID="ServiceEventsThresholdRelativeDateQualifier" Width="99%" runat="server">
                                                                        
                                                                            <Items>
                                                                            
                                                                                <Telerik:RadComboBoxItem Value="0" Text="Days" />
                                                                                
                                                                                <Telerik:RadComboBoxItem Value="1" Text="Months" Selected="true" />
                                                                                
                                                                                <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                                                            
                                                                            </Items>
                                                                        
                                                                        </Telerik:RadComboBox>
                                                                        
                                                                    </td>
                                                                      
                                                                    <td style="width: 02%">&nbsp</td>
                                                                    
                                                                    <td style="width: 10%;">Status:&nbsp</td>
                                                                    
                                                                    <td style="width: 36%">
                                                                    
                                                                        <Telerik:RadComboBox ID="ServiceEventsThresholdStatusSelection" Width="99%" runat="server">
                                                                        
                                                                            <Items>
                                                                            
                                                                                <Telerik:RadComboBoxItem Value="0" Text="No Change" Selected="true" />
                                                                                
                                                                                <Telerik:RadComboBoxItem Value="1" Text="Open"/>
                                                                                
                                                                                <Telerik:RadComboBoxItem Value="2" Text="Open - Informational" />
                                                                            
                                                                                <Telerik:RadComboBoxItem Value="3" Text="Open - Warning" />

                                                                                <Telerik:RadComboBoxItem Value="4" Text="Open - Critical" />
                                                                                
                                                                            </Items>
                                                                        
                                                                        </Telerik:RadComboBox>
                                                                        
                                                                    </td>
                                                                    
                                                                    <td style="width: 04%">&nbsp</td>
                                                                    
                                                                    </tr><tr>
                                                                    
                                                                </tr></table>
                                                                 
                                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                       
                                                                    <td style="width: 10%;">Action:&nbsp</td>
                                                                    
                                                                    <td style="width: 55%"><Telerik:RadComboBox ID="ServiceEventsThresholdActionSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                                    
                                                                    <td style="width: 02%">&nbsp</td>
                                                                    
                                                                    <td style="width: 15%"><asp:Button ID="ButtonServiceEventThresholdUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                    <td style="width: 02%">&nbsp</td>

                                                                    <td style="width: 15%"><asp:Button ID="ButtonServiceEventThresholdCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                
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
                                        
                                    </Telerik:RadPageView>
                                    
                                </Telerik:RadMultiPage>
                                
                            </div>
                            
                        </div>
                        
                    </div>                        
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                           
                        <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateServiceEvent" Text="Update Selected" OnClick="ButtonAddUpdateServiceEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                        <div style="position: relative; float: right"><asp:Button ID="ButtonAddServiceEvent" Text="Add Service" OnClick="ButtonAddUpdateServiceEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
        
                    </div>    
                    
                </div>
                    
            </Telerik:RadPageView>
            
            <Telerik:RadPageView ID="PageTriggerEvents" runat="server">
            
                <div id="PageTriggerEventsContent" style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementTriggerEvent.png" alt="Population Trigger Events" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Trigger Events for the Population</div>
                    
                    </div>
                       
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="TriggerEventsGrid" Height="300" OnDeleteCommand="TriggerEventsGrid_OnDeleteCommand" OnItemCommand="TriggerEventsGrid_OnItemCommand" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="TriggerEventId" UniqueName="TriggerEventId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="EventType" UniqueName="EventType" HeaderText="Type" ReadOnly="true" Visible="false" />
                                    
                                    <Telerik:GridBoundColumn DataField="Trigger" UniqueName="Trigger" HeaderText="Trigger" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="ProblemStatement" UniqueName="ProblemStatement" HeaderText="Problem" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />

                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this trigger?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this trigger?" UniqueName="EditEvent" Text="Edit" />

                                </Columns>
                            
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style=" border: solid 1px black">
                                                      
                            <Telerik:RadTabStrip ID="TriggerEventsTabStrip" MultiPageID="TriggerEventsMultiPage" SelectedIndex="0" runat="server">
                            
                                <Tabs>
                                
                                    <Telerik:RadTab Text="Trigger"></Telerik:RadTab>

                                    <Telerik:RadTab Text="Action"></Telerik:RadTab>
                                    
                                </Tabs>
                                      
                            </Telerik:RadTabStrip>
                            
                            <div style="margin: 2px 0px 2px 0px; height: 230px; line-height: 150%; overflow: auto;">
                         
                                <Telerik:RadMultiPage ID="TriggerEventsMultiPage" SelectedIndex="0" runat="server">
                                
                                    <Telerik:RadPageView ID="PageTriggerEventsTrigger" runat="server">
                                   
                                        <div style="clear: both; margin: 4px 10px 4px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 25%">Type:</div>
                                            
                                            <div style="position: relative; float: left; width: 70%">
                                            
                                                <Telerik:RadComboBox ID="TriggerEventsTypeSelection" OnClientSelectedIndexChanged="TriggerEventsTypeSelection_OnClientSelectedIndexChanged" Width="99%" runat="server">
                                                
                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Value="0" Text="Service Event" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="1" Text="Metric"  />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Authorized Service" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                        
                                            </div>
                                            
                                        </div>
                                        
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div id="TriggerEventsTypeService">
                                        
                                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                            
                                                <div style="position: relative; float: left; width: 25%">Service:</div>
                                                
                                                <div style="position: relative; float: left; width: 70%"><Telerik:RadComboBox ID="TriggerEventsServiceSelection" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                           
                                            </div>
                                        
                                        </div>
                                        
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div id="TriggerEventsTypeMetric" style="display: none;">
                                        
                                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                            
                                                <div style="position: relative; float: left; width: 25%">Metric:</div>
                                        
                                                <div style="position: relative; float: left; width: 70%"><Telerik:RadComboBox ID="TriggerEventsMetricSelection" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                                
                                            </div>
                                            
                                            <div style="clear: both; margin: 4px 10px 4px 10px; padding: 1px; line-height: 150%"></div>
                                            
                                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                            
                                                <div style="position: relative; float: left; width: 25%;">Value Range:&nbsp</div>
                                                
                                                <div style="position: relative; float: left; width: 15%; text-align: center">Floor:&nbsp</div>
                                        
                                                <div style="position: relative; float: left; width: 20%"><Telerik:RadNumericTextBox ID="TriggerEventsMetricMinimum" Width="98%" runat="server" /></div>                     

                                                <div style="position: relative; float: left; width: 15%; text-align: center">Ceiling:&nbsp</div>

                                                <div style="position: relative; float: left; width: 20%"><Telerik:RadNumericTextBox ID="TriggerEventsMetricMaximum" Width="98%" runat="server" /></div>                     

                                            </div>
                                        
                                        </div>
                                        
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div id="TriggerEventsTypeAuthorizedService" style="display:none;">
                                        
                                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                            
                                                <div style="position: relative; float: left; width: 25%">Authorized Service:</div>
                                                
                                                <div style="position: relative; float: left; width: 70%"><Telerik:RadComboBox ID="TriggerEventsAuthorizedServiceSelection" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                           
                                            </div>
                                        
                                        </div>
                                        
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div id="TriggerEventsProblemStatement">
                                        
                                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                            
                                                <div style="position: relative; float: left; width: 25%">Problem Statement:</div>
                                                
                                                <div style="position: relative; float: left; width: 70%">
                                                
                                                    <Telerik:RadComboBox ID="TriggerEventsProblemStatementSelection" OnItemCreated="TriggerEventsProblemStatementSelection_OnItemCreated" Width="99%" AutoPostBack="true" runat="server">

                                                        <ItemTemplate>
                                                        
                                                            <Telerik:RadTreeView ID="TriggerEventsProblemStatementTreeView" OnClientNodeClicking="TriggerEventsProblemStatementTreeView_OnClientNodeClicking" runat="server"></Telerik:RadTreeView>
                                                        
                                                        </ItemTemplate>
                                                    
                                                    </Telerik:RadComboBox>
                                                    
                                                </div>
                                           
                                            </div>
                                        
                                        </div>
                                        
                                    </Telerik:RadPageView>
                                    
                                    <Telerik:RadPageView ID="PageTriggerEventsAction" runat="server">
                                    
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <div style="position: relative; float: left; width: 20%">Action:</div>
                                            
                                            <div style="position: relative; float: left; width: 70%"><Telerik:RadComboBox ID="TriggerEventsActionSelection" AutoPostBack="true" OnSelectedIndexChanged="TriggerEventsActionSelection_OnSelectedIndexChanged" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                            
                                         </div>
                                                         
                                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                                        
                                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                        
                                            <Telerik:RadGrid ID="TriggerEventsActionParametersGrid" Height="178" OnNeedDataSource="TriggerEventsActionParametersGrid_OnNeedDataSource" OnItemCommand="TriggerEventsActionParametersGrid_OnItemCommand" OnItemDataBound="TriggerEventsActionParametersGrid_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                                        
                                                <MasterTableView TableLayout="Auto" EditMode="EditForms" DataKeyNames="ParameterName">
                                                
                                                    <Columns>
                                                
                                                        <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                                        
                                                        <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />
    
                                                        <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>                                                       

                                                    </Columns>
                                            
                                                    <EditFormSettings EditFormType="Template">
                                                    
                                                        <FormTemplate>
                                                            
                                                            <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                                
                                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed;"><tr>
                                                                                                                        
                                                                    <td style="width: 08%;">Value:&nbsp</td>
                                                                    
                                                                    <td style="width: 25%"><Telerik:RadComboBox ID="TriggerEventParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                                
                                                                    <td style="width: 02%">&nbsp</td>
                                                                            
                                                                    <td style="width: 08%;">Fixed:&nbsp</td>

                                                                    <td style="width: 15%"><Telerik:RadTextBox ID="TriggerEventParameterFixedValue" Width="98%" runat="server" /></td>

                                                                    <td style="width: 03%">&nbsp</td>
                                                                    
                                                                    <td style="width: 10%"><asp:Button ID="ButtonTriggerEventUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                                    <td style="width: 01%">&nbsp</td>

                                                                    <td style="width: 10%"><asp:Button ID="ButtonTriggerEventCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                                
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
                                        
                                    </Telerik:RadPageView>
                                    
                                </Telerik:RadMultiPage>
                                
                            </div>
                            
                        </div>
                        
                    </div>                        
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
             
                        <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateTriggerEvent" Text="Update Selected" OnClick="ButtonAddUpdateTriggerEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                        <div style="position: relative; float: right"><asp:Button ID="ButtonAddTriggerEvent" Text="Add Trigger" OnClick="ButtonAddUpdateTriggerEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
       
                    </div>                                                          
                    
                </div>
                
            </Telerik:RadPageView>

            <Telerik:RadPageView ID="PageActivityEvents" runat="server">
            
                <div id="PageActivityEventsContent" style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementActivityEvent.png" alt="Population Activity Events" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Activity Events for the Population</div>
                    
                    </div>
                   
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%;"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ActivityEventsGrid" Height="300" OnItemCommand="ActivityEventsGrid_OnItemCommand" OnDeleteCommand="ActivityEventsGrid_OnDeleteCommand" AutoGenerateColumns="false" runat="server">
                    
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="ActivityEventId" UniqueName="ActivityEventId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Schedule" UniqueName="Schedule" HeaderText="Schedule" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Action" UniqueName="Action" HeaderText="Action" ReadOnly="true" Visible="true" />

                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this activity?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="Edit" ButtonType="LinkButton" ConfirmText="Are you sure you want to edit this activity?" UniqueName="EditEvent" Text="Edit" />

                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                    <div style="margin: 0px 10px 0px 10px; padding: 1px; line-height: 150%;"></div>
                    
                    <div style="margin: 2px 14px 2px 14px; height: 251px; padding: 4px; border: solid 1px black; line-height: 150%; overflow: hidden;">
                                          
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                            
                                <div style="position: relative; float: left; width: 15%">Schedule:</div>
                                
                                <div style="position: relative; float: left; width: 15%">
                                
                                    <Telerik:RadComboBox ID="ActivityScheduleTypeSelection" OnClientSelectedIndexChanged="ActivityScheduleTypeSelection_OnClientSelectedIndexChanged" Width="99%" runat="server">
                                    
                                        <Items>

                                            <Telerik:RadComboBoxItem Value="0" Text="By Frequency" />
                                            
                                            <Telerik:RadComboBoxItem Value="1" Text="Monthly"  />
                                        
                                            <Telerik:RadComboBoxItem Value="2" Text="Quarterly"  />

                                            <Telerik:RadComboBoxItem Value="3" Text="Yearly"  />

                                            <Telerik:RadComboBoxItem Value="4" Text="Birth Month"  />

                                            <Telerik:RadComboBoxItem Value="5" Text="By Calendar Month"  />

                                        </Items>
                                    
                                    </Telerik:RadComboBox>
                            
                                </div>
                                
                                <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                
                                <div style="position: relative; float: left; width: 10%; text-align: center">Value:&nbsp</div>
                        
                                <div style="position: relative; float: left; width: 05%"><Telerik:RadNumericTextBox ID="ActivityScheduleValue" Width="98%" NumberFormat-DecimalDigits="0" runat="server" /></div>                     

                                <div style="position: relative; float: left; width: 02%">&nbsp</div>

                                <div style="position: relative; float: left; width: 08%">
                                
                                    <Telerik:RadComboBox ID="ActivityScheduleQualifierSelection" Width="99%" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadComboBoxItem Value="0" Text="Days" />
                                            
                                            <Telerik:RadComboBoxItem Value="1" Text="Months" Selected="true" />
                                            
                                            <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                                        </Items>
                                    
                                    </Telerik:RadComboBox>
                                    
                                </div>
                                
                                <div style="position: relative; float: left; width: 08%; text-align: center">From:&nbsp</div>

                                <div style="position: relative; float: left; width: 20%">
                                
                                    <Telerik:RadComboBox ID="ActivityAnchorDate" Width="99%" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadComboBoxItem Value="0" Text="Population Effective Date" Selected="true" />
                                            
                                            <Telerik:RadComboBoxItem Value="1" Text="Population Anchor Date" />
                                                                                   
                                        </Items>
                                    
                                    </Telerik:RadComboBox>
                                    
                                </div>
                                
                                <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                
                                <div style="position: relative; float: left; width: 10%; display: block;"><asp:CheckBox ID="ActivityReoccurring" Text="Reoccurring" Checked="true" runat="server" /></div>
                                
                            </div>
                            
                            <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                            
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                            
                                <div style="position: relative; float: left; width: 15%">Action Date:</div>
                                
                                <div style="position: relative; float: left; width: 15%">
                                
                                    <Telerik:RadComboBox ID="ActivityActionDateTypeSelection" Width="99%" runat="server">
                                    
                                        <Items>
                                        
                                            <Telerik:RadComboBoxItem Value="0" Text="Immediately" />
                                            
                                            <Telerik:RadComboBoxItem Value="1" Text="On First Day of Month"  />
                                        
                                            <Telerik:RadComboBoxItem Value="2" Text="On 15th Day Of Month"  />

                                            <Telerik:RadComboBoxItem Value="3" Text="On Last Day of Month"  />

                                        </Items>
                                    
                                    </Telerik:RadComboBox>
                            
                                </div>
                           
                                <div style="position: relative; float: left; width: 02%">&nbsp</div>
                                
                                <div style="position: relative; float: left; width: 10%; text-align: center">Action:</div>
                                
                                <div style="position: relative; float: left; width: 55%"><Telerik:RadComboBox ID="ActivityEventsActionSelection" AutoPostBack="true" OnSelectedIndexChanged="ActivityEventsActionSelection_OnSelectedIndexChanged" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                
                             </div>
                                             
                            <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                            
                            <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                            
                                <Telerik:RadGrid ID="ActivityEventsActionParametersGrid" Height="168" OnNeedDataSource="ActivityEventsActionParametersGrid_OnNeedDataSource" OnItemCommand="ActivityEventsActionParametersGrid_OnItemCommand" OnItemDataBound="ActivityEventsActionParametersGrid_OnItemDataBound" AutoGenerateColumns="false" runat="server">
                            
                                    <MasterTableView TableLayout="Auto" EditMode="EditForms" DataKeyNames="ParameterName">
                                    
                                        <Columns>
                                    
                                            <Telerik:GridBoundColumn DataField="ParameterName" UniqueName="ParameterName" HeaderText="Parameter Name" ReadOnly="true" Visible="true" />
                                            
                                            <Telerik:GridBoundColumn DataField="ParameterValue" UniqueName="ParameterValue" HeaderText="Value" ReadOnly="true" Visible="true" />

                                            <Telerik:GridEditCommandColumn></Telerik:GridEditCommandColumn>                                                       

                                        </Columns>
                                
                                        <EditFormSettings EditFormType="Template">
                                        
                                            <FormTemplate>
                                                
                                                <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 98%; table-layout: fixed; border: solid 1px black"><tr><td>
                                                    
                                                    <table style="margin: 2px 0px 2px 0px; padding: 4px; line-height: 150%; width: 99%; table-layout: fixed;"><tr>
                                                                                                            
                                                        <td style="width: 08%;">Value:&nbsp</td>
                                                        
                                                        <td style="width: 25%"><Telerik:RadComboBox ID="ActivityEventParameterValueSelection" Width="99%" runat="server"></Telerik:RadComboBox></td>
                                                    
                                                        <td style="width: 02%">&nbsp</td>

                                                        <td style="width: 08%;">Fixed:&nbsp</td>
                                                       
                                                        <td style="width: 15%"><Telerik:RadTextBox ID="ActivityEventParameterFixedValue" Width="99%" runat="server" /></td>

                                                        <td style="width: 03%">&nbsp</td>
                                                        
                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityEventUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Add" : "Update" %>' CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>' Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>

                                                        <td style="width: 02%">&nbsp</td>

                                                        <td style="width: 10%"><asp:Button ID="ButtonActivityEventCancel" Text="Cancel" CommandName="Cancel" Width="60px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                                    
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
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
             
                        <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateActivityEvent" Text="Update Selected" OnClick="ButtonAddUpdateActivityEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
                        <div style="position: relative; float: right"><asp:Button ID="ButtonAddActivityEvent" Text="Add Activity" OnClick="ButtonAddUpdateActivityEvent_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
       
                    </div>      
                    
                </div>
                
            </Telerik:RadPageView>

            <Telerik:RadPageView ID="PageExtendedProperties" runat="server">
            
                <div style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementExtendedProperties.png" alt="Population Extended Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Extended Properties</div>
                    
                    </div>
       
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Current Extended Properties</b></div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ExtendedPropertiesGrid" Height="460" AutoGenerateColumns="false" OnDeleteCommand="ExtendedPropertiesGrid_OnDeleteCommand" runat="server">
                    
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
                    
                    </div>     
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                        
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Extended Property to Population</b></div>

                    <div style="margin: 4px 14px 2px 14px; padding: 4px; border: solid 1px black; line-height: 150%; overflow: hidden;">
                       
                        <div style="clear: both;">
                            
                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                            
                            <div style="position: relative; float: left; width: 10%">Name:</div>
                            
                            
                            <div style="position: relative; float: left; width: 35%"><Telerik:RadTextBox ID="PopulationExtendedPropertyName" Width="100%" runat="server"></Telerik:RadTextBox></div>
                            
                            <div style="position: relative; float: left; width: 05%">&nbsp</div>
                            
                            <div style="position: relative; float: left; width: 10%">Value:</div>
                            
                            <div style="position: relative; float: left; width: 35%"><Telerik:RadTextBox ID="PopulationExtendedPropertyValue" Width="100%" runat="server"></Telerik:RadTextBox></div>
                            
                            <div style="position: relative; float: left; width: 02%">&nbsp</div>
                            
                        </div>
                                                                          
                        <div style="clear: both; margin: 12px 10px 0px 10px; padding: 0px; line-height: 150%;">
                 
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddExtendedProperty" Text="Add Property" OnClick="ButtonAddExtendedProperty_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
           
                        </div>      
                   
                    </div>     
                
                </div>  
                                    
            </Telerik:RadPageView>        

            <Telerik:RadPageView ID="PagePreview" runat="server">
            
                <div style="position: relative; float: left; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/PopulationCareManagementPreview.png" alt="Population Membership Preview" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Populaton Membership Preview</div>
                    
                    </div>

                </div>            
                        
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="PopulationPreviewGrid" Height="580" AutoGenerateColumns="false" runat="server">
                    
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

    <div style="clear: both; height: 10px"><span></span></div>

    <div style="height: 20px; padding: 0px 10px 0px 10px;">
    
        <span style="float: left"><asp:Label ID="SaveResponseLabel" Text="" runat="server" /></span>
    
        <span style="float: right;"></span>

        <span style="float: right;"><asp:Button ID="ButtonApply" Text="Apply" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonApply_OnClick" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonCancel" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonCancel_OnClick" runat="Server" /></span>

        <span style="float: right; width: 10px">&nbsp</span>

        <span style="float: right;"><asp:Button ID="ButtonOk" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonOk_OnClick" runat="Server" /></span>

    </div>
                
</div>

</form>

<script type="text/javascript">

function HelpPanel_OnHide (panelId) {

    helpPanel = document.getElementById (panelId + "Help");
    
    if (helpPanel != null) {
    
        helpPanel.style.display = "none";
        
        contentPanel = document.getElementById (panelId + "Content");
        
        if (contentPanel != null) { 
        
            contentPanel.style.width = "99%";
            
        }
        
    }

    return;

}


    function ServiceEventsAnchorSelection_OnClientSelectedIndexChanged (sender, eventArgs) {
    
        var selectedItem = eventArgs.get_item ();
        
        var anchorDateValue = $find ("ServiceEventsAnchorDateValue");
        
        if (anchorDateValue != null) {
        
            switch (selectedItem.get_value ()) {
            
                case "2": 
                
                case "3": 
                
                case "4":
                
                    anchorDateValue.enable ();
                    
                    break;
                    
                default: 
                
                    anchorDateValue.disable ();
                    
                    break;
                    
            }
            
        }
        
        return;
        
    }
    

    function TriggerEventsTypeSelection_OnClientSelectedIndexChanged (sender, eventArgs) {
    
        var selectedItem = eventArgs.get_item ();
        
        if (selectedItem.get_value () == 0) { 

            document.getElementById ("TriggerEventsTypeService").style.display = "block";

            document.getElementById("TriggerEventsTypeMetric").style.display = "none";

            document.getElementById("TriggerEventsTypeAuthorizedService").style.display = "none";
        
        }
        
        else if (selectedItem.get_value () == 1) {
        
            document.getElementById ("TriggerEventsTypeService").style.display = "none";
            
            document.getElementById ("TriggerEventsTypeMetric").style.display = "block";

            document.getElementById("TriggerEventsTypeAuthorizedService").style.display = "none";

        }

        else if (selectedItem.get_value() == 2) {

            document.getElementById("TriggerEventsTypeService").style.display = "none";

            document.getElementById("TriggerEventsTypeMetric").style.display = "none";

            document.getElementById("TriggerEventsTypeAuthorizedService").style.display = "block";

        }
        
        else {
        
            document.getElementById ("TriggerEventsTypeService").style.display = "none";
            
            document.getElementById ("TriggerEventsTypeMetric").style.display = "none";

            document.getElementById("TriggerEventsTypeAuthorizedService").style.display = "none";

        }
    
        return;

    }    
    
    
    function ActivityScheduleTypeSelection_OnClientSelectedIndexChanged (sender, eventArgs) {
    
        var selectedItem = eventArgs.get_item ();
        
        var scheduleValue = $find ("ActivityScheduleValue");

        var qualifierSelection = $find("ActivityScheduleQualifierSelection");

        var anchorDate = $find("ActivityAnchorDate");

        var reoccurringCheckBox = document.getElementById ("<%= ActivityReoccurring.ClientID %>");
        
        if ((scheduleValue == null) || (qualifierSelection == null)) { return; }
        
        
        switch (selectedItem.get_value ()) {

            case "0":

                scheduleValue.enable();

                qualifierSelection.enable();

                anchorDate.enable();

                reoccurringCheckBox.disabled = false;

                break;

            case "5":

                scheduleValue.enable();

                qualifierSelection.disable();

                anchorDate.disable();

                reoccurringCheckBox.disabled = true;

                reoccurringCheckBox.checked = true;

                break;

            default:

                scheduleValue.disable();

                qualifierSelection.disable();

                anchorDate.disable();

                reoccurringCheckBox.disabled = true;

                reoccurringCheckBox.checked = true;

                break;
                
        }
    
        return;

    }

    function TriggerEventsProblemStatementTreeView_OnClientNodeClicking(sender, e) {

        var triggerEventsProblemStatementSelection = $find("<%= TriggerEventsProblemStatementSelection.ClientID  %>");

        var selectedNode = e.get_node();

        var selectedNodeChildren = selectedNode.get_allNodes();

        if (selectedNodeChildren.length > 0) { return; }


        var selectionText = selectedNode.get_text();

        if (selectedNode.get_parent() != null) {

            selectionText = selectedNode.get_parent().get_parent().get_text() + " - " + selectedNode.get_parent().get_text() + " - " + selectedNode.get_text();

        }

        triggerEventsProblemStatementSelection.set_text(selectionText);

        triggerEventsProblemStatementSelection.trackChanges();

        triggerEventsProblemStatementSelection.get_items().getItem(0).set_text(selectedNode.get_text());

        triggerEventsProblemStatementSelection.get_items().getItem(0).set_value(selectedNode.get_value());

        triggerEventsProblemStatementSelection.commitChanges();

        triggerEventsProblemStatementSelection.hideDropDown();


        return;

    }

</script>
    
</body>

</html>

