if (window.addEventListener) { window.addEventListener('resize', Body_OnResize, false); } else { window.attachEvent('onresize', Body_OnResize); }

if (window.addEventListener) { window.addEventListener('keydown', Document_OnKeyDown, false); } else { document.attachEvent('onkeydown', Document_OnKeyDown); }


var radDesignerSplitter;

var radFormSplitter;

var isPainting = false;

var draggingObject = null;

var isDragging = false;

var dragOffsetX = 0;

var dragOffsetY = 0;


Page_Load();


function GetWindowWidth() { return (window.innerWidth) ? window.innerWidth : document.documentElement.clientWidth; }

function GetWindowHeight() { return (window.innerHeight) ? window.innerHeight : document.documentElement.clientHeight; }


function FormDesignerState_Reset () { 

    // RESET TITLE BAR AND DROP ZONE VIEWS

    TitleBars_ShowHide(document.getElementById("ToggleTitleBars").checked);

    DropZones_ShowHide(document.getElementById("ToggleDropZones").checked);

    return;

}

function Page_Load() {

    Paint();

    return;

}

function Paint(forEvent) {

    if (isPainting) { return; }

    isPainting = true;


    var designerSplitter = $find("DesignerSplitter");

    var documentSplitter = $find("DocumentSplitter");

    var titleBar = document.getElementById("DesignerTitleBar");
    
    if ((designerSplitter == null) || (documentSplitter == null) || (titleBar == null)) {

        isPainting = false;

        setTimeout("Paint ();", 100);

        return;

    }



    var availableHeight = GetWindowHeight();

    availableHeight = availableHeight - titleBar.offsetTop;

    availableHeight = availableHeight - titleBar.offsetHeight;


    if (availableHeight < 100) { availableHeight = 100; }



    designerSplitter.set_width("100%");

    designerSplitter.set_height(availableHeight);

    documentSplitter.set_width ("100%");

    documentSplitter.set_height (availableHeight);

    
    isPainting = false;

    return;

}

function Body_OnResize(forEvent) { Paint(); }

function Document_OnKeyDown(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var restrictedKeyPressed = false;

    restrictedKeyPressed = restrictedKeyPressed || (forEvent.keyCode == 116); // F5

    // restrictedKeyPressed = restrictedKeyPressed || (forEvent.keyCode == 008); // Backspace

    restrictedKeyPressed = restrictedKeyPressed || ((forEvent.ctrlKey) && (forEvent.keyCode == 78)); // Crtl-N

    restrictedKeyPressed = restrictedKeyPressed || ((forEvent.ctrlKey) && (forEvent.keyCode == 82)); // Ctrl-R


    if (restrictedKeyPressed) {

        forEvent.keyCode = 0;

        forEvent.cancelBubble = true;

        forEvent.returnValue = false;

        if (forEvent.preventDefault) { forEvent.preventDefault(); }

        if (forEvent.stopPropagation) { forEvent.stopPropagation(); }

        return false;

    }

    return true;

}

function Pane_OnChange(sender, eventArgs) { Paint(); }

function FindControlByBubbleClassTag(initialControl, className, tagName) {

    var itemFound = false;

    if (initialControl == null) { return null; }

    var searchControl = initialControl;


    if (searchControl.className != undefined) {

        if ((searchControl.className == className) && (searchControl.tagName == tagName)) {

            itemFound = true;

        }

    }

    while (!itemFound) {

        searchControl = searchControl.parentNode;

        if ((searchControl == null) || (searchControl == undefined)) { return null; }

        if (searchControl.className != undefined) {

            if ((searchControl.className == className) && (searchControl.tagName == tagName)) {

                itemFound = true;

            }

        }

    }

    if (itemFound) { return searchControl; }

    return null;

}

function DeleteControl_OnClick(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    eventObject = FindControlByBubbleClassTag(eventObject, "FormControl", "DIV");

    if (eventObject != null) {

        document.getElementById("DeleteControlClientId").value = eventObject.id;

        confirmationPrompt = "<div style=\"line-height: 150%; padding-top: 4px;\">";

        confirmationPrompt = confirmationPrompt + "Delete control ";

        confirmationPrompt = confirmationPrompt + eventObject.getAttribute("formControlName");

        confirmationPrompt = confirmationPrompt + " of type " + eventObject.getAttribute("formControlType") + "? </div>";


        confirmationPrompt = "Delete control " + eventObject.getAttribute("formControlName") + " of type " + eventObject.getAttribute("formControlType") + "?";

        if (confirm(confirmationPrompt, "Delete Control")) {

            DeleteControl_OnConfirm(true);

        }


        if (window.event) { window.event.cancelBubble = true; }

        if (forEvent.preventDefault != null) { forEvent.preventDefault(); }


        return false;

    }

}

function DeleteControl_OnConfirm(confirmed) {

    if (confirmed) {

        document.getElementById("DeleteControl").click();

    }

}

function ControlProperties_OnClick(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    eventObject = FindControlByBubbleClassTag(eventObject, "FormControl", "DIV");

    if (eventObject != null) {

        var controlId = eventObject.id.substring(10, 60);

        selectedControlId = document.getElementById("PropertiesSelectedControlId");

        selectedControlId.value = controlId;

        document.getElementById("SelectControlProperties").click();


        if (window.event) { window.event.cancelBubble = true; }

        if (forEvent.preventDefault != null) { forEvent.preventDefault(); }

    }

    return;

}

function MouseCoordinates(forEvent) {

    if (forEvent.pageX || forEvent.pageY) { return { x: forEvent.pageX, y: forEvent.pageY }; }

    return {
        x: forEvent.clientX + document.body.scrollLeft - document.body.clientLeft,
        y: forEvent.clientY + document.body.scrollTop - document.body.clientTop
    };

}


function ToolbarItem_OnDragBegin(forEvent) {

    if (isDragging) { return true; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    var toolbarItem = FindControlByBubbleClassTag(eventObject, "DesignerToolbarItem", "DIV");

    if (toolbarItem != null) {

        isDragging = true;

        draggingObject = toolbarItem.cloneNode(true);

        draggingObject.id = "DragObject_" + draggingObject.id;

        draggingObject.style.position = "absolute";

        draggingObject.style.zIndex = "100";

        draggingObject.style.display = "block";

        draggingObject.style.width = toolbarItem.offsetWidth + "px";

        draggingObject.style.opacity = ".80";

        document.body.appendChild(draggingObject);

        draggingObject.style.cursor = "move";

        document.body.style.cursor = "move";


        DropZones_ShowHide(true);


        dragOffsetX = MouseCoordinates(forEvent).x;

        dragOffsetY = MouseCoordinates(forEvent).y;

        draggingObject.style.left = (dragOffsetX + 4) + "px";

        draggingObject.style.top = (dragOffsetY + 4) + "px";

        document.onmousemove = ToolbarItem_OnDragging;

        document.onmouseup = ToolbarItem_OnDrop;

        if (window.event) { window.event.cancelBubble = true; window.event.returnValue = false; }

        if (forEvent.preventDefault != null) { forEvent.preventDefault(); }

        return false;

    }

    return;

}


function ToolbarItem_OnDragging(forEvent) {

    if (!isDragging) { return; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    if (forEvent.button <= 1 && isDragging) {

        draggingObject.style.left = (forEvent.clientX + 4) + "px";

        draggingObject.style.top = (forEvent.clientY + 4) + "px";

        return true;

    }

    return true;

}

function ToolbarItem_OnDrop(forEvent) {

    DropZones_ShowHide(document.getElementById("ToggleDropZones").checked);

    document.onmousemove = null;

    document.onmouseup = null;

    document.body.removeChild(draggingObject);

    document.body.style.cursor = "default";

    isDragging = false;

    return;

}


function FormControl_OnDragBegin(forEvent) {

    if (isDragging) { return true; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    var formControl = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "TABLE");

    if (formControl == null) { formControl = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "DIV"); }

    if (formControl == null) { return; }

    formControl = FindControlByBubbleClassTag(eventObject, "FormControl", "DIV");

    if (formControl != null) {

        isDragging = true;

        draggingObject = formControl.cloneNode(true);

        draggingObject.id = "DragObject_" + draggingObject.id;

        draggingObject.style.position = "absolute";

        draggingObject.style.zIndex = "10";

        draggingObject.style.display = "block";

        draggingObject.style.width = formControl.offsetWidth + "px";

        draggingObject.style.opacity = ".80";

        draggingObject.style.cursor = "move";

        document.body.style.cursor = "move";

        document.body.appendChild(draggingObject);


        DropZones_ShowHide(true);


        dragOffsetX = MouseCoordinates(forEvent).x;

        dragOffsetY = MouseCoordinates(forEvent).y;

        draggingObject.style.left = (dragOffsetX + 4) + "px";

        draggingObject.style.top = (dragOffsetY + 4) + "px";

        document.onmousemove = FormControl_OnDragging;

        document.onmouseup = FormControl_OnDrop;

        if (window.event) { window.event.cancelBubble = true; }

        if (forEvent.preventDefault != null) { forEvent.preventDefault(); }

        return false;

    }

    return;

}


function FormControl_OnDragging(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    if (forEvent.button <= 1 && isDragging) {

        draggingObject.style.left = (forEvent.clientX + 4) + "px";

        draggingObject.style.top = (forEvent.clientY + 4) + "px";

        return false;

    }

    return;

}

function FormControl_OnDrop(forEvent) {

    DropZones_ShowHide(document.getElementById("ToggleDropZones").checked);

    document.onmousemove = null;

    document.onmouseup = null;

    document.body.style.cursor = "default";

    document.body.removeChild(draggingObject);

    isDragging = false;

    return;

}


function TitleBars_ShowHide(showTitleBars) {

    var titleBar;

    var cellElements = document.getElementsByTagName("div");

    for (currentCellIndex = 0; currentCellIndex < cellElements.length; currentCellIndex++) {

        if (cellElements[currentCellIndex].className == "FormControl_TitleBar") {

            titleBar = cellElements[currentCellIndex];

            if (titleBar.firstChild.firstChild != null) {

                titleBar.firstChild.style.display = "none";

                titleBar.lastChild.style.display = (showTitleBars) ? "block" : "none";

            }

        }

    }

    cellElements = document.getElementsByTagName("table");

    for (currentCellIndex = 0; currentCellIndex < cellElements.length; currentCellIndex++) {

        if (cellElements[currentCellIndex].className == "FormControl_TitleBar") {

            titleBar = cellElements[currentCellIndex];

            if (titleBar.firstChild.firstChild != null) {

                titleBar.firstChild.style.display = "none";

                titleBar.lastChild.style.display = (showTitleBars) ? "block" : "none";

            }

        }

    }

    return;

}


function DropZones_ShowHide(showDropZones) {

    if (draggingObject != null) {

        sourceControlType = draggingObject.getAttribute("formControlType");

    }

    else { sourceControlType = ""; }

    var cellElements = document.getElementsByTagName("td");

    for (currentCellIndex = 0; currentCellIndex < cellElements.length; currentCellIndex++) {

        if (cellElements[currentCellIndex].className == "ColumnDropZoneCell") {

            cellElements[currentCellIndex].style.display = (showDropZones) ? "block" : "none";

            cellElements[currentCellIndex].firstChild.style.display = (showDropZones) ? "block" : "none";

        }

    }

    cellElements = document.getElementsByTagName("div");

    for (currentCellIndex = 0; currentCellIndex < cellElements.length; currentCellIndex++) {

        if (cellElements[currentCellIndex].className == "DropZone") {

            cellElements[currentCellIndex].style.display = (showDropZones) ? "block" : "none";

        }

    }

    return;

}

function DropAllowed(dropZone) {

    sourceControlType = draggingObject.getAttribute("formControlType");

    dropZoneType = dropZone.getAttribute("dropZoneType");


    var compareControl = dropZone;

    do {

        if (compareControl != null) {

            if ("DragObject_" + compareControl.id == draggingObject.id) { return false; }

        }

        compareControl = compareControl.parentNode;

    } while (compareControl != null);

    switch (sourceControlType) {

        case "Section":

            if (dropZoneType == "Section") { return false; }

            return true;

            break;

        case "SectionColumn":

            if (dropZoneType == "Section") { return true; }

            break;

        case "Label":

        case "Text":

        case "Input":

        case "Selection":

        case "Button":

        case "Entity":

        case "Collection":

        case "Address":

        case "Service":

        case "Metric":

            if (dropZoneType == "SectionColumn") { return true; }

            break;

    }

    return false;

}

function DropZone_OnMouseOver(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    if ((isDragging) && (DropAllowed(eventObject))) {

        eventObject.className = "DropZoneAllow";

        return false;

    }

}

function DropZone_OnMouseOut(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    if (isDragging) {

        eventObject.className = "DropZone";

        return false;

    }

}

function DropZone_OnMouseUp(forEvent) {

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    if ((isDragging) && (DropAllowed(eventObject))) {

        eventObject.className = "DropZone";

        document.getElementById("DropItemSource").value = draggingObject.className + "|" + draggingObject.id;

        document.getElementById("DropItemDestination").value = eventObject.className + "|" + eventObject.id;

        document.getElementById("DropControl").click();

        return false;

    }

}

function TitleBar_OnMouseOver(forEvent) {

    return;

    if (isDragging) { return; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    titleBar = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "TABLE");

    if (titleBar != null) {

        if (titleBar.firstChild.firstChild != null) {

            titleBar.firstChild.firstChild.style.display = "block";

            titleBar.firstChild.lastChild.style.display = "none";

        }

    }

    else {

        titleBar = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "DIV");

        if (titleBar != null) {

            titleBar.firstChild.style.display = "block";

            titleBar.lastChild.style.display = "none";

        }

    }

    return;


}

function TitleBar_OnMouseOut(forEvent) {

    return;

    if (isDragging) { return; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    titleBar = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "TABLE");

    if (titleBar != null) {

        if (titleBar.firstChild.firstChild != null) {

            titleBar.firstChild.firstChild.style.display = "none";

            titleBar.firstChild.lastChild.style.display = (document.getElementById("ToggleTitleBars").checked) ? "block" : "none";

        }

    }

    else {

        titleBar = FindControlByBubbleClassTag(eventObject, "FormControl_TitleBar", "DIV");

        if (titleBar != null) {

            titleBar.firstChild.style.display = "none";

            titleBar.lastChild.style.display = (document.getElementById("ToggleTitleBars").checked) ? "block" : "none";

        }

    }

    return;

}


function FormControl_OnMouseOver(forEvent) {

    if (isDragging) { return; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    titleBar = FindControlByBubbleClassTag(eventObject, "FormControl", "DIV");

    if (titleBar != null) {

        if (titleBar.firstChild.firstChild != null) {

            titleBar.firstChild.firstChild.style.display = "block";

            titleBar.firstChild.firstChild.style.position = "relative";

            titleBar.firstChild.firstChild.style.top = "0";

            titleBar.firstChild.lastChild.style.display = "none";

        }

        if (titleBar.parentNode != null) {

            if (titleBar.parentNode != null) {

                FormControl_OnMouseOver(titleBar);

            }

        }

    }

    return;


}

function FormControl_OnMouseOut(forEvent) {

    if (isDragging) { return; }

    forEvent = (forEvent) ? forEvent : ((event) ? event : null);

    var eventObject = (forEvent.target) ? forEvent.target : ((forEvent.srcElement) ? forEvent.srcElement : null);

    titleBar = FindControlByBubbleClassTag(eventObject, "FormControl", "DIV");

    if (titleBar != null) {

        if (titleBar.firstChild.firstChild != null) {

            titleBar.firstChild.firstChild.style.display = "none";

            titleBar.firstChild.lastChild.style.display = (document.getElementById("ToggleTitleBars").checked) ? "block" : "none";

        }

    }

    return;

}

function HtmlEditor_Display(forEvent) {

    var availableWidth = GetWindowWidth();

    var availableHeight = GetWindowHeight() - 4;


    htmlEditorContainer = document.getElementById("HtmlEditorContainer");

    htmlEditorContainer.style.display = "block";


    htmlEditor = document.getElementById("HtmlEditor");

    htmlEditorCommandButtons = document.getElementById("HtmlEditorCommandButtons");

    htmlEditorCommandButtons.style.width = (htmlEditor.offsetWidth) + "px";


    var positionLeft = parseInt((availableWidth - htmlEditorContainer.offsetWidth) / 2);

    if (positionLeft < 0) { positionLeft = 0; }

    var positionTop = parseInt((availableHeight - htmlEditorContainer.offsetHeight) / 2);

    if (positionTop < 0) { positionTop = 0; }

    htmlEditorContainer.style.left = positionLeft + "px";

    htmlEditorContainer.style.top = positionTop + "px";

    return;

}

function HtmlEditor_Close(forEvent) {

    htmlEditorContainer = document.getElementById("HtmlEditorContainer");

    htmlEditorContainer.style.display = "none";

    return;

}


function EventHandlerEditorDialog_OnOpenDialog(controlType) {

    var availableWidth = GetWindowWidth();

    var availableHeight = GetWindowHeight() - 4;


    var windowManager = GetRadWindowManager();

    var radWindow = windowManager.GetWindowByName("DialogEventHandlerEditor");

    radWindow.SetWidth(795);

    radWindow.SetHeight(700);

    radWindow.SetUrl("Windows/EventHandlerEditor.aspx?PageInstanceId=" + document.getElementById("PageInstanceId").value + "&ControlId=" + document.getElementById("PropertiesSelectedControlId").value + "&EventName=" + document.getElementById("PropertiesControlEvent_Input").value);

    radWindow.Show();

    radWindow.SetWidth(795);

    radWindow.SetHeight(700);

    radWindow.Center();

}

function EventHandlerEditorDialog_OnClientClose(forEvent) {

    var windowManager = GetRadWindowManager();

    var radWindow = windowManager.GetWindowByName("DialogEventHandlerEditor");

    radWindow.SetUrl("/WindowLoading.aspx");

    return;

}


function EventHandlerEditorDialog_Callback(sender, eventName) {

    var windowManager = GetRadWindowManager();

    var radWindow = windowManager.GetWindowByName("DialogEventHandlerEditor");

    radWindow.SetUrl("/WindowLoading.aspx");

    return;

}

function SelectionItemsDialog_Show() {

    var dialog = $find("SelectionItemsDialog");

    if (dialog != null) { dialog.show(); }

    else { alert("Unable to show Selection Items window."); }

    return;

}

function OpenDialog_Show() {

    var dialog = $find("OpenFormDialog");

    if (dialog != null) { dialog.show(); }

    else { alert("Unable to show Open Dialog window."); }

    return;

}

function OnRequestServerRefresh(sender, eventArgs) {

    document.getElementById("RequestServerRefresh").click();


    return;

}
