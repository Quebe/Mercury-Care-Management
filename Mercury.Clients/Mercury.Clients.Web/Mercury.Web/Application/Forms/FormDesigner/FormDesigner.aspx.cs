using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Forms.FormDesigner {

	public partial class FormDesigner : System.Web.UI.Page 	{

        private DateTime pageStartTime = DateTime.Now;


        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion 


        #region Private Session Cache

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private System.Data.DataTable SelectionItemsGridTable {

            get {

                System.Data.DataTable selectionItemsTable = new System.Data.DataTable ();

                selectionItemsTable.Columns.Add ("ItemText");

                selectionItemsTable.Columns.Add ("ItemValue");

                selectionItemsTable.Columns.Add ("ItemEnabled");

                selectionItemsTable.Columns.Add ("ItemSelected");


                if (!String.IsNullOrEmpty (PropertiesSelectedControlId.Text)) {

                    Mercury.Client.Core.Forms.Control selectedControl;

                    selectedControl = DesignerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

                    if (selectedControl != null) {

                        if (selectedControl.ControlType == Mercury.Server.Application.FormControlType.Selection) {

                            foreach (Client.Core.Forms.Structures.SelectionItem currentItem in ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Items) {

                                selectionItemsTable.Rows.Add (currentItem.Text, currentItem.Value, currentItem.Enabled.ToString (), currentItem.Selected.ToString ());

                            }

                        }

                    }

                }

                return selectionItemsTable;

            }

        }

        private Mercury.Client.Core.Forms.Form DesignerForm {

            get {

                Mercury.Client.Core.Forms.Form designerForm = (Mercury.Client.Core.Forms.Form)Session[SessionCachePrefix + "DesignerForm"];

                if (designerForm == null) {

                    designerForm = new Mercury.Client.Core.Forms.Form (MercuryApplication);

                    designerForm.FormType = Mercury.Server.Application.FormType.Template;

                    Mercury.Client.Core.Forms.Controls.Section initialSection = new Mercury.Client.Core.Forms.Controls.Section (MercuryApplication, designerForm);

                    designerForm.InsertNewControl (0, initialSection);

                    Session[SessionCachePrefix + "DesignerForm"] = designerForm;

                }

                return designerForm;

            }

            set { Session[SessionCachePrefix + "DesignerForm"] = value; }

        }

        private Boolean DesignerModeForm {

            get {

                Boolean designerModeForm = true;

                if (Session[SessionCachePrefix + "DesignerModeForm"] != null) {

                    designerModeForm = (Boolean)Session[SessionCachePrefix + "DesignerModeForm"];

                }

                return designerModeForm;

            }

            set { Session[SessionCachePrefix + "DesignerModeForm"] = value; }

        }

        private String FormExplorerTree_SelectNode {

            get {

                String selectedNode = String.Empty;

                if (Session[SessionCachePrefix + "FormExplorerTree_SelectNode"] != null) {

                    selectedNode = (String)Session[SessionCachePrefix + "FormExplorerTree_SelectNode"];

                }

                return selectedNode;

            }

            set { Session[SessionCachePrefix + "FormExplorerTree_SelectNode"] = value; }

        }


        #endregion


        #region Private Properties

        private Boolean isFormRendered = false;

        private FormRenderEngine renderEngine;

        private Mercury.Server.Application.FormCompileMessageCollectionResponse compileOutput = new Mercury.Server.Application.FormCompileMessageCollectionResponse ();

        #endregion


        #region Page Events

        protected void Page_PreInit (Object sender, EventArgs e) {

            pageStartTime = DateTime.Now;

            return;

        }

        protected void Page_Load (object sender, EventArgs e) {

            if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.FormDesigner)) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            Response.Cache.SetCacheability (HttpCacheability.NoCache);

            Response.Cache.SetExpires (DateTime.Now);


            renderEngine = new FormRenderEngine (this, MercuryApplication, Session, SessionCachePrefix);

            Page.Title = "Mercury - Form Designer [" + MercuryApplication.Session.EnvironmentName + "]";

            InitializeDesignerTitleBarDock ();

            if (!IsPostBack) {

                if (!String.IsNullOrEmpty ((String)Request.QueryString["FormId"])) {

                    DesignerForm = MercuryApplication.FormGet (Int64.Parse ((String)Request.QueryString["FormId"]));

                }

                else if (!String.IsNullOrEmpty (Request.QueryString["FormName"])) {

                    DesignerForm = MercuryApplication.FormGet ((String)Request.QueryString["FormName"]);

                }

            }

            TelerikAjaxManager.ResponseScripts.Add ("FormDesignerState_Reset ();");

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (Page_Load): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void Page_LoadComplete (Object sender, EventArgs e) {

            DateTime loadCompleteTime = DateTime.Now;

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (Page_LoadComplete->START): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            RenderForm ();

            InitializeProperties ();

            InitializeCompileOutput ();

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (Page_LoadComplete->END  ): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (Page_LoadComplete->TOTAL): " + DateTime.Now.Subtract (loadCompleteTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeDesignerTitleBarDock () {

            ApplicationTitle.Text = MercuryApplication.Session.UserDisplayName + " [" + MercuryApplication.Session.EnvironmentName + "]";

            return;

        }


        private void FormExplorerTree_AddNode (Telerik.Web.UI.RadTreeNode parentNode, Client.Core.Forms.Control formControl) {

            Telerik.Web.UI.RadTreeNode currentNode;

            String nodeText = formControl.Name;

            //if ((formControl.ReadOnly) || (!formControl.Visible) || (formControl.Required)) {

            //    if (formControl.Required) { nodeText = nodeText + " { Required }"; }

            //    if (formControl.ReadOnly) { nodeText = nodeText + " { Read Only }"; }

            //    if (!formControl.Visible) { nodeText = nodeText + " { Not Visible }"; }

            //}


            currentNode = new Telerik.Web.UI.RadTreeNode ();

            currentNode.Text = nodeText;

            currentNode.Value = formControl.ControlId.ToString ();

            currentNode.Category = formControl.ControlType.ToString ();

            currentNode.ImageUrl = "/Images/Common16/" + formControl.ControlType.ToString () + ".png";

            currentNode.ExpandMode = Telerik.Web.UI.TreeNodeExpandMode.ClientSide;

            parentNode.Nodes.Add (currentNode);

            foreach (Client.Core.Forms.Control currentChildControl in formControl.Controls) {

                FormExplorerTree_AddNode (currentNode, currentChildControl);

            }

            return;

        }

        private void InitializeFormExplorerTree () {

            // SAVE CURRENT STATE TO RE-EXPAND

            Telerik.Web.UI.RadTreeNode currentNodeState = null;

            if (FormExplorerTree.Nodes.Count != 0) { currentNodeState = FormExplorerTree.Nodes[0]; }


            Telerik.Web.UI.RadTreeNode rootNode = new Telerik.Web.UI.RadTreeNode ();

            FormExplorerTree_AddNode (rootNode, DesignerForm);

            FormExplorerTree.Nodes.Add (rootNode.Nodes[0]);

            FormExplorerTree.Nodes[0].Expanded = true;


            if (FormExplorerTree.FindNodeByValue (FormExplorerTree_SelectNode) != null) {

                FormExplorerTree.FindNodeByValue (FormExplorerTree_SelectNode).Selected = true;

            }

            return;

        }


        protected void InitializeHomeToolbars () {

            Client.Core.Forms.Form designerForm = DesignerForm;

            if ((designerForm == null) || (MercuryApplication == null)) { return; }

            ((Telerik.Web.UI.RadToolBarButton)HomeToolbar.FindItemByValue ("DesignerModeForm")).Checked = DesignerModeForm;

            ((Telerik.Web.UI.RadToolBarButton)HomeToolbar.FindItemByValue ("DesignerModeTree")).Checked = !DesignerModeForm;

            InteractiveFormDesignerContainer.Attributes.Add ("style", ((!DesignerModeForm) ? "display: none;" : "display: block; width: 100%;"));

            FormExplorerTreeContainer.Attributes.Add ("style", ((DesignerModeForm) ? "display: none;" : "display: block; width: 100%;"));

            return;

        }

        protected void InitializeStyleToolbars () {

            Mercury.Client.Core.Forms.Control selectedControl;

            Boolean stylePropertiesForControl = true;

            Client.Core.Forms.Form designerForm = DesignerForm;

            if ((designerForm == null) || (MercuryApplication == null)) { return; }

            if (string.IsNullOrEmpty (PropertiesSelectedControlId.Text)) {

                PropertiesSelectedControlId.Text = designerForm.ControlId.ToString ();

            }


            #region Preset Defaults

            StyleToolbarControlSelection.Items[0].Enabled = false;

            StyleToolbarControlSelection.Items[2].Enabled = false;



            Telerik.Web.UI.RadComboBox StyleToolbarFont1FontSelection = (Telerik.Web.UI.RadComboBox)StyleToolbarFont1.Items[0].FindControl ("StyleToolbarFont1FontSelection");

            if (StyleToolbarFont1FontSelection != null) {

                for (Int32 currentFontIndex = 0; currentFontIndex < StyleToolbarFont1FontSelection.Items.Count; currentFontIndex++) {

                    StyleToolbarFont1FontSelection.Items[currentFontIndex].DataBind ();

                }

                StyleToolbarFont1FontSelection.SelectedValue = String.Empty;

            }

            Telerik.Web.UI.RadComboBox StyleToolbarFont1SizeSelection = (Telerik.Web.UI.RadComboBox)StyleToolbarFont1.Items[1].FindControl ("StyleToolbarFont1SizeSelection");

            if (StyleToolbarFont1SizeSelection != null) { StyleToolbarFont1SizeSelection.SelectedValue = String.Empty; }


            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[0]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[1]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[2]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[3]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[4]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[5]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[0]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[1]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[2]).Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[3]).Checked = false;


            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[0].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[1].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[2].Checked = false;


            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[0].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[1].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[2].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[3].Checked = false;

            ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[4].Checked = false;

            ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph2.Items[6]).Checked = false;

            #endregion


            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl != null) {

                StyleToolbarControlSelection.Items[0].Enabled = true;

                StyleToolbarControlSelection.Items[0].Text = selectedControl.ControlType.ToString ().Replace ("SectionColumn", "Column");

                StyleToolbarControlSelection.Items[0].ImageUrl = "/Images/Common32/" + selectedControl.ControlType.ToString () + ".png";

                StyleToolbarControlSelection.Items[2].Enabled = selectedControl.Capabilities.HasLabel;

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked = (((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked && selectedControl.Capabilities.HasLabel);

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[0]).Checked = !((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked;

                Mercury.Server.Application.FormControlStyle styleProperties = selectedControl.Style;

                if ((selectedControl.Capabilities.HasLabel) && (((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked)) {

                    styleProperties = selectedControl.Label.Style;

                    stylePropertiesForControl = false;

                }


                StyleToolbarFont1FontSelection.SelectedValue = styleProperties.FontFamily;

                StyleToolbarFont1SizeSelection.SelectedValue = styleProperties.FontSize;


                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[0]).Checked = !(String.IsNullOrEmpty (styleProperties.FontWeight));

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[1]).Checked = !(String.IsNullOrEmpty (styleProperties.FontStyle));

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[2]).Checked = ((!String.IsNullOrEmpty (styleProperties.TextDecoration)) ? styleProperties.TextDecoration.Contains ("underline") : false);

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[3]).Checked = ((!String.IsNullOrEmpty (styleProperties.TextDecoration)) ? styleProperties.TextDecoration.Contains ("line-through") : false);

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarFont2.Items[4]).Checked = ((!String.IsNullOrEmpty (styleProperties.FontVariant)) ? styleProperties.FontVariant.Contains ("small-caps") : false);


                HtmlGenericControl FontColorSample = (HtmlGenericControl)StyleToolbarFont2.Items[6].FindControl ("FontColorSample");

                if (FontColorSample != null) {

                    FontColorSample.Style.Add ("background-color", styleProperties.Color);

                }



                if (String.IsNullOrEmpty (styleProperties.TextAlign)) { styleProperties.TextAlign = String.Empty; }

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[0]).Checked = (styleProperties.TextAlign == "left");

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[1]).Checked = (styleProperties.TextAlign == "center");

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[2]).Checked = (styleProperties.TextAlign == "right");

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph1.Items[3]).Checked = (styleProperties.TextAlign == "justify");


                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[0].Checked = (styleProperties.VerticalAlign == "top");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[1].Checked = (styleProperties.VerticalAlign == "middle");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).Buttons[2].Checked = (styleProperties.VerticalAlign == "bottom");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[5]).ImageUrl = "/Images/Common20/VerticalAlign" + ((String.IsNullOrEmpty (styleProperties.VerticalAlign)) ? "middle" : styleProperties.VerticalAlign) + ".png";


                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[0].Checked = (styleProperties.LineHeight == "100");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[0].ImageUrl = (styleProperties.LineHeight == "100") ? "/Images/Common16/Check.png" : String.Empty;

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[1].Checked = (styleProperties.LineHeight == "150");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[1].ImageUrl = (styleProperties.LineHeight == "150") ? "/Images/Common16/Check.png" : String.Empty;

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[2].Checked = (styleProperties.LineHeight == "200");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[2].ImageUrl = (styleProperties.LineHeight == "200") ? "/Images/Common16/Check.png" : String.Empty;

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[3].Checked = (styleProperties.LineHeight == "250");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[3].ImageUrl = (styleProperties.LineHeight == "250") ? "/Images/Common16/Check.png" : String.Empty;

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[4].Checked = (styleProperties.LineHeight == "300");

                ((Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph1.Items[6]).Buttons[4].ImageUrl = (styleProperties.LineHeight == "300") ? "/Images/Common16/Check.png" : String.Empty;


                Telerik.Web.UI.RadToolBarSplitButton borderSplitButton = (Telerik.Web.UI.RadToolBarSplitButton)StyleToolbarParagraph2.FindItemByValue ("ParagraphBorder");

                if ((styleProperties.IsBorderSame) && (!String.IsNullOrEmpty (styleProperties.BorderTopStyle))) {

                    borderSplitButton.ImageUrl = "/Images/Common20/ParagraphBorderOutside.png";

                }

                else {

                    borderSplitButton.ImageUrl = "/Images/Common20/ParagraphBorderNone.png";

                    borderSplitButton.ImageUrl = (!String.IsNullOrEmpty (styleProperties.BorderBottomStyle)) ? "/Images/Common20/ParagraphBorderBottom.png" : borderSplitButton.ImageUrl;

                    borderSplitButton.ImageUrl = (!String.IsNullOrEmpty (styleProperties.BorderLeftStyle)) ? "/Images/Common20/ParagraphBorderLeft.png" : borderSplitButton.ImageUrl;

                    borderSplitButton.ImageUrl = (!String.IsNullOrEmpty (styleProperties.BorderRightStyle)) ? "/Images/Common20/ParagraphBorderRight.png" : borderSplitButton.ImageUrl;

                    borderSplitButton.ImageUrl = (!String.IsNullOrEmpty (styleProperties.BorderTopStyle)) ? "/Images/Common20/ParagraphBorderTop.png" : borderSplitButton.ImageUrl;

                }

                ((Telerik.Web.UI.RadToolBarButton)StyleToolbarParagraph2.Items[6]).Checked = (styleProperties.WhiteSpace == "nowrap");


                Telerik.Web.UI.RadNumericTextBox StyleToolbarBox1Width = (Telerik.Web.UI.RadNumericTextBox)StyleToolbarBox1.Items[0].FindControl ("StyleToolbarBox1Width");

                StyleToolbarBox1Width.Value = (String.IsNullOrEmpty (styleProperties.Width)) ? null : (Double?)Convert.ToDouble (styleProperties.Width);

                Telerik.Web.UI.RadComboBox StyleToolbarBox1WidthUnit = (Telerik.Web.UI.RadComboBox)StyleToolbarBox1.Items[1].FindControl ("StyleToolbarBox1WidthUnit");

                StyleToolbarBox1WidthUnit.SelectedValue = styleProperties.WidthUnit;


                Telerik.Web.UI.RadNumericTextBox StyleToolbarBox1Height = (Telerik.Web.UI.RadNumericTextBox)StyleToolbarBox1.Items[2].FindControl ("StyleToolbarBox1Height");

                StyleToolbarBox1Height.Value = (String.IsNullOrEmpty (styleProperties.Height)) ? null : (Double?)Convert.ToDouble (styleProperties.Height);

                Telerik.Web.UI.RadComboBox StyleToolbarBox1HeightUnit = (Telerik.Web.UI.RadComboBox)StyleToolbarBox1.Items[3].FindControl ("StyleToolbarBox1HeightUnit");

                StyleToolbarBox1HeightUnit.SelectedValue = styleProperties.HeightUnit;


                Telerik.Web.UI.RadTextBox StyleToolbarBox2Margin = (Telerik.Web.UI.RadTextBox)StyleToolbarBox2.Items[0].FindControl ("StyleToolbarBox2Margin");

                StyleToolbarBox2Margin.Text = styleProperties.Margin;

                StyleToolbarBox2Margin.Enabled = stylePropertiesForControl && (selectedControl.ControlType != Mercury.Server.Application.FormControlType.SectionColumn);

                Telerik.Web.UI.RadTextBox StyleToolbarBox2Padding = (Telerik.Web.UI.RadTextBox)StyleToolbarBox2.Items[1].FindControl ("StyleToolbarBox2Padding");

                StyleToolbarBox2Padding.Text = styleProperties.Padding;

            }

            return;

        }

        protected void InitializeProperties_AddItem (System.Collections.SortedList sortedControls, Mercury.Client.Core.Forms.Control control) {

            if (control == null) { return; }

            sortedControls.Add (control.Name + " [" + control.ControlType.ToString () + "]" + "|" + control.ControlId.ToString (), control.Name + " [" + control.ControlType.ToString () + "]");

            foreach (Mercury.Client.Core.Forms.Control currentChild in control.Controls) {

                InitializeProperties_AddItem (sortedControls, currentChild);

            }

            return;

        }

        protected void InitializeProperties () {

            DateTime startTime = DateTime.Now;

            Mercury.Client.Core.Forms.Control selectedControl;

            System.Collections.SortedList sortedControls = new System.Collections.SortedList ();

            String itemText;

            String itemValue;

            Client.Core.Forms.Form designerForm = DesignerForm;

            if ((designerForm == null) || (MercuryApplication == null)) { return; }

            if (string.IsNullOrEmpty (PropertiesSelectedControlId.Text)) {

                PropertiesSelectedControlId.Text = designerForm.ControlId.ToString ();

            }


            InitializeHomeToolbars ();

            InitializeStyleToolbars ();


            #region Control Selection Drop Down

            PropertiesControlSelection.Items.Clear ();

            InitializeProperties_AddItem (sortedControls, designerForm);

            foreach (System.Collections.DictionaryEntry currentEntry in sortedControls) {

                itemValue = currentEntry.Key.ToString ().Split ('|')[1];

                itemText = currentEntry.Value.ToString (); // +" | " + itemValue;

                PropertiesControlSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (itemText, itemValue));

            }

            PropertiesControlSelection.SelectedValue = PropertiesSelectedControlId.Text;

            #endregion


            #region Reset Properties

            PropertiesRow_ControlEvent.Visible = false;

            PropertiesRow_ControlEventHandler.Visible = false;


            PropertiesRow_ControlBindableProperty.Visible = false;

            PropertiesRow_ControlDataBindingControlId.Visible = false;

            PropertiesRow_ControlDataBindingContext.Visible = false;


            PropertiesRow_ControlReadOnly.Visible = false;

            PropertiesRow_ControlRequired.Visible = false;


            PropertiesRow_FormEntityType.Visible = false;

            PropertiesRow_FormAllowPrecompileEvents.Visible = false;

            PropertiesRow_SectionPageBreakAfter.Visible = false;


            PropertiesRow_TextValue.Visible = false;

            PropertiesRow_InputType.Visible = false;

            PropertiesRow_InputTextMode.Visible = false;

            PropertiesRow_InputMaxLength.Visible = false;

            PropertiesRow_InputColumns.Visible = false;

            PropertiesRow_InputRows.Visible = false;

            PropertiesRow_InputWrap.Visible = false;

            PropertiesRow_InputMask.Visible = false;

            PropertiesRow_InputEmptyMessage.Visible = false;

            PropertiesRow_InputValidation.Visible = false;

            PropertiesRow_InputNumericType.Visible = false;

            PropertiesRow_InputMinValue.Visible = false;

            PropertiesRow_InputMaxValue.Visible = false;

            PropertiesRow_InputShowSpinButtons.Visible = false;

            PropertiesRow_InputButtonPosition.Visible = false;

            PropertiesRow_InputDateFormat.Visible = false;

            PropertiesRow_InputDisplayDateFormat.Visible = false;

            PropertiesRow_InputSelectionOnFocus.Visible = false;

            PropertiesRow_InputText.Visible = false;


            PropertiesRow_SelectionType.Visible = false;

            PropertiesRow_SelectionDataSource.Visible = false;

            PropertiesRow_SelectionReferenceSource.Visible = false;

            PropertiesRow_SelectionReferenceDefault.Visible = false;

            PropertiesRow_SelectionItems.Visible = false;

            PropertiesRow_SelectionAllowCustomText.Visible = false;

            PropertiesRow_SelectionMode.Visible = false;

            PropertiesRow_SelectionColumns.Visible = false;

            PropertiesRow_SelectionRows.Visible = false;

            PropertiesRow_SelectionDirection.Visible = false;

            SelectionItemsGrid.DataSource = SelectionItemsGridTable;

            SelectionItemsGrid.Rebind ();


            PropertiesRow_ButtonText.Visible = false;


            PropertiesRow_EntityType.Visible = false;

            PropertiesRow_EntityAllowCustomEntityName.Visible = false;

            PropertiesRow_EntityDisplayAgeFormat.Visible = false;

            PropertiesRow_EntityDisplayStyle.Visible = false;


            PropertiesRow_Service.Visible = false;

            PropertiesRow_ServiceDateVisible.Visible = false;

            PropertiesRow_ServiceMostRecentDateVisible.Visible = false;

            PropertiesRow_Metric.Visible = false;


            PropertiesRow_LabelText.Visible = false;

            PropertiesRow_LabelVisible.Visible = false;

            PropertiesRow_LabelPosition.Visible = false;

            #endregion


            #region Set Specific Control Settings

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl != null) {

                Session[SessionCachePrefix + "FormControl." + selectedControl.ControlId.ToString ()] = selectedControl.Style;


                if (selectedControl.Events.Count > 0) {

                    #region Has Events

                    PropertiesRow_ControlEvent.Visible = true;

                    PropertiesRow_ControlEventHandler.Visible = true;

                    PropertiesControlEvent.Items.Clear ();

                    PropertiesControlEvent.Text = String.Empty;

                    foreach (String eventName in selectedControl.Events) {

                        PropertiesControlEvent.Items.Add (new Telerik.Web.UI.RadComboBoxItem (eventName, eventName));

                    }

                    PropertiesControlEvent.SelectedValue = String.Empty;

                    if (PropertiesControlEvent.Items.Count > 0) {

                        PropertiesControlEvent.SelectedValue = PropertiesControlEvent.Items[0].Value;

                    }

                    Session[SessionCachePrefix + "SelectedEvent"] = PropertiesControlEvent.SelectedValue;


                    Mercury.Server.Application.FormControlEventHandler eventHandler = selectedControl.GetEventHandler (PropertiesControlEvent.SelectedValue);

                    if (eventHandler != null) {

                        if (eventHandler.MethodSource.Length > 60) {

                            PropertiesControlEventHandler.Text = eventHandler.MethodSource.Substring (1, 60);

                        }

                        else {

                            PropertiesControlEventHandler.Text = eventHandler.MethodSource;

                        }

                    }

                    else {

                        PropertiesControlEventHandler.Text = String.Empty;

                    }

                    #endregion

                }

                if (selectedControl.DataBindableProperties.Count > 0) {

                    #region CanDataBind Properties


                    PropertiesRow_ControlBindableProperty.Visible = true;

                    PropertiesRow_ControlDataBindingControlId.Visible = true;

                    PropertiesRow_ControlDataBindingContext.Visible = true;


                    #region Bindable Property

                    String selectedBindableProperty = (String)Session[SessionCachePrefix + "SelectedBindableProperty"];

                    if (String.IsNullOrEmpty (selectedBindableProperty)) { selectedBindableProperty = String.Empty; }

                    PropertiesControlBindableProperty.Items.Clear ();

                    PropertiesControlBindableProperty.Text = String.Empty;


                    Dictionary<String, String> dataBindableProperties = selectedControl.DataBindableProperties;

                    foreach (String bindableProperty in dataBindableProperties.Keys) {

                        PropertiesControlBindableProperty.Items.Add (new Telerik.Web.UI.RadComboBoxItem (bindableProperty, bindableProperty));

                    }

                    PropertiesControlBindableProperty.SelectedValue = String.Empty;

                    if (PropertiesControlBindableProperty.Items.Count > 0) {

                        PropertiesControlBindableProperty.SelectedValue = PropertiesControlBindableProperty.Items[0].Value;

                    }

                    if (PropertiesControlBindableProperty.Items.FindItemByValue (selectedBindableProperty) != null) {

                        PropertiesControlBindableProperty.SelectedValue = selectedBindableProperty;

                    }

                    selectedBindableProperty = PropertiesControlBindableProperty.SelectedValue;

                    Session[SessionCachePrefix + "SelectedBindableProperty"] = selectedBindableProperty;

                    #endregion


                    #region Data Source Control Id

                    PropertiesControlDataBindingControlId.Items.Clear ();

                    PropertiesControlDataBindingControlId.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("None", Guid.Empty.ToString ()));

                    /*
                    if (selectedBindableProperty != "Collection") {

                        PropertiesControlDataBindingControlId.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Fixed Value", "Fixed Value"));

                        PropertiesControlDataBindingControlId.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Function", "Function"));

                    }
                    */

                    foreach (Mercury.Client.Core.Forms.Control dataSourceControl in designerForm.GetDataSources ()) {

                        if (dataSourceControl.ControlId != selectedControl.ControlId) {

                            PropertiesControlDataBindingControlId.Items.Add (new Telerik.Web.UI.RadComboBoxItem (dataSourceControl.Name, dataSourceControl.ControlId.ToString ()));

                        }

                    }

                    PropertiesControlDataBindingControlId.SelectedValue = Guid.Empty.ToString ();

                    #endregion


                    #region Data Binding Data Source Control Id and Context

                    PropertiesControlDataBindingContext.Items.Clear ();

                    PropertiesControlDataBindingContext.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("Not Specified", String.Empty));

                    PropertiesControlDataBindingContext.Text = String.Empty;


                    Mercury.Server.Application.FormControlDataBinding dataBinding = selectedControl.GetDataBinding (selectedBindableProperty);

                    if (dataBinding != null) {

                        switch (dataBinding.DataBindingType) {

                            case Mercury.Server.Application.FormControlDataBindingType.Control:

                                PropertiesControlDataBindingControlId.SelectedValue = dataBinding.DataSourceControlId.ToString ();

                                break;

                            case Mercury.Server.Application.FormControlDataBindingType.FixedValue:

                                PropertiesControlDataBindingControlId.SelectedValue = "Fixed Value";

                                break;

                            case Mercury.Server.Application.FormControlDataBindingType.Function:

                                PropertiesControlDataBindingControlId.SelectedValue = "Function";

                                break;

                        }

                        if (designerForm.FindControlById (dataBinding.DataSourceControlId) != null) {

                            System.Collections.Generic.Dictionary<String, String> dataBindingContexts = designerForm.FindControlById (dataBinding.DataSourceControlId).DataBindingContexts;

                            foreach (String bindingContext in dataBindingContexts.Keys) {

                                String bindingDataType = dataBindingContexts[bindingContext];

                                Boolean bindingSupported = selectedControl.DataBindingAllowed (selectedBindableProperty, bindingDataType);

                                if (bindingSupported) {

                                    PropertiesControlDataBindingContext.Items.Add (new Telerik.Web.UI.RadComboBoxItem (bindingContext, bindingContext));

                                }

                            }

                        }

                        switch (dataBinding.DataBindingType) {

                            case Mercury.Server.Application.FormControlDataBindingType.Control:

                                PropertiesControlDataBindingContext.SelectedValue = dataBinding.BindingContext;

                                PropertiesControlDataBindingContext.Text = dataBinding.BindingContext;

                                break;

                            case Mercury.Server.Application.FormControlDataBindingType.FixedValue:

                            case Mercury.Server.Application.FormControlDataBindingType.Function:

                                PropertiesControlDataBindingContext.SelectedValue = dataBinding.BindingContext;

                                PropertiesControlDataBindingContext.Text = dataBinding.BindingContext;

                                break;

                        }

                    }

                    #endregion


                    #endregion

                }

                if (selectedControl.Capabilities.HasLabel) {

                    #region HasLabel Properties

                    PropertiesRow_LabelText.Visible = true;

                    PropertiesRow_LabelVisible.Visible = true;

                    PropertiesRow_LabelPosition.Visible = true;

                    #endregion

                }

                #region Common Properties

                PropertiesControlId.Text = selectedControl.ControlId.ToString ();

                PropertiesControlName.Text = selectedControl.Name;

                PropertiesControlTitle.Text = selectedControl.Description;

                PropertiesControlTabIndex.Text = String.Empty;

                if (selectedControl.TabIndex != 0) { PropertiesControlTabIndex.Text = selectedControl.TabIndex.ToString (); }

                if ((selectedControl.Enabled) && (selectedControl.Visible)) { PropertiesControlDisplay.SelectedValue = "0"; }

                else if ((!selectedControl.Enabled) && (selectedControl.Visible)) { PropertiesControlDisplay.SelectedValue = "1"; }

                else { PropertiesControlDisplay.SelectedValue = "2"; }


                switch (selectedControl.ControlType) {

                    case Mercury.Server.Application.FormControlType.Input:

                    case Mercury.Server.Application.FormControlType.Selection:

                    case Mercury.Server.Application.FormControlType.Entity:

                    case Mercury.Server.Application.FormControlType.Address:

                        PropertiesRow_ControlReadOnly.Visible = true;

                        PropertiesControlReadOnly.SelectedValue = selectedControl.ReadOnly.ToString ();

                        PropertiesRow_ControlRequired.Visible = true;

                        PropertiesControlRequired.SelectedValue = selectedControl.Required.ToString ();

                        break;

                    case Mercury.Server.Application.FormControlType.Collection:

                        PropertiesRow_ControlReadOnly.Visible = true;

                        PropertiesControlReadOnly.SelectedValue = selectedControl.ReadOnly.ToString ();

                        break;

                }


                #endregion

                switch (selectedControl.ControlType) {

                    case Mercury.Server.Application.FormControlType.Form:

                        PropertiesRow_FormEntityType.Visible = true;

                        PropertiesRow_FormAllowPrecompileEvents.Visible = true;

                        PropertiesFormEntityType.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Form)selectedControl).EntityType).ToString ();

                        PropertiesFormAllowPrecompileEvents.SelectedValue = ((Mercury.Client.Core.Forms.Form)selectedControl).AllowPrecompileEvents.ToString ();

                        break;

                    case Mercury.Server.Application.FormControlType.Section:

                        // ALLOW PAGE BREAK OPTION ON ROOT SECTIONS ONLY

                        if (selectedControl.Parent.ControlType == Mercury.Server.Application.FormControlType.Form) {

                            PropertiesRow_SectionPageBreakAfter.Visible = true;

                            PropertiesSectionPageBreakAfter.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Section)selectedControl).PageBreakAfterSection.ToString ();

                        }

                        break;

                    case Mercury.Server.Application.FormControlType.Text:

                        PropertiesRow_TextValue.Visible = true;

                        HtmlEditor.Content = ((Mercury.Client.Core.Forms.Controls.Text)selectedControl).TextContent;

                        break;

                    case Mercury.Server.Application.FormControlType.Input:

                        #region Input Control Properties

                        Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Style;


                        PropertiesRow_InputType.Visible = true;

                        PropertiesInputType.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).InputType).ToString ();

                        PropertiesRow_InputSelectionOnFocus.Visible = true;

                        PropertiesInputSelectionOnFocus.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).SelectionOnFocus).ToString ();

                        PropertiesRow_InputText.Visible = true;

                        PropertiesInputText.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Text;

                        PropertiesRow_LabelText.Visible = true;

                        PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Text;

                        PropertiesRow_LabelVisible.Visible = true;

                        PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Visible.ToString ();

                        PropertiesRow_LabelPosition.Visible = true;

                        PropertiesLabelPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Position).ToString ();


                        switch (((Mercury.Client.Core.Forms.Controls.Input)selectedControl).InputType) {

                            case Mercury.Server.Application.FormControlInputType.Text:

                                PropertiesRow_InputTextMode.Visible = true;

                                PropertiesInputTextMode.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).TextMode).ToString ();

                                PropertiesRow_InputMaxLength.Visible = true;

                                PropertiesInputMaxLength.Value = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MaxLength;

                                PropertiesRow_InputColumns.Visible = false; // DISABLED COLUMNS

                                PropertiesInputColumns.Value = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Columns;

                                PropertiesRow_InputRows.Visible = true;

                                PropertiesInputRows.Value = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Rows;

                                PropertiesRow_InputWrap.Visible = true;

                                PropertiesInputWrap.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Wrap.ToString ();

                                PropertiesRow_InputMask.Visible = true;

                                PropertiesInputMask.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Mask;

                                PropertiesRow_InputEmptyMessage.Visible = true;

                                PropertiesInputEmptyMessage.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).EmptyMessage;

                                PropertiesRow_InputValidation.Visible = true;

                                PropertiesInputValidation.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Validation;

                                break;

                            case Mercury.Server.Application.FormControlInputType.Numeric:

                                PropertiesRow_InputNumericType.Visible = true;

                                PropertiesInputNumericType.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).NumericType).ToString ();

                                PropertiesRow_InputMinValue.Visible = true;

                                //                                PropertiesInputMinValue.MinValue = Double.MinValue;

                                //                                PropertiesInputMinValue.MaxValue = Double.MaxValue;

                                PropertiesInputMinValue.Value = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MinValue;

                                PropertiesRow_InputMaxValue.Visible = true;

                                //                                PropertiesInputMaxValue.MinValue = Double.MinValue;

                                //                                PropertiesInputMaxValue.MaxValue = Double.MaxValue;

                                PropertiesInputMaxValue.Value = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MaxValue;

                                PropertiesRow_InputShowSpinButtons.Visible = true;

                                PropertiesInputShowSpinButtons.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).ShowSpinButtons.ToString ();

                                PropertiesRow_InputButtonPosition.Visible = true;

                                PropertiesInputButtonPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Input)selectedControl).ButtonPosition).ToString ();

                                PropertiesRow_InputEmptyMessage.Visible = true;

                                PropertiesInputEmptyMessage.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).EmptyMessage;

                                return;

                            case Mercury.Server.Application.FormControlInputType.DateTime:

                                PropertiesRow_InputDateFormat.Visible = true;

                                PropertiesInputDateFormat.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).DateFormat;

                                PropertiesRow_InputDisplayDateFormat.Visible = true;

                                PropertiesInputDisplayDateFormat.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).DisplayDateFormat;

                                PropertiesRow_InputEmptyMessage.Visible = true;

                                PropertiesInputEmptyMessage.Text = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).EmptyMessage;

                                break;

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Selection:

                        #region Selection Control Properties

                        Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Style;

                        PropertiesRow_SelectionType.Visible = true;

                        PropertiesSelectionType.SelectedIndex = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectionType);


                        PropertiesRow_SelectionItems.Visible = true;


                        PropertiesSelectionRows.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Rows.ToString ();

                        PropertiesSelectionColumns.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Columns.ToString ();

                        PropertiesSelectionDirection.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Direction).ToString ();


                        switch (((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectionType) {

                            case Mercury.Server.Application.FormControlSelectionType.DropDownList:

                                PropertiesSelectionDataSource.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).DataSource).ToString ();

                                PropertiesRow_SelectionDataSource.Visible = true;

                                if (((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).DataSource == Mercury.Server.Application.FormControlDataSource.Reference) {

                                    PropertiesSelectionReferenceSource.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).ReferenceSource).ToString ();

                                    PropertiesRow_SelectionReferenceSource.Visible = true;

                                    if (((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).HasValue) {

                                        PropertiesSelectionReferenceDefault.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectedItem.Value;

                                        PropertiesSelectionReferenceDefault.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectedItem.Text;

                                    }

                                    else {

                                        PropertiesSelectionReferenceDefault.SelectedValue = String.Empty;

                                        PropertiesSelectionReferenceDefault.Text = String.Empty;

                                    }

                                    PropertiesRow_SelectionReferenceDefault.Visible = true;

                                    PropertiesRow_SelectionItems.Visible = false;

                                    PropertiesRow_SelectionAllowCustomText.Visible = false;

                                }

                                else {

                                    PropertiesRow_SelectionAllowCustomText.Visible = true;

                                    PropertiesSelectionAllowCustomText.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).AllowCustomText.ToString ();

                                }

                                break;

                            case Mercury.Server.Application.FormControlSelectionType.ListBox:

                                PropertiesRow_SelectionRows.Visible = true;

                                PropertiesSelectionRows.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Rows.ToString ();

                                PropertiesRow_SelectionMode.Visible = true;

                                PropertiesSelectionMode.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectionMode).ToString ();

                                break;

                            case Mercury.Server.Application.FormControlSelectionType.CheckBox:

                                PropertiesRow_SelectionColumns.Visible = true;

                                PropertiesSelectionColumns.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Columns.ToString ();

                                PropertiesRow_SelectionDirection.Visible = true;

                                PropertiesSelectionDirection.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Direction).ToString ();

                                break;

                            case Mercury.Server.Application.FormControlSelectionType.RadioButton:

                                PropertiesRow_SelectionColumns.Visible = true;

                                PropertiesRow_SelectionDirection.Visible = true;

                                break;

                        }


                        PropertiesRow_LabelText.Visible = true;

                        PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Text;

                        PropertiesRow_LabelVisible.Visible = true;

                        PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Visible.ToString ();

                        PropertiesRow_LabelPosition.Visible = true;

                        PropertiesLabelPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Position).ToString ();

                        SelectionItemsGrid_OnNeedDataSource (this, new Telerik.Web.UI.GridNeedDataSourceEventArgs (Telerik.Web.UI.GridRebindReason.ExplicitRebind));

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Button:

                        PropertiesRow_ButtonText.Visible = true;

                        PropertiesButtonText.Text = ((Mercury.Client.Core.Forms.Controls.Button)selectedControl).Text;

                        break;

                    case Mercury.Server.Application.FormControlType.Entity:

                        #region Entity Control Properties

                        PropertiesRow_EntityType.Visible = true;

                        PropertiesEntityType.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).EntityType).ToString ();


                        PropertiesRow_EntityDisplayStyle.Visible = true;

                        PropertiesEntityDisplayStyle.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayStyle).ToString ();


                        PropertiesRow_EntityAllowCustomEntityName.Visible = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayStyle == Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly;

                        PropertiesEntityAllowCustomEntityName.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).AllowCustomEntityName.ToString ();


                        PropertiesRow_EntityDisplayAgeFormat.Visible = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayStyle != Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly;

                        PropertiesEntityDisplayAgeFormat.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayAgeFormat).ToString ();


                        Boolean entityShowLabel = ((((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayStyle == Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly));

                        PropertiesRow_LabelText.Visible = entityShowLabel;

                        PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Text;

                        PropertiesRow_LabelVisible.Visible = entityShowLabel;

                        PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Visible.ToString ();

                        PropertiesRow_LabelPosition.Visible = entityShowLabel;

                        PropertiesLabelPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Position).ToString ();


                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Collection:

                        #region Collection Control Properties

                        //PropertiesLabelStyle.Text = ((Mercury.Client.Core.Forms.Controls.Collection) selectedControl).Label.StyleAttribute;

                        //Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Collection) selectedControl).Label.Style;

                        //PropertiesRow_LabelText.Visible = true;

                        //PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Collection) selectedControl).Label.Text;

                        //PropertiesRow_LabelVisible.Visible = true;

                        //PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Collection) selectedControl).Label.Visible.ToString ();

                        //PropertiesRow_LabelPosition.Visible = true;

                        //PropertiesLabelPosition.SelectedValue = ((Int32) ((Mercury.Client.Core.Forms.Controls.Collection) selectedControl).Label.Position).ToString ();

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Address:

                        #region Address Control Properties

                        //Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Address) selectedControl).Label.Style;

                        //PropertiesRow_LabelText.Visible = true;

                        //PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Address) selectedControl).Label.Text;

                        //PropertiesRow_LabelVisible.Visible = true;

                        //PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Address) selectedControl).Label.Visible.ToString ();

                        //PropertiesRow_LabelPosition.Visible = true;

                        //PropertiesLabelPosition.SelectedValue = ((Int32) ((Mercury.Client.Core.Forms.Controls.Address) selectedControl).Label.Position).ToString ();

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Service:

                        #region Service Control Properties

                        PropertiesRow_Service.Visible = true;

                        PropertiesRow_ServiceDateVisible.Visible = true;

                        PropertiesRow_ServiceMostRecentDateVisible.Visible = true;


                        if (PropertiesServiceSelection.Items.Count == 0) {

                            PropertiesServiceSelection.Items.Clear ();

                            PropertiesServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** You must select a Service.", "0"));

                            foreach (Server.Application.SearchResultMedicalServiceHeader currentService in MercuryApplication.MedicalServiceHeadersGet (true)) {

                                if (currentService.Enabled) {

                                    PropertiesServiceSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentService.Name, currentService.Id.ToString ()));

                                }

                            }

                        }

                        PropertiesServiceSelection.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).ServiceId.ToString ();

                        PropertiesServiceDateVisibleSelection.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).ServiceDateVisible.ToString ();

                        PropertiesServiceMostRecentDateVisibleSelection.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).MostRecentMemberServiceDateVisible.ToString ();


                        Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Style;

                        PropertiesRow_LabelText.Visible = true;

                        PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Text;

                        PropertiesRow_LabelVisible.Visible = true;

                        PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Visible.ToString ();

                        PropertiesRow_LabelPosition.Visible = true;

                        PropertiesLabelPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Position).ToString ();

                        #endregion

                        break;

                    case Mercury.Server.Application.FormControlType.Metric:

                        #region Metric Control Properties

                        PropertiesRow_Metric.Visible = true;

                        if (PropertiesMetricSelection.Items.Count == 0) {

                            PropertiesMetricSelection.Items.Clear ();

                            PropertiesMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** You must select a Metric.", "0"));

                            foreach (Client.Core.Metrics.Metric currentMetric in MercuryApplication.MetricsAvailable (false)) {

                                if (currentMetric.Enabled) {

                                    PropertiesMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMetric.Name, currentMetric.Id.ToString ()));

                                }

                            }

                        }

                        PropertiesMetricSelection.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).MetricId.ToString ();


                        Session[SessionCachePrefix + "ControlLabel." + selectedControl.ControlId.ToString ()] = ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Style;

                        PropertiesRow_LabelText.Visible = true;

                        PropertiesLabelText.Text = ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Text;

                        PropertiesRow_LabelVisible.Visible = true;

                        PropertiesLabelVisible.SelectedValue = ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Visible.ToString ();

                        PropertiesRow_LabelPosition.Visible = true;

                        PropertiesLabelPosition.SelectedValue = ((Int32)((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Position).ToString ();

                        #endregion

                        break;
                }

            }

            #endregion


            System.Diagnostics.Debug.WriteLine ("Initialize Properties: " + DateTime.Now.Subtract (startTime).TotalMilliseconds.ToString ());

            return;

        }

        protected void InitializeCompileOutput () {

            System.Data.DataTable compileOutputTable = new System.Data.DataTable ();

            compileOutputTable.Columns.Add ("MessageType");
            compileOutputTable.Columns.Add ("Description");
            compileOutputTable.Columns.Add ("ControlId");
            compileOutputTable.Columns.Add ("ControlType");
            compileOutputTable.Columns.Add ("ControlName");

            if (compileOutput.Collection != null) {

                foreach (Mercury.Server.Application.FormCompileMessage currentMessage in compileOutput.Collection) {

                    compileOutputTable.Rows.Add (currentMessage.MessageType.ToString (), currentMessage.Description, currentMessage.ControlId, currentMessage.ControlType.ToString (), currentMessage.ControlName);

                }

            }

            CompileMessagesGrid.DataSource = compileOutputTable;

            CompileMessagesGrid.Rebind ();

            return;

        }

        #endregion


        #region Toolbar Events

        protected void HomeToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            String postScript = String.Empty;

            Client.Core.Forms.Form designerForm = DesignerForm;

            switch (eventArgs.Item.Value) {

                case "New":

                    Response.Redirect ("/Application/Forms/FormDesigner/FormDesigner.aspx", true);

                    break;

                case "Open":

                    FormDesignerControlOpenDialog.Refresh ();

                    postScript = "OpenDialog_Show();";

                    break;

                case "Save":

                    designerForm.EntityObjectId = 0;

                    Mercury.Server.Application.ObjectSaveResponse response;

                    response = MercuryApplication.FormSave (designerForm);

                    if (!response.HasException) {

                        designerForm.FormId = response.Id;

                        postScript = "alert ('Form Saved Successfully.', 200, 100, 'Save');";

                    }

                    else {

                        postScript = "alert ('Unable to successfully save document. " + response.Exception.Message.Replace ("\r\n", " ").Replace ("'", "") + "', 300, 100, 'Save');";

                    }

                    break;

                case "DesignerModeForm":

                case "DesignerModeTree":

                    DesignerModeForm = !DesignerModeForm;

                    InitializeHomeToolbars ();

                    break;

                case "Compile":

                    compileOutput = MercuryApplication.FormCompile (designerForm);

                    if (compileOutput.HasException) {

                        postScript = "alert ('Unable to successfully compile document. " + compileOutput.Exception.Message.Replace ("\r\n", " ").Replace ("'", "") + "', 300, 100, 'Compile');";

                    }

                    else {

                        Boolean hasCompileErrors = false;

                        Boolean hasCompileWarnings = false;


                        foreach (Server.Application.FormCompileMessage currentMessage in compileOutput.Collection) {

                            if (currentMessage.MessageType == Mercury.Server.Application.FormCompileMessageType.Error) {

                                hasCompileErrors = true;

                                break;

                            }

                            else if (currentMessage.MessageType == Mercury.Server.Application.FormCompileMessageType.Warning) {

                                hasCompileWarnings = true;

                            }

                        }

                        String compileResults = "Successfully";

                        if (hasCompileErrors) { compileResults = "with Errors"; }

                        else if (hasCompileWarnings) { compileResults = "with Warnings"; }

                        postScript = "alert ('Form Compiled " + compileResults + ".', 200, 100, 'Compile');";

                    }

                    break;

                case "Preview":

                    String previewLink = "/Application/Forms/FormDesigner/Windows/FormPreview.aspx?PageInstanceId=" + PageInstanceId.Text;

                    postScript = "window.open (\"" + previewLink + "\", \"Preview\");";

                    break;

                case "Export":

                    if (designerForm.FormId != 0) {

                        String exportLink = "/Application/Configuration/Windows/ConfigurationExport.aspx?ConfigurationType=Form&ConfigurationId=" + designerForm.FormId.ToString ();

                        postScript = "window.open (\"" + exportLink + "\", \"Export\");";

                    }

                    else {

                        postScript = "alert ('Form must be saved before exporting.', 200, 100, 'Export');";

                    }

                    break;

            }

            DesignerForm = designerForm;

            if (!String.IsNullOrEmpty (postScript)) {

                TelerikAjaxManager.ResponseScripts.Add (postScript);

            }

            return;

        }

        protected void StyleToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventsArgs) {

            System.Diagnostics.Debug.WriteLine (eventsArgs.Item.Value);

            Mercury.Client.Core.Forms.Control selectedControl;

            Client.Core.Forms.Form designerForm = DesignerForm;

            if ((designerForm == null) || (MercuryApplication == null)) { return; }

            if (string.IsNullOrEmpty (PropertiesSelectedControlId.Text)) {

                PropertiesSelectedControlId.Text = designerForm.ControlId.ToString ();

            }

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl != null) {

                Mercury.Server.Application.FormControlStyle styleProperties = selectedControl.Style;

                #region Label Style Properties

                if ((selectedControl.Capabilities.HasLabel) && (((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked)) {

                    switch (selectedControl.ControlType) {

                        case Mercury.Server.Application.FormControlType.Address:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Address)selectedControl).Label.Style;

                            break;

                        case Mercury.Server.Application.FormControlType.Collection:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Collection)selectedControl).Label.Style;

                            break;

                        case Mercury.Server.Application.FormControlType.Entity:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Style;

                            break;

                        case Mercury.Server.Application.FormControlType.Input:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Style;

                            break;


                        case Mercury.Server.Application.FormControlType.Metric:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Style;

                            break;

                        case Mercury.Server.Application.FormControlType.Selection:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Style;

                            break;

                        case Mercury.Server.Application.FormControlType.Service:

                            styleProperties = ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Style;

                            break;

                        default: throw new ApplicationException ("Not Implemented");

                    }

                }

                #endregion

                switch (eventsArgs.Item.Value) {

                    #region StyleToolbarFont1

                    case "TextFont":

                        Telerik.Web.UI.RadComboBox StyleToolbarFont1FontSelection = (Telerik.Web.UI.RadComboBox)eventsArgs.Item.FindControl ("StyleToolbarFont1FontSelection");

                        if (StyleToolbarFont1FontSelection != null) {

                            styleProperties.FontFamily = StyleToolbarFont1FontSelection.SelectedValue;

                        }

                        break;

                    case "TextFontSize":

                        Telerik.Web.UI.RadComboBox StyleToolbarFont1SizeSelection = (Telerik.Web.UI.RadComboBox)eventsArgs.Item.FindControl ("StyleToolbarFont1SizeSelection");

                        if (StyleToolbarFont1SizeSelection != null) {

                            styleProperties.FontSize = StyleToolbarFont1SizeSelection.SelectedValue;

                            styleProperties.FontSizeUnit = "pt";

                        }

                        break;

                    #endregion

                    #region StyleToolbarFont2

                    case "TextBold": styleProperties.FontWeight = (String.IsNullOrEmpty (styleProperties.FontWeight)) ? "bold" : String.Empty; break;

                    case "TextItalic": styleProperties.FontStyle = (String.IsNullOrEmpty (styleProperties.FontStyle)) ? "italic" : String.Empty; break;

                    case "TextUnderline":

                        if (String.IsNullOrEmpty (styleProperties.TextDecoration)) { styleProperties.TextDecoration = String.Empty; }

                        styleProperties.TextDecoration = (styleProperties.TextDecoration.Contains ("underline")) ?

                            styleProperties.TextDecoration.Replace ("underline", "").Trim () :

                            (styleProperties.TextDecoration + " underline").Trim ();

                        break;

                    case "TextOverline":

                        if (String.IsNullOrEmpty (styleProperties.TextDecoration)) { styleProperties.TextDecoration = String.Empty; }

                        styleProperties.TextDecoration = (styleProperties.TextDecoration.Contains ("overline")) ?

                            styleProperties.TextDecoration.Replace ("overline", "").Trim () :

                            (styleProperties.TextDecoration + " overline").Trim ();

                        break;

                    case "TextStrikethrough":

                        if (String.IsNullOrEmpty (styleProperties.TextDecoration)) { styleProperties.TextDecoration = String.Empty; }

                        styleProperties.TextDecoration = (styleProperties.TextDecoration.Contains ("line-through")) ?

                            styleProperties.TextDecoration.Replace ("line-through", "").Trim () :

                            (styleProperties.TextDecoration + " line-through").Trim ();

                        break;

                    case "TextSmallCaps": styleProperties.FontVariant = (String.IsNullOrEmpty (styleProperties.FontVariant)) ? "small-caps" : String.Empty; break;

                    case "TextColor":

                        Telerik.Web.UI.RadColorPicker StyleFontColor = (Telerik.Web.UI.RadColorPicker)eventsArgs.Item.Controls[0].Controls[1];

                        if (StyleFontColor != null) {

                            styleProperties.Color = System.Drawing.ColorTranslator.ToHtml (StyleFontColor.SelectedColor);

                        }

                        break;

                    #endregion

                    #region StyleToolbarParagraph1

                    case "ParagraphAlignLeft": styleProperties.TextAlign = ((styleProperties.TextAlign == "left") ? String.Empty : "left"); break;

                    case "ParagraphAlignCenter": styleProperties.TextAlign = ((styleProperties.TextAlign == "center") ? String.Empty : "center"); break;

                    case "ParagraphAlignRight": styleProperties.TextAlign = ((styleProperties.TextAlign == "right") ? String.Empty : "right"); break;

                    case "ParagraphAlignJustify": styleProperties.TextAlign = ((styleProperties.TextAlign == "justify") ? String.Empty : "justify"); break;

                    case "VerticalAlignTop": styleProperties.VerticalAlign = ((styleProperties.VerticalAlign == "top") ? String.Empty : "top"); break;

                    case "VerticalAlignMiddle": styleProperties.VerticalAlign = ((styleProperties.VerticalAlign == "middle") ? String.Empty : "middle"); break;

                    case "VerticalAlignBottom": styleProperties.VerticalAlign = ((styleProperties.VerticalAlign == "bottom") ? String.Empty : "bottom"); break;

                    case "LineHeight100%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "100") ? String.Empty : "100");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    case "LineHeight150%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "150") ? String.Empty : "150");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    case "LineHeight200%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "200") ? String.Empty : "200");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    case "LineHeight250%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "250") ? String.Empty : "250");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    case "LineHeight300%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "300") ? String.Empty : "300");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    case "LineHeight350%":

                        styleProperties.LineHeight = ((styleProperties.LineHeight == "350") ? String.Empty : "350");

                        styleProperties.LineHeightUnit = "%";

                        break;

                    #endregion

                    #region StyleToolbarParagraph2 - Border

                    case "ParagraphBorderBottom":

                        styleProperties.IsBorderSame = false;

                        styleProperties.BorderBottomStyle = (!String.IsNullOrEmpty (styleProperties.BorderBottomStyle)) ? String.Empty : "Solid";

                        styleProperties.BorderBottomColor = "Black";

                        styleProperties.BorderBottomWidth = "1";

                        styleProperties.BorderBottomWidthUnit = "px";

                        styleProperties.BorderLeftStyle = String.Empty;

                        styleProperties.BorderRightStyle = String.Empty;

                        styleProperties.BorderTopStyle = String.Empty;

                        break;


                    case "ParagraphBorderLeft":

                        styleProperties.IsBorderSame = false;

                        styleProperties.BorderBottomStyle = String.Empty;

                        styleProperties.BorderLeftStyle = (!String.IsNullOrEmpty (styleProperties.BorderLeftStyle)) ? String.Empty : "Solid";

                        styleProperties.BorderLeftColor = "Black";

                        styleProperties.BorderLeftWidth = "1";

                        styleProperties.BorderLeftWidthUnit = "px";

                        styleProperties.BorderRightStyle = String.Empty;

                        styleProperties.BorderTopStyle = String.Empty;

                        break;


                    case "ParagraphBorderRight":

                        styleProperties.IsBorderSame = false;

                        styleProperties.BorderBottomStyle = String.Empty;

                        styleProperties.BorderLeftStyle = String.Empty;

                        styleProperties.BorderRightStyle = (!String.IsNullOrEmpty (styleProperties.BorderRightStyle)) ? String.Empty : "Solid";

                        styleProperties.BorderRightColor = "Black";

                        styleProperties.BorderRightWidth = "1";

                        styleProperties.BorderRightWidthUnit = "px";

                        styleProperties.BorderTopStyle = String.Empty;

                        break;


                    case "ParagraphBorderTop":

                        styleProperties.IsBorderSame = false;

                        styleProperties.BorderBottomStyle = String.Empty;

                        styleProperties.BorderLeftStyle = String.Empty;

                        styleProperties.BorderRightStyle = String.Empty;

                        styleProperties.BorderTopStyle = (!String.IsNullOrEmpty (styleProperties.BorderTopStyle)) ? String.Empty : "Solid";

                        styleProperties.BorderTopColor = "Black";

                        styleProperties.BorderTopWidth = "1";

                        styleProperties.BorderTopWidthUnit = "px";

                        break;

                    case "ParagraphBorderOutside":

                        styleProperties.IsBorderSame = true;

                        styleProperties.BorderBottomStyle = String.Empty;

                        styleProperties.BorderLeftStyle = String.Empty;

                        styleProperties.BorderRightStyle = String.Empty;

                        styleProperties.BorderTopStyle = (!String.IsNullOrEmpty (styleProperties.BorderTopStyle)) ? String.Empty : "Solid";

                        styleProperties.BorderTopColor = "Black";

                        styleProperties.BorderTopWidth = "1";

                        styleProperties.BorderTopWidthUnit = "px";

                        break;

                    case "ParagraphBorderNone":

                        styleProperties.IsBorderSame = false;

                        styleProperties.BorderBottomStyle = String.Empty;

                        styleProperties.BorderLeftStyle = String.Empty;

                        styleProperties.BorderRightStyle = String.Empty;

                        styleProperties.BorderTopStyle = String.Empty;

                        break;

                    case "ParagraphBorderSolid":

                    case "ParagraphBorderDotted":

                    case "ParagraphBorderDashed":

                        if ((styleProperties.IsBorderSame) || (!String.IsNullOrEmpty (styleProperties.BorderTopStyle))) {

                            styleProperties.BorderTopStyle = eventsArgs.Item.Value.Replace ("ParagraphBorder", "");

                        }

                        else {

                            if (!String.IsNullOrEmpty (styleProperties.BorderBottomStyle)) { styleProperties.BorderBottomStyle = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                            if (!String.IsNullOrEmpty (styleProperties.BorderLeftStyle)) { styleProperties.BorderLeftStyle = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                            if (!String.IsNullOrEmpty (styleProperties.BorderRightStyle)) { styleProperties.BorderRightStyle = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                        }

                        break;

                    case "ParagraphBorderSize":

                        Telerik.Web.UI.RadComboBox ParagraphBorderSizeSelection = (Telerik.Web.UI.RadComboBox)StyleToolbarParagraph2.Items[3].FindControl ("ParagraphBorderSizeSelection");

                        styleProperties.BorderBottomWidthUnit = "px";

                        styleProperties.BorderLeftWidthUnit = "px";

                        styleProperties.BorderRightWidthUnit = "px";

                        styleProperties.BorderTopWidthUnit = "px";

                        if (ParagraphBorderSizeSelection != null) {

                            if ((styleProperties.IsBorderSame) || (!String.IsNullOrEmpty (styleProperties.BorderTopWidth))) {

                                styleProperties.BorderTopWidth = ParagraphBorderSizeSelection.SelectedValue;
                            }

                            else {

                                if (!String.IsNullOrEmpty (styleProperties.BorderBottomWidth)) { styleProperties.BorderBottomWidth = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                                if (!String.IsNullOrEmpty (styleProperties.BorderLeftWidth)) { styleProperties.BorderLeftWidth = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                                if (!String.IsNullOrEmpty (styleProperties.BorderRightWidth)) { styleProperties.BorderRightWidth = eventsArgs.Item.Value.Replace ("ParagraphBorder", ""); }

                            }

                        }

                        break;

                    #endregion

                    #region StyleToolbarParagraph2

                    case "ParagraphNoWrap":

                        styleProperties.WhiteSpace = (String.IsNullOrEmpty (styleProperties.WhiteSpace)) ? "nowrap" : String.Empty;

                        break;

                    case "BackgroundColor":

                        Telerik.Web.UI.RadColorPicker StyleBackgroundColor = (Telerik.Web.UI.RadColorPicker)StyleToolbarParagraph2.Items[5].Controls[0].Controls[1];

                        if (StyleBackgroundColor != null) {

                            styleProperties.BackgroundColor = System.Drawing.ColorTranslator.ToHtml (StyleBackgroundColor.SelectedColor);

                        }

                        break;

                    #endregion

                    #region StyleToolbarBox1

                    case "BoxWidth":

                        Telerik.Web.UI.RadNumericTextBox StyleToolbarBox1Width = (Telerik.Web.UI.RadNumericTextBox)eventsArgs.Item.FindControl ("StyleToolbarBox1Width");

                        if (StyleToolbarBox1Width != null) {

                            if (!StyleToolbarBox1Width.Value.HasValue) {

                                styleProperties.Width = String.Empty;

                                styleProperties.WidthUnit = String.Empty;

                            }

                            else {

                                styleProperties.Width = StyleToolbarBox1Width.Value.Value.ToString ();

                                if (String.IsNullOrEmpty (styleProperties.WidthUnit)) {

                                    styleProperties.WidthUnit = (StyleToolbarBox1Width.Value.Value <= 100) ? "%" : "px";

                                }

                            }

                        }

                        break;

                    case "BoxWidthUnit":

                        Telerik.Web.UI.RadComboBox StyleToolbarBox1WidthUnit = (Telerik.Web.UI.RadComboBox)eventsArgs.Item.FindControl ("StyleToolbarBox1WidthUnit");

                        if (StyleToolbarBox1WidthUnit != null) {

                            styleProperties.WidthUnit = StyleToolbarBox1WidthUnit.SelectedValue;

                        }

                        break;

                    case "BoxHeight":

                        Telerik.Web.UI.RadNumericTextBox StyleToolbarBox1Height = (Telerik.Web.UI.RadNumericTextBox)eventsArgs.Item.FindControl ("StyleToolbarBox1Height");

                        if (StyleToolbarBox1Height != null) {

                            if (!StyleToolbarBox1Height.Value.HasValue) {

                                styleProperties.Height = String.Empty;

                                styleProperties.HeightUnit = String.Empty;

                            }

                            else {

                                styleProperties.Height = StyleToolbarBox1Height.Value.Value.ToString ();

                                if (String.IsNullOrEmpty (styleProperties.HeightUnit)) {

                                    styleProperties.HeightUnit = "px";

                                }

                            }

                        }

                        break;

                    case "BoxHeightUnit":

                        Telerik.Web.UI.RadComboBox StyleToolbarBox1HeightUnit = (Telerik.Web.UI.RadComboBox)eventsArgs.Item.FindControl ("StyleToolbarBox1HeightUnit");

                        if (StyleToolbarBox1HeightUnit != null) {

                            styleProperties.HeightUnit = StyleToolbarBox1HeightUnit.SelectedValue;

                        }

                        break;

                    #endregion

                    #region StyleToolbarBox2

                    case "BoxMargin":

                        Telerik.Web.UI.RadTextBox StyleToolbarBox2Margin = (Telerik.Web.UI.RadTextBox)eventsArgs.Item.FindControl ("StyleToolbarBox2Margin");

                        if (StyleToolbarBox2Margin != null) {

                            styleProperties.Margin = StyleToolbarBox2Margin.Text;

                        }

                        break;

                    case "BoxPadding":

                        Telerik.Web.UI.RadTextBox StyleToolbarBox2Padding = (Telerik.Web.UI.RadTextBox)eventsArgs.Item.FindControl ("StyleToolbarBox2Padding");

                        if (StyleToolbarBox2Padding != null) {

                            styleProperties.Padding = StyleToolbarBox2Padding.Text;

                        }

                        break;

                    #endregion

                    default: System.Diagnostics.Debug.WriteLine ("Unknown Style Toolbar Command: " + eventsArgs.Item.Value); break;

                }

                #region Label Style Properties

                if ((selectedControl.Capabilities.HasLabel) && (((Telerik.Web.UI.RadToolBarButton)StyleToolbarControlSelection.Items[2]).Checked)) {

                    switch (selectedControl.ControlType) {

                        case Mercury.Server.Application.FormControlType.Address:

                            ((Mercury.Client.Core.Forms.Controls.Address)selectedControl).Label.Style = styleProperties;

                            break;

                        case Mercury.Server.Application.FormControlType.Collection:

                            ((Mercury.Client.Core.Forms.Controls.Collection)selectedControl).Label.Style = styleProperties;

                            break;

                        case Mercury.Server.Application.FormControlType.Entity:

                            ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Style = styleProperties;

                            break;

                        case Mercury.Server.Application.FormControlType.Input:

                            ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Style = styleProperties;

                            break;


                        case Mercury.Server.Application.FormControlType.Metric:

                            ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Style = styleProperties;

                            break;

                        case Mercury.Server.Application.FormControlType.Selection:

                            ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Style = styleProperties;

                            break;

                        case Mercury.Server.Application.FormControlType.Service:

                            ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Style = styleProperties;

                            break;

                    }

                }

                else { selectedControl.Style = styleProperties; }

                #endregion

            }

            DesignerForm = designerForm;

            return;

        }

        protected void StyleToolbarFont1FontSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarFont1.Items[0]));

            return;

        }

        protected void StyleToolbarFont1SizeSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarFont1.Items[1]));

            return;

        }

        protected void StyleToolbarFont1FontColor_OnColorChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarFont2.Items[7]));

            return;

        }

        protected void ParagraphBorderSizeSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarParagraph2.Items[3]));

            return;

        }

        protected void StyleParagraph2BackgroundColor_OnColorChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarParagraph2.Items[5]));

            return;

        }

        protected void StyleToolbarBox1Width_OnTextChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox1.Items[0]));

            return;

        }

        protected void StyleToolbarBox1Height_OnTextChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox1.Items[2]));

            return;

        }

        protected void StyleToolbarBox1WidthUnit_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox1.Items[1]));

            return;

        }

        protected void StyleToolbarBox1HeightUnit_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox1.Items[3]));

            return;

        }

        protected void StyleToolbarBox2Margin_OnTextChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox2.Items[0]));

            return;

        }

        protected void StyleToolbarBox2Padding_OnTextChanged (Object sender, EventArgs e) {

            StyleToolbar_OnButtonClick (sender, new Telerik.Web.UI.RadToolBarEventArgs (StyleToolbarBox2.Items[1]));

            return;

        }

        #endregion


        #region Render Form

        public void RenderForm () {

            HtmlGenericControl dropZone;

            if (isFormRendered) { return; }

            if (DesignerForm == null) { return; }

            isFormRendered = true;

            Int32 dropZonePosition = 0;


            InteractiveFormDesigner.Controls.Clear ();

            FormExplorerTree.Nodes.Clear ();


            if (DesignerModeForm) {

                dropZone = renderEngine.RenderControl_DropZone (DesignerForm, dropZonePosition);

                dropZone.InnerText = "[Section Drop Zone]";

                InteractiveFormDesigner.Controls.Add (dropZone);


                // SECTIONS 

                foreach (Mercury.Client.Core.Forms.Controls.Section currentSection in DesignerForm.Controls) {

                    renderEngine.RenderFormControl (InteractiveFormDesigner, currentSection);


                    dropZonePosition = dropZonePosition + 1;

                    dropZone = renderEngine.RenderControl_DropZone (DesignerForm, dropZonePosition);

                    dropZone.InnerText = "[Section Drop Zone]";

                    InteractiveFormDesigner.Controls.Add (dropZone);

                }

            }

            else {

                InitializeFormExplorerTree ();

            }

            return;

        }

        #endregion


        #region Form Events

        protected void RequestServerRefresh_OnClick (Object sender, EventArgs eventArgs) {

            // HANDLED IN LOAD COMPLETE EVENT

            return;

        }

        protected void DropControl_OnClick (Object sender, EventArgs eventArgs) {

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (DropControl_OnClick): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;

            String sourceClassName = DropItemSource.Text.Split ('|')[0];

            String desinationClassName = DropItemDestination.Text.Split ('|')[0];

            String sourceControlId;

            String destinationControlId;

            Mercury.Client.Core.Forms.Control sourceControl;

            Mercury.Client.Core.Forms.Control destinationControl;

            Mercury.Client.Core.Forms.Control childControl = null;

            Int32 insertIndex = 0;

            Int32 sourceIndex = 0;


            switch (sourceClassName) {

                case "DesignerToolbarItem": // this occurs on a new control insert

                    #region Designer Toolbar Item

                    sourceControlId = DropItemSource.Text.Split ('_')[2];

                    destinationControlId = DropItemDestination.Text.Split ('_')[1];

                    destinationControl = designerForm.FindControlById (new Guid (destinationControlId));

                    if (destinationControl != null) {

                        insertIndex = Int32.Parse (DropItemDestination.Text.Split ('_')[2]);

                        switch (sourceControlId) {

                            case "Section": childControl = new Mercury.Client.Core.Forms.Controls.Section (MercuryApplication, destinationControl); break;

                            case "SectionColumn": childControl = new Mercury.Client.Core.Forms.Controls.SectionColumn (MercuryApplication); break;

                            case "Label": childControl = new Mercury.Client.Core.Forms.Controls.Label (MercuryApplication); break;

                            case "Text": childControl = new Mercury.Client.Core.Forms.Controls.Text (MercuryApplication); break;

                            case "Input": childControl = new Mercury.Client.Core.Forms.Controls.Input (MercuryApplication); break;

                            case "Selection": childControl = new Mercury.Client.Core.Forms.Controls.Selection (MercuryApplication); break;

                            case "Button": childControl = new Mercury.Client.Core.Forms.Controls.Button (MercuryApplication); break;

                            case "Entity": childControl = new Mercury.Client.Core.Forms.Controls.Entity (MercuryApplication); break;

                            case "Collection": childControl = new Mercury.Client.Core.Forms.Controls.Collection (MercuryApplication); break;

                            case "Address": childControl = new Mercury.Client.Core.Forms.Controls.Address (MercuryApplication); break;

                            case "Service": childControl = new Mercury.Client.Core.Forms.Controls.Service (MercuryApplication); break;

                            case "Metric": childControl = new Mercury.Client.Core.Forms.Controls.Metric (MercuryApplication); break;

                        }

                        if (childControl != null) {

                            if (destinationControl.AllowChildControl (childControl.ControlType)) {

                                destinationControl.InsertNewControl (insertIndex, childControl);

                            }

                        }

                    }

                    #endregion

                    break;


                case "FormControl": // this occurs during a node move

                    #region Form Control

                    sourceControlId = DropItemSource.Text.Split ('_')[2];

                    sourceControl = designerForm.FindControlById (new Guid (sourceControlId));

                    destinationControlId = DropItemDestination.Text.Split ('_')[1];

                    destinationControl = designerForm.FindControlById (new Guid (destinationControlId));

                    if ((sourceControl != null) && (destinationControl != null)) {

                        insertIndex = Int32.Parse (DropItemDestination.Text.Split ('_')[2]);

                        sourceIndex = sourceControl.Parent.ControlIndex (sourceControl.ControlId);


                        // validate move (not same position)
                        if ((destinationControl.ControlId == sourceControl.Parent.ControlId) && ((insertIndex == sourceIndex) || (insertIndex == sourceIndex + 1))) { break; }

                        // validate move (source control allowed as child of destination control)
                        if (!destinationControl.AllowChildControl (sourceControl.ControlType)) { break; }

                        // if destination and source parent are the same, and source is before insert position, 
                        // account for the removal of the source from the index position
                        if ((destinationControl.ControlId == sourceControl.Parent.ControlId) && (insertIndex > sourceIndex)) {

                            insertIndex = insertIndex - 1;

                        }

                        sourceControl.Parent.Controls.Remove (sourceControl);

                        destinationControl.InsertControl (insertIndex, sourceControl);

                    }

                    #endregion

                    break;

            }   // switch

            if (childControl != null) {

                PropertiesSelectedControlId.Text = childControl.ControlId.ToString ();

            }

            DesignerForm = designerForm;

            return;

        }

        protected void DeleteControl_OnClick (Object sender, EventArgs eventArgs) {

            System.Diagnostics.Debug.WriteLine ("FORM DESIGNER (DeleteControl_OnClick): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());


            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            String controlId = DeleteControlClientId.Text.Split ('_')[1];

            Mercury.Client.Core.Forms.Control DeleteControl = designerForm.FindControlById (new Guid (controlId));

            if (DeleteControl.ControlId.ToString () == PropertiesSelectedControlId.Text) {

                PropertiesSelectedControlId.Text = DeleteControl.Parent.ControlId.ToString ();

            }

            DeleteControl.Parent.Controls.Remove (DeleteControl);


            DesignerForm = designerForm;

            return;

        }

        protected void SelectControlProperties_OnClick (Object sender, EventArgs eventArgs) {

            return;

        }

        #endregion


        #region Form Explorer Tree Events

        private void FormExplorerTree_Refresh () {

            foreach (Client.Core.Forms.Control currentControl in DesignerForm.GetAllControls ()) {

                Telerik.Web.UI.RadTreeNode controlNode = FormExplorerTree.FindNodeByValue (currentControl.ControlId.ToString ());

                if (controlNode != null) {

                    String nodeText = currentControl.Name;

                    if ((currentControl.ReadOnly) || (!currentControl.Visible) || (currentControl.Required)) {

                        if (currentControl.Required) { nodeText = nodeText + " { Required }"; }

                        if (currentControl.ReadOnly) { nodeText = nodeText + " { Read Only }"; }

                        if (!currentControl.Visible) { nodeText = nodeText + " { Not Visible }"; }

                    }

                    controlNode.Text = nodeText;

                }

            }

            return;

        }

        protected void FormExplorerTree_OnNodeClick (Object sender, Telerik.Web.UI.RadTreeNodeEventArgs eventArgs) {

            PropertiesSelectedControlId.Text = eventArgs.Node.Value;

            FormExplorerTree_SelectNode = eventArgs.Node.Value;

            return;

        }

        protected void FormExplorerTree_OnNodeEdit (Object sender, Telerik.Web.UI.RadTreeNodeEditEventArgs eventArgs) {

            Client.Core.Forms.Form designerForm = DesignerForm;

            Client.Core.Forms.Control editedControl = DesignerForm.FindControlById (new Guid (eventArgs.Node.Value));

            if (editedControl != null) {

                editedControl.Name = eventArgs.Text;

            }

            PropertiesSelectedControlId.Text = eventArgs.Node.Value;

            return;

        }

        #endregion



        #region Properties - Data Change Events

        protected void PropertiesControlSelection_SelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            PropertiesSelectedControlId.Text = PropertiesControlSelection.SelectedValue;

            return;

        }

        protected void Properties_OnTextChange (Object sender, EventArgs eventArgs) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            String propertyName = String.Empty;

            String comboBoxValue = String.Empty;

            String propertyValue = String.Empty;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Server.Application.FormControlDataBinding dataBinding;



            #region Control Type, Initialize Property Value and Id

            if (sender is Telerik.Web.UI.RadComboBox) {

                Telerik.Web.UI.RadComboBox propertyComboBox = (Telerik.Web.UI.RadComboBox)sender;

                propertyValue = propertyComboBox.Text;

                comboBoxValue = propertyComboBox.SelectedValue;

                propertyName = propertyComboBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadTextBox) {

                Telerik.Web.UI.RadTextBox propertyTextBox = (Telerik.Web.UI.RadTextBox)sender;

                propertyValue = propertyTextBox.Text;

                propertyName = propertyTextBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadNumericTextBox) {

                Telerik.Web.UI.RadNumericTextBox propertyNumericTextBox = (Telerik.Web.UI.RadNumericTextBox)sender;

                propertyValue = propertyNumericTextBox.Value.ToString ();

                propertyName = propertyNumericTextBox.ID;

            }

            else { return; }

            #endregion


            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl != null) {

                switch (propertyName) {

                    case "PropertiesControlName": selectedControl.Name = propertyValue; break;

                    case "PropertiesControlTitle": selectedControl.Description = propertyValue; break;

                    case "PropertiesControlTabIndex":

                        Int16 tabIndex = -1;

                        Int16.TryParse (propertyValue, out tabIndex);

                        selectedControl.TabIndex = tabIndex;

                        break;

                    case "PropertiesControlDisplay":

                        #region PropertiesControlDisplay

                        if (comboBoxValue == "2") { selectedControl.Visible = false; }

                        if (comboBoxValue == "1") { selectedControl.Enabled = false; selectedControl.Visible = true; }

                        if (comboBoxValue == "0") { selectedControl.Enabled = true; selectedControl.Visible = true; }

                        #endregion

                        break;

                    case "PropertiesControlBindableProperty":

                        Session[SessionCachePrefix + "SelectedBindableProperty"] = PropertiesControlBindableProperty.Text;

                        break;

                    case "PropertiesControlDataBindingControlId":

                        #region PropertiesControlDataBindingControlId

                        dataBinding = selectedControl.GetDataBinding (PropertiesControlBindableProperty.SelectedValue);

                        if (dataBinding == null) {

                            dataBinding = new Mercury.Server.Application.FormControlDataBinding ();

                            dataBinding.DataBindingType = Mercury.Server.Application.FormControlDataBindingType.Control;

                            dataBinding.BoundProperty = PropertiesControlBindableProperty.SelectedValue;

                            dataBinding.DataSourceControlId = Guid.Empty;

                            dataBinding.BindingContext = String.Empty;

                            selectedControl.DataBindings.Add (dataBinding);

                        }

                        dataBinding = selectedControl.GetDataBinding (PropertiesControlBindableProperty.SelectedValue);

                        switch (comboBoxValue) {

                            case "Fixed Value":

                                dataBinding.DataBindingType = Mercury.Server.Application.FormControlDataBindingType.FixedValue;

                                break;

                            case "Function":

                                dataBinding.DataBindingType = Mercury.Server.Application.FormControlDataBindingType.Function;

                                break;

                            default:

                                dataBinding.DataBindingType = Mercury.Server.Application.FormControlDataBindingType.Control;

                                if (designerForm.FindControlById (new Guid (comboBoxValue)) != null) {

                                    dataBinding.DataSourceControlId = new Guid (comboBoxValue);

                                }

                                else { selectedControl.DataBindings.Remove (dataBinding); }

                                break;

                        }

                        dataBinding.BindingContext = String.Empty;

                        selectedControl.DataBindingsResetCache ();

                        #endregion

                        break;

                    case "PropertiesControlDataBindingContext":

                        #region PropertiesControlDataBindingContext

                        dataBinding = selectedControl.GetDataBinding (PropertiesControlBindableProperty.SelectedValue);

                        if (dataBinding != null) {

                            if (propertyValue != "Not Specified") {

                                dataBinding.BindingContext = propertyValue;

                            }

                            selectedControl.DataBindingsResetCache ();

                        }

                        #endregion

                        break;

                    case "PropertiesFormEntityType": ((Mercury.Client.Core.Forms.Form)selectedControl).EntityType = (Mercury.Server.Application.EntityType)Int32.Parse (comboBoxValue); break;

                    case "PropertiesFormAllowPrecompileEvents": ((Mercury.Client.Core.Forms.Form)selectedControl).AllowPrecompileEvents = Boolean.Parse (comboBoxValue); break;

                    case "PropertiesSectionPageBreakAfter": ((Mercury.Client.Core.Forms.Controls.Section)selectedControl).PageBreakAfterSection= Boolean.Parse (comboBoxValue); break;

                    case "PropertiesInputType":

                        ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).InputType = (Mercury.Server.Application.FormControlInputType)Int32.Parse (comboBoxValue);

                        ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).DataBindings.Clear ();

                        break;

                    case "PropertiesInputTextMode": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).TextMode = (Mercury.Server.Application.FormControlTextMode)Int32.Parse (comboBoxValue); break;

                    case "PropertiesInputMaxLength": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MaxLength = Int32.Parse (propertyValue); break;

                    case "PropertiesInputColumns": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Columns = Int32.Parse (propertyValue); break;

                    case "PropertiesInputRows": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Rows = Int32.Parse (propertyValue); break;

                    case "PropertiesInputWrap": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Wrap = Boolean.Parse (propertyValue); break;

                    case "PropertiesInputMask": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Mask = propertyValue; break;

                    case "PropertiesInputEmptyMessage": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).EmptyMessage = propertyValue; break;

                    case "PropertiesInputValidation": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Validation = propertyValue; break;

                    case "PropertiesControlReadOnly": selectedControl.ReadOnly = Boolean.Parse (propertyValue); break;

                    case "PropertiesControlRequired": selectedControl.Required = Boolean.Parse (propertyValue); break;

                    case "PropertiesInputNumericType": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).NumericType = (Mercury.Server.Application.FormControlNumericType)Int32.Parse (comboBoxValue); break;

                    case "PropertiesInputMinValue": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MinValue = Int32.Parse (propertyValue); break;

                    case "PropertiesInputMaxValue": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).MaxValue = Int32.Parse (propertyValue); break;

                    case "PropertiesInputShowSpinButtons": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).ShowSpinButtons = Boolean.Parse (propertyValue); break;

                    case "PropertiesInputButtonPosition": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).ButtonPosition = (Mercury.Server.Application.FormControlSpinnerButtonPosition)Int32.Parse (comboBoxValue); break;

                    case "PropertiesInputDateFormat": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).DateFormat = propertyValue; break;

                    case "PropertiesInputDisplayDateFormat": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).DisplayDateFormat = propertyValue; break;

                    case "PropertiesInputSelectionOnFocus": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).SelectionOnFocus = (Mercury.Server.Application.FormControlSelectionOnFocus)Int32.Parse (comboBoxValue); break;

                    case "PropertiesInputText": ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Text = propertyValue; break;

                    case "PropertiesSelectionType": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectionType = (Mercury.Server.Application.FormControlSelectionType)Int32.Parse (comboBoxValue); break;

                    case "PropertiesSelectionDataSource": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).DataSource = (Mercury.Server.Application.FormControlDataSource)Int32.Parse (comboBoxValue); break;

                    case "PropertiesSelectionReferenceSource":

                        ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).ReferenceSource = (Mercury.Server.Application.FormControlSelectionReferenceSource)Int32.Parse (comboBoxValue);

                        ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Items.Clear ();

                        break;

                    case "PropertiesSelectionReferenceDefault": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SetItemSelection (PropertiesSelectionReferenceDefault.SelectedValue, PropertiesSelectionReferenceDefault.Text, true); break;

                    case "PropertiesSelectionAllowCustomText": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).AllowCustomText = Convert.ToBoolean (comboBoxValue); break;

                    case "PropertiesSelectionMode": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).SelectionMode = (Mercury.Server.Application.FormControlSelectionMode)Int32.Parse (comboBoxValue); break;

                    case "PropertiesSelectionColumns": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Columns = Int32.Parse (propertyValue); break;

                    case "PropertiesSelectionRows": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Rows = Int32.Parse (propertyValue); break;

                    case "PropertiesSelectionDirection": ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Direction = (Mercury.Server.Application.FormControlSelectionDirection)Int32.Parse (comboBoxValue); break;


                    case "PropertiesButtonText": ((Mercury.Client.Core.Forms.Controls.Button)selectedControl).Text = propertyValue; break;


                    case "PropertiesEntityType": ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).EntityType = (Mercury.Server.Application.EntityType)Int32.Parse (comboBoxValue); break;

                    case "PropertiesEntityDisplayAgeFormat": ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayAgeFormat = (Mercury.Server.Application.FormControlEntityDisplayAgeFormat)Int32.Parse (comboBoxValue); break;

                    case "PropertiesEntityDisplayStyle": ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).DisplayStyle = (Mercury.Server.Application.FormControlEntityDisplayStyle)Int32.Parse (comboBoxValue); break;

                    case "PropertiesEntityAllowCustomEntityName": ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).AllowCustomEntityName = Convert.ToBoolean (comboBoxValue); break;


                    case "PropertiesServiceSelection": ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).ServiceId = Int64.Parse (comboBoxValue); break;

                    case "PropertiesServiceDateVisibleSelection": ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).ServiceDateVisible = Convert.ToBoolean (comboBoxValue); break;

                    case "PropertiesServiceMostRecentDateVisibleSelection": ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).MostRecentMemberServiceDateVisible = Convert.ToBoolean (comboBoxValue); break;


                    case "PropertiesMetricSelection": ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).MetricId = Int64.Parse (comboBoxValue); break;


                    case "PropertiesLabelText":

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Input) { ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) { ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Entity) { ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Collection) { ((Mercury.Client.Core.Forms.Controls.Collection)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) { ((Mercury.Client.Core.Forms.Controls.Address)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Service) { ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Text = propertyValue; }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Metric) { ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Text = propertyValue; }

                        break;

                    case "PropertiesLabelVisible":

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Input) { ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) { ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Entity) { ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Collection) { ((Mercury.Client.Core.Forms.Controls.Collection)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) { ((Mercury.Client.Core.Forms.Controls.Address)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Service) { ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Metric) { ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Visible = Boolean.Parse (propertyValue); }

                        break;

                    case "PropertiesLabelPosition":

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Input) { ((Mercury.Client.Core.Forms.Controls.Input)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) { ((Mercury.Client.Core.Forms.Controls.Selection)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Entity) { ((Mercury.Client.Core.Forms.Controls.Entity)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Collection) { ((Mercury.Client.Core.Forms.Controls.Collection)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) { ((Mercury.Client.Core.Forms.Controls.Address)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Service) { ((Mercury.Client.Core.Forms.Controls.Service)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        if (selectedControl is Mercury.Client.Core.Forms.Controls.Metric) { ((Mercury.Client.Core.Forms.Controls.Metric)selectedControl).Label.Position = (Mercury.Server.Application.FormControlPosition)Int32.Parse (comboBoxValue); }

                        break;


                }

            }

            DesignerForm = designerForm;

            return;

        }

        protected void HtmlEditorCommandButtonsOk_OnClick (Object sender, EventArgs eventArgs) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            Mercury.Client.Core.Forms.Control selectedControl;

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Text) {

                String content = HtmlEditor.Content;

                content = content.Replace (((Char)8211), ((Char)45));

                ((Mercury.Client.Core.Forms.Controls.Text)selectedControl).TextContent = content;

            }


            DesignerForm = designerForm;

            TelerikAjaxManager.ResponseScripts.Add ("HtmlEditor_Close (event);");

            return;

        }

        protected void HtmlEditorCommandButtonsCancel_OnClick (Object sender, EventArgs eventArgs) {

            TelerikAjaxManager.ResponseScripts.Add ("HtmlEditor_Close (event);");

            return;

        }



        protected void SelectionItemsProperties_OnTextChange (Object sender, EventArgs eventArgs) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            String propertyName = String.Empty;

            String comboBoxValue = String.Empty;

            String propertyValue = String.Empty;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            //Mercury.Server.Application.FormControlSelectionItem selectionItem;

            //Int32 selectedIndex = 0;

            if (designerForm == null) { return; }


            #region Control Type, Initialize Property Value and Id

            if (sender is Telerik.Web.UI.RadComboBox) {

                Telerik.Web.UI.RadComboBox propertyComboBox = (Telerik.Web.UI.RadComboBox)sender;

                propertyValue = propertyComboBox.Text;

                comboBoxValue = propertyComboBox.SelectedValue;

                propertyName = propertyComboBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadTextBox) {

                Telerik.Web.UI.RadTextBox propertyTextBox = (Telerik.Web.UI.RadTextBox)sender;

                propertyValue = propertyTextBox.Text;

                propertyName = propertyTextBox.ID;

            }

            else if (sender is Telerik.Web.UI.RadNumericTextBox) {

                Telerik.Web.UI.RadNumericTextBox propertyNumericTextBox = (Telerik.Web.UI.RadNumericTextBox)sender;

                propertyValue = propertyNumericTextBox.Value.ToString ();

                propertyName = propertyNumericTextBox.ID;

            }

            else { return; }

            #endregion


            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

            }


            DesignerForm = designerForm;

            return;

        }

        protected void SelectionItemsCommandButtonsOk_OnClick (Object sender, EventArgs eventArgs) {

            Mercury.Client.Core.Forms.Control selectedControl;

            selectedControl = DesignerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            TelerikAjaxManager.ResponseScripts.Add ("SelectionItems_Close (event);");

            return;

        }

        protected void SelectionReferenceDefault_OnItemsRequested (Object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs eventArgs) {

            const Int32 itemsPerRequest = 20;


            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            if (DesignerForm == null) { return; }


            selectedControl = DesignerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

            }

            if (selectionControl != null) {

                Int32 pageItemBegin = eventArgs.NumberOfItems;

                System.Data.DataTable pageTable = selectionControl.ReferenceGetPage (eventArgs.Text, pageItemBegin, itemsPerRequest);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    PropertiesSelectionReferenceDefault.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentRow["Text"].ToString (), currentRow["Value"].ToString ()));

                }

                eventArgs.EndOfItems = (pageTable.Rows.Count < itemsPerRequest);

                eventArgs.Message = (eventArgs.EndOfItems) ? "End of List" : "More Available";

            }

            return;

        }

        #endregion


        #region Selection Items Grid Events

        protected void SelectionItemsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventsArgs) {

            if (String.IsNullOrEmpty (PropertiesSelectedControlId.Text)) { return; }

            Mercury.Client.Core.Forms.Control selectedControl;

            selectedControl = DesignerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl.ControlType != Mercury.Server.Application.FormControlType.Selection) { return; }

            SelectionItemsGrid.DataSource = SelectionItemsGridTable;

            return;

        }

        protected void SelectionItemsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (String.IsNullOrEmpty (PropertiesSelectedControlId.Text)) { return; }

            Mercury.Client.Core.Forms.Control selectedControl;

            selectedControl = DesignerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl.ControlType != Mercury.Server.Application.FormControlType.Selection) { return; }


            Int32 selectedIndex = -1;

            switch (eventArgs.CommandName) {

                case "AddItem":

                    SelectionItems_AddItem ();

                    SelectionItemsGrid.Rebind ();

                    break;

                case "MoveItemUp":

                    if (SelectionItemsGrid.SelectedIndexes != null) {

                        if (!String.IsNullOrEmpty (SelectionItemsGrid.SelectedIndexes[0])) {

                            if (Int32.TryParse (SelectionItemsGrid.SelectedIndexes[0], out selectedIndex)) {

                                SelectionItems_MoveItemUp (selectedIndex);

                            }

                        }

                    }

                    SelectionItemsGrid.Rebind ();

                    break;

                case "MoveItemDown":

                    if (SelectionItemsGrid.SelectedIndexes != null) {

                        if (!String.IsNullOrEmpty (SelectionItemsGrid.SelectedIndexes[0])) {

                            if (Int32.TryParse (SelectionItemsGrid.SelectedIndexes[0], out selectedIndex)) {

                                SelectionItems_MoveItemDown (selectedIndex);

                            }

                        }

                    }

                    SelectionItemsGrid.Rebind ();

                    break;

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    break;

                case Telerik.Web.UI.RadGrid.UpdateCommandName:


                    break;

                case Telerik.Web.UI.RadGrid.DeleteCommandName: SelectionItems_DeleteItem (eventArgs.Item.ItemIndex); break;

                case Telerik.Web.UI.RadGrid.CancelCommandName:

                    eventArgs.Item.Edit = false;

                    SelectionItemsGrid.Rebind ();

                    break;


            }

            return;

        }

        protected void SelectionItemsGrid_UpdateCommand (Object source, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            Telerik.Web.UI.GridEditableItem gridItem = (Telerik.Web.UI.GridEditableItem)eventArgs.Item;

            System.Collections.Hashtable values = new System.Collections.Hashtable ();

            eventArgs.Item.OwnerTableView.ExtractValuesFromItem (values, gridItem);


            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            Int32 selectedIndex = gridItem.ItemIndex;

            if (designerForm == null) { return; }

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

                if (selectedIndex != -1) {

                    try {

                        selectionControl.Items[selectedIndex].Text = (String)values["ItemText"];

                        selectionControl.Items[selectedIndex].Value = (String)values["ItemValue"];

                        selectionControl.Items[selectedIndex].Enabled = Convert.ToBoolean (values["ItemEnabled"]);

                        selectionControl.Items[selectedIndex].Selected = Convert.ToBoolean (values["ItemSelected"]);

                    }

                    catch (ArgumentOutOfRangeException removeException) {

                        // DO NOTHING

                        System.Diagnostics.Debug.WriteLine (removeException.Message);

                    }

                }

            }

            DesignerForm = designerForm;


            SelectionItemsGrid.Rebind ();

            return;

        }


        private void SelectionItems_AddItem () {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;


            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

            }

            if (selectionControl != null) {

                Client.Core.Forms.Structures.SelectionItem selectionItem;

                selectionItem = selectionControl.InsertNewItem (-1);

            }


            DesignerForm = designerForm;

            return;

        }

        private void SelectionItems_DeleteItem (Int32 itemIndex) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            Int32 selectedIndex = itemIndex;

            if (designerForm == null) { return; }

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

                if (selectedIndex != -1) {

                    try {

                        selectionControl.Items.RemoveAt (selectedIndex);

                        selectedIndex = selectedIndex - 1;

                    }

                    catch (ArgumentOutOfRangeException removeException) {

                        // DO NOTHING

                        System.Diagnostics.Debug.WriteLine (removeException.Message);

                    }

                }

            }

            DesignerForm = designerForm;

            return;

        }

        private void SelectionItems_MoveItemUp (Int32 itemIndex) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;


            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            Client.Core.Forms.Structures.SelectionItem selectionItem;

            Int32 selectedIndex = itemIndex;

            if (designerForm == null) { return; }

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

                if (selectedIndex > 0) {

                    selectionItem = (Client.Core.Forms.Structures.SelectionItem)selectionControl.Items[selectedIndex];

                    if (selectionItem != null) {

                        selectionControl.Items.RemoveAt (selectedIndex);

                        selectedIndex = selectedIndex - 1;

                        selectionControl.Items.Insert (selectedIndex, selectionItem);

                    }

                }

            }

            DesignerForm = designerForm;

            return;

        }

        private void SelectionItems_MoveItemDown (Int32 itemIndex) {

            if (DesignerForm == null) { return; }

            Client.Core.Forms.Form designerForm = DesignerForm;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;

            Client.Core.Forms.Structures.SelectionItem selectionItem;

            Int32 selectedIndex = itemIndex;

            if (designerForm == null) { return; }

            selectedControl = designerForm.FindControlById (new Guid (PropertiesSelectedControlId.Text));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection)selectedControl;

                if (selectedIndex < (selectionControl.Items.Count - 1)) {

                    selectionItem = (Client.Core.Forms.Structures.SelectionItem)selectionControl.Items[selectedIndex];

                    if (selectionItem != null) {

                        selectionControl.Items.RemoveAt (selectedIndex);

                        selectedIndex = selectedIndex + 1;

                        selectionControl.Items.Insert (selectedIndex, selectionItem);

                    }

                }

            }

            DesignerForm = designerForm;

            return;

        }

        #endregion 


    }

}