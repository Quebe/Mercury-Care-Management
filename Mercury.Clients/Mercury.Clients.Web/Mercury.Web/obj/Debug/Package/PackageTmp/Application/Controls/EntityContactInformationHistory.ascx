<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EntityContactInformationHistory.ascx.cs" Inherits="Mercury.Web.Application.Controls.EntityContactInformationHistory" %>


<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div style="display: none"><asp:TextBox ID="UserControlInstanceId" Text="" runat="server" /></div>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityContactInformationHistoryGrid" >
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityContactInformationHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />
                
            </UpdatedControls>
            
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="EntityContactInformationTerminateWindow_ButtonOk" >
        
            <UpdatedControls>

                <Telerik:AjaxUpdatedControl ControlID="EntityContactInformationTerminateWindow_ButtonOk" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="EntityContactInformationTerminateResponse" LoadingPanelID="AjaxLoadingPanel" />

                <Telerik:AjaxUpdatedControl ControlID="EntityContactInformationHistoryGrid" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls>
            
        </Telerik:AjaxSetting>
        
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>

<Telerik:RadGrid ID="EntityContactInformationHistoryGrid" AllowPaging="true" AllowCustomPaging="true" EnableViewState="true" 

    OnItemCreated="EntityContactInformationHistoryGrid_OnItemCreated" 
    
    OnNeedDataSource="EntityContactInformationHistoryGrid_OnNeedDataSource" 
    
    OnPageSizeChanged="EntityContactInformationHistoryGrid_OnPageSizeChanged"
    
    OnInsertCommand="EntityContactInformationHistoryGrid_OnInsertUpdateCommand"

    OnUpdateCommand="EntityContactInformationHistoryGrid_OnInsertUpdateCommand"

    OnItemCommand="EntityContactInformationHistoryGrid_OnItemCommand" 
       
    AutoGenerateColumns="false" runat="server">

    <MasterTableView Name="EntityContactInformationHistoryMasterView" TableLayout="Auto" DataKeyNames="Id" 

        CommandItemDisplay="None"     

        ClientDataKeyNames="Id,ContactTypeInt32,ContactTypeDescription,NumberFormatted,NumberExtension,Email,EffectiveDate,TerminationDate">
        
        <Columns>

            <Telerik:GridBoundColumn DataField="Id" Visible="false" />

            <Telerik:GridBoundColumn DataField="EntityId" Visible="false" />

            <Telerik:GridBoundColumn DataField="ContactType" Visible="false" />

            <Telerik:GridBoundColumn DataField="ContactTypeInt32" Visible="false" />

            <Telerik:GridBoundColumn DataField="ContactTypeDescription" HeaderText="Contact Type" HeaderStyle-Width="160" ItemStyle-Width="160"  />
            
            <Telerik:GridBoundColumn DataField="NumberFormatted" HeaderText="Number" Visible="true" />

            <Telerik:GridBoundColumn DataField="NumberExtension" HeaderText="Extension" Visible="true" />

            <Telerik:GridBoundColumn DataField="Email" HeaderText="Email" Visible="true" />

            <Telerik:GridBoundColumn DataField="EffectiveDate" HeaderText="Effective" HeaderStyle-Width="80" ItemStyle-Width="80" DataFormatString="{0:MM/dd/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />

            <Telerik:GridBoundColumn DataField="TerminationDate" Visible="false" />

            <Telerik:GridBoundColumn DataField="TerminationDateDescription" HeaderText="Termination" HeaderStyle-Width="80" ItemStyle-Width="80" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="true" />
             
            <Telerik:GridEditCommandColumn HeaderText="Action" HeaderStyle-Width="30" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="30" ItemStyle-HorizontalAlign="Center"></Telerik:GridEditCommandColumn>
      
            <Telerik:GridTemplateColumn HeaderText="Action" AllowFiltering="false" HeaderStyle-Width="75" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="75" ItemStyle-HorizontalAlign="Center" Visible="false">
                    
                <ItemTemplate>
                    
                    <div><a href="javascript:EntityContactInformation_Terminate ('<%# Eval ("Id") %>');" title="Terminate ContactInformation">(terminate)</a></div>                        
                        
                </ItemTemplate>

            </Telerik:GridTemplateColumn>
         
        </Columns>
        
        <PagerStyle Mode="NextPrevAndNumeric" />

        <EditFormSettings EditFormType="Template">
        
            <FormTemplate>
            
                <div style="padding: 10px 10px 10px 10px;">

                    <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr style="height: 32px;">
                
                        <td style="white-space: nowrap; padding-right: 8px;">Type:</td>

                        <td style="padding-right: 8px;">

                            <Telerik:RadComboBox ID="EntityContactInformationEditContactType" Width="130" Text='<%# Bind ("ContactTypeInt32") %>' runat="server">
                
                                <Items>

                                    <Telerik:RadComboBoxItem Text="Alternate Facsimile" Value="10" />

                                    <Telerik:RadComboBoxItem Text="Alternate Telephone" Value="9" />

                                    <Telerik:RadComboBoxItem Text="Email" Value="3" />

                                    <Telerik:RadComboBoxItem Text="Emergency Phone" Value="6" />

                                    <Telerik:RadComboBoxItem Text="Facsimile" Value="2" />

                                    <Telerik:RadComboBoxItem Text="Mobile" Value="7" />

                                    <Telerik:RadComboBoxItem Text="Pager" Value="8" />

                                    <Telerik:RadComboBoxItem Text="Telephone" Value="1" Selected="true" />
                    
                                </Items>
                
                            </Telerik:RadComboBox>

                        </td>

                        <td style="width: 60px; text-align: right; padding-right: 8px;">Number:</td>

                        <td style="width: 110px; padding-right: 8px;"><Telerik:RadMaskedTextBox ID="EntityContactInformationEditNumber" Width="90" Mask="(###) ###-####" Text='<%# Bind ("NumberFormatted") %>' runat="server"></Telerik:RadMaskedTextBox></td>

                        <td style="width: 80px; text-align: right; padding-right: 8px;">Extension:</td>

                        <td style="width: 110px; padding-right: 8px;"><Telerik:RadTextBox ID="EntityContactInformationEditExtension" Width="50" MaxLength="20" Text='<%# Bind ("NumberExtension") %>'  runat="server"></Telerik:RadTextBox></td>
                        
                        <td style="width: 60px; text-align: right; padding-right: 8px;">Email:</td>

                        <td style="width: 100%;"><Telerik:RadTextBox ID="EntityContactInformationEditEmail" Width="100%" MaxLength="60" runat="server"></Telerik:RadTextBox></td>
                        
                    </tr></table>
                    
                    <table cellpadding="0" cellspacing="0" border="0" style="padding-top: 10px;" ><tr>

                            <td style="width: 80px; padding-right: 10px;">Effective Date:</td>
                            
                            <td><Telerik:RadDatePicker ID="EntityContactInformationEditEffectiveDate" Width="100" MinDate="01/01/1900" MaxDate="12/31/9999" runat="server" /></td>

                            <td style="width: 100px; padding-right: 10px;">Termination Date:</td>

                            <td><Telerik:RadDatePicker ID="EntityContactInformationEditTerminationDate" Width="100" MinDate="01/01/1900" MaxDate="12/31/9999" runat="server" /></td>

                    </tr></table>
                    
                </div>


                <div style="padding: 0px 10px 10px 10px;">
   
                    <table cellpadding="0" cellspacing="0" border="0" width="100%"><tr>

                        <td><asp:Label ID="EntityContactInformationSaveResponse" ForeColor="Red" runat="server"></asp:Label></td>
                                    
                        <td style="width: 80px; padding-right: 10px;">
                        
                            <asp:Button ID="EntityContactInformationEditUpdate" Text='<%# (Container is GridEditFormInsertItem) ? "Insert" : "Update" %>'

                                CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'

                                Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" 

                                runat="server"/>

                        </td>
                
                        <td style="width: 80px; padding-right: 10px;">

                            <asp:Button ID="EntityContactInformationEditCancel" Text="Cancel" CausesValidation="false" CommandName="Cancel" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" />
                        
                        </td> 

                    </tr></table>

                </div>
               
            </FormTemplate>
        
        </EditFormSettings>
    
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
            
        <Telerik:RadWindow ID="EntityContactInformationTerminateWindow" Behaviors="Close" Modal="true" Width="450" Height="365" VisibleStatusbar="false"  Title="Terminate a Contact Information Record" Skin="Sunset" runat="server">

            <ContentTemplate>
                
                <div id="DialogTerminateContent">

                    <div style="margin: .125in" >

                        <p class="ColorDark" style="margin-left: .125in; margin-right: .125in; font-weight: bold">Terminate a Contact Information Record?</p>
                            
                        <p>This will set the termination date for a contact information record. The termination date must be equal to or greater than the effective

                        date. 
                    
                        </p>
                        
                        <div style="display: none;"><Telerik:RadTextBox ID="EntityContactInformationTerminateId" runat="server"></Telerik:RadTextBox></div>

                        <p style="font-weight: bold;"><asp:Label ID="EntityContactInformationTerminateType" runat="server" /></p>

                        <p style="display: none;"><b>Sequence: </b><asp:Label ID="EntityContactInformationTerminateSequence" runat="server" /></p>

                        <p><b>Number:</b> <asp:Label ID="EntityContactInformationTerminateNumber" runat="server" /></p>

                        <p><b>Extension:</b> <asp:Label ID="EntityContactInformationTerminateExtension" runat="server" /></p>

                        <p><b>Email: </b><asp:Label ID="EntityContactInformationTerminateEmail" runat="server" /></p>

                        <p></p>

                        <div style="overflow: hidden; width: 98%; margin-top: .125in;">

                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 80px; padding-right: 10px; font-weight: bold;">Effective Date:</td>

                                <td style="width: 80px; overflow: hidden;"><asp:Label ID="EntityContactInformationTerminateEffectiveDate" runat="server" /></td>
                                    
                                <td style="width: 100px; padding-right: 10px; font-weight: bold;">Termination Date:</td>

                                <td><Telerik:RadDatePicker ID="EntityContactInformationTerminateTerminationDate" Width="100" runat="server" /></td>

                            </tr></table>

                        </div>
                        
                        <asp:Label ID="EntityContactInformationTerminateResponse" ForeColor="Red" runat="server" />

                        <div style="height: 5px;"></div>
                                
                        <div class="BackgroundColorComplementNormal" style="margin-top: 5px; margin-bottom: 5px; padding-left: 5px; padding-left: 5px; height: 1px; width: 98%"></div>

                        <div style="height: 20px; padding: 0px 10px 0px 10px;">
   
                            <table cellpadding="0" cellspacing="0" border="0"><tr>

                                <td style="width: 100%;">&nbsp</td>
                                    
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="EntityContactInformationTerminateWindow_ButtonOk" Text="OK" OnClick="EntityContactInformationTerminateWindow_ButtonOk_OnClick" Width="73px" Font-Names="segoe ui, arial"  Font-Size="11px" Height="24" runat="Server" /></td>
                
                                <td style="width: 80px; padding-right: 10px;"><asp:Button ID="EntityContactInformationTerminateWindow_ButtonCancel" Text="Cancel" OnClientClick="return EntityContactInformationTerminateWindow_Close ();" Width="73px" Font-Names="segoe ui, arial" Height="24" Font-Size="11px" runat="server" /></td> 

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

        function EntityContactInformation_Terminate(entityContactInformationId) {

            // RETREIVE GRID OBJECT, MASTER TABLE VIEW, AND ROW COLLECTION 

            var entityContactInformationGrid = $find("<%= EntityContactInformationHistoryGrid.ClientID %>");

            var masterTableView = entityContactInformationGrid.get_masterTableView();

            var masterTableViewRows = masterTableView.get_dataItems();


            var entityContactInformationTerminateType = "";

            var entityContactInformationTerminateSequence = "";

            var entityContactInformationTerminateNumber = "";

            var entityContactInformationTerminateExtension = "";

            var entityContactInformationTerminateEmail = "";

            var entityContactInformationTerminateEffectiveDate = "";


            // FOREACH ROW, FIND CLICKED ROW AND SELECT IT (REMOVE SELECTION FROM OTHER ROWS)

            for (currentRow in masterTableViewRows) {


                var currentEntityContactInformationId = masterTableViewRows[currentRow].getDataKeyValue("Id");

                if (currentEntityContactInformationId == entityContactInformationId) {

                    selectedRow = masterTableViewRows[currentRow];

                    masterTableViewRows[currentRow].set_selected(true);


                    entityContactInformationTerminateType = masterTableViewRows[currentRow].getDataKeyValue("ContactTypeDescription");

                    entityContactInformationTerminateSequence = masterTableViewRows[currentRow].getDataKeyValue("Sequence");

                    entityContactInformationTerminateNumber = masterTableViewRows[currentRow].getDataKeyValue("NumberFormatted");

                    entityContactInformationTerminateExtension = masterTableViewRows[currentRow].getDataKeyValue("NumberExtension");

                    entityContactInformationTerminateEmail = masterTableViewRows[currentRow].getDataKeyValue("Email");

                    entityContactInformationTerminateEffectiveDate = masterTableViewRows[currentRow].getDataKeyValue("EffectiveDate");

                    entityContactInformationTerminateEffectiveDate = entityContactInformationTerminateEffectiveDate.replace("12:00:00 AM", "");

                }

                else { masterTableViewRows[currentRow].set_selected(false); }

            }



            // SET DIALOG PROPERTIES

            var entityContactInformationTerminateId = $find("<%= EntityContactInformationTerminateId.ClientID %>");

            entityContactInformationTerminateId.set_value(entityContactInformationId);

            var EntityContactInformationTerminateType = document.getElementById("<%= EntityContactInformationTerminateType.ClientID %>");

            EntityContactInformationTerminateType.innerText = entityContactInformationTerminateType;

            var EntityContactInformationTerminateSequence = document.getElementById("<%= EntityContactInformationTerminateSequence.ClientID %>");

            EntityContactInformationTerminateSequence.innerText = entityContactInformationTerminateSequence;

            var EntityContactInformationTerminateNumber = document.getElementById("<%= EntityContactInformationTerminateNumber.ClientID %>");

            EntityContactInformationTerminateNumber.innerText = entityContactInformationTerminateNumber;

            var EntityContactInformationTerminateExtension = document.getElementById("<%= EntityContactInformationTerminateExtension.ClientID %>");

            EntityContactInformationTerminateExtension.innerText = entityContactInformationTerminateExtension;

            var EntityContactInformationTerminateEmail = document.getElementById("<%= EntityContactInformationTerminateEmail.ClientID %>");

            EntityContactInformationTerminateEmail.innerText = entityContactInformationTerminateEmail;

            var EntityContactInformationTerminateEffectiveDate = document.getElementById("<%= EntityContactInformationTerminateEffectiveDate.ClientID %>");

            EntityContactInformationTerminateEffectiveDate.innerText = entityContactInformationTerminateEffectiveDate;

            var EntityContactInformationTerminateResponse = document.getElementById("<%= EntityContactInformationTerminateResponse.ClientID %>");

            EntityContactInformationTerminateResponse.innerText = "";

            // SHOW DIALOG

            var dialogWindow = $find("<%= EntityContactInformationTerminateWindow.ClientID %>");

            dialogWindow.show();

            return;

        }

        function EntityContactInformationTerminateWindow_Close() {

            var dialogWindow = $find("<%= EntityContactInformationTerminateWindow.ClientID %>");

            dialogWindow.close();

            return false;

        }


    </script>
        
</Telerik:RadCodeBlock>

<!-- DIALOG WINDOWS ( END ) -->

