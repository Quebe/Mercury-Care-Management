using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models {

    public class ApplicationModel {

        #region Private Properties

        private Guid pageInstanceId = Guid.NewGuid ();

        private String exceptionMessage = String.Empty;

        private String informationMessage = String.Empty;

        private String responseScript = String.Empty;

        #endregion 


        #region Public Properties

        public Guid PageInstanceId { get { return pageInstanceId; } }

        /// <summary>
        /// Determines if the Controller should reload State data from in memory (ASP SESSION STATE) or from form fields on the POST.
        /// </summary>
        public virtual Boolean StoreModelStateInMemory { get; set; }


        public Mercury.Client.Application MercuryApplication {

            get {

                // RETREIVE PREVIOUS APPLICATION OBJECT FROM SESSION STORAGE                 

                Mercury.Client.Application mercuryApplication = (Mercury.Client.Application) HttpContext.Current.Session["Mercury.Client.Application"];

                if (mercuryApplication == null) { HttpContext.Current.Response.Redirect ("/PermissionDenied"); }

                return mercuryApplication;

            }

            set { HttpContext.Current.Session["Mercury.Client.Application"] = value; }

        }

        public Boolean HasException { get { return !(String.IsNullOrWhiteSpace (exceptionMessage)); } }

        public String ExceptionMessage { get { return exceptionMessage; } }

        public Boolean HasInformationMessage { get { return !(String.IsNullOrWhiteSpace (informationMessage)); } }

        public String InformationMessage { get { return informationMessage; } }

        public String ResponseScript { get { return responseScript; } set { responseScript = value; } }

        #endregion 


        #region Constructors

        public ApplicationModel () { /* DO NOTHING */ }

        public ApplicationModel (System.Collections.Specialized.NameValueCollection form) {

            Guid.TryParse (form["PageInstanceId"], out pageInstanceId);

            exceptionMessage = form["ExceptionMessage"];

            informationMessage = form["InformationMessage"];

            return;

        }

        #endregion 


        #region Public Methods

        public virtual void UpdateValues (System.Collections.Specialized.NameValueCollection form) { /* DO NOTHING */ }

        public virtual void RaiseEvent (String eventTarget, String eventArguments) { /* DO NOTHING */ } 

        public void SetException (String forExceptionMessage) {

            exceptionMessage = forExceptionMessage;

            return;

        }

        public void SetInformationMessage (String forInformationMessage) {

            informationMessage = forInformationMessage;

            return;

        }
      
        #endregion 

    }

}