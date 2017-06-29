
var isWorkflowPainting = false;


// PAGE RESIZE (PAINT) EVENT

$(window).resize(function () {

    if (isWorkflowPainting) { return; }

    isWorkflowPainting = true;

    try {


        var adjustedHeight = 0; // AMOUNT TO RESIZE THE HEIGHT

        var marginHeight = 0; // THE SIZE OF THE STANDARD MARGIN BEING USED


        var section = $("#WorkflowContentSection");

        var container = $("#WorkflowContentContainer");

        var panel = $("#WorkflowContentPanel");


        if (panel.length == 0) { isWorkflowPainting = false; return; }


        marginHeight = panel.offset().top - container.offset().top;


        adjustedHeight = $(window).height(); // SET TO AVAILABLE WINDOW HEIGHT 

        adjustedHeight = adjustedHeight - section.offset().top - (marginHeight * 2); // SUBTRACT STARTING TOP POSITION

        adjustedHeight = adjustedHeight - (marginHeight * 4); // SUBTRACT MARGIN HEIGHT


        // RESIZE OUTER CONTAINER

        section.height(adjustedHeight);

        adjustedHeight = adjustedHeight - (marginHeight * 2);

        container.height(adjustedHeight);

        panel.height(adjustedHeight);

    }

    finally {

        isWorkflowPainting = false;

    }

    return;

});


function SetLastWorkflowMessage(message) {

    $("#LastWorkflowMessage").val(message);

    if (message.length > 0) {

        $("#WorkflowLastMessageContainer").show();

        $("LastWorkflowMessageLabel").html(message);

    }

    else {

        $("#WorkflowLastMessageContainer").hide();

    }

}


function SetExceptionMessage(message) {

    $("#ExceptionMessage").val(message);

    if (message.length > 0) {

        $("#WorkflowCancelContainer").show();

        $("#WorkflowExitContainer").show();

        $("#WorkflowExceptionMessageRow").show();

        $("ExceptionMessageLabel").html(message);

    }

    else {

        $("#WorkflowCancelContainer").hide();

        $("#WorkflowExitContainer").hide();

        $("#WorkflowExceptionMessageRow").hide();

    }

}

function SetInformationalMessage(message) {

    $("#InformationMessage").val(message);

    if (message.length > 0) {

        $("#WorkflowInformationalMessageRow").show();

        $("InformationalMessageLabel").html(message);

    }

    else {

        $("#WorkflowInformationalMessageRow").hide();

    }

}


function WorkflowHandleResponse(response) {

    $("#WorkflowContentContainer_LoadingOverlay").hide();

    $("#WorkflowContentContainer_LoadingMessage").hide();


    // LOAD NEW DOM CONTENT

    if ($("#WorkflowFormContent", response).length == 1) {

        $("#WorkflowForm").empty();

        $("#WorkflowForm").append($("#WorkflowFormContent", response));

    }

    else {

        // $("#WorkflowContentPanel").empty();

        // $("#WorkflowContentPanel").append($(response));

        $("#WorkflowContentPanel").replaceWith($(response));

    }

    $("#WorkflowContentSection").removeAttr("disabled");  // DISABLE CONTENT SECTION FOR POST BACK


    // RE-EVALUATE ALL SCRIPTS FROM NEW SOURCE

    // var scripts = $(response).filter(function () { return $(this).is('script') });

    var scripts = $("script", response);

    scripts.each(function () {

        eval(this.text);

        // eval(this.text || this.textContext || this.innerHTML || "");

    });


    $(window).resize();

    InitializeElementExtendedProperties();

    return;

}


function WorkflowStart_OnClick() {

    var formData = $("#WorkflowForm").serialize(); // CAPTURE DATA BEFORE DISABLE

    $("#WorkflowContentSection").attr("disabled", "disabled"); // DISABLE CONTENT SECTION FOR POST BACK

    $("#WorkflowContentContainer_LoadingOverlay").show();

    $("#WorkflowContentContainer_LoadingMessage").show();

    $.ajax({

        type: "POST",

        url: "/Workflow/Start",

        data: formData,

        success: WorkflowHandleResponse

    });

}


function WorkflowContinue_OnClick() {

    var formData = $("#WorkflowForm").serialize(); // CAPTURE DATA BEFORE DISABLE

    $("#WorkflowContentSection").attr("disabled", "disabled"); // DISABLE CONTENT SECTION FOR POST BACK

    $("#WorkflowContentContainer_LoadingOverlay").show();

    $("#WorkflowContentContainer_LoadingMessage").show();

    $.ajax({

        type: "POST",

        url: "/Workflow/Continue",

        data: formData,

        success: WorkflowHandleResponse

    });

}

function WorkQueueItemInformation_Clear() {

    $("#WorkQueueItemInformation").empty();

}

function WorkQueueItemInformation_SetMember(memberId) {

    var requiresLoad = (($("#WorkQueueItemInformation").attr("EntityType") != "Member") || ($("#WorkQueueItemInformation").attr("EntityObjectId") != memberId));

    if (requiresLoad) {

        $("#WorkQueueItemInformation").attr("EntityType", "Member");

        $("#WorkQueueItemInformation").attr("EntityObjectId", memberId);


        WorkQueueItemInformation_Clear();

        var requestUrl = "/Application/MemberInformationSummary?memberId=" + memberId;

        $("#WorkQueueItemInformation").load(requestUrl);


        $(window).resize();

    }

}


function MemberInformationCoverage_Toggle() {

    var coverageDiv = document.getElementById("WorkQueueItemInformationMemberCoverage");

    var coverageAnchor = document.getElementById("MemberInformationCoverageToggle");

    if (coverageDiv != null) {

        if (coverageDiv.style.display == "none") {

            coverageDiv.style.display = "block";

            coverageAnchor.innerText = "(less)";

        }

        else {

            coverageDiv.style.display = "none";

            coverageAnchor.innerText = "(more)";

        }

    }

    $(window).resize();

    return;

}

// PAGE INITIALIZATION (I.E. PAGE_LOAD)

$("document").ready(function () {

    // CALL RESIZE WHEN WINDOW IS READY (INITIAL SIZING)

    $(window).resize();

    InitializeElementExtendedProperties();

});
