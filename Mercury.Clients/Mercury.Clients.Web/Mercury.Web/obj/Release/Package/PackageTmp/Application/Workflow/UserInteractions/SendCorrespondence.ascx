<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SendCorrespondence.ascx.cs" Inherits="Mercury.Web.Application.Workflow.UserInteractions.SendCorrespondence" %>

<%@ Register TagPrefix="Telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntitySendCorrespondence" Src="/Application/Controls/EntitySendCorrespondence.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="MemberDemographics" Src="~/Application/Controls/MemberDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="ProviderDemographics" Src="~/Application/Controls/ProviderDemographics.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityDocumentHistory" Src="~/Application/Controls/EntityDocumentHistory.ascx"  %>

<%@ Register TagPrefix="MercuryUserControl" TagName="EntityNoteHistory" Src="~/Application/Controls/EntityNoteHistory.ascx"  %>

<Telerik:RadAjaxManagerProxy ID="TelerikAjaxManagerProxy" runat="server">

    <AjaxSettings>
    
        <Telerik:AjaxSetting AjaxControlID="EntityInfoSendCorrespondenceSplitter" >
 
            <UpdatedControls>
            
                <Telerik:AjaxUpdatedControl ControlID="EntityInfoSendCorrespondenceSplitter" />

            </UpdatedControls>        

        </Telerik:AjaxSetting>
    
    </AjaxSettings>

</Telerik:RadAjaxManagerProxy>


<div id="UserInteractionContentEntityContainer"> 

    <Telerik:RadSplitter ID="EntityInfoSendCorrespondenceSplitter" Orientation="Horizontal" BackColor="#6699CC" Width="100%" runat="server">
    
        <Telerik:RadPane ID="EntityInfoPane" Height="22" Scrolling="None" runat="server">
        
            <Telerik:RadSlidingZone ID="EntityInfoSlidingZone" ClickToOpen="true" Height="22" SlideDirection="Bottom" runat="server">
            
                <Telerik:RadSlidingPane ID="EntityInfoDemographics" Title="Demographics" Height="400" runat="server">
                                        
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">
                    
                        <MercuryUserControl:MemberDemographics ID="MemberDemographicsControl" Visible="false" runat="server" />
                        
                        <MercuryUserControl:ProviderDemographics ID="ProviderDemographicsControl" Visible="false" runat="server" />

                    </div>
               
                </Telerik:RadSlidingPane>
            
                <Telerik:RadSlidingPane ID="EntityInfoSendCorrespondenceHistory" Title="Correspondence History" Height="400" runat="server">
                                
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">

                        <MercuryUserControl:EntityDocumentHistory ID="EntityDocumentHistoryControl" runat="server" />
                   
                    </div>
                    
               </Telerik:RadSlidingPane>
            
                <Telerik:RadSlidingPane ID="EntityNoteHistorySlidingPane" Title="Notes" Height="400" runat="server">
                                
                    <div style="width: 100%; font-family: segoe ui, arial; font-size: 12px; overflow: auto;">
                   
                        <MercuryUserControl:EntityNoteHistory ID="EntityNoteHistoryControl" runat="server" />

                    </div>
                    
               </Telerik:RadSlidingPane>

            </Telerik:RadSlidingZone>
            
        </Telerik:RadPane>    

        <Telerik:RadPane ID="EntitySendCorrespondencePane" BackColor="White"  Height="100%" runat="server">

            <MercuryUserControl:EntitySendCorrespondence ID="EntitySendCorrespondenceControl" OnSendCorrespondence="EntitySendCorrespondenceControl_OnSendCorrespondence" runat="server" />          
        
        </Telerik:RadPane>
        
    </Telerik:RadSplitter>

</div>


<Telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">

<script type="text/javascript">

    if (window.addEventListener) { window.addEventListener('resize', UserInteractionSendCorrespondenceEntity_Body_OnResize, false); } else { window.attachEvent('onresize', UserInteractionSendCorrespondenceEntity_Body_OnResize); }

    if (window.addEventListener) { window.addEventListener('load', UserInteractionSendCorrespondenceEntity_Page_Load, false); } else { window.attachEvent('onload', UserInteractionSendCorrespondenceEntity_Page_Load); }


    function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

    function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


    var isUserInteractionSendCorrespondenceEntityPainting = false;

    setTimeout('UserInteractionSendCorrespondenceEntity_OnPaint()', 250);


    function UserInteractionSendCorrespondenceEntity_Page_Load() {

        setTimeout('UserInteractionSendCorrespondenceEntity_OnPaint()', 250);

        return;

    }

    function UserInteractionSendCorrespondenceEntity_OnPaint(forEvent) {

        if (isUserInteractionSendCorrespondenceEntityPainting) { return; }

        isUserInteractionSendCorrespondenceEntityPainting = true;


        var container = document.getElementById("UserInteractionContentEntityContainer");

        var panel = document.getElementById("WorkflowContentPanel");

        var splitter = $find("<%= EntityInfoSendCorrespondenceSplitter.ClientID %>");

        if ((container == null) || (splitter == null)) {

            isUserInteractionSendCorrespondenceEntityPainting = false;

            setTimeout('UserInteractionSendCorrespondenceEntity_OnPaint ()', 100);

            return;

        }


        container.style.height = (container.parentNode.offsetHeight) + "px";


        var adjustedHeight = container.offsetHeight;

        splitter.set_width("100%");

        splitter.set_height(adjustedHeight);


        isUserInteractionSendCorrespondenceEntityPainting = false;

        return;

    }


    function UserInteractionSendCorrespondenceEntity_Body_OnResize(forEvent) {

        UserInteractionSendCorrespondenceEntity_OnPaint(forEvent);

        return;

    }

</script>

</Telerik:RadScriptBlock>