<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceSingleton.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.ServiceSingleton" %>

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

<form id="FormServiceSingleton" runat="server">

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
        
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanel" runat="server"></Telerik:RadAjaxLoadingPanel>
    
    <Telerik:RadAjaxLoadingPanel ID="AjaxLoadingPanelWhiteout" Transparency="75" InitialDelayTime="100" MinDisplayTime="0" Skin="" EnableAjaxSkinRendering="false" runat="server">
    
        <div style="background-color: white; min-height: 100%; height: 100%; opacity: 0.25; filter: alpha(opacity=25); z-index: 10">
    
        </div>
            
    </Telerik:RadAjaxLoadingPanel>
    
 </div>
 
  
<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="Buttons" runat="server" />

<div style="min-width: 800px;">

    <Telerik:RadTabStrip ID="PropertiesTab" MultiPageID="PropertiesContent" SelectedIndex="0" runat="server">
        
        <Tabs>
            
            <Telerik:RadTab Text="General"></Telerik:RadTab>

            <Telerik:RadTab Text="Definition"></Telerik:RadTab>

            <Telerik:RadTab Text="Preview"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Document2.png" alt="Service Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Service</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonDescription" Width="98%" Rows="12" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    
                        <div style="clear: both"></div>
                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="position: relative; float: left; width: 12%; padding: 4px;">Classification:</div>
                    
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
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ServiceSingletonEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 15%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="ServiceSingletonVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 30%">
                        
                            <div style="position: relative; float: left; width: 50%; padding: 4px;">Last Paid Date:</div>
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadDateInput ID="SingletonLastPaidDate" DateFormat="MM/dd/yyyy" Width="100%" MinDate="01/01/1900" runat="server" /></div>

                        </div>

                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ServiceSingletonCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="ServiceSingletonModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="ServiceSingletonModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="Definition" runat="server">
            
                <div style="margin: 1px; border: solid 1px white;">
                
                    <div style=" margin: 2px; padding: 2px;">
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2DatabaseTable.png" alt="Service Definition" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Service Identification Criteria</div>
                    
                    </div>
                    
                    
                    <div class="PropertyPageSectionTitle">Current Criteria</div>
                                                            
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="ServiceDefinitionGrid" Height="235" AutoGenerateColumns="false" OnItemCommand="ServiceDefinitionGrid_OnItemCommand" OnDeleteCommand="ServiceDefinitionGrid_OnDeleteCommand" runat="server">
                        
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
                    

                    <div class="PropertyPageSectionTitle">Add Criteria</div>
                                                          
                    <div id="AddDefinitionDiv" style="clear: both" runat="server">
                      
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
                        
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%;  height: 190px; border: solid 1px #DDDDDD; overflow: auto;">
                        
                            <Telerik:RadMultiPage ID="ServiceTypeCriteria" SelectedIndex="0" runat="server">

                                <Telerik:RadPageView ID="AllMedical" runat="server">
                                
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">
                                       
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Event Date Order:</td>

                                            <td>
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalEventDateOrder" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="0" Text="Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="1" Text="Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Service Date From, Claim Date From" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="3" Text="Service Date Thru, Claim Date Thru" runat="server" />
                                                                                                            
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>

                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>

                                            <td width="15%">Principal Diagnosis:</td>

                                            <td><Telerik:RadTextBox ID="CriteriaAllMedicalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>

                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalPrincipalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                            </tr><tr>
                                    
                                            <td style="width: 15%">Diagnosis Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaAllMedicalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                        
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Procedure Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaAllMedicalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                        </tr></table>
                                        
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Modifier Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaAllMedicalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>

                                            <td>Provider Specialties:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaAllMedicalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>
                                            
                                            <td>PCP Performed Service:</td>
                                        
                                            <td>
                                            
                                                <Telerik:RadComboBox ID="CriteriaAllMedicalPcpPerformedService" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="false" Text="Not Required" Selected="true" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="true" Text="Required" runat="server" />
                                                                                                           
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                         
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

                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">

                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Event Date Order:</span>

                                            <td>
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalEventDateOrder" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="0" Text="Claim Date From" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="1" Text="Claim Date Thru" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="2" Text="Service Date From, Claim Date From" runat="server" />
                                                        
                                                        <Telerik:RadComboBoxItem Value="3" Text="Service Date Thru, Claim Date Thru" runat="server" />
                                                       
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                                                                
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>

                                            <td width="15%">Principal Diagnosis:</td>

                                            <td><Telerik:RadTextBox ID="CriteriaProfessionalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>

                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalPrincipalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                            </tr><tr>
                                    
                                            <td style="width: 15%">Diagnosis Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaProfessionalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                        
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Service Locations:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaProfessionalServiceLocations" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr></tr>

                                            <td>Procedure Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaProfessionalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr></tr>

                                            <td>Modifier Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaProfessionalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                        </tr></table>
                                         
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Provider Specialties:</td>
                                        
                                            <td style="width: 80%"><Telerik:RadTextBox ID="CriteriaProfessionalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            <td style="width: 05%" align="right"><img id="CriteriaProfessionalProviderSpecialtiesSearch" alt="Provider Specialties Selection" src="/Images/Common16/Search.png" /></td>
                                            
                                        </tr></table>
                                            
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">PCP Performed Service:</td>
                                        
                                            <td>
                                            
                                                <Telerik:RadComboBox ID="CriteriaProfessionalPcpPerformedService" Width="100%" runat="server">
                                                
                                                    <Items> 

                                                        <Telerik:RadComboBoxItem Value="false" Text="Not Required" Selected="true" runat="server" />
                                                    
                                                        <Telerik:RadComboBoxItem Value="true" Text="Required" runat="server" />
                                                                                                           
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                         
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
                                
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">
                                    
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">Event Date Order:</td>

                                            <td>
                                            
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
                                                
                                            </td>
                                            
                                        </tr></table>

                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>

                                            <td width="15%">Principal Diagnosis:</td>

                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>

                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaInstitutionalPrincipalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                            </tr><tr>
                                    
                                            <td style="width: 15%">Diagnosis Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaInstitutionalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                        
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                            <td style="width: 15%">DRG Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalDrgCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>
                                    
                                            <td>ICD-9 Procedures:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalIcd9Codes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>

                                            <td>Bill Types:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalBillTypes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>
                                        
                                            <td>Revenue Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalRevenueCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>
                                        
                                            <td>Procedure Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalProcedureCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>

                                            <td>Modifier Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalModifierCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            </tr><tr>

                                            <td>Provider Specialties:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaInstitutionalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                        </tr></table>
                                         
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
                                   
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">

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
                                
                                   <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">

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

                                   <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">

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
                
                    <Telerik:RadGrid ID="ServicePreviewGrid" Height="470" AutoGenerateColumns="false" runat="server">
                    
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
 
<div id="ToolTipSection" style="display: none">

    <Telerik:RadToolTipManager ID="CriteriaAllMedicalProviderSpecialtiesToolTip" Width="300px" Height="200px" Animation="Fade" Sticky="true" ManualClose="false"  runat="server">
    
        <TargetControls>
        
            <Telerik:ToolTipTargetControl TargetControlID="CriteriaProfessionalProviderSpecialtiesSearch" IsClientID="true" />
        
        </TargetControls>
    
    </Telerik:RadToolTipManager>

</div>


<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    function ServiceTypeSelection_OnClientSelectedIndexChanged(sender, eventArgs) {

        var pageView = $find("<%= ServiceTypeCriteria.ClientID %>");

        var pageViewIndex = parseInt(sender.get_selectedIndex());

        pageView.get_pageViews().getPageView(pageViewIndex).set_selected(true);

        return;

    }

    function ServiceDefinitionGrid_OnRowSelected(sender, eventArgs) {

        document.getElementsByName("ButtonUpdateDefinition")[0].disabled = false;

        return;

    }

    function ServiceDefinitionGrid_OnRowDeselected(sender, eventArgs) {

        document.getElementsByName("ButtonUpdateDefinition")[0].disabled = true;

        return;

    }
    
</script>
 
 </Telerik:RadScriptBlock>

</form>
    
</body>

</html>