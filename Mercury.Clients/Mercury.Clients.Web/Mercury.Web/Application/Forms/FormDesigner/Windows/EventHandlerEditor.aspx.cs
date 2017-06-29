using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Forms.FormDesigner.Windows {

    public partial class EventHandlerEditor : System.Web.UI.Page {

        #region Private Session Cache

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Mercury.Client.Core.Forms.Form DesignerForm {

            get {

                Mercury.Client.Core.Forms.Form designerForm = (Mercury.Client.Core.Forms.Form) Session[SessionCachePrefix + "DesignerForm"];

                return designerForm;

            }

            set { Session[SessionCachePrefix + "DesignerForm"] = value; }

        }

        private Mercury.Client.Core.Forms.Control FormControl {

            get {

                if (DesignerForm == null) { return null; }

                return DesignerForm.FindControlById (new Guid (DocumentControlId.Text));

            }

        }

        private Mercury.Server.Application.FormControlEventHandler EventHandler {

            get {

                Mercury.Server.Application.FormControlEventHandler eventHandler = null;

                if (FormControl != null) {

                    eventHandler = FormControl.GetEventHandler (EventName.Text);

                    if ((eventHandler == null) && (FormControl.Events.Contains (EventName.Text))) {

                        eventHandler = new Mercury.Server.Application.FormControlEventHandler ();

                        eventHandler.EventName = EventName.Text;

                        eventHandler.MethodSource = String.Empty;

                        eventHandler.ExecuteClientSide = true;

                        eventHandler.SmartEvent = true;


                        String controlType = FormControl.ControlType.ToString ();

                        String methodSource = "    Core.Forms.Controls." + controlType + " ";

                        methodSource = methodSource + controlType.Substring (0, 1).ToLower () + controlType.Substring (1, controlType.Length - 1) + "Control = ";

                        methodSource = methodSource + "(Core.Forms.Controls." + controlType + ") sender;";

                        eventHandler.MethodSource = methodSource;

                    }

                }

                return eventHandler;

            }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (!IsPostBack) {

                if (!String.IsNullOrEmpty ((String) Request.QueryString["PageInstanceId"])) {

                    PageInstanceId.Text = (String) Request.QueryString["PageInstanceId"];

                }

                if (!String.IsNullOrEmpty ((String) Request.QueryString["ControlId"])) { 

                    DocumentControlId.Text = (String) Request.QueryString ["ControlId"];

                }

                if (!String.IsNullOrEmpty ((String) Request.QueryString["EventName"])) {

                    EventName.Text = (String) Request.QueryString["EventName"];

                }

            }

            if (DesignerForm == null) { Response.Redirect ("/SessionExpired.aspx", true); }

            if (FormControl == null) { Response.Redirect ("/SessionExpired.aspx", true); }

            if (EventHandler == null) { Response.Redirect ("/SessionExpired.aspx", true); }

            Page.Title = FormControl.Name;

            EventNameLiteral.Text = EventName.Text;

            if (!IsPostBack) {

                String content = EventHandler.MethodSource.Replace ("\r\n", "<br />");

                content = content.Replace ("\n\r", "<br />");

                ScriptEditor.Content = content;

                ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[0]).Checked = EventHandler.ExecuteClientSide;

                ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[2]).Checked = EventHandler.SmartEvent;
            
            }

            ExecuteOnClient.Style.Add ("display", ((EventHandler.ExecuteClientSide) ? "block" : "none"));

            ExecuteOnServer.Style.Add ("display", ((!EventHandler.ExecuteClientSide) ? "block" : "none"));

            return;

        }

        #endregion 


        #region Support Methods 

        private String FormatSourceCode (String sourceCode) {

            String formattedCode = sourceCode;


            formattedCode = formattedCode.Replace ("\n\r", "\r\n");

            formattedCode = formattedCode.Replace (" \r ", " \r\n ");

            formattedCode = formattedCode.Replace ("\r\n", "\n");

            formattedCode = formattedCode.Replace ("\n", "\r\n");


            return formattedCode;

        }

        #endregion 


        #region Editor Toolbar Events

        protected void EditorToolbar_OnButtonClick (Object sender, Telerik.Web.UI.RadToolBarEventArgs eventArgs) {

            switch (eventArgs.Item.Text) {
                   
                case "Compile":

                    Mercury.Client.Core.Forms.Form designerForm = DesignerForm;

                    Mercury.Client.Core.Forms.Control eventControl = designerForm.FindControlById (new Guid (DocumentControlId.Text));

                    Mercury.Server.Application.FormControlEventHandler eventHandler = eventControl.GetEventHandler (EventName.Text);

                    if (eventHandler == null) {

                        eventHandler = new Mercury.Server.Application.FormControlEventHandler ();

                        eventControl.EventHandlers.Add (eventHandler);

                    }

                    eventHandler.EventName = EventName.Text;

                    eventHandler.ExecuteClientSide = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[0]).Checked;

                    eventHandler.SmartEvent = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[2]).Checked;


                    eventHandler.MethodSource = FormatSourceCode (ScriptEditor.Text);


                    List<Mercury.Server.Application.FormCompileMessage> compileMessages;

                    compileMessages = eventControl.EventHandler_Compile (EventName.Text);

                    EditorToolbar.Items[5].Text = "Errors (" + compileMessages.Count.ToString () + ")";

                    System.Data.DataTable errorListTable = new System.Data.DataTable ();

                    errorListTable.Columns.Add ("ErrorLine");

                    errorListTable.Columns.Add ("ErrorColumn");

                    errorListTable.Columns.Add ("ErrorText");

                    foreach (Mercury.Server.Application.FormCompileMessage currentMessage in compileMessages) {

                        errorListTable.Rows.Add (currentMessage.Line, currentMessage.Column, currentMessage.Description);

                    }

                    ErrorListGrid.DataSource = errorListTable;

                    ErrorListGrid.DataBind ();

                    break;

                case "Save":

                    Save (sender, new EventArgs ());

                    break;

                case "Save and Close":

                    Save (sender, new EventArgs ());

                    TelerikAjaxManager.ResponseScripts.Add ("CloseWindow ();");

                    break;

            }

            return;

        }

        #endregion 


        protected void Save (Object sender, EventArgs eventArgs) {

            String sourceCode = String.Empty;

            Mercury.Client.Core.Forms.Form designerForm = DesignerForm;

            Mercury.Client.Core.Forms.Control eventControl = designerForm.FindControlById (new Guid (DocumentControlId.Text));

            if (eventControl != null) {

                sourceCode = FormatSourceCode (ScriptEditor.Text);

                Mercury.Server.Application.FormControlEventHandler eventHandler = eventControl.GetEventHandler (EventName.Text);

                if (eventHandler != null) {

                    if (ScriptEditor.Content == String.Empty) {

                        eventControl.EventHandlers.Remove (eventHandler);

                    }

                    else {

                        eventHandler.MethodSource = sourceCode;

                        eventHandler.ExecuteClientSide = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[0]).Checked;

                        eventHandler.SmartEvent = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[2]).Checked;

                    }

                }

                else {

                    eventHandler = new Mercury.Server.Application.FormControlEventHandler ();

                    eventHandler.EventName = EventName.Text;

                    eventHandler.MethodSource = sourceCode;

                    eventHandler.ExecuteClientSide = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[0]).Checked;

                    eventHandler.SmartEvent = ((Telerik.Web.UI.RadToolBarButton) EditorToolbar.Items[2]).Checked;

                    eventControl.EventHandlers.Add (eventHandler);

                }

            } // if (eventControl != null) {

            DesignerForm = designerForm;


            return;

        }

    }

}
