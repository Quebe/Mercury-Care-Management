<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityAddress.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityAddress" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="ButtonOk">
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="ButtonOk" />
                
                <Telerik:AjaxUpdatedControl ControlID="ActionResponseLabel" />
           
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityAddressZipCode" >
        
            <UpdatedControls>
        
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCounty" LoadingPanelID="AjaxLoadingPanel" UpdatePanelRenderMode="Inline" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCity" LoadingPanelID="AjaxLoadingPanel" UpdatePanelRenderMode="Inline" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressState" LoadingPanelID="AjaxLoadingPanel" UpdatePanelRenderMode="Inline" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressZipCode" LoadingPanelID="AjaxLoadingPanel" UpdatePanelRenderMode="Inline" />
                    
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityAddressCity">
        
            <UpdatedControls>
        
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressZipCode" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCity" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressState" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCounty" />
                    
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityAddressState">
        
            <UpdatedControls>
        
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressZipCode" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCity" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressState" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCounty" />
                    
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityAddressCounty">
        
            <UpdatedControls>
        
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressZipCode" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCity" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressState" />
                    
                    <Telerik:AjaxUpdatedControl ControlID="EntityAddressCounty" />
                    
            </UpdatedControls>
        
        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>




<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<div style="margin: 10px;">

<div style="font-family: segoe ui, arial; padding-left: 2px; padding-top: 2px; padding-bottom:2px; font-size: 9pt; line-height: 150%; height: 24px; color: White; background-color: #330066;">

       <b>Current and Historical Addresses</b>
      
</div>  

    <Telerik:RadGrid ID="EntityAddressContentGrid" Height="175" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 
            
            AutoGenerateColumns="false" runat="server">

        <MasterTableView Name="EntityAddressContentMasterView" TableLayout="Auto" DataKeyNames="EntityAddressId">

            <Columns>
                                                   
                <Telerik:GridBoundColumn DataField="EntityAddressId" Visible="false" />

                <Telerik:GridBoundColumn DataField="AddressType" HeaderText="Address Type" Visible="true" />

                <Telerik:GridBoundColumn DataField="Line1" HeaderText="Line1" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="Line2" HeaderText="Line2" Visible="true" />

                <Telerik:GridBoundColumn DataField="City" HeaderText="City" Visible="true" />

                <Telerik:GridBoundColumn DataField="State" HeaderText="State" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="ZipCode" HeaderText="Zip Code" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="ZipPlus4" HeaderText="Zip Plus 4" Visible="false" />
                
                <Telerik:GridBoundColumn DataField="PostalCode" HeaderText="Postal Date" Visible="false" />
                
                <Telerik:GridBoundColumn DataField="County" HeaderText="County" Visible="false" />
                
                <Telerik:GridBoundColumn DataField="Longitude" HeaderText="Longitude" Visible="false" />
                
                <Telerik:GridBoundColumn DataField="Latitude" HeaderText="Latitude" Visible="false" />
                
                <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective Date" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="TerminationDate" HeaderText="Termination Date" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="CreateAccountName" HeaderText="Created By" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="CreateDate" HeaderText="Create Date" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="ModifiedAccountName" HeaderText="Modified By" Visible="true" />
                
                <Telerik:GridBoundColumn DataField="ModifiedDate" HeaderText="Modified Date" Visible="true" />
                
            </Columns>
        
        </MasterTableView>
        
        <ClientSettings>
        
            <Selecting AllowRowSelect="true" />
            
            <Scrolling AllowScroll="true" />
        
        </ClientSettings>
        
    </Telerik:RadGrid>

</div>

<div style="margin: 10px;">

    <div style="border: solid 1px #bbd7fa;">
    
        <div style="font-family: segoe ui, arial; padding-left: 2px; padding-top: 2px; padding-bottom:2px; font-size: 9pt; line-height: 150%; height: 24px; color: White; background-color: #330066;">

            <b>New Address:</b>
          
        </div> 

        <table cellpadding="4" cellspacing="2" border="0" style="height: auto;" >
        
            <tr><td colspan="8">&nbsp</td></tr>
            
            <tr>
                <td style="width: 75px;">Address Type: </td>
                
                <td>
                
                    <Telerik:RadComboBox ID="EntityAddressType" Width="240px" runat="server">
                    
                        <Items>
                        
                            <Telerik:RadComboBoxItem Text="Not Specified"    Value="0" Selected="true" Visible="false" />
                        
                            <Telerik:RadComboBoxItem Text="Physical Address" Value="1" Selected="false" />
                            
                            <Telerik:RadComboBoxItem Text="Mailing Address"  Value="31" Selected="false" />
                            
                            <Telerik:RadComboBoxItem Text="Service Location" Value="77" Selected="false" Visible="false" />
                            
                            <Telerik:RadComboBoxItem Text="Alternate Physical Address" Value="101" Selected="false" />
                            
                            <Telerik:RadComboBoxItem Text="Corrected Physical Address" Value="201" Selected="false" Visible="false" />
                            
                            <Telerik:RadComboBoxItem Text="Alternate Mailing Address"  Value="131" Selected="false" />
                            
                            <Telerik:RadComboBoxItem Text="Corrected Mailing Address"  Value="231" Selected="false" Visible="false" />
                        
                        </Items>
                           
                    </Telerik:RadComboBox>
                    
                </td>
                
                <td>Effective:</td>
                
                <td><Telerik:RadDatePicker ID="EntityAddressEffectiveDatePicker" Width="120px" runat="server"></Telerik:RadDatePicker></td>
                
                <td>Termination:</td>
            
                <td><Telerik:RadDatePicker ID="EntityAddressTerminationDatePicker" Width="120px" runat="server"></Telerik:RadDatePicker></td>
            
            </tr>
            
            </table>
            
            <table cellpadding="4" cellspacing="2" border="0" >
            
            <tr>
            
                <td align="left" style="width: 75px;">Line 1:</td>
            
                <td colspan="7"><Telerik:RadTextBox Width="100%" ID="EntityAddressLine1" EmptyMessage="(required)" TextMode="SingleLine" runat="server"></Telerik:RadTextBox></td>
                
            </tr>
                
            <tr>
            
                <td align="left" style="width: 75px;">Line 2:</td>
            
                <td colspan="7"><Telerik:RadTextBox Width="100%" ID="EntityAddressLine2" TextMode="SingleLine" runat="server"></Telerik:RadTextBox></td>
                
            </tr>
            
            <tr>
            
                <td align="left" style="width: 75px;">Zip Code:</td>
            
                <td><Telerik:RadMaskedTextBox ID="EntityAddressZipCode" Width="75px" Mask="#####-####" AutoPostBack="true" OnTextChanged="EntityAddressZipCode_OnTextChanged" EmptyMessage="(required)" runat="server"></Telerik:RadMaskedTextBox></td>
                
                <td align="left" style="width: 50px;">City:</td>
                
                <td><Telerik:RadComboBox ID="EntityAddressCity" Width="150px" OnSelectedIndexChanged="EntityAddressCity_OnSelectedIndexChanged" EmptyMessage="(required)" runat="server"></Telerik:RadComboBox></td>
                
                <td align="left" style="width: 50px;">State:</td>
                
                <td><Telerik:RadComboBox ID="EntityAddressState" Width="50px" AutoPostBack="true" OnSelectedIndexChanged="EntityAddressState_OnSelectedIndexChanged" EmptyMessage="(required)" runat="server"></Telerik:RadComboBox></td>
            
                <td align="left" style="width: 50px;">County:</td>
                
                <td><Telerik:RadComboBox ID="EntityAddressCounty" Width="150px" OnSelectedIndexChanged="EntityAddressCounty_OnSelectedIndexChanged" runat="server"></Telerik:RadComboBox></td>
            
            </tr>
            
            <tr>
            
            </tr>       

        </table>

        <table cellspacing="2" cellpadding="2" border="0" style="width: 100%; line-height: 150%; padding: 8px">
          
            <tr>
            
                <td style="width: 100%">&nbsp</td>

                <td align="right"><asp:Button ID="ButtonOk" OnClick="ButtonOk_OnClick" Text="OK" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                              
                <td align="center"><asp:Button ID="ButtonCancel" OnClick="ButtonCancel_OnClick" Text="Cancel" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>

            </tr>
          
        </table>
        
        <div style="padding: 4px; padding-top: 8px; line-height: 150%"><asp:Label ID="ActionResponseLabel" Text="" runat="server" /></div>
    
    </div>

</div>