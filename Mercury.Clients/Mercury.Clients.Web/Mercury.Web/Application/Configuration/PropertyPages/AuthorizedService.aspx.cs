using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Configuration.PropertyPages {

    public partial class AuthorizedService : System.Web.UI.Page {


        #region Private Properties

        private const String ReviewPermission = Mercury.Server.EnvironmentPermissions.AuthorizedServiceReview;

        private const String ManagePermission = Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage;


        private Client.Core.AuthorizedServices.AuthorizedService authorizedService;

        #endregion


        #region Private Session Properties

        private String SessionCachePrefix {

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

            Int64 forAuthorizedServiceId = 0;


            if (MercuryApplication == null) { return; }

            if ((!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceReview))

                && (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage))) { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!Page.IsPostBack) {

                #region Initial Page Load

                if (Request.QueryString["AuthorizedServiceId"] != null) {

                    forAuthorizedServiceId = Int64.Parse (Request.QueryString["AuthorizedServiceId"]);

                }

                if (forAuthorizedServiceId != 0) {

                    authorizedService = MercuryApplication.AuthorizedServiceGet (forAuthorizedServiceId);

                    if (authorizedService == null) {

                        authorizedService = new Mercury.Client.Core.AuthorizedServices.AuthorizedService (MercuryApplication);

                    }

                }

                else {

                    authorizedService = new Mercury.Client.Core.AuthorizedServices.AuthorizedService (MercuryApplication);

                }

                InitializeAll ();

                Session[SessionCachePrefix + "AuthorizedService"] = authorizedService;

                Session[SessionCachePrefix + "AuthorizedServiceUnmodified"] = authorizedService.Copy ();

                #endregion

            } // Initial Page Load

            else { // Postback

                authorizedService = (Mercury.Client.Core.AuthorizedServices.AuthorizedService)Session[SessionCachePrefix + "AuthorizedService"];

            }

            ApplySecurity ();

            SaveResponseLabel.Text = String.Empty;

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

            InitializeDefinitionPage ();

            InitializeDefinitionGrid ();

            return;

        }


        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (authorizedService.Name)) { Page.Title = "Authorized Service - " + authorizedService.Name; } else { Page.Title = "Authorized Service"; }


            AuthorizedServiceName.Text = authorizedService.Name;

            AuthorizedServiceDescription.Text = authorizedService.Description;


            AuthorizedServiceEnabled.Checked = authorizedService.Enabled;

            AuthorizedServiceVisible.Checked = authorizedService.Visible;


            AuthorizedServiceCreateAuthorityName.Text = authorizedService.CreateAccountInfo.SecurityAuthorityName;

            AuthorizedServiceCreateAccountId.Text = authorizedService.CreateAccountInfo.UserAccountId;

            AuthorizedServiceCreateAccountName.Text = authorizedService.CreateAccountInfo.UserAccountName;

            AuthorizedServiceCreateDate.MinDate = DateTime.MinValue;

            AuthorizedServiceCreateDate.SelectedDate = authorizedService.CreateAccountInfo.ActionDate;


            AuthorizedServiceModifiedAuthorityName.Text = authorizedService.ModifiedAccountInfo.SecurityAuthorityName;

            AuthorizedServiceModifiedAccountId.Text = authorizedService.ModifiedAccountInfo.UserAccountId;

            AuthorizedServiceModifiedAccountName.Text = authorizedService.ModifiedAccountInfo.UserAccountName;

            AuthorizedServiceModifiedDate.MinDate = DateTime.MinValue;

            AuthorizedServiceModifiedDate.SelectedDate = authorizedService.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeDefinitionPage () {

            List<Client.Core.Authorizations.AuthorizationType> authorizationTypes = MercuryApplication.AuthorizationTypesAvailable (true);


            SortedDictionary<String, String> categories = new SortedDictionary<String, String> ();

            foreach (Client.Core.Authorizations.AuthorizationType currentType in authorizationTypes) {

                if (!categories.ContainsKey (currentType.Category)) {

                    categories.Add (currentType.Category, currentType.Category);

                }

            }


            AuthorizationCategory.Items.Clear ();

            AuthorizationCategory.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));

            foreach (String currentCategory in categories.Keys) {

                AuthorizationCategory.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentCategory, currentCategory));

            }


            AuthorizationSubcategory.Items.Clear ();

            AuthorizationSubcategory.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));


            AuthorizationServiceType.Items.Clear ();

            AuthorizationServiceType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));

            return;

        }

        protected void InitializeDefinitionGrid () {

            System.Data.DataTable definitionTable = new System.Data.DataTable ();

            definitionTable.Columns.Add ("DefinitionId");

            definitionTable.Columns.Add ("Criteria");


            foreach (Mercury.Client.Core.AuthorizedServices.AuthorizedServiceDefinition currentDefinition in authorizedService.Definitions) {

                definitionTable.Rows.Add (currentDefinition.Id.ToString (), currentDefinition.CriteriaDescription);

            }


            AuthorizedServiceDefinitionGrid.DataSource = definitionTable;

            AuthorizedServiceDefinitionGrid.DataBind ();


            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage);

            AuthorizedServiceName.ReadOnly = !hasManagePermission;

            AuthorizedServiceDescription.ReadOnly = !hasManagePermission;


            AuthorizedServiceEnabled.Enabled = hasManagePermission;

            AuthorizedServiceVisible.Enabled = hasManagePermission;


            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Authorization Type Events

        protected void AuthorizationCategory_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            List<Client.Core.Authorizations.AuthorizationType> authorizationTypes = MercuryApplication.AuthorizationTypesAvailable (true);


            SortedDictionary<String, String> subcategories = new SortedDictionary<String, String> ();

            foreach (Client.Core.Authorizations.AuthorizationType currentType in authorizationTypes) {

                if (!subcategories.ContainsKey (currentType.Subcategory) && (currentType.Category == AuthorizationCategory.SelectedValue)) {

                    subcategories.Add (currentType.Subcategory, currentType.Subcategory);

                }

            }


            AuthorizationSubcategory.Items.Clear ();

            AuthorizationSubcategory.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));

            foreach (String currentSubcategory in subcategories.Keys) {

                AuthorizationSubcategory.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentSubcategory, currentSubcategory));

            }

            AuthorizationServiceType.Items.Clear ();

            AuthorizationServiceType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));

            return;

        }

        protected void AuthorizationSubcategory_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs eventArgs) {

            List<Client.Core.Authorizations.AuthorizationType> authorizationTypes = MercuryApplication.AuthorizationTypesAvailable (true);


            SortedDictionary<String, String> serviceTypes = new SortedDictionary<String, String> ();

            foreach (Client.Core.Authorizations.AuthorizationType currentType in authorizationTypes) {

                if (!serviceTypes.ContainsKey (currentType.ServiceType) && (currentType.Subcategory == AuthorizationSubcategory.SelectedValue)) {

                    serviceTypes.Add (currentType.ServiceType, currentType.ServiceType);

                }

            }

            AuthorizationServiceType.Items.Clear ();

            AuthorizationServiceType.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("** All", String.Empty));

            foreach (String currentServiceType in serviceTypes.Keys) {

                AuthorizationServiceType.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentServiceType, currentServiceType));

            }

            return;

        }

        #endregion


        #region Definition Criteria Add/Delete

        protected void ButtonAddUpdateDefinition_OnClick (Object sender, EventArgs eventArgs) {

            Boolean hasValue = false;

            Client.Core.AuthorizedServices.AuthorizedServiceDefinition definition = null;

            Dictionary<String, String> validationResponse;

            if (MercuryApplication == null) { Response.RedirectLocation = "/PermissionDenied.aspx"; }


            #region All Medical Definition

            if (!String.IsNullOrEmpty (AuthorizationCategory.SelectedValue)) { hasValue = true; }

            if (!String.IsNullOrEmpty (AuthorizationSubcategory.SelectedValue)) { hasValue = true; }

            if (!String.IsNullOrEmpty (AuthorizationServiceType.SelectedValue)) { hasValue = true; }

            if (CriteriaPrincipalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

            if (CriteriaDiagnosisCodes.Text.Length != 0) { hasValue = true; }

            if (CriteriaRevenueCodes.Text.Length != 0) { hasValue = true; }

            if (CriteriaProcedureCodes.Text.Length != 0) { hasValue = true; }

            if (CriteriaAllMedicalProviderSpecialties.Text.Length != 0) { hasValue = true; }

            if (hasValue) {

                definition = new Mercury.Client.Core.AuthorizedServices.AuthorizedServiceDefinition (MercuryApplication);

                definition.Category = AuthorizationCategory.SelectedValue;

                definition.Subcategory = AuthorizationSubcategory.SelectedValue;

                definition.ServiceType = AuthorizationServiceType.SelectedValue;

                definition.PrincipalDiagnosisCriteria = CriteriaPrincipalDiagnosisCodes.Text;

                definition.PrincipalDiagnosisVersion = Convert.ToInt32 (CriteriaPrincipalDiagnosisVersion.SelectedValue);

                definition.DiagnosisCriteria = CriteriaDiagnosisCodes.Text;

                definition.DiagnosisVersion = Convert.ToInt32 (CriteriaDiagnosisVersion.SelectedValue);

                definition.RevenueCodeCriteria = CriteriaRevenueCodes.Text;

                definition.ProcedureCodeCriteria = CriteriaProcedureCodes.Text;

                definition.ProviderSpecialtyCriteria = CriteriaAllMedicalProviderSpecialties.Text;

            }

            else { SaveResponseLabel.Text = "No criteria specified for definition."; }

            #endregion


            if (definition != null) {

                validationResponse = definition.Validate ();

                if (validationResponse.Count == 0) {

                    switch (((System.Web.UI.WebControls.Button)sender).ID) {

                        case "ButtonAddDefinition": authorizedService.AddDefinition (definition); break;

                        case "ButtonUpdateDefinition":

                            if (AuthorizedServiceDefinitionGrid.SelectedItems.Count == 0) {

                                authorizedService.AddDefinition (definition);

                            }

                            else {

                                definition.SetAuthorizedServiceDefinitionId (authorizedService.Definitions[AuthorizedServiceDefinitionGrid.SelectedItems[0].DataSetIndex].Id);

                                authorizedService.Definitions.RemoveAt (AuthorizedServiceDefinitionGrid.SelectedItems[0].DataSetIndex);

                                authorizedService.AddDefinition (definition);

                                break;

                            }

                            break;
                    }

                    SaveResponseLabel.Text = String.Empty;

                }

                else {

                    foreach (String validationKey in validationResponse.Keys) {

                        SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                        break;

                    }

                }

            }

            InitializeDefinitionGrid ();

            return;

        }

        protected void AuthorizedServiceDefinitionGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            authorizedService.Definitions.RemoveAt (deleteIndex);

            InitializeDefinitionGrid ();

            return;

        }

        protected void AuthorizedServiceDefinitionGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    authorizedService.Definitions[itemIndex].Enabled = !authorizedService.Definitions[itemIndex].Enabled;

                    break;

            }

            InitializeDefinitionGrid ();

            return;

        }

        #endregion


        #region Dialog Button Event Handlers

        protected void ButtonPreview_OnClick (Object sender, EventArgs eventArgs) {

            System.Collections.Generic.List<Mercury.Server.Application.MemberAuthorizedServiceDetail> previewDetails;

            previewDetails = authorizedService.Preview (MercuryApplication);


            System.Data.DataTable previewTable = new System.Data.DataTable ();

            previewTable.Columns.Add ("AuthorizedServiceDefinitionId");

            previewTable.Columns.Add ("EventDate");

            previewTable.Columns.Add ("AuthorizationId");

            previewTable.Columns.Add ("ExternalAuthorizationId");

            previewTable.Columns.Add ("AuthorizationNumber");

            previewTable.Columns.Add ("AuthorizationLine");

            previewTable.Columns.Add ("MemberId");

            previewTable.Columns.Add ("ReferringProviderId");

            previewTable.Columns.Add ("ServiceProviderId");

            previewTable.Columns.Add ("AuthorizationCategory");

            previewTable.Columns.Add ("AuthorizationSubcategory");

            previewTable.Columns.Add ("AuthorizationServiceType");

            previewTable.Columns.Add ("AuthorizationStatus");

            previewTable.Columns.Add ("ReceivedDate");

            previewTable.Columns.Add ("ReferralDate");

            previewTable.Columns.Add ("EffectiveDate");

            previewTable.Columns.Add ("TerminationDate");

            previewTable.Columns.Add ("ServiceDate");

            previewTable.Columns.Add ("PrincipalDiagnosisCode");

            previewTable.Columns.Add ("PrincipalDiagnosisVersion");

            previewTable.Columns.Add ("DiagnosisCode");

            previewTable.Columns.Add ("DiagnosisVersion");

            previewTable.Columns.Add ("RevenueCode");

            previewTable.Columns.Add ("ProcedureCode");

            previewTable.Columns.Add ("ModifierCode");

            previewTable.Columns.Add ("NdcCode");

            previewTable.Columns.Add ("SpecialtyName");

            previewTable.Columns.Add ("Description");


            foreach (Mercury.Server.Application.MemberAuthorizedServiceDetail detail in previewDetails) {

                previewTable.Rows.Add (

                    detail.AuthorizedServiceDefinitionId, detail.EventDate.ToString ("MM/dd/yyyy"),

                    detail.AuthorizationId, detail.AuthorizationNumber, detail.ExternalAuthorizationId, detail.AuthorizationLine, detail.MemberId, detail.ReferringProviderId, detail.ServiceProviderId,

                    detail.AuthorizationCategory, detail.AuthorizationSubcategory, detail.AuthorizationServiceType,

                    detail.AuthorizationStatus,

                    (detail.ReceivedDate.HasValue) ? detail.ReceivedDate.Value.ToString ("MM/dd/yyyy") : null,

                    (detail.ReferralDate.HasValue) ? detail.ReferralDate.Value.ToString ("MM/dd/yyyy") : null,

                    detail.EffectiveDate.ToString ("MM/dd/yyyy"),

                    detail.TerminationDate.ToString ("MM/dd/yyyy"),

                    (detail.ServiceDate.HasValue) ? detail.ServiceDate.Value.ToString ("MM/dd/yyyy") : null,

                    detail.PrincipalDiagnosisCode, detail.PrincipalDiagnosisVersion, detail.DiagnosisCode, detail.DiagnosisVersion, detail.RevenueCode, detail.ProcedureCode, detail.ModifierCode, detail.NdcCode, detail.SpecialtyName,

                    detail.Description

                    );

            }



            AuthorizedServicePreviewGrid.DataSource = previewTable;

            AuthorizedServicePreviewGrid.DataBind ();


        }

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }


            Mercury.Client.Core.AuthorizedServices.AuthorizedService authorizedServiceUnmodified = (Mercury.Client.Core.AuthorizedServices.AuthorizedService)Session[SessionCachePrefix + "AuthorizedServiceUnmodified"];

            if (authorizedServiceUnmodified.Id == 0) { isModified = true; }


            authorizedService.Name = AuthorizedServiceName.Text.Trim ();

            authorizedService.Description = AuthorizedServiceDescription.Text.Trim ();

            authorizedService.Enabled = AuthorizedServiceEnabled.Checked;

            authorizedService.Visible = AuthorizedServiceVisible.Checked;

            if (!isModified) { isModified = !authorizedService.IsEqual (authorizedServiceUnmodified); }


            validationResponse = authorizedService.Validate ();

            isValid = (validationResponse.Count == 0);



            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                success = MercuryApplication.AuthorizedServiceSave (authorizedService);

                if (success) {

                    authorizedService = MercuryApplication.AuthorizedServiceGet (authorizedService.Id);

                    Session[SessionCachePrefix + "AuthorizedService"] = authorizedService;

                    Session[SessionCachePrefix + "AuthorizedServiceUnmodified"] = authorizedService.Copy ();

                    SaveResponseLabel.Text = "Save Successful";

                    InitializeAll ();

                }

                else {

                    SaveResponseLabel.Text = "Unable to Save.";

                    if (MercuryApplication.LastException != null) { SaveResponseLabel.Text = SaveResponseLabel.Text + " [" + MercuryApplication.LastException.Message + "]"; }

                    success = false;

                }

            }

            else if (!isModified) { SaveResponseLabel.Text = "No Changes Detected."; success = true; }

            else if (!isValid) {

                foreach (String validationKey in validationResponse.Keys) {

                    SaveResponseLabel.Text = "Invalid [" + validationKey + "]: " + validationResponse[validationKey];

                    break;

                }

                success = false;

            }

            return success;

        }

        protected void ButtonOk_OnClick (Object sender, EventArgs eventArgs) {

            Boolean success = false;

            if (MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.AuthorizedServiceManage)) {

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
