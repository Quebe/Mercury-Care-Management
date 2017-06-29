using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Mercury.Server.Services.Responses;
using Mercury.Server.Services.Responses.Objects;
using Mercury.Server.Services.Responses.SearchResults;
using Mercury.Server.Services.Responses.TypedCollections;

namespace Mercury.Server.Services.Core {
    
    [ServiceContract]
    public interface IApplication {


        #region Version

        [OperationContract]
        String VersionServer (String token);

        #endregion


        #region Session

        [OperationContract]
        Server.Session SessionGet (String token);

        [OperationContract]
        AuditAuthenticationCollectionResponse ActiveSessionsAvailable (String token);

        #endregion 


        #region Enumerations - Expose Internal Server-side Enumerations for Client Use

        [OperationContract]
        void Enumeration_DataExplorerNodeResultDataType (Server.Core.DataExplorer.Enumerations.DataExplorerNodeResultDataType e);

        #endregion 


        #region Enterprise Permissions

        [OperationContract]
        StringListResponse EnterprisePermissionList (String token);

        [OperationContract]
        DictionaryResponse EnterprisePermissionDictionary (String token);

        [OperationContract]
        PermissionCollectionResponse EnterprisePermissionsAvailable (String token);

        [OperationContract]
        SecurityGroupPermissionCollectionResponse SecurityGroupEnterprisePermissionsGet (String token, String securityAuthorityName, String securityGroupId);

        [OperationContract]
        BooleanResponse SecurityGroupEnterprisePermissionSave (String token, Int64 securityAuthorityId, String securityGroupId, Int64 permissionId, Boolean isGranted, Boolean isDenied);

        #endregion

        
        #region Security Authority

        [OperationContract]
        SecurityAuthorityCollectionResponse SecurityAuthoritiesAvailable (String token);

        [OperationContract]
        DictionaryResponse SecurityAuthorityDictionary (String token);

        [OperationContract]
        DirectoryEntryCollectionResponse SecurityAuthorityGroupGetMembership (String token, Int64 securityAuthorityId, String securityGroupId);


        [OperationContract]
        Mercury.Server.Security.SecurityAuthority SecurityAuthorityGetByName (String token, String securityAuthorityName);

        [OperationContract]
        Mercury.Server.Security.SecurityAuthority SecurityAuthorityGet (String token, Int64 securityAuthorityId);


        [OperationContract]
        BooleanResponse SecurityAuthoritySave (String token, Mercury.Server.Security.SecurityAuthority securityAuthority);

        [OperationContract]
        BooleanResponse SecurityAuthorityDelete (String token, String securityAuthorityName);



        #region Security Authority Provider

        [OperationContract]
        DirectoryEntryCollectionResponse SecurityAuthorityProviderBrowseDirectory (String token, String securityAuthorityName, String directoryPath);

        #endregion 


        #region Security Authority Groups

        [OperationContract]
        DictionaryStringResponse SecurityAuthoritySecurityGroupDictionary (String token, Int64 securityAuthorityId);

        [OperationContract]
        SecurityGroupCollectionResponse SecurityAuthoritySecurityGroups (String token, Int64 securityAuthorityId);

        [OperationContract]
        Mercury.Server.Public.Interfaces.Security.SecurityGroup SecurityAuthoritySecurityGroupGet (String token, String securityAuthorityName, String securityGroupId);

        [OperationContract]
        DirectoryEntryCollectionResponse SecurityAuthoritySecurityGroupMembership (String token, String securityAuthorityName, String securityGroupId);

        #endregion 

        #endregion


        #region Environment

        [OperationContract]
        EnvironmentAccessCollectionResponse EnvironmentAccessGetByEnvironmentName (String token, String environmentName);

        [OperationContract]
        EnvironmentAccessCollectionResponse SecurityGroupEnvironmentAccessGet (String token, String securityAuthorityName, String securityGroupId);

        [OperationContract]
        BooleanResponse SecurityGroupEnvironmentAccessSave (String token, Int64 securityAuthorityId, String securityGroupId, Int64 environmentId, Boolean isGranted, Boolean isDenied);


        [OperationContract]
        Mercury.Server.Environment.Environment EnvironmentGet (String token, Int64 environmentId);

        [OperationContract]
        Mercury.Server.Environment.Environment EnvironmentGetByName (String token, String environmentName);

        [OperationContract]
        BooleanResponse EnvironmentSave (String token, Mercury.Server.Environment.Environment environment);

        [OperationContract]
        BooleanResponse EnvironmentDelete (String token, String environmentName);


        [OperationContract]
        StringListResponse EnvironmentList (String token);

        [OperationContract]
        DictionaryResponse EnvironmentDictionary (String token);

        [OperationContract]
        EnvironmentCollectionResponse EnvironmentsAvailable (String token);


        [OperationContract]
        BooleanResponse HasEnvironmentPermissionByEnvironment (String token, String environmentName, String permissionName);

        [OperationContract]
        StringListResponse EnvironmentPermissionList (String token, String environmentName);

        [OperationContract]
        PermissionCollectionResponse EnvironmentPermissionsAvailable (String token, String environmentName);

        [OperationContract]
        StringListResponse EnvironmentRoleList (String token, String environmentName);

        [OperationContract]
        DictionaryResponse EnvironmentRoleDictionary (String token, String environmentName);

        [OperationContract]
        RoleCollectionResponse EnvironmentRolesAvailable (String token, String environmentName);

        [OperationContract]
        RolePermissionCollectionResponse EnvironmentRoleGetPermissions (String token, String environmentName, String roleName);

        [OperationContract]
        RoleMembershipCollectionResponse EnvironmentRoleGetMembership (String token, String environmentName, String roleName);

        [OperationContract]
        Mercury.Server.Environment.Role EnvironmentRoleGetByEnvironment (String token, String environmentName, String roleName);

        [OperationContract]
        ObjectSaveResponse EnvironmentRoleSaveByEnvironment (String token, String environmentName, Mercury.Server.Environment.Role environmentRole);

        [OperationContract]
        BooleanResponse EnvironmentRoleSetMembership (String token, String environmentName, String roleName, System.Collections.Generic.List<Mercury.Server.Environment.RoleMembership> roleMembership);

        [OperationContract]
        BooleanResponse EnvironmentRoleSetPermission (String token, String environmentName, String roleName, Int64 permissionId, Boolean isGranted, Boolean isDenied);

        #endregion


        #region Core Objects

        [OperationContract]
        DictionaryResponse CoreObjectDictionary (String token, String objectType);

        [OperationContract]
        DictionaryStringResponse CoreObject_Validate (String token, Server.Core.CoreObject coreObject);

        [OperationContract]
        DictionaryStringResponse CoreConfigurationObject_Validate (String token, Server.Core.CoreConfigurationObject coreConfigurationObject);

        [OperationContract]
        DictionaryStringResponse CoreObject_DataBindingContexts (String token, Server.Core.CoreObject coreObject);

        [OperationContract]
        StringResponse CoreObject_EvaluateDataBinding (String token, Server.Core.CoreObject coreObject, String bindingContext);

        [OperationContract]
        StringResponse CoreObject_XmlSerialize (String token, Server.Core.CoreObject coreObject);

        [OperationContract]
        ImportExportResponse CoreObject_XmlImport (String token, String serializedXml);
        
        #endregion 

        
        #region Core Reference

        #region Core Reference - Contact Regarding

        [OperationContract]
        ContactRegardingCollectionResponse ContactRegardingsAvailable (String token);

        [OperationContract]
        DictionaryResponse ContactRegardingDictionary (String token);

        [OperationContract]
        Server.Core.Reference.ContactRegarding ContactRegardingGet (String token, Int64 contactRegardingId);

        [OperationContract]
        Server.Core.Reference.ContactRegarding ContactRegardingGetByName (String token, String contactRegardingName);

        [OperationContract]
        ObjectSaveResponse ContactRegardingSave (String token, Server.Core.Reference.ContactRegarding contactRegarding);

        #endregion


        #region Core Reference - Correspondence

        [OperationContract]
        CorrespondenceCollectionResponse CorrespondencesAvailable (String token);

        [OperationContract]
        DictionaryResponse CorrespondenceDictionary (String token);

        [OperationContract]
        Server.Core.Reference.Correspondence CorrespondenceGet (String token, Int64 correspondenceId);

        [OperationContract]
        Server.Core.Reference.Correspondence CorrespondenceGetByName (String token, String correspondenceName);
        
        [OperationContract]
        ObjectSaveResponse CorrespondenceSave (String token, Server.Core.Reference.Correspondence correspondence);


        [OperationContract]
        Server.Core.Reference.CorrespondenceContent CorrespondenceContentGet (String token, Int64 correspondenceContentId);

        #endregion


        #region Core Reference - Note Type

        [OperationContract]
        NoteTypeCollectionResponse NoteTypesAvailable (String token);

        [OperationContract]
        DictionaryResponse NoteTypeDictionary (String token);

        [OperationContract]
        Server.Core.Reference.NoteType NoteTypeGet (String token, Int64 noteTypeId);

        [OperationContract]
        Server.Core.Reference.NoteType NoteTypeGetByName (String token, String noteTypeName);

        [OperationContract]
        ObjectSaveResponse NoteTypeSave (String token, Server.Core.Reference.NoteType noteType);

        #endregion


        #region Geographic References

        [OperationContract]
        StringListResponse StateReference (String token);

        [OperationContract]
        String StateReferenceByZipCode (String token, String zipCode);

        [OperationContract]
        CityStateZipCodeViewCollectionResponse CityReferenceByState (String token, String state);

        [OperationContract]
        Server.Core.Reference.Views.CityStateZipCodeView CityStateReferenceByZipCode (String token, String zipCode);

        [OperationContract]
        String CityReferenceByZipCode (String token, String ZipCode);

        [OperationContract]
        StringListResponse CityReferenceByStateCityName (String token, String state, String cityName);

        [OperationContract]
        StringListResponse CountyReferenceByState (String token, String state);

        [OperationContract]
        String CountyReferenceByZipCode (String token, String zipCode);

        #endregion

        #endregion


        #region References - Old

        [OperationContract]
        System.Collections.Generic.Dictionary<String, String> DiagnosisDictionary (String token, String diagnosisPrefix, Int32 version);

        [OperationContract]
        String DiagnosisDescription (String token, String diagnosisCode, Int32 version);

        [OperationContract]
        System.Collections.Generic.Dictionary<String, String> ProcedureCodeDictionary (String token, String procedureCodePrefix);

        [OperationContract]
        System.Collections.Generic.Dictionary<String, String> RevenueCodeDictionary (String token, String revenueCodePrefix);

        [OperationContract]
        System.Collections.Generic.Dictionary<String, String> BillTypeDictionary (String token, String billTypePrefix);

        [OperationContract]
        System.Collections.Generic.Dictionary<String, String> Icd9ProcedureCodeDictionary (String token, String icd9Icd9ProcedureCodePrefix);

        #endregion 

        
        #region Actions

        [OperationContract]
        ActionCollectionResponse ActionsAvailable (String token);

        #endregion


        #region Authorizations

        [OperationContract]
        AuthorizationTypeCollectionResponse AuthorizationTypesAvailable (String token);

        [OperationContract]
        Server.Core.Authorizations.AuthorizationType AuthorizationTypeGet (String token, Int64 authorizationTypeId);


        #region Member Authorizations
        
        [OperationContract]
        Int64 MemberAuthorizationsGetCount (String token, Int64 memberId);

        [OperationContract]
        AuthorizationCollectionResponse MemberAuthorizationsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count);

        [OperationContract]
        AuthorizationLineCollectionResponse AuthorizationLineGetByAuthorization (String token, Int64 authorizationId);
        
        #endregion 

        #endregion


        #region Authorized Services

        [OperationContract]
        AuthorizedServiceCollectionResponse AuthorizedServicesAvailable (String token);

        [OperationContract]
        Server.Core.AuthorizedServices.AuthorizedService AuthorizedServiceGet (String token, Int64 metricId);

        [OperationContract]
        ObjectSaveResponse AuthorizedServiceSave (String token, Server.Core.AuthorizedServices.AuthorizedService metric);

        [OperationContract]
        MemberAuthorizedServiceDetailCollectionResponse AuthorizedServicePreview (String token, Server.Core.AuthorizedServices.AuthorizedService authorizedService);


        [OperationContract]
        Int64 MemberAuthorizedServicesGetCount (String token, Int64 memberId, Boolean showHidden);

        [OperationContract]
        MemberAuthorizedServiceCollectionResponse MemberAuthorizedServicesGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden);

        [OperationContract]
        MemberAuthorizedServiceDetailCollectionResponse MemberAuthorizedServiceDetailsGet (String token, Int64 memberAuthorizedServiceId);

        #endregion


        #region Claims

        #region Member Claims

        [OperationContract]
        Int64 MemberClaimsGetCount (String token, Int64 memberId);

        [OperationContract]
        ClaimCollectionResponse MemberClaimsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count);

        [OperationContract]
        ClaimLineCollectionResponse ClaimLinesGet (String token, Int64 claimId);

        [OperationContract]
        Int64 MemberPharmacyClaimsGetCount (String token, Int64 memberId);

        [OperationContract]
        PharmacyClaimCollectionResponse MemberPharmacyClaimsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count);

        [OperationContract]
        Int64 MemberLabResultsGetCount (String token, Int64 memberId);

        [OperationContract]
        LabResultCollectionResponse MemberLabResultsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count);

        #endregion 

        #endregion 


        #region Core - Condition

        [OperationContract]
        ConditionClassCollectionResponse ConditionClassesAvailable (String token);

        [OperationContract]
        ConditionCollectionResponse ConditionsAvailable (String token);

        [OperationContract]
        Server.Core.Condition.Condition ConditionGet (String token, Int64 conditionId);

        [OperationContract]
        ObjectSaveResponse ConditionSave (String token, Mercury.Server.Core.Condition.Condition condition);

        #endregion 


        #region Core - Entity

        #region Entity

        [OperationContract]
        Server.Core.Entity.Entity EntityGet (String token, Int64 entityId);

        #endregion 


        #region Entity Address

        [OperationContract]
        Server.Core.Entity.EntityAddress EntityAddressGet (String token, Int64 entityAddressId);

        [OperationContract]
        Server.Core.Entity.EntityAddress EntityAddressGetByTypeDate (String token, Int64 entityId, Server.Core.Enumerations.EntityAddressType addressType, DateTime forDate);

        [OperationContract]
        EntityAddressCollectionResponse EntityAddressesGet (String token, Int64 entityId);


        [OperationContract]
        ObjectSaveResponse EntityAddressSave (String token, Server.Core.Entity.EntityAddress entityAddress);

        [OperationContract]
        BooleanResponse EntityAddressTerminate (String token, Server.Core.Entity.EntityAddress entityAddress, DateTime terminationDate); 

        #endregion 


        #region Entity Contact

        [OperationContract]
        Server.Core.Entity.EntityContact EntityContactGet (String token, Int64 entityContactId);

        [OperationContract]
        Int64 EntityContactsGetCount (String token, Int64 entityId);

        [OperationContract]
        EntityContactCollectionResponse EntityContactsGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count);


        [OperationContract]
        ObjectSaveResponse EntityContactSave (String token, Server.Core.Entity.EntityContact entityContact);

        #endregion 


        #region Entity Contact Information

        [OperationContract]
        Server.Core.Entity.EntityContactInformation EntityContactInformationGet (String token, Int64 entityContactInformationId);

        [OperationContract]
        Server.Core.Entity.EntityContactInformation EntityContactInformationGetByTypeDate (String token, Int64 entityId, Server.Core.Enumerations.EntityContactType contactType, DateTime forDate);

        [OperationContract]
        EntityContactInformationCollectionResponse EntityContactInformationsGet (String token, Int64 entityId);


        [OperationContract]
        ObjectSaveResponse EntityContactInformationSave (String token, Server.Core.Entity.EntityContactInformation entityContactInformation);

        [OperationContract]
        BooleanResponse EntityContactInformationTerminate (String token, Server.Core.Entity.EntityContactInformation entityContactInformation, DateTime terminationDate); 

        #endregion 


        #region Entity Correspondence

        [OperationContract]
        Server.Core.Entity.EntityCorrespondence EntityCorrespondenceGet (String token, Int64 entityCorrespondenceId);

        [OperationContract]
        ImageResponse EntityCorrespondenceImageGet (String token, Int64 entityCorrespondenceId, Boolean render);

        [OperationContract]
        ObjectSaveResponse EntityCorrespondenceSave (String token, Server.Core.Entity.EntityCorrespondence entityCorrespondence);


        [OperationContract]
        Int64 EntityDocumentsGetCount (String token, Int64 entityId);

        [OperationContract]
        EntityDocumentCollectionResponse EntityDocumentsGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count);

        #endregion 


        #region Entity Note

        [OperationContract]
        Server.Core.Entity.EntityNote EntityNoteGet (String token, Int64 entityNoteId);

        [OperationContract]
        ObjectSaveResponse EntityNoteSave (String token, Server.Core.Entity.EntityNote entityNote);

        [OperationContract]
        BooleanResponse EntityNoteTerminate (String token, Server.Core.Entity.EntityNote entityNote, DateTime terminationDate);


        [OperationContract]
        EntityNoteContentCollectionResponse EntityNoteContentsGet (String token, Int64 entityNoteId);

        [OperationContract]
        ObjectSaveResponse EntityNoteContentAppend (String token, Server.Core.Entity.EntityNote entityNote, String content);



        [OperationContract]
        Int64 EntityNotesGetCount (String token, Int64 entityId);

        [OperationContract]
        EntityNoteCollectionResponse EntityNotesGetByPage (String token, Int64 entityId, Int32 initialRow, Int32 count);

        [OperationContract]
        Server.Core.Entity.EntityNote EntityNoteGetMostRecentByImportance (String token, Int64 entityId, Server.Core.Enumerations.NoteImportance importance);

        [OperationContract]
        EntityNoteCollectionResponse EntityNoteGetMostRecentByAllImportances (String token, Int64 entityId);

        [OperationContract]
        Server.Core.Entity.EntityNote EntityNoteGetMostRecentByType (String token, Int64 entityId, Int64 noteTypeId);

        #endregion

        #endregion


        #region Core - Forms

        #region Form

        [OperationContract]
        SearchResultFormHeaderCollectionResponse FormsAvailable (String token);

        [OperationContract]
        Server.Core.Forms.Form FormGet (String token, Int64 formId);

        [OperationContract]
        Server.Core.Forms.Form FormGetByName (String token, String formName);

        [OperationContract]
        Server.Core.Forms.Form FormGetByEntityFormId (String token, Int64 entityFormId);


        [OperationContract]
        FormCompileMessageCollectionResponse FormCompile (String token, Server.Core.Forms.Form form);

        [OperationContract]
        FormSubmitResponse FormSubmit (String token, Server.Core.Forms.Form form);

        [OperationContract]
        ObjectSaveResponse FormSave (String token, Server.Core.Forms.Form form);

        #endregion 
        

        #region Form Control - Data Binding Extensions

        [OperationContract]
        Dictionary<String, String> FormControl_DataBindableProperties (String token, Server.Core.Forms.Form form, Guid controlId);

        [OperationContract]
        Dictionary<String, String> FormControl_DataBindingContexts (String token, Server.Core.Forms.Form form, Guid controlId);

        [OperationContract]
        BooleanResponse FormControl_DataBindingAllowed (String token, Server.Core.Forms.Form form, Guid controlId, String bindableProperty, String forDataType);

        [OperationContract]
        String FormControl_EvaluateDataBinding (String token, Server.Core.Forms.Form form, Guid controlId, Server.Core.Forms.Structures.DataBinding dataBinding);

        [OperationContract]
        Server.Core.Forms.Form Form_OnDataSourceChanged (String token, Server.Core.Forms.Form form, Guid controlId);

        #endregion


        #region Form Control - Event Handlers

        [OperationContract]
        List<String> FormControl_Events (String token, Server.Core.Forms.Form form, Guid controlId);

        [OperationContract]
        FormCompileMessageCollectionResponse FormControl_EventHandler_Compile (String token, Server.Core.Forms.Form form, Guid controlId, String eventName);

        [OperationContract]
        FormControlRaiseEventResponse FormControl_RaiseEvent (String token, Server.Core.Forms.Form form, Guid controlId, String eventName);

        [OperationContract]
        FormControlValueChangedResponse FormControl_ValueChanged (String token, Server.Core.Forms.Form form, Guid controlId);

        #endregion


        #region Control Specific Methods

        [OperationContract]
        Dictionary<String, String> FormControlSelection_ReferenceGetPage (String token, Server.Core.Forms.Form form, Guid controlId, String text, Int32 initialRow, Int32 pageSize);

        #endregion

        #endregion


        #region Core - Individual

        #region Care Level

        [OperationContract]
        CareLevelCollectionResponse CareLevelsAvailable (String token);

        [OperationContract]
        Server.Core.Individual.CareLevel CareLevelGet (String token, Int64 careLevelId);

        [OperationContract]
        ObjectSaveResponse CareLevelSave (String token, Mercury.Server.Core.Individual.CareLevel careLevel);

        #endregion 
        

        #region Care Measures

        [OperationContract]
        CareMeasureScaleCollectionResponse CareMeasureScalesAvailable (String token);

        [OperationContract]
        Server.Core.Individual.CareMeasureScale CareMeasureScaleGet (String token, Int64 careMeasureScaleId);

        [OperationContract]
        ObjectSaveResponse CareMeasureScaleSave (String token, Mercury.Server.Core.Individual.CareMeasureScale careMeasureScale);
        
        #endregion 


        #region Care Measure

        [OperationContract]
        CareMeasureDomainCollectionResponse CareMeasureDomainsAvailable (String token);

        [OperationContract]
        CareMeasureClassCollectionResponse CareMeasureClassesAvailable (String token);

        [OperationContract]
        CareMeasureCollectionResponse CareMeasuresAvailable (String token);

        [OperationContract]
        Server.Core.Individual.CareMeasure CareMeasureGet (String token, Int64 careMeasureId);

        [OperationContract]
        ObjectSaveResponse CareMeasureSave (String token, Mercury.Server.Core.Individual.CareMeasure careMeasure);

        #endregion 

        

        #region Care Intervention

        [OperationContract]
        CareInterventionCollectionResponse CareInterventionsAvailable (String token);

        [OperationContract]
        Server.Core.Individual.CareIntervention CareInterventionGet (String token, Int64 careInterventionId);

        [OperationContract]
        ObjectSaveResponse CareInterventionSave (String token, Mercury.Server.Core.Individual.CareIntervention careIntervention);

        #endregion 


        #region Care Plan

        [OperationContract]
        CarePlanCollectionResponse CarePlansAvailable (String token);

        [OperationContract]
        Server.Core.Individual.CarePlan CarePlanGet (String token, Int64 carePlanId);

        [OperationContract]
        ObjectSaveResponse CarePlanSave (String token, Mercury.Server.Core.Individual.CarePlan carePlan);

        #endregion 
        

        #region Problem Statement

        [OperationContract]
        ProblemDomainCollectionResponse ProblemDomainsAvailable (String token);

        [OperationContract]
        ProblemClassCollectionResponse ProblemClassesAvailable (String token);

        [OperationContract]
        ProblemStatementCollectionResponse ProblemStatementsAvailable (String token);

        [OperationContract]
        Server.Core.Individual.ProblemStatement ProblemStatementGet (String token, Int64 problemStatementId);

        [OperationContract]
        ObjectSaveResponse ProblemStatementSave (String token, Mercury.Server.Core.Individual.ProblemStatement problemStatement);

        [OperationContract]
        MemberProblemStatementIdentifiedCollectionResponse MemberProblemStatementIdentifiedAvailable (String token, Int64 memberId, Boolean includeCompleted);

        #endregion 

        
        #region Care - Care Outcome

        [OperationContract]
        CareOutcomeCollectionResponse CareOutcomesAvailable (String token);

        [OperationContract]
        DictionaryResponse CareOutcomeDictionary (String token);


        [OperationContract]
        Server.Core.Individual.CareOutcome CareOutcomeGet (String token, Int64 careOutcomeId);

        [OperationContract]
        Server.Core.Individual.CareOutcome CareOutcomeGetByName (String token, String careOutcomeName);

        [OperationContract]
        ObjectSaveResponse CareOutcomeSave (String token, Server.Core.Individual.CareOutcome careOutcome);

        #endregion 


        #region Case - Member Case
        
        [OperationContract]
        MemberCaseCreateResponse MemberCaseCreate (String token, Int64 memberId, Boolean ignoreExisting);

        [OperationContract]
        Server.Core.Individual.Case.MemberCase MemberCaseGet (String token, Int64 memberCaseId);


        /* TODO: DAVID: ADDED ON 9/13/2011 (START) */

        [OperationContract]
        MemberCaseAuditCollectionResponse MemberCaseAuditHistoryGetByMemberCaseIdPage (String token, Int64 memberCaseId, Int64 initialRow, Int64 count);

        [OperationContract]
        Int64 MemberCaseAuditHistoryGetCount (String token, Int64 memberCaseId);

        /* TODO: DAVID: ADDED ON 9/13/2011 ( END ) */


        /* ADDED ON 10/25/2011 (START) */

        [OperationContract]
        MemberCaseModificationResponse MemberCaseProblemClass_AssignToUser (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToSecurityAuthorityId, String assignToUserAccountId, String assignToUserAccountName, String assignToUserDisplayName);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseProblemClass_AssignToProvider (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemClassId, Int64 assignToProviderId);

        /* ADDED ON 10/25/2011 ( END ) */


        /* ADDED ON 11/10/11 (START) */

        [OperationContract]
        MemberCaseCarePlanSummaryCollectionResponse MemberCaseCarePlanSummaryGetByMemberCase (String token, Int64 memberCaseId, Boolean useCaching);

        /* ADDED ON 11/10/11 ( END ) */


        [OperationContract]
        MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByMemberPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showClosed);

        [OperationContract]
        MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByUserWorkTeamsPage (String token, Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed);

        [OperationContract]
        MemberCaseSummaryCollectionResponse MemberCaseSummaryGetByAssignedToUserPage (String token, Int64 securityAuthorityId, String userAccountId, Int32 initialRow, Int32 count, Boolean showClosed);


        [OperationContract]
        MemberCaseLoadSummaryCollectionResponse MemberCaseLoadSummaryGetByUser (String token, Int64 securityAuthorityId, String userAccountId, Boolean showClosed);

        [OperationContract]
        MemberCaseLoadSummaryCollectionResponse MemberCaseLoadSummaryGetByWorkTeam (String token, Int64 workTeamId, Boolean showClosed);


        [OperationContract]
        MemberCaseModificationResponse MemberCase_SetDescription (String token, Server.Core.Individual.Case.MemberCase memberCase, String description);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_SetReferenceNumber (String token, Server.Core.Individual.Case.MemberCase memberCase, String referenceNumber);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_Lock (String token, Server.Core.Individual.Case.MemberCase memberCase);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_Unlock (String token, Server.Core.Individual.Case.MemberCase memberCase);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_AssignToWorkTeam (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 workTeamId);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_AssignToUser (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName);


        [OperationContract]
        MemberCaseModificationResponse MemberCase_AddProblemStatement (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 problemStatementId, Boolean isSingleInstance);

        [OperationContract]
        MemberCaseModificationResponse MemberCase_DeleteProblemStatement (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseProblemCarePlanId);


        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanGoal_Delete (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanGoal_Add (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCarePlanGoalId, String carePlanGoalName, Int64 careMeasureId);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanGoal_Update (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanGoal_AddCareIntervention (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanGoalId, Int64 careInterventionId, Boolean isSingleInstance);


        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanIntervention_Delete (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanIntervention_Add (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanId, Int64 copyCareInterventionId, String carePlanInterventionName);

        [OperationContract]
        MemberCaseModificationResponse MemberCaseCarePlanIntervention_Update (String token, Server.Core.Individual.Case.MemberCase memberCase, Int64 memberCaseCarePlanInterventionId);

        #endregion 


        #region Case - Member Case Care Plan Assessment

        [OperationContract]
        ObjectSaveResponse MemberCaseCarePlanAssessmentSave (String token, Server.Core.Individual.Case.MemberCaseCarePlanAssessment assessment);

        #endregion 

        #endregion


        #region Core - Insurer

        #region Benefit Plan

        [OperationContract]
        BenefitPlanCollectionResponse BenefitPlansAvailable (String token);

        [OperationContract]
        DictionaryResponse BenefitPlanDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.BenefitPlan BenefitPlanGet (String token, Int64 benefitPlanId);

        #endregion


        #region Contract 

        [OperationContract]
        ContractCollectionResponse ContractsAvailable (String token);

        [OperationContract]
        DictionaryResponse ContractDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.Contract ContractGet (String token, Int64 contractId);

        #endregion


        #region Coverage Level

        [OperationContract]
        CoverageLevelCollectionResponse CoverageLevelsAvailable (String token);

        [OperationContract]
        DictionaryResponse CoverageLevelDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.CoverageLevel CoverageLevelGet (String token, Int64 coverageLevelId);

        #endregion


        #region Coverage Type

        [OperationContract]
        CoverageTypeCollectionResponse CoverageTypesAvailable (String token);

        [OperationContract]
        DictionaryResponse CoverageTypeDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.CoverageType CoverageTypeGet (String token, Int64 coverageTypeId);

        #endregion 


        #region Insurance Type
        
        [OperationContract]
        InsuranceTypeCollectionResponse InsuranceTypesAvailable (String token);

        [OperationContract]
        DictionaryResponse InsuranceTypeDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.InsuranceType InsuranceTypeGet (String token, Int64 insuranceTypeId);

        #endregion 
        

        #region Insurer

        [OperationContract]
        InsurerCollectionResponse InsurersAvailable (String token);

        [OperationContract]
        DictionaryResponse InsurerDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.Insurer InsurerGet (String token, Int64 insurerId);

        #endregion


        #region Program

        [OperationContract]
        ProgramCollectionResponse ProgramsAvailable (String token);

        [OperationContract]
        DictionaryResponse ProgramDictionary (String token);

        [OperationContract]
        Server.Core.Insurer.Program ProgramGet (String token, Int64 programId);

        #endregion


        #endregion


        #region Core - Medical Services

        [OperationContract]
        SearchResultMedicalServiceHeaderCollectionResponse MedicalServiceHeadersGet (String token);

        [OperationContract]
        SearchResultMedicalServiceHeaderCollectionResponse MedicalServiceHeadersGetByType (String token, Server.Core.MedicalServices.Enumerations.MedicalServiceType serviceType);


        [OperationContract]
        Server.Core.MedicalServices.Service MedicalServiceGet (String token, Int64 serviceId);

        [OperationContract]
        Int64 MedicalServiceGetIdByName (String token, String serviceName);

        [OperationContract]
        BooleanResponse MedicalServiceDelete (String token, Int64 serviceId);

        [OperationContract]
        Server.Core.MedicalServices.ServiceSingleton MedicalServiceSingletonGet (String token, Int64 serviceId);

        [OperationContract]
        ObjectSaveResponse MedicalServiceSingletonSave (String token, Server.Core.MedicalServices.ServiceSingleton singleton);

        [OperationContract]
        Dictionary<String, String> MedicalServiceSingletonDefinitionValidate (String token, Server.Core.MedicalServices.Definitions.ServiceSingletonDefinition singletonDefinition);

        [OperationContract]
        MedicalServiceDetailSingletonCollectionResponse MedicalServiceSingletonPreview (String token, Server.Core.MedicalServices.ServiceSingleton serviceSingleton);

        [OperationContract]
        MedicalServiceDetailSetCollectionResponse MedicalServiceSetPreview (String token, Server.Core.MedicalServices.ServiceSet serviceSet);

        [OperationContract]
        Server.Core.MedicalServices.ServiceSet MedicalServiceSetGet (String token, Int64 serviceId);

        [OperationContract]
        Dictionary<String, String> MedicalServiceSetDefinitionValidate (String token, Server.Core.MedicalServices.Definitions.ServiceSetDefinition setDefinition);

        [OperationContract]
        ObjectSaveResponse MedicalServiceSetSave (String token, Server.Core.MedicalServices.ServiceSet serviceSet);

        [OperationContract]
        Server.Core.MedicalServices.MemberService MemberServiceGet (String token, Int64 memberServiceId);
        

        [OperationContract]
        ObjectSaveResponse MemberServiceAddManual (String token, Int64 memberId, Int64 serviceId, DateTime eventDate);

        [OperationContract]
        BooleanResponse MemberServiceRemoveManual (String token, Int64 memberServiceId);



        [OperationContract]
        Int64 MemberServicesGetCount (String token, Int64 memberId, Boolean showHidden);

        [OperationContract]
        MemberServiceCollectionResponse MemberServicesGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden);


        [OperationContract]
        MemberServiceDetailSingletonCollectionResponse MemberServiceDetailSingletonGet (String token, Int64 memberServiceId);

        [OperationContract]
        MemberServiceDetailSetCollectionResponse MemberServiceDetailSetGet (String token, Int64 memberServiceId);

        #endregion


        #region Core - Member

        #region Member

        [OperationContract]
        Server.Core.Member.Member MemberGet (String token, Int64 memberId);

        [OperationContract]
        Server.Core.Member.Member MemberGetByEntityId (String token, Int64 entityId);

        [OperationContract]
        MemberDemographicsResponse MemberGetDemographics (String token, Int64 memberId);

        [OperationContract]
        MemberDemographicsResponse MemberGetDemographicsByEntityId (String token, Int64 entityId);

        #endregion 


        #region Member Enrollment

        [OperationContract]
        Server.Core.Member.MemberEnrollment MemberEnrollmentGet (String token, Int64 memberEnrollmentId);

        [OperationContract]
        MemberEnrollmentCollectionResponse MemberEnrollmentsGet (String token, Int64 memberId);


        [OperationContract]
        Server.Core.Member.MemberEnrollmentCoverage MemberEnrollmentCoverageGet (String token, Int64 memberEnrollmentCoverageId);

        [OperationContract]
        MemberEnrollmentCoverageCollectionResponse MemberEnrollmentCoveragesGet (String token, Int64 memberEnrollmentId);


        [OperationContract]
        Server.Core.Member.MemberEnrollmentPcp MemberEnrollmentPcpGet (String token, Int64 memberEnrollmentPcpId);

        [OperationContract]
        MemberEnrollmentPcpCollectionResponse MemberEnrollmentPcpsGet (String token, Int64 memberEnrollmentId);


        [OperationContract]
        Server.Core.Member.MemberEnrollmentTplCob MemberEnrollmentTplCobGet (String token, Int64 memberEnrollmentTplCobId);

        [OperationContract]
        MemberEnrollmentTplCobCollectionResponse MemberEnrollmentTplCobsGet (String token, Int64 memberId);

        #endregion 


        #region Member Relationship

        [OperationContract]
        Server.Core.Member.MemberRelationship MemberRelationshipGet (String token, Int64 memberRelationshipId);

        [OperationContract]
        MemberRelationshipCollectionResponse MemberRelationshipsGet (String token, Int64 memberId);

        #endregion

        #endregion


        #region Core - Metrics

        [OperationContract]
        MetricCollectionResponse MetricsAvailable (String token);

        [OperationContract]
        Server.Core.Metrics.Metric MetricGet (String token, Int64 metricId);

        [OperationContract]
        ObjectSaveResponse MetricSave (String token, Server.Core.Metrics.Metric metric);


        [OperationContract]
        ObjectSaveResponse MemberMetricAddManual (String token, Int64 memberId, Int64 metricId, DateTime eventDate, Decimal value);

        [OperationContract]
        BooleanResponse MemberMetricRemoveManual (String token, Int64 memberMetricId);


        [OperationContract]
        Int64 MemberMetricsGetCount (String token, Int64 memberId, Boolean showHidden);

        [OperationContract]
        MemberMetricCollectionResponse MemberMetricsGetByPage (String token, Int64 memberId, Int32 initialRow, Int32 count, Boolean showHidden);

        #endregion


        #region Core - Population

        #region Population Type

        [OperationContract]
        PopulationTypeCollectionResponse PopulationTypesAvailable (String token);

        [OperationContract]
        Server.Core.Population.PopulationType PopulationTypeGet (String token, Int64 populationTypeId);

        [OperationContract]
        ObjectSaveResponse PopulationTypeSave (String token, Mercury.Server.Core.Population.PopulationType populationType);

        #endregion 


        [OperationContract]
        PopulationHeadersCollectionResponse PopulationsAvailable (String token);


        [OperationContract]
        Server.Core.Population.Population PopulationGet (String token, Int64 populationId);

        [OperationContract]
        Server.Core.Population.PopulationEvents.PopulationServiceEvent PopulationServiceEventGet (String token, Int64 populationServiceEventId);


        [OperationContract]
        ObjectSaveResponse PopulationSave (String token, Mercury.Server.Core.Population.Population population);

        [OperationContract]
        BooleanResponse PopulationDelete (String token, Int64 populationId);

        [OperationContract]
        Dictionary<String, String> Population_DataBindingContexts (String token, Server.Core.Population.Population population);


        #region Population Membership

        [OperationContract]
        Server.Core.Population.PopulationMembership PopulationMembershipGet (String token, Int64 populationMembershipId);

        [OperationContract]
        PopulationMembershipServiceEventCollectionResponse PopulationMembershipServiceEventsGetByPopulationMembership (String token, Int64 populationMembershipId);

        [OperationContract]
        Server.Core.Population.PopulationEvents.PopulationMembershipServiceEvent PopulationMembershipServiceEventGet (String token, Int64 populationMembershipServiceEventId);


        [OperationContract]
        PopulationMembershipCollectionResponse PopulationMembershipGetByMember (String token, Int64 memberId);

        [OperationContract]
        PopulationMembershipSummaryCollectionResponse PopulationMembershipSummaryByMember (String token, Int64 memberId);

        [OperationContract]
        PopulationMembershipServiceEventDataViewCollectionResponse PopulationMembershipServiceEventsByMembershipDataView (String token, Int64 membershipId);

        [OperationContract]
        PopulationMembershipTriggerEventDataViewCollectionResponse PopulationMembershipTriggerEventsByMembershipDataView (String token, Int64 membershipId);

        [OperationContract]
        Int64 PopulationMembershipGetCountByName (String token, Int64 populationId, String namePrefix);

        [OperationContract]
        PopulationMembershipEntryStatusDataViewCollectionResponse PopulationMembershipGetByMembershipPage (String token, Int64 populationId, String namePrefix, Int64 initialRow, Int32 count);

        #endregion 

        #endregion


        #region Core - Provider

        #region Provider

        [OperationContract]
        Server.Core.Provider.Provider ProviderGet (String token, Int64 providerId);

        [OperationContract]
        Server.Core.Provider.Provider ProviderGetByEntityId (String token, Int64 entityId);

        #endregion 


        #region Provider Enrollments

        [OperationContract]
        ProviderEnrollmentCollectionResponse ProviderEnrollmentsGet (String token, Int64 providerId);

        [OperationContract]
        Server.Core.Provider.ProviderEnrollment ProviderEnrollmentGet (String token, Int64 providerEnrollmentId);

        #endregion 
        

        [OperationContract]
        ProviderAffiliationCollectionResponse ProviderAffiliationsGet (String token, Int64 providerId);

        [OperationContract]
        Server.Core.Provider.ProviderAffiliation ProviderAffiliationGet (String token, Int64 providerAffiliationId);


        [OperationContract]
        ProviderContractCollectionResponse ProviderContractsGet (String token, Int64 providerId);

        [OperationContract]
        Server.Core.Provider.ProviderContract ProviderContractGet (String token, Int64 providerContractId);


        [OperationContract]
        ProviderServiceLocationCollectionResponse ProviderServiceLocationsGet (String token, Int64 providerId);

        #endregion


        #region Core - Sponsor

        [OperationContract]
        Server.Core.Sponsor.Sponsor SponsorGet (String token, Int64 sponsorId);

        #endregion 


        #region Core.Work

        #region Routing Rules

        [OperationContract]
        RoutingRuleCollectionResponse RoutingRulesAvailable (String token);

        [OperationContract]
        Server.Core.Work.RoutingRule RoutingRuleGet (String token, Int64 routingRuleId);

        [OperationContract]
        ObjectSaveResponse RoutingRuleSave (String token, Server.Core.Work.RoutingRule routingRule);

        #endregion 


        #region Work - Workflow

        [OperationContract]
        WorkflowCollectionResponse WorkflowsAvailable(String token);


        [OperationContract]
        Server.Core.Work.Workflow WorkflowGet (String token, Int64 workflowId);

        [OperationContract]
        Server.Core.Work.Workflow WorkflowGetByName (String token, String workflowName);

        [OperationContract]
        Server.Core.Work.Workflow WorkflowGetByWorkQueueId (String token, Int64 workQueueId);


        [OperationContract]
        ObjectSaveResponse WorkflowSave (String token, Server.Core.Work.Workflow workflow);

        #endregion


        #region Work - Workflow Flow Control

        [OperationContract]
        WorkflowResponse WorkflowStart (String token, Server.Workflows.WorkflowStartRequest startRequest);

        [OperationContract]
        WorkflowResponse WorkflowContinue (String token, String workflowName, Guid workflowInstanceId, Server.Workflows.UserInteractions.Response.ResponseBase userInteractionResponse);

        #endregion 


        #region Work - Work Outcome

        [OperationContract]
        WorkOutcomeCollectionResponse WorkOutcomesAvailable (String token);

        [OperationContract]
        DictionaryResponse WorkOutcomeDictionary (String token);


        [OperationContract]
        Server.Core.Work.WorkOutcome WorkOutcomeGet (String token, Int64 workOutcomeId);

        [OperationContract]
        Server.Core.Work.WorkOutcome WorkOutcomeGetByName (String token, String workOutcomeName);

        [OperationContract]
        ObjectSaveResponse WorkOutcomeSave (String token, Server.Core.Work.WorkOutcome workOutcome);

        #endregion 


        #region Work - Work Queue

        [OperationContract]
        WorkQueueCollectionResponse WorkQueuesAvailable (String token);

        [OperationContract]
        DictionaryResponse WorkQueueDictionary (String token);


        [OperationContract]
        Server.Core.Work.WorkQueue WorkQueueGet (String token, Int64 workQueueId);

        [OperationContract]
        Server.Core.Work.WorkQueue WorkQueueGetByName (String token, String workQueueName);

        [OperationContract]
        Server.Core.Work.WorkQueue WorkQueueGetByWorkQueueItem (String token, Int64 workQueueItemId);


        [OperationContract]
        ObjectSaveResponse WorkQueueSave (String token, Server.Core.Work.WorkQueue workQueue);

        [OperationContract]
        ObjectSaveResponse WorkQueueSaveGetWork (String token, Server.Core.Work.WorkQueue workQueue);


        [OperationContract]
        GetWorkResponse WorkQueueGetWork (String token, Int64 workQueueId);

        [OperationContract]
        BooleanResponse WorkQueueInsertEntity (String token, Int64 workQueueId, Int64 entityId, Server.Core.CoreObject sender, Server.Core.CoreObject eventObject, Int64 eventInstanceId, String eventDescription, Int32 priority);

        #endregion


        #region Work Queue Item 
        
        [OperationContract]
        Server.Core.Work.WorkQueueItem WorkQueueItemGet (String token, Int64 workQueueItemId);


        [OperationContract]
        BooleanResponse WorkQueueItemAssignTo (String token, Int64 workQueueItemId, Int64 securityAuthorityId, String userAccountId, String userAccountName, String userDisplayName, String assignmentSource);

        [OperationContract]
        BooleanResponse WorkQueueItemMoveToQueue (String token, Int64 workQueueItemId, Int64 workQueueId);

        [OperationContract]
        BooleanResponse WorkQueueItemClose (String token, Int64 workQueueItemId, Int64 workOutcomeId);

        [OperationContract]
        BooleanResponse WorkQueueItemSuspend (String token, Int64 workQueueItemId, String lastStep, String nextStep, Int32 constraintDays, Int32 milestoneDays, Boolean releaseItem);


        [OperationContract]
        WorkQueueItemSenderCollectionResponse WorkQueueItemSendersGet (String token, Int64 workQueueItemId);

        [OperationContract]
        WorkQueueItemAssignmentHistoryCollectionResponse WorkQueueItemAssignmentHistoryGet (String token, Int64 workQueueItemId);

        [OperationContract]
        WorkQueueItemWorkflowStepCollectionResponse WorkQueueItemWorkflowStepsGet (String token, Int64 workQueueItemId);

        #endregion 


        #region Work Queue Items (By Views)

        [OperationContract]
        Int64 WorkQueueItemsGetCount (String token, List<Server.Data.FilterDescriptor> filters);

        [OperationContract]
        Int64 WorkQueueItemsGetCountByView (String token, Server.Core.Work.WorkQueueView workQueueView, List<Server.Data.FilterDescriptor> filters);

        [OperationContract]
        WorkQueueItemCollectionResponse WorkQueueItemsGetByViewPage (String token, Server.Core.Work.WorkQueueView workQueueView, List<Server.Data.FilterDescriptor> filters, List<Server.Data.SortDescriptor> sorts, Int32 initialRow, Int32 count);

        #endregion 


        #region Work - Work Team 
        
        [OperationContract]
        WorkTeamCollectionResponse WorkTeamsAvailable (String token);

        [OperationContract]
        WorkTeamCollectionResponse WorkTeamsForSession (String token);


        [OperationContract]
        Server.Core.Work.WorkTeam WorkTeamGet (String token, Int64 workTeamId);

        [OperationContract]
        Server.Core.Work.WorkTeam WorkTeamGetByName (String token, String workTeamName);

        
        [OperationContract]
        ObjectSaveResponse WorkTeamSave (String token, Server.Core.Work.WorkTeam workTeam);

        #endregion


        #region Work - Work Queue View

        [OperationContract]
        WorkQueueViewCollectionResponse WorkQueueViewsAvailable (String token);

        [OperationContract]
        DictionaryResponse WorkQueueViewDictionary (String token);


        [OperationContract]
        Server.Core.Work.WorkQueueView WorkQueueViewGet (String token, Int64 workQueueViewId);

        [OperationContract]
        Server.Core.Work.WorkQueueView WorkQueueViewGetByName (String token, String workQueueViewName);

        [OperationContract]
        ObjectSaveResponse WorkQueueViewSave (String token, Server.Core.Work.WorkQueueView workQueueView);


        [OperationContract]
        DictionaryStringResponse WorkQueueView_ValidateFieldDefinition (String token, Server.Core.Work.WorkQueueView workQueueView, Server.Core.Work.WorkQueueViewFieldDefinition fieldDefinition);

        [OperationContract]
        Dictionary<String, Boolean> WorkQueueView_WellKnownFields (String token, Server.Core.Work.WorkQueueView workQueueView);

        #endregion


        #region Work Queue Monitor

        [OperationContract]
        WorkQueueSummaryCollectionResponse WorkQueueMonitorSummary (String token);

        [OperationContract]
        DictionaryKeyCountResponse WorkQueueMonitorAgingByWorkQueue (String token, Int64 workQueueId);

        [OperationContract]
        DictionaryKeyCountResponse WorkQueueMonitorAgingAvailableByWorkQueue (String token, Int64 workQueueId);

        #endregion 

        #endregion


        #region DataExplorer

        [OperationContract]
        DataExplorerCollectionResponse DataExplorersAvailable (String token);

        [OperationContract]
        Server.Core.DataExplorer.DataExplorer DataExplorerGet (String token, Int64 dataExporerId);

        [OperationContract]
        ObjectSaveResponse DataExplorerSave (String token, Server.Core.DataExplorer.DataExplorer dataExplorer);

        [OperationContract]
        DataExplorerNodeExecutionResponse DataExplorerNodeExecute (String token, Server.Core.DataExplorer.DataExplorer dataExplorer, Guid nodeInstanceId);

        [OperationContract]
        Int64CollectionResponse DataExplorerNodeResultsGet (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        MemberCollectionResponse DataExplorerNodeResultsGetForMember (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        EntityAddressCollectionResponse DataExplorerNodeResultsGetForMemberEntityCurrentAddress (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        EntityContactInformationCollectionResponse DataExplorerNodeResultsGetForMemberEntityCurrentContactInformation (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        MemberEnrollmentCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollment (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        MemberEnrollmentCoverageCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollmentCoverage (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        [OperationContract]
        MemberEnrollmentPcpCollectionResponse DataExplorerNodeResultsGetForMemberCurrentEnrollmentPcp (String token, Guid nodeInstanceId, Int32 initialRow, Int32 count);

        #endregion


        #region Core  - Reporting Server

        [OperationContract]
        ReportingServerCollectionResponse ReportingServersAvailable (String token);
        
        [OperationContract]
        DictionaryResponse ReportingServerDictionary (String token);
        
        [OperationContract]
        Server.Reporting.ReportingServer ReportingServerGet (String token, Int64 reportingServerId);

        [OperationContract]
        Server.Reporting.ReportingServer ReportingServerGetByName (String token, String reportingServerName);
        
        [OperationContract]
        ObjectSaveResponse ReportingServerSave (String token, Server.Reporting.ReportingServer reportingServer);

        #endregion


        #region Core  - Fax Server

        [OperationContract]
        FaxServerCollectionResponse FaxServersAvailable (String token);

        [OperationContract]
        DictionaryResponse FaxServerDictionary (String token);

        [OperationContract]
        Server.Faxing.FaxServer FaxServerGet (String token, Int64 reportingServerId);

        [OperationContract]
        Server.Faxing.FaxServer FaxServerGetByName (String token, String reportingServerName);

        [OperationContract]
        ObjectSaveResponse FaxServerSave (String token, Server.Faxing.FaxServer reportingServer);

        #endregion


        #region Core  - Printers

        [OperationContract]
        PrinterCollectionResponse PrintersAvailable (String token);

        [OperationContract]
        DictionaryStringResponse PrintQueuesAvailable (String token, String printServerName);

        [OperationContract]
        DictionaryResponse PrinterDictionary (String token);

        [OperationContract]
        Server.Printing.Printer PrinterGet (String token, Int64 printerId);

        [OperationContract]
        Server.Printing.Printer PrinterGetByName (String token, String printerName);
        
        [OperationContract]
        Server.Printing.PrinterCapabilities PrinterCapabilitiesGet (String token, String printServerName, String printQueueName);

        [OperationContract]
        ObjectSaveResponse PrinterSave (String token, Server.Printing.Printer printer);

        #endregion


        #region Search Operations

        [OperationContract]
        SearchResultsGlobalResponse SearchGlobal (String token, String criteria);

        [OperationContract]
        SearchResultsMemberResponse SearchMemberByName (String token, String memberName, DateTime? birthDate);

        [OperationContract]
        SearchResultsMemberResponse SearchMemberById (String token, String memberId);

        [OperationContract]
        SearchResultsMemberResponse SearchMember (String token, String memberName, DateTime? birthDate, String memberId);

        [OperationContract]
        SearchResultsProviderResponse SearchProvider (String token, String memberName, String providerId);

        [OperationContract]
        SearchResultsProviderResponse SearchProviderByName (String token, String providerName);

        #endregion

    }

}
