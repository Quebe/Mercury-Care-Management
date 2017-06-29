using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mercury.Web.Application.Automation {

    public partial class Automation : System.Web.UI.Page {

        #region Private Properties

        private Boolean useCaching = true;

        #endregion


        #region Private Session States

        private Mercury.Client.Application MercuryApplication { get { return Master.MercuryApplication; } }

        private String SessionCachePrefix { get { return Master.SessionCachePrefix; } }

        private Boolean UseCaching { get { return ((useCaching) && (IsPostBack)); } set { useCaching = value; } }

        #endregion


        #region Page Events

        protected void Page_Load (object sender, EventArgs e) {

            if (!IsPostBack) {

                InitializeCorrespondenceAvailable ();

                InitializePrintersAvailable ();

                InitializeJobDescriptionGrid ();

            }

            return;

        }

        #endregion


        #region Initializations

        private void InitializeCorrespondenceAvailable () {

            List<Client.Core.Reference.Correspondence> correspondenceAll = MercuryApplication.CorrespondencesAvailable (true);

            List<Client.Core.Reference.Correspondence> correspondenceAvailable = new List<Client.Core.Reference.Correspondence> ();

            

            foreach (Client.Core.Reference.Correspondence currentCorrespondence in correspondenceAll) {

                if ((currentCorrespondence.Content.Count > 0) && (currentCorrespondence.Enabled) && (currentCorrespondence.Visible)) {

                    correspondenceAvailable.Add (currentCorrespondence);

                }

            }


            JobCorrespondenceSelection.Items.Clear ();

            JobCorrespondenceSelection.DataSource = correspondenceAvailable;

            JobCorrespondenceSelection.DataTextField = "Name";

            JobCorrespondenceSelection.DataValueField = "Id";
            
            JobCorrespondenceSelection.DataBind ();

            return;

        }

        private void InitializePrintersAvailable () {

            List<Client.Printing.Printer> printersAll = MercuryApplication.PrintersAvailable (true);

            List<Client.Printing.Printer> printersAvailable = new List<Client.Printing.Printer> ();


            foreach (Client.Printing.Printer currentPrinter in printersAll) {

                if ((currentPrinter.Enabled) && (currentPrinter.Visible)) {

                    printersAvailable.Add (currentPrinter);

                }

            }


            JobPrinterSelection.Items.Clear ();

            JobPrinterSelection.DataSource = printersAvailable;

            JobPrinterSelection.DataTextField = "Name";

            JobPrinterSelection.DataValueField = "Id";

            JobPrinterSelection.DataBind ();

            return;

        }

        private void InitializeJobDescriptionGrid () {

            System.Data.DataTable jobDescriptionTable = new System.Data.DataTable ();

            jobDescriptionTable.Columns.Add ("CorrespondenceId");

            jobDescriptionTable.Columns.Add ("CorrespondenceName");

            jobDescriptionTable.Columns.Add ("CorrespondenceContentId");

            jobDescriptionTable.Columns.Add ("CorrespondenceContentDescription");

            jobDescriptionTable.Columns.Add ("PrinterResolution");

            jobDescriptionTable.Columns.Add ("PrinterColor");

            jobDescriptionTable.Columns.Add ("PrinterInputBin");

            jobDescriptionTable.Columns.Add ("PrinterOutputBin");


            JobDescriptionGrid.DataSource = jobDescriptionTable;

            JobDescriptionGrid.DataBind ();

            return;

        }

        #endregion


        #region Job Description Events

        protected void JobCorrespondenceSelection_OnItemCheck (Object sender, Telerik.Web.UI.RadListBoxItemEventArgs e) {

            InitializeJobDescriptionGrid ();

            System.Data.DataTable jobDescriptionTable = (System.Data.DataTable)JobDescriptionGrid.DataSource;


            Client.Core.Reference.Correspondence selectedCorrespondence = MercuryApplication.CorrespondenceGet (Convert.ToInt64 (e.Item.Value), true);

            if (selectedCorrespondence != null) {

                foreach (Client.Core.Reference.CorrespondenceContent currentContent in selectedCorrespondence.Content.Values) {

                    jobDescriptionTable.Rows.Add (

                        selectedCorrespondence.Id,

                        selectedCorrespondence.Name,

                        currentContent.Id,

                        currentContent.ReportNameRaw,

                        String.Empty,

                        String.Empty,

                        String.Empty,

                        String.Empty

                        );

                }

            }


            JobDescriptionGrid.Rebind ();

            return;

        }

        protected void JobCorrespondenceSelection_OnReordered (Object sender, Telerik.Web.UI.RadListBoxEventArgs e) {

            return;

        }

        protected void JobPrinterSelection_OnSelectedIndexChanged (Object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e) {

            Client.Printing.Printer selectedPrinter = MercuryApplication.PrinterGet (Convert.ToInt64 (e.Value), true);

            Mercury.Server.Application.PrinterCapabilities printerCapabilities =

                MercuryApplication.PrinterCapabilitiesGet (selectedPrinter.PrintServerName, selectedPrinter.PrintQueueName, true);






            return;

        }

        #endregion 

    }

}