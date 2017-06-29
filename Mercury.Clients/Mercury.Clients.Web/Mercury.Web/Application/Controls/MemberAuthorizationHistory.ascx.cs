using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberAuthorizationHistory : System.Web.UI.UserControl {

        #region Session Properties

        private String SessionCachePrefix {

            get {

                if (String.IsNullOrEmpty (UserControlInstanceId.Text)) { UserControlInstanceId.Text = Guid.NewGuid ().ToString ().Replace ("-", ""); }

                return UserControlInstanceId.Text + ".";

            }

        }

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }


        public Client.Core.Member.Member Member {

            get { return (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"]; }

            set {

                Client.Core.Member.Member member = (Client.Core.Member.Member) Session[SessionCachePrefix + "Member"];

                if (member != value) {

                    Session[SessionCachePrefix + "Member"] = value;

                    InitializeMemberAuthorizationHistoryGrid ();

                    MemberAuthorizationHistoryGrid_ManualDataRebind ();

                }

            }

        }


        private System.Data.DataTable MemberAuthorizationHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("AuthorizationId");

                    dataTable.Columns.Add ("AuthorizationNumber");

                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("ReferringProvider");

                    dataTable.Columns.Add ("ServiceProvider");

                    dataTable.Columns.Add ("ServiceProviderSpecialtyName");

                    dataTable.Columns.Add ("AuthorizationType");

                    dataTable.Columns.Add ("EffectiveDate");

                    dataTable.Columns.Add ("TerminationDate");

                    dataTable.Columns.Add ("AuthorizationStatus");

                    dataTable.Columns.Add ("PrincipalDiagnosisCode");

                    dataTable.Columns.Add ("PrincipalDiagnosisDescription");

                    dataTable.Columns.Add ("AdmittingDiagnosisCode");

                    dataTable.Columns.Add ("AdmittingDiagnosisDescription");

                    dataTable.Columns.Add ("DischargeDiagnosisCode");

                    dataTable.Columns.Add ("DischargeDiagnosisDescription");

                    dataTable.Columns.Add ("AssignedToUserAccountName");

                    dataTable.Columns.Add ("ModifiedBy");

                    dataTable.Columns.Add ("ModifiedDate");
                    
                    Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_DataTable"] = dataTable;
                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_DataTable"] = value; }

        }

        private System.Data.DataTable MemberAuthorizationHistoryGrid_AuthorizationLineTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_AuthorizationLineTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("AuthorizationId");

                    dataTable.Columns.Add ("LineNumber");

                    dataTable.Columns.Add ("LineStatus");


                    dataTable.Columns.Add ("ServiceDate");

                    dataTable.Columns.Add ("AdmissionDate");

                    dataTable.Columns.Add ("DischargeDate");


                    dataTable.Columns.Add ("RevenueCode");

                    dataTable.Columns.Add ("RevenueDescription");

                    dataTable.Columns.Add ("ServiceCode");

                    dataTable.Columns.Add ("ServiceDescription");

                    dataTable.Columns.Add ("ModifiedCode1");

                    dataTable.Columns.Add ("UtilizedUnits");

                    dataTable.Columns.Add ("Units");


                    Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_AuthorizationLineTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_AuthorizationLineTable"] = value; }

        }

        private Int32 MemberAuthorizationHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberAuthorizationHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_PageSize"] = value; }

        }

        private Int32 MemberAuthorizationHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberAuthorizationHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight {

            get { return MemberAuthorizationHistoryGrid.Height; }

            set {

                Unit heightValue = new Unit (value.Value - 28, value.Type);

                MemberAuthorizationHistoryGrid.Height = heightValue;

            }

        }

        public Int32 HistoryGridPageSize {

            get { return MemberAuthorizationHistoryGrid.PageSize; }

            set {

                MemberAuthorizationHistoryGrid_PageSize = value;

            }

        }


        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }


        protected void InitializeMemberAuthorizationHistoryGrid () {

            MemberAuthorizationHistoryGrid.CurrentPageIndex = 0;

            MemberAuthorizationHistoryGrid.PageSize = MemberAuthorizationHistoryGrid_PageSize;

            MemberAuthorizationHistoryGrid_Count = 0;

            MemberAuthorizationHistoryGrid.DataSource = MemberAuthorizationHistoryGrid_DataTable;

            MemberAuthorizationHistoryGrid.MasterTableView.DetailTables[0].DataSource = MemberAuthorizationHistoryGrid_AuthorizationLineTable;

            MemberAuthorizationHistoryGrid.DataBind ();

            return;

        }

        #endregion


        #region Member Authorization History Grid Events

        protected void MemberAuthorizationHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            #region Validate Call

            if (MercuryApplication == null) { return; }

            if (Member == null) { return; }

            #endregion


            Boolean needsData = false;

            needsData = needsData || (MemberAuthorizationHistoryGrid_Count == 0);

            needsData = needsData || (MemberAuthorizationHistoryGrid_DataTable.Rows.Count == 0);

            needsData = needsData || (MemberAuthorizationHistoryGrid_PageSize != MemberAuthorizationHistoryGrid.PageSize);

            needsData = needsData || (MemberAuthorizationHistoryGrid_CurrentPage != MemberAuthorizationHistoryGrid.CurrentPageIndex);



            System.Data.DataTable dataTable = MemberAuthorizationHistoryGrid_DataTable;

            if (needsData) {

                MemberAuthorizationHistoryGrid.PageSize = MemberAuthorizationHistoryGrid_PageSize;

                dataTable.Rows.Clear ();

                if (MemberAuthorizationHistoryGrid_Count == 0) {

                    MemberAuthorizationHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.MemberAuthorizationsGetCount (Member.Id));

                    MemberAuthorizationHistoryGrid.VirtualItemCount = Convert.ToInt32 (MemberAuthorizationHistoryGrid_Count);

                }

                MemberAuthorizationHistoryGrid_CurrentPage = MemberAuthorizationHistoryGrid.CurrentPageIndex;

                List<Mercury.Server.Application.Authorization> memberAuthorizations;

                Int32 initialRow = MemberAuthorizationHistoryGrid.CurrentPageIndex * MemberAuthorizationHistoryGrid.PageSize + 1;

                memberAuthorizations = MercuryApplication.MemberAuthorizationsGetByPage (Member.Id, initialRow, MemberAuthorizationHistoryGrid.PageSize);


                foreach (Mercury.Server.Application.Authorization currentAuthorization in memberAuthorizations) {

                    // String principalDiagnosisInformation = "<span title=\"" + currentAuthorization.PrincipalDiagnosisDescription + "\">" + currentAuthorization.PrincipalDiagnosisCode + "</span>";

                    // String admittingDiagnosisInformation = "<span title=\"" + currentAuthorization.AdmittingDiagnosisDescription + "\">" + currentAuthorization.AdmittingDiagnosisCode + "</span>";

                    // String dischargeDiagnosisInformation = "<span title=\"" + currentAuthorization.DischargeDiagnosisDescription + "\">" + currentAuthorization.DischargeDiagnosisCode + "</span>";


                    String principalDiagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentAuthorization.PrincipalDiagnosisCode, currentAuthorization.PrincipalDiagnosisVersion);

                    String admittingDiagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentAuthorization.AdmittingDiagnosisCode, currentAuthorization.AdmittingDiagnosisVersion);

                    String dischargeDiagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentAuthorization.DischargeDiagnosisCode, currentAuthorization.DischargeDiagnosisVersion);



                    // Client.Core.Provider serviceProvider = MercuryApplication.ProviderGet (currentAuthorization.ServiceProviderId);

                    // String serviceProviderInformation = "<span title=\"" + ((serviceProvider != null) ? serviceProvider.

                    dataTable.Rows.Add (

                        currentAuthorization.Id.ToString (),

                        currentAuthorization.AuthorizationNumber,

                        currentAuthorization.MemberId.ToString (),

                        CommonFunctions.ProviderProfileAnchor (MercuryApplication, currentAuthorization.ReferringProviderId),

                        CommonFunctions.ProviderProfileAnchor (MercuryApplication, currentAuthorization.ServiceProviderId),

                        currentAuthorization.ServiceProviderSpecialtyName,

                        currentAuthorization.AuthorizationCategory + " - " + currentAuthorization.AuthorizationSubcategory + " - " + currentAuthorization.AuthorizationServiceType,

                        currentAuthorization.EffectiveDate.ToString ("MM/dd/yyyy"),

                        currentAuthorization.TerminationDate.ToString ("MM/dd/yyyy"),

                        currentAuthorization.AuthorizationStatus,

                        (!String.IsNullOrEmpty (currentAuthorization.PrincipalDiagnosisCode)) ? principalDiagnosisInformation : "&nbsp",

                        currentAuthorization.PrincipalDiagnosisDescription,

                        (!String.IsNullOrEmpty (currentAuthorization.AdmittingDiagnosisCode)) ? admittingDiagnosisInformation : "&nbsp",

                        currentAuthorization.AdmittingDiagnosisDescription,

                        (!String.IsNullOrEmpty (currentAuthorization.DischargeDiagnosisCode)) ? dischargeDiagnosisInformation : "&nbsp",

                        currentAuthorization.DischargeDiagnosisDescription,

                        currentAuthorization.AssignedToUserAccountName,

                        currentAuthorization.ModifiedAccountInfo.UserAccountName,

                        currentAuthorization.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                    );

                }

                MemberAuthorizationHistoryGrid_DataTable = dataTable;

            }

            MemberAuthorizationHistoryGrid.DataSource = MemberAuthorizationHistoryGrid_DataTable;

            MemberAuthorizationHistoryGrid.MasterTableView.DetailTables[0].DataSource = MemberAuthorizationHistoryGrid_AuthorizationLineTable;

            return;

        }

        protected void MemberAuthorizationHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable authorizationLineTable = MemberAuthorizationHistoryGrid_AuthorizationLineTable;


            if ((eventArgs.CommandName == "ExpandCollapse")) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                Int64 authorizationId;


                if (Int64.TryParse (gridItem["AuthorizationId"].Text, out authorizationId)) {

                    authorizationLineTable.Rows.Clear ();

                    List<Mercury.Server.Application.AuthorizationLine> authorizationLines;

                    authorizationLines = MercuryApplication.AuthorizationLinesGet (authorizationId);

                    foreach (Mercury.Server.Application.AuthorizationLine currentDetail in authorizationLines) {

                        String revenueInformation = "<span title=\"" + currentDetail.RevenueDescription + "\">" + currentDetail.RevenueCode + "</span>";

                        String serviceInformation = "<span title=\"" + currentDetail.ServiceDescription + "\">" + currentDetail.ServiceCode + "</span>";

                        authorizationLineTable.Rows.Add (

                            currentDetail.AuthorizationId.ToString (),

                            currentDetail.LineNumber.ToString (),

                            currentDetail.Status,

                            currentDetail.ServiceDate.ToString ("MM/dd/yyyy"),

                            (currentDetail.AdmissionDate.HasValue) ? currentDetail.AdmissionDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                            (currentDetail.DischargeDate.HasValue) ? currentDetail.DischargeDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                            (!String.IsNullOrEmpty (currentDetail.RevenueCode)) ? revenueInformation : "&nbsp",

                            currentDetail.RevenueDescription,

                            (!String.IsNullOrEmpty (currentDetail.ServiceCode)) ? serviceInformation : "&nbsp",

                            currentDetail.ServiceDescription,

                            currentDetail.ModifierCode1,

                            currentDetail.UtilizedUnits, 
                            
                            currentDetail.Units

                        );

                    }


                    MemberAuthorizationHistoryGrid_AuthorizationLineTable = authorizationLineTable;

                    MemberAuthorizationHistoryGrid.MasterTableView.DetailTables[0].DataSource = authorizationLineTable;

                }

            }

            return;

        }

        protected void MemberAuthorizationHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberAuthorizationHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberAuthorizationHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberAuthorizationHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        public void MemberAuthorizationHistoryGrid_ManualDataRebind () {

            MemberAuthorizationHistoryGrid_Count = 0;

            MemberAuthorizationHistoryGrid.DataSource = null;

            MemberAuthorizationHistoryGrid.Rebind ();

            return;

        }

        #endregion


    }

}