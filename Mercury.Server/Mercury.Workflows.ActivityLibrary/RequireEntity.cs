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

    public partial class RequireEntity : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (RequireEntity));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (RequireEntity));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty EntityTypeProperty = DependencyProperty.Register ("EntityType", typeof (Server.Core.Enumerations.EntityType), typeof (RequireEntity));

        public Server.Core.Enumerations.EntityType EntityType { get { return (Server.Core.Enumerations.EntityType) GetValue (EntityTypeProperty); } set { SetValue (EntityTypeProperty, value); } }

        public static readonly DependencyProperty InitialEntityObjectIdProperty = DependencyProperty.Register ("InitialEntityObjectId", typeof (Int64), typeof (RequireEntity));

        public Int64 InitialEntityObjectId { get { return (Int64) GetValue (InitialEntityObjectIdProperty); } set { SetValue (InitialEntityObjectIdProperty, value); } }

        public static readonly DependencyProperty EntityObjectIdProperty = DependencyProperty.Register ("EntityObjectId", typeof (Int64), typeof (RequireEntity));

        public Int64 EntityObjectId { get { return (Int64) GetValue (EntityObjectIdProperty); } set { SetValue (EntityObjectIdProperty, value); } }

        public static readonly DependencyProperty RegardingMessageProperty = DependencyProperty.Register ("RegardingMessage", typeof (String), typeof (RequireEntity));

        public String RegardingMessage { get { return (String) GetValue (RegardingMessageProperty); } set { SetValue (RegardingMessageProperty, value); } }

        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public RequireEntity () {

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

            }

            return;

        }

        #endregion


        #region Workflow Steps

        private void InitializeActivity_OnCodeExecute (object sender, EventArgs e) {

            EntityObjectId = 0;

            return;

        }

        private void WhileWaitingForEntity_OnEvaluation (Object Sender, ConditionalEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            Boolean entityFound = false;

            String entityName = EntityType.ToString () + ": ";


            if (EntityObjectId != 0) {

                switch (EntityType) {

                    case Mercury.Server.Core.Enumerations.EntityType.Member:

                        Server.Core.Member.Member member = Application.MemberGet (EntityObjectId);

                        if (member != null) {

                            entityFound = true;

                            entityName = entityName + member.Name;

                        }

                        break;

                    case Mercury.Server.Core.Enumerations.EntityType.Provider:

                        Server.Core.Provider.Provider provider = Application.ProviderGet (EntityObjectId);

                        if (provider != null) {

                            entityFound = true;

                            entityName = entityName + provider.Name;

                        }

                        break;

                }

                if (entityFound) {

                    WorkflowStepsAdd ("Entity Selected: " + entityName);

                }

            }


            eventArgs.Result = !entityFound; // CONTINUE WHILE IF TRUE

            return;

        }

        private void EntityRequest_OnInvoking (Object sender, EventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }


            WorkflowStepsAdd ("Requesting Entity for: " + RegardingMessage);

            UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequireEntityRequest (EntityType, RegardingMessage);

            ((UserInteractions.Request.RequireEntityRequest) UserInteractionRequest).InitialEntityObjectId = InitialEntityObjectId;

            return;

        }

        private void EntityResponse_Invoked (Object sender, ExternalDataEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.RequireEntity) {

                        Mercury.Server.Workflows.UserInteractions.Response.RequireEntityResponse response = (Mercury.Server.Workflows.UserInteractions.Response.RequireEntityResponse) UserInteractionResponseEventArgs.Response;

                        EntityObjectId = response.EntityId;

                    }

                }

            }

            return;

        }

        #endregion


    }

}
