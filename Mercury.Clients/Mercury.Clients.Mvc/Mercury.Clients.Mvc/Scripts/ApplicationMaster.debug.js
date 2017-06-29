
/* TOGGLE THE NAVIGATION MENU */

function NavigationToggle() {

    var navigationBar = $("#NavigationBar");

    navigationBar.toggle(0, function () { $(window).resize(); });
    
    return;

}