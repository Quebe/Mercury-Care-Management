<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthorizedService.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.AuthorizedService" %>

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

<form id="FormAuthorizedService" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
       
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="ButtonAddDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddDefinition" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizedServiceDefinitionGrid" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonUpdateDefinition">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonUpdateDefinition" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizedServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel"  />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" />
                
                </UpdatedControls>
            
            </Telerik:AjaxSetting>

            <Telerik:AjaxSetting AjaxControlID="AuthorizedServiceDefinitionGrid">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizedServiceDefinitionGrid" LoadingPanelID="AjaxLoadingPanel"  />
                
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="AuthorizationCategory">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizationCategory"   LoadingPanelID="AjaxLoadingPanel"  />
                
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizationSubcategory" LoadingPanelID="AjaxLoadingPanel"  />
                    
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizationServiceType" LoadingPanelID="AjaxLoadingPanel"  />
                    
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="AuthorizationSubcategory">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizationSubcategory" LoadingPanelID="AjaxLoadingPanel"  />
                
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizationServiceType" LoadingPanelID="AjaxLoadingPanel"  />
                    
                </UpdatedControls>            
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonPreview" >
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonPreview" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="AuthorizedServicePreviewGrid" LoadingPanelID="AjaxLoadingPanel"  />

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
 
  
<Telerik:RadFormDecorator ID="TelerikFormDecorator" DecoratedControls="All" runat="server" />

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
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/AuthorizedService.png" alt="Authorized Service Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Authorized Service</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="AuthorizedServiceEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="AuthorizedServiceVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="AuthorizedServiceCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="AuthorizedServiceModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="AuthorizedServiceModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
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
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2DatabaseTable.png" alt="Authorized Service Definition" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Authorized Service Identification Criteria</div>
                    
                    </div>
                    
                    <div class="PropertyPageSectionTitle">Current Criteria</div>
                                        
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="AuthorizedServiceDefinitionGrid" Height="240" AutoGenerateColumns="false" OnItemCommand="AuthorizedServiceDefinitionGrid_OnItemCommand" OnDeleteCommand="AuthorizedServiceDefinitionGrid_OnDeleteCommand" runat="server">
                        
                            <MasterTableView TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="DefinitionId" UniqueName="DefinitionId" HeaderText="Id" ReadOnly="true" Visible="true" />
                                                                        
                                    <Telerik:GridBoundColumn DataField="Criteria" UniqueName="Criteria" HeaderText="Criteria" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to delete this definition?" UniqueName="DeleteDefinition" Text="Delete" />
                                    
                                    <Telerik:GridButtonColumn CommandName="ToggleActive" ButtonType="LinkButton" ConfirmText="Are you sure you want to toggle the active status of this definition?" UniqueName="IsActive" Text="Toggle Enabled" />
                                
                                </Columns>
                                
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                                
                                <ClientEvents OnRowSelected="AuthorizedServiceDefinitionGrid_OnRowSelected" OnRowDeselected="AuthorizedServiceDefinitionGrid_OnRowDeselected" />
                            
                            </ClientSettings>
                       
                        </Telerik:RadGrid>
                        
                    </div>
                    
                    <div class="PropertyPageSectionTitle">Add Criteria</div>
                                      
                    <div id="AddDefinitionDiv" style="clear: both" runat="server">
                      
                        <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                                                    
                            <div style="position: relative; float: right"><asp:Button ID="ButtonUpdateDefinition" Text="Update Selected" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddUpdateDefinition_OnClick" Enabled="false" runat="server" /></div>
                        
                            <div style="position: relative; float: right"><asp:Button ID="ButtonAddDefinition" Text="Add Criteria" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" OnClick="ButtonAddUpdateDefinition_OnClick" runat="server" /></div>
                            
                        </div>
                        
                        <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%; height: 190px; border: solid 1px #DDDDDD; overflow: auto;">
                        
                            <Telerik:RadMultiPage ID="ServiceTypeCriteria" SelectedIndex="0" runat="server">

                                <Telerik:RadPageView ID="AllMedical" runat="server">
                                
                                    <div style="margin: 2px 6px 2px 6px; padding: 4px; line-height: 150%;">
                                    
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                                <td style="width: 15%">Category:</td>
                                            
                                                <td><Telerik:RadComboBox ID="AuthorizationCategory" OnSelectedIndexChanged="AuthorizationCategory_OnSelectedIndexChanged" Width="100%" AutoPostBack="true" runat="server" /></td>
                                        
                                            </tr><tr>
                                        
                                                <td style="width: 15%">Subcategory:</td>
                                            
                                                <td><Telerik:RadComboBox ID="AuthorizationSubcategory" OnSelectedIndexChanged="AuthorizationSubcategory_OnSelectedIndexChanged" Width="100%" AutoPostBack="true" runat="server" /></td>
                                            
                                            </tr><tr>
                                        
                                                <td style="width: 15%">Service Type:</td>
                                            
                                                <td><Telerik:RadComboBox ID="AuthorizationServiceType" Width="100%" runat="server" /></td>
                                            
                                        </tr></table>
                                                                            
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>

                                            <td width="15%">Principal Diagnosis:</td>

                                            <td><Telerik:RadTextBox ID="CriteriaPrincipalDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>

                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaPrincipalDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                            </tr><tr>
                                    
                                            <td style="width: 15%">Diagnosis Codes:</td>
                                        
                                            <td><Telerik:RadTextBox ID="CriteriaDiagnosisCodes" Width="99%" runat="server"></Telerik:RadTextBox></td>
                                            
                                            <td width="08%">Version:</td>

                                            <td width="05%">
                                            
                                                <Telerik:RadComboBox ID="CriteriaDiagnosisVersion" Width="99%" runat="server">

                                                    <Items>
                                                    
                                                        <Telerik:RadComboBoxItem Text="9" Value="9" Selected="true" />

                                                        <Telerik:RadComboBoxItem Text="10" Value="10" />
                                                    
                                                    </Items>
                                                
                                                </Telerik:RadComboBox>
                                                
                                            </td>
                                            
                                        </tr></table>
                                        
                                        <table width="100%" cellspacing="0" cellpadding="4"><tr>
                                        
                                                <td style="width: 15%">Revenue Codes:</td>
                                            
                                                <td><Telerik:RadTextBox ID="CriteriaRevenueCodes" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                        
                                            </tr><tr>
                                        
                                                <td style="width: 15%">Procedure Codes:</td>
                                            
                                                <td><Telerik:RadTextBox ID="CriteriaProcedureCodes" Width="100%" runat="server"></Telerik:RadTextBox></td>
                                            
                                        </tr></table>
                                                                                
                                        <div style="width: 95%; padding: 4px; overflow: hidden; display: none">

                                            <span style="float: left;  width: 20%">Provider Specialties:</span>
                                        
                                            <span style="float: right; width: 80%"><Telerik:RadTextBox ID="CriteriaAllMedicalProviderSpecialties" Width="99%" runat="server"></Telerik:RadTextBox></span>
                                            
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
                    
                        <div style="float: left; height: 32px; width: 40px;"><img src="/Images/Common32/Document2Preview.png" alt="Authorized Service Preview" /></div>
                        
                        <div style="float: left; height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Authorized Service Preview</div>
                    
                    </div>

                </div>            
                        
                <div style="clear: both; margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                
                    <Telerik:RadGrid ID="AuthorizedServicePreviewGrid" Height="480" AutoGenerateColumns="false" runat="server">
                    
                        <MasterTableView Width="99%" TableLayout="Auto">
                        
                            <Columns>
                        
                                <Telerik:GridBoundColumn DataField="AuthorizedServiceDefinitionId" UniqueName="AuthorizedServiceDefinitionId" HeaderText="Definition Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />
                                
                                <Telerik:GridBoundColumn DataField="AuthorizationId" UniqueName="AuthorizationId" HeaderText="Authorization Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationNumber" UniqueName="AuthorizationNumber" HeaderText="Authorization Number" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationLine" UniqueName="AuthorizationLine" HeaderText="Line" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="MemberId" UniqueName="MemberId" HeaderText="Member Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ReferringProviderId" UniqueName="ReferringProviderId" HeaderText="Referring Provider Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceProviderId" UniqueName="ServiceProviderId" HeaderText="Service Provider Id" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationCategory" UniqueName="AuthorizationCategory" HeaderText="Category"  ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationSubcategory" UniqueName="AuthorizationSubcategory" HeaderText="Subcategory" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationServiceType" UniqueName="AuthorizationServiceType" HeaderText="Service Type"  ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="AuthorizationStatus" UniqueName="AuthorizationStatus" HeaderText="Status" ReadOnly="true" Visible="true" />


                                <Telerik:GridBoundColumn DataField="ReceivedDate" UniqueName="ReceivedDate" HeaderText="ReceivedDate"  ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ReferralDate" UniqueName="ReferralDate" HeaderText="ReferralDate" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="EffectiveDate" UniqueName="EffectiveDate" HeaderText="EffectiveDate" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="TerminationDate" UniqueName="TerminationDate" HeaderText="TerminationDate" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ServiceDate" UniqueName="ServiceDate" HeaderText="ServiceDate" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="PrincipalDiagnosisCode" UniqueName="PrincipalDiagnosisCode" HeaderText="PrincipalDiagnosisCode" ReadOnly="true" Visible="true" />
                                
                                <Telerik:GridBoundColumn DataField="PrincipalDiagnosisVersion" UniqueName="PrincipalDiagnosisVersion" HeaderText="PrincipalDiagnosisVersion" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="DiagnosisCode" UniqueName="DiagnosisCode" HeaderText="DiagnosisCode" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="DiagnosisVersion" UniqueName="DiagnosisVersion" HeaderText="DiagnosisVersion" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="RevenueCode" UniqueName="RevenueCode" HeaderText="RevenueCode" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ProcedureCode" UniqueName="ProcedureCode" HeaderText="ProcedureCode" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="ModifierCode" UniqueName="ModifierCode" HeaderText="ModifierCode" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="NdcCode" UniqueName="NdcCode" HeaderText="NdcCode" ReadOnly="true" Visible="true" />

                                <Telerik:GridBoundColumn DataField="SpecialtyName" UniqueName="SpecialtyName" HeaderText="SpecialtyName" ReadOnly="true" Visible="true" />

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
                
                    <span style="float: left;">Warning: Retreiving authorizations may take time.</span>
                        
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
 
 
 <Telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
 
<script type="text/javascript">

    function AuthorizedServiceDefinitionGrid_OnRowSelected(sender, eventArgs) {

        document.getElementsByName("ButtonUpdateDefinition")[0].disabled = false;

        return;

    }

    function AuthorizedServiceDefinitionGrid_OnRowDeselected(sender, eventArgs) {

        document.getElementsByName("ButtonUpdateDefinition")[0].disabled = true;

        return;

    }

</script>

</Telerik:RadCodeBlock>


</form>
    
</body>

</html>