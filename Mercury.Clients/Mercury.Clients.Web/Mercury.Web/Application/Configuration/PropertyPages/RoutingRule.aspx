<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoutingRule.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.RoutingRule" %>

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

<form id="FormRoutingRule" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
            <Telerik:AjaxSetting AjaxControlID="RoutingRuleInsurerSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleInsurerSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleProgramSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
        
            <Telerik:AjaxSetting AjaxControlID="RoutingRuleProgramSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleProgramSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleBenefitPlanSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="RoutingRuleStateSelection">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleStateSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleCitySelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRuleCountySelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="ButtonAddRule">
            
                <UpdatedControls>
                
                    <Telerik:AjaxUpdatedControl ControlID="ButtonAddRule" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRulesGrid" LoadingPanelID="AjaxLoadingPanel" />
                
                    <Telerik:AjaxUpdatedControl ControlID="SaveResponseLabel" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                    
                </UpdatedControls>
            
            </Telerik:AjaxSetting>
            
            <Telerik:AjaxSetting AjaxControlID="RoutingRulesGrid">
            
                <UpdatedControls>
                    
                    <Telerik:AjaxUpdatedControl ControlID="RoutingRulesGrid" LoadingPanelID="AjaxLoadingPanel" />
                
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
                
            <Telerik:RadTab Text="Rules"></Telerik:RadTab>

        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/RoutingRule.png" alt="Routing Rule Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Routing Rule</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                    
                        
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Description:</div>
                    
                        <div style="position: relative; float: left; width: 100%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleDescription" Width="98%" Rows="13" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="clear: both"></div>
                       
                        <div style="position: relative; float: left; width: 15%; padding: 4px;">Default Queue:</div>
                    
                        <div style="position: relative; float: left; width: 80%; padding: 4px;"><Telerik:RadComboBox ID="RoutingRuleDefaultWorkQueue" Width="99%" runat="server" /></div>
                    
                    </div>
                    
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="RoutingRuleEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="RoutingRuleVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="RoutingRuleCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="RoutingRuleModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="RoutingRuleModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="PageTeams" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; overflow: hidden;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/RoutingRule.png" alt="Routing Rule Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Routing Rules</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Current Rules</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <Telerik:RadGrid ID="RoutingRulesGrid" Height="230" AutoGenerateColumns="false" OnDeleteCommand="RoutingRulesGrid_OnDeleteCommand" OnItemCommand="RoutingRulesGrid_OnItemCommand" runat="server">
                    
                            <MasterTableView Width="100%" DataKeyNames="Sequence" TableLayout="Auto">
                            
                                <Columns>
                            
                                    <Telerik:GridBoundColumn DataField="Sequence" UniqueName="Sequence" HeaderText="Sequence" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="Rule" UniqueName="Rule" HeaderText="Rule" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridBoundColumn DataField="WorkQueue" UniqueName="WorkQueue" HeaderText="Work Queue" ReadOnly="true" Visible="true" />
                                    
                                    <Telerik:GridButtonColumn CommandName="MoveUp" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Up" />
                                                                           
                                    <Telerik:GridButtonColumn CommandName="MoveDown" ButtonType="LinkButton" UniqueName="MoveEvent" Text="Down" />
                                                                                                                                                      
                                    <Telerik:GridButtonColumn CommandName="Delete" ButtonType="LinkButton" ConfirmText="Are you sure you want to remove this Routing Rule?" UniqueName="DeleteEvent" Text="Delete" />
                                                                           
                                </Columns>
                            
                            </MasterTableView>
                            
                            <ClientSettings>
                            
                                <Selecting AllowRowSelect="true" />
                                
                                <Scrolling AllowScroll="true" />
                            
                            </ClientSettings>
                        
                        </Telerik:RadGrid>
                    
                    </div>            
                                        
                    <div class="PropertyPageSectionTitle">Add a New Rule</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">Insurer:</div>
                        
                        <div style="position: relative; float: left; width: 30%"><Telerik:RadComboBox ID="RoutingRuleInsurerSelection" OnSelectedIndexChanged="RoutingRuleInsurerSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="99%" runat="server"></Telerik:RadComboBox></div>
                   
                        <div style="position: relative; float: left; width: 05%">&nbsp</div>
                   
                        <div style="position: relative; float: left; width: 15%">Program:</div>
                        
                        <div style="position: relative; float: left; width: 35%"><Telerik:RadComboBox ID="RoutingRuleProgramSelection" OnSelectedIndexChanged="RoutingRuleProgramSelection_OnSelectedIndexChanged" AutoPostBack="true" Width="99%" runat="server"></Telerik:RadComboBox></div>
                                       
                    </div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">Benefit Plan:</div>
                        
                        <div style="position: relative; float: left; width: 80%"><Telerik:RadComboBox ID="RoutingRuleBenefitPlanSelection" Width="99%" runat="server"></Telerik:RadComboBox></div>
                        
                    </div>
        
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%; border-bottom: solid 1px black"></div>
                                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="position: relative; float: left; width: 15%">Gender:</div>
                        
                        <div style="position: relative; float: left; width: 15%">
                        
                            <Telerik:RadComboBox ID="RoutingRuleGender" Width="99%" runat="server">
                            
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Both" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Female" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Male" />
                                
                                </Items>
                            
                            </Telerik:RadComboBox>
                    
                        </div>
                    
                        <div style="position: relative; float: right; width: 60%">
                        
                            <div style="position: relative; float: left; width: 20%">Age Minimum:</div>
                            
                            <div style="position: relative; float: left; width: 10%">
                            
                                <Telerik:RadNumericTextBox ID="RoutingRuleAgeMinimum" Width="99%" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" runat="server" />

                            </div>

                            <div style="position: relative; float: left; padding-left: 8px; width: 20%">Maximum:</div>
                            
                            <div style="position: relative; float: left; width: 10%">
                            
                                <Telerik:RadNumericTextBox ID="RoutingRuleAgeMaximum" Width="99%" MinValue="0" MaxValue="199" NumberFormat-DecimalDigits="0" runat="server" />

                            </div>
                            
                            <div style="position: relative; float: left; width: 5%;">&nbsp</div>
                            
                            <div style="position: relative; float: left; width: 15%;">
                            
                                <asp:CheckBox ID="RoutingRuleAgeInMonths" Checked="false" Text="In Months" runat="server" />

                            </div>

                        </div>
                   
                    </div>

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">Ethnicity:</div>
                        
                        <div style="position: relative; float: left; width: 35%"><Telerik:RadComboBox id="RoutingRuleEthnicitySelection" Width="98%" Height="500" runat="server" /></div>
                   
                        <div style="position: relative; float: left; width: 15%">Language:</div>
                        
                        <div style="position: relative; float: left; width: 35%"><Telerik:RadComboBox id="RoutingRuleLanguageSelection" Width="98%" Height="500" runat="server" /></div>
                   
                    </div>

                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%; border-bottom: solid 1px black"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">State:</div>
                        
                        <div style="position: relative; float: left; width: 15%">
                        
                            <Telerik:RadComboBox ID="RoutingRuleStateSelection" MarkFirstMatch="true" OnSelectedIndexChanged="RoutingRuleStateSelection_OnSelectedIndexChanged" Height="500" AutoPostBack="true" Width="99%" runat="server"></Telerik:RadComboBox>
                    
                        </div>
                   
                        <div style="position: relative; float: left; width: 02%">&nbsp</div>
                   
                        <div style="position: relative; float: left; width: 10%">City:</div>
                        
                        <div style="position: relative; float: left; width: 40%">
                        
                            <Telerik:RadComboBox ID="RoutingRuleCitySelection" MarkFirstMatch="true" Height="500" Width="99%" runat="server"></Telerik:RadComboBox>
                    
                        </div>
                   
                    </div>
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%"></div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%">County:</div>
                        
                        <div style="position: relative; float: left; width: 40%">
                        
                            <Telerik:RadComboBox ID="RoutingRuleCountySelection" MarkFirstMatch="true" Height="500" Width="99%" runat="server"></Telerik:RadComboBox>
                    
                        </div>
                   
                        <div style="position: relative; float: left; width: 04%">&nbsp</div>
                   
                        <div style="position: relative; float: left; width: 05%">Zip:</div>
                        
                        <div style="position: relative; float: left; width: 10%"><Telerik:RadTextBox ID="RoutingRuleZipCode" Width="99%" runat="server" MaxLength="5" /></div>
                        
                    </div>
                    
                    <div style="clear: both; margin: 2px 10px 2px 10px; padding: 1px; line-height: 150%; border-bottom: solid 1px black"></div>
                                      
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 15%;">Work Queue:</div>
                    
                        <div style="position: relative; float: left; width: 65%;"><Telerik:RadComboBox ID="RoutingRuleWorkQueue" Width="99%" runat="server" /></div>
                        
                        <div style="position: relative; float: left; width: 15%;"><asp:Button ID="ButtonAddRule" Text="Add Rule" OnClick="ButtonAddRule_OnClick" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="server" /></div>
                    
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
 
 
</form>
    
</body>

</html>