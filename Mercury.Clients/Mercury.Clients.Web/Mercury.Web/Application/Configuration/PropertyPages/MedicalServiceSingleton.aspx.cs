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

namespace Mercury.Web.Application.Configuration.Windows {

    public partial class MedicalServiceSingleton : System.Web.UI.Page {

        #region Private Propreties

        private const String ReviewPermission = Mercury.Server.EnvironmentPermissions.MedicalServiceReview;

        private const String ManagePermission = Mercury.Server.EnvironmentPermissions.MedicalServiceManage;


        private Mercury.Client.Core.MedicalServices.ServiceSingleton serviceSingleton;

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

            if (MercuryApplication == null) { return; }


            if ((!MercuryApplication.HasEnvironmentPermission (ReviewPermission))
            
               && (!MercuryApplication.HasEnvironmentPermission (ManagePermission)))

                { Response.Redirect ("/PermissionDenied.aspx", true); return; }


            if (!MercuryApplication.HasEnvironmentPermission (ManagePermission)) {

                ButtonApply.Enabled = false;

                ButtonOk.Enabled = false;

            }

            else {

                ButtonApply.Click += new EventHandler (this.ButtonApply_OnClick);

                ButtonOk.Click += new EventHandler (this.ButtonOk_OnClick);

            }

            ButtonCancel.Click += new EventHandler (this.ButtonCancel_OnClick);

            InitializeProperties ();

        }

        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }

        #endregion


        #region Initialization

        protected void InitializeProperties () {

            Int64 serviceId = 0;


            if ((MercuryApplication != null) && (!IsPostBack)) {

                if (Request.QueryString["ServiceId"] != null) {

                    serviceId = Int64.Parse (Request.QueryString["ServiceId"]);

                }

                if (serviceId != 0) {

                    serviceSingleton = MercuryApplication.MedicalServiceSingletonGet (serviceId);

                    if (serviceSingleton == null) {

                        serviceSingleton = new Mercury.Client.Core.MedicalServices.ServiceSingleton (MercuryApplication);

                    }

                    Page.Title = "Service Singleton - " + serviceSingleton.Name;

                }

                else {

                    serviceSingleton = new Mercury.Client.Core.MedicalServices.ServiceSingleton (MercuryApplication);

                }

                InitializeGeneralPage ();

                InitializeDefinitionPage ();

                InitializeDefinitionGrid ();

                Session[SessionCachePrefix + "ServiceSingleton"] = serviceSingleton;

                Session[SessionCachePrefix + "ServiceSingletonUnmodified"] = serviceSingleton.Copy ();

            }

            else { // Postback

                serviceSingleton = (Mercury.Client.Core.MedicalServices.ServiceSingleton) Session[SessionCachePrefix + "ServiceSingleton"];

            }

            ApplySecurity ();

            return;

        }

        protected void InitializeGeneralPage () {

            if (!String.IsNullOrEmpty (serviceSingleton.Name)) { Page.Title = "Singleton - " + serviceSingleton.Name; } else { Page.Title = "New Singleton"; }

            SingletonName.Text = serviceSingleton.Name;

            SingletonDescription.Text = serviceSingleton.Description;

            SingletonClassification.SelectedValue = ((Int32) serviceSingleton.ServiceClassification).ToString ();

            SingletonEnabled.Checked = serviceSingleton.Enabled;

            SingletonVisible.Checked = serviceSingleton.Visible;

            // SingletonLastPaidDate.MinDate = DateTime.MinValue;

            SingletonLastPaidDate.SelectedDate = serviceSingleton.LastPaidDate;


            SingletonCreateAuthorityName.Text = serviceSingleton.CreateAccountInfo.SecurityAuthorityName;

            SingletonCreateAccountId.Text = serviceSingleton.CreateAccountInfo.UserAccountId;

            SingletonCreateAccountName.Text = serviceSingleton.CreateAccountInfo.UserAccountName;

            SingletonCreateDate.MinDate = DateTime.MinValue;

            SingletonCreateDate.SelectedDate = serviceSingleton.CreateAccountInfo.ActionDate;


            SingletonModifiedAuthorityName.Text = serviceSingleton.ModifiedAccountInfo.SecurityAuthorityName;

            SingletonModifiedAccountId.Text = serviceSingleton.ModifiedAccountInfo.UserAccountId;

            SingletonModifiedAccountName.Text = serviceSingleton.ModifiedAccountInfo.UserAccountName;

            SingletonModifiedDate.MinDate = DateTime.MinValue;
            
            SingletonModifiedDate.SelectedDate = serviceSingleton.ModifiedAccountInfo.ActionDate;

            return;

        }

        protected void InitializeDefinitionPage () {

            CriteriaLabMetricSelection.Items.Clear ();

            CriteriaLabMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem ("* Not Assigned", "0"));

            foreach (Client.Core.Metrics.Metric currentMetric in MercuryApplication.MetricsAvailable (false)) {

                if (currentMetric.Enabled) {

                    CriteriaLabMetricSelection.Items.Add (new Telerik.Web.UI.RadComboBoxItem (currentMetric.Name, currentMetric.Id.ToString ()));

                }

            }

            return;

        }

        protected void InitializeDefinitionGrid () {

            System.Data.DataTable definitionTable = new DataTable ();

            definitionTable.Columns.Add ("DefinitionId");

            definitionTable.Columns.Add ("DefinitionType");

            definitionTable.Columns.Add ("Criteria");


            foreach (Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition in serviceSingleton.Definitions) {

                definitionTable.Rows.Add (singletonDefinition.Id, singletonDefinition.DataSourceType.ToString (), singletonDefinition.Criteria ());

            }


            ServiceDefinitionGrid.DataSource = definitionTable;

            ServiceDefinitionGrid.DataBind ();


            return;

        }

        protected void ApplySecurity () {

            Boolean hasManagePermission = MercuryApplication.HasEnvironmentPermission (ManagePermission);


            SingletonName.ReadOnly = !hasManagePermission;

            SingletonDescription.ReadOnly = !hasManagePermission;

            SingletonEnabled.Enabled = hasManagePermission;

            SingletonVisible.Enabled = hasManagePermission;

            SingletonLastPaidDate.ReadOnly = !hasManagePermission;


            AddDefinitionDiv.Style.Add ("display", ((hasManagePermission) ? "block;" : "none;"));

            ButtonAddDefinition.Visible = hasManagePermission;

            ButtonCancel.Visible = hasManagePermission;

            ButtonApply.Visible = hasManagePermission;

            return;

        }

        #endregion


        #region Definition Criteria Add/Delete

        protected void ButtonAddUpdateDefinition_OnClick (Object sender, EventArgs eventArgs) {

            Boolean hasValue = false;

            Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition = null;

            Dictionary<String, String> validationResponse;

            if (MercuryApplication == null) { Response.RedirectLocation = "/PermissionDenied.aspx"; }


            switch (ServiceTypeSelection.SelectedValue) {

                case "CustomCriteria":

                    if (!String.IsNullOrEmpty (CriteriaCustomStoredProcedureName.Text)) { hasValue = true; }

                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Custom;

                        singletonDefinition.CustomCriteria = CriteriaCustomStoredProcedureName.Text;

                    }

                    break;

                case "AllMedical":

                    #region All Medical Definition

                    if (CriteriaAllMedicalPrincipalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaAllMedicalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaAllMedicalProcedureCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaAllMedicalModifierCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaAllMedicalProviderSpecialties.Text.Length != 0) { hasValue = true; }

                    if (CriteriaAllMedicalUseMemberAgeCriteria.Checked) { hasValue = true; }

                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.AllMedical;

                        singletonDefinition.EventDateOrder = (Mercury.Server.Application.EventDateOrder) (Int32.Parse (CriteriaAllMedicalEventDateOrder.SelectedValue));

                        singletonDefinition.PrincipalDiagnosisCriteria = CriteriaAllMedicalPrincipalDiagnosisCodes.Text;

                        singletonDefinition.DiagnosisCriteria = CriteriaAllMedicalDiagnosisCodes.Text;

                        singletonDefinition.ProcedureCodeCriteria = CriteriaAllMedicalProcedureCodes.Text;

                        singletonDefinition.ModifierCodeCriteria = CriteriaAllMedicalModifierCodes.Text;

                        singletonDefinition.ProviderSpecialtyCriteria = CriteriaAllMedicalProviderSpecialties.Text;

                        singletonDefinition.IsPcpRequiredCriteria = Convert.ToBoolean (CriteriaAllMedicalPcpPerformedService.SelectedValue);

                        singletonDefinition.UseMemberAgeCriteria = CriteriaAllMedicalUseMemberAgeCriteria.Checked;

                        singletonDefinition.MemberAgeDateQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (CriteriaAllMedicalMemberAgeDateQualifier.SelectedValue);

                        singletonDefinition.MemberAgeMinimum = (CriteriaAllMedicalMemberAgeMinimum.Value.HasValue) ? Convert.ToInt32 (CriteriaAllMedicalMemberAgeMinimum.Value.Value) : 0;

                        singletonDefinition.MemberAgeMaximum = (CriteriaAllMedicalMemberAgeMaximum.Value.HasValue) ? Convert.ToInt32 (CriteriaAllMedicalMemberAgeMaximum.Value.Value) : 0;

                    }
                   
                    #endregion

                    break;

                case "Professional":

                    #region Professional Definition

                    if (CriteriaProfessionalPrincipalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalServiceLocations.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalProcedureCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalModifierCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalProviderSpecialties.Text.Length != 0) { hasValue = true; }

                    if (CriteriaProfessionalUseMemberAgeCriteria.Checked) { hasValue = true; }

                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Professional;

                        singletonDefinition.EventDateOrder = (Mercury.Server.Application.EventDateOrder) (Int32.Parse (CriteriaProfessionalEventDateOrder.SelectedValue));

                        singletonDefinition.PrincipalDiagnosisCriteria = CriteriaProfessionalPrincipalDiagnosisCodes.Text;

                        singletonDefinition.DiagnosisCriteria = CriteriaProfessionalDiagnosisCodes.Text;

                        singletonDefinition.LocationCodeCriteria = CriteriaProfessionalServiceLocations.Text;

                        singletonDefinition.ProcedureCodeCriteria = CriteriaProfessionalProcedureCodes.Text;

                        singletonDefinition.ModifierCodeCriteria = CriteriaProfessionalModifierCodes.Text;

                        singletonDefinition.ProviderSpecialtyCriteria = CriteriaProfessionalProviderSpecialties.Text;

                        singletonDefinition.IsPcpRequiredCriteria = Convert.ToBoolean (CriteriaProfessionalPcpPerformedService.SelectedValue);

                        singletonDefinition.UseMemberAgeCriteria = CriteriaProfessionalUseMemberAgeCriteria.Checked;

                        singletonDefinition.MemberAgeDateQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (CriteriaProfessionalMemberAgeDateQualifier.SelectedValue);

                        singletonDefinition.MemberAgeMinimum = (CriteriaProfessionalMemberAgeMinimum.Value.HasValue) ? Convert.ToInt32 (CriteriaProfessionalMemberAgeMinimum.Value.Value) : 0;

                        singletonDefinition.MemberAgeMaximum = (CriteriaProfessionalMemberAgeMaximum.Value.HasValue) ? Convert.ToInt32 (CriteriaProfessionalMemberAgeMaximum.Value.Value) : 0;

                    }

                    #endregion

                    break;


                case "Institutional":

                    #region Institutional Definition

                    if (CriteriaInstitutionalPrincipalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalDiagnosisCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalDrgCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalIcd9Codes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalBillTypes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalRevenueCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalProcedureCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalModifierCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalProviderSpecialties.Text.Length != 0) { hasValue = true; }

                    if (CriteriaInstitutionalUseMemberAgeCriteria.Checked) { hasValue = true; }

                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Institutional;

                        singletonDefinition.EventDateOrder = (Mercury.Server.Application.EventDateOrder) (Int32.Parse (CriteriaInstitutionalEventDateOrder.SelectedValue));

                        singletonDefinition.PrincipalDiagnosisCriteria = CriteriaInstitutionalPrincipalDiagnosisCodes.Text;

                        singletonDefinition.DiagnosisCriteria = CriteriaInstitutionalDiagnosisCodes.Text;

                        singletonDefinition.DrgCriteria = CriteriaInstitutionalDrgCodes.Text;

                        singletonDefinition.Icd9ProcedureCodeCriteria = CriteriaInstitutionalIcd9Codes.Text;

                        singletonDefinition.BillTypeCriteria = CriteriaInstitutionalBillTypes.Text;

                        singletonDefinition.RevenueCodeCriteria = CriteriaInstitutionalRevenueCodes.Text;

                        singletonDefinition.ProcedureCodeCriteria = CriteriaInstitutionalProcedureCodes.Text;

                        singletonDefinition.ModifierCodeCriteria = CriteriaInstitutionalModifierCodes.Text;

                        singletonDefinition.ProviderSpecialtyCriteria = CriteriaInstitutionalProviderSpecialties.Text;

                        singletonDefinition.UseMemberAgeCriteria = CriteriaInstitutionalUseMemberAgeCriteria.Checked;

                        singletonDefinition.MemberAgeDateQualifier = (Mercury.Server.Application.DateQualifier) Convert.ToInt32 (CriteriaInstitutionalMemberAgeDateQualifier.SelectedValue);

                        singletonDefinition.MemberAgeMinimum = (CriteriaInstitutionalMemberAgeMinimum.Value.HasValue) ? Convert.ToInt32 (CriteriaInstitutionalMemberAgeMinimum.Value.Value) : 0;

                        singletonDefinition.MemberAgeMaximum = (CriteriaInstitutionalMemberAgeMaximum.Value.HasValue) ? Convert.ToInt32 (CriteriaInstitutionalMemberAgeMaximum.Value.Value) : 0;

                    }

                    #endregion

                    break;

                case "Pharmacy":

                    #region Pharmacy Definition

                    if (CriteriaPharmacyNdcCodes.Text.Length != 0) { hasValue = true; }

                    if (CriteriaPharmacyDrugNames.Text.Length != 0) { hasValue = true; }

                    if (CriteriaPharmacyDeaClassifications.Text.Length != 0) { hasValue = true; }

                    if (CriteriaPharmacyTherapeuticClassifications.Text.Length != 0) { hasValue = true; }

                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Pharmacy;

                        singletonDefinition.EventDateOrder = Mercury.Server.Application.EventDateOrder.ServiceClaimFromDate;

                        singletonDefinition.NdcCodeCriteria = CriteriaPharmacyNdcCodes.Text;

                        singletonDefinition.DrugNameCriteria = CriteriaPharmacyDrugNames.Text;

                        singletonDefinition.DeaClassificationCriteria = CriteriaPharmacyDeaClassifications.Text;

                        singletonDefinition.TherapeuticClassificationCriteria = CriteriaPharmacyTherapeuticClassifications.Text;

                    }

                    #endregion

                    break;

                case "Lab":

                    #region Lab Definition

                    if (CriteriaLabLoincCodes.Text.Length != 0) { hasValue = true; }


                    if (hasValue) {

                        singletonDefinition = new Mercury.Client.Core.MedicalServices.Definitions.ServiceSingletonDefinition ();

                        singletonDefinition.DataSourceType = Mercury.Server.Application.ServiceDataSourceType.Lab;

                        singletonDefinition.EventDateOrder = Mercury.Server.Application.EventDateOrder.ServiceClaimFromDate;

                        singletonDefinition.LabLoincCodeCriteria = CriteriaLabLoincCodes.Text;

                        singletonDefinition.LabValueExpressionCriteria = CriteriaLabValueExpressions.Text;

                        singletonDefinition.LabMetricId = Convert.ToInt64 (CriteriaLabMetricSelection.SelectedValue);

                    }

                    #endregion

                    break;

            } // switch

            if (singletonDefinition != null) {

                validationResponse = MercuryApplication.MedicalServiceSingletonDefinitionValidate (singletonDefinition);

                if (validationResponse.Count == 0) {

                    switch (((System.Web.UI.WebControls.Button) sender).ID) {

                        case "ButtonAddDefinition": serviceSingleton.AddDefinition (singletonDefinition); break;

                        case "ButtonUpdateDefinition":  

                            if (ServiceDefinitionGrid.SelectedItems.Count == 0) {

                                serviceSingleton.AddDefinition (singletonDefinition); 

                            }

                            else {

                                singletonDefinition.ServiceSingletonDefinitionId = serviceSingleton.Definitions[ServiceDefinitionGrid.SelectedItems[0].DataSetIndex].Id;

                                serviceSingleton.Definitions.RemoveAt (ServiceDefinitionGrid.SelectedItems[0].DataSetIndex);

                                serviceSingleton.AddDefinition (singletonDefinition);

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

        protected void ServiceDefinitionGrid_OnDeleteCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 deleteIndex = eventArgs.Item.DataSetIndex;

            serviceSingleton.Definitions.RemoveAt (deleteIndex);

            InitializeDefinitionGrid ();

            return;

        }

        protected void ServiceDefinitionGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            Int32 itemIndex = eventArgs.Item.DataSetIndex;

            switch (eventArgs.CommandName) {

                case "ToggleActive":

                    serviceSingleton.Definitions[itemIndex].Enabled = !serviceSingleton.Definitions[itemIndex].Enabled;

                    break;

            }

            InitializeDefinitionGrid ();

            return;

        }

        #endregion


        #region Button Event Handlers

        protected void ButtonPreview_OnClick (Object sender, EventArgs eventArgs) {

            System.Collections.Generic.List<Mercury.Server.Application.MemberServiceDetailSingleton> previewDetailSingleton;

            previewDetailSingleton = serviceSingleton.Preview (MercuryApplication);


            System.Data.DataTable previewTable = new DataTable ();

            previewTable.Columns.Add ("SingletonDefinitionId");

            previewTable.Columns.Add ("EventDate");

            previewTable.Columns.Add ("ClaimId");

            previewTable.Columns.Add ("ExternalClaimId");

            previewTable.Columns.Add ("ClaimLine");

            previewTable.Columns.Add ("MemberId");

            previewTable.Columns.Add ("ProviderId");

            previewTable.Columns.Add ("ClaimType");

            previewTable.Columns.Add ("ClaimDateFrom");

            previewTable.Columns.Add ("ClaimDateThru");

            previewTable.Columns.Add ("ServiceDateFrom");

            previewTable.Columns.Add ("ServiceDateThru");

            previewTable.Columns.Add ("AdmissionDate");

            previewTable.Columns.Add ("DischargeDate");

            previewTable.Columns.Add ("BillType");

            previewTable.Columns.Add ("PrincipalDiagnosisCode");

            previewTable.Columns.Add ("DiagnosisCode");

            previewTable.Columns.Add ("Icd9ProcedureCode");

            previewTable.Columns.Add ("LocationCode");

            previewTable.Columns.Add ("RevenueCode");

            previewTable.Columns.Add ("ProcedureCode");

            previewTable.Columns.Add ("ModifierCode");

            previewTable.Columns.Add ("SpecialtyName");

            previewTable.Columns.Add ("IsPcpClaim");

            previewTable.Columns.Add ("NdcCode");

            previewTable.Columns.Add ("DeaClassification");

            previewTable.Columns.Add ("TherapeuticClassification");

            previewTable.Columns.Add ("LoincCode");

            previewTable.Columns.Add ("LabValue");

            previewTable.Columns.Add ("Description");


            foreach (Mercury.Server.Application.MemberServiceDetailSingleton detail in previewDetailSingleton) {

                previewTable.Rows.Add (

                    detail.SingletonDefinitionId, detail.EventDate.ToString ("MM/dd/yyyy"),
                    
                    detail.ClaimId, detail.ExternalClaimId, detail.ClaimLine, detail.MemberId, detail.ProviderId, detail.ClaimType, 

                    (detail.ClaimDateFrom.HasValue) ? detail.ClaimDateFrom.Value.ToString ("MM/dd/yyyy") : null,
                    
                    (detail.ClaimDateThru.HasValue) ? detail.ClaimDateThru.Value.ToString ("MM/dd/yyyy") : null, 
                    
                    (detail.ServiceDateFrom.HasValue) ? detail.ServiceDateFrom.Value.ToString ("MM/dd/yyyy") : null, 

                    (detail.ServiceDateThru.HasValue) ? detail.ServiceDateThru.Value.ToString ("MM/dd/yyyy") : null, 

                    (detail.AdmissionDate.HasValue) ? detail.AdmissionDate.Value.ToString ("MM/dd/yyyy") : null, 

                    (detail.DischargeDate.HasValue) ? detail.DischargeDate.Value.ToString ("MM/dd/yyyy") : null, 

                    detail.BillType, detail.PrincipalDiagnosisCode, detail.DiagnosisCode, detail.Icd9ProcedureCode, detail.LocationCode, detail.RevenueCode, detail.ProcedureCode, detail.ModifierCode, 
                    
                    detail.SpecialtyName, detail.IsPcpClaim.ToString (),
                    
                    detail.NdcCode, detail.DeaClassification, detail.TherapeuticClassification, 
                    
                    detail.LabLoincCode, detail.LabValue.ToString (),
                    
                    detail.Description
                    
                    );

            }



            ServicePreviewGrid.DataSource = previewTable;

            ServicePreviewGrid.DataBind ();


        }

        protected Boolean ApplyChanges () {

            Boolean success = false;

            Boolean isModified = false;

            Boolean isValid = false;

            Dictionary<String, String> validationResponse;


            if (MercuryApplication == null) { return false; }


            Mercury.Client.Core.MedicalServices.ServiceSingleton serviceSingletonUnmodified = (Mercury.Client.Core.MedicalServices.ServiceSingleton) Session [SessionCachePrefix + "ServiceSingletonUnmodified"];

            Mercury.Server.Application.ObjectSaveResponse saveResponse;


            serviceSingleton.Name = SingletonName.Text;

            serviceSingleton.Description = SingletonDescription.Text;

            serviceSingleton.ServiceType = Mercury.Server.Application.MedicalServiceType.Singleton;

            serviceSingleton.ServiceClassification = (Mercury.Server.Application.ServiceClassification) Int32.Parse (SingletonClassification.SelectedValue);

            serviceSingleton.Enabled = SingletonEnabled.Checked;

            serviceSingleton.Visible = SingletonVisible.Checked;


            if (SingletonLastPaidDate.SelectedDate.HasValue) { serviceSingleton.LastPaidDate = SingletonLastPaidDate.SelectedDate.Value; }

            else { serviceSingleton.LastPaidDate = new DateTime (1900, 01, 01); }


            if (serviceSingletonUnmodified.Id == 0) { isModified = true; }

            if (!isModified) { isModified = !serviceSingleton.IsEqual (serviceSingletonUnmodified); }


            validationResponse = serviceSingleton.Validate ();

            isValid = (validationResponse.Count == 0);


            if ((isModified) && (isValid)) {

                if (!MercuryApplication.HasEnvironmentPermission (ManagePermission)) {

                    SaveResponseLabel.Text = "Permission Denied.";

                    return false;

                }

                saveResponse = MercuryApplication.MedicalServiceSave (serviceSingleton);

                success = saveResponse.Success;

                if (success) {

                    serviceSingleton = MercuryApplication.MedicalServiceSingletonGet (saveResponse.Id);

                    Session[SessionCachePrefix + "ServiceSingleton"] = serviceSingleton;

                    Session[SessionCachePrefix + "ServiceSingletonUnmodified"] = serviceSingleton.Copy ();

                    SaveResponseLabel.Text = "Save Successful.";

                    InitializeGeneralPage ();

                    InitializeDefinitionGrid ();

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

            if (ApplyChanges ()) {

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
