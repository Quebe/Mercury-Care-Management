<%@ Page Language="C#" MasterPageFile="~/PageBranding/NoBranding.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Mercury.Web.Default" %>


<asp:Content ID="contentLogOn" ContentPlaceHolderID="pageContent" runat="server">

        <asp:TextBox id="JavaScriptEnabled" Text="false" style="display:none;" runat="server" />

        <asp:Panel ID="uiPanelLogOn" width="100%" runat="server">
            
            <!-- Window [LogOn] (BEGIN) -->

            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Black">
            
                <tr style="height:1px"><td></td></tr>

                <tr>
                    <td style="width:1px"></td>
                    <td style="background:background-image; background-image:url('images/background_fade_blue.jpg'); background-repeat:repeat-y; background-color:Blue">
                        <table width="100%" cellpadding="0"  cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%;background-color:Transparent">
                            <tr style="height:32px">
                            <td valign="middle" style="width:10px; text-align:center"></td>
                            <td valign="middle" style="font-weight:bold; color:white">Log On to the System</td>
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
                        <table width="100%" cellpadding="10" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:White"><tr><td style="background-color:Transparent">
                        
                        
                        <asp:Panel ID="uiPanelException" runat="server" Visible="false">
                        
                            <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                <tr>
                                    <td align="left" valign="middle" style="width:12px"><asp:Image ID="uiExceptionImage" ImageUrl="~/Images/Misc/logon_lockbang.gif" runat="server" /></td>
                                    <td align="left"><asp:Label ID="uiExceptionMessageLabel" Text="" runat="server" /></td>
                                </tr>
                            </table>
                        
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>

                        </asp:Panel>
                        

                        <asp:Panel ID="uiPanelIAmSelection" runat="server" Visible="true">
                        
                            <table width="100%" cellpadding="2" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                            
                                <tr>
                                    <td align="left" style="width:80px">I am a(n):</td>
                                    <td align="left" style="width:245px">
                                        <asp:RadioButtonList ID="uiIAmSelectionRadioButtonList" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Enabled="true" Selected="False" Text="Provider"  Value="Provider"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Selected="False" Text="Member"    Value="Member"></asp:ListItem>
                                            <asp:ListItem Enabled="true" Selected="False" Text="Associate" Value="Associate"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                
                            </table>
                        
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>
                        
                        </asp:Panel>
                            
                        
                        <asp:Panel ID="uiPanelCredentials" runat="server">
                        
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:5px"><td></td></tr></table>

                            <asp:Panel ID="uiPanelSecurityAuthority" Visible="true" runat="server">

                                <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                    <tr>
                                        <td align="left" style="width:125px">Security Authority:</td>
                                        <td align="left"><asp:DropDownList ID="uiSecurityAuthorityDropDownList" Width="98%" runat="server"></asp:DropDownList></td>
                                    </tr>
                                </table>
                                
                            </asp:Panel>

                            <div id="divLogOnNameWarning" style="display:none" >
                            
                                <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                  <tr>
                                    <td style="text-align:left"><b style="color:Red"> ** </b> Your Log On Name cannot be empty.<b style="color:Red"> ** </b></td>
                                 
                                  </tr>
                                </table>
                            
                            </div>

                            <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                <tr>
                                    <td align="left" style="width:125px">Log On Name:</td>
                                    <td align="left"><asp:TextBox ID="uiLogOnNameTextBox" Width="95%" runat="server"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td align="left">Password:</td>
                                    <td align="left"><asp:TextBox ID="uiPasswordTextBox" TextMode="Password" Width="95%" runat="server"></asp:TextBox></td>
                                </tr>
                            </table>
                            
                            <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                <tr>
                                    <td align="left" style="width:145px">
                                       <input type="checkbox" id="uiChangePasswordCheckBox" name="uiChangePasswordCheckBox" onclick="if (this.checked) { <%= uiPanelChangePassword.ClientID %>.style.display = 'block'; } else { <%= uiPanelChangePassword.ClientID %>.style.display = 'none'; }" style="cursor:hand;" />
                                       <a onclick="uiChangePasswordCheckBox.click ();" style="cursor:hand;">Change Password</a>
                                    </td>
                                    <td align="right"><asp:Button ID="uiLogOnButton" width="80" runat="server" 
                                            Text="Log On" onclick="uiLogOnButton_Click" /></td>
                                </tr>
                            
                            </table>
                            
                            <asp:Panel ID="uiPanelChangePassword" Visible="true" runat="server" style="display:none">
                            
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>
        
                                <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                                <div id="divPasswordWarning" style="display:none" >
                                
                                    <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                      <tr>
                                        <td style="text-align:left"><b style="color:Red"> ** </b> New passwords are not allowed to be blank, and the confirmation password must match the new password.<b style="color:Red"> ** </b></td>
                                     
                                      </tr>
                                    </table>
                                
                                </div>
                                
                                <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                    <tr>
                                        <td align="left" style="width:125px">New Password:</td>
                                        <td align="left"><asp:TextBox ID="uiNewPasswordTextBox" TextMode="Password" Width="95%" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left">Confirm Password:</td>
                                        <td align="left"><asp:TextBox ID="uiConfirmPasswordTextBox" TextMode="Password" Width="95%" runat="server"></asp:TextBox></td>
                                    </tr>
                                </table>
                            
                            </asp:Panel>
                        
                        </asp:Panel>

                        <asp:Panel ID="uiPanelEnvironment" Visible="false" runat="server">
                        
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:Black"><tr style="height:1px"><td></td></tr></table>
    
                            <table width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:White"><tr style="height:1px"><td></td></tr></table>

                            <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                <tr>
                                    <td align="left" style="width:125px">Environment:</td>
                                    <td align="left"><asp:DropDownList ID="uiEnvironmentDropDownList" Width="98%" runat="server"></asp:DropDownList></td>
                                </tr>
                            </table>
                            
                            <table width="100%" cellpadding="4" cellspacing="0" border="0" style="font-family:Arial;font-size:10pt;line-height:150%; background-color:Transparent">
                                <tr>
                                    <td align="left" style="width:145px">
                                    </td>
                                    <td align="right"><asp:Button ID="uiEnvironmentSelect" width="80" runat="server" 
                                            Text="Select" onclick="uiEnvironmentSelect_Click" /></td>
                                </tr>
                            
                            </table>
                        
                        </asp:Panel>

                        </td></tr></table> <!-- INNER PADDING ( END ) -->

                    </td>
                    <td style="width:1px"></td>
                </tr>

                <tr style="height:1px"><td></td></tr>

            </table>

            <!-- Window [LogOn] (End) -->
        
        </asp:Panel>

        <script type="text/javascript">

            document.getElementById("<%= JavaScriptEnabled.ClientID %>").value="true";
            
            aspnetForm.onsubmit = ValidateLogOnSubmission;
            
            function ValidateLogOnSubmission () {
            
                var isValid = true;
            
                controlLogOnNameTextBox = document.getElementById ("<%= uiLogOnNameTextBox.ClientID %>");
                
                if (controlLogOnNameTextBox != null) { 
                
                    if (controlLogOnNameTextBox.value.length == 0) {
                    
                        divLogOnNameWarning.style.display = "block";
                    
                        controlLogOnNameTextBox.style.backgroundColor = "Yellow";
                       
                        isValid = false;
                                            
                    }
                    
                }
                               
                controlNewPasswordTextBox = document.getElementById ("<%= uiNewPasswordTextBox.ClientID %>");
                
                controlConfirmPasswordTextBox = document.getElementById ("<%= uiConfirmPasswordTextBox.ClientID %>");
                
                controlChangePasswordCheckBox = document.getElementById ("uiChangePasswordCheckBox");
                
                if (controlChangePasswordCheckBox != null) {
                
                    if (controlChangePasswordCheckBox .checked) { 
                    
                        if ((controlNewPasswordTextBox.value != controlConfirmPasswordTextBox.value) || (controlNewPasswordTextBox.value.length == 0)) {
                 
                            divPasswordWarning.style.display = "block";
           
                            controlNewPasswordTextBox.style.backgroundColor = "Yellow";
                            
                            controlConfirmPasswordTextBox.style.backgroundColor = "Yellow";
                            
                            isValid = false;
                            
                        }
                        
                    }
                
                }
            
                return isValid;
                
            }
            
        </script>

</asp:Content>
