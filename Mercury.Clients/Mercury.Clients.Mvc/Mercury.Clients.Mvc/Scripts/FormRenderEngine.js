
var RenderEngineMode = { "Designer": 0, "Editor": 1, "Viewer": 2 };

var renderEngineMode = 0;

(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";
            var input = this.input = $("<input>")
					.insertAfter(select)
					.val(value)
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if ($(this).text().match(matcher)) {
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					                $(this).val("");
					                select.val("");
					                input.data("autocomplete").term = "";
					                return false;
					            }
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };

            this.button = $("<button type='button'>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.insertAfter(input)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // work around a bug (likely same cause as #5265)
					    $(this).blur();

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
        },

        destroy: function () {
            this.input.remove();
            this.button.remove();
            this.element.show();
            $.Widget.prototype.destroy.call(this);
        }
    });
})(jQuery);


function StyleAttribute_FontProperties(style) {

    if (!style) { return ""; }

    var value = "";


    if (style.FontFamily) { value += "font-family: " + style.FontFamily + ";"; }

    if (style.FontSize) {

        value += "font-size: " + style.FontSize;

        if (style.FontSizeUnit) { value += style.FontSizeUnit + ";"; }

        else { value += "px;"; }

    }

    if (style.FontWeight) { value += "font-weight: " + style.FontWeight + ";"; }

    if (style.FontStyle) { value += "font-style: " + style.FontStyle + ";"; }

    if (style.FontVariant) { value += "font-variant: " + style.FontVariant + ";"; }

    if (style.TextTransform) { value += "text-transform: " + style.TextTransform + ";"; }

    if (style.TextDecoration) { value += "text-decoration: " + style.TextDecoration + ";"; }

    if (style.Color) { value += "color: " + style.Color + ";"; }

    if (style.BackgroundColor) { value += "background-color: " + style.BackgroundColor + ";"; }


    if (!style) { alert("Style Font Property Error"); }

    return value;

}


function StyleAttribute_ParagraphProperties(style) {

    if (!style) { return ""; }

    var value = "";


    if (style.LineHeight) {

        value += "line-height: " + style.LineHeight;

        if (style.LineHeightUnit) { value += style.LineHeightUnit + ";"; }

        else { value += "%;"; }

    }

    if (style.VerticalAlign) { value += "vertical-align: " + style.VerticalAlign + ";"; }

    if (style.TextAlign) { value += "text-align: " + style.TextAlign + ";"; }

    if (style.TextIndent) { value += "text-indent: " + style.TextIndent + style.TextIndentUnit + ";"; }

    if (style.WhiteSpace) { value += "white-space: " + style.WhiteSpace + ";"; }

    if (style.WordSpacing) { value += "word-spacing: " + style.WordSpacing + style.WordSpacingUnit + ";"; }

    if (style.LetterSpacing) { value += "letter-spacing: " + style.LetterSpacing + style.LetterSpacingUnit + ";"; }
    

    if (!style) { alert("Style Paragraph Property Error"); }

    return value;

}


function StyleAttribute_BlockProperties(style) {

    if (!style) { return ""; }

    var value = "";


    if (style.Width) {

        value += "width: " + style.Width;

        if (style.WidthUnit) { value += style.WidthUnit + ";"; }

        else { value += "px;"; }

    }

    if (style.Height) {

        value += "height: " + style.Height;

        if (style.HeightUnit) { value += style.HeightUnit + ";"; }

        else { value += "px;"; }

    }


    // BORDER

    if (style.Border) { value += "border: " + style.Border + ";"; }

    else {

        if (style.BorderTop) { value += "border-top: " + style.BorderTop + ";"; }

        if (style.BorderLeft) { value += "border-left: " + style.BorderLeft + ";"; }

        if (style.BorderBottom) { value += "border-bottom: " + style.BorderBottom + ";"; }

        if (style.BorderRight) { value += "border-right: " + style.BorderRight + ";"; }

    }

    // MARGIN

    if (style.Margin) { value += "margin: " + style.Margin + ";"; }

    else if ((style.IsMarginSame === true) && (style.MarginTop)) { value += "margin: " + style.MarginTop + style.MarginTopUnit + ";"; }



    // PADDING

    if (style.Padding) { value += "padding: " + style.Padding + ";"; }

    else if ((style.IsPaddingSame === true) && (style.PaddingTop)) { value += "padding: " + style.PaddingTop + style.PaddingTopUnit + ";"; }



    if (style.Overflow) { value += "overflow: " + style.Overflow + ";"; }
    

    if (!style) { alert("Style Block Property Error"); }

    return value;

}


function StyleAttribute_TextOnly(style) {

    return StyleAttribute_FontProperties(style) + StyleAttribute_ParagraphProperties(style);

}

function StyleAttribute_WithoutText(style) {

    return StyleAttribute_BlockProperties(style);

}

function StyleAttribute_All(style) {

    return StyleAttribute_FontProperties(style) + StyleAttribute_ParagraphProperties(style) + StyleAttribute_BlockProperties(style);

}


function HtmlElementDiv(forId, forStyle) {

    if (!forId) { forId = ""; }

    if (!forStyle) { forStyle = ""; }


    var htmlElement = $(document.createElement("div"));
    

    if (forId.length > 0) { htmlElement.attr("id", forId); }

    if (forStyle.length > 0) { htmlElement.attr("style", forStyle); }


    return htmlElement;

}

function HtmlElementImage(forId, src, title, forStyle, onClickEvent) {

    if (!forId) { forId = ""; }

    if (!src) { src = ""; }

    if (!title) { title = ""; }

    if (!forStyle) { forStyle = ""; }

    if (!onClickEvent) { onClickEvent = ""; } 



    var htmlElement = new $(document.createElement("img"));


    if (forId.length > 0) { htmlElement.attr("id", forId); }

    if (src.length > 0) { htmlElement.attr("src", src); }

    if (title.length > 0) {

        htmlElement.attr("title", title);

        htmlElement.attr("alt", title);

    }

    if (forStyle.length > 0) { htmlElement.attr("style", forStyle); }

    if (onClickEvent.length > 0) { htmlElement.attr("onclick", onClickEvent); }


    return htmlElement;

}


function RenderControl_TitleBar(imageUrl, title) {

    var titleBarContainerDiv = HtmlElementDiv("", "clear: both; width: 100%;");

    titleBarContainerDiv.addClass("FormControl_TitleBar");

    // TODO: MOUSE OVER EVENTS



    return titleBarContainerDiv;

}


function RenderControl_DropZone(ownerControl, dropZonePosition) {

    var dropZoneDiv = HtmlElementDiv("ControlId_" + ownerControl.ControlId + "_" + dropZonePosition, "");

    dropZoneDiv.addClass("DropZone");

    dropZoneDiv.attr("dropZoneType", ownerControl.Type);


    // TOOD: MOUSE EVENTS

    dropZoneDiv.bind("mouseover", function () { $(this).addClass("DropZoneAllow"); });

    dropZoneDiv.bind("mouseout", function () { $(this).removeClass("DropZoneAllow"); });


    switch (ownerControl.Type) {

        case "Form": dropZoneDiv.html("[Section Drop Zone]"); break;

        case "Section": dropZoneDiv.html("&nbsp;"); break;

        default: dropZoneDiv.html("[Control Drop Zone]"); break;

    }

    return dropZoneDiv;

}


function RenderControl_Section(sectionControl) {

    var controlContainer = HtmlElementDiv("ControlId_" + sectionControl.ControlId, StyleAttribute_All(sectionControl.Style));

    controlContainer.addClass("FormControl");

    controlContainer.attr("formControlType", "Section");

    controlContainer.attr("formControlName", sectionControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(sectionControl.Visible === 0)) { controlContainer.css("display", "block"); }

    else { controlContainer.css("display", "none"); }


    var sectionLayoutTable = $(document.createElement("table"));

    controlContainer.append(sectionLayoutTable);

    sectionLayoutTable.attr("cellpadding", "0");

    sectionLayoutTable.attr("cellspacing", "0");

    sectionLayoutTable.css("style", "table-layout: fixed");


    var sectionLayoutTableRow = $(document.createElement("tr"));

    sectionLayoutTable.append(sectionLayoutTableRow); // APPEND ROW TO TABLE

    var sectionLayoutTableCell;


    // RENDER COLUMN DROP ZONE (LEFT SIDE)

    var dropZonePosition = 0;

    var dropZone;

    if (renderEngineMode == RenderEngineMode.Designer) {

        sectionLayoutTableCell = $(document.createElement("td"));

        sectionLayoutTableCell.addClass("ColumnDropZoneCell");

        dropZone = RenderControl_DropZone(sectionControl, dropZonePosition);

        sectionLayoutTableCell.append(dropZone);

        sectionLayoutTableRow.append(sectionLayoutTableCell);

    }


    var hasColumnSize = false;

    if (sectionControl.Children) {

        for (var currentColumnIndex = 0; currentColumnIndex < sectionControl.Children.length; currentColumnIndex++) {

            var currentColumnControl = sectionControl.Children[currentColumnIndex];

            sectionLayoutTableCell = $(document.createElement("td"));


            var tableCellWidth = (100 / sectionControl.Children.length) + "%";

            if ((currentColumnControl.Style.Width) && (sectionControl.Children.length > 1)) {

                tableCellWidth = currentColumnControl.Style.Width + currentColumnControl.Style.WidthUnit;

                hasColumnSize = true;

            }


            sectionLayoutTableRow.append(sectionLayoutTableCell);

            sectionLayoutTableCell.attr("valign", "top");

            sectionLayoutTableCell.attr("style", StyleAttribute_All(currentColumnControl.Style));

            sectionLayoutTableCell.css("width", tableCellWidth);


            RenderFormControl(sectionLayoutTableCell, currentColumnControl);


            if (renderEngineMode == RenderEngineMode.Designer) {

                dropZonePosition = dropZonePosition + 1;

                sectionLayoutTableCell = $(document.createElement("td"));

                sectionLayoutTableCell.addClass("ColumnDropZoneCell");

                dropZone = RenderControl_DropZone(sectionControl, dropZonePosition);

                sectionLayoutTableCell.append(dropZone);

                sectionLayoutTableRow.append(sectionLayoutTableCell);

            }

        }

    }

    if (!hasColumnSize) { sectionLayoutTable.css("width", "100%"); }


    return controlContainer;

}

function RenderControl_SectionColumn(sectionColumnControl) {

    var controlContainer = HtmlElementDiv("ControlId_" + sectionColumnControl.ControlId, "");

    controlContainer.addClass("FormControl");

    controlContainer.attr("formControlType", "SectionColumn");

    controlContainer.attr("formControlName", sectionColumnControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(sectionColumnControl.Visible === 0)) { controlContainer.css("display", "block"); }

    else { controlContainer.css("display", "none"); }


    var dropZonePosition = 0;

    var dropZone;

    if (renderEngineMode == RenderEngineMode.Designer) {

        dropZone = RenderControl_DropZone(sectionColumnControl, dropZonePosition);

        controlContainer.append(dropZone);

    }


    if (sectionColumnControl.Children) {

        for (var currentChildIndex = 0; currentChildIndex < sectionColumnControl.Children.length; currentChildIndex ++) {

            var currentChildControl = sectionColumnControl.Children[currentChildIndex];

            RenderFormControl(controlContainer, currentChildControl);

            if (renderEngineMode == RenderEngineMode.Designer) {

                dropZonePosition = dropZonePosition + 1;

                dropZone = RenderControl_DropZone(sectionColumnControl, dropZonePosition);

                controlContainer.append(dropZone);

            }

        }

    }


    return controlContainer;

}

function RenderControl_Label(labelControl, ownerControl) {

    var controlContainerDiv = HtmlElementDiv("", StyleAttribute_All(labelControl.Style));

    controlContainerDiv.append(labelControl.Text);

    return controlContainerDiv;

}

function RenderControl_LabelTable(labelControl, ownerControl, controlDiv) {

    var labelTable = $(document.createElement("table"));

    var labelTableRow;

    var labelCell;

    var controlCell;


    labelTable.css("width", "100%");

    labelTable.css("border", "0");

    labelTableRow = $(document.createElement("tr"));

    labelTable.append(labelTableRow);


    labelCell = $(document.createElement("td"));

    // STYLE IS APPLIED AT THE TABLE CELL AS THE CELL CONTENTS IS THE LABEL TEXT ONLY

    labelCell.attr("style", StyleAttribute_All(labelControl.Style));

    labelCell.append(labelControl.Text);


    controlCell = $(document.createElement("td"));

    // STYLE IS APPLIED AT THE CONTROL DIV LEVEL, NOT THE TABLE CELL

    controlCell.append(controlDiv);


    if (labelControl.Position == undefined) { labelControl.Position = 0; }

    switch (labelControl.Position) {

        case 0: // LEFT 

            labelTableRow.append(labelCell);

            labelTableRow.append(controlCell);

            break;

        case 1: // RIGHT

            labelTableRow.append(controlCell);

            labelTableRow.append(labelCell);

            break;

    }


    return labelTable;

}

function RenderControl_Text(textControl) {

    var controlContainer = HtmlElementDiv("ControlId_" + textControl.ControlId, StyleAttribute_All (textControl.Style));

    controlContainer.addClass("FormControl");

    controlContainer.attr("formControlType", "Text");

    controlContainer.attr("formControlName", textControl.Name);


    if (textControl.Text) { controlContainer.html(textControl.Text); }


    return controlContainer;

}

function RenderControl_Input(inputControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId, "");

    var contentDiv;


    if (!(inputControl.Label.Visible === -1)) {

        contentDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId + "_Content", StyleAttribute_All(inputControl.Style));

    }

    else {

        contentDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId + "_Content", StyleAttribute_WithoutText(inputControl.Style));

    }


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Input");

    controlContainerDiv.attr("formControlName", inputControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(inputControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }

    if ((inputControl.Style.TextAlign === "center") && (inputControl.Style.Width)) {

        controlContainerDiv.css("margin", "0 auto");

        controlContainerDiv.css("width", inputControl.Style.Width + inputControl.Style.WidthUnit);

    }

    contentControlDiv = HtmlElementDiv("", "");


    var inputElement = null;

    inputElement = $(document.createElement("input"));

    inputElement.attr("name", "ControlId_" + inputControl.ControlId);

    inputElement.attr("type", "text");

    inputElement.css("width", "100%");

    if (inputControl.TabIndex) { inputElement.attr("tabindex", inputControl.TabIndex); }

    switch (inputControl.InputType) {

        case 0: // TEXT

            if (!(inputControl.TextMode)) { inputControl.TextMode = 0; }

            switch (inputControl.TextMode) {

                case 1: // MULTI-LINE

                    inputElement = $(document.createElement("textarea"));

                    inputElement.attr("name", "ControlId_" + inputControl.ControlId);

                    if (inputControl.TabIndex) { inputElement.attr("tabindex", inputControl.TabIndex); }

                    inputElement.attr("rows", inputControl.Rows);

                    if (inputControl.Style.Width) { inputElement.css("width", inputControl.Style.Width + inputControl.Style.WidthUnit); }

                    else { inputElement.css("width", "100%"); }

                    if (inputControl.EmptyMessage) { if (inputControl.EmptyMessage.length > 0) { inputElement.attr("EmptyMessage", inputControl.EmptyMessage); } }

                    break;

                case 0: // SINGLE LINE TEXT

                case 2: // PASSWORD

                default:

                    inputElement = $(document.createElement("input"));

                    inputElement.attr("name", "ControlId_" + inputControl.ControlId);

                    if (inputControl.TabIndex) { inputElement.attr("tabindex", inputControl.TabIndex); }

                    if (inputControl.TextMode == 2) { inputElement.attr("type", "password"); }

                    else (inputElement.attr("type", "text"));


                    if (!(inputControl.MaxLength)) { inputControl.MaxLength = 8000; }

                    inputElement.attr("maxlength", inputControl.MaxLength);

                    if (inputControl.ReadOnly === -1) { inputElement.attr("readonly", "readonly"); }

                    inputElement.css("width", "100%");

                    if (inputControl.Text) { inputElement.attr("value", inputControl.Text); }

                    if (inputControl.Mask) { if (inputControl.Mask.length > 0) { inputElement.mask(inputControl.Mask.replace (/#/g, "9")); } }

                    if (inputControl.EmptyMessage) { if (inputControl.EmptyMessage.length > 0) { inputElement.attr("EmptyMessage", inputControl.EmptyMessage); } }

                    break;


            }

            break;

        case 1: // NUMERIC

            inputElement.numeric();

            break;

        case 2: // DATETIME

            var displayDateFormat = "mm/dd/yy";

            if (inputControl.DisplayDateFormat) { // CONVERT C# DISPLAY DATE FORMAT TO JQUERY DISPLAY DATE FORMAT

                displayDateFormat = displayDateFormat.toLowerCase();

                displayDateFormat = displayDateFormat.replace("/wyy", "y"); // DOUBLE Y TO SINGLE Y (YYYY -> YY AND YY -> Y)

                displayDateFormat = displayDateFormat.replace("/wyyyy", "y"); // DOUBLE Y TO SINGLE Y (YYYY -> YY AND YY -> Y)

            }

            var minDate = new Date("01/01/1900");

            var maxDate = new Date("12/31/2999");

            if (inputControl.MinDate) { minDate = new Date(inputControl.MinDate); }

            if (inputControl.MaxDate) { maxDate = new Date(inputControl.MaxDate); }

            // CHANGE TO JQUERY DATEPICKER 

            $(inputElement).datepicker({ minDate: minDate, maxDate: maxDate, dateFormat: displayDateFormat, showButtonPanel: true }); // CREATE PICKER BEFORE SETTING OPTIONS

            $("#ui-datepicker-div").css("font-size", ".8em");

            break;

    }


    if ((renderEngineMode == RenderEngineMode.Designer) || (renderEngineMode == RenderEngineMode.Editor)) { contentControlDiv.append(inputElement); }

    else { contentControlDiv.append(inputControl.Text); }


    if (inputControl.Label.Visible) {

        if (inputControl.Label.Position == undefined) { inputControl.Label.Position = 0; }

        switch (inputControl.Label.Position) {

            case 0: // LEFT

            case 1: // RIGHT

                contentDiv.append(RenderControl_LabelTable(inputControl.Label, inputControl, contentControlDiv));

                break;

            case 2: // TOP

                contentDiv.append(RenderControl_Label(inputControl.Label, inputControl));

                contentDiv.append(contentControlDiv);

                break;

            case 3: // BOTTOM

                contentDiv.append(contentControlDiv);

                contentDiv.append(RenderControl_Label(inputControl.Label, inputControl));

                break;

        }

    }

    else {

        contentDiv.append(contentControlDiv);

    }


    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}

function RenderControl_Selection(selectionControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + selectionControl.ControlId, "");

    var contentDiv;


    if (!(selectionControl.Label.Visible === -1)) {

        contentDiv = HtmlElementDiv("ControlId_" + selectionControl.ControlId + "_Content", StyleAttribute_All(selectionControl.Style));

    }

    else {

        contentDiv = HtmlElementDiv("ControlId_" + selectionControl.ControlId + "_Content", StyleAttribute_WithoutText(selectionControl.Style));

    }

    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Selection");

    controlContainerDiv.attr("formControlName", selectionControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(selectionControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }

    if ((selectionControl.Style.TextAlign === "center") && (selectionControl.Style.Width)) {

        controlContainerDiv.css("margin", "0 auto");

        controlContainerDiv.css("width", selectionControl.Style.Width + selectionControl.Style.WidthUnit);

    }

    contentControlDiv = HtmlElementDiv("", "");


    var selectionElement = null;

    selectionElement = $(document.createElement("div"));

    selectionElement.addClass("SelectionElement");


    switch (selectionControl.SelectionType) {

        case 0: // DROP DOWN LIST

            var selectionElement = $(document.createElement("select"));

            selectionElement.attr("name", "ControlId_" + selectionControl.ControlId);

            selectionElement.css("width", "100%");

            if (selectionControl.TabIndex) { selectionElement.attr("tabindex", selectionControl.TabIndex) };

            if (selectionControl.Enabled === false) { selectionElement.attr("disabled", "disabled"); }


            if (!(selectionControl.DataSource)) { selectionControl.DataSource = 0; }

            if ((renderEngineMode == RenderEngineMode.Designer) && (selectionControl.DataSource == 1)) { // REFERENCE LIST

                // TODO: REFERENCE LIST DESIGNER

            }

            else if ((renderEngineMode == RenderEngineMode.Editor) && (selectionControl.DataSource == 1)) { // REFERENCE LIST IN 

                // TODO: REFERENCE LIST EDITOR

            }

            else if (selectionControl.DataSource == 0) { // ITEM LIST

                for (var currentItemIndex = 0; currentItemIndex < selectionControl.Items.length; currentItemIndex++) {

                    var optionItem = $(document.createElement("option"));

                    if (selectionControl.Items[currentItemIndex].Enabled === false) { optionItem.attr("disabled", "disabled"); }

                    if (selectionControl.Items[currentItemIndex].Selected === true) { optionItem.attr("selected", "selected"); }

                    optionItem.attr("value", selectionControl.Items[currentItemIndex].Value);

                    optionItem.append(selectionControl.Items[currentItemIndex].Text);

                    selectionElement.append(optionItem);

                }

                // TODO: ALLOW CUSTOM TEXT, SET VALUE IF CUSTOM TEXT 

                if (!(selectionControl.AllowCustomText)) { selectionControl.AllowCustomText = false; }

                // TODO: WIRE EVENTS

            }

            break;

        case 1: // LIST BOX

            break;

        case 2: // CHECK BOX

        case 3: // RADIO BUTTON

            // ADD DEFAULT PROPERTIES

            if (!(selectionControl.Direction)) { selectionControl.Direction = 0; }



            var selectionTable = $(document.createElement("table"));

            selectionTable.attr("cellpadding", "0");

            selectionTable.attr("cellspacing", "0");

            selectionTable.attr("border", "0");

            selectionTable.css("width", "100%");

            var selectionTableRow;

            var selectionTableCell;



            var totalRows = 1;

            if (selectionControl.Columns != 0) {

                totalRows = Math.floor(selectionControl.Items.length / selectionControl.Columns);

                if ((selectionControl.Items.length % selectionControl.Columns) != 0) { totalRows = totalRows + 1; }

            }

            for (var currentRowIndex = 0; currentRowIndex < totalRows; currentRowIndex++) {

                selectionTableRow = $(document.createElement("tr"));

                selectionTable.append(selectionTableRow);

            }


            var currentRow = 0;

            var currentColumn = 0;

            for (var currentItemIndex = 0; currentItemIndex < selectionControl.Items.length; currentItemIndex++) {

                selectionTableRow = $("tr:eq(" + currentRow + ")", selectionTable);


                selectionTableCell = $(document.createElement("td"));

                selectionTableCellInput = $(document.createElement("input"));

                selectionTableCellInput.attr("id", "ControlId_" + selectionControl.ControlId + "_" + currentItemIndex);

                selectionTableCellInput.attr("name", "ControlId_" + selectionControl.ControlId);

                if (selectionControl.TabIndex) { selectionTableCellInput.attr("tabindex", selectionControl.TabIndex); }


                if (selectionControl.SelectionType == 2) { selectionTableCellInput.attr("type", "checkbox"); }

                else { selectionTableCellInput.attr("type", "radio"); }


                selectionTableCellLabel = $(document.createElement("label"));

                selectionTableCellLabel.append(selectionControl.Items[currentItemIndex].Text);

                selectionTableCellLabel.attr("for", "ControlId_" + selectionControl.ControlId + "_" + currentItemIndex);




                if (selectionControl.Columns == 1) {

                    // INSERT DIRECTLY INTO DIV AND NOT TABLE (ONLY ONE COLUMN)

                    selectionElement.append(selectionTableCellInput);

                    selectionElement.append(selectionTableCellLabel);

                }

                else {

                    // INSERT INTO TABLE

                    selectionTableCell.append(selectionTableCellInput);

                    selectionTableCell.append(selectionTableCellLabel);

                    selectionTableRow.append(selectionTableCell);


                    if (selectionControl.Direction == 0) { // HORIZONTAL 

                        currentColumn = currentColumn + 1;

                        if (currentColumn >= selectionControl.Columns) {

                            currentColumn = 0;

                            currentRow = currentRow + 1;

                        }

                    }

                    else { // VERTICAL 

                        currentRow = currentRow + 1;

                        if (currentRow >= totalRows) {

                            currentRow = 0;

                            currentColumn = currentColumn + 1;

                        }

                    }

                }

            }

            if (selectionControl.Columns > 1) { selectionElement = selectionTable; }

            break;

    }

    if ((renderEngineMode == RenderEngineMode.Designer) || (renderEngineMode == RenderEngineMode.Editor)) { contentControlDiv = selectionElement; }

    else { contentControlDiv.append(selectionControl.Text); }
    
    if (selectionControl.Label.Visible) {

        if (selectionControl.Label.Position == undefined) { selectionControl.Label.Position = 0; }

        switch (selectionControl.Label.Position) {

            case 0: // LEFT

            case 1: // RIGHT

                contentDiv.append(RenderControl_LabelTable(selectionControl.Label, selectionControl, contentControlDiv));

                break;

            case 2: // TOP

                contentDiv.append(RenderControl_Label(selectionControl.Label, selectionControl));

                contentDiv.append(contentControlDiv);

                break;

            case 3: // BOTTOM

                contentDiv.append(contentControlDiv);

                contentDiv.append(RenderControl_Label(selectionControl.Label, selectionControl));

                break;

        }

    }

    else {

        contentDiv.append(contentControlDiv);

    }


    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}

function RenderControl_Button(buttonControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + buttonControl.ControlId, "");

    var contentDiv = HtmlElementDiv("ControlId_" + buttonControl.ControlId + "_Content", StyleAttribute_All(buttonControl.Style));

    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Button");

    controlContainerDiv.attr("formControlName", buttonControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(buttonControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }

    if ((buttonControl.Style.TextAlign === "center") && (buttonControl.Style.Width)) {

        controlContainerDiv.css("margin", "0 auto");

        controlContainerDiv.css("width", buttonControl.Style.Width + buttonControl.Style.WidthUnit);

    }


    var buttonElement = null;

    buttonElement = $(document.createElement("button"));

    buttonElement.attr("name", "ControlId_" + buttonControl.ControlId);

    buttonElement.attr("type", "button");

    buttonElement.css("width", "100%");

    buttonElement.css("min-height", "24px");

    if (buttonControl.Enabled === false) { buttonElement.attr("disabled", "disabled"); }

    if (buttonControl.TabIndex) { buttonElement.attr("tabindex", buttonControl.TabIndex); }

    buttonElement.append(buttonControl.Text);

    contentDiv.append(buttonElement);


    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;


}

function RenderControl_Address(addressControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + addressControl.ControlId, "");

    var contentDiv = HtmlElementDiv("ControlId_" + addressControl.ControlId + "_Content", StyleAttribute_All(addressControl.Style));


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Address");

    controlContainerDiv.attr("formControlName", addressControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(buttonControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }


    // ADDRESS TABLE - DECLARATIONS AND STRUCTURE 

    var addressTable = $(document.createElement("table"));

    addressTable.attr("style", StyleAttribute_TextOnly(addressControl.Style));

    addressTable.attr("border", "0");

    addressTable.css("width", "100%");

    addressTable.css("padding", "4px");


    var addressLine1Row = $(document.createElement("tr"));

    addressTable.append (addressLine1Row);


    var addressLine1CellLabel = $(document.createElement("td"));

    addressLine1CellLabel.css("whitespace", "nowrap");

    addressLine1CellLabel.append("Line 1: ");

    addressLine1Row.append(addressLine1CellLabel);


    var addressLine1CellContent = $(document.createElement("td"));

    addressLine1CellContent.attr("colspan", "5");

    addressLine1Row.append(addressLine1CellContent);


    var addressLine2Row = $(document.createElement("tr"));

    addressTable.append(addressLine2Row);


    var addressLine2CellLabel = $(document.createElement("td"));

    addressLine2CellLabel.css("whitespace", "nowrap");

    addressLine2CellLabel.append("Line 2: ");

    addressLine2Row.append(addressLine2CellLabel);


    var addressLine2CellContent = $(document.createElement("td"));

    addressLine2CellContent.attr("colspan", "5");

    addressLine2Row.append(addressLine2CellContent);


    var addressCityStateZipRow = $(document.createElement("tr"));

    addressTable.append(addressCityStateZipRow);


    var addressZipCodeCellLabel = $(document.createElement("td"));

    addressZipCodeCellLabel.attr("style", "width: 10%; white-space: nowrap;");

    addressZipCodeCellLabel.append("Zip:");

    addressCityStateZipRow.append(addressZipCodeCellLabel);

    var addressZipCodeCellContent = $(document.createElement("td"));

    addressZipCodeCellContent.css("width", "10%");

    addressCityStateZipRow.append(addressZipCodeCellContent);


    var addressCityCellLabel = $(document.createElement("td"));

    addressCityCellLabel.attr("style", "width: 15%; white-space: nowrap;");

    addressCityCellLabel.attr("align", "right");

    addressCityCellLabel.append("City:");

    addressCityStateZipRow.append(addressCityCellLabel);

    var addressCityCellContent = $(document.createElement("td"));

    addressCityCellContent.attr ("style", "width: 40%; here; white-space: nowrap");

    addressCityStateZipRow.append(addressCityCellContent);


    var addressStateCellLabel = $(document.createElement("td"));

    addressStateCellLabel.attr("style", "width: 15%; whitespace: nowrap;");

    addressStateCellLabel.attr("align", "right");

    addressStateCellLabel.attr("valign", "middle");

    addressStateCellLabel.append("State:");

    addressCityStateZipRow.append(addressStateCellLabel);

    var addressStateCellContent = $(document.createElement("td"));

    addressStateCellContent.attr("style", "width: 10%; white-space: nowrap");

    addressCityStateZipRow.append(addressStateCellContent);


    if ((renderEngineMode == RenderEngineMode.Designer) || (renderEngineMode == RenderEngineMode.Editor)) {

        var addressLine1Input = $(document.createElement("input"));

        addressLine1Input.attr("id", "ControlId_" + addressControl.ControlId + "_Line1");

        addressLine1Input.attr("name", "ControlId_" + addressControl.ControlId + "_Line1");

        addressLine1Input.attr("type", "text");

        addressLine1Input.css("width", "99%");

        addressLine1Input.attr("maxlength", 60);

        if (addressControl.TabIndex) { addressLine1Input.attr("tabindex", addressControl.TabIndex); }

        // TODO: DISABLED / READONLY

        addressLine1Input.val(addressControl.Line1);

        addressLine1CellContent.append(addressLine1Input);


        var addressLine2Input = $(document.createElement("input"));

        addressLine2Input.attr("id", "ControlId_" + addressControl.ControlId + "_Line2");

        addressLine2Input.attr("name", "ControlId_" + addressControl.ControlId + "_Line2");

        addressLine2Input.attr("type", "text");

        addressLine2Input.css("width", "99%");

        addressLine2Input.attr("maxlength", 60);

        if (addressControl.TabIndex) { addressLine2Input.attr("tabindex", addressControl.TabIndex); }

        // TODO: DISABLED / READONLY

        addressLine2Input.val(addressControl.Line2);

        addressLine2CellContent.append(addressLine2Input);



        var addressZipCodeInput = $(document.createElement("input"));

        addressZipCodeInput.attr("id", "ControlId_" + addressControl.ControlId + "_ZipCode");

        addressZipCodeInput.attr("name", "ControlId_" + addressControl.ControlId + "_ZipCode");

        addressZipCodeInput.attr("type", "text");

        addressZipCodeInput.css("width", "100%");

        addressZipCodeInput.attr("maxlength", 9);

        if (addressControl.TabIndex) { addressZipCodeInput.attr("tabindex", addressControl.TabIndex); }

        // TODO: DISABLED / READONLY

        addressZipCodeInput.val(addressControl.ZipCode);

        addressZipCodeCellContent.append(addressZipCodeInput);

        addressZipCodeInput.mask("99999?-9999");


        var addressCitySelect = $(document.createElement("select"));

        addressCitySelect.attr("id", "ControlId_" + addressControl.ControlId + "_City");

        addressCitySelect.attr("name", "ControlId_" + addressControl.ControlId + "_City");

        addressCitySelect.attr("maxlength", 9);

        if (addressControl.TabIndex) { addressCitySelect.attr("tabindex", addressControl.TabIndex); }

        addressCitySelect.val(addressControl.City);

        addressCityCellContent.append (addressCitySelect);

        addressCitySelect.combobox();

        if (addressControl.TabIndex) {

            addressCitySelect.attr("tabindex", addressControl.TabIndex);

            $("+ input", addressCitySelect).attr("tabindex", addressControl.TabIndex);

            $("+ input + button", addressCitySelect).attr("tabindex", addressControl.TabIndex);

        }

        $("+ input", addressCitySelect).css("width", "90%");

        $("+ input + button > span.ui-button-text", addressCitySelect).css("padding", "0");

        $("+ input + button", addressCitySelect).css("width", "21");

        $("+ input + button", addressCitySelect).css("height", "21");

        $("+ input + button", addressCitySelect).css("top", "1px");

        $("+ input + button > span.ui-icon", addressCitySelect).css("left", "0");

        $("+ input + button > span.ui-icon", addressCitySelect).css("margin-left", "0");

        var addressStateSelect = $(document.createElement("select"));

        addressStateSelect.attr("id", "ControlId_" + addressControl.ControlId + "_State");

        addressStateSelect.attr("name", "ControlId_" + addressControl.ControlId + "_State");

        addressStateSelect.attr("maxlength", 2);

        addressStateSelect.val(addressControl.State);

        addressStateCellContent.append(addressStateSelect);

        addressStateSelect.combobox();

        if (addressControl.TabIndex) {

            addressStateSelect.attr("tabindex", addressControl.TabIndex);

            $("+ input", addressStateSelect).attr("tabindex", addressControl.TabIndex);

            $("+ input + button", addressStateSelect).attr("tabindex", addressControl.TabIndex); 
            
        }

        $("+ input", addressStateSelect).css("width", "60");

        $("+ input + button > span.ui-button-text", addressStateSelect).css("padding", "0");

        $("+ input + button", addressStateSelect).css("width", "21");

        $("+ input + button", addressStateSelect).css("height", "21");

        $("+ input + button", addressStateSelect).css("top", "1px");

        $("+ input + button > span.ui-icon", addressStateSelect).css("left", "0");

        $("+ input + button > span.ui-icon", addressStateSelect).css("margin-left", "0");


    }

    else if (renderEngineMode == RenderEngineMode.Viewer) {

        addressLine1CellContent.append(addressControl.Line1);

        addressLine2CellContent.append(addressControl.Line2);

        addressLineCityCellContent.append(addressControl.City);

        addressLineStateCellContent.append(addressControl.State);

        addressLineZipCodeCellContent.append(addressControl.ZipCode);

    }

    
    contentDiv.append(addressTable);

    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}


function RenderControl_Service(serviceControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + serviceControl.ControlId, "");

    var contentDiv = HtmlElementDiv("ControlId_" + serviceControl.ControlId + "_Content", StyleAttribute_All(serviceControl.Style));


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Service");

    controlContainerDiv.attr("formControlName", serviceControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(buttonControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }



    var htmlTable = $(document.createElement("table"));

    var htmlTableRow = $(document.createElement("tr"));

    var htmlTableCell;

    var cellWidthPercent = 100;


    htmlTable.attr("style", StyleAttribute_TextOnly(serviceControl.Style));

    htmlTable.css("width", "100%");

    htmlTable.css("padding", "4px");

    htmlTable.append(htmlTableRow);


    if (serviceControl.MostRecentMemberServiceDateVisible == undefined) { serviceControl.MostRecentMemberServiceDateVisible = true; }

    if (serviceControl.ServiceDateVisible == undefined) { serviceControl.ServiceDateVisible = true; }


    if (serviceControl.MostRecentMemberServiceDateVisible) { cellWidthPercent = cellWidthPercent - 30; }

    if (serviceControl.ServiceDateVisible) { cellWidthPercent = cellWidthPercent - 30; }


    // SERVICE CELL

    htmlTableCell = $(document.createElement("td"));

    htmlTableCell.css("width", cellWidthPercent + "%");

    htmlTableCell.css("text-align", "left");

    if (serviceControl.MedicalServiceName == undefined) { serviceControl.MedicalServiceName = ""; }

    if (serviceControl.MedicalServiceName.length > 0) { htmlTableCell.append(serviceControl.MedicalServiceName); }

    else { htmlTableCell.append("** No Service Available"); }

    htmlTableRow.append(htmlTableCell);


    // LAST SERVICE DATE 

    htmlTableCell = $(document.createElement("td"));

    htmlTableCell.css("width", "15%");

    if (!serviceControl.MostRecentMemberServiceDateVisible) { htmlTableCell.css("display", "none"); }

    htmlTableCell.append("Last Date:");

    htmlTableRow.append(htmlTableCell);

    htmlTableCell = $(document.createElement("td"));

    htmlTableCell.css("width", "15%");

    if (!serviceControl.MostRecentMemberServiceDateVisible) { htmlTableCell.css("display", "none"); }

    if (serviceControl.MostRecentMemberServiceDate) { htmlTableCell.append("TODO: DATE"); }


    contentDiv.append(htmlTable);

    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}

function RenderFormControl(parentControl, control) {

    var controlContent = null;

    
    // ASSIGN DEFAULT VALUES TO UNDEFINED PROPERTIES
    
    if (!control.Visible) { control.Visible = -1; } // IF NOT VISIBILTIY SET ON CONTROL, DEFAULT TO VISIBLE

    if (!(control.Label)) { control.Label = {};  control.Label.Visible = 0; } // IF NOT LABEL PROPERTY, ASSUME LABEL IS NOT VISIBLE

    else if (control.Label) { if (!control.Label.Visible) { control.Label.Visible = -1; } }  // IF LABEL BUT NOT VISIBLE PROPERTY, DEFAULT TO VISIBLE


    switch (control.Type) {

        case "Section": controlContent = RenderControl_Section(control); break;

        case "SectionColumn": controlContent = RenderControl_SectionColumn(control); break;

        case "Text": controlContent = RenderControl_Text(control); break;

        case "Input": controlContent = RenderControl_Input(control); break;

        case "Selection": controlContent = RenderControl_Selection(control); break;

        case "Button": controlContent = RenderControl_Button(control); break;

        case "Address": controlContent = RenderControl_Address(control); break;

        case "Service": controlContent = RenderControl_Service(control); break;

        default:

            //controlContent = $(document.createElement("div"));

            //controlContent.html(control.Name);

            break;

    }

    if (controlContent) { parentControl.append(controlContent); }

    return;

}


function RenderForm(form, renderMode, appendToElement) {

    renderEngineMode = renderMode;


    var formControlDiv = HtmlElementDiv("ControlId_" + form.ControlId, StyleAttribute_TextOnly(form.Style));

    formControlDiv.addClass("ui-helper-reset"); // CLEAR ALL INHERITED STYLES


    formControlDiv.attr("formControlType", "Form");

    formControlDiv.attr("formControlName", form.Name);


    $(appendToElement).empty();

    $(appendToElement).append(formControlDiv);


    var dropZonePosition = 0;

    var dropZone;


    if (renderEngineMode == RenderEngineMode.Designer) {

        dropZone = RenderControl_DropZone(form, dropZonePosition);

        formControlDiv.append(dropZone);

    }

    if (form.Children) {

        for (var currentChildIndex = 0; currentChildIndex < form.Children.length; currentChildIndex++) {

            var currentChildControl = form.Children[currentChildIndex];

            RenderFormControl(formControlDiv, currentChildControl);

            if (renderEngineMode == RenderEngineMode.Designer) {

                dropZonePosition = dropZonePosition + 1;

                dropZone = RenderControl_DropZone(form, dropZonePosition);

                formControlDiv.append(dropZone);

            }

        }

    }

    InitializeElementExtendedProperties();

    return;

}