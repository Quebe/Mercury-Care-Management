if (window.addEventListener) { window.addEventListener('resize', Workflow_Body_OnResize, false); } else { window.attachEvent('onresize', Workflow_Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', Workflow_Page_Load, false); } else { window.attachEvent('onload', Workflow_Page_Load); }


function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


var isWorkflowPainting = false;


function Workflow_Page_Load() {

    setTimeout('Workflow_OnPaint()', 250);

    return;

}

function Workflow_OnPaint(forEvent) {

    if (isWorkflowPainting) { return; }

    isWorkflowPainting = true;

/*    
<div id="WorkflowContentSection" style="padding-left: .125in; padding-bottom: .125in; padding-right: .125in;">

    <div id="WorkflowContentContainer" class="BackgroundColorComplementNormal BorderColorDark" style="background-color: White; padding: .125in; overflow: scroll;">
    
        <asp:Panel ID="WorkflowContentPanel" BackColor="White" runat="server" />
*/

    var section = document.getElementById("WorkflowContentSection");

    var container = document.getElementById("WorkflowContentContainer");

    var panel = document.getElementById("WorkflowContentPanel");

    if ((container == null) || (panel == null)) {

        isWorkflowPainting = false;

        setTimeout('Workflow_OnPaint ()', 100);

        return;
    }


    var adjustedHeight = 0; // AMOUNT TO RESIZE THE HEIGHT


    // SET MARGIN HEIGHT FOR .125IN, ACCOUNT FOR IE PADDING VERSUS CHROME PADDING (USING FIRST CONTAINER)

    var marginHeight = panel.offsetTop - container.offsetTop;

    var marginMultiplier = (window.addEventListener) ? 3 : 3; // ALLOWS FOR DIFFERENT MULTIPLIERS BASED ON IE/NON-IE


    adjustedHeight = GetWindowHeight() - section.offsetTop - (marginHeight);

    if (adjustedHeight < 0) { adjustedHeight = 0; }

    section.style.height = adjustedHeight + "px";


    adjustedHeight = adjustedHeight - (marginHeight * 2);

    if (adjustedHeight < 0) { adjustedHeight = 0; }

    container.style.height = adjustedHeight + "px";


    adjustedHeight = adjustedHeight;

    panel.style.height = adjustedHeight + "px";


    isWorkflowPainting = false;

    return;

}


function Workflow_Body_OnResize(forEvent) {

    Workflow_OnPaint(forEvent);

    // setTimeout('Workflow_OnPaint()', 250);

    return;

}


function Page_Repaint() {

    Workflow_OnPaint();

    return;

}