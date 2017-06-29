<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberCaseCarePlanGoal.ascx.cs" Inherits="Mercury.Web.Application.MemberCase.MemberCaseCarePlanGoal" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="margin: .125in; border: 1px solid #215485">

    <div class="PropertyPageSectionTitleComplement" style="margin-top: 0px; padding: 0px;">
    
        <table cellpadding="2" cellspacing="2" border="0" style="width: 100%; border-bottom: 1px solid black;"><tr>
        
            <td style="width: 36px;">Goal: </td>

            <td style=""><span style="font-weight: normal;"><asp:Label ID="CarePlanGoalName" runat="server" /></span></td>

            <td align="right">
            
                <div id="TitlePanel_UnderDevelopment" visible="false" runat="server">
                
                    <table cellpadding="0" cellspacing="0" border="0" style="font-weight: normal;"><tr>
                    
                        <td><a id="CarePlanGoalEdit_<%= this.ClientID  %>" href="javascript:

                            (function () { 
                        
                                var editAnchor = document.getElementById ('CarePlanGoalEdit_<%= this.ClientID %>');
                                
                                var applyCancelPanel = document.getElementById ('CarePlanGoalEditApplyCancel_<%= this.ClientID %>');
                                
                                var viewPanel = document.getElementById ('CarePlanGoalViewPanel_<%= this.ClientID %>');

                                var editPanel = document.getElementById ('CarePlanGoalEditPanel_<%= this.ClientID %>');
                                
                                editAnchor.style.display = 'none';
                                
                                applyCancelPanel.style.display = 'inline';

                                viewPanel.style.display = 'none';

                                editPanel.style.display = 'block';

                                return;

                            })();
                        
                            " style="display: inline;">(edit)</a></td>

                        <td>

                            <div id="CarePlanGoalEditApplyCancel_<%=this.ClientID %>" style="display: none;">
                            
                                <asp:LinkButton ID="CarePlanGoalEditApply" Text="(apply)" runat="server"></asp:LinkButton>

                                <a href="javascript:

                                    (function () { 
                        
                                        var editAnchor = document.getElementById ('CarePlanGoalEdit_<%= this.ClientID %>');

                                        var applyCancelPanel = document.getElementById ('CarePlanGoalEditApplyCancel_<%= this.ClientID %>');
                                        
                                        var viewPanel = document.getElementById ('CarePlanGoalViewPanel_<%= this.ClientID %>');

                                        var editPanel = document.getElementById ('CarePlanGoalEditPanel_<%= this.ClientID %>');
                                
                                        editAnchor.style.display = 'inline';

                                        applyCancelPanel.style.display = 'none';
                                        
                                        viewPanel.style.display = 'block';

                                        editPanel.style.display = 'none';

                                        return;

                                    })();
                        
                                    " style="display: inline;">(cancel)
                                    
                                </a>

                            </div>

                        </td>
                    
                    </tr></table>

                </div>

            </td>
                
        </tr></table>

        <div id="CarePlanGoalViewPanel_<%=this.ClientID %>" style="padding: .125in; line-height: 150%; background-color: White; font-weight: normal; display: block;"> 
                        

            <div style="font-weight: bold;">Clinical Narrative:</div>

            <div><asp:Label ID="CarePlanGoalClinicalNarrative" Width="100%" runat="server" /></div>

            <div style="font-weight: bold;">Common Narrative:</div>

            <div><asp:Label ID="CarePlanGoalCommonNarrative" Width="100%" runat="server" /></div>

            
            <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                <tr class="" style="min-height: 24px;">

                    <td style="width: 100px; font-weight: bold;">Measurement*:</td>

                    <td><asp:Label ID="CarePlanGoalMeasurementName" runat="server"></asp:Label></td>

                    <td style="width: 100px; font-weight: bold;">Initial Value:</td>

                    <td style="width: 100px;"><asp:Label ID="CarePlanGoalMeasurementInitialValue" runat="server"></asp:Label></td>

                    <td style="width: 100px; font-weight: bold;">Last Value:</td>

                    <td style="width: 100px;"><asp:Label ID="CarePlanGoalMeasurementLastValue" runat="server"></asp:Label></td>
                    
                    <td style="width: 100px; font-weight: bold;">Target Value:</td>

                    <td style="width: 100px;"><asp:Label ID="CarePlanGoalMeasurementTargetValue" runat="server"></asp:Label></td>

                </tr>
                
            </table>
            
            <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                <tr class="" style="height: 24px;">

                    <td style="text-align: left; width: 18%"><b>Status:</b> <asp:Label id="CaseStatus" Text="Not Specified" runat="server" /></td>
                        
                    <td style="text-align: left; width: 18%"><b>Added Date:</b></td>
                                    
                    <td style="text-align: left; width: 18%"><b>Term:</b> <asp:Label id="Label4" Text="Short-term" runat="server" /></td>

                    <td style="text-align: left; width: 18%"><b>Goal Date:</b> <asp:Label id="Label5" Text="MM/DD/YYYY" runat="server" /></td>
                        
                </tr>
        
            </table>                
                            
            <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                <tr class="" style="height: 24px;">

                    <td style="text-align: left; width: 18%"><b>Effective Date:</b> <asp:Label id="CaseEffectiveDate" Text="" runat="server" /></td>
                        
                    <td style="text-align: left; width: 18%"><b>Termination Date:</b> <asp:Label id="CaseTerminationDate" Text="< active >" runat="server" /></td>
                        
                    <td style="text-align: left; width: 28%"><b>Outcome:</b> <asp:Label id="CaseOutcome" Text="Not Specified" runat="server" /></td>
                        
                </tr>
        
            </table>        

        </div>
        
        <div id="CarePlanGoalEditPanel_<%=this.ClientID %>" style="padding: .125in; line-height: 150%; background-color: White; font-weight: normal; display: none;"> 
                        
            <table width="100%" cellpadding="0" cellspacing="0" style="">

                <tr class="" style="min-height: 24px;">

                    <td style="width: 80px; font-weight: bold;">Goal Name:</td>

                    <td><Telerik:RadTextBox ID="CarePlanGoalEditName" Width="100%" MaxLength="60" runat="server"></Telerik:RadTextBox></td>

                </tr>    

            </table>
    
            <div style="font-weight: bold;">Clinical Narrative:</div>

            <div><Telerik:RadTextBox ID="CarePlanGoalEditClinicalNarrative" Width="100%" MaxLength="999" TextMode="MultiLine" Rows="3" runat="server"></Telerik:RadTextBox></div>

            <div style="font-weight: bold;">Common Narrative:</div>

            <div><Telerik:RadTextBox ID="CarePlanGoalEditCommonNarrative" Width="100%" MaxLength="999" TextMode="MultiLine" Rows="3" runat="server"></Telerik:RadTextBox></div>

            <!-- CARE PLAN GOAL EDIT PANEL FOR UNDER DEVELOPMENT STATUS -->

            <div id="CarePlanGoalEditPanel_UnderDevelopment" visible="false" runat="server">

                <table width="100%" cellpadding="0" cellspacing="2"><tr>
                                        
                    <td style="width: 75px; font-weight: bold">Timeframe:</td>

                    <td style="width: 10%;">
                                                
                        <Telerik:RadComboBox ID="CarePlanGoalTimeframeSelection" Width="99%" runat="server">
                                    
                            <Items>
                                        
                                <Telerik:RadComboBoxItem Value="0" Text="Short-term" Selected="true" />
                                            
                                <Telerik:RadComboBoxItem Value="1" Text="Long-term" />
                                            
                            </Items>
                                    
                        </Telerik:RadComboBox>

                    </td>
                                  
                    <td style="width: 10%; text-align: right; font-weight: bold;">Schedule Value:</td>

                    <td style="width: 05%"><Telerik:RadNumericTextBox ID="CarePlanGoalScheduleValue" Width="98%" NumberFormat-DecimalDigits="0" MinValue="0" Value="0" runat="server" /></td>

                    <td style="width: 08%;">
                                                
                        <Telerik:RadComboBox ID="CarePlanGoalScheduleQualifierSelection" Width="99%" runat="server">
                                    
                            <Items>
                                        
                                <Telerik:RadComboBoxItem Value="0" Text="Days" Selected="true" />
                                            
                                <Telerik:RadComboBoxItem Value="1" Text="Months" />
                                            
                                <Telerik:RadComboBoxItem Value="2" Text="Years" />
                                        
                            </Items>
                                    
                        </Telerik:RadComboBox>

                    </td>
                        
                    <td style="width: 14%; text-align: right; font-weight: bold;">Measurement:</td>
                        
                    <td style="width: 45%;">
                                                
                        <Telerik:RadComboBox ID="CarePlanGoalCareMeasurementSelection" Width="99%" runat="server"></Telerik:RadComboBox>

                    </td>

                </tr></table>
            
            </div>

            <!-- CARE PLAN GOAL EDIT PANEL FOR ACTIVE STATUS -->

            <div id="CarePlanGoalEditPanel_Active" visible="false" runat="server">
            
                <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                    <tr class="" style="min-height: 24px;">

                        <td style="font-weight: bold;">Measurement:</td>

                        <td><asp:Label ID="Label3" runat="server"></asp:Label></td>

                        <td style="font-weight: bold;">Initial Value:</td>

                        <td><asp:Label ID="Label6" runat="server"></asp:Label></td>

                        <td style="font-weight: bold;">Last Value:</td>

                        <td><asp:Label ID="Label7" runat="server"></asp:Label></td>
                    
                        <td style="font-weight: bold;">Target Value:</td>

                        <td><asp:Label ID="Label8" runat="server"></asp:Label></td>

                    </tr>
                
                </table>
            
                <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                    <tr class="" style="height: 24px;">

                        <td style="text-align: left; width: 18%"><b>Status:</b> <asp:Label id="Label9" Text="Not Specified" runat="server" /></td>
                        
                        <td style="text-align: left; width: 18%"><b>Added Date:</b></td>
                                    
                        <td style="text-align: left; width: 18%"><b>Term:</b> <asp:Label id="Label10" Text="Short-term" runat="server" /></td>

                        <td style="text-align: left; width: 18%"><b>Goal Date:</b> <asp:Label id="Label11" Text="MM/DD/YYYY" runat="server" /></td>
                        
                    </tr>
        
                </table>                
                            
                <table width="100%" cellpadding="0" cellspacing="0" style="">
    
                    <tr class="" style="height: 24px;">

                        <td style="text-align: left; width: 18%"><b>Effective Date:</b> <asp:Label id="Label12" Text="" runat="server" /></td>
                        
                        <td style="text-align: left; width: 18%"><b>Termination Date:</b> <asp:Label id="Label13" Text="< active >" runat="server" /></td>
                        
                        <td style="text-align: left; width: 28%"><b>Outcome:</b> <asp:Label id="Label14" Text="Not Specified" runat="server" /></td>
                        
                    </tr>
        
                </table>        

            </div>


        </div>

    </div>

</div>

