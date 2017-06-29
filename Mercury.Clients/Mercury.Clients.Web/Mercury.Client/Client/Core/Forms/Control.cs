using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Client.Core.Forms {

    [Serializable]
    public class Control : CoreObject {

        #region Private Properties

        protected Guid controlId = Guid.NewGuid ();

        protected Server.Application.FormControlType controlType = Server.Application.FormControlType.Undefined;

        protected Int16 tabIndex = 0;

        protected Boolean enabled = true;

        protected Boolean visible = true;

        protected Boolean readOnly = false;

        protected Boolean required = false;

        protected Server.Application.FormControlPosition position = Server.Application.FormControlPosition.Left;

        protected Server.Application.FormControlStyle style = new Server.Application.FormControlStyle ();

        protected Server.Application.FormControlCapabilities capabilities = new Server.Application.FormControlCapabilities ();

        protected List<Server.Application.FormControlEventHandler> eventHandlers = new List<Server.Application.FormControlEventHandler> ();

        protected List<Server.Application.FormControlDataBinding> dataBindings = new List<Server.Application.FormControlDataBinding> ();


        protected Control parent = null;

        protected List<Control> controls = new List<Control> ();

        protected Controls.Label label = null;


        protected Dictionary<String, String> dataBindingContexts = null;

        protected Dictionary<String, String> bindableProperties = null;

        protected List<String> events = null;

        #endregion 
        
        
        #region Public Properties

        public Guid ControlId { get { return controlId; } set { controlId = value; } }

        public Server.Application.FormControlType ControlType { get { return controlType; } set { controlType = value; } }

        public Int16 TabIndex { get { return tabIndex; } set { tabIndex = (value > 3200) ? (Int16)3200 : value; } }

        public Boolean Enabled { get { return enabled; } set { enabled = value; } }

        public Boolean Visible { get { return visible; } set { visible = value; } }

        public Boolean ReadOnly { get { return readOnly; } set { readOnly = value; } }

        public Boolean Required { get { return required; } set { required = value; } }

        public Server.Application.FormControlPosition Position { get { return position; } set { position = value; } }

        public Server.Application.FormControlStyle Style { get { return style; } set { style = value; } }

        public Server.Application.FormControlCapabilities Capabilities { get { return capabilities; } set { capabilities = value; } }


        public virtual Boolean HasValue { get { return false; } }

        public virtual String Value { get { return String.Empty; } }


        public Control Parent { get { return parent; } set { parent = value; } }

        public List<Control> Controls { get { return controls; } set { controls = value; } }

        public Controls.Label Label { get { return label; } set { label = value; } }



        public String StyleAttribute {

            get {

                StringBuilder styleAttribute = new StringBuilder ();

                #region Font Properties

                if (!String.IsNullOrWhiteSpace (style.FontFamily)) { styleAttribute.Append ("font-family: " + style.FontFamily + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontSize)) { styleAttribute.Append ("font-size: " + style.FontSize + style.FontSizeUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontWeight)) { styleAttribute.Append ("font-weight: " + style.FontWeight + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontStyle)) { styleAttribute.Append ("font-style: " + style.FontStyle + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontVariant)) { styleAttribute.Append ("font-variant: " + style.FontVariant + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextTransform)) { styleAttribute.Append ("text-transform: " + style.TextTransform + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextDecoration)) { styleAttribute.Append ("text-decoration: " + style.TextDecoration + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Color)) { styleAttribute.Append ("color: " + style.Color + ";"); }

                if (!String.IsNullOrWhiteSpace (style.BackgroundColor)) { styleAttribute.Append ("background-color: " + style.BackgroundColor + ";"); }

                #endregion


                #region Block Properties

                if (!String.IsNullOrWhiteSpace (style.Width)) { styleAttribute.Append ("width: " + style.Width + style.WidthUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Height)) { styleAttribute.Append ("height: " + style.Height + style.HeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.LineHeight)) { styleAttribute.Append ("line-height: " + style.LineHeight + style.LineHeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.VerticalAlign)) { styleAttribute.Append ("vertical-align: " + style.VerticalAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextAlign)) { styleAttribute.Append ("text-align: " + style.TextAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextIndent)) { styleAttribute.Append ("text-indent: " + style.TextIndent + style.TextIndentUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WhiteSpace)) { styleAttribute.Append ("white-space: " + style.WhiteSpace + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WordSpacing)) { styleAttribute.Append ("word-spacing: " + style.WordSpacing + style.WordSpacingUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.LetterSpacing)) { styleAttribute.Append ("letter-spacing: " + style.LetterSpacing + style.LetterSpacingUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Overflow)) { styleAttribute.Append ("overflow: " + style.Overflow + ";"); }

                #endregion


                #region Border

                if ((style.IsBorderSame) && (!String.IsNullOrWhiteSpace (style.BorderTopStyle))) {

                    styleAttribute.Append ("border: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.BorderTopStyle)) { styleAttribute.Append ("border-top: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderLeftStyle)) { styleAttribute.Append ("border-left: " + style.BorderLeftStyle + " " + style.BorderLeftWidth + style.BorderLeftWidthUnit + " " + style.BorderLeftColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderBottomStyle)) { styleAttribute.Append ("border-bottom: " + style.BorderBottomStyle + " " + style.BorderBottomWidth + style.BorderBottomWidthUnit + " " + style.BorderBottomColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderRightStyle)) { styleAttribute.Append ("border-right: " + style.BorderRightStyle + " " + style.BorderRightWidth + style.BorderRightWidthUnit + " " + style.BorderRightColor + ";"); }

                }

                #endregion


                #region Padding and Margin

                if (!String.IsNullOrWhiteSpace (style.Margin)) {

                    styleAttribute.Append ("margin: " + style.Margin + ";");

                }

                else if ((style.IsMarginSame) && (!String.IsNullOrWhiteSpace (style.MarginTop))) {

                    styleAttribute.Append ("margin: " + style.MarginTop + style.MarginTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.MarginTop)) { styleAttribute.Append ("margin-top: " + style.MarginTop + style.MarginTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginLeft)) { styleAttribute.Append ("margin-left: " + style.MarginLeft + style.MarginLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginBottom)) { styleAttribute.Append ("margin-bottom: " + style.MarginBottom + style.MarginBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginRight)) { styleAttribute.Append ("margin-right: " + style.MarginRight + style.MarginRightUnit + ";"); }

                }


                if (!String.IsNullOrWhiteSpace (style.Padding)) {

                    styleAttribute.Append ("padding: " + style.Padding + ";");

                }

                else if ((style.IsPaddingSame) && (!String.IsNullOrWhiteSpace (style.PaddingTop))) {

                    styleAttribute.Append ("padding: " + style.PaddingTop + style.PaddingTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.PaddingTop)) { styleAttribute.Append ("padding-top: " + style.PaddingTop + style.PaddingTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingLeft)) { styleAttribute.Append ("padding-left: " + style.PaddingLeft + style.PaddingLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingBottom)) { styleAttribute.Append ("padding-bottom: " + style.PaddingBottom + style.PaddingBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingRight)) { styleAttribute.Append ("padding-right: " + style.PaddingRight + style.PaddingRightUnit + ";"); }

                }

                #endregion

                return styleAttribute.ToString ();

            }

        }

        public String StyleAttributeTextOnly {

            get {

                StringBuilder styleAttribute = new StringBuilder ();

                #region Font Properties

                if (!String.IsNullOrWhiteSpace (style.FontFamily)) { styleAttribute.Append ("font-family: " + style.FontFamily + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontSize)) { styleAttribute.Append ("font-size: " + style.FontSize + style.FontSizeUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontWeight)) { styleAttribute.Append ("font-weight: " + style.FontWeight + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontStyle)) { styleAttribute.Append ("font-style: " + style.FontStyle + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontVariant)) { styleAttribute.Append ("font-variant: " + style.FontVariant + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextTransform)) { styleAttribute.Append ("text-transform: " + style.TextTransform + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextDecoration)) { styleAttribute.Append ("text-decoration: " + style.TextDecoration + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Color)) { styleAttribute.Append ("color: " + style.Color + ";"); }

                if (!String.IsNullOrWhiteSpace (style.BackgroundColor)) { styleAttribute.Append ("background-color: " + style.BackgroundColor + ";"); }

                #endregion


                #region Block Properties

                if (!String.IsNullOrWhiteSpace (style.LineHeight)) { styleAttribute.Append ("line-height: " + style.LineHeight + style.LineHeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.VerticalAlign)) { styleAttribute.Append ("vertical-align: " + style.VerticalAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextAlign)) { styleAttribute.Append ("text-align: " + style.TextAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextIndent)) { styleAttribute.Append ("text-indent: " + style.TextIndent + style.TextIndentUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WhiteSpace)) { styleAttribute.Append ("white-space: " + style.WhiteSpace + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WordSpacing)) { styleAttribute.Append ("word-spacing: " + style.WordSpacing + style.WordSpacingUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.LetterSpacing)) { styleAttribute.Append ("letter-spacing: " + style.LetterSpacing + style.LetterSpacingUnit + ";"); }

                #endregion

                return styleAttribute.ToString ();

            }

        }

        public String StyleAttributeWithoutText {

            get {

                StringBuilder styleAttribute = new StringBuilder ();

                #region Block Properties

                if (!String.IsNullOrWhiteSpace (style.Width)) { styleAttribute.Append ("width: " + style.Width + style.WidthUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Height)) { styleAttribute.Append ("height: " + style.Height + style.HeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.VerticalAlign)) { styleAttribute.Append ("vertical-align: " + style.VerticalAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextIndent)) { styleAttribute.Append ("text-indent: " + style.TextIndent + style.TextIndentUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Overflow)) { styleAttribute.Append ("overflow: " + style.Overflow + ";"); }

                #endregion


                #region Border

                if ((style.IsBorderSame) && (!String.IsNullOrWhiteSpace (style.BorderTopStyle))) {

                    styleAttribute.Append ("border: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.BorderTopStyle)) { styleAttribute.Append ("border-top: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderLeftStyle)) { styleAttribute.Append ("border-left: " + style.BorderLeftStyle + " " + style.BorderLeftWidth + style.BorderLeftWidthUnit + " " + style.BorderLeftColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderBottomStyle)) { styleAttribute.Append ("border-bottom: " + style.BorderBottomStyle + " " + style.BorderBottomWidth + style.BorderBottomWidthUnit + " " + style.BorderBottomColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderRightStyle)) { styleAttribute.Append ("border-right: " + style.BorderRightStyle + " " + style.BorderRightWidth + style.BorderRightWidthUnit + " " + style.BorderRightColor + ";"); }

                }

                #endregion


                #region Padding and Margin

                if (!String.IsNullOrWhiteSpace (style.Margin)) {

                    styleAttribute.Append ("margin: " + style.Margin + ";");

                }

                else if ((style.IsMarginSame) && (!String.IsNullOrWhiteSpace (style.MarginTop))) {

                    styleAttribute.Append ("margin: " + style.MarginTop + style.MarginTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.MarginTop)) { styleAttribute.Append ("margin-top: " + style.MarginTop + style.MarginTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginLeft)) { styleAttribute.Append ("margin-left: " + style.MarginLeft + style.MarginLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginBottom)) { styleAttribute.Append ("margin-bottom: " + style.MarginBottom + style.MarginBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginRight)) { styleAttribute.Append ("margin-right: " + style.MarginRight + style.MarginRightUnit + ";"); }

                }


                if (!String.IsNullOrWhiteSpace (style.Padding)) {

                    styleAttribute.Append ("padding: " + style.Padding + ";");

                }

                else if ((style.IsPaddingSame) && (!String.IsNullOrWhiteSpace (style.PaddingTop))) {

                    styleAttribute.Append ("padding: " + style.PaddingTop + style.PaddingTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.PaddingTop)) { styleAttribute.Append ("padding-top: " + style.PaddingTop + style.PaddingTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingLeft)) { styleAttribute.Append ("padding-left: " + style.PaddingLeft + style.PaddingLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingBottom)) { styleAttribute.Append ("padding-bottom: " + style.PaddingBottom + style.PaddingBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingRight)) { styleAttribute.Append ("padding-right: " + style.PaddingRight + style.PaddingRightUnit + ";"); }

                }

                #endregion

                return styleAttribute.ToString ();

            }

        }

        public String StyleAttributeWithoutSizeWidth {

            get {

                StringBuilder styleAttribute = new StringBuilder ();

                #region Font Properties

                if (!String.IsNullOrWhiteSpace (style.FontFamily)) { styleAttribute.Append ("font-family: " + style.FontFamily + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontSize)) { styleAttribute.Append ("font-size: " + style.FontSize + style.FontSizeUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontWeight)) { styleAttribute.Append ("font-weight: " + style.FontWeight + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontStyle)) { styleAttribute.Append ("font-style: " + style.FontStyle + ";"); }

                if (!String.IsNullOrWhiteSpace (style.FontVariant)) { styleAttribute.Append ("font-variant: " + style.FontVariant + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextTransform)) { styleAttribute.Append ("text-transform: " + style.TextTransform + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextDecoration)) { styleAttribute.Append ("text-decoration: " + style.TextDecoration + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Color)) { styleAttribute.Append ("color: " + style.Color + ";"); }

                if (!String.IsNullOrWhiteSpace (style.BackgroundColor)) { styleAttribute.Append ("background-color: " + style.BackgroundColor + ";"); }

                #endregion


                #region Block Properties

                if (!String.IsNullOrWhiteSpace (style.Height)) { styleAttribute.Append ("height: " + style.Height + style.HeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.LineHeight)) { styleAttribute.Append ("line-height: " + style.LineHeight + style.LineHeightUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.VerticalAlign)) { styleAttribute.Append ("vertical-align: " + style.VerticalAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextAlign)) { styleAttribute.Append ("text-align: " + style.TextAlign + ";"); }

                if (!String.IsNullOrWhiteSpace (style.TextIndent)) { styleAttribute.Append ("text-indent: " + style.TextIndent + style.TextIndentUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WhiteSpace)) { styleAttribute.Append ("white-space: " + style.WhiteSpace + ";"); }

                if (!String.IsNullOrWhiteSpace (style.WordSpacing)) { styleAttribute.Append ("word-spacing: " + style.WordSpacing + style.WordSpacingUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.LetterSpacing)) { styleAttribute.Append ("letter-spacing: " + style.LetterSpacing + style.LetterSpacingUnit + ";"); }

                if (!String.IsNullOrWhiteSpace (style.Overflow)) { styleAttribute.Append ("overflow: " + style.Overflow + ";"); }

                #endregion


                #region Border

                if ((style.IsBorderSame) && (!String.IsNullOrWhiteSpace (style.BorderTopStyle))) {

                    styleAttribute.Append ("border: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.BorderTopStyle)) { styleAttribute.Append ("border-top: " + style.BorderTopStyle + " " + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderLeftStyle)) { styleAttribute.Append ("border-left: " + style.BorderLeftStyle + " " + style.BorderLeftWidth + style.BorderLeftWidthUnit + " " + style.BorderLeftColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderBottomStyle)) { styleAttribute.Append ("border-bottom: " + style.BorderBottomStyle + " " + style.BorderBottomWidth + style.BorderBottomWidthUnit + " " + style.BorderBottomColor + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.BorderRightStyle)) { styleAttribute.Append ("border-right: " + style.BorderRightStyle + " " + style.BorderRightWidth + style.BorderRightWidthUnit + " " + style.BorderRightColor + ";"); }

                }

                #endregion


                #region Padding and Margin

                if (!String.IsNullOrWhiteSpace (style.Margin)) {

                    styleAttribute.Append ("margin: " + style.Margin + ";");

                }

                else if ((style.IsMarginSame) && (!String.IsNullOrWhiteSpace (style.MarginTop))) {

                    styleAttribute.Append ("margin: " + style.MarginTop + style.MarginTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.MarginTop)) { styleAttribute.Append ("margin-top: " + style.MarginTop + style.MarginTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginLeft)) { styleAttribute.Append ("margin-left: " + style.MarginLeft + style.MarginLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginBottom)) { styleAttribute.Append ("margin-bottom: " + style.MarginBottom + style.MarginBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.MarginRight)) { styleAttribute.Append ("margin-right: " + style.MarginRight + style.MarginRightUnit + ";"); }

                }


                if (!String.IsNullOrWhiteSpace (style.Padding)) {

                    styleAttribute.Append ("padding: " + style.Padding + ";");

                }

                else if ((style.IsPaddingSame) && (!String.IsNullOrWhiteSpace (style.PaddingTop))) {

                    styleAttribute.Append ("padding: " + style.PaddingTop + style.PaddingTopUnit + ";");

                }

                else {

                    if (!String.IsNullOrWhiteSpace (style.PaddingTop)) { styleAttribute.Append ("padding-top: " + style.PaddingTop + style.PaddingTopUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingLeft)) { styleAttribute.Append ("padding-left: " + style.PaddingLeft + style.PaddingLeftUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingBottom)) { styleAttribute.Append ("padding-bottom: " + style.PaddingBottom + style.PaddingBottomUnit + ";"); }

                    if (!String.IsNullOrWhiteSpace (style.PaddingRight)) { styleAttribute.Append ("padding-right: " + style.PaddingRight + style.PaddingRightUnit + ";"); }

                }

                #endregion

                return styleAttribute.ToString ();

            }

        }

        public Server.Application.FormControlStyle CopyStyle {

            get {

                Server.Application.FormControlStyle copiedStyle = new Server.Application.FormControlStyle ();

                copiedStyle.BackgroundColor = style.BackgroundColor;

                copiedStyle.BorderBottomColor = style.BorderBottomColor;

                copiedStyle.BorderBottomStyle = style.BorderBottomStyle;

                copiedStyle.BorderBottomWidth = style.BorderBottomWidth;

                copiedStyle.BorderBottomWidthUnit = style.BorderBottomWidthUnit;

                copiedStyle.BorderLeftColor = style.BorderLeftColor;

                copiedStyle.BorderLeftStyle = style.BorderLeftStyle;

                copiedStyle.BorderLeftWidth = style.BorderLeftWidth;

                copiedStyle.BorderLeftWidthUnit = style.BorderLeftWidthUnit;

                copiedStyle.BorderRightColor = style.BorderRightColor;

                copiedStyle.BorderRightStyle = style.BorderRightStyle;

                copiedStyle.BorderRightWidth = style.BorderRightWidth;

                copiedStyle.BorderRightWidthUnit = style.BorderRightWidthUnit;

                copiedStyle.BorderTopColor = style.BorderTopColor;

                copiedStyle.BorderTopStyle = style.BorderTopStyle;

                copiedStyle.BorderTopWidth = style.BorderTopWidth;

                copiedStyle.BorderTopWidthUnit = style.BorderTopWidthUnit;

                copiedStyle.Color = style.Color;

                copiedStyle.FontFamily = style.FontFamily;

                copiedStyle.FontSize = style.FontSize;

                copiedStyle.FontSizeUnit = style.FontSizeUnit;

                copiedStyle.FontStyle = style.FontStyle;

                copiedStyle.FontVariant = style.FontVariant;

                copiedStyle.FontWeight = style.FontWeight;

                copiedStyle.Height = style.Height;

                copiedStyle.HeightUnit = style.HeightUnit;

                copiedStyle.IsBorderSame = style.IsBorderSame;

                copiedStyle.IsMarginSame = style.IsMarginSame;

                copiedStyle.IsPaddingSame = style.IsPaddingSame;

                copiedStyle.LetterSpacing = style.LetterSpacing;

                copiedStyle.LetterSpacingUnit = style.LetterSpacingUnit;

                copiedStyle.LineHeight = style.LineHeight;

                copiedStyle.LineHeightUnit = style.LineHeightUnit;


                copiedStyle.Margin = style.Margin;

                copiedStyle.MarginBottom = style.MarginBottom;

                copiedStyle.MarginBottomUnit = style.MarginBottomUnit;

                copiedStyle.MarginLeft = style.MarginLeft;

                copiedStyle.MarginLeftUnit = style.MarginLeftUnit;

                copiedStyle.MarginRight = style.MarginRight;

                copiedStyle.MarginRightUnit = style.MarginRightUnit;

                copiedStyle.MarginTop = style.MarginTop;

                copiedStyle.MarginTopUnit = style.MarginTopUnit;

                copiedStyle.Overflow = style.Overflow;


                copiedStyle.Padding = style.Padding;

                copiedStyle.PaddingBottom = style.PaddingBottom;

                copiedStyle.PaddingBottomUnit = style.PaddingBottomUnit;

                copiedStyle.PaddingLeft = style.PaddingLeft;

                copiedStyle.PaddingLeftUnit = style.PaddingLeftUnit;

                copiedStyle.PaddingRight = style.PaddingRight;

                copiedStyle.PaddingRightUnit = style.PaddingRightUnit;

                copiedStyle.PaddingTop = style.PaddingTop;

                copiedStyle.PaddingTopUnit = style.PaddingTopUnit;

                copiedStyle.TextAlign = style.TextAlign;

                copiedStyle.TextDecoration = style.TextDecoration;

                copiedStyle.TextIndent = style.TextIndent;

                copiedStyle.TextIndentUnit = style.TextIndentUnit;

                copiedStyle.TextTransform = style.TextTransform;

                copiedStyle.VerticalAlign = style.VerticalAlign;

                copiedStyle.WhiteSpace = style.WhiteSpace;

                copiedStyle.Width = style.Width;

                copiedStyle.WidthUnit = style.WidthUnit;

                copiedStyle.WordSpacing = style.WordSpacing;

                copiedStyle.WordSpacingUnit = style.WordSpacingUnit;

                return copiedStyle;

            }

        }

        public override Mercury.Client.Application Application {

            set {

                application = value;

                if (Label != null) { Label.Application = application; }

                foreach (Control currentChildControl in controls) {

                    currentChildControl.Application = value;

                }

            }

        }


        public virtual String JsonExtendedProperties { get { return String.Empty; } }

        public virtual String Json {

            get {

                StringBuilder jsonBuilder = new StringBuilder ();


                jsonBuilder.Append ("{\"ControlId\":\"" + ControlId.ToString ().Replace ("-", "_") + "\"");

                jsonBuilder.Append (",\"Type\":\"" + controlType.ToString () + "\"");

                if (controlType == Server.Application.FormControlType.Form) { jsonBuilder.Append (",\"FormId\": " + Id.ToString ()); }

                jsonBuilder.Append (",\"Name\":\"" + name.Replace ("\"", "\\\"") + "\"");


                #region Properties and Extended Properties

                if (!visible) { jsonBuilder.Append (",\"Visible\":" + (Convert.ToInt32 (visible)).ToString () + " "); }
                
                if (tabIndex != 0) { jsonBuilder.Append (",\"TabIndex\":" + TabIndex.ToString ()); } // TAB INDEX 0 IS DEFAULT, -1 IS NO TAB STOP

                if (!enabled) { jsonBuilder.Append (",\"Enabled\":" + (Convert.ToInt32 (enabled)).ToString ()); }

                if (readOnly) { jsonBuilder.Append (",\"ReadOnly\":" + (Convert.ToInt32 (readOnly)).ToString ()); }

                if (required) { jsonBuilder.Append (",\"Required\":" + (Convert.ToInt32 (required)).ToString ()); }

                if (position != Server.Application.FormControlPosition.Left) { jsonBuilder.Append ("," + JsonObjectProperty ("Position", Convert.ToInt32 (Position))); }


                jsonBuilder.Append (JsonExtendedProperties); // EXTENDED PROPERTIES 
                
                #endregion 


                #region Style Properties

                jsonBuilder.Append (",\"Style\":{{}");

                // TEXT PROPERTIES

                if (!String.IsNullOrWhiteSpace (style.FontFamily)) { jsonBuilder.Append (",\"FontFamily\":\"" + style.FontFamily + "\""); }

                if (!String.IsNullOrWhiteSpace (style.FontSize)) { 
                    
                    jsonBuilder.Append (",\"FontSize\":\"" + style.FontSize + "\"");

                    jsonBuilder.Append (",\"FontSizeUnit\":\"" + style.FontSizeUnit + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.FontWeight)) { jsonBuilder.Append (",\"FontWeight\":\"" + style.FontWeight + "\""); }

                if (!String.IsNullOrWhiteSpace (style.FontStyle)) { jsonBuilder.Append (",\"FontStyle\":\"" + style.FontStyle + "\""); }

                if (!String.IsNullOrWhiteSpace (style.FontVariant)) { jsonBuilder.Append (",\"FontVariant\":\"" + style.FontVariant + "\""); }

                if (!String.IsNullOrWhiteSpace (style.Color)) { jsonBuilder.Append (",\"Color\":\"" + style.Color + "\""); }

                if (!String.IsNullOrWhiteSpace (style.TextTransform)) { jsonBuilder.Append (",\"TextTransform\":\"" + style.TextTransform + "\""); }

                if (!String.IsNullOrWhiteSpace (style.TextDecoration)) { jsonBuilder.Append (",\"TextDecoration\":\"" + style.TextDecoration + "\""); }

                if (!String.IsNullOrWhiteSpace (style.LetterSpacing)) { 
                    
                    jsonBuilder.Append (",\"LetterSpacing\":\"" + style.LetterSpacing + "\"");

                    jsonBuilder.Append (",\"LetterSpacingUnit\":\"" + style.LetterSpacingUnit + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.WordSpacing)) { 
                    
                    jsonBuilder.Append (",\"WordSpacing\":\"" + style.WordSpacing + "\"");

                    jsonBuilder.Append (",\"WordSpacingUnit\":\"" + style.WordSpacingUnit + "\""); 

                }

                if (!String.IsNullOrWhiteSpace (style.LineHeight)) { 
                    
                    jsonBuilder.Append (",\"LineHeight\":\"" + style.LineHeight + "\"");

                    jsonBuilder.Append (",\"LineHeightUnit\":\"" + style.LineHeightUnit + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.TextAlign)) { jsonBuilder.Append (",\"TextAlign\":\"" + style.TextAlign + "\""); }

                if (!String.IsNullOrWhiteSpace (style.VerticalAlign)) { jsonBuilder.Append (",\"VerticalAlign\":\"" + style.VerticalAlign + "\""); }


                /// BOX MODEL AND BACKGROUND STYLES

                if (!String.IsNullOrWhiteSpace (style.BackgroundColor)) { jsonBuilder.Append (",\"BackgroundColor\":\"" + style.BackgroundColor + "\""); }

                if (!String.IsNullOrWhiteSpace (style.Width)) { 
                    
                    jsonBuilder.Append (",\"Width\":\"" + style.Width + "\"");

                    jsonBuilder.Append (",\"WidthUnit\":\"" + style.WidthUnit + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.Height)) { 
                    
                    jsonBuilder.Append (",\"Height\":\"" + style.Height + "\"");

                    jsonBuilder.Append (",\"HeightUnit\":\"" + style.HeightUnit + "\"");

                }


                #region Margin and Padding

                if (!style.IsMarginSame) { jsonBuilder.Append (",\"IsMarginSame\":false"); }

                if (!String.IsNullOrWhiteSpace (style.Margin)) { jsonBuilder.Append (", \"Margin\": \"" + style.Margin + "\" "); }

                if (!String.IsNullOrWhiteSpace (style.MarginBottom)) {

                    jsonBuilder.Append (", \"MarginBottom\": \"" + style.MarginBottom + "\" ");

                    jsonBuilder.Append (", \"MarginBottomUnit\": \"" + style.MarginBottomUnit + "\" ");

                }

                if (!String.IsNullOrWhiteSpace (style.MarginLeft)) {

                    jsonBuilder.Append (", \"MarginLeft\": \"" + style.MarginLeft + "\" ");

                    jsonBuilder.Append (", \"MarginLeftUnit\": \"" + style.MarginLeftUnit + "\" ");

                }

                if (!String.IsNullOrWhiteSpace (style.MarginRight)) {

                    jsonBuilder.Append (", \"MarginRight\": \"" + style.MarginRight + "\" ");

                    jsonBuilder.Append (", \"MarginRightUnit\": \"" + style.MarginRightUnit + "\" ");

                }

                if (!String.IsNullOrWhiteSpace (style.MarginTop)) {

                    jsonBuilder.Append (", \"MarginTop\": \"" + style.MarginTop + "\" ");

                    jsonBuilder.Append (", \"MarginTopUnit\": \"" + style.MarginTopUnit + "\" ");

                }


                if (!style.IsPaddingSame) { jsonBuilder.Append (",\"IsPaddingSame\":false"); }

                if (!String.IsNullOrWhiteSpace (style.Padding)) { jsonBuilder.Append (", \"Padding\": \"" + style.Padding + "\" "); }

                if (!String.IsNullOrWhiteSpace (style.PaddingBottom)) { 
                    
                    jsonBuilder.Append (", \"PaddingBottom\": \"" + style.PaddingBottom + "\" "); 

                    jsonBuilder.Append (", \"PaddingBottomUnit\": \"" + style.PaddingBottomUnit + "\" "); 
                
                }

                if (!String.IsNullOrWhiteSpace (style.PaddingLeft)) { 
                    
                    jsonBuilder.Append (", \"PaddingLeft\": \"" + style.PaddingLeft + "\" "); 

                    jsonBuilder.Append (", \"PaddingLeftUnit\": \"" + style.PaddingLeftUnit + "\" "); 
                
                }

                if (!String.IsNullOrWhiteSpace (style.PaddingRight)) { 
                    
                    jsonBuilder.Append (", \"PaddingRight\": \"" + style.PaddingRight + "\" ");

                    jsonBuilder.Append (", \"PaddingRightUnit\": \"" + style.PaddingRightUnit + "\" "); 
                    
                }

                if (!String.IsNullOrWhiteSpace (style.PaddingTop)) {

                    jsonBuilder.Append (", \"PaddingTop\": \"" + style.PaddingTop + "\" ");

                    jsonBuilder.Append (", \"PaddingTopUnit\": \"" + style.PaddingTopUnit + "\" ");

                }

                #endregion 


                #region Border

                if ((style.IsBorderSame) && (!String.IsNullOrWhiteSpace (style.BorderTopWidth))) {

                    jsonBuilder.Append (", \"Border\": \"" + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopStyle + " " + style.BorderTopColor + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.BorderTopWidth)) {

                    jsonBuilder.Append (", \"BorderTop\": \"" + style.BorderTopWidth + style.BorderTopWidthUnit + " " + style.BorderTopStyle + " " + style.BorderTopColor + "\"");

                }


                if (!String.IsNullOrWhiteSpace (style.BorderBottomWidth)) {

                    jsonBuilder.Append (", \"BorderBottom\": \"" + style.BorderBottomWidth + style.BorderBottomWidthUnit + " " + style.BorderBottomStyle + " " + style.BorderBottomColor + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.BorderLeftWidth)) {

                    jsonBuilder.Append (", \"BorderLeft\": \"" + style.BorderLeftWidth + style.BorderLeftWidthUnit + " " + style.BorderLeftStyle + " " + style.BorderLeftColor + "\"");

                }

                if (!String.IsNullOrWhiteSpace (style.BorderRightWidth)) {

                    jsonBuilder.Append (", \"BorderRight\": \"" + style.BorderRightWidth + style.BorderRightWidthUnit + " " + style.BorderRightStyle + " " + style.BorderRightColor + "\"");

                }

                #endregion 


                jsonBuilder.Append ("}");
                
                #endregion 


                #region Label

                if ((Capabilities.HasLabel) && (Label.Visible)) {

                    jsonBuilder.Append (",\"Label\":" + Label.Json);

                }

                #endregion 


                #region Child Controls

                if (controls.Count > 0) {

                    jsonBuilder.Append (",\"Children\":[");

                    Boolean isFirstChildControl = true;

                    foreach (Control currentChildControl in controls) {

                        if (!isFirstChildControl) { jsonBuilder.Append (", "); }

                        else { isFirstChildControl = false; }

                        jsonBuilder.Append (currentChildControl.Json);

                    }

                    jsonBuilder.Append ("]");

                }

                #endregion 


                jsonBuilder.Append ("}");

                return jsonBuilder.ToString ().Replace ("[{},", "[").Replace ("{{},", "{").Replace ("{{}}", "{}");

            }

        }

        #endregion


        #region Constructors

        public virtual void BaseConstructor (Application applicationReference, Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            base.BaseConstructor (applicationReference, (Server.Application.CoreObject)serverControl);


            controlId = serverControl.ControlId;
            
            controlType = serverControl.ControlType;

            tabIndex = serverControl.TabIndex;

            enabled = serverControl.Enabled;

            visible = serverControl.Visible;

            readOnly = serverControl.ReadOnly;

            required = serverControl.Required;

            position = serverControl.Position;

            capabilities = serverControl.Capabilities;


            eventHandlers = new List<Server.Application.FormControlEventHandler> ();

            eventHandlers.AddRange (serverControl.EventHandlers);


            dataBindings = new List<Server.Application.FormControlDataBinding> ();

            dataBindings.AddRange (serverControl.DataBindings);


            style = serverControl.Style;

            parent = parentControl;

            return;

        }

        public void LocalControlToServer (Server.Application.FormControl serverControl) {

            base.MapToServerObject ((Server.Application.CoreObject)serverControl);


            serverControl.ControlId = controlId;

            serverControl.ControlType = controlType;

            serverControl.TabIndex = tabIndex;

            serverControl.Enabled = enabled;

            serverControl.Visible = visible;

            serverControl.ReadOnly = readOnly;

            serverControl.Required = required;

            serverControl.Position = position;

            serverControl.Capabilities = capabilities;

            serverControl.Style = style;


            if (eventHandlers == null) { eventHandlers = new List<Server.Application.FormControlEventHandler> (); }

            serverControl.EventHandlers = new Server.Application.FormControlEventHandler[eventHandlers.Count];

            eventHandlers.CopyTo (serverControl.EventHandlers);


            if (dataBindings == null) { dataBindings = new List<Server.Application.FormControlDataBinding> (); }

            serverControl.DataBindings = new Server.Application.FormControlDataBinding[dataBindings.Count];

            dataBindings.CopyTo (serverControl.DataBindings);

            return;

        }

        virtual public void LocalControlToServer (Server.Application.FormControl parentControl, Server.Application.FormControl serverControl) {

            Int32 controlIndex = 0;

            LocalControlToServer (serverControl);

            serverControl.Controls = new Server.Application.FormControl[Controls.Count];

            foreach (Control currentLocalControl in Controls) {

                switch (currentLocalControl.ControlType) {

                    case Server.Application.FormControlType.Section:

                        Server.Application.FormControlSection serverSection = new Server.Application.FormControlSection ();

                        ((Mercury.Client.Core.Forms.Controls.Section)currentLocalControl).LocalControlToServer (serverControl, serverSection);

                        serverControl.Controls[controlIndex] = serverSection;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.SectionColumn:

                        Server.Application.FormControlSectionColumn serverSectionColumn = new Server.Application.FormControlSectionColumn ();

                        ((Mercury.Client.Core.Forms.Controls.SectionColumn)currentLocalControl).LocalControlToServer (serverControl, serverSectionColumn);

                        serverControl.Controls[controlIndex] = serverSectionColumn;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Label:

                        Server.Application.FormControlLabel serverLabel = new Server.Application.FormControlLabel ();

                        ((Mercury.Client.Core.Forms.Controls.Label)currentLocalControl).LocalControlToServer (serverControl, serverLabel);

                        serverControl.Controls[controlIndex] = serverLabel;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Text:

                        Server.Application.FormControlText serverText = new Server.Application.FormControlText ();

                        ((Mercury.Client.Core.Forms.Controls.Text)currentLocalControl).LocalControlToServer (serverControl, serverText);

                        serverControl.Controls[controlIndex] = serverText;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Input:

                        Server.Application.FormControlInput serverInput = new Server.Application.FormControlInput ();

                        ((Mercury.Client.Core.Forms.Controls.Input)currentLocalControl).LocalControlToServer (serverControl, serverInput);

                        serverControl.Controls[controlIndex] = serverInput;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Selection:

                        Server.Application.FormControlSelection serverSelection = new Server.Application.FormControlSelection ();

                        ((Mercury.Client.Core.Forms.Controls.Selection)currentLocalControl).LocalControlToServer (serverControl, serverSelection);

                        serverControl.Controls[controlIndex] = serverSelection;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Button:

                        Server.Application.FormControlButton serverButton = new Server.Application.FormControlButton ();

                        ((Mercury.Client.Core.Forms.Controls.Button)currentLocalControl).LocalControlToServer (serverControl, serverButton);

                        serverControl.Controls[controlIndex] = serverButton;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Entity:

                        Server.Application.FormControlEntity serverEntity = new Server.Application.FormControlEntity ();

                        ((Mercury.Client.Core.Forms.Controls.Entity)currentLocalControl).LocalControlToServer (serverControl, serverEntity);

                        serverControl.Controls[controlIndex] = serverEntity;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Collection:

                        Server.Application.FormControlCollection serverCollection = new Server.Application.FormControlCollection ();

                        ((Mercury.Client.Core.Forms.Controls.Collection)currentLocalControl).LocalControlToServer (serverControl, serverCollection);

                        serverControl.Controls[controlIndex] = serverCollection;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Address:

                        Server.Application.FormControlAddress serverAddress = new Server.Application.FormControlAddress ();

                        ((Mercury.Client.Core.Forms.Controls.Address)currentLocalControl).LocalControlToServer (serverControl, serverAddress);

                        serverControl.Controls[controlIndex] = serverAddress;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Service:

                        Server.Application.FormControlService serverService = new Server.Application.FormControlService ();

                        ((Mercury.Client.Core.Forms.Controls.Service)currentLocalControl).LocalControlToServer (serverControl, serverService);

                        serverControl.Controls[controlIndex] = serverService;

                        controlIndex = controlIndex + 1;

                        break;

                    case Server.Application.FormControlType.Metric:

                        Server.Application.FormControlMetric serverMetric = new Server.Application.FormControlMetric ();

                        ((Mercury.Client.Core.Forms.Controls.Metric)currentLocalControl).LocalControlToServer (serverControl, serverMetric);

                        serverControl.Controls[controlIndex] = serverMetric;

                        controlIndex = controlIndex + 1;

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unable to process Local to Server: " + currentLocalControl.controlType.ToString ());

                        throw new ApplicationException ("Local Control to Server. Unable to process Local to Server: " + currentLocalControl.controlType.ToString ());

                } // switch

            } // foreach

            return;

        }

        public void ChildServerControlsToLocal (Mercury.Client.Core.Forms.Control parentControl, Server.Application.FormControl serverControl) {

            foreach (Server.Application.FormControl currentControl in serverControl.Controls) {

                switch (currentControl.ControlType) {

                    case Server.Application.FormControlType.Section:

                        Mercury.Client.Core.Forms.Controls.Section sectionControl;

                        sectionControl = new Mercury.Client.Core.Forms.Controls.Section (Application, this, (Server.Application.FormControlSection)currentControl);

                        Controls.Insert (Controls.Count, sectionControl);

                        break;

                    case Server.Application.FormControlType.SectionColumn:

                        Mercury.Client.Core.Forms.Controls.SectionColumn sectionColumnControl;

                        sectionColumnControl = new Mercury.Client.Core.Forms.Controls.SectionColumn (Application, this, (Server.Application.FormControlSectionColumn)currentControl);

                        Controls.Insert (Controls.Count, sectionColumnControl);

                        break;


                    case Server.Application.FormControlType.Label:

                        Mercury.Client.Core.Forms.Controls.Label labelControl;

                        labelControl = new Mercury.Client.Core.Forms.Controls.Label (Application, this, (Server.Application.FormControlLabel)currentControl);

                        Controls.Insert (Controls.Count, labelControl);

                        break;


                    case Server.Application.FormControlType.Text:

                        Mercury.Client.Core.Forms.Controls.Text textControl;

                        textControl = new Mercury.Client.Core.Forms.Controls.Text (Application, this, (Server.Application.FormControlText)currentControl);

                        Controls.Insert (Controls.Count, textControl);

                        break;

                    case Server.Application.FormControlType.Input:

                        Mercury.Client.Core.Forms.Controls.Input inputControl;

                        inputControl = new Mercury.Client.Core.Forms.Controls.Input (Application, this, (Server.Application.FormControlInput)currentControl);

                        Controls.Insert (Controls.Count, inputControl);

                        break;

                    case Server.Application.FormControlType.Selection:

                        Mercury.Client.Core.Forms.Controls.Selection selectionControl;

                        selectionControl = new Mercury.Client.Core.Forms.Controls.Selection (Application, this, (Server.Application.FormControlSelection)currentControl);

                        Controls.Insert (Controls.Count, selectionControl);

                        break;

                    case Server.Application.FormControlType.Button:

                        Mercury.Client.Core.Forms.Controls.Button buttonControl;

                        buttonControl = new Mercury.Client.Core.Forms.Controls.Button (Application, this, (Server.Application.FormControlButton)currentControl);

                        Controls.Insert (Controls.Count, buttonControl);

                        break;

                    case Server.Application.FormControlType.Entity:

                        Mercury.Client.Core.Forms.Controls.Entity entityControl;

                        entityControl = new Mercury.Client.Core.Forms.Controls.Entity (Application, this, (Server.Application.FormControlEntity)currentControl);

                        Controls.Insert (Controls.Count, entityControl);

                        break;

                    case Server.Application.FormControlType.Collection:

                        Mercury.Client.Core.Forms.Controls.Collection collectionControl;

                        collectionControl = new Mercury.Client.Core.Forms.Controls.Collection (Application, this, (Server.Application.FormControlCollection)currentControl);

                        Controls.Insert (Controls.Count, collectionControl);

                        break;

                    case Server.Application.FormControlType.Address:

                        Mercury.Client.Core.Forms.Controls.Address addressControl;

                        addressControl = new Mercury.Client.Core.Forms.Controls.Address (Application, this, (Server.Application.FormControlAddress)currentControl);

                        Controls.Insert (Controls.Count, addressControl);

                        break;

                    case Server.Application.FormControlType.Service:

                        Mercury.Client.Core.Forms.Controls.Service serviceControl;

                        serviceControl = new Mercury.Client.Core.Forms.Controls.Service (Application, this, (Server.Application.FormControlService)currentControl);

                        Controls.Insert (Controls.Count, serviceControl);

                        break;

                    case Server.Application.FormControlType.Metric:

                        Mercury.Client.Core.Forms.Controls.Metric metricControl;

                        metricControl = new Mercury.Client.Core.Forms.Controls.Metric (Application, this, (Server.Application.FormControlMetric)currentControl);

                        Controls.Insert (Controls.Count, metricControl);

                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine ("Unable to map Server control to Local control for " + currentControl.ControlType.ToString () + ".");

                        throw new ApplicationException ("Unable to map Server control to Local control for " + currentControl.ControlType.ToString () + ".");

                } // switch

            } // foreach 

        }

        #endregion


        #region Public Methods

        virtual public Boolean AllowChildControl (Server.Application.FormControlType childControlType) {

            return false;

        }

        virtual public void AddChildControl (Control childControl) {

            if (AllowChildControl (childControl.ControlType)) {

                childControl.Parent = this;

                childControl.Application = Application;

                controls.Add (childControl);

            }

            return;

        }


        public Int32 ControlIndex (Guid forControlId) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control)controls[currentControlIndex];

                if (currentControl.ControlId == forControlId) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Int32 ControlIndex (String controlName) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control)controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Int32 ControlIndexByName (String controlName) {

            Int32 controlIndex = -1;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control)controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlIndex = currentControlIndex;

                    break;

                }

            }

            return controlIndex;

        }

        public Boolean ControlExists (Guid forControlId) {

            Boolean controlFound = false;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control)controls[currentControlIndex];

                if (currentControl.ControlId == forControlId) {

                    controlFound = true;

                    break;

                }

            }

            return controlFound;

        }

        public Boolean ControlExistsByName (String controlName) {

            Boolean controlFound = false;

            for (Int32 currentControlIndex = 0; currentControlIndex < controls.Count; currentControlIndex++) {

                Control currentControl = (Control)controls[currentControlIndex];

                if (currentControl.Name == controlName) {

                    controlFound = true;

                    break;

                }

            }

            return controlFound;

        }

        public Mercury.Client.Core.Forms.Control GetChildControl (String controlName) {

            Int32 controlIndex = ControlIndex (controlName);

            if (controlIndex != -1) {

                return (Mercury.Client.Core.Forms.Control)controls[controlIndex];

            }

            return null;

        }

        public Mercury.Client.Core.Forms.Control GetChildControl (Int32 controlIndex) {

            return (Mercury.Client.Core.Forms.Control)controls[controlIndex];

        }

        virtual public Mercury.Client.Core.Forms.Control FindControlById (Guid forControlId) {

            Mercury.Client.Core.Forms.Control control = null;

            if (controlId == forControlId) { return this; }

            foreach (Mercury.Client.Core.Forms.Control currentControl in controls) {

                if (currentControl.ControlId == forControlId) {

                    control = currentControl;

                    break;

                }

                else {

                    control = currentControl.FindControlById (forControlId);

                    if (control != null) { break; }

                }

            }

            return control;

        }

        virtual public Mercury.Client.Core.Forms.Control FindControlByName (String forName) {

            Mercury.Client.Core.Forms.Control control = null;

            if (name == forName) { return this; }

            foreach (Mercury.Client.Core.Forms.Control currentControl in controls) {

                currentControl.Parent = this; // RESET PARENT REFERENCE IN CASE OF CHANGE OR LOST REFERENCE

                control = currentControl.FindControlByName (forName);

                if (control != null) { break; }

            }

            return control;

        }

        public void InsertNewControl (Int32 index, Mercury.Client.Core.Forms.Control control) {

            String controlPrefix = control.controlType.ToString ();

            Int32 controlSuffix = 1;


            control.parent = this;


            if (index == -1) { index = controls.Count; }

            while (Form.FindControlByName (controlPrefix + controlSuffix.ToString ()) != null) {

                controlSuffix = controlSuffix + 1;

            }


            control.Name = controlPrefix + controlSuffix.ToString ();

            control.Description = controlPrefix + controlSuffix.ToString ();

            controls.Insert (index, control);

            return;

        }

        public void InsertControl (Int32 index, Mercury.Client.Core.Forms.Control control) {

            if ((index == -1) || (index > controls.Count)) { index = controls.Count; }

            control.Parent = this;

            controls.Insert (index, control);

            return;

        }

        public Mercury.Client.Core.Forms.Form Form {

            get {

                Mercury.Client.Core.Forms.Form form = null;

                if (controlType == Mercury.Server.Application.FormControlType.Form) { form = (Form)this; }

                else if (parent == null) { form = null; }

                else if (controlId != parent.controlId) { form = parent.Form; }

                return form;

            }

        }

        virtual public System.Collections.ArrayList GetAllControls () {

            System.Collections.ArrayList controlList = new System.Collections.ArrayList ();

            controlList.Add (this);

            foreach (Control currentChild in Controls) {

                controlList.AddRange (currentChild.GetAllControls ());

            }

            return controlList;

        }

        #endregion


        #region Virtual Public Methods

        virtual public Object Property (String propertyName) {

            switch (propertyName.ToLower ()) {

                case "id": return controlId;

                case "name": return name;

                case "title": // BACKWARDS COMPATIBILITY

                case "description":
                                        
                    return description;

                case "controltype": return controlType;

                case "enabled": return enabled;

                case "visible": return visible;

                case "position": return position;

                case "capabilities": return capabilities;

                case "style": return style;

            }

            return null;

        }

        #endregion


        #region Public Data Binding Methods

        virtual public List<Control> GetDataSources () {

            List<Control> dataSources = new List<Control> ();

            if (Capabilities.IsDataSource) {

                dataSources.Add (this);

            }

            foreach (Control currentControl in Controls) {

                List<Control> childDataSources = currentControl.GetDataSources ();

                if (childDataSources.Count > 0) {

                    dataSources.AddRange (childDataSources);

                }

            }

            return dataSources;

        }

        virtual public Boolean IsDataBindingCircular (Guid dataSourceId) {

            return false;

        }

        #endregion


        #region Event Handlers

        public virtual List<String> Events {

            get {

                if (events == null) {

                    if (Application != null) {

                        events = Application.FormControl_Events (Form, this);

                        if (events == null) { events = new List<String> (); }

                    }

                }

                return events;

            }

        }

        public virtual List<Server.Application.FormControlEventHandler> EventHandlers { get { return eventHandlers; } set { eventHandlers = value; } }

        public virtual Server.Application.FormControlEventHandler GetEventHandler (String eventName) {

            foreach (Server.Application.FormControlEventHandler currentEventHandler in eventHandlers) {

                if (currentEventHandler.EventName == eventName) {

                    return currentEventHandler;

                }

            }

            return null;

        }

        public Dictionary<Guid, String> EventHandler_UpdatedControls (String eventName) {

            Dictionary<Guid, String> updatedControls = new Dictionary<Guid, String> ();


            Int32 positionInitial = 0;

            Int32 positionFinal = 0;

            const String updatedControlsDefinition = "/* DEFINE_UPDATEDCONTROLS:";

            const String findControlByNameDefinition = "FindControlByName(";


            try {

                String eventSource = EventHandler_Source (eventName);

                #region Extract by User Definition

                if (eventSource.Contains (updatedControlsDefinition)) {

                    /* USE USER DEFINITION OF UPDATED CONTROLS */

                    positionInitial = eventSource.IndexOf (updatedControlsDefinition) + updatedControlsDefinition.Length;

                    positionFinal = eventSource.IndexOf ("*/", positionInitial);

                    if (positionFinal > 0) {

                        foreach (String updatedControlName in eventSource.Substring (positionInitial, positionFinal - positionInitial).Split ('|')) {

                            if (updatedControlName.Trim () != name) {

                                Control updatedControl = Form.FindControlByName (updatedControlName.Trim ());

                                if (updatedControl != null) {

                                    if (!updatedControls.ContainsKey (updatedControl.ControlId)) {

                                        updatedControls.Add (updatedControl.ControlId, updatedControl.Name);

                                    }

                                }

                            }

                        }

                    }

                }

                #endregion

                #region Extract by FindControlByName Method

                else {

                    eventSource = eventSource.Replace ("FindControlByName (\"", "FindControlByName(\"");


                    while (eventSource.Contains (findControlByNameDefinition)) {

                        eventSource = eventSource.Substring (eventSource.IndexOf (findControlByNameDefinition) + findControlByNameDefinition.Length).Trim ();

                        positionInitial = 0;

                        positionFinal = eventSource.IndexOf (")");

                        if (positionFinal == -1) { break; }

                        String updatedControlName = eventSource.Substring (positionInitial, (positionFinal - positionInitial)).Replace ("\"", "");

                        Client.Core.Forms.Control updatedControl = Form.FindControlByName (updatedControlName);

                        if (updatedControl != null) {

                            if (!updatedControls.ContainsKey (updatedControl.ControlId)) {

                                updatedControls.Add (updatedControl.ControlId, updatedControl.Name);

                            }

                        }

                        eventSource = eventSource.Substring (positionFinal);

                    }

                }

                #endregion

            }

            catch (Exception applicationException) {

                Application.SetLastException (applicationException);

            }

            updatedControls.Remove (controlId);

            return updatedControls;

        }

        public Dictionary<Guid, String> EventHandler_UpdatedControls () {

            Dictionary<Guid, String> updatedControls = new Dictionary<Guid, String> ();

            foreach (Server.Application.FormControlEventHandler currentEventHandler in EventHandlers) {

                Dictionary<Guid, String> eventUpdatedControls = EventHandler_UpdatedControls (currentEventHandler.EventName);

                foreach (Guid updatedControlId in eventUpdatedControls.Keys) {

                    if (!updatedControls.ContainsKey (updatedControlId)) {

                        updatedControls.Add (updatedControlId, eventUpdatedControls[updatedControlId]);

                    }

                }

            }

            return updatedControls;

        }

        public virtual Boolean EventHandlers_AllSmart () {

            Boolean allSmart = true;

            foreach (Server.Application.FormControlEventHandler currentEventHandler in EventHandlers) {

                allSmart = allSmart && currentEventHandler.SmartEvent;

                if (!allSmart) { break; }

            }


            return allSmart;

        }

        private String EventHandler_Source (String eventName) {

            StringBuilder codeSource = new StringBuilder ();

            Server.Application.FormControlEventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler == null) { return String.Empty; }

            if (String.IsNullOrEmpty (eventHandler.MethodSource)) { return String.Empty; }



            codeSource.Append ("using System; \r\n ");

            codeSource.Append ("using System.IO; \r\n");

            codeSource.Append ("using Enumerations = Mercury.Server.Application; \r\n \r\n"); // LOCALIZE SERVER ENUMERATIONS TO ONE FOLDER

            codeSource.Append ("using Core = Mercury.Client.Core; \r\n \r\n");

            codeSource.Append ("namespace Mercury.Client.EventHandlerNamespace { \r\n \r\n");

            codeSource.Append ("public class EventHandlerClass { \r\n \r\n");

            codeSource.Append ("public void OnEvent (ref Core.Forms.Form form, ref Core.Forms.Control sender, System.Diagnostics.TraceListener traceListener) { \r\n \r\n");

            codeSource.Append (eventHandler.MethodSource.Replace ("\r", "\r\n"));

            codeSource.Append (" \r\n  return; ");

            codeSource.Append (" \r\n } } }");


            String source = codeSource.ToString ().Replace ("\r\n", "\r");

            return source;

        }

        public System.Reflection.Assembly EventHandler_Compile (String eventName, ref List<Server.Application.FormCompileMessage> compileMessages) {

            compileMessages = new List<Mercury.Server.Application.FormCompileMessage> ();

            Server.Application.FormCompileMessage message;

            String codeSource = EventHandler_Source (eventName);

            if (String.IsNullOrEmpty (codeSource)) {

                message = new Mercury.Server.Application.FormCompileMessage ();

                message.ControlId = controlId.ToString ();

                message.ControlName = name;

                message.ControlType = controlType.ToString ();

                message.MessageType = Mercury.Server.Application.FormCompileMessageType.Error;

                message.Description = "No Event Handler found for control.";

                compileMessages.Add (message);

            }

            else {

                Microsoft.CSharp.CSharpCodeProvider compiler = new Microsoft.CSharp.CSharpCodeProvider ();

                System.CodeDom.Compiler.CompilerParameters compilerParameters = new System.CodeDom.Compiler.CompilerParameters ();

                compilerParameters.ReferencedAssemblies.Add ("System.dll");


                foreach (System.Reflection.Assembly loadedAssembly in AppDomain.CurrentDomain.GetAssemblies ()) {

                    System.Diagnostics.Debug.WriteLine (loadedAssembly.FullName);

                    if ((loadedAssembly.FullName.Contains ("Mercury.Client,")) || (loadedAssembly.FullName.Contains ("System.Runtime.Serialization,"))) {

                        compilerParameters.ReferencedAssemblies.Add (loadedAssembly.Location);

                    }

                }

                System.CodeDom.Compiler.CompilerResults compiledResults = compiler.CompileAssemblyFromSource (compilerParameters, codeSource);

                if (compiledResults.Errors.HasErrors) {

                    foreach (System.CodeDom.Compiler.CompilerError currentError in compiledResults.Errors) {

                        message = new Mercury.Server.Application.FormCompileMessage ();

                        message.ControlId = controlId.ToString ();

                        message.ControlName = name;

                        message.ControlType = controlType.ToString ();

                        message.MessageType = Mercury.Server.Application.FormCompileMessageType.Error;

                        message.Line = currentError.Line - 10;

                        message.Column = currentError.Column;

                        message.Description = currentError.ErrorText;

                        compileMessages.Add (message);

                    }

                }

                try {

                    const String findControlByNameDefinition = "FindControlByName(";

                    String eventHandlerSource = codeSource.Replace ("FindControlByName (", "FindControlByName(");

                    while (eventHandlerSource.Contains (findControlByNameDefinition)) {

                        eventHandlerSource = eventHandlerSource.Substring (eventHandlerSource.IndexOf (findControlByNameDefinition) + findControlByNameDefinition.Length).Trim ();

                        Int32 positionInitial = 0;

                        Int32 positionFinal = eventHandlerSource.IndexOf (")");

                        if (positionFinal == -1) { break; }


                        String referencedControlName = eventHandlerSource.Substring (positionInitial, (positionFinal - positionInitial));

                        if ((referencedControlName[0] == '"') && (referencedControlName[referencedControlName.Length - 1] == '"')) {

                            referencedControlName = referencedControlName.Substring (1, referencedControlName.Length - 2);

                            if (!referencedControlName.Contains ("\"")) {

                                if (Form.FindControlByName (referencedControlName) == null) {

                                    message = new Mercury.Server.Application.FormCompileMessage ();

                                    message.ControlId = controlId.ToString ();

                                    message.ControlName = name;

                                    message.ControlType = controlType.ToString ();

                                    message.MessageType = Mercury.Server.Application.FormCompileMessageType.Error;

                                    message.Line = 0;

                                    message.Column = 0;

                                    message.Description = "Unable to find control: " + referencedControlName;

                                    compileMessages.Add (message);

                                }

                            }

                        }

                        eventHandlerSource = eventHandlerSource.Substring (positionFinal);

                    }

                }

                catch { /* DO NOTHING */ }


                if (compileMessages.Count > 0) { return null; }

                return compiledResults.CompiledAssembly;

            }

            return null;

        }

        public void EventHandlers_Precompile () {

            Form.AllowPrecompileEvents = false; // 11-23-2009 DISABLE PRECOMPILE, USE NEW CACHING METHOD

            if (!Form.AllowPrecompileEvents) { return; }

            DateTime startTime = DateTime.Now;

            foreach (String currentEventName in Events) {

                if (!Form.CompiledEvents.ContainsKey (controlId + "." + currentEventName)) {

                    Server.Application.FormControlEventHandler eventHandler = GetEventHandler (currentEventName);

                    if (eventHandler != null) {

                        if (eventHandler.ExecuteClientSide) {

                            List<Server.Application.FormCompileMessage> compileMessages = new List<Mercury.Server.Application.FormCompileMessage> ();

                            System.Reflection.Assembly compiledAssembly = EventHandler_Compile (currentEventName, ref compileMessages);

                            if (compiledAssembly != null) {

                                Form.CompiledEvents.Add (controlId.ToString () + "." + currentEventName, compiledAssembly);

                            }

                        }

                    }

                }

            }

            foreach (Control currentChildControl in controls) {

                currentChildControl.EventHandlers_Precompile ();

            }

            if (controlType == Mercury.Server.Application.FormControlType.Form) {

                System.Diagnostics.Debug.WriteLine ("Precompile Time: " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

            }

            return;

        }

        public List<Server.Application.FormCompileMessage> EventHandler_Compile (String eventName) {

            List<Server.Application.FormCompileMessage> compileMessages = new List<Mercury.Server.Application.FormCompileMessage> ();

            Server.Application.FormControlEventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler != null) {

                //if (eventHandler.ExecuteClientSide) {

                //    EventHandler_Compile (eventName, ref compileMessages);

                //}

                //else {

                compileMessages = Application.FormControl_EventHandler_Compile (this, eventName);

                //}

            }

            return compileMessages;

        }

        private EventResult EventHandler_Execute (String eventName) {

            EventResult eventResult = new EventResult (controlId, eventName, true);

            List<Server.Application.FormCompileMessage> compileMessages = new List<Mercury.Server.Application.FormCompileMessage> ();

            System.Reflection.Assembly compiledAssembly = null;



            String cacheKey = Form.Application.Session.EnvironmentId.ToString () + "." + Form.FormId.ToString () + controlId.ToString () + eventName + Form.ModifiedAccountInfo.ActionDate.ToString ("MMddyyyy");

            if (Application.UseFormControlEventHandlerCaching) {

                compiledAssembly = (System.Reflection.Assembly)Application.CacheManager.GetObject (cacheKey);

            }


            if (compiledAssembly == null) {

                compiledAssembly = EventHandler_Compile (eventName, ref compileMessages);

                Application.CacheManager.CacheObject (cacheKey, compiledAssembly, new TimeSpan (8, 0, 0));

            }

            if (compiledAssembly == null) {

                Application.SetLastException (new ApplicationException ("Unable to obtain compiled assembly reference for: " + Form.Name + "." + Name + "." + eventName + "."));

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to obtain compiled assembly reference."));

                return eventResult;

            }

            if ((compiledAssembly == null) || (compileMessages.Count > 0)) {

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to successfuly compile event handler source."));

                return eventResult;

            }


            Object functionInstance = compiledAssembly.CreateInstance ("Mercury.Client.EventHandlerNamespace.EventHandlerClass");

            if (functionInstance == null) {

                Application.SetLastException (new ApplicationException ("Unable to create an instance of the Event Handler."));

                eventResult = new EventResult (controlId, eventName, new ApplicationException ("Unable to create an instance of the Event Handler."));

            }

            else {

                System.IO.StringWriter stringWriter = new System.IO.StringWriter ();

                System.Diagnostics.TextWriterTraceListener traceListener = new System.Diagnostics.TextWriterTraceListener (stringWriter);

                try {

                    Object[] functionArguments = new Object[3];

                    functionArguments[0] = Form;

                    functionArguments[1] = (Control)this;

                    functionArguments[2] = traceListener;

                    functionInstance.GetType ().InvokeMember ("OnEvent", System.Reflection.BindingFlags.InvokeMethod, null, functionInstance, functionArguments);

                    eventResult.ListenerOutput = stringWriter.ToString ();

                }
                catch (System.Reflection.TargetInvocationException invocationException) {

                    if (invocationException.InnerException != null) {

                        Application.SetLastException (invocationException.InnerException);

                        eventResult = new EventResult (controlId, eventName, invocationException.InnerException);

                        eventResult.ListenerOutput = stringWriter.ToString ();

                    }

                    else {

                        Application.SetLastException (invocationException);

                        eventResult = new EventResult (controlId, eventName, invocationException);

                        eventResult.ListenerOutput = stringWriter.ToString ();

                    }

                }

                catch (Exception eventHandlerException) {

                    String exceptionMessage = "Form Event Handler Exception [" + Form.Id + "]: " + Form.Name + "." + this.Name + "." + eventName;

                    ApplicationException applicationException = new ApplicationException (exceptionMessage, eventHandlerException);

                    Application.SetLastException (applicationException);

                    eventResult = new EventResult (controlId, eventName, applicationException);

                    eventResult.ListenerOutput = stringWriter.ToString ();

                }

            }

            if (eventResult != null) { System.Diagnostics.Debug.WriteLine (name + " (" + eventName + ") \r\n" + eventResult.ListenerOutput); }


            return eventResult;

        }

        public virtual void RaiseEvent (String eventName) {

            Server.Application.FormControlEventHandler eventHandler = GetEventHandler (eventName);

            if (eventHandler == null) { return; }


            if (application != null) { application.ClearLastException (); }


            if (eventHandler.ExecuteClientSide) {

                // CLIENT SIDE EXECUTION

                EventResult eventResult;

                eventResult = EventHandler_Execute (eventName);

                Form.EventResults.Add (eventResult);

            }

            else { if (Form != null) { Form.RaiseEvent (this, eventName); } }

            return;

        }

        #endregion


        #region Data Bindings 2.0

        public override Dictionary<String, String> DataBindingContexts {

            get {

                if (dataBindingContexts == null) {

                    if (Application != null) {

                        dataBindingContexts = Application.FormControl_DataBindingContexts (Form, ControlId);

                    }

                }

                return dataBindingContexts;

            }

        }

        public virtual Dictionary<String, String> DataBindableProperties {

            get {

                if (bindableProperties == null) {

                    if (Application != null) {

                        bindableProperties = Application.FormControl_DataBindableProperties (Form, ControlId);

                        if (bindableProperties == null) { bindableProperties = new Dictionary<String, String> (); }

                    }

                }


                return bindableProperties;

            }

        }

        public virtual Boolean DataBindingAllowed (String bindableProperty, String forDataType) {

            // FOR PERFORMANCE REASONS (AND TO REDUCE ROUNDTRIPS), THIS CODE IS DUPLICATED BETWEEN SERVER AND CLIENT
            // ANY CHANGES TO THE BELOW CODE MUST BE COPIED TO THE OTHER PLACE

            Boolean bindingAllowed = false;

            Dictionary<String, String> bindableProperties = DataBindableProperties;

            foreach (String currentKey in bindableProperties.Keys) {

                if (currentKey == bindableProperty) {

                    String propertyDataType = bindableProperties[currentKey].Split ('|')[0];

                    String sourceDataType = forDataType.Split ('|')[0];


                    switch (propertyDataType) {

                        case "Id": if (bindableProperties[currentKey] == forDataType) { bindingAllowed = true; } break;

                        case "String": if (sourceDataType != "Collection") { bindingAllowed = true; } break;

                        case "Int16": if ((sourceDataType == "Int16") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "Int32": if ((sourceDataType == "Int16") || (sourceDataType == "Int32") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "Int64":

                        case "Numeric":

                            if ((sourceDataType == "Int16") || (sourceDataType == "Int32") || (sourceDataType == "Int64") || (sourceDataType == "Numeric")) { bindingAllowed = true; } break;

                        case "DateTime": if (sourceDataType == "DateTime") { bindingAllowed = true; } break;

                        case "Collection": if (sourceDataType == "Collection") { bindingAllowed = true; } break;

                        default: bindingAllowed = false; break;

                    }

                    break;

                }

            }

            return bindingAllowed;

        }

        public List<Server.Application.FormControlDataBinding> DataBindings { get { return dataBindings; } set { dataBindings = value; } }

        public Boolean ContainsDataBinding (Guid forControlId) {

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) { return true; }

            }

            return false;

        }

        public List<Server.Application.FormControlDataBinding> GetDataBindings (Guid forControlId) {

            List<Server.Application.FormControlDataBinding> bindingSubset = new List<Server.Application.FormControlDataBinding> ();

            foreach (Server.Application.FormControlDataBinding currentDataBinding in dataBindings) {

                if (currentDataBinding.DataSourceControlId == forControlId) {

                    bindingSubset.Add (currentDataBinding);

                }

            }

            return bindingSubset;

        }

        public Server.Application.FormControlDataBinding GetDataBinding (String forProperty) {

            foreach (Server.Application.FormControlDataBinding currentBinding in DataBindings) {

                if (currentBinding.BoundProperty == forProperty) { return currentBinding; }

            }

            return null;

        }

        public String GetDataBindingContextDataType (String context) {

            String dataType = String.Empty;

            Dictionary<String, String> dataBindingContexts = DataBindingContexts;

            if (dataBindingContexts != null) {

                foreach (String currentContext in dataBindingContexts.Keys) {

                    if (currentContext == context) {

                        dataType = dataBindingContexts[currentContext];

                        break;

                    }

                }

            }

            return dataType;

        }

        public virtual void OnDataSourceChanged (Control dataSourceControl, Boolean propogate) {

            if (propogate) {

                foreach (Control currentControl in Controls) {

                    currentControl.Parent = this;  // RESET PARENT REFERENCE IN CASE OF CHANGE OR LOST REFERENCE

                    currentControl.OnDataSourceChanged (dataSourceControl, propogate);

                }

            }

            return;

        }

        public void DataSourceChanged () {

            if (Form != null) {

                if (Form.GetDataBindingDependencies (ControlId).Count != 0) {

                    Form.OnDataSourceChanged (this, true);

                }

            }

            return;

        }

        virtual public String EvaluateDataBinding (Server.Application.FormControlDataBinding dataBinding) {

            switch (dataBinding.DataBindingType) {

                case Mercury.Server.Application.FormControlDataBindingType.FixedValue: return dataBinding.BindingContext;

                case Mercury.Server.Application.FormControlDataBindingType.Function: return "TODO";

                default:

                    if (Application != null) {

                        return Application.FormControl_EvaluateDataBinding (Form, controlId, dataBinding);

                    }

                    break;

            }

            return String.Empty;

        }

        virtual public System.Collections.ArrayList GetDataBindingDependencies (Guid forDataSourceControlId) {

            System.Collections.ArrayList dependencies = new System.Collections.ArrayList ();

            foreach (Server.Application.FormControlDataBinding currentBinding in dataBindings) {

                if (currentBinding.DataSourceControlId == forDataSourceControlId) {

                    if (!dependencies.Contains (this)) {

                        dependencies.Add (this);

                        dependencies.AddRange (Form.GetDataBindingDependencies (controlId));

                    }

                }

            }

            foreach (Control currentControl in Controls) {

                dependencies.AddRange (currentControl.GetDataBindingDependencies (forDataSourceControlId));

            }

            return dependencies;

        }

        public virtual void DataBindingsResetCache () {

            dataBindingContexts = null;

            bindableProperties = null;

            return;

        }

        #endregion


        #region JSON Support 

        protected String JsonObjectProperty (String propertyName, String value) {

            return "\"" + propertyName + "\":\"" + value.Replace ("\"", @"\""").Replace (System.Environment.NewLine, "") + "\"";

        }

        protected String JsonObjectProperty (String propertyName, Boolean value) {

            return "\"" + propertyName + "\":" + value.ToString ().ToLower();

        }

        protected String JsonObjectProperty (String propertyName, Int32 value) {

            return "\"" + propertyName + "\":" + value.ToString ();

        }

        protected String JsonObjectProperty (String propertyName, Double value) {

            return "\"" + propertyName + "\":" + value.ToString ();

        }

        protected String JsonObjectProperty (String propertyName, DateTime value) {

            return "\"" + propertyName + "\":\"" + value.ToString ("MM/dd/yyyy HH:mm:ss tt") + "\"";

        }

        #endregion 

    }

}
