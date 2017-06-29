using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberCaseView : System.Web.UI.UserControl {

        #region Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application)Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }

        private Telerik.Web.UI.RadAjaxManager TelerikAjaxManager { get { return (Telerik.Web.UI.RadAjaxManager)Page.FindControl ("TelerikAjaxManager"); } }


        public Client.Core.Member.Member Member { get { return (Client.Core.Member.Member)Session[SessionCachePrefix + "Member"]; } set { Session[SessionCachePrefix + "Member"] = value; } }

        public System.Data.DataTable MemberCaseGridMasterTable {

            get {

                System.Data.DataTable gridTable = (System.Data.DataTable)Session[SessionCachePrefix + "MemberCaseGridMasterTable"];

                if (gridTable == null) {

                    gridTable = new System.Data.DataTable ();

                    gridTable.Columns.Add ("MemberCaseId");

                    gridTable.Columns.Add ("MemberId");

                    gridTable.Columns.Add ("CareLevelId");

                    gridTable.Columns.Add ("MemberCaseIdAnchor");

                    gridTable.Columns.Add ("CareLevelName");

                    gridTable.Columns.Add ("ResponsibleParty");

                    gridTable.Columns.Add ("AssignedTo");


                    gridTable.Columns.Add ("NextActivity");

                    gridTable.Columns.Add ("NextActivityDate");

                    gridTable.Columns.Add ("NextThresholdDate");

                    gridTable.Columns.Add ("Status");


                    gridTable.Columns.Add ("EffectiveDate");

                    gridTable.Columns.Add ("TerminationDate");

                    gridTable.Columns.Add ("CareOutcomeId");

                    gridTable.Columns.Add ("CareOutcomeName");

                    Session[SessionCachePrefix + "MemberCaseGridMasterTable"] = gridTable;

                }

                return gridTable;

            }

            set { Session[SessionCachePrefix + "MemberCaseGridMasterTable"] = value; }

        }

        public System.Data.DataTable MemberCaseGridCarePlanTable {

            get {

                System.Data.DataTable gridTable = (System.Data.DataTable)Session[SessionCachePrefix + "MemberCaseGridCarePlanTable"];

                if (gridTable == null) {

                    gridTable = new System.Data.DataTable ();

                    gridTable.Columns.Add ("MemberCaseId");

                    gridTable.Columns.Add ("MemberCarePlanId");

                    gridTable.Columns.Add ("ProblemStatementId");

                    gridTable.Columns.Add ("CarePlanId");

                    gridTable.Columns.Add ("ProviderId");

                    gridTable.Columns.Add ("ProblemStatementText");

                    gridTable.Columns.Add ("CarePlanName");

                    gridTable.Columns.Add ("ProviderName");

                    gridTable.Columns.Add ("NextObjectiveName");

                    //gridTable.Columns.Add ("NextObjectiveDate");

                    gridTable.Columns.Add ("NextInterventionName");

                    //gridTable.Columns.Add ("NextInterventionDate");

                    gridTable.Columns.Add ("EffectiveDate");

                    gridTable.Columns.Add ("TerminationDate");

                    Session[SessionCachePrefix + "MemberCaseGridCarePlanTable"] = gridTable;

                }

                return gridTable;

            }

            set { Session[SessionCachePrefix + "MemberCaseGridCarePlanTable"] = value; }

        }


        public Boolean AllowUserInteraction {

            get {

                Boolean allowUserInteraction = false;

                if (Session[SessionCachePrefix + "AllowUserInteraction"] != null) {

                    allowUserInteraction = (Boolean)Session[SessionCachePrefix + "AllowUserInteraction"];

                }

                return allowUserInteraction;

            }

            set {

                Session[SessionCachePrefix + "AllowUserInteraction"] = value;

                InitializeToolbar ();

            }

        }

        public Unit MemberCaseGridHeight { get { return MemberCaseGrid.Height; } set { MemberCaseGrid.Height = value; } }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }


        private LinkButton toolbarCreateCaseButton = null;

        #endregion


        #region Constructors

        protected void Page_Load (object sender, EventArgs e) {


            return;

        }

        protected void Page_PreRender (Object sender, EventArgs e) {

            return;

        }

        #endregion 

        
        #region Initialization

        private void InitializeToolbar () {

            return;

        }

        public void InitializeMember (Client.Core.Member.Member forMember) {

            Member = forMember;

            MemberCaseGrid_OnNeedDataSource (this, new Telerik.Web.UI.GridNeedDataSourceEventArgs (Telerik.Web.UI.GridRebindReason.ExplicitRebind));


            return;

        }

        #endregion 
        

        #region Member Case Grid Events

        protected void MemberCaseGrid_OnItemCreated (Object sender, Telerik.Web.UI.GridItemEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            if (Member == null) { return; }

            if (eventArgs.Item is Telerik.Web.UI.GridCommandItem) {

                Telerik.Web.UI.GridCommandItem commandItem = (Telerik.Web.UI.GridCommandItem)eventArgs.Item;

                // PERFORM ANY INITIALIZATIONS OF THE TOOLBAR COMMAND ITEMS HERE 

            }

            return;

        }

        protected void MemberCaseGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            System.Data.DataTable masterTable = MemberCaseGridMasterTable;

            if ((MercuryApplication == null) || (Member == null) || (masterTable == null)) { return; }


            Boolean hasCaseManagementPermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.MemberCaseReview);



            switch (eventArgs.RebindReason) {

                case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                case Telerik.Web.UI.GridRebindReason.PostBackEvent:



                    #region Initialize Toolbar and Security
                    

                    #endregion


//                    masterTable.Rows.Clear ();

//                    MemberCaseGridMasterTable = masterTable;

//                    MemberCaseGrid.DataSource = masterTable;

                    MemberCaseGrid.DataSource = MercuryApplication.MemberCaseSummaryGetByMemberPage (Member.Id, 1, 999999, true);

                    MemberCaseGrid.MasterTableView.DetailTables[0].DataSource = MemberCaseGridCarePlanTable;

                    MemberCaseGrid.MasterTableView.DetailTables[0].DataBind ();

                    break;

            }


            return;

        }

        protected void MemberCaseGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Telerik.Web.UI.GridDataItem gridItem;

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            Int64 memberCaseId;

            Client.Core.Individual.Case.MemberCase memberCase;

            String responseScript = String.Empty;

            switch (eventArgs.CommandName) {

                case "ExpandCollapse":


                    #region Expand/Collapse

                    gridItem = (Telerik.Web.UI.GridDataItem)eventArgs.Item;

                    System.Data.DataTable carePlanTable = MemberCaseGridCarePlanTable;


                    memberCaseId = Convert.ToInt64 (gridItem.GetDataKeyValue ("Id"));


                    carePlanTable.Rows.Clear ();

                    foreach (Client.Core.Individual.Case.Views.MemberCaseCarePlanSummary currentPlanSummary in MercuryApplication.MemberCaseCarePlanSummaryGetByMemberCase (memberCaseId, false)) {

                        carePlanTable.Rows.Add (

                            currentPlanSummary.Id.ToString (),

                            currentPlanSummary.MemberCarePlanId.ToString (),

                            currentPlanSummary.ProblemStatementId.ToString (),

                            currentPlanSummary.CarePlanId.ToString (),

                            currentPlanSummary.ProviderId.ToString (),

                            currentPlanSummary.ProblemStatementText,

                            currentPlanSummary.CarePlanName,

                            currentPlanSummary.ProviderName,

                            // ...

                            currentPlanSummary.NextObjectiveName,

                            currentPlanSummary.NextInterventionName,

                            // ...

                            currentPlanSummary.EffectiveDate.ToString ("MM/dd/yyyy"),

                            currentPlanSummary.TerminationDate.ToString ("MM/dd/yyyy")

                            );

                    } /* END FOREACH */


                    #region Old Method

                    ////foreach (Client.QuickSilver.Services.Core.Application.MemberCarePlanSummaryDataView currentPlanSummary in MercuryApplication.MemberCarePlanGetSummaryByMemberCase (memberCaseId)) {

                    //foreach (Client.Core.Individual.Case.Views.MemberCaseCarePlanSummary currentPlanSummary in MercuryApplication.MemberCaseCarePlanSummaryGetByMemberCase (memberCaseId, false)) {

                    //    carePlanTable.Rows.Add (

                    //        currentPlanSummary.Id.ToString (),

                    //        //currentPlanSummary.MemberCaseId.ToString (),

                    //        currentPlanSummary.MemberCarePlanId.ToString (),

                    //        currentPlanSummary.ProblemStatementId.ToString (),

                    //        currentPlanSummary.CarePlanId.ToString (),

                    //        currentPlanSummary.ProviderId.ToString (),

                    //        currentPlanSummary.ProblemStatementText,

                    //        currentPlanSummary.CarePlanName,

                    //        currentPlanSummary.ProviderName,


                    //        currentPlanSummary.NextObjectiveName,

                    //        //(currentPlanSummary.NextObjectiveDate.HasValue) ? currentPlanSummary.NextObjectiveDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                    //        currentPlanSummary.NextInterventionName,

                    //        //(currentPlanSummary.NextInterventionDate.HasValue) ? currentPlanSummary.NextInterventionDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",


                    //        //// (currentPlanSummary.MemberAcceptanceDate.HasValue) ? currentPlanSummary.MemberAcceptanceDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                    //        //// (currentPlanSummary.ProviderAcceptanceDate.HasValue) ? currentPlanSummary.ProviderAcceptanceDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",



                    //        currentPlanSummary.EffectiveDate.ToString ("MM/dd/yyyy"),

                    //        currentPlanSummary.TerminationDate.ToString ("MM/dd/yyyy")

                    //        );

                    //}

                    #endregion


                    MemberCaseGridCarePlanTable = carePlanTable;

                    MemberCaseGrid.MasterTableView.DetailTables[0].DataSource = MemberCaseGridCarePlanTable;

                    MemberCaseGrid.MasterTableView.DetailTables[0].DataBind ();

                    break;

                    #endregion


                case "MemberCaseCreate":


                    #region Member Case Create

                    memberCase = MercuryApplication.MemberCaseCreate (Member.Id, false);

                    if (memberCase != null) {

                        // SUCCESSFULLY CREATE NEW MEMBER CASE

                        Response.Redirect (@"/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + memberCase.Id.ToString (), true);

                    }

                    else {

                        // ISSUE CREATING CASE, SEE IF IT WAS EXISTING CASE THAT NEEDS TO BE IGNORED

                        if (MercuryApplication.LastException != null) {

                            if ((MercuryApplication.LastExceptionMessage.Contains ("Existing"))

                                && (MercuryApplication.LastExceptionMessage.Contains ("found"))) {

                                // EXISTING CASE EXISTS, PROMPT USER TO IGNORE EXISTING CASE AND CREATE NEW ONE

                                TelerikAjaxManager.ResponseScripts.Add ("MemberCaseExistingWindow_Open ();");

                            }

                            else {

                                // OTHER UNHANDLED EXCEPTION OR PERMISSION DENIED OCCURRED

                                responseScript = @"alert(""" + MercuryApplication.LastExceptionMessage.Replace ('"', '\'') + @""");";

                                TelerikAjaxManager.ResponseScripts.Add (responseScript);

                            }

                        }

                    }

                    break;

                    #endregion


                default:

                    break;

            }

            return;

        }

        #endregion

        #region Dialog Windows Events

        protected void MemberCaseExistingWindow_ButtonOk_OnClick (Object sender, EventArgs e) {

            Client.Core.Individual.Case.MemberCase memberCase;

            String responseScript = String.Empty;


            memberCase = MercuryApplication.MemberCaseCreate (Member.Id, true);

            if (memberCase != null) {

                // SUCCESSFULLY CREATE NEW MEMBER CASE

                Response.Redirect (@"/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + memberCase.Id.ToString (), true);

            }

            else {

                // ISSUE CREATING CASE, SEE IF IT WAS EXISTING CASE THAT NEEDS TO BE IGNORED

                if (MercuryApplication.LastException != null) {

                    // OTHER UNHANDLED EXCEPTION OR PERMISSION DENIED OCCURRED

                    responseScript = "alert('" + MercuryApplication.LastExceptionMessage.Replace ("'", "''") + "');";

                    TelerikAjaxManager.ResponseScripts.Add (responseScript);

                }

                else {

                    // OTHER UNHANDLED EXCEPTION OR PERMISSION DENIED OCCURRED

                    responseScript = "alert('Unknown Exception Occurred. Unable to create new Member Case.');";

                    TelerikAjaxManager.ResponseScripts.Add (responseScript);

                }

            }


            TelerikAjaxManager.ResponseScripts.Add ("MemberCaseExistingWindow_Close ();");

            return;

        }

        #endregion

    }

}
