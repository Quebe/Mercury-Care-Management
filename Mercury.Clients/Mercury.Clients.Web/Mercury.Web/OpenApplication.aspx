<%@ Page Language="C#" MasterPageFile="~/PageBranding/NoBranding.Master" AutoEventWireup="true" CodeBehind="OpenApplication.aspx.cs" Inherits="Mercury.Web.OpenApplication" %>

<asp:Content ID="contentLogOn" ContentPlaceHolderID="pageContent" runat="server">


    <asp:Panel ID="uiPanelRequirements" width="100%" runat="server">
    
        <!-- Window [Requirements] (BEGIN) -->

        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black;font-family:Arial;font-size:10pt;line-height:150%" >
        
            <tr style="height:1px"><td></td></tr>

            <tr>
                <td style="width:1px"></td>
                <td style="background-image:url('/Images/Backgrounds/background_fade_blue.jpg'); background-repeat:repeat-y; background-color:Blue">
                    <table width="100%" cellpadding="0"  cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%;background-color:Transparent">
                        <tr style="height:32px">
                        <td style="width:10px; text-align:center; vertical-align:middle"></td>
                        <td style="vertical-align:middle; font-weight:bold; color:white">Application Requirements</td>
                        </tr>
                    </table>
                </td>
                <td style="width:1px"></td>
            </tr> 

            <tr style="height:1px"><td></td></tr>

            <tr>
                <td style="width:1px"></td>
                <td align="center">

                    <!-- INNER PADDING (BEGIN) -->
                    <table width="100%" cellpadding="10" cellspacing="0" border="0" style="background-color:White;font-family:Arial;font-size:10pt;line-height:150%;"><tr><td style="background-color:Transparent">
                    
                    
                    <asp:Panel ID="uiPanelRequirementsDetail" runat="server" Visible="true">
                    
                        <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                            <tr>
                                <td style="width:12px; text-align:left; vertical-align:middle"><asp:Image ID="Image1" ImageUrl="~/Images/Misc/requirements_javascript_icon64.jpg" runat="server" /></td>
                                <td align="left">JavaScript: This site requires that you have JavaScript enabled in your browser. JavaScript is a scripting language most often used for client-side web development, and is enabled by default.</td>
                            </tr>
                        </table>
                    
                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                            <tr>
                                <td style="width:12px; text-align:left; vertical-align:middle"><asp:Image ID="Image2" ImageUrl="~/Images/Misc/requirements_cookies_icon64.jpg" runat="server" /></td>
                                <td align="left">Cookies: This site requires that you have Cookies enabled in your browser. This is for storing Session state only, and does not collect or store any personal information.</td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                            <tr>
                                <td style="width:12px; text-align:left; vertical-align:middle"><asp:Image ID="Image3" ImageUrl="~/Images/Misc/requirements_popup_icon64.jpg" runat="server" /></td>
                                <td align="left">Popups: This site requires that you have Popup Windows enabled in your browser. This is to support dialog boxes that require actions and confirmations.</td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>

                        <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                            <tr>
                                <td style="width:12px; text-align:left; vertical-align:middle"><asp:Image ID="Image4" ImageUrl="~/Images/Misc/requirements_ie_icon64.jpg" runat="server" /></td>
                                <td align="left">This site has been tested with Microsoft Internet Explorer 8. Click <a href="http://www.microsoft.com/windows/downloads/ie/getitnow.mspx">here</a> if you need to obtain the latest version of Microsoft Internet Explorer.</td>
                            </tr>
                            <tr>
                                <td style="width:12px; text-align:left; vertical-align:middle"><asp:Image ID="Image5" ImageUrl="~/Images/Misc/requirements_firefox_icon64.jpg" runat="server" /></td>
                                <td align="left">This site has been tested with Mozilla Firefox. Click <a href="http://www.mozilla.com/en-US /">here</a> if you need to obtain the latest version of Firefox.</td>
                            </tr>
                        </table>

                        <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                    </asp:Panel>

                    </td></tr></table> <!-- INNER PADDING ( END ) -->

                </td>
                <td style="width:1px"></td>
            </tr>

            <tr style="height:1px"><td></td></tr>

        </table>

        <!-- Window [Requirements] (End) -->
    
    </asp:Panel>


    <!-- CONFIDENTIALITY STATEMENT (BEGIN) -->
    
    <asp:Panel ID="uiPanelWindowConfidentiality" runat="server" Visible="true">

        <div id="statementWindowFrame" style="position: absolute; left: 0px; top: 0px; width: 100%; height: 100%; min-width:100%; min-height:100%; display: none;">

            <div id="statementWindowBackground" style="position:absolute; left: 0px; top: 0px; width: 100%; height: 100%; min-width:100%; min-height:100%; background-color:Black;" />
            
        </div>
                
        <!-- STATEMENT WINDOW CONTENT (BEGIN) -->
        <div id="statementWindowContent" style="display:none; background-color:Black; position:absolute; left:50%; top:25%; width: 350px; margin-left:-175px; margin-top:0px;">    
            
            <table width="100%" cellpadding="0"  cellspacing="0" border="0" style="background-color:Black;font-family:Arial;font-size:10pt;line-height:150%" >
            
                <tr style="height:1px"><td></td></tr>

                <tr style="height:32px">
                    <td style="width:1px"></td>
                    <td style="background-image:url('../images/backgrounds/fade_blue.jpg');">
                        <table width="100%" cellpadding="0"  cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%;background-color:Transparent">
                            <tr style="height:32px">
                            <td style="width:10px; height:32px; text-align:center; vertical-align:middle"></td>
                            <td valign="middle" style="font-weight:bold; color:white">Confidentiality Statement</td>
                            </tr>
                        </table>
                    </td>
                    <td style="width:1px"></td>
                </tr> 

                <tr style="height:1px"><td></td></tr>

                <tr>
                    <td style="width:1px"></td>
                    <td align="center">

                        <!-- INNER PADDING (BEGIN) -->
                        <table width="100%" cellpadding="10" cellspacing="0" border="0" style="background-color:White;font-family:Arial;font-size:10pt;line-height:150%;"><tr><td style="background-color:Transparent" align="left">
                        
                        <p>
                        <asp:Label ID="uiStatementLabel" runat="server"/>
                        
                        </p>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                        <p style="text-align:center">
                            <input type="button" value="Accept" onclick="acceptStatement ();" />
                        </p>

                        </td></tr></table> <!-- INNER PADDING ( END ) -->

                    </td>
                    <td style="width:1px"></td>
                </tr>

                <tr style="height:1px"><td></td></tr>

            </table>
        
        </div>
        <!-- STATEMENT WINDOW CONTENT ( END ) -->
    
    </asp:Panel>
    
    <!-- CONFIDENTIALITY STATEMENT ( END ) -->


    <!-- CONFIDENTIALITY STATEMENT CODE (BEGIN) -->

    <asp:Panel ID="uiPanelWindowConfidentialityCode" runat="server">

        <script type="text/javascript">
                        
            windowFrame = document.getElementById ('statementWindowFrame');
            windowBackground = document.getElementById ('statementWindowBackground');
            windowContent = document.getElementById ('statementWindowContent');
            
            opacityStart = 0;
            opacityEnd   = 50;
            opacityCurrent = 0;
            opacityStep  = 5;
            
            timeoutInterval = 1;
                        
            function OpacityIncrease () {
            
                opacityCurrent = opacityCurrent + opacityStep;
                
                windowBackground.style.opacity = opacityCurrent / 100;
                windowBackground.style.filter = "alpha(opacity=" + opacityCurrent + ")";
                
                if (opacityCurrent < opacityEnd) {
                    
                    var timer = setTimeout ("OpacityIncrease ()", timeoutInterval);
                    
                }
                
                else { 
                
                    windowContent.style.display = "block";

                }
                
            } // OpacityIncrease ()
            
            function DisplayStatement () {
            
                windowFrame.style.width  = document.body.offsetWidth;
                windowFrame.style.height = document.body.offsetHeight;

                windowBackground.style.opacity = 0;
                windowBackground.style.filter = "alpha(opacity=0)";
                
                windowFrame.style.display = "block";                
                windowContent.style.display = "none";
                
                var timer = setTimeout ("OpacityIncrease ()", timeoutInterval);
                
            }

            // DisplayStatement ();

            // applicationWindow = window.open('Application/Workspace/Workspace.aspx', 'MercuryApplication', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1');

            // window.close();


            // BELOW IS CORRECT

            window.location = "/Application/Workspace/Workspace.aspx";

            // BELOW IS FOR TESTING

            // window.location = "http://localhost/Application/DataExplorer/DataExplorer.aspx";

            // window.location = "http://localhost/Application/DataExplorer/Exports/DataExplorerNodeResultExportMember.aspx?NodeInstanceId=E8AAC01B-7A0D-40B5-888E-00EDCAAF6901&NodeInstanceCount=5433"

            // window.location = "http://localhost/Application/MemberCase/MemberCase.aspx?MemberCaseId=1000000008";

            // window.location = "http://localhost/Application/MemberCase/Actions/AddProblemStatement.aspx?MemberCaseId=1000000004";

            // window.location = "http://localhost/Application/MemberCase/MemberCase.aspx?MemberCaseId=0&MemberId=1211000000200724";

            // window.location = "http://localhost/Application/Configuration/Management.aspx";

            // window.location = "http://localhost/Application/Configuration/PropertyPages/CarePlan.aspx";

            // window.location = "http://localhost/Application/MemberCase/Actions/AddCarePlanGoalIntervention.aspx?MemberCaseId=1000000004&MemberCaseCarePlanGoalId=1000000189";

            //window.location = "http://localhost/Application/MemberCase/Actions/MemberCaseCarePlanAssessment.aspx?MemberCaseId=1000000004&MemberCaseCarePlanId=1000000039";

            // window.open('http://localhost/Application/Configuration/Management.aspx', 'MercuryApplication', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1');
            
                        
	        function acceptStatement () {
	        
	            windowFrame = document.getElementById ('statementWindowFrame');
	            windowContent = document.getElementById ('statementWindowContent');
	            
	            windowFrame.style.display   = "none";
	            windowContent.style.display = "none";
	            
	            applicationWindow = window.open ('Application/Workspace/Workspace.aspx', 'MercuryApplication', 'toolbar=0, location=0, directories=0, status=1, menubar=0, scrollbars=1, resizable=1');
	            
	        }
	        
	        

        </script>    
           
    </asp:Panel>    
    
    <!-- CONFIDENTIALITY STATEMENT CODE ( END ) -->
  
</asp:Content>
