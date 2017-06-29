<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProviderDemographics.ascx.cs" Inherits="Mercury.Web.Application.Controls.ProviderDemographics" %>

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

        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px;">
    
            <tr style="font-weight: bold;">
        
                <td style="width: 15%; text-align: left;">Unique Id</td>
        
                <td style="width: 35%; text-align: left;">Name</td>
        
                <td style="width: 35%; text-align: center;">NPI</td>

                <td style="width: 15%; text-align: center;">Federal Tax Id</td>
            
            </tr>
    
            <tr>
            
                <td style="text-align: left; "><asp:Label ID="ProviderDemographicUniqueId" Text="" runat="server" /></td>
            
                <td style="text-align: left; "><asp:Label ID="ProviderDemographicName" runat="server" /></td>
                
                <td style="text-align: center;"><asp:Label ID="ProviderDemographicNpi" Width="95%" runat="server" /></td>
                
                <td style="text-align: center;"><asp:Label ID="ProviderDemographicFederalId" Width="95%" runat="server" /></td>

            </tr>
            
        </table>
        
        <div style="background-color:#CCCCCC; height:1px; margin-left: 4px; margin-right: 8px;"></div>

        <table cellpadding="0" cellspacing="0" style="width: 100%; padding: 2px;">
    
            <tr style="font-weight: bold;">
        
                <td style="width: 34%; text-align: left;">Ethnicity</td>
                
                <td style="width: 30%; text-align: left;">Citizenship</td>

                <td style="width: 08%; text-align: center;">Gender</td>
            
                <td style="width: 08%; text-align: center;">Age</td>
                        
                <td style="width: 10%; text-align: center;">Birth Date</td>

                <td style="width: 10%; text-align: center;">Death Date</td>
                
            </tr>

            <tr>
            
                <td style="text-align: left;"><asp:Label ID="ProviderDemographicEthnicity" runat="server" /></td>
                
                <td style="text-align: left;"><asp:Label ID="ProviderDemographicCitizenship" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="ProviderDemographicGender" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="ProviderDemographicCurrentAge" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="ProviderDemographicBirthDate" runat="server" /></td>

                <td style="text-align: center;"><asp:Label ID="ProviderDemographicDeathDate" runat="server" /></td>
            
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
            
            <td style="text-align: left; "><asp:Label ID="ProviderDemographicTelephone" runat="server" /></td>
            
            <td style="text-align: left; "><asp:Label ID="ProviderDemographicEmail" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="ProviderDemographicEmergencyPhone" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="ProviderDemographicCellPhone" runat="server" /></td>

            <td style="text-align: center;"><asp:Label ID="ProviderDemographicFax" runat="server" /></td>
            
            <td style="text-align: left;"><asp:Label ID="ProviderDemographicPager" runat="server" /></td>

        </tr>
        
    </table>

</div> <!-- END OF PAGE CONTENT -->
