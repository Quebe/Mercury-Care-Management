using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Actions {

    public partial class EntityAddress : System.Web.UI.Page {

        #region State Properties

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        public Int64 EntityId {

            get {

                Int64 entityId = 0;

                if (Session[SessionCachePrefix + "EntityId"] != null) {

                    entityId = (Int64)Session[SessionCachePrefix + "EntityId"];

                }

                else {

                    Int64.TryParse (Request.QueryString["EntityId"], out entityId);

                    Session[SessionCachePrefix + "EntityId"] = entityId;

                }

                return entityId;

            }

        }

        public Client.Core.Entity.Entity Entity {

            get {

                Client.Core.Entity.Entity entity;

                if (Session[SessionCachePrefix + "Entity"] != null) {

                    entity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"];

                }

                else {

                    entity = MercuryApplication.EntityGet (EntityId, true);

                    Session[SessionCachePrefix + "Entity"] = entity;

                }

                return entity;

            }

        }

        public String ReferrerUrl {

            get {

                String referringUrl = "/Application/Workspace/Workspace.aspx";

                if (Session[SessionCachePrefix + "ReferrerUrl"] != null) {

                    referringUrl = (String)Session[SessionCachePrefix + "ReferrerUrl"];

                }

                return referringUrl;

            }

            set { Session[SessionCachePrefix + "ReferrerUrl"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (!IsPostBack) { if (Request.UrlReferrer != null) { ReferrerUrl = Request.UrlReferrer.ToString (); } }


            if ((!IsPostBack) && (Entity != null)) {

                EntityName.Text = Entity.Name;


                ((Mercury.Web.Application.Controls.EntityAddress)EntityAddressControl).Entity = Entity;


                switch (Entity.EntityType) {

                    case Mercury.Server.Application.EntityType.Member:

                        MemberProfileDemographicsControl.Visible = true;

                        MemberProfileDemographicsControl.InstanceId = this.ID + "MemberProfileDemographicsControl";

                        MemberProfileDemographicsControl.AllowUserInteraction = false;

                        MemberProfileDemographicsControl.InitializeMemberDemographicsByEntityId (Entity.Id);

                        break;

                    case Mercury.Server.Application.EntityType.Provider:

                        ProviderProfileDemographicsControl.Visible = true;

                        ProviderProfileDemographicsControl.InstanceId = this.ID + "ProviderProfileDemographicsControl";

                        ProviderProfileDemographicsControl.AllowUserInteraction = false;

                        ProviderProfileDemographicsControl.InitializeProviderDemographicsByEntityId (Entity.Id);

                        break;

                }

            }


            Page.Header.Controls.Add (new LiteralControl ("<style type=\"text/css\">.dataFieldTitle { float: left; overflow: hidden; white-space: nowrap; text-align: left; margin-left: 5px; margin-right: 5px; }</style>"));

            Page.Header.Controls.Add (new LiteralControl ("<style type=\"text/css\">.dataField { float: left; overflow: hidden; white-space: nowrap; text-align: left; margin-left: 5px; margin-right: 5px; }</style>"));

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Control Events

        protected void EntityAddressControl_OnEntityAddressCompeleted (Object sender, Web.Application.Controls.EntityAddressCompletedEventArgs eventArgs) {

            if (eventArgs.Cancel) { Response.Redirect (ReferrerUrl, true); }

            if (eventArgs.EntityAddress != null) {

                Boolean success = false;

                success = MercuryApplication.EntityAddressSave (eventArgs.EntityAddress);

                if (success) {

                    List<Client.Core.Entity.EntityAddress> addresses = MercuryApplication.EntityAddressesGet (EntityId, false);

                    Response.Redirect (ReferrerUrl, true);

                }

                else {

                    String postScript = ("alert (\"Unable to Save Address. " + ((MercuryApplication.LastException != null) ? MercuryApplication.LastException.Message : String.Empty) + "\");");

                    if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                }

            }

            return;

        }

        #endregion

    }

}