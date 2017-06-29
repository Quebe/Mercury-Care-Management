<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MedicalServiceSingleton.aspx.cs" Inherits="Mercury.Web.Application.Configuration.Windows.MedicalServiceSingleton" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">

<title>Medical Service - New Singleton</title>

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

<form id="FormMedicalServiceSingleton" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />

    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddDefinition" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceDefinitionGrid" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateDefinition" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel"  />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="ServiceDefinitionGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel"  />
                
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonPreview" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonPreview" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="ServicePreviewGrid" LoadingPanelID="AjaxLoadingPanel"  />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                                    
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

    <div style="position: relative; float: left; width: 99%; height: 700px; margin: 1px; border: solid 1px black;">
    
        <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Tabs>
            
                <Telerik:RadTab Text="General"></Telerik:RadTab>

                <Telerik:RadTab Text="Definition"></Telerik:RadTab>

                <Telerik:RadTab Text="Preview"></Telerik:RadTab>

            </Tabs>
                  
        </Telerik:RadTabStrip>

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white; overflow: hidden">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2.png" alt="Service Properties" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Service</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Name and Description</b></div>
                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadTextBox ID="SingletonName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 98%; padding: 4px;"><Telerik:RadTextBox ID="SingletonDescription" Width="98%" Rows="15" EmptyMessage="(required)" TextMode="MultiLine" runat="server" /></div>


                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Classification:</div>
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">
                    
                            <Telerik:RadComboBox ID="SingletonClassification" Width="98%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Not Specified" runat="server" />

                                    <Telerik:RadComboBoxItem Value="1" Text="Medical" Selected="true" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Medication" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="3" Text="Lab" runat="server" />
                                
                                    <Telerik:RadComboBoxItem Value="4" Text="Immunization" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="5" Text="Diagnosis" runat="server" />

                                    <Telerik:RadComboBoxItem Value="6" Text="Exclusion" runat="server" />
                                
                                </Items>
                                
                            </Telerik:RadComboBox>
                                    
                        </div>

                        <div style="position: relative; float: left; width: 15%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="SingletonEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 15%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="SingletonVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 30%">
                        
                            <div style="position: relative; float: left; width: 50%; padding: 4px;">Last Paid Date:</div>
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadDateInput ID="SingletonLastPaidDate" DateFormat="MM/dd/yyyy" Width="100%" MinDate="01/01/1900" runat="server" /></div>

                        </div>

                    </div>
                

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px"><b>Created and Modified</b></div>
                
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="SingletonCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="SingletonModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="SingletonModifiedDate" DateFormat="MM/dd/yyyy" TabIndex="-1" Width="100%" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </div>

                <div style="position: relative; float: left; width:33%; height: 661px; margin: 4px; border: solid 1px black; background-color: #dee6ee">
                
                </div>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="Definition" runat="server">
            
                <div style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2DatabaseTable.png" alt="Service Definition" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Service Identification Criteria</div>
                    
                    </div>
                    
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Current Criteria</b></div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ServiceDefinitionGrid" Height="300" AutoGenerateColumns="false" OnItemCommand="ServiceDefinitionGrid_OnItemCommand" OnDeleteCommand="ServiceDefinitionGrid_OnDeleteCommand" runat="server">
                        
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="DefinitionId" UniqueName="DefinitionId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="DefinitionType" UniqueName="DefinitionType" HeaderText="Type" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Criteria" UniqueName="Criteria" HeaderText="Criteria" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this definition?" UniqueName="DeleteDefinition" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this definition?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
                                </Columns>
                                
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                                
                                <ClientEvents OnRowSelected="ServiceDefinitionGrid_OnRowSelected" OnRowDeselected="ServiceDefinitionGrid_OnRowDeselected" />
                            
                            </ClientSettings>
                       
                        </Telerik:RadGrid>
                        
                    </div>
                    
                                      
                    <div id="AddDefinitionDiv" style="clear: both" runat="server">
                      
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; font-family: Arial; line-height: 150%; background-color: #dee6ee; color: Navy; border: outset 1px;"><b>Add Criteria</b></div>                      

                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                        
                            <div style="position: relative; float: left; width: 10%">Type:</div>
                            
                            <div style="position: relative; float: left">
                            
                                <Telerik:RadComboBox ID="ServiceTypeSelection" OnClientSelectedIndexChanged="ServiceTypeSelection_OnClientSelectedIndexChanged" runat="server">
                                
                                    <Items> 

                                        <Telerik:RadComboBoxItem Value="AllMedical" Text="All Medical" Selected="true" runat="server" />
                                        
                                        <Telerik:RadComboBoxItem Value="Professional" Text="Professional" runat="server" />
                                        
                                        <Telerik:RadComboBoxItem Value="Institutional" Text="Institutional" runat="server" />
                                    
                                        <Telerik:RadComboBoxItem Value="Pharmacy" Text="Pharmacy" runat="server" />

                                        <Telerik:RadComboBoxItem Value="Lab" Text="Lab" runat="server" />

                                        <Telerik:RadComboBoxItem Value="CustomCriteria" Text="Custom" runat="server" Visible="true" />
                                    
                                    </Items>
                                
                                </Telerik:RadComboBox>
                        
                            </div>
                            
                            <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateDefinition" Text="Update Selected" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddUpdateDefinition_OnClick" Enabled="false" runat="server" /></div>
                        
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddDefinition" Text="Add Criteria" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddUpdateDefinition_OnClick" runat="server" /></div>
                            
                        </div>
                        
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%">
                        
                            <Telerik:RadMultiPage ID="ServiceTypeCriteria" SelectedIndex="0" runat="server">

                                <Telerik:RadPageView ID="AllMedical" runat="server">
                                
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 205px; border: solid 1px #DDDDDD; overflow: auto;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Event Date Order:</span>

                                            <span style="float: right; width: 80%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalEventDateOrder" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="0" Text="Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="1" Text="Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Service Date From, Claim Date From" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="3" Text="Service Date Thru, Claim Date Thru" runat="server" />
                                                                                                            
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">Principal Diagnosis:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                    
                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <div style="float: left;  width: 20%">Diagnosis Codes:</div>
                                        
                                            <div style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></div>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">
                                        
                                            <span style="float: left;  width: 20%">Procedure Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">Modifier Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                            
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">Provider Specialties:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                            
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">PCP Performed Service:</span>
                                        
                                            <span style="float: right; width: 80%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalPcpPerformedService" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="false" Text="Not Required" Selected="true" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="true" Text="Required" runat="server" />
                                                                                                           
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </span>
                                            
                                         </div>
                                         
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <table width="100%"><tr>
                                            
                                                <td><asp:CheckBox ID="CriteriaAllMedicalUseMemberAgeCriteria" Text="Use Member Age Criteria" Checked="false" runat="server" /></td>
                                            
                                                <td style="width:05%">&nbsp</td>
                                            
                                                <td>Age in: </td>
                                                
                                                <td>
                                                
                                                    <Telerik:RadComboBox ID="CriteriaAllMedicalMemberAgeDateQualifier" Width="80" runat="server">
                                                    
                                                        <Items>
                                                        
                                                            <Telerik:RadComboBoxItem Text="Months" Value="1" />
                                                            
                                                            <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="true" />
                                                        
                                                        </Items>
                                                
                                                    </Telerik:RadComboBox>
                                                
                                                </td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Minimum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaAllMedicalMemberAgeMinimum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Maximum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaAllMedicalMemberAgeMaximum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                                                            
                                            </tr></table>

                                        </div>

                                    </div>
                                
                                </Telerik:RadPageView>
                                
                                <Telerik:RadPageView ID="Professional" runat="server">

                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 210px; border: solid 1px #DDDDDD; overflow: auto;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Event Date Order:</span>

                                            <span style="float: right; width: 80%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalEventDateOrder" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="0" Text="Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="1" Text="Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Service Date From, Claim Date From" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="3" Text="Service Date Thru, Claim Date Thru" runat="server" />
                                                       
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">Principal Diagnosis:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                    
                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">Diagnosis Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Service Locations:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalServiceLocations" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                        
                                            <span style="float: left;  width: 20%">Procedure Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Modifier Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                         
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">Provider Specialties:</span>
                                        
                                            <span style="float: left; width: 75%"><Telerik:RadTextBox ID="CriteriaProfessionalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                            <span style="float: right; width: 05%"><img id="CriteriaProfessionalProviderSpecialtiesSearch" alt="Provider Specialties Selection" src="/Images/Common16/Search.png" /></span>
                                            
                                         </div>
                                            
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">PCP Performed Service:</span>
                                        
                                            <span style="float: right; width: 80%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalPcpPerformedService" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="false" Text="Not Required" Selected="true" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="true" Text="Required" runat="server" />
                                                                                                           
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </span>
                                            
                                         </div>
                                         
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <table width="100%"><tr>
                                            
                                                <td><asp:CheckBox ID="CriteriaProfessionalUseMemberAgeCriteria" Text="Use Member Age Criteria" Checked="false" runat="server" /></td>
                                            
                                                <td style="width:05%">&nbsp</td>
                                            
                                                <td>Age in: </td>
                                                
                                                <td>
                                                
                                                    <Telerik:RadComboBox ID="CriteriaProfessionalMemberAgeDateQualifier" Width="80" runat="server">
                                                    
                                                        <Items>
                                                        
                                                            <Telerik:RadComboBoxItem Text="Months" Value="1" />
                                                            
                                                            <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="true" />
                                                        
                                                        </Items>
                                                
                                                    </Telerik:RadComboBox>
                                                
                                                </td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Minimum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaProfessionalMemberAgeMinimum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Maximum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaProfessionalMemberAgeMaximum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                                                            
                                            </tr></table>

                                        </div>
                                        
                                    </div>
                                
                                </Telerik:RadPageView>
                            
                                <Telerik:RadPageView ID="Institutional" runat="server">
                                
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 210px; border: solid 1px #DDDDDD; overflow: scroll;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Event Date Order:</span>

                                            <span style="float: right; width: 80%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaInstitutionalEventDateOrder" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="0" Text="Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="1" Text="Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Service Date From, Claim Date From" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="3" Text="Service Date Thru, Claim Date Thru" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="4" Text="Admission Date, Claim Date From" runat="server" />

                                                        <Telerik:RadComboBoxItem Value="5" Text="Discharge Date, Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="6" Text="Service Date From, Admission Date, Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="7" Text="Service Date Thru, Discharge Date, Claim Date Thru" runat="server" />
                                                        
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">Principal Diagnosis:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                    
                                        <div style="width: 95%; padding: 4px; overflow: hidden">    
                                    
                                            <span style="float: left;  width: 20%">Diagnosis Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">DRG Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalDrgCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">ICD-9 Procedures:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalIcd9Codes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Bill Types:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalBillTypes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                        
                                            <span style="float: left;  width: 20%">Revenue Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalRevenueCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                        
                                            <span style="float: left;  width: 20%">Procedure Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden">

                                            <span style="float: left;  width: 20%">Modifier Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                            
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">Provider Specialties:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaInstitutionalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                         
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <table width="100%"><tr>
                                            
                                                <td><asp:CheckBox ID="CriteriaInstitutionalUseMemberAgeCriteria" Text="Use Member Age Criteria" Checked="false" runat="server" /></td>
                                            
                                                <td style="width:05%">&nbsp</td>
                                            
                                                <td>Age in: </td>
                                                
                                                <td>
                                                
                                                    <Telerik:RadComboBox ID="CriteriaInstitutionalMemberAgeDateQualifier" Width="80" runat="server">
                                                    
                                                        <Items>
                                                        
                                                            <Telerik:RadComboBoxItem Text="Months" Value="1" />
                                                            
                                                            <Telerik:RadComboBoxItem Text="Years" Value="2" Selected="true" />
                                                        
                                                        </Items>
                                                
                                                    </Telerik:RadComboBox>
                                                
                                                </td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Minimum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaInstitutionalMemberAgeMinimum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                
                                                <td style="width:05%">&nbsp</td>

                                                <td>Maximum Age:</td>
                                                
                                                <td style="width:80"><Telerik:RadNumericTextBox ID="CriteriaInstitutionalMemberAgeMaximum" Width="80" NumberFormat-DecimalDigits="0" MinValue="0" MaxValue="99999" runat="server"></Telerik:RadNumericTextBox></td>
                                                                                            
                                            </tr></table>

                                        </div>
                                        
                                    </div>               
                                        
                                </Telerik:RadPageView>
                                
                                <Telerik:RadPageView ID="Pharmacy" runat="server">
                                   
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 205px; border: solid 1px #DDDDDD; overflow: auto;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">NDC Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaPharmacyNdcCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                    
                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <div style="float: left;  width: 20%">Drug Names:</div>
                                        
                                            <div style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaPharmacyDrugNames" Width="99%" runat="server"></Telerik:RadTextBox></div>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden; display: none;">
                                        
                                            <span style="float: left;  width: 20%">DEA Classifications:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaPharmacyDeaClassifications" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">

                                            <span style="float: left;  width: 20%">Therapeutic Classifications:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaPharmacyTherapeuticClassifications" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                         </div>
                                            
                                    </div>
                            
                                </Telerik:RadPageView>
                            
                                <Telerik:RadPageView ID="Lab" runat="server">
                                
                                   <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 205px; border: solid 1px #DDDDDD; overflow: auto;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">LOINC Codes:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaLabLoincCodes" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                    
                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <div style="float: left;  width: 20%">Value Expressions:</div>
                                        
                                            <div style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaLabValueExpressions" Width="99%" runat="server"></Telerik:RadTextBox></div>
                                            
                                        </div>
                                        
                                        <div style="width: 95%; padding: 4px; overflow: hidden;">
                                        
                                            <span style="float: left;  width: 20%">Metric:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadComboBox ID="CriteriaLabMetricSelection" Width="100%" runat="server" /></span>
                                            
                                        </div>
                                                                                  
                                    </div>
                                                                    
                                </Telerik:RadPageView>
                                
                                <Telerik:RadPageView ID="CustomCriteria" runat="server">

                                   <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%; height: 205px; border: solid 1px #DDDDDD; overflow: auto;">

                                        <div style="width: 95%; padding: 4px; overflow: hidden">
                                    
                                            <span style="float: left;  width: 20%">Stored Procedure Name:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaCustomStoredProcedureName" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
                                        </div>
                                        
                                    </div>
                                                                    
                                </Telerik:RadPageView>
                                
                            </Telerik:RadMultiPage>
                        
                        </div>
                    
                    </div>                                      
                    
                </div>
                
            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="Preview" runat="server">
            
                <div style="position: relative; float: left; width:65%; margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2Preview.png" alt="Service Preview" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Service Preview</div>
                    
                    </div>

                </div>            
                        
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="ServicePreviewGrid" Height="580" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView Width="99%" TableLayout="Auto">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="SingletonDefinitionId" UniqueName="SingletonDefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />
                                
                                <Telerik:GridBoundColumn DataField="ClaimId" UniqueName="ClaimId" HeaderText="Claim Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ExternalClaimId" UniqueName="ExternalClaimId" HeaderText="External Claim Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ClaimLine" UniqueName="ClaimLine" HeaderText="Line" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="MemberId" UniqueName="MemberId" HeaderText="Member Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProviderId" UniqueName="ProviderId" HeaderText="Provider Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ClaimType" UniqueName="ClaimType" HeaderText="Type" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ClaimDateFrom" UniqueName="ClaimDateFrom" HeaderText="Claim From"  ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ClaimDateThru" UniqueName="ClaimDateThru" HeaderText="Claim Thru" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceDateFrom" UniqueName="ServiceDateFrom" HeaderText="Service From" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceDateThru" UniqueName="ServiceDateThru" HeaderText="Service Thru" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AdmissionDate" UniqueName="AdmissionDate" HeaderText="Admission Date" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="DischargeDate" UniqueName="DischargeDate" HeaderText="Discharge Date" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="BillType" UniqueName="BillType" HeaderText="BillType" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="PrincipalDiagnosisCode" UniqueName="PrincipalDiagnosisCode" HeaderText="Principal Diagnosis" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="DiagnosisCode" UniqueName="DiagnosisCode" HeaderText="Diagnosis Code" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="Icd9ProcedureCode" UniqueName="Icd9ProcedureCode" HeaderText="Icd-9 Procedure" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="LocationCode" UniqueName="LocationCode" HeaderText="Location" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="RevenueCode" UniqueName="RevenueCode" HeaderText="Revenue" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProcedureCode" UniqueName="ProcedureCode" HeaderText="Procedure" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ModifierCode" UniqueName="ModifierCode" HeaderText="Modifier" ReadOnly="true" Visible="true" ConvertEmptyStringToNull="false" />
                                
                                <Telerik:GridBoundColumn DataField="SpecialtyName" UniqueName="SpecialtyName" HeaderText="Specialty" ReadOnly="true" Visible="true" ConvertEmptyStringToNull="false" />
                                
                                <Telerik:GridBoundColumn DataField="IsPcpClaim" UniqueName="IsPcpClaim" HeaderText="PCP Claim" ReadOnly="true" Visible="true" ConvertEmptyStringToNull="false" />
                                
                                <Telerik:GridBoundColumn DataField="NdcCode" UniqueName="NdcCode" HeaderText="NDC" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="DeaClassification" UniqueName="DeaClassification" HeaderText="DEA" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="TherapeuticClassification" UniqueName="TherapeuticClassification" HeaderText="Therapeutic" ReadOnly="true" Visible="true" />
                                
                                <Telerik:GridBoundColumn DataField="LoincCode" UniqueName="LoincCode" HeaderText="LOINC" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="LabValue" UniqueName="LabValue" HeaderText="Lab Value" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="Description" UniqueName="Description" HeaderText="Description" ReadOnly="true" Visible="true" />

                            </Columns>
                        
                        </MasterTableView>
                        
                        <ClientSettings>
                        
                            <Selecting AllowRowSelect="true" />
                            
                            <Scrolling AllowScroll="true" />
                        
                        </ClientSettings>
                    
                    </Telerik:RadGrid>
                    
                </div>
                
                <div style="height: 20px; padding: 0px 10px 0px 10px;">
                
                    <span style="float: left;">Warning: Retreiving claims may take time.</span>
                        
                    <span style="float: right;"><asp:Button ID="ButtonPreview" Text="Preview" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" OnClick="ButtonPreview_OnClick" runat="Server" /></span>

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

<div id="ToolTipSection" style="display: none">

    <Telerik:RadToolTipManager ID="CriteriaAllMedicalProviderSpecialtiesToolTip" Width="300px" Height="200px" Animation="Fade" Sticky="true" ManualClose="false"  runat="server">
    
        <TargetControls>
        
            <Telerik:ToolTipTargetControl TargetControlID="CriteriaProfessionalProviderSpecialtiesSearch" IsClientID="true" />
        
        </TargetControls>
    
    </Telerik:RadToolTipManager>

</div>

</form>

<script type="text/javascript">

    function ServiceTypeSelection_OnClientSelectedIndexChanged (sender, eventArgs) {
    
        var pageView = $find("<%= ServiceTypeCriteria.ClientID %>");
        
        var pageViewIndex = parseInt (sender.get_selectedIndex());

        pageView.get_pageViews().getPageView(pageViewIndex).set_selected(true);

        return;
        
    }
    
    function ServiceDefinitionGrid_OnRowSelected (sender, eventArgs) { 
    
        document.getElementsByName ("ButtonUpdateDefinition")[0].disabled = false;
    
        return;
    
    }

    function ServiceDefinitionGrid_OnRowDeselected (sender, eventArgs) { 
    
        document.getElementsByName ("ButtonUpdateDefinition")[0].disabled = true;
       
        return;
    
    }
    
</script>

</body>

</html>
