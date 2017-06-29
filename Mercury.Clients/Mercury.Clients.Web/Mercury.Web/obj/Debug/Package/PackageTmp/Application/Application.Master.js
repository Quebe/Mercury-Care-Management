if (window.addEventListener) { window.addEventListener('resize', Body_OnResize, false); } else { window.attachEvent('onresize', Body_OnResize); }

if (window.addEventListener) { window.addEventListener('load', Page_Load, false); } else { window.attachEvent('onload', Page_Load); }

function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


var isPainting = false;


function Page_Load() {

    OnPaint();

    return;

}



function OnPaint(forEvent) {

    if (isPainting) { return; }

    isPainting = true;


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


    isPainting = false; 

    return;

}


function Body_OnResize(forEvent) {

    OnPaint(forEvent);

}


function NavigationToggle() {

    var navigationBar = document.getElementById("NavigationBar");


    if (navigationBar.style.display == "none") {

        // EXPAND NAVIGATION 

        navigationBar.style.display = "";

    }

    else { // COLLAPSE NAVIGATION 

        navigationBar.style.display = "none";

    }

    return;

}