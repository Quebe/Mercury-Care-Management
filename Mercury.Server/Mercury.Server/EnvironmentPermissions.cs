using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mercury.Server {

    public class EnvironmentPermissions {

        public const String EnvironmentAdministrator = ".EnvironmentAdministrator";

        public const String ConfigurationManagement = "Environment.ConfigurationManagement.Review";

        public const String FormDesigner = "Environment.Configuration.FormDesigner";

        public const String Automation = "Environment.Automation";

        public const String DataExplorer = "Environment.DataExplorer";


        public const String RoleReview = "Environment.Role.Review";

        public const String RoleManage = "Environment.Role.Manage";


        public const String ConfigurationImportExport = "Environment.ConfigurationManagement.ImportExport";

        
        public const string ReportingServerReview = "EnterpriseManagement.Environment.ReportingServer.Review";

        public const string ReportingServerManage = "EnterpriseManagement.Environment.ReportingServer.Manage";


        public const string FaxServerReview = "EnterpriseManagement.Environment.FaxServer.Review";

        public const string FaxServerManage = "EnterpriseManagement.Environment.FaxServer.Manage";


        public const string PrinterReview = "EnterpriseManagement.Environment.Printer.Review";

        public const string PrinterManage = "EnterpriseManagement.Environment.Printer.Manage";


        public const String MedicalServiceReview = "Environment.ConfigurationManagement.MedicalService.Review";

        public const String MedicalServiceManage = "Environment.ConfigurationManagement.MedicalService.Manage";


        public const String MetricReview = "Environment.ConfigurationManagement.Metric.Review";

        public const String MetricManage = "Environment.ConfigurationManagement.Metric.Manage";


        public const String AuthorizedServiceReview = "Environment.ConfigurationManagement.AuthorizedService.Review";

        public const String AuthorizedServiceManage = "Environment.ConfigurationManagement.AuthorizedService.Manage";


        public const String NoteTypeReview = "Environment.ConfigurationManagement.NoteType.Review";

        public const String NoteTypeManage = "Environment.ConfigurationManagement.NoteType.Manage";


        public const String ContactRegardingReview = "Environment.ConfigurationManagement.ContactRegarding.Review";

        public const String ContactRegardingManage = "Environment.ConfigurationManagement.ContactRegarding.Manage";


        public const String CorrespondenceReview = "Environment.ConfigurationManagement.Correspondence.Review";

        public const String CorrespondenceManage = "Environment.ConfigurationManagement.Correspondence.Manage";


        public const String WorkflowReview = "Environment.ConfigurationManagement.Workflow.Review";

        public const String WorkflowManage = "Environment.ConfigurationManagement.Workflow.Manage";


        public const String WorkTeamReview = "Environment.ConfigurationManagement.WorkTeam.Review";

        public const String WorkTeamManage = "Environment.ConfigurationManagement.WorkTeam.Manage";


        public const String WorkQueueReview = "Environment.ConfigurationManagement.WorkQueue.Review";

        public const String WorkQueueManage = "Environment.ConfigurationManagement.WorkQueue.Manage";


        public const String WorkQueueViewReview = "Environment.ConfigurationManagement.WorkQueueView.Review";

        public const String WorkQueueViewManage = "Environment.ConfigurationManagement.WorkQueueView.Manage";


        public const String WorkOutcomeReview = "Environment.ConfigurationManagement.WorkOutcome.Review";

        public const String WorkOutcomeManage = "Environment.ConfigurationManagement.WorkOutcome.Manage";


        public const String RoutingRuleReview = "Environment.ConfigurationManagement.RoutingRule.Review";

        public const String RoutingRuleManage = "Environment.ConfigurationManagement.RoutingRule.Manage";


        public const String PopulationTypeReview = "Environment.ConfigurationManagement.PopulationType.Review";

        public const String PopulationTypeManage = "Environment.ConfigurationManagement.PopulationType.Manage";



        public const String ConditionReview = "Environment.ConfigurationManagement.Condition.Review";

        public const String ConditionManage = "Environment.ConfigurationManagement.Condition.Manage";


        public const String PopulationReview = "Environment.ConfigurationManagement.Population.Review";

        public const String PopulationManage = "Environment.ConfigurationManagement.Population.Manage";


        public const String ProblemStatementReview = "Environment.ConfigurationManagement.ProblemStatement.Review";

        public const String ProblemStatementManage = "Environment.ConfigurationManagement.ProblemStatement.Manage";


        public const String CareMeasureScaleReview = "ConfigurationManagement.CareMeasureScale.Review";

        public const String CareMeasureScaleManage = "ConfigurationManagement.CareMeasureScale.Manage";

        public const String CareMeasureReview = "ConfigurationManagement.CareMeasure.Review";

        public const String CareMeasureManage = "ConfigurationManagement.CareMeasure.Manage";

        public const String CareInterventionReview = "ConfigurationManagement.CareIntervention.Review";

        public const String CareInterventionManage = "ConfigurationManagement.CareIntervention.Manage";


        public const String CarePlanReview = "Environment.ConfigurationManagement.CarePlan.Review";

        public const String CarePlanManage = "Environment.ConfigurationManagement.CarePlan.Manage";


        public const String CareLevelReview = "Environment.ConfigurationManagement.CareLevel.Review";

        public const String CareLevelManage = "Environment.ConfigurationManagement.CareLevel.Manage";


        public const String CareOutcomeReview = "Environment.ConfigurationManagement.CareOutcome.Review";

        public const String CareOutcomeManage = "Environment.ConfigurationManagement.CareOutcome.Manage";


        public const String WidgetReview = "Environment.ConfigurationManagement.Widget.Review";

        public const String WidgetManage = "Environment.ConfigurationManagement.Widget.Manage";


        #region Insurer

        public const String InsuranceTypeReview = "Environment.Configuration.InsuranceType.Review";

        public const String InsuranceTypeManage = "Environment.Configuration.InsuranceType.Manage";

        #endregion 

        
        public const String DataExplorerReview = "Environment.DataExplorer.Review";

        public const String DataExplorerManage = "Environment.DataExplorer.Manage";


        public const String MemberServiceReview = "Environment.Member.Service.Review";

        public const String MemberServiceManage = "Environment.Member.Service.Manage";


        public const String MemberMetricReview = "Environment.Member.Metric.Review";

        public const String MemberMetricManage = "Environment.Member.Metric.Manage";


        public const String MemberCaseReview = "Environment.Member.Case.Review";

        public const String MemberCaseManage = "Environment.Member.Case.Manage";


        public const String MemberActionContact = "Environment.Member.Action.Contact";

        public const String MemberActionSendCorrespondence = "Environment.Member.Action.SendCorrespondence";

        public const String MemberActionDataEnterForm = "Environment.Member.Action.DataEnterForm";


        public const String MemberAddressManage = "Environment.Member.Address.Manage";

        public const String MemberContactInformationManage = "Environment.Member.ContactInformation.Manage";


        public const String MemberNoteRead = "Environment.Member.Note.Read";

        public const String MemberNoteAdd = "Environment.Member.Note.Add";

        public const String MemberNoteModify = "Environment.Member.Note.Modify";

        public const String MemberNoteAppend = "Environment.Member.Note.Append";

        public const String MemberNoteTerminate = "Environment.Member.Note.Terminate";


        public const String ProviderAddressManage = "Environment.Provider.Address.Manage";

        public const String ProviderContactInformationManage = "Environment.Provider.ContactInformation.Manage";


        public const String ProviderActionContact = "Environment.Provider.Action.Contact";

        public const String ProviderActionSendCorrespondence = "Environment.Provider.Action.SendCorrespondence";

        public const String ProviderActionDataEnterForm = "Environment.Provider.Action.DataEnterForm";


        public const String ProviderNoteRead = "Environment.Provider.Note.Read";

        public const String ProviderNoteAdd = "Environment.Provider.Note.Add";

        public const String ProviderNoteModify = "Environment.Provider.Note.Modify";

        public const String ProviderNoteAppend = "Environment.Provider.Note.Append";

        public const String ProviderNoteTerminate = "Environment.Provider.Note.Terminate";


        public const String FormReview = "Environment.ConfigurationManagement.Form.Review";

        public const String FormManage = "Environment.ConfigurationManagement.Form.Manage";


        public const String SearchMemberOptionalBirthDate = "Search.Member.OptionalBirthDate";

        public const String SearchResultsExtraLarge = "Search.Results.ExtraLarge";

        public const String SearchResultsLarge = "Search.Results.Large";

    }

}
