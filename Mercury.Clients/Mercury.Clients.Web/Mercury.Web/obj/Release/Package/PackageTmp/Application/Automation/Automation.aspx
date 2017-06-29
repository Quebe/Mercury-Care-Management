<%@ Page Title="" Language="C#" MasterPageFile="~/Application/Application.Master" AutoEventWireup="true" CodeBehind="Automation.aspx.cs" Inherits="Mercury.Web.Application.Automation.Automation" %>

<%@ MasterType VirtualPath="~/Application/Application.Master" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="AutomationContentHead" ContentPlaceHolderID="head" runat="server">

    <link rel="Stylesheet" href="/Styles/PropertyPage.css" type="text/css" />

</asp:Content>


<asp:Content ID="AutomationContentControl" ContentPlaceHolderID="ApplicationContentControl" runat="server">

<asp:ScriptManagerProxy ID="AjaxScriptManagerProxy" runat="server">

    <Scripts>
        
    </Scripts>

</asp:ScriptManagerProxy>

<Telerik:RadAjaxManagerProxy ID="AjaxManagerProxy" runat="server">

    <AjaxSettings>

        <Telerik:AjaxSetting AjaxControlID="JobCorrespondenceSelection">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="JobCorrespondenceSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
            
                <Telerik:AjaxUpdatedControl ControlID="JobDescriptionGrid" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
        
        <Telerik:AjaxSetting AjaxControlID="JobPrinterSelection">
        
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="JobPrinterSelection" LoadingPanelID="AjaxLoadingPanelWhiteout" />
            
                <Telerik:AjaxUpdatedControl ControlID="JobDescriptionGrid" LoadingPanelID="AjaxLoadingPanel" />

            </UpdatedControls>
        
        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="AutomationContent" style="padding: .125in;">

    <!-- AUTOMATION SELECTION/DEFINITION (BEGIN) -->
    
    <div class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in">
    
        <table width="100%" cellpadding="0" cellspacing="0">
        
            <tr>

                <td style="white-space: nowrap;">Automation Job Description</td>

                <td></td>

                <td colspan="2" style="text-align: right">
                
                        <asp:LinkButton ID="LinkButton6" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(new)</asp:LinkButton>
                
                        <asp:LinkButton ID="LinkButton3" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(open)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton5" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(save)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton4" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(save as)</asp:LinkButton>

                        <asp:LinkButton ID="LinkButton1" CssClass="NoDecoration ColorComplementDarker HoverTextBlack" style="white-space: nowrap" runat="server">(query)</asp:LinkButton>

                </td>

            </tr>

            <tr style="height: 10px;"><td>&nbsp</td></tr>
            
        </table>
        
        Select Correspondence:

        <Telerik:RadListBox ID="JobCorrespondenceSelection" Width="100%" Height="120" SelectionMode="Multiple" CheckBoxes="true" AllowReorder="true" AutoPostBack="true" AutoPostBackOnReorder="true" OnItemCheck="JobCorrespondenceSelection_OnItemCheck" OnReordered="JobCorrespondenceSelection_OnReordered" runat="server"></Telerik:RadListBox>
        
        <div style="height: .125in;">&nbsp</div>

        <table cellpadding="0" cellspacing="0"><tr>

            <td style="width: 80px; white-space: nowrap;">Correspondence to Send:</td>

            <td style="width: 80px; white-space: nowrap;">
            
                <asp:RadioButtonList ID="JonCorrespondenceSendSelection" RepeatDirection="Horizontal" runat="server">
                
                    <asp:ListItem Text="Ready to Send" Value="0" Selected="True"></asp:ListItem>

                    <asp:ListItem Text="Previously Sent on Date:" Value="1"></asp:ListItem>
                
                </asp:RadioButtonList>

            </td>

            <td><Telerik:RadDatePicker ID="JobCorrespondenceSendDate" Width="100px" runat="server"></Telerik:RadDatePicker></td>

            
        </tr></table>
        

        <div style="height: .125in;">&nbsp</div>

        <table cellpadding="0" cellspacing="0"><tr>

            <td style="width: 80px; white-space: nowrap;">Printer:</td>

            <td style="width: 300px;"><Telerik:RadComboBox ID="JobPrinterSelection" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="JobPrinterSelection_OnSelectedIndexChanged" runat="server"></Telerik:RadComboBox></td>

            <td style="width: 90px; text-align: center; white-space: nowrap;">(Defaults)</td>

            <td style="width: 90px; white-space: nowrap;">Resolution:</td>

            <td style="width: 300px;"><Telerik:RadComboBox ID="JobPrinterDefaultResolutionSelection" Width="100%" runat="server"></Telerik:RadComboBox></td>

            <td style="width: 90px; white-space: nowrap;">Color:</td>

            <td style="width: 300px;"><Telerik:RadComboBox ID="JobPrinterDefaultColorSelection" Width="100%" runat="server"></Telerik:RadComboBox></td>

            <td style="width: 90px; white-space: nowrap;">Input Bin:</td>

            <td style="width: 300px;"><Telerik:RadComboBox ID="JobPrinterDefaultInputBinSelection" Width="100%" runat="server"></Telerik:RadComboBox></td>

            <td style="width: 90px; white-space: nowrap;">Output Bin:</td>

            <td style="width: 300px;"><Telerik:RadComboBox ID="JobPrinterDefaultOutputBinSelection" Width="100%" runat="server"></Telerik:RadComboBox></td>

        </tr></table>
        
        <div style="height: .125in;">&nbsp</div>

        <Telerik:RadGrid ID="JobDescriptionGrid"  Width="100%" Height="200px" runat="server">

            <MasterTableView AutoGenerateColumns="false">

                <Columns>
                
                    <Telerik:GridBoundColumn DataField="CorrespondenceId" Visible="false" />

                    <Telerik:GridBoundColumn DataField="CorrespondenceName" HeaderText="Correspondence" />

                    <Telerik:GridBoundColumn DataField="CorrespondenceContentDescription" HeaderText="Content" />
                    
                    <Telerik:GridBoundColumn HeaderText="Use Defaults" />

                    <Telerik:GridTemplateColumn HeaderText="Resolution">
                    
                        <ItemTemplate></ItemTemplate>

                        <EditItemTemplate>
                        
                            <Telerik:RadComboBox ID="JobPrinterContentResolutionSelection" runat="server"></Telerik:RadComboBox>
                        
                        </EditItemTemplate>
                    
                    </Telerik:GridTemplateColumn>
                
                    <Telerik:GridTemplateColumn HeaderText="Color">
                    
                        <ItemTemplate></ItemTemplate>

                        <EditItemTemplate>
                        
                            <Telerik:RadComboBox ID="JobPrinterContentColorSelection" runat="server"></Telerik:RadComboBox>
                        
                        </EditItemTemplate>
                    
                    </Telerik:GridTemplateColumn>
                    
                    <Telerik:GridTemplateColumn HeaderText="Input Bin">
                    
                        <ItemTemplate></ItemTemplate>

                        <EditItemTemplate>
                        
                            <Telerik:RadComboBox ID="JobPrinterContentInputBinSelection" runat="server"></Telerik:RadComboBox>
                        
                        </EditItemTemplate>
                    
                    </Telerik:GridTemplateColumn>
                    
                    <Telerik:GridTemplateColumn HeaderText="Output Bin">
                    
                        <ItemTemplate></ItemTemplate>

                        <EditItemTemplate>
                        
                            <Telerik:RadComboBox ID="JobPrinterContentOutputBinSelection" runat="server"></Telerik:RadComboBox>
                        
                        </EditItemTemplate>
                    
                    </Telerik:GridTemplateColumn>

                </Columns>
            
            </MasterTableView>
        
        </Telerik:RadGrid>

    </div>

    <!-- AUTOMATION SELECTION/DEFINITION ( END ) -->
    
    <div style="height: .125in;">&nbsp</div>


</div>



<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', Automation_Body_OnResize, false); } else { window.attachEvent('onresize', Automation_Body_OnResize); }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isAutomationPainting = false;

    setTimeout('Automation_OnPaint()', 500);


    function Automation_OnPaint(forEvent) {

        if (isAutomationPainting) { return; }

        isAutomationPainting = true;


//        var container = document.getElementById("= AutomationResultsSection.ClientID ");

//        var splitter = $find("= AutomationResultsGridSplitter.ClientID ");

//        if ((container == null) || (splitter == null)) {

//            isAutomationPainting = false;

//            setTimeout('Automation_OnPaint ()', 100);

//            return;

//        }


//        var availableHeight = GetWindowHeight() - container.offsetTop;


//        availableHeight = availableHeight - (13 * 3); // MARGIN * 2

//        availableHeight = availableHeight - 10;

//        if (availableHeight < 100) { availableHeight = 100; }

//        container.style.height = availableHeight + "px";

//        splitter.set_width("100%");

//        splitter.set_height(availableHeight - 36);


        isAutomationPainting = false;

        return;

    }


    function Automation_Body_OnResize(forEvent) {

        Automation_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>

    
</asp:Content>
