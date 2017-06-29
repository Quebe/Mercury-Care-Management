<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="WorkQueueMonitor.aspx.cs" Inherits="Mercury.Web.Application.Work.WorkQueueMonitor" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>
   
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>

        <Telerik:AjaxSetting AjaxControlID="WorkQueueMonitorSummaryGrid">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSummaryGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
            
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueAgingName" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueAgingChartAsp" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>

        <Telerik:AjaxSetting AjaxControlID="WorkQueueSummaryRefresh">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSummaryRefresh" LoadingPanelID="AjaxLoadingPanelWhiteout" />

                <Telerik:AjaxUpdatedControl ControlID="WorkQueueSummaryGridSplitter" LoadingPanelID="AjaxLoadingPanel" />
            
            
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueAgingName" LoadingPanelID="AjaxLoadingPanelWhiteout" />
                
                <Telerik:AjaxUpdatedControl ControlID="WorkQueueAgingChartAsp" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
                      
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="WorkQueueMonitorContent" style="padding: .125in">

<!-- WORK QUEUE SUMMARY (BEGIN) -->

    <div id="WorkQueueSummarySection" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in" runat="server">
            
        <table id="WorkQueueSummary_TitleTable" width="100%" cellpadding="0" cellspacing="0" style="padding-bottom: .125in;">
        
            <tr>

                <td style="white-space: nowrap;">Work Queue Monitor</td>

                <td></td>

                <td colspan="2" style="text-align: right">
                
                    <asp:LinkButton ID="WorkQueueSummaryRefresh" class="NoDecoration ColorComplementDarker HoverTextBlack" OnClick="WorkQueueSummaryRefresh_OnClick" style="white-space: nowrap" runat="server">(refresh)</asp:LinkButton>
                
                </td>

            </tr>        
            
        </table>

        <Telerik:RadSplitter ID="WorkQueueSummaryGridSplitter" Orientation="Horizontal" Width="100%" runat="server">

            <Telerik:RadPane ID="WorkQueueSummaryGridSplitterPane" Scrolling="None" Width="100%" Height="200" runat="server">

                <Telerik:RadGrid ID="WorkQueueMonitorSummaryGrid" Width="100%" Height="100%" AutoGenerateColumns="false"

                    OnSelectedIndexChanged="WorkQueueMonitorSummaryGrid_OnSelectedIndexChanged"
            
                    runat="server">

                    <MasterTableView DataKeyNames="Id,Name">

                        <Columns>

                            <Telerik:GridButtonColumn Text="Select" CommandName="Select" HeaderText="" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" ItemStyle-Width="50" Visible="true" />

                            <Telerik:GridBoundColumn DataField="Id" Visible="false" />

                            <Telerik:GridBoundColumn DataField="Name" HeaderText="Work Queue Name" Visible="true" />
                        
                            <Telerik:GridBoundColumn DataField="FirstWorkedTime"     HeaderText="First" DataFormatString="{0:hh:mm:ss tt}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" Visible="true" />

                            <Telerik:GridBoundColumn DataField="LastWorkedTime"      HeaderText="Last"  DataFormatString="{0:hh:mm:ss tt}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80" ItemStyle-Width="80" Visible="true" />

                            <Telerik:GridBoundColumn DataField="WorkedItemsCount"    HeaderText="Worked"    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65" ItemStyle-Width="65" Visible="true" />

                            <Telerik:GridBoundColumn DataField="CompletedItemsCount" HeaderText="Completed" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70" ItemStyle-Width="70" Visible="true" />

                            <Telerik:GridBoundColumn DataField="AvailableItemsCount" HeaderText="Available" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65" ItemStyle-Width="65" Visible="true" />

                            <Telerik:GridBoundColumn DataField="TotalItemsCount"     HeaderText="Open"     HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65" ItemStyle-Width="65" Visible="true" />
                        
                            <Telerik:GridBoundColumn DataField="WarningItemsCount"   HeaderText="Warning"     HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65" ItemStyle-Width="65" Visible="true" />

                            <Telerik:GridBoundColumn DataField="OverdueItemsCount"   HeaderText="Overdue"     HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="65" ItemStyle-Width="65" Visible="true" />

                            <Telerik:GridBoundColumn DataField="UsersInQueueCount"   HeaderText="Users"     HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" ItemStyle-Width="50" Visible="true" />
                    
                        </Columns>
                
                    </MasterTableView>
            
                        <ClientSettings EnableRowHoverStyle="true">          

                            <Selecting AllowRowSelect="true" />

                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                
                        </ClientSettings>

                </Telerik:RadGrid>

            </Telerik:RadPane>

        </Telerik:RadSplitter>

    </div>

<!-- WORK QUEUE SUMMARY ( END ) -->

    <table id="WorkQueueMonitorCharts" width="100%" cellpadding="0" cellspacing="0" style="padding-top: .125in; padding-bottom: .125in;"><tr>

        <td valign="top" style="width: 50%;">
        
            <div id="Div1" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in" runat="server">
            
                <table id="Table2" width="100%" cellpadding="0" cellspacing="0" style="padding-bottom: .125in;">
        
                    <tr>

                        <td style="white-space: nowrap; font-weight: bold">Item Aging (Available/Open): &nbsp </td>
                        
                        <td style="width: 100%;"><asp:Label ID="WorkQueueAgingName" runat="server"></asp:Label></td>

                    </tr>        
            
                </table>

                <!-- CHART SERIES DEFINED IN CODE -->
                
                <asp:Chart ID="WorkQueueAgingChartAsp" Width="800" runat="server">

                    <Series>
                    
                        <asp:Series Name="AvailableSeries" ChartType="Column" ChartArea="ChartArea1" 
                        
                            CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" 
                            
                            IsValueShownAsLabel="True">
                        
                        </asp:Series>

                        <asp:Series Name="OpenSeries" ChartType="Column" ChartArea="ChartArea1"

                            CustomProperties="DrawingStyle=Cylinder, MaxPixelPointWidth=50" ShadowOffset="2" 
                            
                            IsValueShownAsLabel="True">
                        
                        </asp:Series>

                    </Series>

                    <ChartAreas>

                        <asp:ChartArea Name="ChartArea1" BackGradientStyle="TopBottom" BackSecondaryColor="#B6D6EC" BorderDashStyle="Solid" BorderWidth="2">

                            <AxisX>

                                <LabelStyle Enabled="true" Angle="-90" Interval="1" />

                                <MajorGrid Enabled="true" />
                                
                                <MinorGrid Enabled="true" />

                            </AxisX>

                            <AxisY>

                                <ScaleBreakStyle BreakLineStyle="Wave" Enabled="true" MaxNumberOfBreaks="1" />
                            
                                <MajorGrid Enabled="true" />
                            
                            </AxisY>

                        </asp:ChartArea>

                    </ChartAreas>

                </asp:Chart>        

            </div>

        </td>        

    </tr></table>

</div>

<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', WorkQueueMonitor_Body_OnResize, false); } else { window.attachEvent('onresize', WorkQueueMonitor_Body_OnResize); }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isWorkQueueMonitorPainting = false;

    setTimeout('WorkQueueMonitor_OnPaint()', 500);


    function WorkQueueMonitor_OnPaint(forEvent) {

        if (isWorkQueueMonitorPainting) { return; }

        isWorkQueueMonitorPainting = true;


        var availableHeight = 0;

        var availableWidth = 0;


        var container = document.getElementById("<%= WorkQueueSummarySection.ClientID %>");

        var splitter = $find("<%= WorkQueueSummaryGridSplitter.ClientID %>");

        if ((container == null) || (splitter == null)) {

            isWorkQueueMonitorPainting = false;

            setTimeout('WorkQueueMonitor_OnPaint ()', 100);

            return;

        }


        availableWidth = container.offsetWidth - container.offsetLeft * 2;

        if (availableWidth < 0) { availableWidth = 0; }

        splitter.set_width(availableWidth + "px");

        
        var chartdiv = document.getElementById("<%= WorkQueueAgingChartAsp.ClientID %>");

        if (chartdiv != null) {

            chartdiv.style.width = "100%";

//            var chartimg = chartdiv.getElementsByTagName("img")[0];

//            chartdiv.style.width = "100%";

//            chartimg.style.width = "100%";

//            chartimg.style.height = "100%";

        }


        isWorkQueueMonitorPainting = false;

        return;

    }


    function WorkQueueMonitor_Body_OnResize(forEvent) {

        WorkQueueMonitor_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>


</asp:Content>
