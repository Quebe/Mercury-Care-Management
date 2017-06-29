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

    public partial class SendCorrespondence : SequenceActivity {

        #region Dependency Properties

        public static readonly DependencyProperty ApplicationProperty = DependencyProperty.Register ("Application", typeof (Server.Application), typeof (SendCorrespondence));

        public Server.Application Application { get { return (Server.Application) GetValue (ApplicationProperty); } set { SetValue (ApplicationProperty, value); } }

        protected static readonly DependencyProperty ActivityCompletedProperty = DependencyProperty.Register ("ActivityCompleted", typeof (Boolean), typeof (SendCorrespondence));

        protected Boolean ActivityCompleted { get { return (Boolean) GetValue (ActivityCompletedProperty); } set { SetValue (ActivityCompletedProperty, value); } }

        public static readonly DependencyProperty WorkflowStepsProperty = DependencyProperty.Register ("WorkflowSteps", typeof (System.Collections.Generic.List<Server.Workflows.WorkflowStep>), typeof (SendCorrespondence));

        public System.Collections.Generic.List<Server.Workflows.WorkflowStep> WorkflowSteps { get { return (System.Collections.Generic.List<Server.Workflows.WorkflowStep>) GetValue (WorkflowStepsProperty); } set { SetValue (WorkflowStepsProperty, value); } }


        public static readonly DependencyProperty EntityProperty = DependencyProperty.Register ("Entity", typeof (Server.Core.Entity.Entity), typeof (SendCorrespondence));

        public Server.Core.Entity.Entity Entity { get { return (Server.Core.Entity.Entity)GetValue (EntityProperty); } set { SetValue (EntityProperty, value); } }

        public static readonly DependencyProperty RelatedEntityProperty = DependencyProperty.Register ("RelatedEntity", typeof (Server.Core.Entity.Entity), typeof (SendCorrespondence));

        public Server.Core.Entity.Entity RelatedEntity { get { return (Server.Core.Entity.Entity)GetValue (RelatedEntityProperty); } set { SetValue (RelatedEntityProperty, value); } }


        public static readonly DependencyProperty CorrespondenceNameProperty = DependencyProperty.Register ("CorrespondenceName", typeof (String), typeof (SendCorrespondence));

        public String CorrespondenceName { get { return (String) GetValue (CorrespondenceNameProperty); } set { SetValue (CorrespondenceNameProperty, value); } }


        public static readonly DependencyProperty CorrespondenceAttentionProperty = DependencyProperty.Register ("CorrespondenceAttention", typeof (String), typeof (SendCorrespondence));

        public String CorrespondenceAttention { get { return (String) GetValue (CorrespondenceAttentionProperty); } set { SetValue (CorrespondenceAttentionProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowUserSelectionProperty = DependencyProperty.Register ("CorrespondenceAllowUserSelection", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowUserSelection { get { return (Boolean) GetValue (CorrespondenceAllowUserSelectionProperty); } set { SetValue (CorrespondenceAllowUserSelectionProperty, value); } }


        public static readonly DependencyProperty CorrespondenceAllowAlternateAddressProperty = DependencyProperty.Register ("CorrespondenceAllowAlternateAddress", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowAlternateAddress { get { return (Boolean) GetValue (CorrespondenceAllowAlternateAddressProperty); } set { SetValue (CorrespondenceAllowAlternateAddressProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAlternateAddressProperty = DependencyProperty.Register ("CorrespondenceAlternateAddress", typeof (Server.Core.Entity.EntityAddress), typeof (SendCorrespondence));

        public Server.Core.Entity.EntityAddress CorrespondenceAlternateAddress { get { return (Server.Core.Entity.EntityAddress)GetValue (CorrespondenceAlternateAddressProperty); } set { SetValue (CorrespondenceAlternateAddressProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowSendByFacsimileProperty = DependencyProperty.Register ("CorrespondenceAllowSendByFacsimile", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowSendByFacsimile { get { return (Boolean) GetValue (CorrespondenceAllowSendByFacsimileProperty); } set { SetValue (CorrespondenceAllowSendByFacsimileProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAlternateFaxNumberProperty = DependencyProperty.Register ("CorrespondenceAlternateFaxNumber", typeof (String), typeof (SendCorrespondence));

        public String CorrespondenceAlternateFaxNumber { get { return (String) GetValue (CorrespondenceAlternateFaxNumberProperty); } set { SetValue (CorrespondenceAlternateFaxNumberProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowSendByEmailProperty = DependencyProperty.Register ("CorrespondenceAllowSendByEmail", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowSendByEmail { get { return (Boolean) GetValue (CorrespondenceAllowSendByEmailProperty); } set { SetValue (CorrespondenceAllowSendByEmailProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAlternateEmailProperty = DependencyProperty.Register ("CorrespondenceAlternateEmail", typeof (String), typeof (SendCorrespondence));

        public String CorrespondenceAlternateEmail { get { return (String) GetValue (CorrespondenceAlternateEmailProperty); } set { SetValue (CorrespondenceAlternateEmailProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowSendByInPersonProperty = DependencyProperty.Register ("CorrespondenceAllowSendByInPerson", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowSendByInPerson { get { return (Boolean) GetValue (CorrespondenceAllowSendByInPersonProperty); } set { SetValue (CorrespondenceAllowSendByInPersonProperty, value); } }


        public static readonly DependencyProperty CorrespondenceAllowCancelProperty = DependencyProperty.Register ("CorrespondenceAllowCancel", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowCancel { get { return (Boolean) GetValue (CorrespondenceAllowCancelProperty); } set { SetValue (CorrespondenceAllowCancelProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowEditRelatedEntityProperty = DependencyProperty.Register ("CorrespondenceAllowEditRelatedEntity", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowEditRelatedEntity { get { return (Boolean)GetValue (CorrespondenceAllowEditRelatedEntityProperty); } set { SetValue (CorrespondenceAllowEditRelatedEntityProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowHistoricalSendDateProperty = DependencyProperty.Register ("CorrespondenceAllowHistoricalSendDate", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowHistoricalSendDate { get { return (Boolean) GetValue (CorrespondenceAllowHistoricalSendDateProperty); } set { SetValue (CorrespondenceAllowHistoricalSendDateProperty, value); } }

        public static readonly DependencyProperty CorrespondenceAllowFutureSendDateProperty = DependencyProperty.Register ("CorrespondenceAllowFutureSendDate", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean CorrespondenceAllowFutureSendDate { get { return (Boolean) GetValue (CorrespondenceAllowFutureSendDateProperty); } set { SetValue (CorrespondenceAllowFutureSendDateProperty, value); } }

        public static readonly DependencyProperty CorrespondenceSendDateProperty = DependencyProperty.Register ("CorrespondenceSendDate", typeof (DateTime), typeof (SendCorrespondence));

        public DateTime CorrespondenceSendDate { get { return (DateTime) GetValue (CorrespondenceSendDateProperty); } set { SetValue (CorrespondenceSendDateProperty, value); } }


        public static readonly DependencyProperty RequestMessageForSendCorrespondenceProperty = DependencyProperty.Register ("RequestMessageForSendCorrespondence", typeof (String), typeof (SendCorrespondence));

        public String RequestMessageForSendCorrespondence { get { return (String) GetValue (RequestMessageForSendCorrespondenceProperty); } set { SetValue (RequestMessageForSendCorrespondenceProperty, value); } }


        public static readonly DependencyProperty AutoSaveCorrespondenceProperty = DependencyProperty.Register ("AutoSaveCorrespondence", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean AutoSaveCorrespondence { get { return (Boolean) GetValue (AutoSaveCorrespondenceProperty); } set { SetValue (AutoSaveCorrespondenceProperty, value); } }

        public static readonly DependencyProperty EntityCorrespondenceProperty = DependencyProperty.Register ("EntityCorrespondence", typeof (Server.Core.Entity.EntityCorrespondence), typeof (SendCorrespondence));

        public Server.Core.Entity.EntityCorrespondence EntityCorrespondence { get { return (Server.Core.Entity.EntityCorrespondence)GetValue (EntityCorrespondenceProperty); } set { SetValue (EntityCorrespondenceProperty, value); } }

        public static readonly DependencyProperty SendCorrespondenceCanceledProperty = DependencyProperty.Register ("SendCorrespondenceCanceled", typeof (Boolean), typeof (SendCorrespondence));

        public Boolean SendCorrespondenceCanceled { get { return (Boolean) GetValue (SendCorrespondenceCanceledProperty); } set { SetValue (SendCorrespondenceCanceledProperty, value); } }

        public static readonly DependencyProperty WorkQueueItemIdProperty = DependencyProperty.Register ("WorkQueueItemId", typeof (Int64), typeof (SendCorrespondence));

        public Int64 WorkQueueItemId { get { return (Int64) GetValue (WorkQueueItemIdProperty); } set { SetValue (WorkQueueItemIdProperty, value); } }

        #endregion


        #region Event Parameters - Request/Response

        public UserInteractions.Request.RequestBase UserInteractionRequest = new Mercury.Server.Workflows.UserInteractions.Request.RequestBase ();

        public UserInteractions.Response.ResponseEventArgs UserInteractionResponseEventArgs;


        private UserInteractions.Response.ResponseBase UserInteractionResponse { get { return (UserInteractionResponseEventArgs == null) ? null : UserInteractionResponseEventArgs.Response; } }

        private UserInteractions.Enumerations.UserInteractionType UserInteractionResponseType { get { return (UserInteractionResponse == null) ? UserInteractions.Enumerations.UserInteractionType.NotSpecified : UserInteractionResponse.UserInteractionType; } }

        #endregion


        #region Constructors

        public SendCorrespondence () {

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

        private void WhileWaitingForCorrespondence_OnEvaluation (Object Sender, ConditionalEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }


            eventArgs.Result = !ActivityCompleted; // CONTINUE WHILE IF TRUE

            return;

        }

        private void SendCorrespondenceRequest_OnInvoking (Object sender, EventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }


            Int64 correspondenceId = Application.CorrespondenceGetIdByName (CorrespondenceName);

            if (correspondenceId == 0) { RaiseActivityException ("Unable to find Correspondence (" + CorrespondenceName + ")."); }


            Mercury.Server.Workflows.UserInteractions.Request.SendCorrespondenceRequest sendCorrespondenceRequest;

            sendCorrespondenceRequest = new Mercury.Server.Workflows.UserInteractions.Request.SendCorrespondenceRequest (Entity, RelatedEntity, correspondenceId, CorrespondenceAllowUserSelection, CorrespondenceAllowAlternateAddress, CorrespondenceAllowCancel);

            sendCorrespondenceRequest.Attention = CorrespondenceAttention;

            sendCorrespondenceRequest.AllowEditRelatedEntity = CorrespondenceAllowEditRelatedEntity;

            sendCorrespondenceRequest.AllowHistoricalSendDate = CorrespondenceAllowHistoricalSendDate;

            sendCorrespondenceRequest.AllowFutureSendDate = CorrespondenceAllowFutureSendDate;

            sendCorrespondenceRequest.AllowSendByFacsimile = CorrespondenceAllowSendByFacsimile;

            sendCorrespondenceRequest.AllowSendByEmail = CorrespondenceAllowSendByEmail;

            sendCorrespondenceRequest.AllowSendByInPerson = CorrespondenceAllowSendByInPerson;
            

            if (CorrespondenceSendDate >= DateTime.Today) { sendCorrespondenceRequest.SendDate = CorrespondenceSendDate; }

            sendCorrespondenceRequest.AlternateAddress = CorrespondenceAlternateAddress;

            sendCorrespondenceRequest.AlternateFaxNumber = CorrespondenceAlternateFaxNumber;

            sendCorrespondenceRequest.AlternateEmail = CorrespondenceAlternateEmail;


            sendCorrespondenceRequest.Message = RequestMessageForSendCorrespondence;

            UserInteractionRequest = sendCorrespondenceRequest;


            WorkflowStepsAdd ("Send Correspondence: " + CorrespondenceName);

            return;

        }

        private void SendCorrespondenceResponse_OnInvoked (Object sender, ExternalDataEventArgs eventArgs) {

            if (Application == null) { RaiseActivityException ("Invalid Application Reference Specified."); }

            if (Entity == null) { RaiseActivityException ("Entity not assigned."); }


            //Server.Core.EntityAddress entityAddress = null;

            //Server.Core.EntityContactInformation entityContactInformation = null;

            //Server.Core.Correspondence correspondence = null;


            if (UserInteractionResponseEventArgs != null) {

                if (UserInteractionResponseEventArgs.Response != null) {

                    if (UserInteractionResponseEventArgs.Response.UserInteractionType == Mercury.Server.Workflows.UserInteractions.Enumerations.UserInteractionType.SendCorrespondence) {

                        Mercury.Server.Workflows.UserInteractions.Response.SendCorrespondenceResponse correspondenceResponse = (Mercury.Server.Workflows.UserInteractions.Response.SendCorrespondenceResponse) UserInteractionResponseEventArgs.Response;


                        if ((CorrespondenceAllowCancel) && (correspondenceResponse.Cancel)) {

                            EntityCorrespondence = null;

                            ActivityCompleted = true;

                            SendCorrespondenceCanceled = true;

                            return;

                        }


                        SendCorrespondenceCanceled = false;

                        if (correspondenceResponse.EntityCorrespondence == null) { return; }

                        EntityCorrespondence = correspondenceResponse.EntityCorrespondence;

                        EntityCorrespondence.Application = Application;

                        
                        if (AutoSaveCorrespondence) { ActivityCompleted = EntityCorrespondence.Save (); }

                        else { ActivityCompleted = true; }


                        if (ActivityCompleted) {

                            WorkflowStepsAdd ("Send Correspondence: " + EntityCorrespondence.CorrespondenceName);

                            EntityCorrespondence = EntityCorrespondence; // UPDATE TO SELF TO ALLOW FOR CAPTURING THE NEW UNIQUE ID

                        }

                        else { RaiseActivityException ("Unable to save correspondence information."); }


                        #region Old Entity Correspondence Mappings

                        //if (correspondenceResponse.CorrespondenceId == 0) { return; }

                        //EntityCorrespondence = new Mercury.Server.Core.EntityCorrespondence (Application, Entity.EntityId, correspondenceResponse.CorrespondenceId);

                        //entityAddress = Application.EntityAddressGet (correspondenceResponse.EntityAddressId);

                        //entityContactInformation = Application.EntityContactInformationGet (correspondenceResponse.EntityContactInformationId);

                        //correspondence = Application.CorrespondenceGet (correspondenceResponse.CorrespondenceId);

                        //if (correspondence != null) {

                        //    EntityCorrespondence.CorrespondenceId = correspondence.CorrespondenceId;

                        //    EntityCorrespondence.CorrespondenceName = correspondence.Name;

                        //    EntityCorrespondence.ReadyToSendDate = correspondenceResponse.SendDate;

                        //    EntityCorrespondence.EntityAddressId = correspondenceResponse.EntityAddressId;

                        //    EntityCorrespondence.EntityContactInformationId = correspondenceResponse.EntityContactInformationId;

                        //    EntityCorrespondence.Attention = correspondenceResponse.Attention;


                        //    EntityCorrespondence.ContactType = correspondenceResponse.ContactType;

                        //    switch (correspondenceResponse.ContactType) {

                        //        case Mercury.Server.Core.Enumerations.EntityContactType.ByMail:

                        //            if ((!correspondenceResponse.UseAlternateAddress) && (entityAddress != null)) {

                        //                EntityCorrespondence.AddressLine1 = entityAddress.Line1;

                        //                EntityCorrespondence.AddressLine2 = entityAddress.Line2;

                        //                EntityCorrespondence.AddressCity = entityAddress.City;

                        //                EntityCorrespondence.AddressState = entityAddress.State;

                        //                EntityCorrespondence.AddressZipCode = entityAddress.ZipCode;

                        //                EntityCorrespondence.AddressZipPlus4 = entityAddress.ZipPlus4;

                        //                EntityCorrespondence.AddressPostalCode = entityAddress.PostalCode;

                        //            }

                        //            else {

                        //                EntityCorrespondence.AddressLine1 = correspondenceResponse.AlternateAddressLine1;

                        //                EntityCorrespondence.AddressLine2 = correspondenceResponse.AlternateAddressLine2;

                        //                EntityCorrespondence.AddressCity = correspondenceResponse.AlternateAddressCity;

                        //                EntityCorrespondence.AddressState = correspondenceResponse.AlternateAddressState;

                        //                EntityCorrespondence.AddressZipCode = correspondenceResponse.AlternateAddressZipCode;

                        //            }

                        //            break;

                        //        case Mercury.Server.Core.Enumerations.EntityContactType.Facsimile:

                        //            if ((!correspondenceResponse.UseAlternateContactFaxNumber) && (entityContactInformation != null)) {

                        //                EntityCorrespondence.ContactFaxNumber = entityContactInformation.Number;

                        //            }

                        //            else {

                        //                EntityCorrespondence.ContactFaxNumber = correspondenceResponse.AlternateContactFaxNumber;

                        //            }

                        //            break;

                        //        case Mercury.Server.Core.Enumerations.EntityContactType.Email:

                        //            if ((!correspondenceResponse.UseAlternateContactEmail) && (entityContactInformation != null)) {

                        //                EntityCorrespondence.ContactEmail = entityContactInformation.Email;

                        //            }

                        //            else {

                        //                EntityCorrespondence.ContactEmail = correspondenceResponse.AlternateContactEmail;

                        //            }

                        //            break;

                        //    }
                            

                        //    EntityCorrespondence.ReportUrl = correspondence.SingleReportUrl;

                        //    EntityCorrespondence.ReportQueryString = correspondence.SingleReportQueryString;


                        //    if (AutoSaveCorrespondence) { ActivityCompleted = EntityCorrespondence.Save (); }

                        //    else { ActivityCompleted = true; }


                        //    if (ActivityCompleted) {

                        //        WorkflowStepsAdd ("Send Correspondence: " + EntityCorrespondence.CorrespondenceName);

                        //        EntityCorrespondence = EntityCorrespondence; // UPDATE TO SELF TO ALLOW FOR CAPTURING THE NEW UNIQUE ID

                        //    }

                        //    else { RaiseActivityException ("Unable to save correspondence information."); }

                        //}

                        #endregion

                    }

                }

            }

            return;

        }

        #endregion

    }

}
