using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Forms {

    public class FormRenderEngine {

        #region Private Properties

        private RenderEngineMode renderMode = RenderEngineMode.Designer;


        private FormDesigner.FormDesigner designerPage = null;

        private FormEditor.FormEditor editorPage = null;

        private FormViewer.FormViewer viewerPage = null;


        private Client.Application application = null;

        private System.Web.SessionState.HttpSessionState session = null;

        private String sessionCachePrefix = String.Empty;

        private Boolean isNonstandardCss = false;

        private Boolean isFormRendered = false;

        #endregion


        #region Private Session Cache

         private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                return application;

            }

        }

        #endregion


        #region Public Properties

        public System.Web.SessionState.HttpSessionState Session { get { return session; } set { session = value; } }

        public Boolean IsFormRendered { get { return isFormRendered; } set { isFormRendered = value; } }

        #endregion


        #region Constructors

        public FormRenderEngine (FormDesigner.FormDesigner formDesignerPage, Client.Application applicationReference, System.Web.SessionState.HttpSessionState sessionReference, String cachePrefix) {

            renderMode = RenderEngineMode.Designer;

            designerPage = formDesignerPage;

            application = applicationReference;

            session = sessionReference;

            sessionCachePrefix = cachePrefix;

            isNonstandardCss = (designerPage.Request.Browser.Browser == "IE");

            return;

        }

        public FormRenderEngine (FormEditor.FormEditor formEditorPage, Client.Application applicationReference, System.Web.SessionState.HttpSessionState sessionReference, String cachePrefix) { 

            renderMode = RenderEngineMode.Editor;

            editorPage = formEditorPage;

            application = applicationReference;

            session = sessionReference;

            sessionCachePrefix = cachePrefix;

            isNonstandardCss = (formEditorPage.Request.Browser.Browser == "IE");

            return;

        }

        public FormRenderEngine (FormViewer.FormViewer formViewerPage, Client.Application applicationReference, System.Web.SessionState.HttpSessionState sessionReference, String cachePrefix) { 

            renderMode = RenderEngineMode.Viewer;

            viewerPage = formViewerPage;

            application = applicationReference;

            session = sessionReference;

            sessionCachePrefix = cachePrefix;

            isNonstandardCss = (formViewerPage.Request.Browser.Browser == "IE");

            return;

        }

        #endregion


        #region Support Methods

        private UnitType ConvertUnit (String originalUnit) {

            switch (originalUnit) {

                case "px": return UnitType.Pixel;

                case "pt": return UnitType.Point;

                case "in": return UnitType.Inch;

                case "cm": return UnitType.Cm;

                case "mm": return UnitType.Mm;

                case "pc": return UnitType.Pica;

                case "em": return UnitType.Em;

                case "ex": return UnitType.Ex;

                case "%": return UnitType.Percentage;

                default: return UnitType.Pixel;

            }

        }

        #endregion


        #region Render - Basic HTML Elements 

        public HtmlGenericControl HtmlElementDiv (String id, String style) {

            HtmlGenericControl htmlElement = new HtmlGenericControl ("div");

            if (!String.IsNullOrEmpty (id)) { htmlElement.ID = id; }

            if (!String.IsNullOrEmpty (style)) { htmlElement.Attributes.Add ("style", style); }

            return htmlElement;

        }

        public HtmlGenericControl HtmlElementImage (String id, String imageUrl, String title, String style, String onClickEvent) {

            HtmlGenericControl htmlElement = new HtmlGenericControl ("img");

            if (!String.IsNullOrEmpty (id)) { htmlElement.ID = id; }

            htmlElement.Attributes.Add ("src", imageUrl);

            htmlElement.Attributes.Add ("title", title);

            htmlElement.Attributes.Add ("alt", title);

            if (!String.IsNullOrEmpty (style)) { htmlElement.Attributes.Add ("style", style); }

            if (!String.IsNullOrEmpty (onClickEvent)) { htmlElement.Attributes.Add ("onclick", onClickEvent); }

            return htmlElement;

        }

        #endregion


        #region Render - Form Controls

        public HtmlGenericControl RenderControl_TitleBar (String imageUrl, String title) {


            HtmlGenericControl titleBarContainerDiv = HtmlElementDiv (String.Empty, "clear: both; width: 100%;");

            titleBarContainerDiv.Attributes.Add ("class", "FormControl_TitleBar");

            titleBarContainerDiv.Attributes.Add ("onmouseover", "TitleBar_OnMouseOver (event);");

            titleBarContainerDiv.Attributes.Add ("onmouseout", "TitleBar_OnMouseOut (event);");


            #region Title Bar

            HtmlGenericControl titleBar;

            HtmlContainerControl titleBarElement;

            HtmlGenericControl imageElement;


            titleBar = HtmlElementDiv (String.Empty, "height: 20px; padding: 4px; border: solid 1px #4f81bd; display: none; cursor: move");

            titleBar.Attributes.Add ("onmousedown", "FormControl_OnDragBegin (event);");


            // HORIZONTAL GRAB BAR

            titleBarElement = HtmlElementDiv (String.Empty, "float: left; min-width: 5px; cursor: move");

            imageElement = HtmlElementImage (String.Empty, "/Images/Common16/GrabBarBlueHorizontal.png", String.Empty, String.Empty, String.Empty);

            titleBarElement.Controls.Add (imageElement);

            titleBar.Controls.Add (titleBarElement);


            // CONTROL ICON

            titleBarElement = HtmlElementDiv (String.Empty, "float: left; min-width: 24px; cursor: move");

            imageElement = HtmlElementImage (String.Empty, imageUrl, title, String.Empty, String.Empty);

            titleBarElement.Controls.Add (imageElement);

            titleBar.Controls.Add (titleBarElement);


            // CONTROL TITLE

            titleBarElement = HtmlElementDiv (String.Empty, "float: left; cursor: move; display: block; overflow: hidden");

            HtmlContainerControl titleText = HtmlElementDiv (String.Empty, "display: block; white-space: nowrap;");

            titleText.Attributes.Add ("class", "FormControl_TitleBarText");

            titleText.InnerText = title;

            titleBarElement.Controls.Add (titleText);

            titleBar.Controls.Add (titleBarElement);


            // DELETE ICON

            titleBarElement = HtmlElementDiv (String.Empty, "float: left; min-width: 24px; cursor: pointer");

            titleBarElement.Attributes.Add ("onclick", "DeleteControl_OnClick (event);");

            imageElement = HtmlElementImage (String.Empty, "/Images/Common16/Delete.png", "Delete", String.Empty, "DeleteControl_OnClick (event);");

            titleBarElement.Controls.Add (imageElement);

            titleBar.Controls.Add (titleBarElement);


            // PROPERTIES ICON

            titleBarElement = HtmlElementDiv (String.Empty, "float: left; min-width: 24px; cursor: pointer");

            imageElement = HtmlElementImage (String.Empty, "/Images/Common16/Properties.png", "Properties", String.Empty, "ControlProperties_OnClick (event);");

            titleBarElement.Controls.Add (imageElement);

            titleBar.Controls.Add (titleBarElement);

            #endregion


            #region Title Bar Expander

            HtmlGenericControl titleBarExpander = HtmlElementDiv (String.Empty, String.Empty);

            titleBarExpander.Attributes.Add ("class", "FormControl_TitleBarExpander");

            imageElement = new HtmlGenericControl ("img");

            imageElement.Attributes.Add ("src", "/Images/Common32/PointerDown.png");

            titleBarExpander.Controls.Add (imageElement);

            #endregion


            titleBarContainerDiv.Controls.Add (titleBar);

            titleBarContainerDiv.Controls.Add (titleBarExpander);

            return titleBarContainerDiv;

        }

        public HtmlGenericControl RenderControl_DropZone (Mercury.Client.Core.Forms.Control ownerControl, Int32 dropZonePosition) {

            HtmlGenericControl dropZoneDiv;

            dropZoneDiv = HtmlElementDiv ("ControlId_" + ownerControl.ControlId.ToString () + "_" + dropZonePosition.ToString (), String.Empty);

            dropZoneDiv.Attributes.Add ("class", "DropZone");

            dropZoneDiv.Attributes.Add ("dropZoneType", ownerControl.ControlType.ToString ());

            dropZoneDiv.Attributes.Add ("onmouseover", "DropZone_OnMouseOver (event);");

            dropZoneDiv.Attributes.Add ("onmouseout", "DropZone_OnMouseOut (event);");

            dropZoneDiv.Attributes.Add ("onmouseup", "DropZone_OnMouseUp (event);");

            switch (ownerControl.ControlType) {

                case Mercury.Server.Application.FormControlType.Form:

                    dropZoneDiv.InnerText = "[Section Drop Zone]";

                    break;

                case Mercury.Server.Application.FormControlType.Section:

                    dropZoneDiv.InnerHtml = "&nbsp";

                    break;

                default:

                    dropZoneDiv.InnerText = "[Control Drop Zone]";

                    break;

            }

            return dropZoneDiv;

        }

        public HtmlGenericControl RenderControl_Section (Mercury.Client.Core.Forms.Controls.Section sectionControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv;


            contentDiv = HtmlElementDiv ("ControlId_" + sectionControl.ControlId.ToString () + "_Content", sectionControl.StyleAttribute);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + sectionControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Section");

            controlContainerDiv.Attributes.Add ("formControlName", sectionControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (sectionControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/Section.png", sectionControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

                controlContainerDiv.Attributes.Add ("onmouseover", "FormControl_OnMouseOver (event);");

                controlContainerDiv.Attributes.Add ("onmouseout", "FormControl_OnMouseOut (event);");

            }


            Int32 dropZonePosition = 0;

            HtmlGenericControl dropZone;


            HtmlGenericControl htmlTable;

            HtmlGenericControl htmlTableRow;

            HtmlGenericControl htmlTableCell;

            Boolean hasColumnSize = false;


            htmlTable = new HtmlGenericControl ("table");

            htmlTable.Attributes.Add ("cellpadding", "0");

            htmlTable.Attributes.Add ("cellspacing", "0");

            htmlTable.Attributes.Add ("style", "table-layout: fixed");

            // htmlTable.Attributes.Add ("style", "table-layout: fixed; " + ((isNonstandardCss) ? "width: 98%;" : "width: 100%;"));

            // htmlTable.Attributes.Add ("style", "width: 100%");

            

            htmlTableRow = new HtmlGenericControl ("tr");

            htmlTable.Controls.Add (htmlTableRow);


            if (renderMode == RenderEngineMode.Designer) {

                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("class", "ColumnDropZoneCell");

                htmlTableCell.Attributes.Add ("style", "min-width: 8px; min-height: 24px; display: none");

                dropZone = RenderControl_DropZone (sectionControl, dropZonePosition);

                htmlTableCell.Controls.Add (dropZone);

                htmlTableRow.Controls.Add (htmlTableCell);

            }

            
            foreach (Mercury.Client.Core.Forms.Controls.SectionColumn currentColumn in sectionControl.Controls) {

                String tableCellWidth = (100 / sectionControl.Controls.Count).ToString () + "%";

                if ((!String.IsNullOrEmpty (currentColumn.Style.Width)) && (sectionControl.Controls.Count > 1)) { 
                    
                    tableCellWidth = currentColumn.Style.Width + currentColumn.Style.WidthUnit;

                    hasColumnSize = true;

                }

                htmlTableCell = new HtmlGenericControl ("td");

                if (!String.IsNullOrEmpty (currentColumn.Style.TextAlign)) { htmlTableCell.Attributes.Add ("align", currentColumn.Style.TextAlign); }

                htmlTableCell.Attributes.Add ("valign", "top");

                htmlTableCell.Attributes.Add ("width", tableCellWidth);

                htmlTableCell.Attributes.Add ("style", currentColumn.StyleAttributeWithoutSizeWidth);

                HtmlGenericControl sectionControlDiv = RenderControl_SectionColumn (currentColumn);

                htmlTableCell.Controls.Add (sectionControlDiv);

                htmlTableRow.Controls.Add (htmlTableCell);


                if (renderMode == RenderEngineMode.Designer) {

                    dropZonePosition = dropZonePosition + 1;

                    htmlTableCell = new HtmlGenericControl ("td");

                    htmlTableCell.Attributes.Add ("class", "ColumnDropZoneCell");

                    htmlTableCell.Attributes.Add ("style", "min-width: 8px; min-height: 24px; display: none");

                    dropZone = RenderControl_DropZone (sectionControl, dropZonePosition);

                    htmlTableCell.Controls.Add (dropZone);

                    htmlTableRow.Controls.Add (htmlTableCell);

                }

            }

            if (!hasColumnSize) { htmlTable.Attributes.Add ("style", "width: 100%;"); }

            contentDiv.Controls.Add (htmlTable);

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_SectionColumn (Mercury.Client.Core.Forms.Controls.SectionColumn sectionColumnControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv;


            contentDiv = HtmlElementDiv ("ControlId_" + sectionColumnControl.ControlId.ToString () + "_Content", String.Empty);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + sectionColumnControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "SectionColumn");

            controlContainerDiv.Attributes.Add ("formControlName", sectionColumnControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (sectionColumnControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/Columns2.png", sectionColumnControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

                controlContainerDiv.Attributes.Add ("onmouseover", "FormControl_OnMouseOver (event);");

                controlContainerDiv.Attributes.Add ("onmouseout", "FormControl_OnMouseOut (event);");


                Int32 dropZonePosition = 0;

                HtmlGenericControl dropZone;


                dropZone = RenderControl_DropZone (sectionColumnControl, dropZonePosition);

                contentDiv.Controls.Add (dropZone);

                foreach (Mercury.Client.Core.Forms.Control currentControl in sectionColumnControl.Controls) {

                    RenderFormControl (contentDiv, currentControl);

                    dropZonePosition = dropZonePosition + 1;

                    dropZone = RenderControl_DropZone (sectionColumnControl, dropZonePosition);

                    contentDiv.Controls.Add (dropZone);

                }

            }

            else {

                foreach (Mercury.Client.Core.Forms.Control currentControl in sectionColumnControl.Controls) {

                    RenderFormControl (contentDiv, currentControl);

                }

            }

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Label (Mercury.Client.Core.Forms.Controls.Label labelControl, Mercury.Client.Core.Forms.Control ownerControl) {

            HtmlGenericControl controlContainerDiv;

            String labelText = labelControl.Text;


            controlContainerDiv = HtmlElementDiv (String.Empty, labelControl.StyleAttribute);


            controlContainerDiv.InnerHtml = labelText;

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_LabelTable (Mercury.Client.Core.Forms.Controls.Label labelControl, Mercury.Client.Core.Forms.Control ownerControl, HtmlGenericControl controlDiv) {

            HtmlGenericControl labelTable;
            
            HtmlGenericControl tableRow;

            HtmlGenericControl labelCell;

            HtmlGenericControl controlCell;
            

            labelTable = new HtmlGenericControl ("table");

            labelTable.Attributes.Add ("style", "width: 100%");

            labelTable.Attributes.Add ("border", "0");

            tableRow = new HtmlGenericControl ("tr");

            labelTable.Controls.Add (tableRow);


            labelCell = new HtmlGenericControl ("td");

            // STYLE IS APPLIED AT THE TABLE CELL AS THE CELL CONTENTS IS THE LABEL TEXT ONLY

            labelCell.Attributes.Add ("style", labelControl.StyleAttribute);

            labelCell.InnerText = labelControl.Text;


            controlCell = new HtmlGenericControl ("td");

            // STYLE IS APPLIED AT THE CONTROL DIV LEVEL, NOT THE TABLE CELL

            controlCell.Controls.Add (controlDiv);


            switch (labelControl.Position) {

                case Mercury.Server.Application.FormControlPosition.Left:

                    tableRow.Controls.Add (labelCell);

                    tableRow.Controls.Add (controlCell);

                    break;

                case Mercury.Server.Application.FormControlPosition.Right:

                    tableRow.Controls.Add (controlCell);

                    tableRow.Controls.Add (labelCell);

                    break;

            }

            return labelTable;

        }

        public HtmlGenericControl RenderControl_Text (Mercury.Client.Core.Forms.Controls.Text textControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + textControl.ControlId.ToString () + "_Content", textControl.StyleAttribute);

            String styleAttribute;


            controlContainerDiv = HtmlElementDiv ("ControlId_" + textControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Text");

            controlContainerDiv.Attributes.Add ("formControlName", textControl.Name);

            styleAttribute = "display: " + (((renderMode == RenderEngineMode.Designer) || (textControl.Visible)) ? "block;" : "none;");

            if ((textControl.Style.TextAlign == "center") && (!String.IsNullOrEmpty (textControl.Style.Width))) {

                styleAttribute = styleAttribute + "margin: 0 auto;";

                styleAttribute = styleAttribute + "width: " + textControl.Style.Width + textControl.Style.WidthUnit;

            }

            controlContainerDiv.Attributes.Add ("style", styleAttribute);


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + textControl.ControlType.ToString () + ".png", textControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }


            contentDiv.InnerHtml = textControl.TextContent;


            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Input (Mercury.Client.Core.Forms.Controls.Input inputControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv;

            String styleAttribute;


            if (inputControl.Label.Visible) {

                contentDiv = HtmlElementDiv ("ControlId_" + inputControl.ControlId.ToString () + "_Content", inputControl.StyleAttributeWithoutText);

            }

            else {

                contentDiv = HtmlElementDiv ("ControlId_" + inputControl.ControlId.ToString () + "_Content", inputControl.StyleAttribute);

            }


            controlContainerDiv = HtmlElementDiv ("ControlId_" + inputControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Input");

            controlContainerDiv.Attributes.Add ("formControlName", inputControl.Name);

            styleAttribute = "display: " + (((renderMode == RenderEngineMode.Designer) || (inputControl.Visible)) ? "block;" : "none;");

            if ((inputControl.Style.TextAlign == "center") && (!String.IsNullOrEmpty (inputControl.Style.Width))) {

                styleAttribute = styleAttribute + "margin: 0 auto;";

                styleAttribute = styleAttribute + "width: " + inputControl.Style.Width + inputControl.Style.WidthUnit;

            }

            controlContainerDiv.Attributes.Add ("style", styleAttribute);


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/Textbox.png", inputControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

                controlContainerDiv.Attributes.Add ("onmouseover", "FormControl_OnMouseOver (event);");

                controlContainerDiv.Attributes.Add ("onmouseout", "FormControl_OnMouseOut (event);");

            }

            
            HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);
            
            Telerik.Web.UI.RadInputControl telerikInput = null;

            switch (inputControl.InputType) {

                case Mercury.Server.Application.FormControlInputType.Text:

                    #region Text Input

                    if (String.IsNullOrEmpty (inputControl.Mask)) {

                        telerikInput = new Telerik.Web.UI.RadTextBox ();

                        ((Telerik.Web.UI.RadTextBox) telerikInput).TextMode = (Telerik.Web.UI.InputMode) inputControl.TextMode;

                        // ((Telerik.Web.UI.RadTextBox) telerikInput).Columns = inputControl.Columns;

                        ((Telerik.Web.UI.RadTextBox) telerikInput).Rows = inputControl.Rows - 1;

                        ((Telerik.Web.UI.RadTextBox) telerikInput).Wrap = inputControl.Wrap;

                        ((Telerik.Web.UI.RadTextBox) telerikInput).MaxLength = inputControl.MaxLength;

                        ((Telerik.Web.UI.RadTextBox) telerikInput).ShowButton = false;

                    }

                    else {

                        telerikInput = new Telerik.Web.UI.RadMaskedTextBox ();

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).TextMode = (Telerik.Web.UI.InputMode) inputControl.TextMode;

                        // ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).Columns = inputControl.Columns;

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).Rows = inputControl.Rows - 1;

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).Wrap = inputControl.Wrap;

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).MaxLength = inputControl.MaxLength;

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).Mask = inputControl.Mask;

                        

                        ((Telerik.Web.UI.RadMaskedTextBox) telerikInput).ShowButton = false;

                    }

                    telerikInput.Text = inputControl.Text;

                    #endregion

                    break;

                case Mercury.Server.Application.FormControlInputType.Numeric:

                    #region Numeric Input Type

                    telerikInput = new Telerik.Web.UI.RadNumericTextBox ();

                    switch (inputControl.NumericType) {

                        case Mercury.Server.Application.FormControlNumericType.Currency:

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).Type = Telerik.Web.UI.NumericType.Currency;

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).NumberFormat.DecimalDigits = 2;

                            break;

                        case Mercury.Server.Application.FormControlNumericType.Integer:

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).Type = Telerik.Web.UI.NumericType.Number;

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).NumberFormat.DecimalDigits = 0;

                            break;

                        case Mercury.Server.Application.FormControlNumericType.Number:

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).Type = Telerik.Web.UI.NumericType.Number;

                            break;

                        case Mercury.Server.Application.FormControlNumericType.Percent:

                            ((Telerik.Web.UI.RadNumericTextBox) telerikInput).Type = Telerik.Web.UI.NumericType.Percent;

                            break;

                    }

                    ((Telerik.Web.UI.RadNumericTextBox) telerikInput).MinValue = inputControl.MinValue;

                    ((Telerik.Web.UI.RadNumericTextBox) telerikInput).MaxValue = inputControl.MaxValue;

                    ((Telerik.Web.UI.RadNumericTextBox) telerikInput).ShowSpinButtons = inputControl.ShowSpinButtons;

                    Double doubleOutput;

                    if (Double.TryParse (inputControl.Text, out doubleOutput)) {

                        ((Telerik.Web.UI.RadNumericTextBox) telerikInput).Value = Double.Parse (inputControl.Text);

                    }

                    #endregion

                    break;

                case Mercury.Server.Application.FormControlInputType.DateTime:

                    #region DateTime Input Type

                    telerikInput = new Telerik.Web.UI.RadDateInput ();

                    ((Telerik.Web.UI.RadDateInput) telerikInput).DateFormat = inputControl.DateFormat;

                    ((Telerik.Web.UI.RadDateInput) telerikInput).DisplayDateFormat = inputControl.DisplayDateFormat;

                    ((Telerik.Web.UI.RadDateInput) telerikInput).ShowButton = false;

                    ((Telerik.Web.UI.RadDateInput) telerikInput).MinDate = inputControl.MinDate;

                    ((Telerik.Web.UI.RadDateInput) telerikInput).MaxDate = inputControl.MaxDate;

                    DateTime dateTimeOutput;

                    if (DateTime.TryParse (inputControl.Text, out dateTimeOutput)) {

                        ((Telerik.Web.UI.RadDateInput) telerikInput).SelectedDate = dateTimeOutput;

                    }

                    else {

                        ((Telerik.Web.UI.RadDateInput) telerikInput).SelectedDate = null;

                    }

                    #endregion

                    break;

            }

            telerikInput.ID = inputControl.Name + "_" + inputControl.ControlId.ToString ();

//            telerikInput.Skin = "Office2007";

            telerikInput.MaxLength = inputControl.MaxLength;

            telerikInput.EmptyMessage = inputControl.EmptyMessage;

            telerikInput.ReadOnly = ((renderMode != RenderEngineMode.Editor) || (inputControl.ReadOnly));

            telerikInput.Enabled = inputControl.Enabled;

            telerikInput.SelectionOnFocus = (Telerik.Web.UI.SelectionOnFocus) inputControl.SelectionOnFocus;

            telerikInput.ShowButton = inputControl.ShowSpinButtons;

            telerikInput.ButtonsPosition = ((Telerik.Web.UI.InputButtonsPosition) ((Int32) inputControl.ButtonPosition));

            telerikInput.Width = new Unit (99, UnitType.Percentage);

            if (inputControl.TabIndex > 0) { telerikInput.TabIndex = (Int16) (inputControl.TabIndex * 10); }

            else { telerikInput.TabIndex = (Int16) (inputControl.TabIndex); }


            telerikInput.EnableViewState = (renderMode == RenderEngineMode.Editor);

            telerikInput.AutoPostBack = false; 

            // if ((renderMode == RenderEngineMode.Editor) && ((inputControl.GetEventHandler ("TextChanged") != null) || (!String.IsNullOrEmpty (inputControl.Validation)))) { telerikInput.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange); }


            if (renderMode == RenderEngineMode.Editor) {

                telerikInput.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                if ((inputControl.GetEventHandler ("TextChanged") != null) || (!String.IsNullOrEmpty (inputControl.Validation))) { 
                    
                    telerikInput.AutoPostBack = true;

                }

                if ((inputControl.InputType == Mercury.Server.Application.FormControlInputType.Text) && (!String.IsNullOrEmpty (inputControl.Validation))) {

                    System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (inputControl.Validation);

                    if ((!String.IsNullOrEmpty (inputControl.Text)) && (!validator.IsMatch (inputControl.Text))) {

                        telerikInput.BackColor = System.Drawing.Color.LightYellow;

                        telerikInput.ToolTip = "Invalid Input";

                    }

                }

            }

            if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor)) {

                contentControlDiv.Controls.Add (telerikInput);

            }

            else { contentControlDiv.InnerText = inputControl.Text; }


            #region Label Layout

            if (!inputControl.Label.Visible) {

                contentDiv.Controls.Add (contentControlDiv);

            }

            else {

                switch (inputControl.Label.Position) {

                    case Mercury.Server.Application.FormControlPosition.Left:

                    case Mercury.Server.Application.FormControlPosition.Right:

                        contentDiv.Controls.Add (RenderControl_LabelTable (inputControl.Label, inputControl, contentControlDiv));

                        break;

                    case Mercury.Server.Application.FormControlPosition.Top:

                        contentDiv.Controls.Add (RenderControl_Label (inputControl.Label, inputControl));

                        contentDiv.Controls.Add (contentControlDiv);

                        break;

                    case Mercury.Server.Application.FormControlPosition.Bottom:

                        contentDiv.Controls.Add (contentControlDiv);

                        contentDiv.Controls.Add (RenderControl_Label (inputControl.Label, inputControl));

                        break;

                }

            }

            #endregion


            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Selection (Mercury.Client.Core.Forms.Controls.Selection selectionControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv; 

            String styleAttribute;

            
            styleAttribute = (selectionControl.Label.Visible)? selectionControl.StyleAttributeWithoutText : selectionControl.StyleAttribute;

            contentDiv = HtmlElementDiv ("ControlId_" + selectionControl.ControlId.ToString () + "_Content", styleAttribute);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + selectionControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Selection");

            controlContainerDiv.Attributes.Add ("formControlName", selectionControl.Name);

            styleAttribute = "display: " + (((renderMode == RenderEngineMode.Designer) || (selectionControl.Visible)) ? "block;" : "none;");

            if ((selectionControl.Style.TextAlign == "center") && (!String.IsNullOrEmpty (selectionControl.Style.Width))) {

                styleAttribute = styleAttribute + "margin: 0 auto;";

                styleAttribute = styleAttribute + "width: " + selectionControl.Style.Width + selectionControl.Style.WidthUnit;

            }

            styleAttribute = styleAttribute + selectionControl.StyleAttributeTextOnly;

            controlContainerDiv.Attributes.Add ("style", styleAttribute);


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + selectionControl.SelectionType.ToString () + ".png", selectionControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }


            HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);

            switch (selectionControl.SelectionType) {

                case Mercury.Server.Application.FormControlSelectionType.DropDownList: 

                    #region Designer/Editor DropDownList

                    Telerik.Web.UI.RadComboBox selectionComboBox = new Telerik.Web.UI.RadComboBox ();

                    selectionComboBox.ID = selectionControl.Name + "_" + selectionControl.ControlId.ToString ();

                    selectionComboBox.EnableViewState = (renderMode == RenderEngineMode.Editor);

                    selectionComboBox.AutoPostBack = false;

                    selectionComboBox.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (selectionControl.EventHandlers.Count > 0);


                    selectionComboBox.Enabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    selectionComboBox.MarkFirstMatch = true;

                    selectionComboBox.NoWrap = !selectionControl.Wrap;

                    selectionComboBox.MaxLength = selectionControl.MaxLength;

                    selectionComboBox.Width = new Unit (100, UnitType.Percentage);

                    if (selectionControl.TabIndex > 0) { selectionComboBox.TabIndex = (Int16) (selectionControl.TabIndex * 10); }

                    else { selectionComboBox.TabIndex = selectionControl.TabIndex; }

                    
                    if ((renderMode == RenderEngineMode.Designer) && (selectionControl.DataSource == Mercury.Server.Application.FormControlDataSource.Reference)) { 

                        selectionComboBox.Text = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Text : String.Empty;

                        selectionComboBox.SelectedValue = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Value : String.Empty;

                    }

                    else if ((renderMode == RenderEngineMode.Editor) && (selectionControl.DataSource == Mercury.Server.Application.FormControlDataSource.Reference)) { 
                            
                        selectionComboBox.EnableLoadOnDemand = true;

                        selectionComboBox.ShowMoreResultsBox = true;

                        selectionComboBox.EnableVirtualScrolling = true;

                        selectionComboBox.TextChanged += new EventHandler(editorPage.FormControl_OnTextChange);

                        selectionComboBox.ItemsRequested += new Telerik.Web.UI.RadComboBoxItemsRequestedEventHandler (editorPage.FormControlSelectionControl_OnItemsRequested);

                        selectionComboBox.SelectedValue = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Value : String.Empty;

                        selectionComboBox.Text = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Text : String.Empty;

                    }

                    else if (selectionControl.DataSource == Mercury.Server.Application.FormControlDataSource.ItemList) {

                        foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                            Telerik.Web.UI.RadComboBoxItem telerikComboBoxItem = new Telerik.Web.UI.RadComboBoxItem (currentItem.Text, currentItem.Value);

                            telerikComboBoxItem.Enabled = currentItem.Enabled;

                            selectionComboBox.Items.Add (telerikComboBoxItem);

                        }

                        selectionComboBox.AllowCustomText = selectionControl.AllowCustomText;

                        if (!selectionControl.HasCustomTextValue) {

                            selectionComboBox.SelectedValue = selectionControl.SelectedValue;

                        }

                        else {

                            selectionComboBox.Text = selectionControl.CustomText;

                        }

                        if (renderMode == RenderEngineMode.Editor) {

                            selectionComboBox.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                        }

                    }

                    contentControlDiv.Controls.Add (selectionComboBox);

                    #endregion

                    break;

                case Mercury.Server.Application.FormControlSelectionType.ListBox:

                    #region Designer/Editor ListBox

                    //ListBox selectionListBox = new ListBox ();

                    //selectionListBox.ID = selectionControl.Name + "_" + selectionControl.ControlId.ToString ();

                    //selectionListBox.EnableViewState = (renderMode == RenderEngineMode.Editor);

                    //selectionListBox.AutoPostBack = false;

                    //selectionListBox.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (selectionControl.EventHandlers.Count > 0);


                    //selectionListBox.Enabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    //selectionListBox.SelectionMode = (ListSelectionMode) ((Int32) selectionControl.SelectionMode);

                    //selectionListBox.Rows = selectionControl.Rows;

                    //selectionListBox.Width = new Unit (100, UnitType.Percentage);

                    //if (selectionControl.TabIndex > 0) { selectionListBox.TabIndex = (Int16) (selectionControl.TabIndex * 10); }


                    //foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                    //    ListItem listItem = new ListItem (currentItem.Text, currentItem.Value, currentItem.Enabled);

                    //    listItem.Selected = currentItem.Selected;

                    //    selectionListBox.Items.Add (listItem);

                    //}

                    //if (renderMode == RenderEngineMode.Editor) {

                    //    selectionListBox.SelectedIndexChanged += new EventHandler (editorPage.FormControlSelectionControl_OnSelectedIndexChanged);

                    //}


                    Telerik.Web.UI.RadListBox selectionListBox = new Telerik.Web.UI.RadListBox ();

                    selectionListBox.ID = selectionControl.Name + "_" + selectionControl.ControlId.ToString ();

                    selectionListBox.EnableViewState = (renderMode == RenderEngineMode.Editor);

                    selectionListBox.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (selectionControl.EventHandlers.Count > 0);



                    selectionListBox.Enabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    selectionListBox.SelectionMode = (Telerik.Web.UI.ListBoxSelectionMode) ((Int32) selectionControl.SelectionMode);

                    if (String.IsNullOrWhiteSpace (selectionControl.Style.Width)) {

                        selectionListBox.Width = new Unit (100, UnitType.Percentage);

                    }

                    else {

                        selectionListBox.Width = new Unit (Convert.ToDouble (selectionControl.Style.Width), ConvertUnit (selectionControl.Style.Width));

                    }

                    if (selectionControl.TabIndex > 0) { selectionListBox.TabIndex = (Int16) (selectionControl.TabIndex * 10); }

                    else { selectionListBox.TabIndex = (Int16) selectionControl.TabIndex; }



                    foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                        Telerik.Web.UI.RadListBoxItem listItem = new Telerik.Web.UI.RadListBoxItem (currentItem.Text, currentItem.Value);

                        listItem.Enabled = currentItem.Enabled;

                        listItem.Selected = currentItem.Selected;

                        selectionListBox.Items.Add (listItem);

                    }

                    if (renderMode == RenderEngineMode.Editor) {

                        selectionListBox.SelectedIndexChanged += new EventHandler (editorPage.FormControlSelectionControl_OnSelectedIndexChanged);

                    }

                    contentControlDiv.Controls.Add (selectionListBox);

                    #endregion

                    break;

                case Mercury.Server.Application.FormControlSelectionType.CheckBox:

                    #region Designer/Editor CheckBox

                    CheckBoxList selectionCheckBox = new CheckBoxList ();

                    selectionCheckBox.ID = selectionControl.Name + "_" + selectionControl.ControlId.ToString ();

                    selectionCheckBox.EnableViewState = (renderMode == RenderEngineMode.Editor);

                    selectionCheckBox.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (selectionControl.EventHandlers.Count > 0);


                    selectionCheckBox.Enabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    selectionCheckBox.RepeatColumns = selectionControl.Columns;

                    selectionCheckBox.RepeatDirection = (RepeatDirection) ((Int32) selectionControl.Direction);

                    selectionCheckBox.Attributes.Add ("style", selectionControl.StyleAttributeTextOnly);

                    selectionCheckBox.Width = new Unit (100, UnitType.Percentage);

                    if (selectionControl.TabIndex > 0) { selectionCheckBox.TabIndex = (Int16) (selectionControl.TabIndex * 10); }

                    else { selectionCheckBox.TabIndex = (Int16) selectionControl.TabIndex; }


                    foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                        ListItem listItem = new ListItem (currentItem.Text, currentItem.Value, currentItem.Enabled);

                        listItem.Selected = currentItem.Selected;

                        selectionCheckBox.Items.Add (listItem);

                    }

                    if (renderMode == RenderEngineMode.Editor) {

                        selectionCheckBox.SelectedIndexChanged += new EventHandler (editorPage.FormControlSelectionControl_OnSelectedIndexChanged);

                    }

                    contentControlDiv.Controls.Add (selectionCheckBox);

                    #endregion 

                    break;

                case Mercury.Server.Application.FormControlSelectionType.RadioButton:

                    #region Designer/Editor RadioButton

                    RadioButtonList selectionRadioButton = new RadioButtonList ();

                    selectionRadioButton.ID = selectionControl.Name + "_" + selectionControl.ControlId.ToString ();

                    selectionRadioButton.EnableViewState = (renderMode == RenderEngineMode.Editor);

                    selectionRadioButton.AutoPostBack = false;

                    selectionRadioButton.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (selectionControl.EventHandlers.Count > 0);


                    selectionRadioButton.Enabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    selectionRadioButton.RepeatColumns = selectionControl.Columns;

                    selectionRadioButton.RepeatDirection = (RepeatDirection) ((Int32) selectionControl.Direction);

                    selectionRadioButton.Attributes.Add ("style", selectionControl.StyleAttributeTextOnly);

                    selectionRadioButton.Width = new Unit (100, UnitType.Percentage);

                    if (selectionControl.TabIndex > 0) { selectionRadioButton.TabIndex = (Int16) (selectionControl.TabIndex * 10); }

                    else { selectionRadioButton.TabIndex = (Int16) selectionControl.TabIndex; }



                    foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                        ListItem listItem = new ListItem (currentItem.Text, currentItem.Value, currentItem.Enabled);

                        listItem.Selected = currentItem.Selected;

                        selectionRadioButton.Items.Add (listItem);

                    }

                    if (renderMode == RenderEngineMode.Editor) {

                        selectionRadioButton.SelectedIndexChanged += new EventHandler (editorPage.FormControlSelectionControl_OnSelectedIndexChanged);

                    }

                    contentControlDiv.Controls.Add (selectionRadioButton);

                    #endregion

                    break;

            } // switch (selectionControl.SelectionType) { 


            if (!selectionControl.Label.Visible) {

                contentDiv.Controls.Add (contentControlDiv);

            }

            else {

                switch (selectionControl.Label.Position) {

                    case Mercury.Server.Application.FormControlPosition.Left: 

                    case Mercury.Server.Application.FormControlPosition.Right:

                        contentDiv.Controls.Add (RenderControl_LabelTable (selectionControl.Label, selectionControl, contentControlDiv));

                        break;

                    case Mercury.Server.Application.FormControlPosition.Top:

                        contentDiv.Controls.Add (RenderControl_Label (selectionControl.Label, selectionControl));

                        contentDiv.Controls.Add (contentControlDiv);

                        break;

                    case Mercury.Server.Application.FormControlPosition.Bottom:

                        contentDiv.Controls.Add (contentControlDiv);

                        contentDiv.Controls.Add (RenderControl_Label (selectionControl.Label, selectionControl));

                        break;

                }

            }

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Button (Mercury.Client.Core.Forms.Controls.Button buttonControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + buttonControl.ControlId.ToString () + "_Content", buttonControl.StyleAttribute);

            String styleAttribute;


            controlContainerDiv = HtmlElementDiv ("ControlId_" + buttonControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Button");

            controlContainerDiv.Attributes.Add ("formControlName", buttonControl.Name);

            styleAttribute = "display: " + (((renderMode == RenderEngineMode.Designer) || (buttonControl.Visible)) ? "block;" : "none;");

            if ((buttonControl.Style.TextAlign == "center") && (!String.IsNullOrEmpty (buttonControl.Style.Width))) {

                styleAttribute = styleAttribute + "margin: 0 auto;";

                styleAttribute = styleAttribute + "width: " + buttonControl.Style.Width + buttonControl.Style.WidthUnit;

            }

            controlContainerDiv.Attributes.Add ("style", styleAttribute);


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + buttonControl.ControlType.ToString () + ".png", buttonControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }


            Button button = new Button ();

            button.ID = buttonControl.Name + "_" + buttonControl.ControlId.ToString ();

            button.Text = buttonControl.Text;

            button.Attributes.Add ("style", buttonControl.StyleAttribute);

            button.Enabled = buttonControl.Enabled && (buttonControl.EventHandlers.Count > 0) && (renderMode == RenderEngineMode.Editor);

            button.UseSubmitBehavior = false;

            if (buttonControl.TabIndex > 0) { button.TabIndex = (Int16) (buttonControl.TabIndex * 10); }

            else { button.TabIndex = (Int16) buttonControl.TabIndex; }


            if ((renderMode == RenderEngineMode.Editor) && (buttonControl.EventHandlers.Count > 0)) {

                button.Click += new EventHandler (editorPage.FormControlButton_OnClick);

            }


            controlContainerDiv.Controls.Add (button);

            return controlContainerDiv;

        }

        protected HtmlGenericControl RenderControl_EntityMember (Mercury.Client.Core.Forms.Controls.Entity entityControl) {

            HtmlGenericControl controlDiv = HtmlElementDiv (String.Empty, String.Empty);

            HtmlGenericControl selectedMemberDiv = HtmlElementDiv (entityControl.Name + "_MemberSelected_" + entityControl.ControlId.ToString (), String.Empty);

            HtmlGenericControl memberSearchDiv = HtmlElementDiv (String.Empty, (((entityControl.ReadOnly) || (renderMode == RenderEngineMode.Viewer)) ? "display: none;" : String.Empty));

            HtmlGenericControl searchMessageDiv = HtmlElementDiv (entityControl.Name + "_MemberSearchMessage_" + entityControl.ControlId.ToString (), ((entityControl.ReadOnly) ? "display: none;" : String.Empty));

            HtmlGenericControl searchResultsDiv = HtmlElementDiv (String.Empty, String.Empty);


            HtmlGenericControl htmlTable;

            HtmlGenericControl htmlTableRow;

            HtmlGenericControl htmlTableCell;


            Client.Core.Member.Member selectedMember = entityControl.Member;

            String selectedMemberAnchor;


            if (selectedMember != null) {

                selectedMemberAnchor = "<a href=\"/Application/Member/MemberProfile.aspx?MemberId=" + selectedMember.Id + "\" target=\"_blank\">" + selectedMember.Entity.Name + "</a>";

            }

            else { selectedMemberAnchor = "No Member Selected"; }


            #region Selected Member Information

            htmlTable = new HtmlGenericControl ("table");

            htmlTable.Attributes.Add ("style", "width: 100%; padding 4px;" + entityControl.StyleAttributeTextOnly);

            htmlTableRow = new HtmlGenericControl ("tr");

            htmlTable.Controls.Add (htmlTableRow);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left;");

            htmlTableCell.InnerHtml = "Member Name: " + selectedMemberAnchor;

            htmlTableRow.Controls.Add (htmlTableCell);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "Birth Date: " + ((selectedMember != null) ? selectedMember.BirthDate.ToString ("MM/dd/yyyy") : "00/00/0000");

            htmlTableRow.Controls.Add (htmlTableCell);


            htmlTableCell = new HtmlGenericControl ("td");

            switch (entityControl.DisplayAgeFormat) { 

                case Mercury.Server.Application.FormControlEntityDisplayAgeFormat.InYears:

                    htmlTableCell.InnerText = "Current Age: " + ((selectedMember != null) ? selectedMember.CurrentAge.ToString () + "y" : "0");

                    break;

                case Mercury.Server.Application.FormControlEntityDisplayAgeFormat.InMonths:

                    htmlTableCell.InnerText = "Current Age: " + ((selectedMember != null) ? selectedMember.CurrentAgeInMonths.ToString () + "m" : "0");

                    break;

            }

            
            htmlTableRow.Controls.Add (htmlTableCell);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "Gender: " + ((selectedMember != null) ? selectedMember.GenderDescription : "---");

            htmlTableRow.Controls.Add (htmlTableCell);


            selectedMemberDiv.Controls.Add (htmlTable);

            #endregion


            #region Member Search 
            
            htmlTable = new HtmlGenericControl ("table");

            htmlTable.Attributes.Add ("style", "width: 100%; padding 4px; " + entityControl.StyleAttributeTextOnly + " " + ((isNonstandardCss) ? "table-layout: fixed; " : String.Empty));

            htmlTableRow = new HtmlGenericControl ("tr");

            htmlTable.Controls.Add (htmlTableRow);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left;");

            htmlTableCell.InnerText = "Member Name:";

            htmlTableRow.Controls.Add (htmlTableCell);

            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left;");

            Telerik.Web.UI.RadTextBox inputMemberName = new Telerik.Web.UI.RadTextBox ();

            inputMemberName.ID = entityControl.Name + "_MemberName_" + entityControl.ControlId.ToString ();

            inputMemberName.Width = new Unit (100, UnitType.Percentage);

            inputMemberName.EmptyMessage = "Full or partial (last, first)";

            inputMemberName.MaxLength = 60;

            inputMemberName.Enabled = entityControl.Enabled;

            if (entityControl.TabIndex > 0) { inputMemberName.TabIndex = (Int16) (entityControl.TabIndex * 10); }

            else { inputMemberName.TabIndex = (Int16) entityControl.TabIndex; }


            htmlTableCell.Controls.Add (inputMemberName);

            htmlTableRow.Controls.Add (htmlTableCell);



            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "Birth Date:";

            htmlTableRow.Controls.Add (htmlTableCell);

            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableRow.Controls.Add (htmlTableCell);

            Telerik.Web.UI.RadDateInput inputMemberBirthDate = new Telerik.Web.UI.RadDateInput ();

            inputMemberBirthDate.MinDate = new DateTime (1900, 1, 1);

            inputMemberBirthDate.ID = entityControl.Name + "_MemberBirthDate_" + entityControl.ControlId.ToString ();

            inputMemberBirthDate.Width = new Unit (75, UnitType.Pixel);

            if (entityControl.TabIndex > 0) { inputMemberBirthDate.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 1); }

            else { inputMemberBirthDate.TabIndex = (Int16) entityControl.TabIndex; }

            inputMemberBirthDate.EmptyMessage = "(required)";

            if (application.HasEnvironmentPermission ("Search.Member.OptionalBirthDate")) { inputMemberBirthDate.EmptyMessage = "(optional)"; }

            inputMemberBirthDate.MaxLength = 10;

            inputMemberBirthDate.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (inputMemberBirthDate);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "(or) Id:";

            htmlTableRow.Controls.Add (htmlTableCell);

            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableRow.Controls.Add (htmlTableCell);

            Telerik.Web.UI.RadTextBox inputMemberId = new Telerik.Web.UI.RadTextBox ();

            inputMemberId.ID = entityControl.Name + "_MemberId_" + entityControl.ControlId.ToString ();

            inputMemberId.Width = new Unit (100, UnitType.Pixel);

            if (entityControl.TabIndex > 0) { inputMemberId.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 2); }

            else { inputMemberId.TabIndex = (Int16) entityControl.TabIndex; }

            inputMemberId.EmptyMessage = "";

            inputMemberId.MaxLength = 20;

            inputMemberId.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (inputMemberId);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableRow.Controls.Add (htmlTableCell);

            Button entityMemberSearchButton = new Button ();

            entityMemberSearchButton.ID = entityControl.Name + "_MemberSearchButton_" + entityControl.ControlId.ToString ();

            entityMemberSearchButton.Text = "Search";

            entityMemberSearchButton.Width = new Unit (73);

            entityMemberSearchButton.Height = new Unit (24);

            entityMemberSearchButton.Font.Names = new String[] { "segoe ui", "arial" };

            entityMemberSearchButton.Font.Size = new FontUnit (11, UnitType.Pixel);

            entityMemberSearchButton.Enabled = false;

            if (entityControl.TabIndex > 0) { entityMemberSearchButton.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 3); }

            else { entityMemberSearchButton.TabIndex = (Int16) entityControl.TabIndex; }

            if (renderMode == RenderEngineMode.Editor) { entityMemberSearchButton.Enabled = !entityControl.ReadOnly; }

            if (renderMode == RenderEngineMode.Editor) { entityMemberSearchButton.Click += new EventHandler (editorPage.FormControlButton_OnClick); }

            entityMemberSearchButton.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (entityMemberSearchButton);


            memberSearchDiv.Controls.Add (htmlTable);

            #endregion


            #region Search Message

            String searchStatus = String.Empty;

            String searchMessage = String.Empty;


            if (renderMode == RenderEngineMode.Editor) {

                Exception searchException = (Exception) Session[sessionCachePrefix + entityControl.Name + "_MemberSearchException_" + entityControl.ControlId.ToString ()];

                searchStatus = (String) Session[sessionCachePrefix + entityControl.Name + "_MemberSearchStatus_" + entityControl.ControlId.ToString ()];

                if (searchException != null) { searchMessage = searchException.Message; }

            }


            if (!String.IsNullOrEmpty (searchMessage)) {

                searchMessageDiv.Attributes.Add ("style", "width: 100%; height: 24px; font-family: Arial; font-size: 10pt; line-height: 150%"); 

            }

            searchMessageDiv.InnerText = searchMessage;

            #endregion 


            #region Search Results

            Telerik.Web.UI.RadGrid resultsGrid = new Telerik.Web.UI.RadGrid ();

            resultsGrid.Visible = (searchStatus == "Success");

            resultsGrid.ID = entityControl.Name + "_MemberResults_" + entityControl.ControlId.ToString ();

            resultsGrid.Width = new Unit (99, UnitType.Percentage);

            resultsGrid.AutoGenerateColumns = false;


            resultsGrid.EnableViewState = false;

            resultsGrid.MasterTableView.Width = new Unit (99, UnitType.Percentage);

            resultsGrid.MasterTableView.EnableColumnsViewState = false;

            resultsGrid.MasterTableView.EnableViewState = false;


            resultsGrid.ClientSettings.Scrolling.AllowScroll = true;

            resultsGrid.ClientSettings.Scrolling.UseStaticHeaders = true;

            resultsGrid.ClientSettings.Scrolling.SaveScrollPosition = false;



            if (renderMode == RenderEngineMode.Editor) {

                System.Data.DataTable memberResultsTable = (System.Data.DataTable) Session[sessionCachePrefix + entityControl.Name + "_MemberResultsTable_" + entityControl.ControlId.ToString ()];

                if (memberResultsTable == null) { memberResultsTable = new DataTable (); }


                foreach (System.Data.DataColumn currentColumn in memberResultsTable.Columns) {

                    Telerik.Web.UI.GridBoundColumn gridColumn = new Telerik.Web.UI.GridBoundColumn ();

                    gridColumn.UniqueName = currentColumn.ColumnName;

                    gridColumn.DataField = currentColumn.ColumnName;

                    gridColumn.HeaderText = currentColumn.ColumnName;

                    gridColumn.Visible = ((currentColumn.ColumnName != "MemberId") && (currentColumn.ColumnName != "EntityId"));

                    if (currentColumn.ColumnName == "Name") { gridColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left; }

                    resultsGrid.MasterTableView.Columns.Add (gridColumn);

                }


                #region Master Table View - Button Column - Select Member

                Telerik.Web.UI.GridButtonColumn selectRowButton = new Telerik.Web.UI.GridButtonColumn ();

                selectRowButton.UniqueName = "Select";

                selectRowButton.ButtonType = Telerik.Web.UI.GridButtonColumnType.LinkButton;

                selectRowButton.HeaderText = "Action";

                selectRowButton.Text = "Select";

                selectRowButton.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                selectRowButton.CommandName = "Select";

                resultsGrid.MasterTableView.Columns.Add (selectRowButton);

                #endregion


                resultsGrid.DataSource = memberResultsTable;

                resultsGrid.Rebind ();

                resultsGrid.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler (editorPage.FormControlEntityGrid_OnItemCommand);

            }


            searchResultsDiv.Controls.Add (resultsGrid);

            #endregion 


            controlDiv.Controls.Add (selectedMemberDiv);

            if (renderMode == RenderEngineMode.Editor) {

                controlDiv.Controls.Add (memberSearchDiv);

                controlDiv.Controls.Add (searchMessageDiv);

                controlDiv.Controls.Add (searchResultsDiv);

            }

            return controlDiv;

        }

        protected HtmlGenericControl RenderControl_EntityProvider (Mercury.Client.Core.Forms.Controls.Entity entityControl) {

            HtmlGenericControl controlDiv = HtmlElementDiv (String.Empty, String.Empty);

            HtmlGenericControl selectedProviderDiv = HtmlElementDiv (entityControl.Name + "_ProviderSelected_" + entityControl.ControlId.ToString (), String.Empty);

            HtmlGenericControl providerSearchDiv = HtmlElementDiv (String.Empty, (((entityControl.ReadOnly) || (renderMode == RenderEngineMode.Viewer)) ? "display: none;" : String.Empty));

            HtmlGenericControl searchMessageDiv = HtmlElementDiv (entityControl.Name + "_ProviderSearchMessage_" + entityControl.ControlId.ToString (), ((entityControl.ReadOnly) ? "display: none;" : String.Empty));

            HtmlGenericControl searchResultsDiv = HtmlElementDiv (String.Empty, String.Empty);


            HtmlGenericControl htmlTable;

            HtmlGenericControl htmlTableRow;

            HtmlGenericControl htmlTableCell;


            Client.Core.Provider.Provider selectedProvider = entityControl.Provider;

            String selectedProviderAnchor;


            if (selectedProvider != null) {

                selectedProviderAnchor = "<a href=\"/Application/Provider/ProviderProfile.aspx?ProviderId=" + selectedProvider.Id + "\" target=\"_blank\">" + selectedProvider.Entity.Name + "</a>";

            }

            else { selectedProviderAnchor = "No Provider Selected"; }


            #region Selected Provider Information

            htmlTable = new HtmlGenericControl ("table");

            htmlTable.Attributes.Add ("style", "width: 100%; padding 4px;" + entityControl.StyleAttributeTextOnly);

            htmlTableRow = new HtmlGenericControl ("tr");

            htmlTable.Controls.Add (htmlTableRow);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left;");

            htmlTableCell.InnerHtml = "Provider Name: " + selectedProviderAnchor;

            htmlTableRow.Controls.Add (htmlTableCell);

            
            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "NPI: " + ((selectedProvider != null) ? selectedProvider.NationalProviderId : "---");

            htmlTableRow.Controls.Add (htmlTableCell);


            selectedProviderDiv.Controls.Add (htmlTable);

            #endregion


            #region Provider Search

            htmlTable = new HtmlGenericControl ("table");

            htmlTable.Attributes.Add ("style", "width: 100%; padding 4px; font-family: arial; font-size: 10pt; line-height: 150%; font-weight: normal; table-layout: fixed");

            htmlTableRow = new HtmlGenericControl ("tr");

            htmlTable.Controls.Add (htmlTableRow);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left;");

            htmlTableCell.InnerText = "Provider Name:";

            htmlTableRow.Controls.Add (htmlTableCell);

            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.Attributes.Add ("style", "text-align: left; min-width: 120px");

            Telerik.Web.UI.RadTextBox inputMemberName = new Telerik.Web.UI.RadTextBox ();

            inputMemberName.ID = entityControl.Name + "_ProviderName_" + entityControl.ControlId.ToString ();

            inputMemberName.Width = new Unit (100, UnitType.Percentage);

            if (entityControl.TabIndex > 0) { inputMemberName.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 0); }

            else { inputMemberName.TabIndex = (Int16) entityControl.TabIndex; }


            inputMemberName.EmptyMessage = "Full or partial (last, first)";

            inputMemberName.MaxLength = 60;

            inputMemberName.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (inputMemberName);

            htmlTableRow.Controls.Add (htmlTableCell);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableCell.InnerText = "(or) Id:";

            htmlTableRow.Controls.Add (htmlTableCell);

            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableRow.Controls.Add (htmlTableCell);

            Telerik.Web.UI.RadTextBox inputMemberId = new Telerik.Web.UI.RadTextBox ();

            inputMemberId.ID = entityControl.Name + "_ProviderId_" + entityControl.ControlId.ToString ();

            inputMemberId.Width = new Unit (100, UnitType.Pixel);

            if (entityControl.TabIndex > 0) { inputMemberId.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 1); }

            else { inputMemberId.TabIndex = (Int16) entityControl.TabIndex; }


            inputMemberId.EmptyMessage = "";

            inputMemberId.MaxLength = 20;

            inputMemberId.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (inputMemberId);


            htmlTableCell = new HtmlGenericControl ("td");

            htmlTableRow.Controls.Add (htmlTableCell);

            Button entityMemberSearchButton = new Button ();

            entityMemberSearchButton.ID = entityControl.Name + "_ProviderSearchButton_" + entityControl.ControlId.ToString ();

            entityMemberSearchButton.Text = "Search";

            entityMemberSearchButton.Width = new Unit (73);

            entityMemberSearchButton.Height = new Unit (24);

            entityMemberSearchButton.Font.Names = new String[] { "segoe ui", "arial" };

            entityMemberSearchButton.Font.Size = new FontUnit (11, UnitType.Pixel);

            entityMemberSearchButton.Enabled = false;

            if (entityControl.TabIndex > 0) { entityMemberSearchButton.TabIndex = (Int16) ((entityControl.TabIndex * 10) + 2); }

            else { entityMemberSearchButton.TabIndex = (Int16) entityControl.TabIndex; }

            if (renderMode == RenderEngineMode.Editor) { entityMemberSearchButton.Enabled = !entityControl.ReadOnly; }

            if (renderMode == RenderEngineMode.Editor) { entityMemberSearchButton.Click += new EventHandler (editorPage.FormControlButton_OnClick); }

            entityMemberSearchButton.Enabled = entityControl.Enabled;

            htmlTableCell.Controls.Add (entityMemberSearchButton);


            providerSearchDiv.Controls.Add (htmlTable);

            #endregion


            #region Search Message

            String searchStatus = String.Empty;

            String searchMessage = String.Empty;


            if (renderMode == RenderEngineMode.Editor) {

                Exception searchException = (Exception) Session[sessionCachePrefix + entityControl.Name + "_ProviderSearchException_" + entityControl.ControlId.ToString ()];

                searchStatus = (String) Session[sessionCachePrefix + entityControl.Name + "_ProviderSearchStatus_" + entityControl.ControlId.ToString ()];

                if (searchException != null) { searchMessage = searchException.Message; }

            }


            if (!String.IsNullOrEmpty (searchMessage)) {

                searchMessageDiv.Attributes.Add ("style", "width: 100%; height: 24px; font-family: Arial; font-size: 10pt; line-height: 150%");

            }

            searchMessageDiv.InnerText = searchMessage;

            #endregion


            #region Search Results

            Telerik.Web.UI.RadGrid resultsGrid = new Telerik.Web.UI.RadGrid ();

            resultsGrid.Visible = (searchStatus == "Success");

            resultsGrid.ID = entityControl.Name + "_ProviderResults_" + entityControl.ControlId.ToString ();

            resultsGrid.Width = new Unit (((isNonstandardCss) ? 98 : 100), UnitType.Percentage);

            resultsGrid.AutoGenerateColumns = false;

            //resultsGrid.EnableEmbeddedSkins = false;

            //resultsGrid.Skin = "Vista";


            resultsGrid.MasterTableView.Width = new Unit (((isNonstandardCss) ? 98 : 100), UnitType.Percentage);

            resultsGrid.EnableViewState = false;

            resultsGrid.MasterTableView.EnableColumnsViewState = false;

            resultsGrid.MasterTableView.EnableViewState = false;


            resultsGrid.ClientSettings.Scrolling.AllowScroll = true;

            resultsGrid.ClientSettings.Scrolling.UseStaticHeaders = true;

            resultsGrid.ClientSettings.Scrolling.SaveScrollPosition = false;



            if (renderMode == RenderEngineMode.Editor) {

                System.Data.DataTable providerResultsTable = (System.Data.DataTable) Session[sessionCachePrefix + entityControl.Name + "_ProviderResultsTable_" + entityControl.ControlId.ToString ()];

                if (providerResultsTable == null) { providerResultsTable = new DataTable (); }


                foreach (System.Data.DataColumn currentColumn in providerResultsTable.Columns) {

                    Telerik.Web.UI.GridBoundColumn gridColumn = new Telerik.Web.UI.GridBoundColumn ();

                    gridColumn.UniqueName = currentColumn.ColumnName;

                    gridColumn.DataField = currentColumn.ColumnName;

                    gridColumn.HeaderText = currentColumn.ColumnName;

                    gridColumn.Visible = ((currentColumn.ColumnName != "ProviderId") && (currentColumn.ColumnName != "EntityId"));

                    if (currentColumn.ColumnName == "Name") { gridColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left; }

                    resultsGrid.MasterTableView.Columns.Add (gridColumn);

                }


                #region Master Table View - Button Column - Select Provider

                Telerik.Web.UI.GridButtonColumn selectRowButton = new Telerik.Web.UI.GridButtonColumn ();

                selectRowButton.UniqueName = "Select";

                selectRowButton.ButtonType = Telerik.Web.UI.GridButtonColumnType.LinkButton;

                selectRowButton.HeaderText = "Action";

                selectRowButton.Text = "Select";

                selectRowButton.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                selectRowButton.CommandName = "Select";

                resultsGrid.MasterTableView.Columns.Add (selectRowButton);

                #endregion


                resultsGrid.DataSource = providerResultsTable;

                resultsGrid.Rebind ();

                resultsGrid.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler (editorPage.FormControlEntityGrid_OnItemCommand);

            }


            searchResultsDiv.Controls.Add (resultsGrid);

            #endregion


            controlDiv.Controls.Add (selectedProviderDiv);

            controlDiv.Controls.Add (providerSearchDiv);

            controlDiv.Controls.Add (searchMessageDiv);

            controlDiv.Controls.Add (searchResultsDiv);

            return controlDiv;

        }

        public HtmlGenericControl RenderControl_Entity (Mercury.Client.Core.Forms.Controls.Entity entityControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + entityControl.ControlId.ToString () + "_Content", entityControl.StyleAttribute);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + entityControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Entity");

            controlContainerDiv.Attributes.Add ("formControlName", entityControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (entityControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + entityControl.ControlType.ToString () + ".png", entityControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }

            HtmlGenericControl controlDiv = HtmlElementDiv (String.Empty, String.Empty);

            switch (entityControl.DisplayStyle) {

                case Mercury.Server.Application.FormControlEntityDisplayStyle.NormalExpanded:

                    #region Normal/Expanded Rendering

                    switch (entityControl.EntityType) {

                        case Mercury.Server.Application.EntityType.Member:

                            controlDiv = RenderControl_EntityMember (entityControl);

                            break;

                        case Mercury.Server.Application.EntityType.Provider:

                            controlDiv = RenderControl_EntityProvider (entityControl);

                            break;

                        default:

                            controlDiv = HtmlElementDiv (String.Empty, String.Empty);

                            controlDiv.InnerText = "Not implemented. Unknown Entity Type for Entity Control (" + entityControl.EntityType.ToString () + ").";

                            break;

                    }

                    #endregion

                    break;

                case Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly:

                    #region Name Only/Advanced

                    HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);

                    if (renderMode == RenderEngineMode.Viewer) {

                        contentControlDiv.InnerText = entityControl.EntityName;

                    }

                    else {

                        Telerik.Web.UI.RadComboBox entityNameComboBox = new Telerik.Web.UI.RadComboBox ();

                        entityNameComboBox.ID = entityControl.Name + "_" + entityControl.ControlId.ToString () + "_Name";

                        entityNameComboBox.AutoPostBack = (renderMode == RenderEngineMode.Editor);

                        entityNameComboBox.AllowCustomText = entityControl.AllowCustomEntityName;

                        entityNameComboBox.MarkFirstMatch = true;

                        entityNameComboBox.NoWrap = true;

                        entityNameComboBox.MaxLength = 60;

                        entityNameComboBox.Width = new Unit (100, UnitType.Percentage);

                        entityNameComboBox.Enabled = !((renderMode != RenderEngineMode.Editor) || (entityControl.ReadOnly) || (!entityControl.Enabled));

                        if (entityControl.TabIndex > 0) { entityNameComboBox.TabIndex = (Int16) (entityControl.TabIndex * 10); }

                        else { entityNameComboBox.TabIndex = (Int16) entityControl.TabIndex; }

                        if (renderMode == RenderEngineMode.Editor) {

                            switch (entityControl.EntityType) {

                                case Mercury.Server.Application.EntityType.Member:

                                    entityNameComboBox.DropDownWidth = Unit.Pixel (500);

                                    entityNameComboBox.HeaderTemplate = new Mercury.Web.Application.Forms.FormEditor.EntityControlMemberHeaderTemplate ();

                                    entityNameComboBox.ItemTemplate = new Mercury.Web.Application.Forms.FormEditor.EntityControlMemberItemTemplate ();

                                    entityNameComboBox.HighlightTemplatedItems = true;

                                    break;

                                case Mercury.Server.Application.EntityType.Provider:

                                    entityNameComboBox.DropDownWidth = Unit.Pixel (700);

                                    entityNameComboBox.HeaderTemplate = new FormEditor.EntityControlProviderHeaderTemplate ();

                                    entityNameComboBox.ItemTemplate = new FormEditor.EntityControlProviderItemTemplate ();

                                    entityNameComboBox.HighlightTemplatedItems = true;

                                    break;

                            }

                            entityNameComboBox.Height = 150;

                            entityNameComboBox.EnableLoadOnDemand = true;

                            entityNameComboBox.ShowMoreResultsBox = true;

                            entityNameComboBox.EnableVirtualScrolling = true;

                            entityNameComboBox.ItemsRequested += new Telerik.Web.UI.RadComboBoxItemsRequestedEventHandler (editorPage.FormControlEntityControl_NameOnItemsRequested);

                            entityNameComboBox.ItemDataBound += new Telerik.Web.UI.RadComboBoxItemEventHandler(editorPage.FormControlEntityControl_NameOnItemDataBound);

                            entityNameComboBox.TextChanged += new EventHandler(editorPage.FormControlEntityControl_NameOnTextChanged);

                        }

                        entityNameComboBox.SelectedValue = entityControl.EntityObjectId.ToString ();

                        entityNameComboBox.Text = entityControl.EntityName;

                        contentControlDiv.Controls.Add (entityNameComboBox);

                    }

                    #region Label Layout

                    if (!entityControl.Label.Visible) {

                        contentDiv.Controls.Add (contentControlDiv);

                    }

                    else {

                        switch (entityControl.Label.Position) {

                            case Mercury.Server.Application.FormControlPosition.Left:

                            case Mercury.Server.Application.FormControlPosition.Right:

                                contentDiv.Controls.Add (RenderControl_LabelTable (entityControl.Label, entityControl, contentControlDiv));

                                break;

                            case Mercury.Server.Application.FormControlPosition.Top:

                                contentDiv.Controls.Add (RenderControl_Label (entityControl.Label, entityControl));

                                contentDiv.Controls.Add (contentControlDiv);

                                break;

                            case Mercury.Server.Application.FormControlPosition.Bottom:

                                contentDiv.Controls.Add (contentControlDiv);

                                contentDiv.Controls.Add (RenderControl_Label (entityControl.Label, entityControl));

                                break;

                        }

                    }

                    #endregion

                    #endregion 

                    break;

            }

            contentDiv.Controls.Add (controlDiv);

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Collection (Mercury.Client.Core.Forms.Controls.Collection collectionControl) {

            DateTime startTime = DateTime.Now;

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + collectionControl.ControlId.ToString () + "_Content", collectionControl.StyleAttribute);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + collectionControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Collection");

            controlContainerDiv.Attributes.Add ("formControlName", collectionControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((collectionControl.Visible) || (renderMode == RenderEngineMode.Designer)) ? "block; " : "none; "));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + collectionControl.ControlType.ToString () + ".png", collectionControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }

            
            HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);

            #region Designer/Editor Control Render

            if ((renderMode == RenderEngineMode.Editor) || (renderMode == RenderEngineMode.Viewer)) {

                #region Collection Grid

                Telerik.Web.UI.RadGrid collectionGrid = new Telerik.Web.UI.RadGrid ();

                collectionGrid.ID = collectionControl.Name + "_CollectionGrid_" + collectionControl.ControlId.ToString ();

                collectionGrid.EnableViewState = (renderMode == RenderEngineMode.Editor);

                collectionGrid.MasterTableView.EnableViewState = (renderMode == RenderEngineMode.Editor);

                collectionGrid.MasterTableView.EnableColumnsViewState = (renderMode == RenderEngineMode.Editor);


                collectionGrid.Width = new Unit (((isNonstandardCss) ? 98 : 100), UnitType.Percentage);

                collectionGrid.Width = new Unit (100, UnitType.Percentage);

                if (!String.IsNullOrEmpty (collectionControl.Style.Height)) { collectionGrid.Height = new Unit (Convert.ToDouble (collectionControl.Style.Height), ConvertUnit (collectionControl.Style.HeightUnit)); }

                collectionGrid.MasterTableView.Width = new Unit (((isNonstandardCss) ? 98 : 100), UnitType.Percentage);

                collectionGrid.MasterTableView.Width = new Unit (100, UnitType.Percentage);

                if (isNonstandardCss) { collectionGrid.MasterTableView.TableLayout = Telerik.Web.UI.GridTableLayout.Fixed; }

                if (collectionControl.TabIndex > 0) { collectionGrid.TabIndex = (Int16) (collectionControl.TabIndex * 10); }

                else { collectionGrid.TabIndex = (Int16) collectionControl.TabIndex; }


                if (renderMode == RenderEngineMode.Editor) {

                    collectionGrid.ClientSettings.Scrolling.AllowScroll = true;

                    collectionGrid.ClientSettings.Scrolling.UseStaticHeaders = true;

                    collectionGrid.ClientSettings.Scrolling.SaveScrollPosition = false;

                    collectionGrid.ClientSettings.Selecting.AllowRowSelect = false;

                }

                else { collectionGrid.ClientSettings.Scrolling.AllowScroll = false; } 



                #region Master Table View - Button Column - Select Member

                if ((!collectionControl.ReadOnly) && (collectionControl.Enabled)) {

                    Telerik.Web.UI.GridButtonColumn gridButtonColumn = new Telerik.Web.UI.GridButtonColumn ();

                    gridButtonColumn.UniqueName = "Select";

                    gridButtonColumn.ButtonType = Telerik.Web.UI.GridButtonColumnType.LinkButton;

                    gridButtonColumn.HeaderText = "Action";

                    gridButtonColumn.Text = "Select";

                    gridButtonColumn.HeaderStyle.HorizontalAlign = HorizontalAlign.Center;

                    gridButtonColumn.CommandName = "Select";

                    gridButtonColumn.Resizable = true;

                    collectionGrid.MasterTableView.Columns.Add (gridButtonColumn);
                    
                }

                #endregion


                collectionGrid.MasterTableView.AutoGenerateColumns = false;

                foreach (System.Data.DataColumn currentColumn in collectionControl.DataTable.Columns) {

                    Telerik.Web.UI.GridBoundColumn gridColumn = new Telerik.Web.UI.GridBoundColumn ();

                    gridColumn.UniqueName = currentColumn.ColumnName;

                    gridColumn.DataField = currentColumn.ColumnName;

                    gridColumn.HeaderText = currentColumn.ColumnName;

                    gridColumn.Visible = (currentColumn.ColumnName != "Id");

                    if (!currentColumn.ColumnName.Contains ("Date")) { gridColumn.ItemStyle.HorizontalAlign = HorizontalAlign.Left; }

                    collectionGrid.MasterTableView.Columns.Add (gridColumn);

                }


                collectionGrid.DataSource = collectionControl.DataTable;

                collectionGrid.Rebind ();
            

                foreach (Telerik.Web.UI.GridDataItem currentItem in collectionGrid.Items) {

                    if (currentItem["Id"].Text == collectionControl.SelectedItem.ToString ()) {

                        currentItem.Selected = true;

                    }

                }


                if (renderMode == RenderEngineMode.Editor) { collectionGrid.ItemCommand += new Telerik.Web.UI.GridCommandEventHandler (editorPage.FormControlCollectionGrid_OnItemCommand); }
                   
                #endregion

                contentControlDiv.Controls.Add (collectionGrid);

            }

            else if (renderMode == RenderEngineMode.Designer) {

                contentControlDiv.Attributes.Add ("style", "background-color: #EEEEEE; height: 100%; text-align: center;");

                contentControlDiv.InnerText = "[" + collectionControl.CollectionType.ToString () + " Collection]";

            }

            else {

                contentControlDiv.InnerText = "[Not Implemented: " + collectionControl.CollectionType.ToString () + " Collection]";

            }

            #endregion


            contentDiv.Controls.Add (contentControlDiv);


            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Address (Mercury.Client.Core.Forms.Controls.Address addressControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + addressControl.ControlId.ToString () + "_Content", addressControl.StyleAttribute);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + addressControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Address");

            controlContainerDiv.Attributes.Add ("formControlName", addressControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (addressControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/" + addressControl.ControlType.ToString () + ".png", addressControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }

            HtmlGenericControl controlDiv = HtmlElementDiv (String.Empty, String.Empty);


            #region Address Table - Declarations

            HtmlGenericControl htmlAddressTable;


            HtmlGenericControl htmlAddressLine1Row;

            HtmlGenericControl htmlAddressLine1CellLabel;

            HtmlGenericControl htmlAddressLine1CellContent;
            
            Telerik.Web.UI.RadInputControl telerikInputLine1 = null;

            
            HtmlGenericControl htmlAddressLine2Row;

            HtmlGenericControl htmlAddressLine2CellLabel;

            HtmlGenericControl htmlAddressLine2CellContent;
            
            Telerik.Web.UI.RadInputControl telerikInputLine2 = null;


            HtmlGenericControl htmlAddressCityStateZipRow;

            HtmlGenericControl htmlAddressCityCellLabel;

            HtmlGenericControl htmlAddressCityCellContent;

            Telerik.Web.UI.RadComboBox telerikComboCity = null;


            HtmlGenericControl htmlAddressStateCellLabel;

            HtmlGenericControl htmlAddressStateCellContent;

            Telerik.Web.UI.RadComboBox telerikComboState = null;

            
            HtmlGenericControl htmlAddressZipCodeCellLabel;

            HtmlGenericControl htmlAddressZipCodeCellContent;

            Telerik.Web.UI.RadMaskedTextBox telerikInputZipCode = null;

            #endregion


            htmlAddressTable = new HtmlGenericControl ("table");

            htmlAddressTable.Attributes.Add ("style", "width: 100%; padding 4px;" + addressControl.StyleAttributeTextOnly);


            #region Address Table - Line 1

            htmlAddressLine1Row = new HtmlGenericControl ("tr");

            htmlAddressTable.Controls.Add (htmlAddressLine1Row);


            htmlAddressLine1CellLabel = new HtmlGenericControl ("td");

            htmlAddressLine1CellLabel.Attributes.Add ("style", "whitespace: nowrap");

            htmlAddressLine1CellLabel.InnerText = "Line 1: ";

            htmlAddressLine1Row.Controls.Add (htmlAddressLine1CellLabel);


            htmlAddressLine1CellContent = new HtmlGenericControl ("td");

            htmlAddressLine1CellContent.Attributes.Add ("colspan", "5");

            htmlAddressLine1Row.Controls.Add (htmlAddressLine1CellContent);            

            #endregion 


            #region Address Table - Line 2

            htmlAddressLine2Row = new HtmlGenericControl ("tr");

            htmlAddressTable.Controls.Add (htmlAddressLine2Row);


            htmlAddressLine2CellLabel = new HtmlGenericControl ("td");

            htmlAddressLine2CellLabel.Attributes.Add ("style", "whitespace: nowrap");

            htmlAddressLine2CellLabel.InnerText = "Line 2: ";

            htmlAddressLine2Row.Controls.Add (htmlAddressLine2CellLabel);


            htmlAddressLine2CellContent = new HtmlGenericControl ("td");

            htmlAddressLine2CellContent.Attributes.Add ("colspan", "5");

            htmlAddressLine2Row.Controls.Add (htmlAddressLine2CellContent);

            #endregion


            #region Address Table - City, State Zip Code

            htmlAddressCityStateZipRow = new HtmlGenericControl ("tr");

            htmlAddressTable.Controls.Add (htmlAddressCityStateZipRow);


            htmlAddressZipCodeCellLabel = new HtmlGenericControl ("td");

            htmlAddressZipCodeCellLabel.Attributes.Add ("style", "width: 15%; whitespace: nowrap");

            htmlAddressZipCodeCellLabel.InnerText = "Zip: ";

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressZipCodeCellLabel);


            htmlAddressZipCodeCellContent = new HtmlGenericControl ("td");

            htmlAddressZipCodeCellContent.Attributes.Add ("style", "width: 10%");

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressZipCodeCellContent);


            htmlAddressCityCellLabel = new HtmlGenericControl ("td");

            htmlAddressCityCellLabel.Attributes.Add ("style", "width: 15%; whitespace: nowrap");

            htmlAddressCityCellLabel.InnerText = "City: ";

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressCityCellLabel);


            htmlAddressCityCellContent = new HtmlGenericControl ("td");

            htmlAddressCityCellContent.Attributes.Add ("style", "width: 35%");

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressCityCellContent);


            htmlAddressStateCellLabel = new HtmlGenericControl ("td");

            htmlAddressStateCellLabel.Attributes.Add ("style", "width: 15%; whitespace: nowrap");

            htmlAddressStateCellLabel.InnerText = "State: ";

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressStateCellLabel);


            htmlAddressStateCellContent = new HtmlGenericControl ("td");

            htmlAddressStateCellContent.Attributes.Add ("style", "width: 10%");

            htmlAddressCityStateZipRow.Controls.Add (htmlAddressStateCellContent);

            #endregion

            if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor)) {

                #region Address Line 1

                telerikInputLine1 = new Telerik.Web.UI.RadTextBox ();

                telerikInputLine1.ID = addressControl.Name + "_" + addressControl.ControlId.ToString () + "_Line1";

                ((Telerik.Web.UI.RadTextBox) telerikInputLine1).TextMode = Telerik.Web.UI.InputMode.SingleLine;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine1).Width = new Unit (99, UnitType.Percentage);

                ((Telerik.Web.UI.RadTextBox) telerikInputLine1).Rows = 1;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine1).Wrap = false;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine1).MaxLength = 60;

                telerikInputLine1.ReadOnly = ((renderMode != RenderEngineMode.Editor) || (addressControl.ReadOnly) || (!addressControl.Enabled));

                telerikInputLine1.Enabled = addressControl.Enabled;

                telerikInputLine1.Text = addressControl.Line1;

                if (addressControl.TabIndex > 0) { telerikInputLine1.TabIndex = (Int16) ((addressControl.TabIndex * 10) + 1); }

                else { telerikInputLine1.TabIndex = (Int16) addressControl.TabIndex; }

                if (renderMode == RenderEngineMode.Editor) {

                    telerikInputLine1.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                }

                htmlAddressLine1CellContent.Controls.Add (telerikInputLine1);

                #endregion 


                #region Address Line 2

                telerikInputLine2 = new Telerik.Web.UI.RadTextBox ();

                telerikInputLine2.ID = addressControl.Name + "_" + addressControl.ControlId.ToString () + "_Line2";

                ((Telerik.Web.UI.RadTextBox) telerikInputLine2).TextMode = Telerik.Web.UI.InputMode.SingleLine;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine2).Width = new Unit (99, UnitType.Percentage);

                ((Telerik.Web.UI.RadTextBox) telerikInputLine2).Rows = 1;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine2).Wrap = false;

                ((Telerik.Web.UI.RadTextBox) telerikInputLine2).MaxLength = 60;

                telerikInputLine2.ReadOnly = ((renderMode != RenderEngineMode.Editor) || (addressControl.ReadOnly) || (!addressControl.Enabled));

                telerikInputLine2.Enabled = addressControl.Enabled;

                telerikInputLine2.Text = addressControl.Line2;

                if (addressControl.TabIndex > 0) { telerikInputLine2.TabIndex = (Int16) ((addressControl.TabIndex * 10) + 2); }

                else { telerikInputLine2.TabIndex = (Int16) addressControl.TabIndex; }

                if (renderMode == RenderEngineMode.Editor) {

                    telerikInputLine2.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                }

                htmlAddressLine2CellContent.Controls.Add (telerikInputLine2);

                #endregion 


                #region Address Zip Code

                telerikInputZipCode = new Telerik.Web.UI.RadMaskedTextBox ();

                telerikInputZipCode.ID = addressControl.Name + "_ZipCode_" + addressControl.ControlId.ToString ();

                telerikInputZipCode.TextMode = Telerik.Web.UI.InputMode.SingleLine;

                telerikInputZipCode.Width = new Unit (99, UnitType.Percentage);

                telerikInputZipCode.Rows = 1;

                telerikInputZipCode.Wrap = false;

                telerikInputZipCode.MaxLength = 9;

                telerikInputZipCode.DisplayPromptChar = " ";

                telerikInputZipCode.Mask = "#####";

                telerikInputZipCode.ReadOnly = ((renderMode != RenderEngineMode.Editor) || (addressControl.ReadOnly) || (!addressControl.Enabled));

                telerikInputZipCode.Enabled = addressControl.Enabled;

                if (renderMode == RenderEngineMode.Editor) {

                    telerikInputZipCode.AutoPostBack = true;

                    telerikInputZipCode.TextChanged += new EventHandler (editorPage.FormControlAddressControl_ZipCodeOnTextChanged);

                }
                
                telerikInputZipCode.Text = addressControl.ZipCode + (addressControl.ZipPlus4 + "    ").Substring (0, 4);

                if (addressControl.TabIndex > 0) { telerikInputZipCode.TabIndex = (Int16) ((addressControl.TabIndex * 10) + 3); }

                else { telerikInputZipCode.TabIndex = (Int16) addressControl.TabIndex; }

                htmlAddressZipCodeCellContent.Controls.Add (telerikInputZipCode);

                #endregion


                #region Address City

                telerikComboCity = new Telerik.Web.UI.RadComboBox ();

                telerikComboCity.ID = addressControl.Name + "_City_" + addressControl.ControlId.ToString ();

                telerikComboCity.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (addressControl.EventHandlers.Count > 0);

                telerikComboCity.AllowCustomText = true;

                telerikComboCity.MarkFirstMatch = true;

                telerikComboCity.NoWrap = true;

                telerikComboCity.MaxLength = 30;

                telerikComboCity.Width = new Unit (100, UnitType.Percentage);

                telerikComboCity.Enabled = !((renderMode != RenderEngineMode.Editor) || (addressControl.ReadOnly) || (!addressControl.Enabled));

                if (addressControl.TabIndex > 0) { telerikComboCity.TabIndex = (Int16) ((addressControl.TabIndex * 10) + 4); }

                else { telerikComboCity.TabIndex = (Int16) addressControl.TabIndex; }

                if (renderMode == RenderEngineMode.Editor) {

                    telerikComboCity.EnableLoadOnDemand = true;

                    telerikComboCity.ShowMoreResultsBox = true;

                    telerikComboCity.EnableVirtualScrolling = true;

                    telerikComboCity.ItemsRequested += new Telerik.Web.UI.RadComboBoxItemsRequestedEventHandler (editorPage.FormControlAddressControl_CityOnItemsRequested);

                    telerikComboCity.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                }
                
                telerikComboCity.Text = addressControl.City;

                telerikComboCity.SelectedValue = addressControl.City;

                htmlAddressCityCellContent.Controls.Add (telerikComboCity);

                #endregion 


                #region Address State

                telerikComboState = new Telerik.Web.UI.RadComboBox ();

                telerikComboState.ID = addressControl.Name + "_State_" + addressControl.ControlId.ToString ();

                // telerikComboState.AutoPostBack = (renderMode == RenderEngineMode.Editor) && (addressControl.EventHandlers.Count > 0);

                telerikComboState.AutoPostBack = true;

                telerikComboState.MarkFirstMatch = true;

                telerikComboState.NoWrap = true;

                telerikComboState.MaxLength = 02;

                telerikComboState.Width = new Unit (100, UnitType.Percentage);

                telerikComboState.Enabled = !((renderMode != RenderEngineMode.Editor) || (addressControl.ReadOnly) || (!addressControl.Enabled));

                if (addressControl.TabIndex > 0) { telerikComboState.TabIndex = (Int16) ((addressControl.TabIndex * 10) + 5); }

                else { telerikComboState.TabIndex = (Int16) addressControl.TabIndex; }

                if (renderMode == RenderEngineMode.Editor) {

                    telerikComboState.TextChanged += new EventHandler (editorPage.FormControlAddressControl_StateOnTextChanged);

                    telerikComboState.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Select", String.Empty));

                    foreach (String currentState in MercuryApplication.StateReference (true)) {

                        telerikComboState.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentState, currentState));

                    }

                }
                
                telerikComboState.Text = addressControl.State;

                telerikComboState.SelectedValue = addressControl.State;

                htmlAddressStateCellContent.Controls.Add (telerikComboState);

                #endregion 

            }

            else if (renderMode == RenderEngineMode.Viewer) {

                htmlAddressLine1CellContent.InnerText = addressControl.Line1;

                htmlAddressStateCellContent.InnerText = addressControl.Line2;

                htmlAddressCityCellContent.InnerText = addressControl.City;

                htmlAddressStateCellContent.InnerText = addressControl.State;

                htmlAddressZipCodeCellContent.InnerText = addressControl.ZipCode;

            }

            controlDiv.Controls.Add (htmlAddressTable);

            contentDiv.Controls.Add (controlDiv);

            controlContainerDiv.Controls.Add (contentDiv);
           
            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Service (Mercury.Client.Core.Forms.Controls.Service serviceControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + serviceControl.ControlId.ToString () + "_Content", serviceControl.StyleAttribute);


            Client.Core.MedicalServices.Service medicalService = application.MedicalServiceGet (serviceControl.ServiceId, true);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + serviceControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Service");

            controlContainerDiv.Attributes.Add ("formControlName", serviceControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (serviceControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/Service.png", serviceControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }


            HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);

            #region Designer/Editor Control Render

            if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor) || (renderMode == RenderEngineMode.Viewer)) {

                HtmlGenericControl htmlTable;

                HtmlGenericControl htmlTableRow;

                HtmlGenericControl htmlTableCell;

                Int32 CellWidthPercent = 100;


                htmlTable = new HtmlGenericControl ("table");

                htmlTable.Attributes.Add ("style", "width: 100%; padding 4px;" + serviceControl.StyleAttributeTextOnly);

                htmlTableRow = new HtmlGenericControl ("tr");

                htmlTable.Controls.Add (htmlTableRow);


                CellWidthPercent = 100 - (30 * (Convert.ToInt32 (serviceControl.MostRecentMemberServiceDateVisible))) - (30 * (Convert.ToInt32 (serviceControl.ServiceDateVisible)));

                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", CellWidthPercent.ToString () + "%");

                htmlTableCell.Attributes.Add ("style", "text-align: left;");

                htmlTableCell.InnerHtml = (medicalService != null) ? medicalService.Name : "** No Service Available";

                htmlTableRow.Controls.Add (htmlTableCell);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.InnerText = "Last Date:";

                htmlTableCell.Attributes.Add ("width", "15%");

                if (!serviceControl.MostRecentMemberServiceDateVisible) { htmlTableCell.Attributes.Add ("style", "display: none;"); }

                htmlTableRow.Controls.Add (htmlTableCell);

                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableCell.InnerText = (serviceControl.MostRecentMemberServiceDate.HasValue) ? serviceControl.MostRecentMemberServiceDate.Value.ToString ("MM/dd/yyyy") : "< never >";

                if (!serviceControl.MostRecentMemberServiceDateVisible) { htmlTableCell.Attributes.Add ("style", "display: none;"); }

                htmlTableRow.Controls.Add (htmlTableCell);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableCell.InnerText = "Service Date:";

                if (!serviceControl.ServiceDateVisible) { htmlTableCell.Attributes.Add ("style", "display: none;"); }

                htmlTableRow.Controls.Add (htmlTableCell);

                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", "15%");

                if (!serviceControl.ServiceDateVisible) { htmlTableCell.Attributes.Add ("style", "display: none;"); }

                htmlTableRow.Controls.Add (htmlTableCell);

                Telerik.Web.UI.RadDateInput inputServiceDate = new Telerik.Web.UI.RadDateInput ();

                inputServiceDate.ID = serviceControl.Name + "_ServiceDate_" + serviceControl.ControlId.ToString ();

                inputServiceDate.Width = new Unit (75, UnitType.Pixel);

                inputServiceDate.EmptyMessage = (serviceControl.Required) ? "(required)" : "(optional)";

                inputServiceDate.MaxLength = 10;

                inputServiceDate.Enabled = (serviceControl.Enabled) && (!serviceControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer);

                if (serviceControl.TabIndex > 0) { inputServiceDate.TabIndex = (Int16)((serviceControl.TabIndex * 10) + 1); }

                else { inputServiceDate.TabIndex = (Int16)serviceControl.TabIndex; }

                inputServiceDate.ReadOnly = serviceControl.ReadOnly;

                inputServiceDate.AutoPostBack = ((renderMode == RenderEngineMode.Editor) && (serviceControl.EventHandlers.Count > 0));

                if (renderMode == RenderEngineMode.Editor) {

                    inputServiceDate.TextChanged += new EventHandler (editorPage.FormControlServiceControl_ServiceDateOnTextChanged);

                }

                inputServiceDate.SelectedDate = serviceControl.ServiceDate;

                htmlTableCell.Controls.Add (inputServiceDate);

                contentControlDiv.Controls.Add (htmlTable);

            } // if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor)) { 

            #endregion


            #region Label

            if (!serviceControl.Label.Visible) {

                contentDiv.Controls.Add (contentControlDiv);

            }

            else {

                switch (serviceControl.Label.Position) {

                    case Mercury.Server.Application.FormControlPosition.Left:

                    case Mercury.Server.Application.FormControlPosition.Right:

                        contentDiv.Controls.Add (RenderControl_LabelTable (serviceControl.Label, serviceControl, contentControlDiv));

                        break;

                    case Mercury.Server.Application.FormControlPosition.Top:

                        contentDiv.Controls.Add (RenderControl_Label (serviceControl.Label, serviceControl));

                        contentDiv.Controls.Add (contentControlDiv);

                        break;

                    case Mercury.Server.Application.FormControlPosition.Bottom:

                        contentDiv.Controls.Add (contentControlDiv);

                        contentDiv.Controls.Add (RenderControl_Label (serviceControl.Label, serviceControl));

                        break;

                }

            }

            #endregion

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }

        public HtmlGenericControl RenderControl_Metric (Mercury.Client.Core.Forms.Controls.Metric metricControl) {

            HtmlGenericControl controlContainerDiv;

            HtmlGenericControl contentDiv = HtmlElementDiv ("ControlId_" + metricControl.ControlId.ToString () + "_Content", metricControl.StyleAttribute);


            Client.Core.Metrics.Metric metric = application.MetricGet (metricControl.MetricId, true);


            controlContainerDiv = HtmlElementDiv ("ControlId_" + metricControl.ControlId.ToString (), String.Empty);

            controlContainerDiv.Attributes.Add ("class", "FormControl");

            controlContainerDiv.Attributes.Add ("formControlType", "Metric");

            controlContainerDiv.Attributes.Add ("formControlName", metricControl.Name);

            controlContainerDiv.Attributes.Add ("style", "display: " + (((renderMode == RenderEngineMode.Designer) || (metricControl.Visible)) ? "block;" : "none;"));


            if (renderMode == RenderEngineMode.Designer) {

                HtmlGenericControl titleBar = RenderControl_TitleBar ("/Images/Common16/Metric.png", metricControl.Name);

                controlContainerDiv.Controls.Add (titleBar);

            }


            HtmlGenericControl contentControlDiv = HtmlElementDiv (String.Empty, String.Empty);

            #region Designer/Editor Control Render

            if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor)) {


                HtmlGenericControl htmlTable;

                HtmlGenericControl htmlTableRow;

                HtmlGenericControl htmlTableCell;


                htmlTable = new HtmlGenericControl ("table");

                htmlTable.Attributes.Add ("style", "width: 100%; padding 4px;" + metricControl.StyleAttributeTextOnly);

                htmlTableRow = new HtmlGenericControl ("tr");

                htmlTable.Controls.Add (htmlTableRow);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("style", "text-align: left;");

                htmlTableCell.InnerHtml = (metric != null) ? metric.Name : "** No Metric Available";

                htmlTableRow.Controls.Add (htmlTableCell);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.InnerText = "Metric Date:";

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableRow.Controls.Add (htmlTableCell);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableRow.Controls.Add (htmlTableCell);

                Telerik.Web.UI.RadDateInput inputMetricServiceDate = new Telerik.Web.UI.RadDateInput ();

                inputMetricServiceDate.ID = metricControl.Name + "_" + metricControl.ControlId.ToString () + "_MetricDate";

                inputMetricServiceDate.Width = new Unit (75, UnitType.Pixel);

                inputMetricServiceDate.EmptyMessage = (metricControl.Required) ? "(required)" : "(optional)";

                inputMetricServiceDate.MaxLength = 10;

                inputMetricServiceDate.ReadOnly = metricControl.ReadOnly;

                inputMetricServiceDate.Enabled = metricControl.Enabled;

                if (metricControl.TabIndex > 0) { inputMetricServiceDate.TabIndex = (Int16)((metricControl.TabIndex * 10) + 1); }

                else { inputMetricServiceDate.TabIndex = (Int16)metricControl.TabIndex; }

                inputMetricServiceDate.AutoPostBack = ((renderMode == RenderEngineMode.Editor) && (metricControl.EventHandlers.Count > 0));

                if (renderMode == RenderEngineMode.Editor) {

                    inputMetricServiceDate.TextChanged += new EventHandler (editorPage.FormControlMetricControl_MetricDateOnTextChanged);

                }

                inputMetricServiceDate.SelectedDate = metricControl.MetricDate;

                htmlTableCell.Controls.Add (inputMetricServiceDate);

                contentControlDiv.Controls.Add (htmlTable);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.InnerText = "Value:";

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableRow.Controls.Add (htmlTableCell);


                htmlTableCell = new HtmlGenericControl ("td");

                htmlTableCell.Attributes.Add ("width", "15%");

                htmlTableRow.Controls.Add (htmlTableCell);

                Telerik.Web.UI.RadNumericTextBox inputMetricValue = new Telerik.Web.UI.RadNumericTextBox ();

                inputMetricValue.ID = metricControl.Name + "_" + metricControl.ControlId.ToString () + "_MetricValue";

                inputMetricValue.Width = new Unit (75, UnitType.Pixel);

                inputMetricValue.EmptyMessage = (metricControl.Required) ? "(required)" : "(optional)";

                inputMetricValue.MaxLength = 10;

                inputMetricValue.ReadOnly = metricControl.ReadOnly;

                inputMetricValue.Enabled = metricControl.Enabled;

                if (metricControl.TabIndex > 0) { inputMetricValue.TabIndex = (Int16)((metricControl.TabIndex * 10) + 2); }

                else { inputMetricValue.TabIndex = (Int16)metricControl.TabIndex; }

                if (metric != null) {

                    inputMetricValue.MinValue = Convert.ToDouble (metric.MinimumValue);

                    inputMetricValue.MaxValue = Convert.ToDouble (metric.MaximumValue);

                }

                inputMetricValue.Value = Convert.ToDouble (metricControl.MetricValue);

                inputMetricValue.AutoPostBack = ((renderMode == RenderEngineMode.Editor) && (metricControl.EventHandlers.Count > 0));

                if (renderMode == RenderEngineMode.Editor) {

                    inputMetricValue.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                }


                htmlTableCell.Controls.Add (inputMetricValue);

                contentControlDiv.Controls.Add (htmlTable);


                //                    #region Designer/Editor DropDownList

                //                    Telerik.Web.UI.RadComboBox metricComboBox = new Telerik.Web.UI.RadComboBox ();

                //                    metricComboBox.ID = metricControl.Name + "_" + metricControl.ControlId.ToString ();

                //                    metricComboBox.EnableViewState = (renderMode == RenderEngineMode.Editor);

                //                    metricComboBox.AutoPostBack = (renderMode == RenderEngineMode.Editor);

                //                    metricComboBox.MarkFirstMatch = true;

                //                    //metricComboBox.NoWrap = !metricControl.Wrap;

                //                    //metricComboBox.MaxLength = metricControl.MaxLength;

                //                    metricComboBox.Width = new Unit (100, UnitType.Percentage);

                //                    metricComboBox.Skin = "Vista";


                //                    Client.Core.MedicalServices.Metric metric = application.MedicalMetricGet (metricControl.MetricId);

                //                    if (metric != null) { 

                //                        metricComboBox.Items.Add (new Telerik.Web.UI.RadComboBoxItem (metric.Name, metric.MetricId.ToString ()));

                //                        metricComboBox.SelectedValue = metricControl.MetricId.ToString ();

                //                    }

                //                    else {

                //                        metricComboBox.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** Must Set Properties", "0"));

                //                    }

                ///*
                //                    else if ((renderMode == RenderEngineMode.Editor) && (selectionControl.DataSource == Mercury.Server.Application.DataSource.Reference)) {

                //                        selectionComboBox.EnableLoadOnDemand = true;

                //                        selectionComboBox.ShowMoreResultsBox = true;

                //                        selectionComboBox.EnableVirtualScrolling = true;

                //                        selectionComboBox.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                //                        selectionComboBox.ItemsRequested += new Telerik.Web.UI.RadComboBoxItemsRequestedEventHandler (editorPage.FormControlSelectionControl_OnItemsRequested);

                //                        selectionComboBox.SelectedValue = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Value : String.Empty;

                //                        selectionComboBox.Text = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Text : String.Empty;

                //                    }
                //*/

                //                    contentControlDiv.Controls.Add (metricComboBox);

                //                    #endregion

            } // if ((renderMode == RenderEngineMode.Designer) || (renderMode == RenderEngineMode.Editor)) { 

            #endregion


            if (!metricControl.Label.Visible) {

                contentDiv.Controls.Add (contentControlDiv);

            }

            else {

                switch (metricControl.Label.Position) {

                    case Mercury.Server.Application.FormControlPosition.Left:

                    case Mercury.Server.Application.FormControlPosition.Right:

                        contentDiv.Controls.Add (RenderControl_LabelTable (metricControl.Label, metricControl, contentControlDiv));

                        break;

                    case Mercury.Server.Application.FormControlPosition.Top:

                        contentDiv.Controls.Add (RenderControl_Label (metricControl.Label, metricControl));

                        contentDiv.Controls.Add (contentControlDiv);

                        break;

                    case Mercury.Server.Application.FormControlPosition.Bottom:

                        contentDiv.Controls.Add (contentControlDiv);

                        contentDiv.Controls.Add (RenderControl_Label (metricControl.Label, metricControl));

                        break;

                }

            }

            controlContainerDiv.Controls.Add (contentDiv);

            return controlContainerDiv;

        }        

        public void RenderFormControl (System.Web.UI.Control parentControl, Mercury.Client.Core.Forms.Control control) {

            HtmlGenericControl controlContent = null;

            DateTime startTime = DateTime.Now;

            switch (control.ControlType) {

                case Mercury.Server.Application.FormControlType.Section:

                    controlContent = RenderControl_Section ((Mercury.Client.Core.Forms.Controls.Section) control);

                    break;

                case Mercury.Server.Application.FormControlType.SectionColumn:

                    controlContent = RenderControl_SectionColumn ((Mercury.Client.Core.Forms.Controls.SectionColumn) control);

                    break;

                case Mercury.Server.Application.FormControlType.Text:

                    controlContent = RenderControl_Text ((Mercury.Client.Core.Forms.Controls.Text) control);

                    break;

                case Mercury.Server.Application.FormControlType.Input:

                    controlContent = RenderControl_Input ((Mercury.Client.Core.Forms.Controls.Input) control);

                    break;

                case Mercury.Server.Application.FormControlType.Selection:

                    controlContent = RenderControl_Selection ((Mercury.Client.Core.Forms.Controls.Selection) control);

                    break;

                case Mercury.Server.Application.FormControlType.Button:

                    controlContent = RenderControl_Button ((Mercury.Client.Core.Forms.Controls.Button) control);

                    break;

                case Mercury.Server.Application.FormControlType.Entity:

                    controlContent = RenderControl_Entity ((Mercury.Client.Core.Forms.Controls.Entity) control);

                    break;

                case Mercury.Server.Application.FormControlType.Collection:

                    controlContent = RenderControl_Collection ((Mercury.Client.Core.Forms.Controls.Collection) control);

                    break;


                case Mercury.Server.Application.FormControlType.Address:

                    controlContent = RenderControl_Address ((Mercury.Client.Core.Forms.Controls.Address) control);

                    break;

                case Mercury.Server.Application.FormControlType.Service:

                    controlContent = RenderControl_Service ((Mercury.Client.Core.Forms.Controls.Service) control);

                    break;

                case Mercury.Server.Application.FormControlType.Metric:

                    controlContent = RenderControl_Metric ((Mercury.Client.Core.Forms.Controls.Metric) control);

                    break;

                default:

                    Mercury.Client.Core.Forms.Controls.Label labelUnsupportedControl = new Mercury.Client.Core.Forms.Controls.Label (application);

                    labelUnsupportedControl.Text = "Not Implemented (" + control.GetType ().ToString () + ").";

                    controlContent = RenderControl_Label (labelUnsupportedControl, control);

                    System.Diagnostics.Debug.WriteLine ("Not Implemented (" + control.GetType ().ToString () + ").");

                    break;

            }

            if (controlContent != null) { parentControl.Controls.Add (controlContent); }

            //if ((DateTime.Now.Subtract (startTime).TotalMilliseconds > 0) 
                
            //    && (control.ControlType != Mercury.Server.Application.FormControlType.Section) 
            
            //    && (control.ControlType != Mercury.Server.Application.FormControlType.SectionColumn)) {

            //    System.Diagnostics.Debug.WriteLine ("Render Form Control (" + control.ControlType.ToString () + ": " + control.Name + "): " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            //}

            return;

        }

        public HtmlGenericControl RenderFormPage (Mercury.Client.Core.Forms.Form form, Int32 pageIndex) {
            
            if (form == null) { return null; }

            if (application == null) { return null; }

            if (isFormRendered) { return null; }


            DateTime startTime = DateTime.Now;

            Boolean pagingEnabled = true;

            HtmlGenericControl formControlDiv;


            if (pageIndex > form.PageCountVisible) { pageIndex = form.PageCountVisible; }

            if (pageIndex < 1) { pageIndex = 1; }


            formControlDiv = HtmlElementDiv ("ControlId_" + form.ControlId.ToString (), form.StyleAttributeTextOnly);

            if ((renderMode == RenderEngineMode.Editor) && (form.PageCountVisible > 1) && (pagingEnabled)) {

                // RENDER PAGE BREAK FORM

                foreach (Mercury.Client.Core.Forms.Controls.Section currentControl in form.PageControls (pageIndex)) {

                    RenderFormControl (formControlDiv, currentControl);

                }

            }

            else { return RenderForm (form); }// NOT IN EDITOR MODE OR NO PAGE BREAKS, RENDER FULL PAGE FORM


            isFormRendered = true;

            form.EventResults.Clear ();


            System.Diagnostics.Debug.WriteLine ("Engine Render Form: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            return formControlDiv;

        }

        public HtmlGenericControl RenderForm (Mercury.Client.Core.Forms.Form form) {

            if (form == null) { return null; }

            if (application == null) { return null; }

            if (isFormRendered) { return null; }


            DateTime startTime = DateTime.Now;


            HtmlGenericControl formControlDiv;

            Int32 dropZonePosition = 0;

            HtmlGenericControl dropZone;


            formControlDiv = HtmlElementDiv ("ControlId_" + form.ControlId.ToString (), form.StyleAttributeTextOnly);

            
            if (renderMode == RenderEngineMode.Designer) {

                dropZone = RenderControl_DropZone (form, dropZonePosition);

                formControlDiv.Controls.Add (dropZone);

            }


            foreach (Mercury.Client.Core.Forms.Control currentControl in form.Controls) {

                RenderFormControl (formControlDiv, currentControl);

                if (renderMode == RenderEngineMode.Designer) {

                    dropZonePosition = dropZonePosition + 1;

                    dropZone = RenderControl_DropZone (form, dropZonePosition);

                    formControlDiv.Controls.Add (dropZone);

                }

            }


            isFormRendered = true;

            form.EventResults.Clear ();


            System.Diagnostics.Debug.WriteLine ("Engine Render Form: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());


            return formControlDiv;

        }

        #endregion

    }

    public enum RenderEngineMode { Designer, Editor, Viewer }

}