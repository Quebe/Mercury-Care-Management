using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercury.Clients.Mvc.Controllers {

    public class WorkflowController : ApplicationController {

        #region Public Properties

        public Models.Workflow.WorkflowModel WorkflowModel { 

            get { 

                String pageInstanceId = Request.Form ["PageInstanceId"];

                Models.Workflow.WorkflowModel workflowModel = (Models.Workflow.WorkflowModel) Session [pageInstanceId + ".WorkflowModel"];

                return workflowModel;

            }

            set {

                String pageInstanceId = value.PageInstanceId.ToString ();

                Session [pageInstanceId + ".WorkflowModel"] = value;

            }

        }

        #endregion 


        public ActionResult Execute(Int64 workflowId = 0, String workflowName = "", Int64 workQueueItemId = 0, String workflowInstanceId = "") {

            Models.Workflow.WorkflowModel workflowModel = null;


            // INITIALIZE WORKFLOW 

            if (!String.IsNullOrWhiteSpace (workflowInstanceId)) {

                // RESUME EXISTING WORKFLOW 

                workflowModel = new Models.Workflow.WorkflowModel (new Guid (workflowInstanceId));

            }

            else {
               
                // SAVE REFERRER SO THAT THE APPLICATION CAN RETURN TO IT AFTER COMPLETING THE WORKFLOW

                workflowModel = new Models.Workflow.WorkflowModel (workflowId, workQueueItemId,
                    
                    ((Request.UrlReferrer != null) ? Request.UrlReferrer.ToString () : "/Workspace")
                    
                    );

            }

            workflowModel.StoreModelStateInMemory = StoreModelStateInMemory; // SET CHILD STATE IN MEMORY

            workflowModel.UrlOriginal = Request.RawUrl;


            // STORE MODEL IN SESSION STATE (BEFORE RESPONSE SCRIPT IS SET) 

            if (StoreModelStateInMemory) {

                WorkflowModel = workflowModel;

            }


            // SET START EVENT TO OCCUR 

            workflowModel.ResponseScript = workflowModel.ResponseScriptWorkflowStart;


            return View(workflowModel);

        }

        public ActionResult Start (String pageInstanceId) {

            // CREATE MODEL AND RESTORE STATE FROM REQUEST FORM

            Models.Workflow.WorkflowModel workflowModel = WorkflowModel; // TRY TO RETREIVE FROM SESSION STATE

            if (workflowModel != null) {

                workflowModel.UpdateValues (Request.Form);

                workflowModel.ResponseScript = String.Empty; // CLEAR RESPONSE SCRIPT TO STOP LOOPING AFFECT
            
            }

            else {

                // RESTORE FROM FORM IF NOT AVAILABLE IN SESSION 

                workflowModel = new Models.Workflow.WorkflowModel (Request.Form);

            }

            workflowModel.UpdateValues (Request.Form); // UPDATE VALUES FROM FORM WHERE APPROPRIATE


            // START WORKFLOW 

            workflowModel.StartWorkflow ();            

            // PUSH MODEL OUT TO STATE STORAGE 

            if (StoreModelStateInMemory) { WorkflowModel = workflowModel; }

            // RETURN RESULTS OF WORKFLOW EXECUTION

            switch (workflowModel.ActionResultType) {

                case Models.ActionResultType.Control:

                    return View (workflowModel.WorkflowControl, workflowModel);

            }

            return View ("Execute", workflowModel);

        }

        public ActionResult Continue () {

            // CREATE MODEL AND RESTORE STATE FROM REQUEST FORM

            Models.Workflow.WorkflowModel workflowModel = WorkflowModel; // TRY TO RETREIVE FROM SESSION STATE
            
            if (workflowModel != null) {

                workflowModel.UpdateValues (Request.Form);

                workflowModel.ResponseScript = String.Empty; // CLEAR RESPONSE SCRIPT TO STOP LOOPING AFFECT

            }

            else {

                // RESTORE FROM FORM IF NOT AVAILABLE IN SESSION (THIS WILL CALL UPDATE VALUES)

                workflowModel = new Models.Workflow.WorkflowModel (Request.Form);

            }


            // CONTINUE WORKFLOW 

            workflowModel.ContinueWorkflow (Request.Form);

            // PUSH MODEL OUT TO STATE STORAGE 

            if (StoreModelStateInMemory) { WorkflowModel = workflowModel; }

            
            // RETURN RESULTS OF WORKFLOW EXECUTION

            switch (workflowModel.ActionResultType) {

                case Models.ActionResultType.Control:

                    return View (workflowModel.WorkflowControl, workflowModel);

            }

            return View ("Execute", workflowModel);

        }

    }

}
