


var RenderEngineMode = { "Designer": 0, "Editor": 1, "Viewer": 2 };

var renderEngineMode = 0;



function StyleAttribute_FontProperties(style) {

    if (style == undefined) { return ""; }

    var styleAttribute = "";

    // FONT PROPERTIES

    if (style.FontFamily) { styleAttribute += "font-family: " + style.FontFamily + ";"; }

    if (style.FontSize) {

        styleAttribute += "font-size: " + style.FontSize;

        if (style.FontSizeUnit) { styleAttribute += style.FontSizeUnit + ";"; }

        else { styleAttribute += "px;"; }

    }

    if (style.FontWeight) { styleAttribute += "font-weight: " + style.FontWeight + ";"; }

    if (style.FontStyle) { styleAttribute += "font-style: " + style.FontStyle + ";"; }

    if (style.FontVariant) { styleAttribute += "font-variant: " + style.FontVariant + ";"; }

    if (style.TextTransform) { styleAttribute += "text-transform: " + style.TextTransform + ";"; }

    if (style.TextDecoration) { styleAttribute += "text-decoration: " + style.TextDecoration + ";"; }

    if (style.Color) { styleAttribute += "color: " + style.Color + ";"; }

    if (style.BackgroundColor) { styleAttribute += "background-color: " + style.BackgroundColor + ";"; }


    return styleAttribute;

}


function StyleAttribute_BlockProperties(style) {

    if (style == undefined) { return ""; }

    var styleAttribute = "";


    // BLOCK PROPERTIES

    if (style.LineHeight) {

        styleAttribute += "line-height: " + style.LineHeight;

        if (style.LineHeightUnit) { styleAttribute += style.LineHeightUnit + ";"; }

        else { styleAttribute += "%;"; }

    }

    if (style.VerticalAlign) { styleAttribute += "vertical-align: " + style.VerticalAlign + ";"; }

    if (style.TextAlign) { styleAttribute += "text-align: " + style.TextAlign + ";"; }

    if (style.TextIndent) { styleAttribute += "text-indent: " + style.TextIndent + style.TextIndentUnit + ";"; }

    if (style.WhiteSpace) { styleAttribute += "white-space: " + style.WhiteSpace + ";"; }

    if (style.WordSpacing) { styleAttribute += "word-spacing: " + style.WordSpacing + style.WordSpacingUnit + ";"; }

    if (style.LetterSpacing) { styleAttribute += "letter-spacing: " + style.LetterSpacing + style.LetterSpacingUnit + ";"; }


    return styleAttribute;

}

function StyleAttributeTextOnly(style) {

    if (style == undefined) { return ""; }

    var styleAttribute = "";


    styleAttribute = StyleAttribute_FontProperties(style);

    styleAttribute = styleAttribute + StyleAttribute_BlockProperties(style);


    return styleAttribute;

}

function StyleAttribute(style) {

    if (style == undefined) { return ""; }


    styleAttribute = StyleAttribute_FontProperties(style);

    styleAttribute = styleAttribute + StyleAttribute_BlockProperties(style);


    // BLOCK PROPERTIES 

    if (style.Width) {

        styleAttribute += "width: " + style.Width;

        if (style.WidthUnit) { styleAttribute += style.WidthUnit + ";"; }

        else { styleAttribute += "px;"; }

    }

    if (style.Height) {

        styleAttribute += "height: " + style.Height;

        if (style.HeightUnit) { styleAttribute += style.HeightUnit + ";"; }

        else { styleAttribute += "px;"; }

    }

    // BORDER

    if (style.IsBorderSame === true) {

        styleAttribute += "border: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";";

    }

    else {

        if (style.BorderTopStyle) { styleAttribute += "border-top: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";"; }

        if (style.BorderLeftStyle) { styleAttribute += "border-left: " + style.BorderLeftStyle + " " + style.BorderLeftWidth + style.BorderLeftWidthUnit + " " + style.BorderLeftColor + ";"; }

        if (style.BorderBottomStyle) { styleAttribute += "border-bottom: " + style.BorderBottomStyle + " " + style.BorderBottomWidth + style.BorderBottomWidthUnit + " " + style.BorderBottomColor + ";"; }

        if (style.BorderRightStyle) { styleAttribute += "border-right: " + style.BorderRightStyle + " " + style.BorderRightWidth + style.BorderRightWidthUnit + " " + style.BorderRightColor + ";"; }

    }

    // MARGIN

    if (style.Margin) { styleAttribute += "margin: " + style.Margin + ";"; }

    else if ((style.IsMarginSame === true) && (style.MarginTop)) { styleAttribute += "margin: " + style.MarginTop + style.MarginTopUnit + ";"; }



    // PADDING

    if (style.Padding) { styleAttribute += "margin: " + style.Padding + ";"; }

    else if ((style.IsPaddingSame === true) && (style.PaddingTop)) { styleAttribute += "margin: " + style.PaddingTop + style.PaddingTopUnit + ";"; }

        
    
    
    
    if (style.Overflow) { styleAttribute += "overflow: " + style.Overflow + ";"; }

    return styleAttribute;

}

function HtmlElementDiv (id, style) {

    htmlElement = $(document.createElement ("div"));


    if (id.length > 0) { htmlElement.attr("id", id); }

    if (style.length > 0) { htmlElement.attr("style", style); }


    return htmlElement;

}



function RenderControl_Section(sectionControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + sectionControl.ControlId, StyleAttribute (sectionControl.Style));

    var contentDiv = HtmlElementDiv("ControlId_" + sectionControl.ControlId + "_Content", "");


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Section");

    controlContainerDiv.attr("formControlName", sectionControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(sectionControl.Visible === 0)) { controlContainerDiv.css("display", "block"); }

    else { controlContainerDiv.css("display", "none"); }


    htmlTable = $(document.createElement("table"));

    htmlTable.attr("cellpadding", "0");

    htmlTable.attr("cellspacing", "0");

    htmlTable.css("style", "table-layout: fixed");


    htmlTableRow = $(document.createElement("tr"));

    htmlTable.append(htmlTableRow);


    // RENDER DROP ZONE ABOVE

    if (renderEngineMode == RenderEngineMode.Designer) {

        htmlTableCell = $(document.createElement("td"));

        htmlTableCell.addClass("ColumnDropZoneCell");

        htmlTableCell.attr("style", "min-width: 8px; min-height: 24px; display: none;");

        // RENDER DROP ZONE

    }



    var hasColumnSize = false;

    if (sectionControl.Children) {

        for (var currentChildIndex = 0; currentChildIndex < sectionControl.Children.length; currentChildIndex++) {

            currentColumn = sectionControl.Children[currentChildIndex];

            var tableCellWidth = (100 / sectionControl.Children.length) + "%";

            if ((currentColumn.Style.Width) && (sectionControl.Children.length > 1)) {

                tableCellWidth = currentColumn.Style.Width + currentColumn.Style.WidthUnit;

                hasColumnSize = true;

            }

            htmlTableCell = $(document.createElement("td"));

            if (currentColumn.Style.TextAlign) { htmlTableCell.attr("align", currentChildIndex.Style.TextAlign); }

            htmlTableCell.attr("valign", "top");

            htmlTableCell.attr("style", StyleAttribute (currentColumn.Style));

            htmlTableCell.css("width", tableCellWidth);

            htmlTableRow.append(htmlTableCell);

            RenderFormControl(htmlTableCell, currentColumn);

        }

    }

    if (!hasColumnSize) { htmlTable.css("width", "100%"); }


    contentDiv.append(htmlTable);

    controlContainerDiv.append(contentDiv);

    controlContainerDiv.css("border", "1px solid black");

    return controlContainerDiv;

}


function RenderControl_SectionColumn(sectionColumnControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + sectionColumnControl.ControlId, "");

    var contentDiv = HtmlElementDiv("ControlId_" + sectionColumnControl.ControlId + "_Content", "");
    

    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "SectionColumn");

    controlContainerDiv.attr("formControlName", sectionColumnControl.Name);


    if (sectionColumnControl.Children) {

        for (var currentChildIndex = 0; currentChildIndex < sectionColumnControl.Children.length; currentChildIndex++) {

            currentChildControl = sectionColumnControl.Children[currentChildIndex];

            RenderFormControl(contentDiv, currentChildControl);

            contentDiv.append(currentChildControl.Name + ": " + currentChildControl.Type);

        }

    }


    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}

function RenderControl_Label(labelControl, ownerControl) {

    var controlContainerDiv = HtmlElementDiv("", StyleAttribute(labelControl.Style));

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

    labelCell.attr("style", StyleAttribute(labelControl.Style));

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

    var controlContainerDiv = HtmlElementDiv("ControlId_" + textControl.ControlId, "");

    var contentDiv = HtmlElementDiv("ControlId_" + textControl.ControlId + "_Content", StyleAttribute(textControl.Style));


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Text");

    controlContainerDiv.attr("formControlName", textControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || (textControl.Visible === 1)) { controlContainerDiv.css("display", "block"); }

    else { controlContainerDiv.css("display", "none"); }


    if ((textControl.Style.TextAlign === "center") && (textControl.Style.Width)) {

        controlContainerDiv.css("margin", "0 auto;");

        controlContainerDiv.css("width", textControl.Style.Width + textControl.Style.WidthUnit);

    }


    contentDiv.html(textControl.Text);

    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}

function RenderControl_Input(inputControl) {

    var controlContainerDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId);

    var contentDiv;


    if (!(inputControl.Label.Visible === -1)) {

        contentDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId + "_Content", StyleAttribute(inputControl.Style));

    }

    else {

        contentDiv = HtmlElementDiv("ControlId_" + inputControl.ControlId + "_Content", StyleAttributeWithoutText(inputControl.Style));

    }


    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Text");

    controlContainerDiv.attr("formControlName", inputControl.Name);

    if ((renderEngineMode == RenderEngineMode.Designer) || !(textControl.Visible === -1)) { controlContainerDiv.css("display", "block"); } else { controlContainerDiv.css("display", "none"); }

    if ((inputControl.Style.TextAlign === "center") && (inputControl.Style.Width)) {

        controlContainerDiv.css ("margin", "0 auto");

        controlContainerDiv.css("width", inputControl.Style.Width + inputControl.Style.WidthUnit);

    }

    contentControlDiv = HtmlElementDiv("", "");


    var inputElement = null;

    inputElement = $(document.createElement("input"));

    inputElement.attr("type", "text");

    switch (inputControl.InputType) {

        case 0: // TEXT


            switch (inputControl.TextMode) {

                case 1: // MULTI-LINE

                    inputElement = $(document.createElement("textarea"));

                    inputElement.attr("cols", inputControl.Columns);

                    inputElement.attr("rows", inputControl.Rows);

                    if (inputControl.Style.Width) { inputElement.css("width", inputControl.Style.Width + inputControl.Style.WidthUnit); }

                    else { inputElement.css("width", "100%"); }

                    break;

                case 0: // SINGLE LINE TEXT

                case 2: // PASSWORD

                default:

                    inputElement = $(document.createElement("input"));

                    if (inputControl.TextMode == 2) { inputElement.attr("type", "password"); }

                    else (inputElement.attr("type", "text"));



                    inputElement.attr("maxlength", inputControl.MaxLength);

                    if (inputControl.ReadOnly === -1) { inputElement.attr("readonly", "readonly"); }

                    if (inputControl.Columns) { inputElement.attr("size", inputControl.Columns); }

                    else { inputElement.css("width", "100%"); }

                    if (inputControl.Text) { inputElement.attr("value", inputControl.Text); }

                    break;


            }

            break;

        case 1: // NUMERIC

            break;

        case 2: // DATETIME

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

    var controlContainerDiv;

    var contentDiv;

    var styleAttribute;


    styleAttribute = (selectionControl.Label.Visible === 1) ? StyleAttribute(selectionControl.Style) : StyleAttribute(selectionControl.Style);

    contentDiv = HtmlElementDiv("ControlId_" + selectionControl.ControlId + "_Content", styleAttribute);


    controlContainerDiv = HtmlElementDiv("ControlId_" + selectionControl.ControlId, "");

    controlContainerDiv.addClass("FormControl");

    controlContainerDiv.attr("formControlType", "Selection");

    controlContainerDiv.attr("formControlName", selectionControl.Name);

    if (!(selectionControl.Visible === 1)) { controlContainerDiv.css("display", "none"); }

    if ((selectionControl.Style.TextAlign === "center") && (selectionControl.Style.Width)) {

    }


    var contentControlDiv = HtmlElementDiv("", "");

    switch (selectionControl.SelectionType) {

        case 0: // DROP DOWN LIST

            break;


        case 1: // LIST BOX

            break;

        case 2: // CHECK BOX

            break;

        case 4: // RADIO BUTTON

            break;

    }


    contentDiv.append(contentControlDiv);


    controlContainerDiv.append(contentDiv);

    return controlContainerDiv;

}



function RenderFormControl(parentControl, control) {

    var controlContent = null;

    switch (control.Type) {

        case "Section":

            controlContent = RenderControl_Section(control);

            break;

        case "SectionColumn": controlContent = RenderControl_SectionColumn(control); break;

        /// case "Text": controlContent = RenderControl_Text(control); break;

//        case "Input": controlContent = RenderControl_Input (control); break;

//        case "Selection": controlContent = RenderControl_Selection (control); break;

        default:

            var styleAttribute = StyleAttribute(control.Style);

            controlContent = HtmlElementDiv(control.ControlId, styleAttribute);

            controlContent.append(control.Type + ": " + control.Name);

            break;

    }

    if (controlContent != null) { parentControl.append(controlContent); }

    return;

}


function RenderForm(form, renderMode, appendToElement) {

    renderEngineMode = renderMode;


    formControlDiv = HtmlElementDiv("ControlId_" + form.ControlId, StyleAttributeTextOnly (form.Style));

    formControlDiv.addClass("ui-helper-reset"); // CLEAR ALL INCOMING INHERITED STYLES


    formControlDiv.attr("formControlType", "Form");

    formControlDiv.attr("formControlName", form.Name);


    $(appendToElement).empty();

    $(appendToElement).append(formControlDiv); 


    if (form.Children) {

        for (var currentChildIndex = 0; currentChildIndex < form.Children.length; currentChildIndex++) {

            var currentChildControl = form.Children[currentChildIndex];

            var sectionControl = RenderControl_Section(currentChildControl);

            formControlDiv.append(sectionControl);


        }

    }

    return;

}