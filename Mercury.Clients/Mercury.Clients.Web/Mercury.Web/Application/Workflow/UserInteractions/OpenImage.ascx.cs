using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.UserInteractions {

    public partial class OpenImage : System.Web.UI.UserControl {
        
        #region Private Properties

        private const String ResponseScriptPaint = "setTimeout (\"UserInteractionOpenImage_OnPaint ();\", 100);";

        private Server.Application.WorkflowUserInteractionRequestOpenImage openImageRequest;

        #endregion


        #region State Properties

        public Mercury.Web.Application.Workflow.Workflow WorkflowPage { get { return (Mercury.Web.Application.Workflow.Workflow)Page; } }

        public String SessionCachePrefix { get { return WorkflowPage.SessionCachePrefix + this.GetType ().ToString (); } }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }
        
        public String ResponseScript { get { return (String)Session[SessionCachePrefix + "ResponseScript"]; } set { Session[SessionCachePrefix + "ResponseScript"] = value; } }

        public Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager"); } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            // HANDLE OPEN IMAGE REQUEST

            openImageRequest = (Mercury.Server.Application.WorkflowUserInteractionRequestOpenImage)WorkflowPage.UserInteractionRequest;

            String imageUrl = "/Application/Common/Image.aspx?ObjectType=" + openImageRequest.ObjectType + "&ObjectId=" + openImageRequest.ObjectId.ToString () + "&Render=" + openImageRequest.Render.ToString ();

            ImageFrame.Attributes.Add ("src", imageUrl);

            OpenImageWindow.HRef = imageUrl;
                
            
            // SET WORKFLOW PAGE OPEN IMAGE RESPONSE

            Mercury.Server.Application.WorkflowUserInteractionResponseOpenImage openImageResponse = new Server.Application.WorkflowUserInteractionResponseOpenImage ();

            openImageResponse.InteractionType = Mercury.Server.Application.UserInteractionType.OpenImage;

            WorkflowPage.UserInteractionResponse = openImageResponse;

            return;

        }

        #endregion



    }

}