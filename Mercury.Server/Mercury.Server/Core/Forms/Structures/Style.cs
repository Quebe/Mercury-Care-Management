using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Mercury.Server.Core.Forms.Structures {

    [Serializable]
    [DataContract (Name = "FormControlStyle")]
    public class Style {

        #region Private Properties - Font Properties

        [DataMember (Name = "FontFamily")]
        private String fontFamily = String.Empty;

        [DataMember (Name = "FontSize")]
        private String fontSize = String.Empty;

        [DataMember (Name = "FontSizeUnit")]
        private String fontSizeUnit = "px"; // px, pt, %

        [DataMember (Name = "FontWeight")]
        private String fontWeight = String.Empty; // normal, bold, bolder, lighter

        [DataMember (Name = "FontStyle")]
        private String fontStyle = String.Empty; // normal, italic, oblique

        [DataMember (Name = "FontVariant")]
        private String fontVariant = String.Empty; // normal, smallcaps

        [DataMember (Name = "TextTransform")]
        private String textTransform = String.Empty; // none, capitalize, uppercase, lowercase

        [DataMember (Name = "TextDecoration")]
        private String textDecoration = String.Empty; // none, underline, overline, line-through, blink

        [DataMember (Name = "Color")]
        private String color = String.Empty;

        [DataMember (Name = "BackgroundColor")]
        private String backgroundColor = String.Empty;

        #endregion


        #region Private Properties - Block Properties

        [DataMember (Name = "Width")]
        protected String width = String.Empty;

        [DataMember (Name = "WidthUnit")]
        protected String widthUnit = "px";

        [DataMember (Name = "Height")]
        protected String height = String.Empty;

        [DataMember (Name = "HeightUnit")]
        protected String heightUnit = "px";

        [DataMember (Name = "LineHeight")]
        private String lineHeight = String.Empty;

        [DataMember (Name = "LineHeightUnit")]
        private String lineHeightUnit = "%";

        [DataMember (Name = "VerticalAlign")]
        private String verticalAlign = String.Empty; // baseline, bottom, middle, sub, super, text-bottom, text-top, top

        [DataMember (Name = "TextAlign")]
        private String textAlign = String.Empty; // center, justify, left, right

        [DataMember (Name = "TextIndent")]
        private String textIndent = String.Empty; // String.Empty, or Double

        [DataMember (Name = "TextIndentUnit")]
        private String textIndentUnit = "px";

        [DataMember (Name = "WhiteSpace")]
        private String whiteSpace = String.Empty; // String.Empty, normal, nowrap, pre, pre-line, pre-wrap

        [DataMember (Name = "WordSpacing")]
        private String wordSpacing = String.Empty; // String.Empty, normal, [Double]

        [DataMember (Name = "WordSpacingUnit")]
        private String wordSpacingUnit = "px";

        [DataMember (Name = "LetterSpacing")]
        private String letterSpacing = String.Empty; // String.Empty, normal, [Double]

        [DataMember (Name = "LetterSpacingUnit")]
        private String letterSpacingUnit = "px";

        [DataMember (Name = "Overflow")]
        private String overflow = String.Empty; // String.Empty, visible, hidden, scroll, auto

        #endregion


        #region Private Properties - Border

        [DataMember (Name = "IsBorderSame")]
        Boolean isBorderSame = true;


        [DataMember (Name = "BorderTopStyle")]
        String borderTopStyle = String.Empty;

        [DataMember (Name = "BorderTopWidth")]
        String borderTopWidth = String.Empty;

        [DataMember (Name = "BorderTopWidthUnit")]
        String borderTopWidthUnit = String.Empty;

        [DataMember (Name = "BorderTopColor")]
        String borderTopColor = String.Empty;


        [DataMember (Name = "BorderLeftStyle")]
        String borderLeftStyle = String.Empty;

        [DataMember (Name = "BorderLeftWidth")]
        String borderLeftWidth = String.Empty;

        [DataMember (Name = "BorderLeftWidthUnit")]
        String borderLeftWidthUnit = String.Empty;

        [DataMember (Name = "BorderLeftColor")]
        String borderLeftColor = String.Empty;


        [DataMember (Name = "BorderBottomStyle")]
        String borderBottomStyle = String.Empty;

        [DataMember (Name = "BorderBottomWidth")]
        String borderBottomWidth = String.Empty;

        [DataMember (Name = "BorderBottomWidthUnit")]
        String borderBottomWidthUnit = String.Empty;

        [DataMember (Name = "BorderBottomColor")]
        String borderBottomColor = String.Empty;


        [DataMember (Name = "BorderRightStyle")]
        String borderRightStyle = String.Empty;

        [DataMember (Name = "BorderRightWidth")]
        String borderRightWidth = String.Empty;

        [DataMember (Name = "BorderRightWidthUnit")]
        String borderRightWidthUnit = String.Empty;

        [DataMember (Name = "BorderRightColor")]
        String borderRightColor = String.Empty;

        #endregion


        #region Private Properties - Padding and Margins

        [DataMember (Name = "IsPaddingSame")]
        private Boolean isPaddingSame = true;


        [DataMember (Name = "PaddingTop")]
        private String paddingTop = String.Empty;

        [DataMember (Name = "PaddingTopUnit")]
        private String paddingTopUnit = String.Empty;

        [DataMember (Name = "PaddingLeft")]
        private String paddingLeft = String.Empty;

        [DataMember (Name = "PaddingLeftUnit")]
        private String paddingLeftUnit = String.Empty;

        [DataMember (Name = "PaddingBottom")]
        private String paddingBottom = String.Empty;

        [DataMember (Name = "PaddingBottomUnit")]
        private String paddingBottomUnit = String.Empty;

        [DataMember (Name = "PaddingRight")]
        private String paddingRight = String.Empty;

        [DataMember (Name = "PaddingRightUnit")]
        private String paddingRightUnit = String.Empty;

        [DataMember (Name = "Padding")]
        private String padding = String.Empty;


        [DataMember (Name = "IsMarginSame")]
        private Boolean isMarginSame = true;


        [DataMember (Name = "MarginTop")]
        private String marginTop = String.Empty;

        [DataMember (Name = "MarginTopUnit")]
        private String marginTopUnit = String.Empty;

        [DataMember (Name = "MarginLeft")]
        private String marginLeft = String.Empty;

        [DataMember (Name = "MarginLeftUnit")]
        private String marginLeftUnit = String.Empty;

        [DataMember (Name = "MarginBottom")]
        private String marginBottom = String.Empty;

        [DataMember (Name = "MarginBottomUnit")]
        private String marginBottomUnit = String.Empty;

        [DataMember (Name = "MarginRight")]
        private String marginRight = String.Empty;

        [DataMember (Name = "MarginRightUnit")]
        private String marginRightUnit = String.Empty;

        [DataMember (Name = "Margin")]
        private String margin = String.Empty;

        #endregion



        #region Public Properties - Font

        public String FontFamily { get { return fontFamily; } set { fontFamily = value; } }

        public String FontSize { get { return fontSize; } set { fontSize = SetValueDouble (value, String.Empty); } }

        public String FontSizeUnit { get { return fontSizeUnit; } set { fontSizeUnit = SetUnit (value); } }
        
        public Double FontSizeToPixels {

            get {

                if (String.IsNullOrWhiteSpace (fontSize)) { return 0.1; }

                    


                Double size = 0.0f;

                Double pixels = 0.1f;

                if (!Double.TryParse (fontSize, out size)) { return 0.1; }


                switch (fontSizeUnit.ToUpper ()) {

                    case "PT": pixels = size * 96.0f / 72.0f; break;

                    default: pixels = size; break; // DEFAULT TO PIXELS

                }

                return pixels;

            }

        }

        public String FontWeight { get { return fontWeight; } set { fontWeight = SetValue (value, "normal;bold;bolder;lighter", String.Empty); } }

        public String FontStyle { get { return fontStyle; } set { fontStyle = SetValue (value, "normal;italic;oblique", String.Empty); } }

        public String FontVariant { get { return fontVariant; } set { fontVariant = SetValue (value, "normal;smallcaps", String.Empty); } }

        public String TextTransform { get { return textTransform; } set { textTransform = SetValue (value, "normal;capitalize;uppercase;lowercase", String.Empty); } }

        public String TextDecoration { get { return textDecoration; } set { textDecoration = SetValue (value, "none;underline;overline;line-through;blink", String.Empty); } }

        public String Color { get { return color; } set { color = value; } }

        public String BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }

        #endregion 


        #region Public Properties - Block

        public String Width { get { return width; } set { width = SetValueDouble (value, String.Empty); } }

        public String WidthUnit { get { return widthUnit; } set { widthUnit = SetUnit (value); } }

        public String Height { get { return height; } set { height = SetValueDouble (value, String.Empty); } }

        public String HeightUnit { get { return heightUnit; } set { heightUnit = SetUnit (value); } }

        public String LineHeight { get { return lineHeight; } set { lineHeight = SetValueDouble (value, String.Empty); } }

        public String LineHeightUnit { get { return lineHeightUnit; } set { lineHeightUnit = SetUnit (value); } }

        public String VerticalAlign { get { return verticalAlign; } set { verticalAlign = SetValue (value, "baseline;bottom;middle;sub;super;text-bottom;text-top;top", String.Empty); } }

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

        public Boolean IsBorderSame { get { return isBorderSame; } set { isBorderSame = value; } }


        public String BorderTopStyle { get { return borderTopStyle; } set { borderTopStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty); } }

        public String BorderTopWidth { get { return borderTopWidth; } set { borderTopWidth = SetValueDouble (value, String.Empty); } }

        public String BorderTopWidthUnit { get { return borderTopWidthUnit; } set { borderTopWidthUnit = SetUnit (value); } }

        public String BorderTopColor { get { return borderTopColor; } set { borderTopColor = value; } }


        public String BorderLeftStyle { get { return borderLeftStyle; } set { borderLeftStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty); } }

        public String BorderLeftWidth { get { return borderLeftWidth; } set { borderLeftWidth = SetValueDouble (value, String.Empty); } }

        public String BorderLeftWidthUnit { get { return borderLeftWidthUnit; } set { borderLeftWidthUnit = SetUnit (value); } }

        public String BorderLeftColor { get { return borderLeftColor; } set { borderLeftColor = value; } }


        public String BorderBottomStyle { get { return borderBottomStyle; } set { borderBottomStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty); } }

        public String BorderBottomWidth { get { return borderBottomWidth; } set { borderBottomWidth = SetValueDouble (value, String.Empty); } }

        public String BorderBottomWidthUnit { get { return borderBottomWidthUnit; } set { borderBottomWidthUnit = SetUnit (value); } }

        public String BorderBottomColor { get { return borderBottomColor; } set { borderBottomColor = value; } }

        
        public String BorderRightStyle { get { return borderRightStyle; } set { borderRightStyle = SetValue (value, "dashed;dotted;double;groove;hidden;inset;none;outset;ridge;solid", String.Empty); } }

        public String BorderRightWidth { get { return borderRightWidth; } set { borderRightWidth = SetValueDouble (value, String.Empty); } }

        public String BorderRightWidthUnit { get { return borderRightWidthUnit; } set { borderRightWidthUnit = SetUnit (value); } }

        public String BorderRightColor { get { return borderRightColor; } set { borderRightColor = value; } }

        #endregion


        #region Public Properties - Padding and Margin

        public Boolean IsPaddingSame { get { return isPaddingSame; } set { isPaddingSame = value; } }

        public String PaddingTop { get { return paddingTop; } set { paddingTop = SetValueDouble (value, String.Empty); } }

        public String PaddingTopUnit { get { return paddingTopUnit; } set { paddingTopUnit = SetUnit (value); } }

        public String PaddingLeft { get { return paddingLeft; } set { paddingLeft = SetValueDouble (value, String.Empty); } }

        public String PaddingLeftUnit { get { return paddingLeftUnit; } set { paddingLeftUnit = SetUnit (value); } }

        public String PaddingBottom { get { return paddingBottom; } set { paddingBottom = SetValueDouble (value, String.Empty); } }

        public String PaddingBottomUnit { get { return paddingBottomUnit; } set { paddingBottomUnit = SetUnit (value); } }

        public String PaddingRight { get { return paddingRight; } set { paddingRight = SetValueDouble (value, String.Empty); } }

        public String PaddingRightUnit { get { return paddingRightUnit; } set { paddingRightUnit = SetUnit (value); } }

        public String Padding { get { return padding; } set { padding = value; } }

        public Boolean IsMarginSame { get { return isMarginSame; } set { isMarginSame = value; } }

        public String MarginTop { get { return marginTop; } set { marginTop = SetValueDouble (value, String.Empty); } }

        public String MarginTopUnit { get { return marginTopUnit; } set { marginTopUnit = SetUnit (value); } }

        public String MarginLeft { get { return marginLeft; } set { marginLeft = SetValueDouble (value, String.Empty); } }

        public String MarginLeftUnit { get { return marginLeftUnit; } set { marginLeftUnit = SetUnit (value); } }

        public String MarginBottom { get { return marginBottom; } set { marginBottom = SetValueDouble (value, String.Empty); } }

        public String MarginBottomUnit { get { return marginBottomUnit; } set { marginBottomUnit = SetUnit (value); } }

        public String MarginRight { get { return marginRight; } set { marginRight = SetValueDouble (value, String.Empty); } }

        public String MarginRightUnit { get { return marginRightUnit; } set { marginRightUnit = SetUnit (value); } }

        public String Margin { get { return margin; } set { margin = value; } }

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


        public Style () {

            return;

        }

        public Style (System.Xml.XmlDocument styleProperties) {

            foreach (System.Xml.XmlNode currentPropertyNode in styleProperties.ChildNodes[0].ChildNodes) {

                switch (currentPropertyNode.Attributes["Name"].InnerText) {

                    #region Font Properties

                    case "FontFamily": FontFamily = currentPropertyNode.InnerText; break;

                    case "FontSize": FontSize = currentPropertyNode.InnerText; break;

                    case "FontSizeUnit": FontSizeUnit = currentPropertyNode.InnerText; break;

                    case "FontWeight": FontWeight = currentPropertyNode.InnerText; break;

                    case "FontVariant": FontVariant = currentPropertyNode.InnerText; break;

                    case "TextTransform": TextTransform = currentPropertyNode.InnerText; break;

                    case "TextDecoration": TextDecoration = currentPropertyNode.InnerText; break;

                    case "Color": Color = currentPropertyNode.InnerText; break;

                    case "BackgroundColor": BackgroundColor = currentPropertyNode.InnerText; break;

                    #endregion


                    #region Block Properties

                    case "Width": Width = currentPropertyNode.InnerText; break;

                    case "WidthUnit": WidthUnit = currentPropertyNode.InnerText; break;

                    case "Height": Height = currentPropertyNode.InnerText; break;

                    case "HeightUnit": HeightUnit = currentPropertyNode.InnerText; break;

                    case "LineHeight": LineHeight = currentPropertyNode.InnerText; break;

                    case "LineHeightUnit": LineHeightUnit = currentPropertyNode.InnerText; break;

                    case "TextAlign": TextAlign = currentPropertyNode.InnerText; break;

                    case "TextIndent": TextIndent = currentPropertyNode.InnerText; break;

                    case "TextIndentUnit": TextIndentUnit = currentPropertyNode.InnerText; break;

                    case "WhiteSpace": WhiteSpace = currentPropertyNode.InnerText; break;

                    case "WordSpacing": WordSpacing = currentPropertyNode.InnerText; break;

                    case "WordSpacingUnit": WordSpacingUnit = currentPropertyNode.InnerText; break;

                    case "LetterSpacing": LetterSpacing = currentPropertyNode.InnerText; break;

                    case "LetterSpacingUnit": LetterSpacingUnit = currentPropertyNode.InnerText; break;

                    #endregion


                    #region Border Properties

                    case "IsBorderSame": IsBorderSame = Boolean.Parse (currentPropertyNode.InnerText); break;

                    case "BorderTopStyle": BorderTopStyle = currentPropertyNode.InnerText; break;

                    case "BorderTopWidth": BorderTopWidth = currentPropertyNode.InnerText; break;

                    case "BorderTopWidthUnit": BorderTopWidthUnit = currentPropertyNode.InnerText; break;

                    case "BorderTopColor": BorderTopColor = currentPropertyNode.InnerText; break;

                    case "BorderLeftStyle": BorderLeftStyle = currentPropertyNode.InnerText; break;

                    case "BorderLeftWidth": BorderLeftWidth = currentPropertyNode.InnerText; break;

                    case "BorderLeftWidthUnit": BorderLeftWidthUnit = currentPropertyNode.InnerText; break;

                    case "BorderLeftColor": BorderLeftColor = currentPropertyNode.InnerText; break;

                    case "BorderBottomStyle": BorderBottomStyle = currentPropertyNode.InnerText; break;

                    case "BorderBottomWidth": BorderBottomWidth = currentPropertyNode.InnerText; break;

                    case "BorderBottomWidthUnit": BorderBottomWidthUnit = currentPropertyNode.InnerText; break;

                    case "BorderBottomColor": BorderBottomColor = currentPropertyNode.InnerText; break;

                    case "BorderRightStyle": BorderRightStyle = currentPropertyNode.InnerText; break;

                    case "BorderRightWidth": BorderRightWidth = currentPropertyNode.InnerText; break;

                    case "BorderRightWidthUnit": BorderRightWidthUnit = currentPropertyNode.InnerText; break;

                    case "BorderRightColor": BorderRightColor = currentPropertyNode.InnerText; break;

                    #endregion


                    #region Padding and Margin Properties

                    case "Padding": Padding = currentPropertyNode.InnerText; break;

                    case "Margin": Margin = currentPropertyNode.InnerText; break;

                    #endregion 

                }

            }


            #region Font Properties

            //FontFamily = StyleProperties_ReadProperty (styleProperties, "FontFamily");

            //FontSize = StyleProperties_ReadProperty (styleProperties, "FontSize");

            //FontSizeUnit = StyleProperties_ReadProperty (styleProperties, "FontSizeUnit");

            //FontWeight = StyleProperties_ReadProperty (styleProperties, "FontWeight");

            //FontStyle = StyleProperties_ReadProperty (styleProperties, "FontStyle");

            //FontVariant = StyleProperties_ReadProperty (styleProperties, "FontVariant");

            //TextTransform = StyleProperties_ReadProperty (styleProperties, "TextTransform");

            //TextDecoration = StyleProperties_ReadProperty (styleProperties, "TextDecoration");

            //Color = StyleProperties_ReadProperty (styleProperties, "Color");

            //BackgroundColor = StyleProperties_ReadProperty (styleProperties, "BackgroundColor");

            #endregion


            #region Block Properties

            //Width = StyleProperties_ReadProperty (styleProperties, "Width");

            //WidthUnit = StyleProperties_ReadProperty (styleProperties, "WidthUnit");

            //Height = StyleProperties_ReadProperty (styleProperties, "Height");

            //HeightUnit = StyleProperties_ReadProperty (styleProperties, "HeightUnit");

            //LineHeight = StyleProperties_ReadProperty (styleProperties, "LineHeight");

            //LineHeightUnit = StyleProperties_ReadProperty (styleProperties, "LineHeightUnit");

            //VerticalAlign = StyleProperties_ReadProperty (styleProperties, "VerticalAlign");

            //TextAlign = StyleProperties_ReadProperty (styleProperties, "TextAlign");

            //TextIndent = StyleProperties_ReadProperty (styleProperties, "TextIndent");

            //TextIndentUnit = StyleProperties_ReadProperty (styleProperties, "TextIndentUnit");

            //WhiteSpace = StyleProperties_ReadProperty (styleProperties, "WhiteSpace");

            //WordSpacing = StyleProperties_ReadProperty (styleProperties, "WordSpacing");

            //WordSpacingUnit = StyleProperties_ReadProperty (styleProperties, "WordSpacingUnit");

            //LetterSpacing = StyleProperties_ReadProperty (styleProperties, "LetterSpacing");

            //LetterSpacingUnit = StyleProperties_ReadProperty (styleProperties, "LetterSpacingUnit");

            #endregion


            #region Border Properties

            //IsBorderSame = Boolean.Parse (StyleProperties_ReadProperty (styleProperties, "IsBorderSame", "False"));


            //BorderTopStyle = StyleProperties_ReadProperty (styleProperties, "BorderTopStyle");

            //BorderTopWidth = StyleProperties_ReadProperty (styleProperties, "BorderTopWidth");

            //BorderTopWidthUnit = StyleProperties_ReadProperty (styleProperties, "BorderTopWidthUnit");

            //BorderTopColor = StyleProperties_ReadProperty (styleProperties, "BorderTopColor");


            //BorderLeftStyle = StyleProperties_ReadProperty (styleProperties, "BorderLeftStyle");

            //BorderLeftWidth = StyleProperties_ReadProperty (styleProperties, "BorderLeftWidth");

            //BorderLeftWidthUnit = StyleProperties_ReadProperty (styleProperties, "BorderLeftWidthUnit");

            //BorderLeftColor = StyleProperties_ReadProperty (styleProperties, "BorderLeftColor");


            //BorderBottomStyle = StyleProperties_ReadProperty (styleProperties, "BorderBottomStyle");

            //BorderBottomWidth = StyleProperties_ReadProperty (styleProperties, "BorderBottomWidth");

            //BorderBottomWidthUnit = StyleProperties_ReadProperty (styleProperties, "BorderBottomWidthUnit");

            //BorderBottomColor = StyleProperties_ReadProperty (styleProperties, "BorderBottomColor");


            //BorderRightStyle = StyleProperties_ReadProperty (styleProperties, "BorderRightStyle");

            //BorderRightWidth = StyleProperties_ReadProperty (styleProperties, "BorderRightWidth");

            //BorderRightWidthUnit = StyleProperties_ReadProperty (styleProperties, "BorderRightWidthUnit");

            //BorderRightColor = StyleProperties_ReadProperty (styleProperties, "BorderRightColor");

            #endregion


            #region Padding and Margin Properties

            //IsPaddingSame = Boolean.Parse (StyleProperties_ReadProperty (styleProperties, "IsPaddingSame", "False"));


            //PaddingTop = StyleProperties_ReadProperty (styleProperties, "PaddingTop");

            //PaddingTopUnit = StyleProperties_ReadProperty (styleProperties, "PaddingTopUnit");

            //PaddingLeft = StyleProperties_ReadProperty (styleProperties, "PaddingLeft");

            //PaddingLeftUnit = StyleProperties_ReadProperty (styleProperties, "PaddingLeftUnit");

            //PaddingBottom = StyleProperties_ReadProperty (styleProperties, "PaddingBottom");

            //PaddingBottomUnit = StyleProperties_ReadProperty (styleProperties, "PaddingBottomUnit");

            //PaddingRight = StyleProperties_ReadProperty (styleProperties, "PaddingRight");

            //PaddingRightUnit = StyleProperties_ReadProperty (styleProperties, "PaddingRightUnit");

            //Padding = StyleProperties_ReadProperty (styleProperties, "Padding");


            //IsMarginSame = Boolean.Parse (StyleProperties_ReadProperty (styleProperties, "IsMarginSame", "False"));


            //MarginTop = StyleProperties_ReadProperty (styleProperties, "MarginTop");

            //MarginTopUnit = StyleProperties_ReadProperty (styleProperties, "MarginTopUnit");

            //MarginLeft = StyleProperties_ReadProperty (styleProperties, "MarginLeft");

            //MarginLeftUnit = StyleProperties_ReadProperty (styleProperties, "MarginLeftUnit");

            //MarginBottom = StyleProperties_ReadProperty (styleProperties, "MarginBottom");

            //MarginBottomUnit = StyleProperties_ReadProperty (styleProperties, "MarginBottomUnit");

            //MarginRight = StyleProperties_ReadProperty (styleProperties, "MarginRight");

            //MarginRightUnit = StyleProperties_ReadProperty (styleProperties, "MarginRightUnit");

            //Margin = StyleProperties_ReadProperty (styleProperties, "Margin");

            #endregion

            return;

        }

        public void StyleProperties_AddProperty (System.Xml.XmlDocument styleProperties, String propertyName, String propertyValue) {

            System.Xml.XmlNode rootNode = styleProperties.GetElementsByTagName ("StyleProperties")[0];

            System.Xml.XmlElement propertyNode;


            propertyNode = styleProperties.CreateElement ("Property");

            propertyNode.SetAttribute ("Name", propertyName);

            propertyNode.InnerText = propertyValue;

            rootNode.AppendChild (propertyNode);

        }

        public String StyleProperties_ReadProperty (System.Xml.XmlDocument styleProperties, String propertyName) {

            System.Xml.XmlNode property;

            property = styleProperties.SelectSingleNode ("//Property[@Name='" + propertyName + "']");

            if (property != null) {

                return property.InnerText;

            }

            return String.Empty;

        }

        public String StyleProperties_ReadProperty (System.Xml.XmlDocument styleProperties, String propertyName, String defaultValue) {

            System.Xml.XmlNode property;

            property = styleProperties.SelectSingleNode ("//Property[@Name='" + propertyName + "']");

            if (property != null) {

                return property.InnerText;

            }

            return defaultValue;

        }

        public System.Xml.XmlDocument StyleProperties {

            get {

                System.Xml.XmlDocument styleProperties = new System.Xml.XmlDocument ();

                System.Xml.XmlDeclaration xmlDeclaration = styleProperties.CreateXmlDeclaration ("1.0", "utf-8", null);

                System.Xml.XmlElement rootNode = styleProperties.CreateElement ("StyleProperties");

                styleProperties.InsertBefore (xmlDeclaration, styleProperties.DocumentElement);

                styleProperties.AppendChild (rootNode);


                #region Font Properties

                if (!String.IsNullOrEmpty (fontFamily)) { StyleProperties_AddProperty (styleProperties, "FontFamily", fontFamily); }

                if (!String.IsNullOrEmpty (fontSize)) { StyleProperties_AddProperty (styleProperties, "FontSize", fontSize); }

                if (!String.IsNullOrEmpty (fontSize)) { StyleProperties_AddProperty (styleProperties, "FontSizeUnit", fontSizeUnit); }

                if (!String.IsNullOrEmpty (fontWeight)) { StyleProperties_AddProperty (styleProperties, "FontWeight", fontWeight); }

                if (!String.IsNullOrEmpty (fontStyle)) { StyleProperties_AddProperty (styleProperties, "FontStyle", fontStyle); }

                if (!String.IsNullOrEmpty (fontVariant)) { StyleProperties_AddProperty (styleProperties, "FontVariant", fontVariant); }

                if (!String.IsNullOrEmpty (textTransform)) { StyleProperties_AddProperty (styleProperties, "TextTransform", textTransform); }

                if (!String.IsNullOrEmpty (textDecoration)) { StyleProperties_AddProperty (styleProperties, "TextDecoration", textDecoration); }

                if (!String.IsNullOrEmpty (color)) { StyleProperties_AddProperty (styleProperties, "Color", color); }

                if (!String.IsNullOrEmpty (backgroundColor)) { StyleProperties_AddProperty (styleProperties, "BackgroundColor", backgroundColor); }

                #endregion


                #region Block Properties

                if (!String.IsNullOrEmpty (width)) { StyleProperties_AddProperty (styleProperties, "Width", width); }

                if (!String.IsNullOrEmpty (width)) { StyleProperties_AddProperty (styleProperties, "WidthUnit", widthUnit); }

                if (!String.IsNullOrEmpty (height)) { StyleProperties_AddProperty (styleProperties, "Height", height); }

                if (!String.IsNullOrEmpty (height)) { StyleProperties_AddProperty (styleProperties, "HeightUnit", heightUnit); }

                if (!String.IsNullOrEmpty (lineHeight)) { StyleProperties_AddProperty (styleProperties, "LineHeight", lineHeight); }

                if (!String.IsNullOrEmpty (lineHeight)) { StyleProperties_AddProperty (styleProperties, "LineHeightUnit", lineHeightUnit); }

                if (!String.IsNullOrEmpty (verticalAlign)) { StyleProperties_AddProperty (styleProperties, "VerticalAlign", verticalAlign); }

                if (!String.IsNullOrEmpty (textAlign)) { StyleProperties_AddProperty (styleProperties, "TextAlign", textAlign); }

                if (!String.IsNullOrEmpty (textIndent)) { StyleProperties_AddProperty (styleProperties, "TextIndent", textIndent); }

                if (!String.IsNullOrEmpty (textIndent)) { StyleProperties_AddProperty (styleProperties, "TextIndentUnit", textIndentUnit); }

                if (!String.IsNullOrEmpty (whiteSpace)) { StyleProperties_AddProperty (styleProperties, "WhiteSpace", whiteSpace); }

                if (!String.IsNullOrEmpty (wordSpacing)) { StyleProperties_AddProperty (styleProperties, "WordSpacing", wordSpacing); }

                if (!String.IsNullOrEmpty (wordSpacing)) { StyleProperties_AddProperty (styleProperties, "WordSpacingUnit", wordSpacingUnit); }

                if (!String.IsNullOrEmpty (letterSpacing)) { StyleProperties_AddProperty (styleProperties, "LetterSpacing", letterSpacing); }

                if (!String.IsNullOrEmpty (letterSpacing)) { StyleProperties_AddProperty (styleProperties, "LetterSpacingUnit", letterSpacingUnit); }

                if (!String.IsNullOrEmpty (overflow)) { StyleProperties_AddProperty (styleProperties, "Overflow", overflow); }

                #endregion


                #region Border Properties

                StyleProperties_AddProperty (styleProperties, "IsBorderSame", isBorderSame.ToString ());


                if (!String.IsNullOrEmpty (borderTopStyle)) { StyleProperties_AddProperty (styleProperties, "BorderTopStyle", borderTopStyle); }

                if (!String.IsNullOrEmpty (borderTopStyle)) { StyleProperties_AddProperty (styleProperties, "BorderTopWidth", borderTopWidth); }

                if (!String.IsNullOrEmpty (borderTopStyle)) { StyleProperties_AddProperty (styleProperties, "BorderTopWidthUnit", borderTopWidthUnit); }

                if (!String.IsNullOrEmpty (borderTopStyle)) { StyleProperties_AddProperty (styleProperties, "BorderTopColor", borderTopColor); }


                if (!String.IsNullOrEmpty (borderLeftStyle)) { StyleProperties_AddProperty (styleProperties, "BorderLeftStyle", borderLeftStyle); }

                if (!String.IsNullOrEmpty (borderLeftStyle)) { StyleProperties_AddProperty (styleProperties, "BorderLeftWidth", borderLeftWidth); }

                if (!String.IsNullOrEmpty (borderLeftStyle)) { StyleProperties_AddProperty (styleProperties, "BorderLeftWidthUnit", borderLeftWidthUnit); }

                if (!String.IsNullOrEmpty (borderLeftStyle)) { StyleProperties_AddProperty (styleProperties, "BorderLeftColor", borderLeftColor); }


                if (!String.IsNullOrEmpty (borderBottomStyle)) { StyleProperties_AddProperty (styleProperties, "BorderBottomStyle", borderBottomStyle); }

                if (!String.IsNullOrEmpty (borderBottomStyle)) { StyleProperties_AddProperty (styleProperties, "BorderBottomWidth", borderBottomWidth); }

                if (!String.IsNullOrEmpty (borderBottomStyle)) { StyleProperties_AddProperty (styleProperties, "BorderBottomWidthUnit", borderBottomWidthUnit); }

                if (!String.IsNullOrEmpty (borderBottomStyle)) { StyleProperties_AddProperty (styleProperties, "BorderBottomColor", borderBottomColor); }


                if (!String.IsNullOrEmpty (borderRightStyle)) { StyleProperties_AddProperty (styleProperties, "BorderRightStyle", borderRightStyle); }

                if (!String.IsNullOrEmpty (borderRightStyle)) { StyleProperties_AddProperty (styleProperties, "BorderRightWidth", borderRightWidth); }

                if (!String.IsNullOrEmpty (borderRightStyle)) { StyleProperties_AddProperty (styleProperties, "BorderRightWidthUnit", borderRightWidthUnit); }

                if (!String.IsNullOrEmpty (borderRightStyle)) { StyleProperties_AddProperty (styleProperties, "BorderRightColor", borderRightColor); }

                #endregion


                #region Padding and Margin Properties

                //StyleProperties_AddProperty (styleProperties, "IsPaddingSame", isPaddingSame.ToString ());


                //StyleProperties_AddProperty (styleProperties, "PaddingTop", paddingTop);

                //StyleProperties_AddProperty (styleProperties, "PaddingTopUnit", paddingTopUnit);

                //StyleProperties_AddProperty (styleProperties, "PaddingLeft", paddingLeft);

                //StyleProperties_AddProperty (styleProperties, "PaddingLeftUnit", paddingLeftUnit);

                //StyleProperties_AddProperty (styleProperties, "PaddingBottom", paddingBottom);

                //StyleProperties_AddProperty (styleProperties, "PaddingBottomUnit", paddingBottomUnit);

                //StyleProperties_AddProperty (styleProperties, "PaddingRigth", paddingRight);

                //StyleProperties_AddProperty (styleProperties, "PaddingRigthUnit", paddingRightUnit);

                if (!String.IsNullOrEmpty (padding)) { StyleProperties_AddProperty (styleProperties, "Padding", padding); }


                //StyleProperties_AddProperty (styleProperties, "IsMarginSame", isMarginSame.ToString ());


                //StyleProperties_AddProperty (styleProperties, "MarginTop", marginTop);

                //StyleProperties_AddProperty (styleProperties, "MarginTopUnit", marginTopUnit);

                //StyleProperties_AddProperty (styleProperties, "MarginLeft", marginLeft);

                //StyleProperties_AddProperty (styleProperties, "MarginLeftUnit", marginLeftUnit);

                //StyleProperties_AddProperty (styleProperties, "MarginBottom", marginBottom);

                //StyleProperties_AddProperty (styleProperties, "MarginBottomUnit", marginBottomUnit);

                //StyleProperties_AddProperty (styleProperties, "MarginRigth", marginRight);

                //StyleProperties_AddProperty (styleProperties, "MarginRigthUnit", marginRightUnit);

                if (!String.IsNullOrEmpty (margin)) { StyleProperties_AddProperty (styleProperties, "Margin", margin); }

                #endregion


                return styleProperties;

            }

        }

    }

}
