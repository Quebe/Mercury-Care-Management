using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.UserInteractions {

    public partial class SendCorrespondence : System.Web.UI.UserControl {


        #region Private Properties

        private const String ResponseScriptPaint = "setTimeout (\"UserInteractionSendCorrespondence_OnPaint ();\", 100);";

        private Server.Application.WorkflowUserInteractionRequestSendCorrespondence sendCorrespondenceRequest;

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

        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity)Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    entity = value;

                    Session[SessionCachePrefix + "Entity"] = entity;

                    sendCorrespondenceRequest = (Mercury.Server.Application.WorkflowUserInteractionRequestSendCorrespondence)WorkflowPage.UserInteractionRequest;

                    if ((entity != null) && (sendCorrespondenceRequest != null)) {

                        EntitySendCorrespondenceControl.Entity = Entity;

                        
                        // SETUP DEMOGRAPHICS CONTROL BASED ON ENTITY TYPE

                        switch (Entity.EntityType) {

                            case Mercury.Server.Application.EntityType.Member:

                                MemberDemographicsControl.Visible = true;

                                MemberDemographicsControl.InitializeMemberDemographicsByEntityId (Entity.Id);

                                MemberDemographicsControl.AllowUserInteraction = false;


                                break;

                            case Mercury.Server.Application.EntityType.Provider:

                                ProviderDemographicsControl.Visible = true;

                                ProviderDemographicsControl.InitializeProviderDemographicsByEntityId (Entity.Id);

                                ProviderDemographicsControl.AllowUserInteraction = false;


                                break;

                        }
                        


                        EntitySendCorrespondenceControl.CorrespondenceId = sendCorrespondenceRequest.CorrespondenceId;

                        EntitySendCorrespondenceControl.Attention = sendCorrespondenceRequest.Attention;

                        EntitySendCorrespondenceControl.AllowAlternateAddress = sendCorrespondenceRequest.AllowAlternateAddress;

                        if (sendCorrespondenceRequest.AlternateAddress != null) {

                            EntitySendCorrespondenceControl.AlternateAddress = new Mercury.Client.Core.Entity.EntityAddress (MercuryApplication, sendCorrespondenceRequest.AlternateAddress);

                        }

                        EntitySendCorrespondenceControl.AllowSendByFacsimile = sendCorrespondenceRequest.AllowSendByFacsimile;

                        EntitySendCorrespondenceControl.AllowSendByEmail = sendCorrespondenceRequest.AllowSendByEmail;

                        EntitySendCorrespondenceControl.AllowSendByInPerson = sendCorrespondenceRequest.AllowSendByInPerson;


                        EntitySendCorrespondenceControl.AllowCancel = sendCorrespondenceRequest.AllowCancel;

                        EntitySendCorrespondenceControl.AllowUserSelection = sendCorrespondenceRequest.AllowUserSelection;

                        EntitySendCorrespondenceControl.AllowHistoricalSendDate = sendCorrespondenceRequest.AllowHistoricalSendDate;

                        EntitySendCorrespondenceControl.AllowFutureSendDate = sendCorrespondenceRequest.AllowFutureSendDate;

                        EntitySendCorrespondenceControl.SendDate = sendCorrespondenceRequest.SendDate;


                        EntitySendCorrespondenceControl.AlternateEmail = sendCorrespondenceRequest.AlternateEmail;

                        EntitySendCorrespondenceControl.AlternateFaxNumber = sendCorrespondenceRequest.AlternateFaxNumber;

                    }

                }

            }

        }

        public String ResponseScript { get { return (String)Session[SessionCachePrefix + "ResponseScript"]; } set { Session[SessionCachePrefix + "ResponseScript"] = value; } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }


            MemberDemographicsControl.InstanceId = SessionCachePrefix + "MemberDemographicsControl";

            ProviderDemographicsControl.InstanceId = SessionCachePrefix + "ProviderDemographicsControl";

            EntityDocumentHistoryControl.InstanceId = SessionCachePrefix + "EntityDocumentHistoryControl";

            EntityNoteHistoryControl.InstanceId = SessionCachePrefix + "EntityNoteHistoryControl";


            sendCorrespondenceRequest = (Server.Application.WorkflowUserInteractionRequestSendCorrespondence)WorkflowPage.UserInteractionRequest;

            Entity = new Client.Core.Entity.Entity (MercuryApplication, sendCorrespondenceRequest.Entity);


            if (Entity != null) {

                EntitySendCorrespondenceControl.Entity = Entity;

                EntitySendCorrespondenceControl.RelatedEntity = (sendCorrespondenceRequest.RelatedEntity != null) ? new Client.Core.Entity.Entity (MercuryApplication, sendCorrespondenceRequest.RelatedEntity) : null;

                EntitySendCorrespondenceControl.CorrespondenceId = sendCorrespondenceRequest.CorrespondenceId;

                EntitySendCorrespondenceControl.Attention = sendCorrespondenceRequest.Attention;

                EntitySendCorrespondenceControl.AllowAlternateAddress = sendCorrespondenceRequest.AllowAlternateAddress;

                if (sendCorrespondenceRequest.AlternateAddress != null) {

                    EntitySendCorrespondenceControl.AlternateAddress = new Mercury.Client.Core.Entity.EntityAddress (MercuryApplication, sendCorrespondenceRequest.AlternateAddress);

                }

                EntitySendCorrespondenceControl.AllowSendByFacsimile = sendCorrespondenceRequest.AllowSendByFacsimile;

                EntitySendCorrespondenceControl.AllowSendByEmail = sendCorrespondenceRequest.AllowSendByEmail;

                EntitySendCorrespondenceControl.AllowSendByInPerson = sendCorrespondenceRequest.AllowSendByInPerson;


                EntitySendCorrespondenceControl.AllowCancel = sendCorrespondenceRequest.AllowCancel;

                EntitySendCorrespondenceControl.AllowUserSelection = sendCorrespondenceRequest.AllowUserSelection;

                EntitySendCorrespondenceControl.AllowHistoricalSendDate = sendCorrespondenceRequest.AllowHistoricalSendDate;

                EntitySendCorrespondenceControl.AllowFutureSendDate = sendCorrespondenceRequest.AllowFutureSendDate;

                EntitySendCorrespondenceControl.SendDate = sendCorrespondenceRequest.SendDate;


                EntitySendCorrespondenceControl.AlternateEmail = sendCorrespondenceRequest.AlternateEmail;

                EntitySendCorrespondenceControl.AlternateFaxNumber = sendCorrespondenceRequest.AlternateFaxNumber;

            }


            if (WorkflowPage != null) { WorkflowPage.WorkflowAjaxManager.ResponseScripts.Add ("setTimeout (\"Workflow_OnPaint ();\", 100);setTimeout (\"UserInteractionSendCorrespondenceEntity_OnPaint ();\", 200);"); }

            return;

        }

        #endregion 
        
        #region On Send Correspondence Event

        protected void EntitySendCorrespondenceControl_OnSendCorrespondence (Object sender, Web.Application.Controls.SendCorrespondenceEntityEventArgs eventArgs) {

            Server.Application.WorkflowUserInteractionResponseSendCorrespondence sendCorrespondenceResponse = new Server.Application.WorkflowUserInteractionResponseSendCorrespondence ();

            sendCorrespondenceResponse.InteractionType = Mercury.Server.Application.UserInteractionType.SendCorrespondence;

            if (!eventArgs.Cancel) {

                sendCorrespondenceResponse.EntityCorrespondence = (Mercury.Server.Application.EntityCorrespondence)eventArgs.EntityCorrespondence.ToServerObject ();

            }

            else { sendCorrespondenceResponse.EntityCorrespondence = null; }


            sendCorrespondenceResponse.Send = (!eventArgs.Cancel);

            sendCorrespondenceResponse.Cancel = eventArgs.Cancel;


            WorkflowPage.UserInteractionResponse = sendCorrespondenceResponse;


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                ajaxManager.ResponseScripts.Add (ResponseScript);

            }

            return;

        }

        #endregion

    }

}