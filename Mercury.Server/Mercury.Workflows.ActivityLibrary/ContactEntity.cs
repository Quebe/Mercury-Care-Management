using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

using Server = Mercury.Server;
using UserInteractions = Mercury.Server.Workflows.UserInteractions;

namespace Mercury.Workflows.ActivityLibrary {

    public partial class ContactEntity : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (ContactEntity));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        protected static readonly DependencyProperty ActivityCompletedProperty = DependencyProperty.Register ("ActivityCompleted", typeof (Boolean), typeof (ContactEntity));

        protected Boolean ActivityCompleted { get { return (Boolean) GetValue (ActivityCompletedProperty); } set { SetValue (ActivityCompletedProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (ContactEntity));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty EntityProperty = DependencyProperty.Register ("Entity", typeof (Server.Core.Entity.Entity), typeof (ContactEntity));

        public Server.Core.Entity.Entity Entity { get { return (Server.Core.Entity.Entity) GetValue (EntityProperty); } set { SetValue (EntityProperty, value); } }

        public static readonly DependencyProperty RelatedEntityProperty = DependencyProperty.Register ("RelatedEntity", typeof (Server.Core.Entity.Entity), typeof (ContactEntity));

        public Server.Core.Entity.Entity RelatedEntity { get { return (Server.Core.Entity.Entity)GetValue (RelatedEntityProperty); } set { SetValue (RelatedEntityProperty, value); } }



        public static readonly DependencyProperty AllowEditRelatedEntityProperty = DependencyProperty.Register ("AllowEditRelatedEntity", typeof (Boolean), typeof (ContactEntity));

        public Boolean AllowEditRelatedEntity { get { return (Boolean)GetValue (AllowEditRelatedEntityProperty); } set { SetValue (AllowEditRelatedEntityProperty, value); } }

        public static readonly DependencyProperty AllowEditContactDateTimeProperty = DependencyProperty.Register ("AllowEditContactDateTime", typeof (Boolean), typeof (ContactEntity));

        public Boolean AllowEditContactDateTime { get { return (Boolean) GetValue (AllowEditContactDateTimeProperty); } set { SetValue (AllowEditContactDateTimeProperty, value); } }

        public static readonly DependencyProperty AllowEditRegardingProperty = DependencyProperty.Register ("AllowEditRegarding", typeof (Boolean), typeof (ContactEntity));

        public Boolean AllowEditRegarding { get { return (Boolean) GetValue (AllowEditRegardingProperty); } set { SetValue (AllowEditRegardingProperty, value); } }

        public static readonly DependencyProperty RegardingMessageProperty = DependencyProperty.Register ("RegardingMessage", typeof (String), typeof (ContactEntity));

        public String RegardingMessage { get { return (String) GetValue (RegardingMessageProperty); } set { SetValue (RegardingMessageProperty, value); } }

        public static readonly DependencyProperty IntroductionScriptProperty = DependencyProperty.Register ("IntroductionScript", typeof (String), typeof (ContactEntity));

        public String IntroductionScript { get { return (String) GetValue (IntroductionScriptProperty); } set { SetValue (IntroductionScriptProperty, value); } }

        public static readonly DependencyProperty AllowCancelProperty = DependencyProperty.Register ("AllowCancel", typeof (Boolean), typeof (ContactEntity));

        public Boolean AllowCancel { get { return (Boolean) GetValue (AllowCancelProperty); } set { SetValue (AllowCancelProperty, value); } }


        public static readonly DependencyProperty ContactAttemptsProperty = DependencyProperty.Register ("ContactAttempts", typeof (Int32), typeof (ContactEntity));

        public Int32 ContactAttempts { get { return (Int32) GetValue (ContactAttemptsProperty); } set { SetValue (ContactAttemptsProperty, value); } }

        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register ("Success", typeof (Boolean), typeof (ContactEntity));

        public Boolean Success { get { return (Boolean) GetValue (SuccessProperty); } set { SetValue (SuccessProperty, value); } }

        public static readonly DependencyProperty ContactOutcomeProperty = DependencyProperty.Register ("ContactOutcome", typeof (Server.Core.Enumerations.ContactOutcome), typeof (ContactEntity));

        public Server.Core.Enumerations.ContactOutcome ContactOutcome { get { return (Mercury.Server.Core.Enumerations.ContactOutcome) GetValue (ContactOutcomeProperty); } set { SetValue (ContactOutcomeProperty, value); } }

        public static readonly DependencyProperty ContactCanceledProperty = DependencyProperty.Register ("ContactCanceled", typeof (Boolean), typeof (ContactEntity));

        public Boolean ContactCanceled { get { return (Boolean) GetValue (ContactCanceledProperty); } set { SetValue (ContactCanceledProperty, value); } }



        public static readonly DependencyProperty AutoSaveContactProperty = DependencyProperty.Register ("AutoSaveContact", typeof (Boolean), typeof (ContactEntity));

        public Boolean AutoSaveContact { get { return (Boolean) GetValue (AutoSaveContactProperty); } set { SetValue (AutoSaveContactProperty, value); } }

        public static readonly DependencyProperty EntityContactProperty = DependencyProperty.Register ("EntityContact", typeof (Server.Core.Entity.EntityContact), typeof (ContactEntity));

        public Server.Core.Entity.EntityContact EntityContact { get { return (Server.Core.Entity.EntityContact)GetValue (EntityContactProperty); } set { SetValue (EntityContactProperty, value); } }


        public static readonly DependencyProperty WorkQueueItemIdProperty = DependencyProperty.Register ("WorkQueueItemId", typeof (Int64), typeof (ContactEntity));

        public Int64 WorkQueueItemId { get { return (Int64) GetValue (WorkQueueItemIdProperty); } set { SetValue (WorkQueueItemIdProperty, value); } }

        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public ContactEntity () {

            InitializeComponent ();

            return;

        }

        #endregion


        #region Workflow Support Methods

        private void RaiseActivityException (String exceptionMessage) {

            String caller = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

            WorkflowSteps.Add (new Mercury.Server.Workflows.WorkflowStep (Application, "Exception [" + caller + "]", exceptionMessage));

            throw new ApplicationException (this.GetType ().ToString () + ": " + exceptionMessage);

        }

        private void WorkflowStepsAdd (String stepDescription) {

            if (WorkflowSteps != null) {

                String stepName = (new System.Diagnostics.StackFrame (1)).GetMethod ().Name;

                Server.Workflows.WorkflowStep workflowStep = new Mercury.Server.Workflows.WorkflowStep (Application, stepName, stepDescription);

                WorkflowSteps.Add (workflowStep);

                if (WorkQueueItemId != 0) {

                    Application.WorkQueueItemWorkflowStepsSave (WorkQueueItemId, WorkflowSteps);

                }

            }

            return;

        }

        #endregion


        #region Workflow Steps

        private void InitializeActivity_OnCodeExecute (object sender, EventArgs e) {

            ActivityCompleted = false;

            return;

        }

        private void WhileWaitingForContact_OnEvaluation (Object Sender, ConditionalEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }


            eventArgs.Result = !ActivityCompleted; // CONTINUE WHILE IF TRUE

            return;

        }

        private void ContactRequest_OnInvoking (Object sender, EventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }


            WorkflowStepsAdd ("Contact: " + RegardingMessage);

            UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.ContactEntityRequest (Entity, RelatedEntity, RegardingMessage, IntroductionScript);

            ((Mercury.Server.Workflows.UserInteractions.Request.ContactEntityRequest)UserInteractionRequest).AllowEditRelatedEntity = AllowEditRelatedEntity;

            ((Mercury.Server.Workflows.UserInteractions.Request.ContactEntityRequest)UserInteractionRequest).AllowEditContactDateTime = AllowEditContactDateTime;

            ((Mercury.Server.Workflows.UserInteractions.Request.ContactEntityRequest) UserInteractionRequest).AllowEditRegarding = AllowEditRegarding;

            ((Mercury.Server.Workflows.UserInteractions.Request.ContactEntityRequest) UserInteractionRequest).AllowCancel = AllowCancel;

            return;

        }

        private void ContactResponse_Invoked (Object sender, ExternalDataEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }

            ContactCanceled = false;

            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.ContactEntity) {

                        Mercury.Server.Workflows.UserInteractions.Response.ContactEntityResponse response = (Mercury.Server.Workflows.UserInteractions.Response.ContactEntityResponse) UserInteractionResponseEventArgs.Response;

                        if ((!response.Cancel) || (!AllowCancel)) {

                            if ((response.EntityContact.ContactType != Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified) &&
                                (response.EntityContact.ContactOutcome != Mercury.Server.Core.Enumerations.ContactOutcome.NotSpecified)) {

                                #region Contact Received

                                EntityContact = response.EntityContact;

                                EntityContact.Application = Application;


                                if (!AllowEditContactDateTime) {

                                    EntityContact.ContactDate = DateTime.Now;

                                }

                                //else {

                                //    EntityContact.ContactDate = response.EntityContact.ContactDate;

                                //}

                                //EntityContact.EntityId = Entity.Id;

                                //EntityContact.RelatedEntityId = (response.RelatedEntity != null) ? response.RelatedEntity.Id : 0; 

                                //EntityContact.EntityContactInformationId = response.EntityContactInformationId;


                                //EntityContact.Direction = response.Direction;

                                //EntityContact.ContactType = response.ContactType;


                                //EntityContact.ContactOutcome = response.ContactOutcome;

                                //EntityContact.Successful = (response.ContactOutcome == Mercury.Server.Core.Enumerations.ContactOutcome.Success);


                                //EntityContact.Regarding = response.Regarding;

                                //EntityContact.Remarks = response.Remarks;


                                EntityContact.ContactedByName = Application.Session.UserDisplayName;



                                if (AutoSaveContact) { ActivityCompleted = EntityContact.Save (); }

                                else { ActivityCompleted = true; }


                                if (ActivityCompleted) {

                                    ContactAttempts = ContactAttempts + 1;

                                    Success = EntityContact.Successful;

                                    ContactOutcome = EntityContact.ContactOutcome;

                                    WorkflowStepsAdd ("Contact Outcome: " + ContactOutcome + "  |  # of Attempts = " + ContactAttempts);

                                }

                                else { RaiseActivityException ("Unable to save contact information."); }

                                #endregion

                            }

                        }

                        else {

                            #region Contact Canceled

                            EntityContact = null;

                            ContactCanceled = true;

                            ActivityCompleted = true;

                            #endregion

                        }

                    }

                }

            }

            return;

        }

        #endregion

    }

}
