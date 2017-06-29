using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace Mercury.Server.Workflows.Activities.ContactEntity {

    public sealed class ContactEntityProcessResponse : CodeActivity {

        #region Input Arguments

        public InOutArgument<Workflows.WorkflowManager4> WorkflowManager { get; set; }

        public InArgument<Workflows.UserInteractions.Response.ResponseBase> UserInteractionResponse { get; set; }

        public InArgument<Int64> WorkQueueItemId { get; set; }

        public InOutArgument<List<WorkflowStep>> WorkflowSteps { get; set; }


        public InArgument<Boolean> AllowEditRelatedEntity { get; set; }

        public InArgument<Boolean> AllowEditContactDateTime { get; set; }

        public InArgument<Boolean> AllowCancel { get; set; }

        public InArgument<Boolean> AutoSaveContact { get; set; }


        public InOutArgument<Int32> ContactAttempts { get; set; }


        #endregion 


        #region Output Arguments

        public OutArgument<Boolean> ActivityCompleted { get; set; }

        public OutArgument<Boolean> ActivityCanceled { get; set; }

        public OutArgument<Core.Entity.EntityContact> EntityContact { get; set; }

        public OutArgument<Boolean> ContactSuccess { get; set; }

        public OutArgument<Core.Enumerations.ContactOutcome> ContactOutcome { get; set; }

        #endregion 


        #region Activity

        protected override void Execute (CodeActivityContext context) {

            if (UserInteractionResponse.Get (context) == null) { return; } // VALIDATE RESPONSE WAS RECEIVED, OTHERWISE LOOP REQUEST


            ActivityCompleted.Set (context, false);

            ActivityCanceled.Set (context, false);

            if (UserInteractionResponse.Get (context).UserInteractionType == UserInteractions.Enumerations.UserInteractionType.ContactEntity) {

                Workflows.UserInteractions.Response.ContactEntityResponse response = (UserInteractions.Response.ContactEntityResponse) UserInteractionResponse.Get (context);


                if ((!response.Cancel) || (!AllowCancel.Get (context))) {


                    //if ((response.ContactType != Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified) &&
                    //    (response.ContactOutcome != Mercury.Server.Core.Enumerations.ContactOutcome.NotSpecified)) {

                    if ((response.EntityContact.ContactType != Mercury.Server.Core.Enumerations.EntityContactType.NotSpecified) &&
                        (response.EntityContact.ContactOutcome != Mercury.Server.Core.Enumerations.ContactOutcome.NotSpecified)) {

                        #region Contact Received

                        // Core.Entity.EntityContact entityContact = new Mercury.Server.Core.Entity.EntityContact (WorkflowManager.Get (context).Application);

                        Core.Entity.EntityContact entityContact = response.EntityContact;

                        entityContact.Application = WorkflowManager.Get (context).Application;


                        if (!AllowEditContactDateTime.Get (context)) {

                            entityContact.ContactDate = DateTime.Now;

                        }

                        //else {

                        //    entityContact.ContactDate = response.EntityContact.ContactDate;

                        //}


                        //entityContact.EntityId = (response.Entity != null) ? response.Entity.Id : 0;

                        //entityContact.RelatedEntityId = (response.RelatedEntity != null) ? response.RelatedEntity.Id : 0;

                        //entityContact.EntityContactInformationId = response.EntityContactInformationId;


                        //entityContact.Direction = response.Direction;

                        //entityContact.ContactType = response.ContactType;


                        //entityContact.ContactOutcome = response.ContactOutcome;

                        //entityContact.Successful = (response.ContactOutcome == Mercury.Server.Core.Enumerations.ContactOutcome.Success);


                        //entityContact.Regarding = response.Regarding;

                        //entityContact.Remarks = response.Remarks;



                        entityContact.ContactedByName = WorkflowManager.Get (context).Application.Session.UserDisplayName;



                        if (AutoSaveContact.Get (context)) { ActivityCompleted.Set (context, entityContact.Save ()); }

                        else { ActivityCompleted.Set (context, true); }

                        EntityContact.Set (context, entityContact);


                        if (ActivityCompleted.Get (context)) {

                            ContactAttempts.Set (context, ContactAttempts.Get (context) + 1);

                            ContactSuccess.Set (context, entityContact.Successful);

                            ContactOutcome.Set (context, entityContact.ContactOutcome);

                            Activities.CommonFunctions.WorkflowStepsAdd (
                                
                                WorkflowManager.Get (context).Application,

                                1,

                                WorkQueueItemId.Get (context),

                                WorkflowSteps.Get (context),

                                "Contact Outcome: " + ContactOutcome.Get (context).ToString () + "  |  # of Attempts = " + ContactAttempts.Get (context).ToString ()
                                
                                );

                        }

                        else { 
                            
                            Activities.CommonFunctions.RaiseActivityException (

                                WorkflowManager.Get (context).Application,

                                1,

                                WorkQueueItemId.Get (context),

                                WorkflowSteps.Get (context),

                                "Unable to save contact information."
                                
                                ); 
                        
                        }

                        #endregion

                    }


                }

                else {

                    // CONTACT CANCELED 

                    EntityContact.Set (context, null);

                    ActivityCanceled.Set (context, true);

                    ActivityCompleted.Set (context, true);

                }

            }


        }

        #endregion 

    }

}
