using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
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

    public partial class OpenImage : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (OpenImage));

        public Server.Application Application { get { return (Server.Application)GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        protected static readonly DependencyProperty ActivityCompletedProperty = DependencyProperty.Register ("ActivityCompleted", typeof (Boolean), typeof (OpenImage));

        protected Boolean ActivityCompleted { get { return (Boolean)GetValue (ActivityCompletedProperty); } set { SetValue (ActivityCompletedProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (OpenImage));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>)GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty ObjectTypeProperty = DependencyProperty.Register ("ObjectType", typeof (String), typeof (OpenImage));

        public String ObjectType { get { return (String)GetValue (ObjectTypeProperty); } set { SetValue (ObjectTypeProperty, value); } }

        public static readonly DependencyProperty ObjectIdProperty = DependencyProperty.Register ("ObjectId", typeof (Int64), typeof (OpenImage));

        public Int64 ObjectId { get { return (Int64)GetValue (ObjectIdProperty); } set { SetValue (ObjectIdProperty, value); } }

        public static readonly DependencyProperty RenderProperty = DependencyProperty.Register ("Render", typeof (Boolean), typeof (OpenImage));

        public Boolean Render { get { return (Boolean)GetValue (RenderProperty); } set { SetValue (RenderProperty, value); } }



        public static readonly DependencyProperty WorkQueueItemIdProperty = DependencyProperty.Register ("WorkQueueItemId", typeof (Int64), typeof (OpenImage));

        public Int64 WorkQueueItemId { get { return (Int64)GetValue (WorkQueueItemIdProperty); } set { SetValue (WorkQueueItemIdProperty, value); } }


        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public OpenImage () {

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


            eventArgs.Result = !ActivityCompleted; // CONTINUE WHILE IF TRUE

            return;

        }

        private void OpenImage_OnInvoking (Object sender, EventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            WorkflowStepsAdd ("Opening Image for " + ObjectType + " [" + ObjectId.ToString () + "].");

            UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.OpenImageRequest (ObjectType, ObjectId, Render);

            return;

        }

        private void OpenImage_Invoked (Object sender, ExternalDataEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.OpenImage) {

                        ActivityCompleted = true;

                    }

                }

            }

            return;

        }

        #endregion


    }

}
