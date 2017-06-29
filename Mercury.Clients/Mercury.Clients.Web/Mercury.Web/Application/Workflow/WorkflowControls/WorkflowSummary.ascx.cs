using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Workflow.WorkflowControls {

    public partial class WorkflowSummary : System.Web.UI.UserControl {

        #region State Properties

        public Mercury.Web.Application.Workflow.Workflow WorkflowPage { get { return (Mercury.Web.Application.Workflow.Workflow)Page; } }

        public String SessionCachePrefix { get { return WorkflowPage.SessionCachePrefix + this.GetType ().ToString (); } }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            InitializeSteps ();

            return;

        }

        #endregion


        #region Initialization

        private void InitializeSteps () {

            #region Workflow Steps

            WorkflowStepsRepeater.DataSource = WorkflowPage.WorkflowResponse.WorkflowSteps;

            WorkflowStepsRepeater.DataBind ();

            #endregion

            return;

        }

        public void SetWorkflowSteps (List<Server.Application.WorkflowStep> workflowSteps) {

            WorkflowStepsRepeater.DataSource = workflowSteps;

            WorkflowStepsRepeater.DataBind ();

            return;

        }

        public void SetWorkflowSteps (Server.Application.WorkflowStep[] workflowSteps) {

            WorkflowStepsRepeater.DataSource = workflowSteps;

            WorkflowStepsRepeater.DataBind ();

            return;

        }

        #endregion 
    }

}