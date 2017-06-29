<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MemberDemographics.ascx.cs" Inherits="Mercury.Web.Application.MemberProfile.MemberProfileDemographics" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<%@ Register TagPrefix="MercuryUserControl" TagName="EntityAddressHistory" Src="~/Application/Controls/EntityAddressHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityContactInformationHistory" Src="~/Application/Controls/EntityContactInformationHistory.ascx"  %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>

<div style="margin: 10px;">

    <div style="border: solid 1px #bbd7fa; padding: 2px;">

        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; padding: 2px;">
    
            <tr style="font-weight: bold;">
        
                <td style="width: 15%; text-align: left;">UniqueId</td>
        
                <td style="width: 34%; text-align: left;">Name</td>
        
                <td style="width: 08%; text-align: center;">Gender</td>
            
                <td style="width: 08%; text-align: center;">Age</td>
                        
                <td style="width: 10%; text-align: center;">Birth Date</td>

                <td style="width: 10%; text-align: center;">Death Date</td>
            
                <td style="width: 15%; text-align: center;">Federal Tax Id</td>

            </tr>
    
            <tr>
            
                <td style="text-align: left; "><asp:Label ID="MemberDemographicUniqueId" runat="server" /></td>
            
                <td style="text-align: left; "><asp:Label ID="MemberDemographicName" runat="server" /></td>
            
                <td style="text-align: center;"><asp:Label ID="MemberDemographicGender" runat="server" /></td>
            
                <td style="text-align: center;"><asp:Label ID="MemberDemographicCurrentAge" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicBirthDate" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicDeathDate" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicsFederalTaxId" runat="server" /></td>
            
            </tr>
        
        </table>

        <div style="background-color:#CCCCCC; height:1px; margin-left: 4px; margin-right: 8px;"></div>
    
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px;">
    
            <tr style="font-weight: bold;">
        
                <td style="width: 30%; text-align: left;">Ethnicity</td>
        
                <td style="width: 30%; text-align: left;">Language</td>
            
                <td style="width: 20%; text-align: left;">Citizenship</td>
        
                <td style="width: 20%; text-align: left;">Marital Status</td>

            </tr>
    
            <tr>
            
                <td valign="top" style="text-align: left;"><asp:Label ID="MemberDemographicEthnicity" runat="server" /></td>

                <td valign="top" style="text-align: left;"><asp:Label ID="MemberDemographicLanguage" runat="server" /></td>
            
                <td valign="top" style="text-align: left;"><asp:Label ID="MemberDemographicCitizenship" runat="server" /></td>

                <td valign="top" style="text-align: left;"><asp:Label ID="MemberDemographicMaritalStatus" runat="server" /></td>

            </tr>
        
        </table>

    </div>

    <div style="height: 2px; padding-top: 2px;"></div>
    

    <MercuryUserControl:EntityAddressHistory ID="EntityAddressHistoryControl" runat="server" />
    
    <div style="height: 2px; padding-top: 2px;"></div>

    <MercuryUserControl:EntityContactInformationHistory ID="EntityContactInformationHistoryControl" runat="server" />

        
    <div style="height: 2px; padding-top: 2px;"></div>
    
    <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; border: solid 1px #bbd7fa; display: none;">
    
        <tr style="font-weight: bold;">
        
            <td style="width: 15%; text-align: left;">Telephone</td>
        
            <td style="width: 25%; text-align: left;">Email</td>
        
            <td style="width: 15%; text-align: center;">Emergency</td>

            <td style="width: 15%; text-align: center;">Cell</td>
            
            <td style="width: 15%; text-align: center;">Fax</td>
            
            <td style="width: 15%; text-align: center;">Pager</td>
        
        </tr>
    
        <tr>
            
            <td style="text-align: left; "><asp:Label ID="MemberDemographicTelephone" runat="server" /></td>
            
            <td style="text-align: left; "><asp:Label ID="MemberDemographicEmail" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="MemberDemographicEmergencyPhone" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="MemberDemographicCellPhone" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="MemberDemographicFax" runat="server" /></td>
            
            <td style="text-align: left;"><asp:Label ID="MemberDemographicPager" runat="server" /></td>

        </tr>
        
    </table>

    <div style="height: 2px; padding-top: 2px;"></div>
    
    <div style="padding: 4px; border: solid 1px #bbd7fa;">
        
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">

            <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
            
                <td style="width: 40%;">Insurer</td>

                <td style="width: 30%;">Program</td>
                
                <td style="width: 20%;">Type</td>

                <td style="width: 10%;">Member Id</td>
                
            </tr>
            
            <tr>
            
                <td class="dataField"><asp:Label ID="MemberDemographicEnrollmentInsurer" Width="95%" runat="server" /></td>

                <td class="dataField"><asp:Label ID="MemberDemographicEnrollmentProgram" Width="95%" runat="server" /></td>
                
                <td class="dataField"><asp:Label ID="MemberDemographicEnrollmentInsuranceType" Width="95%" runat="server" /></td>

                <td class="dataField"><asp:Label ID="MemberDemographicEnrollmentMemberProgramId" Width="95%" runat="server" /></td>
                
            </tr>
            
        </table>
        
        <div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div>
    
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">

            <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
            
                <td style="width: 40%;">Sponsor</td>
            
                <td style="width: 40%;">Subscriber</td>
            
                <td style="width: 10%; text-align: center;">Effective</td>

                <td style="width: 10%; text-align: center;">Termination</td>

            </tr>
    
            <tr>
            
                <td style="width: 40%;"><asp:Label ID="MemberDemographicEnrollmentSponsor" Width="95%" runat="server" /></td>

                <td style="width: 40%;"><asp:Label ID="MemberDemographicEnrollmentSubscriber" Width="95%" runat="server" /></td>
                
                <td style="text-align: center;"><asp:Label ID="MemberDemographicEnrollmentEffective" runat="server" /></td>
                
                <td style="text-align: center;"><asp:Label ID="MemberDemographicEnrollmentTermination" runat="server" /></td>

            </tr>
            
        </table>
                            
        <div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div>
        
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
            
            <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
            
                <td style="width: 25%;">Benefit Plan</td>
                
                <td style="width: 20%;">Coverage Type</td>

                <td style="width: 20%;">Coverage Level</td>
            
                <td style="width: 15%;">Rate Code</td>

                <td style="width: 10%; text-align: center;">Effective</td>

                <td style="width: 10%; text-align: center;">Termination</td>

            </tr>
        
            <tr>
                                        
                <td style=""><asp:Label ID="MemberDemographicCoverageBenefitPlan" Width="95%" runat="server" /></td>

                <td style=""><asp:Label ID="MemberDemographicCoverageType" Width="95%" runat="server" /></td>

                <td style=""><asp:Label ID="MemberDemographicCoverageLevel" Width="95%" runat="server" /></td>
                
                <td style=""><asp:Label ID="MemberDemographicCoverageRateCode" Width="95%" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicCoverageEffectiveDate" Width="95%" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicCoverageTerminationDate" Width="95%" runat="server" /></td>

            </tr>
            
        </table>
        
        <div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div>
        
        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 4px; line-height: 150%; vertical-align: middle;">
        
            <tr style="font-weight: bold;">
                
                <td style="width: 40%;">PCP Name</td>
            
                <td style="width: 40%;">PCP Affiliate Name</td>

                <td style="width: 10%; text-align: center;">Effective</td>
                
                <td style="width: 10%; text-align: center;">Termination</td>

            </tr>
        
            <tr>
                
                <td class="dataField"><asp:Label ID="MemberDemographicPcpName" Width="95%" runat="server" /></td>

                <td class="dataField"><asp:Label ID="MemberDemographicPcpAffiliateName" Width="95%" runat="server" /></td>
                
                <td style="text-align: center;"><asp:Label ID="MemberDemographicPcpEffectiveDate" Width="95%" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="MemberDemographicPcpTerminationDate" Width="95%" runat="server" /></td>

            </tr>
        
        </table>
    
    </div>
    
    <div style="height: 2px; padding-top: 2px;"></div>
    
    <div style="padding: 4px; border: solid 1px #bbd7fa;">
    
        <asp:Repeater ID="MemberRelationshipRepeater" runat="server">
        
            <HeaderTemplate>
                    
                <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px; line-height: 150%; vertical-align: middle;">
                    
                    <tr style="width: 100%; font-weight: bold; line-height: 150%; vertical-align: middle;">
                    
                        <td style="width: 15%;">Family Id</td>
                        
                        <td style="width: 25%;">Related Member Name</td>
                    

                        <td style="width: 05%; text-align: center;">Gender</td>

                        <td style="width: 10%; text-align: center;">Birth Date</td>

                        <td style="width: 05%; text-align: center;">Age</td>


                        <td style="width: 20%;">Relationship</td>

                        <td style="width: 10%; text-align: center;">Effective</td>

                        <td style="width: 10%; text-align: center;">Termination</td>

                    </tr>
                    
                     <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
                
            </HeaderTemplate>
            
            
            <ItemTemplate>
            
                <tr>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "FamilyId") %></td>
                
                    <td><%# DataBinder.Eval(Container.DataItem, "RelatedMemberName") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "RelatedMemberGender") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "RelatedMemberBirthDate") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "RelatedMemberCurrentAge") %></td>

                    <td><%# DataBinder.Eval(Container.DataItem, "Relationship") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "EffectiveDate") %></td>

                    <td style="text-align: center;"><%# DataBinder.Eval(Container.DataItem, "TerminationDate") %></td>

                </tr>

            </ItemTemplate>
            
            <SeparatorTemplate>
            
                <tr><td colspan="8"><div style="height: 1px; padding-top: 2px; padding-bottom: 2px; border-bottom: solid 1px #EEEEEE;"></div></td></tr>
            
            </SeparatorTemplate>
            
            <FooterTemplate>
                    
                </table>
                    
            </FooterTemplate>
        
        </asp:Repeater>
    
    </div>
    
    <div style="height: 2px; padding-top: 2px;"></div>
    
</div> <!-- END OF PAGE CONTENT -->

