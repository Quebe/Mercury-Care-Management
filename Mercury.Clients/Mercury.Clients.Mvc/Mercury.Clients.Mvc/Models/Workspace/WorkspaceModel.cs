using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercury.Clients.Mvc.Models.Workspace {

    public class WorkspaceModel : Models.ApplicationModel {

        #region Private Properties


        #endregion 


        #region Public Properties

        public List<Client.Core.Work.WorkQueue> WorkQueuesAvailable {

            get {

                List<Client.Core.Work.WorkQueue> workQueues = (

                    from currentWorkQueue in MercuryApplication.WorkQueuesAvailable (true)

                    where ((currentWorkQueue.Enabled) && (currentWorkQueue.Visible))

                        && (MercuryApplication.Session.WorkQueuePermissions.ContainsKey (currentWorkQueue.Id))

                    orderby currentWorkQueue.Name

                    select currentWorkQueue).ToList ();

                if ((SelectedWorkQueueId == 0) && (workQueues.Count > 0)) {

                    SelectedWorkQueueId = workQueues[0].Id;

                }                              

                return workQueues;

            }

        }
        

        public Int64 SelectedWorkQueueId { get; set; }

        public Client.Core.Work.WorkQueue SelectedWorkQueue { get { return MercuryApplication.WorkQueueGet (SelectedWorkQueueId, true); } }

        public Int64 SelectedWorkQueueAvailableCount {

            get {

                if (SelectedWorkQueue == null) { return 0; }


                Int64 queueAvailableItems = 0;


                List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

                filters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, SelectedWorkQueueId));

                filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

                filters.Add (MercuryApplication.CreateFilterDescriptor ("HasConstraintDatePassed", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

                filters.Add (MercuryApplication.CreateFilterDescriptor ("IsAssigned", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

                filters.Add (MercuryApplication.CreateFilterDescriptor ("WithinWorkTimeRestrictions", Mercury.Server.Application.DataFilterOperator.IsEqualTo, true));

                queueAvailableItems = MercuryApplication.WorkQueueItemsGetCount (SelectedWorkQueue.GetWorkView, filters, false);


                return queueAvailableItems;

            }

        }

        public Int64 SelectedWorkQueueTotalCount {

            get {

                if (SelectedWorkQueue == null) { return 0; }


                // UPDATE COUNT

                Int64 queueTotalItems = 0;


                List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

                filters.Add (MercuryApplication.CreateFilterDescriptor ("WorkQueueId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, SelectedWorkQueueId));

                filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));

                queueTotalItems = MercuryApplication.WorkQueueItemsGetCount (SelectedWorkQueue.GetWorkView, filters, false);


                return queueTotalItems;

            }

        }


        public Client.Core.Work.Workflow SelectedWorkQueueWorkflow { get { return ((SelectedWorkQueue != null) ? SelectedWorkQueue.Workflow : null); } }

        public Int64 SelectedWorkQueueWorkflowId { get { return ((SelectedWorkQueueWorkflow != null) ? SelectedWorkQueueWorkflow.Id : 0); } }

        public String SelectedWorkQueueWorkflowName { get { return ((SelectedWorkQueueWorkflow != null) ? SelectedWorkQueueWorkflow.Name : "(Manual)"); } }


        public List<Client.Core.Work.WorkOutcome> WorkOutcomesAvailable {

            get {

                List<Client.Core.Work.WorkOutcome> workOutcomes = (

                    from currentWorkOutcome in MercuryApplication.WorkOutcomesAvailable (true)

                    where ((currentWorkOutcome.Enabled) && (currentWorkOutcome.Visible))

                    orderby currentWorkOutcome.Name

                    select currentWorkOutcome).ToList ();

                if ((SelectedWorkOutcomeId == 0) && (workOutcomes.Count > 0)) {

                    SelectedWorkOutcomeId = workOutcomes[0].Id;

                }

                return workOutcomes;

            }

        }

        public Int64 SelectedWorkOutcomeId { get; set; }

        public Int64 WorkQueueItemCloseId { get; set; }

        #endregion 


        #region Constructors

        #endregion 


        #region Public Methods

        public void GetWork (Int64 workQueueId) {

            SelectedWorkQueueId = workQueueId;

            if (workQueueId == 0) { 
                
                SetException ("No Work Queue Selected for Get Work.");

                return;
            
            }

            Mercury.Server.Application.GetWorkResponse response;

            response = MercuryApplication.WorkQueueGetWork (SelectedWorkQueueId);


            if (response.HasException) {

                // EXCEPTION OCCURRED WHILE GETTING WORK QUEUE ITEM

                SetException ("[" + response.Exception.Source + "] " + response.Exception.Message);

                MercuryApplication.SetLastException (response.Exception);

            }

            else if (response.WorkQueueItem == null) {

                // NO WORK QUEUE ITEM AVAILABLE

                SetInformationMessage ("No Work Queue Items Available in the selected Work Queue.");

            }

            else {

                // VALID WORK QUEUE ITEM FOUND AND RETURNED

                if (response.Workflow != null) {

                    // KICK-OFF WORKFLOW PROCESS

                    String parameterString = String.Empty;

                    parameterString = parameterString + "?WorkflowId=" + SelectedWorkQueue.WorkflowId.ToString ();

                    parameterString = parameterString + "&WorkQueueItemId=" + response.WorkQueueItem.Id.ToString ();

                    parameterString = parameterString + "&" + response.WorkQueueItem.ItemObjectType + "Id=" + response.WorkQueueItem.ItemObjectId.ToString ();


                    if (response.WorkQueueItem.WorkflowInstanceId != Guid.Empty) {

                        parameterString = parameterString + "&WorkflowInstanceId=" + response.WorkQueueItem.WorkflowInstanceId.ToString ();

                    }

                    ResponseScript = "window.location = \"/Workflow/Execute" + parameterString + "\";";

                }

                else { // MANUAL WORKFLOW, JUST HARD REFRESH

                    ResponseScript = "$(\"#MyAssignedWorkGrid\").trigger(\"reloadGrid\");";

                }

            }

            return;

        }

        #endregion 

    }

}