using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Common {

    public partial class Image : System.Web.UI.Page {

        #region Private Properties

        private Boolean isPageUnloading = false;

        #endregion


        #region Public State Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return "Mercury" + PageInstanceId.Text;

            }

        }

        public Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if ((application == null) && (!isPageUnloading)) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion 
    
    
        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (!Page.IsPostBack) {

                InitializeImage ();

                //String ResponseScriptWorkflowStart = "setTimeout (\"document.getElementById ('" + ImageStart.ClientID + "').click ();\", 125);";

                //TelerikAjaxManager.ResponseScripts.Add (ResponseScriptWorkflowStart);

            }

            else { LoadingProgressImage.Visible = false; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            isPageUnloading = true;

            if (MercuryApplication != null) { MercuryApplication.ApplicationClientClose (); }

            return;

        }

        #endregion


        #region Initialization
        
        protected void InitializeImage () {

            String objectType = String.Empty;

            objectType = Request.QueryString["ObjectType"];


            switch (objectType) {

                case "EntityCorrespondence":

                    break;

                default:

                    SetException (new ApplicationException ("Unknown or unsupported Object Type: " + objectType));

                    break;

            }

        
            return;

        }

        protected void SetException (Exception exception) {

            ExceptionMessageRow.Style.Clear ();

            ExceptionMessage.Text = exception.Message;


            if (exception.InnerException != null) {

                InnerException.Text = exception.InnerException.Message + "< br/>";

                if (!String.IsNullOrWhiteSpace (exception.InnerException.StackTrace)) {

                    InnerException.Text += exception.InnerException.StackTrace.Replace ("\r\n", "< br/>");

                }

            }

            return;


        }

        #endregion 


        #region Events

        protected void ImageStart_OnClick (Object sender, EventArgs e) {

            Server.Application.ImageResponse imageResponse = null;


            String objectType = String.Empty;

            Int64 objectId = 0;

            Boolean render = false;



            objectType = Request.QueryString["ObjectType"];

            objectId = Convert.ToInt64 (Request.QueryString["ObjectId"]);

            render = Convert.ToBoolean (Request.QueryString["Render"]);


            try {

                switch (objectType) {

                    case "EntityCorrespondence":

                        imageResponse = MercuryApplication.EntityCorrespondenceImageGet (objectId, render);

                        HandleResponse (imageResponse);



                        break;

                    default:

                        SetException (new ApplicationException ("Unknown or unsupported Object Type: " + objectType));

                        break;

                }

            }

            catch (Exception exception) {

                SetException (exception);

            }

            return;

        }

        private void HandleResponse (Server.Application.ImageResponse imageResponse) {

            String objectType = String.Empty;

            Int64 objectId = 0;

            Boolean render = false;



            objectType = Request.QueryString["ObjectType"];

            objectId = Convert.ToInt64 (Request.QueryString["ObjectId"]);

            render = Convert.ToBoolean (Request.QueryString["Render"]);


            LoadingProgressImage.Style.Add ("display", "none");


            if (imageResponse == null) {

                SetException (new ApplicationException ("Image not found or unable to generate for " + objectType + " [" + objectId + "].", MercuryApplication.LastException));

                return;

            }

            if ((imageResponse != null) ? ((!String.IsNullOrWhiteSpace (imageResponse.ImageBase64)) && (!imageResponse.HasException)) : false) {

                System.IO.MemoryStream image = new System.IO.MemoryStream (Convert.FromBase64String (imageResponse.ImageBase64));

                Response.ContentType = imageResponse.MimeType;

                Response.AddHeader ("content-disposition", "inline; filename=" + imageResponse.ImageName);

                Response.BinaryWrite (image.ToArray ());

                // Response.End ();

            }

            else {

                SetException (new ApplicationException ("Image not found or unable to generate for " + objectType + " [" + objectId + "].", MercuryApplication.LastException));

            }


        }

        #endregion 

    }

}