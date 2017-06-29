<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntitySendCorrespondence.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntitySendCorrespondence" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ButtonOk">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="ButtonOk" />
                
                <Telerik:AjaxUpdatedControl ControlID="ActionResponseLabel" />
           
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="CorrespondenceUseAlternativeAddress">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeAddress" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeAddressDetail" />
                      
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeFaxNumber" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeFaxNumberDetail" />
                
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeEmail" />
                                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeEmailDetail" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="CorrespondenceUseAlternativeFaxNumber">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeAddress" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeAddressDetail" />
                      
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeFaxNumber" />
                
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceAlternateFaxNumber" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeFaxNumberDetail" />
                
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeEmail" />
                                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeEmailDetail" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="CorrespondenceUseAlternativeEmail">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeAddress" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeAddressDetail" />
                      
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeFaxNumber" />
                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeFaxNumberDetail" />
                
                <Telerik:AjaxUpdatedControl ControlID="CorrespondenceUseAlternativeEmail" />
                                
                <Telerik:AjaxUpdatedControl ControlID="AlternativeEmailDetail" />
                
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>



<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div id="EntitySendCorrespondenceControlContainer" style="">

    <div id="EntitySendCorrespondenceStep1SectionTitle" class="SectionTitle" style="margin-top: 0px;">Step 1: Select an Address (or Method)</div>
    
    <table id="EntitySendCorrespondenceInformationGridContainer" width="100%" style="height: 145px;"><tr><td style="width: 100%; height: 100%; padding: .0625in; overflow: hidden;">
        
        <Telerik:RadGrid ID="EntitySendCorrespondenceInformationGrid" Width="100%" Height="100%" runat="server">

            <MasterTableView AutoGenerateColumns="false" >

                <Columns>

                    <Telerik:GridClientSelectColumn UniqueName="ClientSelection" HeaderText="Select"><HeaderStyle HorizontalAlign="Center" Width="60" /><ItemStyle HorizontalAlign="Center" Width="60" /></Telerik:GridClientSelectColumn>                                                      

                
                    <Telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="false"></Telerik:GridBoundColumn>
                                                            
                    <Telerik:GridBoundColumn DataField="AddressId" UniqueName="AddressId" Visible="false"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="EntityId" UniqueName="EntityId" Visible="false"></Telerik:GridBoundColumn>

                    <Telerik:GridBoundColumn DataField="AddressType" HeaderText="Type" HeaderStyle-Width="100" ItemStyle-Width="100" Visible="true"></Telerik:GridBoundColumn>
                
                    <Telerik:GridBoundColumn DataField="Line1" HeaderText="Line 1" UniqueName="Line1" Visible="true"></Telerik:GridBoundColumn>
                                
                    <Telerik:GridBoundColumn DataField="Line2" HeaderText="Line 2" UniqueName="Line2" Visible="true"></Telerik:GridBoundColumn>
                
                    <Telerik:GridBoundColumn DataField="CityStateZip" HeaderText="City, State Zip Code" UniqueName="CityStateZip" Visible="true"></Telerik:GridBoundColumn>
                               
                
                    <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" UniqueName="EffectiveDate" Visible="false"><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" />

                    </Telerik:GridBoundColumn><Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination" UniqueName="TerminationDate" Visible="false"><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" /></Telerik:GridBoundColumn>

            </Columns>

            </MasterTableView>
            
            <ClientSettings>
        
                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
            
                <Selecting AllowRowSelect="true" EnableDragToSelectRows="false" />
        
            </ClientSettings>      

        </Telerik:RadGrid>
    
    </td></tr></table>

    <div id="EntitySendCorrespondenceStep2SectionTitle" class="SectionTitle" style="margin-top: 0px;">Step 2: Select Correspondence and Edit Attention Line</div>

    <div id="EntitySendCorrespondenceStep2" style="padding: .125in;">

        <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr style="height: 24px;">
        
            <td style="width: 110px; min-width: 110px; white-space: nowrap; padding-right: .125in;">Correspondence: </td>
        
            <td style=""><Telerik:RadComboBox ID="SendCorrespondenceSelection" Width="100%" runat="server"></Telerik:RadComboBox> </td>
        
            <td style="white-space: nowrap; padding-left: .125in; padding-right: .125in;"><asp:Label ID="SendCorrespondenceDateLabel" Text="Send Date:" runat="server"></asp:Label></td>
        
            <td><Telerik:RadDatePicker ID="SendCorrespondenceDate" DateInput-AutoPostBack="false" AutoPostBack="false" Visible="true" runat="server"></Telerik:RadDatePicker></td>           
    
        </tr></table>

        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
            <td style="width: 110px; white-space: nowrap; padding-right: .125in;">Attention: </td>
        
            <td style=""><Telerik:RadTextBox ID="SendCorrespondenceAttention" Width="100%" MaxLength="60" EmptyMessage="(optional)" runat="server"></Telerik:RadTextBox></td>    
           
        </tr></table>
    
        <table cellpadding="0" cellspacing="0" border="0" width="100%" style="margin-top: .0625in;"><tr style="height: 24px;">
        
            <td style="width: 110px; white-space: nowrap; padding-right: .125in; vertical-align: top;">Remarks: </td>
        
            <td style=""><Telerik:RadTextBox ID="SendCorrespondenceRemarks" Width="100%" MaxLength="999" EmptyMessage="(optional)" Rows="3" TextMode="MultiLine" runat="server"></Telerik:RadTextBox></td>    
           
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
        
            <td style="width: 100%;"><asp:Label ID="ActionResponseLabel" ForeColor="Red" runat="server"></asp:Label></td>
        
            <td style="width: 100px; padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonOk" OnClick="ButtonOk_OnClick" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                          
            <td style="width: 100px; padding-left: .25in; padding-right: .25in;"><asp:Button ID="ButtonCancel" OnClick="ButtonCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>

        </tr></table>
    
        <div style="padding: 10px; line-height: 150%"> ** Using an Alternative Method Below overrides any selection in Step 1.</div>
    
        <div id="AlternativeAddressDiv" runat="server">
        
            <table cellspacing="2" cellpadding="2" border="0" style="width: 95%; line-height: 150%;">
          
                <tr style="height: 32px;">
            
                    <td style="width: 10px;"><asp:CheckBox ID="CorrespondenceUseAlternativeAddress" AutoPostBack="true" OnCheckedChanged="CorrespondenceUseAlternativeAddress_OnCheckedChanged" runat="server" /></td>
                
                    <td style="width: 150px;">Use Alternative Address</td>
            
                    <td><hr /></td>
                
                </tr>
            
            </table>
                
            <div id="AlternativeAddressDetail" style="width: 100%; display: none;" runat="server">
                
                <table cellspacing="2" cellpadding="2" border="0" style="line-height: 150%;">
              
                    <tr style="height: 32px;">
                
                        <td style="width: 100px;">Address Line 1:</td>
                  
                        <td style="width: 80%;"><Telerik:RadTextBox ID="CorrespondenceAlternateAddressLine1" Width="99%" runat="server" /></td>
                  
                    </tr>
                
                    <tr style="height: 32px;">
                
                        <td>Address Line 2:</td>
                  
                        <td><Telerik:RadTextBox ID="CorrespondenceAlternateAddressLine2" Width="99%" runat="server" /></td>
                  
                    </tr>

                </table>      
                
                <table cellspacing="2" cellpadding="2" border="0" style="line-height: 150%;">
              
                    <tr style="height: 32px;">
                
                        <td style="width: 80px;">City:</td>
                  
                        <td style="width: 150px;"><Telerik:RadTextBox ID="CorrespondenceAlternateAddressCity" Width="99%" runat="server" /></td>
                  
                        <td align="center" style="width: 80px;">State:</td>
                  
                        <td style="width: 80px;"><Telerik:RadTextBox ID="CorrespondenceAlternateAddressState" MaxLength="2" Width="99%" runat="server" /></td>

                        <td align="center" style="width: 110px;">Zip Code:</td>
                  
                        <td style="width: 80px;"><Telerik:RadTextBox ID="CorrespondenceAlternateAddressZipCode" MaxLength="5" Width="99%" runat="server" /></td>

                        <td>&nbsp</td>

                    </tr>
                
                </table>
        
            </div>                 
              
        </div>
    
        <div id="AlternativeFaxNumberDiv" runat="server">
        
            <table cellspacing="2" cellpadding="2" border="0">
          
                <tr style="height: 32px;">
            
                    <td style="width: 10px;"><asp:CheckBox ID="CorrespondenceUseAlternativeFaxNumber" AutoPostBack="true" OnCheckedChanged="CorrespondenceUseAlternativeFaxNumber_OnCheckedChanged" runat="server" /></td>
                
                    <td style="width: 200px;" align="left">Use Alternative FAX Number</td>
            
                    <td><div id="AlternativeFaxNumberDetail" style="display: none;" runat="server"><Telerik:RadTextBox ID="CorrespondenceAlternateFaxNumber" Width="99%" runat="server" /></div></td>
                
                    <td><hr /></td>
                
                </tr>
            
            </table>
    
        </div>
    
        <div id="AlternativeEmailDiv" runat="server">
        
            <table cellspacing="2" cellpadding="2" border="0">
          
                <tr style="height: 32px;">
            
                    <td style="width: 10px;"><asp:CheckBox ID="CorrespondenceUseAlternativeEmail" AutoPostBack="true" OnCheckedChanged="CorrespondenceUseAlternativeEmail_OnCheckedChanged" runat="server" /></td>
                
                    <td style="width: 200px;">Use Alternative Email</td>
            
                    <td><div id="AlternativeEmailDetail" style="display: none;" runat="server"><Telerik:RadTextBox ID="CorrespondenceAlternateEmail" Width="99%" runat="server" /></div></td>
                
                    <td><hr /></td>
                </tr>
            
            </table>
    
        </div>
    
    </div>

</div>



<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', EntitySendCorrespondenceControl_Body_OnResize, false); } else { window.attachEvent('onresize', EntitySendCorrespondenceControl_Body_OnResize); }


    var isEntitySendCorrespondenceControlPainting = false;

    setTimeout('EntitySendCorrespondenceControl_OnPaint()', 500);


    function EntitySendCorrespondenceControl_OnPaint(forEvent) {

        if (isEntitySendCorrespondenceControlPainting) { return; }

        isEntitySendCorrespondenceControlPainting = true;


        var container = document.getElementById("EntitySendCorrespondenceControlContainer");

        var gridControl = $find("<%= EntitySendCorrespondenceInformationGrid.ClientID %>");

        if ((container == null) || (gridControl == null)) {

            isEntitySendCorrespondenceControlPainting = false;

            setTimeout('EntitySendCorrespondenceControl_OnPaint ()', 100);

            return;

        }


        if (container.parentNode.id.toString().indexOf("RAD_SPLITTER_PANE_CONTENT") >= 0) {

            // CONTAINER INSIDE OF PANEL, RESIZE BASED ON PANEL SIZE

            var splitterHtmlControl = container.parentNode

            while (!((splitterHtmlControl.id.toString().indexOf("RAD_SPLITTER_") >= 0) && (splitterHtmlControl.id.toString().indexOf("RAD_SPLITTER_PANE") < 0))) {

                splitterHtmlControl = splitterHtmlControl.parentNode;
                
            }

            var splitterName = splitterHtmlControl.id.toString().replace("RAD_SPLITTER_", "");

            var splitterControl = $find(splitterName);

            var containerPane = splitterControl.GetPaneByIndex(1);

            var availableHeight = containerPane.get_height();


            availableHeight = availableHeight - document.getElementById("EntitySendCorrespondenceStep1SectionTitle").offsetHeight;

            availableHeight = availableHeight - document.getElementById("EntitySendCorrespondenceStep2SectionTitle").offsetHeight;

            availableHeight = availableHeight - document.getElementById("EntitySendCorrespondenceStep2").offsetHeight;

            availableHeight = availableHeight - (13 * 3); // MARGIN * 2

            if (availableHeight < 100) { availableHeight = 100; }

            document.getElementById("EntitySendCorrespondenceInformationGridContainer").style.height = availableHeight + "px";


            // gridControl.get_element().style.height = "100%";

            gridControl.repaint();

        }


        isEntitySendCorrespondenceControlPainting = false;

        return;

    }


    function EntitySendCorrespondenceControl_Body_OnResize(forEvent) {

        EntitySendCorrespondenceControl_OnPaint(forEvent);

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

        EntitySendCorrespondenceControl_OnPaint();

        return;

    }

</script>

</Telerik:RadScriptBlock>