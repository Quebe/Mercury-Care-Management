
function InitializeElementExtendedProperties() {

    // MAX LENGTH SUPPORT: INPUT[TEXT]/TEXTAREA

    $("input[type=text][maxlength]").keyup(function () {

        var maximumAllowed = parseInt($(this).attr("maxlength"));

        if ($(this).val().length > maximumAllowed) {

            $(this).val($(this).val().substr(0, maximumAllowed));

        }

    });
    
    $("textarea[maxlength]").keyup(function () {

        var maximumAllowed = parseInt($(this).attr("maxlength"));

        if ($(this).val().length > maximumAllowed) {

            $(this).val($(this).val().substr(0, maximumAllowed));

        }

    });

    // EMPTY MESSAGE SUPPORT: INPUT[TEXT]/TEXTAREA

    $("input[type=text][emptymessage]").each(function () {

        var defaultMessage = $(this).attr("emptymessage");

        if ($(this).val() == "") {

            $(this).filter(function () {

                var defaultMessage = $(this).attr("emptymessage");

                return ($(this).val() == "");

            }).addClass("Watermark").val($(this).attr("emptymessage"));

        }

    });

    $("input[type=text][emptymessage]").focus(function () {

        $(this).filter(function () {

            var defaultMessage = $(this).attr("emptymessage");

            return (($(this).val() == "") || ($(this).val() == defaultMessage));

        }).removeClass("Watermark").val("");

    });


    $("input[type=text][emptymessage]").blur(function () {

        $(this).filter(function () {

            var defaultMessage = $(this).attr("emptymessage");

            return ($(this).val() == "");

        }).addClass("Watermark").val($(this).attr("emptymessage"));

    });

    $("textarea[emptymessage]").each(function () {

        var defaultMessage = $(this).attr("emptymessage");

        if ($(this).val() == "") {

            $(this).filter(function () {

                var defaultMessage = $(this).attr("emptymessage");

                return ($(this).val() == "");

            }).addClass("Watermark").val($(this).attr("emptymessage"));

        }

    });

    $("textarea[emptymessage]").focus(function () {

        $(this).filter(function () {

            var defaultMessage = $(this).attr("emptymessage");

            return (($(this).val() == "") || ($(this).val() == defaultMessage));

        }).removeClass("Watermark").val("");

    });


    $("textarea[emptymessage]").blur(function () {

        $(this).filter(function () {

            var defaultMessage = $(this).attr("emptymessage");

            return ($(this).val() == "");

        }).addClass("Watermark").val($(this).attr("emptymessage"));

    });

}





$("document").ready(function () {

    InitializeElementExtendedProperties();

});