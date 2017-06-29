using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.MemberCase {

    public partial class MemberCaseAuditHistory : System.Web.UI.UserControl {


        #region Private Properties

        private Boolean pageSizeChanged = false;

        #endregion


        #region Session Properties

        public String SessionCachePrefix {

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

        public Client.Core.Individual.Case.MemberCase Case {

            get { return (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"]; }

            set {

                Client.Core.Individual.Case.MemberCase memberCase = (Client.Core.Individual.Case.MemberCase)Session[SessionCachePrefix + "MemberCase"];

                memberCase = value;

                Session[SessionCachePrefix + "MemberCase"] = value;

                MemberCaseAuditHistoryGrid.DataSource = null;

                MemberCaseAuditHistoryGrid.Rebind ();

            }

        }

        public String InstanceId { get { return UserControlInstanceId.Text; } set { UserControlInstanceId.Text = value; } }

        public Int32 HistoryGridPageSize { get { return MemberCaseAuditHistoryGrid.PageSize; } set { MemberCaseAuditHistoryGrid.PageSize = value; } }

        private Int32 MemberCaseAuditHistoryGrid_CurrentPage {

            get {

                Int32 currentPage = -1;

                if (Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_CurrentPage"] != null) {

                    currentPage = (Int32)Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_CurrentPage"];

                }

                return currentPage;

            }

            set { Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_CurrentPage"] = value; }

        }

        private Int32 MemberCaseAuditHistoryGrid_PageSize {

            get {

                Int32 pageSize = 10;

                if (Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"] != null) {

                    pageSize = (Int32)Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"];

                }

                return pageSize;

            }

            set {

                // INITIAL PAGE SIZE SETTING

                if (Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"] == null) {

                    Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"] = value;

                }

                // VALIDATE IF TRUE PAGE CHANGE

                else if (((Int32)Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"]) != value) {

                    Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_PageSize"] = value;

                    pageSizeChanged = true;

                }

            }

        }

        private Int32 MemberCaseAuditHistoryGrid_Count {

            get {

                Int32 count = 0;

                if (Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_Count"] != null) {

                    count = (Int32)Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_Count"];

                }

                return count;

            }

            set { Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_Count"] = value; }

        }


        private List <Client.Core.Individual.Case.MemberCaseAudit> MemberCaseAuditList {

            get {

                List<Client.Core.Individual.Case.MemberCaseAudit> memberCaseAuditList = (List<Client.Core.Individual.Case.MemberCaseAudit>)Session[SessionCachePrefix + "MemberCaseAuditList"];

                if (memberCaseAuditList == null) { memberCaseAuditList = new List<Client.Core.Individual.Case.MemberCaseAudit> (); }

                return memberCaseAuditList;

            }

            set { Session[SessionCachePrefix + "MemberCaseAuditList"] = value; }

        }

        private System.Data.DataTable MemberCaseAuditHistoryGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable)Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("Id");

                    dataTable.Columns.Add ("AuditObjectType");

                    dataTable.Columns.Add ("AuditObjectId");

                    dataTable.Columns.Add ("Description");

                    dataTable.Columns.Add ("SourceObjectType");

                    dataTable.Columns.Add ("SourceObjectId");

                    dataTable.Columns.Add ("UserDisplayName");

                    dataTable.Columns.Add ("CreateAccountInfo.ActionDate");

                    Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "MemberCaseAuditHistoryGrid_DataTable"] = value; }

        }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (Case == null) { return; }

            //InitializeAll ();

            return;

        }

        #endregion


        #region Initializations

        protected void MemberCaseAuditHistoryGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            System.Data.DataTable dataTable = MemberCaseAuditHistoryGrid_DataTable;

            if (!eventArgs.IsFromDetailTable) {

                switch (eventArgs.RebindReason) {

                    case Telerik.Web.UI.GridRebindReason.InitialLoad:


                        #region Initialize Grid

                        MemberCaseAuditHistoryGrid_Count = 0;

                        MemberCaseAuditHistoryGrid_CurrentPage = 0;

                        MemberCaseAuditHistoryGrid_PageSize = 10;


                        MemberCaseAuditHistoryGrid.CurrentPageIndex = MemberCaseAuditHistoryGrid_CurrentPage;

                        MemberCaseAuditHistoryGrid.PageSize = MemberCaseAuditHistoryGrid_PageSize;

                        MemberCaseAuditHistoryGrid.VirtualItemCount = MemberCaseAuditHistoryGrid_Count;

                        #endregion


                        break;

                    case Telerik.Web.UI.GridRebindReason.PostbackViewStateNotPersisted:


                        #region Restore Grid State

                        MemberCaseAuditHistoryGrid.CurrentPageIndex = MemberCaseAuditHistoryGrid_CurrentPage;

                        MemberCaseAuditHistoryGrid.PageSize = MemberCaseAuditHistoryGrid_PageSize;

                        MemberCaseAuditHistoryGrid.VirtualItemCount = MemberCaseAuditHistoryGrid_Count;

                        #endregion


                        break;

                    case Telerik.Web.UI.GridRebindReason.ExplicitRebind:

                    case Telerik.Web.UI.GridRebindReason.PostBackEvent:


                        #region Rebind Grid

                        if (Case == null) { dataTable.Rows.Clear (); }

                        else {

                            if (MemberCaseAuditHistoryGrid_Count == 0) {

                                MemberCaseAuditHistoryGrid_Count = Convert.ToInt32 (MercuryApplication.MemberCaseAuditHistoryGetCount (Case.Id, false));

                                MemberCaseAuditHistoryGrid.VirtualItemCount = MemberCaseAuditHistoryGrid_Count;

                            }

                            if (!pageSizeChanged) {

                                MemberCaseAuditHistoryGrid_PageSize = MemberCaseAuditHistoryGrid.PageSize;

                            }

                            else {

                                MemberCaseAuditHistoryGrid.PageSize = MemberCaseAuditHistoryGrid_PageSize;

                                pageSizeChanged = false;

                            }

                            MemberCaseAuditHistoryGrid_CurrentPage = MemberCaseAuditHistoryGrid.CurrentPageIndex;

                            dataTable.Rows.Clear ();

                            List<Client.Core.Individual.Case.MemberCaseAudit> memberCaseAuditHistory;

                            Int64 initialRow = MemberCaseAuditHistoryGrid.CurrentPageIndex * MemberCaseAuditHistoryGrid.PageSize + 1;

                            memberCaseAuditHistory = MercuryApplication.MemberCaseAuditHistoryGetByMemberCaseIdPage (Case.Id, initialRow, MemberCaseAuditHistoryGrid.PageSize, false);

                            MemberCaseAuditList = memberCaseAuditHistory;

                            foreach (Client.Core.Individual.Case.MemberCaseAudit currentMemberCaseAudit in memberCaseAuditHistory) {

                                if (dataTable.Rows.Count < MemberCaseAuditHistoryGrid.PageSize) {

                                dataTable.Rows.Add (

                                    currentMemberCaseAudit.Id,

                                    currentMemberCaseAudit.AuditObjectType,

                                    currentMemberCaseAudit.AuditObjectId,

                                    currentMemberCaseAudit.Description,

                                    currentMemberCaseAudit.SourceObjectType,

                                    currentMemberCaseAudit.SourceObjectId,

                                    currentMemberCaseAudit.UserDisplayName,

                                    currentMemberCaseAudit.CreateAccountInfo.ActionDate

                                    );

                                }

                            } /* END FOREACH */

                        }

                        #endregion


                        break;

                    default:

                        System.Diagnostics.Debug.WriteLine (eventArgs.RebindReason + " [" + eventArgs.IsFromDetailTable.ToString () + "]");

                        break;

                }

            }

            MemberCaseAuditHistoryGrid_DataTable = dataTable;

            MemberCaseAuditHistoryGrid.DataSource = MemberCaseAuditHistoryGrid_DataTable;

            return;

        }

        #endregion


        #region Control Events

        protected void MemberCaseAuditHistoryGrid_OnPageSizeChanged (Object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs eventArgs) {

            if (MemberCaseAuditHistoryGrid_PageSize != eventArgs.NewPageSize) {

                MemberCaseAuditHistoryGrid_PageSize = eventArgs.NewPageSize;

                MemberCaseAuditHistoryGrid_ManualDataRebind ();

            }

            return;

        }

        public void MemberCaseAuditHistoryGrid_ManualDataRebind () {

            MemberCaseAuditHistoryGrid_Count = 0;

            MemberCaseAuditHistoryGrid.DataSource = null;

            MemberCaseAuditHistoryGrid.Rebind ();

            return;

        }

        #endregion


    }

}