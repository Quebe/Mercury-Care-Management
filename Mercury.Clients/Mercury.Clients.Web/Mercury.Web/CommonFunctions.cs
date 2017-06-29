using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Mercury.Web {

    public class CommonFunctions {

        static public String MemberProfileAnchor (Int64 memberId, String memberName) {

            String anchor = String.Empty;

            anchor = "'/Application/Member/MemberProfile.aspx?MemberId=" + memberId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", 'MemberProfile_" + memberId.ToString () + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Member Profile\" alt=\"Open Member Profile\">" + memberName + "</a>";

            return anchor;

        }

        static public String MemberProfileAnchorOld (Int64 memberId, String memberName) {

            String anchor = String.Empty;

            anchor = "'/Application/Member/MemberProfile.aspx?MemberId=" + memberId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", '_blank', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Member Profile\" alt=\"Open Member Profile\">" + memberName + "</a>";

            return anchor;

        }

        static public String ProviderProfileAnchor (Int64 providerId, String providerName) {

            String anchor = String.Empty;

            anchor = "'/Application/Provider/ProviderProfile.aspx?ProviderId=" + providerId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", 'ProviderProfile_" + providerId.ToString () + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Provider Profile\" alt=\"Open Provider Profile\">" + providerName + "</a>";

            return anchor;

        }

        static public String ProviderProfileAnchor (Mercury.Client.Application application, Int64 providerId) {

            String anchor = String.Empty;

            String providerName = String.Empty;

            Client.Core.Provider.Provider provider = application.ProviderGet (providerId, true);

            if (provider != null) { providerName = provider.Name; }

            if (!String.IsNullOrWhiteSpace (providerName)) {

                anchor = ProviderProfileAnchor (providerId, providerName);

            }
            
            return anchor;

        }

        static public String ProviderProfileAnchorOld (Int64 providerId, String providerName) {

            String anchor = String.Empty;

            anchor = "'/Application/Provider/ProviderProfile.aspx?ProviderId=" + providerId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", '_blank', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Provider Profile\" alt=\"Open Provider Profile\">" + providerName + "</a>";

            return anchor;

        }

        static public String FormAnchor (Int64 entityFormId, String formName) {

            String anchor = "'/Application/Forms/FormViewer/FormViewer.aspx?EntityFormId=" + entityFormId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", '" + (Guid.NewGuid ().ToString ().Replace ("-", "")) + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Form\" alt=\"Open Form\">" + formName + "</a>";

            return anchor;

        }

        static public String CaseAnchor (Int64 memberCaseId, String caseDescription) {

            String anchor = "'/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + memberCaseId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", '_blank', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Case\" alt=\"Open Case\">" + caseDescription + "</a>";

            return anchor;

        }

        static public String CaseAnchor (Int64 memberCaseId, Int64 memberId, String caseDescription, String target) {

            String anchor = "'/Application/MemberCase/MemberCase.aspx?MemberCaseId=" + memberCaseId.ToString () + "&MemberId=" + memberId.ToString () + "'";

            anchor = "<a href=\"#\" onclick=\"javascript:window.open(" + anchor + ", '" + target + "', 'location=no,directories=no,menubar=no,toolbar=no,resizable=yes,scrollbars=yes,status=yes')\" title=\"Open Case\" alt=\"Open Case\">" + caseDescription + "</a>";

            return anchor;

        }

        static public String CoreObjectHyperLink (Mercury.Client.Core.Work.WorkQueueItem forWorkQueueItem) {

            if (forWorkQueueItem == null) { return String.Empty; }


            String hyperLink = String.Empty;

            switch (forWorkQueueItem.ItemObjectType) {

                case "Member": hyperLink = MemberProfileAnchor (forWorkQueueItem.ItemObjectId, forWorkQueueItem.Name); break;

                case "Provider": hyperLink = ProviderProfileAnchor (forWorkQueueItem.ItemObjectId, forWorkQueueItem.Name); break;

                default:

                    hyperLink = "<span title=\"" + forWorkQueueItem.Description + "\">" + forWorkQueueItem.Name + "</span>";

                    break;

            }

            return hyperLink;

        }

        static public String DiagnosisDescription (Mercury.Client.Application application, String diagnosisCode, Int32 diagnosisVersion) {

            if (String.IsNullOrWhiteSpace (diagnosisCode)) { return String.Empty; }

            String diagnosisDescription = application.DiagnosisDescription (diagnosisCode, diagnosisVersion);

            String span = "<span title=\"" + diagnosisDescription + "\">" + diagnosisCode + " [" + diagnosisVersion.ToString () + "]</span>";

            return span;

        }

        static public String TitleSpan (String title, String innerText) {

            String span = innerText;

            if (!String.IsNullOrWhiteSpace (title)) { span = "<span title=\"" + title + "\">" + innerText + "</span>"; }
            
            return span;

        }

    }

}
