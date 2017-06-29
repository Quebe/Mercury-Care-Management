<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityContact.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityContact" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ButtonOk">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="ButtonOk" />
                
                <Telerik:AjaxUpdatedControl ControlID="ActionResponseLabel" />
           
            </UpdatedControls>
        
        </Telerik:AjaxSetting>

    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>



<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div id="EntityContactControlContainer" style="">

    <div id="EntityContactStep1SectionTitle" class="SectionTitle" style="margin-top: 0px;">Step 1: Select a Contact Method</div>
    
    <table id="EntityContactInformationGridContainer" width="100%" style="height: 145px;"><tr><td style="width: 100%; height: 100%; padding: .0625in; overflow: hidden;">
        
        <Telerik:RadGrid ID="EntityContactInformationGrid" Width="100%" Height="100%" runat="server">

            <MasterTableView AutoGenerateColumns="false" >

                <Columns>
                 
                <Telerik:GridClientSelectColumn UniqueName="ClientSelection" HeaderText="Select">

                    <HeaderStyle HorizontalAlign="Center" />
                        
                    <ItemStyle HorizontalAlign="Center" />
                    
                </Telerik:GridClientSelectColumn>                                                      
                
                                                            
                <Telerik:GridBoundColumn DataField="Id" UniqueName="Id" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="ContactSequence" HeaderText="Sequence" UniqueName="ContactSequence" Visible="false"></Telerik:GridBoundColumn>
                
                
                <Telerik:GridBoundColumn DataField="ContactType" Visible="false"></Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="ContactTypeDescription" HeaderText="Type" Visible="true"></Telerik:GridBoundColumn>
                
                <Telerik:GridBoundColumn DataField="NumberFormatted" HeaderText="Number" UniqueName="Number" Visible="true"></Telerik:GridBoundColumn>
                
                <Telerik:GridBoundColumn DataField="Extension" HeaderText="Extension" UniqueName="Extension" Visible="true"></Telerik:GridBoundColumn>
                
                <Telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" Visible="true"></Telerik:GridBoundColumn>
                                
                
                <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" UniqueName="EffectiveDate" Visible="false">
                
                    <HeaderStyle HorizontalAlign="Center" />
                        
                    <ItemStyle HorizontalAlign="Center" />

                </Telerik:GridBoundColumn>

                <Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination" UniqueName="TerminationDate" Visible="false">
                
                    <HeaderStyle HorizontalAlign="Center" />
                        
                    <ItemStyle HorizontalAlign="Center" />

                </Telerik:GridBoundColumn>

            </Columns>

            </MasterTableView>
            
            <ClientSettings>
        
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            
                <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" />
        
            </ClientSettings>      

        </Telerik:RadGrid>
    
    </td></tr></table>

    <div id="EntityContactStep2SectionTitle" class="SectionTitle" style="margin-top: 0px;">Step 2: Record Contact Results</div>

    <div id="EntityContactStep2" style="padding: .125in;">

    <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr style="height: 24px;">
        
        <td style="width: 110px; min-width: 110px; white-space: nowrap; padding-right: .125in;">Contact Direction: </td>
        
        <td style="">
        
            <Telerik:RadComboBox ID="ContactDirection" Width="100" runat="server">
                            
                <Items>
                    
                    <Telerik:RadComboBoxItem Text="Outbound" Value="1" Selected="true" />
                        
                    <Telerik:RadComboBoxItem Text="Inbound" Value="2" Selected="false" />
                    
                </Items>
                
            </Telerik:RadComboBox>

        </td>
        
        <td style="white-space: nowrap; padding-left: .125in; padding-right: .125in;"><asp:Label ID="ContactDateTimeLabel" Text="Contact Date/Time:" runat="server"></asp:Label></td>
        
        <td>
            
            <Telerik:RadDateTimePicker ID="ContactDateTime" DateInput-AutoPostBack="false" AutoPostBackControl="None" Calendar-AutoPostBack="false" Visible="true" runat="server">
                    
                <Calendar ID="ContactDateTimeCalendar" runat="server">
                    
                </Calendar>
                    
            </Telerik:RadDateTimePicker>
                           
        </td>           

        <td style="width: 100%;">&nbsp;</td>
    
    </tr></table>

    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
        <td style="width: 110px; white-space: nowrap; padding-right: .125in;">Contact Regarding: </td>
        
        <td style="">
        
            <Telerik:RadComboBox ID="ContactRegarding" Width="100%" MaxLength="120" EmptyMessage="(required)" runat="server"></Telerik:RadComboBox>

            <asp:Label ID="ContactRegardingLabel" runat="server" Visible="false"></asp:Label>
            
        </td>
           
    </tr></table>

    <div id="RelatedEntitySection" runat="server">
            
        <div id="RelatedMemberInformation" style="display: none;" runat="server">

            <table width="100%" cellpadding="0" cellspacing="0">
    
                <tr class="" style="height: 36px;">

                    <td style="max-width: 24px; padding-right: 4px;"><img id="RelatedMemberInformationMemberNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="display: none;" runat="server" /></td>   
        
                    <td style="max-width: 24px; padding-right: 4px;"><img id="RelatedMemberInformationMemberNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="display: none;" runat="server" /></td>   

        
                    <td style="text-align: left"><b>Regarding Member:</b> <asp:Label id="RelatedMemberInformationMemberName" Text="** No Member Selected" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Birth Date:</b> <asp:Label id="RelatedMemberInformationMemberBirthDate" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Age:</b> <asp:Label id="RelatedMemberInformationMemberAge" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Gender:</b> <asp:Label id="RelatedMemberInformationMemberGender" Text="" runat="server" /></td>
                        
                    <td style="text-align: left"><b>Program:</b> <asp:Label id="RelatedMemberInformationMemberProgram" Text="" runat="server" /></td>
                                                
                    <td style="text-align: left"><b>Id:</b> <asp:Label id="RelatedMemberInformationMemberProgramMemberId" Text="" runat="server" /></td>

                    <td style="width: 50px; text-align: center;"><a id="RelatedMemberInformationCoverageToggle" href="#" onclick="javascript:RelatedMemberInformationCoverage_Toggle()" title="Toggle Coverage Information">(more)</a></td>

                </tr>
        
            </table>

            <div id="RelatedMemberInformationCoverage" style="display: none;" runat="server">
            
                <table width="100%" cellpadding="0" cellspacing="0">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left"><b>Benefit Plan:</b> <asp:Label id="RelatedMemberInformationMemberCoverageBenefitPlan" Text="** Not Enrolled" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Coverage Type:</b> <asp:Label id="RelatedMemberInformationMemberCoverageType" Text="" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Coverage Level:</b> <asp:Label id="RelatedMemberInformationMemberCoverageLevel" Text="" runat="server" /></td>
                        
                        <td style="text-align: left"><b>Rate Code:</b> <asp:Label id="RelatedMemberInformationMemberCoverageRateCode" Text="" runat="server" /></td>

                    </tr>
        
                </table>
            
                <table width="100%" cellpadding="0" cellspacing="0">
    
                    <tr class="" style="height: 36px;">

                        <td style="text-align: left"><b>PCP Name:</b> <asp:Label id="RelatedMemberInformationMemberPcpName" Text="** No PCP" runat="server" /></td>
                        
                        <td style="text-align: left"><b>PCP Affiliate Name:</b> <asp:Label id="RelatedMemberInformationMemberPcpAffiliateName" Text="" runat="server" /></td>
                        
                    </tr>
        
                </table>

            </div>

        </div>
                
        <div id="RelatedProviderInformation" style="display: none;" runat="server">

            <table width="100%" cellpadding="0" cellspacing="0">
    
                <tr class="" style="height: 36px;">

                    <td style="max-width: 24px; padding-right: 4px;"><img id="RelatedProviderInformationProviderNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="display: none;" runat="server" /></td>   
        
                    <td style="max-width: 24px; padding-right: 4px;"><img id="RelatedProviderInformationProviderNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="display: none;" runat="server" /></td>   

        
                    <td style="text-align: left"><b>Regarding Provider:</b> <asp:Label id="RelatedProviderInformationProviderName" Text="** No Provider Selected" runat="server" /></td>
                        
                    <td style="text-align: left"><b>NPI:</b> <asp:Label id="RelatedProviderInformationProviderNpi" Text="" runat="server" /></td>

                    <td style="text-align: left"><b>Program:</b> <asp:Label id="RelatedProviderInformationProviderProgram" Text="" runat="server" /></td>
                                                
                    <td style="text-align: left"><b>Id:</b> <asp:Label id="RelatedProviderInformationProviderProgramProviderId" Text="" runat="server" /></td>

                </tr>
        
            </table>

        </div>
        
    </div>
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
        <td style="width: 110px; white-space: nowrap; padding-right: .125in; vertical-align: top;">Introduction Script: </td>
        
        <td style=""><asp:Label ID="ContactIntroductionScript" Text="[Script]" runat="server"></asp:Label></td>
           
    </tr></table>
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
        <td style="width: 110px; white-space: nowrap; padding-right: .125in; vertical-align: top;">Contact Remarks: </td>
        
        <td style=""><Telerik:RadTextBox ID="ContactRemarks" Width="100%" MaxLength="999" EmptyMessage="(optional)" Rows="3" TextMode="MultiLine" runat="server"></Telerik:RadTextBox></td>    
           
    </tr></table>
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
        <td style="width: 110px; white-space: nowrap; padding-right: .125in;">Contact Outcome: </td>
        
        <td>

            <Telerik:RadComboBox ID="ContactOutcome" runat="server">
                            
                <Items>
                        
                    <Telerik:RadComboBoxItem Text="Successful"   Value="1" Selected="false" />

                        
                    <Telerik:RadComboBoxItem Text="No Answer"    Value="2" Selected="false" />
                        
                    <Telerik:RadComboBoxItem Text="Left Message" Value="3" Selected="false" />
                        
                    <Telerik:RadComboBoxItem Text="Busy"         Value="4" Selected="false" />
                        
                    <Telerik:RadComboBoxItem Text="Wrong Number" Value="5" Selected="false" />
                        
                    <Telerik:RadComboBoxItem Text="Disconnected" Value="6" Selected="false" />


                    <Telerik:RadComboBoxItem Text="Refused Call" Value="7" Selected="false" />                       
                        
                    <Telerik:RadComboBoxItem Text="Rescheduled Call" Value="8" Selected="false" /> 
                        
                    <Telerik:RadComboBoxItem Text="Not Available" Value="9" Selected="false" />
                        
                    <Telerik:RadComboBoxItem Text="Language Barrier" Value="10" Selected="false" />
                        
                        
                    <Telerik:RadComboBoxItem Text="Deceased" Value="11" Selected="false" />
                    
                </Items>
                
            </Telerik:RadComboBox>
                        
        </td>

        <td><asp:Label ID="ActionResponseLabel" ForeColor="Red" runat="server"></asp:Label></td>
        
        <td style="width: 100px; padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonOk" OnClick="ButtonOk_OnClick" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                          
        <td style="width: 100px; padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonCancel" OnClick="ButtonCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>

    </tr></table>
    
    </div>

</div>



<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', EntityContactControl_Body_OnResize, false); } else { window.attachEvent('onresize', EntityContactControl_Body_OnResize); }


    var isEntityContactControlPainting = false;

    setTimeout('EntityContactControl_OnPaint()', 500);


    function EntityContactControl_OnPaint(forEvent) {

        if (isEntityContactControlPainting) { return; }

        isEntityContactControlPainting = true;


        var container = document.getElementById("EntityContactControlContainer");

        var gridControl = $find("<%= EntityContactInformationGrid.ClientID %>");

        if ((container == null) || (gridControl == null)) {

            isEntityContactControlPainting = false;

            setTimeout('EntityContactControl_OnPaint ()', 100);

            return;

        }


        if (container.parentNode.id.toString().indexOf("RAD_SPLITTER_PANE_CONTENT") >= 0) {

            // CONTAINER INSIDE OF PANEL, RESIZE BASED ON PANEL SIZE

            var splitterHtmlControl = container.parentNode;

            while (!((splitterHtmlControl.id.toString().indexOf("RAD_SPLITTER_") >= 0) && (splitterHtmlControl.id.toString().indexOf("RAD_SPLITTER_PANE") < 0))) {

                splitterHtmlControl = splitterHtmlControl.parentNode;

            }

            var splitterName = splitterHtmlControl.id.toString().replace("RAD_SPLITTER_", "");

            var splitterControl = $find(splitterName);

            var containerPane = splitterControl.GetPaneByIndex(1);

            var availableHeight = containerPane.get_height();


            availableHeight = availableHeight - document.getElementById("EntityContactStep1SectionTitle").offsetHeight;

            availableHeight = availableHeight - document.getElementById("EntityContactStep2SectionTitle").offsetHeight;

            availableHeight = availableHeight - document.getElementById("EntityContactStep2").offsetHeight;

            availableHeight = availableHeight - (13 * 3); // MARGIN * 2

            if (availableHeight < 100) { availableHeight = 100; }
            
            document.getElementById("EntityContactInformationGridContainer").style.height = availableHeight + "px";


            // gridControl.get_element().style.height = "100%";

            gridControl.repaint();

        }


        isEntityContactControlPainting = false;

        return;

    }


    function EntityContactControl_Body_OnResize(forEvent) {

        EntityContactControl_OnPaint(forEvent);

        return;

    }

    function RelatedMemberInformationCoverage_Toggle() {

        var coverageDiv = document.getElementById("<%= RelatedMemberInformationCoverage.ClientID %>");

        var coverageAnchor = document.getElementById("RelatedMemberInformationCoverageToggle");

        if (coverageDiv != null) {

            if (coverageDiv.style.display == "none") {

                coverageDiv.style.display = "block";

                coverageAnchor.innerText = "(less)";

            }

            else {

                coverageDiv.style.display = "none";

                coverageAnchor.innerText = "(more)";

            }

        }

        EntityContactControl_OnPaint();

        return;

    }
</script>

</Telerik:RadScriptBlock>