/* TOOLBAR LIBRARY */

/*

COPYRIGHT (C) 2011 BY DENNIS QUEBE 

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.

*/

(function ($, undefined) { // JQUERY WRAPPER

    $.widget("ui.toolbar", {

        options: {

    },

    _create: function () {

        var toolbarControlId = this.element[0].id;

        var toolbar = $("#" + toolbarControlId);

        var toolbarBorder = $(document.createElement("div"));

        var toolbarItemsTable = $(document.createElement("table"));

        var toolbarItemsTableRow = $(document.createElement("tr"));


        // EXPAND TOOLBAR DOM STRUCTURE AND DECORATE WITH CSS

        toolbar.addClass("ui-widget-header");

        toolbar.addClass("ui-toolbar");

        toolbarBorder.addClass("ui-toolbar-border");

        toolbarItemsTable.attr("cellpadding", "0");

        toolbarItemsTable.attr("cellspacing", "0");

        toolbarItemsTable.addClass("ui-toolbar-items-list");


        toolbar.wrapInner(toolbarItemsTableRow);

        toolbar.wrapInner(toolbarItemsTable);

        toolbar.wrapInner(toolbarBorder);


        /* STYLE SEPARATORS */

        toolbarItemSeparators = ($("table.ui-toolbar-items-list>tbody>tr>div.ui-toolbar-item-separator", toolbar));

        toolbarItemSeparators.each(function () {

            var toolbarItem = $(document.createElement("td"));

            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-separator");

            $(this).html("&nbsp;");

            $(this).wrap(toolbarItem);

            $(this).hide();

        });

        /* STYLE BUTTONS */

        toolbarItemButtons = ($("table.ui-toolbar-items-list>tbody>tr>input[type=button], input[type=submit]", toolbar));

        toolbarItemButtons.each(function (self) {

            // DEFINE STRUCTURE 

            var toolbarItem = $(document.createElement("td"));

            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            toolbarItem.attr("title", $(this).attr("tooltip"));

            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-button");


            toolbarItem.prop("inputReference", $(this));

            toolbarItem.bind("mouseover", function () { $(this).addClass("ui-state-highlight"); });

            toolbarItem.bind("mouseout", function () { $(this).removeClass("ui-state-highlight"); });

            toolbarItem.bind("click", function () { $(this).prop("inputReference").click(); });


            var toolbarItemSpan = $(document.createElement("span"));

            toolbarItemSpan.addClass("ui-toolbar-item-title-span");


            // CONSTRUCT STRUCTURE 

            $(this).before(toolbarItem);

            toolbarItem.append(toolbarItemSpan);


            // DEFINE CONTENT 

            if ($(this).attr("icon")) {

                if ($(this).attr("icon").length > 0) {

                    var toolbarItemSpanImage = $(document.createElement("img"));

                    toolbarItemSpanImage.attr("src", $(this).attr("icon"));

                    toolbarItemSpanImage.addClass("ui-toolbar-item-icon");

                    if ($(this).attr("iconposition") == "AboveText") {

                        toolbarItemSpanImage.css("display", "block");

                        toolbarItemSpan.before(toolbarItemSpanImage);

                    }

                    else { toolbarItemSpan.append(toolbarItemSpanImage); }

                }

            }

            toolbarItemSpan.append($(this).attr("title"));


            // HIDE BASE ELEMENT

            $(this).css("display", "none");

        });


        /* STYLE CHECKBOXES */

        toolbarItemCheckboxes = ($("table.ui-toolbar-items-list>tbody>tr>input[type=checkbox]", toolbar));

        toolbarItemCheckboxes.each(function () {

            // DEFINE STRUCTURE 

            var toolbarItem = $(document.createElement("td"));

            toolbarItem.attr("valign", "middle");


            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            toolbarItem.attr("title", $(this).attr("tooltip"));


            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-checkradio");


            // SET DEFAULT STATE 

            if ($(this).prop("checked")) {

                toolbarItem.addClass("ui-state-active");

                toolbarItem.css("font-weight", "normal");

            }

            else { toolbarItem.removeClass("ui-state-active"); }


            // WIRE EVENTS

            toolbarItem.bind("mouseover", function () { $(this).addClass("ui-state-highlight"); });

            toolbarItem.bind("mouseout", function () { $(this).removeClass("ui-state-highlight"); });

            toolbarItem.prop("checkboxReference", $(this)); // CREATE REFERENCE TO BASE ELEMENT 

            toolbarItem.click(function () {

                var checkboxControl = $(this).prop("checkboxReference");

                var isChecked = !checkboxControl.prop("checked"); // INVERSE CURRENT STATE

                checkboxControl.prop("checked", isChecked);

                if (isChecked) {

                    $(this).addClass("ui-state-active");

                    $(this).css("font-weight", "normal");

                }

                else { $(this).removeClass("ui-state-active"); }

            });


            var toolbarItemSpan = $(document.createElement("span"));

            toolbarItemSpan.addClass("toolbar-item-title-span");


            // CONSTRUCT STRUCTURE

            $(this).before(toolbarItem);

            toolbarItem.append(toolbarItemSpan);


            // DEFINE CONTENT 

            if ($(this).attr("icon")) {

                if ($(this).attr("icon").length > 0) {

                    var toolbarItemSpanImage = $(document.createElement("img"));

                    toolbarItemSpanImage.attr("src", $(this).attr("icon"));

                    toolbarItemSpanImage.addClass("ui-toolbar-item-icon");

                    if ($(this).attr("iconposition") == "AboveText") {

                        toolbarItemSpanImage.css("display", "block");

                        toolbarItemSpan.before(toolbarItemSpanImage);

                    }

                    else { toolbarItemSpan.append(toolbarItemSpanImage); }

                }

            }

            toolbarItemSpan.append($(this).attr("title"));


            // HIDE BASE ELEMENT

            $(this).css("display", "none");

        });


        /* STYLE RADIO BUTTONS */

        toolbarItemRadioButtons = ($("table.ui-toolbar-items-list>tbody>tr>input[type=radio]", toolbar));

        toolbarItemRadioButtons.each(function () {

            // DEFINE STRUCTURE 

            var radioControlName = $(this).attr("name");


            var toolbarItem = $(document.createElement("td"));

            toolbarItem.attr("align", "center");

            toolbarItem.attr("valign", "middle");

            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            toolbarItem.attr("title", $(this).attr("tooltip"));


            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-checkradio");


            var toolbarItemSpan = $(document.createElement("span"));

            toolbarItemSpan.addClass("toolbar-item-title-span");


            // SET DEFAULT STATE 

            if ($(this).prop("checked")) {

                toolbarItem.addClass("ui-state-active");

                toolbarItem.css("font-weight", "normal");

            }

            else { toolbarItem.removeClass("ui-state-active"); }


            // WIRE EVENTS 

            toolbarItem.bind("mouseover", function () { $(this).addClass("ui-state-highlight"); });

            toolbarItem.bind("mouseout", function () { $(this).removeClass("ui-state-highlight"); });

            toolbarItem.prop("radiobuttonReference", $(this)); // CREATE REFERENCE TO BASE ELEMENT 

            toolbarItem.click(function () {

                var radiobuttonControl = $(this).prop("radiobuttonReference");

                var radioControlName = $(radiobuttonControl).attr("name");

                var isChecked = radiobuttonControl.prop("checked");

                var allowUncheck = radiobuttonControl.attr("allowuncheck");

                if ((!isChecked) || (allowUncheck)) { // ONLY CHANGE THE CHECK IF CURRENTLY NOT CHECKED, OR ALLOW UNCHECK

                    // REMOVE ALL ACTIVE STATES FROM OTHER RADIO BOXES OF SAME FORM NAME

                    $("input[name=" + radioControlName + "]").prev("td").removeClass("ui-state-active");

                    $(radiobuttonControl).prop("checked", !isChecked);

                    if (!isChecked) { // CHANGING FROM UNCHECKED TO CHECK

                        $(radiobuttonControl).prev("td").addClass("ui-state-active");

                        $(radiobuttonControl).prev("td").css("font-weight", "normal");

                    }

                    else { $(this).removeClass("ui-state-active"); }

                }

            });


            // CONSTRUCT STRUCTURE 

            $(this).before(toolbarItem);

            toolbarItem.append(toolbarItemSpan);


            // DEFINE CONTENT 

            if ($(this).attr("icon")) {

                if ($(this).attr("icon").length > 0) {

                    var toolbarItemSpanImage = $(document.createElement("img"));

                    toolbarItemSpanImage.attr("src", $(this).attr("icon"));

                    toolbarItemSpanImage.addClass("ui-toolbar-item-icon");

                    if ($(this).attr("iconposition") == "AboveText") { toolbarItemSpanImage.css("display", "block"); }

                    if ($(this).attr("iconposition") == "AboveText") {

                        toolbarItemSpanImage.css("display", "block");

                        toolbarItemSpan.before(toolbarItemSpanImage);

                    }

                    else { toolbarItemSpan.append(toolbarItemSpanImage); }

                }

            }

            toolbarItemSpan.append($(this).attr("title"));


            // HIDE BASE ELEMENT 

            $(this).css("display", "none");

        });

        /* STYLE SPLIT BUTTONS */

        toolbarItemSplitButtons = ($("table.ui-toolbar-items-list>tbody>tr>div.ui-toolbar-item-splitbutton", toolbar));

        toolbarItemSplitButtons.each(function () {

            var toolbarItem = $(document.createElement("td"));

            toolbarItem.attr("valign", "middle");


            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            toolbarItem.attr("title", $(this).attr("tooltip"));


            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-button");

            toolbarItem.addClass("ui-toolbar-item-splitbutton");


            toolbarItem.bind("mouseover", function () {

                $(this).addClass("ui-state-highlight");

                $(".ui-toolbar-item-splitbutton-separator", $(this)).addClass("ui-toolbar-item-splitbutton-separator-show");

            });

            toolbarItem.bind("mouseout", function () {

                $(this).removeClass("ui-state-highlight");

                $(".ui-toolbar-item-splitbutton-separator", $(this)).removeClass("ui-toolbar-item-splitbutton-separator-show");

            });


            var standardButton = $("input[type=button]:first", $(this));

            var toolbarItemAnchor = $(document.createElement("a"));

            toolbarItemAnchor.attr("id", "toolbar-item-button-" + $(standardButton).attr("id"));

            toolbarItemAnchor.addClass("ui-toolbar-item-anchor");

            toolbarItemAnchor.prop("inputReference", standardButton);

            toolbarItemAnchor.click(function () { $(this).prop("inputReference").click(); });


            var toolbarItemSpan = $(document.createElement("span"));

            toolbarItemSpan.addClass("toolbar-item-title-span");



            // CONSTRUCT STRUCTURE 

            $(this).before(toolbarItem);

            toolbarItem.append(toolbarItemAnchor);

            toolbarItemAnchor.append(toolbarItemSpan);



            // DEFINE CONTENT 

            if ($(standardButton).attr("icon")) {

                if ($(standardButton).attr("icon").length > 0) {

                    var toolbarItemSpanImage = $(document.createElement("img"));

                    toolbarItemSpanImage.attr("src", $(standardButton).attr("icon"));

                    toolbarItemSpanImage.addClass("ui-toolbar-item-icon");

                    if ($(standardButton).attr("iconposition") == "AboveText") {

                        toolbarItemSpanImage.css("display", "block");

                        toolbarItemSpan.before(toolbarItemSpanImage);

                    }

                    else { toolbarItemSpan.append(toolbarItemSpanImage); }

                }

            }

            toolbarItemSpan.append($(this).attr("title"));


            var toolbarItemSeparator = $(document.createElement("span"));

            toolbarItemSeparator.addClass("ui-toolbar-item-splitbutton-separator");

            toolbarItem.append(toolbarItemSeparator);


            var toolbarItemDropDown = $(document.createElement("a"));

            toolbarItemDropDown.addClass("ui-icon");

            toolbarItemDropDown.addClass("ui-icon-triangle-1-s");

            toolbarItemDropDown.addClass("ui-toolbar-item-icon");

            toolbarItemDropDown.prop("inputReference", standardButton.next());

            toolbarItemDropDown.click(function (e) {

                toolbarItemContent = $(this).next().next();

                toolbarItemDropDown.prop("inputReference").click();

                if (toolbarItemContent.length != 1) { return; }

                if ($(toolbarItemContent).is(":visible")) { toolbarItemContent.fadeOut("fast"); }

                else { toolbarItemContent.slideDown("fast"); }

                e.stopPropagation();

            });


            toolbarItem.append(toolbarItemDropDown);


            toolbarItem.append($(this));

            $(this).hide();


            // SET UP CONTENT DROP DOWN 

            toolbarItemContent = $("div:first", $(this));

            toolbarItemContent.addClass("ui-helper-reset");

            toolbarItemContent.addClass("ui-toolbar-item-dropdown-content");


            toolbarItemContent.appendTo(toolbarItem);

            toolbarItemContent.hide();


        });

        /* STYLE TEMPLATES */

        toolbarItemTemplates = ($("table.ui-toolbar-items-list>tbody>tr>div.ui-toolbar-item-template", toolbar));

        toolbarItemTemplates.each(function () {

            var toolbarItem = $(document.createElement("td"));

            toolbarItem.attr("valign", "middle");


            // PROMOTE (COPY) CLASSES AND STYLES 

            toolbarItem.attr("class", $(this).attr("class"));

            toolbarItem.attr("style", $(this).attr("style"));

            // ASSIGN STANDARD TOOLBAR CLASSES

            toolbarItem.addClass("ui-toolbar-item");

            toolbarItem.addClass("ui-toolbar-item-template");

            $(this).wrap(toolbarItem);

            $(this).removeClass("ui-toolbar-item-template");

        });




    } // CREATE

}); // WIDGET

})(jQuery);    