<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Metric.aspx.cs" Inherits="Mercury.Web.Application.Configuration.PropertyPages.Metric" %>

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

<form id="FormMetric" runat="server">

<div style="display: none"><asp:TextBox ID="PageInstanceId" Text="" runat="server" /></div>

<div id="AjaxManagerDiv" style="display: none">

    <asp:ScriptManager ID="AjaxScriptManager" runat="Server" />
    
    <Telerik:RadAjaxManager ID="AjaxManager" runat="server">
    
        <AjaxSettings>
        
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
                
            <Telerik:RadTab Text="Cost Definition"></Telerik:RadTab>
                
        </Tabs>
                  
    </Telerik:RadTabStrip>
    
    <div style="height: 600px; overflow: auto; border: 1px solid black;">

        <Telerik:RadMultiPage ID="PropertiesContent" SelectedIndex="0" runat="server">
        
            <Telerik:RadPageView ID="General" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">

                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Metric.png" alt="Metric Properties" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">General Properties of the Metric</td>
                    
                    </tr></table>
                                       
                    <div class="PropertyPageSectionTitle">Name and Description</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 20%; padding: 4px;">Name:</div>
                    
                        <div style="position: relative; float: left; width: 75%; padding: 4px;"><Telerik:RadTextBox ID="MetricName" Width="100%" MaxLength="60" EmptyMessage="(required)" runat="server" /></div>
                    
                        <div style="clear: both;"></div>
                        
                        <div style="padding: 4px;">Description:</div>
                    
                        <div style="width: 100%; padding: 4px;"><Telerik:RadTextBox ID="MetricDescription" Width="98%" Rows="10" TextMode="MultiLine" EmptyMessage="(required)" runat="server" /></div>

                    </div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 25%; padding: 4px;">Type:</div>
                        
                            <div style="position: relative; float: left; width: 60%; padding: 4px;">
            
                                <Telerik:RadComboBox ID="MetricType" Width="99%" OnClientSelectedIndexChanged="MetricType_OnClientSelectedIndexChanged" runat="server">
                                
                                    <Items> 

                                        <Telerik:RadComboBoxItem Value="0" Text="Health" runat="server" />
                                        
                                        <Telerik:RadComboBoxItem Value="1" Text="Cost" runat="server" />
                                    
                                        <Telerik:RadComboBoxItem Value="2" Text="Utilization" Visible="false" runat="server" />
                                        
                                    </Items>
                                    
                                </Telerik:RadComboBox>
                                
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 20%; padding: 4px;">Data:</div>
                        
                            <div style="position: relative; float: left; width: 60%; padding: 4px;">
            
                                <Telerik:RadComboBox ID="MetricDataType" Width="99%" runat="server">
                                
                                    <Items> 

                                        <Telerik:RadComboBoxItem Value="0" Text="Decimal" runat="server" />
                                    
                                        <Telerik:RadComboBoxItem Value="1" Text="Integer" runat="server" />
                                        
                                    </Items>
                                    
                                </Telerik:RadComboBox>
                                
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;">Minimum:</div>
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadNumericTextBox ID="MetricMinimumValue" Width="99%" runat="server" /></div>

                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;">Maximum:</div>
                        
                            <div style="position: relative; float: left; width: 40%; padding: 4px;"><Telerik:RadNumericTextBox ID="MetricMaximumValue" Width="99%" runat="server" /></div>

                        </div>

                    </div>
                    
                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">

                        <div style="clear: both"></div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="MetricEnabled" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Enabled</div>
                        
                        </div>
                        
                        <div style="position: relative; float: left; width: 25%">
                        
                            <div style="position: relative; float: left; width: 12%; padding: 4px;"><asp:CheckBox ID="MetricVisible" runat="server" /></div>

                            <div style="position: relative; float: left; width: 15%; padding: 4px;">Visible</div>
                        
                        </div>
                        
                    </div>
                
                    <div class="PropertyPageSectionTitle">Created and Modified Information</div>

                    <div style="margin: 2px 10px 2px 10px; padding: 4px; line-height: 150%;">
                    
                        <div style="position: relative; float: left; width: 50%">
                        
                            <div style="width: 99%; padding: 4px; text-align: center">Created Information</div>
                        

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricCreateAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricCreateAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>

                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricCreateAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                            
                            </div>

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                                
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="MetricCreateDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                            
                            </div>

                        </div>

                        <div style="position: relative; float: left; width: 50%">

                            <div style="width: 99%; padding: 4px; text-align: center">Modified Information</div>


                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Authority:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricModifiedAuthorityName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div> 
                            
                            
                            <div style="overflow: hidden;">
    
                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Account Id:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricModifiedAccountId" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>


                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Name:</div>
                        
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadTextBox ID="MetricModifiedAccountName" Width="100%" TabIndex="-1" ReadOnly="true" runat="server"></Telerik:RadTextBox></div>
                                
                            </div>
                            

                            <div style="overflow: hidden;">

                                <div style="position: relative; float: left; width: 30%; padding: 4px;">Date:</div>
                            
                                <div style="position: relative; float: left; width: 60%; padding: 4px;"><Telerik:RadDateInput ID="MetricModifiedDate" DateFormat="MM/dd/yyyy" Width="100%" TabIndex="-1" ReadOnly="true" runat="server" /></div>
                                
                            </div>
                            
                          </div>

                    <div>
                    
                    </div>
                
                </div>
                
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

                </tr></table>

            </Telerik:RadPageView>    
            
            <Telerik:RadPageView ID="CostMetricDefinitionPage" runat="server">
            
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 600px;  min-height: 600px;"><tr><td valign="top">
                
                    <table cellpadding="0" cellspacing="0" style="width: 100%; margin: .125in;"><tr>
                    
                        <td style="height: 32px; width: 40px;"><img src="/Images/Common32/Metric.png" alt="Cost Metric Definition" /></td>
                        
                        <td style="height: 32px; margin-top: 7px; color: Navy; font-size: 12pt;">Cost Metric Definition</td>
                    
                    </tr></table>
   
                    <div class="PropertyPageSectionTitle">Definition</div>
                      
                    <table style="margin: 0px 10px 0px 10px; padding: 4px; line-height: 150%;"><tr>
                    
                        <td style="width: 16%; padding: 4px;">Cost Data Source:</td>
                    
                        <td style="width: 45%; padding: 4px;">
        
                            <Telerik:RadComboBox ID="MetricCostDataSource" Width="99%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Total by All Claims (paid/reversed)" Selected="true" runat="server" />

                                    <Telerik:RadComboBoxItem Value="1" Text="Total by All Medical Claims (paid/reversed)" Selected="true" runat="server" />

                                    <Telerik:RadComboBoxItem Value="2" Text="Total by All Pharmacy Claims (paid/reversed)" Selected="true" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="3" Text="Total by Selected Services (reserved for future use)" Enabled="false" runat="server" />
                                                                   
                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td style="width: 11%; padding: 4px;">Claim Date:</td>
                    
                        <td style="width: 19%; padding: 4px;">
        
                            <Telerik:RadComboBox ID="MetricCostClaimDateType" Width="99%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Paid Date" Selected="true" runat="server" />

                                    <Telerik:RadComboBoxItem Value="1" Text="Service Date (From)" Selected="true" runat="server" />
                                                                   
                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>

                    </tr></table>
                    
                    <table style="margin: 0px 10px 0px 10px; padding: 4px; line-height: 150%;"><tr>
                    
                        <td style="width: 20%; padding: 4px;">Reporting Period:</td>
                    
                        <td style="width: 25%; padding: 4px;">
        
                            <Telerik:RadComboBox ID="MetricCostReportingPeriod" OnClientSelectedIndexChanged="MetricCostReportingPeriod_OnClientSelectedIndexChanged" Width="99%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Calendar Year" Selected="true" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Year from Start Month" runat="server" />
                                                                   
                                    <Telerik:RadComboBoxItem Value="2" Text="Rolling Period" runat="server" />

                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>

                        <td style="width: 10%; padding: 4px;">Value:</td>
                        
                        <td style="width: 10%; padding: 4px;"><Telerik:RadNumericTextBox ID="MetricCostReportingPeriodValue" MaxValue="-1" NumberFormat-DecimalDigits="0" Width="99%" runat="server" /></td>

                        <td style="width: 15%; padding: 4px;">
        
                            <Telerik:RadComboBox ID="MetricCostReportingPeriodQualifier" Width="99%" runat="server">
        
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>

                        <td>&nbsp</td>
                        
                    </tr></table>
                    
                    <table style="margin: 0px 10px 0px 10px; padding: 4px; line-height: 150%;"><tr>
                    
                        <td style="width: 20%; padding: 4px;">Watermark:</td>
                    
                        <td style="width: 25%; padding: 4px;">
        
                            <Telerik:RadComboBox ID="MetricCostWatermarkPeriod" OnClientSelectedIndexChanged="MetricCostWatermark_OnClientSelectedIndexChanged" Width="99%" runat="server">
                            
                                <Items> 

                                    <Telerik:RadComboBoxItem Value="0" Text="Calendar Year" runat="server" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Year from Start Month"  runat="server" />
                                                                   
                                    <Telerik:RadComboBoxItem Value="2" Text="Month" runat="server" Selected="true" />

                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td style="width: 10%; padding: 4px;">Value:</td>
                        
                        <td style="width: 10%; padding: 4px;"><Telerik:RadNumericTextBox ID="MetricCostWatermarkPeriodValue" MaxValue="-1" NumberFormat-DecimalDigits="0" Width="99%" Enabled="false" runat="server" /></td>
       
                        <td style="width: 15%; padding: 4px; display: none;">
        
                            <Telerik:RadComboBox ID="MetricCostWatermarkPeriodQualifier" Width="99%" Enabled="false" runat="server">
        
                                <Items>
                                
                                    <Telerik:RadComboBoxItem Value="0" Text="Days" />
                                    
                                    <Telerik:RadComboBoxItem Value="1" Text="Months" Selected="true" />
                                    
                                    <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                
                                </Items>
                                
                            </Telerik:RadComboBox>
                            
                        </td>
                        
                        <td>&nbsp</td>

                    </tr></table>
            
                </td>
                
                <td class="BackgroundColorNormal BorderColorLight" style="width:33%;"></td>

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
 
 
<script type="text/javascript"> 

//<![CDATA[

    function MetricType_OnClientSelectedIndexChanged(sender, eventArgs) {

        var selectedItem = eventArgs.get_item();

        var selectedValue = selectedItem.get_text();


        var tabStrip = $find("<%= PropertiesTab.ClientID %>");

        var generalTab = tabStrip.findTabByText("General");

        var costTab = tabStrip.findTabByText("Cost Definition")


        var metricDataType = $find("<%= MetricDataType.ClientID %>");

        var metricMinimumValue = $find("<%= MetricMinimumValue.ClientID %>");

        var metricMaximumValue = $find("<%= MetricMaximumValue.ClientID %>");

        switch (selectedValue) {

            case "Cost":

                costTab.set_enabled(true);

                metricDataType.disable();

                metricMinimumValue.disable();

                metricMaximumValue.disable();

                break;

            default:

                costTab.set_enabled(false);

                metricDataType.enable();

                metricMinimumValue.enable();

                metricMaximumValue.enable();

                break;

        }

        return;

    }

    function MetricCostReportingPeriod_OnClientSelectedIndexChanged(sender, eventArgs) {

        var selectedItem = eventArgs.get_item();

        var selectedValue = selectedItem.get_text();


        var metricCostReportingPeriodValue = $find("<%= MetricCostReportingPeriodValue.ClientID %>");

        var metricCostReportingPeriodDateQualifier = $find("<%= MetricCostReportingPeriodQualifier.ClientID %>");


        switch (selectedValue) {

            case "Calendar Year":

                metricCostReportingPeriodValue.disable();

                metricCostReportingPeriodDateQualifier.disable();

                break;

            case "Year from Start Month":

                metricCostReportingPeriodValue.enable();

                metricCostReportingPeriodValue.set_minValue(1);

                metricCostReportingPeriodValue.set_maxValue(12);

                metricCostReportingPeriodValue.set_value(metricCostReportingPeriodValue.get_value());

                metricCostReportingPeriodDateQualifier.disable();

                break;

            case "Rolling Period":

                metricCostReportingPeriodValue.enable();

                metricCostReportingPeriodValue.set_minValue(-9999);

                metricCostReportingPeriodValue.set_maxValue(-1);

                metricCostReportingPeriodValue.set_value(metricCostReportingPeriodValue.get_value());

                metricCostReportingPeriodDateQualifier.enable();

                break;

        }

        return;

    }

    function MetricCostWatermark_OnClientSelectedIndexChanged(sender, eventArgs) {

        var selectedItem = eventArgs.get_item();

        var selectedValue = selectedItem.get_text();


        var metricCostWatermarkPeriodValue = $find("<%= MetricCostWatermarkPeriodValue.ClientID %>");

        var metricCostWatermarkPeriodDateQualifier = $find("<%= MetricCostWatermarkPeriodQualifier.ClientID %>");


        switch (selectedValue) {

            case "Calendar Year":

                metricCostWatermarkPeriodValue.disable();

                metricCostWatermarkPeriodDateQualifier.disable();

                break;

            case "Year from Start Month":

                metricCostWatermarkPeriodValue.enable();

                metricCostWatermarkPeriodValue.set_minValue(1);

                metricCostWatermarkPeriodValue.set_maxValue(12);

                metricCostWatermarkPeriodValue.set_value(metricCostWatermarkPeriodValue.get_value());

                metricCostWatermarkPeriodDateQualifier.disable();

                break;

            case "Month":

                metricCostWatermarkPeriodValue.disable();

                metricCostWatermarkPeriodDateQualifier.disable();

                break;

        }

        return;

    }
    
    //]]>

</script>

 
</form>
    
</body>

</html>