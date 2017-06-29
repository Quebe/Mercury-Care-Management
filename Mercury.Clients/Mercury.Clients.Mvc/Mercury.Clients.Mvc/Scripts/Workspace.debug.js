
var isWorkspacePainting = false;


// PAGE RESIZE (PAINT) EVENT

$(window).resize(function () {

    if (isWorkspacePainting) { return; }

    isWorkspacePainting = true;


    var adjustedHeight = 0; // AMOUNT TO RESIZE THE HEIGHT

    var marginHeight = 0; // THE SIZE OF THE STANDARD MARGIN BEING USED


    var myWorkQueuesContainer = $("#MyWorkQueuesContainer");

    var assignedWorkContainer = $("#MyAssignedWorkContainer");


    marginHeight = $("#MyAssignedWorkContainer_TitleTable").position().top - assignedWorkContainer.position().top;


    adjustedHeight = $(window).height(); // SET TO AVAILABLE WINDOW HEIGHT 

    adjustedHeight = adjustedHeight - assignedWorkContainer.offset().top; // SUBTRACT STARTING TOP POSITION

    adjustedHeight = adjustedHeight - (marginHeight * 3); // SUBTRACT MARGIN HEIGHT


    // RESIZE OUTER CONTAINER

    assignedWorkContainer.height(adjustedHeight);


    // RESIZE MULTIPAGE CONTAINER

    adjustedHeight = adjustedHeight - (marginHeight * 3);

    $("#MyAssignedWorkContainer_Multipage").height(adjustedHeight);


    // RESIZE PAGES

    $("#MyAssignedWorkContainer_PageMyAssignedWork").height(adjustedHeight);

    $("#MyAssignedWorkContainer_PageMyAssignedCases").height(adjustedHeight);

    $("#MyAssignedWorkContainer_PageMyTeamCases").height(adjustedHeight);

    $("#MyAssignedWorkContainer_PageCaseLoads").height(adjustedHeight);


    // RESIZE GRIDS

    adjustedHeight = adjustedHeight - (marginHeight * 4) + 1;

    try { // CREATE SAFE RESIZE FOR GRIDS BECAUSE OF FIXED COLUMN LENGTHS

        $("#MyAssignedWorkGrid").setGridHeight(adjustedHeight);

        $("#MyAssignedWorkGrid").setGridWidth($("#MyAssignedWorkContainer_PageMyAssignedWork").innerWidth() - 2);

    } catch (exception) { }

    try { // CREATE SAFE RESIZE FOR GRIDS BECAUSE OF FIXED COLUMN LENGTHS

        $("#MyAssignedCasesGrid").setGridHeight(adjustedHeight);

        $("#MyAssignedCasesGrid").setGridWidth($("#MyAssignedWorkContainer_PageMyAssignedCases").innerWidth() - 2);

    } catch (exception) { }

    try { // CREATE SAFE RESIZE FOR GRIDS BECAUSE OF FIXED COLUMN LENGTHS

        $("#MyTeamCasesGrid").setGridHeight(adjustedHeight);

        $("#MyTeamCasesGrid").setGridWidth($("#MyAssignedWorkContainer_PageMyTeamCases").innerWidth() - 2);

    } catch (exception) { }

    try { // CREATE SAFE RESIZE FOR GRIDS BECAUSE OF FIXED COLUMN LENGTHS

        $("#CaseLoadsGrid").setGridHeight(adjustedHeight);

        $("#CaseLoadsGrid").setGridWidth($("#MyAssignedWorkContainer_PageCaseLoads").innerWidth() - 2);

    } catch (exception) { }


    isWorkspacePainting = false;

    return;

});




// SELECTED WORK QUEUE ID, CHANGE EVENT

function SelectedWorkQueueId_OnChange() {

    // LOCK DOWN PARTIAL VIEW FOR POST BACK

    $("#MyWorkQueuesContainer").attr("disabled", "disabled");

    var selectedWorkQueueId = $("#SelectedWorkQueueId").val();

    $.post("/Workspace/WorkQueueSelected_OnChanged", { "selectedWorkQueueId": selectedWorkQueueId },

        function (response) {

            // RE-ENABLE THE EXISTING INCASE OF FAILURE

            $("#MyWorkQueuesContainer").removeAttr("disabled");

            // REPLACE WITH NEW CONTENT AND FOCUS ON SELECTION

            $("#MyWorkQueuesContainer").replaceWith(response);

            $("#SelectedWorkQueueId").focus();

        }

    );

}

function GetWorkLink_OnClick() {

    // LOCK DOWN PARTIAL VIEW FOR POST BACK

    $("#MyWorkQueuesContainer").attr("disabled", "disabled");

    $.post($("#GetWorkLink").attr("href"), null,

        function (response) {

            // RE-ENABLE THE EXISTING INCASE OF FAILURE

            $("#MyWorkQueuesContainer").removeAttr("disabled");

            // REPLACE WITH NEW CONTENT AND FOCUS ON SELECTION

            $("#MyWorkQueuesContainer").replaceWith(response);

            $("#SelectedWorkQueueId").focus();

            $(window).resize();

        }

    );

    return false; // DO NOT CALL DEFAULT CLICK EVENT

}


// TAB STRIP ON CLICK EVENT

$("a.TabStripLink").live("click", function (e) {

    // DISABLE TAB STRIP FOR AJAX QUERIES

    $("#MyAssignedWorkTabStrip").attr("disabled", "disabled");


    // SET TO DEFAULT TAB STATUS (UNSELECTED) AND DISABLE ALL PAGES 

    $("#MyAssignedWork").removeClass("TabStripLinkSelected");

    $("#MyAssignedCases").removeClass("TabStripLinkSelected");

    $("#MyTeamCases").removeClass("TabStripLinkSelected");

    $("#CaseLoads").removeClass("TabStripLinkSelected");


    $("#MyAssignedWorkContainer_PageMyAssignedWork").hide();

    $("#MyAssignedWorkContainer_PageMyAssignedCases").hide();

    $("#MyAssignedWorkContainer_PageMyTeamCases").hide();

    $("#MyAssignedWorkContainer_PageCaseLoads").hide();


    // SET TAB AND ENABLE PAGE 

    $(this).addClass("TabStripLinkSelected");

    $("#MyAssignedWorkContainer_Page" + $(this).context.id).show();


    // RE-ENABLED TAB STRIP

    $("#MyAssignedWorkTabStrip").removeAttr("disabled");


    setTimeout(function () { $(window).resize(); }, 1); // CALL RESIZE ON DIFFERENT THREAD AFTER PAGE CHANGES

    e.returnValue = false;

    return false;

});


// GRID REFRESH BUTTON CLICK

$("#GridRefresh").live("click", function (e) {

    $("#MyAssignedWorkGrid").trigger("reloadGrid");

    $("#MyAssignedCasesGrid").trigger("reloadGrid");

    $("#MyTeamCasesGrid").trigger("reloadGrid");

    $("#CaseLoadsGrid").trigger("reloadGrid");

    return false;

});


function WorkQueueItemSuspendWindow_ButtonOk_OnClick() {

    // LOCK DOWN PARTIAL VIEW FOR POST BACK

    $("#WorkQueueItemSuspendWindow").attr("disabled", "disabled");

    $.ajax({

        type: "POST",

        url: "/Workspace/WorkQueueItemSuspend",

        data: $("#WorkQueueItemSuspendWindowForm").serialize(),

        success: function (response) {

            // RE-ENABLE THE EXISTING INCASE OF FAILURE

            $("#WorkQueueItemSuspendWindow").removeAttr("disabled");

            $("#WorkQueueItemSuspendWindow").dialog("close");

            if (response.Success) {

                $("#MyAssignedWorkGrid").trigger("reloadGrid");

            }

            else {

                alert(response.ExceptionMessage);

            }

            $(window).resize();

        }

    });

    return false; // DO NOT CALL DEFAULT CLICK EVENT

}

function WorkQueueItemCloseWindow_ButtonOk_OnClick() {

    // LOCK DOWN PARTIAL VIEW FOR POST BACK

    $("#WorkQueueItemCloseWindow").attr("disabled", "disabled");

    $.ajax({

        type: "POST",

        url: "/Workspace/WorkQueueItemClose",

        data: $("#WorkQueueItemCloseWindowForm").serialize(),

        success: function (response) {

            // RE-ENABLE THE EXISTING INCASE OF FAILURE

            $("#WorkQueueItemCloseWindow").removeAttr("disabled");

            $("#WorkQueueItemCloseWindow").dialog("close");

            if (response.Success) {

                $("#MyAssignedWorkGrid").trigger("reloadGrid");

            }

            else { 

                alert (response.ExceptionMessage);

            }

            $(window).resize();

        }

    });

    return false; // DO NOT CALL DEFAULT CLICK EVENT

}


// MY ASSIGNED WORK GRID INITIALIZATION

$("document").ready(function () {

    $("#MyAssignedWorkGrid").jqGrid({

        pager: "#MyAssignedWorkGridPager",

        url: "/Workspace/MyAssignedWorkGrid",

        datatype: "json",

        colModel: [
                { name: "id", label: "Id", hidden: true },

                { name: "Status", label: "Status", width: 60, fixed: true, align: "center", sortable: false, formatter: MyAssignedWork_FormatColumn_Status },

                { name: "WorkQueueName", index: "WorkQueue.WorkQueueName", label: "Queue", formatter: Grid_FormatColumn_WrapText },

                { name: "Name", index: "WorkQueueItemName", label: "Name", formatter: MyAssignedWork_FormatColumn_Name },

                { name: "WorkflowNextStep", index: "WorkflowNextStep", label: "Next Step" },

                { name: "AddedDate", index: "AddedDate", label: "Added", width: 70, fixed: true },

                { name: "ConstraintDate", index: "ConstraintDate", label: "Constraint", width: 70, fixed: true },

                { name: "LastWorkedDate", index: "LastWorkedDate", label: "Worked", width: 70, fixed: true },

                { name: "DueDate", index: "DueDate", label: "Due", width: 70, fixed: true },

                { name: "Priority", index: "Priority", label: "Priority", align: "center", width: 50, fixed: true },

                { name: "WorkflowName", index: "Workflow.WorkflowName", label: "Workflow", formatter: MyAssignedWork_FormatColumn_WorkflowName },

                { name: "Action", label: "Action", align: "center", sortable: false, formatter: MyAssignedWork_FormatColumn_Action },

                ],

        viewrecords: true,

        jsonReader: { repeatitems: false },


        subGrid: true,

        subGridOptions: {

            "plusicon": "ui-icon-triangle-1-e",

            "minusicon": "ui-icon-triangle-1-s",

	    	"openicon": "ui-icon-arrowreturn-1-e"

	    },

	    subGridRowExpanded: MyAssignedWorkGrid_OnExpandRow

    });

    setTimeout(function () { $(window).resize(); }, 10); // FORCE RESIZE ON DIFFERENT THREAD WHEN GRID IS READY

});

// MY ASSIGNED WORK GRID - SUBGRID EXPANDED (BEGIN)

function MyAssignedWorkGrid_OnExpandRow (subGridId, rowId) {

    var subGridTableId = subGridId + "_t"; // CREATE NEW ID REFERENCE

    $("#" + subGridId).html("<table id='" + subGridTableId + "' style='width: 100%;'></table>"); // INSERT NEW HTML TABLE CONTAINER

    $("#" + subGridTableId).jqGrid({

        height: '100%',

        url: "/Workspace/MyAssignedWorkGridDetail?workQueueItemKey=" + rowId,

        datatype: "json",

        colModel: [

            { name: "id", label: "id", hidden: true },

            { name: "WorkQueueItemSenderId", label: "Sender Id", width: 50, fixed: true, hidden: true },

            { name: "EventDescription", label: "Description", width: "auto", formatter: Grid_FormatColumn_WrapText },

            { name: "Priority", label: "Priority", align: "center", width: 50, fixed: true },

            { name: "CreateAccountInfoActionDate", label: "Create Date", width: 140, fixed: true }

            ],

        viewrecords: true,

        jsonReader: { repeatitems: false },

        loadComplete: function (data) {

            // ON COMPLETION OF CHILD GRID, REMOVE WIDTH SO THAT IT SIZES OUT TO THE PARENT

            var gridId = $(this).context.id;

            $("#gbox_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gbox_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gview_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId + " .ui-jqgrid-hdiv").removeAttr("style"); // REMOVE WIDTH

            $("#gview_" + gridId + " .ui-jqgrid-bdiv").removeAttr("style"); // REMOVE WIDTH

            $("#gview_" + gridId + " .ui-jqgrid-hbox").attr("style", "padding-right: 0px;"); // REMOVE RESERVED SPACER

            $("#gview_" + gridId + " table.ui-jqgrid-htable").attr("width", "100%"); // SET FIRST COLUMN TO AUTOFIT WIDTH

        }

    });

    return;

}

// MY ASSIGNED WORK GRID - SUBGRID EXPANDED ( END )


// MY ASSIGNED WORK GRID - COLUMN FORMATTERS (BEGIN)

function MyAssignedWork_FormatColumn_Status(cellValue, options, rowObject) {

    var formattedValue = "<img src='/images/common16/Status" + cellValue + ".png' alt='Item Status' />";

    formattedValue += "<div><a href='/Work/WorkQueueItemDetail?WorkQueueItemId=" + rowObject.WorkQueueItemId + "' title='Work Queue Item Detail' target='_blank'>(detail)</a></div>";

    return formattedValue;

}

function MyAssignedWork_FormatColumn_Name(cellValue, options, rowObject) {

    var formattedValue = CoreObjectHyperLink(rowObject.ItemObjectType, rowObject.ItemObjectId, rowObject.Name, rowObject.Name);

    return formattedValue;

}

function MyAssignedWork_FormatColumn_WorkflowName(cellValue, options, rowObject) {

    if (rowObject.WorkflowId == 0) { return ""; }

    var formattedValue = "<div><a href=\"/Workflow/Execute?"; 
    
    formattedValue += "WorkflowId=" + rowObject.WorkflowId;

    if (rowObject.WorkflowInstanceIdEmpty == "true") {

        formattedValue += "&WorkQueueItemId=" + rowObject.WorkQueueItemId;

        formattedValue += "&" + rowObject.ItemObjectType + "Id=" + rowObject.ItemObjectId;

    }

    else { 

        formattedValue += "&WorkflowInstanceId=" + rowObject.WorkflowInstanceId;

    }

    formattedValue += "\" title=\"" + rowObject.WorkflowName + "\" style='white-space: normal !important'>";
    
    formattedValue += rowObject.WorkflowName + ((rowObject.WorkflowInstanceIdEmpty == "true") ? " (start)" : " (resume)");

    formattedValue += "</a></div>";

    return formattedValue;

}

function MyAssignedWork_FormatColumn_Action(cellValue, options, rowObject) {

    var formattedValue = "";

    var parameters = rowObject.WorkQueueItemId + ", \"" + rowObject.WorkQueueName + "\", \"" + rowObject.Name + "\"";

    formattedValue += "<div><a href='javascript:WorkQueueItem_Suspend (" + parameters + ");' title='Release or Suspend the Item back to the Work Queue'>(suspend)</a></div>";

    formattedValue += "<div><a href='javascript:WorkQueueItem_Close (" + parameters + ");' title='Release or Suspend the Item back to the Work Queue'>(close)</a></div>";

    return formattedValue;

}

// MY ASSIGNED WORK GRID - COLUMN FORMATTERS ( END ) 


// MY ASSIGNED CASES GRID INITIALIZATION

$("document").ready(function () {

    $("#MyAssignedCasesGrid").jqGrid({

        pager: "#MyAssignedCasesGridPager",

        url: "/Workspace/MyAssignedCasesGrid",

        datatype: "json",

        colModel: [
                { name: "id", label: "Id", hidden: true },

                { name: "MemberCaseId", label: "Case Id", width: 110, fixed: true, align: "center", sortable: false, formatter: MyAssignedCasesGrid_FormatColumn_MemberCaseId },

                { name: "AssignedToWorkTeamName", label: "Assigned to Team", formatter: Grid_FormatColumn_WrapText },

                { name: "AssignedToUserDisplayName", label: "Assigned To", formatter: Grid_FormatColumn_WrapText },

                { name: "MemberName", label: "Member", formatter: MyAssignedCasesGrid_FormatColumn_MemberName },

                { name: "StatusDescription", label: "Status" }

                ],

        viewrecords: true,

        jsonReader: { repeatitems: false },

        subGrid: true,

        subGridOptions: {

            "plusicon": "ui-icon-triangle-1-e",

            "minusicon": "ui-icon-triangle-1-s",

            "openicon": "ui-icon-arrowreturn-1-e"

        },

        subGridRowExpanded: MyAssignedCasesGrid_OnExpandRow

    });

    setTimeout(function () { $(window).resize(); }, 10); // FORCE RESIZE ON DIFFERENT THREAD WHEN GRID IS READY

});

// MY ASSIGNED CASES GRID - SUBGRID EXPANDED (BEGIN)

function MyAssignedCasesGrid_OnExpandRow(subGridId, rowId) {

    var subGridTableId = subGridId + "_t"; // CREATE NEW ID REFERENCE

    $("#" + subGridId).html("<table id='" + subGridTableId + "' style='width: 100%;'></table>"); // INSERT NEW HTML TABLE CONTAINER

    $("#" + subGridTableId).jqGrid({

        height: '100%',

        url: "/Workspace/MyAssignedCasesGridDetail?memberCaseKey=" + rowId,

        datatype: "json",

        colModel: [

            { name: "id", label: "id", hidden: true },

            { name: "Classification", label: "Problem Statement Domain/Class", width: "auto", formatter: Grid_FormatColumn_WrapText },

            { name: "AssignedToUserDisplayName", label: "Assigned To", formatter: Grid_FormatColumn_WrapText },

            { name: "AssignedToProviderName", label: "Provider", width: "auto", formatter: Grid_FormatColumn_WrapText }

            ],

        viewrecords: true,

        jsonReader: { repeatitems: false },

        loadComplete: function (data) {

            // ON COMPLETION OF CHILD GRID, REMOVE WIDTH SO THAT IT SIZES OUT TO THE PARENT

            var gridId = $(this).context.id;

            $("#gbox_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gbox_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gview_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId + " .ui-jqgrid-hdiv").removeAttr("style"); // REMOVE WIDTH
            
            $("#gview_" + gridId + " .ui-jqgrid-bdiv").removeAttr("style"); // REMOVE WIDTH

            $("#gview_" + gridId + " .ui-jqgrid-hbox").attr("style", "padding-right: 0px;"); // REMOVE RESERVED SPACER

            $("#gview_" + gridId + " table.ui-jqgrid-htable").attr("width", "100%"); // SET FIRST COLUMN TO AUTOFIT WIDTH

        }

    });

    return;

}

function MyAssignedCasesGrid_FormatColumn_MemberCaseId(cellValue, options, rowObject) {

    return CoreObjectHyperLink("MemberCase", rowObject.MemberCaseId, rowObject.MemberName, rowObject.MemberCaseId);

}

function MyAssignedCasesGrid_FormatColumn_MemberName(cellValue, options, rowObject) {

    return CoreObjectHyperLink("Member", rowObject.MemberId, rowObject.MemberName, rowObject.MemberName);

}


// MY TEAM CASES GRID INITIALIZATION

$("document").ready(function () {

    $("#MyTeamCasesGrid").jqGrid({

        pager: "#MyTeamCasesGridPager",

        url: "/Workspace/MyTeamCasesGrid",

        datatype: "json",

        colModel: [
                { name: "id", label: "Id", hidden: true },

                { name: "MemberCaseId", label: "Case Id", width: 110, fixed: true, align: "center", sortable: false, formatter: MyTeamCasesGrid_FormatColumn_MemberCaseId },

                { name: "AssignedToWorkTeamName", label: "Assigned to Team", formatter: Grid_FormatColumn_WrapText },

                { name: "AssignedToUserDisplayName", label: "Assigned To", formatter: Grid_FormatColumn_WrapText },

                { name: "MemberName", label: "Member", formatter: MyTeamCasesGrid_FormatColumn_MemberName },

                { name: "StatusDescription", label: "Status" },

                { name: "LockedByUserDisplayName", label: "Locked By", formatter: Grid_FormatColumn_WrapText },

                ],

        viewrecords: true,

        jsonReader: { repeatitems: false }

    });

    setTimeout(function () { $(window).resize(); }, 10); // FORCE RESIZE ON DIFFERENT THREAD WHEN GRID IS READY

});

function MyTeamCasesGrid_FormatColumn_MemberCaseId(cellValue, options, rowObject) {

    return CoreObjectHyperLink("MemberCase", rowObject.MemberCaseId, rowObject.MemberName, rowObject.MemberCaseId);

}

function MyTeamCasesGrid_FormatColumn_MemberName(cellValue, options, rowObject) {

    return CoreObjectHyperLink("Member", rowObject.MemberId, rowObject.MemberName, rowObject.MemberName);

}

// CASE LOADS GRID INITIALIZATION

$("document").ready(function () {

    $("#CaseLoadsGrid").jqGrid({

        pager: "#CaseLoadsPager",

        url: "/Workspace/CaseLoadsGrid",

        datatype: "json",

        colModel: [
                { name: "id", label: "Id", hidden: true },

                { name: "AssignedToWorkTeamName", label: "Work Team", formatter: Grid_FormatColumn_WrapText },

                { name: "UnderDevelopmentCount", label: "Under Development" },

                { name: "ActiveCount", label: "Active" },

                { name: "TotalOpenCount", label: "Total Open" }

                ],

        viewrecords: true,

        jsonReader: { repeatitems: false },

        subGrid: true,

        subGridOptions: {

            "plusicon": "ui-icon-triangle-1-e",

            "minusicon": "ui-icon-triangle-1-s",

            "openicon": "ui-icon-arrowreturn-1-e"

        },

        subGridRowExpanded: CaseLoadsGrid_OnExpandRow

    });

    setTimeout(function () { $(window).resize(); }, 10); // FORCE RESIZE ON DIFFERENT THREAD WHEN GRID IS READY

});

// MY ASSIGNED CASES GRID - SUBGRID EXPANDED (BEGIN)

function CaseLoadsGrid_OnExpandRow(subGridId, rowId) {

    var subGridTableId = subGridId + "_t"; // CREATE NEW ID REFERENCE

    $("#" + subGridId).html("<table id='" + subGridTableId + "' style='width: 100%;'></table>"); // INSERT NEW HTML TABLE CONTAINER

    $("#" + subGridTableId).jqGrid({

        height: '100%',

        url: "/Workspace/CaseLoadsGridDetail?assignedToWorkTeamKey=" + rowId,

        datatype: "json",

        colModel: [

            { name: "id", label: "id", hidden: true },

            { name: "AssignedToUserDisplayName", label: "Assigned To", width: "auto", formatter: Grid_FormatColumn_WrapText },

            { name: "UnderDevelopmentCount", label: "Under Development" },

            { name: "ActiveCount", label: "Active" },

            { name: "TotalOpenCount", label: "Total Open" }

            ],

        viewrecords: true,

        jsonReader: { repeatitems: false },

        loadComplete: function (data) {

            // ON COMPLETION OF CHILD GRID, REMOVE WIDTH SO THAT IT SIZES OUT TO THE PARENT

            var gridId = $(this).context.id;

            $("#gbox_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gbox_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId).removeAttr("style"); // REMOVE WIDTH (IE, FF)

            $("#gview_" + gridId).width("100%"); // WIDTH (CHROME SPECIFIC SUPPORT)


            $("#gview_" + gridId + " .ui-jqgrid-hdiv").removeAttr("style"); // REMOVE WIDTH

            $("#gview_" + gridId + " .ui-jqgrid-bdiv").removeAttr("style"); // REMOVE WIDTH

            $("#gview_" + gridId + " .ui-jqgrid-hbox").attr("style", "padding-right: 0px;"); // REMOVE RESERVED SPACER

            $("#gview_" + gridId + " table.ui-jqgrid-htable").attr("width", "100%"); // SET FIRST COLUMN TO AUTOFIT WIDTH

        }

    });

    return;

}

// GENERIC GRID - COLUMN FORMATTERS (BEGIN)

function Grid_FormatColumn_WrapText(cellValue, options, rowObject) {

    var formattedValue = "<span style='white-space: normal !important'>" + cellValue + "</span>";

    return formattedValue;

}

// GENERIC GRID - COLUMN FORMATTERS ( END )


function CoreObjectHyperLink(objectType, objectId, linkTitle, linkContent) {

    var hyperlink = "";


    switch (objectType) {

        case "Member":

            hyperlink = "'/MemberProfile?MemberId=" + objectId + "'";

            hyperlink = "<a href=\"#\" onclick=\"javascript:window.open(" + hyperlink + ", 'MemberProfile_" + objectId + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Member Profile\" alt=\"Open Member Profile\">";

            hyperlink += "<span style='white-space: normal !important'>" + linkContent + "</span></a>";

            break;
            
        case "MemberCase":

            hyperlink = "'/MemberCase?MemberCaseId=" + objectId + "'";

            hyperlink = "<a href=\"#\" onclick=\"javascript:window.open(" + hyperlink + ", 'MemberCase_" + objectId + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Member Case\" alt=\"Open Member Case\">";

            hyperlink += "<span style='white-space: normal !important'>" + linkContent + "</span></a>";

            break;

        case "Provider":

            break;

        default:

            hyperlink = "<span title='" + linkTitle + "' style='white-space: normal !important'>" + linkContent + "</span>";

            break;

    }


    return hyperlink;

}


function WorkQueueItem_Suspend(workQueueItemId, workQueueName, workQueueItemName) {

    // SET VALUES

    $("#WorkQueueItemSuspendId").val(workQueueItemId.toString());

    $("#WorkQueueItemSuspendWorkQueueName").text(workQueueName);

    $("#WorkQueueItemSuspendName").text(workQueueItemName);


    // SHOW DIALOG

    $("#WorkQueueItemSuspendWindow").dialog({ width: 500, height: 365, closeOnEscape: true, modal: true, resizable: false });

    return;

}

function WorkQueueItem_Close(workQueueItemId, workQueueName, workQueueItemName) {

    // SET VALUES

    $("#WorkQueueItemCloseId").val(workQueueItemId.toString());

    $("#WorkQueueItemCloseWorkQueueName").text(workQueueName);

    $("#WorkQueueItemCloseName").text(workQueueItemName);

   
    // SHOW DIALOG

    $("#WorkQueueItemCloseWindow").dialog({ width: 500, height: 300, closeOnEscape: true, modal: true, resizable: false });

    return;

}


// PAGE INITIALIZATION (I.E. PAGE_LOAD)

$("document").ready(function () {

    // CALL RESIZE WHEN WINDOW IS READY (INITIAL SIZING)

    $(window).resize();

    $("#MyAssignedWork").click(); // SET DEFAULT SELECTED TAB (LAZY LOAD GRID)

    $("#WorkQueueItemSuspendWindow_ButtonOk").click(WorkQueueItemSuspendWindow_ButtonOk_OnClick);

    $("#WorkQueueItemCloseWindow_ButtonOk").click(WorkQueueItemCloseWindow_ButtonOk_OnClick);

    $("#SuspendDays").numeric({

        buttons: false,

        emptyValue: 0,

        minValue: 0,

        maxValue: 999,
        
        format: { format: '0' }

    });

    $(".ui-corner-all").removeClass("ui-corner-all");

    $(".ui-corner-bottom").removeClass("ui-corner-bottom");

});
