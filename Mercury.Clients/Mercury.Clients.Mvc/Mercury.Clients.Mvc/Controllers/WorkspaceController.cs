using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers {

    [SessionState(System.Web.SessionState.SessionStateBehavior.Required)]

    public class WorkspaceController : Controller {

        #region Private Properties

        private Models.Workspace.WorkspaceModel workspaceModel = new Models.Workspace.WorkspaceModel ();

        #endregion 

        
        #region Public Properties

        public Mercury.Client.Application MercuryApplication {

            get {

                // RETREIVE PREVIOUS APPLICATION OBJECT FROM SESSION STORAGE                 

                Mercury.Client.Application mercuryApplication = (Mercury.Client.Application)Session["Mercury.Client.Application"];

                if (mercuryApplication == null) { Response.Redirect ("/PermissionDenied"); }

                return mercuryApplication;

            }

            set { Session["Mercury.Client.Application"] = value; }

        }

        #endregion 


        #region Public Methods

        public ActionResult Index(String token) {

            return View ("WorkspaceIndex", workspaceModel);

        }

        public ActionResult WorkQueueSelected_OnChanged (Int64 selectedWorkQueueId = 0) {

            if (Request.IsAjaxRequest ()) {

                workspaceModel.SelectedWorkQueueId = selectedWorkQueueId;

                return PartialView("GetWorkView", workspaceModel);

            }

            return View ("Index", workspaceModel);

        }

        public ActionResult GetWork (Int64 workQueueId = 0) {

            workspaceModel.GetWork (workQueueId); 

            return PartialView ("GetWorkView", workspaceModel);

        }

        public JsonResult MyAssignedWorkGrid (Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) { 

            List<Mercury.Server.Application.DataFilterDescriptor> filters = new List<Mercury.Server.Application.DataFilterDescriptor> ();

            List<Mercury.Server.Application.DataSortDescriptor> sorts = new List<Mercury.Server.Application.DataSortDescriptor> ();

            List<Client.Core.Work.WorkQueueItem> workQueueItems;


            // CREATE QUERY FILTERS

            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToSecurityAuthorityId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.SecurityAuthorityId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("AssignedToUserAccountId", Mercury.Server.Application.DataFilterOperator.IsEqualTo, MercuryApplication.Session.UserAccountId));

            filters.Add (MercuryApplication.CreateFilterDescriptor ("IsCompleted", Mercury.Server.Application.DataFilterOperator.IsEqualTo, false));


            // ADD SORT VALUE IF AVAILABLE 

            if (!String.IsNullOrWhiteSpace (sidx)) {

                sorts.Add (MercuryApplication.CreateSortDescription (sidx, ((sord == "asc") ? Mercury.Server.Application.DataSortDirection.Ascending : Mercury.Server.Application.DataSortDirection.Descending)));

            }
            

            Int64 totalRows = MercuryApplication.WorkQueueItemsGetCount (filters, false);

            Int32 initialRow = (page - 1) * rows + 1;

            workQueueItems = MercuryApplication.WorkQueueItemsGetByViewPage ((Mercury.Server.Application.WorkQueueView) null, filters, sorts, initialRow, rows, false);

            var gridData =

                (from currentWorkQueueItem in workQueueItems

                 select new {

                     id = "MyAssignedWork_WorkQueueItemId_" + currentWorkQueueItem.Id.ToString (), // UNIQUE ID ON PAGE

                     WorkQueueItemId = currentWorkQueueItem.Id,

                     Status = currentWorkQueueItem.StatusText,

                     WorkQueueName = currentWorkQueueItem.WorkQueueName,

                     ItemObjectType = currentWorkQueueItem.ItemObjectType,

                     ItemObjectId = currentWorkQueueItem.ItemObjectId,

                     Name = currentWorkQueueItem.Name,

                     WorkflowNextStep = currentWorkQueueItem.WorkflowNextStep,

                     AddedDate = currentWorkQueueItem.AddedDate.ToString ("MM/dd/yyyy"),

                     ConstraintDate = currentWorkQueueItem.ConstraintDate.ToString ("MM/dd/yyyy"),

                     LastWorkedDate = ((currentWorkQueueItem.LastWorkedDate.HasValue) ? currentWorkQueueItem.LastWorkedDate.Value.ToString ("MM/dd/yyyy") : String.Empty),

                     DueDate = currentWorkQueueItem.DueDate.ToString ("MM/dd/yyyy"),

                     Priority = currentWorkQueueItem.Priority,

                     WorkflowId = currentWorkQueueItem.WorkQueue.WorkflowId,

                     WorkflowName = currentWorkQueueItem.WorkflowName,

                     WorkflowInstanceId = currentWorkQueueItem.WorkflowInstanceId.ToString (),

                     WorkflowInstanceIdEmpty = (currentWorkQueueItem.WorkflowInstanceId == Guid.Empty) ? "true" : "false",

                     Action = String.Empty

                 });



            var jsonData = new {

                total = (totalRows / rows) + (((totalRows % rows) == 0) ? 0 : 1),

                page = page,

                records = totalRows,

                rows = gridData

            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult MyAssignedWorkGridDetail (String workQueueItemKey, Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            Int64 workQueueItemId = 0;

            Int64.TryParse (workQueueItemKey.Split ('_')[workQueueItemKey.Split ('_').Length - 1], out workQueueItemId);


            List<Client.Core.Work.WorkQueueItemSender> workQueueItemSenders = MercuryApplication.WorkQueueItemSendersGet (workQueueItemId, true);

            var gridData = 

                from currentWorkQueueItemSender in workQueueItemSenders

                select new { 

                    id = workQueueItemKey + "_" + currentWorkQueueItemSender.Id.ToString (),

                    WorkQueueItemSenderId = currentWorkQueueItemSender.Id,

                    EventDescription = currentWorkQueueItemSender.EventDescription,

                    Priority = currentWorkQueueItemSender.Priority,

                    CreateAccountInfoActionDate = currentWorkQueueItemSender.CreateAccountInfo.ActionDate.ToString ()

                };


            var jsonData = new {

                total = 1,

                page = 1,

                records = workQueueItemSenders.Count,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult MyAssignedCasesGrid (Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            List<Client.Core.Individual.Case.Views.MemberCaseSummary> memberCaseSummaries;


            // CREATE QUERY FILTERS (POTENTIAL FUTURE FUNCTIONALITY)

            // ADD SORT VALUE IF AVAILABLE (POTENTIAL FUTURE FUNCTIONALITY)
            

            memberCaseSummaries = MercuryApplication.MemberCaseSummaryGetByAssignedToUserPage (1, 999999, false);

            Int64 totalRows = memberCaseSummaries.Count;

            var gridData =

                (from currentMemberCaseSummary in memberCaseSummaries

                 select new {

                     id = "MyAssignedCases_MemberCaseId_" + currentMemberCaseSummary.Id.ToString (), // UNIQUE ID ON PAGE

                     MemberCaseId = currentMemberCaseSummary.Id,

                     AssignedToWorkTeamName = ((currentMemberCaseSummary.AssignedToWorkTeam != null) ? currentMemberCaseSummary.AssignedToWorkTeam.Name : "* Unassigned"),

                     AssignedToUserDisplayName = ((!String.IsNullOrWhiteSpace (currentMemberCaseSummary.AssignedToUserDisplayName)) ? currentMemberCaseSummary.AssignedToUserDisplayName : "* Unassigned"),

                     MemberId = currentMemberCaseSummary.MemberId,

                     MemberName = currentMemberCaseSummary.Member.Name,

                     StatusDescription = currentMemberCaseSummary.StatusDescription
                     
                 });



            var jsonData = new {

                total = 1,

                page = 1,

                records = totalRows,

                rowNum = totalRows,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult MyAssignedCasesGridDetail (String memberCaseKey, Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            Int64 memberCaseId = 0;

            Int64.TryParse (memberCaseKey.Split ('_')[memberCaseKey.Split ('_').Length - 1], out memberCaseId);


            Client.Core.Individual.Case.MemberCase memberCase = MercuryApplication.MemberCaseGet (memberCaseId, true);

            var gridData =

                from currentProblemClass in memberCase.ProblemClasses

                select new {

                    id = memberCaseId + "_" + currentProblemClass.Id.ToString (),

                    Classification = currentProblemClass.Classification,

                    AssignedToUserDisplayName = currentProblemClass.AssignedToUserDisplayName,

                    AssignedToProviderName = ((currentProblemClass.AssignedToProvider != null) ? currentProblemClass.AssignedToProvider.Name : String.Empty)

                };


            var jsonData = new {

                total = 1,

                page = 1,

                records = memberCase.ProblemClasses.Count,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult MyTeamCasesGrid (Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            List<Client.Core.Individual.Case.Views.MemberCaseSummary> memberCaseSummaries;


            // CREATE QUERY FILTERS (POTENTIAL FUTURE FUNCTIONALITY)

            // ADD SORT VALUE IF AVAILABLE (POTENTIAL FUTURE FUNCTIONALITY)


            memberCaseSummaries = MercuryApplication.MemberCaseSummaryGetByUserWorkTeamsPage (1, 999999, false);

            Int64 totalRows = memberCaseSummaries.Count;

            var gridData =

                (from currentMemberCaseSummary in memberCaseSummaries

                 select new {

                     id = "MyTeamCases_MemberCaseId_" + currentMemberCaseSummary.Id.ToString (), // UNIQUE ID ON PAGE

                     MemberCaseId = currentMemberCaseSummary.Id,

                     AssignedToWorkTeamName = ((currentMemberCaseSummary.AssignedToWorkTeam != null) ? currentMemberCaseSummary.AssignedToWorkTeam.Name : "* Unassigned"),

                     AssignedToUserDisplayName = ((!String.IsNullOrWhiteSpace (currentMemberCaseSummary.AssignedToUserDisplayName)) ? currentMemberCaseSummary.AssignedToUserDisplayName : "* Unassigned"),

                     MemberId = currentMemberCaseSummary.MemberId,

                     MemberName = currentMemberCaseSummary.Member.Name,

                     StatusDescription = currentMemberCaseSummary.StatusDescription,

                     LockedByUserDisplayName = currentMemberCaseSummary.LockedByUserDisplayName

                 });



            var jsonData = new {

                total = 1,

                page = 1,

                records = totalRows,

                rowNum = totalRows,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CaseLoadsGrid (Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> memberCaseLoadSummaries;


            // CREATE QUERY FILTERS (POTENTIAL FUTURE FUNCTIONALITY)

            // ADD SORT VALUE IF AVAILABLE (POTENTIAL FUTURE FUNCTIONALITY)


            memberCaseLoadSummaries = MercuryApplication.MemberCaseLoadSummaryGetByUser (MercuryApplication.Session.SecurityAuthorityId, MercuryApplication.Session.UserAccountId, false);

            Int64 totalRows = memberCaseLoadSummaries.Count;

            var gridData =

                (from currentCaseLoadSummary in memberCaseLoadSummaries

                 select new {

                     id = "MyCaseLoads_AssignedToWorkTeamId_" + currentCaseLoadSummary.AssignedToWorkTeamId.ToString (),

                     MemberCaseId = currentCaseLoadSummary.Id,

                     AssignedToWorkTeamName = ((!String.IsNullOrWhiteSpace (currentCaseLoadSummary.AssignedToWorkTeamName)) ? currentCaseLoadSummary.AssignedToWorkTeamName : "* Unassigned"),

                     UnderDevelopmentCount = currentCaseLoadSummary.StatusUnderDevelopmentCount,

                     ActiveCount = currentCaseLoadSummary.StatusActiveCount,

                     TotalOpenCount = currentCaseLoadSummary.StatusTotalOpenCount

                 });



            var jsonData = new {

                total = 1,

                page = 1,

                records = totalRows,

                rowNum = totalRows,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CaseLoadsGridDetail (String assignedToWorkTeamKey, Boolean _search, String nd, Int32 page, Int32 rows, String sidx, String sord) {

            Int64 assignedToWorkTeamId = 0;

            Int64.TryParse (assignedToWorkTeamKey.Split ('_')[assignedToWorkTeamKey.Split ('_').Length - 1], out assignedToWorkTeamId);


            List<Client.Core.Individual.Case.Views.MemberCaseLoadSummary> memberCaseLoadSummaries = MercuryApplication.MemberCaseLoadSummaryGetByWorkTeam (assignedToWorkTeamId, false);

            var gridData =

                (from currentCaseLoadSummary in memberCaseLoadSummaries

                 select new {

                     id = "MyCaseLoads_AssignedToWorkTeamId_" + currentCaseLoadSummary.AssignedToWorkTeamId.ToString (),

                     MemberCaseId = currentCaseLoadSummary.Id,

                     AssignedToWorkTeamName = ((!String.IsNullOrWhiteSpace (currentCaseLoadSummary.AssignedToWorkTeamName)) ? currentCaseLoadSummary.AssignedToWorkTeamName : "* Unassigned"),

                     AssignedToUserDisplayName = ((!String.IsNullOrWhiteSpace (currentCaseLoadSummary.AssignedToUserDisplayName)) ? currentCaseLoadSummary.AssignedToUserDisplayName : "* Unassigned"),

                     UnderDevelopmentCount = currentCaseLoadSummary.StatusUnderDevelopmentCount,

                     ActiveCount = currentCaseLoadSummary.StatusActiveCount,

                     TotalOpenCount = currentCaseLoadSummary.StatusTotalOpenCount

                 });


            var jsonData = new {

                total = 1,

                page = 1,

                records = memberCaseLoadSummaries.Count,

                rows = gridData

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult WorkQueueItemSuspend (Int64 workQueueItemSuspendId = 0, Int32 suspendDays = 0) {

            Boolean success = false;

            String exceptionMessage = String.Empty;


            success = MercuryApplication.WorkQueueItemSuspend (workQueueItemSuspendId, "Manual Suspend", String.Empty, suspendDays, 0, true);

            if (!success) { exceptionMessage = MercuryApplication.LastExceptionMessage; }


            var jsonData = new {

                Success = success,

                ExceptionMessage = exceptionMessage

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        public JsonResult WorkQueueItemClose (Int64 workQueueItemCloseId = 0, Int64 selectedWorkOutcomeId = 0) {

            Boolean success = false;

            String exceptionMessage = String.Empty;


            success = MercuryApplication.WorkQueueItemClose (workQueueItemCloseId, selectedWorkOutcomeId);

            if (!success) { exceptionMessage = MercuryApplication.LastExceptionMessage; }


            var jsonData = new {

                Success = success,

                ExceptionMessage = exceptionMessage

            };

            return Json (jsonData, JsonRequestBehavior.AllowGet);

        }

        #endregion 

    }

}
