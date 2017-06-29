<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityInformationMember.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityInformationMember" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>


<div id="MemberInformation" style="display: block;" runat="server">

    <table width="100%" cellpadding="0" cellspacing="0">
    
        <tr class="" style="height: 36px;">

            <td style="max-width: 24px;"><img id="MemberNoteWarning"  src="/Images/Common24/NoteWarning.png" alt="Warning from Note" style="display: none;" runat="server" /></td>   
        
            <td style="max-width: 24px;"><img id="MemberNoteCritical" src="/Images/Common24/NoteCritical.png" alt="Critical from Note" style="display: none;" runat="server" /></td>   

        
            <td style="text-align: left"><b>Member Name:</b> <asp:Label id="MemberName" Text="** No Member Selected" runat="server" /></td>
                        
            <td style="text-align: left"><b>Birth Date:</b> <asp:Label id="MemberBirthDate" Text="" runat="server" /></td>
                        
            <td style="text-align: left"><b>Age:</b> <asp:Label id="MemberAge" Text="" runat="server" /></td>
                        
            <td style="text-align: left"><b>Gender:</b> <asp:Label id="MemberGender" Text="" runat="server" /></td>
                        
            <td style="text-align: left"><b>Program:</b> <asp:Label id="MemberProgram" Text="" runat="server" /></td>
                                                
            <td style="text-align: left"><b>Id:</b> <asp:Label id="MemberProgramMemberId" Text="" runat="server" /></td>

            <td style="width: 50px; text-align: center;"><a id="MemberInformationCoverageToggle" href="javascript:MemberInformationCoverage_Toggle()" title="Toggle Coverage Information" runat="server">(more)</a></td>

        </tr>
        
    </table>

    <div id="MemberInformationCoverage" style="display: none;" runat="server">
            
        <table width="100%" cellpadding="0" cellspacing="0">
    
            <tr class="" style="height: 36px;">

                <td style="text-align: left"><b>Benefit Plan:</b> <asp:Label id="MemberCoverageBenefitPlan" Text="** Not Enrolled" runat="server" /></td>
                        
                <td style="text-align: left"><b>Coverage Type:</b> <asp:Label id="MemberCoverageType" Text="" runat="server" /></td>
                        
                <td style="text-align: left"><b>Coverage Level:</b> <asp:Label id="MemberCoverageLevel" Text="" runat="server" /></td>
                        
                <td style="text-align: left"><b>Rate Code:</b> <asp:Label id="MemberCoverageRateCode" Text="" runat="server" /></td>

            </tr>
        
        </table>
            
        <table width="100%" cellpadding="0" cellspacing="0">
    
            <tr class="" style="height: 36px;">

                <td style="text-align: left"><b>PCP Name:</b> <asp:Label id="MemberPcpName" Text="** No PCP" runat="server" /></td>
                        
                <td style="text-align: left"><b>PCP Affiliate Name:</b> <asp:Label id="MemberPcpAffiliateName" Text="" runat="server" /></td>
                        
            </tr>
        
        </table>

    </div>

</div>
                


<Telerik:RadCodeBlock runat="server">

<script type="text/javascript">

    function MemberInformationCoverage_Toggle() {

        var coverageDiv = document.getElementById("<%= MemberInformationCoverage.ClientID %>");

        var coverageAnchor = document.getElementById("<%= MemberInformationCoverageToggle.ClientID %>");

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


        if (typeof (Page_Repaint) == "function") { Page_Repaint(); }

        return;

    }
    
</script>

</Telerik:RadCodeBlock>