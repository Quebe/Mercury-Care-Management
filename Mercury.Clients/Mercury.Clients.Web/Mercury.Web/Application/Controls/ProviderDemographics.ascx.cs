using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Controls {

    public partial class ProviderDemographics : System.Web.UI.UserControl {

        #region Private Properties

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        public Client.Core.Provider.Provider Provider {

            get { return (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"]; }

            set {

                Client.Core.Provider.Provider provider = (Client.Core.Provider.Provider)Session[SessionCachePrefix + "Provider"];

                if (provider != value) {

                    Session[SessionCachePrefix + "Provider"] = value;

                    InitializeProviderDemographics (provider);

                }

            }

        }

        #endregion

        
        #region Public Properties

        public String InstanceId { 
            
            get { return UserControlInstanceId.Text; } 
            
            set { 
                
                UserControlInstanceId.Text = value;

                EntityAddressHistoryControl.InstanceId = value + "EntityAddressHistoryControl";

                EntityContactInformationHistoryControl.InstanceId = value + "EntityContactInformationHistoryControl";
            
            } 
        
        }

        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean) Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                Session[SessionCachePrefix + "AllowUserInteraction"] = value;

                EntityAddressHistoryControl.AllowUserInteraction = value;

                EntityContactInformationHistoryControl.AllowUserInteraction = value;

            }

        }

        public Boolean AllowAddressAction { get { return ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderAddressManage)) && (AllowUserInteraction)); } }

        public Boolean AllowContactInformationAction { get { return ((MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProviderContactInformationManage)) && (AllowUserInteraction)); } }

        #endregion


        #region Page Events

        public void Page_Load (Object sender, EventArgs e) {

            EntityAddressHistoryControl.InstanceId = InstanceId + "EntityAddressHistoryControl";

            EntityContactInformationHistoryControl.InstanceId = InstanceId + "EntityContactInformationHistoryControl";

            return;

        }

        #endregion


        #region Initializations

        public void InitializeProviderDemographics (Mercury.Client.Core.Provider.Provider provider) {

            if (provider == null) { return; }

            //             ProviderDemographicHeaderLabel.Text = provider.Entity.Name + " (" + provider.CurrentAge + " | " + provider.GenderDescription + ") ";

            ProviderDemographicUniqueId.Text = provider.Entity.UniqueId;


            String anchorText = "<a href=\"/Application/ProviderProfile/ProviderProfile.aspx?ProviderId=" + provider.Id.ToString () + "\" alt=\"Provider Profile\" target=\"blank\">" + provider.Entity.Name + "</a>";

            ProviderDemographicName.Text = anchorText;

            ProviderDemographicBirthDate.Text = provider.BirthDateDescription;

            ProviderDemographicDeathDate.Text = provider.DeathDateDescription;

            ProviderDemographicCurrentAge.Text = (provider.CurrentAge != -1) ? provider.CurrentAge.ToString () : "&nbsp";

            ProviderDemographicGender.Text = provider.GenderDescription;

            ProviderDemographicEthnicity.Text = provider.EthnicityDescription;


            if (provider.Entity != null) {

                ProviderDemographicFederalId.Text = provider.Entity.FederalTaxId;
                
                EntityAddressHistoryControl.Entity = provider.Entity;

                EntityContactInformationHistoryControl.Entity = provider.Entity;

            }


            ProviderDemographicNpi.Text = provider.NationalProviderId;

            return;

        }

        public void InitializeProviderDemographicsByEntityId (Int64 entityId) {

            InitializeProviderDemographics (MercuryApplication.ProviderGetByEntityId (entityId, true));

            return;

        }
        
        #endregion 

    }
}