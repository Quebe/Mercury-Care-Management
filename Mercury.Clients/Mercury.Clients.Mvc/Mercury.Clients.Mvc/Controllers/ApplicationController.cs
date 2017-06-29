using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers {

    public class ApplicationController : Controller {

        #region Private Properties

        /// <summary>
        /// Determines if the Controller should reload State data from in memory (ASP SESSION STATE) or from form fields on the POST.
        /// </summary>
        private Boolean? storeModelStateInMemory = null;

        #endregion 


        #region Public Properties

        /// <summary>
        /// Determines if the Controller should reload State data from in memory (ASP SESSION STATE) or from form fields on the POST.
        /// </summary>
        public Boolean StoreModelStateInMemory {

            get {

                if (storeModelStateInMemory.HasValue) { return storeModelStateInMemory.Value; }

                storeModelStateInMemory = false;

                try {

                    // TRY TO READ FROM SESSION STATE FIRST BEFORE DISK

                    if (Session["ApplicationModel.StoreModelStateInMemory"] != null) {

                        storeModelStateInMemory = (Boolean)Session["ApplicationModel.StoreModelStateInMemory"];

                    }

                    else { // READ FROM DISK

                        String configurationValueString = String.Empty;

                        String[] configurationValueStrings = System.Configuration.ConfigurationManager.AppSettings.GetValues ("StoreModelStateInMemory");

                        if (configurationValueStrings != null) {

                            if (configurationValueStrings.Length >= 1) {

                                configurationValueString = configurationValueStrings[0];
                            }

                        }

                        Boolean configurationValue;

                        if (Boolean.TryParse (configurationValueString, out configurationValue)) {

                            StoreModelStateInMemory = configurationValue; // THIS WILL CAUSE IT STORE IN LOCAL VARIABLE AND PUSH TO SESSION STATE

                        }

                    }

                }

                catch { /* DO NOTHING */ }

                return storeModelStateInMemory.Value;

            }

            set {

                storeModelStateInMemory = value;

                Session["ApplicationModel.StoreModelStateInMemory"] = storeModelStateInMemory; // STORE IN STATE FOR OTHER POSTBACKS

            }

        }

        #endregion 

        #region General Access Views

        public ActionResult MemberInformationSummary (Int64 memberId) {

            Models.ApplicationModel applicationModel = new Models.ApplicationModel ();

            Client.Core.Member.Member member = applicationModel.MercuryApplication.MemberGet (memberId, true);

            return View ("~/Views/Controls/MemberInformationSummary.cshtml", member);

        }

        #endregion

    }

}