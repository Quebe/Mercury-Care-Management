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

    public partial class ConfigurationImport : System.Web.UI.Page {

        private Mercury.Client.Application MercuryApplication {

            get {

                Mercury.Client.Application application = (Mercury.Client.Application) Session["Mercury.Application"];

                if (application == null) { Response.Redirect ("/SessionExpired.aspx", true); }

                return application;

            }

        }
        
        protected System.Xml.XmlDocument configurationDocument;

        protected void Page_Load (object sender, EventArgs e) {

            if (MercuryApplication == null) { return; }

            if (!MercuryApplication.HasEnvironmentPermission (Mercury.Server.EnvironmentPermissions.ConfigurationImportExport)) {

                if (!IsPostBack) { Server.Transfer ("/PermissionDenied.aspx"); }

                else { Response.RedirectLocation = "/PermissionDenied.aspx"; }

                return;

            }

            return;

        }


        protected void Page_Unload (object sender, EventArgs e) {

            MercuryApplication.ApplicationClientClose ();

            return;

        }


        protected void ButtonUploadFile_OnClick (Object sender, EventArgs eventArgs) {

            Boolean validFile = true;


            SaveResponseLabel.Text = String.Empty;

            if (TelerikUpload.UploadedFiles.Count != 1) {

                validFile = false;

                SaveResponseLabel.Text = "Invalid number of files detected.";

            }

            else {

                try {

                    configurationDocument = new System.Xml.XmlDocument ();

                    System.IO.StreamReader streamReader = new System.IO.StreamReader (TelerikUpload.UploadedFiles[0].InputStream);

                    configurationDocument.LoadXml (streamReader.ReadToEnd ());

                }

                catch (Exception applicationException) {

                    System.Diagnostics.Debug.WriteLine (applicationException.Message);

                    validFile = false;

                    SaveResponseLabel.Text = "Invalid Configuration File.";

                }

            }


            if (!validFile) {

                FileUploadSection.Style.Add ("display", "inline");

                ConfigurationProcessSection.Style.Add ("display", "none");

            }

            else {

                Mercury.Server.Application.ImportExportResponse importResponse;

                importResponse = MercuryApplication.CoreObject_XmlImport (configurationDocument.OuterXml);

            
                System.Data.DataTable importResultsTable = new DataTable ();

                importResultsTable.Columns.Add ("ObjectType");

                importResultsTable.Columns.Add ("ObjectName");

                importResultsTable.Columns.Add ("Success");

                importResultsTable.Columns.Add ("Exception");

                importResultsTable.Columns.Add ("Id");

                if (MercuryApplication.LastException == null) {

                    foreach (Mercury.Server.Application.ImportExportResult currentResult  in importResponse.Results) {

                        importResultsTable.Rows.Add (

                            currentResult.ObjectType,

                            currentResult.ObjectName,

                            currentResult.Success.ToString (),

                            (currentResult.HasException) ? currentResult.Exception.Message : String.Empty,

                            currentResult.Id.ToString ()

                        );

                    }

                }

                else {

                    importResultsTable.Rows.Add ("Error", "Import Error", "False", MercuryApplication.LastException.Message, 0);

                }

                ImportResultsGrid.DataSource = importResultsTable;

                ImportResultsGrid.Rebind ();

            }

            return;

        }

        protected void ButtonClose_OnClick (Object sender, EventArgs eventArgs) {

            Server.Transfer ("/WindowClose.aspx");

        }

    }

}

/*


                    <Telerik:RadProgressManager ID="TelerikUploadProgressManager" runat="server" />

                        <Telerik:RadProgressArea ID="TelerikUploadProgressArea" DisplayCancelButton="true" runat="server" />

*/