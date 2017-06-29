using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Forms.FormViewer {


    public partial class FormViewer : System.Web.UI.Page {


        #region Private Properties

        private FormRenderEngine renderEngine;

        #endregion 


        #region Session Properties

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

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            Client.Core.Forms.Form entityForm = null;

            renderEngine = new FormRenderEngine (this, MercuryApplication, Session, SessionCachePrefix);

            if (!IsPostBack) {

                Int64 entityFormId = 0;

                if (Request.QueryString["EntityFormId"] != null) {

                    if (Int64.TryParse ((String) Request.QueryString["EntityFormId"], out entityFormId)) {

                        entityForm = MercuryApplication.FormGetByEntityFormId (entityFormId);

                        if (entityForm == null) { entityFormId = 0; }

                    }

                }

                else if (Request.QueryString["EntityFormSessionKey"] != null) {

                    entityForm = (Client.Core.Forms.Form)Session[((String)Request.QueryString["EntityFormSessionKey"])];

                    if (Request.QueryString["ForPrint"] != null) {

                        Boolean forPrint = Convert.ToBoolean ((String)Request.QueryString["ForPrint"]);

                        if (forPrint) {

                            TelerikAjaxManager.ResponseScripts.Add ("window.print (); window.close ();");

                        }

                    }

                }

                if (entityForm == null) { Server.Transfer ("/PermissionDenied.aspx"); return; }

                Page.Title = entityForm.Name;

                RenderForm (entityForm);

            }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion 

        
        protected void RenderForm (Client.Core.Forms.Form viewerForm) {

            if (viewerForm == null) { return; }

            if (MercuryApplication == null) { return; }

            if (renderEngine.IsFormRendered) { return; }


            FormContent.Controls.Clear ();

            FormContent.Controls.Add (renderEngine.RenderForm (viewerForm));

            return;

        }

    }

}
