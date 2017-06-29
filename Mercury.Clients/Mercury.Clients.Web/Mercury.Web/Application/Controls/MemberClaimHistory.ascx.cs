using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Controls {

    public partial class MemberClaimHistory : System.Web.UI.UserControl {

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

                    InitializeMemberClaimHistoryGrid ();

                    MemberClaimHistoryGrid_ManualDataRebind ();

                    InitializeMemberPharmacyClaimHistoryGrid ();

                    MemberPharmacyClaimHistoryGrid_ManualDataRebind ();

                    InitializeMemberLabResultHistoryGrid ();

                    MemberLabResultHistoryGrid_ManualDataRebind ();
                }

            }

        }


        private System.Data.DataTable MemberClaimHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberClaimHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ClaimId");

                    dataTable.Columns.Add ("ClaimNumber");

                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("ServiceProviderId");

                    dataTable.Columns.Add ("PayToProviderId");

                    dataTable.Columns.Add ("ClaimForm");

                    dataTable.Columns.Add ("ClaimFromDate");

                    dataTable.Columns.Add ("ClaimThruDate");

                    dataTable.Columns.Add ("AdmissionDate");

                    dataTable.Columns.Add ("Status");

                    dataTable.Columns.Add ("BillType");

                    dataTable.Columns.Add ("PrimaryDiagnosisCode");

                    dataTable.Columns.Add ("PrimaryDiagnosisDescription");

                    dataTable.Columns.Add ("BillingProviderName");

                    dataTable.Columns.Add ("ServiceProviderName");

                    dataTable.Columns.Add ("BilledAmount");

                    dataTable.Columns.Add ("PaidAmount");

                    dataTable.Columns.Add ("PaidDate");

                    Session[SessionCachePrefix + "MemberClaimHistoryGrid_DataTable"] = dataTable;
                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberClaimHistoryGrid_DataTable"] = value; }

        }

        private System.Data.DataTable MemberClaimHistoryGrid_ClaimLineTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberClaimHistoryGrid_ClaimLineTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ClaimId");


                    dataTable.Columns.Add ("Line");

                    dataTable.Columns.Add ("ServiceDateFrom");

                    dataTable.Columns.Add ("ServiceDateThru");

                    dataTable.Columns.Add ("LineStatus");

                    dataTable.Columns.Add ("LocationCode");

                    dataTable.Columns.Add ("RevenueCode");

                    dataTable.Columns.Add ("RevenueCodeName");

                    dataTable.Columns.Add ("ProcedureCode");

                    dataTable.Columns.Add ("ProcedureCodeName");

                    dataTable.Columns.Add ("ModifiedCode1");

                    dataTable.Columns.Add ("Units");

                    dataTable.Columns.Add ("BilledAmount");

                    dataTable.Columns.Add ("PaidAmount");

                    Session[SessionCachePrefix + "MemberClaimHistoryGrid_ClaimLineTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberClaimHistoryGrid_ClaimLineTable"] = value; }

        }

        private Int32 MemberClaimHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberClaimHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberClaimHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberClaimHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberClaimHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberClaimHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberClaimHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberClaimHistoryGrid_PageSize"] = value; }

        }

        private Int32 MemberClaimHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberClaimHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberClaimHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberClaimHistoryGrid_Count"] = value; }

        }


        private System.Data.DataTable MemberPharmacyClaimHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("ClaimId");


                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("ClaimDateFrom");

                    dataTable.Columns.Add ("ClaimDateThru");

                    dataTable.Columns.Add ("Status");

                    dataTable.Columns.Add ("NdcCode");

                    dataTable.Columns.Add ("DrugName");

                    dataTable.Columns.Add ("DaysSupply");

                    dataTable.Columns.Add ("DeaClassification");

                    dataTable.Columns.Add ("TherapeuticClassification");

                    dataTable.Columns.Add ("PharmacyName");

                    dataTable.Columns.Add ("ServiceProviderName");

                    dataTable.Columns.Add ("ServiceProviderSpecialtyName");

                    dataTable.Columns.Add ("BilledAmount");

                    dataTable.Columns.Add ("PaidAmount");

                    dataTable.Columns.Add ("ExternalClaimId");

                    Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_DataTable"] = value; }

        }

        private Int32 MemberPharmacyClaimHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32) Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberPharmacyClaimHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32) Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_PageSize"] = value; }

        }

        private Int32 MemberPharmacyClaimHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_Count"] != null) {

                    count = (Int32) Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberPharmacyClaimHistoryGrid_Count"] = value; }

        }


        private System.Data.DataTable MemberLabResultHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable)Session[SessionCachePrefix + "MemberLabResultHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("LabResultId");

                    dataTable.Columns.Add ("LabReferenceNumber");

                    dataTable.Columns.Add ("MemberId");

                    dataTable.Columns.Add ("ProviderId");

                    dataTable.Columns.Add ("ClaimId");

                    dataTable.Columns.Add ("ServiceDate");

                    dataTable.Columns.Add ("Loinc");

                    dataTable.Columns.Add ("LabTestName");

                    dataTable.Columns.Add ("LabValue");

                    dataTable.Columns.Add ("LabUnitType");

                    dataTable.Columns.Add ("LabResultText");

                    Session[SessionCachePrefix + "MemberLabResultHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberLabResultHistoryGrid_DataTable"] = value; }

        }

        private Int32 MemberLabResultHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberLabResultHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32)Session[SessionCachePrefix + "MemberLabResultHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberLabResultHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberLabResultHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberLabResultHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32)Session[SessionCachePrefix + "MemberLabResultHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set { Session[SessionCachePrefix + "MemberLabResultHistoryGrid_PageSize"] = value; }

        }

        private Int32 MemberLabResultHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberLabResultHistoryGrid_Count"] != null) {

                    count = (Int32)Session[SessionCachePrefix + "MemberLabResultHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberLabResultHistoryGrid_Count"] = value; }

        }


        public Unit HistoryGridHeight { 
            
            get { return MemberClaimHistoryGrid.Height; } 
            
            set {

                Unit heightValue = new Unit (value.Value - 28, value.Type);

                MemberClaimHistoryGrid.Height = heightValue;

                MemberPharmacyClaimHistoryGrid.Height = heightValue; 
            
            } 
        
        }

        public Int32 HistoryGridPageSize { 
            
            get { return MemberClaimHistoryGrid.PageSize; } 
            
            set {

                MemberClaimHistoryGrid_PageSize = value;

                MemberPharmacyClaimHistoryGrid_PageSize = value; 
            
            } 
        
        }


        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        #endregion


        #region Initializations

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            return;

        }


        protected void InitializeMemberClaimHistoryGrid () {

            MemberClaimHistoryGrid.CurrentPageIndex = 0;

            MemberClaimHistoryGrid.PageSize = MemberClaimHistoryGrid_PageSize;

            MemberClaimHistoryGrid_Count = 0;

            MemberClaimHistoryGrid.DataSource = MemberClaimHistoryGrid_DataTable;

            MemberClaimHistoryGrid.MasterTableView.DetailTables[0].DataSource = MemberClaimHistoryGrid_ClaimLineTable;

            MemberClaimHistoryGrid.DataBind ();

            return;

        }

        protected void InitializeMemberPharmacyClaimHistoryGrid () {

            MemberPharmacyClaimHistoryGrid.CurrentPageIndex = 0;

            MemberPharmacyClaimHistoryGrid.PageSize = MemberPharmacyClaimHistoryGrid_PageSize;

            MemberPharmacyClaimHistoryGrid_Count = 0;

            MemberPharmacyClaimHistoryGrid.DataSource = MemberPharmacyClaimHistoryGrid_DataTable;

            MemberPharmacyClaimHistoryGrid.DataBind ();

            return;

        }

        protected void InitializeMemberLabResultHistoryGrid () {

            MemberLabResultHistoryGrid.CurrentPageIndex = 0;

            MemberLabResultHistoryGrid.PageSize = MemberLabResultHistoryGrid_PageSize;

            MemberLabResultHistoryGrid_Count = 0;

            MemberLabResultHistoryGrid.DataSource = MemberLabResultHistoryGrid_DataTable;

            MemberLabResultHistoryGrid.DataBind ();

            return;

        }
        
        #endregion


        #region Member Claim History Grid Events

        protected void MemberClaimHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            #region Validate Call

            if (MercuryApplication == null) { return; }

            if (Member == null) { return; }

            #endregion


            Boolean needsData = false;

            needsData = needsData || (MemberClaimHistoryGrid_Count == 0);

            needsData = needsData || (MemberClaimHistoryGrid_DataTable.Rows.Count == 0);

            needsData = needsData || (MemberClaimHistoryGrid_PageSize != MemberClaimHistoryGrid.PageSize);

            needsData = needsData || (MemberClaimHistoryGrid_CurrentPage != MemberClaimHistoryGrid.CurrentPageIndex);

            

            System.Data.DataTable dataTable = MemberClaimHistoryGrid_DataTable;

            if (needsData) {

                MemberClaimHistoryGrid.PageSize = MemberClaimHistoryGrid_PageSize;

                dataTable.Rows.Clear ();

                if (MemberClaimHistoryGrid_Count == 0) {

                    MemberClaimHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.MemberClaimsGetCount (Member.Id));

                    MemberClaimHistoryGrid.VirtualItemCount = Convert.ToInt32 (MemberClaimHistoryGrid_Count);

                }

                MemberClaimHistoryGrid_CurrentPage = MemberClaimHistoryGrid.CurrentPageIndex;

                List<Server.Application.Claim> memberClaims;

                Int32 initialRow = MemberClaimHistoryGrid.CurrentPageIndex * MemberClaimHistoryGrid.PageSize + 1;

                memberClaims = MercuryApplication.MemberClaimsGetByPage (Member.Id, initialRow, MemberClaimHistoryGrid.PageSize);


                foreach (Server.Application.Claim currentClaim in memberClaims) {

                    // String diagnosisInformation = "<span title=\"" + currentClaim.PrimaryDiagnosisDescription + "\">" + currentClaim.PrimaryDiagnosisCode + "</span>";

                    String diagnosisInformation = CommonFunctions.DiagnosisDescription (MercuryApplication, currentClaim.PrincipalDiagnosisCode, currentClaim.PrincipalDiagnosisVersion);

                    String billTypeInformation = "<span title=\"" + MercuryApplication.BillTypeDescription (currentClaim.BillType) + "\">" + currentClaim.BillType + "</span>";

                    Client.Core.Provider.Provider billingProvider = MercuryApplication.ProviderGet (currentClaim.PayToProviderId, true);

                    Client.Core.Provider.Provider serviceProvider = MercuryApplication.ProviderGet (currentClaim.ServiceProviderId, true);

                    String billingProviderName = String.Empty;

                    String serviceProviderName = String.Empty;

                    if (billingProvider != null) {

                        billingProviderName = Web.CommonFunctions.ProviderProfileAnchor (billingProvider.Id, billingProvider.Name);

                    }

                    if (serviceProvider != null) {

                        serviceProviderName = Mercury.Web.CommonFunctions.ProviderProfileAnchor (serviceProvider.Id, serviceProvider.Name);

                    }


                    String claimStatusInformation = "<span title=\"" + currentClaim.DenialReason + "\">" + currentClaim.Status + ((!String.IsNullOrEmpty (currentClaim.DenialReason)) ? "*" : String.Empty) + "</span>";


                    dataTable.Rows.Add (

                        currentClaim.Id.ToString (),

                        currentClaim.ClaimNumber,

                        currentClaim.MemberId.ToString (),

                        currentClaim.ServiceProviderId.ToString (),

                        currentClaim.PayToProviderId.ToString (),

                        currentClaim.ClaimType.ToString (),

                        currentClaim.ClaimFromDate.ToString ("MM/dd/yyyy"),

                        currentClaim.ClaimThruDate.ToString ("MM/dd/yyyy"),

                        (currentClaim.AdmissionDate.HasValue) ? currentClaim.AdmissionDate.Value.ToString ("MM/dd/yyyy") : "&nbsp",

                        claimStatusInformation,

                        billTypeInformation,

                        diagnosisInformation,

                        currentClaim.PrincipalDiagnosisDescription,

                        billingProviderName,

                        serviceProviderName,

                        currentClaim.BilledAmount.ToString ("$###,###,##0.00"),

                        currentClaim.PaidAmount.ToString ("$###,###,##0.00"),

                        (currentClaim.PaidDate.HasValue) ? currentClaim.PaidDate.Value.ToString ("MM/dd/yyyy") : "&nbsp"

                    );

                }

                MemberClaimHistoryGrid_DataTable = dataTable;

            }

            MemberClaimHistoryGrid.DataSource = MemberClaimHistoryGrid_DataTable;

            MemberClaimHistoryGrid.MasterTableView.DetailTables[0].DataSource = MemberClaimHistoryGrid_ClaimLineTable;

            return;

        }

        protected void MemberClaimHistoryGrid_OnItemCommand (Object sender, Telerik.Web.UI.GridCommandEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable claimLineTable = MemberClaimHistoryGrid_ClaimLineTable;


            if ((eventArgs.CommandName == "ExpandCollapse")) {

                Telerik.Web.UI.GridDataItem gridItem = (Telerik.Web.UI.GridDataItem) eventArgs.Item;

                Int64 claimId;


                if (Int64.TryParse (gridItem["ClaimId"].Text, out claimId)) {

                    claimLineTable.Rows.Clear ();

                    List<Mercury.Server.Application.ClaimLine> claimLines;

                    claimLines = MercuryApplication.ClaimLinesGet (claimId);

                    if (claimLines != null) {

                        foreach (Mercury.Server.Application.ClaimLine currentDetail in claimLines) {

                            String revenueInformation = "<span title=\"" + currentDetail.RevenueCodeName + "\">" + currentDetail.RevenueCode + "</span>";

                            String serviceInformation = "<span title=\"" + currentDetail.ProcedureCodeName + "\">" + currentDetail.ProcedureCode + "</span>";

                            String claimLineStatusInformation = "<span title=\"" + currentDetail.DenialReason + "\">" + currentDetail.Status + ((!String.IsNullOrEmpty (currentDetail.DenialReason)) ? "*" : String.Empty) + "</span>";

                            claimLineTable.Rows.Add (

                                currentDetail.ClaimId.ToString (),

                                currentDetail.Line.ToString (),

                                currentDetail.ServiceDateFrom.ToString ("MM/dd/yyyy"),

                                currentDetail.ServiceDateThru.ToString ("MM/dd/yyyy"),

                                claimLineStatusInformation,

                                currentDetail.ServicePlace,

                                (!String.IsNullOrEmpty (currentDetail.RevenueCode)) ? revenueInformation : "&nbsp",

                                currentDetail.RevenueCodeName,

                                (!String.IsNullOrEmpty (currentDetail.ProcedureCode)) ? serviceInformation : "&nbsp",

                                currentDetail.ProcedureCodeName,

                                currentDetail.ModifierCode1,

                                currentDetail.Units,

                                currentDetail.BilledAmount,

                                currentDetail.PaidAmount

                            );

                        }

                    }


                    MemberClaimHistoryGrid_ClaimLineTable = claimLineTable;

                    MemberClaimHistoryGrid.MasterTableView.DetailTables[0].DataSource = claimLineTable;

                }

            }

            return;

        }

        protected void MemberClaimHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberClaimHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberClaimHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberClaimHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        public void MemberClaimHistoryGrid_ManualDataRebind () {

            MemberClaimHistoryGrid_Count = 0;

            MemberClaimHistoryGrid.DataSource = null;

            MemberClaimHistoryGrid.Rebind ();
            
            return;

        }

        #endregion


        #region Member Pharmacy Claim History Grid Events

        protected void MemberPharmacyClaimHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            #region Validate Call

            if (MercuryApplication == null) { return; }

            if (Member == null) { return; }

            #endregion 
            

            Boolean needsData = false;

            needsData = needsData || (MemberPharmacyClaimHistoryGrid_Count == 0);

            needsData = needsData || (MemberPharmacyClaimHistoryGrid_DataTable.Rows.Count == 0);

            needsData = needsData || (MemberPharmacyClaimHistoryGrid_PageSize != MemberPharmacyClaimHistoryGrid.PageSize);

            needsData = needsData || (MemberPharmacyClaimHistoryGrid_CurrentPage != MemberPharmacyClaimHistoryGrid.CurrentPageIndex);

            
            System.Data.DataTable dataTable = MemberPharmacyClaimHistoryGrid_DataTable;

            if (needsData) {

                MemberPharmacyClaimHistoryGrid.PageSize = MemberPharmacyClaimHistoryGrid_PageSize;

                dataTable.Rows.Clear ();

                if (MemberPharmacyClaimHistoryGrid_Count == 0) {

                    MemberPharmacyClaimHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.MemberPharmacyClaimsGetCount (Member.Id));

                    MemberPharmacyClaimHistoryGrid.VirtualItemCount = Convert.ToInt32 (MemberPharmacyClaimHistoryGrid_Count);

                }

                MemberPharmacyClaimHistoryGrid_CurrentPage = MemberPharmacyClaimHistoryGrid.CurrentPageIndex;

                List<Mercury.Server.Application.PharmacyClaim> memberClaims;

                Int32 initialRow = MemberPharmacyClaimHistoryGrid.CurrentPageIndex * MemberPharmacyClaimHistoryGrid.PageSize + 1;

                memberClaims = MercuryApplication.MemberPharmacyClaimsGetByPage (Member.Id, initialRow, MemberPharmacyClaimHistoryGrid.PageSize);

                foreach (Server.Application.PharmacyClaim currentClaim in memberClaims) {

                    String serviceProviderInformation = "<span title=\"" + currentClaim.ServiceProviderSpecialtyName + "\">" + currentClaim.ServiceProviderName + "</span>";

                    dataTable.Rows.Add (

                        currentClaim.ClaimId.ToString (),

                        currentClaim.MemberId.ToString (),

                        currentClaim.ClaimDateFrom.ToString ("MM/dd/yyyy"),

                        currentClaim.ClaimDateThru.ToString ("MM/dd/yyyy"),

                        currentClaim.Status,

                        currentClaim.NdcCode,

                        currentClaim.DrugName,

                        currentClaim.DaysSupply.ToString ("########0.00"),

                        currentClaim.DeaClassification,

                        currentClaim.TherapeuticClassification,

                        currentClaim.PharmacyName,

                        (!String.IsNullOrEmpty (currentClaim.ServiceProviderName)) ? serviceProviderInformation : "&nbsp",

                        currentClaim.ServiceProviderSpecialtyName,

                        currentClaim.BilledAmount.ToString ("$###,###,##0.00"),

                        currentClaim.PaidAmount.ToString ("$###,###,##0.00"),

                        currentClaim.ExternalClaimId

                    );

                }

                MemberPharmacyClaimHistoryGrid_DataTable = dataTable;

            }

            MemberPharmacyClaimHistoryGrid.DataSource = MemberPharmacyClaimHistoryGrid_DataTable;

            return;

        }

        protected void MemberPharmacyClaimHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberPharmacyClaimHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberPharmacyClaimHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberPharmacyClaimHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        public void MemberPharmacyClaimHistoryGrid_ManualDataRebind () {

            MemberPharmacyClaimHistoryGrid_Count = 0;

            MemberPharmacyClaimHistoryGrid.DataSource = null;

            MemberPharmacyClaimHistoryGrid.Rebind ();

            return;

        }

        #endregion


        #region Member Pharmacy Claim History Grid Events

        protected void MemberLabResultHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            #region Validate Call

            if (MercuryApplication == null) { return; }

            if (Member == null) { return; }

            #endregion


            Boolean needsData = false;

            needsData = needsData || (MemberLabResultHistoryGrid_Count == 0);

            needsData = needsData || (MemberLabResultHistoryGrid_DataTable.Rows.Count == 0);

            needsData = needsData || (MemberLabResultHistoryGrid_PageSize != MemberLabResultHistoryGrid.PageSize);

            needsData = needsData || (MemberLabResultHistoryGrid_CurrentPage != MemberLabResultHistoryGrid.CurrentPageIndex);


            System.Data.DataTable dataTable = MemberLabResultHistoryGrid_DataTable;

            if (needsData) {

                MemberLabResultHistoryGrid.PageSize = MemberLabResultHistoryGrid_PageSize;

                dataTable.Rows.Clear ();

                if (MemberLabResultHistoryGrid_Count == 0) {

                    MemberLabResultHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.MemberLabResultsGetCount (Member.Id));

                    MemberLabResultHistoryGrid.VirtualItemCount = Convert.ToInt32 (MemberLabResultHistoryGrid_Count);

                }

                MemberLabResultHistoryGrid_CurrentPage = MemberLabResultHistoryGrid.CurrentPageIndex;

                List<Mercury.Server.Application.LabResult> memberLabs;

                Int32 initialRow = MemberLabResultHistoryGrid.CurrentPageIndex * MemberLabResultHistoryGrid.PageSize + 1;

                memberLabs = MercuryApplication.MemberLabResultsGetByPage (Member.Id, initialRow, MemberLabResultHistoryGrid.PageSize);

                foreach (Server.Application.LabResult currentLab in memberLabs) {

                    dataTable.Rows.Add (

                        currentLab.Id,

                        currentLab.LabReferenceNumber,

                        currentLab.MemberId,

                        currentLab.ProviderId,

                        currentLab.ClaimId,

                        currentLab.ServiceDate.ToString ("MM/dd/yyyy"),

                        currentLab.Loinc,

                        currentLab.LabTestName,

                        currentLab.LabValue.ToString ("0.####"),

                        currentLab.LabUnitType,

                        currentLab.LabResultText

                    );

                }

                MemberLabResultHistoryGrid_DataTable = dataTable;

            }

            MemberLabResultHistoryGrid.DataSource = MemberLabResultHistoryGrid_DataTable;

            return;

        }

        protected void MemberLabResultHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberLabResultHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberLabResultHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberLabResultHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        public void MemberLabResultHistoryGrid_ManualDataRebind () {

            MemberLabResultHistoryGrid_Count = 0;

            MemberLabResultHistoryGrid.DataSource = null;

            MemberLabResultHistoryGrid.Rebind ();

            return;

        }

        #endregion

    }

}