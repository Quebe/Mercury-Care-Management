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

namespace Mercury.Silverlight.Forms {

    public class FormRenderEngine {

        #region Private Properties

        private Client.Application MercuryApplication = ((App) Application.Current).MercuryApplication;

        private RenderEngineMode renderMode = RenderEngineMode.Designer;

        private Boolean showDropZones = false;


        // private FormDesigner.FormDesigner designerPage = null;

        private FormEditor.FormEditor editorPage = null;

        private Boolean isFormRendered = false;

        private Boolean isStateReferenceRequested = false;


        private const Double pixelsPerInch = 96;

        private const Double formWidth = 768;


        #endregion 


        #region Public Properties

        public Boolean ShowDropZones { get { return showDropZones; } set { showDropZones = value; } }

        public Boolean IsFormRendered { get { return isFormRendered; } set { isFormRendered = value; } }

        #endregion 


        #region Constructors

        //public FormRenderEngine (FormDesigner.FormDesigner formDesignerPage) {

        //    renderMode = RenderEngineMode.Designer;

        //    designerPage = formDesignerPage;

        //    return;

        //}

        public FormRenderEngine (FormEditor.FormEditor formEditorPage) {

            renderMode = RenderEngineMode.Editor;

            editorPage = formEditorPage;

            return;

        }

        #endregion 


        #region Style Support Functions 

        private Double FontPointSizeIntoPixels (Int32 pointSize) {

            // ASSUME THE FOLLOWING: 72 POINTS PER INCH, 96 PIXELS PER INCH

            // RATIO IS 72 PT / 96 PX

            return (Convert.ToDouble (pointSize) * 96.0f / 72.0f);

        }

        private Double FontPointSizeIntoPixels (String pointSize) {

            Int32 size = 0;

            if (Int32.TryParse (pointSize, out size)) {

                return FontPointSizeIntoPixels (size);

            }

            return 0;

        }

        private Double StyleLengthIntoPixels (String length, String lengthUnit) {

            Double lengthInPixels = 0;


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

        private GridLength StyleLengthIntoGridLength (String length, String lengthUnit) {

            GridLength gridLength = GridLength.Auto;

            switch (lengthUnit.ToUpper ().Trim ()) {

                case "PX":

                    gridLength = new GridLength (Convert.ToDouble (length), GridUnitType.Pixel);

                    break;

                case "%":

                    gridLength = new GridLength (Convert.ToDouble (length) / 100, GridUnitType.Star);

                    break;

            }

            return gridLength;

        }

        private Thickness StyleMarginPaddingIntoThickness (String paddingMargin) {

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

        private FontWeight StyleFontWeight (String forFontWeight) {

            FontWeight fontWeight = FontWeights.Normal;

            try {

                if (FontWeights.Normal.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Normal; }
                
                else if (FontWeights.Bold.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Bold; }

                else if (FontWeights.Black.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Black; }

                else if (FontWeights.ExtraBlack.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.ExtraBlack; }

                else if (FontWeights.ExtraBold.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.ExtraBold; }

                else if (FontWeights.ExtraLight.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.ExtraLight; }

                else if (FontWeights.Light.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Light; }

                else if (FontWeights.Medium.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Medium; }

                else if (FontWeights.SemiBold.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.SemiBold; }

                else if (FontWeights.Thin.ToString ().Trim ().ToUpper () == forFontWeight.Trim ().ToUpper ()) { fontWeight = FontWeights.Thin; }

            }

            catch { /* DO NOTHING */ }

            return fontWeight;

        }

        private FontStyle StyleFontStyle (String forFontStyle) {

            return (forFontStyle.ToUpper ().Contains ("ITALIC")) ? FontStyles.Italic : FontStyles.Normal;

        }

        private TextAlignment StyleTextAlignment (String forTextAlignment) {

            TextAlignment alignment = TextAlignment.Left;

            if (!String.IsNullOrEmpty (forTextAlignment)) {

                switch (forTextAlignment.ToUpper ().Trim ()) {

                    case "CENTER": alignment = TextAlignment.Center; break;

                    case "RIGHT": alignment = TextAlignment.Right; break;

                }

            }

            return alignment;

        }

        private VerticalAlignment StyleVerticalAlignment (String forVerticalAlignment) {

            VerticalAlignment alignment = VerticalAlignment.Top;

            if (!String.IsNullOrEmpty (forVerticalAlignment)) {

                switch (forVerticalAlignment.ToUpper ().Trim ()) {

                    case "MIDDLE":

                    case "CENTER":

                        alignment = VerticalAlignment.Center;

                        break;

                    case "BOTTOM": alignment = VerticalAlignment.Bottom; break;

                }

            }

            return alignment;

        }

        private Brush SolidColorBrush (String webColor) {

            String htmlColor = webColor.Replace ("#", String.Empty);

            byte r = System.Convert.ToByte (htmlColor.Substring (0, 2), 16);

            byte g = System.Convert.ToByte (htmlColor.Substring (2, 2), 16);

            byte b = System.Convert.ToByte (htmlColor.Substring (4, 2), 16);

            return new SolidColorBrush (Color.FromArgb (255, r, g, b));

        }

        #endregion 


        #region Support Methods

        private ColumnDefinition columnDefinitionForWidth (GridLength forWidth) {

            ColumnDefinition definition = new ColumnDefinition ();

            definition.Width = forWidth;

            return definition;

        }

        #endregion 


        #region Data Binding Methods

        public System.Windows.Data.Binding PropertyDataBinding (String propertyName, Object source, System.Windows.Data.BindingMode bindingMode) {

            System.Windows.Data.Binding dataBinding = new System.Windows.Data.Binding (propertyName);

            dataBinding.Source = source;

            dataBinding.Mode = bindingMode;

            return dataBinding;

        }

        public System.Windows.Data.Binding PropertyDataBinding (String propertyName, Object source, System.Windows.Data.BindingMode bindingMode, System.Windows.Data.IValueConverter converter) {

            System.Windows.Data.Binding dataBinding = PropertyDataBinding (propertyName, source, bindingMode);

            dataBinding.Converter = converter;
                
            return dataBinding;

        }

        private void BindFontStyleToTextBlock (TextBlock forTextBlock, Client.Core.Forms.Structures.Style forStyle) {

            forTextBlock.SetBinding (TextBlock.FontFamilyProperty, PropertyDataBinding ("SlFontFamily", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.FontSizeProperty, PropertyDataBinding ("SlFontSize", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.FontWeightProperty, PropertyDataBinding ("SlFontWeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.FontStyleProperty, PropertyDataBinding ("SlFontStyle", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.LineHeightProperty, PropertyDataBinding ("SlLineHeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.TextDecorationsProperty, PropertyDataBinding ("SlTextDecorations", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.TextWrappingProperty, PropertyDataBinding ("SlTextWrapping", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.SetBinding (TextBlock.TextAlignmentProperty, PropertyDataBinding ("SlTextAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

//            forTextBlock.SetBinding (TextBlock.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBlock.VerticalAlignment = VerticalAlignment.Center;

            forTextBlock.SetBinding (TextBlock.ForegroundProperty, PropertyDataBinding ("SlForeground", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forTextBlock.SetBinding (TextBlock.BackgroundProperty, PropertyDataBinding ("SlBackground", forStyle, System.Windows.Data.BindingMode.OneWay));

            return;

        }

        private void BindFontStyleToTextBox (TextBox forTextBox, Client.Core.Forms.Structures.Style forStyle) {

            forTextBox.SetBinding (TextBox.FontFamilyProperty, PropertyDataBinding ("SlFontFamily", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.SetBinding (TextBox.FontSizeProperty, PropertyDataBinding ("SlFontSize", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.SetBinding (TextBox.FontWeightProperty, PropertyDataBinding ("SlFontWeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.SetBinding (TextBox.FontStyleProperty, PropertyDataBinding ("SlFontStyle", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forTextBox.SetBinding (TextBox.TextDecorationsProperty, PropertyDataBinding ("SlTextDecorations", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.SetBinding (TextBox.TextWrappingProperty, PropertyDataBinding ("SlTextWrapping", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.SetBinding (TextBox.TextAlignmentProperty, PropertyDataBinding ("SlTextAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forTextBox.SetBinding (TextBox.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            forTextBox.VerticalAlignment = VerticalAlignment.Center;

            //forTextBox.SetBinding (TextBox.ForegroundProperty, PropertyDataBinding ("SlForeground", forStyle, System.Windows.Data.BindingMode.OneWay));

            //forTextBox.SetBinding (TextBox.BackgroundProperty, PropertyDataBinding ("SlBackground", forStyle, System.Windows.Data.BindingMode.OneWay));

            return;

        }

        private void BindFontStyleToRadMaskedTextBox (Telerik.Windows.Controls.RadMaskedTextBox forRadMaskedTextBox, Client.Core.Forms.Structures.Style forStyle) {

            forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.FontFamilyProperty, PropertyDataBinding ("SlFontFamily", forStyle, System.Windows.Data.BindingMode.OneWay));

            forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.FontSizeProperty, PropertyDataBinding ("SlFontSize", forStyle, System.Windows.Data.BindingMode.OneWay));

            forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.FontWeightProperty, PropertyDataBinding ("SlFontWeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.FontStyleProperty, PropertyDataBinding ("SlFontStyle", forStyle, System.Windows.Data.BindingMode.OneWay));

            //forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.TextDecorationsProperty, PropertyDataBinding ("SlTextDecorations", forStyle, System.Windows.Data.BindingMode.OneWay));

            //forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.TextWrappingProperty, PropertyDataBinding ("SlTextWrapping", forStyle, System.Windows.Data.BindingMode.OneWay));

            forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.TextAlignmentProperty, PropertyDataBinding ("SlTextAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            forRadMaskedTextBox.VerticalAlignment = VerticalAlignment.Center;

            //forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.ForegroundProperty, PropertyDataBinding ("SlForeground", forStyle, System.Windows.Data.BindingMode.OneWay));

            //forRadMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.BackgroundProperty, PropertyDataBinding ("SlBackground", forStyle, System.Windows.Data.BindingMode.OneWay));

            return;

        }

        private void BindFontStyleToToggleButton (System.Windows.Controls.Primitives.ToggleButton forToggleButton, Client.Core.Forms.Structures.Style forStyle) {

            forToggleButton.SetBinding (TextBox.FontFamilyProperty, PropertyDataBinding ("SlFontFamily", forStyle, System.Windows.Data.BindingMode.OneWay));

            forToggleButton.SetBinding (TextBox.FontSizeProperty, PropertyDataBinding ("SlFontSize", forStyle, System.Windows.Data.BindingMode.OneWay));

            forToggleButton.SetBinding (TextBox.FontWeightProperty, PropertyDataBinding ("SlFontWeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forToggleButton.SetBinding (TextBox.FontStyleProperty, PropertyDataBinding ("SlFontStyle", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forToggleButton.SetBinding (TextBox.TextDecorationsProperty, PropertyDataBinding ("SlTextDecorations", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forToggleButton.SetBinding (TextBox.TextWrappingProperty, PropertyDataBinding ("SlTextWrapping", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forToggleButton.SetBinding (TextBox.TextAlignmentProperty, PropertyDataBinding ("SlTextAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forToggleButton.SetBinding (TextBox.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            forToggleButton.VerticalAlignment = VerticalAlignment.Center;

            forToggleButton.SetBinding (TextBox.ForegroundProperty, PropertyDataBinding ("SlForeground", forStyle, System.Windows.Data.BindingMode.OneWay));

            forToggleButton.SetBinding (TextBox.BackgroundProperty, PropertyDataBinding ("SlBackground", forStyle, System.Windows.Data.BindingMode.OneWay));

            return;

        }

        private void BindFontStyleToButton (Button forButton, Client.Core.Forms.Structures.Style forStyle) {

            forButton.SetBinding (TextBox.FontFamilyProperty, PropertyDataBinding ("SlFontFamily", forStyle, System.Windows.Data.BindingMode.OneWay));

            forButton.SetBinding (TextBox.FontSizeProperty, PropertyDataBinding ("SlFontSize", forStyle, System.Windows.Data.BindingMode.OneWay));

            forButton.SetBinding (TextBox.FontWeightProperty, PropertyDataBinding ("SlFontWeight", forStyle, System.Windows.Data.BindingMode.OneWay));

            forButton.SetBinding (TextBox.FontStyleProperty, PropertyDataBinding ("SlFontStyle", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forButton.SetBinding (TextBox.TextDecorationsProperty, PropertyDataBinding ("SlTextDecorations", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forButton.SetBinding (TextBox.TextWrappingProperty, PropertyDataBinding ("SlTextWrapping", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forButton.SetBinding (TextBox.TextAlignmentProperty, PropertyDataBinding ("SlTextAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forButton.SetBinding (TextBox.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", forStyle, System.Windows.Data.BindingMode.OneWay));

            forButton.VerticalAlignment = VerticalAlignment.Center;

            // forButton.SetBinding (TextBox.ForegroundProperty, PropertyDataBinding ("SlForeground", forStyle, System.Windows.Data.BindingMode.OneWay));

            // forButton.SetBinding (TextBox.BackgroundProperty, PropertyDataBinding ("SlBackground", forStyle, System.Windows.Data.BindingMode.OneWay));

            return;

        }

        #endregion


        /* RENDER CONTROL PANEL STRUCTURE
         * 
         * [-GRID-] "FormControl_[ControlId.Replace ("-", "")]_Panel"           [VISIBILITY,WIDTH]
         * 
         *   [-BORDER-] "FormControl_[ControlId.Replace ("-", "")]_Border"      [MARGIN,PADDING,BACKGROUND,BORDER]
         * 
         *     [-GRID--] "FormControl_[ControlId.Replace ("-", "")]_Content"    [*NONE*]
         * 
         */


        #region Render Functions

        private void RenderControlWithLabelGrid (Client.Core.Forms.Control formControl, Grid controlGrid, FrameworkElement controlElement) {

            Boolean labelVisible = false;

            ColumnDefinition leftColumnDefinition = new ColumnDefinition ();

            ColumnDefinition rightColumnDefinition = new ColumnDefinition ();

            RowDefinition singleRowDefinition = new RowDefinition ();

            ColumnDefinition singleColumnDefinition = new ColumnDefinition ();

            RowDefinition topRowDefinition = new RowDefinition ();

            RowDefinition bottomRowDefinition = new RowDefinition ();



            #region Common Style Applications 

            controlElement.HorizontalAlignment = HorizontalAlignment.Stretch;

            #endregion



            if (formControl.Label != null) {

                labelVisible = formControl.Label.Visible;

            }


            if (labelVisible) {

                TextBlock label = RenderControl_Label (formControl.Label);
                
                GridLength labelColumnWidth = GridLength.Auto;

                GridLength controlColumnWidth = GridLength.Auto;

                if (!String.IsNullOrEmpty (formControl.Label.Style.Width)) {

                    labelColumnWidth = StyleLengthIntoGridLength (formControl.Label.Style.Width, formControl.Label.Style.WidthUnit);

                    if (labelColumnWidth.IsStar) {

                        controlColumnWidth = new GridLength (1 - labelColumnWidth.Value, GridUnitType.Star);

                    }

                }

                switch (formControl.Label.Position) {

                    case global::Mercury.Server.Application.FormControlPosition.Left:

                        #region Label Position Left

                        controlGrid.ColumnDefinitions.Add (leftColumnDefinition);

                        if (labelColumnWidth != GridLength.Auto) { leftColumnDefinition.Width = labelColumnWidth; }

                        controlGrid.ColumnDefinitions.Add (rightColumnDefinition);

                        if (controlColumnWidth != GridLength.Auto) { rightColumnDefinition.Width = controlColumnWidth; }

                        controlGrid.RowDefinitions.Add (singleRowDefinition);


                        label.SetValue (Grid.ColumnProperty, 0);

                        label.SetValue (Grid.RowProperty, 0);

                        controlElement.SetValue (Grid.ColumnProperty, 1);

                        controlElement.SetValue (Grid.RowProperty, 0);

                        #endregion 

                        break;

                    case global::Mercury.Server.Application.FormControlPosition.Right:

                        #region Label Position Right

                        controlGrid.ColumnDefinitions.Add (leftColumnDefinition);

                        controlGrid.ColumnDefinitions.Add (rightColumnDefinition);

                        if (labelColumnWidth != GridLength.Auto) { rightColumnDefinition.Width = labelColumnWidth; }

                        controlGrid.RowDefinitions.Add (singleRowDefinition);


                        label.SetValue (Grid.ColumnProperty, 1);

                        label.SetValue (Grid.RowProperty, 0);

                        controlElement.SetValue (Grid.ColumnProperty, 0);

                        controlElement.SetValue (Grid.RowProperty, 0);

                        #endregion 

                        break;

                    case global::Mercury.Server.Application.FormControlPosition.Top:

                        #region Label Position Top

                        controlGrid.ColumnDefinitions.Add (singleColumnDefinition);

                        controlGrid.RowDefinitions.Add (topRowDefinition);

                        controlGrid.RowDefinitions.Add (bottomRowDefinition);


                        label.SetValue (Grid.ColumnProperty, 0);

                        label.SetValue (Grid.RowProperty, 0);

                        controlElement.SetValue (Grid.ColumnProperty, 0);

                        controlElement.SetValue (Grid.RowProperty, 1);

                        #endregion 

                        break;

                    case global::Mercury.Server.Application.FormControlPosition.Bottom:

                        #region Label Position Bottom

                        controlGrid.ColumnDefinitions.Add (singleColumnDefinition);

                        controlGrid.RowDefinitions.Add (topRowDefinition);

                        controlGrid.RowDefinitions.Add (bottomRowDefinition);


                        label.SetValue (Grid.ColumnProperty, 0);

                        label.SetValue (Grid.RowProperty, 1);

                        controlElement.SetValue (Grid.ColumnProperty, 0);

                        controlElement.SetValue (Grid.RowProperty, 0);

                        #endregion 

                        break;

                }

                controlGrid.Children.Add (label);

                controlGrid.Children.Add (controlElement);

            }

            else {

                if (!String.IsNullOrEmpty (formControl.Style.Padding)) { controlElement.Margin = StyleMarginPaddingIntoThickness (formControl.Style.Padding); }

                controlGrid.Children.Add (controlElement);

            }

            return;

        }

        private Grid RenderControlPanel (Client.Core.Forms.Control formControl) {

            Grid controlGrid = new Grid ();

            controlGrid.Name = "FormControl_" + formControl.ControlId.ToString ().Replace ("-", "") + "_Panel";

            controlGrid.HorizontalAlignment = HorizontalAlignment.Stretch;


            if (renderMode != RenderEngineMode.Designer) {

                controlGrid.SetBinding (Grid.VisibilityProperty, PropertyDataBinding ("Visibility", formControl, System.Windows.Data.BindingMode.OneWay));

                ToolTipService.SetToolTip (controlGrid, formControl.Name); // TODO: RETURN TO TITLE WHEN COMPLETED.

            }


            controlGrid.SetBinding (Grid.WidthProperty, PropertyDataBinding ("SlWidth", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlGrid.SetBinding (Grid.HeightProperty, PropertyDataBinding ("SlHeight", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlGrid.SetBinding (Grid.MinHeightProperty, PropertyDataBinding ("SlLineHeight", formControl.Style, System.Windows.Data.BindingMode.OneWay));



            Border controlBorder = new Border ();

            controlBorder.Name = "FormControl_" + formControl.ControlId.ToString ().Replace ("-", "") + "_Border";

            controlBorder.SetBinding (Border.MarginProperty, PropertyDataBinding ("SlMargin", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlBorder.SetBinding (Border.PaddingProperty, PropertyDataBinding ("SlPadding", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlBorder.SetBinding (Border.BackgroundProperty, PropertyDataBinding ("SlBackground", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlBorder.SetBinding (Border.BorderBrushProperty, PropertyDataBinding ("SlBorderBrush", formControl.Style, System.Windows.Data.BindingMode.OneWay));

            controlBorder.SetBinding (Border.BorderThicknessProperty, PropertyDataBinding ("SlBorderThickness", formControl.Style, System.Windows.Data.BindingMode.OneWay));


            //controlBorder.BorderBrush = new SolidColorBrush (Colors.Black);

            //controlBorder.BorderThickness = new Thickness (1);


            controlGrid.Children.Add (controlBorder);


            Grid controlContentGrid = new Grid ();

            controlContentGrid.Name = "FormControl_" + formControl.ControlId.ToString ().Replace ("-", "") + "_Content";

            controlGrid.HorizontalAlignment = HorizontalAlignment.Stretch;
            
            controlBorder.Child = controlContentGrid;


            return controlGrid;

        }

        private Panel RenderControl_DropZone (Client.Core.Forms.Control ownerControl, Int32 dropZonePosition) {

            Grid dropZone = new Grid ();


            dropZone.Name = "ControlId_" + ownerControl.ControlId.ToString () + "_" + dropZonePosition.ToString ();

            dropZone.Margin = new Thickness (4, 4, 4, 4);

            dropZone.Background = SolidColorBrush ("#ffffcc");

            dropZone.HorizontalAlignment = HorizontalAlignment.Stretch;

            dropZone.Visibility = (Visibility) Convert.ToInt32 (!showDropZones);

            dropZone.Tag = "DropZone";


            Telerik.Windows.Controls.DragDrop.RadDragAndDropManager.SetAllowDrop (dropZone, true);

            //Telerik.Windows.Controls.DragDrop.RadDragAndDropManager.AddDropInfoHandler (dropZone, designerPage.DesignerFormDropZone_OnDropInfo);

            //Telerik.Windows.Controls.DragDrop.RadDragAndDropManager.AddDropQueryHandler (dropZone, designerPage.DesignerFormDropZone_OnDropQuery);
           

            dropZone.ColumnDefinitions.Add (new ColumnDefinition ());

            dropZone.RowDefinitions.Add (new RowDefinition ());


            TextBlock dropZoneContent = new TextBlock ();

            dropZoneContent.SetValue (Grid.ColumnProperty, 0);

            dropZoneContent.SetValue (Grid.RowProperty, 0);

            dropZoneContent.HorizontalAlignment = HorizontalAlignment.Center;

            dropZoneContent.VerticalAlignment = VerticalAlignment.Center;

            dropZoneContent.Margin = new Thickness (4, 4, 4, 4);

            dropZoneContent.TextWrapping = TextWrapping.Wrap;

            switch (ownerControl.ControlType) {

                case global::Mercury.Server.Application.FormControlType.Form:

                    dropZoneContent.Text = "[Section Drop Zone]";

                    break;

                case global::Mercury.Server.Application.FormControlType.Section:

                    dropZoneContent.TextWrapping = TextWrapping.NoWrap;

                    dropZoneContent.Text = "[ ]";

                    break;

                default: 

                    dropZoneContent.Text = "[Control Drop Zone]";

                    break;

            }

            dropZone.Children.Add (dropZoneContent);

            return dropZone;

        }

        private TextBlock RenderSimpleTextBlock (String text, Client.Core.Forms.Structures.Style forStyle, Int32 gridColumn, Int32 gridRow) {

            TextBlock simpleTextBlock = new TextBlock ();

            simpleTextBlock.Text = text;

            BindFontStyleToTextBlock (simpleTextBlock, forStyle);

            simpleTextBlock.SetValue (Grid.ColumnProperty, gridColumn);

            simpleTextBlock.SetValue (Grid.RowProperty, gridRow);

            simpleTextBlock.LineStackingStrategy = LineStackingStrategy.BlockLineHeight;

            return simpleTextBlock;

        }

        private TextBlock RenderControl_Label (Client.Core.Forms.Controls.Label labelControl) {

            TextBlock label = new TextBlock ();

            label.HorizontalAlignment = HorizontalAlignment.Stretch;

            label.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("Text", labelControl, System.Windows.Data.BindingMode.OneWay));

            BindFontStyleToTextBlock (label, labelControl.Style);

           
            return label;

        }


        // OLD RENDER SECTION SUPPORTED DROP ZONES
        //private Panel RenderControl_Section (Client.Core.Forms.Controls.Section sectionControl) {

        //    Grid sectionGrid = RenderControlPanel (sectionControl);

        //    Grid sectionContentGrid = ((Grid) ((Border) sectionGrid.Children[0]).Child);

        //    Panel dropZone;
            
        //    Int32 dropZonePosition = 0;

        //    Int32 gridColumnPosition = 0;

        //    ColumnDefinition columnDefinition;


        //    sectionContentGrid.ShowGridLines = true;


        //    sectionContentGrid.ShowGridLines = ((showDropZones) && (renderMode == RenderEngineMode.Designer));

        //    sectionContentGrid.HorizontalAlignment = HorizontalAlignment.Stretch;


        //    if (sectionControl.Controls.Count > 0) {

        //        Double controlColumnWidth = (100 / sectionControl.Controls.Count);

        //        if (renderMode == RenderEngineMode.Designer) {

        //            controlColumnWidth = (100 / ((sectionControl.Controls.Count * 2) + 1));

        //        }

        //        for (Int32 currentColumnIndex = 0; currentColumnIndex < (sectionControl.Controls.Count); currentColumnIndex++) {

        //            if ((renderMode == RenderEngineMode.Designer) && (showDropZones)) {

        //                /* DROP ZONE COLUMN */

        //                columnDefinition = new ColumnDefinition ();

        //                columnDefinition.MinWidth = 16;

        //                columnDefinition.Width = new GridLength (16, GridUnitType.Pixel);

        //                sectionContentGrid.ColumnDefinitions.Add (columnDefinition); // DROP ZONE

        //            }


        //            /* CONTROL COLUMN */

        //            columnDefinition = new ColumnDefinition ();

        //            // columnDefinition.Width = sectionControl.Controls[currentColumnIndex].Style.SlGridWidth;

        //            if ((!String.IsNullOrEmpty (sectionControl.Controls[currentColumnIndex].Style.Width)) && (sectionControl.Controls.Count > 1)) {

        //                // SET SPECIFIC WIDTH

        //                if (sectionControl.Controls[currentColumnIndex].Style.WidthUnit == "%") {

        //                    columnDefinition.Width = new GridLength (StyleLengthIntoPixels (sectionControl.Controls[currentColumnIndex].Style.Width, sectionControl.Controls[currentColumnIndex].Style.WidthUnit), GridUnitType.Star);

        //                }

        //                else {

        //                    columnDefinition.Width = new GridLength (StyleLengthIntoPixels (sectionControl.Controls[currentColumnIndex].Style.Width, sectionControl.Controls[currentColumnIndex].Style.WidthUnit), GridUnitType.Pixel);

        //                }

        //            }

        //            // else { columnDefinition.Width = new GridLength (controlColumnWidth, GridUnitType.Star); }


        //            sectionContentGrid.ColumnDefinitions.Add (columnDefinition); // TRUE COLUMN

        //        }

        //        if ((renderMode == RenderEngineMode.Designer) && (showDropZones)) {

        //            columnDefinition = new ColumnDefinition ();

        //            columnDefinition.MinWidth = 16;

        //            columnDefinition.Width = new GridLength (16, GridUnitType.Pixel);

        //            sectionContentGrid.ColumnDefinitions.Add (columnDefinition); // DROP ZONE

        //        }


        //        sectionContentGrid.RowDefinitions.Add (new RowDefinition ());


        //        if ((renderMode == RenderEngineMode.Designer) && (showDropZones)) {

        //            dropZone = RenderControl_DropZone (sectionControl, dropZonePosition);

        //            dropZone.SetValue (Grid.RowProperty, 0);

        //            dropZone.SetValue (Grid.ColumnProperty, dropZonePosition);

        //            sectionContentGrid.Children.Add (dropZone);

        //            dropZonePosition = dropZonePosition + 1;

        //            gridColumnPosition = gridColumnPosition + 1;

        //        }

        //        foreach (Client.Core.Forms.Controls.SectionColumn currentColumn in sectionControl.Controls) {

        //            Panel columnPanel = RenderControl_SectionColumn (currentColumn);

        //            columnPanel.SetValue (Grid.RowProperty, 0);

        //            columnPanel.SetValue (Grid.ColumnProperty, gridColumnPosition);

        //            sectionContentGrid.Children.Add (columnPanel);

        //            if ((renderMode == RenderEngineMode.Designer) && (showDropZones)) {

        //                gridColumnPosition = gridColumnPosition + 1;

        //                dropZone = RenderControl_DropZone (sectionControl, dropZonePosition);

        //                dropZone.SetValue (Grid.RowProperty, 0);

        //                dropZone.SetValue (Grid.ColumnProperty, gridColumnPosition);

        //                sectionContentGrid.Children.Add (dropZone);

        //                dropZonePosition = dropZonePosition + 1;

        //            }

        //            gridColumnPosition = gridColumnPosition + 1;

        //        }

        //    }

        //    return sectionGrid;

        //}

        private Panel RenderControl_Section (Client.Core.Forms.Controls.Section sectionControl) {

            Grid sectionGrid = RenderControlPanel (sectionControl);

            Grid sectionContentGrid = ((Grid) ((Border) sectionGrid.Children[0]).Child);


            sectionContentGrid.HorizontalAlignment = HorizontalAlignment.Stretch;


            if (sectionControl.Controls.Count > 0) { // VALID HAS COLUMNS

                for (Int32 currentColumnIndex = 0; currentColumnIndex < sectionControl.Controls.Count; currentColumnIndex++) {

                    // CYCLE THROUGH COLUMN DEFINITIONS

                    ColumnDefinition column = new ColumnDefinition ();

                    column.Width = sectionControl.Controls[currentColumnIndex].Style.SlGridWidth;

                    if (column.Width == GridLength.Auto) { column.Width = new GridLength (.99, GridUnitType.Star); }

                    // column.Width = new GridLength (Double.NaN, GridUnitType.Star);

                    sectionContentGrid.ColumnDefinitions.Add (column);


                    Panel columnPanel = RenderControl_SectionColumn ((Client.Core.Forms.Controls.SectionColumn) sectionControl.Controls[currentColumnIndex]);

                    columnPanel.SetValue (Grid.ColumnProperty, currentColumnIndex);

                    columnPanel.SetValue (Grid.RowProperty, 0);

                    sectionContentGrid.Children.Add (columnPanel);


                }

            }


            RowDefinition sectionRowDefinition = new RowDefinition ();

            sectionRowDefinition.Height = sectionControl.Style.SlGridHeight;

            sectionContentGrid.RowDefinitions.Add (sectionRowDefinition);


            return sectionGrid;

        }

        // OLDER RENDER COLUMN SUPPORTED DROP ZONES
        //private Panel RenderControl_SectionColumn (Client.Core.Forms.Controls.SectionColumn sectionColumnControl) {

        //    Grid sectionColumn = RenderControlPanel (sectionColumnControl);

        //    Grid sectionColumnContent = ((Grid) ((Border) sectionColumn.Children[0]).Child);


        //    StackPanel sectionColumnStackPanel = new StackPanel ();

        //    Panel dropZone;
            
        //    Int32 dropZonePosition = 0;


        //    sectionColumnStackPanel.Name = "FormControl_" + sectionColumnControl.ControlId.ToString () + "_Panel";

        //    sectionColumnStackPanel.HorizontalAlignment = HorizontalAlignment.Stretch;

        //    sectionColumnStackPanel.SetBinding (StackPanel.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", sectionColumnControl.Style, System.Windows.Data.BindingMode.OneWay));

        //    sectionColumnStackPanel.SetBinding (StackPanel.VisibilityProperty, PropertyDataBinding ("Visibility", sectionColumnControl, System.Windows.Data.BindingMode.OneWay));


        //    if (renderMode == RenderEngineMode.Designer) {

        //        dropZone = RenderControl_DropZone (sectionColumnControl, dropZonePosition);

        //        sectionColumnStackPanel.Children.Add (dropZone);

        //    }

        //    foreach (Client.Core.Forms.Control currentChildControl in sectionColumnControl.Controls) {

        //        RenderFormControl (sectionColumnStackPanel, currentChildControl);

        //        if (renderMode == RenderEngineMode.Designer) {

        //            dropZonePosition = dropZonePosition + 1;

        //            dropZone = RenderControl_DropZone (sectionColumnControl, dropZonePosition);

        //            sectionColumnStackPanel.Children.Add (dropZone);

        //        }

        //    }

        //    sectionColumnContent.Children.Add (sectionColumnStackPanel);

        //    return sectionColumn;

        //}

        private Panel RenderControl_SectionColumn (Client.Core.Forms.Controls.SectionColumn sectionColumnControl) {

            Grid sectionColumn = RenderControlPanel (sectionColumnControl);

            Grid sectionColumnContent = ((Grid) ((Border) sectionColumn.Children[0]).Child);


            sectionColumn.Width = Double.NaN; // RESET WIDTH TO AUTO (STRETCH), WIDTH IS SET IN THE SECTION RENDER BY SECTION GRID

            sectionColumnContent.Width = Double.NaN; // RESET WIDTH TO AUTO (STRETCH), WIDTH IS SET IN THE SECTION RENDER BY SECTION GRID

            sectionColumn.HorizontalAlignment = HorizontalAlignment.Stretch;

            sectionColumnContent.HorizontalAlignment = HorizontalAlignment.Stretch;


            StackPanel sectionColumnStackPanel = new StackPanel ();

            foreach (Client.Core.Forms.Control currentChildControl in sectionColumnControl.Controls) {

                RenderFormControl (sectionColumnStackPanel, currentChildControl);

            }

            sectionColumnContent.Children.Add (sectionColumnStackPanel);


            return sectionColumn;

        }


        private Panel RenderControl_Text (Client.Core.Forms.Controls.Text textControl) {

            Grid textGrid = RenderControlPanel (textControl);

            Grid textContentGrid = ((Grid) ((Border) textGrid.Children[0]).Child);

            TextBlock textContent = new TextBlock ();

            //textContentGrid.Background = new SolidColorBrush (Colors.Yellow);


            BindFontStyleToTextBlock (textContent, textControl.Style);

            textContent.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("TextContentHtmlStripped", textControl, System.Windows.Data.BindingMode.OneWay));

            // TODO: STRIP HTML


            textContent.SetValue (Grid.ColumnProperty, 0);

            textContent.SetValue (Grid.RowProperty, 0);


            textContentGrid.Children.Add (textContent);

            return textGrid;

        }

        private Panel RenderControl_Input (Client.Core.Forms.Controls.Input inputControl) {

            Grid inputGrid = RenderControlPanel (inputControl);

            Grid inputContentGrid = ((Grid) ((Border) inputGrid.Children[0]).Child);

            FrameworkElement inputElement = null;


            switch (inputControl.InputType) {

                case global::Mercury.Server.Application.FormControlInputType.Text:

                    #region Text Input

                    if (String.IsNullOrEmpty (inputControl.Mask)) {

                        TextBox inputTextBox = new TextBox ();

                        inputTextBox.SetBinding (TextBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputTextBox.SetBinding (TextBox.IsEnabledProperty, PropertyDataBinding ("Enabled", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputTextBox.SetBinding (TextBox.TabIndexProperty, PropertyDataBinding ("TabIndex", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputTextBox.SetBinding (TextBox.MaxLengthProperty, PropertyDataBinding ("MaxLength", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputTextBox.SetBinding (TextBox.AcceptsReturnProperty, PropertyDataBinding ("SlAcceptsReturns", inputControl, System.Windows.Data.BindingMode.OneWay));

                        //inputTextBox.SetBinding (TextBox.VerticalScrollBarVisibility, PropertyDataBinding ("SlVerticalScrollBarVisibility", inputControl, System.Windows.Data.BindingMode.OneWay));

                        // inputTextBox.VerticalScrollBarVisibility = (inputTextBox.AcceptsReturn) ? ScrollBarVisibility.Visible : ScrollBarVisibility.Hidden;

                        // TODO: FOCUS EVENT (SELECTION ON FOCUS) WHEN IN EDIT MODE

                        inputTextBox.SetBinding (TextBox.TextProperty, PropertyDataBinding ("Text", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;

                        inputTextBox.VerticalAlignment = VerticalAlignment.Center;


                        if (renderMode == RenderEngineMode.Editor) {

                            // TODO: WIRE LOST FOCUS EVENT TO DO ON TEXT CHANGED

                            if (!String.IsNullOrEmpty (inputControl.Validation)) {

                                System.Text.RegularExpressions.Regex validator = new System.Text.RegularExpressions.Regex (inputControl.Validation);

                                if ((!String.IsNullOrEmpty (inputControl.Text)) && (!validator.IsMatch (inputControl.Text))) {

                                    inputTextBox.Background = SolidColorBrush ("#FFFFCC");

                                }

                            }

                            inputTextBox.GotFocus += new RoutedEventHandler(editorPage.Control_GotFocus);

                        }

                        inputElement = inputTextBox;

                    }

                    else {

                        Telerik.Windows.Controls.RadMaskedTextBox inputMaskedTextBox = new Telerik.Windows.Controls.RadMaskedTextBox ();

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.MaskProperty, PropertyDataBinding ("Mask", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.EmptyContentProperty, PropertyDataBinding ("EmptyMessage", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.IsEnabledProperty, PropertyDataBinding ("Enabled", inputControl, System.Windows.Data.BindingMode.OneWay));

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.TabIndexProperty, PropertyDataBinding ("TabIndex", inputControl, System.Windows.Data.BindingMode.OneWay));

                        // TODO: FOCUS EVENT (SELECTION ON FOCUS) WHEN IN EDIT MODE

                        inputMaskedTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.ValueProperty, PropertyDataBinding ("Text", inputControl, System.Windows.Data.BindingMode.OneWay));


                        inputMaskedTextBox.HorizontalAlignment = HorizontalAlignment.Stretch;

                        inputMaskedTextBox.VerticalAlignment = VerticalAlignment.Center;

                        if (renderMode == RenderEngineMode.Editor) {

                            inputMaskedTextBox.GotFocus += new RoutedEventHandler (editorPage.Control_GotFocus);

                        }


                        inputElement = inputMaskedTextBox;

                    }

                    #endregion 

                    break;

                case global::Mercury.Server.Application.FormControlInputType.Numeric:

                    #region Numeric Input

                    Telerik.Windows.Controls.RadNumericUpDown inputNumericUpDown = new Telerik.Windows.Controls.RadNumericUpDown ();

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.IsEnabledProperty, PropertyDataBinding ("SlEnabledAndNotReadOnly", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.TabIndexProperty, PropertyDataBinding ("TabIndex", inputControl, System.Windows.Data.BindingMode.OneWay));

              
                    switch (inputControl.NumericType) {

                        case global::Mercury.Server.Application.FormControlNumericType.Currency:

                            inputNumericUpDown.ValueFormat = Telerik.Windows.Controls.ValueFormat.Currency;

                            inputNumericUpDown.NumberFormatInfo.NumberDecimalDigits = 2;

                            break;

                        case global::Mercury.Server.Application.FormControlNumericType.Integer:

                            inputNumericUpDown.ValueFormat = Telerik.Windows.Controls.ValueFormat.Numeric;

                            inputNumericUpDown.NumberFormatInfo.NumberDecimalDigits = 0;

                            break;

                        case global::Mercury.Server.Application.FormControlNumericType.Number:

                            inputNumericUpDown.ValueFormat = Telerik.Windows.Controls.ValueFormat.Numeric;

                            inputNumericUpDown.NumberFormatInfo.NumberDecimalDigits = 4;

                            break;

                        case global::Mercury.Server.Application.FormControlNumericType.Percent:

                            inputNumericUpDown.ValueFormat = Telerik.Windows.Controls.ValueFormat.Percentage;

                            inputNumericUpDown.NumberFormatInfo.NumberDecimalDigits = 2;

                            break;

                    }

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.MinimumProperty, PropertyDataBinding ("MinValue", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.MaximumProperty, PropertyDataBinding ("MaxValue", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.ShowButtonsProperty, PropertyDataBinding ("ShowSpinButtons", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputNumericUpDown.SetBinding (Telerik.Windows.Controls.RadNumericUpDown.ValueProperty, PropertyDataBinding ("NumericValue", inputControl, System.Windows.Data.BindingMode.OneWay));


                    inputNumericUpDown.HorizontalAlignment = HorizontalAlignment.Stretch;

                    inputNumericUpDown.VerticalAlignment = VerticalAlignment.Center;

                    if (renderMode == RenderEngineMode.Editor) {

                        inputNumericUpDown.GotFocus += new RoutedEventHandler (editorPage.Control_GotFocus);

                    }
                   

                    inputElement = inputNumericUpDown;

                    #endregion 

                    break;

                case global::Mercury.Server.Application.FormControlInputType.DateTime:

                    #region DateTime Input

                    Telerik.Windows.Controls.RadDatePicker inputDatePicker = new Telerik.Windows.Controls.RadDatePicker ();

                    //inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", inputControl, System.Windows.Data.BindingMode.OneWay));
                    
                    //inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.IsEnabledProperty, PropertyDataBinding ("Enabled", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.IsEnabledProperty, PropertyDataBinding ("EnabledAndNotReadOnly", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.TabIndexProperty, PropertyDataBinding ("TabIndex", inputControl, System.Windows.Data.BindingMode.OneWay));

                     //TODO: DISPLAY FORMAT

                    inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.SelectableDateStartProperty, PropertyDataBinding ("MinDate", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.SelectableDateEndProperty, PropertyDataBinding ("MaxDate", inputControl, System.Windows.Data.BindingMode.OneWay));

                    inputDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.SelectedDateProperty, PropertyDataBinding ("SelectedDate", inputControl, System.Windows.Data.BindingMode.OneWay));


                    inputDatePicker.HorizontalAlignment = HorizontalAlignment.Stretch;

                    inputDatePicker.VerticalAlignment = VerticalAlignment.Center;


                    if (renderMode == RenderEngineMode.Editor) {

                        inputDatePicker.GotFocus += new RoutedEventHandler (editorPage.Control_GotFocus);

                    }
                   

                    inputElement = inputDatePicker;

                    #endregion

                    break;

            }

            if (inputElement != null) {

                inputElement.Name = "Input_" + inputControl.ControlId.ToString ();

                RenderControlWithLabelGrid (inputControl, inputContentGrid, inputElement);

            }

            return inputGrid;

        }

        private Panel RenderControl_Selection (Client.Core.Forms.Controls.Selection selectionControl) {

            Grid selectionGrid = RenderControlPanel (selectionControl);

            Grid selectionContentGrid = ((Grid) ((Border) selectionGrid.Children[0]).Child);

            FrameworkElement selectionElement = null;


            switch (selectionControl.SelectionType) {

                case global::Mercury.Server.Application.FormControlSelectionType.DropDownList:

                    #region Drop Down List

                    Telerik.Windows.Controls.RadComboBox selectionComboBox = new Telerik.Windows.Controls.RadComboBox ();

                    selectionComboBox.Name = selectionControl.ControlId.ToString ();

                    selectionComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEnabledProperty, PropertyDataBinding ("Enabled", selectionControl, System.Windows.Data.BindingMode.OneWay));

                    selectionComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", selectionControl, System.Windows.Data.BindingMode.OneWay));

                    selectionComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.TabIndexProperty, PropertyDataBinding ("TabIndex", selectionControl, System.Windows.Data.BindingMode.OneWay));

                    selectionComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", selectionControl.Style, System.Windows.Data.BindingMode.OneWay));     

                    selectionComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;


                    
                    if ((renderMode == RenderEngineMode.Designer) && (selectionControl.DataSource == Server.Application.FormControlDataSource.Reference)) { 

                        selectionComboBox.Text = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Text : String.Empty;

                        selectionComboBox.SelectedValue = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Value : String.Empty;

                    }

                    else if ((renderMode == RenderEngineMode.Editor) && (selectionControl.DataSource == Server.Application.FormControlDataSource.Reference)) {

                        // TODO: HOOK UP LOOK UP EVENTS

                        selectionComboBox.SelectedValue = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Value : String.Empty;

                        selectionComboBox.Text = (selectionControl.SelectedItem != null) ? selectionControl.SelectedItem.Text : String.Empty;

                    }

                    else if (selectionControl.DataSource == Server.Application.FormControlDataSource.ItemList) {

                        //foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                        //    Telerik.Windows.Controls.RadComboBoxItem comboBoxItem = new Telerik.Windows.Controls.RadComboBoxItem ();

                        //    comboBoxItem.IsEnabled = currentItem.Enabled;

                        //    comboBoxItem.Content = currentItem.Text;

                        //    comboBoxItem.Tag = currentItem.Value;

                        //    selectionComboBox.Items.Add (comboBoxItem);

                        //}

                        //selectionComboBox.IsEditable = selectionControl.AllowCustomText;

                        //if (!selectionControl.HasCustomTextValue) {

                        //    selectionComboBox.Text = selectionControl.SelectedValue;

                        //}

                        //else {

                        //    selectionComboBox.Text = selectionControl.CustomText;

                        //}

                        selectionComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.ItemsSourceProperty, PropertyDataBinding ("Items", selectionControl, System.Windows.Data.BindingMode.OneWay));

                        selectionComboBox.SelectedValuePath = "Value";

                        selectionComboBox.DisplayMemberPath = "Text";

                        if (renderMode == RenderEngineMode.Editor) {

                            // TODO: HOOK UP ON CHANGE EVENT

                            // selectionComboBox.TextChanged += new EventHandler (editorPage.FormControl_OnTextChange);

                        }

                    }

                    selectionElement = selectionComboBox;

                    #endregion 

                    break;

                case global::Mercury.Server.Application.FormControlSelectionType.ListBox:

                    #region List Box

                    ListBox selectionListBox = new ListBox ();

                    selectionListBox.Name = selectionControl.ControlId.ToString ();

                    selectionListBox.IsEnabled = ((selectionControl.Enabled) && (!selectionControl.ReadOnly) && (renderMode != RenderEngineMode.Viewer));

                    // if (selectionControl.Rows != 0) { selectionListBox.Height = (selectionControl.Rows * 22); }
 
                    selectionListBox.TabIndex = selectionControl.TabIndex;


                    selectionListBox.SetBinding (ListBox.ItemsSourceProperty, PropertyDataBinding ("Items", selectionControl, System.Windows.Data.BindingMode.OneWay));

                    selectionListBox.DisplayMemberPath = "Text";


                    //foreach (Client.Core.Forms.Structures.SelectionItem currentItem in selectionControl.Items) {

                    //    ListBoxItem listItem = new ListBoxItem ();
                        
                    //    listItem.Content = currentItem.Text;
                        
                    //    listItem.Tag = currentItem.Value;

                    //    listItem.IsEnabled = currentItem.Enabled;

                    //    listItem.IsSelected = currentItem.Selected;

                    //    selectionListBox.Items.Add (listItem);

                    //}

                    if (renderMode == RenderEngineMode.Editor) {

                        // TODO: EVENT HANDLER 

                        // selectionListBox.SelectedIndexChanged += new EventHandler (editorPage.FormControlSelectionControl_OnSelectedIndexChanged);

                    }


                    selectionElement = selectionListBox;

                    #endregion

                    break;

                case global::Mercury.Server.Application.FormControlSelectionType.CheckBox:

                case global::Mercury.Server.Application.FormControlSelectionType.RadioButton:

                    #region Check Boxes and Radio Buttons

                    Int32 totalColumns = selectionControl.Columns;

                    Int32 totalRows = (selectionControl.Items.Count / selectionControl.Columns);

                    if ((selectionControl.Items.Count % selectionControl.Columns) != 0) { totalRows = totalRows + 1; }

                    Int32 currentColumn = 0;

                    Int32 currentRow = 0;

                    Grid ListGrid = new Grid ();

                    ListGrid.SetBinding (Grid.VerticalAlignmentProperty, PropertyDataBinding ("SlVerticalAlignment", selectionControl.Style, System.Windows.Data.BindingMode.OneWay));

                    

                    for (Int32 currentColumnIndex = 0; currentColumnIndex < totalColumns; currentColumnIndex++) {

                        ColumnDefinition listColumnDefinition = new ColumnDefinition ();

                        // COMMENT OUT FOR EVEN DISTRIBUTION OF COLUMNS

                        // listColumnDefinition.Width = new GridLength (0, GridUnitType.Auto);

                        ListGrid.ColumnDefinitions.Add (listColumnDefinition);

                    }

                    for (Int32 currentRowIndex = 0; currentRowIndex < totalRows; currentRowIndex++) {

                        ListGrid.RowDefinitions.Add (new RowDefinition ());

                    }

                    foreach (Client.Core.Forms.Structures.SelectionItem currentSelectionItem in selectionControl.Items) {

                        #region Create List Grid Item

                        System.Windows.Controls.Primitives.ToggleButton item = null;

                        if (selectionControl.SelectionType == global::Mercury.Server.Application.FormControlSelectionType.CheckBox) {

                            item = new CheckBox ();

                            item.SetBinding (CheckBox.TabIndexProperty, PropertyDataBinding ("TabIndex", selectionControl, System.Windows.Data.BindingMode.OneWay));

                        }

                        else {

                            item = new RadioButton ();

                            item.SetBinding (RadioButton.TabIndexProperty, PropertyDataBinding ("TabIndex", selectionControl, System.Windows.Data.BindingMode.OneWay));
                           
                        }

                        
                        item.SetBinding (System.Windows.Controls.Primitives.ToggleButton.IsEnabledProperty, PropertyDataBinding ("IsEnabled", currentSelectionItem, System.Windows.Data.BindingMode.OneWay));

                        item.SetBinding (System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, PropertyDataBinding ("Selected", currentSelectionItem, System.Windows.Data.BindingMode.OneWay));

                        item.Tag = currentSelectionItem;


                        TextBlock itemContent = new TextBlock ();

                        itemContent.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("Text", currentSelectionItem, System.Windows.Data.BindingMode.OneWay));

                        BindFontStyleToTextBlock (itemContent, selectionControl.Style);

                        itemContent.TextWrapping = TextWrapping.Wrap;

                        item.Content = itemContent;

             
                        if (renderMode == RenderEngineMode.Editor) {

                            item.GotFocus += new RoutedEventHandler (editorPage.Control_GotFocus);

                            item.Checked += new RoutedEventHandler(editorPage.FormControlSelection_CheckRadioChanged);

                            if (item is CheckBox) {

                                // ONLY SUPPORT CHECK BOXES, RADIO BUTTONS ARE MUTUALLY EXCLUSIVE, AN UNCHECK AUTOMATICALLY MEANS A NEW CHECK

                                item.Unchecked += new RoutedEventHandler (editorPage.FormControlSelection_CheckRadioChanged);

                            }

                        }

                        #region Place Item in Grid and Increment Location

                        item.SetValue (Grid.ColumnProperty, currentColumn);

                        item.SetValue (Grid.RowProperty, currentRow);

                        ListGrid.Children.Add (item);

                        switch (selectionControl.Direction) {

                            case global::Mercury.Server.Application.FormControlSelectionDirection.Horizontal:

                                currentColumn = currentColumn + 1;

                                if (currentColumn == totalColumns) {

                                    currentColumn = 0;

                                    currentRow = currentRow + 1;

                                }

                                break;

                            case global::Mercury.Server.Application.FormControlSelectionDirection.Vertical:

                                currentRow = currentRow + 1;

                                if (currentRow == totalRows) {

                                    currentRow = 0;

                                    currentColumn = currentColumn + 1;

                                }

                                break;

                        }

                        #endregion 

                        #endregion

                    }

                    selectionElement = ListGrid;

                    #endregion

                    break;

            }

            if (selectionElement != null) {

                selectionElement.Name = "Selection_" + selectionControl.ControlId.ToString ();

                RenderControlWithLabelGrid (selectionControl, selectionContentGrid, selectionElement);

            }

            return selectionGrid;

        }

        private Panel RenderControl_Button (Client.Core.Forms.Controls.Button buttonControl) {

            Grid buttonGrid = RenderControlPanel (buttonControl);

            Grid buttonContentGrid = ((Grid) ((Border) buttonGrid.Children[0]).Child);


            Button button = new Button ();

            button.Name = "Button_" + buttonControl.ControlId.ToString ();

            button.Content = (!String.IsNullOrEmpty (buttonControl.Text)) ? buttonControl.Text : "{ No Text }";


            BindFontStyleToButton (button, buttonControl.Style);

            button.SetBinding (Button.IsEnabledProperty, PropertyDataBinding ("Enabled", buttonControl, System.Windows.Data.BindingMode.OneWay));

            button.SetBinding (Button.TabIndexProperty, PropertyDataBinding ("TabIndex", buttonControl, System.Windows.Data.BindingMode.OneWay));


            buttonContentGrid.Children.Add (button);

            return buttonGrid;

        }


        private Panel RenderControl_EntityMember (Client.Core.Forms.Controls.Entity entityControl) {

            Grid entityExpandedGrid = new Grid ();

            entityExpandedGrid.RowDefinitions.Add (new RowDefinition ());

            entityExpandedGrid.ShowGridLines = false;


            #region Entity Name 

            ColumnDefinition columnEntityNameLabel = new ColumnDefinition ();

            columnEntityNameLabel.Width = new GridLength (1, GridUnitType.Auto);

            columnEntityNameLabel.MinWidth = 100;

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityNameLabel);

            TextBlock textEntityNameLabel = new TextBlock ();

            BindFontStyleToTextBlock (textEntityNameLabel, entityControl.Style);

            textEntityNameLabel.Margin = new Thickness (6, 0, 4, 0);

            textEntityNameLabel.SetValue (Grid.ColumnProperty, 0);

            textEntityNameLabel.SetValue (Grid.RowProperty, 0);

            textEntityNameLabel.Text = "Member Name:";

            entityExpandedGrid.Children.Add (textEntityNameLabel);


            ColumnDefinition columnEntityName = new ColumnDefinition ();

            columnEntityName.Width = new GridLength (1, GridUnitType.Star);

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityName);



            TextBlock textEntityName = new TextBlock ();

            BindFontStyleToTextBlock (textEntityName, entityControl.Style);

            textEntityName.Margin = new Thickness (6, 0, 4, 0);

            textEntityName.SetValue (Grid.ColumnProperty, 1);

            textEntityName.SetValue (Grid.RowProperty, 0);

            textEntityName.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("EntityName", entityControl, System.Windows.Data.BindingMode.OneWay));

            textEntityName.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("EnabledAndNotReadOnly", entityControl, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.BooleanToVisibilityInverse ()));

            entityExpandedGrid.Children.Add (textEntityName);


            if (renderMode != RenderEngineMode.Viewer) {

                Telerik.Windows.Controls.RadComboBox selectionEntityName = new Telerik.Windows.Controls.RadComboBox ();

                selectionEntityName.Name = entityControl.Name + "_" + entityControl.ControlId.ToString () + "_EntityName";


                selectionEntityName.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEnabledProperty, PropertyDataBinding ("EnabledAndNotReadOnly", entityControl, System.Windows.Data.BindingMode.OneWay));

                selectionEntityName.SetBinding (Telerik.Windows.Controls.RadComboBox.TabIndexProperty, PropertyDataBinding ("TabIndex", entityControl, System.Windows.Data.BindingMode.OneWay));

                selectionEntityName.SetBinding (Telerik.Windows.Controls.RadComboBox.TextProperty, PropertyDataBinding ("EntityName", entityControl, System.Windows.Data.BindingMode.OneWay));

                selectionEntityName.SetBinding (Telerik.Windows.Controls.RadComboBox.VisibilityProperty, PropertyDataBinding ("EnabledAndNotReadOnly", entityControl, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.BooleanToVisibility ()));


                selectionEntityName.IsEditable = true;

                selectionEntityName.IsTextSearchEnabled = true;

                selectionEntityName.Tag = entityControl;


                selectionEntityName.SetValue (Grid.ColumnProperty, 1);

                selectionEntityName.SetValue (Grid.RowProperty, 0);

                entityExpandedGrid.Children.Add (selectionEntityName);

            }

            #endregion 


            #region Entity Birth Date

            ColumnDefinition columnEntityBirthDateLabel = new ColumnDefinition ();

            columnEntityBirthDateLabel.Width = new GridLength (1, GridUnitType.Auto);

            columnEntityBirthDateLabel.MinWidth = 65;

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityBirthDateLabel);

            TextBlock textEntityBirthDateLabel = new TextBlock ();

            BindFontStyleToTextBlock (textEntityBirthDateLabel, entityControl.Style);

            textEntityBirthDateLabel.Margin = new Thickness (6, 0, 4, 0);

            textEntityBirthDateLabel.SetValue (Grid.ColumnProperty, 2);

            textEntityBirthDateLabel.SetValue (Grid.RowProperty, 0);

            textEntityBirthDateLabel.Text = "Birth Date:";

            entityExpandedGrid.Children.Add (textEntityBirthDateLabel);


            ColumnDefinition columnEntityBirthDate = new ColumnDefinition ();

            columnEntityBirthDate.Width = new GridLength (1, GridUnitType.Auto);

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityBirthDate);

            TextBlock textEntityBirthDate = new TextBlock ();

            BindFontStyleToTextBlock (textEntityBirthDate, entityControl.Style);

            textEntityBirthDate.Margin = new Thickness (4, 0, 4, 0);

            textEntityBirthDate.SetValue (Grid.ColumnProperty, 3);

            textEntityBirthDate.SetValue (Grid.RowProperty, 0);

            textEntityBirthDate.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("Member.BirthDate", entityControl, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.DateToStringFormatter ()));

            entityExpandedGrid.Children.Add (textEntityBirthDate);

            #endregion 

            
            #region Entity Age

            ColumnDefinition columnEntityAgeLabel = new ColumnDefinition ();

            columnEntityAgeLabel.Width = new GridLength (1, GridUnitType.Auto);

            columnEntityAgeLabel.MinWidth = 80;

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityAgeLabel);

            TextBlock textEntityAgeLabel = new TextBlock ();

            BindFontStyleToTextBlock (textEntityAgeLabel, entityControl.Style);

            textEntityAgeLabel.Margin = new Thickness (6, 0, 4, 0);

            textEntityAgeLabel.SetValue (Grid.ColumnProperty, 4);

            textEntityAgeLabel.SetValue (Grid.RowProperty, 0);

            textEntityAgeLabel.Text = "Current Age:";

            entityExpandedGrid.Children.Add (textEntityAgeLabel);


            ColumnDefinition columnEntityAge = new ColumnDefinition ();

            columnEntityAge.Width = new GridLength (1, GridUnitType.Auto);

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityAge);

            TextBlock textEntityAge = new TextBlock ();

            BindFontStyleToTextBlock (textEntityAge, entityControl.Style);

            textEntityAge.Margin = new Thickness (4, 0, 4, 0);

            textEntityAge.SetValue (Grid.ColumnProperty, 5);

            textEntityAge.SetValue (Grid.RowProperty, 0);

            textEntityAge.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("Member.BirthDate", entityControl, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.DateToAgeInYearsMonthsString ()));

            entityExpandedGrid.Children.Add (textEntityAge);

            #endregion 

            
            #region Entity Gender

            ColumnDefinition columnEntityGenderLabel = new ColumnDefinition ();

            columnEntityGenderLabel.Width = new GridLength (1, GridUnitType.Auto);

            columnEntityGenderLabel.MinWidth = 50;

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityGenderLabel);

            TextBlock textEntityGenderLabel = new TextBlock ();

            BindFontStyleToTextBlock (textEntityGenderLabel, entityControl.Style);

            textEntityGenderLabel.Margin = new Thickness (6, 0, 4, 0);

            textEntityGenderLabel.SetValue (Grid.ColumnProperty, 6);

            textEntityGenderLabel.SetValue (Grid.RowProperty, 0);

            textEntityGenderLabel.Text = "Gender:";

            entityExpandedGrid.Children.Add (textEntityGenderLabel);


            ColumnDefinition columnEntityGender = new ColumnDefinition ();

            columnEntityGender.Width = new GridLength (1, GridUnitType.Auto);

            entityExpandedGrid.ColumnDefinitions.Add (columnEntityGender);

            TextBlock textEntityGender = new TextBlock ();

            BindFontStyleToTextBlock (textEntityGender, entityControl.Style);

            textEntityGenderLabel.Margin = new Thickness (4, 0, 0, 0);

            textEntityGender.SetValue (Grid.ColumnProperty, 7);

            textEntityGender.SetValue (Grid.RowProperty, 0);

            textEntityGender.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("Member.Gender", entityControl, System.Windows.Data.BindingMode.OneWay, new Client.ValueConverters.GenderDescriptionFormatter ()));

            entityExpandedGrid.Children.Add (textEntityGender);

            #endregion 


            return entityExpandedGrid;

        }

        private Panel RenderControl_Entity (Client.Core.Forms.Controls.Entity entityControl) {

            Grid entityGrid = RenderControlPanel (entityControl);

            Grid entityContentGrid = ((Grid) ((Border) entityGrid.Children[0]).Child);

            FrameworkElement entityContent = new TextBlock ();


            switch (entityControl.DisplayStyle) {

                case global::Mercury.Server.Application.FormControlEntityDisplayStyle.NormalExpanded:

                    #region Normal/Expanded Style

                    switch (entityControl.EntityType) {

                        case Mercury.Server.Application.EntityType.Member:

                            entityContent = RenderControl_EntityMember (entityControl);

                            break;

                        default:

                            TextBlock unknownEntityType  = new TextBlock ();

                            unknownEntityType.Text = "Unhandled Entity Type: " + entityControl.EntityType.ToString ();

                            entityContent = unknownEntityType;

                            break;

                    }

                    #endregion

                    break;


                case global::Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly:

                    #region Name Only Style

                    if (renderMode == RenderEngineMode.Viewer) {

                        TextBlock entityNameText = new TextBlock ();

                        entityNameText.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("EntityName", entityControl, System.Windows.Data.BindingMode.OneWay));

                        entityContent = entityNameText;

                    }

                    else {

                        Telerik.Windows.Controls.RadComboBox entityNameComboBox = new Telerik.Windows.Controls.RadComboBox ();

                        entityNameComboBox.Name = entityControl.Name + "_" + entityControl.ControlId.ToString () + "_Name";

                        entityNameComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEditableProperty, PropertyDataBinding ("AllowCustomEntityName", entityControl, System.Windows.Data.BindingMode.OneWay));

                        entityNameComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEnabledProperty, PropertyDataBinding ("Enabled", entityControl, System.Windows.Data.BindingMode.OneWay));

                        entityNameComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", entityControl, System.Windows.Data.BindingMode.OneWay));

                        entityNameComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.TabIndexProperty, PropertyDataBinding ("TabIndex", entityControl, System.Windows.Data.BindingMode.OneWay));

                        entityNameComboBox.HorizontalAlignment = HorizontalAlignment.Stretch;
                        
                        entityNameComboBox.VerticalAlignment = VerticalAlignment.Center;

                        entityNameComboBox.IsTextSearchEnabled = true;

                        entityContent = entityNameComboBox;
                        

                        // TODO: LOAD ON DEMAND SUPPORT

                        // TODO: SET SELECTED VALUE

                    }


                    #endregion

                    break;

            }


            entityContentGrid.Children.Add (entityContent);

            return entityGrid;

        }

        private Panel RenderControl_Collection (Client.Core.Forms.Controls.Collection collectionControl) {

            Grid collectionGrid = RenderControlPanel (collectionControl);

            Grid collectionContentGrid = ((Grid) ((Border) collectionGrid.Children[0]).Child);

            
            Telerik.Windows.Controls.RadGridView collectionGridView = new Telerik.Windows.Controls.RadGridView ();

            collectionGridView.Name = collectionGrid.Name + "_GridView";

            collectionGridView.HorizontalAlignment = HorizontalAlignment.Stretch;

            collectionGridView.AutoGenerateColumns = true;

            collectionGridView.IsReadOnly = true;

            collectionGridView.SetBinding (Telerik.Windows.Controls.RadGridView.TabIndexProperty, PropertyDataBinding ("TabIndex", collectionControl, System.Windows.Data.BindingMode.OneWay));

                      

            collectionContentGrid.Children.Add (collectionGridView);

            return collectionGrid;

        }

        private Panel RenderControl_Address (Client.Core.Forms.Controls.Address addressControl) {

            Grid addressGrid = RenderControlPanel (addressControl);

            Grid addressContentGrid = ((Grid) ((Border) addressGrid.Children[0]).Child);

            TextBlock labelText;

            TextBox addressTextBox;


            #region Grid Table Definitions

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.30, GridUnitType.Star)));

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            addressContentGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.10, GridUnitType.Star)));



            addressContentGrid.RowDefinitions.Add (new RowDefinition ());

            addressContentGrid.RowDefinitions.Add (new RowDefinition ());

            addressContentGrid.RowDefinitions.Add (new RowDefinition ());

            #endregion


            #region Address Line 1

            labelText = RenderSimpleTextBlock ("Line 1:", addressControl.Style, 0, 0);

            addressContentGrid.Children.Add (labelText);


            addressTextBox = new TextBox ();

            addressTextBox.Name = addressControl.ControlId.ToString () + "_Line1";

            addressTextBox.SetValue (Grid.ColumnProperty, 1);

            addressTextBox.SetValue (Grid.RowProperty, 0);

            addressTextBox.SetValue (Grid.ColumnSpanProperty, 5);

            addressTextBox.VerticalAlignment = VerticalAlignment.Center;


            BindFontStyleToTextBox (addressTextBox, addressControl.Style);

            addressTextBox.SetBinding (TextBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", addressControl, System.Windows.Data.BindingMode.OneWay));

            addressTextBox.SetBinding (TextBox.IsEnabledProperty, PropertyDataBinding ("Enabled", addressControl, System.Windows.Data.BindingMode.OneWay));

            if (renderMode == RenderEngineMode.Editor) {

                addressTextBox.Tag = addressControl;

                addressTextBox.SetBinding (TextBox.TextProperty, PropertyDataBinding ("Line1", addressControl, System.Windows.Data.BindingMode.OneWay));

                addressTextBox.LostFocus += new RoutedEventHandler (editorPage.FormControlAddress_LostFocus);

            }

            else { addressTextBox.Text = addressControl.Line1; }

            addressContentGrid.Children.Add (addressTextBox);

            #endregion 


            #region Address Line 2

            labelText = RenderSimpleTextBlock ("Line 2:", addressControl.Style, 0, 1);

            addressContentGrid.Children.Add (labelText);


            addressTextBox = new TextBox ();

            addressTextBox.Name = addressControl.ControlId.ToString () + "_Line2";

            addressTextBox.SetValue (Grid.ColumnProperty, 1);

            addressTextBox.SetValue (Grid.RowProperty, 1);

            addressTextBox.SetValue (Grid.ColumnSpanProperty, 5);

            addressTextBox.VerticalAlignment = VerticalAlignment.Center;


            BindFontStyleToTextBox (addressTextBox, addressControl.Style);

            addressTextBox.SetBinding (TextBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", addressControl, System.Windows.Data.BindingMode.OneWay));

            addressTextBox.SetBinding (TextBox.IsEnabledProperty, PropertyDataBinding ("Enabled", addressControl, System.Windows.Data.BindingMode.OneWay));

            if (renderMode == RenderEngineMode.Editor) {

                addressTextBox.Tag = addressControl;

                addressTextBox.SetBinding (TextBox.TextProperty, PropertyDataBinding ("Line2", addressControl, System.Windows.Data.BindingMode.OneWay));

                addressTextBox.LostFocus += new RoutedEventHandler (editorPage.FormControlAddress_LostFocus);

            }

            else { addressTextBox.Text = addressControl.Line2; }

            addressContentGrid.Children.Add (addressTextBox);

            #endregion 


            #region Zip Code

            labelText = RenderSimpleTextBlock ("Zip Code:", addressControl.Style, 0, 2);

            addressContentGrid.Children.Add (labelText);


            Telerik.Windows.Controls.RadMaskedTextBox zipCodeTextBox = new Telerik.Windows.Controls.RadMaskedTextBox ();

            zipCodeTextBox.Name = addressControl.ControlId.ToString () + "_ZipCode";

            zipCodeTextBox.Mask = "00000-####";

            zipCodeTextBox.SetValue (Grid.ColumnProperty, 1);

            zipCodeTextBox.SetValue (Grid.RowProperty, 2);

            zipCodeTextBox.VerticalAlignment = VerticalAlignment.Center;

            zipCodeTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.ValueProperty, PropertyDataBinding ("ZipCode", addressControl, System.Windows.Data.BindingMode.OneWay));

            BindFontStyleToRadMaskedTextBox (zipCodeTextBox, addressControl.Style);

            zipCodeTextBox.SetBinding (Telerik.Windows.Controls.RadMaskedTextBox.IsReadOnlyProperty, PropertyDataBinding ("ReadOnly", addressControl, System.Windows.Data.BindingMode.OneWay));


            if (renderMode == RenderEngineMode.Editor) {

                zipCodeTextBox.Tag = addressControl;

                zipCodeTextBox.LostFocus += new RoutedEventHandler (editorPage.FormControlAddress_LostFocus);

            }


            addressContentGrid.Children.Add (zipCodeTextBox);

            #endregion 


            #region City

            labelText = RenderSimpleTextBlock ("City:", addressControl.Style, 2, 2);

            addressContentGrid.Children.Add (labelText);



            Telerik.Windows.Controls.RadComboBox cityComboBox = new Telerik.Windows.Controls.RadComboBox ();

            cityComboBox.Name = addressControl.ControlId.ToString () + "_City";

            cityComboBox.SetValue (Grid.ColumnProperty, 3);

            cityComboBox.SetValue (Grid.RowProperty, 2);

            cityComboBox.VerticalAlignment = VerticalAlignment.Center;

            cityComboBox.IsEditable = true;


            cityComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEnabledProperty, PropertyDataBinding ("EnabledAndNotReadOnly", addressControl, System.Windows.Data.BindingMode.OneWay));

            cityComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, PropertyDataBinding ("City", addressControl, System.Windows.Data.BindingMode.OneWay));


            cityComboBox.Tag = addressControl;

            addressContentGrid.Children.Add (cityComboBox);

            if (renderMode == RenderEngineMode.Editor) {

                MercuryApplication.CityReferenceByState (addressControl.State, true, editorPage.FormControlAddress_CityReferenceByStateCompleted);

                cityComboBox.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler (editorPage.FormControlAddress_CitySelectionChanged);

            }                

            #endregion 

            
            #region State

            labelText = RenderSimpleTextBlock ("State:", addressControl.Style, 4, 2);

            addressContentGrid.Children.Add (labelText);


            Telerik.Windows.Controls.RadComboBox stateComboBox = new Telerik.Windows.Controls.RadComboBox ();

            stateComboBox.Name = addressControl.ControlId.ToString () + "_State";

            stateComboBox.SetValue (Grid.ColumnProperty, 5);

            stateComboBox.SetValue (Grid.RowProperty, 2);

            stateComboBox.VerticalAlignment = VerticalAlignment.Center;

            stateComboBox.IsEditable = true;


            stateComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.IsEnabledProperty, PropertyDataBinding ("EnabledAndNotReadOnly", addressControl, System.Windows.Data.BindingMode.OneWay));

            stateComboBox.SetBinding (Telerik.Windows.Controls.RadComboBox.SelectedValueProperty, PropertyDataBinding ("State", addressControl, System.Windows.Data.BindingMode.OneWay));


            stateComboBox.Tag = addressControl;

            addressContentGrid.Children.Add (stateComboBox);


            if (renderMode == RenderEngineMode.Editor) {

                if (!isStateReferenceRequested) {

                    isStateReferenceRequested = true;

                    MercuryApplication.StateReference (true, editorPage.FormControlAddress_StateReferenceCompleted);

                }


                stateComboBox.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler (editorPage.FormControlAddress_StateSelectionChanged);
               

            }                

            

            #endregion 


            return addressGrid;

        }


        private Panel RenderControl_Service (Client.Core.Forms.Controls.Service serviceControl) {

            Grid serviceGrid = RenderControlPanel (serviceControl);

            Grid serviceContentGrid = ((Grid) ((Border) serviceGrid.Children[0]).Child);

            Grid serviceCompositeGrid = new Grid ();

            FrameworkElement serviceElement = null;


            TextBlock labelText;

            Telerik.Windows.Controls.RadDatePicker serviceDatePicker;


            serviceCompositeGrid.HorizontalAlignment = HorizontalAlignment.Stretch;


            #region Grid Table Definitions

            Double nameColumnWidth = .40;

            if (!serviceControl.MostRecentMemberServiceDateVisible) { nameColumnWidth = nameColumnWidth + .30; }

            if (!serviceControl.ServiceDateVisible) { nameColumnWidth = nameColumnWidth + .30; }


            serviceCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (nameColumnWidth, GridUnitType.Star))); // NAME


            if (serviceControl.MostRecentMemberServiceDateVisible) {

                serviceCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star))); // LAST DATE

                serviceCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star))); // LAST DATE

            }


            if (serviceControl.ServiceDateVisible) {

                serviceCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star))); // SERVICE DATE

                serviceCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star))); // SERVICE DATE

            }


            serviceCompositeGrid.RowDefinitions.Add (new RowDefinition ());

            #endregion


            #region Service Name Cell

            labelText = new TextBlock ();

            labelText.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("ServiceName", serviceControl, System.Windows.Data.BindingMode.OneWay));

            labelText.SetValue (Grid.ColumnProperty, 0);

            labelText.SetValue (Grid.RowProperty, 0);

            BindFontStyleToTextBlock (labelText, serviceControl.Style);

            labelText.HorizontalAlignment = HorizontalAlignment.Stretch;

            labelText.VerticalAlignment = VerticalAlignment.Center;

            serviceCompositeGrid.Children.Add (labelText);

            #endregion


            #region Service Last Date

            if (serviceControl.MostRecentMemberServiceDateVisible) {

                labelText = new TextBlock ();

                labelText.Text = "Last Date: ";

                labelText.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("ServiceLastDateVisibility", serviceControl, System.Windows.Data.BindingMode.OneWay));

                labelText.SetValue (Grid.ColumnProperty, 1);

                labelText.SetValue (Grid.RowProperty, 0);

                BindFontStyleToTextBlock (labelText, serviceControl.Style);

                labelText.HorizontalAlignment = HorizontalAlignment.Center;

                labelText.VerticalAlignment = VerticalAlignment.Center;

                serviceCompositeGrid.Children.Add (labelText);


                labelText = new TextBlock ();

                labelText.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("ServiceLastDateText", serviceControl, System.Windows.Data.BindingMode.OneWay));

                labelText.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("ServiceLastDateVisibility", serviceControl, System.Windows.Data.BindingMode.OneWay));

                labelText.MinWidth = 80;

                labelText.SetValue (Grid.ColumnProperty, 2);

                labelText.SetValue (Grid.RowProperty, 0);

                BindFontStyleToTextBlock (labelText, serviceControl.Style);

                labelText.HorizontalAlignment = HorizontalAlignment.Center;

                labelText.VerticalAlignment = VerticalAlignment.Center;

                serviceCompositeGrid.Children.Add (labelText);

            }

            #endregion


            #region Service Date Entry

            if (serviceControl.ServiceDateVisible) {

                labelText = new TextBlock ();

                labelText.Text = "Service Date: ";

                labelText.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("ServiceDateVisibility", serviceControl, System.Windows.Data.BindingMode.OneWay));


                if (serviceControl.MostRecentMemberServiceDateVisible) {

                    labelText.SetValue (Grid.ColumnProperty, 3);

                }

                else {

                    labelText.SetValue (Grid.ColumnProperty, 1);

                }

                labelText.SetValue (Grid.RowProperty, 0);


                BindFontStyleToTextBlock (labelText, serviceControl.Style);

                labelText.HorizontalAlignment = HorizontalAlignment.Center;

                labelText.VerticalAlignment = VerticalAlignment.Center;

                serviceCompositeGrid.Children.Add (labelText);


                serviceDatePicker = new Telerik.Windows.Controls.RadDatePicker ();

                serviceDatePicker.HorizontalAlignment = HorizontalAlignment.Stretch;

                serviceDatePicker.VerticalAlignment = VerticalAlignment.Center;

                serviceDatePicker.MinWidth = 80;

                serviceDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.VisibilityProperty, PropertyDataBinding ("ServiceDateVisibility", serviceControl, System.Windows.Data.BindingMode.OneWay));

                serviceDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.TabIndexProperty, PropertyDataBinding ("TabIndex", serviceControl, System.Windows.Data.BindingMode.OneWay));

                serviceDatePicker.TabIndex = serviceControl.TabIndex;

                serviceDatePicker.SetValue (Grid.ColumnProperty, 4);

                serviceDatePicker.SetValue (Grid.RowProperty, 0);

                serviceDatePicker.Tag = serviceControl;


                if (renderMode == RenderEngineMode.Editor) {

                    serviceDatePicker.SelectionChanged += new Telerik.Windows.Controls.SelectionChangedEventHandler (editorPage.FormControlService_ServiceDateChanged);

                }

                serviceCompositeGrid.Children.Add (serviceDatePicker);

            }

            #endregion


            serviceElement = serviceCompositeGrid;

            if (serviceElement != null) {

                serviceElement.Name = "Service_" + serviceControl.ControlId.ToString ();

                RenderControlWithLabelGrid (serviceControl, serviceContentGrid, serviceElement);

            }

            return serviceGrid;

        }

        private Panel RenderControl_Metric (Client.Core.Forms.Controls.Metric metricControl) {

            Grid metricGrid = RenderControlPanel (metricControl);

            Grid metricContentGrid = ((Grid) ((Border) metricGrid.Children[0]).Child);

            Grid metricCompositeGrid = new Grid ();

            FrameworkElement metricElement = null;


            TextBlock labelText;

            Telerik.Windows.Controls.RadDatePicker metricDatePicker;


            metricCompositeGrid.HorizontalAlignment = HorizontalAlignment.Stretch;


            #region Grid Table Definitions

            metricCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.40, GridUnitType.Star)));

            metricCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            metricCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            metricCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));

            metricCompositeGrid.ColumnDefinitions.Add (columnDefinitionForWidth (new GridLength (.15, GridUnitType.Star)));


            metricCompositeGrid.RowDefinitions.Add (new RowDefinition ());

            #endregion


            #region Metric Name Cell

            labelText = new TextBlock ();

            labelText.SetBinding (TextBlock.TextProperty, PropertyDataBinding ("MetricNameText", metricControl, System.Windows.Data.BindingMode.OneWay));

            labelText.SetValue (Grid.ColumnProperty, 0);

            labelText.SetValue (Grid.RowProperty, 0);

            BindFontStyleToTextBlock (labelText, metricControl.Style);

            labelText.HorizontalAlignment = HorizontalAlignment.Stretch;

            labelText.VerticalAlignment = VerticalAlignment.Center;

            metricCompositeGrid.Children.Add (labelText);

            #endregion


            #region Metric Date

            labelText = new TextBlock ();

            labelText.Text = "Date: ";

//            labelText.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("MetricDateVisibility", metricControl, System.Windows.Data.BindingMode.OneWay));

            labelText.SetValue (Grid.ColumnProperty, 1);

            labelText.SetValue (Grid.RowProperty, 0);

            BindFontStyleToTextBlock (labelText, metricControl.Style);

            labelText.HorizontalAlignment = HorizontalAlignment.Center;

            labelText.VerticalAlignment = VerticalAlignment.Center;

            metricCompositeGrid.Children.Add (labelText);


            metricDatePicker = new Telerik.Windows.Controls.RadDatePicker ();

            metricDatePicker.SetValue (Grid.ColumnProperty, 2);

            metricDatePicker.SetValue (Grid.RowProperty, 0);

            metricDatePicker.HorizontalAlignment = HorizontalAlignment.Stretch;

            metricDatePicker.VerticalAlignment = VerticalAlignment.Center;

            metricDatePicker.MinWidth = 80;

//            metricDatePicker.SetBinding (Telerik.Windows.Controls.RadDatePicker.VisibilityProperty, PropertyDataBinding ("MetricDateVisibility", metricControl, System.Windows.Data.BindingMode.OneWay));

            metricCompositeGrid.Children.Add (metricDatePicker);

            #endregion 


            #region Metric Value

            labelText = new TextBlock ();

            labelText.Text = "Value: ";

            //            labelText.SetBinding (TextBlock.VisibilityProperty, PropertyDataBinding ("MetricValueVisibility", metricControl, System.Windows.Data.BindingMode.OneWay));

            labelText.SetValue (Grid.ColumnProperty, 3);

            labelText.SetValue (Grid.RowProperty, 0);

            BindFontStyleToTextBlock (labelText, metricControl.Style);

            labelText.HorizontalAlignment = HorizontalAlignment.Center;

            labelText.VerticalAlignment = VerticalAlignment.Center;

            metricCompositeGrid.Children.Add (labelText);


            Telerik.Windows.Controls.RadNumericUpDown metricValue = new Telerik.Windows.Controls.RadNumericUpDown ();

            metricValue.SetValue (Grid.ColumnProperty, 4);

            metricValue.SetValue (Grid.RowProperty, 0);

            metricValue.HorizontalAlignment = HorizontalAlignment.Stretch;

            metricValue.VerticalAlignment = VerticalAlignment.Center;

            metricValue.MinWidth = 80;

            ////            metricValuePicker.SetBinding (Telerik.Windows.Controls.RadValuePicker.VisibilityProperty, PropertyDataBinding ("MetricValueVisibility", metricControl, System.Windows.Data.BindingMode.OneWay));

            metricCompositeGrid.Children.Add (metricValue);

            #endregion 


            metricElement = metricCompositeGrid;

            if (metricElement != null) {

                metricElement.Name = metricGrid.Name + "_MetricComposite";

                RenderControlWithLabelGrid (metricControl, metricContentGrid, metricElement);

            }

            return metricGrid;

        }

        public Panel RenderFormControlPanel (Client.Core.Forms.Control control) {

            Panel controlContent = null;

            DateTime startTime = DateTime.Now;

            switch (control.ControlType) {

                case Server.Application.FormControlType.Section:

                    controlContent = RenderControl_Section ((Client.Core.Forms.Controls.Section) control);

                    break;


                case Server.Application.FormControlType.SectionColumn:

                    controlContent = RenderControl_SectionColumn ((Client.Core.Forms.Controls.SectionColumn) control);

                    break;

                case Server.Application.FormControlType.Text:

                    controlContent = RenderControl_Text ((Client.Core.Forms.Controls.Text) control);

                    break;

                case Server.Application.FormControlType.Input:

                    controlContent = RenderControl_Input ((Client.Core.Forms.Controls.Input) control);

                    break;

                case Server.Application.FormControlType.Selection:

                    controlContent = RenderControl_Selection ((Client.Core.Forms.Controls.Selection) control);

                    break;

                case Server.Application.FormControlType.Button:

                    controlContent = RenderControl_Button ((Client.Core.Forms.Controls.Button) control);

                    break;

                case Server.Application.FormControlType.Entity:

                    controlContent = RenderControl_Entity ((Client.Core.Forms.Controls.Entity) control);

                    break;

                case Server.Application.FormControlType.Collection:

                    controlContent = RenderControl_Collection ((Client.Core.Forms.Controls.Collection) control);

                    break;

                case Server.Application.FormControlType.Address:

                    controlContent = RenderControl_Address ((Client.Core.Forms.Controls.Address) control);

                    break;

                case Server.Application.FormControlType.Service:

                    controlContent = RenderControl_Service ((Client.Core.Forms.Controls.Service) control);

                    break;

                case Server.Application.FormControlType.Metric:

                    controlContent = RenderControl_Metric ((Client.Core.Forms.Controls.Metric) control);

                    break;

                //default:

                //    Mercury.Client.Core.Forms.Controls.Label labelUnsupportedControl = new Mercury.Client.Core.Forms.Controls.Label (application);

                //    labelUnsupportedControl.Text = "Not Implemented (" + control.GetType ().ToString () + ").";

                //    controlContent = RenderControl_Label (labelUnsupportedControl, control);

                //    System.Diagnostics.Debug.WriteLine ("Not Implemented (" + control.GetType ().ToString () + ").");

                //    break;

                default:

                    controlContent = RenderControlPanel (control);

                    break;

            }

            return controlContent;

        }

        private void RenderFormControl (Panel parentControl, Client.Core.Forms.Control control) {

            DateTime startTime = DateTime.Now;

            Panel controlContent = RenderFormControlPanel (control);

          
            if (controlContent != null) { parentControl.Children.Add (controlContent); }

            if ((DateTime.Now.Subtract (startTime).Milliseconds > 5)

                && (control.ControlType != Server.Application.FormControlType.Section)

                && (control.ControlType != Server.Application.FormControlType.SectionColumn)) {

                System.Diagnostics.Debug.WriteLine ("Render Form Control (" + control.ControlType.ToString () + ": " + control.Name + "): " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

            }

            return;

        }


        // OLD RENDER FORM SUPPORT DROP ZONES, REMOVED
        //public FrameworkElement RenderForm (Client.Core.Forms.Form form) {

        //    if (form == null) { return null; }

        //    DateTime startTime = DateTime.Now;


        //    Grid formPanel = RenderControlPanel (form);

        //    Grid formContent = ((Grid) ((Border) formPanel.Children[0]).Child);

        //    StackPanel formStackPanel = new StackPanel ();

        //    Panel dropZone;

        //    Int32 dropZonePosition = 0;


        //    if (renderMode == RenderEngineMode.Designer) {

        //        dropZone = RenderControl_DropZone (form, dropZonePosition);

        //        formStackPanel.Children.Add (dropZone);

        //    }


        //    foreach (Client.Core.Forms.Control currentChildControl in form.Controls) {

        //        RenderFormControl (formStackPanel, currentChildControl);

        //        if (renderMode == RenderEngineMode.Designer) {

        //            dropZonePosition = dropZonePosition + 1;

        //            dropZone = RenderControl_DropZone (form, dropZonePosition);

        //            formStackPanel.Children.Add (dropZone);

        //        }

        //    }


        //    form.EventResults.Clear ();

        //    formContent.Children.Add (formStackPanel);


        //    System.Diagnostics.Debug.WriteLine ("Engine Render Form: " + DateTime.Now.Subtract (startTime).Milliseconds.ToString ());

        //    return formPanel;

        //}

        public FrameworkElement RenderForm (Client.Core.Forms.Form form) {

            if (form == null) { return null; }

            form.EventResults.Clear ();

            
            Grid formPanel = RenderControlPanel (form);

            Grid formContent = ((Grid) ((Border) formPanel.Children[0]).Child);


            StackPanel sectionsStackPanel = new StackPanel ();

            foreach (Client.Core.Forms.Control currentSection in form.Controls) {

                RenderFormControl (sectionsStackPanel, currentSection);

            }

            formContent.Children.Add (sectionsStackPanel);

            return formPanel;

        }

        public FrameworkElement RenderForm (Client.Core.Forms.Form form, Boolean forShowDropZones) {

            Boolean saveShowDropZones = showDropZones;

            showDropZones = forShowDropZones;


            FrameworkElement renderedForm = RenderForm (form);


            showDropZones = saveShowDropZones;

            return renderedForm;

        }

        #endregion 

    }

    public enum RenderEngineMode { Designer, Editor, Viewer }

}
