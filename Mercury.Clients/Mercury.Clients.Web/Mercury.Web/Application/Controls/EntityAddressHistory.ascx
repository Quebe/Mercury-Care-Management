<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityAddressHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityAddressHistory" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityAddressHistoryGrid" >
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityAddressHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
            
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityAddressTerminateWindow_ButtonOk" >
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="EntityAddressTerminateWindow_ButtonOk" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="EntityAddressTerminateResponse" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="EntityAddressHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls>
            
        </Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>

<Telerik:RadGrid ID="EntityAddressHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="false" 

    OnItemCreated="EntityAddressHistoryGrid_OnItemCreated" 
    
    OnNeedDataSource="EntityAddressHistoryGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="EntityAddressHistoryGrid_OnPageSizeChanged"
    
    OnItemCommand="EntityAddressHistoryGrid_OnItemCommand" 
    
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="EntityAddressHistoryMasterView" TableLayout="Auto" CommandItemDisplay="None" DataKeyNames="Id" ClientDataKeyNames="Id,AddressTypeDescription,Line1,Line2,CityStateZipCode,EffectiveDate">
        
        <Columns>
    
            <Telerik:GridBoundColumn DataField="Id" Visible="false" />

            <Telerik:GridBoundColumn DataField="EntityId" Visible="false" />

            <Telerik:GridBoundColumn DataField="AddressType" Visible="false" />

            <Telerik:GridBoundColumn DataField="AddressTypeDescription" HeaderText="Address Type" HeaderStyle-Width="160" ItemStyle-Width="160"  />

            <Telerik:GridBoundColumn DataField="Line1" HeaderText="Line1" Visible="true" />

            <Telerik:GridBoundColumn DataField="Line2" HeaderText="Line2" Visible="true" />

            <Telerik:GridBoundColumn DataField="CityStateZipCode" HeaderText="City, State, ZIP Code" Visible="true" />

            <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" HeaderStyle-Width="80" ItemStyle-Width="80" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

            <Telerik:GridBoundColumn DataField="TerminationDateDescription" HeaderText="Termination" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
            
            <Telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" HeaderStyle-Width="75" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="75" ItemStyle-HorizontalAlign="Center" Visible="false">
                    
                <ItemTemplate>
                    
                    <div><a href="javascript:EntityAddress_Terminate ('<%# Eval ("Id") %>');" title="Terminate Address">(terminate)</a></div>                        
                        
                </ItemTemplate>

            </Telerik:GridTemplateColumn>

        </Columns>
        
        <PagerStyle Mode="NextPrevAndNumeric" />
    
    </MasterTableView>
    
    <ClientSettings>          
    
        <Selecting AllowRowSelect="true" />
        
        <Scrolling AllowScroll="false" />
    
    </ClientSettings>
    
    <PagerStyle NextPageText="Next" PrevPageText="Previous"></PagerStyle>

</Telerik:RadGrid>


<!-- DIALOG WINDOWS (BEGIN) -->

<Telerik:RadWindowManager ID="WorkspaceWindowManager" runat="server">

    <Windows>
            
        <Telerik:RadWindow ID="EntityAddressTerminateWindow" Behaviors="Close" Modal="true" Width="450" Height="355" VisibleStatusbar="false"  Title="Terminate an Address" Skin="Sunset" runat="server">

            <ContentTemplate>
                
                <div id="DialogTerminateContent">

                    <div style="margin: .125in" >

                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Terminate an address?</p>
                            
                        <p>This will set the termination date for an address. The termination date must be equal to or greater than the effective

                        date of the address. 
                    
                        </p>
                        
                        <div style="display: none;"><Telerik:RadTextBox ID="EntityAddressTerminateId" runat="server"></Telerik:RadTextBox></div>

                        <p style="font-weight: bold;"><asp:Label ID="EntityAddressTerminateType" runat="server" /></p>

                        <p><asp:Label ID="EntityAddressTerminateLine1" runat="server" /></p>

                        <p><asp:Label ID="EntityAddressTerminateLine2" runat="server" /></p>

                        <p><asp:Label ID="EntityAddressTerminateCityStateZipCode" runat="server" /></p>

                        <p></p>

                        <div style="overflow: hidden; width: 98%; margin-top: .125in;">

                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 80px; padding-right: 10px; font-weight: bold;">Effective Date:</td>

                                <td style="width: 80px; overflow: hidden;"><asp:Label ID="EntityAddressTerminateEffectiveDate" runat="server" /></td>
                                    
                                <td style="width: 100px; padding-right: 10px; font-weight: bold;">Termination Date:</td>

                                <td><Telerik:RadDatePicker ID="EntityAddressTerminateTerminationDate" Width="100" runat="server" /></td>

                            </tr></table>

                        </div>
                        
                        <asp:Label ID="EntityAddressTerminateResponse" ForeColor="Red" runat="server" />

                        <div style="height: 5px;"></div>
                                
                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="EntityAddressTerminateWindow_ButtonOk" Text="OK" OnClick="EntityAddressTerminateWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="EntityAddressTerminateWindow_ButtonCancel" Text="Cancel" OnClientClick="return EntityAddressTerminateWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

                            </tr></table>

                        </div>
            
                    </div>

                </div>

            </ContentTemplate>

        </Telerik:RadWindow>
            
    </Windows>

</Telerik:RadWindowManager>

<Telerik:RadCodeBlock ID="WindowFunctions" runat="server" >

    <script language="javascript" type="text/javascript">

        function EntityAddress_Terminate(entityAddressId) {

            // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

            var entityAddressGrid = $find("<%= EntityAddressHistoryGrid.ClientID %>");

            var masterTableView = entityAddressGrid.get_masterTableView();

            var masterTableViewRows = masterTableView.get_dataItems();


            var entityAddressTerminateType = "";

            var entityAddressTerminateLine1 = "";

            var entityAddressTerminateLine2 = "";

            var entityAddressTerminateCityStateZipCode = "";

            var entityAddressTerminateEffectiveDate = "";


            // FOREACH ROW, FIND CLICKED ROW AND SELECT IT (REMOVE SELECTION FROM OTHER ROWS)

            for (currentRow in masterTableViewRows) {


                var currentEntityAddressId = masterTableViewRows[currentRow].getDataKeyValue("Id");

                if (currentEntityAddressId == entityAddressId) {

                    selectedRow = masterTableViewRows[currentRow];

                    masterTableViewRows[currentRow].set_selected(true);


                    entityAddressTerminateType = masterTableViewRows[currentRow].getDataKeyValue("AddressTypeDescription");

                    entityAddressTerminateLine1 = masterTableViewRows[currentRow].getDataKeyValue("Line1");

                    entityAddressTerminateLine2 = masterTableViewRows[currentRow].getDataKeyValue("Line2");

                    entityAddressTerminateCityStateZipCode = masterTableViewRows[currentRow].getDataKeyValue("CityStateZipCode");

                    entityAddressTerminateEffectiveDate = masterTableViewRows[currentRow].getDataKeyValue("EffectiveDate");

                    entityAddressTerminateEffectiveDate = entityAddressTerminateEffectiveDate.replace("12:00:00 AM", "");

                }

                else { masterTableViewRows[currentRow].set_selected(false); }

            }



            // SET DIALOG PROPERTIES

            var entityAddressTerminateId = $find("<%= EntityAddressTerminateId.ClientID %>");

            entityAddressTerminateId.set_value(entityAddressId);

            var EntityAddressTerminateType = document.getElementById("<%= EntityAddressTerminateType.ClientID %>");

            EntityAddressTerminateType.innerText = entityAddressTerminateType;

            var EntityAddressTerminateLine1 = document.getElementById("<%= EntityAddressTerminateLine1.ClientID %>");

            EntityAddressTerminateLine1.innerText = entityAddressTerminateLine1;

            var EntityAddressTerminateLine2 = document.getElementById("<%= EntityAddressTerminateLine2.ClientID %>");

            EntityAddressTerminateLine2.innerText = entityAddressTerminateLine2;

            var EntityAddressTerminateCityStateZipCode = document.getElementById("<%= EntityAddressTerminateCityStateZipCode.ClientID %>");

            EntityAddressTerminateCityStateZipCode.innerText = entityAddressTerminateCityStateZipCode;

            var EntityAddressTerminateEffectiveDate = document.getElementById("<%= EntityAddressTerminateEffectiveDate.ClientID %>");

            EntityAddressTerminateEffectiveDate.innerText = entityAddressTerminateEffectiveDate;

            var EntityAddressTerminateResponse = document.getElementById("<%= EntityAddressTerminateResponse.ClientID %>");

            EntityAddressTerminateResponse.innerText = "";

            // SHOW DIALOG

            var dialogWindow = $find("<%= EntityAddressTerminateWindow.ClientID %>");

            dialogWindow.show();

            return;

        }

        function EntityAddressTerminateWindow_Close() {

            var dialogWindow = $find("<%= EntityAddressTerminateWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }


    </script>
        
</Telerik:RadCodeBlock>

<!-- DIALOG WINDOWS ( END ) -->

