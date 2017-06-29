using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class CareMeasure : System.Web.UI.Page {


        #region Private Properties

        private Client.Core.Individual.CareMeasure careMeasure;

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

            Int64 forCareMeasureId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                // CACHE AVAILABLE PROBLEM STATEMENT TREE

                List<Client.Core.Individual.CareMeasure> careMeasuresAvailable = MercuryApplication.CareMeasuresAvailable (false);

                #region Initial Page Load

                if (Request.QueryString["CareMeasureId"] != null) {

                    forCareMeasureId = Int64.Parse (Request.QueryString["CareMeasureId"]);

                }

                if (forCareMeasureId != 0) {

                    careMeasure = MercuryApplication.CareMeasureGet (forCareMeasureId, false);

                    if (careMeasure == null) {

                        careMeasure = new Mercury.Client.Core.Individual.CareMeasure (MercuryApplication);

                    }

                }

                else {

                    careMeasure = new Mercury.Client.Core.Individual.CareMeasure (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "CareMeasure"] = careMeasure;

                Session[SessionCachePrefix + "CareMeasureUnmodified"] = careMeasure.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                careMeasure = (Mercury.Client.Core.Individual.CareMeasure)Session[SessionCachePrefix + "CareMeasure"];

            }

            ApplySecurity ();

            if (!String.IsNullOrEmpty (careMeasure.Name)) { Page.Title = "CareMeasure  - " + careMeasure.Name; } else { Page.Title = "CareMeasure "; }

            return;

        }

        protected void Page_Unload (object sender, EventArgs e) {

            if (MercuryApplication != null) {

                MercuryApplication.ApplicationClientClose ();

            }

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeAll () {

            InitializeGeneralPage ();

            InitializeClassification ();

            InitializeComponentPage ();

            return;

        }

        protected void InitializeClassification () {

            List<Client.Core.Individual.CareMeasure> careMeasuresAvailable = MercuryApplication.CareMeasuresAvailable (false);


            #region CareMeasure Domains

            var careMeasureDomains =

                from currentCareMeasureDomain in MercuryApplication.CareMeasureDomainsAvailable (false)

                orderby currentCareMeasureDomain.Name

                select currentCareMeasureDomain;


            CareMeasureDomainSelection.DataSource = careMeasureDomains;

            CareMeasureDomainSelection.DataTextField = "Name";

            CareMeasureDomainSelection.DataValueField = "Id";

            CareMeasureDomainSelection.DataBind ();

            if (careMeasure.CareMeasureDomainId != 0) { CareMeasureDomainSelection.SelectedValue = careMeasure.CareMeasureDomainId.ToString (); }

            #endregion


            #region CareMeasure Class

            var careMeasureClasses =

                from currentCareMeasureClass in MercuryApplication.CareMeasureClassesAvailable (false)

                where (currentCareMeasureClass.CareMeasureDomainId == careMeasure.CareMeasureDomainId)

                orderby currentCareMeasureClass.Name

                select currentCareMeasureClass;


            CareMeasureClassSelection.DataSource = careMeasureClasses;

            CareMeasureClassSelection.DataTextField = "Name";

            CareMeasureClassSelection.DataValueField = "Id";

            CareMeasureClassSelection.DataBind ();

            if (careMeasure.CareMeasureClassId != 0) { CareMeasureClassSelection.SelectedValue = careMeasure.CareMeasureClassId.ToString (); }


            #endregion


            return;

        }

        protected void InitializeGeneralPage () {

            CareMeasureName.Text = careMeasure.Name;

            CareMeasureDescription.Text = careMeasure.Description;


            CareMeasureEnabled.Checked = careMeasure.Enabled;

            CareMeasureVisible.Checked = careMeasure.Visible;


            CareMeasureCreateAuthorityName.Text = careMeasure.CreateAccountInfo.SecurityAuthorityName;

            CareMeasureCreateAccountId.Text = careMeasure.CreateAccountInfo.UserAccountId;

            CareMeasureCreateAccountName.Text = careMeasure.CreateAccountInfo.UserAccountName;

            CareMeasureCreateDate.MinDate = DateTime.MinValue;

            CareMeasureCreateDate.SelectedDate = careMeasure.CreateAccountInfo.ActionDate;


            CareMeasureModifiedAuthorityName.Text = careMeasure.ModifiedAccountInfo.SecurityAuthorityName;

            CareMeasureModifiedAccountId.Text = careMeasure.ModifiedAccountInfo.UserAccountId;

            CareMeasureModifiedAccountName.Text = careMeasure.ModifiedAccountInfo.UserAccountName;

            CareMeasureModifiedDate.MinDate = DateTime.MinValue;

            CareMeasureModifiedDate.SelectedDate = careMeasure.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeComponentPage () {

            InitializeCareMeasureComponentsGrid ();


            CareMeasureComponentScaleSelection.DataSource = MercuryApplication.CareMeasureScalesAvailable (false);

            CareMeasureComponentScaleSelection.DataTextField = "Name";

            CareMeasureComponentScaleSelection.DataValueField = "Id";

            CareMeasureComponentScaleSelection.DataBind ();


            return;

        }

        protected void InitializeCareMeasureComponentsGrid () {

            CareMeasureComponentsGrid.DataSource = careMeasure.Components;

            CareMeasureComponentsGrid.DataBind ();

            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureManage);

            CareMeasureName.ReadOnly = !hasManagePermission;

            CareMeasureDescription.ReadOnly = !hasManagePermission;


            CareMeasureEnabled.Enabled = hasManagePermission;

            CareMeasureVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Classification Events

        protected void CareMeasureDomainOnChange () {

            if (CareMeasureDomainSelection.SelectedItem != null) {

                careMeasure.CareMeasureDomainId = Convert.ToInt64 (CareMeasureDomainSelection.SelectedValue);

                careMeasure.CareMeasureDomainName = CareMeasureDomainSelection.SelectedItem.Text;

            }

            else {

                careMeasure.CareMeasureDomainId = 0;

                careMeasure.CareMeasureDomainName = CareMeasureDomainSelection.Text;

            }

            careMeasure.CareMeasureClassId = 0;

            careMeasure.CareMeasureClassName = String.Empty;


            InitializeClassification ();

            return;

        }

        protected void CareMeasureDomainSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            CareMeasureDomainOnChange ();

            return;

        }

        protected void CareMeasureDomainSelection_OnTextChanged (Object sender, EventArgs e) {

            CareMeasureDomainOnChange ();

            return;

        }

        #endregion


        #region Goal Grid Events

        protected void ButtonAddUpdateCareMeasureComponent_OnClick (Object sender, EventArgs eventArgs) {

            if (MercuryApplication == null) { return; }


            Boolean existingGoalFound = false;

            Boolean duplicateName = false;

            Client.Core.Individual.CareMeasureComponent careMeasureComponent = null;

            Dictionary<String, String> validationResponse;


            // CREATE NEW GOAL OBJECT AN ASSOCIATE WITH PARENT GOAL

            careMeasureComponent = new Client.Core.Individual.CareMeasureComponent (MercuryApplication);

            careMeasureComponent.CareMeasureId = careMeasure.Id;


            // ASSIGN PROPERTIES FROM CONTROLS INTO OBJECT

            careMeasureComponent.Name = CareMeasureComponentName.Text;

            careMeasureComponent.Description = CareMeasureComponentName.Text;

            careMeasureComponent.Tag = CareMeasureComponentTag.Text;

            careMeasureComponent.Enabled = CareMeasureComponentEnabled.Checked;

            if (CareMeasureComponentScaleSelection.SelectedItem != null) {

                careMeasureComponent.CareMeasureScaleId = Convert.ToInt64 (CareMeasureComponentScaleSelection.SelectedValue);

            }


            validationResponse = careMeasureComponent.Validate ();

            if (validationResponse.Count == 0) {

                SaveResponseLabel.Text = String.Empty;

                foreach (Client.Core.Individual.CareMeasureComponent currentCareMeasureComponent in careMeasure.Components) {

                    if (currentCareMeasureComponent.IsEqual (careMeasureComponent)) { existingGoalFound = true; break; }

                    if (currentCareMeasureComponent.Name.ToUpper () == careMeasureComponent.Name.ToUpper ()) {

                        duplicateName = true;

                    }

                }

                switch (((System.Web.UI.WebControls.Button)sender).ID) {

                    case "CareMeasureComponentAdd":

                        if ((!existingGoalFound) && (!duplicateName)) {

                            careMeasure.Components.Add (careMeasureComponent);

                        }

                        else { SaveResponseLabel.Text = "Duplicate Goal Exists."; }

                        break;

                    case "CareMeasureComponentUpdate":

                        if (CareMeasureComponentsGrid.SelectedItems.Count != 0) {

                            careMeasureComponent.Id = careMeasure.Components[CareMeasureComponentsGrid.SelectedItems[0].DataSetIndex].Id;

                            careMeasure.Components.RemoveAt (CareMeasureComponentsGrid.SelectedItems[0].DataSetIndex);

                            careMeasure.Components.Add (careMeasureComponent);

                        }

                        break;

                }

            }

            else {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

            }


            InitializeCareMeasureComponentsGrid ();

            return;

        }

        protected void CareMeasureComponentsGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            careMeasure.Components.RemoveAt (deleteIndex);

            InitializeCareMeasureComponentsGrid ();

            return;

        }

        protected void CareMeasureComponentsGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    careMeasure.Components[itemIndex].Enabled = !careMeasure.Components[itemIndex].Enabled;

                    break;

                case Telerik.Web.UI.RadGrid.EditCommandName:

                    Client.Core.Individual.CareMeasureComponent careMeasureComponent = careMeasure.Components[eventArgs.Item.ItemIndex];


                    CareMeasureComponentName.Text = careMeasureComponent.Name;

                    CareMeasureComponentScaleSelection.SelectedValue = careMeasureComponent.CareMeasureScaleId.ToString ();

                    CareMeasureComponentTag.Text = careMeasureComponent.Tag;

                    CareMeasureComponentEnabled.Checked = careMeasureComponent.Enabled;


                    eventArgs.Canceled = true;

                    break;

            }

            InitializeCareMeasureComponentsGrid ();

            CareMeasureComponentsGrid.SelectedIndexes.Add (itemIndex);

            return;

        }

        #endregion 


        #region Dialog Button Event Handlers

        protected Boolean ApplyChanges () {

            Boolean isModified = false;

            Boolean success = false;


            Mercury.Client.Core.Individual.CareMeasure careMeasureUnmodified = (Mercury.Client.Core.Individual.CareMeasure)Session[SessionCachePrefix + "CareMeasureUnmodified"];

            if (careMeasureUnmodified.Id == 0) { isModified = true; }


            // CLASSIFICATION 

            if (CareMeasureDomainSelection.SelectedItem != null) {

                careMeasure.CareMeasureDomainId = Convert.ToInt64 (CareMeasureDomainSelection.SelectedItem.Value);

            }

            careMeasure.CareMeasureDomainName = CareMeasureDomainSelection.Text.Trim ();



            if (CareMeasureClassSelection.SelectedItem != null) {

                careMeasure.CareMeasureClassId = Convert.ToInt64 (CareMeasureClassSelection.SelectedItem.Value);

            }

            careMeasure.CareMeasureClassName = CareMeasureClassSelection.Text.Trim ();

        
            careMeasure.Name = CareMeasureName.Text.Trim ();

            careMeasure.Description = CareMeasureDescription.Text.Trim ();

            careMeasure.Enabled = CareMeasureEnabled.Checked;

            careMeasure.Visible = CareMeasureVisible.Checked;

            
            if (!isModified) { isModified = !careMeasure.IsEqual (careMeasureUnmodified); }

            if (isModified) {

                success = MercuryApplication.CareMeasureSave (careMeasure);

                if (success) {

                    careMeasure = MercuryApplication.CareMeasureGet (careMeasure.Id, false);

                    Session[SessionCachePrefix + "CareMeasure"] = careMeasure;

                    Session[SessionCachePrefix + "CareMeasureUnmodified"] = careMeasure.Copy ();

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

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.CareMeasureManage)) {

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
