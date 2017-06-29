using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class ProblemStatement : System.Web.UI.Page {


        #region Private Properties

        private Client.Core.Individual.ProblemStatement problemStatement;

        #endregion


        #region Public Properties

        public String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (PageInstanceId.Text)) { PageInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return Form.Name + PageInstanceId.Text + ".";

            }

        }

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

            Int64 forProblemStatementId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                // CACHE AVAILABLE PROBLEM STATEMENT TREE

                List<Client.Core.Individual.ProblemStatement> problemStatementsAvailable = MercuryApplication.ProblemStatementsAvailable (false);

                #region Initial Page Load

                if (Request.QueryString["ProblemStatementId"] != null) {

                    forProblemStatementId = Int64.Parse (Request.QueryString["ProblemStatementId"]);

                }

                if (forProblemStatementId != 0) {

                    problemStatement = MercuryApplication.ProblemStatementGet (forProblemStatementId, false);

                    if (problemStatement == null) {

                        problemStatement = new Mercury.Client.Core.Individual.ProblemStatement (MercuryApplication);

                    }

                }

                else if (Request.QueryString["CopyProblemStatementId"] != null) {

                    forProblemStatementId = Int64.Parse (Request.QueryString["CopyProblemStatementId"]);

                    if (forProblemStatementId != 0) {

                        problemStatement = MercuryApplication.ProblemStatementGet (forProblemStatementId, false);

                        if (problemStatement == null) {

                            problemStatement = new Client.Core.Individual.ProblemStatement (MercuryApplication);

                        }

                        problemStatement.Id = 0;

                    }

                    else {

                        problemStatement = new Client.Core.Individual.ProblemStatement (MercuryApplication);

                    }

                }

                else {

                    problemStatement = new Mercury.Client.Core.Individual.ProblemStatement (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "ProblemStatement"] = problemStatement;

                Session[SessionCachePrefix + "ProblemStatementUnmodified"] = problemStatement.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                problemStatement = (Mercury.Client.Core.Individual.ProblemStatement)Session[SessionCachePrefix + "ProblemStatement"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (problemStatement.Name)) { Page.Title = "Problem Statement - " + problemStatement.Name; } else { Page.Title = "Problem Statement"; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeClassification ();

            InitializeCarePlans ();

            return;

        }

        protected void InitializeClassification () {
            
            List<Client.Core.Individual.ProblemStatement> problemStatementsAvailable = MercuryApplication.ProblemStatementsAvailable (true);


            #region Problem Domains

            var problemDomains =

                from currentProblemDomain in MercuryApplication.ProblemDomainsAvailable (false)

                orderby currentProblemDomain.Name

                select currentProblemDomain;


            ProblemStatementDomainSelection.DataSource = problemDomains;

            ProblemStatementDomainSelection.DataTextField = "Name";

            ProblemStatementDomainSelection.DataValueField = "Id";

            ProblemStatementDomainSelection.DataBind ();

            if (problemStatement.ProblemDomainId != 0) { ProblemStatementDomainSelection.SelectedValue = problemStatement.ProblemDomainId.ToString (); }

            #endregion 
            

            #region Problem Class

            var problemClasses =

                from currentProblemClass in MercuryApplication.ProblemClassesAvailable (false)

                where (currentProblemClass.ProblemDomainId == problemStatement.ProblemDomainId)

                orderby currentProblemClass.Name

                select currentProblemClass;


            ProblemStatementClassSelection.DataSource = problemClasses;

            ProblemStatementClassSelection.DataTextField = "Name";

            ProblemStatementClassSelection.DataValueField = "Id";

            ProblemStatementClassSelection.DataBind ();

            if (problemStatement.ProblemClassId != 0) { ProblemStatementClassSelection.SelectedValue = problemStatement.ProblemClassId.ToString (); }


            #endregion 


            return;

        }

        protected void InitializeCarePlans () {

            ProblemStatementDefaultCarePlanSelection.Items.Clear ();

            ProblemStatementDefaultCarePlanSelection.DataSource = MercuryApplication.CarePlansAvailable (false);

            ProblemStatementDefaultCarePlanSelection.DataTextField = "Name";

            ProblemStatementDefaultCarePlanSelection.DataValueField = "Id";

            ProblemStatementDefaultCarePlanSelection.DataBind ();


            if (problemStatement.DefaultCarePlanId != 0) {

                ProblemStatementDefaultCarePlanSelection.SelectedValue = problemStatement.DefaultCarePlanId.ToString ();

            }

            return;

        }

        protected void InitializeGeneralPage () {

            ProblemStatementName.Text = problemStatement.Name;

            ProblemStatementDescription.Text = problemStatement.Description;


            ProblemStatementDefiningCharacteristics.Text = problemStatement.DefiningCharacteristics;

            ProblemStatementRelatedFactors.Text = problemStatement.RelatedFactors;


            ProblemStatementEnabled.Checked = problemStatement.Enabled;

            ProblemStatementVisible.Checked = problemStatement.Visible;


            ProblemStatementCreateAuthorityName.Text = problemStatement.CreateAccountInfo.SecurityAuthorityName;

            ProblemStatementCreateAccountId.Text = problemStatement.CreateAccountInfo.UserAccountId;

            ProblemStatementCreateAccountName.Text = problemStatement.CreateAccountInfo.UserAccountName;

            ProblemStatementCreateDate.MinDate = DateTime.MinValue;

            ProblemStatementCreateDate.SelectedDate = problemStatement.CreateAccountInfo.ActionDate;


            ProblemStatementModifiedAuthorityName.Text = problemStatement.ModifiedAccountInfo.SecurityAuthorityName;

            ProblemStatementModifiedAccountId.Text = problemStatement.ModifiedAccountInfo.UserAccountId;

            ProblemStatementModifiedAccountName.Text = problemStatement.ModifiedAccountInfo.UserAccountName;

            ProblemStatementModifiedDate.MinDate = DateTime.MinValue;

            ProblemStatementModifiedDate.SelectedDate = problemStatement.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage);

            ProblemStatementName.ReadOnly = !hasManagePermission;

            ProblemStatementDescription.ReadOnly = !hasManagePermission;


            ProblemStatementEnabled.Enabled = hasManagePermission;

            ProblemStatementVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Classification Events

        protected void ProblemStatementDomainOnChange () {

            if (ProblemStatementDomainSelection.SelectedItem != null) {

                problemStatement.ProblemDomainId = Convert.ToInt64 (ProblemStatementDomainSelection.SelectedValue);

                problemStatement.ProblemDomainName = ProblemStatementDomainSelection.SelectedItem.Text;

            }

            else {

                problemStatement.ProblemDomainId = 0;

                problemStatement.ProblemDomainName = ProblemStatementDomainSelection.Text;

            }

            problemStatement.ProblemClassId = 0;

            problemStatement.ProblemClassName = String.Empty;


            InitializeClassification ();

            return;

        }

        protected void ProblemStatementDomainSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            ProblemStatementDomainOnChange ();

            return;

        }

        protected void ProblemStatementDomainSelection_OnTextChanged (Object sender, EventArgs e) {

            ProblemStatementDomainOnChange ();

            return;

        }

        //protected void ProblemStatementCategoryOnChange () {

        //    if (ProblemStatementCategorySelection.SelectedItem != null) {

        //        problemStatement.ProblemCategoryId = Convert.ToInt64 (ProblemStatementCategorySelection.SelectedValue);

        //        problemStatement.ProblemCategoryName = ProblemStatementCategorySelection.SelectedItem.Text;

        //    }

        //    else {

        //        problemStatement.ProblemCategoryId = 0;

        //        problemStatement.ProblemCategoryName = ProblemStatementCategorySelection.Text;

        //    }

        //    problemStatement.ProblemSubcategoryId = 0;

        //    problemStatement.ProblemSubcategoryName = String.Empty;


        //    ProblemStatementSubcategorySelection.SelectedValue = String.Empty;

        //    ProblemStatementSubcategorySelection.Text = String.Empty;


        //    InitializeClassification ();

        //    return;

        //}

        //protected void ProblemStatementCategorySelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

        //    ProblemStatementCategoryOnChange ();

        //    return;

        //}

        //protected void ProblemStatementCategorySelection_OnTextChanged (Object sender, EventArgs e) {

        //    ProblemStatementCategoryOnChange ();

        //    return;

        //}

        #endregion 


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Core.Individual.ProblemStatement problemStatementUnmodified = (Mercury.Client.Core.Individual.ProblemStatement)Session[SessionCachePrefix + "ProblemStatementUnmodified"];

            if (problemStatementUnmodified.Id == 0) { isModified = true; }

            
            // CLASSIFICATION 

            if (ProblemStatementDomainSelection.SelectedItem != null) {

                problemStatement.ProblemDomainId = Convert.ToInt64 (ProblemStatementDomainSelection.SelectedItem.Value);

            }

            problemStatement.ProblemDomainName = ProblemStatementDomainSelection.Text.Trim ();



            if (ProblemStatementClassSelection.SelectedItem != null) {

                problemStatement.ProblemClassId = Convert.ToInt64 (ProblemStatementClassSelection.SelectedItem.Value);

            }

            problemStatement.ProblemClassName = ProblemStatementClassSelection.Text.Trim ();


            //if (ProblemStatementCategorySelection.SelectedItem != null) {

            //    problemStatement.ProblemCategoryId = Convert.ToInt64 (ProblemStatementCategorySelection.SelectedItem.Value);

            //}

            //problemStatement.ProblemCategoryName = ProblemStatementCategorySelection.Text;


            //if (ProblemStatementSubcategorySelection.SelectedItem != null) {

            //    problemStatement.ProblemSubcategoryId = Convert.ToInt64 (ProblemStatementSubcategorySelection.SelectedItem.Value);

            //}

            //problemStatement.ProblemSubcategoryName = ProblemStatementSubcategorySelection.Text;



            problemStatement.Name = ProblemStatementName.Text.Trim ();

            problemStatement.Description = ProblemStatementDescription.Text.Trim ();

            problemStatement.DefiningCharacteristics = ProblemStatementDefiningCharacteristics.Text.Trim ();

            problemStatement.RelatedFactors = ProblemStatementRelatedFactors.Text.Trim ();

            problemStatement.Enabled = ProblemStatementEnabled.Checked;

            problemStatement.Visible = ProblemStatementVisible.Checked;


            if (ProblemStatementDefaultCarePlanSelection.SelectedItem != null) {

                problemStatement.DefaultCarePlanId = Convert.ToInt64 (ProblemStatementDefaultCarePlanSelection.SelectedValue);

            }


            if (!isModified) { isModified = !problemStatement.IsEqual (problemStatementUnmodified); }

            if (isModified) {

                success = MercuryApplication.ProblemStatementSave (problemStatement);

                if (success) {

                    problemStatement = MercuryApplication.ProblemStatementGet (problemStatement.Id, false);

                    Session[SessionCachePrefix + "ProblemStatement"] = problemStatement;

                    Session[SessionCachePrefix + "ProblemStatementUnmodified"] = problemStatement.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ProblemStatementManage)) {

                success = ApplyChanges ();

            }

            else {

                success = true;

            }


            if (success) {

                Server.Transfer ("/WindowClose.aspx");

            }

            return;

        }

        protected void ButtonApply_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = ApplyChanges ();

            return;

        }

        protected void ButtonCancel_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

            return;

        }

        #endregion

    }

}
