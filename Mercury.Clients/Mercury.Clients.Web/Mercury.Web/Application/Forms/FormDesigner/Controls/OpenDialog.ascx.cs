using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Forms.FormDesigner.Controls {

    public partial class OpenDialog : System.Web.UI.UserControl {

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

        private System.Data.DataTable FormsGrid_DataTable {

            get {

                System.Data.DataTable dataTable = (System.Data.DataTable) Session[SessionCachePrefix + "FormsGrid_DataTable"];

                if (dataTable == null) {

                    dataTable = new System.Data.DataTable ();

                    dataTable.Columns.Add ("FormId");

                    dataTable.Columns.Add ("FormName");

                    dataTable.Columns.Add ("ModifiedAccountName");

                    dataTable.Columns.Add ("ModifiedDate");

                    Session[SessionCachePrefix + "FormsGrid_DataTable"] = dataTable;

                }

                return dataTable;

            }

            set { Session[SessionCachePrefix + "FormsGrid_DataTable"] = value; }

        }

        #endregion 


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            Refresh ();

            return;

        }

        #endregion 

        
        protected void FormsGrid_OnNeedDataSource (Object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs eventArgs) {

            if (MercuryApplication == null) { return; }

            FormsGrid.DataSource = FormsGrid_DataTable;

            return;

        }

        #region Public Methods

        public void Refresh () {

            if (MercuryApplication == null) { return; }


            System.Data.DataTable formsAvailableTable = FormsGrid_DataTable;

            formsAvailableTable.Rows.Clear ();

            foreach (Mercury.Server.Application.SearchResultFormHeader currentForm in MercuryApplication.FormsAvailable (false)) {

                String anchorText = "<a href=\"/Application/Forms/FormDesigner/FormDesigner.aspx?FormId=" + currentForm.Id.ToString () + "\" alt=\"Open Form: " + currentForm.Name + "\">" + currentForm.Name + "</a>";

                formsAvailableTable.Rows.Add (

                    currentForm.Id.ToString (),

                    anchorText,

                    currentForm.ModifiedAccountInfo.UserAccountName,

                    currentForm.ModifiedAccountInfo.ActionDate.ToString ("MM/dd/yyyy")

                    );

            }

            FormsGrid_DataTable = formsAvailableTable;


            FormsGrid.DataSource = null;

            FormsGrid.Rebind ();

            return;

        }

        #endregion 

    }

}