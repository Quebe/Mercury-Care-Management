using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace Mercury.Web.Application.Forms.FormEditor {

    public partial class FormEditor : System.Web.UI.UserControl {

        private DateTime pageStartTime = DateTime.Now;

        private FormRenderEngine renderEngine;


        #region Public Properties

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (FormInstanceId.Text)) { FormInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return FormInstanceId.Text + ".";
            
            }

        }
    
        private Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager) Page.FindControl ("TelerikAjaxManager"); } }

        private ScriptManager MicrosoftScriptManager { get { return (ScriptManager) Page.FindControl ("MicrosoftScriptManager"); } }

        public String ResponseScript {

            get { return (String) Session[SessionCachePrefix + "ResponseScript"]; }

            set { Session[SessionCachePrefix + "ResponseScript"] = value; }

        }

        public String EditorFormCacheKey { get { return SessionCachePrefix + "EditorForm"; } }

        public Mercury.Client.Core.Forms.Form EditorForm {

            get {

                Mercury.Client.Core.Forms.Form editorForm = (Mercury.Client.Core.Forms.Form) Session[SessionCachePrefix + "EditorForm"];

                return editorForm;

            }

            set { Session[SessionCachePrefix + "EditorForm"] = value; }

        }

        public List<Client.Core.Forms.EventResult> EditorFormEventResults {

            get {

                List<Client.Core.Forms.EventResult> eventResults = (List<Client.Core.Forms.EventResult>) Session[SessionCachePrefix + "EditorFormEventResults"];

                if (eventResults == null) { eventResults = new List<Mercury.Client.Core.Forms.EventResult> (); }

                return eventResults;

            }

            set { Session[SessionCachePrefix + "EditorFormEventResults"] = value; }

        }

        public Int32 CurrentPage { 

            get { return Convert.ToInt32 (Session [SessionCachePrefix + "EditorForm.CurrentPage"]); }

            set { Session [SessionCachePrefix + "EditorForm.CurrentPage"] = value; }

        }

        #endregion


        #region Public Methods

        public void SetFormInstanceId (String formInstanceId) {

            FormInstanceId.Text = formInstanceId;

            return;

        }

        #endregion 


        #region Page Events

        protected void Page_PreInit (Object sender, EventArgs e) {

            pageStartTime = DateTime.Now;

            System.Diagnostics.Debug.WriteLine ("");

            System.Diagnostics.Debug.WriteLine ("---------------------");

            System.Diagnostics.Debug.WriteLine ("FORM EDITOR (BEGIN)");

            return;

        }


        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            renderEngine = new FormRenderEngine (this, MercuryApplication, Session, SessionCachePrefix);

            if (EditorForm != null) { RenderForm (true); }


            Page.LoadComplete += new EventHandler (Page_LoadComplete);

            //System.Diagnostics.Debug.WriteLine ("FORM EDITOR (Page_Load): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            return;

        }

        public void Page_LoadComplete (Object sender, EventArgs e) {

            //System.Diagnostics.Debug.WriteLine ("FORM EDITOR (Page_LoadComplete): " + DateTime.Now.Subtract (pageStartTime).TotalMilliseconds.ToString ());

            renderEngine.IsFormRendered = false;

            RenderForm (true);

            return;

        }
       
        #endregion 


        #region Initializations

        private void InitializePager () {

            if (EditorForm.PageCountVisible > 1) { FormPaging.Style.Add ("display", "block"); }

            else { FormPaging.Style.Add ("display", "none"); }


            for (Int32 currentPageIndex = 1; currentPageIndex <= 9; currentPageIndex++) {

                String pageButtonName = "FormPagerPage" + currentPageIndex.ToString ();

                LinkButton pageButton = (LinkButton)FindControl (pageButtonName);

                if (pageButton != null) {

                    if (currentPageIndex <= EditorForm.PageCountVisible) {

                        pageButton.CssClass = "PagerButton";

                        if (currentPageIndex == CurrentPage) {

                            pageButton.CssClass = pageButton.CssClass + " PagerButtonSelected";

                        }

                    }

                    else {

                        pageButton.CssClass = "PagerButtonHidden";

                    }

                }

            }

            return;

        }

        #endregion


        #region Public Methods

        public void SetForm (Int64 entityFormId) {

            if (MercuryApplication == null) { return; }

            Client.Core.Forms.Form editorForm = MercuryApplication.FormGetByEntityFormId (entityFormId);

            editorForm.EventHandlers_Precompile ();

            EditorForm = editorForm;

            renderEngine = new FormRenderEngine (this, MercuryApplication, Session, SessionCachePrefix);

            return;

        }

        public void SetForm (Mercury.Client.Core.Forms.Form forForm) {

            forForm.EventHandlers_Precompile ();

            EditorForm = forForm;

            if (MercuryApplication == null) { return; }

            renderEngine = new FormRenderEngine (this, MercuryApplication, Session, SessionCachePrefix);


            // BELOW IS FOR EXPORTING THE FORM IN BINARY FORMAT TO CAPTURE AND USE IN A DIFFERENT APPLICATION (E.G. BUG TESTING)

            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binarySerializer = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter ();

            // System.IO.FileStream fileWriter = new System.IO.FileStream ("C:\\MERCURY\\" + EditorForm.Name + "_" + Guid.NewGuid ().ToString () + ".form", System.IO.FileMode.CreateNew);

            //binarySerializer.Serialize (fileWriter, EditorForm);

            //fileWriter.Close ();
            

            return;

        }

        public void SetFormForPreview (Mercury.Client.Core.Forms.Form forForm) {

            Client.Core.Forms.Form editorForm = forForm.Copy ();

            editorForm.EventHandlers_Precompile ();

            EditorForm = editorForm;

            return;

        }

        #endregion 


        #region Form Render Engine

        public HtmlGenericControl AppendElement_HtmlDiv (System.Web.UI.Control parentControl, String elementId, String style) {

            HtmlGenericControl element = new HtmlGenericControl ("div");

            element.ID = elementId;

            if (!String.IsNullOrEmpty (style)) {

                element.Attributes.Add ("style", style);

            }

            parentControl.Controls.Add (element);

            return element;

        }

        public void RenderForm (Boolean wireEvents) {

            if (EditorForm == null) { return; }

            if (MercuryApplication == null) { return; }

            if (renderEngine.IsFormRendered) { return; }


            DateTime startTime = DateTime.Now;

            //System.Diagnostics.Debug.WriteLine ("");
            //System.Diagnostics.Debug.WriteLine ("");
            //System.Diagnostics.Debug.WriteLine ("");
            //System.Diagnostics.Debug.WriteLine ("EDITOR RENDER (BEGIN)");

            
            EditorFormEventResults = EditorForm.EventResultsCopy;

            FormContent.Controls.Clear ();



            if (EditorForm.PageCountVisible > 1) {

                FormPaging.Style.Add ("display", "block");

                if (CurrentPage < 1) { CurrentPage = 1; }

                InitializePager ();

                FormContent.Controls.Add (renderEngine.RenderFormPage (EditorForm, CurrentPage));

            }

            else {

                FormContent.Controls.Add (renderEngine.RenderForm (EditorForm));

            }

            System.Diagnostics.Debug.WriteLine ("Editor Form Render Time: " + DateTime.Now.Subtract (startTime).TotalMilliseconds);

            if (wireEvents) { WireFormEvents (); }

            System.Diagnostics.Debug.WriteLine ("Editor Form Wire Events Time: " + DateTime.Now.Subtract (startTime).TotalMilliseconds);

            return;

        }

        public void WireFormEvents () {

            Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.AjaxSetting ajaxSetting;

            foreach (Mercury.Client.Core.Forms.Control currentControl in editorForm.GetAllControls ()) {

                ajaxSetting = null;

                Boolean isControlHot = false; // DOES CONTROL REQUIRE WIRING BECAUSE OF EVENT OR DATA DEPENDENCIES?

                Boolean requiresCustomWiring = false; // DOES CONTROL REQUIRE CUSTOM WIRING?

                if (currentControl.EventHandlers.Count != 0) { isControlHot = true; }

                else if (editorForm.GetDataBindingDependencies (currentControl.ControlId).Count != 0) { isControlHot = true; }


                #region Custom Hot Control Determinations

                switch (currentControl.ControlType) {

                    case Mercury.Server.Application.FormControlType.Input:

                        if (!String.IsNullOrEmpty (((Client.Core.Forms.Controls.Input) currentControl).Validation)) { isControlHot = true; }

                        break;

                }


                #endregion 


                #region Determine if Control Requires Custom Wiring

                switch (currentControl.ControlType) { 

                    case Mercury.Server.Application.FormControlType.Button:

                    case Mercury.Server.Application.FormControlType.Entity:

                    case Mercury.Server.Application.FormControlType.Address:

                    case Mercury.Server.Application.FormControlType.Collection:

                    case Mercury.Server.Application.FormControlType.Service:

                        requiresCustomWiring = true;

                        break;

                }

                #endregion 


                #region Default Wiring

                if ((isControlHot) && (!requiresCustomWiring)) {

                    // SET AJAX TRIGGER DIRECTLY ON THE CONTROL ITSELF AND NOT THE CONTAINING DIV

                    ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_" + currentControl.ControlId.ToString ());

                    if ((currentControl.EventHandlers_AllSmart ()) && (currentControl.DataBindings.Count == 0)) {

                        // ADD SELF AS UPDATED CONTROL
                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                        foreach (Guid currentControlId in currentControl.EventHandler_UpdatedControls ().Keys) {

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControlId.ToString (), "AjaxLoadingPanel"));

                        }

                    }

                    else {

                        // DON'T DO SMART UPDATE IF DATA DEPENDENCIES, SINCE THEY CAN FIRE THEIR OWN EVENTS

                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", "AjaxLoadingPanel"));

                    }

                    TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                }

                #endregion


                #region Custom Wiring

                else if (requiresCustomWiring) {

                    switch (currentControl.ControlType) {
                        
                        case Mercury.Server.Application.FormControlType.Button:

                            //ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_" + currentControl.ControlId.ToString ());

                            //// ADD SELF AS UPDATED CONTROL
                            //ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                            //ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", "AjaxLoadingPanel"));

                            //TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                            break;

                        case Mercury.Server.Application.FormControlType.Entity:

                            #region Entity

                            Mercury.Client.Core.Forms.Controls.Entity entityControl = ((Mercury.Client.Core.Forms.Controls.Entity) currentControl);

                            switch (entityControl.DisplayStyle) {

                                #region NormalExpanded Entity

                                case Mercury.Server.Application.FormControlEntityDisplayStyle.NormalExpanded:

                                    String entityType = ((Mercury.Client.Core.Forms.Controls.Entity) currentControl).EntityType.ToString ();

                                    ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_" + entityType + "SearchButton_" + currentControl.ControlId.ToString ());

                                    if (((Mercury.Client.Core.Forms.Controls.Entity) currentControl).EventHandlers.Count == 0) {

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "SearchButton_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "SearchMessage_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "Results_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                    }

                                    else {

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", "AjaxLoadingPanel"));

                                    }

                                    TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);


                                    ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_" + entityType + "Results_" + currentControl.ControlId.ToString ());

                                    if ((((Mercury.Client.Core.Forms.Controls.Entity) currentControl).EventHandlers.Count == 0) 
                                        
                                        && (editorForm.GetDataBindingDependencies (currentControl.ControlId).Count == 0)) {

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "Selected_" + currentControl.ControlId.ToString (), "AjaxLoadingPanelWhiteout"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "SearchButton_" + currentControl.ControlId.ToString (), "AjaxLoadingPanelWhiteout"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "SearchMessage_" + currentControl.ControlId.ToString (), "AjaxLoadingPanelWhiteout"));

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_" + entityType + "Results_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                    }

                                    else {

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", "AjaxLoadingPanel"));

                                    }

                                    TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                                   #endregion 

                                    break;

                                case Mercury.Server.Application.FormControlEntityDisplayStyle.NameOnly:

                                    ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_" + currentControl.ControlId.ToString () + "_Name");

                                    
                                    if ((((Mercury.Client.Core.Forms.Controls.Entity) currentControl).EventHandlers.Count == 0) 

                                        && (editorForm.GetDataBindingDependencies (currentControl.ControlId).Count == 0)) {

                                        // ONLY UPDATE SELF, NO EVENTS OR DATA DEPENDENCIES
                                            
                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                                    }

                                    else {

                                        ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", "AjaxLoadingPanel"));

                                    }

                                    TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                                    break;

                            }

                            #endregion 

                            break;
                                
                        case Mercury.Server.Application.FormControlType.Collection:

                            #region Collection

                            ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_CollectionGrid_" + currentControl.ControlId.ToString ());

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_CollectionGrid_" + currentControl.ControlId.ToString (), ""));

                            foreach (Mercury.Client.Core.Forms.Control dependentControl in editorForm.GetDataBindingDependencies (currentControl.ControlId)) {

                                ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + dependentControl.ControlId.ToString (), ""));

                            }

                            TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                            #endregion 

                            break;

                        case Mercury.Server.Application.FormControlType.Address:

                            #region Address

                            ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_ZipCode_" + currentControl.ControlId.ToString ());

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_State_" + currentControl.ControlId.ToString (), ""));

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_City_" + currentControl.ControlId.ToString (), ""));

                            foreach (Mercury.Client.Core.Forms.Control dependentControl in editorForm.GetDataBindingDependencies (currentControl.ControlId)) {

                                ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + dependentControl.ControlId.ToString (), ""));

                            }

                            TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                            
                            ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_State_" + currentControl.ControlId.ToString ());

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_State_" + currentControl.ControlId.ToString (), ""));

                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl (currentControl.Name + "_City_" + currentControl.ControlId.ToString (), ""));

                            foreach (Mercury.Client.Core.Forms.Control dependentControl in editorForm.GetDataBindingDependencies (currentControl.ControlId)) {

                                ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + dependentControl.ControlId.ToString (), ""));

                            }

                            TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                            #endregion 

                            break;

                        case Mercury.Server.Application.FormControlType.Service:

                            #region Service

                            ajaxSetting = new Telerik.Web.UI.AjaxSetting (currentControl.Name + "_ServiceDate_" + currentControl.ControlId.ToString ());

                            // ADD SELF AS UPDATED CONTROL
                            ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControl.ControlId.ToString (), "AjaxLoadingPanel"));

                            if ((currentControl.EventHandlers_AllSmart ()) && (currentControl.DataBindings.Count == 0)) {

                                foreach (Guid currentControlId in currentControl.EventHandler_UpdatedControls ().Keys) {

                                    ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("ControlId_" + currentControlId.ToString (), "AjaxLoadingPanel"));

                                }

                            }

                            else {

                                // DON'T DO SMART UPDATE IF DATA DEPENDENCIES, SINCE THEY CAN FIRE THEIR OWN EVENTS

                                ajaxSetting.UpdatedControls.Add (new Telerik.Web.UI.AjaxUpdatedControl ("FormContent", String.Empty));

                            }

                            TelerikAjaxManagerProxy.AjaxSettings.Add (ajaxSetting);

                            #endregion 

                            break;

                    }

                }

                #endregion 

            }

            return;

        }
           
        #endregion


        #region Form Control Events

        public void FormControl_OnTextChange (Object sender, EventArgs eventArgs) {

            String controlName = ((System.Web.UI.Control) sender).ID;

            String comboBoxValue = String.Empty;

            String comboBoxText = String.Empty;

            String controlValue = String.Empty;

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Mercury.Client.Core.Forms.Control formControl;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            if (controlName.Split ('_').Length <= 0) { return; }


            #region Control Type, Initialize Property Value and Id

            if (sender is Telerik.Web.UI.RadComboBox) {

                Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

                if (controlComboBox.SelectedItem != null) {

                    comboBoxValue = controlComboBox.SelectedItem.Value;

                    comboBoxText = controlComboBox.SelectedItem.Text;

                    controlValue = controlComboBox.SelectedItem.Text;

                }

                else {

                    comboBoxValue = controlComboBox.SelectedValue;

                    comboBoxText = controlComboBox.Text;

                    controlValue = controlComboBox.Text;    

                }                

            }

            else if (sender is Telerik.Web.UI.RadTextBox) {

                Telerik.Web.UI.RadTextBox controlTextBox = (Telerik.Web.UI.RadTextBox) sender;

                controlValue = controlTextBox.Text;

            }

            else if (sender is Telerik.Web.UI.RadMaskedTextBox) {

                Telerik.Web.UI.RadMaskedTextBox controlMaskedTextBox = (Telerik.Web.UI.RadMaskedTextBox) sender;

                controlValue = controlMaskedTextBox.Text;

            }

            else if (sender is Telerik.Web.UI.RadDateInput) {

                Telerik.Web.UI.RadDateInput controlDateInput = (Telerik.Web.UI.RadDateInput) sender;

                if (controlDateInput.SelectedDate.HasValue) {

                    controlValue = controlDateInput.SelectedDate.Value.ToString ("MM/dd/yyyy");

                }

            }

            else if (sender is Telerik.Web.UI.RadNumericTextBox) {

                Telerik.Web.UI.RadNumericTextBox controlNumericTextBox = (Telerik.Web.UI.RadNumericTextBox) sender;

                controlValue = controlNumericTextBox.Value.ToString ();

            }

            else {

                System.Diagnostics.Debug.WriteLine (sender.ToString ());

            }

            #endregion


            Guid controlGuid = Guid.Empty;


            for (Int32 currentIndex = (controlName.Split ('_').Length - 1); currentIndex >= 0; currentIndex--) {
                
                if (Mercury.Server.CommonFunctions.IsGuid (controlName.Split ('_')[currentIndex])) {

                    controlGuid = new Guid (controlName.Split ('_')[currentIndex]);

                    break;

                }

            }


            if (controlGuid == Guid.Empty) { return; }


            // formControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[controlName.Split ('_').Length - 1]));

            formControl = editorForm.FindControlById (controlGuid);

            if (formControl != null) {

                if (!formControl.ReadOnly) {

                    switch (formControl.ControlType) {

                        case Mercury.Server.Application.FormControlType.Input:

                            ((Mercury.Client.Core.Forms.Controls.Input) formControl).Text = controlValue;

                            break;

                        case Mercury.Server.Application.FormControlType.Selection:

                            switch (((Mercury.Client.Core.Forms.Controls.Selection) formControl).SelectionType) {

                                case Mercury.Server.Application.FormControlSelectionType.DropDownList:

                                    ((Mercury.Client.Core.Forms.Controls.Selection) formControl).SetItemSelection (comboBoxValue, comboBoxText, true);

                                    break;

                                case Mercury.Server.Application.FormControlSelectionType.ListBox:

                                case Mercury.Server.Application.FormControlSelectionType.CheckBox:

                                case Mercury.Server.Application.FormControlSelectionType.RadioButton:

                                    ListControl listBoxControl = (ListControl) sender;

                                    foreach (ListItem currentItem in listBoxControl.Items) {

                                        ((Mercury.Client.Core.Forms.Controls.Selection) formControl).SetItemSelectionManual (currentItem.Value, currentItem.Selected);

                                    }

                                    ((Mercury.Client.Core.Forms.Controls.Selection) formControl).RaiseEvent ("SelectionChanged");

                                    break;

                            } // switch (((Mercury.Client.Core.Forms.Controls.Selection) formControl).SelectionType) {

                            break;

                        case Mercury.Server.Application.FormControlType.Address:

                            Mercury.Client.Core.Forms.Controls.Address addressControl = (Mercury.Client.Core.Forms.Controls.Address) formControl;

                            if (sender is Telerik.Web.UI.RadTextBox) {

                                Telerik.Web.UI.RadTextBox addressLine = (Telerik.Web.UI.RadTextBox) sender;

                                if (addressLine.ID.Contains ("_Line1")) { addressControl.Line1 = addressLine.Text; }

                                if (addressLine.ID.Contains ("_Line2")) { addressControl.Line2 = addressLine.Text; }

                                addressControl.EntityAddressId = 0;

                            }

                            else if (sender is Telerik.Web.UI.RadComboBox) {

                                Telerik.Web.UI.RadComboBox addressCity = (Telerik.Web.UI.RadComboBox) sender;

                                addressControl.City = addressCity.Text;

                                addressControl.EntityAddressId = 0;

                            }

                            break;

                        case Mercury.Server.Application.FormControlType.Metric:

                            Mercury.Client.Core.Forms.Controls.Metric metricControl = (Mercury.Client.Core.Forms.Controls.Metric) formControl;

                            if (sender is Telerik.Web.UI.RadNumericTextBox) {

                                Telerik.Web.UI.RadNumericTextBox metricValue = (Telerik.Web.UI.RadNumericTextBox) sender;

                                if (metricValue.Value.HasValue) {

                                    metricControl.MetricValue = Convert.ToDecimal (metricValue.Value.Value);

                                }

                                else { metricControl.MetricValue = 0; }

                            }

                            break;

                    } // switch (formControl.ControlType)

                } // if (!formControl.ReadOnly)

            } // if (formControl != null) {


            renderEngine.IsFormRendered = false;

            EditorForm = editorForm;

            return;

        }

        public void FormControlButton_OnClick (Object sender, EventArgs eventArgs) {

            String formControlId = ((Button) sender).ID;

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Mercury.Client.Core.Forms.Control formControl;

            formControlId = formControlId.Split ('_')[formControlId.Split ('_').Length - 1];

            Boolean performSearch = true;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }


            formControl = editorForm.FindControlById (new Guid (formControlId));

            if (formControl != null) {

                switch (formControl.ControlType) {

                    case Mercury.Server.Application.FormControlType.Button:

                        ((Client.Core.Forms.Controls.Button) formControl).Click ();
                           
                        break;

                    case Mercury.Server.Application.FormControlType.Entity:

                        #region Mercury.Server.Application.FormControlType.Entity

                        switch (((Mercury.Client.Core.Forms.Controls.Entity) formControl).EntityType) {

                            case Mercury.Server.Application.EntityType.Member:

                                #region Entity Type - Member

                                Telerik.Web.UI.RadTextBox memberName = (Telerik.Web.UI.RadTextBox) this.FindControl (formControl.Name + "_MemberName_" + formControlId);

                                Telerik.Web.UI.RadDateInput memberBirthDate = (Telerik.Web.UI.RadDateInput) this.FindControl (formControl.Name + "_MemberBirthDate_" + formControlId);

                                Telerik.Web.UI.RadTextBox memberId = (Telerik.Web.UI.RadTextBox) this.FindControl (formControl.Name + "_MemberId_" + formControlId);

                                List<Mercury.Server.Application.SearchResultMember> memberResults;


                                Session[SessionCachePrefix + formControl.Name + "_MemberSearchStatus_" + formControl.ControlId.ToString ()] = String.Empty;

                                // VALIDATE: MEMBER NAME OR MEMBER ID AVAILABLE
                                if ((String.IsNullOrEmpty (memberName.Text)) && (String.IsNullOrEmpty (memberId.Text))) {

                                    Session[SessionCachePrefix + formControl.Name + "_MemberSearchMessage_" + formControl.ControlId.ToString ()] = "Member Name or Id not valid or found.";

                                    performSearch = false;
                                
                                }

                                // VALIDATE: MINIMUM LENGTH
                                if ((memberName.Text.Length < 3) && (String.IsNullOrEmpty (memberId.Text))) {

                                    Session[SessionCachePrefix + formControl.Name + "_MemberSearchMessage_" + formControl.ControlId.ToString ()] = "Member Name or Id not valid or found.";

                                    performSearch = false; 
                                
                                }

                                // VALIDATE: BIRTH DATE REQUIREMENTS
                                if ((!MercuryApplication.HasEnvironmentPermission ("Search.Member.OptionalBirthDate")) && (memberName.Text.Length > 0)) {

                                    if (memberBirthDate.Text.Length == 0) {

                                        Session[SessionCachePrefix + formControl.Name + "_MemberSearchMessage_" + formControl.ControlId.ToString ()] = "Birth Date field required.";

                                        performSearch = false; 
                                    
                                    }

                                }

                                if (performSearch) {

                                    if (memberName.Text.Length > 0) {

                                        memberResults = MercuryApplication.SearchMemberByName (memberName.Text, memberBirthDate.SelectedDate);

                                    }

                                    else {

                                        memberResults = MercuryApplication.SearchMemberById (memberId.Text);

                                    }

                                    System.Data.DataTable memberResultsTable = new DataTable ();

                                    memberResultsTable.Columns.Add ("MemberId");

                                    memberResultsTable.Columns.Add ("EntityId");

                                    memberResultsTable.Columns.Add ("Name");

                                    memberResultsTable.Columns.Add ("BirthDate");

                                    memberResultsTable.Columns.Add ("Gender");

                                    memberResultsTable.Columns.Add ("Active");

                                    foreach (Mercury.Server.Application.SearchResultMember currentResult in memberResults) {

                                        memberResultsTable.Rows.Add (
                                            currentResult.MemberId,
                                            currentResult.EntityId,
                                            currentResult.Name,
                                            currentResult.BirthDate.ToString ("MM/dd/yyyy"),
                                            currentResult.Gender,
                                            currentResult.CurrentlyEnrolled.ToString ()
                                            
                                        );

                                    }

                                    Session[SessionCachePrefix + formControl.Name + "_MemberResultsTable_" + formControl.ControlId.ToString ()] = memberResultsTable;

                                    if (MercuryApplication.LastException == null) {

                                        Session[SessionCachePrefix + formControl.Name + "_MemberSearchStatus_" + formControl.ControlId.ToString ()] = "Success";

                                    }

                                    Session[SessionCachePrefix + formControl.Name + "_MemberSearchException_" + formControl.ControlId.ToString ()] = MercuryApplication.LastException;

                                    Telerik.Web.UI.RadGrid memberResultsGrid = (Telerik.Web.UI.RadGrid) this.FindControl (formControl.Name + "_MemberResults_" + formControl.ControlId.ToString ());

                                    if (memberResultsGrid != null) {

                                        // DO NOTHING

                                    }

                                }

                                #endregion

                                break;

                            case Mercury.Server.Application.EntityType.Provider:
                                
                                #region Entity Type - Provider

                                Telerik.Web.UI.RadTextBox providerName = (Telerik.Web.UI.RadTextBox) this.FindControl (formControl.Name + "_ProviderName_" + formControlId);

                                // Telerik.Web.UI.RadDateInput providerBirthDate = (Telerik.Web.UI.RadDateInput) this.FindControl (formControl.Name + "_ProviderBirthDate_" + formControlId);

                                // Telerik.Web.UI.RadTextBox providerId = (Telerik.Web.UI.RadTextBox) this.FindControl (formControl.Name + "_ProviderId_" + formControlId);

                                List<Mercury.Server.Application.SearchResultProvider> providerResults;


                                Session[SessionCachePrefix + formControl.Name + "_ProviderSearchStatus_" + formControl.ControlId.ToString ()] = String.Empty;

                                // VALIDATE: PROVIDER NAME OR MEMBER ID AVAILABLE
                                if (String.IsNullOrEmpty (providerName.Text)) {

                                    Session[SessionCachePrefix + formControl.Name + "_ProviderSearchMessage_" + formControl.ControlId.ToString ()] = "Provider Name not valid or found.";

                                    performSearch = false;

                                }

                                // VALIDATE: MINIMUM LENGTH
                                if (providerName.Text.Length < 3) {

                                    Session[SessionCachePrefix + formControl.Name + "_ProviderSearchMessage_" + formControl.ControlId.ToString ()] = "Provider Name not valid or found.";

                                    performSearch = false;

                                }

                                if (performSearch) {

                                    providerResults = MercuryApplication.SearchProviderByName (providerName.Text, true);

                                    System.Data.DataTable providerResultsTable = new DataTable ();

                                    providerResultsTable.Columns.Add ("ProviderId");

                                    providerResultsTable.Columns.Add ("EntityId");

                                    providerResultsTable.Columns.Add ("Name");

                                    foreach (Mercury.Server.Application.SearchResultProvider currentResult in providerResults) {

                                        providerResultsTable.Rows.Add (
                                            currentResult.ProviderId,
                                            currentResult.EntityId,
                                            currentResult.Name
                                        );

                                    }

                                    Session[SessionCachePrefix + formControl.Name + "_ProviderResultsTable_" + formControl.ControlId.ToString ()] = providerResultsTable;

                                    if (MercuryApplication.LastException == null) {

                                        Session[SessionCachePrefix + formControl.Name + "_ProviderSearchStatus_" + formControl.ControlId.ToString ()] = "Success";

                                    }

                                    Session[SessionCachePrefix + formControl.Name + "_ProviderSearchException_" + formControl.ControlId.ToString ()] = MercuryApplication.LastException;

                                    Telerik.Web.UI.RadGrid providerResultsGrid = (Telerik.Web.UI.RadGrid) this.FindControl (formControl.Name + "_ProviderResults_" + formControl.ControlId.ToString ());

                                    if (providerResultsGrid != null) {

                                        // DO NOTHING

                                    }

                                }

                                #endregion

                                break;

                        } // (((Mercury.Client.Core.Forms.Controls.Entity) formControl).EntityType) { 

                        #endregion

                        break;


                } // switch (formControl.ControlType) {

            } // if (formControl != null) {



            renderEngine.IsFormRendered = false;

            EditorForm = editorForm;

//            RenderForm ();

            return;

        }

        public void FormControlEntityGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            String controlName = ((System.Web.UI.Control) sender).ID;

            String formControlId = controlName.Split ('_')[controlName.Split ('_').Length - 1];

            Mercury.Client.Core.Forms.Controls.Entity entityControl;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }


            entityControl = (Mercury.Client.Core.Forms.Controls.Entity) editorForm.FindControlById (new Guid (formControlId));

            if (entityControl != null) {

                switch (entityControl.EntityType) {

                    case Mercury.Server.Application.EntityType.Member:

                        #region Entity - Member

                        Int64 memberId;

                        if (Int64.TryParse (eventArgs.Item.Cells [2].Text, out memberId)) {

                            entityControl.EntityObjectId = memberId;

                            Session[SessionCachePrefix + entityControl.Name + "_MemberSearchStatus_" + entityControl.ControlId.ToString ()] = "";

                        }

                        else {

                            Session[SessionCachePrefix + entityControl.Name + "_MemberSearchStatus_" + entityControl.ControlId.ToString ()] = "Error";

                        }

                        #endregion

                        break;

                    case Mercury.Server.Application.EntityType.Provider:

                        #region Entity - Provider

                        Int64 providerId;

                        if (Int64.TryParse (eventArgs.Item.Cells[2].Text, out providerId)) {

                            entityControl.EntityObjectId = providerId;

                            Session[SessionCachePrefix + entityControl.Name + "_ProviderSearchStatus_" + entityControl.ControlId.ToString ()] = "";

                        }
                        
                        else {

                            Session[SessionCachePrefix + entityControl.Name + "_ProviderSearchStatus_" + entityControl.ControlId.ToString ()] = "Error";

                        }

                        #endregion

                        break;

                } // switch (entityControl.EntityType) {

            } // if (entityControl != null) 

            renderEngine.IsFormRendered = false;

            EditorForm = editorForm;

//            RenderForm ();

            return;

        }

        public void FormControlEntityControl_NameOnItemsRequested (Object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs eventArgs) {
            
            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Entity entityControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Entity) {

                entityControl = (Mercury.Client.Core.Forms.Controls.Entity) selectedControl;

            }

            if (entityControl != null) {

                switch (entityControl.EntityType) {

                    case Mercury.Server.Application.EntityType.Member:

                        List<Mercury.Server.Application.SearchResultMember> memberResults = MercuryApplication.SearchMemberByName (eventArgs.Text, null);

                        System.Data.DataTable memberResultsTable = new DataTable ();

                        memberResultsTable.Columns.Add ("Id");

                        memberResultsTable.Columns.Add ("MemberName");

                        memberResultsTable.Columns.Add ("BirthDate");

                        memberResultsTable.Columns.Add ("CurrentAge");

                        memberResultsTable.Columns.Add ("Gender");

                        memberResultsTable.Columns.Add ("Enrolled");


                        if (memberResults != null) {

                            foreach (Mercury.Server.Application.SearchResultMember currentResult in memberResults) {

                                memberResultsTable.Rows.Add (

                                    currentResult.MemberId.ToString (),

                                    currentResult.Name,

                                    currentResult.BirthDate.ToString ("MM/dd/yyyy"),

                                    currentResult.CurrentAge.ToString (),

                                    currentResult.Gender,

                                    currentResult.CurrentlyEnrolled.ToString ()

                                    );

                                // controlComboBox.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentResult.Name, currentResult.MemberId.ToString ()));


                            }

                        }

                        controlComboBox.DataSource = memberResultsTable;

                        controlComboBox.ClearSelection ();

                        controlComboBox.DataBind ();

                        break;

                    case Mercury.Server.Application.EntityType.Provider:

                        List<Mercury.Server.Application.SearchResultProvider> providerResults = MercuryApplication.SearchProviderByName (eventArgs.Text, true);

                        System.Data.DataTable providerResultsTable = new DataTable ();

                        providerResultsTable.Columns.Add ("Id");

                        providerResultsTable.Columns.Add ("ProviderName");

                        providerResultsTable.Columns.Add ("FederalTaxId");

                        providerResultsTable.Columns.Add ("NationalProviderId");

                        providerResultsTable.Columns.Add ("PrimarySpecialtyName");


                        if (providerResults != null) {

                            foreach (Mercury.Server.Application.SearchResultProvider currentResult in providerResults) {

                                providerResultsTable.Rows.Add (

                                    currentResult.ProviderId,

                                    currentResult.Name,

                                    currentResult.FederalTaxId,

                                    currentResult.NationalProviderId,

                                    currentResult.PrimarySpecialtyName

                                    );

                            }

                        }

                        controlComboBox.DataSource = providerResultsTable;

                        controlComboBox.ClearSelection ();

                        controlComboBox.DataBind ();

                        break;

                }


                eventArgs.EndOfItems = true;

                eventArgs.Message = (eventArgs.EndOfItems) ? "End of List" : "More Available";

            }

            EditorForm = editorForm;

            return;

        }

        public void FormControlEntityControl_NameOnItemDataBound (Object sender, Telerik.Web.UI.RadComboBoxItemEventArgs eventArgs) {

            Telerik.Web.UI.RadComboBoxItem currentItem = eventArgs.Item;

            System.Data.DataRowView currentRow = (System.Data.DataRowView) currentItem.DataItem;

            eventArgs.Item.Text = String.Empty;

            eventArgs.Item.Value = (String) currentRow["Id"];

            return;

        }

        public void FormControlEntityControl_NameOnTextChanged (Object sender, EventArgs eventArgs) {

            String controlName = ((System.Web.UI.Control) sender).ID;
            
            String comboBoxValue = String.Empty;

            String comboBoxText = String.Empty;

            String controlValue = String.Empty;

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Mercury.Client.Core.Forms.Controls.Entity entityControl;

            Int64 entityObjectId = 0;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }



            Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

            if (controlComboBox.SelectedItem != null) {

                comboBoxValue = controlComboBox.SelectedItem.Value;

                comboBoxText = controlComboBox.SelectedItem.Text;

                controlValue = controlComboBox.SelectedItem.Text;

            }

            else {

                comboBoxValue = controlComboBox.SelectedValue;

                comboBoxText = controlComboBox.Text;

                controlValue = controlComboBox.Text;

            }
            
            entityControl = (Client.Core.Forms.Controls.Entity) editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

            if (entityControl != null) {

                if (Int64.TryParse (comboBoxValue, out entityObjectId)) {

                    entityControl.EntityObjectId = entityObjectId;

                }

                else {

                    if (entityControl.AllowCustomEntityName) {

                        entityControl.EntityObjectId = 0;

                        entityControl.EntityName = comboBoxText;

                    }

                    else if (String.IsNullOrEmpty (comboBoxText)) {

                        entityControl.EntityObjectId = 0;

                    }

                }

            }

            return;

        }


        public void FormControlCollectionGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            System.Diagnostics.Debug.WriteLine ("Collection Item Select");


            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            String controlName = ((System.Web.UI.Control) sender).ID;

            String formControlId = controlName.Split ('_')[controlName.Split ('_').Length - 1];

            Mercury.Client.Core.Forms.Controls.Collection collectionControl;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }


            collectionControl = (Mercury.Client.Core.Forms.Controls.Collection) editorForm.FindControlById (new Guid (formControlId));

            if (collectionControl != null) {

                switch (eventArgs.CommandName) {

                    case Telerik.Web.UI.RadGrid.SelectCommandName:

                        Int64 selectedItem = Int64.Parse (eventArgs.Item.Cells[3].Text);

                        collectionControl.SelectedItem = selectedItem;

                        break;

                }

            }

            renderEngine.IsFormRendered = false;

            EditorForm = editorForm;

//            RenderForm ();

            return;

        }

        public void FormControlSelectionControl_OnItemsRequested (Object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs eventArgs) {

            const Int32 itemsPerRequest = 20;


            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }


            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                selectionControl = (Mercury.Client.Core.Forms.Controls.Selection) selectedControl;

            }

            if (selectionControl != null) {

                Int32 pageItemBegin = eventArgs.NumberOfItems;

                System.Data.DataTable pageTable = selectionControl.ReferenceGetPage (eventArgs.Text, pageItemBegin, itemsPerRequest);

                foreach (System.Data.DataRow currentRow in pageTable.Rows) {

                    controlComboBox.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentRow["Text"].ToString (), currentRow["Value"].ToString ()));

                }

                eventArgs.EndOfItems = (pageTable.Rows.Count < itemsPerRequest);

                eventArgs.Message = (eventArgs.EndOfItems) ? "End of List" : "More Available";

            }

            EditorForm = editorForm;

//            RenderForm ();

            return;

        }

        public void FormControlSelectionControl_OnSelectedIndexChanged (Object sender, EventArgs eventArgs) {

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            if (sender is Telerik.Web.UI.RadListBox) {

                #region Telerik RadListBox

                Telerik.Web.UI.RadListBox selectionListControlRadListBox = (Telerik.Web.UI.RadListBox) sender;

                String controlName = ((System.Web.UI.Control) sender).ID;


                Mercury.Client.Core.Forms.Control selectedControl;

                Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;


                if (MercuryApplication == null) { return; }

                if (editorForm == null) { return; }


                selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

                if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                    selectionControl = (Mercury.Client.Core.Forms.Controls.Selection) selectedControl;

                }

                if (selectionControl != null) {

                    if (!selectedControl.ReadOnly) {

                        foreach (Telerik.Web.UI.RadListBoxItem currentItem in selectionListControlRadListBox.Items) {

                            selectionControl.SetItemSelectionManual (currentItem.Value, currentItem.Selected);

                        }

                        selectionControl.RaiseEvent ("SelectionChanged");

                        selectionControl.DataSourceChanged ();

                    }

                }

                #endregion

            }

            else {

                #region Standard List Controls (CheckBox/Radio Buttons)

                ListControl selectionListControl = (ListControl) sender;

                String controlName = ((System.Web.UI.Control) sender).ID;


                Mercury.Client.Core.Forms.Control selectedControl;

                Mercury.Client.Core.Forms.Controls.Selection selectionControl = null;


                if (MercuryApplication == null) { return; }

                if (editorForm == null) { return; }


                selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

                if (selectedControl is Mercury.Client.Core.Forms.Controls.Selection) {

                    selectionControl = (Mercury.Client.Core.Forms.Controls.Selection) selectedControl;

                }

                if (selectionControl != null) {

                    if (!selectedControl.ReadOnly) {

                        foreach (ListItem currentItem in selectionListControl.Items) {

                            selectionControl.SetItemSelectionManual (currentItem.Value, currentItem.Selected);

                        }

                        selectionControl.RaiseEvent ("SelectionChanged");

                        selectionControl.DataSourceChanged ();

                    }

                }

                #endregion

            }



            EditorForm = editorForm;
  
            return;

        }

        public void FormControlAddressControl_CityOnItemsRequested (Object sender, Telerik.Web.UI.RadComboBoxItemsRequestedEventArgs eventArgs) {
            
//            const Int32 itemsPerRequest = 20;


            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Address addressControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[2]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) {

                addressControl = (Mercury.Client.Core.Forms.Controls.Address) selectedControl;

            }

            if (addressControl != null) {

                Int32 pageItemBegin = eventArgs.NumberOfItems;

                List<String> cityReference = MercuryApplication.CityReferenceByState (addressControl.State, eventArgs.Text);

                foreach (String cityName in cityReference) {

                    controlComboBox.Items.Add (new Telerik.Web.UI.RadComboBoxItem (cityName, cityName));

                }

//                 eventArgs.EndOfItems = (pageTable.Rows.Count < itemsPerRequest);

                eventArgs.EndOfItems = true;

                eventArgs.Message = (eventArgs.EndOfItems) ? "End of List" : "More Available";

            }

            EditorForm = editorForm;

            return;

        }

        public void FormControlAddressControl_StateOnTextChanged (Object sender, EventArgs eventArgs) {
            
            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadComboBox controlComboBox = (Telerik.Web.UI.RadComboBox) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Address addressControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[2]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) {

                addressControl = (Mercury.Client.Core.Forms.Controls.Address) selectedControl;

            }

            if (addressControl != null) {

                if (!addressControl.ReadOnly) {

                    if (addressControl.State != controlComboBox.Text) {

                        addressControl.City = String.Empty;

                        Telerik.Web.UI.RadComboBox controlComboBoxCity = (Telerik.Web.UI.RadComboBox) controlComboBox.Parent.FindControl (controlName.Replace ("State", "City"));

                        if (controlComboBoxCity != null) { controlComboBoxCity.Text = String.Empty; }

                        addressControl.State = controlComboBox.Text;

                        addressControl.EntityAddressId = 0;

                    }

                }

            }

            EditorForm = editorForm;

            return;

        }

        public void FormControlAddressControl_ZipCodeOnTextChanged (Object sender, EventArgs eventArgs) {

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadMaskedTextBox controlZipCode = (Telerik.Web.UI.RadMaskedTextBox) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Address addressControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[2]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Address) {

                addressControl = (Mercury.Client.Core.Forms.Controls.Address) selectedControl;

            }

            if (addressControl != null) {

                if (!addressControl.ReadOnly) {

                    if (addressControl.ZipCode != controlZipCode.Text) {

                        addressControl.ZipCode = controlZipCode.Text;

                        addressControl.City = MercuryApplication.CityReferenceByZipCode (addressControl.ZipCode);

                        addressControl.State = MercuryApplication.StateReferenceByZipCode (addressControl.ZipCode);

                        Telerik.Web.UI.RadComboBox controlComboBoxCity = (Telerik.Web.UI.RadComboBox) controlZipCode.Parent.FindControl (controlName.Replace ("ZipCode", "City"));

                        Telerik.Web.UI.RadComboBox controlComboBoxState = (Telerik.Web.UI.RadComboBox) controlZipCode.Parent.FindControl (controlName.Replace ("ZipCode", "State"));

                        if (controlComboBoxCity != null) { controlComboBoxCity.Text = addressControl.City; }

                        if (controlComboBoxState != null) { controlComboBoxState.SelectedValue = addressControl.State; }

                        addressControl.EntityAddressId = 0;

                    }

                }

            }

            EditorForm = editorForm;

            return;

        }

        public void FormControlServiceControl_ServiceDateOnTextChanged (Object sender, EventArgs eventArgs) {

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadDateInput inputServiceDate = (Telerik.Web.UI.RadDateInput) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Service serviceControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[2]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Service) {

                serviceControl = (Mercury.Client.Core.Forms.Controls.Service) selectedControl;

            }

            if (serviceControl != null) {

                serviceControl.ServiceDate = inputServiceDate.SelectedDate;

            }

            EditorForm = editorForm;



            return;

        }

        public void FormControlMetricControl_MetricDateOnTextChanged (Object sender, EventArgs eventArgs) {

            Mercury.Client.Core.Forms.Form editorForm = EditorForm;

            Telerik.Web.UI.RadDateInput inputMetricDate = (Telerik.Web.UI.RadDateInput) sender;

            String controlName = ((System.Web.UI.Control) sender).ID;

            Mercury.Client.Core.Forms.Control selectedControl;

            Mercury.Client.Core.Forms.Controls.Metric metricControl = null;


            if (MercuryApplication == null) { return; }

            if (editorForm == null) { return; }

            selectedControl = editorForm.FindControlById (new Guid (controlName.Split ('_')[1]));

            if (selectedControl is Mercury.Client.Core.Forms.Controls.Metric) {

                metricControl = (Mercury.Client.Core.Forms.Controls.Metric) selectedControl;

            }

            if (metricControl != null) {

                metricControl.MetricDate = inputMetricDate.SelectedDate;

            }

            EditorForm = editorForm;



            return;

        }

        #endregion


        #region Form Paging Events

        public void FormPagerButton_OnClick (Object sender, EventArgs e) {

            LinkButton pagerButton = (LinkButton)sender;

            switch (pagerButton.ID) {

                case "FormPagerFirst": CurrentPage = 1; break;

                case "FormPagerPrevious": CurrentPage = CurrentPage - 1; break;

                case "FormPagerNext": CurrentPage = CurrentPage + 1; break;

                case "FormPagerLast": CurrentPage = EditorForm.PageCountVisible; break;

                default:

                    CurrentPage = Convert.ToInt32 (pagerButton.ID.Replace ("FormPagerPage", String.Empty));

                    break;

            }


            if (EditorForm != null) { RenderForm (true); }


            String scrollToTopScript = "setTimeout (\"document.getElementById ('" + Page.FindControl ("WorkflowContentPanel").ClientID + "').scrollTop = 0;\", 10);";

            TelerikAjaxManager.ResponseScripts.Add (scrollToTopScript);

            return;

        }


        #endregion 

    }

}