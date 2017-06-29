using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.UserInteractions {

    public partial class ContactEntity : System.Web.UI.UserControl {
        
        #region Private Properties

        private const String ResponseScriptPaint = "setTimeout (\"UserInteractionContactEntity_OnPaint ();\", 100);";

        private Server.Application.WorkflowUserInteractionRequestContactEntity contactEntityRequest;

        #endregion


        #region State Properties

        public Mercury.Web.Application.Workflow.Workflow WorkflowPage { get { return (Mercury.Web.Application.Workflow.Workflow) Page; } }

        public String SessionCachePrefix { get { return WorkflowPage.SessionCachePrefix + this.GetType ().ToString (); } }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        public Client.Core.Entity.Entity Entity {

            get { return (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"]; }

            set {

                Client.Core.Entity.Entity entity = (Client.Core.Entity.Entity) Session[SessionCachePrefix + "Entity"];

                if (entity != value) {

                    entity = value;

                    Session[SessionCachePrefix + "Entity"] = entity;
                    
                    contactEntityRequest = (Mercury.Server.Application.WorkflowUserInteractionRequestContactEntity) WorkflowPage.UserInteractionRequest;

                    if ((entity != null) && (contactEntityRequest != null)) {


                        // SETUP DEMOGRAPHICS CONTROL BASED ON ENTITY TYPE

                        switch (Entity.EntityType) {

                            case Mercury.Server.Application.EntityType.Member:

                                MemberDemographicsControl.Visible = true;

                                MemberDemographicsControl.InitializeMemberDemographicsByEntityId (Entity.Id);

                                MemberDemographicsControl.AllowUserInteraction = false;


                                // MemberWorkHistoryControl.Member = MercuryApplication.MemberGetByEntityId (Entity.Id, true);

                                MemberWorkHistoryControl.AllowUserInteraction = false;     

                                break;

                            case Mercury.Server.Application.EntityType.Provider:

                                ProviderDemographicsControl.Visible = true;

                                ProviderDemographicsControl.InitializeProviderDemographicsByEntityId (Entity.Id);

                                ProviderDemographicsControl.AllowUserInteraction = false;

                                break;

                        }
                        

                        EntityContactControl.Entity = Entity;

                        EntityContactControl.RelatedEntity = (contactEntityRequest.RelatedEntity != null) ? new Mercury.Client.Core.Entity.Entity (MercuryApplication, contactEntityRequest.RelatedEntity) : null;


                        EntityContactControl.AllowEditRelatedEntity = true;

                        EntityContactControl.AllowEditContactDateTime = contactEntityRequest.AllowEditContactDateTime;

                        EntityContactControl.AllowEditRegarding = contactEntityRequest.AllowEditRegarding;

                        EntityContactControl.AllowCancel = contactEntityRequest.AllowCancel;

                        EntityContactControl.RegardingMessage = contactEntityRequest.Regarding;

                        EntityContactControl.IntroductionScript = contactEntityRequest.IntroductionScript;


                        // EntityContactHistoryControl.Entity = Entity;

                        EntityContactHistoryControl.AllowUserInteraction = false;


                        // EntityNoteHistoryControl.Entity = Entity;

                        EntityNoteHistoryControl.AllowUserInteraction = false;

                   

                    }

                }

            }

        }

        public String ResponseScript { get { return (String) Session[SessionCachePrefix + "ResponseScript"]; } set { Session[SessionCachePrefix + "ResponseScript"] = value; } }

        public Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager) Page.FindControl ("TelerikAjaxManager"); } }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {
            
            if (MercuryApplication == null) { return; }



            MemberDemographicsControl.InstanceId = SessionCachePrefix + "MemberDemographicsControl";

            ProviderDemographicsControl.InstanceId = SessionCachePrefix + "ProviderDemographicsControl";

            EntityContactHistoryControl.InstanceId = SessionCachePrefix + "EntityContactHistoryControl";

            EntityNoteHistoryControl.InstanceId = SessionCachePrefix + "EntityNoteHistoryControl";

            MemberWorkHistoryControl.InstanceId = SessionCachePrefix + "MemberWorkHistoryControl";

            
            contactEntityRequest = (Server.Application.WorkflowUserInteractionRequestContactEntity) WorkflowPage.UserInteractionRequest;

            Entity = MercuryApplication.EntityGet (contactEntityRequest.Entity.Id, true);


            if (WorkflowPage != null) { WorkflowPage.WorkflowAjaxManager.ResponseScripts.Add ("setTimeout (\"Workflow_OnPaint ();\", 100);setTimeout (\"UserInteractionContactEntity_OnPaint ();\", 200);"); }
            
            return;

        }

        #endregion 


        #region Information Pane Events

        protected void InformationPaneContactHistoryExpanded_OnClick (Object sender, EventArgs e) {

            EntityContactHistoryControl.Entity = Entity;

            String expandPaneScript = String.Empty;

            expandPaneScript += "var slidingZone = $find ('" + EntityInfoSlidingZone.ClientID + "');";

            expandPaneScript += "slidingZone.ExpandPane ('" + EntityInfoContactHistory.ClientID + "');";

            if (TelerikAjaxManager != null) {

                TelerikAjaxManager.ResponseScripts.Add (expandPaneScript);

            }

            return;

        }

        protected void InformationPaneNoteHistoryExpanded_OnClick (Object sender, EventArgs e) {

            EntityNoteHistoryControl.Entity = Entity;

            String expandPaneScript = String.Empty;

            expandPaneScript += "var slidingZone = $find ('" + EntityInfoSlidingZone.ClientID + "');";

            expandPaneScript += "slidingZone.ExpandPane ('" + EntityNoteHistorySlidingPane.ClientID + "');";

            if (TelerikAjaxManager != null) {

                TelerikAjaxManager.ResponseScripts.Add (expandPaneScript);

            }

            return;

        }
        
        protected void InformationPaneWorkHistoryExpanded_OnClick (Object sender, EventArgs e) {

            if (Entity.EntityType != Mercury.Server.Application.EntityType.Member) { return; }

            MemberWorkHistoryControl.Member = MercuryApplication.MemberGetByEntityId (Entity.Id, true);

            String expandPaneScript = String.Empty;

            expandPaneScript += "var slidingZone = $find ('" + EntityInfoSlidingZone.ClientID + "');";

            expandPaneScript += "slidingZone.ExpandPane ('" + EntityInfoMemberWorkHistory.ClientID + "');";

            if (TelerikAjaxManager != null) {

                TelerikAjaxManager.ResponseScripts.Add (expandPaneScript);

            }

            return;

        }

        #endregion 


        #region Control Events

        protected void EntityContactControl_OnContact (Object sender, Web.Application.Controls.ContactEntityEventArgs eventArgs) {

            Server.Application.WorkflowUserInteractionResponseContactEntity contactResponse = new Server.Application.WorkflowUserInteractionResponseContactEntity ();

            contactResponse.InteractionType = Mercury.Server.Application.UserInteractionType.ContactEntity;


            if (!eventArgs.Cancel) {

                contactResponse.EntityContact = (Mercury.Server.Application.EntityContact)eventArgs.EntityContact.ToServerObject ();

                // contactResponse.EntityType = eventArgs.Entity.EntityType;

                // contactResponse.EntityId = eventArgs.EntityContact.EntityId;

                //contactResponse.Entity = (Mercury.Server.Application.Entity)eventArgs.Entity.ToServerObject ();

                //contactResponse.RelatedEntity = (eventArgs.RelatedEntity != null) ? (Mercury.Server.Application.Entity)eventArgs.RelatedEntity.ToServerObject () : null;

                //contactResponse.EntityContactInformationId = eventArgs.EntityContact.EntityContactInformationId;

                //contactResponse.ContactType = eventArgs.EntityContact.ContactType;

                //contactResponse.ContactDate = eventArgs.EntityContact.ContactDate;

                //contactResponse.Direction = eventArgs.EntityContact.Direction;

                //contactResponse.Regarding = eventArgs.EntityContact.Regarding;

                //contactResponse.Remarks = eventArgs.EntityContact.Remarks;

                //contactResponse.Successful = eventArgs.EntityContact.Successful && (!eventArgs.Cancel);

                //contactResponse.ContactOutcome = eventArgs.EntityContact.ContactOutcome;

            }

            contactResponse.Cancel = eventArgs.Cancel;




            WorkflowPage.UserInteractionResponse = contactResponse;


            if (!String.IsNullOrEmpty (ResponseScript)) {

                Telerik.Web.UI.RadAjaxManager ajaxManager = (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager");

                if (ajaxManager != null) { ajaxManager.ResponseScripts.Add (ResponseScript); }

            }

            return;

        }

        #endregion 

    }

}