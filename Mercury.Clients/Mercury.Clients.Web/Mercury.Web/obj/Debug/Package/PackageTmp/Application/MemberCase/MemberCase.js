if (window.addEventListener) { window.addEventListener('resize', MemberCase_Body_OnResize, false); } else { window.attachEvent('onresize', MemberCase_Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', MemberCase_Page_Load, false); } else { window.attachEvent('onload', MemberCase_Page_Load); }


var isMemberCasePainting = false;


function MemberCase_Page_Load() {

    setTimeout('MemberCase_OnPaint()', 250);

    return;

}

function MemberCase_OnPaint(forEvent) {

    if (isMemberCasePainting) { return; }

    isMemberCasePainting = true;


    var container = document.getElementById("ApplicationContent");

    if (container == null) {

        isMemberCasePainting = false;

        setTimeout('MemberCase_OnPaint ()', 100);

        return;
    }


    var applicationTitleBar = document.getElementById("ApplicationTitleBar");

    if (applicationTitleBar == null) { return; }


    var navigationBar = document.getElementById("NavigationBar");

    if (navigationBar == null) { return; }


    var applicationContent = document.getElementById("ApplicationContent");


    // GET AVAILABLE WINDOW WIDTH
    if (window.innerWidth) { windowWidth = window.innerWidth; } else { windowWidth = document.documentElement.clientWidth; }

    // GET AVAILABLE WINDOW HEIGHT
    if (window.innerHeight) { windowHeight = window.innerHeight; } else { windowHeight = document.documentElement.clientHeight; }


    availableWidth = windowWidth - 0;

    availableHeight = windowHeight - applicationTitleBar.offsetHeight;

    if (navigationBar.style.display == "") { availableHeight = availableHeight - navigationBar.offsetHeight; }


    if (availableHeight < 1) { availableHeight = 1; }

    applicationContent.style.height = availableHeight + "px";



    // RESIZE MULTIPAGE CONTAINER
    
    contentContainer = document.getElementById("ContentContainer");

    availableHeight = availableHeight - (contentContainer.offsetTop) + 20;

    if (availableHeight < 1) { availableHeight = 1; }

    contentContainer.style.height = availableHeight + "px";

    
    isMemberCasePainting = false;

    return;

}


function MemberCase_Body_OnResize(forEvent) {

    MemberCase_OnPaint(forEvent);

    setTimeout('MemberCase_OnPaint()', 250);

    return;

}


function Page_Repaint() {

    MemberCase_OnPaint();

    return;

}