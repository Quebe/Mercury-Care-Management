using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Mercury.Client.Core.Forms.Structures {

    public class Style : System.ComponentModel.INotifyPropertyChanged {


        #region Private Properties

        private const Double pixelsPerInch = 96;

        private const Double formWidth = 800;

        private Control parentControl = null;

        #endregion 


        #region Private Properties - Font Properties

        private String fontFamily = String.Empty;

        private String fontSize = String.Empty;

        private String fontSizeUnit = "px"; // px, pt, %

        private String fontWeight = String.Empty; // normal, bold, bolder, lighter

        private String fontStyle = String.Empty; // normal, italic, oblique

        private String fontVariant = String.Empty; // normal, smallcaps

        private String textTransform = String.Empty; // none, capitalize, uppercase, lowercase

        private String textDecoration = String.Empty; // none, underline, overline, line-through, blink

        private String color = String.Empty;

        private String backgroundColor = String.Empty;

        #endregion


        #region Private Properties - Block Properties

        protected String width = String.Empty;

        protected String widthUnit = "px";

        protected String height = String.Empty;

        protected String heightUnit = "px";

        private String lineHeight = String.Empty;

        private String lineHeightUnit = "%";

        private String verticalAlign = String.Empty; // baseline, bottom, middle, sub, super, text-bottom, text-top, top

        private String textAlign = String.Empty; // center, justify, left, right

        private String textIndent = String.Empty; // String.Empty, or Double

        private String textIndentUnit = "px";

        private String whiteSpace = String.Empty; // String.Empty, normal, nowrap, pre, pre-line, pre-wrap

        private String wordSpacing = String.Empty; // String.Empty, normal, [Double]

        private String wordSpacingUnit = "px";

        private String letterSpacing = String.Empty; // String.Empty, normal, [Double]

        private String letterSpacingUnit = "px";

        private String overflow = String.Empty; // String.Empty, visible, hidden, scroll, auto

        #endregion


        #region Private Properties - Border

        Boolean isBorderSame = true;


        String borderTopStyle = String.Empty;

        String borderTopWidth = String.Empty;

        String borderTopWidthUnit = String.Empty;

        String borderTopColor = String.Empty;


        String borderLeftStyle = String.Empty;

        String borderLeftWidth = String.Empty;

        String borderLeftWidthUnit = String.Empty;

        String borderLeftColor = String.Empty;


        String borderBottomStyle = String.Empty;

        String borderBottomWidth = String.Empty;

        String borderBottomWidthUnit = String.Empty;

        String borderBottomColor = String.Empty;


        String borderRightStyle = String.Empty;

        String borderRightWidth = String.Empty;

        String borderRightWidthUnit = String.Empty;

        String borderRightColor = String.Empty;

        #endregion


        #region Private Properties - Padding and Margins

        private String padding = String.Empty;

        private String margin = String.Empty;

        #endregion


        #region Public Properties

        public Control ParentControl { get { return parentControl; } set { parentControl = value; } }
        
        public String StyleAttributeTextOnly {

            get {

                String styleAttribute = String.Empty;

                #region Font Properties

                styleAttribute = styleAttribute + "font-family: " + SlFontFamily + ";";

                styleAttribute = styleAttribute + "font-size: " + SlFontSize.ToString () + "px;"; 

                styleAttribute = styleAttribute + "font-weight: " + SlFontWeight.ToString () + ";"; 

                if (!String.IsNullOrEmpty (FontStyle)) { styleAttribute = styleAttribute +  "font-style: " + FontStyle + ";"; }

                if (!String.IsNullOrEmpty (FontVariant)) { styleAttribute = styleAttribute +  "font-variant: " + FontVariant + ";"; }

                if (!String.IsNullOrEmpty (TextTransform)) { styleAttribute = styleAttribute +  "text-transform: " + TextTransform + ";"; }

                if (!String.IsNullOrEmpty (TextDecoration)) { styleAttribute = styleAttribute +  "text-decoration: " + TextDecoration + ";"; }

                if (!String.IsNullOrEmpty (Color)) { styleAttribute = styleAttribute +  "color: " + Color + ";"; }

                if (!String.IsNullOrEmpty (BackgroundColor)) { styleAttribute = styleAttribute +  "background-color: " + BackgroundColor + ";"; }

                #endregion


                #region Block Properties

                if (!String.IsNullOrEmpty (LineHeight)) { styleAttribute = styleAttribute +  "line-height: " + LineHeight + LineHeightUnit + ";"; }

                if (!String.IsNullOrEmpty (VerticalAlign)) { styleAttribute = styleAttribute +  "vertical-align: " + VerticalAlign + ";"; }

                if (!String.IsNullOrEmpty (TextAlign)) { styleAttribute = styleAttribute +  "text-align: " + TextAlign + ";"; }

                if (!String.IsNullOrEmpty (TextIndent)) { styleAttribute = styleAttribute +  "text-indent: " + TextIndent + TextIndentUnit + ";"; }

                if (!String.IsNullOrEmpty (WhiteSpace)) { styleAttribute = styleAttribute +  "white-space: " + WhiteSpace + ";"; }

                if (!String.IsNullOrEmpty (WordSpacing)) { styleAttribute = styleAttribute +  "word-spacing: " + WordSpacing + WordSpacingUnit + ";"; }

                if (!String.IsNullOrEmpty (LetterSpacing)) { styleAttribute = styleAttribute +  "letter-spacing: " + LetterSpacing + LetterSpacingUnit + ";"; }

                #endregion

                return styleAttribute.ToString ();

            }

        }

        #endregion 


        #region Public Properties - Font

        public String FontFamily { 
            
            get { return fontFamily; } 
            
            set {

                if (fontFamily != value) {

                    fontFamily = value;

                    NotifyPropertyChanged ("SlFontFamily");

                }
                
            } 
            
        }

        public String FontSize {

            get { return fontSize; }

            set {

                if (fontSize != SetValueDouble (value, String.Empty)) {

                    fontSize = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("SlFontSize");

                }

            }

        }

        public String FontSizeUnit { get { return fontSizeUnit; } set { fontSizeUnit = SetUnit (value); NotifyPropertyChanged ("SlFontSize"); } }

        public String FontWeight { get { return fontWeight; } set { fontWeight = SetValue (value, "normal;bold;bolder;lighter", String.Empty); NotifyPropertyChanged ("SlFontWeight"); } }

        public String FontStyle { get { return fontStyle; } set { fontStyle = SetValue (value, "normal;italic;oblique", String.Empty); NotifyPropertyChanged ("SlFontStyle"); } }

        public String FontVariant { get { return fontVariant; } set { fontVariant = SetValue (value, "normal;smallcaps", String.Empty); NotifyPropertyChanged (""); } }

        public String TextTransform { get { return textTransform; } set { textTransform = SetValue (value, "normal;capitalize;uppercase;lowercase", String.Empty); NotifyPropertyChanged (""); } }

        public String TextDecoration { get { return textDecoration; } set { textDecoration = SetValue (value, "none;underline;overline;line-through;blink", String.Empty); NotifyPropertyChanged ("SlTextDecoration"); } }

        public String Color {

            get { return color; }

            set {

                if (color != value) {

                    color = value;

                    NotifyPropertyChanged ("SlForeground");

                }

            }

        }

        public String BackgroundColor {

            get { return backgroundColor; }

            set {

                if (backgroundColor != value) {

                    backgroundColor = value;

                    NotifyPropertyChanged ("SlBackground");

                }

            }

        }

        #endregion


        #region Public Properties - Block

        public String Width {

            get { return width; }

            set {

                if (width != value) {

                    width = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("SlWidth");

                }

            }

        }

        public String WidthUnit {

            get { return widthUnit; }

            set {

                if (widthUnit != value) {

                    widthUnit = value;

                    NotifyPropertyChanged ("SlWidth");

                }

            }

        }

        public String Height {

            get { return height; }

            set {

                if (height != value) {

                    height = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("SlHeight");

                }

            }

        }

        public String HeightUnit {

            get { return heightUnit; }

            set {

                if (heightUnit != value) {

                    heightUnit = value;

                    NotifyPropertyChanged ("SlHeight");

                }

            }

        }

        public String LineHeight {

            get { return lineHeight; }

            set {

                if (lineHeight != SetValueDouble (value, String.Empty)) {

                    lineHeight = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("LineHeight");

                    NotifyPropertyChanged ("SlLineHeight");

                }

            }

        }

        public String LineHeightUnit { get { return lineHeightUnit; } set { lineHeightUnit = SetUnit (value); } }

        public String VerticalAlign {

            get { return verticalAlign; }

            set {

                if (verticalAlign != value) {

                    verticalAlign = SetValue (value, "baseline;bottom;middle;sub;super;text-bottom;text-top;top", String.Empty);

                    NotifyPropertyChanged ("VerticalAlign");

                    NotifyPropertyChanged ("SlVerticalAlignment");

                }

            }

        }

        public String TextAlign { get { return textAlign; } set { textAlign = SetValue (value, "center;justify;left;right", String.Empty); } }

        public String TextIndent { get { return textIndent; } set { textIndent = SetValueDouble (value, String.Empty); } }

        public String TextIndentUnit { get { return textIndentUnit; } set { textIndentUnit = SetUnit (value); } }

        public String WhiteSpace { get { return whiteSpace; } set { whiteSpace = SetValue (value, "normal;nowrap;pre;pre-line;pre-wrap", String.Empty); } }

        public String WordSpacing { get { return wordSpacing; } set { wordSpacing = SetValueDouble (value, String.Empty); } }

        public String WordSpacingUnit { get { return wordSpacingUnit; } set { wordSpacingUnit = SetUnit (value); } }

        public String LetterSpacing { get { return letterSpacing; } set { letterSpacing = SetValueDouble (value, String.Empty); } }

        public String LetterSpacingUnit { get { return letterSpacingUnit; } set { letterSpacingUnit = SetUnit (value); } }

        public String Overflow { get { return overflow; } set { overflow = SetValue (value, "visible;hidden;scroll;auto", String.Empty); } }

        #endregion


        #region Public Properties - Border

        public Boolean IsBorderSame {

            get { return isBorderSame; }

            set {

                if (isBorderSame != value) {

                    isBorderSame = value;

                    NotifyPropertyChanged ("IsBorderSame");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }


        public String BorderTopStyle {

            get { return borderTopStyle; }

            set {

                if (borderTopStyle != value) {

                    borderTopStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty);

                    NotifyPropertyChanged ("BorderTopStyle");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderTopWidth {

            get { return borderTopWidth; }

            set {

                if (borderTopWidth != value) {

                    borderTopWidth = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("BorderTopWidth");

                    NotifyPropertyChanged ("SlBorderBrush");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderTopWidthUnit { get { return borderTopWidthUnit; } set { borderTopWidthUnit = SetUnit (value); } }

        public String BorderTopColor {

            get { return borderTopColor; }

            set {

                if (borderTopColor != value) {

                    borderTopColor = value;

                    NotifyPropertyChanged ("SlBorderBrush");

                }

            }

        }


        public String BorderLeftStyle {

            get { return borderLeftStyle; }

            set {

                if (borderLeftStyle != value) {

                    borderLeftStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty);

                    NotifyPropertyChanged ("BorderLeftStyle");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderLeftWidth {

            get { return borderLeftWidth; }

            set {

                if (borderLeftWidth != value) {

                    borderLeftWidth = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("BorderLeftWidth");

                    NotifyPropertyChanged ("SlBorderBrush");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderLeftWidthUnit { get { return borderLeftWidthUnit; } set { borderLeftWidthUnit = SetUnit (value); } }

        public String BorderLeftColor {

            get { return borderLeftColor; }

            set {

                if (borderLeftColor != value) {

                    borderLeftColor = value;

                    NotifyPropertyChanged ("SlBorderBrush");

                }

            }

        }


        public String BorderBottomStyle {

            get { return borderBottomStyle; }

            set {

                if (borderBottomStyle != value) {

                    borderBottomStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty);

                    NotifyPropertyChanged ("BorderBottomStyle");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderBottomWidth {

            get { return borderBottomWidth; }

            set {

                if (borderBottomWidth != value) {

                    borderBottomWidth = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("BorderBottomWidth");

                    NotifyPropertyChanged ("SlBorderBrush");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderBottomWidthUnit { get { return borderBottomWidthUnit; } set { borderBottomWidthUnit = SetUnit (value); } }

        public String BorderBottomColor {

            get { return borderBottomColor; }

            set {

                if (borderBottomColor != value) {

                    borderBottomColor = value;

                    NotifyPropertyChanged ("SlBorderBrush");

                }

            }

        }


        public String BorderRightStyle {

            get { return borderRightStyle; }

            set {

                if (borderRightStyle != value) {

                    borderRightStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty);

                    NotifyPropertyChanged ("BorderRightStyle");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderRightWidth {

            get { return borderRightWidth; }

            set {

                if (borderRightWidth != value) {

                    borderRightWidth = SetValueDouble (value, String.Empty);

                    NotifyPropertyChanged ("BorderRightWidth");

                    NotifyPropertyChanged ("SlBorderBrush");

                    NotifyPropertyChanged ("SlBorderThickness");

                }

            }

        }

        public String BorderRightWidthUnit { get { return borderRightWidthUnit; } set { borderRightWidthUnit = SetUnit (value); } }

        public String BorderRightColor {

            get { return borderRightColor; }

            set {

                if (borderRightColor != value) {

                    borderRightColor = value;

                    NotifyPropertyChanged ("SlBorderBrush");

                }

            }

        }

        #endregion


        #region Public Properties - Padding and Margin

        public String Padding {

            get { return padding; }

            set {

                if (padding != value) {

                    padding = value;

                    NotifyPropertyChanged ("SlPadding");

                }

            }

        }

        public String Margin {

            get { return margin; }

            set {

                if (margin != value) {

                    margin = value;

                    NotifyPropertyChanged ("SlMargin");

                }

            }

        }

        #endregion 
    


        #region Constructors

        public Style (Control forParentControl) {

            parentControl = forParentControl; 

            return; 
        
        }

        public Style (Control forParentControl, Server.Application.FormControlStyle serverStyle) {

            parentControl = forParentControl;

            MapServerObject (serverStyle);

            return;

        }

        public void MapServerObject (Server.Application.FormControlStyle serverStyle) {

            #region Font Properties

            FontFamily = serverStyle.FontFamily;

            FontSize = serverStyle.FontSize;

            FontSizeUnit = serverStyle.FontSizeUnit;

            FontWeight = serverStyle.FontWeight;

            FontStyle = serverStyle.FontStyle;

            FontVariant = serverStyle.FontVariant;

            TextTransform = serverStyle.TextTransform;

            TextDecoration = serverStyle.TextDecoration;

            Color = serverStyle.Color;

            BackgroundColor = serverStyle.BackgroundColor;

            #endregion


            #region Block Properties

            Width = serverStyle.Width;

            WidthUnit = serverStyle.WidthUnit;

            Height = serverStyle.Height;

            HeightUnit = serverStyle.HeightUnit;

            LineHeight = serverStyle.LineHeight;

            LineHeightUnit = serverStyle.LineHeightUnit;

            VerticalAlign = serverStyle.VerticalAlign;

            TextAlign = serverStyle.TextAlign;

            TextIndent = serverStyle.TextIndent;

            TextIndentUnit = serverStyle.TextIndentUnit;

            WhiteSpace = serverStyle.WhiteSpace;

            WordSpacing = serverStyle.WordSpacing;

            WordSpacingUnit = serverStyle.WordSpacingUnit;

            LetterSpacing = serverStyle.LetterSpacing;

            Overflow = serverStyle.Overflow;

            #endregion

                
            #region Private Properties - Border

            IsBorderSame = serverStyle.IsBorderSame;


            BorderTopStyle = serverStyle.BorderTopStyle;

            BorderTopWidth = serverStyle.BorderTopWidth;

            BorderTopWidthUnit = serverStyle.BorderTopWidthUnit;

            BorderTopColor = serverStyle.BorderTopColor;


            BorderLeftStyle = serverStyle.BorderLeftStyle;

            BorderLeftWidth = serverStyle.BorderLeftWidth;

            BorderLeftWidthUnit = serverStyle.BorderLeftWidthUnit;

            BorderLeftColor = serverStyle.BorderLeftColor;


            BorderBottomStyle = serverStyle.BorderBottomStyle;

            BorderBottomWidth = serverStyle.BorderBottomWidth;

            BorderBottomWidthUnit = serverStyle.BorderBottomWidthUnit;

            BorderBottomColor = serverStyle.BorderBottomColor;


            BorderRightStyle = serverStyle.BorderRightStyle;

            BorderRightWidth = serverStyle.BorderRightWidth;

            BorderRightWidthUnit = serverStyle.BorderRightWidthUnit;

            BorderRightColor = serverStyle.BorderRightColor;

            #endregion


            #region Private Properties - Padding and Margins

            Padding = serverStyle.Padding;

            Margin = serverStyle.Margin;

            #endregion


            return;

        }

        #endregion



        #region Public Methods - Set Value

        public String SetUnit (String originalUnit) {

            String unit = "px";

            switch (originalUnit.ToLower ()) {

                case "px":

                case "pt":

                case "in":

                case "cm":

                case "mm":

                case "pc":

                case "em":

                case "ex":

                case "%":

                    unit = originalUnit.ToLower ();

                    break;

            }


            return unit;

        }

        public String SetValue (String originalValue, String allowedValues, String defaultValue) {

            String value = String.Empty;

            String[] valueList = allowedValues.Split (';');

            for (Int32 currentIndex = 0; currentIndex < valueList.Length; currentIndex++) {

                if (originalValue.ToLower () == valueList[currentIndex].ToString ()) {

                    value = originalValue.ToLower ();

                    break;

                }

            }

            if (string.IsNullOrEmpty (value)) { value = defaultValue; }

            return value;

        }

        public String SetValueDouble (String originalValue, String defaultValue) {

            String value = String.Empty;

            Double doubleResult;

            if (Double.TryParse (originalValue, out doubleResult)) {

                value = originalValue;

            }

            if (String.IsNullOrEmpty (value)) { value = defaultValue; }

            return value;

        }

        #endregion
        

        #region Silverlight Data Binding Support 

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        // PUBLIC NOTIFY ALLOWES "PUSH DOWNS" FROM PARENT CONTROLS

        public virtual void NotifyPropertyChanged (String propertyName) {

            if (PropertyChanged != null) {

                PropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (propertyName));

            }

            if (parentControl.ControlType != Server.Application.FormControlType.Label) {

                parentControl.NotifyStylePropretyChangedToChildren (parentControl.ControlId, propertyName);

            }

            return;

        }

        public virtual void NotifyPropertyChangedReceivedFromParent (String propertyName) {

            if (PropertyChanged != null) {

                PropertyChanged (this, new System.ComponentModel.PropertyChangedEventArgs (propertyName));

            }

            return;

        }

        #endregion 


        #region Silverlight Support Methods

        private Double LengthIntoPixels (String length, String lengthUnit) {

            Double lengthInPixels = Double.NaN;


            try {

                switch (lengthUnit) {

                    case "%": lengthInPixels = (Convert.ToDouble (length) / 100) * formWidth; break;

                    case "in": lengthInPixels = Convert.ToDouble (length) * pixelsPerInch; break;

                    default: lengthInPixels = Convert.ToDouble (length); break;

                }

            }

            catch { /* DO NOTHING */ }

            return lengthInPixels;

        }

        private Thickness MarginPaddingIntoThickness (String paddingMargin) {

            if (String.IsNullOrEmpty (paddingMargin)) { return new Thickness (0); }

            Thickness thickness = new Thickness ();

            Int32 settingIndex = 0;

            Double settingValue = 0;

            foreach (String currentThickness in paddingMargin.Split (' ')) {

                settingValue = 0;

                if (currentThickness.ToUpper ().Contains ("PX")) {

                    Double.TryParse (currentThickness.ToUpper ().Replace ("PX", ""), out settingValue);

                }

                else if (currentThickness.ToUpper ().Contains ("IN")) {

                    if (Double.TryParse (currentThickness.ToUpper ().Replace ("IN", ""), out settingValue)) {

                        settingValue = settingValue * pixelsPerInch;

                    }

                }

                else { Double.TryParse (currentThickness, out settingValue); }

                switch (settingIndex) {

                    case 0:

                        thickness = new Thickness (settingValue);

                        break;

                    case 1:

                        thickness.Right = settingValue;

                        break;

                    case 2:

                        thickness.Bottom = settingValue;

                        break;

                    case 3:

                        thickness.Left = settingValue;

                        break;

                }

                settingIndex = settingIndex + 1;

            }

            return thickness;

        }

        private Brush WebColorToSolidColorBrush (String webColor) {

            SolidColorBrush colorBrush = new SolidColorBrush (Colors.Transparent);

            if (String.IsNullOrEmpty (webColor)) { return colorBrush; }

            byte a = 255;

            String htmlColor = webColor.Replace ("#", String.Empty);

            if (htmlColor.Length == 8) {

                a = System.Convert.ToByte (htmlColor.Substring (0, 2), 16);

                htmlColor = htmlColor.Substring (2, 6);

            }

            if (htmlColor.Length == 6) {

                byte r = System.Convert.ToByte (htmlColor.Substring (0, 2), 16);

                byte g = System.Convert.ToByte (htmlColor.Substring (2, 2), 16);

                byte b = System.Convert.ToByte (htmlColor.Substring (4, 2), 16);

                colorBrush = new SolidColorBrush (System.Windows.Media.Color.FromArgb (a, r, g, b));

            }

            else {

                switch (webColor.ToLower ()) {

                    case "black": colorBrush = new SolidColorBrush (Colors.Black); break;

                }

            }

            return colorBrush;

        }

        #endregion 


        #region Silverlight Framework Element Support - Font

        public FontFamily SlFontFamily {  
            
            get {

                if (String.IsNullOrEmpty (fontFamily)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return parentControl.Parent.Style.SlFontFamily;

                    }

                    else { return new FontFamily ("Arial"); }

                }

                return new FontFamily (fontFamily); 
            
            } 
        
        }

        public Double SlFontSize {

            get {

                Double pointsToPixels = 96.0 / 72.0;

                if (String.IsNullOrEmpty (fontSize)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return parentControl.Parent.Style.SlFontSize;

                    }

                    else { return Convert.ToDouble (Convert.ToInt32 (10 * pointsToPixels)); }

                }

                Double slFontSize = 0;

                if (Double.TryParse (fontSize, out slFontSize)) {

                    slFontSize = slFontSize * pointsToPixels;

                }

                return Convert.ToDouble (Convert.ToInt32 (slFontSize));
         
            }

        }

        public System.Windows.FontWeight SlFontWeight {

            get {

                if (String.IsNullOrEmpty (fontWeight)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {
                    
                        return parentControl.Parent.Style.SlFontWeight;

                    }

                    else { return FontWeights.Normal; }

                }


                System.Windows.FontWeight slFontWeight = FontWeights.Normal;

                try {

                    if (FontWeights.Normal.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Normal; }

                    else if (FontWeights.Bold.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Bold; }

                    else if (FontWeights.Black.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Black; }

                    else if (FontWeights.ExtraBlack.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.ExtraBlack; }

                    else if (FontWeights.ExtraBold.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.ExtraBold; }

                    else if (FontWeights.ExtraLight.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.ExtraLight; }

                    else if (FontWeights.Light.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Light; }

                    else if (FontWeights.Medium.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Medium; }

                    else if (FontWeights.SemiBold.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.SemiBold; }

                    else if (FontWeights.Thin.ToString ().Trim ().ToUpper () == fontWeight.Trim ().ToUpper ()) { slFontWeight = FontWeights.Thin; }

                }

                catch { /* DO NOTHING */ }

                return slFontWeight;

            }

        }

        public FontStyle SlFontStyle {

            get {

                if (String.IsNullOrEmpty (fontStyle)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return parentControl.Parent.Style.SlFontStyle;

                    }

                    else { return FontStyles.Normal; }

                }

                return (fontStyle.ToUpper ().Contains ("ITALIC")) ? FontStyles.Italic : FontStyles.Normal;

            }

        }

        public TextWrapping SlTextWrapping { get { return (WhiteSpace.ToUpper ().Contains ("NOWRAP") ? TextWrapping.NoWrap : TextWrapping.Wrap); } }

        public TextDecorationCollection SlTextDecorations {

            get {

                if (textDecoration.ToUpper ().Contains ("UNDERLINE")) {

                    return TextDecorations.Underline;

                }

                else { return null; }

            }

        }

        public System.Windows.TextAlignment SlTextAlignment {

            get {

                System.Windows.TextAlignment alignment = TextAlignment.Left;

                if (!String.IsNullOrEmpty (textAlign)) {

                    switch (textAlign.ToUpper ().Trim ()) {

                        case "CENTER": alignment = TextAlignment.Center; break;

                        case "RIGHT": alignment = TextAlignment.Right; break;

                    }

                }

                return alignment;

            }

        }

        public Brush SlForeground {

            get {

                Brush foregroundColor = new SolidColorBrush (Colors.Black);

                if (String.IsNullOrEmpty (color)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return parentControl.Parent.Style.SlForeground;

                    }

                }

                else {

                    foregroundColor = WebColorToSolidColorBrush (color);

                }

                return foregroundColor;

            }

        }

        public Brush SlBackground {

            get {

                if (String.IsNullOrEmpty (backgroundColor)) { return new SolidColorBrush (Colors.Transparent); }

                return WebColorToSolidColorBrush (backgroundColor);

            }

        }

        #endregion 


        #region Silverlight Framework Element Support - Block

        public Double SlWidth {

            get {

                Double slWidth = Double.NaN;

                if (!String.IsNullOrEmpty (width)) {

                    if (!((width == "100") && (widthUnit == "%"))) {

                        slWidth = LengthIntoPixels (width, widthUnit);

                    }

                }

                return slWidth;

            }

        }

        public GridLength SlGridWidth {

            get {

                if (String.IsNullOrEmpty (width)) { return GridLength.Auto; }

                GridLength length = GridLength.Auto;

                switch (widthUnit) {

                    case "%": length = new GridLength (Convert.ToDouble (width) / 100, GridUnitType.Star); break;

                    case "px": length = new GridLength (Convert.ToDouble (width), GridUnitType.Pixel); break;

                }

                return length;

            }

        }

        public Double SlHeight {

            get {

                Double slHeight = Double.NaN;

                if (!String.IsNullOrEmpty (height)) {

                    if (!((height == "100") && (heightUnit == "%"))) {

                        slHeight = LengthIntoPixels (height, heightUnit);

                    }

                }

                return slHeight;

            }

        }

        public GridLength SlGridHeight {

            get {

                if (String.IsNullOrEmpty (height)) { return GridLength.Auto; }

                GridLength length = GridLength.Auto;

                switch (heightUnit) {

                    case "%": length = new GridLength (Convert.ToDouble (height) / 100, GridUnitType.Star); break;

                    case "px": length = new GridLength (Convert.ToDouble (height), GridUnitType.Pixel); break;

                }

                return length;

            }

        }

        public Double SlLineHeight {

            get {

                if (String.IsNullOrEmpty (lineHeight)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return (parentControl.Parent.Style.SlLineHeight);

                    }

                    else { return SlFontSize; }

                }

                return ((Convert.ToDouble (lineHeight) / 100) * SlFontSize);

            }

        }

        public Thickness SlMargin { get { return MarginPaddingIntoThickness (margin); } }

        public Thickness SlPadding { get { return MarginPaddingIntoThickness (padding); } }

        public Brush SlBorderBrush { get { return WebColorToSolidColorBrush (borderTopColor); } }

        public Thickness SlBorderThickness { 
            
            get { 

                Int32 widthTop = 0;

                Int32 widthRight = 0;

                Int32 widthBottom = 0;

                Int32 widthLeft = 0;

                Thickness borderThickness = new Thickness (0);


                if ((isBorderSame) && (String.IsNullOrEmpty (borderTopStyle))) { return new Thickness (0); }

                if (isBorderSame) {

                    if (Int32.TryParse (borderTopWidth, out widthTop)) {

                        borderThickness = new Thickness (widthTop);

                    }

                }

                else {

                    if (!String.IsNullOrEmpty (borderTopStyle)) { Int32.TryParse (borderTopWidth, out widthTop); }

                    if (!String.IsNullOrEmpty (borderRightStyle)) { Int32.TryParse (borderRightWidth, out widthRight); }

                    if (!String.IsNullOrEmpty (borderBottomStyle)) { Int32.TryParse (borderBottomWidth, out widthBottom); }

                    if (!String.IsNullOrEmpty (borderLeftStyle)) { Int32.TryParse (borderLeftWidth, out widthLeft); }

                    borderThickness = new Thickness (widthLeft, widthTop, widthRight, widthBottom);

                }

                return borderThickness;
            
            } 
        
        }
        
        public VerticalAlignment SlVerticalAlignment {

            get {

                VerticalAlignment verticalAlignment = VerticalAlignment.Center;

                if (String.IsNullOrEmpty (verticalAlign)) {

                    if ((parentControl.Parent != null) && (parentControl.ControlType != Server.Application.FormControlType.Form)) {

                        return parentControl.Parent.Style.SlVerticalAlignment;

                    }

                    else { return VerticalAlignment.Center; }

                }

                switch (verticalAlign.ToLower ()) {

                    case "baseline":

                    case "bottom": 

                    case "text-bottom":

                    case "sub": 

                        verticalAlignment = VerticalAlignment.Bottom;

                        break;

                    case "middle":

                        verticalAlignment = VerticalAlignment.Center;

                        break;

                    case "top":

                    case "text-top":

                    case "super":

                        verticalAlignment = VerticalAlignment.Top;

                        break;

                }

                return verticalAlignment;

            }

        }

        #endregion 


        #region Public Methods

        public Server.Application.FormControlStyle ToServerObject () {

            Server.Application.FormControlStyle serverStyle = new Server.Application.FormControlStyle ();


            serverStyle.BackgroundColor = BackgroundColor;

            serverStyle.BorderBottomColor = BorderBottomColor;

            serverStyle.BorderBottomStyle = BorderBottomStyle;

            serverStyle.BorderBottomWidth = BorderBottomWidth;

            serverStyle.BorderBottomWidthUnit = BorderBottomWidthUnit;

            serverStyle.BorderLeftColor = BorderLeftColor;

            serverStyle.BorderLeftStyle = BorderLeftStyle;

            serverStyle.BorderLeftWidth = BorderLeftWidth;

            serverStyle.BorderLeftWidthUnit = BorderLeftWidthUnit;

            serverStyle.BorderRightColor = BorderRightColor;

            serverStyle.BorderRightStyle = BorderRightStyle;

            serverStyle.BorderRightWidth = BorderRightWidth;

            serverStyle.BorderRightWidthUnit = BorderRightWidthUnit;

            serverStyle.BorderTopColor = BorderTopColor;

            serverStyle.BorderTopStyle = BorderTopStyle;

            serverStyle.BorderTopWidth = BorderTopWidth;

            serverStyle.BorderTopWidthUnit = BorderTopWidthUnit;

            serverStyle.Color = Color;

            serverStyle.FontFamily = FontFamily;

            serverStyle.FontSize = FontSize;

            serverStyle.FontSizeUnit = FontSizeUnit;

            serverStyle.FontStyle = FontStyle;

            serverStyle.FontVariant = FontVariant;

            serverStyle.FontWeight = FontWeight;

            serverStyle.Height = Height;

            serverStyle.HeightUnit = HeightUnit;

            serverStyle.IsBorderSame = IsBorderSame;

            serverStyle.LetterSpacing = LetterSpacing;

            serverStyle.LetterSpacingUnit = LetterSpacingUnit;

            serverStyle.LineHeight = LineHeight;

            serverStyle.LineHeightUnit = LineHeightUnit;


            serverStyle.Margin = Margin;

            serverStyle.Overflow = Overflow;


            serverStyle.Padding = Padding;

            serverStyle.TextAlign = TextAlign;

            serverStyle.TextDecoration = TextDecoration;

            serverStyle.TextIndent = TextIndent;

            serverStyle.TextIndentUnit = TextIndentUnit;

            serverStyle.TextTransform = TextTransform;

            serverStyle.VerticalAlign = VerticalAlign;

            serverStyle.WhiteSpace = WhiteSpace;

            serverStyle.Width = Width;

            serverStyle.WidthUnit = WidthUnit;

            serverStyle.WordSpacing = WordSpacing;

            serverStyle.WordSpacingUnit = WordSpacingUnit;

            return serverStyle;

        }

        public Style Copy () {

            return new Style (parentControl, ToServerObject ()); 

        }

        #endregion 

    }

}
