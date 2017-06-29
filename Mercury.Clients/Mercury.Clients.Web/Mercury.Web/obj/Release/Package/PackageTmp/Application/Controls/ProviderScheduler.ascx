<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderScheduler.ascx.cs" Inherits="Mercury.Web.Application.Controls.ProviderScheduler" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>


        <Telerik:RadToolBar ID="ProviderProfileToolbar" Width="100%" runat="server">
        
            <Items>
            
                <Telerik:RadToolBarButton Text="Add Appointment" Value="Add Appointment" ToolTip="Add Appointment" ImagePosition="Left" ImageUrl="/Images/Common16/Calendar.png" Visible="true" />
                
                <Telerik:RadToolBarButton IsSeparator="true" Visible="true" />

                <Telerik:RadToolBarButton BorderStyle="None">
                
                    <ItemTemplate>
                                                            
                        <table cellpadding="0" cellspacing="0" border="0" style="border: none; padding: 0px"><tr>
                        
                            <td style="width: 120px;" align="center">Service Location:</td>
                            
                            <td style="width: 300px"><Telerik:RadComboBox ID="ServiceLocationSelection" Width="300" runat="server" /></td>
                            
                            <td><asp:Button ID="ServiceLocationSelect" CommandName="Select" Text="Select" Width="73px" Font-Names="segoe ui, arial" Font-Size="11px" Height="24" runat="Server" /></td>
                        
                        </tr></table>
                    
                    </ItemTemplate>
                
                </Telerik:RadToolBarButton>    
                           
            </Items>
        
        </Telerik:RadToolBar>


        <Telerik:RadSplitter runat="server" ID="RadSplitter1" PanesBorderSize="0" Width="100%" Height="368" Skin="Office2007">
        
            <Telerik:RadPane runat="Server" ID="leftPane" Width="240px" MinWidth="240" MaxWidth="300" Scrolling="None">
            
                <div class="calendar-container">
        <Telerik:RadCalendar runat="server" ID="RadCalendar1" Skin="Office2007" AutoPostBack="true"
        EnableMultiSelect="false" DayNameFormat="FirstTwoLetters" EnableNavigation="true"
        EnableMonthYearFastNavigation="false" 
        >
        </Telerik:RadCalendar>
        <Telerik:RadCalendar runat="server" ID="RadCalendar2" Skin="Office2007" AutoPostBack="true"
        EnableMultiSelect="false" DayNameFormat="FirstTwoLetters" EnableNavigation="false"
        EnableMonthYearFastNavigation="false" >
        </Telerik:RadCalendar>
        </div>
        
            </Telerik:RadPane>
        
            <Telerik:RadSplitBar runat="server" ID="RadSplitBar2" CollapseMode="Forward" />
            
            <Telerik:RadPane runat="Server" ID="rightPane" Scrolling="None">

                <Telerik:RadScheduler runat="server" ID="RadScheduler1" Width="100%" Height="368" DayStartTime="08:00:00"
                    DayEndTime="18:00:00" TimeZoneOffset="03:00:00" OnAppointmentInsert="RadScheduler1_AppointmentInsert"
                    OnAppointmentUpdate="RadScheduler1_AppointmentUpdate" OnAppointmentDelete="RadScheduler1_AppointmentDelete"
                    DataKeyField="ID" DataSubjectField="Subject" DataStartField="Start" DataEndField="End"
                    DataRecurrenceField="RecurrenceRule" DataRecurrenceParentKeyField="RecurrenceParentId">
                    <AdvancedForm Modal="true" />
                    <TimelineView UserSelectable="false" />
                    <TimeSlotContextMenuSettings EnableDefault="true" />
                    <AppointmentContextMenuSettings EnableDefault="true" />
                </Telerik:RadScheduler>

            </Telerik:RadPane>
            
         </Telerik:RadSplitter>