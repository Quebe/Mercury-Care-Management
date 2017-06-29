/* MENU LIBRARY */

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

    $.widget("ui.menu", {

        options: {

    },

    _create: function () {

        var menuControlId = this.element[0].id;

        var menu = $("#" + menuControlId);

        var menuItemsTable = $(document.createElement("table"));


        // EXPAND MENU DOM STRUCTURE AND DECORATE WITH CSS

        menu.addClass("ui-state-default");

        menu.addClass("ui-menu-control");

        menu.addClass("ui-corner-all");

        menuItemsTable.attr("cellpadding", "0");

        menuItemsTable.attr("cellspacing", "0");

        menuItemsTable.addClass("ui-menu-control-items-list");


        menu.wrapInner(menuItemsTable);

        $(window).bind("mouseup", function () {

            $(".ui-menu-control").each(function () {

                if ($(this).is(":visible")) { $(this).hide(); }

            });

        });


        /* STYLE SEPARATORS */

        menuItemSeparators = ($("table.ui-menu-control-items-list>div.ui-menu-control-item-separator", menu));

        menuItemSeparators.each(function () {

            var menuItem = $(document.createElement("tr"));

            var menuItemIconCell = $(document.createElement("td"));

            var menuItemTextCell = $(document.createElement("td"));


            // ASSIGN STANDARD TOOLBAR CLASSES

            menuItem.addClass("ui-menu-control-item");

            menuItem.addClass("ui-menu-control-item-separator");

            menuItemIconCell.addClass("ui-menu-control-item-icon");

            menuItemTextCell.addClass("ui-menu-control-item-text");

            menuItemTextCell.addClass("ui-widget-content");

            menuItemTextCell.append($("<hr />"));


            menuItem.append(menuItemIconCell);

            menuItem.append(menuItemTextCell);

            $(this).wrap(menuItem);

            $(this).hide();

        });


        /* STYLE BUTTONS */

        menuItemButtons = ($("table.ui-menu-control-items-list>input[type=button], input[type=submit]", menu));

        menuItemButtons.each(function () {

            // DEFINE STRUCTURE 

            var menuItem = $(document.createElement("tr"));

            var menuItemIconCell = $(document.createElement("td"));

            var menuItemTextCell = $(document.createElement("td"));

            var menuItemChildCell = $(document.createElement("td"));


            // [ICON] [TEXT (RS=2)]

            menuItem.addClass("ui-menu-control-item");

            menuItemIconCell.attr("valign", "middle");

            menuItemIconCell.addClass("ui-menu-control-item-icon");


            menuItemTextCell.addClass("ui-menu-control-item-text");

            menuItemTextCell.addClass("ui-widget-content");

            menuItemTextCell.append($(this).attr("title"));


            // ADD IMAGE 

            if ($(this).attr("icon")) {

                menuItemIcon = $(document.createElement("img"));

                menuItemIcon.attr("src", $(this).attr("icon"));

                menuItemIconCell.append(menuItemIcon);

            }


            // WIRE EVENTS 

            menuItem.prop("inputReference", $(this));

            menuItem.bind("mouseover", function () {

                $(this).addClass("ui-state-highlight");

                $("td.ui-menu-control-item-text", $(this)).removeClass("ui-widget-content");

            });

            menuItem.bind("mouseout", function () {

                $(this).removeClass("ui-state-highlight");

                $("td.ui-menu-control-item-text", $(this)).addClass("ui-widget-content");

            });

            menuItem.bind("click", function () { $(this).prop("inputReference").click(); });



            menuItem.append(menuItemIconCell);

            menuItem.append(menuItemTextCell);

            // $(this).parent().append(menuItem);

            $(this).wrap(menuItem);


            // HIDE BASE ELEMENT

            $(this).css("display", "none");

        });

    } // CREATE

}); // WIDGET

})(jQuery);    