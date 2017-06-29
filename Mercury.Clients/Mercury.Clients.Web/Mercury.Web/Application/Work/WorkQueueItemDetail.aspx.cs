using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Work {

    public partial class WorkQueueItemDetail : System.Web.UI.Page {

        #region Private Properties

        private Client.Core.Work.WorkQueueItem workQueueItem = null;

        #endregion


        #region Private Session Cache

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            Int64 workQueueItemId = 0;


            if (MercuryApplication == null) { return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["WorkQueueItemId"] != null) {

                    workQueueItemId = Int64.Parse (Request.QueryString["WorkQueueItemId"]);

                }

                workQueueItem = MercuryApplication.WorkQueueItemGet (workQueueItemId);

                if (workQueueItem == null) { Response.Redirect ("/PermissionDenied.aspx", true); return; }

                InitializeAll ();

                #endregion

            } // Initial Page Load

            else { // Postback



            }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        private void InitializeAll () {

            InitializeGeneral ();

            InitializeWorkTimeRestrictions ();

            InitializeExtendedProperties ();

            InitializeSenders ();

            InitializeAssignmentHistory ();

            InitializeWorkflowSteps ();

            return;

        }

        private void InitializeGeneral () {

            if (workQueueItem == null) { return; }

            Page.Title = "[" + workQueueItem.ObjectType + "] " + workQueueItem.Description + " (" + workQueueItem.Id.ToString () + ")";

            TitleBarLabel.Text = Page.Title;


            WorkQueueItemId.Text = workQueueItem.Id.ToString ();

            WorkQueueItemDescription.Text = workQueueItem.Description;

            WorkQueueItemGroupKey.Text = workQueueItem.ItemGroupKey;

            WorkQueueItemType.Text = workQueueItem.ItemObjectType;


            WorkQueueItemWorkQueueName.Text = workQueueItem.WorkQueueName;

            WorkQueueItemAddedDate.Text = workQueueItem.AddedDate.ToString ("MM/dd/yyyy");

            WorkQueueItemLastWorked.Text = (workQueueItem.LastWorkedDate.HasValue) ? workQueueItem.LastWorkedDate.Value.ToString ("MM/dd/yyyy") : String.Empty;

            WorkQueueItemConstraintDate.Text = workQueueItem.ConstraintDate.ToString ("MM/dd/yyyy");

            WorkQueueItemMilestoneDate.Text = workQueueItem.MilestoneDate.ToString ("MM/dd/yyyy");

            WorkQueueItemThresholdDate.Text = workQueueItem.ThresholdDate.ToString ("MM/dd/yyyy");

            WorkQueueItemDueDate.Text = workQueueItem.DueDate.ToString ("MM/dd/yyyy");

            WorkQueueItemCompletion.Text = (workQueueItem.CompletionDate.HasValue) ? workQueueItem.CompletionDate.Value.ToString ("MM/dd/yyyy") : String.Empty;

            WorkQueueItemOutcome.Text = workQueueItem.WorkOutcomeName;


            WorkQueueItemWorkflowName.Text = workQueueItem.WorkflowName;

            WorkQueueItemWorkflowInstanceId.Text = workQueueItem.WorkflowInstanceId.ToString ();

            WorkQueueItemWorkflowLastStep.Text = workQueueItem.WorkflowLastStep;

            WorkQueueItemWorkflowNextStep.Text = workQueueItem.WorkflowNextStep;



            WorkQueueItemCreateAuthorityName.Text = workQueueItem.CreateAccountInfo.SecurityAuthorityName;

            WorkQueueItemCreateAccountId.Text = workQueueItem.CreateAccountInfo.UserAccountId;

            WorkQueueItemCreateAccountName.Text = workQueueItem.CreateAccountInfo.UserAccountName;

            WorkQueueItemCreateDate.SelectedDate = workQueueItem.CreateAccountInfo.ActionDate;


            WorkQueueItemModifiedAuthorityName.Text = workQueueItem.ModifiedAccountInfo.SecurityAuthorityName;

            WorkQueueItemModifiedAccountId.Text = workQueueItem.ModifiedAccountInfo.UserAccountId;

            WorkQueueItemModifiedAccountName.Text = workQueueItem.ModifiedAccountInfo.UserAccountName;

            WorkQueueItemModifiedDate.SelectedDate = workQueueItem.ModifiedAccountInfo.ActionDate;


            WorkQueueItemAssignedToAuthorityName.Text = workQueueItem.AssignedToSecurityAuthorityId.ToString ();

            WorkQueueItemAssignedToAccountId.Text = workQueueItem.AssignedToUserAccountId;

            WorkQueueItemAssignedToAccountName.Text = workQueueItem.AssignedToUserAccountName;

            WorkQueueItemAssignedToDate.SelectedDate = workQueueItem.AssignedToDate;

            return;

        }

        private void InitializeWorkTimeRestrictions () {

            if (workQueueItem == null) { return; }


            System.Data.DataTable workTimeRestrictionsTable = new System.Data.DataTable ();

            workTimeRestrictionsTable.Columns.Add ("DayOfWeek");

            workTimeRestrictionsTable.Columns.Add ("StartTime");

            workTimeRestrictionsTable.Columns.Add ("EndTime");


            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.SundayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Sunday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }

            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.MondayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Monday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }

            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.TuesdayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Tuesday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }

            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.WednesdayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Wednesday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }


            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.ThursdayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Thursday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }


            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.FridayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Friday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }

            foreach (Mercury.Server.Application.CalendarTimeSegment currentTime in workQueueItem.WorkTimeRestrictions.SaturdayTimes) {

                workTimeRestrictionsTable.Rows.Add (

                    "Saturday",

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.StartTime.Hours, currentTime.StartTime.Minutes, currentTime.StartTime.Seconds),

                    String.Format ("{0:00}:{1:00}:{2:00}", currentTime.EndTime.Hours, currentTime.EndTime.Minutes, currentTime.EndTime.Seconds)

                    );

            }

            WorkTimeRestrictionsRepeater.DataSource = workTimeRestrictionsTable;

            WorkTimeRestrictionsRepeater.DataBind ();

            return;

        }

        private void InitializeExtendedProperties () {

            if (workQueueItem == null) { return; }


            System.Data.DataTable extendedPropertiesTable = new System.Data.DataTable ();

            extendedPropertiesTable.Columns.Add ("PropertyName");

            extendedPropertiesTable.Columns.Add ("PropertyValue");

            foreach (String currentPropertyName in workQueueItem.ExtendedProperties.Keys) {

                extendedPropertiesTable.Rows.Add (currentPropertyName, workQueueItem.ExtendedProperties[currentPropertyName]);

            }

            ExtendedPropertiesRepeater.DataSource = extendedPropertiesTable;

            ExtendedPropertiesRepeater.DataBind ();

            return;

        }

        private void InitializeSenders () {

            if (workQueueItem == null) { return; }

            System.Data.DataTable sendersTable = new System.Data.DataTable ();

            sendersTable.Columns.Add ("SenderObjectType");

            sendersTable.Columns.Add ("EventObjectType");

            sendersTable.Columns.Add ("EventDescription");

            sendersTable.Columns.Add ("CreateAccountName");

            sendersTable.Columns.Add ("CreateDate");


            foreach (Client.Core.Work.WorkQueueItemSender currentSender in MercuryApplication.WorkQueueItemSendersGet (workQueueItem.Id, false)) {

                sendersTable.Rows.Add (

                    
                    currentSender.SenderObjectType.Replace (".", " . "),

                    currentSender.EventObjectType.Replace (".", " . "),

                    currentSender.EventDescription,

                    currentSender.CreateAccountInfo.UserAccountName,

                    currentSender.CreateAccountInfo.ActionDate.ToString ()


                    );

            }
                

            SendersRepeater.DataSource = sendersTable;

            SendersRepeater.DataBind ();

            return;

        }

        private void InitializeAssignmentHistory () {

            if (workQueueItem == null) { return; }

            System.Data.DataTable assignmentHistoryTable = new System.Data.DataTable ();

            assignmentHistoryTable.Columns.Add ("AssignedToWorkQueue");

            assignmentHistoryTable.Columns.Add ("AssignedTo");

            assignmentHistoryTable.Columns.Add ("AssignedDate");

            assignmentHistoryTable.Columns.Add ("AssignmentSource");

            assignmentHistoryTable.Columns.Add ("AssignedBy");


            foreach (Mercury.Server.Application.WorkQueueItemAssignmentHistory currentHistory in MercuryApplication.WorkQueueItemAssignmentHistoryGet (workQueueItem.Id, false)) {

                assignmentHistoryTable.Rows.Add (

                    ((currentHistory.AssignedFromWorkQueueId == currentHistory.AssignedToWorkQueueId) ? " < no change > " : currentHistory.AssignedFromWorkQueueId.ToString () + " - " + currentHistory.AssignedToWorkQueueId.ToString ()),

                    ((String.IsNullOrEmpty (currentHistory.AssignedToUserAccountName)) ? "** Not Assigned" : currentHistory.AssignedToUserDisplayName + " (" + currentHistory.AssignedToUserAccountName + ")"),

                    currentHistory.AssignedToDate.Value.ToString (),

                    currentHistory.AssignmentSource,

                    currentHistory.ModifiedAccountInfo.UserAccountName

                    );

            }
           

            AssignmentHistoryRepeater.DataSource = assignmentHistoryTable;

            AssignmentHistoryRepeater.DataBind ();

            return;

        }

        private void InitializeWorkflowSteps () {

            if (workQueueItem == null) { return; }

            //System.Data.DataTable stepTable = new System.Data.DataTable ();

            //stepTable.Columns.Add ("StepDate");

            //stepTable.Columns.Add ("Name");

            //stepTable.Columns.Add ("Description");

            //stepTable.Columns.Add ("UserName");


            //List<Mercury.Server.Application.WorkflowStep> workflowSteps = MercuryApplication.WorkQueueItemWorkflowStepsGet (workQueueItem.Id, false);

            //if (workflowSteps != null) {

            //    foreach (Mercury.Server.Application.WorkflowStep currentStep in workflowSteps) {

            //        stepTable.Rows.Add (

            //            currentStep.StepDate.ToString (),

            //            currentStep.Name,

            //            currentStep.Description,

            //            currentStep.UserDisplayName

            //        );

            //    }

            //}

            //if (stepTable.Rows.Count == 0) {

            //    stepTable.Rows.Add ("** No Steps", String.Empty, String.Empty, String.Empty);

            //}


            // WorkflowStepsRepeater.DataSource = stepTable;

            WorkflowStepsRepeater.DataSource = MercuryApplication.WorkQueueItemWorkflowStepsGet (workQueueItem.Id, false);

            WorkflowStepsRepeater.DataBind ();

            return;

        }

        #endregion 

    }

}
