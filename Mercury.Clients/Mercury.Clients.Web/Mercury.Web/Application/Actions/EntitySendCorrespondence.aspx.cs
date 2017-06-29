﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Actions {
    public partial class EntitySendCorrespondence : System.Web.UI.Page {

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

                EntityDocumentHistoryControl.Entity = Entity;


                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).Entity = Entity;


                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).CorrespondenceId = 0;

                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).Attention = Entity.Name;

                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).AllowAlternateAddress = true;


                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).AllowCancel = true;

                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).AllowUserSelection = true;

                ((Web.Application.Controls.EntitySendCorrespondence)SendCorrespondenceEntityControl).AllowFutureSendDate = true;

                ((Web.Application.Controls.EntitySendCorrespondence) SendCorrespondenceEntityControl).SendDate = DateTime.Today;

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


        #region On Send Correspondence Event

        protected void SendCorrespondenceEntityControl_OnSendCorrespondence (Object sender, Web.Application.Controls.SendCorrespondenceEntityEventArgs eventArgs) {

            if (eventArgs.Cancel) { Response.Redirect (ReferrerUrl, true); }

            if (eventArgs.EntityCorrespondence != null) {

                Boolean success = MercuryApplication.EntityCorrespondenceSave (eventArgs.EntityCorrespondence);

                if (success) {

                    Response.Redirect (ReferrerUrl, true);

                }

                else {

                    String postScript = ("alert (\"Unable to Save Correspondence. " + ((MercuryApplication.LastException != null) ? MercuryApplication.LastException.Message : String.Empty) + "\");");

                    if ((TelerikAjaxManager != null) && (!String.IsNullOrEmpty (postScript))) { TelerikAjaxManager.ResponseScripts.Add (postScript); }

                }

            }


            return;

        }

        #endregion

    }

}