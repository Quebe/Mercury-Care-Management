if (window.addEventListener) { window.addEventListener('resize', Workspace_Body_OnResize, false); } else { window.attachEvent('onresize', Workspace_Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', Workspace_Page_Load, false); } else { window.attachEvent('onload', Workspace_Page_Load); }


var isWorkspacePainting = false;


function Workspace_Page_Load() {

    setTimeout('Workspace_OnPaint()', 250);

    return;

}

function Workspace_OnPaint(forEvent) {

    if (isWorkspacePainting) { return; }

    isWorkspacePainting = true;


    var splitter = $find("ctl00_ApplicationContentControl_MyAssignedWorkGridSplitter");

    var container = document.getElementById("ApplicationContent");

    if ((splitter == null) || (container == null)) {

        isWorkspacePainting = false;

        setTimeout('Workspace_OnPaint ()', 100);

        return;
    }

    var adjustedHeight = 0; // AMOUNT TO RESIZE THE HEIGHT


    // SET MARGIN HEIGHT FOR .125IN, ACCOUNT FOR IE PADDING VERSUS CHROME PADDING (USING FIRST CONTAINER)

    var marginHeight = document.getElementById("Workspace_MyAssignedWorkContainer_TitleTable").offsetTop - document.getElementById("Workspace_MyAssignedWorkContainer").offsetTop;

    var marginMultiplier = (window.addEventListener) ? 3 : 3; // ALLOWS FOR DIFFERENT MULTIPLIERS BASED ON IE/NON-IE


    // RESIZE MY ASSIGNED WORK CONTAINER

    var myAssignedWorkContainer = document.getElementById("Workspace_MyAssignedWorkContainer");

    var myAssignedWorkContainerTop = document.getElementById("Workspace_MyAssignedWorkContainer").offsetTop;


    // MODIFY TOP TO RELATIVE OF APPLICATION CONTENT CONTROL
    myAssignedWorkContainerTop = myAssignedWorkContainerTop - container.offsetTop;

    // RESIZE
    adjustedHeight = container.offsetHeight - myAssignedWorkContainerTop - (marginHeight * marginMultiplier) - 1;

    myAssignedWorkContainer.style.height = adjustedHeight + "px";

    
    // TOP FROM THE FULL WINDOW 
    var splitterTop = document.getElementById("ctl00_ApplicationContentControl_ContentMultiPage").offsetTop;

    // MODIFY TOP TO BE RELATIVE TO THE APPLICATION CONTENT CONTROL
    splitterTop = splitterTop - container.offsetTop;

    adjustedHeight = container.offsetHeight - splitterTop - (marginHeight * marginMultiplier) - 1;


    splitter.set_width("100%");

    splitter.set_height(adjustedHeight);



    var myAssignedCasesSplitter = $find("ctl00_ApplicationContentControl_MyAssignedCasesGridSplitter");

    myAssignedCasesSplitter.set_width("100%");

    myAssignedCasesSplitter.set_height(adjustedHeight);

    var myTeamCasesSplitter = $find("ctl00_ApplicationContentControl_MyTeamCasesGridSplitter");

    myTeamCasesSplitter.set_width("100%");

    myTeamCasesSplitter.set_height(adjustedHeight);

    var caseLoadSplitter = $find("ctl00_ApplicationContentControl_CaseLoadGridSplitter");

    caseLoadSplitter.set_width("100%");

    caseLoadSplitter.set_height(adjustedHeight);

    
    isWorkspacePainting = false;

    return;

}


function Workspace_Body_OnResize(forEvent) {

    Workspace_OnPaint(forEvent);

    setTimeout('Workspace_OnPaint()', 250);

    return;

}


