<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberMetrics.ascx.cs" Inherits="Mercury.Web.Application.Controls.MemberMetrics" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="MemberMetricToolbar" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberMetricsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberMetricsGrid" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberMetricsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="MemberMetricAction" ><UpdatedControls><Telerik:AjaxUpdatedControl ControlID="MemberMetricsGrid" LoadingPanelID="AjaxLoadingPanel" /></UpdatedControls></Telerik:AjaxSetting>

    </AjaxSettings>
    
</Telerik:RadAjaxManagerProxy>
    

<Telerik:RadGrid ID="MemberMetricsGrid" Height="368" AllowPaging="true" AllowCustomPaging="true" AutoGenerateColumns="false" EnableViewState="false"

    OnNeedDataSource="MemberMetricsGrid_OnNeedDataSource" 
    
    OnItemDataBound="MemberMetricsGrid_OnItemDataBound" 
    
    OnItemCreated="MemberMetricsGrid_OnItemCreated"
    
    OnItemCommand="MemberMetricsGrid_OnItemCommand"
    
    OnPageSizeChanged="MemberMetricsGrid_OnPageSizeChanged"
    
    runat="server">

    <MasterTableView Name="MemberMetricsView" TableLayout="Auto" CommandItemDisplay="Top" DataKeyNames="MemberMetricId">

        <CommandItemTemplate>
        
            <div>
                                         
                <Telerik:RadToolBar ID="MemberMetricToolbar" OnButtonClick="MemberMetricToolbar_OnButtonClick" EnableViewState="false" AutoPostBack="true" runat="server">
                    
                    <Items>
                    
                        <Telerik:RadToolBarButton Text="Show Hidden" CheckOnClick="true" AllowSelfUnCheck="true" Group="ShowHidden" Checked="false" PostBack="true"  />
                        
                        <Telerik:RadToolBarButton IsSeparator="true" />
                        
                        <Telerik:RadToolBarButton BorderStyle="None">
                        
                            <ItemTemplate>
                                                                    
                                <table cellpadding="0" cellspacing="0" border="0" style="border: none; padding: 0px"><tr>
                                
                                    <td style="width: 16px"><img src="/Images/Common16/MetricAdd.png" alt="Add Service" /></td>
                                    
                                    <td style="width: 65px;">Add Metric:</td>
                                    
                                    <td style="width: 300px"><Telerik:RadComboBox ID="MemberMetricSelection" Width="300" runat="server" /></td>
                                    
                                    <td style="width: 30px;">Date:</td>
                                    
                                    <td style="width: 70px"><Telerik:RadDateInput ID="MemberMetricEventDate" Width="70" runat="server" /></td>

                                    <td style="width: 30px;">Value:</td>
                                    
                                    <td style="width: 70px"><Telerik:RadNumericTextBox ID="MemberMetricValue" Width="70" runat="server" /></td>
                                    
                                    <td><asp:Button ID="MemberMetricAdd" Text="Add" CommandName="MemberMetricAdd" CommandArgument="0" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                                
                                </tr></table>
                            
                            </ItemTemplate>
                        
                        </Telerik:RadToolBarButton>                                             
                        
                    </Items>

                </Telerik:RadToolBar>
   
            </div>
    
        </CommandItemTemplate>
    
        <Columns>

            <Telerik:GridBoundColumn DataField="MemberMetricId" UniqueName="MemberMetricId" HeaderText="Member Metric Id" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="MetricName" UniqueName="MetricName" HeaderText="Name" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="MetricType" UniqueName="MetricType" HeaderText="Type" ReadOnly="true" Visible="true" />
            
            <Telerik:GridBoundColumn DataField="MetricValue" UniqueName="MetricValue" HeaderText="Value" ReadOnly="true" Visible="true" />
        
            <Telerik:GridBoundColumn DataField="EventDate" UniqueName="EventDate" HeaderText="Event Date" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="AddedManually" UniqueName="AddedManually" HeaderText="Manual" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateAccountName" UniqueName="CreateAccountName" HeaderText="Created By" ReadOnly="true" Visible="true" />

            <Telerik:GridBoundColumn DataField="CreateDate" UniqueName="CreateDate" HeaderText="Create Date" ReadOnly="true" Visible="true" />

        </Columns>
         
    </MasterTableView>
    
    <ClientSettings>
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="true" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>


<div style="display: none">

    <asp:TextBox ID="MemberMetricAction_MemberMetricId" runat="server" />
    
    <asp:TextBox ID="MemberMetricAction_CommandName" Text="No Command" runat="server" />
    
    <asp:TextBox ID="MemberMetricAction_Arguments" runat="server" />
    
    <asp:Button  ID="MemberMetricAction" OnClick="MemberMetricAction_OnClick" runat="server" />
    
</div>


<div id="Div1" style="display: none;" runat="server">

<script type="text/javascript">

    function MemberMetrics_MemberMetric_OnRemoveManual_<%= MemberMetricAction_MemberMetricId.ClientID.Replace ('.', '_') %> (memberMetricId, metricName, eventDate) {

        var userConfirmed = confirm("Remove Manual Metric: " + metricName + " on " + eventDate + "?");

        if (userConfirmed) {

            document.getElementById("<%= MemberMetricAction_MemberMetricId.ClientID %>").value = memberMetricId;

            document.getElementById("<%= MemberMetricAction_CommandName.ClientID %>").value = "RemoveManual";

            document.getElementById("<%= MemberMetricAction_Arguments.ClientID %>").value = memberMetricId;

            document.getElementById("<%= MemberMetricAction.ClientID %>").click();

        }

        return;

    }

</script>                         

</div>                