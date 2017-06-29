<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContactEntity.ascx.cs" Inherits="Mercury.Web.Application.Workflow.UserInteractions.ContactEntity" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityContact" Src="~/Application/Controls/EntityContact.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityContactHistory" Src="~/Application/Controls/EntityContactHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityNoteHistory" Src="~/Application/Controls/EntityNoteHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberDemographics" Src="~/Application/Controls/MemberDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderDemographics" Src="~/Application/Controls/ProviderDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberWorkHistory" Src="~/Application/Controls/MemberWorkHistory.ascx"  %>



<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityInfoContactSplitter" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityInfoContactSplitter" />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
        <Telerik:AjaxSetting AjaxControlID="InformationPaneContactHistoryExpanded" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="InformationPaneContactHistoryExpanded" />
                
                <Telerik:AjaxUpdatedControl ControlID="EntityContactHistoryControl" LoadingPanelID="AjaxLoadingPanel"  />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
        <Telerik:AjaxSetting AjaxControlID="InformationPaneNoteHistoryExpanded" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="InformationPaneNoteHistoryExpanded" />
                
                <Telerik:AjaxUpdatedControl ControlID="EntityNoteHistoryControl" LoadingPanelID="AjaxLoadingPanel"  />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
        <Telerik:AjaxSetting AjaxControlID="InformationPaneWorkHistoryExpanded" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="InformationPaneWorkHistoryExpanded" />
                
                <Telerik:AjaxUpdatedControl ControlID="MemberWorkHistoryControl" LoadingPanelID="AjaxLoadingPanel"  />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>

<div style="display: none">

    <asp:TextBox ID="InformationPaneContactHistoryExpandedIsLoaded" Text="Unloaded" runat="server" />
    
    <asp:Button ID="InformationPaneContactHistoryExpanded" OnClick="InformationPaneContactHistoryExpanded_OnClick" runat="server" />
    
    <asp:TextBox ID="InformationPaneNoteHistoryExpandedIsLoaded" Text="Unloaded" runat="server" />
    
    <asp:Button ID="InformationPaneNoteHistoryExpanded" OnClick="InformationPaneNoteHistoryExpanded_OnClick" runat="server" />
            
    <asp:TextBox ID="InformationPaneWorkHistoryExpandedIsLoaded" Text="Unloaded" runat="server" />
    
    <asp:Button ID="InformationPaneWorkHistoryExpanded" OnClick="InformationPaneWorkHistoryExpanded_OnClick" runat="server" />

</div>

<div id="UserInteractionContentEntityContainer"> 

    <Telerik:RadSplitter ID="EntityInfoContactSplitter" Orientation="Horizontal" BackColor="#6699CC" Width="100%" runat="server">
    
        <Telerik:RadPane ID="EntityInfoPane" Height="22" Scrolling="None" runat="server">
        
            <Telerik:RadSlidingZone ID="EntityInfoSlidingZone" ClickToOpen="true" Height="22" SlideDirection="Bottom" runat="server">
            
                <Telerik:RadSlidingPane ID="EntityInfoDemographics" Title="Demographics" Height="400" runat="server">
                                        
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">

                        <MercuryUserControl:MemberDemographics ID="MemberDemographicsControl" Visible="false" runat="server" />
                    
                        <MercuryUserControl:ProviderDemographics ID="ProviderDemographicsControl" Visible="false" runat="server" />

                    </div>
               
                </Telerik:RadSlidingPane>
            
                <Telerik:RadSlidingPane ID="EntityInfoContactHistory" Title="Contact History" Height="400" OnClientBeforeExpand="UserInteractionContentEntityContainer_LoadPane_ContactHistory" runat="server">
                                
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">

                        <MercuryUserControl:EntityContactHistory ID="EntityContactHistoryControl" HistoryGridHeight="365" runat="server" />
                   
                    </div>
                    
               </Telerik:RadSlidingPane>
            
                <Telerik:RadSlidingPane ID="EntityNoteHistorySlidingPane" Title="Notes" Height="400" OnClientBeforeExpand="UserInteractionContentEntityContainer_LoadPane_NoteHistory" runat="server">
                                
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">
                   
                        <MercuryUserControl:EntityNoteHistory ID="EntityNoteHistoryControl" HistoryGridHeight="365" runat="server" />

                    </div>
                    
               </Telerik:RadSlidingPane>

                <Telerik:RadSlidingPane ID="EntityInfoMemberWorkHistory" Title="Work History" Height="400" OnClientBeforeExpand="UserInteractionContentEntityContainer_LoadPane_WorkHistory" runat="server">
                                
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">
                                           
                        <MercuryUserControl:MemberWorkHistory ID="MemberWorkHistoryControl" HistoryGridHeight="365" runat="server" />

                    </div>
                    
               </Telerik:RadSlidingPane>
               
            </Telerik:RadSlidingZone>
            
        </Telerik:RadPane>    

        <Telerik:RadPane ID="EntityContactPane" BackColor="White"  Height="100%" runat="server">

            <MercuryUserControl:EntityContact ID="EntityContactControl" OnContact="EntityContactControl_OnContact" runat="server" />
        
        </Telerik:RadPane>
        
    </Telerik:RadSplitter>

</div>


<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', UserInteractionContactEntity_Body_OnResize, false); } else { window.attachEvent('onresize', UserInteractionContactEntity_Body_OnResize); }

    if (window.addEventListener) { window.addEventListener('load', UserInteractionContactEntity_Page_Load, false); } else { window.attachEvent('onload', UserInteractionContactEntity_Page_Load); }


    function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isUserInteractionContactEntityPainting = false;

    setTimeout('UserInteractionContactEntity_OnPaint()', 250);


    function UserInteractionContactEntity_Page_Load() {

        setTimeout('UserInteractionContactEntity_OnPaint()', 250);

        return;

    }

    function UserInteractionContactEntity_OnPaint(forEvent) {

        if (isUserInteractionContactEntityPainting) { return; }

        isUserInteractionContactEntityPainting = true;


        var container = document.getElementById("UserInteractionContentEntityContainer");

        var panel = document.getElementById("WorkflowContentPanel");

        var splitter = $find("<%= EntityInfoContactSplitter.ClientID %>");

        if ((container == null) || (splitter == null)) {

            isUserInteractionContactEntityPainting = false;

            setTimeout('UserInteractionContactEntity_OnPaint ()', 100);

            return;

        }


        container.style.height = (container.parentNode.offsetHeight) + "px";


        var adjustedHeight = container.offsetHeight;

        splitter.set_width("100%");

        splitter.set_height(adjustedHeight);


        isUserInteractionContactEntityPainting = false;

        return;

    }


    function UserInteractionContactEntity_Body_OnResize(forEvent) {

        UserInteractionContactEntity_OnPaint(forEvent);

        return;

    }


    function UserInteractionContentEntityContainer_LoadPane_ContactHistory(sender, args) {
    
        if (document.getElementById("<%= InformationPaneContactHistoryExpandedIsLoaded.ClientID %>").value == "Unloaded") {

            document.getElementById("<%= InformationPaneContactHistoryExpandedIsLoaded.ClientID %>").value = "Loaded";

            args.set_cancel(true);

            document.getElementById("<%= InformationPaneContactHistoryExpanded.ClientID %>").click();

        }

        return;

    }

    function UserInteractionContentEntityContainer_LoadPane_NoteHistory(sender, args) {

        if (document.getElementById("<%= InformationPaneNoteHistoryExpandedIsLoaded.ClientID %>").value == "Unloaded") {

            document.getElementById("<%= InformationPaneNoteHistoryExpandedIsLoaded.ClientID %>").value = "Loaded";

            args.set_cancel(true);

            document.getElementById("<%= InformationPaneNoteHistoryExpanded.ClientID %>").click();

        }

        return;

    }

    function UserInteractionContentEntityContainer_LoadPane_WorkHistory(sender, args) {

        if (document.getElementById("<%= InformationPaneWorkHistoryExpandedIsLoaded.ClientID %>").value == "Unloaded") {

            document.getElementById("<%= InformationPaneWorkHistoryExpandedIsLoaded.ClientID %>").value = "Loaded";

            args.set_cancel(true);

            document.getElementById("<%= InformationPaneWorkHistoryExpanded.ClientID %>").click();

        }

        return;

    }

</script>

</Telerik:RadScriptBlock>